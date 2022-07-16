#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/20 21:41:38
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using RekTec.Crm.Flow.Common;
using RekTec.Crm.Flow.Common.Model;
using RekTec.Crm.Flow.Common.Model.FlowNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rektec.LXY.Flow.Helper
{
    public class FlowDesignHelper
    {
        /// <summary>
        /// 获取新的签核设计【主要替换主键】
        /// </summary>
        /// <param name="OrganizationServiceA"></param>
        /// <param name="OrganizationServiceB"></param>
        /// <param name="flowInfo"></param>
        /// <param name="isMatchWorkFlow"></param>
        /// <param name="msg"></param>
        /// <param name="imortantMsg"></param>
        /// <returns>返回最新的签核设计字符串</returns>
        public static string GetNewFlowDesign(IOrganizationService OrganizationServiceA, IOrganizationService OrganizationServiceB, Entity flowInfo, bool isMatchWorkFlow, out string msg, out string imortantMsg)
        {
            msg = string.Empty;
            imortantMsg = string.Empty;
            string new_flowdesign = flowInfo.GetAttributeValue<string>("new_flowdesign");
            if (!string.IsNullOrEmpty(new_flowdesign))
            {
                FlowDesignModel flowDesignModel = JsonConvert.DeserializeObject<FlowDesignModel>(new_flowdesign);

                List<FlowStepNode> flowsteps = RekTec.Crm.Flow.Common.FlowDesignHelper.ConvertFlowDesignToFlowStepNode(OrganizationServiceA, OrganizationServiceA, flowDesignModel, flowInfo);
                if (flowsteps != null)
                {
                    HashSet<string> flowRoleName = new HashSet<string>();

                    foreach (FlowStepNode item in flowsteps)
                    {
                        string tip = $"{item.StepName}的";
                        string iMsg;
                        if (item.RoleType == FlowRoleType.Role || item.RoleType == FlowRoleType.FlowForm)
                        {
                            #region 签核角色处理
                            var queryExpression = new QueryExpression("new_flowrole");
                            queryExpression.ColumnSet.AddColumns("new_flowroleid");
                            queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                            queryExpression.Criteria.AddCondition("new_name", ConditionOperator.Equal, item.Role.Name);
                            EntityCollection data = OrganizationServiceB.RetrieveMultiple(queryExpression);

                            if (data?.Entities?.Count > 0)
                            {
                                //签核角色已存在,替换签核角色id
                                if (item.Role.Id != data.Entities[0].Id.ToString())
                                    new_flowdesign = new_flowdesign.Replace(item.Role.Id, data.Entities[0].Id.ToString());
                            }
                            else
                            {
                                //保持原来的签核角色的主键，不然重新打开时签核角色为空
                                Entity createEntity = new Entity("new_flowrole", Guid.Parse(item.Role.Id));
                                createEntity["new_name"] = item.Role.Name;
                                OrganizationServiceB.Create(createEntity);
                                flowRoleName.Add(item.Role.Name);
                            }
                            #endregion
                        }
                        else if (item.RoleType == FlowRoleType.Code)
                        {
                            #region 自定义
                            if (isMatchWorkFlow && item.CustomApproveUser != null)
                            {
                                //自定义用户
                                new_flowdesign = FlowActionHandle(OrganizationServiceA, OrganizationServiceB, $"{ tip}自定义用户", item.CustomApproveUser, string.Empty, new_flowdesign, out iMsg);
                                imortantMsg += iMsg;
                            }
                            #endregion
                        }

                        if (isMatchWorkFlow)
                        {
                            //同意动作
                            new_flowdesign = FlowActionHandle(OrganizationServiceA, OrganizationServiceB, $"{ tip}同意动作", item.FlowAgreeAction, new_flowdesign, out iMsg);
                            imortantMsg += iMsg;

                            //驳回动作
                            new_flowdesign = FlowActionHandle(OrganizationServiceA, OrganizationServiceB, $"{ tip}同意动作", item.FlowRejectAction, new_flowdesign, out iMsg);
                            imortantMsg += iMsg;

                            if (item.NextNode != null && item.NextNode.NodeType == FlowDesignNodeType.Condition && item.NextNode.FlowNode != null)
                            {
                                FlowConditionNode flowConditionNode = flowDesignModel.NextNode.FlowNode as FlowConditionNode;
                                if (flowConditionNode.FlowDesignConditionList != null)
                                {
                                    foreach (FlowDesignConditionModel flowDesignConditionModel in flowConditionNode.FlowDesignConditionList)
                                    {
                                        if (flowDesignConditionModel.ConditionType == FlowConditionType.CustomAction)
                                        {
                                            //同意动作
                                            new_flowdesign = FlowActionHandle(OrganizationServiceA, OrganizationServiceB, $"{ tip}条件自定义动作", item.FlowAgreeAction, new_flowdesign, out iMsg);
                                            imortantMsg += iMsg;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (flowRoleName.Count > 0)
                        msg += $"导入签核角色:{string.Join("、", flowRoleName)}";
                }
            }

            return new_flowdesign;
        }

        /// <summary>
        /// 匹配执行动作
        /// </summary>
        /// <param name="OrganizationServiceB"></param>
        /// <param name="stepName"></param>
        /// <param name="flowAction"></param>
        /// <param name="new_flowdesign"></param>
        /// <param name="imortantMsg"></param>
        /// <returns></returns>
        private static string FlowActionHandle(IOrganizationService OrganizationServiceA, IOrganizationService OrganizationServiceB, string tip, FlowActionModel flowAction, string new_flowdesign, out string imortantMsg)
        {
            imortantMsg = string.Empty;
            if (flowAction != null && flowAction.Category == WorkflowType.Action && !string.IsNullOrEmpty(flowAction.Id))
            {
                string uniquename = flowAction.UniqueName;
                new_flowdesign = FlowActionHandle(OrganizationServiceA, OrganizationServiceB, tip, flowAction, uniquename, new_flowdesign, out imortantMsg);
                //var queryExpression = new QueryExpression(logicalName);
                //queryExpression.ColumnSet.AddColumns("uniquename");

                //queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 1); //激活状态
                //queryExpression.Criteria.AddCondition("parentworkflowid", ConditionOperator.Null); //父流程 ID
                //queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, workflowName); //流程名称
                //if (!string.IsNullOrEmpty(uniquename))
                //    queryExpression.Criteria.AddCondition("uniquename", ConditionOperator.Equal, uniquename); //唯一名称
                //EntityCollection data = OrganizationServiceB.RetrieveMultiple(queryExpression);
                //if (data.Entities.Count > 0)
                //{
                //    if (data.Entities.Count > 1)
                //        imortantMsg += $"{tip}匹配到多个流程{workflowName},已将{tip}设置成{ data.Entities[0].Id}";

                //    if (id != data.Entities[0].Id.ToString())
                //        new_flowdesign = new_flowdesign.Replace(id, data.Entities[0].Id.ToString());
                //}
                //else
                //{
                //    imortantMsg += $"{tip}【{workflowName}】不存在，需要手动维护";
                //}
            }

            return new_flowdesign;
        }

        /// <summary>
        /// 匹配执行动作
        /// </summary>
        /// <param name="OrganizationServiceB"></param>
        /// <param name="stepName"></param>
        /// <param name="flowAction"></param>
        /// <param name="new_flowdesign"></param>
        /// <param name="imortantMsg"></param>
        /// <returns></returns>
        private static string FlowActionHandle(IOrganizationService OrganizationServiceA, IOrganizationService OrganizationServiceB, string tip, LookUpModel flowAction, string uniquename, string new_flowdesign, out string imortantMsg)
        {
            imortantMsg = string.Empty;
            if (flowAction != null && !string.IsNullOrEmpty(flowAction.Id))
            {
                string logicalName = flowAction.LogicalName;
                if (string.IsNullOrEmpty(logicalName))
                    logicalName = "workflow";
                string id = flowAction.Id;
                if (string.IsNullOrEmpty(uniquename))
                {
                    uniquename = flowAction.LogicalName;//自定义用户时，这个值保存的是唯一名称
                    logicalName = "workflow";
                    //Entity workflowInfo = OrganizationServiceA.Retrieve(logicalName, Guid.Parse(id), new ColumnSet("uniquename"));
                    //uniquename = workflowInfo.GetAttributeValue<string>("uniquename");//唯一名称
                }

                string workflowName = flowAction.Name;
                var queryExpression = new QueryExpression(logicalName);
                queryExpression.ColumnSet.AddColumns("uniquename");

                queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 1); //激活状态
                queryExpression.Criteria.AddCondition("parentworkflowid", ConditionOperator.Null); //父流程 ID
                queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, workflowName); //流程名称
                if (!string.IsNullOrEmpty(uniquename))
                    queryExpression.Criteria.AddCondition("uniquename", ConditionOperator.Equal, uniquename); //唯一名称
                EntityCollection data = OrganizationServiceB.RetrieveMultiple(queryExpression);
                if (data.Entities.Count > 0)
                {
                    if (data.Entities.Count > 1)
                        imortantMsg += $"{tip}匹配到多个流程{workflowName},已将{tip}设置成{ data.Entities[0].Id}";

                    if (id != data.Entities[0].Id.ToString())
                        new_flowdesign = new_flowdesign.Replace(id, data.Entities[0].Id.ToString());
                }
                else
                {
                    imortantMsg += $"{tip}【{workflowName}】不存在，需要手动维护";
                }
            }

            return new_flowdesign;
        }
    }
}
