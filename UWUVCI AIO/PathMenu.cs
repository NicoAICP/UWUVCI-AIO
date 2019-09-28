using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using UWUVCI_AIO.Properties;

namespace UWUVCI_AIO
{
    public partial class PathMenu : Form
    {
        public PathMenu()
        {
            InitializeComponent();

            if (Properties.Settings.Default.DarkMode)
            {
                EnableDarkMode();
            }
            LoadFromSettings();
        }

        private void EnableDarkMode()
        {
            this.BackColor = Color.FromArgb(60, 60, 60);
            this.ForeColor = Color.WhiteSmoke;
            BaseRomButton.ForeColor = Color.Black;
            InjectionButton.ForeColor = Color.Black;
        }

        private void LoadFromSettings()
        {
            BaseRomTextBox.Text = Properties.Settings.Default.BaseRomPath;
            InjectionTextBox.Text = Properties.Settings.Default.InjectionPath;
        }

        private void BaseRomButton_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog fileDialog = new CommonOpenFileDialog())
            {
                fileDialog.IsFolderPicker = true;

                if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (fileDialog.FileName != Properties.Settings.Default.InjectionPath)
                    {
                        Properties.Settings.Default.BaseRomPath = BaseRomTextBox.Text = fileDialog.FileName;
                        Properties.Settings.Default.Save();
                        MessageBox.Show(Resources.PathSaved, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Resources.BasePathInvalid, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            Activate();
        }

        private void InjectionButton_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog fileDialog = new CommonOpenFileDialog())
            {
                fileDialog.IsFolderPicker = true;

                if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    if (fileDialog.FileName != Properties.Settings.Default.BaseRomPath)
                    {
                        Properties.Settings.Default.InjectionPath = InjectionTextBox.Text = fileDialog.FileName;
                        Properties.Settings.Default.Save();
                        MessageBox.Show(Resources.PathSaved, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Resources.InjectionPathInvalid, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            Activate();
        }
    }
}
