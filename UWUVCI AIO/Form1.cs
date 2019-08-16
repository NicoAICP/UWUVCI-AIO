using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Resources;

namespace UWUVCI_AIO
{
    
    public partial class UWUVCI_AIO : Form
    {
        public string language = Properties.Settings.Default.Language;
        public UWUVCI_AIO()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            
               
            
            InitializeComponent();
            if (Properties.Settings.Default.darkmode == true)
            {
                enableDarkMode();
            }
            
        }

        private void CloseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            language = "de-DE";

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            language = "en-US";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "de-DE";
            Properties.Settings.Default.Save();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = "en-US";
            Properties.Settings.Default.Save();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settings = new Settings();
            settings.Show();
        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TabPage2_Click(object sender, EventArgs e)
        {
            
        }
        private void enableDarkMode()
        {
            splitter1.BackColor = Color.FromArgb(50, 50, 50);
            panel1.BackColor = Color.FromArgb(50, 50, 50);
            tabPage2.BackColor = Color.FromArgb(60, 60, 60);
            tabPage2.ForeColor = Color.WhiteSmoke;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.Show();
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;

        }
    }
}
