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
    public partial class Settings : Form
    {
        private string language = Properties.Settings.Default.Language; //0 = English, 1 = German

        public Settings()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
            radioButton2.Checked = Properties.Settings.Default.darkmode;
            if (Properties.Settings.Default.darkmode)
            {
                enableDarkMode();
            }

            if (language == "en-US")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }

        }



        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (comboBox1.SelectedIndex == 0)
            {
                language = "en-US";

            }
            else
            {
                language = "de-DE";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void enableDarkMode()
        {
            tabPage1.BackColor = Color.FromArgb(50, 50, 50);
            label1.ForeColor = Color.WhiteSmoke;
            radioButton1.ForeColor = Color.WhiteSmoke;
            radioButton2.ForeColor = Color.WhiteSmoke;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Language = language;

            if (radioButton2.Checked)
            {
                Properties.Settings.Default.darkmode = true;
            }
            else
            {
                Properties.Settings.Default.darkmode = false;
            }
            Properties.Settings.Default.Save();
            Application.Restart();
        }
    }
}
