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
        string labelConFocus = string.Empty;

        //public static string fuenteSeleccionada = string.Empty;

        public GenerarEtiqueta()
        {
            InitializeComponent();
        }

        private void GenerarEtiqueta_Load(object sender, EventArgs e)
        {
            CargarPropiedades();

            // Cargar combobox con las fuentes del sistema
            foreach (FontFamily fuente in FontFamily.Families)
            {
                cbFuentes.Items.Add(fuente.Name.ToString());
            }

            cbFuentes.SelectedItem = "Arial";
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

            // La altura entre dos propiedades
            alturaEjeY += 35;

            return panelPropiedad;
        }

        private void botonAgregarPropiedad_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            var propiedad = btn.Tag.ToString();

            Label lbCustom = new Label();
            lbCustom.Text = propiedad;
            lbCustom.Name = "lbCustom" + propiedad;
            lbCustom.Tag = propiedad;
            lbCustom.Cursor = Cursors.Hand;
            lbCustom.DoubleClick += new EventHandler(EditarLabel_dobleClick);
            lbCustom.Click += new EventHandler(AsignarFocus_Click);

            TextBox txtCustom = new TextBox();
            txtCustom.Name = "txtCustom" + propiedad;
            txtCustom.BorderStyle = BorderStyle.None;
            txtCustom.KeyDown += new KeyEventHandler(TerminarEdicion_KeyDown);
            txtCustom.Tag = propiedad;
            txtCustom.Visible = false;

            panelEtiqueta.Controls.Add(lbCustom);
            panelEtiqueta.Controls.Add(txtCustom);
            ControlExtension.Draggable(lbCustom, true);
        }

        private void EditarLabel_dobleClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            var posicionX = label.Location.X;
            var posicionY = label.Location.Y;
            var propiedad = label.Tag.ToString();
            label.Visible = false;

            TextBox txtBox = (TextBox)this.Controls.Find("txtCustom" + propiedad, true)[0];
            txtBox.Location = new Point(posicionX, posicionY);
            txtBox.Font = new Font(label.Font.FontFamily, label.Font.Size);
            txtBox.Width = label.Width;
            txtBox.Height = label.Height;
            txtBox.Visible = true;
            txtBox.Focus();
        }

        private void AsignarFocus_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.TextAlign = ContentAlignment.MiddleCenter;
            labelConFocus = label.Name;
        }

        private void TerminarEdicion_KeyDown(object sender, KeyEventArgs e)
        {
            // Con enter se termina de editar
            if (e.KeyData == Keys.Enter)
            {
                TextBox txtbox = (TextBox)sender;
                txtbox.Visible = false;

                Label label = (Label)this.Controls.Find("lbCustom" + txtbox.Tag, true)[0];
                label.Text = txtbox.Text;

                var infoTexto = TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size));

                label.Width = infoTexto.Width;
                label.Height = infoTexto.Height;
                label.Visible = true;
            }
        }

        private void btnReducir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(labelConFocus))
            {
                Label label = (Label)this.Controls.Find(labelConFocus, true)[0];
                Font fuente = new Font(label.Font.FontFamily, label.Font.Size - 1);
                label.Font = fuente;

                var infoTexto = TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size));

                label.Width = infoTexto.Width;
                label.Height = infoTexto.Height;
            }
        }

        private void btnAumentar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(labelConFocus))
            {
                Label label = (Label)this.Controls.Find(labelConFocus, true)[0];
                Font fuente = new Font(label.Font.FontFamily, label.Font.Size + 1);
                label.Font = fuente;

                var infoTexto = TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size));

                label.Width = infoTexto.Width;
                label.Height = infoTexto.Height;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Guardar");
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

        private void cbFuentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(labelConFocus))
            {
                var fuenteSeleccionada = cbFuentes.SelectedItem.ToString();

                Label label = (Label)this.Controls.Find(labelConFocus, true)[0];
                Font fuente = new Font(fuenteSeleccionada, label.Font.Size);
                label.Font = fuente;

                var infoTexto = TextRenderer.MeasureText(label.Text, new Font(label.Font.FontFamily, label.Font.Size));

                label.Width = infoTexto.Width;
                label.Height = infoTexto.Height;
            } 
        }
    }
}
