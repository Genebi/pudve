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

            using (var ap = new AsignarPropiedad(propiedad))
            {
                ap.ShowDialog();
            }
        }

        private void AgregarOpcion(string nombre, string texto, int altura)
        {
            // PANEL MENSAJE VENTAS
            FlowLayoutPanel panelCustom = new FlowLayoutPanel();
            panelCustom.Name = "panel" + nombre;
            panelCustom.Width = 350;
            panelCustom.Height = 30;
            panelCustom.FlowDirection = FlowDirection.LeftToRight;
            panelCustom.Location = new Point(3, altura);
            panelCustom.BorderStyle = BorderStyle.FixedSingle;

            Label lbCustom = new Label();
            lbCustom.Text = texto;
            lbCustom.Name = "lb" + nombre;
            lbCustom.Width = 100;
            lbCustom.Height = 20;
            lbCustom.TextAlign = ContentAlignment.MiddleCenter;

            Button btnCustom = new Button();
            btnCustom.Name = "btn" + nombre;
            btnCustom.Text = "Asignar";
            btnCustom.Cursor = Cursors.Hand;
            btnCustom.Tag = nombre;
            btnCustom.Click += new EventHandler(botonAsignar_Click);

            panelCustom.Controls.Add(lbCustom);
            panelCustom.Controls.Add(btnCustom);
            panelContenedor.Controls.Add(panelCustom);
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
            AgregarOpcion("Mensaje", "Mensaje Ventas", 10);

            // PANEL MENSAJE INVENTARIO
            AgregarOpcion("MensajeInventario", "Mensaje Inventario", 45);

            // PANEL STOCK
            AgregarOpcion("Stock", "Stock", 80);

            // PANEL STOCK MINIMO
            AgregarOpcion("StockMinimo", "Stock Minimo", 115);

            // PANEL STOCK MAXIMO
            AgregarOpcion("StockMaximo", "Stock Maximo", 150);

            // PANEL PRECIO
            AgregarOpcion("Precio", "Precio", 185);

            // PANEL NUMERO DE REVISION
            AgregarOpcion("Revision", "Número Revisión", 220);

            // PANEL TIPO DE IVA
            AgregarOpcion("IVA", "Tipo de IVA", 255);

            // PANEL CLAVE DE PRODUCTO (FACTURACION)
            AgregarOpcion("ClaveProducto", "Clave de Producto", 290);

            // PANEL CLAVE UNIDAD MEDIDA (FACTURACION)
            AgregarOpcion("ClaveUnidad", "Clave de Unidad", 325);

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
