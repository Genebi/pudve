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
        public EditarMensajesParaEnviar()
        {
            InitializeComponent();
        }

        private void EditarMensajesParaEnviar_Load(object sender, EventArgs e)
        {
            cargarForm(AgregarEditarProducto.nombreProductoEditar.ToString());
        }

        private void cargarForm(string _lbNombreProducto)
        {
            var dato = MensajeVentasYMensajeInventario.enviarDato;
            if (dato == "mensajeVentas")
            {
                this.Height = 281;

                FlowLayoutPanel flpDatos = new FlowLayoutPanel();
                flpDatos.Dock = DockStyle.Top;
                flpDatos.Height = 69;
                
                Panel panelDatos = new Panel();
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
                lbCantidadCompra.Text = "Cantidad Minima En La Venta Para Mostrar Mensaje:";
                lbCantidadCompra.AutoSize = true;
                lbCantidadCompra.Location = new Point(7, 36);

                TextBox txtCantidadCompra = new TextBox();
                txtCantidadCompra.Name = "txtCantidadCompra";
                txtCantidadCompra.Width = 35;
                txtCantidadCompra.Height = 20;
                txtCantidadCompra.Location = new Point(270,33);

                panelDatos.Controls.Add(lbNombreProducto);
                panelDatos.Controls.Add(lbCantidadCompra);
                panelDatos.Controls.Add(txtCantidadCompra);
                flpDatos.Controls.Add(panelDatos);
               
                //Panel para modificar el mensaje------------------------------------------------------

                FlowLayoutPanel flpMensaje = new FlowLayoutPanel();
                flpMensaje.Dock = DockStyle.Top;
                flpMensaje.Height = 118;

                Panel panelMensaje = new Panel();
                panelMensaje.Width = 320;
                panelMensaje.Height = 108;
                panelMensaje.Dock = DockStyle.Top;

                Label lbMensajeActual = new Label();
                lbMensajeActual.Text = "Mensaje Actual:";
                lbMensajeActual.AutoSize = true;
                lbMensajeActual.Location = new Point(7, 6);

                TextBox txtNuevoMensaje = new TextBox();
                txtNuevoMensaje.Name = "txtCantidadCompra";
                txtNuevoMensaje.Multiline = true;
                txtNuevoMensaje.Width = 299;
                txtNuevoMensaje.Height = 79;
                txtNuevoMensaje.Location = new Point(7, 25);

                panelMensaje.Controls.Add(lbMensajeActual);
                panelMensaje.Controls.Add(txtNuevoMensaje);
                flpMensaje.Controls.Add(panelMensaje);

                //Panel para botones------------------------------------------------------------------

                FlowLayoutPanel flBotones = new FlowLayoutPanel();
                flBotones.Dock = DockStyle.Top;
                flBotones.Height = 53;

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
            else if (dato == "mensajeInventario") //EN caso de dar en el boton mensaje inventario
            {
                //Panel para el nombre del producto a modificar

                this.Height = 251;

                FlowLayoutPanel flpDatos = new FlowLayoutPanel();
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
                flpMensaje.Dock = DockStyle.Top;
                flpMensaje.Height = 118;

                Panel panelMensaje = new Panel();
                panelMensaje.Width = 320;
                panelMensaje.Height = 108;
                panelMensaje.Dock = DockStyle.Top;

                Label lbMensajeActual = new Label();
                lbMensajeActual.Text = "Mensaje Actual:";
                lbMensajeActual.AutoSize = true;
                lbMensajeActual.Location = new Point(7, 6);

                TextBox txtNuevoMensaje = new TextBox();
                txtNuevoMensaje.Name = "txtCantidadCompra";
                txtNuevoMensaje.Multiline = true;
                txtNuevoMensaje.Width = 299;
                txtNuevoMensaje.Height = 79;
                txtNuevoMensaje.Location = new Point(7, 25);

                panelMensaje.Controls.Add(lbMensajeActual);
                panelMensaje.Controls.Add(txtNuevoMensaje);
                flpMensaje.Controls.Add(panelMensaje);

                //Panel para botones------------------------------------------------------------------

                FlowLayoutPanel flBotones = new FlowLayoutPanel();
                flBotones.Dock = DockStyle.Top;
                flBotones.Height = 53;

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
                flBotones.Controls.Add(panelBotones);


                this.Controls.Add(flBotones);
                this.Controls.Add(flpMensaje);
                this.Controls.Add(flpDatos);
            }
        }

        private void botonCancelar_click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void botonConfirmar_click(object sender, EventArgs e)
        {
            //boton confirmar click
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
