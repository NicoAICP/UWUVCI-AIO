using System.Drawing;
using System.Windows.Forms;

namespace UWUVCI_AIO
{
    public partial class Credits : Form
    {
        public Credits()
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
        }

        private void label1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
