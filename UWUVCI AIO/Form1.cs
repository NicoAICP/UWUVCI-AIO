using AutoUpdaterDotNET;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
            INJCT_ROM_path = null;
            ini_path = null;
            for(int i = 0; i < bootimages.Count(); i++)
            {
                bootimages[i] = null;
            }
            SNESCSTMNFOLDERS.Enabled = false;
            EBEU.Enabled = false;
            EBUS.Enabled = false;
            EBJP.Enabled = false;
            SMETROIDJP.Enabled = false;
            SMETROIDUS.Enabled = false;
            DKCEU.Enabled = false;
            N64CSTMNFOLDERS.Enabled = false;
            PMEU.Enabled = false;
            PMUS.Enabled = false;
            FZXUS.Enabled = false;
            FZXJP.Enabled = false;
            DK64EU.Enabled = false;
            DK64US.Enabled = false;
            NDSCSTMNFOLDERS.Enabled = false;
            ZSTEU.Enabled = false;
            CSTMBaseRom_path = null;
            ZSTUS.Enabled = false;
            ZSTUS.Enabled = false;
            SMETROIDEU.Enabled = false;
            ZPHEU.Enabled = false;
            ZPHUS.Enabled = false;
            WWEU.Enabled = false;
            WWUS.Enabled = false;
            POEU.Enabled = false;
            POUS.Enabled = false;
            SMBEU.Enabled = false;
            SMBUS.Enabled = false;
            ZMCEU.Enabled = false;
            ZMCUS.Enabled = false;
            MKCEU.Enabled = false;
            MKCUS.Enabled = false;
            Injection.clean();
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
            Injection.clean();
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
            Injection.clean();
            tabControl1.SelectedIndex = 0;
        }

        private void SNES_BACK_Click(object sender, EventArgs e)
        {
            ResetInput();
            Injection.clean();
            tabControl1.SelectedIndex = 0;
        }

        private void GBA_BACK_Click(object sender, EventArgs e)
        {
            ResetInput();
            Injection.clean();
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
            
            tabControl1.SelectedIndex = 5;
        }

        private void UWUVCI_AIO_Load(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) //SNES
        {
            int index = comboBox1.SelectedIndex;
            SNESBRinfopanel(index);
        }
        private void N64BRinfopanel(int b)
        {
            if(b == 0)
            {
                BaseROM = "Custom";
                #region Panels
                N64CSTM_PANEL.Visible = true;
                PMEU_PANEL.Visible = false;
                PMUS_PANEL.Visible = false;
                FXZUS_PANEL.Visible = false;
                FZXJP_PANEL.Visible = false;
                DK64EU_PANEL.Visible = false;
                DK64US_PANEL.Visible = false;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = true;
                PMEU.Enabled = false;
                PMUS.Enabled = false;
                FZXUS.Enabled = false;
                FZXJP.Enabled = false;
                DK64EU.Enabled = false;
                DK64US.Enabled = false;
                #endregion
            }
            if (b == 1)
            {
                BaseROM = "PMEU";
                #region Panels
                N64CSTM_PANEL.Visible = false;
                PMEU_PANEL.Visible = true;
                PMUS_PANEL.Visible = false;
                FXZUS_PANEL.Visible = false;
                FZXJP_PANEL.Visible = false;
                DK64EU_PANEL.Visible = false;
                DK64US_PANEL.Visible = false;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = false;
                PMEU.Enabled = true;
                PMUS.Enabled = false;
                FZXUS.Enabled = false;
                FZXJP.Enabled = false;
                DK64EU.Enabled = false;
                DK64US.Enabled = false;
                #endregion
            }
            if (b == 2)
            {
                BaseROM = "PMUS";
                #region Panels
                N64CSTM_PANEL.Visible = false;
                PMEU_PANEL.Visible = false;
                PMUS_PANEL.Visible = true;
                FXZUS_PANEL.Visible = false;
                FZXJP_PANEL.Visible = false;
                DK64EU_PANEL.Visible = false;
                DK64US_PANEL.Visible = false;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = false;
                PMEU.Enabled = false;
                PMUS.Enabled = true;
                FZXUS.Enabled = false;
                FZXJP.Enabled = false;
                DK64EU.Enabled = false;
                DK64US.Enabled = false;
                #endregion
            }
            if (b == 3)
            {
                BaseROM = "FZXUS";
                #region Panels
                N64CSTM_PANEL.Visible = false;
                PMEU_PANEL.Visible = false;
                PMUS_PANEL.Visible = false;
                FXZUS_PANEL.Visible = true;
                FZXJP_PANEL.Visible = false;
                DK64EU_PANEL.Visible = false;
                DK64US_PANEL.Visible = false;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = false;
                PMEU.Enabled = false;
                PMUS.Enabled = false;
                FZXUS.Enabled = true;
                FZXJP.Enabled = false;
                DK64EU.Enabled = false;
                DK64US.Enabled = false;
                #endregion
            }
            if (b == 4)
            {
                BaseROM = "FZXJP";
                #region Panels
                N64CSTM_PANEL.Visible = false;
                PMEU_PANEL.Visible = false;
                PMUS_PANEL.Visible = false;
                FXZUS_PANEL.Visible = false;
                FZXJP_PANEL.Visible = true;
                DK64EU_PANEL.Visible = false;
                DK64US_PANEL.Visible = false;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = false;
                PMEU.Enabled = false;
                PMUS.Enabled = false;
                FZXUS.Enabled = false;
                FZXJP.Enabled = true;
                DK64EU.Enabled = false;
                DK64US.Enabled = false;
                #endregion
            }
            if (b == 5)
            {
                BaseROM = "DK64EU";
                #region Panels
                N64CSTM_PANEL.Visible = false;
                PMEU_PANEL.Visible = false;
                PMUS_PANEL.Visible = false;
                FXZUS_PANEL.Visible = false;
                FZXJP_PANEL.Visible = false;
                DK64EU_PANEL.Visible = true;
                DK64US_PANEL.Visible = false;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = false;
                PMEU.Enabled = false;
                PMUS.Enabled = false;
                FZXUS.Enabled = false;
                FZXJP.Enabled = false;
                DK64EU.Enabled = true;
                DK64US.Enabled = false;
                #endregion
            }
            if (b == 6)
            {
                BaseROM = "DK64US";
                #region Panels
                N64CSTM_PANEL.Visible = false;
                PMEU_PANEL.Visible = false;
                PMUS_PANEL.Visible = false;
                FXZUS_PANEL.Visible = false;
                FZXJP_PANEL.Visible = false;
                DK64EU_PANEL.Visible = false;
                DK64US_PANEL.Visible = true;
                #endregion
                #region Timers
                N64CSTMNFOLDERS.Enabled = false;
                PMEU.Enabled = false;
                PMUS.Enabled = false;
                FZXUS.Enabled = false;
                FZXJP.Enabled = false;
                DK64EU.Enabled = false;
                DK64US.Enabled =true;
                #endregion
            }
        }
        private void SNESBRinfopanel(int b)
        {
            if (b == 0)
            {
                SNESCUSTOM.Visible = true;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "Custom";
                #region timers
                SNESCSTMNFOLDERS.Enabled = true;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 1)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = true;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "SMetroidEU";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = true;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 2)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = true;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "SMetroidUS";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = true;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 3)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                SMJP_PANEL.Visible = true;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "SMetroidJP";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = true;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if(b == 4)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = true;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "EarthboundEU";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = true;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 5)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = true;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "EarthboundUS";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                DKCEU.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = true;
                EBJP.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 6)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = true;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = false;
                BaseROM = "EarthboundJP";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = true;
                DKCEU.Enabled = false;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 7)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = true;
                DKCUS_PANEL.Visible = false;
                BaseROM = "DKCEU";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = true;
                DKCUS.Enabled = false;
                #endregion
            }
            if (b == 8)
            {
                SNESCUSTOM.Visible = false;
                SMetroidEUPanel.Visible = false;
                SMUS_PANEL.Visible = false;
                SMJP_PANEL.Visible = false;
                EBEU_PANEL.Visible = false;
                EBUS_PANEL.Visible = false;
                EBJP_PANEL.Visible = false;
                DKCEU_PANEL.Visible = false;
                DKCUS_PANEL.Visible = true;
                BaseROM = "DKCUS";
                #region timers
                SNESCSTMNFOLDERS.Enabled = false;
                SMETROIDEU.Enabled = false;
                SMETROIDUS.Enabled = false;
                SMETROIDJP.Enabled = false;
                EBEU.Enabled = false;
                EBUS.Enabled = false;
                EBJP.Enabled = false;
                DKCEU.Enabled = false;
                DKCUS.Enabled = true;
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
                if (tabControl1.SelectedIndex == 1) //NDS
                {
                    ZSTEUCKIMG.Image = Properties.Resources.yes;
                    ZSTEUCK.Text = "No CommonKey stored";
                    ZSTEUCK.ForeColor = Color.Red;
                    ZSTUSCKIMG.Image = Properties.Resources.yes;
                    ZSTUSCK.Text = "No CommonKey stored";
                    ZSTUSCK.ForeColor = Color.Red;
                    ZPHEUCKIMG.Image = Properties.Resources.yes;
                    ZPHEUCK.Text = "No CommonKey stored";
                    ZPHEUCK.ForeColor = Color.Red;
                    ZPHUSCKIMG.Image = Properties.Resources.yes;
                    ZPHUSCK.Text = "No CommonKey stored";
                    ZPHUSCK.ForeColor = Color.Red;
                    WWEUCKIMG.Image = Properties.Resources.yes;
                    WWEUCK.Text = "No CommonKey stored";
                    WWEUCK.ForeColor = Color.Red;
                    WWUSCKIMG.Image = Properties.Resources.yes;
                    WWUSCK.Text = "No CommonKey stored";
                    WWUSCK.ForeColor = Color.Red;
                }
                if (tabControl1.SelectedIndex == 2) //N64
                {
                    PMEUCKIMG.Image = Properties.Resources.yes;
                    PMEUCK.Text = "No CommonKey stored";
                    PMEUCK.ForeColor = Color.Red;
                    PMUSCKIMG.Image = Properties.Resources.yes;
                    PMUSCK.Text = "No CommonKey stored";
                    PMUSCK.ForeColor = Color.Red;
                    FXZUSCKIMG.Image = Properties.Resources.yes;
                    FZXUSCK.Text = "No CommonKey stored";
                    FZXUSCK.ForeColor = Color.Red;
                    FZXJPCKIMG.Image = Properties.Resources.yes;
                    FZXJPCK.Text = "No CommonKey stored";
                    FZXJPCK.ForeColor = Color.Red;
                    DK64USCKIMG.Image = Properties.Resources.yes;
                    DK64USCK.Text = "No CommonKey stored";
                    DK64USCK.ForeColor = Color.Red;
                    DK64EUCKIMG.Image = Properties.Resources.yes;
                    DK64EUCK.Text = "No CommonKey stored";
                    DK64EUCK.ForeColor = Color.Red;
                }
                if (tabControl1.SelectedIndex == 3) //GBA
                {
                    ZMCEUCKIMG.Image = Properties.Resources.yes;
                    ZMCEUCK.Text = "No CommonKey stored";
                    ZMCEUCK.ForeColor = Color.Red;
                    ZMCUSCKIMG.Image = Properties.Resources.yes;
                    ZMCUSCK.Text = "No CommonKey stored";
                    ZMCUSCK.ForeColor = Color.Red;

                    MKCEUCKIMG.Image = Properties.Resources.yes;
                    MKCEUCK.Text = "No CommonKey stored";
                    MKCEUCK.ForeColor = Color.Red;
                    MKCUSCKIMG.Image = Properties.Resources.yes;
                    MKCUSCK.Text = "No CommonKey stored";
                    MKCUSCK.ForeColor = Color.Red;
                }
                if (tabControl1.SelectedIndex == 4) //NES
                {
                    POEUCKIMG.Image = Properties.Resources.yes;
                    POEUCK.Text = "No CommonKey stored";
                    POEUCK.ForeColor = Color.Red;
                    POUSCKIMG.Image = Properties.Resources.yes;
                    POUSCK.Text = "No CommonKey stored";
                    POUSCK.ForeColor = Color.Red;
                    SMBEUCKIMG.Image = Properties.Resources.yes;
                    SMBEUCK.Text = "No CommonKey stored";
                    SMBEUCK.ForeColor = Color.Red;
                    SMBUSCKIMG.Image = Properties.Resources.yes;
                    SMBUSCK.Text = "No CommonKey stored";
                    SMBUSCK.ForeColor = Color.Red;
                }
                if(tabControl1.SelectedIndex == 5) //SNES
                {
                    SMEUCK.Image = Properties.Resources.X;
                    SMEEU_CKEY.Text = "No CommonKey stored";
                    SMEEU_CKEY.ForeColor = Color.Red;
                    SMUSCKEYIMG.Image = Properties.Resources.X;
                    SMUSCKEY.Text = "No CommonKey stored";
                    SMUSCKEY.ForeColor = Color.Red;
                    SMJPCKEYIMG.Image = Properties.Resources.X;
                    SMJPCKEY.Text = "No CommonKey stored";
                    SMJPCKEY.ForeColor = Color.Red;
                    EBEU_CKIMG.Image = Properties.Resources.X;
                    EBEU_CK.Text = "No CommonKey stored";
                    EBEU_CK.ForeColor = Color.Red;
                    EBUS_CKIMG.Image = Properties.Resources.X;
                    EBUS_CK.Text = "No CommonKey stored";
                    EBUS_CK.ForeColor = Color.Red;
                    EBJP_CKIMG.Image = Properties.Resources.X;
                    EBJP_CK.Text = "No CommonKey stored";
                    EBJP_CK.ForeColor = Color.Red;
                    DKCEU_CKIMG.Image = Properties.Resources.X;
                    DKCEU_CK.Text = "No CommonKey stored";
                    DKCEU_CK.ForeColor = Color.Red;
                    DKCUS_CKIMG.Image = Properties.Resources.X;
                    DKCUS_CK.Text = "No CommonKey stored";
                    DKCUS_CK.ForeColor = Color.Red;
                }
                
            }
            else
            {
                if (tabControl1.SelectedIndex == 1) //NDS
                {
                    ZSTEUCKIMG.Image = Properties.Resources.yes;
                    ZSTEUCK.Text = "CommonKey found";
                    ZSTEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    ZSTUSCKIMG.Image = Properties.Resources.yes;
                    ZSTUSCK.Text = "CommonKey found";
                    ZSTUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                    ZPHEUCKIMG.Image = Properties.Resources.yes;
                    ZPHEUCK.Text = "CommonKey found";
                    ZPHEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    ZPHUSCKIMG.Image = Properties.Resources.yes;
                    ZPHUSCK.Text = "CommonKey found";
                    ZPHUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                    WWEUCKIMG.Image = Properties.Resources.yes;
                    WWEUCK.Text = "CommonKey found";
                    WWEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    WWUSCKIMG.Image = Properties.Resources.yes;
                    WWUSCK.Text = "CommonKey found";
                    WWUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                }
                if (tabControl1.SelectedIndex == 2) //N64
                {
                    PMEUCKIMG.Image = Properties.Resources.yes;
                    PMEUCK.Text = "CommonKey found";
                    PMEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    PMUSCKIMG.Image = Properties.Resources.yes;
                    PMUSCK.Text = "CommonKey found";
                    PMUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                    FXZUSCKIMG.Image = Properties.Resources.yes;
                    FZXUSCK.Text = "CommonKey found";
                    FZXUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                    FZXJPCKIMG.Image = Properties.Resources.yes;
                    FZXJPCK.Text = "CommonKey found";
                    FZXJPCK.ForeColor = Color.FromArgb(0, 127, 14);
                    DK64EUCKIMG.Image = Properties.Resources.yes;
                    DK64EUCK.Text = "CommonKey found";
                    DK64EUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    DK64USCKIMG.Image = Properties.Resources.yes;
                    DK64USCK.Text = "CommonKey found";
                    DK64USCK.ForeColor = Color.FromArgb(0, 127, 14);
                }
                if (tabControl1.SelectedIndex == 3) //GBA
                {
                    ZMCEUCKIMG.Image = Properties.Resources.yes;
                    ZMCEUCK.Text = "CommonKey found";
                    ZMCEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    ZMCUSCKIMG.Image = Properties.Resources.yes;
                    ZMCUSCK.Text = "CommonKey found";
                    ZMCUSCK.ForeColor = Color.FromArgb(0, 127, 14);

                    MKCEUCKIMG.Image = Properties.Resources.yes;
                    MKCEUCK.Text = "CommonKey found";
                    MKCEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    MKCUSCKIMG.Image = Properties.Resources.yes;
                    MKCUSCK.Text = "CommonKey found";
                    MKCUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                }
                if (tabControl1.SelectedIndex == 4) //NES
                {
                    POEUCKIMG.Image = Properties.Resources.yes;
                    POEUCK.Text = "CommonKey found";
                    POEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    POUSCKIMG.Image = Properties.Resources.yes;
                    POUSCK.Text = "CommonKey found";
                    POUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                    SMBEUCKIMG.Image = Properties.Resources.yes;
                    SMBEUCK.Text = "CommonKey found";
                    SMBEUCK.ForeColor = Color.FromArgb(0, 127, 14);
                    SMBUSCKIMG.Image = Properties.Resources.yes;
                    SMBUSCK.Text = "CommonKey found";
                    SMBUSCK.ForeColor = Color.FromArgb(0, 127, 14);
                }
                if (tabControl1.SelectedIndex == 5) //SNES
                {
                    SMEUCK.Image = Properties.Resources.yes;
                    SMEEU_CKEY.Text = "CommonKey found";
                    SMEEU_CKEY.ForeColor = Color.FromArgb(0, 127, 14);
                    SMUSCKEYIMG.Image = Properties.Resources.yes;
                    SMUSCKEY.Text = "CommonKey found";
                    SMUSCKEY.ForeColor = Color.FromArgb(0, 127, 14);
                    SMJPCKEYIMG.Image = Properties.Resources.yes;
                    SMJPCKEY.Text = "CommonKey found";
                    SMJPCKEY.ForeColor = Color.FromArgb(0, 127, 14);
                    EBEU_CKIMG.Image = Properties.Resources.yes;
                    EBEU_CK.Text = "CommonKey found";
                    EBEU_CK.ForeColor = Color.FromArgb(0, 127, 14);
                    EBUS_CKIMG.Image = Properties.Resources.yes;
                    EBUS_CK.Text = "CommonKey found";
                    EBUS_CK.ForeColor = Color.FromArgb(0, 127, 14);
                    EBJP_CKIMG.Image = Properties.Resources.yes;
                    EBJP_CK.Text = "CommonKey found";
                    EBJP_CK.ForeColor = Color.FromArgb(0, 127, 14);
                    DKCEU_CKIMG.Image = Properties.Resources.yes;
                    DKCEU_CK.Text = "CommonKey found";
                    DKCEU_CK.ForeColor = Color.FromArgb(0, 127, 14);
                    DKCUS_CKIMG.Image = Properties.Resources.yes;
                    DKCUS_CK.Text = "CommonKey found";
                    DKCUS_CK.ForeColor = Color.FromArgb(0, 127, 14);
                }
                    
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
                SNES_INJCT.Enabled = false;
            }
            else
            {
                SNES_INJCT.Enabled = true;
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
                SNES_INJCT.Enabled = false;
            }
            else
            {
                SMEUBASE.Image = Properties.Resources.yes;
                SMETROIDEUFOLDER.Text = "Base downloaded";
                SMETROIDEUFOLDER.ForeColor = Color.FromArgb(0, 127, 14);
                SMEU_DWNLD.Visible = false;
                SNES_INJCT.Enabled = true;
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
                SNES_INJCT.Enabled = false;
            }
            else
            {
                SMJPBASEIMG.Image = Properties.Resources.yes;
                SMJPBASE.Text = "Base downloaded";
                SMJPBASE.ForeColor = Color.FromArgb(0, 127, 14);
                SNES_INJCT.Enabled = true;
                SMJP_DWNLD.Visible = false;
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
            MessageBox.Show("Opening romutil now. Load your SNES rom in it and check if the checkbox Header is checked. If thats the case click remove header and then okay. This will create a new file called <Gamename>_noheader.sfc/smc. Use this file. If its not the case, continue with your current rom.");
            Process romutil = new Process();
            romutil.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Tools/romutil.exe");
            romutil.Start();
            romutil.WaitForExit();
            openFileDialog1.Filter =  "SNES roms |*.sfc;*.smc";
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
            openFileDialog1.Filter = "N64 Roms |*.n64;*.v64;*.z64" ;
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox1.Text = openFileDialog1.FileName;
                INJCT_ROM_path = textBox1.Text;

            }
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
                SNES_INJCT.Enabled = false;
            }
            else
            {
                SMUSBASEIMG.Image = Properties.Resources.yes;
                SMUSBASE.Text = "Base downloaded";
                SMUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                SNES_INJCT.Enabled = true;
                SMUS_DWNLD.Visible = false;
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
            N64_BTN16.ForeColor = Color.Black;
            N64_BTN17.ForeColor = Color.Black;
            N64CSTMN_PATH_BTN.ForeColor = Color.Black;
            PMEU_DWNLD.ForeColor = Color.Black;
            PMUS_DWNLD.ForeColor = Color.Black;
            FZXUS_DWNLD.ForeColor = Color.Black;
            FZXJP_DWNLD.ForeColor = Color.Black;
            DK64US_DWNLD.ForeColor = Color.Black;
            DK64EU_DWNLD.ForeColor = Color.Black;
            #endregion
            #region nds
            groupBox1.ForeColor = Color.WhiteSmoke;
            groupBox2.ForeColor = Color.WhiteSmoke;
            groupBox3.ForeColor = Color.WhiteSmoke;
            NDS.BackColor = Color.FromArgb(60, 60, 60);
            NDS.ForeColor = Color.WhiteSmoke;
            back_nds.ForeColor = Color.Black;
            ZSTEU_BTN.ForeColor = Color.Black;
            NDS_BTN10.ForeColor = Color.Black;
            NDS_BTN11.ForeColor = Color.Black;
            NDS_BTN12.ForeColor = Color.Black;
            NDS_BTN13.ForeColor = Color.Black;
            NDS_BTN14.ForeColor = Color.Black;
            NDS_BTN15.ForeColor = Color.Black;
            NDSCSTMN_BTN.ForeColor = Color.Black;
            NDS_BTN16.ForeColor = Color.Black;
            NDS_BTN17.ForeColor = Color.Black;
            ZSTUS_BTN.ForeColor = Color.Black;
            ZPHEU_BTN.ForeColor = Color.Black;
            ZPHUS_BTN.ForeColor = Color.Black;
            WWEU_BTN.ForeColor = Color.Black;
            WWUS_BTN.ForeColor = Color.Black;
            
            #endregion
            #region gba
            groupBox4.ForeColor = Color.WhiteSmoke;
            groupBox5.ForeColor = Color.WhiteSmoke;
            groupBox6.ForeColor = Color.WhiteSmoke;
            GBA.BackColor = Color.FromArgb(60, 60, 60);
            GBA.ForeColor = Color.WhiteSmoke;
            GBA_BACK.ForeColor = Color.Black;
            
            GBA_DRC.ForeColor = Color.Black;
            GBA_ICON.ForeColor = Color.Black;
            GBA_INJECT.ForeColor = Color.Black;
            GBA_INST.ForeColor = Color.Black;
            GBA_LOADIINE.ForeColor = Color.Black;
            GBA_LOGO.ForeColor = Color.Black;
            GBA_ROM.ForeColor = Color.Black;
            GBA_TV.ForeColor = Color.Black;
            GBACSTMN_BTN.ForeColor = Color.Black;
            ZMCEU_BTN.ForeColor = Color.Black;
            ZMCUS_BTN.ForeColor = Color.Black;
            MKCEU_BTN.ForeColor = Color.Black;
            MKCUS_BTN.ForeColor = Color.Black;
            #endregion
            #region nes
            groupBox7.ForeColor = Color.WhiteSmoke;
            groupBox8.ForeColor = Color.WhiteSmoke;
            groupBox9.ForeColor = Color.WhiteSmoke;
            NES.BackColor = Color.FromArgb(60, 60, 60);
            NES.ForeColor = Color.WhiteSmoke;
            NES_BACK.ForeColor = Color.Black;
            NES_DRC.ForeColor = Color.Black;
            NES_ICON.ForeColor = Color.Black;
            NES_INJCT.ForeColor = Color.Black;
            NES_INST.ForeColor = Color.Black;
            NES_LOADIINE.ForeColor = Color.Black;
            NES_LOGO.ForeColor = Color.Black;
            NES_ROM.ForeColor = Color.Black;
            NES_TV.ForeColor = Color.Black;
            NESCSTMN_BTN.ForeColor = Color.Black;
            POEU_BTN.ForeColor = Color.Black;
            POUS_BTN.ForeColor = Color.Black;
            SMBEU_BTN.ForeColor = Color.Black;
            SMBUS_BTN.ForeColor = Color.Black;
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
            EBEU_DWNLD.ForeColor = Color.Black;
            EBUS_DWNLD.ForeColor = Color.Black;
            EBJP_DWNLD.ForeColor = Color.Black;
            DKCEU_DWNLD.ForeColor = Color.Black;
            DKCUS_DWNLD.ForeColor = Color.Black;
            
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

        private void SNES_INJCT_Click(object sender, EventArgs e)
        {
            Injection.inject(Injection.Console.SNES, BaseROM, CSTMBaseRom_path, INJCT_ROM_path, ini_path, bootimages, textBox26.Text, false);
            SNES_INST.Enabled = true;
            SNES_LOADIINE.Enabled = true;
        }

        private void SNES_INST_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show("To use this option you need to enter the CommonKey (Settings -> Set CommonKey)");

            }
            else
            {
                Injection.packing(textBox26.Text);
                SNES_INST.Enabled = false;
                SNES_LOADIINE.Enabled = false;
            }
        }

        private void SNES_LOADIINE_Click(object sender, EventArgs e)
        {
            Injection.loadiine(textBox26.Text);
            SNES_INST.Enabled = false;
            SNES_LOADIINE.Enabled = false;
        }

        private void SMJP_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void SMUS_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void SMEU_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.EarthboundEU == "")
            {
                EBEU_TKIMG.Image = Properties.Resources.X;
                EBEU_TK.Text = "No TitleKey stored";
                EBEU_TK.ForeColor = Color.Red;
            }
            else
            {
                EBEU_TKIMG.Image = Properties.Resources.yes;
                EBEU_TK.Text = "TitleKey found";
                EBEU_TK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/EarthboundEU"))
            {
                EBEU_BASEIMG.Image = Properties.Resources.X;
                EBEU_BASE.Text = "Base not downloaded";
                EBEU_BASE.ForeColor = Color.Red;
                SNES_INJCT.Enabled = false;
            }
            else
            {
                EBEU_BASEIMG.Image = Properties.Resources.yes;
                EBEU_BASE.Text = "Base downloaded";
                EBEU_BASE.ForeColor = Color.FromArgb(0, 127, 14);
                EBEU_DWNLD.Visible = false;
                SNES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.EarthboundEU == "")
            {
                EBEU_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    EBEU_DWNLD.Enabled = false;
                }
                else
                {
                    EBEU_DWNLD.Cursor = Cursors.Default;
                    EBEU_DWNLD.Enabled = true;
                }


            }
        }

        private void UWUVCI_AIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            Injection.clean();
        }

        private void EBEU_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void EBUS_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void EBUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.EarthboundUS == "")
            {
                EBUS_TKIMG.Image = Properties.Resources.X;
                EBUS_TK.Text = "No TitleKey stored";
                EBUS_TK.ForeColor = Color.Red;
            }
            else
            {
                EBUS_TKIMG.Image = Properties.Resources.yes;
                EBUS_TK.Text = "TitleKey found";
                EBUS_TK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/EarthboundUS"))
            {
                EBUS_BASEIMG.Image = Properties.Resources.X;
                EBUS_BASE.Text = "Base not downloaded";
                EBUS_BASE.ForeColor = Color.Red;
                SNES_INJCT.Enabled = false;
            }
            else
            {
                EBUS_BASEIMG.Image = Properties.Resources.yes;
                EBUS_BASE.Text = "Base downloaded";
                EBUS_BASE.ForeColor = Color.FromArgb(0, 127, 14);
                EBUS_DWNLD.Visible = false;
                SNES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.EarthboundUS == "")
            {
                EBUS_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    EBUS_DWNLD.Enabled = false;
                }
                else
                {
                    EBUS_DWNLD.Cursor = Cursors.Default;
                    EBUS_DWNLD.Enabled = true;
                }


            }
        }

        private void EBJP_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void EBJP_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.EarthboundJP == "")
            {
                EBJP_TKIMG.Image = Properties.Resources.X;
                EBJP_TK.Text = "No TitleKey stored";
                EBJP_TK.ForeColor = Color.Red;
            }
            else
            {
                EBJP_TKIMG.Image = Properties.Resources.yes;
                EBJP_TK.Text = "TitleKey found";
                EBJP_TK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/EarthboundJP"))
            {
                EBJP_BASEIMG.Image = Properties.Resources.X;
                EBJP_BASE.Text = "Base not downloaded";
                EBJP_BASE.ForeColor = Color.Red;
                SNES_INJCT.Enabled = false;
            }
            else
            {
                EBJP_BASEIMG.Image = Properties.Resources.yes;
                EBJP_BASE.Text = "Base downloaded";
                EBJP_BASE.ForeColor = Color.FromArgb(0, 127, 14);
                EBJP_DWNLD.Visible = false;
                SNES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.EarthboundJP == "")
            {
                EBJP_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    EBJP_DWNLD.Enabled = false;
                }
                else
                {
                    EBJP_DWNLD.Cursor = Cursors.Default;
                    EBJP_DWNLD.Enabled = true;
                }


            }
        }

        private void DKCEU_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void DKCEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DKCEU == "")
            {
                DKCEU_TKIMG.Image = Properties.Resources.X;
                DKCEU_TK.Text = "No TitleKey stored";
                DKCEU_TK.ForeColor = Color.Red;
            }
            else
            {
                DKCEU_TKIMG.Image = Properties.Resources.yes;
                DKCEU_TK.Text = "TitleKey found";
                DKCEU_TK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/DKCEU"))
            {
                DKCEU_BASEIMG.Image = Properties.Resources.X;
                DKCEU_BASE.Text = "Base not downloaded";
                DKCEU_BASE.ForeColor = Color.Red;
                SNES_INJCT.Enabled = false;
            }
            else
            {
                DKCEU_BASEIMG.Image = Properties.Resources.yes;
                DKCEU_BASE.Text = "Base downloaded";
                DKCEU_BASE.ForeColor = Color.FromArgb(0, 127, 14);
                DKCEU_DWNLD.Visible = false;
                SNES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.DKCEU == "")
            {
                DKCEU_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    DKCEU_DWNLD.Enabled = false;
                }
                else
                {
                    DKCEU_DWNLD.Cursor = Cursors.Default;
                    DKCEU_DWNLD.Enabled = true;
                }


            }
        }

        private void DKCUS_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void DKCUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DKCUS == "")
            {
                DKCUS_TKIMG.Image = Properties.Resources.X;
                DKCUS_TK.Text = "No TitleKey stored";
                DKCUS_TK.ForeColor = Color.Red;
            }
            else
            {
                DKCUS_TKIMG.Image = Properties.Resources.yes;
                DKCUS_TK.Text = "TitleKey found";
                DKCUS_TK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/DKCUS"))
            {
                DKCUS_BASEIMG.Image = Properties.Resources.X;
                DKCUS_BASE.Text = "Base not downloaded";
                DKCUS_BASE.ForeColor = Color.Red;
                SNES_INJCT.Enabled = false;
            }
            else
            {
                DKCUS_BASEIMG.Image = Properties.Resources.yes;
                DKCUS_BASE.Text = "Base downloaded";
                DKCUS_BASE.ForeColor = Color.FromArgb(0, 127, 14);
                DKCUS_DWNLD.Visible = false;
                SNES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.DKCUS == "")
            {
                DKCUS_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    DKCUS_DWNLD.Enabled = false;
                }
                else
                {
                    DKCUS_DWNLD.Cursor = Cursors.Default;
                    DKCUS_DWNLD.Enabled = true;
                }


            }

        }

        private void N64_BTN10_Click(object sender, EventArgs e)
        {
            ini_path = "blank";
            textBox2.Text = "Using blank INI";
        }

        private void N64_BTN13_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox5.Text = openFileDialog1.FileName;
                bootimages[2] = textBox5.Text;

            }
        }

        private void N64_BTN14_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox6.Text = openFileDialog1.FileName;
                bootimages[3] = textBox6.Text;

            }
        }

        private void N64_BTN12_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox4.Text = openFileDialog1.FileName;
                bootimages[1] = textBox4.Text;

            }
        }

        private void N64_BTN11_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox3.Text = openFileDialog1.FileName;
                bootimages[0] = textBox3.Text;

            }
        }

        private void N64_BTN9_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Configuartion Files |*.INI";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox2.Text = openFileDialog1.FileName;
                ini_path = textBox6.Text;

            }
        }

        private void N64_BTN15_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Injection.inject(Injection.Console.N64, BaseROM, CSTMBaseRom_path, INJCT_ROM_path, ini_path, bootimages, textBox7.Text, false);
            }
            else
            {
                Injection.inject(Injection.Console.N64, BaseROM, CSTMBaseRom_path, INJCT_ROM_path, ini_path, bootimages, textBox7.Text, true);
            }
            N64_BTN17.Enabled = true;
            N64_BTN16.Enabled = true;
        }

        private void N64_BTN17_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show("To use this option you need to enter the CommonKey (Settings -> Set CommonKey)");

            }
            else
            {
                Injection.packing(textBox7.Text);
                N64_BTN17.Enabled = false;
                N64_BTN16.Enabled = false;
            }
           
        }

        private void N64_BTN16_Click(object sender, EventArgs e)
        {
            Injection.loadiine(textBox7.Text);
            N64_BTN17.Enabled = false;
            N64_BTN16.Enabled = false;
        }

        private void NDS_BTN10_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "NDS Roms |*.nds;*.srl";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox14.Text = openFileDialog1.FileName;
                INJCT_ROM_path = textBox14.Text;

            }
        }

        private void NDS_BTN11_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox12.Text = openFileDialog1.FileName;
                bootimages[0] = textBox12.Text;

            }
        }

        private void NDS_BTN12_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox11.Text = openFileDialog1.FileName;
                bootimages[1] = textBox11.Text;

            }
        }

        private void NDS_BTN13_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox10.Text = openFileDialog1.FileName;
                bootimages[2] = textBox10.Text;

            }
        }

        private void NDS_BTN14_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox9.Text = openFileDialog1.FileName;
                bootimages[3] = textBox9.Text;

            }
        }

        private void NDS_BTN15_Click(object sender, EventArgs e)
        {
            Injection.inject(Injection.Console.NDS, BaseROM, CSTMBaseRom_path, INJCT_ROM_path, ini_path, bootimages, textBox8.Text,false);
            NDS_BTN17.Enabled = true;
            NDS_BTN16.Enabled = true;
        }

        private void NDS_BTN17_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show("To use this option you need to enter the CommonKey (Settings -> Set CommonKey)");

            }
            else
            {
                Injection.packing(textBox8.Text);
                NDS_BTN17.Enabled = false;
                NDS_BTN16.Enabled = false;
            }
            
        }

        private void NDS_BTN16_Click(object sender, EventArgs e)
        {
            Injection.loadiine(textBox8.Text);
            NDS_BTN17.Enabled = false;
            NDS_BTN16.Enabled = false;
        }

        private void NES_ROM_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "NES Roms |*.nes";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox25.Text = openFileDialog1.FileName;
                INJCT_ROM_path = textBox25.Text;

            }
        }

        private void NES_TV_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox24.Text = openFileDialog1.FileName;
                bootimages[0] = textBox24.Text;

            }
        }

        private void NES_DRC_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox23.Text = openFileDialog1.FileName;
                bootimages[1] = textBox23.Text;

            }
        }

        private void NES_ICON_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox22.Text = openFileDialog1.FileName;
                bootimages[2] = textBox22.Text;

            }
        }

        private void NES_LOGO_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox21.Text = openFileDialog1.FileName;
                bootimages[3] = textBox21.Text;

            }
        }

        private void NES_INJCT_Click(object sender, EventArgs e)
        {
            Injection.inject(Injection.Console.NES, BaseROM, CSTMBaseRom_path, INJCT_ROM_path, ini_path, bootimages, textBox20.Text,false);
            NES_INST.Enabled = true;
            NES_LOADIINE.Enabled = true;
        }

        private void NES_INST_Click(object sender, EventArgs e)
        {
            
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show("To use this option you need to enter the CommonKey (Settings -> Set CommonKey)");

            }
            else
            {
                Injection.packing(textBox20.Text);
                NES_INST.Enabled = false;
                NES_LOADIINE.Enabled = false;
            }
        }

        private void NES_LOADIINE_Click(object sender, EventArgs e)
        {
            NES_INST.Enabled = false;
            NES_LOADIINE.Enabled = false;
            Injection.loadiine(textBox20.Text);
        }

        private void GBA_INJECT_Click(object sender, EventArgs e)
        {
            Injection.inject(Injection.Console.GBA, BaseROM, CSTMBaseRom_path, INJCT_ROM_path, ini_path, bootimages, textBox13.Text, false);
            GBA_INST.Enabled = true;
            GBA_LOADIINE.Enabled = true;
        }

        private void GBA_INST_Click(object sender, EventArgs e)
        {
            
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show("To use this option you need to enter the CommonKey (Settings -> Set CommonKey)");

            }
            else
            {
                Injection.packing(textBox13.Text);
                GBA_INST.Enabled = false;
                GBA_LOADIINE.Enabled = false;
            }
        }

        private void GBA_LOADIINE_Click(object sender, EventArgs e)
        {
            Injection.loadiine(textBox13.Text);
            GBA_INST.Enabled = false;
            GBA_LOADIINE.Enabled = false;
        }

        private void GBA_ROM_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "GBA Roms |*.gba";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox19.Text = openFileDialog1.FileName;
                INJCT_ROM_path = textBox19.Text;

            }
        }

        private void GBA_TV_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox18.Text = openFileDialog1.FileName;
                bootimages[0] = textBox18.Text;

            }
        }

        private void GBA_DRC_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox17.Text = openFileDialog1.FileName;
                bootimages[1] = textBox17.Text;

            }
        }

        private void GBA_ICON_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox16.Text = openFileDialog1.FileName;
                bootimages[2] = textBox16.Text;

            }
        }

        private void GBA_LOGO_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.JPG;*.TGA";
            openFileDialog1.FilterIndex = 1;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog1.FileName))
            {
                textBox15.Text = openFileDialog1.FileName;
                bootimages[3] = textBox15.Text;

            }
        }

        private void N64CSTMNFOLDERS_Tick(object sender, EventArgs e)
        {
            if (code == false || content == false || meta == false)
            {
                N64_BTN15.Enabled = false;
            }
            else
            {
                N64_BTN15.Enabled = true;
            }
            if (code == false)
            {
                N64CODEIMG.Image = Properties.Resources.X;
                N64CODE.Text = "No code folder";
                N64CODE.ForeColor = Color.Red;
            }
            else
            {
                N64CODEIMG.Image = Properties.Resources.yes;
                N64CODE.Text = "Code folder found";
                N64CODE.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (content == false)
            {
                N64CONTENTIMG.Image = Properties.Resources.X;
                N64CONTENT.Text = "No content folder";
                N64CONTENT.ForeColor = Color.Red;
            }
            else
            {
                N64CONTENTIMG.Image = Properties.Resources.yes;
                N64CONTENT.Text = "Content folder found";
                N64CONTENT.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (meta == false)
            {
                N64METAIMG.Image = Properties.Resources.X;
                N64META.Text = "No meta folder";
                N64META.ForeColor = Color.Red;
            }
            else
            {
                N64METAIMG.Image = Properties.Resources.yes;
                N64META.Text = "Meta folder found";
                N64META.ForeColor = Color.FromArgb(0, 127, 14);
            }
        }

        private void N64CSTMN_PATH_BTN_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                N64CSTM_PATH.Text = folderBrowserDialog1.SelectedPath;
                CSTMBaseRom_path = N64CSTM_PATH.Text;
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

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            N64BRinfopanel(index);
        }

        private void PMEU_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void PMEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PMEU == "")
            {
                PMEUTKIMG.Image = Properties.Resources.X;
                PMEUTK.Text = "No TitleKey stored";
                PMEUTK.ForeColor = Color.Red;
            }
            else
            {
                PMEUTKIMG.Image = Properties.Resources.yes;
                PMEUTK.Text = "TitleKey found";
                PMEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/PMEU"))
            {
                PMEUBASEIMG.Image = Properties.Resources.X;
                PMEUBASE.Text = "Base not downloaded";
                PMEUBASE.ForeColor = Color.Red;
                N64_BTN15.Enabled = false;
            }
            else
            {
                PMEUBASEIMG.Image = Properties.Resources.yes;
                PMEUBASE.Text = "Base downloaded";
                PMEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                PMEU_DWNLD.Visible = false;
                N64_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.PMEU == "")
            {
                PMEU_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    PMEU_DWNLD.Enabled = false;
                }
                else
                {
                    PMEU_DWNLD.Cursor = Cursors.Default;
                    PMEU_DWNLD.Enabled = true;
                }


            }
        }

        private void PMUS_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void PMUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PMUS == "")
            {
                PMUSTKIMG.Image = Properties.Resources.X;
                PMUSTK.Text = "No TitleKey stored";
                PMUSTK.ForeColor = Color.Red;
            }
            else
            {
                PMUSTKIMG.Image = Properties.Resources.yes;
                PMUSTK.Text = "TitleKey found";
                PMUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/PMUS"))
            {
                PMUSBASEIMG.Image = Properties.Resources.X;
                PMUSBASE.Text = "Base not downloaded";
                PMUSBASE.ForeColor = Color.Red;
                N64_BTN15.Enabled = false;
            }
            else
            {
                PMUSBASEIMG.Image = Properties.Resources.yes;
                PMUSBASE.Text = "Base downloaded";
                PMUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                PMUS_DWNLD.Visible = false;
                N64_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.PMUS == "")
            {
                PMUS_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    PMUS_DWNLD.Enabled = false;
                }
                else
                {
                    PMUS_DWNLD.Cursor = Cursors.Default;
                    PMUS_DWNLD.Enabled = true;
                }


            }
        }

        private void FZXUS_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void FZXUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FZXUS == "")
            {
                FZXUSTKIMG.Image = Properties.Resources.X;
                FZXUSTK.Text = "No TitleKey stored";
                FZXUSTK.ForeColor = Color.Red;
            }
            else
            {
                FZXUSTKIMG.Image = Properties.Resources.yes;
                FZXUSTK.Text = "TitleKey found";
                FZXUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/FZXUS"))
            {
                FZXUSBASEIMG.Image = Properties.Resources.X;
                FZXUSBASE.Text = "Base not downloaded";
                FZXUSBASE.ForeColor = Color.Red;
                N64_BTN15.Enabled = false;
            }
            else
            {
                FZXUSBASEIMG.Image = Properties.Resources.yes;
                FZXUSBASE.Text = "Base downloaded";
                FZXUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                FZXUS_DWNLD.Visible = false;
                N64_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.FZXUS == "")
            {
                FZXUS_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    FZXUS_DWNLD.Enabled = false;
                }
                else
                {
                    FZXUS_DWNLD.Cursor = Cursors.Default;
                    FZXUS_DWNLD.Enabled = true;
                }


            }
        }

        private void FZXJP_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void FZXJP_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.FZXJP == "")
            {
                FZXJPTKIMG.Image = Properties.Resources.X;
                FZXJPTK.Text = "No TitleKey stored";
                FZXJPTK.ForeColor = Color.Red;
            }
            else
            {
                FZXJPTKIMG.Image = Properties.Resources.yes;
                FZXJPTK.Text = "TitleKey found";
                FZXJPTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/FZXJP"))
            {
                FZXJPBASEIMG.Image = Properties.Resources.X;
                FZXJPBASE.Text = "Base not downloaded";
                FZXJPBASE.ForeColor = Color.Red;
                N64_BTN15.Enabled = false;
            }
            else
            {
                FZXJPBASEIMG.Image = Properties.Resources.yes;
                FZXJPBASE.Text = "Base downloaded";
                FZXJPBASE.ForeColor = Color.FromArgb(0, 127, 14);
                FZXJP_DWNLD.Visible = false;
                N64_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.FZXJP == "")
            {
                FZXJP_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    FZXJP_DWNLD.Enabled = false;
                }
                else
                {
                    FZXJP_DWNLD.Cursor = Cursors.Default;
                    FZXJP_DWNLD.Enabled = true;
                }


            }
        }

        private void DK64EU_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void DK64EU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DK64EU == "")
            {
                DK64EUTKIMG.Image = Properties.Resources.X;
                DK64EUTK.Text = "No TitleKey stored";
                DK64EUTK.ForeColor = Color.Red;
            }
            else
            {
                DK64EUTKIMG.Image = Properties.Resources.yes;
                DK64EUTK.Text = "TitleKey found";
                DK64EUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/DK64EU"))
            {
                DK64EUBASEIMG.Image = Properties.Resources.X;
                DK64EUBASE.Text = "Base not downloaded";
                DK64EUBASE.ForeColor = Color.Red;
                N64_BTN15.Enabled = false;
            }
            else
            {
                DK64EUBASEIMG.Image = Properties.Resources.yes;
                DK64EUBASE.Text = "Base downloaded";
                DK64EUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                DK64EU_DWNLD.Visible = false;
                N64_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.DK64EU == "")
            {
                DK64EU_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    DK64EU_DWNLD.Enabled = false;
                }
                else
                {
                    DK64EU_DWNLD.Cursor = Cursors.Default;
                    DK64EU_DWNLD.Enabled = true;
                }


            }
        }

        private void DK64US_DWNLD_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void DK64US_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DK64US == "")
            {
                DK64USTKIMG.Image = Properties.Resources.X;
                DK64USTK.Text = "No TitleKey stored";
                DK64USTK.ForeColor = Color.Red;
            }
            else
            {
                DK64USTKIMG.Image = Properties.Resources.yes;
                DK64USTK.Text = "TitleKey found";
                DK64USTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/DK64US"))
            {
                DK64USBASEIMG.Image = Properties.Resources.X;
                DK64USBASE.Text = "Base not downloaded";
                DK64USBASE.ForeColor = Color.Red;
                N64_BTN15.Enabled = false;
            }
            else
            {
                DK64USBASEIMG.Image = Properties.Resources.yes;
                DK64USBASE.Text = "Base downloaded";
                DK64USBASE.ForeColor = Color.FromArgb(0, 127, 14);
                DK64US_DWNLD.Visible = false;
                N64_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.DK64US == "")
            {
                DK64US_DWNLD.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    DK64US_DWNLD.Enabled = false;
                }
                else
                {
                    DK64US_DWNLD.Cursor = Cursors.Default;
                    DK64US_DWNLD.Enabled = true;
                }


            }
        }

        private void NDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form tkey = new TitleKeyMenu(0); // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            tkey.Show();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox3.SelectedIndex;
            NDSBRinfopanel(index);
        }
        private void NDSBRinfopanel ( int b)
        {
            if(b == 0)
            {
                BaseROM = "Custom";
                #region Panels
                NDSCSTMN_PANEL.Visible = true;
                ZSTEU_PANEL.Visible = false;
                ZSTUS_PANEL.Visible = false;
                ZPHEU_PANEL.Visible = false;
                ZPHUS_PANEL.Visible = false;
                WWEU_PANEL.Visible = false;
                WWUS_PANEL.Visible = false;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = true;
                ZSTEU.Enabled = false;
                ZSTUS.Enabled = false;
                ZPHEU.Enabled = false;
                ZPHUS.Enabled = false;
                WWEU.Enabled = false;
                WWUS.Enabled = false;
                #endregion
            }
            if (b == 1)
            {
                BaseROM = "ZSTEU";
                #region Panels
                NDSCSTMN_PANEL.Visible = false;
                ZSTEU_PANEL.Visible = true;
                ZSTUS_PANEL.Visible = false;
                ZPHEU_PANEL.Visible = false;
                ZPHUS_PANEL.Visible = false;
                WWEU_PANEL.Visible = false;
                WWUS_PANEL.Visible = false;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = false;
                ZSTEU.Enabled = true;
                ZSTUS.Enabled = false;
                ZPHEU.Enabled = false;
                ZPHUS.Enabled = false;
                WWEU.Enabled = false;
                WWUS.Enabled = false;
                #endregion
            }
            if (b == 2)
            {
                BaseROM = "ZSTUS";
                #region Panels
                NDSCSTMN_PANEL.Visible = false;
                ZSTEU_PANEL.Visible = false;
                ZSTUS_PANEL.Visible = true;
                ZPHEU_PANEL.Visible = false;
                ZPHUS_PANEL.Visible = false;
                WWEU_PANEL.Visible = false;
                WWUS_PANEL.Visible = false;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = false;
                ZSTEU.Enabled = false;
                ZSTUS.Enabled = true;
                ZPHEU.Enabled = false;
                ZPHUS.Enabled = false;
                WWEU.Enabled = false;
                WWUS.Enabled = false;
                #endregion
            }
            if (b == 3)
            {
                BaseROM = "ZPHEU";
                #region Panels
                NDSCSTMN_PANEL.Visible = false;
                ZSTEU_PANEL.Visible = false;
                ZSTUS_PANEL.Visible = false;
                ZPHEU_PANEL.Visible = true;
                ZPHUS_PANEL.Visible = false;
                WWEU_PANEL.Visible = false;
                WWUS_PANEL.Visible = false;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = false;
                ZSTEU.Enabled = false;
                ZSTUS.Enabled = false;
                ZPHEU.Enabled = true;
                ZPHUS.Enabled = false;
                WWEU.Enabled = false;
                WWUS.Enabled = false;
                #endregion
            }
            if (b == 4)
            {
                BaseROM = "ZPHUS";
                #region Panels
                NDSCSTMN_PANEL.Visible = false;
                ZSTEU_PANEL.Visible = false;
                ZSTUS_PANEL.Visible = false;
                ZPHEU_PANEL.Visible = false;
                ZPHUS_PANEL.Visible = true;
                WWEU_PANEL.Visible = false;
                WWUS_PANEL.Visible = false;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = false;
                ZSTEU.Enabled = false;
                ZSTUS.Enabled = false;
                ZPHEU.Enabled = false;
                ZPHUS.Enabled = true;
                WWEU.Enabled = false;
                WWUS.Enabled = false;
                #endregion
            }
            if (b == 5)
            {
                BaseROM = "WWEU";
                #region Panels
                NDSCSTMN_PANEL.Visible = false;
                ZSTEU_PANEL.Visible = false;
                ZSTUS_PANEL.Visible = false;
                ZPHEU_PANEL.Visible = false;
                ZPHUS_PANEL.Visible = false;
                WWEU_PANEL.Visible = true;
                WWUS_PANEL.Visible = false;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = false;
                ZSTEU.Enabled = false;
                ZSTUS.Enabled = false;
                ZPHEU.Enabled = false;
                ZPHUS.Enabled = false;
                WWEU.Enabled = true;
                WWUS.Enabled = false;
                #endregion
            }
            if (b == 6)
            {
                BaseROM = "WWUS";
                #region Panels
                NDSCSTMN_PANEL.Visible = false;
                ZSTEU_PANEL.Visible = false;
                ZSTUS_PANEL.Visible = false;
                ZPHEU_PANEL.Visible = false;
                ZPHUS_PANEL.Visible = false;
                WWEU_PANEL.Visible = false;
                WWUS_PANEL.Visible = true;

                #endregion
                #region Timers
                NDSCSTMNFOLDERS.Enabled = false;
                ZSTEU.Enabled = false;
                ZSTUS.Enabled = false;
                ZPHEU.Enabled = false;
                ZPHUS.Enabled = false;
                WWEU.Enabled = false;
                WWUS.Enabled = true;
                #endregion
            }
        }
        private void NESBRinfopanel(int b)
        {
            if (b == 0)
            {
                BaseROM = "Custom";
                #region Panels
                NESCSTMN_PANEL.Visible = true;
                POEU_PANEL.Visible = false;
                POUS_PANEL.Visible = false;
                SMBEU_PANEL.Visible = false;
                SMBUS_PANEL.Visible = false;
                

                #endregion
                #region Timers
                NESCSTMNFOLDERS.Enabled = true;
                POEU.Enabled = false;
                POUS.Enabled = false;
                SMBEU.Enabled = false;
                SMBUS.Enabled = false;
                
                #endregion
            }
            if (b == 1)
            {
                BaseROM = "POEU";
                #region Panels
                NESCSTMN_PANEL.Visible = false;
                POEU_PANEL.Visible = true;
                POUS_PANEL.Visible = false;
                SMBEU_PANEL.Visible = false;
                SMBUS_PANEL.Visible = false;


                #endregion
                #region Timers
                NESCSTMNFOLDERS.Enabled = false;
                POEU.Enabled = true;
                POUS.Enabled = false;
                SMBEU.Enabled = false;
                SMBUS.Enabled = false;

                #endregion
            }
            if (b == 2)
            {
                BaseROM = "POUS";
                #region Panels
                NESCSTMN_PANEL.Visible = false;
                POEU_PANEL.Visible = false;
                POUS_PANEL.Visible = true;
                SMBEU_PANEL.Visible = false;
                SMBUS_PANEL.Visible = false;


                #endregion
                #region Timers
                NESCSTMNFOLDERS.Enabled = false;
                POEU.Enabled = false;
                POUS.Enabled = true;
                SMBEU.Enabled = false;
                SMBUS.Enabled = false;

                #endregion
            }
            if (b == 3)
            {
                BaseROM = "SMBEU";
                #region Panels
                NESCSTMN_PANEL.Visible = false;
                POEU_PANEL.Visible = false;
                POUS_PANEL.Visible = false;
                SMBEU_PANEL.Visible = true;
                SMBUS_PANEL.Visible = false;


                #endregion
                #region Timers
                NESCSTMNFOLDERS.Enabled = false;
                POEU.Enabled = false;
                POUS.Enabled = false;
                SMBEU.Enabled = true;
                SMBUS.Enabled = false;

                #endregion
            }
            if (b == 4)
            {
                BaseROM = "SMBUS";
                #region Panels
                NESCSTMN_PANEL.Visible = false;
                POEU_PANEL.Visible = false;
                POUS_PANEL.Visible = false;
                SMBEU_PANEL.Visible = false;
                SMBUS_PANEL.Visible = true;


                #endregion
                #region Timers
                NESCSTMNFOLDERS.Enabled = false;
                POEU.Enabled = false;
                POUS.Enabled = false;
                SMBEU.Enabled = false;
                SMBUS.Enabled = true;

                #endregion
            }
            
        }
        private void NDSCSTMN_BTN_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                NDSCSTMN_PATH.Text = folderBrowserDialog1.SelectedPath;
                CSTMBaseRom_path = NDSCSTMN_PATH.Text;
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

        private void NDSCSTMNFOLDERS_Tick(object sender, EventArgs e)
        {
            if (code == false || content == false || meta == false)
            {
                NDS_BTN15.Enabled = false;
            }
            else
            {
                NDS_BTN15.Enabled = true;
            }
            if (code == false)
            {
                NDSCODEIMG.Image = Properties.Resources.X;
                NDSCODE.Text = "No code folder";
                NDSCODE.ForeColor = Color.Red;
            }
            else
            {
                NDSCODEIMG.Image = Properties.Resources.yes;
                NDSCODE.Text = "Code folder found";
                NDSCODE.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (content == false)
            {
                NDSCONTENTIMG.Image = Properties.Resources.X;
                NDSCONTENT.Text = "No content folder";
                NDSCONTENT.ForeColor = Color.Red;
            }
            else
            {
                NDSCONTENTIMG.Image = Properties.Resources.yes;
                NDSCONTENT.Text = "Content folder found";
                NDSCONTENT.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (meta == false)
            {
                NDSMETAIMG.Image = Properties.Resources.X;
                NDSMETA.Text = "No meta folder";
                NDSMETA.ForeColor = Color.Red;
            }
            else
            {
                NDSMETAIMG.Image = Properties.Resources.yes;
                NDSMETA.Text = "Meta folder found";
                NDSMETA.ForeColor = Color.FromArgb(0, 127, 14);
            }
        }

        private void ZSTEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void ZSTEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ZSTEU == "")
            {
                ZSTEUTKIMG.Image = Properties.Resources.X;
                ZSTEUTK.Text = "No TitleKey stored";
                ZSTEUTK.ForeColor = Color.Red;
            }
            else
            {
                ZSTEUTKIMG.Image = Properties.Resources.yes;
                ZSTEUTK.Text = "TitleKey found";
                ZSTEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/ZSTEU"))
            {
                ZSTEUBASEIMG.Image = Properties.Resources.X;
                ZSTEUBASE.Text = "Base not downloaded";
                ZSTEUBASE.ForeColor = Color.Red;
                NDS_BTN15.Enabled = false;
            }
            else
            {
                ZSTEUBASEIMG.Image = Properties.Resources.yes;
                ZSTEUBASE.Text = "Base downloaded";
                ZSTEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                ZSTEU_BTN.Visible = false;
                NDS_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.ZSTEU == "")
            {
                ZSTEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    ZSTEU_BTN.Enabled = false;
                }
                else
                {
                    ZSTEU_BTN.Cursor = Cursors.Default;
                    ZSTEU_BTN.Enabled = true;
                }


            }
        }

        private void ZSTUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ZSTUS == "")
            {
                ZSTUSTKIMG.Image = Properties.Resources.X;
                ZSTUSTK.Text = "No TitleKey stored";
                ZSTUSTK.ForeColor = Color.Red;
            }
            else
            {
                ZSTUSTKIMG.Image = Properties.Resources.yes;
                ZSTUSTK.Text = "TitleKey found";
                ZSTUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/ZSTUS"))
            {
                ZSTUSBASEIMG.Image = Properties.Resources.X;
                ZSTUSBASE.Text = "Base not downloaded";
                ZSTUSBASE.ForeColor = Color.Red;
                NDS_BTN15.Enabled = false;
            }
            else
            {
                ZSTUSBASEIMG.Image = Properties.Resources.yes;
                ZSTUSBASE.Text = "Base downloaded";
                ZSTUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                ZSTUS_BTN.Visible = false;
                NDS_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.ZSTUS == "")
            {
                ZSTUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    ZSTUS_BTN.Enabled = false;
                }
                else
                {
                    ZSTUS_BTN.Cursor = Cursors.Default;
                    ZSTUS_BTN.Enabled = true;
                }


            }
        }

        private void ZSTUS_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void ZPHEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void ZPHEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ZPHEU == "")
            {
                ZPHEUTKIMG.Image = Properties.Resources.X;
                ZPHEUTK.Text = "No TitleKey stored";
                ZPHEUTK.ForeColor = Color.Red;
            }
            else
            {
                ZPHEUTKIMG.Image = Properties.Resources.yes;
                ZPHEUTK.Text = "TitleKey found";
                ZPHEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/ZPHEU"))
            {
                ZPHEUBASEIMG.Image = Properties.Resources.X;
                ZPHEUBASE.Text = "Base not downloaded";
                ZPHEUBASE.ForeColor = Color.Red;
                NDS_BTN15.Enabled = false;
            }
            else
            {
                ZPHEUBASEIMG.Image = Properties.Resources.yes;
                ZPHEUBASE.Text = "Base downloaded";
                ZPHEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                ZPHEU_BTN.Visible = false;
                NDS_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.ZPHEU == "")
            {
                ZPHEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    ZPHEU_BTN.Enabled = false;
                }
                else
                {
                    ZPHEU_BTN.Cursor = Cursors.Default;
                    ZPHEU_BTN.Enabled = true;
                }


            }
        }

        private void ZPHUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ZPHUS == "")
            {
                ZPHUSTKIMG.Image = Properties.Resources.X;
                ZPHUSTK.Text = "No TitleKey stored";
                ZPHUSTK.ForeColor = Color.Red;
            }
            else
            {
                ZPHUSTKIMG.Image = Properties.Resources.yes;
                ZPHUSTK.Text = "TitleKey found";
                ZPHUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/ZPHUS"))
            {
                ZPHUSBASEIMG.Image = Properties.Resources.X;
                ZPHUSBASE.Text = "Base not downloaded";
                ZPHUSBASE.ForeColor = Color.Red;
                NDS_BTN15.Enabled = false;
            }
            else
            {
                ZPHUSBASEIMG.Image = Properties.Resources.yes;
                ZPHUSBASE.Text = "Base downloaded";
                ZPHUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                ZPHUS_BTN.Visible = false;
                NDS_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.ZPHUS == "")
            {
                ZPHUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    ZPHUS_BTN.Enabled = false;
                }
                else
                {
                    ZPHUS_BTN.Cursor = Cursors.Default;
                    ZPHUS_BTN.Enabled = true;
                }


            }
        }

        private void ZPHUS_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void WWEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void WWEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WWEU == "")
            {
                WWEUTKIMG.Image = Properties.Resources.X;
                WWEUTK.Text = "No TitleKey stored";
                WWEUTK.ForeColor = Color.Red;
            }
            else
            {
                WWEUTKIMG.Image = Properties.Resources.yes;
                WWEUTK.Text = "TitleKey found";
                WWEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/WWEU"))
            {
                WWEUBASEIMG.Image = Properties.Resources.X;
                WWEUBASE.Text = "Base not downloaded";
                WWEUBASE.ForeColor = Color.Red;
                NDS_BTN15.Enabled = false;
            }
            else
            {
                WWEUBASEIMG.Image = Properties.Resources.yes;
                WWEUBASE.Text = "Base downloaded";
                WWEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                WWEU_BTN.Visible = false;
                NDS_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.WWEU == "")
            {
                WWEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    WWEU_BTN.Enabled = false;
                }
                else
                {
                    WWEU_BTN.Cursor = Cursors.Default;
                    WWEU_BTN.Enabled = true;
                }


            }
        }

        private void WWUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.WWUS == "")
            {
                WWUSTKIMG.Image = Properties.Resources.X;
                WWUSTK.Text = "No TitleKey stored";
                WWUSTK.ForeColor = Color.Red;
            }
            else
            {
                WWUSTKIMG.Image = Properties.Resources.yes;
                WWUSTK.Text = "TitleKey found";
                WWUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/WWUS"))
            {
                WWUSBASEIMG.Image = Properties.Resources.X;
                WWUSBASE.Text = "Base not downloaded";
                WWUSBASE.ForeColor = Color.Red;
                NDS_BTN15.Enabled = false;
            }
            else
            {
                WWUSBASEIMG.Image = Properties.Resources.yes;
                WWUSBASE.Text = "Base downloaded";
                WWUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                WWUS_BTN.Visible = false;
                NDS_BTN15.Enabled = true;
            }
            if (Properties.Settings.Default.WWUS == "")
            {
                WWUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    WWUS_BTN.Enabled = false;
                }
                else
                {
                    WWUS_BTN.Cursor = Cursors.Default;
                    WWUS_BTN.Enabled = true;
                }


            }
        }

        private void WWUS_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void NESToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form tkey = new TitleKeyMenu(3); // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            tkey.Show();
        }

        private void NESCSTMN_BTN_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                NESCSTMN_PATH.Text = folderBrowserDialog1.SelectedPath;
                CSTMBaseRom_path = NESCSTMN_PATH.Text;
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

        private void NESCSTMNFOLDERS_Tick(object sender, EventArgs e)
        {
            if (code == false || content == false || meta == false)
            {
                NES_INJCT.Enabled = false;
            }
            else
            {
                NES_INJCT.Enabled = true;
            }
            if (code == false)
            {
                NESCODEIMG.Image = Properties.Resources.X;
                NESCODE.Text = "No code folder";
                NESCODE.ForeColor = Color.Red;
            }
            else
            {
                NESCODEIMG.Image = Properties.Resources.yes;
                NESCODE.Text = "Code folder found";
                NESCODE.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (content == false)
            {
                NESCONTENTIMG.Image = Properties.Resources.X;
                NESCONTENT.Text = "No content folder";
                NESCONTENT.ForeColor = Color.Red;
            }
            else
            {
                NESCONTENTIMG.Image = Properties.Resources.yes;
                NESCONTENT.Text = "Content folder found";
                NESCONTENT.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (meta == false)
            {
                NESMETAIMG.Image = Properties.Resources.X;
                NESMETA.Text = "No meta folder";
                NESMETA.ForeColor = Color.Red;
            }
            else
            {
                NESMETAIMG.Image = Properties.Resources.yes;
                NESMETA.Text = "Meta folder found";
                NESMETA.ForeColor = Color.FromArgb(0, 127, 14);
            }
        }

        private void POEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void POEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.POEU == "")
            {
                POEUTKIMG.Image = Properties.Resources.X;
                POEUTK.Text = "No TitleKey stored";
                POEUTK.ForeColor = Color.Red;
            }
            else
            {
                POEUTKIMG.Image = Properties.Resources.yes;
                POEUTK.Text = "TitleKey found";
                POEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/POEU"))
            {
                POEUBASEIMG.Image = Properties.Resources.X;
                POEUBASE.Text = "Base not downloaded";
                POEUBASE.ForeColor = Color.Red;
                NES_INJCT.Enabled = false;
            }
            else
            {
                POEUBASEIMG.Image = Properties.Resources.yes;
                POEUBASE.Text = "Base downloaded";
                POEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                POEU_BTN.Visible = false;
                NES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.POEU == "")
            {
                POEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    POEU_BTN.Enabled = false;
                }
                else
                {
                    POEU_BTN.Cursor = Cursors.Default;
                    POEU_BTN.Enabled = true;
                }


            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox4.SelectedIndex;
            NESBRinfopanel(index);
        }

        private void POUS_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void POUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.POUS == "")
            {
                POUSTKIMG.Image = Properties.Resources.X;
                POUSTK.Text = "No TitleKey stored";
                POUSTK.ForeColor = Color.Red;
            }
            else
            {
                POUSTKIMG.Image = Properties.Resources.yes;
                POUSTK.Text = "TitleKey found";
                POUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/POUS"))
            {
                POUSBASEIMG.Image = Properties.Resources.X;
                POUSBASE.Text = "Base not downloaded";
                POUSBASE.ForeColor = Color.Red;
                NES_INJCT.Enabled = false;
            }
            else
            {
                POUSBASEIMG.Image = Properties.Resources.yes;
                POUSBASE.Text = "Base downloaded";
                POUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                POUS_BTN.Visible = false;
                NES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.POUS == "")
            {
                POUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    POUS_BTN.Enabled = false;
                }
                else
                {
                    POUS_BTN.Cursor = Cursors.Default;
                    POUS_BTN.Enabled = true;
                }


            }
        }

        private void SMBEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SMBEU == "")
            {
                SMBEUTKIMG.Image = Properties.Resources.X;
                SMBEUTK.Text = "No TitleKey stored";
                SMBEUTK.ForeColor = Color.Red;
            }
            else
            {
                SMBEUTKIMG.Image = Properties.Resources.yes;
                SMBEUTK.Text = "TitleKey found";
                SMBEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/SMBEU"))
            {
                SMBEUBASEIMG.Image = Properties.Resources.X;
                SMBEUBASE.Text = "Base not downloaded";
                SMBEUBASE.ForeColor = Color.Red;
                NES_INJCT.Enabled = false;
            }
            else
            {
                SMBEUBASEIMG.Image = Properties.Resources.yes;
                SMBEUBASE.Text = "Base downloaded";
                SMBEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                SMBEU_BTN.Visible = false;
                NES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.SMBEU == "")
            {
                SMBEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    SMBEU_BTN.Enabled = false;
                }
                else
                {
                    SMBEU_BTN.Cursor = Cursors.Default;
                    SMBEU_BTN.Enabled = true;
                }


            }
        }

        private void SMBEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void SMBUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SMBUS == "")
            {
                SMBUSTKIMG.Image = Properties.Resources.X;
                SMBUSTK.Text = "No TitleKey stored";
                SMBUSTK.ForeColor = Color.Red;
            }
            else
            {
                SMBUSTKIMG.Image = Properties.Resources.yes;
                SMBUSTK.Text = "TitleKey found";
                SMBUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/SMBUS"))
            {
                SMBUSBASEIMG.Image = Properties.Resources.X;
                SMBUSBASE.Text = "Base not downloaded";
                SMBUSBASE.ForeColor = Color.Red;
                NES_INJCT.Enabled = false;
            }
            else
            {
                SMBUSBASEIMG.Image = Properties.Resources.yes;
                SMBUSBASE.Text = "Base downloaded";
                SMBUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                SMBUS_BTN.Visible = false;
                NES_INJCT.Enabled = true;
            }
            if (Properties.Settings.Default.SMBUS == "")
            {
                SMBUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    SMBUS_BTN.Enabled = false;
                }
                else
                {
                    SMBUS_BTN.Cursor = Cursors.Default;
                    SMBUS_BTN.Enabled = true;
                }


            }
        }

        private void NESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form tkey = new TitleKeyMenu(2); // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            tkey.Show();
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox5.SelectedIndex;
            GBABRinfopanel(index);
        }
        private void GBABRinfopanel(int b)
        {
            if (b == 0)
            {
                BaseROM = "Custom";
                #region Panels
                GBACSTMN_PANEL.Visible = true;
                ZMCEU_PANEL.Visible = false;
                ZMCUS_PANEL.Visible = false;
                MKCEU_PANEL.Visible = false;
                MKCUS_PANEL.Visible = false;


                #endregion
                #region Timers
                GBACSTMNFOLDERS.Enabled = true;
                ZMCEU.Enabled = false;
                ZMCUS.Enabled = false;
                MKCEU.Enabled = false;
                MKCUS.Enabled = false;

                #endregion
            }
            if (b == 1)
            {
                BaseROM = "ZMCEU";
                #region Panels
                GBACSTMN_PANEL.Visible = false;
                ZMCEU_PANEL.Visible = true;
                ZMCUS_PANEL.Visible = false;
                MKCEU_PANEL.Visible = false;
                MKCUS_PANEL.Visible = false;


                #endregion
                #region Timers
                GBACSTMNFOLDERS.Enabled = false;
                ZMCEU.Enabled = true;
                ZMCUS.Enabled = false;
                MKCEU.Enabled = false;
                MKCUS.Enabled = false;

                #endregion
            }
            if (b == 2)
            {
                BaseROM = "ZMCUS";
                #region Panels
                GBACSTMN_PANEL.Visible = false;
                ZMCEU_PANEL.Visible = false;
                ZMCUS_PANEL.Visible = true;
                MKCEU_PANEL.Visible = false;
                MKCUS_PANEL.Visible = false;


                #endregion
                #region Timers
                GBACSTMNFOLDERS.Enabled = false;
                ZMCEU.Enabled = false;
                ZMCUS.Enabled = true;
                MKCEU.Enabled = false;
                MKCUS.Enabled = false;

                #endregion
            }
            if (b == 3)
            {
                BaseROM = "MKCEU";
                #region Panels
                GBACSTMN_PANEL.Visible = false;
                ZMCEU_PANEL.Visible = false;
                ZMCUS_PANEL.Visible = false;
                MKCEU_PANEL.Visible = true;
                MKCUS_PANEL.Visible = false;


                #endregion
                #region Timers
                GBACSTMNFOLDERS.Enabled = false;
                ZMCEU.Enabled = false;
                ZMCUS.Enabled = false;
                MKCEU.Enabled = true;
                MKCUS.Enabled = false;

                #endregion
            }
            if (b == 4)
            {
                BaseROM = "MKCUS";
                #region Panels
                GBACSTMN_PANEL.Visible = false;
                ZMCEU_PANEL.Visible = false;
                ZMCUS_PANEL.Visible = false;
                MKCEU_PANEL.Visible = false;
                MKCUS_PANEL.Visible = true;


                #endregion
                #region Timers
                GBACSTMNFOLDERS.Enabled = false;
                ZMCEU.Enabled = false;
                ZMCUS.Enabled = false;
                MKCEU.Enabled = false;
                MKCUS.Enabled = true;

                #endregion
            }
        }

        private void GBACSTMN_BTN_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                GBACSTMN_PATH.Text = folderBrowserDialog1.SelectedPath;
                CSTMBaseRom_path = GBACSTMN_PATH.Text;
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

        private void GBACSTMNFOLDERS_Tick(object sender, EventArgs e)
        {
            if (code == false || content == false || meta == false)
            {
                GBA_INJECT.Enabled = false;
            }
            else
            {
                GBA_INJECT.Enabled = true;
            }
            if (code == false)
            {
                GBACODEIMG.Image = Properties.Resources.X;
                GBACODE.Text = "No code folder";
                GBACODE.ForeColor = Color.Red;
            }
            else
            {
                GBACODEIMG.Image = Properties.Resources.yes;
                GBACODE.Text = "Code folder found";
                GBACODE.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (content == false)
            {
                GBACONTENTIMG.Image = Properties.Resources.X;
                GBACONTENT.Text = "No content folder";
                GBACONTENT.ForeColor = Color.Red;
            }
            else
            {
                GBACONTENTIMG.Image = Properties.Resources.yes;
                GBACONTENT.Text = "Content folder found";
                GBACONTENT.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (meta == false)
            {
                GBAMETAIMG.Image = Properties.Resources.X;
                GBAMETA.Text = "No meta folder";
                GBAMETA.ForeColor = Color.Red;
            }
            else
            {
                GBAMETAIMG.Image = Properties.Resources.yes;
                GBAMETA.Text = "Meta folder found";
                GBAMETA.ForeColor = Color.FromArgb(0, 127, 14);
            }
        }

        private void ZMCEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void ZMCEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ZMCEU == "")
            {
                ZMCEUTKIMG.Image = Properties.Resources.X;
                ZMCEUTK.Text = "No TitleKey stored";
                ZMCEUTK.ForeColor = Color.Red;
            }
            else
            {
                ZMCEUTKIMG.Image = Properties.Resources.yes;
                ZMCEUTK.Text = "TitleKey found";
                ZMCEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/ZMCEU"))
            {
                ZMCEUBASEIMG.Image = Properties.Resources.X;
                ZMCEUBASE.Text = "Base not downloaded";
                ZMCEUBASE.ForeColor = Color.Red;
                GBA_INJECT.Enabled = false;
            }
            else
            {
                ZMCEUBASEIMG.Image = Properties.Resources.yes;
                ZMCEUBASE.Text = "Base downloaded";
                ZMCEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                ZMCEU_BTN.Visible = false;
                GBA_INJECT.Enabled = true;
            }
            if (Properties.Settings.Default.ZMCEU == "")
            {
                ZMCEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    ZMCEU_BTN.Enabled = false;
                }
                else
                {
                    ZMCEU_BTN.Cursor = Cursors.Default;
                    ZMCEU_BTN.Enabled = true;
                }


            }
        }

        private void ZMCUS_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void ZMCUS_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ZMCUS == "")
            {
                ZMCUSTKIMG.Image = Properties.Resources.X;
                ZMCUSTK.Text = "No TitleKey stored";
                ZMCUSTK.ForeColor = Color.Red;
            }
            else
            {
                ZMCUSTKIMG.Image = Properties.Resources.yes;
                ZMCUSTK.Text = "TitleKey found";
                ZMCUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/ZMCUS"))
            {
                ZMCUSBASEIMG.Image = Properties.Resources.X;
                ZMCUSBASE.Text = "Base not downloaded";
                ZMCUSBASE.ForeColor = Color.Red;
                GBA_INJECT.Enabled = false;
            }
            else
            {
                ZMCUSBASEIMG.Image = Properties.Resources.yes;
                ZMCUSBASE.Text = "Base downloaded";
                ZMCUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                ZMCUS_BTN.Visible = false;
                GBA_INJECT.Enabled = true;
            }
            if (Properties.Settings.Default.ZMCUS == "")
            {
                ZMCUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    ZMCUS_BTN.Enabled = false;
                }
                else
                {
                    ZMCUS_BTN.Cursor = Cursors.Default;
                    ZMCUS_BTN.Enabled = true;
                }


            }
        }

        private void MKCEU_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void MKCEU_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.MKCEU == "")
            {
                MKCEUTKIMG.Image = Properties.Resources.X;
                MKCEUTK.Text = "No TitleKey stored";
                MKCEUTK.ForeColor = Color.Red;
            }
            else
            {
                MKCEUTKIMG.Image = Properties.Resources.yes;
                MKCEUTK.Text = "TitleKey found";
                MKCEUTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/MKCEU"))
            {
                MKCEUBASEIMG.Image = Properties.Resources.X;
                MKCEUBASE.Text = "Base not downloaded";
                MKCEUBASE.ForeColor = Color.Red;
                GBA_INJECT.Enabled = false;
            }
            else
            {
                MKCEUBASEIMG.Image = Properties.Resources.yes;
                MKCEUBASE.Text = "Base downloaded";
                MKCEUBASE.ForeColor = Color.FromArgb(0, 127, 14);
                MKCEU_BTN.Visible = false;
                GBA_INJECT.Enabled = true;
            }
            if (Properties.Settings.Default.MKCEU == "")
            {
                MKCEU_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    MKCEU_BTN.Enabled = false;
                }
                else
                {
                    MKCEU_BTN.Cursor = Cursors.Default;
                    MKCEU_BTN.Enabled = true;
                }


            }
        }

        private void MKCUS_BTN_Click(object sender, EventArgs e)
        {
            Injection.download(BaseROM);
        }

        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.MKCUS == "")
            {
                MKCUSTKIMG.Image = Properties.Resources.X;
                MKCUSTK.Text = "No TitleKey stored";
                MKCUSTK.ForeColor = Color.Red;
            }
            else
            {
                MKCUSTKIMG.Image = Properties.Resources.yes;
                MKCUSTK.Text = "TitleKey found";
                MKCUSTK.ForeColor = Color.FromArgb(0, 127, 14);
            }
            if (!Directory.Exists(Properties.Settings.Default.BaseRomPath + "/MKCUS"))
            {
                MKCUSBASEIMG.Image = Properties.Resources.X;
                MKCUSBASE.Text = "Base not downloaded";
                MKCUSBASE.ForeColor = Color.Red;
                GBA_INJECT.Enabled = false;
            }
            else
            {
                MKCUSBASEIMG.Image = Properties.Resources.yes;
                MKCUSBASE.Text = "Base downloaded";
                MKCUSBASE.ForeColor = Color.FromArgb(0, 127, 14);
                MKCUS_BTN.Visible = false;
                GBA_INJECT.Enabled = true;
            }
            if (Properties.Settings.Default.MKCUS == "")
            {
                MKCUS_BTN.Enabled = false;
            }
            else
            {
                if (Properties.Settings.Default.CommonKey == "")
                {
                    MKCUS_BTN.Enabled = false;
                }
                else
                {
                    MKCUS_BTN.Cursor = Cursors.Default;
                    MKCUS_BTN.Enabled = true;
                }


            }
        }
    }
}
