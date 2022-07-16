#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/20 21:11:08
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rektec.LXY.DataMigrate.Model
{
    public class FlowDesignModel
    { 
        public FlowActionModel FlowAgreeAction { get; set; }
        public FlowActionModel FlowRecallAction { get; set; }
        public FlowActionModel FlowSubmitAction { get; set; }
        public FlowActionModel FlowPreSubmitAction { get; set; }
        public bool AllowEmpty { get; set; }
        public string MemoField3 { get; set; }
        public FlowActionModel FlowRejectAction { get; set; }
        public string MemoField2 { get; set; }
        public string NameField { get; set; }
        public string EntityLineMainId { get; set; }
        public string EntityLineName { get; set; }
        public string EntityName { get; set; }
        public string Description { get; set; }
        public string FlowName { get; set; }
        public string MemoField1 { get; set; }
        //public FlowNextNode NextNode { get; set; }
    }

    public class FilterFiled
    {
    }

    public class FilterContent
    {
        /// <summary>
        /// 
        /// </summary>
        public string LogicalName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MetadataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FilterFiled FilterFiled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> LinkEntities { get; set; }
    }

    public class ConditionAction
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UniqueName { get; set; }
        /// <summary>
        /// 客户的归属平台是否是大客户平台
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Category { get; set; }
    }

    public class FlowDesignConditionListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string NodeId { get; set; }
        /// <summary>
        /// 条件---------
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NextNode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FilterContent FilterContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ConditionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ConditionAction ConditionAction { get; set; }
    }

    public class FlowRoleCode
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LogicalName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UniqueName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Category { get; set; }
    }

    public class CustomApproveUser
    {
        /// <summary>
        /// 业务赠送申请提交校验2
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LogicalName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
    }

    public class Role
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LogicalName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    public class FlowActionModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UniqueName { get; set; }
        /// <summary>
        /// 审核-同意
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Category { get; set; }
    } 
     
   

    public class NextNode
    {
        /// <summary>
        /// 
        /// </summary>
        public int NodeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public FlowNode FlowNode { get; set; }
    }
     
    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public NextNode NextNode { get; set; }
    }


}

