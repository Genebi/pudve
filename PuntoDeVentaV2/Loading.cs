using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            picbox_loading.Load("loading2.gif");
            picbox_loading.Location = new Point(this.Width / 2 - picbox_loading.Width / 2, this.Height / 2 - picbox_loading.Height / 2);
        }
    }
}
