
namespace Rektec.LXY.DataMigrate
{
    partial class MainForm
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
            this.btnFlow = new System.Windows.Forms.Button();
            this.btnStandard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFlow
            // 
            this.btnFlow.Location = new System.Drawing.Point(25, 25);
            this.btnFlow.Name = "btnFlow";
            this.btnFlow.Size = new System.Drawing.Size(128, 45);
            this.btnFlow.TabIndex = 0;
            this.btnFlow.Text = "迁移签核流程";
            this.btnFlow.UseVisualStyleBackColor = true;
            this.btnFlow.Click += new System.EventHandler(this.btnFlow_Click);
            // 
            // btnStandard
            // 
            this.btnStandard.Location = new System.Drawing.Point(223, 25);
            this.btnStandard.Name = "btnStandard";
            this.btnStandard.Size = new System.Drawing.Size(128, 45);
            this.btnStandard.TabIndex = 1;
            this.btnStandard.Text = "迁移标准配置";
            this.btnStandard.UseVisualStyleBackColor = true;
            this.btnStandard.Click += new System.EventHandler(this.btnStandard_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStandard);
            this.Controls.Add(this.btnFlow);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗体";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFlow;
        private System.Windows.Forms.Button btnStandard;
    }
}