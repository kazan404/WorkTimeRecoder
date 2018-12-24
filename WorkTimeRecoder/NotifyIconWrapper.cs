using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WorkTimeRecoder
{
    public partial class NotifyIconWrapper : Component
    {
        private MainWindow window = new MainWindow();

        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        public NotifyIconWrapper()
        {
            InitializeComponent();

            this.toolStripMenuItem_Open.Click += ToolStripMenuItem_Open_Click;
            this.toolStripMenuItem_Exit.Click += ToolStripMenuItem_Exit_Click;
            window.Baloonsetter = ShowTimerBaloon;

            // 起動時はここで最初のウィンドウを出すことでMainWindowのオブジェクトを１つに集約する
            ToolStripMenuItem_Open_Click(null, null);
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
        /// タイマーの内容を表示します
        /// </summary>
        /// <param name="timerTitle">各タイマーのメモ欄にあるテキスト</param>
        /// <param name="time">タイマー値</param>
        public void ShowTimerBaloon(string timerTitle, string time)
        {
            string showStrig = timerTitle + ":" + time;
            timerNotifyIcon.ShowBalloonTip(6000, "時間経過通知", showStrig, ToolTipIcon.Info);
        }

        /// <summary>
        /// コンテキストメニュー "表示" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void ToolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            // MainWindow を生成、表示
            if (window == null)
            {
                MainWindow window = new MainWindow();
            }
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
            System.Windows.Application.Current.Shutdown();
        }
    }
}
