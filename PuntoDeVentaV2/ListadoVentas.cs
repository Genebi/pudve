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
    public partial class ListadoVentas : Form
    {
        public ListadoVentas()
        {
            InitializeComponent();
        }

        private void ListadoVentas_Load(object sender, EventArgs e)
        {
            cbVentas.SelectedIndex = 0;
            cbTipoVentas.SelectedIndex = 0;
            cbVentas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoVentas.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscar ventas");
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            Ventas venta = new Ventas();
            venta.ShowDialog();
        }
    }
}
