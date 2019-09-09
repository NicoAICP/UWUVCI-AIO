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
