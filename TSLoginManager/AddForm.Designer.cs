namespace TSLoginManager
{
    partial class AddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddForm));
            this.TextBox_ID = new System.Windows.Forms.TextBox();
            this.TextBox_Pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.comboBox_LoginSite = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox_ID
            // 
            this.TextBox_ID.Location = new System.Drawing.Point(59, 9);
            this.TextBox_ID.Name = "TextBox_ID";
            this.TextBox_ID.Size = new System.Drawing.Size(169, 19);
            this.TextBox_ID.TabIndex = 1;
            this.TextBox_ID.WordWrap = false;
            // 
            // TextBox_Pass
            // 
            this.TextBox_Pass.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.TextBox_Pass.Location = new System.Drawing.Point(59, 30);
            this.TextBox_Pass.Name = "TextBox_Pass";
            this.TextBox_Pass.PasswordChar = '●';
            this.TextBox_Pass.Size = new System.Drawing.Size(169, 19);
            this.TextBox_Pass.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "パスワード";
            // 
            // Button_OK
            // 
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(234, 4);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(87, 24);
            this.Button_OK.TabIndex = 6;
            this.Button_OK.Text = "&OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancel.Location = new System.Drawing.Point(234, 33);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(87, 24);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "キャンセル(&C)";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // comboBox_LoginSite
            // 
            this.comboBox_LoginSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LoginSite.FormattingEnabled = true;
            this.comboBox_LoginSite.Location = new System.Drawing.Point(59, 50);
            this.comboBox_LoginSite.Name = "comboBox_LoginSite";
            this.comboBox_LoginSite.Size = new System.Drawing.Size(169, 20);
            this.comboBox_LoginSite.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "サイト";
            // 
            // AddForm
            // 
            this.AcceptButton = this.Button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 76);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_LoginSite);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.TextBox_ID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_Pass);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "追加 - TSLoginManager";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.Shown += new System.EventHandler(this.AddForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_ID;
        private System.Windows.Forms.TextBox TextBox_Pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.ComboBox comboBox_LoginSite;
        private System.Windows.Forms.Label label3;
    }
}

