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
    public partial class AgregarTipoCliente : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int id = 0;
        private int tipo = 0;
        int calcu = 0;
        // Tipo 1 = Agregar
        // Tipo 2 = Editar

        public AgregarTipoCliente(int id = 0, int tipo = 1)
        {
            InitializeComponent();

            this.id = id;
            this.tipo = tipo;
        }

        private void AgregarTipoCliente_Load(object sender, EventArgs e)
        {
            if (tipo == 2)
            {
                this.Text = "PUDVE - Editar tipo Cliente";

                var datos = mb.ObtenerTipoCliente(id);

                if (datos.Length > 0)
                {
                    txtNombre.Text = datos[0];
                    txtDescuento.Text = datos[1];
                }
            }

            txtDescuento.KeyPress += new KeyPressEventHandler(SoloDecimales);

            AgregarTipoCliente form = this;
            Utilidades.EjecutarAtajoKeyPreviewDown(AgregarTipoCliente_PreviewKeyDown, form);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var nombre = txtNombre.Text.Trim();
            var descuento = txtDescuento.Text.Trim();

            
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("El nombre para tipo de cliente es obligatorio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(descuento))
                {
                    MessageBox.Show("Ingrese la cantidad de porcentaje para descuento", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescuento.Focus();
                    return;
                }

            if (descuento.Equals("."))
            {
                txtDescuento.Text = string.Empty;
                txtDescuento.Focus();
                MessageBox.Show("Dato incorrecto, ingrese valores numericos", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var consulta = string.Empty;

                if (tipo == 1)
                {
                    consulta = $"INSERT INTO TipoClientes (IDUsuario, Nombre, DescuentoPorcentaje, FechaOperacion) VALUES ('{FormPrincipal.userID}', '{nombre}', '{descuento}', '{fechaOperacion}')";
                }

                if (tipo == 2)
                {
                    consulta = $"UPDATE TipoClientes SET Nombre = '{nombre}', DescuentoPorcentaje = '{descuento}', FechaOperacion = '{fechaOperacion}' WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID}";
                }


                var resultado = cn.EjecutarConsulta(consulta);

                if (resultado > 0)
                {
                    Close();
                }
                else
                {
                    var operacion = "registrar";

                    if (tipo == 2)
                    {
                        operacion = "actualizar";
                    }

                    MessageBox.Show($"Ha ocurrido un error al {operacion} el tipo de cliente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void AgregarTipoCliente_Shown(object sender, EventArgs e)
        {
            // Editar
            if (tipo == 2)
            {
                txtNombre.Select(txtNombre.Text.Length, 0);
                txtNombre.Focus();
            }
        }

        private void txtDescuento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
            else if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarTipoCliente_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
            
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarTipoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {
            decimal Descuento;
            if (string.IsNullOrWhiteSpace(txtDescuento.Text))
            {
                Descuento = 0;
            }
            else
            {
                if (!decimal.TryParse(txtDescuento.Text.Trim(), out Descuento))
                {
                    MessageBox.Show("El valor introducido no es valido", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescuento.Focus();
                    txtDescuento.Clear();
                    return;
                }
            }
            if (Descuento>99)
            {
                txtDescuento.Text = "99";
            }
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txtDescuento.Text = calculadora.lCalculadora.Text;
                        }
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }

                    //if ()
                    //{
                    //    txtStockMaximo.Text = calculadora.lCalculadora.Text;
                    //}
                }
            }
        }
    }
}
