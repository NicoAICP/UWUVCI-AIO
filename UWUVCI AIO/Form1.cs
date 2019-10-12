using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Microsoft.WindowsAPICodePack.Dialogs;
using UWUVCI_AIO.Properties;

namespace UWUVCI_AIO
{
    public partial class UWUVCI_AIO : Form
    {
        #region injector stuff
        #region N64
        private static string iniPath;
        #endregion
        #region ForAll
        private static bool code;
        private static bool content;
        private static bool meta;
        private static string BaseROM;
        private static string customBaseRomPath;
        private static string injectRomPath;
        private static readonly string[] bootimages = new string[4]; // 0 = TV, 1 = DRC, 2 = ICON, 3 = LOGO

        #endregion
        #endregion

        private static About about;

        public UWUVCI_AIO()
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/Hotbrawl20/testing/master/update.xml");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);

            InitializeComponent();
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.DarkMode)
            {
                EnableDarkMode();
            }

            Properties.Settings.Default.PropertyChanged += DefaultOnPropertyChanged;

            if (Properties.Settings.Default.CommonKey.GetHashCode() == 487391367)
            {
                CommonkeyToolStripMenuItem.Enabled = false;
                CommonkeyToolStripMenuItem.Text = Resources.CommonkeyAlreadySet;
            }

