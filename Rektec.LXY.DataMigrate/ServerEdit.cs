using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Rektec.LXY.DataMigrate.Helper;
using Rektec.LXY.DataMigrate.Model;

namespace Rektec.LXY.DataMigrate
{
    public partial class ServerEdit : Form
    {
        /// <summary>
        /// 配置信息的Id
        /// </summary>
        private readonly int Id;
        /// <summary>
        /// 是否服务器地址A
        /// </summary>
        private readonly bool IsServerA;
        /// <summary>
        /// 是否复制
        /// </summary>
        private readonly bool IsCopy;

        public ServerEdit(int id, bool isServerA, bool isCopy = false)
        {
            InitializeComponent();

            this.Id = id;
            this.IsServerA = isServerA;
            this.IsCopy = isCopy;
        }

        private void ServerEdit_Load(object sender, EventArgs e)
        {
            this.chkIsCheckConfig.Checked = true;
            this.cbAuthType.SelectedIndex = 0;
            this.cbServer.SelectedIndex = this.IsServerA ? 0 : 1;
            if (this.Id > 0)
            {
                ServerInfo serverInfo = ServerConfigHelper.GetServerInfo(this.Id);
                if (serverInfo == null)
                {
                    this.btnSure.Visible = false;
                    MessageBox.Show("获取配置信息失败");
                    return;
                }

                this.tbUrl.Text = serverInfo.Url;
                this.tbUserName.Text = serverInfo.UserName;
                this.tbPassword.Text = serverInfo.Password;
                this.tbOrgName.Text = serverInfo.OrgName;
                this.tbDomain.Text = serverInfo.Domain;
                this.cbAuthType.SelectedText = serverInfo.AuthType;
            }
            else
            {
                this.tbUrl.Text = "http://";
                this.tbUserName.Text = "crmadmin";
                this.tbPassword.Text = "P@ssw0rd";
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                ServerConfigInfo serverConfigInfo = ServerConfigHelper.GetServerInfoList();

                ServerInfo serverInfo;
                if (this.Id <= 0 || this.IsCopy)
                {
                    serverInfo = new ServerInfo()
                    {
                        Name = this.tbName.Text,
                        Id = serverConfigInfo.ServerInfos.Count + 1,
                        Url = this.tbUrl.Text.Trim().TrimEnd('/'),
                        AuthType = this.cbAuthType.SelectedItem.ToString(),
                        Domain = this.tbDomain.Text.Trim(),
                        OrgName = this.tbOrgName.Text.Trim(),
                        Password = this.tbPassword.Text.Trim(),
                        UserName = this.tbUserName.Text.Trim(),
                        ServerIndex = this.cbServer.SelectedIndex
                    };
                    serverConfigInfo.ServerInfos.Add(serverInfo);
                }
                else
                {
                    serverInfo = serverConfigInfo.ServerInfos.FirstOrDefault(x => x.Id == this.Id);
                    serverInfo.Name = this.tbName.Text;
                    serverInfo.Url = this.tbUrl.Text.Trim().TrimEnd('/');
                    serverInfo.AuthType = this.cbAuthType.SelectedItem.ToString();
                    serverInfo.Domain = this.tbDomain.Text.Trim();
                    serverInfo.OrgName = this.tbOrgName.Text.Trim();
                    serverInfo.Password = this.tbPassword.Text.Trim();
                    serverInfo.UserName = this.tbUserName.Text.Trim();
                    serverInfo.ServerIndex = this.cbServer.SelectedIndex;
                }

                if (this.cbServer.SelectedIndex == 0)
                    serverConfigInfo.CurrentIdA = serverInfo.Id;
                else
                    serverConfigInfo.CurrentIdB = serverInfo.Id;

                if (this.chkIsCheckConfig.Checked)
                {
                    try
                    {
                        //验证配置信息是否正确
                        IOrganizationService organizationService = CommonHelper.GetOrganizationService(serverInfo);
                        var queryExpression = new QueryExpression("systemuser") { TopCount = 1 };
                        queryExpression.ColumnSet.AddColumns("systemuserid");
                        EntityCollection data = organizationService.RetrieveMultiple(queryExpression);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Server连接失败：" + ex.Message);
                        return;
                    }
                }

                ServerConfigHelper.SaveServerConfig(serverConfigInfo);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置信息异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
