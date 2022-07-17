using Rektec.LXY.DataMigrate.Helper;
using Rektec.LXY.DataMigrate.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rektec.LXY.DataMigrate
{
    public partial class ImportStandard : Form
    {
        public ImportStandard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportStandard_Load(object sender, EventArgs e)
        {
            string filterXml = CommonHelper.GetStandardFilterXml();
            this.tbFilterXml.Text = filterXml.TrimStart();

            this.ComBoboxBind();
        }

        /// <summary>
        /// 显示日志信息
        /// </summary>
        /// <param name="msg">日志信息</param>
        /// <param name="isImportant">是否重要信息</param>
        private void ShowLog(string msg, bool isImportant)
        {
            msg = DateTime.Now.ToString("HH:mm:ss") + "#" + msg + "\r\n";
            this.tbMsg.AppendText(msg);
        }

        /// <summary>
        /// 绑定ComBobox
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dic"></param>
        private void ComBoboxBind()
        {
            ServerConfigInfo serverConfigInfo = ServerConfigHelper.GetServerInfoList();
            List<ListItem> listA = ServerConfigHelper.GetFullServerInfoList(serverConfigInfo, 0);
            this.cbServerA.DisplayMember = "Text";
            this.cbServerA.ValueMember = "Value";
            this.cbServerA.DataSource = listA;

            List<ListItem> listB = ServerConfigHelper.GetFullServerInfoList(serverConfigInfo, 1);
            this.cbServerB.DisplayMember = "Text";
            this.cbServerB.ValueMember = "Value";
            this.cbServerB.DataSource = listB;

            this.InitDropDownWidth(this.cbServerA);
            this.InitDropDownWidth(this.cbServerB);

            this.cbServerA.SelectedValue = serverConfigInfo.CurrentIdA.ToString();
            this.cbServerB.SelectedValue = serverConfigInfo.CurrentIdB.ToString();
        }

        /// <summary>
        /// 初始化下拉框显示的宽度
        /// </summary>
        /// <param name="comboBox"></param>
        private void InitDropDownWidth(ComboBox comboBox)
        {
            // 测量出最⼤的字符⼤⼩
            int maxSize = 0;
            Graphics g = CreateGraphics();
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                comboBox.SelectedIndex = i;
                SizeF size = g.MeasureString(comboBox.Text, comboBox.Font);
                if (maxSize < (int)size.Width)
                    maxSize = (int)size.Width;
            }
            comboBox.DropDownWidth = comboBox.Width;
            if (comboBox.DropDownWidth < maxSize)
                comboBox.DropDownWidth = maxSize - 30;
        }

        /// <summary>
        /// 是否指定条件改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsSpecifiedCondition_CheckedChanged(object sender, EventArgs e)
        {
            this.tbFilterXml.Enabled = this.chkIsSpecifiedCondition.Checked;
        }

        /// <summary>
        /// 全选/全不选改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.chkDefinedButton.Checked = this.chkAllSelect.Checked;
            this.chkDetailSum.Checked = this.chkAllSelect.Checked;
            this.chkRepeatCheck.Checked = this.chkAllSelect.Checked;
            this.chkSystemParameters.Checked = this.chkAllSelect.Checked;
            this.chkAutoNumber.Checked = this.chkAllSelect.Checked;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string filterXml = @"
            <filter type='and'>
                <condition attribute='statecode' operator='eq' value='0' />  
            </filter>";

            int selectCount = 0;
            if (this.chkDefinedButton.Checked)
                selectCount += 1;
            if (this.chkDetailSum.Checked)
                selectCount += 1;
            if (this.chkRepeatCheck.Checked)
                selectCount += 1;
            if (this.chkSystemParameters.Checked)
                selectCount += 1;
            if (this.chkAutoNumber.Checked)
                selectCount += 1;

            if (this.chkIsSpecifiedCondition.Checked)
            {
                if (selectCount > 1)
                {
                    MessageBox.Show(this.lblIsSpecifiedConditionTip.Text);
                    return;
                }
                filterXml = this.tbFilterXml.Text;
                if (string.IsNullOrEmpty(filterXml))
                {
                    MessageBox.Show("过滤条件不能为空！");
                    return;
                }

                CommonHelper.SaveStandardFilterXml(filterXml);
            }

            if (selectCount == 0)
            {
                MessageBox.Show("请选择需要迁移的标准功能配置！");
                return;
            }

            this.tbMsg.Text = string.Empty;
            if (this.chkRepeatCheck.Checked)
            {
                DialogResult dr = MessageBox.Show("重复检查会停用之前的记录，重新导入，确定继续吗?", "继续", MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)//如果点击“确定”按钮 
                {
                }
                else//如果点击“取消”按钮 
                {
                    return;
                }
            }

            ServerConfigInfo serverConfigInfo = ServerConfigHelper.GetServerInfoList();

            if (serverConfigInfo.CurrentIdA <= 0)
            {
                MessageBox.Show("请选择ServerA！");
                return;
            }
            if (serverConfigInfo.CurrentIdB <= 0)
            {
                MessageBox.Show("请选择ServerB！");
                return;
            }

            ServerInfo serviceInfoA = serverConfigInfo.ServerInfos.FirstOrDefault(x => x.Id == serverConfigInfo.CurrentIdA);
            ServerInfo serviceInfoB = serverConfigInfo.ServerInfos.FirstOrDefault(x => x.Id == serverConfigInfo.CurrentIdB);

            if (serviceInfoA == null)
            {
                MessageBox.Show("获取ServerA配置信息失败！");
                return;
            }

            if (serviceInfoB == null)
            {
                MessageBox.Show("获取ServerB配置信息失败！");
                return;
            }

            var helper = new ImportStandardHelper(serviceInfoA, serviceInfoB, this.chkIsUpdate.Checked);
            helper.logDelegate += ShowLog;

            if (this.chkDetailSum.Checked)
                helper.StandardConfigAToB("new_sumrelationshipdetail", filterXml);

            if (this.chkRepeatCheck.Checked)
                helper.StandardConfigAToB("new_duplicatedetect", filterXml);

            if (this.chkSystemParameters.Checked)
                helper.StandardConfigAToB("new_systemparameter", filterXml);

            if (this.chkAutoNumber.Checked)
                helper.StandardConfigAToB("new_autonumber", filterXml);

            if (this.chkDefinedButton.Checked)
                helper.StandardConfigAToB("new_ribbon", filterXml);
        }

        //ServerA改变事件
        private void cbServerA_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveCurrentIndex();
        }

        //ServerB改变事件
        private void cbServerB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SaveCurrentIndex();
        }

        /// <summary>
        /// 保存当前选择的选项
        /// </summary>
        private void SaveCurrentIndex()
        {
            int currentIdA = Convert.ToInt32(this.cbServerA.SelectedValue);
            int currentIdB = Convert.ToInt32(this.cbServerB.SelectedValue);
            ServerConfigHelper.SaveCurrentIndex(currentIdA, currentIdB);
        }
    }
}
