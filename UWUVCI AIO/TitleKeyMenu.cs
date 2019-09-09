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
    public partial class TitleKeyMenu : Form
    {
        public string language = Properties.Settings.Default.Language;
        public TitleKeyMenu(byte b)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            tabControl1.SelectedIndex = b;
            if (Properties.Settings.Default.darkmode == true)
            {
                enableDarkMode();
            }
            CheckN64Keys(0);
            CheckSNESKeys(0);
            CheckNDSKeys(0);
            CheckNESKeys(0);
            CheckGBAKeys(0);
        }

        private void TitleKeyMenu_Load(object sender, EventArgs e)
        {

        }

        private void enableDarkMode()
        {
            tabPage1.BackColor = Color.FromArgb(60, 60, 60);
            tabPage2.BackColor = Color.FromArgb(60, 60, 60);
            tabPage3.BackColor = Color.FromArgb(60, 60, 60);
            tabPage4.BackColor = Color.FromArgb(60, 60, 60);
            tabPage5.BackColor = Color.FromArgb(60, 60, 60);
            SNES1.BackColor = Color.FromArgb(60, 60, 60);
            SNES2.BackColor = Color.FromArgb(60, 60, 60);
            tabPage1.ForeColor = Color.WhiteSmoke;
            tabPage2.ForeColor = Color.WhiteSmoke;
            tabPage3.ForeColor = Color.WhiteSmoke;
            tabPage4.ForeColor = Color.WhiteSmoke;
            tabPage5.ForeColor = Color.WhiteSmoke;
            SNES1.ForeColor = Color.WhiteSmoke;
            SNES2.ForeColor = Color.WhiteSmoke;
            PMEU.ForeColor = Color.Black;
            PMUS.ForeColor = Color.Black;
            FZX.ForeColor = Color.Black;
            DKEU.ForeColor = Color.Black;
            DKUS.ForeColor = Color.Black;
            FZXJP.ForeColor = Color.Black;
            SMetroidEU.ForeColor = Color.Black;
            SMetroidJP.ForeColor = Color.Black;
            SMetroidUS.ForeColor = Color.Black;
            EarthboundEU.ForeColor = Color.Black;
            EarthboundJP.ForeColor = Color.Black;
            EarthboundUS.ForeColor = Color.Black;
            DKCEU.ForeColor = Color.Black;
            DKCUS.ForeColor = Color.Black;
            button7.ForeColor = Color.Black;
            button8.ForeColor = Color.Black;
            ZSTEU.ForeColor = Color.Black;
            ZSTUS.ForeColor = Color.Black;
            ZPHEU.ForeColor = Color.Black;
            ZPHUS.ForeColor = Color.Black;
            WWEU.ForeColor = Color.Black;
            WWUS.ForeColor = Color.Black;
            POEU.ForeColor = Color.Black;
            POUS.ForeColor = Color.Black;
            SMBEU.ForeColor = Color.Black;
            SMBUS.ForeColor = Color.Black;
            ZMCEU.ForeColor = Color.Black;
            ZMCUS.ForeColor = Color.Black;
            MKCEU.ForeColor = Color.Black;
            MKCUS.ForeColor = Color.Black;
        }
        private void CheckN64Keys(byte b)
        {
            if(b == 0)
            {
                PMEUtxt.Text = Properties.Settings.Default.PMEU;
                PMUStxt.Text = Properties.Settings.Default.PMUS;
                FZXUStxt.Text = Properties.Settings.Default.FZXUS;
                DK64EUtxt.Text = Properties.Settings.Default.DK64EU;
                DK64UStxt.Text = Properties.Settings.Default.DK64US;
                FZXJPtxt.Text = Properties.Settings.Default.FZXJP;
            }
            // PAPER MARIO EU
            if(b == 1)
            {
                if(PMEUtxt.Text.GetHashCode() == -551238474)
                {
                    Properties.Settings.Default.PMEU = PMEUtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Paper Mario [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Paper Mario [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if(language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Paper Mario [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if(language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Paper Mario [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // PAPER MARIO US
            if (b == 2)
            {
                if (PMUStxt.Text.GetHashCode() == 519583299)
                {
                    Properties.Settings.Default.PMUS = PMUStxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Paper Mario [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Paper Mario [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Paper Mario [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Paper Mario [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // F-ZERO X US
            if (b == 3)
            {
                if (FZXUStxt.Text.GetHashCode() == -1036835128)
                {
                    Properties.Settings.Default.FZXUS = FZXUStxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for F-Zero X [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für F-Zero X [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for F-Zero X [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für F-Zero X [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // DONKEY KONG 64 EU
            if (b == 4)
            {
                if (DK64EUtxt.Text.GetHashCode() == -206720283)
                {
                    Properties.Settings.Default.DK64EU = DK64EUtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong 64 [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong 64 [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong 64 [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong 64 [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // DONKEY KONG 64 US
            if (b == 5)
            {
                if (DK64UStxt.Text.GetHashCode() == 2018764825)
                {
                    Properties.Settings.Default.DK64US = DK64UStxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong 64 [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong 64 [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong 64 [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong 64 [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // F-ZERO X JP                                    DONT QUESTION MY LISTING
            if (b == 6)
            {
                if (FZXJPtxt.Text.GetHashCode() == -565957635)
                {
                    Properties.Settings.Default.FZXJP = FZXJPtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for F-Zero X [JP] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für F-Zero X [JP] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for F-Zero X [JP] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für F-Zero X [JP] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void CheckSNESKeys(byte b)
        {
            if (b == 0)
            {
                SMetroidEUtxt.Text = Properties.Settings.Default.SMetroidEU;
                SMetroidUStxt.Text = Properties.Settings.Default.SMetroidUS;
                SMetroidJPtxt.Text = Properties.Settings.Default.SMetroidJP;
                EarthboundEUtxt.Text = Properties.Settings.Default.EarthboundEU;
                EarthboundUStxt.Text = Properties.Settings.Default.EarthboundUS;
                EarthboundJPtxt.Text = Properties.Settings.Default.EarthboundJP;
                DKCEUtxt.Text = Properties.Settings.Default.DKCEU;
                DKCUStxt.Text = Properties.Settings.Default.DKCUS;
            }
            // SUPER METROID EU
            if(b == 1)
            {
                if (SMetroidEUtxt.Text.GetHashCode() == -1533216561)
                {
                    Properties.Settings.Default.SMetroidEU = SMetroidEUtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Metroid [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Metroid [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Metroid [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Metroid [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // SUPER METROID US
            if (b == 2)
            {
                if (SMetroidUStxt.Text.GetHashCode() == 533928700)
                {
                    Properties.Settings.Default.SMetroidUS = SMetroidUStxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Metroid [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Metroid [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Metroid [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Metroid [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // スーパーメトロイド SUPER METROID JP
            if (b == 3)
            {
                if (SMetroidJPtxt.Text.GetHashCode() == 1767332967)
                {
                    Properties.Settings.Default.SMetroidJP = SMetroidJPtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for スーパーメトロイド (Super Metroid) [JP] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für スーパーメトロイド (Super Metroid) [JP] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for スーパーメトロイド (Super Metroid) [JP] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für スーパーメトロイド (Super Metroid) [JP] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // EARTHBOUND EU
            if (b == 4)
            {
                if (EarthboundEUtxt.Text.GetHashCode() == 922998333)
                {
                    Properties.Settings.Default.EarthboundEU = EarthboundEUtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Earthbound [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Earthbound [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Earthbound [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Earthbound [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // EARTHBOUND US
            if (b == 5)
            {
                if (EarthboundUStxt.Text.GetHashCode() == 2126250902)
                {
                    Properties.Settings.Default.EarthboundUS = EarthboundUStxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Earthbound [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Earthbound [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Earthbound [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Earthbound [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // MOTHER (EARTHBOUND) JP
            if (b == 6)
            {
                if (EarthboundJPtxt.Text.GetHashCode() == 64041181)
                {
                    Properties.Settings.Default.EarthboundJP = EarthboundJPtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for MOTHER [JP] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für MOTHER [JP] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for MOTHER [JP] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für MOTHER [JP] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // DONKEY KONG COUNTRY EU
            if (b == 7)
            {
                if (DKCEUtxt.Text.GetHashCode() == 2110524003)
                {
                    Properties.Settings.Default.DKCEU = DKCEUtxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong Country [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong Country [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong Country [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong Country [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            // DONKEY KONG COUNTRY US
            if (b == 8)
            {
                if (DKCUStxt.Text.GetHashCode() == -1733982319)
                {
                    Properties.Settings.Default.DKCUS = DKCUStxt.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong Country [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong Country [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Donkey Kong Country [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Donkey Kong Country [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void CheckNDSKeys(byte b)
        {
            if(b == 0)
            {
                ZSTEU_TXT.Text = Properties.Settings.Default.ZSTEU;
                ZSTUS_TXT.Text = Properties.Settings.Default.ZSTUS;
                ZPHEU_TXT.Text = Properties.Settings.Default.ZPHEU;
                ZPHUS_TXT.Text = Properties.Settings.Default.ZPHUS;
                WWEU_TXT.Text = Properties.Settings.Default.WWEU;
                WWUS_TXT.Text = Properties.Settings.Default.WWUS;
            }
            if(b == 1)
            {
                if (ZSTEU_TXT.Text.GetHashCode() == -1633670821)
                {
                    Properties.Settings.Default.ZSTEU = ZSTEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Spirit Tracks [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Spirit Tracks [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Spirit Tracks [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Spirit Tracks [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 2)
            {
                if (ZSTUS_TXT.Text.GetHashCode() == -532174495)
                {
                    Properties.Settings.Default.ZSTUS = ZSTUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Spirit Tracks [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Spirit Tracks [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Spirit Tracks [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Spirit Tracks [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 3)
            {
                if (ZPHEU_TXT.Text.GetHashCode() == 1694123503)
                {
                    Properties.Settings.Default.ZPHEU = ZPHEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Phantom Hourglass [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Phantom Hourglass [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Phantom Hourglass [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Phantom Hourglass [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 4)
            {
                if (ZPHUS_TXT.Text.GetHashCode() == -997138256)
                {
                    Properties.Settings.Default.ZPHUS = ZPHUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Phantom Hourglass [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Phantom Hourglass [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Phantom Hourglass [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Phantom Hourglass [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 5)
            {
                if (WWEU_TXT.Text.GetHashCode() == 1477098714)
                {
                    Properties.Settings.Default.WWEU = WWEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Wario Ware: Touched! [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Wario Ware: Touched! [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Wario Ware: Touched! [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Wario Ware: Touched! [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 6)
            {
                if (WWUS_TXT.Text.GetHashCode() == -829326562)
                {
                    Properties.Settings.Default.WWUS = WWUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Wario Ware: Touched! [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Wario Ware: Touched! [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Wario Ware: Touched! [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Wario Ware: Touched! [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void CheckNESKeys(byte b)
        {
            if (b == 0)
            {
                POEU_TXT.Text = Properties.Settings.Default.POEU;
                POUS_TXT.Text = Properties.Settings.Default.POUS;
                SMBEU_TXT.Text = Properties.Settings.Default.SMBEU;
                SMBEU_TXT.Text = Properties.Settings.Default.SMBUS;
            }
            if (b == 1)
            {
                if (POEU_TXT.Text.GetHashCode() == 1686136738)
                {
                    Properties.Settings.Default.POEU = POEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Punch-Out!! [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Punch-Out!! [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Punch-Out!! [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Punch-Out!! [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 2)
            {
                if (POUS_TXT.Text.GetHashCode() == 683326464)
                {
                    Properties.Settings.Default.POUS = POUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Punch-Out!! Featuring Mr. Dream [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Punch-Out!! Featuring Mr. Dream [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Punch-Out!! Featuring Mr. Dream [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Punch-Out!! Featuring Mr. Dream [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 3)
            {
                if (SMBEU_TXT.Text.GetHashCode() == 1339870877)
                {
                    Properties.Settings.Default.SMBEU = SMBEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Mario Bros. [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Mario Bros. [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Mario Bros. [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Mario Bros. [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 4)
            {
                if (SMBUS_TXT.Text.GetHashCode() == 1464579096)
                {
                    Properties.Settings.Default.SMBUS = SMBUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Mario Bros. [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Mario Bros. [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Super Mario Bros. [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Super Mario Bros. [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void PMEU_Click(object sender, EventArgs e)
        {
            CheckN64Keys(1);
        }

        private void PMUS_Click(object sender, EventArgs e)
        {
            CheckN64Keys(2);
        }

        private void FZX_Click(object sender, EventArgs e)
        {
            CheckN64Keys(3);
        }

        private void DKEU_Click(object sender, EventArgs e)
        {
            CheckN64Keys(4);
        }

        private void DKUS_Click(object sender, EventArgs e)
        {
            CheckN64Keys(5);
        }

        private void FZXJP_Click(object sender, EventArgs e)
        {
            CheckN64Keys(6);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex = 0;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex = 1;
        }

        private void DKCUS_Click_1(object sender, EventArgs e)
        {
            CheckSNESKeys(8);
        }

        private void DKCEU_Click_1(object sender, EventArgs e)
        {
            CheckSNESKeys(7);
        }

        private void SMetroidEU_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(1);
        }

        private void SMetroidUS_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(2);
        }

        private void SMetroidJP_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(3);
        }

        private void EarthboundEU_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(4);
        }

        private void EarthboundUS_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(5);
        }

        private void EarthboundJP_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(6);
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void SNES2_Click(object sender, EventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ZSPEU_Click(object sender, EventArgs e)
        {
            CheckNDSKeys(1);
        }

        private void ZSPUS_Click(object sender, EventArgs e)
        {
            CheckNDSKeys(2);
        }

        private void ZPHEU_Click(object sender, EventArgs e)
        {
            CheckNDSKeys(3);
        }

        private void ZPHUS_Click(object sender, EventArgs e)
        {
            CheckNDSKeys(4);
        }

        private void WWEU_Click(object sender, EventArgs e)
        {
            CheckNDSKeys(5);
        }

        private void WWUS_Click(object sender, EventArgs e)
        {
            CheckNDSKeys(6);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            CheckNESKeys(1);
        }

        private void POUS_Click(object sender, EventArgs e)
        {
            CheckNESKeys(2);
        }

        private void SMBEU_Click(object sender, EventArgs e)
        {
            CheckNESKeys(3);
        }

        private void SMBUS_Click(object sender, EventArgs e)
        {
            CheckNESKeys(4);
        }
        private void CheckGBAKeys(byte b)
        {
            if(b == 0)
            {
                ZMCEU_TXT.Text = Properties.Settings.Default.ZMCEU;
                ZMCUS_TXT.Text = Properties.Settings.Default.ZMCUS;
                MKCEU_TXT.Text = Properties.Settings.Default.MKCEU;
                MKCUS_TXT.Text = Properties.Settings.Default.MKCUS;
            }
            if (b == 1)
            {
                if (ZMCEU_TXT.Text.GetHashCode() == 1694865495)
                {
                    Properties.Settings.Default.ZMCEU = ZMCEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Minish Cap [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Minish Cap [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Minish Cap [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Minish Cap [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 2)
            {
                if (ZMCUS_TXT.Text.GetHashCode() == 1378855071)
                {
                    Properties.Settings.Default.ZMCUS = ZMCUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Minish Cap [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Minish Cap [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for The Legend Of Zelda: Minish Cap [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für The Legend Of Zelda: Minish Cap [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 3)
            {
                if (MKCEU_TXT.Text.GetHashCode() == 1154633832)
                {
                    Properties.Settings.Default.MKCEU = MKCEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Mario Kart Super Circuit [EU] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Mario Kart Super Circuit [EU] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Mario Kart Super Circuit [EU] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Mario Kart Super Circuit [EU] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            if (b == 4)
            {
                if (MKCUS_TXT.Text.GetHashCode() == 1495117536)
                {
                    Properties.Settings.Default.MKCUS = MKCUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Mario Kart Super Circuit [US] is correct.", "Correct Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Mario Kart Super Circuit [US] ist richtig.", "Richtiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    if (language == "en-US")
                    {
                        MessageBox.Show("The entered TitleKey for Mario Kart Super Circuit [US] is incorrect.", "Incorrect Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (language == "de-DE")
                    {
                        MessageBox.Show("Der eingegebene TitleKey für Mario Kart Super Circuit [US] ist falsch.", "Falscher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void ZMCEU_Click(object sender, EventArgs e)
        {
            CheckGBAKeys(1);
        }

        private void ZMCUS_Click(object sender, EventArgs e)
        {
            CheckGBAKeys(2);
        }

        private void MKCEU_Click(object sender, EventArgs e)
        {
            CheckGBAKeys(3);
        }

        private void MKCUS_Click(object sender, EventArgs e)
        {
            CheckGBAKeys(4);
        }
    }
}
