namespace TSLoginManager
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.UpDown_FreezeTime = new System.Windows.Forms.NumericUpDown();
            this.lbl_sec = new System.Windows.Forms.Label();
            this.btn_setting = new System.Windows.Forms.Button();
            this.groupBox_freeze_settings = new System.Windows.Forms.GroupBox();
            this.lbl_freeze_setting_description = new System.Windows.Forms.Label();
            this.checkBox_Freeze_Setting_Enable = new System.Windows.Forms.CheckBox();
            this.groupBox_update_settings = new System.Windows.Forms.GroupBox();
            this.checkBox_isCheckBetaVersion = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoUpdateCheck = new System.Windows.Forms.CheckBox();
            this.button_ChangeEncryptKey = new System.Windows.Forms.Button();
            this.groupBox_WayOfStartUpOfGame = new System.Windows.Forms.GroupBox();
            this.radioButton_WayOfGameStartUp_Launcher = new System.Windows.Forms.RadioButton();
            this.radioButton_WayOfGameStartUp_Direct = new System.Windows.Forms.RadioButton();
            this.groupBox_trayIcon = new System.Windows.Forms.GroupBox();
            this.hScrollBar_change_icon = new System.Windows.Forms.HScrollBar();
            this.pictureBox_icon_view = new System.Windows.Forms.PictureBox();
            this.groupBox_DebugFunction = new System.Windows.Forms.GroupBox();
            this.button_FolderDiag = new System.Windows.Forms.Button();
            this.checkBox_OutputLog = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_FreezeTime)).BeginInit();
            this.groupBox_freeze_settings.SuspendLayout();
            this.groupBox_update_settings.SuspendLayout();
            this.groupBox_WayOfStartUpOfGame.SuspendLayout();
            this.groupBox_trayIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon_view)).BeginInit();
            this.groupBox_DebugFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // UpDown_FreezeTime
            // 
            this.UpDown_FreezeTime.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UpDown_FreezeTime.Location = new System.Drawing.Point(10, 37);
            this.UpDown_FreezeTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.UpDown_FreezeTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDown_FreezeTime.Name = "UpDown_FreezeTime";
            this.UpDown_FreezeTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.UpDown_FreezeTime.Size = new System.Drawing.Size(65, 23);
            this.UpDown_FreezeTime.TabIndex = 1;
            this.UpDown_FreezeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UpDown_FreezeTime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lbl_sec
            // 
            this.lbl_sec.AutoSize = true;
            this.lbl_sec.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_sec.Location = new System.Drawing.Point(77, 41);
            this.lbl_sec.Name = "lbl_sec";
            this.lbl_sec.Size = new System.Drawing.Size(24, 16);
            this.lbl_sec.TabIndex = 2;
            this.lbl_sec.Text = "秒";
            // 
            // btn_setting
            // 
            this.btn_setting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_setting.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_setting.Location = new System.Drawing.Point(152, 308);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(66, 25);
            this.btn_setting.TabIndex = 6;
            this.btn_setting.Text = "設定(&S)";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // groupBox_freeze_settings
            // 
            this.groupBox_freeze_settings.Controls.Add(this.lbl_freeze_setting_description);
            this.groupBox_freeze_settings.Controls.Add(this.lbl_sec);
            this.groupBox_freeze_settings.Controls.Add(this.UpDown_FreezeTime);
            this.groupBox_freeze_settings.Location = new System.Drawing.Point(12, 23);
            this.groupBox_freeze_settings.Name = "groupBox_freeze_settings";
            this.groupBox_freeze_settings.Size = new System.Drawing.Size(206, 66);
            this.groupBox_freeze_settings.TabIndex = 1;
            this.groupBox_freeze_settings.TabStop = false;
            // 
            // lbl_freeze_setting_description
            // 
            this.lbl_freeze_setting_description.AutoSize = true;
            this.lbl_freeze_setting_description.Location = new System.Drawing.Point(8, 11);
            this.lbl_freeze_setting_description.Name = "lbl_freeze_setting_description";
            this.lbl_freeze_setting_description.Size = new System.Drawing.Size(185, 24);
            this.lbl_freeze_setting_description.TabIndex = 0;
            this.lbl_freeze_setting_description.Text = "フリーズ判定をする時間を設定します。" + System.Environment.NewLine + "※短くしすぎに注意。";
            // 
            // checkBox_Freeze_Setting_Enable
            // 
            this.checkBox_Freeze_Setting_Enable.AutoSize = true;
            this.checkBox_Freeze_Setting_Enable.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox_Freeze_Setting_Enable.Location = new System.Drawing.Point(15, 12);
            this.checkBox_Freeze_Setting_Enable.Name = "checkBox_Freeze_Setting_Enable";
            this.checkBox_Freeze_Setting_Enable.Size = new System.Drawing.Size(107, 16);
            this.checkBox_Freeze_Setting_Enable.TabIndex = 0;
            this.checkBox_Freeze_Setting_Enable.Text = "フリーズ検出時間";
            this.checkBox_Freeze_Setting_Enable.UseVisualStyleBackColor = false;
            this.checkBox_Freeze_Setting_Enable.CheckedChanged += new System.EventHandler(this.checkBox_Freeze_Setting_Enable_CheckedChanged);
            // 
            // groupBox_update_settings
            // 
            this.groupBox_update_settings.Controls.Add(this.checkBox_isCheckBetaVersion);
            this.groupBox_update_settings.Controls.Add(this.checkBox_AutoUpdateCheck);
            this.groupBox_update_settings.Location = new System.Drawing.Point(12, 95);
            this.groupBox_update_settings.Name = "groupBox_update_settings";
            this.groupBox_update_settings.Size = new System.Drawing.Size(206, 67);
            this.groupBox_update_settings.TabIndex = 2;
            this.groupBox_update_settings.TabStop = false;
            this.groupBox_update_settings.Text = "最新版の確認";
            // 
            // checkBox_isCheckBetaVersion
            // 
            this.checkBox_isCheckBetaVersion.AutoSize = true;
            this.checkBox_isCheckBetaVersion.Location = new System.Drawing.Point(10, 40);
            this.checkBox_isCheckBetaVersion.Name = "checkBox_isCheckBetaVersion";
            this.checkBox_isCheckBetaVersion.Size = new System.Drawing.Size(166, 16);
            this.checkBox_isCheckBetaVersion.TabIndex = 1;
            this.checkBox_isCheckBetaVersion.Text = "ベータ版の場合も通知する(&B)";
            this.checkBox_isCheckBetaVersion.UseVisualStyleBackColor = true;
            // 
            // checkBox_AutoUpdateCheck
            // 
            this.checkBox_AutoUpdateCheck.AutoSize = true;
            this.checkBox_AutoUpdateCheck.Location = new System.Drawing.Point(10, 18);
            this.checkBox_AutoUpdateCheck.Name = "checkBox_AutoUpdateCheck";
            this.checkBox_AutoUpdateCheck.Size = new System.Drawing.Size(173, 16);
            this.checkBox_AutoUpdateCheck.TabIndex = 0;
            this.checkBox_AutoUpdateCheck.Text = "起動時に自動的に確認する(&A)";
            this.checkBox_AutoUpdateCheck.UseVisualStyleBackColor = true;
            // 
            // button_ChangeEncryptKey
            // 
            this.button_ChangeEncryptKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ChangeEncryptKey.Location = new System.Drawing.Point(12, 308);
            this.button_ChangeEncryptKey.Name = "button_ChangeEncryptKey";
            this.button_ChangeEncryptKey.Size = new System.Drawing.Size(119, 24);
            this.button_ChangeEncryptKey.TabIndex = 5;
            this.button_ChangeEncryptKey.Text = "暗号化キー変更(&E)";
            this.button_ChangeEncryptKey.UseVisualStyleBackColor = true;
            this.button_ChangeEncryptKey.Click += new System.EventHandler(this.button_ChangeEncryptKey_Click);
            // 
            // groupBox_WayOfStartUpOfGame
            // 
            this.groupBox_WayOfStartUpOfGame.Controls.Add(this.radioButton_WayOfGameStartUp_Launcher);
            this.groupBox_WayOfStartUpOfGame.Controls.Add(this.radioButton_WayOfGameStartUp_Direct);
            this.groupBox_WayOfStartUpOfGame.Location = new System.Drawing.Point(12, 168);
            this.groupBox_WayOfStartUpOfGame.Name = "groupBox_WayOfStartUpOfGame";
            this.groupBox_WayOfStartUpOfGame.Size = new System.Drawing.Size(206, 43);
            this.groupBox_WayOfStartUpOfGame.TabIndex = 3;
            this.groupBox_WayOfStartUpOfGame.TabStop = false;
            this.groupBox_WayOfStartUpOfGame.Text = "ゲーム起動方法";
            // 
            // radioButton_WayOfGameStartUp_Launcher
            // 
            this.radioButton_WayOfGameStartUp_Launcher.AutoSize = true;
            this.radioButton_WayOfGameStartUp_Launcher.Location = new System.Drawing.Point(112, 18);
            this.radioButton_WayOfGameStartUp_Launcher.Name = "radioButton_WayOfGameStartUp_Launcher";
            this.radioButton_WayOfGameStartUp_Launcher.Size = new System.Drawing.Size(91, 16);
            this.radioButton_WayOfGameStartUp_Launcher.TabIndex = 1;
            this.radioButton_WayOfGameStartUp_Launcher.Text = "ランチャー経由";
            this.radioButton_WayOfGameStartUp_Launcher.UseVisualStyleBackColor = true;
            // 
            // radioButton_WayOfGameStartUp_Direct
            // 
            this.radioButton_WayOfGameStartUp_Direct.AutoSize = true;
            this.radioButton_WayOfGameStartUp_Direct.Checked = true;
            this.radioButton_WayOfGameStartUp_Direct.Location = new System.Drawing.Point(10, 18);
            this.radioButton_WayOfGameStartUp_Direct.Name = "radioButton_WayOfGameStartUp_Direct";
            this.radioButton_WayOfGameStartUp_Direct.Size = new System.Drawing.Size(71, 16);
            this.radioButton_WayOfGameStartUp_Direct.TabIndex = 0;
            this.radioButton_WayOfGameStartUp_Direct.TabStop = true;
            this.radioButton_WayOfGameStartUp_Direct.Text = "直接起動";
            this.radioButton_WayOfGameStartUp_Direct.UseVisualStyleBackColor = true;
            // 
            // groupBox_trayIcon
            // 
            this.groupBox_trayIcon.Controls.Add(this.hScrollBar_change_icon);
            this.groupBox_trayIcon.Controls.Add(this.pictureBox_icon_view);
            this.groupBox_trayIcon.Location = new System.Drawing.Point(13, 217);
            this.groupBox_trayIcon.Name = "groupBox_trayIcon";
            this.groupBox_trayIcon.Size = new System.Drawing.Size(205, 42);
            this.groupBox_trayIcon.TabIndex = 4;
            this.groupBox_trayIcon.TabStop = false;
            this.groupBox_trayIcon.Text = "トレイアイコン";
            // 
            // hScrollBar_change_icon
            // 
            this.hScrollBar_change_icon.LargeChange = 3;
            this.hScrollBar_change_icon.Location = new System.Drawing.Point(6, 15);
            this.hScrollBar_change_icon.Maximum = 25;
            this.hScrollBar_change_icon.Name = "hScrollBar_change_icon";
            this.hScrollBar_change_icon.Size = new System.Drawing.Size(158, 16);
            this.hScrollBar_change_icon.TabIndex = 0;
            this.hScrollBar_change_icon.ValueChanged += new System.EventHandler(this.hScrollBar_change_icon_ValueChanged);
            // 
            // pictureBox_icon_view
            // 
            this.pictureBox_icon_view.Location = new System.Drawing.Point(175, 12);
            this.pictureBox_icon_view.Name = "pictureBox_icon_view";
            this.pictureBox_icon_view.Size = new System.Drawing.Size(22, 22);
            this.pictureBox_icon_view.TabIndex = 0;
            this.pictureBox_icon_view.TabStop = false;
            // 
            // groupBox_DebugFunction
            // 
            this.groupBox_DebugFunction.Controls.Add(this.button_FolderDiag);
            this.groupBox_DebugFunction.Controls.Add(this.checkBox_OutputLog);
            this.groupBox_DebugFunction.Location = new System.Drawing.Point(12, 265);
            this.groupBox_DebugFunction.Name = "groupBox_DebugFunction";
            this.groupBox_DebugFunction.Size = new System.Drawing.Size(206, 37);
            this.groupBox_DebugFunction.TabIndex = 7;
            this.groupBox_DebugFunction.TabStop = false;
            this.groupBox_DebugFunction.Text = "デバッグ機能";
            // 
            // button_FolderDiag
            // 
            this.button_FolderDiag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_FolderDiag.Image = ((System.Drawing.Image)(resources.GetObject("button_FolderDiag.Image")));
            this.button_FolderDiag.Location = new System.Drawing.Point(140, 10);
            this.button_FolderDiag.Name = "button_FolderDiag";
            this.button_FolderDiag.Size = new System.Drawing.Size(26, 21);
            this.button_FolderDiag.TabIndex = 9;
            this.button_FolderDiag.UseVisualStyleBackColor = true;
            this.button_FolderDiag.Visible = false;
            this.button_FolderDiag.Click += new System.EventHandler(this.button_FolderDiag_Click);
            // 
            // checkBox_OutputLog
            // 
            this.checkBox_OutputLog.AutoSize = true;
            this.checkBox_OutputLog.Location = new System.Drawing.Point(10, 15);
            this.checkBox_OutputLog.Name = "checkBox_OutputLog";
            this.checkBox_OutputLog.Size = new System.Drawing.Size(134, 16);
            this.checkBox_OutputLog.TabIndex = 8;
            this.checkBox_OutputLog.Text = "動作ログを出力する(&O)";
            this.checkBox_OutputLog.UseVisualStyleBackColor = true;
            this.checkBox_OutputLog.CheckedChanged += new System.EventHandler(this.checkBox_OutputLog_CheckedChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 345);
            this.Controls.Add(this.groupBox_DebugFunction);
            this.Controls.Add(this.checkBox_Freeze_Setting_Enable);
            this.Controls.Add(this.groupBox_trayIcon);
            this.Controls.Add(this.groupBox_WayOfStartUpOfGame);
            this.Controls.Add(this.button_ChangeEncryptKey);
            this.Controls.Add(this.groupBox_update_settings);
            this.Controls.Add(this.btn_setting);
            this.Controls.Add(this.groupBox_freeze_settings);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "設定";
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_FreezeTime)).EndInit();
            this.groupBox_freeze_settings.ResumeLayout(false);
            this.groupBox_freeze_settings.PerformLayout();
            this.groupBox_update_settings.ResumeLayout(false);
            this.groupBox_update_settings.PerformLayout();
            this.groupBox_WayOfStartUpOfGame.ResumeLayout(false);
            this.groupBox_WayOfStartUpOfGame.PerformLayout();
            this.groupBox_trayIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon_view)).EndInit();
            this.groupBox_DebugFunction.ResumeLayout(false);
            this.groupBox_DebugFunction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown UpDown_FreezeTime;
        private System.Windows.Forms.Label lbl_sec;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.GroupBox groupBox_freeze_settings;
        private System.Windows.Forms.Label lbl_freeze_setting_description;
        private System.Windows.Forms.GroupBox groupBox_update_settings;
        private System.Windows.Forms.CheckBox checkBox_AutoUpdateCheck;
        private System.Windows.Forms.CheckBox checkBox_isCheckBetaVersion;
        private System.Windows.Forms.Button button_ChangeEncryptKey;
        private System.Windows.Forms.GroupBox groupBox_WayOfStartUpOfGame;
        private System.Windows.Forms.RadioButton radioButton_WayOfGameStartUp_Launcher;
        private System.Windows.Forms.RadioButton radioButton_WayOfGameStartUp_Direct;
        private System.Windows.Forms.GroupBox groupBox_trayIcon;
        private System.Windows.Forms.PictureBox pictureBox_icon_view;
        private System.Windows.Forms.HScrollBar hScrollBar_change_icon;
        private System.Windows.Forms.CheckBox checkBox_Freeze_Setting_Enable;
        private System.Windows.Forms.GroupBox groupBox_DebugFunction;
        private System.Windows.Forms.CheckBox checkBox_OutputLog;
        private System.Windows.Forms.Button button_FolderDiag;
    }
}