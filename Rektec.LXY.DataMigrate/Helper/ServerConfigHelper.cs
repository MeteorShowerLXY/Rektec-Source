#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/4 14:38:10
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
using Newtonsoft.Json;
using Rektec.LXY.DataMigrate.Model;

namespace Rektec.LXY.DataMigrate.Helper
{
    public class ServerConfigHelper
    {
        /// <summary>
        /// 配置信息保存路径
        /// </summary>
        private static readonly string JsonPath = Environment.CurrentDirectory + "\\Config\\ServerConfig.json";

        /// <summary>
        /// 保存当前选择的选项
        /// </summary>
        /// <param name="currentIdA"></param>
        /// <param name="currentIdB"></param>
        public static void SaveCurrentIndex(int currentIdA, int currentIdB)
        {
            ServerConfigInfo serverConfigInfo = GetServerInfoList();
            serverConfigInfo.CurrentIdA = currentIdA;
            serverConfigInfo.CurrentIdB = currentIdB;

            SaveServerConfig(serverConfigInfo);
        }

        /// <summary>
        /// 获取拼接好的服务器地址信息
        /// </summary>
        /// <param name="serverConfigInfo"></param>
        /// <param name="serverIndex">0:ServerA,1:ServerB,小于0获取所有</param>
        /// <returns></returns>
        public static List<ListItem> GetFullServerInfoList(ServerConfigInfo serverConfigInfo, int serverIndex)
        {
            var list = new List<ListItem>();

            if (serverConfigInfo == null || serverConfigInfo.ServerInfos == null)
                return list;

            list.Add(new ListItem() { Text = "", Value = "0" });

            foreach (ServerInfo item in serverConfigInfo.ServerInfos)
            {
                if (serverIndex >= 0 && item.ServerIndex != serverIndex)
                    continue;

                string fullServer = string.Format($"{item.Name}：Url={item.Url}/{item.OrgName};Username={item.UserName};Password={item.Password};Domain={item.Domain};AuthType={item.AuthType};");

                list.Add(new ListItem() { Text = fullServer, Value = item.Id.ToString() });
            }

            return list;
        }

        /// <summary>
        /// 获取指定配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServerInfo GetServerInfo(int id)
        {
            ServerConfigInfo serverConfigInfo = GetServerInfoList();
            if (serverConfigInfo == null)
                return null;

            return serverConfigInfo.ServerInfos.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 获取服务地址信息列表
        /// </summary>
        /// <returns></returns>
        public static ServerConfigInfo GetServerInfoList()
        {
            string json = JsonHelper.GetJsonFile(JsonPath);
            ServerConfigInfo serverConfigInfo = JsonConvert.DeserializeObject<ServerConfigInfo>(json);

            if (serverConfigInfo == null)
                serverConfigInfo = new ServerConfigInfo();

            if (serverConfigInfo.ServerInfos == null)
                serverConfigInfo.ServerInfos = new List<ServerInfo>();
            else
                serverConfigInfo.ServerInfos.OrderByDescending(x => x.Id);

            return serverConfigInfo;
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="serverConfigInfo"></param>
        public static void SaveServerConfig(ServerConfigInfo serverConfigInfo)
        {
            string json = JsonConvert.SerializeObject(serverConfigInfo);
            JsonHelper.WriteJsonFile(JsonPath, json);
        }
    }
}
