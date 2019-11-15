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
    public partial class AsignarPropiedad : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        string propiedad = string.Empty;

        Dictionary<int, string> productos;

        public AsignarPropiedad(object propiedad)
        {
            InitializeComponent();

            this.propiedad = propiedad.ToString();

            productos = Productos.productosSeleccionados;
        }

        private void AsignarPropiedad_Load(object sender, EventArgs e)
        {
            lbNombrePropiedad.Text = $"ASIGNAR {propiedad.ToUpper()}";

            CargarPropiedad();
        }

        private void CargarPropiedad()
        {
            Font fuente = new Font("Century Gothic", 10.0f);
            
            if (propiedad == "Stock")
            {
                TextBox tbStock = new TextBox();
                tbStock.Name = "tb" + propiedad;
                tbStock.Width = 200;
                tbStock.Height = 20;
                tbStock.TextAlign = HorizontalAlignment.Center;
                tbStock.Font = fuente;
                tbStock.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbStock.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbStock);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarStock"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarStock"));
            }
            else if (propiedad == "Precio")
            {
                TextBox tbPrecio = new TextBox();
                tbPrecio.Name = "tb" + propiedad;
                tbPrecio.Width = 200;
                tbPrecio.Height = 20;
                tbPrecio.TextAlign = HorizontalAlignment.Center;
                tbPrecio.Font = fuente;
                tbPrecio.KeyPress += new KeyPressEventHandler(SoloDecimales);
                tbPrecio.Location = new Point(65, 70);

                panelContenedor.Controls.Add(tbPrecio);
                panelContenedor.Controls.Add(GenerarBoton(0, "cancelarPrecio"));
                panelContenedor.Controls.Add(GenerarBoton(1, "aceptarPrecio"));
            }
            else if (propiedad == "Proveedor")
            {
                var listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

                // Comprobamos que tenga proveedores
                if (listaProveedores.Length > 0)
                {
                    Dictionary<int, string> lista = new Dictionary<int, string>();

                    foreach (string proveedor in listaProveedores)
                    {
                        var info = proveedor.Split('-');

                        lista.Add(Convert.ToInt32(info[0].Trim()), info[1].Trim());
                    }

                    ComboBox cbProveedores = new ComboBox();
                    cbProveedores.Name = "cb" + propiedad;
                    cbProveedores.Width = 300;
                    cbProveedores.Height = 20;
                    cbProveedores.Font = fuente;
                    cbProveedores.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbProveedores.DataSource = lista.ToArray();
                    cbProveedores.DisplayMember = "Value";
                    cbProveedores.ValueMember = "Key";
                    cbProveedores.Location = new Point(15, 70);

                    panelContenedor.Controls.Add(cbProveedores);
                    panelContenedor.Controls.Add(GenerarBoton(0, "cancelarProveedor"));
                    panelContenedor.Controls.Add(GenerarBoton(1, "aceptarProveedor"));

                }
                else
                {
                    MessageBox.Show("No hay proveedores registrados", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Dispose();
                }
            }
            else
            {
                // Consulta para obtener todas las opciones registradas para esa propiedad
                var opciones = mb.ObtenerOpcionesPropiedad(FormPrincipal.userID, propiedad);

                if (opciones.Length > 0)
                {
                    // Aqui van todos los que son dinamicos agregados en detalle de producto
                    ComboBox cbPropiedad = new ComboBox();
                    cbPropiedad.Name = "cb" + propiedad;
                    cbPropiedad.Width = 300;
                    cbPropiedad.Height = 20;
                    cbPropiedad.Font = fuente;
                    cbPropiedad.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbPropiedad.Items.AddRange(opciones);
                    cbPropiedad.SelectedIndex = 0;
                    cbPropiedad.Location = new Point(15, 70);

                    panelContenedor.Controls.Add(cbPropiedad);
                    panelContenedor.Controls.Add(GenerarBoton(0, "cancelar" + propiedad));
                    panelContenedor.Controls.Add(GenerarBoton(1, "aceptar" + propiedad));
                }
                else
                {
                    MessageBox.Show("No hay opciones registradas para la propiedad " + propiedad, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Dispose();
                }
            }
        }

        private Button GenerarBoton(int tipo, string nombre)
        {
            Button boton = new Button();
            Font fuenteBoton = new Font("Century Gothic", 9.5f);     

            if (tipo == 0)
            {
                Button btnCancelar = new Button();
                btnCancelar.Text = "Cancelar";
                btnCancelar.Name = nombre;
                btnCancelar.BackColor = Color.FromArgb(192, 0, 0);
                btnCancelar.ForeColor = Color.White;
                btnCancelar.FlatStyle = FlatStyle.Flat;
                btnCancelar.Cursor = Cursors.Hand;
                btnCancelar.Font = fuenteBoton;
                btnCancelar.Width = 95;
                btnCancelar.Height = 25;
                btnCancelar.Click += new EventHandler(botonCancelar_Click);
                btnCancelar.Location = new Point(65, 125);

                boton = btnCancelar;
            }
            
            if (tipo == 1)
            {
                Button btnAceptar = new Button();
                btnAceptar.Text = "Aceptar";
                btnAceptar.Name = nombre;
                btnAceptar.BackColor = Color.Green;
                btnAceptar.ForeColor = Color.White;
                btnAceptar.FlatStyle = FlatStyle.Flat;
                btnAceptar.Cursor = Cursors.Hand;
                btnAceptar.Font = fuenteBoton;
                btnAceptar.Width = 95;
                btnAceptar.Height = 25;
                btnAceptar.Click += new EventHandler(botonAceptar_Click);
                btnAceptar.Location = new Point(170, 125);

                boton = btnAceptar;
            }

            return boton;
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            Button boton = sender as Button;

            string[] datos;

            if (propiedad == "Stock")
            {
                TextBox txtStock = (TextBox)this.Controls.Find("tbStock", true)[0];

                var stock = txtStock.Text;

                if (!string.IsNullOrWhiteSpace(stock))
                {
                    foreach (var producto in productos)
                    {
                        if (producto.Value == "P")
                        {
                            datos = new string[] { producto.Key.ToString(), stock, FormPrincipal.userID.ToString() };

                            cn.EjecutarConsulta(cs.ActualizarStockProductos(datos));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad para stock", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    
            }
            else if (propiedad == "Precio")
            {
                TextBox txtPrecio = (TextBox)this.Controls.Find("tbPrecio", true)[0];

                var precioTmp = txtPrecio.Text;

                if (!string.IsNullOrWhiteSpace(precioTmp))
                {
                    var precio = float.Parse(precioTmp);

                    foreach (var producto in productos)
                    {
                        cn.EjecutarConsulta(cs.SetUpPrecioProductos(producto.Key, precio, FormPrincipal.userID));
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el precio para asignar", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (propiedad == "Proveedor")
            {
                // Acceder al combobox de proveedores
                ComboBox combo = (ComboBox)this.Controls.Find("cbProveedor", true)[0];

                var idProveedor = Convert.ToInt32(combo.SelectedValue.ToString());
                var proveedor = combo.Text;
                
                foreach (var producto in productos)
                {
                    // Comprobar si existe registro en la tabla DetallesProducto
                    var existe = mb.DetallesProducto(producto.Key, FormPrincipal.userID);

                    datos = new string[] {
                            producto.Key.ToString(), FormPrincipal.userID.ToString(),
                            proveedor, idProveedor.ToString()
                    };

                    if (existe.Length > 0)
                    {
                        // Hacemos un UPDATE
                        cn.EjecutarConsulta(cs.GuardarProveedorProducto(datos, 1));
                    }
                    else
                    {
                        // Hacemos un INSERT
                        cn.EjecutarConsulta(cs.GuardarProveedorProducto(datos));
                    }
                }
            }
            else
            {
                ComboBox combo = (ComboBox)this.Controls.Find("cb" + propiedad, true)[0];

                MessageBox.Show(combo.SelectedItem.ToString());
            }

            Dispose();
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            Dispose();
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
