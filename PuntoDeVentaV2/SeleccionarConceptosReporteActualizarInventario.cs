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
    public partial class SeleccionarConceptosReporteActualizarInventario : Form
    {
        string[] itemsCLBConceptosExistentes;

        private void unCheckAllListCLBExistentes()
        {
            for (int i = 0; i < CLBConceptosExistentes.Items.Count; i++)
            {
                CLBConceptosExistentes.SetItemChecked(i, false);
            }
        }

        private void CheckAllListCLBExistentes()
        {
            for (int i = 0; i < CLBConceptosExistentes.Items.Count; i++)
            {
                CLBConceptosExistentes.SetItemChecked(i, true);
            }
        }

        public SeleccionarConceptosReporteActualizarInventario()
        {
            InitializeComponent();

            if (Inventario.esAumentar.Equals(true))
            {
                itemsCLBConceptosExistentes = new string[] { "Producto", "Proveedor", "Unidades Compradas/Disminuidas", "Precio Compra", "Total Compras", "Precio Venta", "Stock Anterior", "Stock Actual", "Fecha de Operacion", "Comentarios" };
            }
            else
            {

                itemsCLBConceptosExistentes = new string[] { "Producto", "Proveedor", "Unidades Compradas/Disminuidas", "Precio Compra","Precio Venta", "Stock Anterior", "Stock Actual", "Fecha de Operacion", "Comentarios" };
            }
            

            CLBConceptosExistentes.Items.AddRange(itemsCLBConceptosExistentes);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!CLBConceptosExistentes.CheckedItems.Count.Equals(0))
            {
                Inventario.listaConceptosSeleccionados.Clear();
                for (int x = 0; x <= CLBConceptosExistentes.CheckedItems.Count - 1; x++)
                {
                    Inventario.listaConceptosSeleccionados.Add(CLBConceptosExistentes.CheckedItems[x].ToString());
                }
                unCheckAllListCLBExistentes();
                this.Close();
            }
            else if (CLBConceptosExistentes.CheckedItems.Count.Equals(0))
            {
                Inventario.listaConceptosSeleccionados.Clear();
                var cant = CLBConceptosExistentes.CheckedItems.Count;

                MessageBox.Show("Cantidad de conceptos seleccionados debe de ser mayor a: " + cant + "\nFavor de seleccionar al menos uno de los conceptos", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Inventario.Aceptar = true;
        }

        private void chkSelectAllOrNot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllOrNot.Checked.Equals(true))
            {
                CheckAllListCLBExistentes();
                chkSelectAllOrNot.Text = "Deseleccionar todas las opciones";
                chkSelectAllOrNot.Font = new Font(chkSelectAllOrNot.Font, System.Drawing.FontStyle.Bold);
                chkSelectOnly.Checked = false;
                chkSelectOnly.Enabled = false;
            }
            else if (chkSelectAllOrNot.Checked.Equals(false))
            {
                unCheckAllListCLBExistentes();
                chkSelectAllOrNot.Text = "Seleccionar todas las opciones";
                chkSelectAllOrNot.Font = new Font(chkSelectAllOrNot.Font, System.Drawing.FontStyle.Regular);
                chkSelectOnly.Checked = true;
                chkSelectOnly.Enabled = true;
            }
        }

        private void SeleccionarConceptosReporteActualizarInventario_Load(object sender, EventArgs e)
        {
            chkSelectAllOrNot.Checked = true;
        }
    }
}
