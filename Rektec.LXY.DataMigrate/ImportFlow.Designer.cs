
namespace Rektec.LXY.DataMigrate
{
    partial class ImportFlow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblServerA = new System.Windows.Forms.Label();
            this.lblServerB = new System.Windows.Forms.Label();
            this.cbServerA = new System.Windows.Forms.ComboBox();
            this.cbServerB = new System.Windows.Forms.ComboBox();
            this.btnEditA = new System.Windows.Forms.Button();
            this.btnEditB = new System.Windows.Forms.Button();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.btnFlowMigrate = new System.Windows.Forms.Button();
            this.tbFilterXml = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddA = new System.Windows.Forms.Button();
            this.btnAddB = new System.Windows.Forms.Button();
            this.chbIsImportDetail = new System.Windows.Forms.CheckBox();
            this.tbImportantMsg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblServerA
            // 
            this.lblServerA.AutoSize = true;
            this.lblServerA.Font = new System.Drawing.Font("宋体", 13.8F);
            this.lblServerA.Location = new System.Drawing.Point(5, 14);
            this.lblServerA.Name = "lblServerA";
            this.lblServerA.Size = new System.Drawing.Size(118, 24);
            this.lblServerA.TabIndex = 0;
            this.lblServerA.Text = "ServerA：";
            this.toolTip1.SetToolTip(this.lblServerA, "导出签核流程的服务器地址");
            // 
            // lblServerB
            // 
            this.lblServerB.AutoSize = true;
            this.lblServerB.Font = new System.Drawing.Font("宋体", 13.8F);
            this.lblServerB.Location = new System.Drawing.Point(5, 54);
            this.lblServerB.Name = "lblServerB";
            this.lblServerB.Size = new System.Drawing.Size(118, 24);
            this.lblServerB.TabIndex = 1;
            this.lblServerB.Text = "ServerB：";
            this.toolTip1.SetToolTip(this.lblServerB, "需要导入签核流程的服务器地址");
            // 
            // cbServerA
            // 
            this.cbServerA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServerA.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbServerA.FormattingEnabled = true;
            this.cbServerA.Location = new System.Drawing.Point(119, 11);
            this.cbServerA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbServerA.Name = "cbServerA";
            this.cbServerA.Size = new System.Drawing.Size(933, 31);
            this.cbServerA.TabIndex = 2;
            this.cbServerA.SelectedIndexChanged += new System.EventHandler(this.cbServerA_SelectedIndexChanged);
            // 
            // cbServerB
            // 
            this.cbServerB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServerB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbServerB.FormattingEnabled = true;
            this.cbServerB.Location = new System.Drawing.Point(119, 51);
            this.cbServerB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbServerB.Name = "cbServerB";
            this.cbServerB.Size = new System.Drawing.Size(933, 31);
            this.cbServerB.TabIndex = 3;
            this.cbServerB.SelectedIndexChanged += new System.EventHandler(this.cbServerB_SelectedIndexChanged);
            // 
            // btnEditA
            // 
            this.btnEditA.Location = new System.Drawing.Point(1141, 11);
            this.btnEditA.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditA.Name = "btnEditA";
            this.btnEditA.Size = new System.Drawing.Size(69, 32);
            this.btnEditA.TabIndex = 5;
            this.btnEditA.Text = "Edit";
            this.btnEditA.UseVisualStyleBackColor = true;
            this.btnEditA.Click += new System.EventHandler(this.btnEditA_Click);
            // 
            // btnEditB
            // 
            this.btnEditB.Location = new System.Drawing.Point(1141, 51);
            this.btnEditB.Margin = new System.Windows.Forms.Padding(4);
            this.btnEditB.Name = "btnEditB";
            this.btnEditB.Size = new System.Drawing.Size(69, 31);
            this.btnEditB.TabIndex = 6;
            this.btnEditB.Text = "Edit";
            this.btnEditB.UseVisualStyleBackColor = true;
            this.btnEditB.Click += new System.EventHandler(this.btnEditB_Click);
            // 
            // tbMsg
            // 
            this.tbMsg.BackColor = System.Drawing.SystemColors.InfoText;
            this.tbMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMsg.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbMsg.Location = new System.Drawing.Point(119, 369);
            this.tbMsg.Margin = new System.Windows.Forms.Padding(4);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMsg.Size = new System.Drawing.Size(1091, 313);
            this.tbMsg.TabIndex = 8;
            // 
            // btnFlowMigrate
            // 
            this.btnFlowMigrate.Font = new System.Drawing.Font("宋体", 9F);
            this.btnFlowMigrate.Location = new System.Drawing.Point(265, 332);
            this.btnFlowMigrate.Margin = new System.Windows.Forms.Padding(4);
            this.btnFlowMigrate.Name = "btnFlowMigrate";
            this.btnFlowMigrate.Size = new System.Drawing.Size(128, 29);
            this.btnFlowMigrate.TabIndex = 9;
            this.btnFlowMigrate.Text = "导入签核流程";
            this.btnFlowMigrate.UseVisualStyleBackColor = true;
            this.btnFlowMigrate.Click += new System.EventHandler(this.btnFlowMigrate_Click);
            // 
            // tbFilterXml
            // 
            this.tbFilterXml.BackColor = System.Drawing.SystemColors.InfoText;
            this.tbFilterXml.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFilterXml.ForeColor = System.Drawing.Color.White;
            this.tbFilterXml.Location = new System.Drawing.Point(121, 99);
            this.tbFilterXml.Margin = new System.Windows.Forms.Padding(4);
            this.tbFilterXml.Multiline = true;
            this.tbFilterXml.Name = "tbFilterXml";
            this.tbFilterXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFilterXml.Size = new System.Drawing.Size(1091, 232);
            this.tbFilterXml.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(5, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "过滤条件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(2, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "执行结果：";
            // 
            // btnAddA
            // 
            this.btnAddA.Location = new System.Drawing.Point(1061, 11);
            this.btnAddA.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddA.Name = "btnAddA";
            this.btnAddA.Size = new System.Drawing.Size(69, 32);
            this.btnAddA.TabIndex = 13;
            this.btnAddA.Text = "Add";
            this.btnAddA.UseVisualStyleBackColor = true;
            this.btnAddA.Click += new System.EventHandler(this.btnAddA_Click);
            // 
            // btnAddB
            // 
            this.btnAddB.Location = new System.Drawing.Point(1061, 51);
            this.btnAddB.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddB.Name = "btnAddB";
            this.btnAddB.Size = new System.Drawing.Size(69, 31);
            this.btnAddB.TabIndex = 14;
            this.btnAddB.Text = "Add";
            this.btnAddB.UseVisualStyleBackColor = true;
            this.btnAddB.Click += new System.EventHandler(this.btnAddB_Click);
            // 
            // chbIsImportDetail
            // 
            this.chbIsImportDetail.AutoSize = true;
            this.chbIsImportDetail.Font = new System.Drawing.Font("宋体", 9F);
            this.chbIsImportDetail.Location = new System.Drawing.Point(3, 338);
            this.chbIsImportDetail.Name = "chbIsImportDetail";
            this.chbIsImportDetail.Size = new System.Drawing.Size(224, 19);
            this.chbIsImportDetail.TabIndex = 15;
            this.chbIsImportDetail.Text = "是否导入签核知会和签核提醒";
            this.chbIsImportDetail.UseVisualStyleBackColor = true;
            // 
            // tbImportantMsg
            // 
            this.tbImportantMsg.BackColor = System.Drawing.SystemColors.InfoText;
            this.tbImportantMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbImportantMsg.ForeColor = System.Drawing.Color.Red;
            this.tbImportantMsg.Location = new System.Drawing.Point(119, 689);
            this.tbImportantMsg.Multiline = true;
            this.tbImportantMsg.Name = "tbImportantMsg";
            this.tbImportantMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbImportantMsg.Size = new System.Drawing.Size(1091, 153);
            this.tbImportantMsg.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(5, 692);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "重要信息：";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "提示";
            // 
            // ImportFlow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 845);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbImportantMsg);
            this.Controls.Add(this.chbIsImportDetail);
            this.Controls.Add(this.btnAddB);
            this.Controls.Add(this.btnAddA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFilterXml);
            this.Controls.Add(this.btnFlowMigrate);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.btnEditB);
            this.Controls.Add(this.btnEditA);
            this.Controls.Add(this.cbServerB);
            this.Controls.Add(this.cbServerA);
            this.Controls.Add(this.lblServerB);
            this.Controls.Add(this.lblServerA);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ImportFlow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportFlow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerA;
        private System.Windows.Forms.Label lblServerB;
        private System.Windows.Forms.ComboBox cbServerA;
        private System.Windows.Forms.ComboBox cbServerB;
        private System.Windows.Forms.Button btnEditA;
        private System.Windows.Forms.Button btnEditB;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btnFlowMigrate;
        private System.Windows.Forms.TextBox tbFilterXml;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddA;
        private System.Windows.Forms.Button btnAddB;
        private System.Windows.Forms.CheckBox chbIsImportDetail;
        private System.Windows.Forms.TextBox tbImportantMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

