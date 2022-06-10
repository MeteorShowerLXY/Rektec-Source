using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LXY.MeteorProject.DAL;
using Maticsoft.DBUtility;
using Rektec.LXY.DataMigrate.Helper;
using Rektec.LXY.DataMigrate.Model;

namespace Rektec.LXY.DataMigrate
{
    public partial class ImportFlow : Form
    {
        public ImportFlow()
        {
            InitializeComponent();
        }

        //窗体加载
        private void Form1_Load(object sender, EventArgs e)
        {
            string filterXml = CommonHelper.GetFilterXml();
            this.tbFilterXml.Text = filterXml.TrimStart();

            this.ComBoboxBind();
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

            this.btnEditA.Visible = serverConfigInfo.CurrentIdA > 0;
            this.btnEditB.Visible = serverConfigInfo.CurrentIdB > 0;
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

        //转移签核流程
        private void btnFlowMigrate_Click(object sender, EventArgs e)
        {
            string filterXml = this.tbFilterXml.Text;
            if (string.IsNullOrEmpty(filterXml))
            {
                MessageBox.Show("过滤条件不能为空！");
                return;
            }

            CommonHelper.SaveFilterXml(filterXml);

            this.tbMsg.Text = string.Empty;

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

            var flowMigrationHelper = new FlowMigrationHelper(serviceInfoA, serviceInfoB);
            flowMigrationHelper.logDelegate += ShowLog;
            flowMigrationHelper.FlowAToB(filterXml, this.chbIsImportDetail.Checked);
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

            if (isImportant)
                this.tbImportantMsg.AppendText(msg);
        }

        #region 配置信息相关按钮
        //添加按钮A
        private void btnAddA_Click(object sender, EventArgs e)
        {
            var serverEdit = new ServerEdit(0, true);
            serverEdit.ShowDialog();

            this.ServerEditAfter(serverEdit);
        }

        //添加按钮B
        private void btnAddB_Click(object sender, EventArgs e)
        {
            var serverEdit = new ServerEdit(0, false);
            serverEdit.ShowDialog();

            this.ServerEditAfter(serverEdit);
        }

        //编辑按钮A
        private void btnEditA_Click(object sender, EventArgs e)
        {
            if (this.cbServerA.SelectedIndex <= 0)
            {
                MessageBox.Show("请先选择需要编辑的ServerA");
                return;
            }
            int id = Convert.ToInt32(this.cbServerA.SelectedValue);
            var serverEdit = new ServerEdit(id, true);
            serverEdit.ShowDialog();

            this.ServerEditAfter(serverEdit);
        }

        //编辑按钮B
        private void btnEditB_Click(object sender, EventArgs e)
        {
            if (this.cbServerB.SelectedIndex <= 0)
            {
                MessageBox.Show("请先选择需要编辑的ServerB");
                return;
            }
            int id = Convert.ToInt32(this.cbServerB.SelectedValue);
            var serverEdit = new ServerEdit(id, false);
            serverEdit.ShowDialog();

            this.ServerEditAfter(serverEdit);
        }

        /// <summary>
        /// 编辑配置信息后执行
        /// </summary>
        private void ServerEditAfter(ServerEdit serverEdit)
        {
            if (serverEdit.DialogResult == DialogResult.OK)
            {
                this.ComBoboxBind();
            }
        }

        //ServerA改变事件
        private void cbServerA_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnEditA.Visible = this.cbServerA.SelectedIndex > 0;

            this.SaveCurrentIndex();
        }

        //ServerB改变事件
        private void cbServerB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnEditB.Visible = this.cbServerB.SelectedIndex > 0;

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
        #endregion
    }
}
