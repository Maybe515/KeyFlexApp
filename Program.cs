using System;
using System.Windows.Forms;

namespace KeyFlexApp
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメインエントリポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}