            if (!AllPathsSet())
            {
                DisableInjection();
            }
        }

        private static void ResetInput()
        {
            BaseROM = null;
            customBaseRomPath = null;
            code = false;
            content = false;
            meta = false;
            injectRomPath = null;
            iniPath = null;
            for (int i = 0; i < bootimages.Length; i++)
            {
                bootimages[i] = null;
            }
            Injection.Clean();
        }

        private static bool AllPathsSet()
        {
            return Properties.Settings.Default.BaseRomPath != "" && Properties.Settings.Default.InjectionPath != "";
        }

        private void DisableInjection()
        {
            MessageBox.Show(Resources.EnterAllPaths, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            NDSImage.Enabled = false;
            N64Image.Enabled = false;
            GBAImage.Enabled = false;
            NESImage.Enabled = false;
            SNESImage.Enabled = false;
            NewToolStripMenuItem.Enabled = false;
        }

        private void EnableInjection()
        {
            MessageBox.Show(Resources.AllPathsSet, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NDSImage.Enabled = true;
            N64Image.Enabled = true;
            GBAImage.Enabled = true;
            NESImage.Enabled = true;
            SNESImage.Enabled = true;
            NewToolStripMenuItem.Enabled = true;
        }

        private void EnableDarkMode()
        {
            splitter1.BackColor = Color.FromArgb(50, 50, 50);
            panel1.BackColor = Color.FromArgb(50, 50, 50);
            Main.BackColor = Color.FromArgb(60, 60, 60);
            Main.ForeColor = Color.WhiteSmoke;

            #region n64
            N64.BackColor = Color.FromArgb(60, 60, 60);
            N64.ForeColor = Color.WhiteSmoke;
            N64BaseGroupBox.ForeColor = Color.WhiteSmoke;
            N64PackingGroupBox.ForeColor = Color.WhiteSmoke;
            N64InjectionGroupBox.ForeColor = Color.WhiteSmoke;
            N64BackButton.ForeColor = Color.Black;
            N64RomButton.ForeColor = Color.Black;
            N64INIButton.ForeColor = Color.Black;
            N64BlankINIButton.ForeColor = Color.Black;
            N64TVButton.ForeColor = Color.Black;
            N64DRCButton.ForeColor = Color.Black;
            N64IconButton.ForeColor = Color.Black;
            N64LogoButton.ForeColor = Color.Black;
            N64InjectButton.ForeColor = Color.Black;
            N64LoadiineButton.ForeColor = Color.Black;
            N64InstallButton.ForeColor = Color.Black;
            N64CustomButton.ForeColor = Color.Black;
            N64BaseDownloadButton.ForeColor = Color.Black;
            #endregion
            #region nds
            NDSInjectionGroupBox.ForeColor = Color.WhiteSmoke;
            NDSBaseGroupBox.ForeColor = Color.WhiteSmoke;
            NDSPackingGroupBox.ForeColor = Color.WhiteSmoke;
            NDS.BackColor = Color.FromArgb(60, 60, 60);
            NDS.ForeColor = Color.WhiteSmoke;
            NDSBackButton.ForeColor = Color.Black;
            NDSRomButton.ForeColor = Color.Black;
            NDSTVButton.ForeColor = Color.Black;
            NDSDRCButton.ForeColor = Color.Black;
            NDSIconButton.ForeColor = Color.Black;
            NDSLogoButton.ForeColor = Color.Black;
            NDSInjectButton.ForeColor = Color.Black;
            NDSCustomButton.ForeColor = Color.Black;
            NDSLoadiineButton.ForeColor = Color.Black;
            NDSInstallButton.ForeColor = Color.Black;
            NDSBaseDownloadButton.ForeColor = Color.Black;
            #endregion
            #region gba
            GBAInjectionGroupBox.ForeColor = Color.WhiteSmoke;
            GBABaseGroupBox.ForeColor = Color.WhiteSmoke;
            GBAPackingGroupBox.ForeColor = Color.WhiteSmoke;
            GBA.BackColor = Color.FromArgb(60, 60, 60);
            GBA.ForeColor = Color.WhiteSmoke;
            GBABackButton.ForeColor = Color.Black;
            GBADRCButton.ForeColor = Color.Black;
            GBAIconButton.ForeColor = Color.Black;
            GBAInjectButton.ForeColor = Color.Black;
            GBAInstallButton.ForeColor = Color.Black;
            GBALoadiineButton.ForeColor = Color.Black;
            GBALogoButton.ForeColor = Color.Black;
            GBARomButton.ForeColor = Color.Black;
            GBATVButton.ForeColor = Color.Black;
            GBACustomButton.ForeColor = Color.Black;
            GBABaseDownloadButton.ForeColor = Color.Black;
            #endregion
            #region nes
            NESInjectionGroupBox.ForeColor = Color.WhiteSmoke;
            NESBaseGroupBox.ForeColor = Color.WhiteSmoke;
            NESPackingGroupBox.ForeColor = Color.WhiteSmoke;
            NES.BackColor = Color.FromArgb(60, 60, 60);
            NES.ForeColor = Color.WhiteSmoke;
            NESBackButton.ForeColor = Color.Black;
            NESDRCButton.ForeColor = Color.Black;
            NESIconButton.ForeColor = Color.Black;
            NESInjectButton.ForeColor = Color.Black;
            NESInstallButton.ForeColor = Color.Black;
            NESLoadiineButton.ForeColor = Color.Black;
            NESLogoButton.ForeColor = Color.Black;
            NESRomButton.ForeColor = Color.Black;
            NESTVButton.ForeColor = Color.Black;
            NESCustomButton.ForeColor = Color.Black;
            NESBaseDownloadButton.ForeColor = Color.Black;
            #endregion
            #region snes
            SNESInjectionGroupBox.ForeColor = Color.WhiteSmoke;
            SNESBaseGroupBox.ForeColor = Color.WhiteSmoke;
            SNESPackingGroupBox.ForeColor = Color.WhiteSmoke;
            SNES.BackColor = Color.FromArgb(60, 60, 60);
            SNES.ForeColor = Color.WhiteSmoke;
            SNESBackButton.ForeColor = Color.Black;
            SNESDRCButton.ForeColor = Color.Black;
            SNESIconButton.ForeColor = Color.Black;
            SNESInjectButton.ForeColor = Color.Black;
            SNESInstallButton.ForeColor = Color.Black;
            SNESLoadiineButton.ForeColor = Color.Black;
            SNESLogoButton.ForeColor = Color.Black;
            SNESRomButton.ForeColor = Color.Black;
            SNESTVButton.ForeColor = Color.Black;
            SNESCustomButton.ForeColor = Color.Black;
            SNESBaseDownloadButton.ForeColor = Color.Black;
            #endregion
            #region WII/GC TODO
            #endregion
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ResetInput();
            MainTabControl.SelectedIndex = 0;
        }

        private void N64ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (TitleKeyMenu titleKeyMenu = new TitleKeyMenu(1)) // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            {
                titleKeyMenu.ShowDialog();
            }
        }

        private void NDSImage_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
            LoadValuesFromTabPage();
        }

        private void N64Image_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
            LoadValuesFromTabPage();
        }

        private void GBAImage_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedIndex = 3;
            LoadValuesFromTabPage();
        }

        private void NESImage_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedIndex = 4;
            LoadValuesFromTabPage();
        }

        private void SNESImage_Click(object sender, EventArgs e)
        {
            MainTabControl.SelectedIndex = 5;
            LoadValuesFromTabPage();
        }

        private void NDSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetInput();
            MainTabControl.SelectedIndex = 1;
            ClearTabPage();
        }

        private void N64ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetInput();
            MainTabControl.SelectedIndex = 2;
            ClearTabPage();
        }

        private void GBAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetInput();
            MainTabControl.SelectedIndex = 3;
            ClearTabPage();
        }

        private void NESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetInput();
            MainTabControl.SelectedIndex = 4;
            ClearTabPage();
        }

        private void SNESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ResetInput();
            MainTabControl.SelectedIndex = 5;
            ClearTabPage();
        }

        private void SNESBaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SNESCustomPanel.Visible = false;
            SNESBasePanel.Visible = false;
            Panel selectedPanel = GetSelectedPanel();
            if (selectedPanel == null)
                return;

            selectedPanel.Visible = true;
            string gameName = GetSelectedGameName();
            if (gameName == null)
                FillCustomPanel(selectedPanel);
            else
                FillPanel(selectedPanel, gameName);

            CheckInjectButton();
        }

        private void SNESToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (TitleKeyMenu titleKeyMenu = new TitleKeyMenu(4)) // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            {
                titleKeyMenu.ShowDialog();
            }
        }

        private void SNESCustomButton_Click(object sender, EventArgs e)
        {
            customBaseRomPath = SNESCustomTextBox.Text = SelectRomFolder() ?? customBaseRomPath;
            FillCustomPanel(SNESCustomPanel);
            CheckInjectButton();
        }

        private void SNESRomButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "SNES roms |*.sfc;*.smc";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    injectRomPath = SNESRomTextBox.Text = fileDialog.FileName;
                    CheckInjectButton();
                }
            }
        }

        private void N64RomButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "N64 Roms |*.n64;*.v64;*.z64" ;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    injectRomPath = N64RomTextBox.Text = fileDialog.FileName;
                    CheckInjectButton();
                }
            }
        }

        #region UI buttons i guess
        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void InterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Settings settings = new Settings())
            {
                settings.ShowDialog();
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (about == null || about.IsDisposed)
            {
                about = new About();
                about.Show();
            }
            else
            {
                about.Activate();
            }
        }

        private void CommonkeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CKey ckey = new CKey())
            {
                ckey.ShowDialog();
            }
        }

        private void PathsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool allPathsWereSet = AllPathsSet();
            using (PathMenu pathMenu = new PathMenu())
            {
                pathMenu.ShowDialog();
            }

            if (AllPathsSet() && !allPathsWereSet)
            {
                EnableInjection();
            }
        }

        #endregion

        private void SNESTVButton_Click(object sender, EventArgs e)
        {
            bootimages[0] = SNESTVTextBox.Text = SelectPngOrTgaFile() ?? bootimages[0];
        }

        private void SNESDRCButton_Click(object sender, EventArgs e)
        {
            bootimages[1] = SNESDRCTextBox.Text = SelectPngOrTgaFile() ?? bootimages[1];
        }

        private void SNESIconButton_Click(object sender, EventArgs e)
        {
            bootimages[2] = SNESIconTextBox.Text = SelectPngOrTgaFile() ?? bootimages[2];
        }

        private void SNESLogoButton_Click(object sender, EventArgs e)
        {
            bootimages[3] = SNESLogoTextBox.Text = SelectPngOrTgaFile() ?? bootimages[3];
        }

        private void SNESInjectButton_Click(object sender, EventArgs e)
        {
            Injection.Inject(Injection.Console.SNES, BaseROM, customBaseRomPath, injectRomPath, bootimages, SNESGameNameTextBox.Text);
            SNESInstallButton.Enabled = true;
            SNESLoadiineButton.Enabled = true;
        }

        private void SNES_INST_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show(Resources.CommonkeyNeeded);
            }
            else
            {
                Injection.Packing(SNESGameNameTextBox.Text);
                SNESInstallButton.Enabled = false;
                SNESLoadiineButton.Enabled = false;
            }
        }

        private void SNESLoadiineButton_Click(object sender, EventArgs e)
        {
            Injection.Loadiine(SNESGameNameTextBox.Text);
            SNESInstallButton.Enabled = false;
            SNESLoadiineButton.Enabled = false;
        }

        private void UWUVCI_AIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            Injection.Clean();
        }

        private void SNESBaseDownloadButton_Click(object sender, EventArgs e)
        {
            Injection.Download(BaseROM);
            SNESBaseComboBox_SelectedIndexChanged(null, null);
        }

        private void N64BlankINIButton_Click(object sender, EventArgs e)
        {
            iniPath = "blank";
            N64INITextBox.Text = "Using blank INI";
        }

        private void N64TVButton_Click(object sender, EventArgs e)
        {
            bootimages[0] = N64TVTextBox.Text = SelectPngOrTgaFile() ?? bootimages[0];
        }

        private void N64DRCButton_Click(object sender, EventArgs e)
        {
            bootimages[1] = N64DRCTextBox.Text = SelectPngOrTgaFile() ?? bootimages[1];
        }

        private void N64IconButton_Click(object sender, EventArgs e)
        {
            bootimages[2] = N64IconTextBox.Text = SelectPngOrTgaFile() ?? bootimages[2];
        }

        private void N64LogoButton_Click(object sender, EventArgs e)
        {
            bootimages[3] = N64LogoTextBox.Text = SelectPngOrTgaFile() ?? bootimages[3];
        }

        private void N64INIButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Configuration Files |*.INI";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    iniPath = N64INITextBox.Text = fileDialog.FileName;
                }
            }
        }

        private void N64InjectButton_Click(object sender, EventArgs e)
        {
            Injection.Inject(Injection.Console.N64, BaseROM, customBaseRomPath, injectRomPath, bootimages, N64GameNameTextBox.Text, iniPath, !N64DarkFilterRadioButton1.Checked);
            N64InstallButton.Enabled = true;
            N64LoadiineButton.Enabled = true;
        }

        private void N64InstallButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show(Resources.CommonkeyNeeded);
            }
            else
            {
                Injection.Packing(N64GameNameTextBox.Text);
                N64InstallButton.Enabled = false;
                N64LoadiineButton.Enabled = false;
            }
        }

        private void N64LoadiineButton_Click(object sender, EventArgs e)
        {
            Injection.Loadiine(N64GameNameTextBox.Text);
            N64InstallButton.Enabled = false;
            N64LoadiineButton.Enabled = false;
        }

        private void NDSRomButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "NDS Roms |*.nds;*.srl";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    injectRomPath = NDSRomTextBox.Text = fileDialog.FileName;
                    CheckInjectButton();
                }
            }
        }

        private void NDSTVButton_Click(object sender, EventArgs e)
        {
            bootimages[0] = NDSTVTextBox.Text = SelectPngOrTgaFile() ?? bootimages[0];
        }

        private void NDSDRCButton_Click(object sender, EventArgs e)
        {
            bootimages[1] = NDSDRCTextBox.Text = SelectPngOrTgaFile() ?? bootimages[1];
        }

        private void NDSIconButton_Click(object sender, EventArgs e)
        {
            bootimages[2] = NDSIconTextBox.Text = SelectPngOrTgaFile() ?? bootimages[2];
        }

        private void NDSLogoButton_Click(object sender, EventArgs e)
        {
            bootimages[3] = NDSLogoTextBox.Text = SelectPngOrTgaFile() ?? bootimages[3];
        }

        private void NDSInjectButton_Click(object sender, EventArgs e)
        {
            Injection.Inject(Injection.Console.NDS, BaseROM, customBaseRomPath, injectRomPath, bootimages, NDSGameNameTextBox.Text);
            NDSInstallButton.Enabled = true;
            NDSLoadiineButton.Enabled = true;
        }

        private void NDSInstallButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show(Resources.CommonkeyNeeded);
            }
            else
            {
                Injection.Packing(NDSGameNameTextBox.Text);
                NDSInstallButton.Enabled = false;
                NDSLoadiineButton.Enabled = false;
            }
        }

        private void NDSLoadiineButton_Click(object sender, EventArgs e)
        {
            Injection.Loadiine(NDSGameNameTextBox.Text);
            NDSInstallButton.Enabled = false;
            NDSLoadiineButton.Enabled = false;
        }

        private void NESRomButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "NES Roms |*.nes";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    injectRomPath = NESRomTextBox.Text = fileDialog.FileName;
                    CheckInjectButton();
                }
            }
        }

        private void NESTVButton_Click(object sender, EventArgs e)
        {
            bootimages[0] = NESTVTextBox.Text = SelectPngOrTgaFile() ?? bootimages[0];
        }

        private void NESDRCButton_Click(object sender, EventArgs e)
        {
            bootimages[1] = NESDRCTextBox.Text = SelectPngOrTgaFile() ?? bootimages[1];
        }

        private void NESIconButton_Click(object sender, EventArgs e)
        {
            bootimages[2] = NESIconTextBox.Text = SelectPngOrTgaFile() ?? bootimages[2];
        }

        private void NESLogoButton_Click(object sender, EventArgs e)
        {
            bootimages[3] = NESLogoTextBox.Text = SelectPngOrTgaFile() ?? bootimages[3];
        }

        private void NESInjectButton_Click(object sender, EventArgs e)
        {
            Injection.Inject(Injection.Console.NES, BaseROM, customBaseRomPath, injectRomPath, bootimages, NESGameNameTextBox.Text);
            NESInstallButton.Enabled = true;
            NESLoadiineButton.Enabled = true;
        }

        private void NESInstallButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show(Resources.CommonkeyNeeded);
            }
            else
            {
                Injection.Packing(NESGameNameTextBox.Text);
                NESInstallButton.Enabled = false;
                NESLoadiineButton.Enabled = false;
            }
        }

        private void NESLoadiineButton_Click(object sender, EventArgs e)
        {
            NESInstallButton.Enabled = false;
            NESLoadiineButton.Enabled = false;
            Injection.Loadiine(NESGameNameTextBox.Text);
        }

        private void GBAInjectButton_Click(object sender, EventArgs e)
        {
            Injection.Inject(Injection.Console.GBA, BaseROM, customBaseRomPath, injectRomPath, bootimages, GBAGameNameTextBox.Text);
            GBAInstallButton.Enabled = true;
            GBALoadiineButton.Enabled = true;
        }

        private void GBAInstallButton_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CommonKey == "")
            {
                MessageBox.Show(Resources.CommonkeyNeeded);
            }
            else
            {
                Injection.Packing(GBAGameNameTextBox.Text);
                GBAInstallButton.Enabled = false;
                GBALoadiineButton.Enabled = false;
            }
        }

        private void GBALoadiineButton_Click(object sender, EventArgs e)
        {
            Injection.Loadiine(GBAGameNameTextBox.Text);
            GBAInstallButton.Enabled = false;
            GBALoadiineButton.Enabled = false;
        }

        private void GBARomButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "GBA Roms |*.gba";

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    injectRomPath = GBARomTextBox.Text = fileDialog.FileName;
                    CheckInjectButton();
                }
            }
        }

        private void GBATVButton_Click(object sender, EventArgs e)
        {
            bootimages[0] = GBATVTextBox.Text = SelectPngOrTgaFile() ?? bootimages[0];
        }

        private void GBADRCButton_Click(object sender, EventArgs e)
        {
            bootimages[1] = GBADRCTextBox.Text = SelectPngOrTgaFile() ?? bootimages[1];
        }

        private void GBAIconButton_Click(object sender, EventArgs e)
        {
            bootimages[2] = GBAIconTextBox.Text = SelectPngOrTgaFile() ?? bootimages[2];
        }

        private void GBALogoButton_Click(object sender, EventArgs e)
        {
            bootimages[3] = GBALogoTextBox.Text = SelectPngOrTgaFile() ?? bootimages[3];
        }

        private void N64CustomButton_Click(object sender, EventArgs e)
        {
            customBaseRomPath = N64CustomTextBox.Text = SelectRomFolder() ?? customBaseRomPath;
            FillCustomPanel(N64CustomPanel);
            CheckInjectButton();
        }

        private void N64BaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            N64CustomPanel.Visible = false;
            N64BasePanel.Visible = false;
            Panel selectedPanel = GetSelectedPanel();
            if (selectedPanel == null)
                return;

            selectedPanel.Visible = true;
            string gameName = GetSelectedGameName();
            if (gameName == null)
                FillCustomPanel(selectedPanel);
            else
                FillPanel(selectedPanel, gameName);

            CheckInjectButton();
        }

        private void N64BaseDownloadButton_Click(object sender, EventArgs e)
        {
            Injection.Download(BaseROM);
            N64BaseComboBox_SelectedIndexChanged(null, null);
        }

        private void NDSToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (TitleKeyMenu titleKeyMenu = new TitleKeyMenu(0)) // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            {
                titleKeyMenu.ShowDialog();
            }
        }

        private void NDSBaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NDSCustomPanel.Visible = false;
            NDSBasePanel.Visible = false;
            Panel selectedPanel = GetSelectedPanel();
            if (selectedPanel == null)
                return;

            selectedPanel.Visible = true;
            string gameName = GetSelectedGameName();
            if (gameName == null)
                FillCustomPanel(selectedPanel);
            else
                FillPanel(selectedPanel, gameName);

            CheckInjectButton();
        }

        private void NDSCustomButton_Click(object sender, EventArgs e)
        {
            customBaseRomPath = NDSCustomTextBox.Text = SelectRomFolder() ?? customBaseRomPath;
            FillCustomPanel(NDSCustomPanel);
            CheckInjectButton();
        }

        private void NDSBaseDownloadButton_Click(object sender, EventArgs e)
        {
            Injection.Download(BaseROM);
            NDSBaseComboBox_SelectedIndexChanged(null, null);
        }

        private void NESToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (TitleKeyMenu titleKeyMenu = new TitleKeyMenu(3)) // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            {
                titleKeyMenu.ShowDialog();
            }
        }

        private void NESCustomButton_Click(object sender, EventArgs e)
        {
            customBaseRomPath = NESCustomTextBox.Text = SelectRomFolder() ?? customBaseRomPath;
            FillCustomPanel(NESCustomPanel);
            CheckInjectButton();
        }

        private void NESBaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NESCustomPanel.Visible = false;
            NESBasePanel.Visible = false;
            Panel selectedPanel = GetSelectedPanel();
            if (selectedPanel == null)
                return;

            selectedPanel.Visible = true;
            string gameName = GetSelectedGameName();
            if (gameName == null)
                FillCustomPanel(selectedPanel);
            else
                FillPanel(selectedPanel, gameName);

            CheckInjectButton();
        }

        private void NESBaseDownloadButton_Click(object sender, EventArgs e)
        {
            Injection.Download(BaseROM);
            NESBaseComboBox_SelectedIndexChanged(null, null);
        }

        private void GBAToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (TitleKeyMenu titleKeyMenu = new TitleKeyMenu(2)) // 0 = NDS; 1 = N64, 2 = GBA, 3 = NES, 4 = SNES
            {
                titleKeyMenu.ShowDialog();
            }
        }

        private void GBABaseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GBACustomPanel.Visible = false;
            GBABasePanel.Visible = false;
            Panel selectedPanel = GetSelectedPanel();
            if (selectedPanel == null)
                return;

            selectedPanel.Visible = true;
            string gameName = GetSelectedGameName();
            if (gameName == null)
                FillCustomPanel(selectedPanel);
            else
                FillPanel(selectedPanel, gameName);

            CheckInjectButton();
        }

        private void GBACustomButton_Click(object sender, EventArgs e)
        {
            customBaseRomPath = GBACustomTextBox.Text = SelectRomFolder() ?? customBaseRomPath;
            FillCustomPanel(GBACustomPanel);
            CheckInjectButton();
        }

        private void GBABaseDownloadButton_Click(object sender, EventArgs e)
        {
            Injection.Download(BaseROM);
            GBABaseComboBox_SelectedIndexChanged(null, null);
        }

        private static string SelectPngOrTgaFile()
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Image Files |*.png;*.tga";

                return (fileDialog.ShowDialog() == DialogResult.OK) ? fileDialog.FileName : null;
            }
        }

        private static string SelectRomFolder()
        {
            using (CommonOpenFileDialog fileDialog = new CommonOpenFileDialog())
            {
                fileDialog.IsFolderPicker = true;

                return (fileDialog.ShowDialog() == CommonFileDialogResult.Ok) ? fileDialog.FileName : null;
            }
        }

        private void CheckInjectButton()
        {
            switch (MainTabControl.SelectedIndex)
            {
                case 1: // NDS
                    if (NDSBaseComboBox.SelectedIndex == 0)
                        NDSInjectButton.Enabled = code && content && meta && !string.IsNullOrEmpty(injectRomPath);
                    else
                        NDSInjectButton.Enabled = !NDSBaseDownloadButton.Visible && !string.IsNullOrEmpty(injectRomPath);
                    break;
                case 2: // N64
                    if (N64BaseComboBox.SelectedIndex == 0)
                        N64InjectButton.Enabled = code && content && meta && !string.IsNullOrEmpty(injectRomPath);
                    else
                        N64InjectButton.Enabled = !N64BaseDownloadButton.Visible && !string.IsNullOrEmpty(injectRomPath);
                    break;
                case 3: // GBA
                    if (GBABaseComboBox.SelectedIndex == 0)
                        GBAInjectButton.Enabled = code && content && meta && !string.IsNullOrEmpty(injectRomPath);
                    else
                        GBAInjectButton.Enabled = !GBABaseDownloadButton.Visible && !string.IsNullOrEmpty(injectRomPath);
                    break;
                case 4: // NES
                    if (NESBaseComboBox.SelectedIndex == 0)
                        NESInjectButton.Enabled = code && content && meta && !string.IsNullOrEmpty(injectRomPath);
                    else
                        NESInjectButton.Enabled = !NESBaseDownloadButton.Visible && !string.IsNullOrEmpty(injectRomPath);
                    break;
                case 5: // SNES
                    if (SNESBaseComboBox.SelectedIndex == 0)
                        SNESInjectButton.Enabled = code && content && meta && !string.IsNullOrEmpty(injectRomPath);
                    else
                        SNESInjectButton.Enabled = !SNESBaseDownloadButton.Visible && !string.IsNullOrEmpty(injectRomPath);
                    break;
            }
        }

        private static void FillPanel(Panel toFill, string gameName)
        {
            BaseROM = gameName;
            Label baseLabel = (Label) toFill.Controls[5];
            PictureBox baseImage = (PictureBox) toFill.Controls[6];
            Label commonKeyLabel = (Label) toFill.Controls[3];
            PictureBox commonKeyImage = (PictureBox) toFill.Controls[4];
            Label titleKeyLabel = (Label) toFill.Controls[1];
            PictureBox titleKeyImage = (PictureBox) toFill.Controls[2];
            Button downloadButton = (Button) toFill.Controls[0];

            if ((string) Properties.Settings.Default[gameName] == "")
            {
                titleKeyImage.Image = Resources.X;
                titleKeyLabel.Text = Resources.TitlekeyNotSet;
                titleKeyLabel.ForeColor = Color.Red;
            }
            else
            {
                titleKeyImage.Image = Resources.yes;
                titleKeyLabel.Text = Resources.TitlekeySet;
                titleKeyLabel.ForeColor = Color.FromArgb(0, 127, 14);
            }

            if (Properties.Settings.Default.CommonKey.Equals(""))
            {
                commonKeyImage.Image = Resources.X;
                commonKeyLabel.Text = Resources.CommonkeyNotSet;
                commonKeyLabel.ForeColor = Color.Red;
            }
            else
            {
                commonKeyImage.Image = Resources.yes;
                commonKeyLabel.Text = Resources.CommonkeySet;
                commonKeyLabel.ForeColor = Color.FromArgb(0, 127, 14);
            }

            downloadButton.Enabled = !Properties.Settings.Default.CommonKey.Equals("") && (string) Properties.Settings.Default[gameName] != "";

            if (Directory.Exists(Path.Combine(Properties.Settings.Default.BaseRomPath, gameName)))
            {
                baseImage.Image = Resources.yes;
                baseLabel.Text = Resources.BaseDownloaded;
                baseLabel.ForeColor = Color.FromArgb(0, 127, 14);
                downloadButton.Visible = false;
            }
            else
            {
                baseImage.Image = Resources.X;
                baseLabel.Text = Resources.BaseNotDownloaded;
                baseLabel.ForeColor = Color.Red;
                downloadButton.Visible = true;
            }
        }

        private static void FillCustomPanel(Panel toFill)
        {
            BaseROM = "Custom";
            Label codeLabel = (Label) toFill.Controls[4];
            PictureBox codeImage = (PictureBox) toFill.Controls[5];
            Label contentLabel = (Label) toFill.Controls[0];
            PictureBox contentImage = (PictureBox) toFill.Controls[1];
            Label metaLabel = (Label) toFill.Controls[2];
            PictureBox metaImage = (PictureBox) toFill.Controls[3];

            string directoryPath = toFill.Controls[8].Text;

            code = Directory.Exists(Path.Combine(directoryPath, "code"));
            if (code)
            {
                codeImage.Image = Resources.yes;
                codeLabel.Text = string.Format(Resources.FolderExists, "Code");
                codeLabel.ForeColor = Color.FromArgb(0, 127, 14);
            }
            else
            {
                codeImage.Image = Resources.X;
                codeLabel.Text = string.Format(Resources.FolderDoesNotExist, "Code");
                codeLabel.ForeColor = Color.Red;
            }

            content = Directory.Exists(Path.Combine(directoryPath, "content"));
            if (content)
            {
                contentImage.Image = Resources.yes;
                contentLabel.Text = string.Format(Resources.FolderExists, "Content");
                contentLabel.ForeColor = Color.FromArgb(0, 127, 14);
            }
            else
            {
                contentImage.Image = Resources.X;
                contentLabel.Text = string.Format(Resources.FolderDoesNotExist, "Content");
                contentLabel.ForeColor = Color.Red;
            }

            meta = Directory.Exists(Path.Combine(directoryPath, "meta"));
            if (meta)
            {
                metaImage.Image = Resources.yes;
                metaLabel.Text = string.Format(Resources.FolderExists, "Meta");
                metaLabel.ForeColor = Color.FromArgb(0, 127, 14);
            }
            else
            {
                metaImage.Image = Resources.X;
                metaLabel.Text = string.Format(Resources.FolderDoesNotExist, "Meta");
                metaLabel.ForeColor = Color.Red;
            }
        }

        private void LoadValuesFromTabPage()
        {
            switch (MainTabControl.SelectedIndex)
            {
                case 1: // NDS
                    NDSBaseComboBox_SelectedIndexChanged(null, null);
                    BaseROM = NDSRomTextBox.Text;
                    if (!string.IsNullOrEmpty(NDSTVTextBox.Text))
                        bootimages[0] = NDSTVTextBox.Text;
                    if (!string.IsNullOrEmpty(NDSDRCTextBox.Text))
                        bootimages[1] = NDSDRCTextBox.Text;
                    if (!string.IsNullOrEmpty(NDSLogoTextBox.Text))
                        bootimages[2] = NDSLogoTextBox.Text;
                    if (!string.IsNullOrEmpty(NDSIconTextBox.Text))
                        bootimages[3] = NDSIconTextBox.Text;
                    break;
                case 2: // N64
                    N64BaseComboBox_SelectedIndexChanged(null, null);
                    BaseROM = N64RomTextBox.Text;
                    if (!string.IsNullOrEmpty(N64INITextBox.Text))
                        iniPath = N64INITextBox.Text;
                    if (!string.IsNullOrEmpty(N64TVTextBox.Text))
                        bootimages[0] = N64TVTextBox.Text;
                    if (!string.IsNullOrEmpty(N64DRCTextBox.Text))
                        bootimages[1] = N64DRCTextBox.Text;
                    if (!string.IsNullOrEmpty(N64LogoTextBox.Text))
                        bootimages[2] = N64LogoTextBox.Text;
                    if (!string.IsNullOrEmpty(N64IconTextBox.Text))
                        bootimages[3] = N64IconTextBox.Text;
                    break;
                case 3: // GBA
                    GBABaseComboBox_SelectedIndexChanged(null, null);
                    BaseROM = GBARomTextBox.Text;
                    if (!string.IsNullOrEmpty(GBATVTextBox.Text))
                        bootimages[0] = GBATVTextBox.Text;
                    if (!string.IsNullOrEmpty(GBADRCTextBox.Text))
                        bootimages[1] = GBADRCTextBox.Text;
                    if (!string.IsNullOrEmpty(GBALogoTextBox.Text))
                        bootimages[2] = GBALogoTextBox.Text;
                    if (!string.IsNullOrEmpty(GBAIconTextBox.Text))
                        bootimages[3] = GBAIconTextBox.Text;
                    break;
                case 4: // NES
                    NESBaseComboBox_SelectedIndexChanged(null, null);
                    BaseROM = NESRomTextBox.Text;
                    if (!string.IsNullOrEmpty(NESTVTextBox.Text))
                        bootimages[0] = NESTVTextBox.Text;
                    if (!string.IsNullOrEmpty(NESDRCTextBox.Text))
                        bootimages[1] = NESDRCTextBox.Text;
                    if (!string.IsNullOrEmpty(NESLogoTextBox.Text))
                        bootimages[2] = NESLogoTextBox.Text;
                    if (!string.IsNullOrEmpty(NESIconTextBox.Text))
                        bootimages[3] = NESIconTextBox.Text;
                    break;
                case 5: // SNES
                    SNESBaseComboBox_SelectedIndexChanged(null, null);
                    BaseROM = SNESRomTextBox.Text;
                    if (!string.IsNullOrEmpty(SNESTVTextBox.Text))
                        bootimages[0] = SNESTVTextBox.Text;
                    if (!string.IsNullOrEmpty(SNESDRCTextBox.Text))
                        bootimages[1] = SNESDRCTextBox.Text;
                    if (!string.IsNullOrEmpty(SNESLogoTextBox.Text))
                        bootimages[2] = SNESLogoTextBox.Text;
                    if (!string.IsNullOrEmpty(SNESIconTextBox.Text))
                        bootimages[3] = SNESIconTextBox.Text;
                    break;
            }
        }

        private void ClearTabPage()
        {
            switch (MainTabControl.SelectedIndex)
            {
                case 1: // NDS
                    NDSBaseComboBox.SelectedIndex = -1;
                    NDSRomTextBox.Text = "";
                    NDSTVTextBox.Text = "";
                    NDSDRCTextBox.Text = "";
                    NDSLogoTextBox.Text = "";
                    NDSIconTextBox.Text = "";
                    NDSGameNameTextBox.Text = "";
                    NDSLoadiineButton.Enabled = false;
                    NDSInstallButton.Enabled = false;
                    break;
                case 2: // N64
                    N64BaseComboBox.SelectedIndex = -1;
                    N64RomTextBox.Text = "";
                    N64DarkFilterRadioButton1.Select();
                    N64INITextBox.Text = "";
                    N64TVTextBox.Text = "";
                    N64DRCTextBox.Text = "";
                    N64LogoTextBox.Text = "";
                    N64IconTextBox.Text = "";
                    N64GameNameTextBox.Text = "";
                    N64LoadiineButton.Enabled = false;
                    N64InstallButton.Enabled = false;
                    break;
                case 3: // GBA
                    GBABaseComboBox.SelectedIndex = -1;
                    GBARomTextBox.Text = "";
                    GBATVTextBox.Text = "";
                    GBADRCTextBox.Text = "";
                    GBALogoTextBox.Text = "";
                    GBAIconTextBox.Text = "";
                    GBAGameNameTextBox.Text = "";
                    GBALoadiineButton.Enabled = false;
                    GBAInstallButton.Enabled = false;
                    break;
                case 4: // NES
                    NESBaseComboBox.SelectedIndex = -1;
                    NESRomTextBox.Text = "";
                    NESTVTextBox.Text = "";
                    NESDRCTextBox.Text = "";
                    NESLogoTextBox.Text = "";
                    NESIconTextBox.Text = "";
                    NESGameNameTextBox.Text = "";
                    NESLoadiineButton.Enabled = false;
                    NESInstallButton.Enabled = false;
                    break;
                case 5: // SNES
                    SNESBaseComboBox.SelectedIndex = -1;
                    SNESRomTextBox.Text = "";
                    SNESTVTextBox.Text = "";
                    SNESDRCTextBox.Text = "";
                    SNESLogoTextBox.Text = "";
                    SNESIconTextBox.Text = "";
                    SNESGameNameTextBox.Text = "";
                    SNESLoadiineButton.Enabled = false;
                    SNESInstallButton.Enabled = false;
                    break;
            }
        }

        private void DefaultOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                switch (MainTabControl.SelectedIndex)
                {
                    case 1: // NDS
                        NDSBackButton.PerformClick();
                        break;
                    case 2: // N64
                        N64BackButton.PerformClick();
                        break;
                    case 3: // GBA
                        GBABackButton.PerformClick();
                        break;
                    case 4: // NES
                        NESBackButton.PerformClick();
                        break;
                    case 5: // SNES
                        SNESBackButton.PerformClick();
                        break;
                }
            }
        }

        private void DefaultOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Panel selectedPanel = GetSelectedPanel();
            string gameName = GetSelectedGameName();
            if (selectedPanel == null || gameName == null)
                return;

            FillPanel(selectedPanel, gameName);
        }

        private Panel GetSelectedPanel()
        {
            switch (MainTabControl.SelectedIndex)
            {
                case 1: // NDS
                    switch (NDSBaseComboBox.SelectedIndex)
                    {
                        case -1:
                            return null;
                        case 0:
                            return NDSCustomPanel;
                        default:
                            return NDSBasePanel;
                    }
                case 2: // N64
                    switch (N64BaseComboBox.SelectedIndex)
                    {
                        case -1:
                            return null;
                        case 0:
                            return N64CustomPanel;
                        default:
                            return N64BasePanel;
                    }
                case 3: // GBA
                    switch (GBABaseComboBox.SelectedIndex)
                    {
                        case -1:
                            return null;
                        case 0:
                            return GBACustomPanel;
                        default:
                            return GBABasePanel;
                    }
                case 4: // NES
                    switch (NESBaseComboBox.SelectedIndex)
                    {
                        case -1:
                            return null;
                        case 0:
                            return NESCustomPanel;
                        default:
                            return NESBasePanel;
                    }
                case 5: // SNES
                    switch (SNESBaseComboBox.SelectedIndex)
                    {
                        case -1:
                            return null;
                        case 0:
                            return SNESCustomPanel;
                        default:
                            return SNESBasePanel;
                    }
                default:
                    return null;
            }
        }

        private string GetSelectedGameName()
        {
            switch (MainTabControl.SelectedIndex)
            {
                case 1: // NDS
                    switch (NDSBaseComboBox.SelectedIndex)
                    {
                        case 1:
                            return "ZSTEU";
                        case 2:
                            return "ZSTUS";
                        case 3:
                            return "ZPHEU";
                        case 4:
                            return "ZPHUS";
                        case 5:
                            return "WWEU";
                        case 6:
                            return "WWUS";
                        default:
                            return null;
                    }
                case 2: // N64
                    switch (N64BaseComboBox.SelectedIndex)
                    {
                        case 1:
                            return "PMEU";
                        case 2:
                            return "PMUS";
                        case 3:
                            return "FZXUS";
                        case 4:
                            return "FZXJP";
                        case 5:
                            return "DK64EU";
                        case 6:
                            return "DK64US";
                        default:
                            return null;
                    }
                case 3: // GBA
                    switch (GBABaseComboBox.SelectedIndex)
                    {
                        case 1:
                            return "ZMCEU";
                        case 2:
                            return "ZMCUS";
                        case 3:
                            return "MKCEU";
                        case 4:
                            return "MKCUS";
                        default:
                            return null;
                    }
                case 4: // NES
                    switch (NESBaseComboBox.SelectedIndex)
                    {
                        case 1:
                            return "POEU";
                        case 2:
                            return "POUS";
                        case 3:
                            return "SMBEU";
                        case 4:
                            return "SMBUS";
                        default:
                            return null;
                    }
                case 5: // SNES
                    switch (SNESBaseComboBox.SelectedIndex)
                    {
                        case 1:
                            return "SMetroidEU";
                        case 2:
                            return "SMetroidUS";
                        case 3:
                            return "SMetroidJP";
                        case 4:
                            return "EarthboundEU";
                        case 5:
                            return "EarthboundUS";
                        case 6:
                            return "EarthboundJP";
                        case 7:
                            return "DKCEU";
                        case 8:
                            return "DKCUS";
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }
    }
}
