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
    public partial class EditarMensajesParaEnviar : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        string mensaje;
        string cantidadDeCompra;
        private const char SignoDecimal = '.';
        public EditarMensajesParaEnviar()
        {
            InitializeComponent();
        }

        private void EditarMensajesParaEnviar_Load(object sender, EventArgs e)
        {
            cargarForm(AgregarEditarProducto.nombreProductoEditar.ToString());
            cargarEstadoCheckboxMensaje();
        }

        private void cargarForm(string _lbNombreProducto)
        {
            var dato = MensajeVentasYMensajeInventario.enviarDato;
            if (dato == "mensajeVentas")
            {
                if (Productos.dobleClickProducto == 1)
                {
                    using (var datos = cn.CargarDatos(cs.mensajeVentas(Productos.idprodDobleClick)))
                    {
                        if (!datos.Rows.Count.Equals(0))
                        {
                            mensaje = Convert.ToString(datos.Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            mensaje = "";
                        }
                    }

                    using (var datos = cn.CargarDatos(cs.cantidadCompraMinima(Productos.idprodDobleClick)))
                    {
                        if (!datos.Rows.Count.Equals(0))
                        {
                            cantidadDeCompra = Convert.ToString(datos.Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            cantidadDeCompra = "";
                        }
                    }
                }
                else
                {
                    using (var datos = cn.CargarDatos(cs.mensajeVentas(Productos.codProductoEditarVenta)))
                    {
                        if (!datos.Rows.Count.Equals(0))
                        {
                            mensaje = Convert.ToString(datos.Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            mensaje = "";
                        }
                    }

                    using (var datos = cn.CargarDatos(cs.cantidadCompraMinima(Productos.codProductoEditarVenta)))
                    {
                        if (!datos.Rows.Count.Equals(0))
                        {
                            cantidadDeCompra = Convert.ToString(datos.Rows[0].ItemArray[0]);
                        }
                        else
                        {
                            cantidadDeCompra = "";
                        }
                    }
                }
               

                this.Height = 281;

                FlowLayoutPanel flpDatos = new FlowLayoutPanel();
                flpDatos.Name = "flpDatos";
                flpDatos.Dock = DockStyle.Top; 
                flpDatos.Height = 69;
                
                Panel panelDatos = new Panel();
                panelDatos.Name = "panelDatos";
                panelDatos.Width = 320;
                panelDatos.Height = 59;
                panelDatos.Dock = DockStyle.Top;

                Label lbNombreProducto = new Label();
                lbNombreProducto.Name = "lbNombreProducto";
                lbNombreProducto.Text = _lbNombreProducto;
                lbNombreProducto.AutoSize = false;
                lbNombreProducto.Width = panelDatos.Width;
                lbNombreProducto.Location = new Point(7, 6);

                Label lbCantidadCompra = new Label();
                lbCantidadCompra.Text = "Cantidad minima en la venta para mostrar mensaje:";
                lbCantidadCompra.AutoSize = true;
                lbCantidadCompra.Location = new Point(7, 36);

                TextBox txtCantidadCompra = new TextBox();
                txtCantidadCompra.Name = "txtCantidadCompra";
                txtCantidadCompra.Text = cantidadDeCompra;
                txtCantidadCompra.KeyPress += new KeyPressEventHandler(txtCantidadCompra_KeyPress);
                txtCantidadCompra.Width = 35;
                txtCantidadCompra.Height = 20;
                txtCantidadCompra.Location = new Point(270,33);
                txtCantidadCompra.ShortcutsEnabled = false;

                panelDatos.Controls.Add(lbNombreProducto);
                panelDatos.Controls.Add(lbCantidadCompra);
                panelDatos.Controls.Add(txtCantidadCompra);
                flpDatos.Controls.Add(panelDatos);
               
                //Panel para modificar el mensaje------------------------------------------------------

                FlowLayoutPanel flpMensaje = new FlowLayoutPanel();
                flpMensaje.Name = "flpMensaje";
                flpMensaje.Dock = DockStyle.Top;
                flpMensaje.Height = 118;

                Panel panelMensaje = new Panel();
                panelMensaje.Name = "panelMensaje";
                panelMensaje.Width = 320;
                panelMensaje.Height = 108;
                panelMensaje.Dock = DockStyle.Top;

                Label lbMensajeActual = new Label();
                lbMensajeActual.Text = "Mensaje Actual:";
                lbMensajeActual.AutoSize = true;
                lbMensajeActual.Location = new Point(7, 6);

                CheckBox chkMostrarMensaje = new CheckBox();
                chkMostrarMensaje.Text = "Mostrar Mensaje";
                chkMostrarMensaje.Name = "chkMostrarMensajeVenta";
                chkMostrarMensaje.Checked = true;
                chkMostrarMensaje.AutoCheck = true;
                chkMostrarMensaje.Location = new Point(190, 3);
                chkMostrarMensaje.CheckedChanged += new EventHandler(cbxN_CheckedChanged);

                TextBox txtNuevoMensaje = new TextBox();
                txtNuevoMensaje.Name = "txtMensaje";
                txtNuevoMensaje.Text = mensaje;
                txtNuevoMensaje.Multiline = true;
                txtNuevoMensaje.Width = 299;
                txtNuevoMensaje.Height = 79;
                txtNuevoMensaje.Location = new Point(7, 25);

                panelMensaje.Controls.Add(lbMensajeActual);
                panelMensaje.Controls.Add(txtNuevoMensaje);
                panelMensaje.Controls.Add(chkMostrarMensaje);
                flpMensaje.Controls.Add(panelMensaje);

                //Panel para botones------------------------------------------------------------------

                FlowLayoutPanel flBotones = new FlowLayoutPanel();
                flBotones.Dock = DockStyle.Top;
                flBotones.Height = 53;

                Panel panelBotones = new Panel();
                panelBotones.Name = "panelBotones";
                panelBotones.Width = 320;
                panelBotones.Height = 108;
                panelBotones.Dock = DockStyle.Top;

                Button botonConfirmar = new Button();
                botonConfirmar.Name = "btnConfirmar";
                botonConfirmar.Text = "Confirmar";
                botonConfirmar.Width = 119;
                botonConfirmar.Height = 37;
                botonConfirmar.Click += new EventHandler(botonConfirmar_click);
                botonConfirmar.Cursor = Cursors.Hand;
                botonConfirmar.Location = new Point(17,3);

                Button botonCancelar = new Button();
                botonCancelar.Name = "btnCancelar";
                botonCancelar.Text = "Cancelar";
                botonCancelar.Width = 119;
                botonCancelar.Height = 37;
                botonCancelar.Click += new EventHandler(botonCancelar_click);
                botonCancelar.Cursor = Cursors.Hand;
                botonCancelar.Location = new Point(180, 3);

                panelBotones.Controls.Add(botonConfirmar);
                panelBotones.Controls.Add(botonCancelar);
                flBotones.Controls.Add(panelBotones);


                this.Controls.Add(flBotones);
                this.Controls.Add(flpMensaje);
                this.Controls.Add(flpDatos);
            }
            else if (dato == "mensajeInventario") //EN caso de dar en el boton mensaje inventario--------------------//Panel para el nombre del producto a modificar
            {
                if (Productos.dobleClickProducto == 1)
                {
                    using (var datos = cn.CargarDatos(cs.mensajeInventario(Productos.idprodDobleClick)))
                    {
                        if (!datos.Rows.Count.Equals(0))
                        {
                            mensaje = Convert.ToString(datos.Rows[0].ItemArray[0]);
                        }
                    }
                }
                else
                {
                    using (var datos = cn.CargarDatos(cs.mensajeInventario(Productos.codProductoEditarInventario)))
                    {
                        if (!datos.Rows.Count.Equals(0))
                        {
                            mensaje = Convert.ToString(datos.Rows[0].ItemArray[0]);
                        }
                    }
                }

                
                this.Height = 251;

                FlowLayoutPanel flpDatos = new FlowLayoutPanel();
                flpDatos.Name = "panelDatos";
                flpDatos.Dock = DockStyle.Top;
                flpDatos.Height = 33;

                Panel panelDatos = new Panel();
                panelDatos.Width = 320;
                panelDatos.Height = 27;
                panelDatos.Dock = DockStyle.Top;

                Label lbNombreProducto = new Label();
                lbNombreProducto.Name = "lbNombreProducto";
                lbNombreProducto.Text = _lbNombreProducto;
                lbNombreProducto.AutoSize = false;
                lbNombreProducto.Width = panelDatos.Width;
                lbNombreProducto.Location = new Point(7, 6);

                panelDatos.Controls.Add(lbNombreProducto);
                flpDatos.Controls.Add(panelDatos);

                //Panel para modificar el mensaje------------------------------------------------------

                FlowLayoutPanel flpMensaje = new FlowLayoutPanel();
                flpMensaje.Name = "flplMensaje";
                flpMensaje.Dock = DockStyle.Top;
                flpMensaje.Height = 118;

                Panel panelMensaje = new Panel();
                panelMensaje.Name = "panelDatos";
                panelMensaje.Width = 320;
                panelMensaje.Height = 108;
                panelMensaje.Dock = DockStyle.Top;

                Label lbMensajeActual = new Label();
                lbMensajeActual.Text = "Mensaje Actual:";
                lbMensajeActual.AutoSize = true;
                lbMensajeActual.Location = new Point(7, 6);

                TextBox txtNuevoMensaje = new TextBox();
                txtNuevoMensaje.Name = "txtMensaje";
                txtNuevoMensaje.Text = mensaje;
                txtNuevoMensaje.Multiline = true;
                txtNuevoMensaje.Width = 299;
                txtNuevoMensaje.Height = 70;
                txtNuevoMensaje.Location = new Point(7, 30);

                CheckBox chkMostrarMensaje = new CheckBox();
                chkMostrarMensaje.Text = "Mostrar Mensaje";
                chkMostrarMensaje.Name = "chkMostrarMensajeInventario";
                chkMostrarMensaje.Location = new Point(190, 3);
                chkMostrarMensaje.CheckedChanged += new EventHandler(chkEstado_CheckedChanged);

                panelMensaje.Controls.Add(chkMostrarMensaje);
                panelMensaje.Controls.Add(lbMensajeActual);
                panelMensaje.Controls.Add(txtNuevoMensaje);
                flpMensaje.Controls.Add(panelMensaje);

                //Panel para botones------------------------------------------------------------------

                FlowLayoutPanel flpBotones = new FlowLayoutPanel();
                flpBotones.Name = "panelBotones";
                flpBotones.Dock = DockStyle.Top;
                flpBotones.Height = 53;

                Panel panelBotones = new Panel();
                panelBotones.Width = 320;
                panelBotones.Height = 108;
                panelBotones.Dock = DockStyle.Top;

                Button botonConfirmar = new Button();
                botonConfirmar.Name = "btnConfirmar";
                botonConfirmar.Text = "Confirmar";
                botonConfirmar.Width = 119;
                botonConfirmar.Height = 37;
                botonConfirmar.Click += new EventHandler(botonConfirmar_click);
                botonConfirmar.Cursor = Cursors.Hand;
                botonConfirmar.Location = new Point(17, 3);

                Button botonCancelar = new Button();
                botonCancelar.Name = "btnCancelar";
                botonCancelar.Text = "Cancelar";
                botonCancelar.Width = 119;
                botonCancelar.Height = 37;
                botonCancelar.Click += new EventHandler(botonCancelar_click);
                botonCancelar.Cursor = Cursors.Hand;
                botonCancelar.Location = new Point(180, 3);

                panelBotones.Controls.Add(botonConfirmar);
                panelBotones.Controls.Add(botonCancelar);
                flpBotones.Controls.Add(panelBotones);


                this.Controls.Add(flpBotones);
                this.Controls.Add(flpMensaje);
                this.Controls.Add(flpDatos);
            }
        }

        private void txtCantidadCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            // Si el carácter pulsado no es un carácter válido se anula
            e.Handled = !char.IsDigit(e.KeyChar) // No es dígito
                        && !char.IsControl(e.KeyChar) // No es carácter de control (backspace)
                        && (e.KeyChar != SignoDecimal // No es signo decimal o es la 1ª posición o ya hay un signo decimal
                            || textBox.SelectionStart == 0
                            || textBox.Text.Contains(SignoDecimal));
        }

        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Name.Equals("chkMostrarMensajeInventario"))
            {
                if (checkbox.Checked.Equals(true))
                {
                    cn.EjecutarConsulta(cs.cambiarEstadoMensajeInventario(Productos.codProductoEditarVenta, 1));
                }
                else
                {
                    cn.EjecutarConsulta(cs.cambiarEstadoMensajeInventario(Productos.codProductoEditarVenta, 0));
                }
            }
        }

        private void cbxN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Name.Equals("chkMostrarMensajeVenta"))
            {
                if (checkbox.Checked.Equals(true))
                {
                    cn.EjecutarConsulta(cs.cambiarEstadoMensaje(Productos.codProductoEditarVenta,1));
                }
                else
                {
                    cn.EjecutarConsulta(cs.cambiarEstadoMensaje(Productos.codProductoEditarVenta, 0));
                }
            }
        }

        private void botonCancelar_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cargarEstadoCheckboxMensaje()
        {
            if (Productos.dobleClickProducto == 1)
            {
                using (DataTable dtPermiso = cn.CargarDatos(cs.verificarEstadoCheckbox(Productos.idprodDobleClick)))
                {
                    if (!dtPermiso.Rows.Count.Equals(0))
                    {
                        foreach (DataRow dtDataRow in dtPermiso.Rows)
                        {
                            foreach (Control item in this.Controls)
                            {
                                if (item is FlowLayoutPanel && item.Name.Equals("flpMensaje"))
                                {
                                    foreach (Control itemMensaje in item.Controls)
                                    {
                                        if (itemMensaje is Panel && itemMensaje.Name.Equals("panelMensaje"))
                                        {
                                            foreach (CheckBox chkMostrarMensaje in itemMensaje.Controls.OfType<CheckBox>())
                                            {
                                                if (chkMostrarMensaje is CheckBox && chkMostrarMensaje.Name.Equals("chkMostrarMensajeVenta"))
                                                {
                                                    chkMostrarMensaje.Checked = (Boolean)dtDataRow["ProductMessageActivated"];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (DataTable dtPermiso = cn.CargarDatos(cs.verificarEstadoCheckbox(Productos.codProductoEditarVenta)))
                {
                    if (!dtPermiso.Rows.Count.Equals(0))
                    {
                        foreach (DataRow dtDataRow in dtPermiso.Rows)
                        {
                            foreach (Control item in this.Controls)
                            {
                                if (item is FlowLayoutPanel && item.Name.Equals("flpMensaje"))
                                {
                                    foreach (Control itemMensaje in item.Controls)
                                    {
                                        if (itemMensaje is Panel && itemMensaje.Name.Equals("panelMensaje"))
                                        {
                                            foreach (CheckBox chkMostrarMensaje in itemMensaje.Controls.OfType<CheckBox>())
                                            {
                                                if (chkMostrarMensaje is CheckBox && chkMostrarMensaje.Name.Equals("chkMostrarMensajeVenta"))
                                                {
                                                    chkMostrarMensaje.Checked = (Boolean)dtDataRow["ProductMessageActivated"];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            ///////////////////////////////-----------------------Inventario------------------------/////////////////////////////////////////

            if (Productos.dobleClickProducto == 1)
            {
                using (DataTable dtPermiso = cn.CargarDatos(cs.verificarEstadoCheckboxInventario(Productos.idprodDobleClick)))
                {
                    if (!dtPermiso.Rows.Count.Equals(0))
                    {
                        foreach (DataRow dtDataRow in dtPermiso.Rows)
                        {
                            foreach (Control item in this.Controls)
                            {
                                if (item is FlowLayoutPanel && item.Name.Equals("flplMensaje"))
                                {
                                    foreach (Control itemMensaje in item.Controls)
                                    {
                                        if (itemMensaje is Panel && itemMensaje.Name.Equals("panelDatos"))
                                        {
                                            foreach (CheckBox chkMostrarMensaje in itemMensaje.Controls.OfType<CheckBox>())
                                            {
                                                if (chkMostrarMensaje is CheckBox && chkMostrarMensaje.Name.Equals("chkMostrarMensajeInventario"))
                                                {
                                                    string estado = dtPermiso.Rows[0]["Activo"].ToString();
                                                    if (estado == "1")
                                                    {
                                                        chkMostrarMensaje.Checked = true;
                                                    }
                                                    else
                                                    {
                                                        chkMostrarMensaje.Checked = false;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (DataTable dtPermiso = cn.CargarDatos(cs.verificarEstadoCheckboxInventario(Productos.codProductoEditarVenta)))
                {
                    if (!dtPermiso.Rows.Count.Equals(0))
                    {
                        foreach (DataRow dtDataRow in dtPermiso.Rows)
                        {
                            foreach (Control item in this.Controls)
                            {
                                if (item is FlowLayoutPanel && item.Name.Equals("flplMensaje"))
                                {
                                    foreach (Control itemMensaje in item.Controls)
                                    {
                                        if (itemMensaje is Panel && itemMensaje.Name.Equals("panelDatos"))
                                        {
                                            foreach (CheckBox chkMostrarMensaje in itemMensaje.Controls.OfType<CheckBox>())
                                            {
                                                if (chkMostrarMensaje is CheckBox && chkMostrarMensaje.Name.Equals("chkMostrarMensajeInventario"))
                                                {
                                                    string estado = dtPermiso.Rows[0]["Activo"].ToString();
                                                    if (estado == "1")
                                                    {
                                                        chkMostrarMensaje.Checked = true;
                                                    }
                                                    else
                                                    {
                                                        chkMostrarMensaje.Checked = false;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void botonConfirmar_click(object sender, EventArgs e)
        {
            var dato = MensajeVentasYMensajeInventario.enviarDato;

            if (dato == "mensajeVentas")
            {
                TextBox cantidadCompra = (TextBox)Controls.Find("txtCantidadCompra", true)[0];
                TextBox mensajeMostrado = (TextBox)Controls.Find("txtMensaje", true)[0];

                if (string.IsNullOrWhiteSpace(cantidadCompra.Text))
                {
                    MessageBox.Show("Favor de asignar una cantidad de compra minima.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrWhiteSpace(mensajeMostrado.Text))
                {
                    MessageBox.Show("Favor de asignar un mensaje a mostrar.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (dato == "mensajeInventario")
            {
                TextBox mensajeMostrado = (TextBox)Controls.Find("txtMensaje", true)[0];

                if (string.IsNullOrWhiteSpace(mensajeMostrado.Text))
                {
                    MessageBox.Show("Favor de asignar un mensaje a mostrar.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (dato == "mensajeVentas")
            {
                foreach (Control item in this.Controls)
                {
                    if (item is FlowLayoutPanel && item.Name.Equals("flpDatos"))
                    {
                        foreach (Control itemMensaje in item.Controls)
                        {
                            if (itemMensaje is Panel && itemMensaje.Name.Equals("panelDatos"))
                            {
                                foreach (Control textoMensaje in itemMensaje.Controls)
                                {
                                    if (textoMensaje is TextBox)
                                    {
                                        if (textoMensaje.Name.Equals("txtCantidadCompra"))
                                        {
                                            if (Productos.dobleClickProducto == 1)
                                            {
                                                var updateOinsert = cn.CargarDatos(cs.viewMensajeVentas(Productos.idprodDobleClick));

                                                if (updateOinsert.Rows.Count.Equals(0))
                                                {
                                                    var cantidadMinimaCompra = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(cantidadDeCompra))
                                                    {
                                                        cn.EjecutarConsulta(cs.insertarCompraMinima(Productos.idprodDobleClick, Convert.ToInt32(cantidadMinimaCompra)));
                                                    }
                                                }
                                                else
                                                {
                                                    var cantidadMinimaCompra = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(cantidadMinimaCompra))
                                                    {
                                                        cn.EjecutarConsulta(cs.actualizarCompraMinima(Productos.idprodDobleClick, cantidadMinimaCompra));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var updateOinsert = cn.CargarDatos(cs.viewMensajeVentas(Productos.codProductoEditarVenta));

                                                if (updateOinsert.Rows.Count.Equals(0))
                                                {
                                                    var cantidadMinimaCompra = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(cantidadDeCompra))
                                                    {
                                                        cn.EjecutarConsulta(cs.insertarCompraMinima(Productos.codProductoEditarVenta, Convert.ToInt32(cantidadMinimaCompra)));
                                                    }
                                                }
                                                else
                                                {
                                                    var cantidadMinimaCompra = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(cantidadMinimaCompra))
                                                    {
                                                        cn.EjecutarConsulta(cs.actualizarCompraMinima(Productos.codProductoEditarVenta, cantidadMinimaCompra));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                        if (item is FlowLayoutPanel && item.Name.Equals("flpMensaje"))
                    {
                            foreach (Control itemMensaje in item.Controls)
                            {
                                if (itemMensaje is Panel && itemMensaje.Name.Equals("panelMensaje"))
                                {
                                    foreach (Control textoMensaje in itemMensaje.Controls)
                                    {
                                        if (textoMensaje is TextBox)
                                        {
                                        if (textoMensaje.Name.Equals("txtMensaje"))
                                            {
                                            if (Productos.dobleClickProducto == 1)
                                            {
                                                var updateOinsert = cn.CargarDatos(cs.viewMensajeVentas(Productos.idprodDobleClick));

                                                if (updateOinsert.Rows.Count.Equals(0))
                                                {
                                                    var NuevoMensaje = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                    {
                                                        var estado = 1;
                                                        CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeVenta", true)[0];
                                                        if (!txtCantidadCompra.Checked.Equals(true))
                                                        {
                                                            estado = 0;
                                                        }
                                                        if (Productos.dobleClickProducto == 1)
                                                        {
                                                            cn.EjecutarConsulta(cs.insertarMensajeVenta(Productos.idprodDobleClick, estado, NuevoMensaje));
                                                            MessageBox.Show("Actualizado Correctamente.","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                                        }
                                                        else
                                                        {
                                                            cn.EjecutarConsulta(cs.insertarMensajeVenta(Productos.codProductoEditarVenta, estado, NuevoMensaje));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                        
                                                    }
                                                }
                                                else
                                                {
                                                    var NuevoMensaje = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                    {
                                                        var estado = 1;
                                                        CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeVenta", true)[0];
                                                        if (!txtCantidadCompra.Checked.Equals(true))
                                                        {
                                                            estado = 0;
                                                        }
                                                        if (Productos.dobleClickProducto == 1)
                                                        {
                                                            cn.EjecutarConsulta(cs.actualizarMensajeVentas(Productos.idprodDobleClick, NuevoMensaje, estado));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                        else
                                                        {
                                                            cn.EjecutarConsulta(cs.actualizarMensajeVentas(Productos.codProductoEditarVenta, NuevoMensaje, estado));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var updateOinsert = cn.CargarDatos(cs.viewMensajeVentas(Productos.codProductoEditarVenta));

                                                if (updateOinsert.Rows.Count.Equals(0))
                                                {
                                                    var NuevoMensaje = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                    {
                                                        var estado = 1;
                                                        CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeVenta", true)[0];
                                                        if (!txtCantidadCompra.Checked.Equals(true))
                                                        {
                                                            estado = 0;
                                                        }
                                                        if (Productos.dobleClickProducto == 1)
                                                        {
                                                            cn.EjecutarConsulta(cs.insertarMensajeVenta(Productos.idprodDobleClick, estado, NuevoMensaje));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                        else
                                                        {
                                                            cn.EjecutarConsulta(cs.insertarMensajeVenta(Productos.codProductoEditarVenta, estado, NuevoMensaje));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    var NuevoMensaje = textoMensaje.Text;
                                                    if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                    {
                                                        var estado = 1;
                                                        CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeVenta", true)[0];
                                                        if (!txtCantidadCompra.Checked.Equals(true))
                                                        {
                                                            estado = 0;
                                                        }
                                                        cn.EjecutarConsulta(cs.actualizarMensajeVentas(Productos.codProductoEditarVenta, NuevoMensaje, estado));
                                                        MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                }
                                            }
                                            
                                            
                                                
                                            }
                                        }
                                    }
                                }
                            }
                    }
                }
                this.Close();
            }
            // Esta seccion es para modificar o insertar el mensaje a la hora de actualizar inventario----------------------------------------------------------
            else if (dato == "mensajeInventario")
            {
                foreach (Control item in this.Controls)
                {
                    if (item is FlowLayoutPanel)
                    {
                        if (item.Name.Equals("flplMensaje"))
                        {
                            foreach (Control itemMensaje in item.Controls)
                            {
                                if (itemMensaje is Panel)
                                {
                                    foreach (Control textoMensaje in itemMensaje.Controls)
                                    {
                                        if (textoMensaje is TextBox)
                                        {
                                            if (textoMensaje.Name.Equals("txtMensaje"))
                                            {
                                                if (Productos.dobleClickProducto == 1)
                                                {
                                                    var updateOinsert = cn.CargarDatos(cs.viewMensajeInventario(Productos.idprodDobleClick));
                                                    if (updateOinsert.Rows.Count.Equals(0))
                                                    {
                                                        var NuevoMensaje = textoMensaje.Text;

                                                        if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                        {
                                                            var estado = 1;
                                                            CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeInventario", true)[0];
                                                            if (!txtCantidadCompra.Checked.Equals(true))
                                                            {
                                                                estado = 0;
                                                            }
                                                            cn.EjecutarConsulta(cs.insertarMensajeInventario(Productos.idprodDobleClick, NuevoMensaje, estado));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var NuevoMensaje = textoMensaje.Text;
                                                        if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                        {
                                                            var estado = 1;
                                                            CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeInventario", true)[0];
                                                            if (!txtCantidadCompra.Checked.Equals(true))
                                                            {
                                                                estado = 0;
                                                            }
                                                            cn.EjecutarConsulta(cs.actualizarMensajeInventario(Productos.idprodDobleClick, NuevoMensaje, estado));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    var updateOinsert = cn.CargarDatos(cs.viewMensajeInventario(Productos.codProductoEditarVenta));
                                                    if (updateOinsert.Rows.Count.Equals(0))
                                                    {
                                                        var NuevoMensaje = textoMensaje.Text;

                                                        if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                        {
                                                            var estado = 1;
                                                            CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeInventario", true)[0];
                                                            if (!txtCantidadCompra.Checked.Equals(true))
                                                            {
                                                                estado = 0;
                                                            }
                                                            cn.EjecutarConsulta(cs.insertarMensajeInventario(Productos.codProductoEditarInventario, NuevoMensaje, estado));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var NuevoMensaje = textoMensaje.Text;
                                                        if (!string.IsNullOrWhiteSpace(NuevoMensaje))
                                                        {
                                                            var estado = 1;
                                                            CheckBox txtCantidadCompra = (CheckBox)Controls.Find("chkMostrarMensajeVenta", true)[0];
                                                            if (!txtCantidadCompra.Checked.Equals(true))
                                                            {
                                                                estado = 0;
                                                            }
                                                            cn.EjecutarConsulta(cs.actualizarMensajeInventario(Productos.codProductoEditarInventario, NuevoMensaje, estado));
                                                            MessageBox.Show("Actualizado Correctamente.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        }
                                                    }
                                                }
                                                
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.Close(); 
        }

        private void EditarMensajesParaEnviar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
