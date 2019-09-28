using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace UWUVCI_AIO
{
    public partial class Settings : Form
    {
        private string language = Properties.Settings.Default.Language;

        public Settings()
        {
            InitializeComponent();
            radioButton2.Checked = Properties.Settings.Default.DarkMode;
            if (Properties.Settings.Default.DarkMode)
            {
                EnableDarkMode();
            }

            comboBox1.SelectedIndex = (language == "en-US") ? 0 : 1;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            language = comboBox1.SelectedIndex == 0 ? "en-US" : "de-DE";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EnableDarkMode()
        {
            tabPage1.BackColor = Color.FromArgb(50, 50, 50);
            label1.ForeColor = Color.WhiteSmoke;
            radioButton1.ForeColor = Color.WhiteSmoke;
            radioButton2.ForeColor = Color.WhiteSmoke;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = language;

            Properties.Settings.Default.DarkMode = radioButton2.Checked;
            Properties.Settings.Default.Save();
            Application.Restart();
        }
    }
}
