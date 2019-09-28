using System;
using System.Drawing;
using System.Windows.Forms;
using UWUVCI_AIO.Properties;

namespace UWUVCI_AIO
{
    public partial class TitleKeyMenu : Form
    {
        public TitleKeyMenu(int index)
        {
            InitializeComponent();

            tabControl1.SelectedIndex = index;
            if (Properties.Settings.Default.DarkMode)
            {
                EnableDarkMode();
            }
            LoadFromSettings();
        }

        private void EnableDarkMode()
        {
            NDSTabPage.BackColor = Color.FromArgb(60, 60, 60);
            N64TabPage.BackColor = Color.FromArgb(60, 60, 60);
            GBATabPage.BackColor = Color.FromArgb(60, 60, 60);
            NESTabPage.BackColor = Color.FromArgb(60, 60, 60);
            SNESTabPage.BackColor = Color.FromArgb(60, 60, 60);
            SNES1.BackColor = Color.FromArgb(60, 60, 60);
            SNES2.BackColor = Color.FromArgb(60, 60, 60);
            NDSTabPage.ForeColor = Color.WhiteSmoke;
            N64TabPage.ForeColor = Color.WhiteSmoke;
            GBATabPage.ForeColor = Color.WhiteSmoke;
            NESTabPage.ForeColor = Color.WhiteSmoke;
            SNESTabPage.ForeColor = Color.WhiteSmoke;
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

        private void LoadFromSettings()
        {
            ZSTEU_TXT.Text = Properties.Settings.Default.ZSTEU;
            ZSTUS_TXT.Text = Properties.Settings.Default.ZSTUS;
            ZPHEU_TXT.Text = Properties.Settings.Default.ZPHEU;
            ZPHUS_TXT.Text = Properties.Settings.Default.ZPHUS;
            WWEU_TXT.Text = Properties.Settings.Default.WWEU;
            WWUS_TXT.Text = Properties.Settings.Default.WWUS;
            PMEUtxt.Text = Properties.Settings.Default.PMEU;
            PMUStxt.Text = Properties.Settings.Default.PMUS;
            FZXUStxt.Text = Properties.Settings.Default.FZXUS;
            DK64EUtxt.Text = Properties.Settings.Default.DK64EU;
            DK64UStxt.Text = Properties.Settings.Default.DK64US;
            FZXJPtxt.Text = Properties.Settings.Default.FZXJP;
            ZMCEU_TXT.Text = Properties.Settings.Default.ZMCEU;
            ZMCUS_TXT.Text = Properties.Settings.Default.ZMCUS;
            MKCEU_TXT.Text = Properties.Settings.Default.MKCEU;
            MKCUS_TXT.Text = Properties.Settings.Default.MKCUS;
            POEU_TXT.Text = Properties.Settings.Default.POEU;
            POUS_TXT.Text = Properties.Settings.Default.POUS;
            SMBEU_TXT.Text = Properties.Settings.Default.SMBEU;
            SMBEU_TXT.Text = Properties.Settings.Default.SMBUS;
            SMetroidEUtxt.Text = Properties.Settings.Default.SMetroidEU;
            SMetroidUStxt.Text = Properties.Settings.Default.SMetroidUS;
            SMetroidJPtxt.Text = Properties.Settings.Default.SMetroidJP;
            EarthboundEUtxt.Text = Properties.Settings.Default.EarthboundEU;
            EarthboundUStxt.Text = Properties.Settings.Default.EarthboundUS;
            EarthboundJPtxt.Text = Properties.Settings.Default.EarthboundJP;
            DKCEUtxt.Text = Properties.Settings.Default.DKCEU;
            DKCUStxt.Text = Properties.Settings.Default.DKCUS;
        }

        private void CheckNDSKeys(int index)
        {
            switch (index)
            {
                case 1 when ZSTEU_TXT.Text.GetHashCode() == -1633670821:
                    Properties.Settings.Default.ZSTEU = ZSTEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "The Legend Of Zelda: Spirit Tracks [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "The Legend Of Zelda: Spirit Tracks [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2 when ZSTUS_TXT.Text.GetHashCode() == -532174495:
                    Properties.Settings.Default.ZSTUS = ZSTUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "The Legend Of Zelda: Spirit Tracks [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "The Legend Of Zelda: Spirit Tracks [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 3 when ZPHEU_TXT.Text.GetHashCode() == 1694123503:
                    Properties.Settings.Default.ZPHEU = ZPHEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "The Legend Of Zelda: Phantom Hourglass [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 3:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "The Legend Of Zelda: Phantom Hourglass [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 4 when ZPHUS_TXT.Text.GetHashCode() == -997138256:
                    Properties.Settings.Default.ZPHUS = ZPHUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "The Legend Of Zelda: Phantom Hourglass [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 4:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "The Legend Of Zelda: Phantom Hourglass [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 5 when WWEU_TXT.Text.GetHashCode() == 1477098714:
                    Properties.Settings.Default.WWEU = WWEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Wario Ware: Touched! [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 5:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Wario Ware: Touched! [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 6 when WWUS_TXT.Text.GetHashCode() == -829326562:
                    Properties.Settings.Default.WWUS = WWUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Wario Ware: Touched! [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 6:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Wario Ware: Touched! [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void CheckN64Keys(int index)
        {
            switch (index)
            {
                // PAPER MARIO EU
                case 1 when PMEUtxt.Text.GetHashCode() == -551238474:
                    Properties.Settings.Default.PMEU = PMEUtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Paper Mario [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Paper Mario [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // PAPER MARIO US
                case 2 when PMUStxt.Text.GetHashCode() == 519583299:
                    Properties.Settings.Default.PMUS = PMUStxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Paper Mario [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Paper Mario [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // F-ZERO X US
                case 3 when FZXUStxt.Text.GetHashCode() == -1036835128:
                    Properties.Settings.Default.FZXUS = FZXUStxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "F-Zero X [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 3:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "F-Zero X [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // F-ZERO X JP
                case 4 when FZXJPtxt.Text.GetHashCode() == -565957635:
                    Properties.Settings.Default.FZXJP = FZXJPtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "F-Zero X [JP]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 4:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "F-Zero X [JP]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // DONKEY KONG 64 EU
                case 5 when DK64EUtxt.Text.GetHashCode() == -206720283:
                    Properties.Settings.Default.DK64EU = DK64EUtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Donkey Kong 64 [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 5:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Donkey Kong 64 [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // DONKEY KONG 64 US
                case 6 when DK64UStxt.Text.GetHashCode() == 2018764825:
                    Properties.Settings.Default.DK64US = DK64UStxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Donkey Kong 64 [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 6:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Donkey Kong 64 [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void CheckGBAKeys(int index)
        {
            switch (index)
            {
                case 1 when ZMCEU_TXT.Text.GetHashCode() == 1694865495:
                    Properties.Settings.Default.ZMCEU = ZMCEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "The Legend Of Zelda: Minish Cap [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "The Legend Of Zelda: Minish Cap [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2 when ZMCUS_TXT.Text.GetHashCode() == 1378855071:
                    Properties.Settings.Default.ZMCUS = ZMCUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "The Legend Of Zelda: Minish Cap [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "The Legend Of Zelda: Minish Cap [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 3 when MKCEU_TXT.Text.GetHashCode() == 1154633832:
                    Properties.Settings.Default.MKCEU = MKCEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Mario Kart Super Circuit [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 3:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Mario Kart Super Circuit [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 4 when MKCUS_TXT.Text.GetHashCode() == 1495117536:
                    Properties.Settings.Default.MKCUS = MKCUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Mario Kart Super Circuit [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 4:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Mario Kart Super Circuit [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void CheckNESKeys(int index)
        {
            switch (index)
            {
                case 1 when POEU_TXT.Text.GetHashCode() == 1686136738:
                    Properties.Settings.Default.POEU = POEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Punch-Out!! [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Punch-Out!! [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2 when POUS_TXT.Text.GetHashCode() == 683326464:
                    Properties.Settings.Default.POUS = POUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Punch-Out!! Featuring Mr. Dream [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Punch-Out!! Featuring Mr. Dream [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 3 when SMBEU_TXT.Text.GetHashCode() == 1339870877:
                    Properties.Settings.Default.SMBEU = SMBEU_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Super Mario Bros. [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 3:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Super Mario Bros. [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 4 when SMBUS_TXT.Text.GetHashCode() == 1464579096:
                    Properties.Settings.Default.SMBUS = SMBUS_TXT.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Super Mario Bros. [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 4:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Super Mario Bros. [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void CheckSNESKeys(int b)
        {
            switch (b)
            {
                // SUPER METROID EU
                case 1 when SMetroidEUtxt.Text.GetHashCode() == -1533216561:
                    Properties.Settings.Default.SMetroidEU = SMetroidEUtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Super Metroid [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 1:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Super Metroid [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // SUPER METROID US
                case 2 when SMetroidUStxt.Text.GetHashCode() == 533928700:
                    Properties.Settings.Default.SMetroidUS = SMetroidUStxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Super Metroid [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 2:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Super Metroid [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // スーパーメトロイド SUPER METROID JP
                case 3 when SMetroidJPtxt.Text.GetHashCode() == 1767332967:
                    Properties.Settings.Default.SMetroidJP = SMetroidJPtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "スーパーメトロイド (Super Metroid) [JP]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 3:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "スーパーメトロイド (Super Metroid) [JP]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                // EARTHBOUND EU
                case 4 when EarthboundEUtxt.Text.GetHashCode() == 922998333:
                    Properties.Settings.Default.EarthboundEU = EarthboundEUtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Earthbound [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 4:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Earthbound [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // EARTHBOUND US
                case 5 when EarthboundUStxt.Text.GetHashCode() == 2126250902:
                    Properties.Settings.Default.EarthboundUS = EarthboundUStxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Earthbound [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 5:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Earthbound [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // MOTHER (EARTHBOUND) JP
                case 6 when EarthboundJPtxt.Text.GetHashCode() == 64041181:
                    Properties.Settings.Default.EarthboundJP = EarthboundJPtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "MOTHER [JP]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 6:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "MOTHER [JP]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // DONKEY KONG COUNTRY EU
                case 7 when DKCEUtxt.Text.GetHashCode() == 2110524003:
                    Properties.Settings.Default.DKCEU = DKCEUtxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Donkey Kong Country [EU]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 7:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Donkey Kong Country [EU]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                // DONKEY KONG COUNTRY US
                case 8 when DKCUStxt.Text.GetHashCode() == -1733982319:
                    Properties.Settings.Default.DKCUS = DKCUStxt.Text;
                    Properties.Settings.Default.Save();
                    MessageBox.Show(string.Format(Resources.TitlekeyCorrect, "Donkey Kong Country [US]"), Resources.CorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case 8:
                    MessageBox.Show(string.Format(Resources.TitlekeyIncorrect, "Donkey Kong Country [US]"), Resources.IncorrectKey, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
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

        private void FZXJP_Click(object sender, EventArgs e)
        {
            CheckN64Keys(4);
        }

        private void DKEU_Click(object sender, EventArgs e)
        {
            CheckN64Keys(5);
        }

        private void DKUS_Click(object sender, EventArgs e)
        {
            CheckN64Keys(6);
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

        private void Button8_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex = 0; // SNES page 1
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedIndex = 1; // SNES page 2
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

        private void DKCEU_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(7);
        }

        private void DKCUS_Click(object sender, EventArgs e)
        {
            CheckSNESKeys(8);
        }
    }
}
