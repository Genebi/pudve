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
    public partial class VentanaDeCarga : Form
    {
        int contador = 0;
        public VentanaDeCarga()
        {
            InitializeComponent();
        }

        private void VentanaDeCarga_Load(object sender, EventArgs e)
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            lbl1.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (contador.Equals(1)) 
            {
                lbl1.Visible = false;
                lbl2.Visible = true;
            }
            else if (contador.Equals(2))
            {
                lbl2.Visible = false;
                lbl3.Visible = true;
            }
            else if (contador.Equals(3))
            {
                lbl3.Visible = false;
                lbl4.Visible = true;
            }
            else if (contador.Equals(4))
            {
                lbl4.Visible = false;
                lbl5.Visible = true;
            }
            else if (contador.Equals(5))
            {
                lbl5.Visible = false;
                lbl6.Visible = true;
            }
            else if (contador.Equals(6))
            {
                lbl6.Visible = false;
                lbl7.Visible = true;
            }
            else if (contador.Equals(7))
            {
                lbl7.Visible = false;
                lbl8.Visible = true;
            }
            else if (contador.Equals(8))
            {
                this.Close();
                timer1.Stop();
            }
            contador++;
        }
    }
}
