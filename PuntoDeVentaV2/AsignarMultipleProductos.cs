using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class AsignarMultipleProductos : Form
    {
        public AsignarMultipleProductos()
        {
            InitializeComponent();
        }

        private void AsignarMultipleProductos_Load(object sender, EventArgs e)
        {
            CargarPropiedades();

            panelContenedor.HorizontalScroll.Visible = false;
        }

        private void botonAsignar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            var propiedad = btn.Tag;

            AsignarPropiedad ap = new AsignarPropiedad(propiedad);

            ap.FormClosed += delegate
            {

            };

            ap.ShowDialog();
        }

        private void CargarPropiedades()
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            // Obtenemos el nodo principal de las propiedades del archivo App.config
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            //======================================================================

            // PANEL MENSAJE VENTAS
            FlowLayoutPanel panelMensaje = new FlowLayoutPanel();
            panelMensaje.Name = "panelMensaje";
            panelMensaje.Width = 350;
            panelMensaje.Height = 30;
            panelMensaje.FlowDirection = FlowDirection.LeftToRight;
            panelMensaje.Location = new Point(3, 10);
            panelMensaje.BorderStyle = BorderStyle.FixedSingle;

            Label lbMensaje = new Label();
            lbMensaje.Text = "Mensaje Ventas";
            lbMensaje.Name = "lbMensaje";
            lbMensaje.Width = 100;
            lbMensaje.Height = 20;
            lbMensaje.TextAlign = ContentAlignment.MiddleCenter;

            Button btnMensaje = new Button();
            btnMensaje.Name = "btnMensaje";
            btnMensaje.Text = "Asignar";
            btnMensaje.Cursor = Cursors.Hand;
            btnMensaje.Tag = "Mensaje";
            btnMensaje.Click += new EventHandler(botonAsignar_Click);

            panelMensaje.Controls.Add(lbMensaje);
            panelMensaje.Controls.Add(btnMensaje);
            panelContenedor.Controls.Add(panelMensaje);

            // PANEL MENSAJE INVENTARIO
            FlowLayoutPanel panelMensajeInventario = new FlowLayoutPanel();
            panelMensajeInventario.Name = "panelMensajeInventario";
            panelMensajeInventario.Width = 350;
            panelMensajeInventario.Height = 30;
            panelMensajeInventario.FlowDirection = FlowDirection.LeftToRight;
            panelMensajeInventario.Location = new Point(3, 45);
            panelMensajeInventario.BorderStyle = BorderStyle.FixedSingle;

            Label lbMensajeInventario = new Label();
            lbMensajeInventario.Text = "Mensaje Inventario";
            lbMensajeInventario.Name = "lbMensajeInventario";
            lbMensajeInventario.Width = 100;
            lbMensajeInventario.Height = 20;
            lbMensajeInventario.TextAlign = ContentAlignment.MiddleCenter;

            Button btnMensajeInventario = new Button();
            btnMensajeInventario.Name = "btnMensajeInventario";
            btnMensajeInventario.Text = "Asignar";
            btnMensajeInventario.Cursor = Cursors.Hand;
            btnMensajeInventario.Tag = "MensajeInventario";
            btnMensajeInventario.Click += new EventHandler(botonAsignar_Click);

            panelMensajeInventario.Controls.Add(lbMensajeInventario);
            panelMensajeInventario.Controls.Add(btnMensajeInventario);
            panelContenedor.Controls.Add(panelMensajeInventario);

            // PANEL STOCK
            FlowLayoutPanel panelStock = new FlowLayoutPanel();
            panelStock.Name = "panelStock";
            panelStock.Width = 350;
            panelStock.Height = 30;
            panelStock.FlowDirection = FlowDirection.LeftToRight;
            panelStock.Location = new Point(3, 80);
            panelStock.BorderStyle = BorderStyle.FixedSingle;

            Label lbStock = new Label();
            lbStock.Text = "Stock";
            lbStock.Name = "lbStock";
            lbStock.Width = 100;
            lbStock.Height = 20;
            lbStock.TextAlign = ContentAlignment.MiddleCenter;

            Button btnStock = new Button();
            btnStock.Name = "btnStock";
            btnStock.Text = "Asignar";
            btnStock.Cursor = Cursors.Hand;
            btnStock.Tag = "Stock";
            btnStock.Click += new EventHandler(botonAsignar_Click);

            panelStock.Controls.Add(lbStock);
            panelStock.Controls.Add(btnStock);
            panelContenedor.Controls.Add(panelStock);

            // PANEL STOCK MINIMO
            FlowLayoutPanel panelStockMinimo = new FlowLayoutPanel();
            panelStockMinimo.Name = "panelStockMinimo";
            panelStockMinimo.Width = 350;
            panelStockMinimo.Height = 30;
            panelStockMinimo.FlowDirection = FlowDirection.LeftToRight;
            panelStockMinimo.Location = new Point(3, 115);
            panelStockMinimo.BorderStyle = BorderStyle.FixedSingle;

            Label lbStockMinimo = new Label();
            lbStockMinimo.Text = "Stock Minimo";
            lbStockMinimo.Name = "lbStockMinimo";
            lbStockMinimo.Width = 100;
            lbStockMinimo.Height = 20;
            lbStockMinimo.TextAlign = ContentAlignment.MiddleCenter;

            Button btnStockMinimo = new Button();
            btnStockMinimo.Name = "btnStockMinimo";
            btnStockMinimo.Text = "Asignar";
            btnStockMinimo.Cursor = Cursors.Hand;
            btnStockMinimo.Tag = "StockMinimo";
            btnStockMinimo.Click += new EventHandler(botonAsignar_Click);

            panelStockMinimo.Controls.Add(lbStockMinimo);
            panelStockMinimo.Controls.Add(btnStockMinimo);
            panelContenedor.Controls.Add(panelStockMinimo);

            // PANEL STOCK MAXIMO
            FlowLayoutPanel panelStockMaximo = new FlowLayoutPanel();
            panelStockMaximo.Name = "panelStockMaximo";
            panelStockMaximo.Width = 350;
            panelStockMaximo.Height = 30;
            panelStockMaximo.FlowDirection = FlowDirection.LeftToRight;
            panelStockMaximo.Location = new Point(3, 150);
            panelStockMaximo.BorderStyle = BorderStyle.FixedSingle;

            Label lbStockMaximo = new Label();
            lbStockMaximo.Text = "Stock Maximo";
            lbStockMaximo.Name = "lbStockMaximo";
            lbStockMaximo.Width = 100;
            lbStockMaximo.Height = 20;
            lbStockMaximo.TextAlign = ContentAlignment.MiddleCenter;

            Button btnStockMaximo = new Button();
            btnStockMaximo.Name = "btnStockMaximo";
            btnStockMaximo.Text = "Asignar";
            btnStockMaximo.Cursor = Cursors.Hand;
            btnStockMaximo.Tag = "StockMaximo";
            btnStockMaximo.Click += new EventHandler(botonAsignar_Click);

            panelStockMaximo.Controls.Add(lbStockMaximo);
            panelStockMaximo.Controls.Add(btnStockMaximo);
            panelContenedor.Controls.Add(panelStockMaximo);

            // PANEL PRECIO
            FlowLayoutPanel panelPrecio = new FlowLayoutPanel();
            panelPrecio.Name = "panelPrecio";
            panelPrecio.Width = 350;
            panelPrecio.Height = 30;
            panelPrecio.FlowDirection = FlowDirection.LeftToRight;
            panelPrecio.Location = new Point(3, 185);
            panelPrecio.BorderStyle = BorderStyle.FixedSingle;

            Label lbPrecio = new Label();
            lbPrecio.Text = "Precio";
            lbPrecio.Name = "lbPrecio";
            lbPrecio.Width = 100;
            lbPrecio.Height = 20;
            lbPrecio.TextAlign = ContentAlignment.MiddleCenter;

            Button btnPrecio = new Button();
            btnPrecio.Name = "btnPrecio";
            btnPrecio.Text = "Asignar";
            btnPrecio.Cursor = Cursors.Hand;
            btnPrecio.Tag = "Precio";
            btnPrecio.Click += new EventHandler(botonAsignar_Click);

            panelPrecio.Controls.Add(lbPrecio);
            panelPrecio.Controls.Add(btnPrecio);
            panelContenedor.Controls.Add(panelPrecio);

            // PANEL NUMERO DE REVISION
            FlowLayoutPanel panelRevision = new FlowLayoutPanel();
            panelRevision.Name = "panelRevision";
            panelRevision.Width = 350;
            panelRevision.Height = 30;
            panelRevision.FlowDirection = FlowDirection.LeftToRight;
            panelRevision.Location = new Point(3, 220);
            panelRevision.BorderStyle = BorderStyle.FixedSingle;

            Label lbRevision = new Label();
            lbRevision.Text = "Número Revisión";
            lbRevision.Name = "lbRevision";
            lbRevision.Width = 100;
            lbRevision.Height = 20;
            lbRevision.TextAlign = ContentAlignment.MiddleCenter;

            Button btnRevision = new Button();
            btnRevision.Name = "btnRevision";
            btnRevision.Text = "Asignar";
            btnRevision.Cursor = Cursors.Hand;
            btnRevision.Tag = "NumeroRevision";
            btnRevision.Click += new EventHandler(botonAsignar_Click);

            panelRevision.Controls.Add(lbRevision);
            panelRevision.Controls.Add(btnRevision);
            panelContenedor.Controls.Add(panelRevision);

            // PANEL TIPO DE IVA
            FlowLayoutPanel panelIVA = new FlowLayoutPanel();
            panelIVA.Name = "panelIVA";
            panelIVA.Width = 350;
            panelIVA.Height = 30;
            panelIVA.FlowDirection = FlowDirection.LeftToRight;
            panelIVA.Location = new Point(3, 255);
            panelIVA.BorderStyle = BorderStyle.FixedSingle;

            Label lbIVA = new Label();
            lbIVA.Text = "Tipo de IVA";
            lbIVA.Name = "lbIVA";
            lbIVA.Width = 100;
            lbIVA.Height = 20;
            lbIVA.TextAlign = ContentAlignment.MiddleCenter;

            Button btnIVA = new Button();
            btnIVA.Name = "btnIVA";
            btnIVA.Text = "Asignar";
            btnIVA.Cursor = Cursors.Hand;
            btnIVA.Tag = "TipoIVA";
            btnIVA.Click += new EventHandler(botonAsignar_Click);

            panelIVA.Controls.Add(lbIVA);
            panelIVA.Controls.Add(btnIVA);
            panelContenedor.Controls.Add(panelIVA);

            // PANEL CLAVE DE PRODUCTO (FACTURACION)
            FlowLayoutPanel panelClaveProducto = new FlowLayoutPanel();
            panelClaveProducto.Name = "panelClaveProducto";
            panelClaveProducto.Width = 350;
            panelClaveProducto.Height = 30;
            panelClaveProducto.FlowDirection = FlowDirection.LeftToRight;
            panelClaveProducto.Location = new Point(3, 290);
            panelClaveProducto.BorderStyle = BorderStyle.FixedSingle;

            Label lbClaveProducto = new Label();
            lbClaveProducto.Text = "Clave de Producto";
            lbClaveProducto.Name = "lbClaveProducto";
            lbClaveProducto.Width = 100;
            lbClaveProducto.Height = 20;
            lbClaveProducto.TextAlign = ContentAlignment.MiddleCenter;

            Button btnClaveProducto = new Button();
            btnClaveProducto.Name = "btnClaveProducto";
            btnClaveProducto.Text = "Asignar";
            btnClaveProducto.Cursor = Cursors.Hand;
            btnClaveProducto.Tag = "ClaveProducto";
            btnClaveProducto.Click += new EventHandler(botonAsignar_Click);

            panelClaveProducto.Controls.Add(lbClaveProducto);
            panelClaveProducto.Controls.Add(btnClaveProducto);
            panelContenedor.Controls.Add(panelClaveProducto);

            // PANEL CLAVE UNIDAD MEDIDA (FACTURACION)
            FlowLayoutPanel panelClaveUnidad = new FlowLayoutPanel();
            panelClaveUnidad.Name = "panelClaveUnidad";
            panelClaveUnidad.Width = 350;
            panelClaveUnidad.Height = 30;
            panelClaveUnidad.FlowDirection = FlowDirection.LeftToRight;
            panelClaveUnidad.Location = new Point(3, 325);
            panelClaveUnidad.BorderStyle = BorderStyle.FixedSingle;

            Label lbClaveUnidad = new Label();
            lbClaveUnidad.Text = "Clave de Unidad";
            lbClaveUnidad.Name = "lbClaveUnidad";
            lbClaveUnidad.Width = 100;
            lbClaveUnidad.Height = 20;
            lbClaveUnidad.TextAlign = ContentAlignment.MiddleCenter;

            Button btnClaveUnidad = new Button();
            btnClaveUnidad.Name = "btnClaveUnidad";
            btnClaveUnidad.Text = "Asignar";
            btnClaveUnidad.Cursor = Cursors.Hand;
            btnClaveUnidad.Tag = "ClaveUnidad";
            btnClaveUnidad.Click += new EventHandler(botonAsignar_Click);

            panelClaveUnidad.Controls.Add(lbClaveUnidad);
            panelClaveUnidad.Controls.Add(btnClaveUnidad);
            panelContenedor.Controls.Add(panelClaveUnidad);

            int alturaEjeY = 360;

            //======================================================================
            foreach (XmlNode childNode in appSettingsNode)
            {
                var key = childNode.Attributes["key"].Value;
                var value = Convert.ToBoolean(childNode.Attributes["value"].Value);

                // Ignoramos los checkbox secundarios de cada propiedad
                if (key.Substring(0, 3) == "chk")
                {
                    continue;
                }

                // Si el valor de la propiedad es true (esta habilitado)
                if (value == true)
                {
                    // Este valor de proveedor esta agregado por defecto
                    if (key == "Proveedor")
                    {
                        FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                        panelHijo.Name = "panel" + key;
                        panelHijo.Width = 350;
                        panelHijo.Height = 30;
                        panelHijo.FlowDirection = FlowDirection.LeftToRight;
                        panelHijo.Location = new Point(3, alturaEjeY);
                        panelHijo.BorderStyle = BorderStyle.FixedSingle;

                        Label lbProveedor = new Label();
                        lbProveedor.Text = key;
                        lbProveedor.Name = "lb" + key;
                        lbProveedor.Width = 100;
                        lbProveedor.Height = 20;
                        lbProveedor.TextAlign = ContentAlignment.MiddleCenter;

                        Button btnPropiedad = new Button();
                        btnPropiedad.Name = "btn" + key;
                        btnPropiedad.Text = "Asignar";
                        btnPropiedad.Cursor = Cursors.Hand;
                        btnPropiedad.Tag = key;
                        btnPropiedad.Click += new EventHandler(botonAsignar_Click);

                        panelHijo.Controls.Add(lbProveedor);
                        panelHijo.Controls.Add(btnPropiedad);

                        panelContenedor.Controls.Add(panelHijo);

                        alturaEjeY += 35;
                    }
                    else
                    {
                        // Aqui consultamos y agregamos todos los restantes
                        FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                        panelHijo.Name = "panel" + key;
                        panelHijo.Width = 350;
                        panelHijo.Height = 30;
                        panelHijo.FlowDirection = FlowDirection.TopDown;
                        panelHijo.Location = new Point(3, alturaEjeY);
                        panelHijo.BorderStyle = BorderStyle.FixedSingle;

                        Label lbPropiedad = new Label();
                        lbPropiedad.Text = key;
                        lbPropiedad.Name = "lb" + key;
                        lbPropiedad.Width = 100;
                        lbPropiedad.Height = 20;
                        lbPropiedad.TextAlign = ContentAlignment.MiddleCenter;

                        Button btnPropiedad = new Button();
                        btnPropiedad.Name = "btn" + key;
                        btnPropiedad.Text = "Asignar";
                        btnPropiedad.Cursor = Cursors.Hand;
                        btnPropiedad.Tag = key;
                        btnPropiedad.Click += new EventHandler(botonAsignar_Click);

                        panelHijo.Controls.Add(lbPropiedad);
                        panelHijo.Controls.Add(btnPropiedad);

                        panelContenedor.Controls.Add(panelHijo);

                        alturaEjeY += 35;
                    }
                }
            }
        }
    }
}
