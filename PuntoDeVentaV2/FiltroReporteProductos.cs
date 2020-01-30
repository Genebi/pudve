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
    public partial class FiltroReporteProductos : Form
    {
        public FiltroReporteProductos()
        {
            InitializeComponent();
        }

        private void FiltroReporteProductos_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> condiciones = new Dictionary<string, string>();
            condiciones.Add(">=", "Mayor o igual que");
            condiciones.Add("<=", "Menor o igual que");
            condiciones.Add("==", "Igual que");
            condiciones.Add(">", "Mayor que");
            condiciones.Add("<", "Menor que");

            cbStock.DataSource = condiciones.ToArray();
            cbStock.ValueMember = "Key";
            cbStock.DisplayMember = "Value";

            cbPrecio.DataSource = condiciones.ToArray();
            cbPrecio.ValueMember = "Key";
            cbPrecio.DisplayMember = "Value";
        }
    }
}
