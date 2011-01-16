namespace TSLoginManager
{
    partial class PluginInfoForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_OK = new System.Windows.Forms.Button();
            this.label_PluginName = new System.Windows.Forms.Label();
            this.groupBox_Description = new System.Windows.Forms.GroupBox();
            this.textBox_Descriptions = new System.Windows.Forms.TextBox();
            this.label_Column_Version = new System.Windows.Forms.Label();
            this.label_Column_Author = new System.Windows.Forms.Label();
            this.label_Version = new System.Windows.Forms.Label();
            this.label_Column_Support = new System.Windows.Forms.Label();
            this.linkLabel_Support = new System.Windows.Forms.LinkLabel();
            this.label_Author = new System.Windows.Forms.Label();
            this.groupBox_Description.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(109, 299);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(121, 29);
            this.button_OK.TabIndex = 8;
            this.button_OK.Text = "&OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // label_PluginName
            // 
            this.label_PluginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_PluginName.AutoSize = true;
            this.label_PluginName.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_PluginName.Location = new System.Drawing.Point(27, 22);
            this.label_PluginName.MinimumSize = new System.Drawing.Size(285, 0);
            this.label_PluginName.Name = "label_PluginName";
            this.label_PluginName.Size = new System.Drawing.Size(285, 15);
            this.label_PluginName.TabIndex = 0;
            this.label_PluginName.Text = "Plugin Name";
            this.label_PluginName.DoubleClick += new System.EventHandler(this.label_PluginName_DoubleClick);
            // 
            // groupBox_Description
            // 
            this.groupBox_Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Description.Controls.Add(this.textBox_Descriptions);
            this.groupBox_Description.Location = new System.Drawing.Point(30, 133);
            this.groupBox_Description.Name = "groupBox_Description";
            this.groupBox_Description.Size = new System.Drawing.Size(282, 151);
            this.groupBox_Description.TabIndex = 7;
            this.groupBox_Description.TabStop = false;
            this.groupBox_Description.Text = "説明";
            // 
            // textBox_Descriptions
            // 
            this.textBox_Descriptions.AcceptsReturn = true;
            this.textBox_Descriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Descriptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Descriptions.Location = new System.Drawing.Point(7, 21);
            this.textBox_Descriptions.Multiline = true;
            this.textBox_Descriptions.Name = "textBox_Descriptions";
            this.textBox_Descriptions.ReadOnly = true;
            this.textBox_Descriptions.Size = new System.Drawing.Size(264, 123);
            this.textBox_Descriptions.TabIndex = 0;
            // 
            // label_Column_Version
            // 
            this.label_Column_Version.AutoSize = true;
            this.label_Column_Version.Location = new System.Drawing.Point(27, 51);
            this.label_Column_Version.Name = "label_Column_Version";
            this.label_Column_Version.Size = new System.Drawing.Size(61, 14);
            this.label_Column_Version.TabIndex = 1;
            this.label_Column_Version.Text = "バージョン:";
            // 
            // label_Column_Author
            // 
            this.label_Column_Author.AutoSize = true;
            this.label_Column_Author.Location = new System.Drawing.Point(27, 75);
            this.label_Column_Author.Name = "label_Column_Author";
            this.label_Column_Author.Size = new System.Drawing.Size(41, 14);
            this.label_Column_Author.TabIndex = 3;
            this.label_Column_Author.Text = "作成者:";
            // 
            // label_Version
            // 
            this.label_Version.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Version.AutoSize = true;
            this.label_Version.Location = new System.Drawing.Point(85, 51);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(47, 14);
            this.label_Version.TabIndex = 2;
            this.label_Version.Text = "1.0.0.0";
            // 
            // label_Column_Support
            // 
            this.label_Column_Support.AutoSize = true;
            this.label_Column_Support.Location = new System.Drawing.Point(27, 100);
            this.label_Column_Support.Name = "label_Column_Support";
            this.label_Column_Support.Size = new System.Drawing.Size(51, 14);
            this.label_Column_Support.TabIndex = 5;
            this.label_Column_Support.Text = "サポート:";
            // 
            // linkLabel_Support
            // 
            this.linkLabel_Support.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel_Support.AutoSize = true;
            this.linkLabel_Support.Location = new System.Drawing.Point(85, 100);
            this.linkLabel_Support.Name = "linkLabel_Support";
            this.linkLabel_Support.Size = new System.Drawing.Size(122, 14);
            this.linkLabel_Support.TabIndex = 6;
            this.linkLabel_Support.TabStop = true;
            this.linkLabel_Support.Text = "http://example.com/";
            this.linkLabel_Support.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // label_Author
            // 
            this.label_Author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Author.AutoSize = true;
            this.label_Author.Location = new System.Drawing.Point(85, 75);
            this.label_Author.Name = "label_Author";
            this.label_Author.Size = new System.Drawing.Size(57, 14);
            this.label_Author.TabIndex = 4;
            this.label_Author.Text = "someone";
            // 
            // PluginInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 340);
            this.Controls.Add(this.label_Author);
            this.Controls.Add(this.linkLabel_Support);
            this.Controls.Add(this.label_Column_Support);
            this.Controls.Add(this.label_Version);
            this.Controls.Add(this.label_Column_Author);
            this.Controls.Add(this.label_Column_Version);
            this.Controls.Add(this.groupBox_Description);
            this.Controls.Add(this.label_PluginName);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginInfoForm";
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "プラグイン情報";
            this.Load += new System.EventHandler(this.PluginInfoForm_Load);
            this.Shown += new System.EventHandler(this.PluginInfoForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginInfoForm_FormClosing);
            this.groupBox_Description.ResumeLayout(false);
            this.groupBox_Description.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label_PluginName;
        private System.Windows.Forms.GroupBox groupBox_Description;
        private System.Windows.Forms.Label label_Column_Version;
        private System.Windows.Forms.Label label_Column_Author;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Label label_Column_Support;
        private System.Windows.Forms.LinkLabel linkLabel_Support;
        private System.Windows.Forms.Label label_Author;
        private System.Windows.Forms.TextBox textBox_Descriptions;
    }
}