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
    public partial class importarExcelProductos : Form
    {
        List<string> valores = new List<string>();
        string excelPathfile;

        public importarExcelProductos(List<string> valoresA, string pathfile)
        {
            this.valores = valoresA;
            valores.Add("Omitir");
            this.excelPathfile = pathfile;

            InitializeComponent();
        }

        private void importarExcelProductos_Load(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            bs1.DataSource = valores;

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = valores;

            BindingSource bs3 = new BindingSource();
            bs3.DataSource = valores;

            BindingSource bs4 = new BindingSource();
            bs4.DataSource = valores;

            BindingSource bs5 = new BindingSource();
            bs5.DataSource = valores;

            BindingSource bs6 = new BindingSource();
            bs6.DataSource = valores; 

            BindingSource bs7 = new BindingSource();
            bs7.DataSource = valores;

            BindingSource bs8 = new BindingSource();
            bs8.DataSource = valores;

            BindingSource bs9 = new BindingSource();
            bs9.DataSource = valores;

            CBNombre.DataSource = bs1;
            CBCodigo.DataSource = bs2;
            CBClaveSat.DataSource = bs3;
            CBPrecioCompra.DataSource = bs4;
            CBPrecioVenta.DataSource = bs5;
            CBProveedor.DataSource = bs6;
            CBStockMax.DataSource = bs7;
            CBStockMin.DataSource = bs8;
            CBUnidadM.DataSource = bs9;

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {

        }
    }
}
