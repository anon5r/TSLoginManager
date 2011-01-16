namespace TSLoginManager
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_Right = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Right_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool_UpdateCheckGame = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool_PluginInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Links = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool_Links_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Tool_Links_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Plugins = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Bar = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Right_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Edit_ID = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Right_Delete_ID = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_Left = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Left_ID = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip_Right.SuspendLayout();
            this.contextMenuStrip_Left.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TSLoginManager";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip_Right
            // 
            this.contextMenuStrip_Right.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_About,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_Right_Add,
            this.ToolStripMenuItem_Right_Delete,
            this.ToolStripMenuItem_Right_Tool,
            this.ToolStripMenuItem_Right_Links,
            this.ToolStripMenuItem_Right_Plugins,
            this.ToolStripMenuItem_Right_Bar,
            this.ToolStripMenuItem_Right_Exit});
            this.contextMenuStrip_Right.Name = "contextMenu_Right";
            this.contextMenuStrip_Right.ShowImageMargin = false;
            this.contextMenuStrip_Right.Size = new System.Drawing.Size(129, 170);
            this.contextMenuStrip_Right.Text = global::TSLoginManager.Properties.Settings.Default.AppName;
            // 
            // ToolStripMenuItem_About
            // 
            this.ToolStripMenuItem_About.Name = "ToolStripMenuItem_About";
            this.ToolStripMenuItem_About.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_About.Text = "A&bout...";
            this.ToolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // ToolStripMenuItem_Right_Add
            // 
            this.ToolStripMenuItem_Right_Add.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolStripMenuItem_Right_Add.Name = "ToolStripMenuItem_Right_Add";
            this.ToolStripMenuItem_Right_Add.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_Right_Add.Text = "追加(&A)";
            this.ToolStripMenuItem_Right_Add.Click += new System.EventHandler(this.ToolStripMenuItem_Right_Add_Click);
            // 
            // ToolStripMenuItem_Right_Delete
            // 
            this.ToolStripMenuItem_Right_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripMenuItem_Right_Delete.Name = "ToolStripMenuItem_Right_Delete";
            this.ToolStripMenuItem_Right_Delete.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_Right_Delete.Text = "削除(&D)";
            // 
            // ToolStripMenuItem_Right_Tool
            // 
            this.ToolStripMenuItem_Right_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Right_Tool_UpdateCheckGame,
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion,
            this.ToolStripMenuItem_Right_Tool_Settings,
            this.ToolStripMenuItem_Right_Tool_PluginInfo});
            this.ToolStripMenuItem_Right_Tool.Name = "ToolStripMenuItem_Right_Tool";
            this.ToolStripMenuItem_Right_Tool.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_Right_Tool.Text = "機能(&F)";
            // 
            // ToolStripMenuItem_Right_Tool_UpdateCheckGame
            // 
            this.ToolStripMenuItem_Right_Tool_UpdateCheckGame.Name = "ToolStripMenuItem_Right_Tool_UpdateCheckGame";
            this.ToolStripMenuItem_Right_Tool_UpdateCheckGame.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuItem_Right_Tool_UpdateCheckGame.Text = "アップデート確認(&U)";
            this.ToolStripMenuItem_Right_Tool_UpdateCheckGame.Click += new System.EventHandler(this.ToolStripMenuItem_Right_Tool_UpdateCheckGame_Click);
            // 
            // ToolStripMenuItem_Right_Tool_CheckNewVersion
            // 
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion.Name = "ToolStripMenuItem_Right_Tool_CheckNewVersion";
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion.Text = "最新版チェック(&K)";
            this.ToolStripMenuItem_Right_Tool_CheckNewVersion.Click += new System.EventHandler(this.ToolStripMenuItem_Right_Tool_CheckNewVersion_Click);
            // 
            // ToolStripMenuItem_Right_Tool_Settings
            // 
            this.ToolStripMenuItem_Right_Tool_Settings.Name = "ToolStripMenuItem_Right_Tool_Settings";
            this.ToolStripMenuItem_Right_Tool_Settings.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuItem_Right_Tool_Settings.Text = "設定変更(&S)";
            this.ToolStripMenuItem_Right_Tool_Settings.Click += new System.EventHandler(this.ToolStripMenuItem_Right_Tool_Settings_Click);
            // 
            // ToolStripMenuItem_Right_Tool_PluginInfo
            // 
            this.ToolStripMenuItem_Right_Tool_PluginInfo.Name = "ToolStripMenuItem_Right_Tool_PluginInfo";
            this.ToolStripMenuItem_Right_Tool_PluginInfo.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuItem_Right_Tool_PluginInfo.Text = "プラグイン情報(&P)";
            // 
            // ToolStripMenuItem_Right_Links
            // 
            this.ToolStripMenuItem_Right_Links.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Right_Tool_Links_1,
            this.ToolStripMenuItem_Right_Tool_Links_2});
            this.ToolStripMenuItem_Right_Links.Name = "ToolStripMenuItem_Right_Links";
            this.ToolStripMenuItem_Right_Links.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_Right_Links.Text = "リンク(&L)";
            // 
            // ToolStripMenuItem_Right_Tool_Links_1
            // 
            this.ToolStripMenuItem_Right_Tool_Links_1.Name = "ToolStripMenuItem_Right_Tool_Links_1";
            this.ToolStripMenuItem_Right_Tool_Links_1.Size = new System.Drawing.Size(156, 22);
            this.ToolStripMenuItem_Right_Tool_Links_1.Text = "公式サイト";
            // 
            // ToolStripMenuItem_Right_Tool_Links_2
            // 
            this.ToolStripMenuItem_Right_Tool_Links_2.Name = "ToolStripMenuItem_Right_Tool_Links_2";
            this.ToolStripMenuItem_Right_Tool_Links_2.Size = new System.Drawing.Size(156, 22);
            this.ToolStripMenuItem_Right_Tool_Links_2.Text = "Trickster Wiki";
            // 
            // ToolStripMenuItem_Right_Plugins
            // 
            this.ToolStripMenuItem_Right_Plugins.Name = "ToolStripMenuItem_Right_Plugins";
            this.ToolStripMenuItem_Right_Plugins.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_Right_Plugins.Text = "プラグイン(&P)";
            // 
            // ToolStripMenuItem_Right_Bar
            // 
            this.ToolStripMenuItem_Right_Bar.Name = "ToolStripMenuItem_Right_Bar";
            this.ToolStripMenuItem_Right_Bar.Size = new System.Drawing.Size(125, 6);
            // 
            // ToolStripMenuItem_Right_Exit
            // 
            this.ToolStripMenuItem_Right_Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripMenuItem_Right_Exit.Name = "ToolStripMenuItem_Right_Exit";
            this.ToolStripMenuItem_Right_Exit.Size = new System.Drawing.Size(128, 22);
            this.ToolStripMenuItem_Right_Exit.Text = "終了(&X)";
            this.ToolStripMenuItem_Right_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Right_Exit_Click);
            // 
            // ToolStripMenuItem_Right_Edit_ID
            // 
            this.ToolStripMenuItem_Right_Edit_ID.Name = "ToolStripMenuItem_Right_Edit_ID";
            this.ToolStripMenuItem_Right_Edit_ID.Size = new System.Drawing.Size(32, 19);
            // 
            // ToolStripMenuItem_Right_Delete_ID
            // 
            this.ToolStripMenuItem_Right_Delete_ID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripMenuItem_Right_Delete_ID.Enabled = false;
            this.ToolStripMenuItem_Right_Delete_ID.Name = "ToolStripMenuItem_Right_Delete_ID";
            this.ToolStripMenuItem_Right_Delete_ID.Size = new System.Drawing.Size(158, 22);
            this.ToolStripMenuItem_Right_Delete_ID.Text = "No Registered ID";
            this.ToolStripMenuItem_Right_Delete_ID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItem_Right_Delete_ID.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // contextMenuStrip_Left
            // 
            this.contextMenuStrip_Left.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Left_ID});
            this.contextMenuStrip_Left.Name = "contextMenuStrip_Left";
            this.contextMenuStrip_Left.Size = new System.Drawing.Size(177, 48);
            this.contextMenuStrip_Left.Text = global::TSLoginManager.Properties.Settings.Default.AppName;
            // 
            // ToolStripMenuItem_Left_ID
            // 
            this.ToolStripMenuItem_Left_ID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolStripMenuItem_Left_ID.Enabled = false;
            this.ToolStripMenuItem_Left_ID.Name = "ToolStripMenuItem_Left_ID";
            this.ToolStripMenuItem_Left_ID.Size = new System.Drawing.Size(176, 22);
            this.ToolStripMenuItem_Left_ID.Text = "No Registered ID";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 3000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(136, 39);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TSLoginManager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip_Right.ResumeLayout(false);
            this.contextMenuStrip_Left.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Right;
        private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem_Right_Bar;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Exit;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Left;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Left_ID;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Delete;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Delete_ID;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Edit_ID;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Add;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_About;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool_UpdateCheckGame;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool_CheckNewVersion;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Links;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool_Links_1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool_Links_2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool_Settings;
        public System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Plugins;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Right_Tool_PluginInfo;

    }
}

