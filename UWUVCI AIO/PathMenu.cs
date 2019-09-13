using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace UWUVCI_AIO
{
    public partial class PathMenu : Form
    {
        public string language = Properties.Settings.Default.Language;
        public PathMenu()
        {
            InitializeComponent();
            loadfromsettings();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (Properties.Settings.Default.darkmode == true)
            {
                enableDarkMode();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath) && folderBrowserDialog1.SelectedPath != Properties.Settings.Default.WorkingPath && folderBrowserDialog1.SelectedPath != Properties.Settings.Default.InjectionPath && IsDirectoryEmpty(folderBrowserDialog1.SelectedPath))
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.BaseRomPath = textBox1.Text;
                Properties.Settings.Default.Save();
                if (language == "de-DE")
                {
                    MessageBox.Show("Pfad erfolgreich gespeichert");
                }
                else
                {
                    MessageBox.Show("Path successfully saved!");
                }

            }
            else
            {
                if (language == "de-DE")
                {
                    MessageBox.Show("Der Pfad fürs Basenverzeichnis darf nicht gleich dem Arbeits- sowie dem Injected Games - Verzeichnis sein.");
                }
                else
                {
                    MessageBox.Show("The path for the Bases shouldn't be the same as the Work and the Injected Games one.");
                }
            }

        }

        private void PathMenu_Load(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
        private void enableDarkMode()
        {
            this.BackColor = Color.FromArgb(60, 60, 60);
            this.ForeColor = Color.WhiteSmoke;
            button1.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
            button3.ForeColor = Color.Black;
        }
        public bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath) && folderBrowserDialog1.SelectedPath != Properties.Settings.Default.BaseRomPath && folderBrowserDialog1.SelectedPath != Properties.Settings.Default.InjectionPath && IsDirectoryEmpty(folderBrowserDialog1.SelectedPath))
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.WorkingPath = textBox3.Text;
                Properties.Settings.Default.Save();
                if (language == "de-DE")
                {
                    MessageBox.Show("Pfad erfolgreich gespeichert");
                }
                else
                {
                    MessageBox.Show("Path successfully saved!");
                }

            }
            else
            {
                if (language == "de-DE")
                {
                    MessageBox.Show("Der Pfad fürs Arbeitsverzeichnis darf nicht gleich dem Basen- sowie dem Injected Games - Verzeichnis sein.");
                }
                else
                {
                    MessageBox.Show("The path for work shouldn't be the same as the Bases and the Injected Games one.");
                }
            }
        }
        private void loadfromsettings()
        {
            textBox2.Text = Properties.Settings.Default.InjectionPath;
            textBox1.Text = Properties.Settings.Default.BaseRomPath;
            textBox3.Text = Properties.Settings.Default.WorkingPath;
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath) && folderBrowserDialog1.SelectedPath != Properties.Settings.Default.BaseRomPath && folderBrowserDialog1.SelectedPath != Properties.Settings.Default.WorkingPath && IsDirectoryEmpty(folderBrowserDialog1.SelectedPath))
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.InjectionPath = textBox2.Text;
                Properties.Settings.Default.Save();
                if (language == "de-DE")
                {
                    MessageBox.Show("Pfad erfolgreich gespeichert");
                }
                else
                {
                    MessageBox.Show("Path successfully saved!");
                }

            }
            else
            {
                if (language == "de-DE")
                {
                    MessageBox.Show("Der Pfad fürs Injected Games - Verzeichnis darf nicht gleich dem Basen- sowie dem Arbeits- Verzeichnis sein.");
                }
                else
                {
                    MessageBox.Show("The path for Injected Games shouldn't be the same as the Base and the work one.");
                }
            }
        }
    }
}
