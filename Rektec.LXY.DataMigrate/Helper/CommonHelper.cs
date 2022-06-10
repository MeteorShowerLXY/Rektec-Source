#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/2 23:23:49
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Discovery;
using Rektec.LXY.DataMigrate.Model;
using System;
using System.IO;
using System.Net;
using System.ServiceModel.Description;

namespace Rektec.LXY.DataMigrate.Helper
{
    public class CommonHelper
    {
        /// <summary>
        /// 创建组织服务
        /// </summary>
        /// <returns></returns>
        public static IOrganizationService GetOrganizationService(ServerInfo serverInfo)
        {
            string fullUrl = $"{serverInfo.Url}/{serverInfo.OrgName}/XRMServices/2011/Organization.svc";
            var credentials = new ClientCredentials();

            if (serverInfo.AuthType == "AD")
            {
                credentials.UserName.UserName = serverInfo.UserName;
                credentials.UserName.Password = serverInfo.Password;

                //clientCredentials.Windows.ClientCredential = new NetworkCredential("<UserName>", "<Password>", "<Domain Name>");
            }
            else if (serverInfo.AuthType == "ADFS")
            {
                credentials.UserName.UserName = serverInfo.Domain + "\\" + serverInfo.UserName;
                credentials.UserName.Password = serverInfo.Password;
            }

            var organizationUri = new Uri(fullUrl);
            Uri homeRealmUri = null;
            var orgProxy = new OrganizationServiceProxy(organizationUri, homeRealmUri, credentials, null);
            var service = (IOrganizationService)orgProxy;
            return service;
        }

        /// <summary>
        /// 复制实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Entity CopyEntity(Entity entity)
        {
            var createEntity = new Entity(entity.LogicalName, Guid.NewGuid());
            createEntity[entity.LogicalName + "id"] = createEntity.Id;
            foreach (var attribute in entity.Attributes)
            {
                if (attribute.Key == entity.LogicalName + "id" || attribute.Key == "createdon" || attribute.Key == "modifiedon")
                    continue;

                createEntity[attribute.Key] = attribute.Value;
            }

            return createEntity;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="isSaveTxt"></param>
        public static void WriteLog(string content)
        {

            //var task = new Task(() =>
            //{
            try
            {
                //log.DebugMsg(content);

                string path = System.Environment.CurrentDirectory + "\\Log\\";// "C:\\Log\\SAP";
                //判断该路径下的文件夹是否存在，不存在则创建
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //当前时间
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //文件名称
                string fileName = DateTime.Now.ToString("yyyyMMdd");
                //文件路径
                string fullPath = path + "/" + fileName + ".txt";
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter sw = File.AppendText(fullPath);
                string writeContent = currentTime + ":\r\n" + content + "\r\n-------------------------------------------------------------------------------------------------------------------\r\n";
                sw.Write(writeContent);
                //关闭日志文件
                sw.Close();
            }
            catch
            {

            }
            //});
            //task.Start();
        }

        /// <summary>
        /// 保存过滤条件
        /// </summary>
        /// <param name="content"></param>
        public static void SaveFilterXml(string content)
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\Config\\";
                //判断该路径下的文件夹是否存在，不存在则创建
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fullPath = path + "filterXml.txt";
                File.WriteAllText(fullPath, content);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取过滤条件
        /// </summary>
        /// <param name="content"></param>
        public static string GetFilterXml()
        {
            try
            {
                string path = Environment.CurrentDirectory + "\\Config\\";
                string fullPath = path + "filterXml.txt";
                return File.ReadAllText(fullPath);
            }
            catch
            {

            }

            string filterXml = @"
<filter type='and'>
    <condition attribute='statecode' operator='eq' value='0' />
    <condition attribute='new_flowstatus' operator='eq' value='2' /> 
    <condition attribute='new_name' operator='in' >
      <value>替换成实际的签核流程名称</value>
    </condition>
</filter>";

            return filterXml;
        }
    }
}
