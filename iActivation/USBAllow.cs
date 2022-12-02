using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iActivation
{
    public partial class USBAllow : Form
    {
        public static bool IsAllow = false;    
        public USBAllow ()
        {
            InitializeComponent();
		}
        private void Close_Click(object sender, EventArgs e)
        {
            USBAllow.IsAllow = true;
            this.Hide();
            this.Close();
        }
    }
}
