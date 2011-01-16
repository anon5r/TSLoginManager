namespace TricksterTools.Plugins.EmergencyButton
{
    partial class EmergencyStopForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        //private System.ComponentModel.IContainer components = null;
        
        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmergencyStopForm));
            this.btnRunStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRunStop
            // 
            this.btnRunStop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunStop.CausesValidation = false;
            this.btnRunStop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRunStop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRunStop.Image = global::TricksterTools.Plugins.EmergencyButton.Properties.Resources.Delete;
            this.btnRunStop.Location = new System.Drawing.Point(12, 12);
            this.btnRunStop.Name = "btnRunStop";
            this.btnRunStop.Size = new System.Drawing.Size(92, 76);
            this.btnRunStop.TabIndex = 0;
            this.btnRunStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRunStop.UseVisualStyleBackColor = true;
            this.btnRunStop.Click += new System.EventHandler(this.btnRunStop_Click);
            // 
            // EmergencyStopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(116, 100);
            this.Controls.Add(this.btnRunStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EmergencyStopForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Emergency Button";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.EmergencyStopForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRunStop;
    }
}