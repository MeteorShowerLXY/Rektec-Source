#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/7/16 21:54:52
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Rektec.LXY.DataMigrate.Model;
using RekTec.Crm.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rektec.LXY.DataMigrate.Helper
{
    public class ImportStandardHelper
    {
        /// <summary>
        /// 记录日志事件
        /// </summary>
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
        /// 存在的数据是否执行更新操作
        /// </summary>
        private readonly bool IsUpdate;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serverInfoA">标准功能配置存在的环境信息</param>
        /// <param name="serverInfoB">需要导入的环境信息</param> 
        /// <param name="isUpdate">存在的数据是否执行更新操作</param>
        public ImportStandardHelper(ServerInfo serverInfoA, ServerInfo serverInfoB, bool isUpdate)
        {
            this.OrganizationServiceA = CommonHelper.GetOrganizationService(serverInfoA);
            this.OrganizationServiceB = CommonHelper.GetOrganizationService(serverInfoB);
            this.IsUpdate = isUpdate;
        }

        /// <summary>
        /// 标准功能配置迁移【A环境迁移到B环境】
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filterXml"></param>
        public void StandardConfigAToB(string entityName, string filterXml)
        {
            this.Log("导入标准功能配置开始");
            try
            {
                this.StandardConfigAToBHandle(entityName, filterXml);
            }
            catch (Exception ex)
            {
                this.Log("导入标准功能配置异常：", ex);
            }

            this.Log("导入标准功能配置结束");
        }

        /// <summary>
        /// 标准功能配置迁移【A环境迁移到B环境】
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="filterXml"></param>
        private void StandardConfigAToBHandle(string entityName, string filterXml)
        {
            string fetchXmlA = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>"
                           + "  <entity name='" + entityName + "'>"
                           + "   <all-attributes />"
                           + filterXml
                           + "  </entity>"
                           + "</fetch>";

            string fetchXmlB = "<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>"
                          + "  <entity name='" + entityName + "'>"
                          + "   <all-attributes />"
                          + "   <filter type='and'>"
                          + "    <condition attribute='statecode' operator='eq' value='0' />"
                          + "   </filter>"
                          + "  </entity>"
                          + "</fetch>";

            //查询A环境符合条件的标准功能配置
            EntityCollection entityListA = OrganizationServiceA.RetrieveMultiple(new FetchExpression(fetchXmlA));
            this.Log($"符合条件的记录数：{entityListA.Entities.Count}");
            if (entityListA.Entities.Count <= 0)
                return;

            //查询B环境符合条件的标准功能配置
            EntityCollection entityListB = OrganizationServiceB.RetrieveMultiple(new FetchExpression(fetchXmlB));

            switch (entityName)
            {
                case "new_autonumber"://自动编号
                    this.AutoNumberHandle(entityListA, entityListB);
                    break;
                case "new_sumrelationshipdetail"://明细汇总
                    this.DetailSumHandle(entityListA, entityListB);
                    break;
                case "new_duplicatedetect": //重复记录检测
                    this.RepeatCheckHandle(entityListA, entityListB);
                    break;
                case "new_ribbon": //自定义按钮
                    this.DefinedButtonHandle(entityListA, entityListB);
                    break;
                case "new_systemparameter": //系统参数
                    this.SystemParametersHandle(entityListA, entityListB);
                    break;
                default:
                    this.Log($"实体[{entityName}]未实现");
                    break;
            }
        }

        /// <summary>
        /// 自动编号处理
        /// </summary>
        private void AutoNumberHandle(EntityCollection entityListA, EntityCollection entityListB)
        {
            int addCount = 0;
            int updateCount = 0;
            foreach (Entity entityA in entityListA?.Entities)
            {
                string new_name = entityA.GetAttributeValue<string>("new_name");//实体名称
                string new_nofieldname = entityA.GetAttributeValue<string>("new_nofieldname");//单号字段
                Entity entityNewB = CommonHelper.CopyEntity(entityA);
                if (entityListB?.Entities?.Count > 0)
                {
                    Entity entityB = entityListB.Entities.FirstOrDefault(x => x.GetAttributeValue<string>("new_name") == new_name
                           && x.GetAttributeValue<string>("new_nofieldname") == new_nofieldname);
                    if (entityB != null)
                    {
                        if (this.IsUpdate)
                        {
                            entityNewB.Id = entityB.Id;
                            entityNewB[entityB.LogicalName + "id"] = entityB.Id;
                            OrganizationServiceB.Update(entityNewB);

                            updateCount++;
                        }

                        continue;
                    }
                }

                addCount++;
                OrganizationServiceB.Create(entityNewB);
            }
            this.Log($"自动编号导入成功,新增数：{addCount},修改数：{updateCount}");

            //初始化,标准功能配置
            this.InitializePlugin("自动编号", "InitializeAutoNumber");
        }

        /// <summary>
        /// 明细汇总处理
        /// </summary>
        private void DetailSumHandle(EntityCollection entityListA, EntityCollection entityListB)
        {
            int addCount = 0;
            int updateCount = 0;
            foreach (Entity entityA in entityListA?.Entities)
            {
                string new_name = entityA.GetAttributeValue<string>("new_name");//父实体
                string new_listentity = entityA.GetAttributeValue<string>("new_listentity");//子实体  
                string new_total = entityA.GetAttributeValue<string>("new_total");//汇总主档字段  
                Entity entityNewB = CommonHelper.CopyEntity(entityA);
                if (entityListB?.Entities?.Count > 0)
                {
                    Entity entityB = entityListB.Entities.FirstOrDefault(x => x.GetAttributeValue<string>("new_name") == new_name
                         && x.GetAttributeValue<string>("new_listentity") == new_listentity
                         && x.GetAttributeValue<string>("new_total") == new_total);
                    if (entityB != null)
                    {
                        if (this.IsUpdate)
                        {
                            entityNewB.Id = entityB.Id;
                            entityNewB[entityA.LogicalName + "id"] = entityB.Id;
                            OrganizationServiceB.Update(entityNewB);

                            updateCount++;
                        }

                        continue;
                    }
                }

                addCount++;
                OrganizationServiceB.Create(entityNewB);
            }

            this.Log($"明细汇总导入成功,新增数：{addCount},修改数：{updateCount}");

            //初始化,标准功能配置
            this.InitializePlugin("明细汇总", "InitializeSumRelationshipDetail");
        }

        /// <summary>
        /// 重复记录检测处理
        /// </summary>
        private void RepeatCheckHandle(EntityCollection entityListA, EntityCollection entityListB)
        {
            int addCount = 0;

            //重复记录检测,无法区分是新增还是修改，所以只能停用之前的记录后，重新导入
            if (entityListB?.Entities?.Count > 0)
            {
                foreach (var item in entityListB?.Entities)
                {
                    var req = new SetStateRequest
                    {
                        EntityMoniker = item.ToEntityReference(),
                        State = new OptionSetValue(1),
                        Status = new OptionSetValue(2)
                    };
                    OrganizationServiceB.Execute(req);
                }
                this.Log($"重复记录检测停用之前的记录数：{entityListB.Entities.Count}");
            }

            foreach (Entity entityA in entityListA?.Entities)
            {
                var queryExpression = new QueryExpression("new_duplicatedetect_key");
                queryExpression.ColumnSet.AllColumns = true;
                queryExpression.Criteria.AddCondition("new_duplicatedetectid", ConditionOperator.Equal, entityA.Id);
                queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                EntityCollection data_key_A = OrganizationServiceA.RetrieveMultiple(queryExpression);

                Entity entityNewB = CommonHelper.CopyEntity(entityA);

                addCount++;
                OrganizationServiceB.Create(entityNewB);

                foreach (Entity key_A in data_key_A.Entities)
                {
                    Entity keyNewB = new Entity("new_duplicatedetect_key", Guid.NewGuid());
                    keyNewB["new_duplicatedetect_keyid"] = keyNewB.Id;
                    keyNewB["new_duplicatedetectid"] = entityNewB.ToEntityReference();
                    keyNewB["new_name"] = key_A.GetAttributeValue<string>("new_name");//字段名称
                    keyNewB["new_null_ne_null"] = key_A.GetAttributeValue<bool>("new_null_ne_null");//NULL值不相等 
                    OrganizationServiceB.Create(keyNewB);
                }
            }
            this.Log($"重复记录检测导入成功,新增数：{addCount}");

            //初始化,标准功能配置
            this.InitializePlugin("重复记录检测", "InitializeDuplicateDetect");
        }

        /// <summary>
        /// 自定义按钮处理
        /// </summary>
        private void DefinedButtonHandle(EntityCollection entityListA, EntityCollection entityListB)
        {
            int addCount = 0;
            int updateCount = 0;
            foreach (Entity entityA in entityListA?.Entities)
            {
                var queryExpression = new QueryExpression("new_ribbonrule");
                queryExpression.ColumnSet.AllColumns = true;
                queryExpression.Criteria.AddCondition("new_ribbon", ConditionOperator.Equal, entityA.Id);
                queryExpression.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                EntityCollection data_ribbonrule_A = OrganizationServiceA.RetrieveMultiple(queryExpression);

                string new_name = entityA.GetAttributeValue<string>("new_name");//名称 
                Entity entityNewB = CommonHelper.CopyEntity(entityA);
                if (entityListB?.Entities?.Count > 0)
                {
                    Entity entityB = entityListB.Entities.FirstOrDefault(x => x.GetAttributeValue<string>("new_name") == new_name);
                    if (entityB != null)
                    {
                        if (this.IsUpdate)
                        {
                            //原本已存在的则直接更新描述，不更新安全角色
                            entityNewB.Id = entityB.Id;
                            entityNewB[entityB.LogicalName + "id"] = entityB.Id;
                            OrganizationServiceB.Update(entityNewB);

                            updateCount++;
                        }
                        continue;
                    }
                }

                addCount++;
                OrganizationServiceB.Create(entityNewB);

                var dicRoleB = new Dictionary<string, EntityReference>();

                foreach (Entity ribbonrule_A in data_ribbonrule_A.Entities)
                {
                    EntityReference new_roleid = ribbonrule_A.GetAttributeValue<EntityReference>("new_roleid");
                    if (new_roleid != null)
                    {
                        EntityReference new_roleid_b = null;
                        if (!dicRoleB.ContainsKey(new_roleid.Name))
                        {
                            queryExpression = new QueryExpression("role");
                            queryExpression.Criteria.AddCondition("name", ConditionOperator.Equal, new_roleid.Name);
                            EntityCollection data = OrganizationServiceB.RetrieveMultiple(queryExpression);
                            if (data?.Entities.Count > 0)
                            {
                                dicRoleB[new_roleid.Name] = data.Entities[0].ToEntityReference();
                            }
                            else
                            {
                                dicRoleB[new_roleid.Name] = null;
                            }

                        }
                        new_roleid_b = dicRoleB[new_roleid.Name];

                        if (new_roleid_b != null)
                        {
                            Entity ribbonruleNewB = new Entity("new_ribbonrule", Guid.NewGuid());
                            ribbonruleNewB["new_ribbonruleid"] = ribbonruleNewB.Id;
                            ribbonruleNewB["new_ribbon"] = entityNewB.ToEntityReference();
                            ribbonruleNewB["new_roleid"] = new_roleid_b;//安全角色 
                            ribbonruleNewB["new_name"] = ribbonrule_A.GetAttributeValue<string>("new_name");//字段名称 
                            OrganizationServiceB.Create(ribbonruleNewB);
                        }
                        else
                        {
                            this.Log($"安全角色[{new_roleid.Name}]获取失败，没有导入自定义按钮[{new_name}]的安全角色");
                        }
                    }
                }
            }

            this.Log($"自定义按钮导入成功,新增数：{addCount},修改数：{updateCount}");
        }

        /// <summary>
        /// 系统参数处理
        /// </summary>
        private void SystemParametersHandle(EntityCollection entityListA, EntityCollection entityListB)
        {
            int addCount = 0;
            int updateCount = 0;
            foreach (Entity entityA in entityListA?.Entities)
            {
                string new_name = entityA.GetAttributeValue<string>("new_name");//名称 
                Entity entityNewB = CommonHelper.CopyEntity(entityA);
                if (entityListB?.Entities?.Count > 0)
                {
                    Entity entityB = entityListB.Entities.FirstOrDefault(x => x.GetAttributeValue<string>("new_name") == new_name);
                    if (entityB != null)
                    {
                        if (this.IsUpdate)
                        {
                            entityNewB.Id = entityB.Id;
                            entityNewB[entityB.LogicalName + "id"] = entityB.Id;
                            OrganizationServiceB.Update(entityNewB);

                            updateCount++;
                        }
                        continue;
                    }
                }

                addCount++;
                OrganizationServiceB.Create(entityNewB);
            }
            this.Log($"系统参数导入成功,新增数：{addCount},修改数：{updateCount}");
            try
            {
                //初始化
                CrmHelper.InvokeHiddenApi(OrganizationServiceB, "new_cachemanager", "CacheManager/ClearAll", new Dictionary<string, object> { });

                this.Log($"系统参数导入成功，并且已清除缓存，如果还没生效，请回收程序池或者重启iis！");
            }
            catch (Exception ex)
            {
                this.Log($"系统参数导入成功，但是清除缓存异常：", ex);
            }
        }

        /// <summary>
        /// 初始化,标准功能配置
        /// </summary>
        /// <param name="tip"></param>
        /// <param name="actionName"></param>
        private void InitializePlugin(string tip, string actionName)
        {
            try
            {
                //初始化
                CrmHelper.InvokeHiddenApi(OrganizationServiceB, "new_commonpluginregistered", $"InitializePlugin/{actionName}", new Dictionary<string, object> { });

                this.Log($"{tip},初始化完成");
            }
            catch (Exception ex)
            {
                this.Log($"{tip}导入成功，但是初始化异常：", ex);
            }
        }

        /// <summary>
        /// 通过委托将日志实时返回窗体
        /// </summary>
        /// <param name="msg">日志</param> 
        /// <param name="ex">异常对象</param>
        private void Log(string msg, Exception ex = null)
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

            this.logDelegate(msg, false);
        }
    }
}
