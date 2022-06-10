#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/4 14:57:02
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
using System.Threading.Tasks;

namespace Rektec.LXY.DataMigrate.Model
{
    public class ServerConfigInfo
    {
        public int CurrentIdA { get; set; }
        public int CurrentIdB { get; set; }

        public List<ServerInfo> ServerInfos { get; set; }
    }
}
