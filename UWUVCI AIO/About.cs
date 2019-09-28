using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace UWUVCI_AIO
{
    public partial class About : Form
    {
        private static Credits credits;

        public About()
        {
            InitializeComponent();

            if (Properties.Settings.Default.DarkMode)
            {
                EnableDarkMode();
            }
        }

        private void EnableDarkMode()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.ForeColor = Color.WhiteSmoke;
            CloseButton.ForeColor = Color.Black;
            CreditsButton.ForeColor = Color.Black;
            linkLabel1.LinkColor = Color.FromArgb(133, 255, 251);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://gbatemp.net/members/nicoaicp.404553/");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            if (credits == null || credits.IsDisposed)
            {
                credits = new Credits();
                credits.Show();
            }
            else
            {
                credits.Activate();
            }
        }
    }
}
