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
    public partial class AgregarDescuentoDirecto : Form
    {
        public string TotalDescuento { get; set; }

        private string nombreProducto;
        private double precioProducto;

        public AgregarDescuentoDirecto(string[] datos)
        {
            InitializeComponent();

            this.nombreProducto = datos[0];
            this.precioProducto = Convert.ToDouble(datos[1]);
        }

        private void AgregarDescuentoDirecto_Load(object sender, EventArgs e)
        {
            lbProducto.Text = nombreProducto;
            lbPrecio.Text = "$" + precioProducto.ToString("0.00");

            txtCantidad.Focus();

            txtCantidad.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtPorcentaje.KeyPress += new KeyPressEventHandler(SoloDecimales);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var descuento = txtCantidad.Text;

            this.TotalDescuento = descuento;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                txtPorcentaje.Enabled = false;
                txtPorcentaje.Text = string.Empty;

                var cantidad = Convert.ToDouble(txtCantidad.Text);

                lbTotalDescuento.Text = cantidad.ToString("0.00");
            }
            else
            {
                txtPorcentaje.Enabled = true;
                lbTotalDescuento.Text = "0.00";
            }
        }

        private void txtPorcentaje_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPorcentaje.Text))
            {
                txtCantidad.Enabled = false;
                txtCantidad.Text = string.Empty;

                var porcentaje = Convert.ToDouble(txtPorcentaje.Text);

                if (porcentaje <= 100)
                {
                    var descuento = precioProducto * (porcentaje / 100);
                    lbTotalDescuento.Text = descuento.ToString("0.00");
                }
                else
                {
                    txtPorcentaje.Text = "100";
                    porcentaje = Convert.ToDouble(txtPorcentaje.Text);
                    var descuento = precioProducto * (porcentaje / 100);
                    lbTotalDescuento.Text = descuento.ToString("0.00");

                    txtPorcentaje.SelectionStart = txtPorcentaje.Text.Length;
                    txtPorcentaje.SelectionLength = 0;
                }
            }
            else
            {
                txtCantidad.Enabled = true;
                lbTotalDescuento.Text = "0.00";
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
