﻿using System;
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
        int index = 0;
        string[] itemsCLBConceptosExistentes;
        List<string> listaConceptos;

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

            itemsCLBConceptosExistentes = new string[] { "Producto", "Proveedor", "Unidades Compradas/Disminuidas", "Precio Compra", "Precio Venta", "Stock Anterior", "Stock Actual", "Fecha de Compra", "Fecha de Operacion", "Comentarios" };

            CLBConceptosExistentes.Items.AddRange(itemsCLBConceptosExistentes);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            listaConceptos = new List<string>();

            if (!CLBConceptosExistentes.CheckedItems.Count.Equals(0))
            {
                for (int x = 0; x <= CLBConceptosExistentes.CheckedItems.Count - 1; x++)
                {
                    listaConceptos.Add(CLBConceptosExistentes.CheckedItems[x].ToString());
                }
            }
            else if (CLBConceptosExistentes.CheckedItems.Count.Equals(0))
            {
                var cant = CLBConceptosExistentes.CheckedItems.Count;

                MessageBox.Show("Cantidad de conceptos seleccionados debe de ser mayor a: " + cant + "\nFavor de seleccionar al menos uno de los conceptos", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkSelectAllOrNot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAllOrNot.Checked.Equals(true))
            {
                CheckAllListCLBExistentes();
            }
            else if (chkSelectAllOrNot.Checked.Equals(false))
            {
                unCheckAllListCLBExistentes();
            }
        }
    }
}
