using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using UWUVCI_AIO.Properties;

namespace UWUVCI_AIO
{
    public partial class CKey : Form
    {
        public CKey()
        {
            InitializeComponent();
            if (Properties.Settings.Default.DarkMode)
            {
                EnableDarkMode();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.GetHashCode() == 487391367)
            {
                Properties.Settings.Default.CommonKey = textBox1.Text;
                Properties.Settings.Default.Save();
                string configFile = Path.Combine(Directory.GetCurrentDirectory(), "TOOLS", "JNUSTOOL", "config");
                if (File.Exists(configFile))
                {
                    string word = "<ckey>";
                    string text = File.ReadAllText(configFile);
                    if (text.Contains(word))
                    {
                        text = text.Replace(word, Properties.Settings.Default.CommonKey);
                        File.WriteAllText(configFile, text);
                    }
                }

                MessageBox.Show(Resources.ValidCommonkey, Resources.ValidKey, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                MessageBox.Show(Resources.InvalidCommonkey, Resources.InvalidKey, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void EnableDarkMode()
        {

            this.BackColor = Color.FromArgb(60, 60, 60);
            this.ForeColor = Color.WhiteSmoke;
            button1.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
