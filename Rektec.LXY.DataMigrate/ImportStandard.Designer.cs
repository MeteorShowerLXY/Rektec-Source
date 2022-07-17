
namespace Rektec.LXY.DataMigrate
{
    partial class ImportStandard
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
            this.chkAutoNumber = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDetailSum = new System.Windows.Forms.CheckBox();
            this.chkRepeatCheck = new System.Windows.Forms.CheckBox();
            this.chkDefinedButton = new System.Windows.Forms.CheckBox();
            this.chkSystemParameters = new System.Windows.Forms.CheckBox();
            this.chkAllSelect = new System.Windows.Forms.CheckBox();
            this.cbServerB = new System.Windows.Forms.ComboBox();
            this.cbServerA = new System.Windows.Forms.ComboBox();
            this.lblServerB = new System.Windows.Forms.Label();
            this.lblServerA = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblIsSpecifiedConditionTip = new System.Windows.Forms.Label();
            this.tbFilterXml = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.chkIsSpecifiedCondition = new System.Windows.Forms.CheckBox();
            this.chkIsUpdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkAutoNumber
            // 
            this.chkAutoNumber.AutoSize = true;
            this.chkAutoNumber.Location = new System.Drawing.Point(15, 144);
            this.chkAutoNumber.Name = "chkAutoNumber";
            this.chkAutoNumber.Size = new System.Drawing.Size(89, 19);
            this.chkAutoNumber.TabIndex = 0;
            this.chkAutoNumber.Text = "自动编号";
            this.chkAutoNumber.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择需要迁移的标准功能配置：";
            // 
            // chkDetailSum
            // 
            this.chkDetailSum.AutoSize = true;
            this.chkDetailSum.Location = new System.Drawing.Point(119, 144);
            this.chkDetailSum.Name = "chkDetailSum";
            this.chkDetailSum.Size = new System.Drawing.Size(89, 19);
            this.chkDetailSum.TabIndex = 2;
            this.chkDetailSum.Text = "明细汇总";
            this.chkDetailSum.UseVisualStyleBackColor = true;
            // 
            // chkRepeatCheck
            // 
            this.chkRepeatCheck.AutoSize = true;
            this.chkRepeatCheck.Location = new System.Drawing.Point(234, 144);
            this.chkRepeatCheck.Name = "chkRepeatCheck";
            this.chkRepeatCheck.Size = new System.Drawing.Size(89, 19);
            this.chkRepeatCheck.TabIndex = 3;
            this.chkRepeatCheck.Text = "重复检测";
            this.chkRepeatCheck.UseVisualStyleBackColor = true;
            // 
            // chkDefinedButton
            // 
            this.chkDefinedButton.AutoSize = true;
            this.chkDefinedButton.Location = new System.Drawing.Point(350, 144);
            this.chkDefinedButton.Name = "chkDefinedButton";
            this.chkDefinedButton.Size = new System.Drawing.Size(104, 19);
            this.chkDefinedButton.TabIndex = 4;
            this.chkDefinedButton.Text = "自定义按钮";
            this.chkDefinedButton.UseVisualStyleBackColor = true;
            // 
            // chkSystemParameters
            // 
            this.chkSystemParameters.AutoSize = true;
            this.chkSystemParameters.Location = new System.Drawing.Point(460, 144);
            this.chkSystemParameters.Name = "chkSystemParameters";
            this.chkSystemParameters.Size = new System.Drawing.Size(89, 19);
            this.chkSystemParameters.TabIndex = 5;
            this.chkSystemParameters.Text = "系统参数";
            this.chkSystemParameters.UseVisualStyleBackColor = true;
            // 
            // chkAllSelect
            // 
            this.chkAllSelect.AutoSize = true;
            this.chkAllSelect.Location = new System.Drawing.Point(231, 112);
            this.chkAllSelect.Name = "chkAllSelect";
            this.chkAllSelect.Size = new System.Drawing.Size(112, 19);
            this.chkAllSelect.TabIndex = 6;
            this.chkAllSelect.Text = "全选/全不选";
            this.chkAllSelect.UseVisualStyleBackColor = true;
            this.chkAllSelect.CheckedChanged += new System.EventHandler(this.chkAllSelect_CheckedChanged);
            // 
            // cbServerB
            // 
            this.cbServerB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServerB.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbServerB.FormattingEnabled = true;
            this.cbServerB.Location = new System.Drawing.Point(125, 51);
            this.cbServerB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbServerB.Name = "cbServerB";
            this.cbServerB.Size = new System.Drawing.Size(1146, 31);
            this.cbServerB.TabIndex = 10;
            this.cbServerB.SelectedIndexChanged += new System.EventHandler(this.cbServerB_SelectedIndexChanged);
            // 
            // cbServerA
            // 
            this.cbServerA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServerA.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbServerA.FormattingEnabled = true;
            this.cbServerA.Location = new System.Drawing.Point(125, 11);
            this.cbServerA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbServerA.Name = "cbServerA";
            this.cbServerA.Size = new System.Drawing.Size(1146, 31);
            this.cbServerA.TabIndex = 9;
            this.cbServerA.SelectedIndexChanged += new System.EventHandler(this.cbServerA_SelectedIndexChanged);
            // 
            // lblServerB
            // 
            this.lblServerB.AutoSize = true;
            this.lblServerB.Font = new System.Drawing.Font("宋体", 13.8F);
            this.lblServerB.Location = new System.Drawing.Point(11, 54);
            this.lblServerB.Name = "lblServerB";
            this.lblServerB.Size = new System.Drawing.Size(118, 24);
            this.lblServerB.TabIndex = 8;
            this.lblServerB.Text = "ServerB：";
            // 
            // lblServerA
            // 
            this.lblServerA.AutoSize = true;
            this.lblServerA.Font = new System.Drawing.Font("宋体", 13.8F);
            this.lblServerA.Location = new System.Drawing.Point(11, 14);
            this.lblServerA.Name = "lblServerA";
            this.lblServerA.Size = new System.Drawing.Size(118, 24);
            this.lblServerA.TabIndex = 7;
            this.lblServerA.Text = "ServerA：";
            // 
            // btnImport
            // 
            this.btnImport.Font = new System.Drawing.Font("宋体", 9F);
            this.btnImport.Location = new System.Drawing.Point(267, 425);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(138, 39);
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "迁移";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblIsSpecifiedConditionTip
            // 
            this.lblIsSpecifiedConditionTip.AutoSize = true;
            this.lblIsSpecifiedConditionTip.Font = new System.Drawing.Font("宋体", 12F);
            this.lblIsSpecifiedConditionTip.Location = new System.Drawing.Point(145, 181);
            this.lblIsSpecifiedConditionTip.Name = "lblIsSpecifiedConditionTip";
            this.lblIsSpecifiedConditionTip.Size = new System.Drawing.Size(749, 20);
            this.lblIsSpecifiedConditionTip.TabIndex = 13;
            this.lblIsSpecifiedConditionTip.Text = "指定条件是只能选择其中一个标准配置进行迁移(非指定条件则获取启用的所有数据)";
            // 
            // tbFilterXml
            // 
            this.tbFilterXml.BackColor = System.Drawing.SystemColors.InfoText;
            this.tbFilterXml.Enabled = false;
            this.tbFilterXml.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFilterXml.ForeColor = System.Drawing.Color.White;
            this.tbFilterXml.Location = new System.Drawing.Point(13, 206);
            this.tbFilterXml.Margin = new System.Windows.Forms.Padding(4);
            this.tbFilterXml.Multiline = true;
            this.tbFilterXml.Name = "tbFilterXml";
            this.tbFilterXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFilterXml.Size = new System.Drawing.Size(1257, 211);
            this.tbFilterXml.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(11, 474);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "执行结果：";
            // 
            // tbMsg
            // 
            this.tbMsg.BackColor = System.Drawing.SystemColors.InfoText;
            this.tbMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMsg.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.tbMsg.Location = new System.Drawing.Point(14, 498);
            this.tbMsg.Margin = new System.Windows.Forms.Padding(4);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMsg.Size = new System.Drawing.Size(1256, 317);
            this.tbMsg.TabIndex = 14;
            // 
            // chkIsSpecifiedCondition
            // 
            this.chkIsSpecifiedCondition.AutoSize = true;
            this.chkIsSpecifiedCondition.Location = new System.Drawing.Point(15, 181);
            this.chkIsSpecifiedCondition.Name = "chkIsSpecifiedCondition";
            this.chkIsSpecifiedCondition.Size = new System.Drawing.Size(119, 19);
            this.chkIsSpecifiedCondition.TabIndex = 16;
            this.chkIsSpecifiedCondition.Text = "是否指定条件";
            this.chkIsSpecifiedCondition.UseVisualStyleBackColor = true;
            this.chkIsSpecifiedCondition.CheckedChanged += new System.EventHandler(this.chkIsSpecifiedCondition_CheckedChanged);
            // 
            // chkIsUpdate
            // 
            this.chkIsUpdate.AutoSize = true;
            this.chkIsUpdate.Location = new System.Drawing.Point(15, 436);
            this.chkIsUpdate.Name = "chkIsUpdate";
            this.chkIsUpdate.Size = new System.Drawing.Size(224, 19);
            this.chkIsUpdate.TabIndex = 17;
            this.chkIsUpdate.Text = "存在的记录是否执行更新操作";
            this.chkIsUpdate.UseVisualStyleBackColor = true;
            // 
            // ImportStandard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 828);
            this.Controls.Add(this.chkIsUpdate);
            this.Controls.Add(this.chkSystemParameters);
            this.Controls.Add(this.chkIsSpecifiedCondition);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.lblIsSpecifiedConditionTip);
            this.Controls.Add(this.tbFilterXml);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.cbServerB);
            this.Controls.Add(this.cbServerA);
            this.Controls.Add(this.lblServerB);
            this.Controls.Add(this.lblServerA);
            this.Controls.Add(this.chkAllSelect);
            this.Controls.Add(this.chkDefinedButton);
            this.Controls.Add(this.chkRepeatCheck);
            this.Controls.Add(this.chkDetailSum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkAutoNumber);
            this.Name = "ImportStandard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "迁移标准功能配置";
            this.Load += new System.EventHandler(this.ImportStandard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAutoNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDetailSum;
        private System.Windows.Forms.CheckBox chkRepeatCheck;
        private System.Windows.Forms.CheckBox chkDefinedButton;
        private System.Windows.Forms.CheckBox chkSystemParameters;
        private System.Windows.Forms.CheckBox chkAllSelect;
        private System.Windows.Forms.ComboBox cbServerB;
        private System.Windows.Forms.ComboBox cbServerA;
        private System.Windows.Forms.Label lblServerB;
        private System.Windows.Forms.Label lblServerA;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblIsSpecifiedConditionTip;
        private System.Windows.Forms.TextBox tbFilterXml;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.CheckBox chkIsSpecifiedCondition;
        private System.Windows.Forms.CheckBox chkIsUpdate;
    }
}