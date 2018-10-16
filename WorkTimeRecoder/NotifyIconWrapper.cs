using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorkTimeRecoder
{
    public partial class NotifyIconWrapper : Component
    {
        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        public NotifyIconWrapper()
        {
            InitializeComponent();

            this.toolStripMenuItem_Open.Click += ToolStripMenuItem_Open_Click;
            this.toolStripMenuItem_Exit.Click += ToolStripMenuItem_Exit_Click;
        }

        /// <summary>
        /// コンテナ を指定して NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        /// <param name="container">コンテナ</param>
        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// コンテキストメニュー "表示" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            // MainWindow を生成、表示
            MainWindow window = new MainWindow();
            window.Show();
        }

        /// <summary>
        /// コンテキストメニュー "終了" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // 現在のアプリケーションを終了
            Application.Current.Shutdown();
        }
    }
}
