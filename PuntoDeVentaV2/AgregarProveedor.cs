using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarProveedor : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        //tipo 1 = agregar
        //tipo 2 = editar
        private int tipo = 0;
        private int idProveedor = 0;
        public static string editarAgregar = "editar";

        public AgregarProveedor(int tipo = 1, int idProveedor = 0)
        {
            InitializeComponent();

            this.tipo = tipo;
            this.idProveedor = idProveedor;
        }

        private void AgregarProveedor_Load(object sender, EventArgs e)
        {
            //El titulo que se mostrara al abrir el form
            if (tipo == 1)
            {
                this.Text = "Nuevo Proveedor";
                //tituloSeccion.Text = "NUEVO PROVEEDOR";
            }
            else
            {
                this.Text = "Editar Proveedor";
                //tituloSeccion.Text = "EDITAR PROVEEDOR";
            }

            //Si viene de la opcion editar buscamos los datos actuales del proveedor
            if (tipo == 2)
            {
                var proveedor = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);

                CargarDatosProveedor(proveedor);
            }


            //foreach (Control control in this.Controls)
            //{
            //    control.PreviewKeyDown += new PreviewKeyDownEventHandler(AgregarProveedor_PreviewKeyDown);
            //}
            AgregarProveedor form = this;
            Utilidades.EjecutarAtajoKeyPreviewDown(AgregarProveedor_PreviewKeyDown, form);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool YaExiste = false;
            using (var ConsultaNombre = cn.CargarDatos($"SELECT Nombre FROM proveedores WHERE `Status` = 1 AND IDUsuario = {FormPrincipal.userID}"))
            {
                if (editarAgregar == "agregar")
                {
                  foreach (DataRow Nombres in ConsultaNombre.Rows)
                  {

                        string unNombre = Nombres[0].ToString();
                        if (txtNombre.Text.Equals(unNombre))
                        {
                        YaExiste = true;
                        MessageBox.Show("Ya existe un proveedor Registrado con ese Nombre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtNombre.SelectAll();
                            txtNombre.Focus();
                        }
                       
                  }
                }

                if (YaExiste.Equals(false))
                {
                    var nombre = txtNombre.Text;
                    var rfc = txtRFC.Text;
                    var calle = txtCalle.Text;
                    var noExt = txtNoExterior.Text;
                    var noInt = txtNoInterior.Text;
                    var colonia = txtColonia.Text;
                    var municipio = txtMunicipio.Text;
                    var estado = txtEstado.Text;
                    var cp = txtCodigoPostal.Text;
                    var email = txtEmail.Text;
                    var telefono = txtTelefono.Text;
                    var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        MessageBox.Show("Ingrese un nombre para el proveedor", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var cantidadCamposRFC = rfc.Length;

                    if (cantidadCamposRFC > 0 && cantidadCamposRFC < 12)
                    {
                        txtRFC.Focus();
                        MessageBox.Show("El RFC no tiene el formato correcto", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtRFC.ForeColor = Color.Red;
                        //txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                        return;
                    }
                    else
                    {
                        txtRFC.ForeColor = Color.Black;
                        txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Regular);

                    }

                    string[] datos = new string[] { FormPrincipal.userID.ToString(), nombre, rfc, calle, noExt, noInt, colonia, municipio, estado, cp, email, telefono, fechaOperacion, idProveedor.ToString() };

                    if (tipo == 1)
                    {
                        //Insertar
                        int respuesta = cn.EjecutarConsulta(cs.GuardarProveedor(datos));

                        if (respuesta > 0)
                        {
                            Close();
                        }
                    }
                    else
                    {
                        //Actualizar
                        int respuesta = cn.EjecutarConsulta(cs.GuardarProveedor(datos, 1));

                        if (respuesta > 0)
                        {
                            Close();
                        }
                    }
                }
            }  
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarDatosProveedor(string[] datos)
        {
            txtNombre.Text = datos[0];
            txtRFC.Text = datos[1];
            txtCalle.Text = datos[2];
            txtNoExterior.Text = datos[3];
            txtNoInterior.Text = datos[4];
            txtColonia.Text = datos[5];
            txtMunicipio.Text = datos[6];
            txtEstado.Text = datos[7];
            txtCodigoPostal.Text = datos[8];
            txtEmail.Text = datos[9];
            txtTelefono.Text = datos[10];
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void AgregarProveedor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtRFC_KeyPress(object sender, KeyPressEventArgs e)
        {
            var cantidadCamposRFC = txtRFC.Text.Length;

            //Es hasta el 13 pero se pone 12 por que el dijito ya fue ingresado (Solo permitir 13 digitos)
            if (cantidadCamposRFC > 12)
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }else if (Char.IsNumber(e.KeyChar))
                {
                    e.Handled = true;

                }          
            }
            else
            {
                e.Handled = false;                
            }
            
            
        }

        private void txtRFC_Leave(object sender, EventArgs e)
        {
            ////var cantidadCamposRFC = txtRFC.Text.Length;

            ////if (cantidadCamposRFC > 0 && cantidadCamposRFC < 12)
            ////{
            ////    txtRFC.Focus();
            ////    MessageBox.Show("El RFC no tiene el formato correcto", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ////    txtRFC.ForeColor = Color.Red;
            ////    txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            ////}
            ////else
            ////{
            ////    txtRFC.ForeColor = Color.Black;
            ////    txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Regular);

            ////}
        }

        private void AgregarProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRFC_TextChanged_1(object sender, EventArgs e)
        {
            var cantidadCamposRFC = txtRFC.Text.Length;


            if (cantidadCamposRFC > 11 )
            {
                txtRFC.ForeColor = Color.Black;
                txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            }
            else
            {
                txtRFC.ForeColor = Color.Red;
               // txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ValidarEntradaDeTexto(sender, e);
        }
        private void ValidarEntradaDeTexto(object sender, EventArgs e)
        {
            var resultado = string.Empty;
            var txtValidarTexto = (TextBox)sender;
            resultado = txtValidarTexto.Text;

            if (!string.IsNullOrWhiteSpace(resultado))
            {
                if (resultado.Contains("|") || resultado.Contains("+") || resultado.Contains("-"))
                {
                    var resultadoAuxialiar = Regex.Replace(resultado, @"[+\|\-]", string.Empty);
                    resultado = resultadoAuxialiar;
                    txtValidarTexto.Text = resultado;
                    txtValidarTexto.Focus();
                    txtValidarTexto.Select(txtValidarTexto.Text.Length, 0);
                }
            }
            else
            {
                txtValidarTexto.Focus();
            }
        }
    }
}
