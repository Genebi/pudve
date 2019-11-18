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
    public partial class GenerarEtiqueta : Form
    {
        int alturaEjeY = 10;

        public GenerarEtiqueta()
        {
            InitializeComponent();
        }

        private void GenerarEtiqueta_Load(object sender, EventArgs e)
        {
            CargarPropiedades();
        }

        private FlowLayoutPanel GenerarPropiedad(string nombreProdiedad, string textoLabel)
        {
            FlowLayoutPanel panelPropiedad = new FlowLayoutPanel();
            panelPropiedad.Name = "panel" + nombreProdiedad;
            panelPropiedad.Width = 195;
            panelPropiedad.Height = 30;
            panelPropiedad.FlowDirection = FlowDirection.LeftToRight;
            panelPropiedad.Location = new Point(3, alturaEjeY);
            panelPropiedad.BorderStyle = BorderStyle.FixedSingle;

            Label lbPropiedad = new Label();
            lbPropiedad.Text = textoLabel;
            lbPropiedad.Name = "lb" + nombreProdiedad;
            lbPropiedad.Width = 155;
            lbPropiedad.Height = 20;
            lbPropiedad.TextAlign = ContentAlignment.MiddleCenter;

            Button btnPropiedad = new Button();
            btnPropiedad.Name = "btn" + nombreProdiedad;
            btnPropiedad.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus-square.png");
            btnPropiedad.Cursor = Cursors.Hand;
            btnPropiedad.Width = 25;
            btnPropiedad.Tag = nombreProdiedad;
            btnPropiedad.Click += new EventHandler(botonAgregarPropiedad_Click);

            panelPropiedad.Controls.Add(lbPropiedad);
            panelPropiedad.Controls.Add(btnPropiedad);

            alturaEjeY += 35;

            return panelPropiedad;
        }

        private void botonAgregarPropiedad_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            var propiedad = btn.Tag.ToString();

            if (propiedad != "Codigo")
            {
                Label lbCustom = new Label();
                lbCustom.Text = "Prueba";
                lbCustom.Width = 150;

                panelEtiqueta.Controls.Add(lbCustom);
                ControlExtension.Draggable(lbCustom, true);
            }
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

            // PANEL NOMBRE PRODUCTO
            panelPropiedades.Controls.Add(GenerarPropiedad("Nombre", "Nombre"));

            // PANEL CODIGO DE BARRAS
            panelPropiedades.Controls.Add(GenerarPropiedad("Codigo", "Código de barras"));

            // PANEL CLAVE DE PRODUCTO
            panelPropiedades.Controls.Add(GenerarPropiedad("Clave", "Clave"));

            // PANEL STOCK
            panelPropiedades.Controls.Add(GenerarPropiedad("Stock", "Stock"));

            // PANEL PRECIO
            panelPropiedades.Controls.Add(GenerarPropiedad("Precio", "Precio"));

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
                        panelPropiedades.Controls.Add(GenerarPropiedad("Proveedor", "Proveedor"));
                    }
                    else
                    {
                        // Aqui consultamos y agregamos todos los restantes
                        panelPropiedades.Controls.Add(GenerarPropiedad(key, key));
                    }
                }
            }
        }
    }
}
