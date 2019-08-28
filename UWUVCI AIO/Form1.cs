using AutoUpdaterDotNET;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace UWUVCI_AIO
{

    public partial class UWUVCI_AIO : Form
    {
        #region injector stuff
        #region N64
        private static string ini_path = null;
        #endregion
        #region ForAll
        private static string BaseROM = null;
        private static string INJCT_ROM_path = null;
        private static string tvtex_path = null;
        private static string drctex_path = null;
        private static string icotex_path = null;
        private static string logo_path = null;
        #endregion
        #endregion

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
                if(language == "de-DE")
                {
                    toolStripMenuItem2.Text = "CommonKey ist bereits gespeichert";
                }
                else
                {
                    toolStripMenuItem2.Text = "CommonKey is already set";
                }
                
            }
            if (Properties.Settings.Default.allpathset)
            {
                if (maybeallpathsaresetbuttheydidntusetheoptionlikethemessageboxtoldthemto())
                {
                    disableInjection();
                }     
            }

        }
      private bool maybeallpathsaresetbuttheydidntusetheoptionlikethemessageboxtoldthemto()
        {
            if(Properties.Settings.Default.BaseRomPath != null && Properties.Settings.Default.WorkingPath != null && Properties.Settings.Default.InjectionPath != null)
            {
                Properties.Settings.Default.allpathset = false;
                Properties.Settings.Default.Save();
                return false;
            }
            else
            {
                return true;
            }
        }
        private void disableInjection()
        {
            if (language == "de-DE")
            {
                MessageBox.Show("Bitte geben sie alle Verzeichnispfade an (Einstellungen -> Verzeichnispfade) und starten Sie das Programm neu (Datei -> Neustarten) um mit dem Injecten zu beginnen.");
            }
            else
            {
                MessageBox.Show("Please enter all Paths (Settings -> Paths) and restart the programm (File -> Restart) to be able to inject");
            }
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox3.Enabled = false;
            pictureBox4.Enabled = false;
            pictureBox5.Enabled = false;
            newToolStripMenuItem.Enabled = false;
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
            
            #region n64
            N64.BackColor = Color.FromArgb(60, 60, 60);
            N64.ForeColor = Color.WhiteSmoke;
            BaseRomN64.ForeColor = Color.WhiteSmoke;
            PackingN64.ForeColor = Color.WhiteSmoke;
            InjectionN64.ForeColor = Color.WhiteSmoke;
            button1.ForeColor = Color.Black;
            N64_BTN8.ForeColor = Color.Black;
            N64_BTN9.ForeColor = Color.Black;
            N64_BTN10.ForeColor = Color.Black;
            N64_BTN11.ForeColor = Color.Black;
            N64_BTN12.ForeColor = Color.Black;
            N64_BTN13.ForeColor = Color.Black;
            N64_BTN14.ForeColor = Color.Black;
            N64_BTN15.ForeColor = Color.Black;
            N64_BTN1.ForeColor = Color.Black;
            N64_BTN6.ForeColor = Color.Black;
            N64_BTN2.ForeColor = Color.Black;
            N64_BTN3.ForeColor = Color.Black;
            N64_BTN4.ForeColor = Color.Black;
            N64_BTN7.ForeColor = Color.Black;
            N64_BTN5.ForeColor = Color.Black;
            N64_BTN16.ForeColor = Color.Black;
            N64_BTN17.ForeColor = Color.Black;
            #endregion
            #region nds
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox2.ForeColor = Color.WhiteSmoke;
            groupBox3.ForeColor = Color.WhiteSmoke;
            NDS.BackColor = Color.FromArgb(60, 60, 60);
            NDS.ForeColor = Color.WhiteSmoke;
            back_nds.ForeColor = Color.Black;
            NDS_BTN8.ForeColor = Color.Black;
            NDS_BTN9.ForeColor = Color.Black;
            NDS_BTN10.ForeColor = Color.Black;
            NDS_BTN11.ForeColor = Color.Black;
            NDS_BTN12.ForeColor = Color.Black;
            NDS_BTN13.ForeColor = Color.Black;
            NDS_BTN14.ForeColor = Color.Black;
            NDS_BTN15.ForeColor = Color.Black;
            NDS_BTN1.ForeColor = Color.Black;
            NDS_BTN6.ForeColor = Color.Black;
            NDS_BTN2.ForeColor = Color.Black;
            NDS_BTN3.ForeColor = Color.Black;
            NDS_BTN4.ForeColor = Color.Black;
            NDS_BTN7.ForeColor = Color.Black;
            NDS_BTN5.ForeColor = Color.Black;
            NDS_BTN16.ForeColor = Color.Black;
            NDS_BTN17.ForeColor = Color.Black;
            #endregion

        }
        private void ResetInput()
        {
            BaseROM = null;
            INJCT_ROM_path = null;
            ini_path = null;
            tvtex_path = null;
            drctex_path = null;
            icotex_path = null;
            logo_path = null;

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

        private void Button1_Click(object sender, EventArgs e) //Gonna replace this with an image that changes according to the theme/language
        {
            ResetInput();
            tabControl1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void N64ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form tkey = new TitleKeyMenu(1); // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            tkey.Show();
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void N64ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 2;
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form path = new PathMenu();
            path.Show();
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 0;
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 1;
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.allpathset)
            {
                if (Properties.Settings.Default.BaseRomPath != null && Properties.Settings.Default.WorkingPath != null && Properties.Settings.Default.InjectionPath != null)
                {
                    Properties.Settings.Default.allpathset = false;
                    Properties.Settings.Default.Save();
                 
                }
            }
            Application.Restart();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
