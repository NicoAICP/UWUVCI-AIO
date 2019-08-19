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
using AutoUpdaterDotNET;

namespace UWUVCI_AIO
{
    
    public partial class UWUVCI_AIO : Form
    {
        public string language = Properties.Settings.Default.Language;
        public UWUVCI_AIO()
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/Hotbrawl20/testing/master/update.xml");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            
               
            
            InitializeComponent();
            if (Properties.Settings.Default.darkmode == true)
            {
                enableDarkMode();
            }
            if(Properties.Settings.Default.CommonKey.GetHashCode() == 487391367)
            {
                toolStripMenuItem2.Enabled = false;
                if(language == "en-US")
                {
                    toolStripMenuItem2.Text = "CommonKey is already set";
                }
                else
                {
                    toolStripMenuItem2.Text = "CommonKey ist bereits gespeichert";
                }
                
            }
        }

        private void CloseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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
            Main.BackColor = Color.FromArgb(60, 60, 60);
            Main.ForeColor = Color.WhiteSmoke;
            NDS.BackColor = Color.FromArgb(60, 60, 60);
            NDS.ForeColor = Color.WhiteSmoke;
            N64.BackColor = Color.FromArgb(60, 60, 60);
            N64.ForeColor = Color.WhiteSmoke;
            BaseRomN64.ForeColor = Color.WhiteSmoke;
            PackingN64.ForeColor = Color.WhiteSmoke;
            InjectionN64.ForeColor = Color.WhiteSmoke;
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

        private void HilfeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form ckey = new CKey();
            ckey.Show();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2; // 0 = Main, 1 = NDS, 2 = N64, 3 = GBA, 4 = NES, 5 = SNES
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1; // 0 = Main, 1 = NDS, 2 = N64, 3 = GBA, 4 = NES, 5 = SNES
        }
    }
}
