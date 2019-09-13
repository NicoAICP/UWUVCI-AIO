using System;
using System.Drawing;
using System.Windows.Forms;

namespace UWUVCI_AIO
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
            if (Properties.Settings.Default.darkmode)
            {
                enableDarkMode();
            }
        }

        private void Credits_Load(object sender, EventArgs e)
        {

        }
        private void enableDarkMode()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.ForeColor = Color.WhiteSmoke;
        }
    }
}
