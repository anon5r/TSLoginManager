namespace TricksterTools.Plugins.UIEditor
{
    partial class UIEditorMainForm
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox_ChatUI_Left = new System.Windows.Forms.PictureBox();
            this.pictureBox_ChatUI_Center = new System.Windows.Forms.PictureBox();
            this.pictureBox_ChatUI_Right = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ChatUI_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ChatUI_Center)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ChatUI_Right)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.ContextMenuStrip = this.contextMenuStrip;
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ChatUI_Left, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ChatUI_Center, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox_ChatUI_Right, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(328, 478);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Close});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(106, 26);
            // 
            // ToolStripMenuItem_Close
            // 
            this.ToolStripMenuItem_Close.Name = "ToolStripMenuItem_Close";
            this.ToolStripMenuItem_Close.Size = new System.Drawing.Size(105, 22);
            this.ToolStripMenuItem_Close.Text = "閉じる(&C)";
            this.ToolStripMenuItem_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.ToolStripMenuItem_Close.Click += new System.EventHandler(this.ToolStripMenuItem_Close_Click);
            // 
            // pictureBox_ChatUI_Left
            // 
            this.pictureBox_ChatUI_Left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox_ChatUI_Left.Image = global::TricksterTools.Plugins.UIEditor.Properties.Resources.ChatUI_Left;
            this.pictureBox_ChatUI_Left.Location = new System.Drawing.Point(0, 458);
            this.pictureBox_ChatUI_Left.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_ChatUI_Left.Name = "pictureBox_ChatUI_Left";
            this.pictureBox_ChatUI_Left.Size = new System.Drawing.Size(60, 20);
            this.pictureBox_ChatUI_Left.TabIndex = 0;
            this.pictureBox_ChatUI_Left.TabStop = false;
            // 
            // pictureBox_ChatUI_Center
            // 
            this.pictureBox_ChatUI_Center.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_ChatUI_Center.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_ChatUI_Center.BackgroundImage = global::TricksterTools.Plugins.UIEditor.Properties.Resources.ChatUI_Center;
            this.pictureBox_ChatUI_Center.Location = new System.Drawing.Point(60, 458);
            this.pictureBox_ChatUI_Center.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_ChatUI_Center.Name = "pictureBox_ChatUI_Center";
            this.pictureBox_ChatUI_Center.Size = new System.Drawing.Size(247, 20);
            this.pictureBox_ChatUI_Center.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_ChatUI_Center.TabIndex = 1;
            this.pictureBox_ChatUI_Center.TabStop = false;
            // 
            // pictureBox_ChatUI_Right
            // 
            this.pictureBox_ChatUI_Right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_ChatUI_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_ChatUI_Right.Image = global::TricksterTools.Plugins.UIEditor.Properties.Resources.ChatUI_Right;
            this.pictureBox_ChatUI_Right.Location = new System.Drawing.Point(307, 458);
            this.pictureBox_ChatUI_Right.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_ChatUI_Right.Name = "pictureBox_ChatUI_Right";
            this.pictureBox_ChatUI_Right.Size = new System.Drawing.Size(26, 20);
            this.pictureBox_ChatUI_Right.TabIndex = 2;
            this.pictureBox_ChatUI_Right.TabStop = false;
            // 
            // UIEditorMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(325, 474);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIEditorMainForm";
            this.Opacity = 0.8;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.UIEditorMainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UIEditorMainForm_FormClosed);
            this.Resize += new System.EventHandler(this.UIEditorMainForm_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ChatUI_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ChatUI_Center)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ChatUI_Right)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox_ChatUI_Left;
        private System.Windows.Forms.PictureBox pictureBox_ChatUI_Center;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Close;
        private System.Windows.Forms.PictureBox pictureBox_ChatUI_Right;



    }
}