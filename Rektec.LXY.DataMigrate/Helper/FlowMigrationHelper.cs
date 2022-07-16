#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/2 11:34:57
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Rektec.LXY.DataMigrate.Model;
using Rektec.LXY.Flow.Helper;
using RekTec.Crm.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rektec.LXY.DataMigrate.Helper
{
    /// <summary>
    /// 记录日志委托
    /// </summary>
    /// <param name="msg">日志信息</param>
    /// <param name="isImportant">是否重要信息</param>
    public delegate void LogDelegate(string msg, bool isImportant);

    public class FlowMigrationHelper
    {
        public event LogDelegate logDelegate;

        /// <summary>
        /// 组织服务A
        /// </summary>
        private readonly IOrganizationService OrganizationServiceA;
        /// <summary>
        /// 组织服务B
        /// </summary>
        private readonly IOrganizationService OrganizationServiceB;

        /// <summary>
        /// 是否匹配流程【两个环境的流程主键一致则不需要匹配】
        /// </summary>
        private bool IsMatchWorkFlow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverInfoA">签核流程存在的环境信息</param>
        /// <param name="serverInfoB">需要导入的环境信息</param>
        /// <param name="isMatchWorkFlow">是否匹配流程【两个环境的流程主键一致则不需要匹配】</param>
        public FlowMigrationHelper(ServerInfo serverInfoA, ServerInfo serverInfoB, bool isMatchWorkFlow)
        {
            this.OrganizationServiceA = CommonHelper.GetOrganizationService(serverInfoA);
            this.OrganizationServiceB = CommonHelper.GetOrganizationService(serverInfoB);
            this.IsMatchWorkFlow = isMatchWorkFlow;
        }

        /// <summary>
        /// 签核流程迁移【A环境迁移到B环境】
        /// </summary> 
        /// <param name="filterXml">过滤条件，格式：【<filter type='and'><condition attribute='new_type' operator='eq' value='1' /></filter>】</param>
        /// <param name="isImportDetail">是否导入明细</param>
        public void FlowAToB(string filterXml, bool isImportDetail = false)
        {
            try
            {
                this.FlowAToBHandle(filterXml, isImportDetail);
            }
            catch (Exception ex)
            {
                this.Log("导入签核流程异常：", true, ex);
            }

            this.Log("导入签核流程结束", false);
        }

        /// <summary>
        /// 签核流程迁移【A环境迁移到B环境】
        /// </summary> 
        /// <param name="filterXml">过滤条件，格式：【<filter type='and'><condition attribute='new_type' operator='eq' value='1' /></filter>】</param>
        /// <param name="isImportDetail">是否导入明细</param>
        public void FlowAToBHandle(string filterXml, bool isImportDetail)
        {
            string fetchXml = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>"
                           + "  <entity name='new_flow'>"
                           + "   <all-attributes />"
                           + filterXml
                           + "  </entity>"
                           + "</fetch>";

            //查询A环境符合条件的签核流程
            EntityCollection entityListA = OrganizationServiceA.RetrieveMultiple(new FetchExpression(fetchXml));

            this.Log($"符合条件的记录数：{entityListA.Entities.Count}", false);
            if (entityListA.Entities.Count <= 0)
                return;

            //查询B环境符合条件的签核流程
            EntityCollection entityListB = OrganizationServiceB.RetrieveMultiple(new FetchExpression(fetchXml));
            foreach (Entity entityB in entityListB.Entities)
            {
                var new_flow_id = entityB.GetAttributeValue<EntityReference>("new_flow_id");
                if (new_flow_id == null)//通过Excel获取其他工具导入时，可能会存在有问题的数据
                {
                    //停用主签核流程为空的签核流程（因为这个在发布的时候不会停用）
                    var req = new SetStateRequest
                    {
                        EntityMoniker = entityB.ToEntityReference(),
                        State = new OptionSetValue(1),
                        Status = new OptionSetValue(2)
                    };
                    OrganizationServiceB.Execute(req);

                    this.Log($"停用主签核流程为空的签核流程【{ entityB.GetAttributeValue<string>("new_name")}】", true);
                }
            }

            foreach (Entity entityA in entityListA.Entities)
            {
                string new_name = entityA.GetAttributeValue<string>("new_name");
                string new_flowdesign = entityA.GetAttributeValue<string>("new_flowdesign");
                string new_flowdesign_new = new_flowdesign;

                #region 获取新的签核设计
                try
                {
                    new_flowdesign_new = FlowDesignHelper.GetNewFlowDesign(OrganizationServiceA, OrganizationServiceB, entityA, this.IsMatchWorkFlow, out string msg, out string imortantMsg);

                    this.Log(msg, false);
                    this.Log(imortantMsg, true);
                }
                catch (Exception ex)
                {
                    this.Log($"签核流程【{new_name}】获取新的签核设计失败：{ex.Message}", true);
                    return;
                }
                #endregion

                #region 创建新的签核流程
                Entity entityNewB = CommonHelper.CopyEntity(entityA);

                #region 匹配相应动作（避免不存在报错）   
                if (IsMatchWorkFlow)
                {
                    string new_presubmitaction = entityA.GetAttributeValue<string>("new_presubmitaction");//提交前动作名称
                    string new_submitaction = entityA.GetAttributeValue<string>("new_submitaction");//提交后动作名称 
                    string new_recallaction = entityA.GetAttributeValue<string>("new_recallaction");//撤回动作名称 
                    string new_approveaction = entityA.GetAttributeValue<string>("new_approveaction");//同意动作名称 
                    string new_rejectaction = entityA.GetAttributeValue<string>("new_rejectaction");//驳回动作名称  
                    this.WorkFlowHandle("new_presubmitworkflow_id", new_presubmitaction, entityA, entityNewB, new_name, "提交前动作"); //提交前动作  
                    this.WorkFlowHandle("new_submitworkflow_id", new_submitaction, entityA, entityNewB, new_name, "提交后动作"); //提交后动作   
                    this.WorkFlowHandle("new_recallworkflow_id", new_recallaction, entityA, entityNewB, new_name, "撤回动作"); //撤回动作   
                    this.WorkFlowHandle("new_agreeworkflow_id", new_approveaction, entityA, entityNewB, new_name, "同意动作"); //同意动作   
                    this.WorkFlowHandle("new_rejectworkflow_id", new_rejectaction, entityA, entityNewB, new_name, "驳回动作"); //驳回动作    

                    this.WorkFlowHandle("new_condition_workflowid", string.Empty, entityA, entityNewB, new_name, "提交前动作"); //操作名称  
                }
                #endregion

                Entity selectEntity = entityListB.Entities.FirstOrDefault(x => x.GetAttributeValue<string>("new_name") == new_name);
                EntityReference new_flow_id = null;//主签核流程
                if (selectEntity != null)
                    new_flow_id = selectEntity.GetAttributeValue<EntityReference>("new_flow_id");

                entityNewB["new_flow_id"] = new_flow_id;
                if (new_flowdesign_new != new_flowdesign)
                {
                    entityNewB["new_flowdesign"] = new_flowdesign_new;
                    entityNewB["new_flowdesigndraft"] = new_flowdesign_new;
                }
                //流程状态  1 草稿 2 已发布
                entityNewB["new_flowstatus"] = new OptionSetValue(1);
                OrganizationServiceB.Create(entityNewB);

                this.Log($"创建签核流程：【{new_name}】", false);
                #endregion

                if (isImportDetail)
                {
                    #region 导入签核知会  
                    //获取A环境的签核知会列表
                    EntityCollection flownotifyList = this.GetFlowDetailListFromA("new_flownotify", entityA.Id);
                    int successCount = 0;
                    foreach (Entity flownotifyA in flownotifyList.Entities)
                    {
                        try
                        {
                            //创建签核知会
                            Entity flownotifyNewB = CommonHelper.CopyEntity(flownotifyA);
                            flownotifyNewB["new_flowid"] = entityNewB.ToEntityReference();

                            OptionSetValue new_usertype = flownotifyA.GetAttributeValue<OptionSetValue>("new_usertype");
                            if (new_usertype != null)
                            {
                                //用户类型   1 单据负责人 2 签核角色 3 特定用户 4 上级主管 9 自定义 
                                if (new_usertype.Value == 2)
                                {
                                    this.FlowNotifyRoleHandle(flownotifyA, flownotifyNewB);
                                }
                                else if (new_usertype.Value == 9)
                                {
                                    this.NotifyUserWorkflowHandle(flownotifyA, flownotifyNewB, new_name);
                                }
                                else
                                {
                                    flownotifyNewB["new_notify_roleid"] = null;
                                    flownotifyNewB["new_notifyuser_workflowid"] = null;
                                }
                            }

                            OrganizationServiceB.Create(flownotifyNewB);
                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            this.Log($"导入签核知会【{flownotifyA.Id}】异常：", true, ex);
                        }
                    }
                    this.Log($"导入签核知会记录数：{successCount},失败数：{flownotifyList.Entities.Count - successCount}", false);

                    #endregion

                    #region 导入签核提醒  
                    //获取A环境的签核提醒列表
                    EntityCollection notificationList = this.GetFlowDetailListFromA("new_flow_notification", entityA.Id);
                    successCount = 0;
                    foreach (Entity notificationA in notificationList.Entities)
                    {
                        try
                        {
                            //创建签核知会
                            Entity notificationNewB = CommonHelper.CopyEntity(notificationA);
                            notificationNewB["new_flowid"] = entityNewB.ToEntityReference();

                            //执行动作 
                            EntityReference new_actionworkflow_id = notificationA.GetAttributeValue<EntityReference>("new_actionworkflow_id");
                            if (new_actionworkflow_id != null)
                            {
                                Entity notifyUserWorkflowA = OrganizationServiceA.Retrieve(new_actionworkflow_id.LogicalName, new_actionworkflow_id.Id, new ColumnSet("uniquename"));
                                string uniquename = notifyUserWorkflowA.GetAttributeValue<string>("uniquename");
                                var queryExpression = new QueryExpression(new_actionworkflow_id.LogicalName);
                                queryExpression.ColumnSet.AddColumns("uniquename");
                                queryExpression.Criteria.AddCondition("uniquename", ConditionOperator.Equal, uniquename);
                                EntityCollection notifyUserWorkflowListB = OrganizationServiceB.RetrieveMultiple(queryExpression);
                                if (notifyUserWorkflowListB.Entities.Count > 0)
                                {
                                    notificationNewB["new_actionworkflow_id"] = notifyUserWorkflowListB.Entities[0].ToEntityReference();
                                }
                                else
                                {
                                    //动作不存在则赋值null
                                    notificationNewB["new_actionworkflow_id"] = null;
                                }
                            }

                            OrganizationServiceB.Create(notificationNewB);
                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            this.Log($"导入签核提醒异常：", true, ex);
                        }
                    }
                    this.Log($"导入签核提醒记录数：{successCount},失败数：{notificationList.Entities.Count - successCount}", false);

                    #endregion
                }

                try
                {
                    //发布签核流程
                    var data = new Dictionary<string, object> { { "flowId", entityNewB.Id } };
                    CrmHelper.InvokeHiddenApi(OrganizationServiceB, "new_flow", "FlowDesign/PublishFlow", data);

                    this.Log($"发布签核流程：【{new_name}】", false);
                }
                catch (Exception ex)
                {
                    this.Log($"导入成功，但是发布签核流程【{new_name}】异常：", true, ex);
                }

                this.Log($"------------------------------------------------------------------", false);
            }

            try
            {
                //初始化签核流程
                CrmHelper.InvokeHiddenApi(OrganizationServiceB, "new_flow", "FlowDesign/InitializeFlowDesign", new Dictionary<string, object> { });
            }
            catch (Exception ex)
            {
                this.Log($"导入成功，但是初始化签核流程异常：", true, ex);
            }
        }

        /// <summary>
        /// 匹配相应的流程
        /// </summary>
        /// <param name="field">流程字段</param>
        /// <param name="uniquename">流程唯一名称</param>
        /// <param name="entityA"></param>
        /// <param name="entityNewB"></param>
        /// <param name="new_flow_name"></param>
        /// <param name="tip"></param>
        private void WorkFlowHandle(string field, string uniquename, Entity entityA, Entity entityNewB, string new_flow_name, string tip)
        {
            EntityReference new_workflow_id = entityA.GetAttributeValue<EntityReference>(field);//动作
            if (new_workflow_id != null)
            {
                if (!string.IsNullOrEmpty(uniquename))
                {
                    uniquename = uniquename.Replace("new_", "");
                }
                else
                {
                    Entity workflowInfo = OrganizationServiceA.Retrieve(new_workflow_id.LogicalName, new_workflow_id.Id, new ColumnSet("uniquename"));
                    uniquename = workflowInfo.GetAttributeValue<string>("uniquename");//唯一名称
                }

                var queryExpression = new QueryExpression(new_workflow_id.LogicalName);
                queryExpression.ColumnSet.AddColumns("uniquename");

                queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 1); //激活状态
                queryExpression.Criteria.AddCondition("parentworkflowid", ConditionOperator.Null); //父流程 ID
                queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, new_workflow_id.Name); //流程名称
                if (!string.IsNullOrEmpty(uniquename))
                    queryExpression.Criteria.AddCondition("uniquename", ConditionOperator.Equal, uniquename); //唯一名称
                EntityCollection data = OrganizationServiceB.RetrieveMultiple(queryExpression);
                if (data.Entities.Count > 0)
                {
                    if (data.Entities.Count > 1)
                        this.Log($"签核流程：【{new_flow_name}】的的{tip}匹配到多个流程{new_workflow_id.Name},已将{tip}设置成{ data.Entities[0].Id}", true);

                    entityNewB[field] = data.Entities[0].ToEntityReference();
                }
                else
                {
                    entityNewB[field] = null;
                    this.Log($"导入错误：签核流程【{new_flow_name}】的{tip}【{new_workflow_id.Name}】不存在，需要手动维护", true);
                }
            }
        }

        /// <summary>
        /// 签核知会的自定义用户导入处理
        /// </summary>
        /// <param name="flownotifyA"></param>
        /// <param name="flownotifyNewB"></param>
        private void NotifyUserWorkflowHandle(Entity flownotifyA, Entity flownotifyNewB, string new_flow_name)
        {
            //自定义用户 
            EntityReference new_notifyuser_workflowid = flownotifyA.GetAttributeValue<EntityReference>("new_notifyuser_workflowid");
            if (new_notifyuser_workflowid != null)
            {
                if (this.IsMatchWorkFlow)
                {
                    Entity notifyUserWorkflowA = OrganizationServiceA.Retrieve(new_notifyuser_workflowid.LogicalName, new_notifyuser_workflowid.Id, new ColumnSet("uniquename"));
                    string uniquename = notifyUserWorkflowA.GetAttributeValue<string>("uniquename");
                    this.WorkFlowHandle("new_notifyuser_workflowid", uniquename, flownotifyA, flownotifyNewB, new_flow_name, "自定义用户");
                }
                else
                {
                    flownotifyNewB["new_notifyuser_workflowid"] = new_notifyuser_workflowid;
                }

                //var queryExpression = new QueryExpression(new_notifyuser_workflowid.LogicalName);
                //queryExpression.ColumnSet.AddColumns("uniquename");
                //queryExpression.Criteria.AddCondition("parentworkflowid", ConditionOperator.Null); //父流程 ID
                //queryExpression.Criteria.AddCondition("uniquename", ConditionOperator.Equal, uniquename);//唯一名称
                //EntityCollection notifyUserWorkflowListB = OrganizationServiceB.RetrieveMultiple(queryExpression);
                //if (notifyUserWorkflowListB.Entities.Count > 0)
                //{
                //    if (notifyUserWorkflowListB.Entities.Count > 1)
                //    {
                //        this.Log($"签核流程：【{new_flow_name}】的自定义用户动作匹配到多个流程{new_notifyuser_workflowid.Name},已将自定义用户动作设置成{ notifyUserWorkflowListB.Entities[0].Id}", true);
                //    }
                //    flownotifyNewB["new_notifyuser_workflowid"] = notifyUserWorkflowListB.Entities[0].ToEntityReference();
                //}
                //else
                //{
                //    //动作不存在则赋值null
                //    flownotifyNewB["new_notifyuser_workflowid"] = null;
                //    this.Log($"导入错误：签核流程【{new_flow_name}】的自定义用户动作【{new_notifyuser_workflowid.Name}】不存在，需要手动维护", true);
                //}
            }
        }

        /// <summary>
        /// 签核知会的签核角色导入处理
        /// </summary>
        /// <param name="flownotifyA"></param>
        /// <param name="flownotifyB"></param>
        private void FlowNotifyRoleHandle(Entity flownotifyA, Entity flownotifyNewB)
        {
            //知会角色 new_notify_roleid
            EntityReference new_notify_roleid = flownotifyA.GetAttributeValue<EntityReference>("new_notify_roleid");
            if (new_notify_roleid != null)
            {
                var queryExpression = new QueryExpression(new_notify_roleid.LogicalName);
                queryExpression.ColumnSet.AddColumns("new_name");
                queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                queryExpression.Criteria.AddCondition("new_name", ConditionOperator.Equal, new_notify_roleid.Name);
                EntityCollection data = OrganizationServiceB.RetrieveMultiple(queryExpression);
                if (data.Entities.Count > 0)
                {
                    flownotifyNewB["new_notify_roleid"] = data.Entities[0].ToEntityReference();
                }
                else
                {
                    Entity notifyRoleCreate = new Entity(new_notify_roleid.LogicalName, Guid.NewGuid());
                    notifyRoleCreate["new_name"] = new_notify_roleid.Name;
                    OrganizationServiceB.Create(notifyRoleCreate);
                    flownotifyNewB["new_notify_roleid"] = notifyRoleCreate.ToEntityReference();
                }

                string new_flow_name = flownotifyA.GetAttributeValue<EntityReference>("new_flowid")?.Name;

                this.Log($"导入提示：签核流程【{new_flow_name}】的知会角色【{new_notify_roleid.Name}】需要手动维护用户和互联用户", true);

                //TODO 不考虑将用户和互联用户也导过来，正式环境需要手动维护
            }
        }

        /// <summary>
        /// 获取A环境签核流程的明细
        /// </summary>
        /// <param name="serviceA"></param>
        /// <param name="entityName"></param>
        /// <param name="new_flowid_old"></param>
        /// <returns></returns>
        private EntityCollection GetFlowDetailListFromA(string entityName, Guid new_flowid_old)
        {
            var queryExpression = new QueryExpression(entityName);
            queryExpression.ColumnSet.AllColumns = true;
            queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            queryExpression.Criteria.AddCondition("new_flowid", ConditionOperator.Equal, new_flowid_old);
            EntityCollection flownotifyList = OrganizationServiceA.RetrieveMultiple(queryExpression);
            return flownotifyList;
        }

        /// <summary>
        /// 通过委托将日志实时返回窗体
        /// </summary>
        /// <param name="msg">日志</param>
        /// <param name="isImportant">是否重要信息</param>
        /// <param name="ex">异常对象</param>
        private void Log(string msg, bool isImportant, Exception ex = null)
        {
            if (ex == null)
            {
                if (string.IsNullOrEmpty(msg))
                    return;

                CommonHelper.WriteLog(msg);
            }
            else
            {
                msg += ex?.Message;
                CommonHelper.WriteLog(msg + ex?.ToString());
            }

            if (this.logDelegate == null)
                return;

            this.logDelegate(msg, isImportant);
        }
    }
}
