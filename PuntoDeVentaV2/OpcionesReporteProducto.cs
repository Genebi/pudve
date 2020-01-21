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
    public partial class OpcionesReporteProducto : Form
    {
        private Dictionary<string, string> opcionesDefault;
        private List<string> seleccionados;
        private List<string> ultimos;
        private string ultimoSeleccionado = string.Empty;
        private int contador = 1;

        public OpcionesReporteProducto()
        {
            InitializeComponent();
        }

        private void OpcionesReporteProducto_Load(object sender, EventArgs e)
        {
            ultimos = new List<string>();
            seleccionados = new List<string>();
            opcionesDefault = new Dictionary<string, string>();

            opcionesDefault.Add("Nombre", "Nombre producto");
            opcionesDefault.Add("Precio", "Precio de venta");
            opcionesDefault.Add("PrecioCompra", "Precio de compra");
            opcionesDefault.Add("Stock", "Stock");
            opcionesDefault.Add("StockMinimo", "Stock mínimo");
            opcionesDefault.Add("StockMaximo", "Stock máximo");
            opcionesDefault.Add("Clave", "Clave de producto");
            opcionesDefault.Add("Codigo", "Código de barras");
            opcionesDefault.Add("CodigoExt", "Código de barras extra");
            opcionesDefault.Add("Proveedor", "Proveedor");
            opcionesDefault.Add("CantidadPedir", "Cantidad a pedir");

            ObtenerDetalles();
            VisualizarDetalles();
        }

        private void ObtenerDetalles()
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
                        continue;
                    }
                    else
                    {
                        opcionesDefault.Add(key, key);
                    }
                }
            }
        }

        private void VisualizarDetalles()
        {
            if (opcionesDefault.Count > 0)
            {
                int alturaEjeY = 10;

                foreach (var opcion in opcionesDefault)
                {
                    FlowLayoutPanel panelCustom = new FlowLayoutPanel();
                    panelCustom.Name = opcion.Key;
                    panelCustom.Tag = "panel" + opcion.Key;
                    panelCustom.Width = 350;
                    panelCustom.Height = 30;
                    panelCustom.FlowDirection = FlowDirection.LeftToRight;
                    panelCustom.Location = new Point(3, alturaEjeY);
                    //panelCustom.BorderStyle = BorderStyle.FixedSingle;

                    CheckBox cbCustom = new CheckBox();
                    cbCustom.Name = opcion.Key;
                    cbCustom.Tag = "cb" + opcion.Key;
                    cbCustom.AutoSize = true;
                    cbCustom.CheckAlign = ContentAlignment.MiddleLeft;
                    cbCustom.CheckedChanged += cbCustom_CheckedChanged;

                    Label lbCustom = new Label();
                    lbCustom.Text = opcion.Value;
                    lbCustom.Name = opcion.Key;
                    lbCustom.Tag = "lb" + opcion.Key;
                    lbCustom.Width = 200;
                    lbCustom.Height = 20;
                    lbCustom.TextAlign = ContentAlignment.MiddleLeft;

                    panelCustom.Controls.Add(cbCustom);
                    panelCustom.Controls.Add(lbCustom);
                    panelContenedor.Controls.Add(panelCustom);

                    alturaEjeY += 30;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (seleccionados.Count > 0)
            {
                foreach (var opcion in seleccionados)
                {
                    MessageBox.Show(opcion);
                }
            }
        }

        private void cbCustom_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;

            if (checkbox.Checked)
            {
                // Agregamos el nombre de la opcion marcada en el checkbox
                if (!seleccionados.Contains(checkbox.Name))
                {
                    seleccionados.Add(checkbox.Name);
                }

                if (seleccionados.Count > 1)
                {
                    //MessageBox.Show("Entro aqui 1");

                    if (!ultimos.Contains(checkbox.Name))
                    {
                        var penultimoAgregado = seleccionados[seleccionados.Count - 2];
                        //MessageBox.Show("Entro aqui 2");
                        ultimos.Add(penultimoAgregado);
                    }
                }

                foreach (Control panelHijo in panelContenedor.Controls)
                {
                    foreach (Control cb in panelHijo.Controls)
                    {
                        // Despues de recorrer los paneles verificamos si el control analizado es un checkbox
                        if (cb is CheckBox)
                        {
                            var cbTmp = (CheckBox)cb;

                            // Si el nombre del checkbox seleccionado es diferentea a los checkbox marcados
                            // previamente los deshabiltamos
                            if (checkbox.Name != cbTmp.Name && cbTmp.Checked == true)
                            {
                                cbTmp.Enabled = false;
                                //ultimoSeleccionado = checkbox.Name;
                            }
                        }
                    } 
                }

                // Agregamos el ultimo seleccionado
                /*if (!string.IsNullOrEmpty(ultimoSeleccionado))
                {
                    ultimos.Add(ultimoSeleccionado);
                    MessageBox.Show(ultimoSeleccionado);
                    ultimoSeleccionado = string.Empty;
                }*/
            }
            else
            {
                seleccionados.Remove(checkbox.Name);

                // Se comprueba que la lista no este vacia
                if (ultimos.Count > 0)
                {
                    // Obtenemos el nombre del penultimo checkbox marcado
                    var ultimoAgregado = ultimos[ultimos.Count - 1];
                    // Obtenemos el indice de ese checkbox con su nombre
                    var ultimoIndice = ultimos.LastIndexOf(ultimoAgregado);
                    // Finalmente lo removemos de la lista para que no se duplique
                    ultimos.RemoveAt(ultimoIndice);

                    //MessageBox.Show(ultimoAgregado);

                    foreach (Control panelHijo in panelContenedor.Controls)
                    {
                        foreach (Control cb in panelHijo.Controls)
                        {
                            if (cb is CheckBox)
                            {
                                var cbTmp = (CheckBox)cb;

                                // Buscamos el penultimo checkbox por su nombre aqui y lo habilitamos
                                if (cbTmp.Name.Equals(ultimoAgregado))
                                {
                                    cbTmp.Enabled = true;
                                }
                            }
                        }
                    } 
                }
            }
        }
    }
}
