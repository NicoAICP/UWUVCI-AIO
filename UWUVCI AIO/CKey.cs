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
using System.IO;
using System.Reflection;

namespace UWUVCI_AIO
{
    public partial class CKey : Form
    {
        public string language = Properties.Settings.Default.Language;
        public CKey()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            if (Properties.Settings.Default.darkmode == true)
            {
                enableDarkMode();
            }


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.GetHashCode() == 487391367)
            {
                Properties.Settings.Default.CommonKey = textBox1.Text;
                Properties.Settings.Default.Save();
                if (File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TOOLS\JNUSTOOL\config")))
                {

                    string cfg = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TOOLS\JNUSTOOL\config");
                    string word = "<ckey>";
                    if (File.ReadAllText(cfg).Contains(word))
                    {
                        string text = File.ReadAllText(cfg);
                        text = text.Replace(word, Properties.Settings.Default.CommonKey);
                        File.WriteAllText(cfg, text);
                    }
                }
                if (language == "en-US")
                {
                    MessageBox.Show("CommonKey successfully set", "Valid Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show("CommonKey erfolgreich gespeichert", "Gültiger Key", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            else
            {
                if (language == "en-US")
                {
                    MessageBox.Show("Invalid CommonKey provided", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Ein Falscher CommonKey wurde angegeben", "Flascher Key", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
        private void enableDarkMode()
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

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
