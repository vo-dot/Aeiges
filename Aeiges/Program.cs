using System;
using System.Windows.Forms;
using Aeiges.Forms;

namespace Aeiges
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Aeiges - Cyber Crime Reporting & Management System
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ensure Evidence folder exists
            string evidencePath = System.IO.Path.Combine(Application.StartupPath, "EvidenceFiles");
            if (!System.IO.Directory.Exists(evidencePath))
                System.IO.Directory.CreateDirectory(evidencePath);
            string profilePath = System.IO.Path.Combine(Application.StartupPath, "ProfilePictures");
            if (!System.IO.Directory.Exists(profilePath))
                System.IO.Directory.CreateDirectory(profilePath);

            Application.Run(new LoginForm());
        }
    }
}
