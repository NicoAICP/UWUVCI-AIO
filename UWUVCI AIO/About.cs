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

    public partial class About : Form
    {

        public string language = Properties.Settings.Default.Language;
        public About()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            InitializeComponent();
            if (Properties.Settings.Default.darkmode)
            {
                enableDarkMode();
            }
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gbatemp.net/members/nicoaicp.404553/");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void enableDarkMode()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.ForeColor = Color.WhiteSmoke;
            button1.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
            linkLabel1.LinkColor = Color.FromArgb(133, 255, 251);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Credits credits = new Credits();
            credits.Show();
        }
    }
}
