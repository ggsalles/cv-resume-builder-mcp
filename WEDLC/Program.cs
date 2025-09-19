using System;
using System.Diagnostics;
using System.Windows.Forms;
using WEDLC.Forms;

namespace WEDLC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Verificação adicional como segurança
            if (Process.GetProcessesByName("WEDLC").Length > 1)
            {
                MessageBox.Show("O programa já está em execução.");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
