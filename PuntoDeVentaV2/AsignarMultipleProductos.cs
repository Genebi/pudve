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
            
            // PANEL STOCK
            FlowLayoutPanel panelStock = new FlowLayoutPanel();
            panelStock.Name = "panelStock";
            panelStock.Width = 350;
            panelStock.Height = 30;
            panelStock.FlowDirection = FlowDirection.LeftToRight;
            panelStock.Location = new Point(3, 10);
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

            // PANEL PRECIO
            FlowLayoutPanel panelPrecio = new FlowLayoutPanel();
            panelPrecio.Name = "panelPrecio";
            panelPrecio.Width = 350;
            panelPrecio.Height = 30;
            panelPrecio.FlowDirection = FlowDirection.LeftToRight;
            panelPrecio.Location = new Point(3, 45);
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

            int alturaEjeY = 80;

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
