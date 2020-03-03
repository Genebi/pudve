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
    public partial class FiltroRevisarInventario : Form
    {
        // Para el filtro de inventario
        public string tipoFiltro { get; set; }
        public string operadorFiltro { get; set; }
        public int cantidadFiltro { get; set; }

        public FiltroRevisarInventario()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FiltroRevisarInventario_Load(object sender, EventArgs e)
        {
            var operadores = new Dictionary<string, string>();
            operadores.Add("NA", "No aplica");
            operadores.Add(">=", "Mayor o igual que");
            operadores.Add("<=", "Menor o igual que");
            operadores.Add("==", "Igual que");
            operadores.Add(">", "Mayor que");
            operadores.Add("<", "Menor que");

            var filtros = new Dictionary<string, string>();
            filtros.Add("Normal", "Revision Normal");
            filtros.Add("Stock", "Stock");
            filtros.Add("StockMinimo", "Stock Mínimo");
            filtros.Add("StockNecesario", "Stock Máximo");
            filtros.Add("NumeroRevision", "Número de Revisión");

            cbFiltro.DataSource = filtros.ToArray();
            cbFiltro.DisplayMember = "Value";
            cbFiltro.ValueMember = "Key";

            cbOperadores.DataSource = operadores.ToArray();
            cbOperadores.DisplayMember = "Value";
            cbOperadores.ValueMember = "Key";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var filtro = cbFiltro.SelectedValue.ToString();
            
            tipoFiltro = filtro;

            if (filtro == "Normal")
            {
                operadorFiltro = "NA";
                cantidadFiltro = 0;
            }
            else
            {
                var operador = cbOperadores.SelectedValue.ToString();
                var cantidad = txtCantidad.Text.Trim();

                if (string.IsNullOrWhiteSpace(cantidad))
                {
                    cantidad = "0";
                }

                operadorFiltro = operador;
                cantidadFiltro = Convert.ToInt32(cantidad);
            }

            DialogResult = DialogResult.OK;
        }

        private void cbFiltro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var filtro = cbFiltro.SelectedValue.ToString();

            if (filtro == "Normal")
            {
                cbOperadores.Visible = false;
                txtCantidad.Visible = false;
            }
            else
            {
                cbOperadores.Visible = true;
                txtCantidad.Visible = true;
            }
        }
    }
}
