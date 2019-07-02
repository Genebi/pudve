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
    public partial class Inventario : Form
    {
        RevisarInventario checkInventory = new RevisarInventario();
        ReporteFinalRevisarInventario FinalReportReviewInventory = new ReporteFinalRevisarInventario();

        public Inventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkInventory.FormClosed += delegate
            {
                FinalReportReviewInventory.FormClosed += delegate
                {

                };
                if (!FinalReportReviewInventory.Visible)
                {
                    FinalReportReviewInventory.ShowDialog();
                }
                else
                {
                    FinalReportReviewInventory.ShowDialog();
                }
            };
            if (!checkInventory.Visible)
            {
                checkInventory.ShowDialog();
            }
            else
            {
                checkInventory.ShowDialog();
            }
        }
    }
}
