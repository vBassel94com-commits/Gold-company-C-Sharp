using System;
using System.Windows.Forms;

namespace iGOLD
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new newDb();
            form.Show();
            Application.Run();
        }
	}
}