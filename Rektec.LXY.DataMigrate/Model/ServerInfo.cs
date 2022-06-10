#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/2 14:21:43
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion

using System;

namespace Rektec.LXY.DataMigrate.Model
{
    public class ServerInfo
    {
        /// <summary>
        /// 0:ServerA,1:ServerB
        /// </summary>
        public int ServerIndex { get; set; }
        public int Id { get; set; }

        /// <summary>
        /// crm地址，格式：【http://192.168.13.150】
        /// </summary>
        public string Url { get; set; }
        public string OrgName { get; set; }
        public string UserName { get; set; } = "crmadmin";
        public string Password { get; set; } = "p@ssw0rd";

        public string Domain { get; set; } = "Handev";
        public string AuthType { get; set; } = "AD";
    }
}
