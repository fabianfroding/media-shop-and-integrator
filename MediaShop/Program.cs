using System;
using System.Windows.Forms;

namespace MediaShop
{
    static class Program
    {
        public static Form mainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}
