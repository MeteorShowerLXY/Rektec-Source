#region 文件描述
/*******************************************************************
* 创建人   : Terry Liu
* 创建时间 : 2022/6/4 14:31:22
* 功能描述 : 
===================================================================
* 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．
* Copyright © 2022 苏州瑞泰信息技术有限公司 All Rights Reserved.
*******************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Rektec.LXY.DataMigrate.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 将序列化的json字符串内容写入Json文件，并且保存
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="jsonConents">Json内容</param>
        public static void WriteJsonFile(string path, string jsonConents)
        {
            File.WriteAllText(path, jsonConents, Encoding.UTF8);
        }

        /// <summary>
        /// 获取到本地的Json文件并且解析返回对应的json字符串
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static string GetJsonFile(string filepath)
        {
            string json = string.Empty;
            using (var fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }

        /// <summary>
        /// 对象 转换为Json字符串
        /// </summary>
        /// <param name="tablelList"></param>
        /// <returns></returns>
        public string ToJson(object tablelList)
        {
            var json = new DataContractJsonSerializer(tablelList.GetType());
            string finJson = "";
            //序列化
            using (var stream = new MemoryStream())
            {
                json.WriteObject(stream, tablelList);
                finJson = Encoding.UTF8.GetString(stream.ToArray());
            }
            return finJson;
        }
    }
}
