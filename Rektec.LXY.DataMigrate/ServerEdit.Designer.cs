
namespace Rektec.LXY.DataMigrate
{
    partial class ServerEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.btnSure = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbDomain = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbAuthType = new System.Windows.Forms.ComboBox();
            this.tbOrgName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.Server = new System.Windows.Forms.Label();
            this.chkIsCheckConfig = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(111, 148);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Url：";
            this.toolTip1.SetToolTip(this.label1, "服务器地址，格式：http://192.168.13.150");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 257);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "UserName：";
            this.toolTip1.SetToolTip(this.label2, "用户名称，格式：crmadmin");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 301);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password：";
            this.toolTip1.SetToolTip(this.label3, "用户密码");
            // 
            // tbUrl
            // 
            this.tbUrl.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUrl.Location = new System.Drawing.Point(172, 144);
            this.tbUrl.Margin = new System.Windows.Forms.Padding(4);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(496, 30);
            this.tbUrl.TabIndex = 3;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPassword.Location = new System.Drawing.Point(172, 293);
            this.tbPassword.Margin = new System.Windows.Forms.Padding(4);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(496, 30);
            this.tbPassword.TabIndex = 6;
            // 
            // tbUserName
            // 
            this.tbUserName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbUserName.Location = new System.Drawing.Point(172, 248);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(4);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(496, 30);
            this.tbUserName.TabIndex = 5;
            // 
            // btnSure
            // 
            this.btnSure.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSure.Location = new System.Drawing.Point(172, 437);
            this.btnSure.Margin = new System.Windows.Forms.Padding(4);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(105, 44);
            this.btnSure.TabIndex = 9;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = true;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(350, 437);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 44);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbDomain
            // 
            this.tbDomain.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbDomain.Location = new System.Drawing.Point(172, 343);
            this.tbDomain.Margin = new System.Windows.Forms.Padding(4);
            this.tbDomain.Name = "tbDomain";
            this.tbDomain.Size = new System.Drawing.Size(496, 30);
            this.tbDomain.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 351);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Domain：";
            this.toolTip1.SetToolTip(this.label4, "域名称");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(62, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "AuthType：";
            this.toolTip1.SetToolTip(this.label5, "目前只支持AD和ADFS,如果部署了ADFS则不能选AD");
            // 
            // cbAuthType
            // 
            this.cbAuthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAuthType.FormattingEnabled = true;
            this.cbAuthType.Items.AddRange(new object[] {
            "AD",
            "ADFS"});
            this.cbAuthType.Location = new System.Drawing.Point(171, 98);
            this.cbAuthType.Margin = new System.Windows.Forms.Padding(4);
            this.cbAuthType.Name = "cbAuthType";
            this.cbAuthType.Size = new System.Drawing.Size(496, 28);
            this.cbAuthType.TabIndex = 2;
            // 
            // tbOrgName
            // 
            this.tbOrgName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbOrgName.Location = new System.Drawing.Point(172, 195);
            this.tbOrgName.Margin = new System.Windows.Forms.Padding(4);
            this.tbOrgName.Name = "tbOrgName";
            this.tbOrgName.Size = new System.Drawing.Size(496, 30);
            this.tbOrgName.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(72, 199);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "OrgName：";
            this.toolTip1.SetToolTip(this.label6, "组织名称，格式：handev");
            // 
            // cbServer
            // 
            this.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServer.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Items.AddRange(new object[] {
            "ServerA",
            "ServerB"});
            this.cbServer.Location = new System.Drawing.Point(170, 53);
            this.cbServer.Margin = new System.Windows.Forms.Padding(4);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(496, 28);
            this.cbServer.TabIndex = 1;
            // 
            // Server
            // 
            this.Server.AutoSize = true;
            this.Server.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Server.Location = new System.Drawing.Point(81, 53);
            this.Server.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(89, 20);
            this.Server.TabIndex = 14;
            this.Server.Text = "Server：";
            // 
            // chkIsCheckConfig
            // 
            this.chkIsCheckConfig.AutoSize = true;
            this.chkIsCheckConfig.Location = new System.Drawing.Point(172, 392);
            this.chkIsCheckConfig.Name = "chkIsCheckConfig";
            this.chkIsCheckConfig.Size = new System.Drawing.Size(117, 19);
            this.chkIsCheckConfig.TabIndex = 8;
            this.chkIsCheckConfig.Text = "CheckConfig";
            this.chkIsCheckConfig.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 393);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "IsCheckConfig：";
            this.toolTip1.SetToolTip(this.label7, "是否检查配置信息是否正确");
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "提示";
            // 
            // ServerEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 529);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkIsCheckConfig);
            this.Controls.Add(this.cbServer);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.tbOrgName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbAuthType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDomain);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ServerEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ServerEdit";
            this.toolTip1.SetToolTip(this, "用户密码");
            this.Load += new System.EventHandler(this.ServerEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Button btnSure;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbDomain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbAuthType;
        private System.Windows.Forms.TextBox tbOrgName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.Label Server;
        private System.Windows.Forms.CheckBox chkIsCheckConfig;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}