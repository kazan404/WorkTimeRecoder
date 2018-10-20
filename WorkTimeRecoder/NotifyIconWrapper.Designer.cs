namespace WorkTimeRecoder
{
    partial class NotifyIconWrapper
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            this.timerNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerContextMenuStrip.SuspendLayout();
            // 
            // timerNotifyIcon
            // 
            this.timerNotifyIcon.ContextMenuStrip = this.timerContextMenuStrip;
            this.timerNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("timerNotifyIcon.Icon")));
            this.timerNotifyIcon.Text = "This is a work time counter.";
            this.timerNotifyIcon.Visible = true;
            this.timerNotifyIcon.DoubleClick += new System.EventHandler(this.ToolStripMenuItem_Open_Click);
            // 
            // timerContextMenuStrip
            // 
            this.timerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Open,
            this.toolStripMenuItem_Exit});
            this.timerContextMenuStrip.Name = "timerContextMenuStrip";
            this.timerContextMenuStrip.Size = new System.Drawing.Size(99, 48);
            // 
            // toolStripMenuItem_Open
            // 
            this.toolStripMenuItem_Open.Name = "toolStripMenuItem_Open";
            this.toolStripMenuItem_Open.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem_Open.Text = "表示";
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(98, 22);
            this.toolStripMenuItem_Exit.Text = "終了";
            this.timerContextMenuStrip.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon timerNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip timerContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
    }
}
