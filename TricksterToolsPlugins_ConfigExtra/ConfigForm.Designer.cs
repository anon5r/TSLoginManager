namespace TricksterTools.Plugins.ConfigExtra
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.groupBox_ScreenSize = new System.Windows.Forms.GroupBox();
            this.label_resolution = new System.Windows.Forms.Label();
            this.comboBox_Resolution = new System.Windows.Forms.ComboBox();
            this.checkBox_FullScreen = new System.Windows.Forms.CheckBox();
            this.groupBox_graphic = new System.Windows.Forms.GroupBox();
            this.checkBox_MapEffect = new System.Windows.Forms.CheckBox();
            this.checkBox_SpecailEffect = new System.Windows.Forms.CheckBox();
            this.groupBox_sound = new System.Windows.Forms.GroupBox();
            this.label_Channel = new System.Windows.Forms.Label();
            this.label_Bits = new System.Windows.Forms.Label();
            this.label_SampleRate = new System.Windows.Forms.Label();
            this.checkBox_FullSoundOFF = new System.Windows.Forms.CheckBox();
            this.comboBox_Channel = new System.Windows.Forms.ComboBox();
            this.comboBox_Bits = new System.Windows.Forms.ComboBox();
            this.comboBox_SampleRate = new System.Windows.Forms.ComboBox();
            this.groupBox_screenshot = new System.Windows.Forms.GroupBox();
            this.label_JPGOption_QualityLevel = new System.Windows.Forms.Label();
            this.label_Quality = new System.Windows.Forms.Label();
            this.trackBar_JPGOption = new System.Windows.Forms.TrackBar();
            this.label_JPEGOption = new System.Windows.Forms.Label();
            this.comboBox_FileFormat = new System.Windows.Forms.ComboBox();
            this.label_FileFormat = new System.Windows.Forms.Label();
            this.button_SystemInfo = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_CANCEL = new System.Windows.Forms.Button();
            this.groupBox_ScreenSize.SuspendLayout();
            this.groupBox_graphic.SuspendLayout();
            this.groupBox_sound.SuspendLayout();
            this.groupBox_screenshot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_JPGOption)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox_ScreenSize
            // 
            this.groupBox_ScreenSize.Controls.Add(this.label_resolution);
            this.groupBox_ScreenSize.Controls.Add(this.comboBox_Resolution);
            this.groupBox_ScreenSize.Controls.Add(this.checkBox_FullScreen);
            this.groupBox_ScreenSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox_ScreenSize.Location = new System.Drawing.Point(14, 14);
            this.groupBox_ScreenSize.Name = "groupBox_ScreenSize";
            this.groupBox_ScreenSize.Size = new System.Drawing.Size(285, 85);
            this.groupBox_ScreenSize.TabIndex = 0;
            this.groupBox_ScreenSize.TabStop = false;
            // 
            // label_resolution
            // 
            this.label_resolution.AutoSize = true;
            this.label_resolution.Location = new System.Drawing.Point(15, 50);
            this.label_resolution.Name = "label_resolution";
            this.label_resolution.Size = new System.Drawing.Size(63, 14);
            this.label_resolution.TabIndex = 1;
            this.label_resolution.Text = "Resolution";
            // 
            // comboBox_Resolution
            // 
            this.comboBox_Resolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Resolution.FormattingEnabled = true;
            this.comboBox_Resolution.Location = new System.Drawing.Point(91, 47);
            this.comboBox_Resolution.Name = "comboBox_Resolution";
            this.comboBox_Resolution.Size = new System.Drawing.Size(173, 22);
            this.comboBox_Resolution.TabIndex = 2;
            // 
            // checkBox_FullScreen
            // 
            this.checkBox_FullScreen.AutoSize = true;
            this.checkBox_FullScreen.Location = new System.Drawing.Point(17, 21);
            this.checkBox_FullScreen.Name = "checkBox_FullScreen";
            this.checkBox_FullScreen.Size = new System.Drawing.Size(81, 18);
            this.checkBox_FullScreen.TabIndex = 0;
            this.checkBox_FullScreen.Text = "FullScreen";
            this.checkBox_FullScreen.UseVisualStyleBackColor = true;
            // 
            // groupBox_graphic
            // 
            this.groupBox_graphic.Controls.Add(this.checkBox_MapEffect);
            this.groupBox_graphic.Controls.Add(this.checkBox_SpecailEffect);
            this.groupBox_graphic.Location = new System.Drawing.Point(14, 113);
            this.groupBox_graphic.Name = "groupBox_graphic";
            this.groupBox_graphic.Size = new System.Drawing.Size(285, 78);
            this.groupBox_graphic.TabIndex = 1;
            this.groupBox_graphic.TabStop = false;
            this.groupBox_graphic.Text = "Graphic";
            // 
            // checkBox_MapEffect
            // 
            this.checkBox_MapEffect.AutoSize = true;
            this.checkBox_MapEffect.Location = new System.Drawing.Point(17, 47);
            this.checkBox_MapEffect.Name = "checkBox_MapEffect";
            this.checkBox_MapEffect.Size = new System.Drawing.Size(85, 18);
            this.checkBox_MapEffect.TabIndex = 1;
            this.checkBox_MapEffect.Text = "Map Effect";
            this.checkBox_MapEffect.UseVisualStyleBackColor = true;
            // 
            // checkBox_SpecailEffect
            // 
            this.checkBox_SpecailEffect.AutoSize = true;
            this.checkBox_SpecailEffect.Location = new System.Drawing.Point(17, 21);
            this.checkBox_SpecailEffect.Name = "checkBox_SpecailEffect";
            this.checkBox_SpecailEffect.Size = new System.Drawing.Size(100, 18);
            this.checkBox_SpecailEffect.TabIndex = 0;
            this.checkBox_SpecailEffect.Text = "Special Effect";
            this.checkBox_SpecailEffect.UseVisualStyleBackColor = true;
            // 
            // groupBox_sound
            // 
            this.groupBox_sound.Controls.Add(this.label_Channel);
            this.groupBox_sound.Controls.Add(this.label_Bits);
            this.groupBox_sound.Controls.Add(this.label_SampleRate);
            this.groupBox_sound.Controls.Add(this.checkBox_FullSoundOFF);
            this.groupBox_sound.Controls.Add(this.comboBox_Channel);
            this.groupBox_sound.Controls.Add(this.comboBox_Bits);
            this.groupBox_sound.Controls.Add(this.comboBox_SampleRate);
            this.groupBox_sound.Location = new System.Drawing.Point(17, 206);
            this.groupBox_sound.Name = "groupBox_sound";
            this.groupBox_sound.Size = new System.Drawing.Size(281, 145);
            this.groupBox_sound.TabIndex = 2;
            this.groupBox_sound.TabStop = false;
            this.groupBox_sound.Text = "Sound";
            // 
            // label_Channel
            // 
            this.label_Channel.AutoSize = true;
            this.label_Channel.Location = new System.Drawing.Point(12, 91);
            this.label_Channel.Name = "label_Channel";
            this.label_Channel.Size = new System.Drawing.Size(50, 14);
            this.label_Channel.TabIndex = 4;
            this.label_Channel.Text = "Channel";
            // 
            // label_Bits
            // 
            this.label_Bits.AutoSize = true;
            this.label_Bits.Location = new System.Drawing.Point(12, 55);
            this.label_Bits.Name = "label_Bits";
            this.label_Bits.Size = new System.Drawing.Size(26, 14);
            this.label_Bits.TabIndex = 2;
            this.label_Bits.Text = "Bits";
            // 
            // label_SampleRate
            // 
            this.label_SampleRate.AutoSize = true;
            this.label_SampleRate.Location = new System.Drawing.Point(12, 24);
            this.label_SampleRate.Name = "label_SampleRate";
            this.label_SampleRate.Size = new System.Drawing.Size(75, 14);
            this.label_SampleRate.TabIndex = 0;
            this.label_SampleRate.Text = "Sample Rate";
            // 
            // checkBox_FullSoundOFF
            // 
            this.checkBox_FullSoundOFF.AutoSize = true;
            this.checkBox_FullSoundOFF.Location = new System.Drawing.Point(14, 118);
            this.checkBox_FullSoundOFF.Name = "checkBox_FullSoundOFF";
            this.checkBox_FullSoundOFF.Size = new System.Drawing.Size(107, 18);
            this.checkBox_FullSoundOFF.TabIndex = 6;
            this.checkBox_FullSoundOFF.Text = "Full Sound OFF";
            this.checkBox_FullSoundOFF.UseVisualStyleBackColor = true;
            // 
            // comboBox_Channel
            // 
            this.comboBox_Channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Channel.FormattingEnabled = true;
            this.comboBox_Channel.Location = new System.Drawing.Point(119, 82);
            this.comboBox_Channel.Name = "comboBox_Channel";
            this.comboBox_Channel.Size = new System.Drawing.Size(142, 22);
            this.comboBox_Channel.TabIndex = 5;
            // 
            // comboBox_Bits
            // 
            this.comboBox_Bits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bits.FormattingEnabled = true;
            this.comboBox_Bits.Location = new System.Drawing.Point(119, 51);
            this.comboBox_Bits.Name = "comboBox_Bits";
            this.comboBox_Bits.Size = new System.Drawing.Size(142, 22);
            this.comboBox_Bits.TabIndex = 3;
            // 
            // comboBox_SampleRate
            // 
            this.comboBox_SampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SampleRate.FormattingEnabled = true;
            this.comboBox_SampleRate.Location = new System.Drawing.Point(119, 21);
            this.comboBox_SampleRate.Name = "comboBox_SampleRate";
            this.comboBox_SampleRate.Size = new System.Drawing.Size(142, 22);
            this.comboBox_SampleRate.TabIndex = 1;
            // 
            // groupBox_screenshot
            // 
            this.groupBox_screenshot.Controls.Add(this.label_JPGOption_QualityLevel);
            this.groupBox_screenshot.Controls.Add(this.label_Quality);
            this.groupBox_screenshot.Controls.Add(this.trackBar_JPGOption);
            this.groupBox_screenshot.Controls.Add(this.label_JPEGOption);
            this.groupBox_screenshot.Controls.Add(this.comboBox_FileFormat);
            this.groupBox_screenshot.Controls.Add(this.label_FileFormat);
            this.groupBox_screenshot.Location = new System.Drawing.Point(17, 369);
            this.groupBox_screenshot.Name = "groupBox_screenshot";
            this.groupBox_screenshot.Size = new System.Drawing.Size(281, 110);
            this.groupBox_screenshot.TabIndex = 3;
            this.groupBox_screenshot.TabStop = false;
            this.groupBox_screenshot.Text = "ScreenShot";
            // 
            // label_JPGOption_QualityLevel
            // 
            this.label_JPGOption_QualityLevel.AutoSize = true;
            this.label_JPGOption_QualityLevel.Location = new System.Drawing.Point(62, 87);
            this.label_JPGOption_QualityLevel.Name = "label_JPGOption_QualityLevel";
            this.label_JPGOption_QualityLevel.Size = new System.Drawing.Size(30, 14);
            this.label_JPGOption_QualityLevel.TabIndex = 5;
            this.label_JPGOption_QualityLevel.Text = "0 %";
            // 
            // label_Quality
            // 
            this.label_Quality.AutoSize = true;
            this.label_Quality.Location = new System.Drawing.Point(12, 87);
            this.label_Quality.Name = "label_Quality";
            this.label_Quality.Size = new System.Drawing.Size(48, 14);
            this.label_Quality.TabIndex = 4;
            this.label_Quality.Text = "Quality:";
            // 
            // trackBar_JPGOption
            // 
            this.trackBar_JPGOption.Location = new System.Drawing.Point(106, 49);
            this.trackBar_JPGOption.Maximum = 100;
            this.trackBar_JPGOption.Name = "trackBar_JPGOption";
            this.trackBar_JPGOption.Size = new System.Drawing.Size(163, 45);
            this.trackBar_JPGOption.TabIndex = 3;
            this.trackBar_JPGOption.TickFrequency = 5;
            this.trackBar_JPGOption.Scroll += new System.EventHandler(this.trackBar_JPGOption_Scroll);
            // 
            // label_JPEGOption
            // 
            this.label_JPEGOption.AutoSize = true;
            this.label_JPEGOption.Location = new System.Drawing.Point(12, 56);
            this.label_JPEGOption.Name = "label_JPEGOption";
            this.label_JPEGOption.Size = new System.Drawing.Size(68, 14);
            this.label_JPEGOption.TabIndex = 2;
            this.label_JPEGOption.Text = "JPG Option";
            // 
            // comboBox_FileFormat
            // 
            this.comboBox_FileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_FileFormat.FormattingEnabled = true;
            this.comboBox_FileFormat.Location = new System.Drawing.Point(119, 19);
            this.comboBox_FileFormat.Name = "comboBox_FileFormat";
            this.comboBox_FileFormat.Size = new System.Drawing.Size(140, 22);
            this.comboBox_FileFormat.TabIndex = 1;
            // 
            // label_FileFormat
            // 
            this.label_FileFormat.AutoSize = true;
            this.label_FileFormat.Location = new System.Drawing.Point(12, 22);
            this.label_FileFormat.Name = "label_FileFormat";
            this.label_FileFormat.Size = new System.Drawing.Size(66, 14);
            this.label_FileFormat.TabIndex = 0;
            this.label_FileFormat.Text = "File Format";
            // 
            // button_SystemInfo
            // 
            this.button_SystemInfo.Location = new System.Drawing.Point(33, 500);
            this.button_SystemInfo.Name = "button_SystemInfo";
            this.button_SystemInfo.Size = new System.Drawing.Size(241, 26);
            this.button_SystemInfo.TabIndex = 4;
            this.button_SystemInfo.Text = "System Info";
            this.button_SystemInfo.UseVisualStyleBackColor = true;
            this.button_SystemInfo.Click += new System.EventHandler(this.button_SystemInfo_Click);
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(33, 533);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(100, 29);
            this.button_OK.TabIndex = 5;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_CANCEL
            // 
            this.button_CANCEL.Location = new System.Drawing.Point(167, 533);
            this.button_CANCEL.Name = "button_CANCEL";
            this.button_CANCEL.Size = new System.Drawing.Size(106, 28);
            this.button_CANCEL.TabIndex = 6;
            this.button_CANCEL.Text = "CANCEL";
            this.button_CANCEL.UseVisualStyleBackColor = true;
            this.button_CANCEL.Click += new System.EventHandler(this.button_CANCEL_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 576);
            this.Controls.Add(this.button_CANCEL);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_SystemInfo);
            this.Controls.Add(this.groupBox_screenshot);
            this.Controls.Add(this.groupBox_sound);
            this.Controls.Add(this.groupBox_graphic);
            this.Controls.Add(this.groupBox_ScreenSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trickster Config (Extra)";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBox_ScreenSize.ResumeLayout(false);
            this.groupBox_ScreenSize.PerformLayout();
            this.groupBox_graphic.ResumeLayout(false);
            this.groupBox_graphic.PerformLayout();
            this.groupBox_sound.ResumeLayout(false);
            this.groupBox_sound.PerformLayout();
            this.groupBox_screenshot.ResumeLayout(false);
            this.groupBox_screenshot.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_JPGOption)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_ScreenSize;
        private System.Windows.Forms.CheckBox checkBox_FullScreen;
        private System.Windows.Forms.Label label_resolution;
        private System.Windows.Forms.ComboBox comboBox_Resolution;
        private System.Windows.Forms.GroupBox groupBox_graphic;
        private System.Windows.Forms.GroupBox groupBox_sound;
        private System.Windows.Forms.GroupBox groupBox_screenshot;
        private System.Windows.Forms.Button button_SystemInfo;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_CANCEL;
        private System.Windows.Forms.CheckBox checkBox_MapEffect;
        private System.Windows.Forms.CheckBox checkBox_SpecailEffect;
        private System.Windows.Forms.Label label_Channel;
        private System.Windows.Forms.Label label_Bits;
        private System.Windows.Forms.Label label_SampleRate;
        private System.Windows.Forms.CheckBox checkBox_FullSoundOFF;
        private System.Windows.Forms.ComboBox comboBox_Channel;
        private System.Windows.Forms.ComboBox comboBox_Bits;
        private System.Windows.Forms.ComboBox comboBox_SampleRate;
        private System.Windows.Forms.Label label_JPEGOption;
        private System.Windows.Forms.ComboBox comboBox_FileFormat;
        private System.Windows.Forms.Label label_FileFormat;
        private System.Windows.Forms.Label label_JPGOption_QualityLevel;
        private System.Windows.Forms.Label label_Quality;
        private System.Windows.Forms.TrackBar trackBar_JPGOption;
    }
}