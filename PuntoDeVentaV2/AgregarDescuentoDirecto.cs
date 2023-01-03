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
    public partial class AgregarDescuentoDirecto : Form
    {
        public string TotalDescuento { get; set; }
        public int TipoDescuento { get; set; }

        private int idProducto;
        private string nombreProducto;
        private double precioProducto;
        private double cantidadProducto;

        public AgregarDescuentoDirecto(string[] datos)
        {
            InitializeComponent();

            this.idProducto = Convert.ToInt32(datos[0]);
            this.nombreProducto = datos[1];
            this.precioProducto = Convert.ToDouble(datos[2]);
            this.cantidadProducto = Convert.ToDouble(datos[3]);
        }

        private void AgregarDescuentoDirecto_Load(object sender, EventArgs e)
        {
            lbTotalFinal.Text = precioProducto.ToString("0.00");
            lbProducto.Text = nombreProducto;
            lbPrecio.Text = "Precio: $" + precioProducto.ToString("0.00");
            lbCantidadProducto.Text = "Cantidad: " + cantidadProducto;

            txtCantidad.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtPorcentaje.KeyPress += new KeyPressEventHandler(SoloDecimales);

            txtCantidad.Focus();

            //==============================================================
            if (Ventas.descuentosDirectos.ContainsKey(idProducto))
            {
                var tipo = Ventas.descuentosDirectos[idProducto].Item1;
                var cantidad = Ventas.descuentosDirectos[idProducto].Item2;

                if (tipo == 1)
                {
                    txtCantidad.Text = cantidad.ToString("N2");
                    txtCantidad.Select(txtCantidad.Text.Length, 0);
                    txtCantidad_KeyUp(sender, new KeyEventArgs(Keys.Up));
                }

                if (tipo == 2)
                {
                    txtPorcentaje.Text = cantidad.ToString("N2");
                    txtPorcentaje.Select(txtPorcentaje.Text.Length, 0);
                    txtPorcentaje_KeyUp(sender, new KeyEventArgs(Keys.Up));
                }
            }
            if (Ventas.SeCambioCantidad == true)
            {
                btnAceptar.PerformClick();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var descuento = Convert.ToDouble(lbTotalDescuento.Text);

            // Esto es para guardar cual campo es el que aplico el descuento y la cantidad
            // ya sea del porcentaje aplicado o un total en especifico
            var tipo = 0;
            var cantidad = txtCantidad.Text;
            var porcentaje = txtPorcentaje.Text;
            var cantidadElegida = 0f;

            if (!string.IsNullOrWhiteSpace(cantidad))
            {
                tipo = 1;
                cantidadElegida = float.Parse(cantidad);
            }

            if (!string.IsNullOrWhiteSpace(porcentaje))
            {
                tipo = 2;
                cantidadElegida = float.Parse(porcentaje);
                
                
                porcentaje = $" - {porcentaje}%";
            }

            // Guardamos los datos en el diccionario de Ventas para el momento en que se quiera editar
            // el descuento de uno de los productos de la lista
            if (Ventas.descuentosDirectos.ContainsKey(idProducto))
            {
                Ventas.descuentosDirectos[idProducto] = Tuple.Create(tipo, cantidadElegida); 
            }
            else
            {
                Ventas.descuentosDirectos.Add(idProducto, new Tuple<int, float>(tipo, cantidadElegida));
            }  

            this.TotalDescuento = lbTotalDescuento.Text + porcentaje;
            this.TipoDescuento = tipo;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                lbCantidadProducto.Visible = true;
                txtPorcentaje.Enabled = false;
                txtPorcentaje.Text = string.Empty;

                if (txtCantidad.Text.Equals("."))
                {
                    txtCantidad.Text = "0.";
                    txtCantidad.Select(txtCantidad.Text.Length, 0);
                }
                else
                {
                    var cantidad = Convert.ToDouble(txtCantidad.Text);

                    if (cantidad == 0)
                    {
                        btnEliminar.PerformClick();
                        return;
                    }

                    if (cantidad < (precioProducto * cantidadProducto))
                    {
                        lbTotalDescuento.Text = cantidad.ToString("0.00");
                        lbTotalFinal.Text = ((precioProducto * cantidadProducto) - cantidad).ToString("0.00");
                    }
                    else
                    {
                        txtCantidad.Text = ((precioProducto * cantidadProducto) - 1).ToString("0.00");
                        cantidad = Convert.ToDouble(txtCantidad.Text);
                        lbTotalDescuento.Text = cantidad.ToString("0.00");
                        lbTotalFinal.Text = ((precioProducto * cantidadProducto) - cantidad).ToString("0.00");

                        txtCantidad.SelectionStart = txtCantidad.Text.Length;
                        txtCantidad.SelectionLength = 0;
                    }
                }
            }
            else
            {
                txtPorcentaje.Enabled = true;
                lbTotalDescuento.Text = "0.00";
                lbTotalFinal.Text = precioProducto.ToString("0.00");
                lbCantidadProducto.Visible = false;
            }
        }

        private void txtPorcentaje_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPorcentaje.Text.Equals("."))
            {
                txtPorcentaje.Text = "0.";
                txtPorcentaje.Select(txtPorcentaje.Text.Length, 0);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtPorcentaje.Text))
                {
                    lbCantidadProducto.Visible = true;
                    txtCantidad.Enabled = false;
                    txtCantidad.Text = string.Empty;

                    var porcentaje = Convert.ToDouble(txtPorcentaje.Text);

                    //if (porcentaje == 0)
                    //{
                    //    btnEliminar.PerformClick();
                    //    return;
                    //}

                    if (porcentaje < 100)
                    {
                        var descuento = (precioProducto * cantidadProducto) * (porcentaje / 100);
                        lbTotalDescuento.Text = descuento.ToString("0.00");
                        lbTotalFinal.Text = ((precioProducto * cantidadProducto) - descuento).ToString("0.00");
                    }
                    else
                    {
                        txtPorcentaje.Text = "99";
                        porcentaje = Convert.ToDouble(txtPorcentaje.Text);
                        var descuento = (precioProducto * cantidadProducto) * (porcentaje / 100);
                        lbTotalDescuento.Text = descuento.ToString("0.00");
                        lbTotalFinal.Text = ((precioProducto * cantidadProducto) - descuento).ToString("0.00");

                        txtPorcentaje.SelectionStart = txtPorcentaje.Text.Length;
                        txtPorcentaje.SelectionLength = 0;
                    }
                }
                else
                {
                    txtCantidad.Enabled = true;
                    lbTotalDescuento.Text = "0.00";
                    lbTotalFinal.Text = precioProducto.ToString("0.00");
                    lbCantidadProducto.Visible = false;
                }
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            // Permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // Verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtPorcentaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //if (Ventas.descuentosDirectos.ContainsKey(idProducto))
            //{
            //    Ventas.descuentosDirectos.Remove(idProducto);
            //    txtCantidad.Text = string.Empty;
            //    txtCantidad.Enabled = true;
            //    txtPorcentaje.Text = string.Empty;
            //    txtPorcentaje.Enabled = true;
            //    lbCantidadProducto.Visible = false;
            //    lbTotalDescuento.Text = "0.00";
            //    lbTotalFinal.Text = "0.00";
            //}

            lbTotalDescuento.Text = "0.00";
            Ventas.descuentosDirectos.Remove(idProducto);
            var descuento = Convert.ToDouble(lbTotalDescuento.Text);

            // Esto es para guardar cual campo es el que aplico el descuento y la cantidad
            // ya sea del porcentaje aplicado o un total en especifico
            var tipo = 0;
            var cantidad = txtCantidad.Text;
            var porcentaje = "0";
            var cantidadElegida = 0f;

            if (!string.IsNullOrWhiteSpace(cantidad))
            {
                tipo = 1;
                cantidadElegida = float.Parse(cantidad);
            }

            if (!string.IsNullOrWhiteSpace(porcentaje))
            {
                tipo = 2;
                cantidadElegida = float.Parse(porcentaje);
                porcentaje = $"";
            }

            // Guardamos los datos en el diccionario de Ventas para el momento en que se quiera editar
            // el descuento de uno de los productos de la lista
            if (Ventas.descuentosDirectos.ContainsKey(idProducto))
            {
                Ventas.descuentosDirectos[idProducto] = Tuple.Create(tipo, cantidadElegida);
            }
            else
            {
                Ventas.descuentosDirectos.Add(idProducto, new Tuple<int, float>(tipo, cantidadElegida));
            }

            this.TotalDescuento = lbTotalDescuento.Text + porcentaje;
            this.TipoDescuento = tipo;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            string[] words;

            if (txtCantidad.Text.Equals(""))
            {
                txtCantidad.Text = "0";
            }
            else if (!txtCantidad.Text.Equals(""))
            {
                words = txtCantidad.Text.Split('.');
                if (words[0].Equals(""))
                {
                    words[0] = "0";
                }
                if (words.Length > 1)
                {
                    if (words[1].Equals(""))
                    {
                        words[1] = "0";
                    }
                    txtCantidad.Text = words[0] + "." + words[1];
                }
            }
        }
    }
}
