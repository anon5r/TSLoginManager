namespace TSLoginManager
{
    partial class AboutForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.labelAppTitle = new System.Windows.Forms.Label();
            this.labelAppVersion = new System.Windows.Forms.Label();
            this.label_pictureCopyright = new System.Windows.Forms.Label();
            this.label_Copyright = new System.Windows.Forms.Label();
            this.label_TSClient = new System.Windows.Forms.Label();
            this.label_TS_Client_Ver = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.linkLabel_Forum = new System.Windows.Forms.LinkLabel();
            this.label_Support = new System.Windows.Forms.Label();
            this.linkLabel_SupportMail = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Icon
            // 
            this.pictureBox_Icon.Image = global::TSLoginManager.Properties.Resources.AppIcon;
            this.pictureBox_Icon.Location = new System.Drawing.Point(14, 9);
            this.pictureBox_Icon.Name = "pictureBox_Icon";
            this.pictureBox_Icon.Size = new System.Drawing.Size(37, 37);
            this.pictureBox_Icon.TabIndex = 27;
            this.pictureBox_Icon.TabStop = false;
            // 
            // labelAppTitle
            // 
            this.labelAppTitle.AutoSize = true;
            this.labelAppTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelAppTitle.Location = new System.Drawing.Point(64, 9);
            this.labelAppTitle.Name = "labelAppTitle";
            this.labelAppTitle.Size = new System.Drawing.Size(137, 14);
            this.labelAppTitle.TabIndex = 0;
            this.labelAppTitle.Text = "Trickster Login Manager";
            // 
            // labelAppVersion
            // 
            this.labelAppVersion.AutoSize = true;
            this.labelAppVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelAppVersion.Location = new System.Drawing.Point(222, 9);
            this.labelAppVersion.Name = "labelAppVersion";
            this.labelAppVersion.Size = new System.Drawing.Size(74, 14);
            this.labelAppVersion.TabIndex = 1;
            this.labelAppVersion.Text = "version 0.00";
            // 
            // label_pictureCopyright
            // 
            this.label_pictureCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_pictureCopyright.AutoSize = true;
            this.label_pictureCopyright.BackColor = System.Drawing.Color.Transparent;
            this.label_pictureCopyright.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_pictureCopyright.Location = new System.Drawing.Point(12, 73);
            this.label_pictureCopyright.Name = "label_pictureCopyright";
            this.label_pictureCopyright.Size = new System.Drawing.Size(257, 13);
            this.label_pictureCopyright.TabIndex = 9;
            this.label_pictureCopyright.Text = "Pictures copyrights: (C) NTreev Soft, (C) GCREST Inc.";
            // 
            // label_Copyright
            // 
            this.label_Copyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_Copyright.AutoSize = true;
            this.label_Copyright.BackColor = System.Drawing.Color.Transparent;
            this.label_Copyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Copyright.Location = new System.Drawing.Point(181, 61);
            this.label_Copyright.Name = "label_Copyright";
            this.label_Copyright.Size = new System.Drawing.Size(22, 13);
            this.label_Copyright.TabIndex = 7;
            this.label_Copyright.Text = "(C)";
            // 
            // label_TSClient
            // 
            this.label_TSClient.AutoSize = true;
            this.label_TSClient.BackColor = System.Drawing.Color.Transparent;
            this.label_TSClient.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_TSClient.Location = new System.Drawing.Point(64, 23);
            this.label_TSClient.Name = "label_TSClient";
            this.label_TSClient.Size = new System.Drawing.Size(84, 13);
            this.label_TSClient.TabIndex = 2;
            this.label_TSClient.Text = "Trickster Client: ";
            // 
            // label_TS_Client_Ver
            // 
            this.label_TS_Client_Ver.AutoSize = true;
            this.label_TS_Client_Ver.BackColor = System.Drawing.Color.Transparent;
            this.label_TS_Client_Ver.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_TS_Client_Ver.Location = new System.Drawing.Point(156, 23);
            this.label_TS_Client_Ver.Name = "label_TS_Client_Ver";
            this.label_TS_Client_Ver.Size = new System.Drawing.Size(64, 13);
            this.label_TS_Client_Ver.TabIndex = 3;
            this.label_TS_Client_Ver.Text = "version 0.00";
            // 
            // linkLabel
            // 
            this.linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel.AutoSize = true;
            this.linkLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel.DisabledLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(20)))), ((int)(((byte)(22)))));
            this.linkLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel.ForeColor = System.Drawing.Color.Blue;
            this.linkLabel.Location = new System.Drawing.Point(198, 61);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(137, 13);
            this.linkLabel.TabIndex = 8;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "とり☆すた～ごにょごにょ～";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // linkLabel_Forum
            // 
            this.linkLabel_Forum.AutoSize = true;
            this.linkLabel_Forum.Location = new System.Drawing.Point(166, 36);
            this.linkLabel_Forum.Name = "linkLabel_Forum";
            this.linkLabel_Forum.Size = new System.Drawing.Size(77, 14);
            this.linkLabel_Forum.TabIndex = 5;
            this.linkLabel_Forum.TabStop = true;
            this.linkLabel_Forum.Text = "サポート掲示板";
            this.linkLabel_Forum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Forum_LinkClicked);
            // 
            // label_Support
            // 
            this.label_Support.AutoSize = true;
            this.label_Support.Location = new System.Drawing.Point(64, 36);
            this.label_Support.Name = "label_Support";
            this.label_Support.Size = new System.Drawing.Size(81, 14);
            this.label_Support.TabIndex = 4;
            this.label_Support.Text = "サポート連絡先:";
            // 
            // linkLabel_SupportMail
            // 
            this.linkLabel_SupportMail.AutoSize = true;
            this.linkLabel_SupportMail.Location = new System.Drawing.Point(265, 36);
            this.linkLabel_SupportMail.Name = "linkLabel_SupportMail";
            this.linkLabel_SupportMail.Size = new System.Drawing.Size(37, 14);
            this.linkLabel_SupportMail.TabIndex = 6;
            this.linkLabel_SupportMail.TabStop = true;
            this.linkLabel_SupportMail.Text = "メール";
            this.linkLabel_SupportMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_SupportMail_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 96);
            this.Controls.Add(this.linkLabel_SupportMail);
            this.Controls.Add(this.label_Support);
            this.Controls.Add(this.linkLabel_Forum);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.label_TS_Client_Ver);
            this.Controls.Add(this.label_TSClient);
            this.Controls.Add(this.label_Copyright);
            this.Controls.Add(this.label_pictureCopyright);
            this.Controls.Add(this.labelAppVersion);
            this.Controls.Add(this.labelAppTitle);
            this.Controls.Add(this.pictureBox_Icon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Opacity = 0;
            this.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TSLoginManager";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.Shown += new System.EventHandler(this.AboutForm_Shown);
            this.Click += new System.EventHandler(this.AboutForm_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AboutForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Icon;
        private System.Windows.Forms.Label labelAppTitle;
        private System.Windows.Forms.Label labelAppVersion;
        private System.Windows.Forms.Label label_pictureCopyright;
        private System.Windows.Forms.Label label_Copyright;
        private System.Windows.Forms.Label label_TSClient;
        private System.Windows.Forms.Label label_TS_Client_Ver;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.LinkLabel linkLabel_Forum;
        private System.Windows.Forms.Label label_Support;
        private System.Windows.Forms.LinkLabel linkLabel_SupportMail;


    }
}
