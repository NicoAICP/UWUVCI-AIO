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
            tabPage1.ForeColor = Color.WhiteSmoke;
            tabPage2.ForeColor = Color.WhiteSmoke;
            tabPage3.ForeColor = Color.WhiteSmoke;
            tabPage4.ForeColor = Color.WhiteSmoke;
            tabPage5.ForeColor = Color.WhiteSmoke;
            PMEU.ForeColor = Color.Black;
            PMUS.ForeColor = Color.Black;
            FZX.ForeColor = Color.Black;
            DKEU.ForeColor = Color.Black;
            DKUS.ForeColor = Color.Black;
            FZXJP.ForeColor = Color.Black;
        }
        private void CheckN64Keys(byte b)
        {
            if(b == 0)
            {
                textBox1.Text = Properties.Settings.Default.PMEU;
                textBox2.Text = Properties.Settings.Default.PMUS;
                textBox3.Text = Properties.Settings.Default.FZX;
                textBox4.Text = Properties.Settings.Default.DKEU;
                textBox5.Text = Properties.Settings.Default.DKUS;
                textBox6.Text = Properties.Settings.Default.FZXJP;
            }
            if(b == 1)
            {
                if(textBox1.Text.GetHashCode() == -551238474)
                {
                    Properties.Settings.Default.PMEU = textBox1.Text;
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
            if (b == 2)
            {
                if (textBox2.Text.GetHashCode() == 519583299)
                {
                    Properties.Settings.Default.PMUS = textBox2.Text;
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
            if (b == 3)
            {
                if (textBox3.Text.GetHashCode() == -1036835128)
                {
                    Properties.Settings.Default.FZX = textBox3.Text;
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
            if (b == 4)
            {
                if (textBox4.Text.GetHashCode() == -206720283)
                {
                    Properties.Settings.Default.DKEU = textBox4.Text;
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
            if (b == 5)
            {
                if (textBox5.Text.GetHashCode() == 2018764825)
                {
                    Properties.Settings.Default.DKUS = textBox5.Text;
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
            if (b == 6)
            {
                if (textBox6.Text.GetHashCode() == -565957635)
                {
                    Properties.Settings.Default.FZXJP = textBox6.Text;
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
    }
}
