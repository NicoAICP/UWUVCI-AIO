using AutoUpdaterDotNET;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private static bool code = false;
        private static bool content = false;
        private static bool meta = false;
        private static bool allowinject = false;
        private static string BaseROM = null;
        private static string CSTMBaseRom_path = null;
        private static string INJCT_ROM_path = null;
        private static string[] bootimages = new string[4]; // 0 = TV, 1 = DRC, 2 = ICON, 3 = LOGO
       
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
            if (Properties.Settings.Default.CommonKey.GetHashCode() == 487391367)
            {
                toolStripMenuItem2.Enabled = false;
                if (language == "de-DE")
                {
                    toolStripMenuItem2.Text = "CommonKey ist bereits gespeichert";
                }
                else
                {
                    toolStripMenuItem2.Text = "CommonKey is already set";
                }

            }

                
                if (Checking())
                {
                    disableInjection();
                }
            

        }
        #region Consoles
        private void ResetInput()
        {
            BaseROM = null;
            code = false;
            content = false;
            meta = false;
            allowinject = false;
            INJCT_ROM_path = null;
            ini_path = null;
            for(int i = 0; i < bootimages.Count(); i++)
            {
                bootimages[i] = null;
            }
            SNESCSTMNFOLDERS.Enabled = false;
            CSTMBaseRom_path = null;
            SMETROIDEU.Enabled = false;
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



        private void PictureBox3_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 3;
        }

        private void NES_BACK_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 0;
        }

        private void SNES_BACK_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 0;
        }

        private void GBA_BACK_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 0;
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 4;
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            ResetInput();
            SNESCSTMNFOLDERS.Enabled = true;
            tabControl1.SelectedIndex = 5;
        }

        private void GBAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 3;
        }

        private void NESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetInput();
            tabControl1.SelectedIndex = 4;
        }

        private void SNESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetInput();
            SNESCSTMNFOLDERS.Enabled = true;
            tabControl1.SelectedIndex = 5;
        }

        private void UWUVCI_AIO_Load(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            SNESBRinfopanel(index);
        }
        private void SNESBRinfopanel(int b)
        {
            if (b == 0)
            {
                SNESCUSTOM.Visible = true;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                #region timers
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                #endregion
            }
            if (b == 1)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = true;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                #region timers
                SMETROIDEU.Enabled = true;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                #endregion
            }
            if (b == 2)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = true;
                SMJP_PANEL.Visible = false;
                #region timers
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = true;
                SMETROIDJP.Enabled = false;
                #endregion
            }
            if (b == 3)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = true;
                SMJP_PANEL.Visible = true;
                #region timers
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = true;
                #endregion
            }
        }
        private void SNESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form tkey = new TitleKeyMenu(4); // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            tkey.Show();
        }

        private void KEYS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == null)
            {
                ICON_PACKING.Image = Properties.Resources._;
                CKEYMSG_PACKING.Text = "No CommonKey stored";
                CKEYMSG_PACKING.ForeColor = Color.FromArgb(255, 216, 0);
                SMEUCK.Image = Properties.Resources.X;
                SMEEU_CKEY.Text = "No CommonKey stored";
                SMEEU_CKEY.ForeColor = Color.Red;
                SMUSCKEYIMG.Image = Properties.Resources.X;
                SMUSCKEY.Text = "No CommonKey stored";
                SMUSCKEY.ForeColor = Color.Red;
                SMJPCKEYIMG.Image = Properties.Resources.X;
                SMJPCKEY.Text = "No CommonKey stored";
                SMJPCKEY.ForeColor = Color.Red;
            }
            else
            {
                ICON_PACKING.Image = null;
                ICON_PACKING.Image = Properties.Resources.yes;
                CKEYMSG_PACKING.Text = "CommonKey found";
                CKEYMSG_PACKING.ForeColor = Color.FromArgb(0, 127, 14);
                SMEUCK.Image = Properties.Resources.yes;
                SMEEU_CKEY.Text = "CommonKey found";
                SMEEU_CKEY.ForeColor = Color.FromArgb(0, 127, 14);
                SMUSCKEYIMG.Image = Properties.Resources.yes;
                SMUSCKEY.Text = "CommonKey found";
                SMUSCKEY.ForeColor = Color.FromArgb(0, 127, 14);
                SMJPCKEYIMG.Image = Properties.Resources.yes;
                SMJPCKEY.Text = "CommonKey found";
                SMJPCKEY.ForeColor = Color.FromArgb(0, 127, 14);
            }

        }
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                textBox32.Text = folderBrowserDialog1.SelectedPath;
                CSTMBaseRom_path = textBox32.Text;
                if (Directory.Exists(folderBrowserDialog1.SelectedPath + "/code"))
                {
                    code = true;
                }
                else
                {
                    code = false;
                }
                if (Directory.Exists(folderBrowserDialog1.SelectedPath + "/content"))
                {
                    content = true;
                }
                else
                {
                    content = false;
                }
                if (Directory.Exists(folderBrowserDialog1.SelectedPath + "/meta"))
                {
                    meta = true;
                }
                else
                {
                    meta = false;
                }
            }


        }

        private void SNESCSTMNFOLDERS_Tick(object sender, EventArgs e)
        {
            if (code == false || content == false || meta == false)
            {
                allowinject = false;
            }
            else
            {
                allowinject = true;
            }
            if (code == false)
            {
                SNESCODEIMG.Image = Properties.Resources.X;
                SNESCODETXT.Text = "No code folder";
                SNESCODETXT.ForeColor = Color.Red;
            }
            else
            {
                SNESCODEIMG.Image = Properties.Resources.yes;
                SNESCODETXT.Text = "Code folder found";
                SNESCODETXT.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (content == false)
            {
                SNESCONTENTIMG.Image = Properties.Resources.X;
                SNESCONTENTTXT.Text = "No content folder";
                SNESCONTENTTXT.ForeColor = Color.Red;
            }
            else
            {
                SNESCONTENTIMG.Image = Properties.Resources.yes;
                SNESCONTENTTXT.Text = "Content folder found";
                SNESCONTENTTXT.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (meta == false)
            {
                SNESMETAIMG.Image = Properties.Resources.X;
                SNESMETATXT.Text = "No meta folder";
                SNESMETATXT.ForeColor = Color.Red;
            }
            else
            {
                SNESMETAIMG.Image = Properties.Resources.yes;
                SNESMETATXT.Text = "Meta folder found";
                SNESMETATXT.ForeColor = Color.FromArgb(0, 127, 14);
            }

        }

        private void SNESCODETXT_Click(object sender, EventArgs e)
        {

        }

        private void SNESCONTENTTXT_Click(object sender, EventArgs e)
        {

        }

        private void SNESMETATXT_Click(object sender, EventArgs e)
        {

        }

        private void SMETROID_Tick(object sender, EventArgs e)
        {

            #region SUPER METROID EU

            if (Properties.Settings.Default.SMetroidEU == "")
            {
                SMEUTK.Image = Properties.Resources.X;
                SMEU_TKEY.Text = "No TitleKey stored";
                SMEU_TKEY.ForeColor = Color.Red;
            }
            else
            {
                SMEUTK.Image = Properties.Resources.yes;
                SMEU_TKEY.Text = "TitleKey found";
                SMEU_TKEY.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/SMetroidEU"))
            {
                SMEUBASE.Image = Properties.Resources.X;
                SMETROIDEUFOLDER.Text = "Base not downloaded";
                SMETROIDEUFOLDER.ForeColor = Color.Red;
            }
            else
            {
                SMEUBASE.Image = Properties.Resources.yes;
                SMETROIDEUFOLDER.Text = "Base downloaded";
                SMETROIDEUFOLDER.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (Properties.Settings.Default.SMetroidEU == "")
            {
                SMEU_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    SMEU_DWNLD.Enabled = false;
                }
                else
                {
                    SMEU_DWNLD.Cursor = Cursors.Default;
                    toolTip1.SetToolTip(SMEU_DWNLD, null);
                    SMEU_DWNLD.Enabled = true;
                }
            
        
        }
            #endregion
        }
        private void SMETROIDJP_Tick(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.SMetroidJP == "")
            {
                SMJPTKIMG.Image = Properties.Resources.X;
                SMJPTK.Text = "No TitleKey stored";
                SMJPTK.ForeColor = Color.Red;
            }
            else
            {
                SMEUTK.Image = Properties.Resources.yes;
                SMEU_TKEY.Text = "TitleKey found";
                SMEU_TKEY.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/SMetroidJP"))
            {
                SMJPBASEIMG.Image = Properties.Resources.X;
                SMJPBASE.Text = "Base not downloaded";
                SMJPBASE.ForeColor = Color.Red;
            }
            else
            {
                SMJPBASEIMG.Image = Properties.Resources.yes;
                SMJPBASE.Text = "Base downloaded";
                SMJPBASE.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (Properties.Settings.Default.SMetroidJP == "")
            {
                SMJP_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    SMJP_DWNLD.Enabled = false;
                }
                else
                {
                    SMJP_DWNLD.Cursor = Cursors.Default;
                    toolTip1.SetToolTip(SMJP_DWNLD, null);
                    SMJP_DWNLD.Enabled = true;
                }
            }
        }
        private void SNES_ROM_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter =  "SNES roms (*.sfc)|*.sfc";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox31.Text = openFileDialog1.FileName;
                INJCT_ROM_path = textBox31.Text;

            }
        }

        private void N64_BTN8_Click(object sender, EventArgs e)
        {

        }

        private void SMETROIDUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SMetroidUS == "")
            {
                SMUSTKEYIMG.Image = Properties.Resources.X;
                SMUSTKEY.Text = "No TitleKey stored";
                SMUSTKEY.ForeColor = Color.Red;
            }
            else
            {
                SMUSTKEYIMG.Image = Properties.Resources.yes;
                SMUSTKEY.Text = "TitleKey found";
                SMUSTKEY.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/SMetroidUS"))
            {
                SMUSBASEIMG.Image = Properties.Resources.X;
                SMUSBASE.Text = "Base not downloaded";
                SMUSBASE.ForeColor = Color.Red;
            }
            else
            {
                SMUSBASEIMG.Image = Properties.Resources.yes;
                SMUSBASE.Text = "Base downloaded";
                SMUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (SMUS_DWNLD.Enabled == false)
            {



                if (Properties.Settings.Default.SMetroidUS == "")
                {
                    SMUS_DWNLD.Enabled = false;
                }
                else
                {
                    if (Properties.Settings.Default.CommonKey == "")
                    {
                        SMUS_DWNLD.Enabled = false;
                    }
                    else
                    {
                        SMUS_DWNLD.Cursor = Cursors.Default;
                        toolTip1.SetToolTip(SMUS_DWNLD, null);
                        SMUS_DWNLD.Enabled = true;
                    }
                }
            }
        }
        #endregion

        #region misc
        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;

        }
        private bool Checking()
        {
            if (Properties.Settings.Default.BaseRomPath != "" && Properties.Settings.Default.WorkingPath != "" && Properties.Settings.Default.InjectionPath != "")
            {
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
            #region gba
            groupBox4.ForeColor = Color.WhiteSmoke;
            groupBox5.ForeColor = Color.WhiteSmoke;
            groupBox6.ForeColor = Color.WhiteSmoke;
            GBA.BackColor = Color.FromArgb(60, 60, 60);
            GBA.ForeColor = Color.WhiteSmoke;
            GBA_BACK.ForeColor = Color.Black;
            GBA_CUSTOM.ForeColor = Color.Black;
            GBA_DRC.ForeColor = Color.Black;
            GBA_ICON.ForeColor = Color.Black;
            GBA_INJECT.ForeColor = Color.Black;
            GBA_INST.ForeColor = Color.Black;
            GBA_LOADIINE.ForeColor = Color.Black;
            GBA_LOGO.ForeColor = Color.Black;
            GBA_ROM.ForeColor = Color.Black;
            GBA_TV.ForeColor = Color.Black;
            #endregion
            #region nes
            groupBox7.ForeColor = Color.WhiteSmoke;
            groupBox8.ForeColor = Color.WhiteSmoke;
            groupBox9.ForeColor = Color.WhiteSmoke;
            NES.BackColor = Color.FromArgb(60, 60, 60);
            NES.ForeColor = Color.WhiteSmoke;
            NES_BACK.ForeColor = Color.Black;
            NES_CSTM.ForeColor = Color.Black;
            NES_DRC.ForeColor = Color.Black;
            NES_ICON.ForeColor = Color.Black;
            NES_INJCT.ForeColor = Color.Black;
            NES_INST.ForeColor = Color.Black;
            NES_LOADIINE.ForeColor = Color.Black;
            NES_LOGO.ForeColor = Color.Black;
            NES_ROM.ForeColor = Color.Black;
            NES_TV.ForeColor = Color.Black;
            #endregion
            #region snes
            
            groupBox10.ForeColor = Color.WhiteSmoke;
            groupBox11.ForeColor = Color.WhiteSmoke;
            groupBox12.ForeColor = Color.WhiteSmoke;
            SNES.BackColor = Color.FromArgb(60, 60, 60);
            SNES.ForeColor = Color.WhiteSmoke;
            SNES_BACK.ForeColor = Color.Black;
            //SNES_CSTM.ForeColor = Color.Black;
            SNES_DRC.ForeColor = Color.Black;
            SNES_ICON.ForeColor = Color.Black;
            SNES_INJCT.ForeColor = Color.Black;
            SNES_INST.ForeColor = Color.Black;
            SNES_LOADIINE.ForeColor = Color.Black;
            SNES_LOGO.ForeColor = Color.Black;
            SNES_ROM.ForeColor = Color.Black;
            SNES_TV.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
            SMEU_DWNLD.ForeColor = Color.Black;
            SMUS_DWNLD.ForeColor = Color.Black;
            SMJP_DWNLD.ForeColor = Color.Black;
            #region bases
            


            #endregion
            #endregion
            #region WII/GC TODO
            #endregion
        }
        #endregion
        #region UI buttons i guess
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.allpathset)
            {
                if (Properties.Settings.Default.BaseRomPath != null && Properties.Settings.Default.WorkingPath != null && Properties.Settings.Default.InjectionPath != null)
                {
                    Properties.Settings.Default.allpathset = false;
                    Properties.Settings.Default.Save();

                }
            }
            Application.Restart();
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
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.Show();
        }



        private void HilfeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form ckey = new CKey();
            ckey.Show();
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form path = new PathMenu();
            path.Show();
        }














        #endregion

        private void SNES_TV_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox30.Text = openFileDialog1.FileName;
                bootimages[0] = textBox30.Text;

            }
        }

        private void SNES_DRC_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox29.Text = openFileDialog1.FileName;
                bootimages[1] = textBox29.Text;

            }
        }

        private void SNES_ICON_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox28.Text = openFileDialog1.FileName;
                bootimages[2] = textBox28.Text;

            }
        }

        private void SNES_LOGO_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.png;*.tga";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox27.Text = openFileDialog1.FileName;
                bootimages[3] = textBox27.Text;

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
