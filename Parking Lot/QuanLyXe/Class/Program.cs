using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parking_Lot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Log_in());
        }
    }
    public static class Globals
    {
        public static string GlobalUserId { get; private set; }
        public static void SetGlobalUserIId(string userId)
        {
            GlobalUserId = userId;
        }
    }
}
