using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ImprimirEtiqueta : Form
    {

        Dictionary<int, string> productos;

        private string[] nombrePlantillas;
        public ImprimirEtiqueta(Dictionary<int, string> lista)
        {
            InitializeComponent();

            this.productos = lista;
        }

        private void ImprimirEtiqueta_Load(object sender, EventArgs e)
        {
            ListaPlantillas();
            cbPlantillas.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            
        }

        private void ListaPlantillas()
        {
            // Obtener los nombres de los archivos de la carpeta plantillas
            var ruta = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Plantillas";

            nombrePlantillas = Directory.GetFiles(ruta, "*.txt").Select(Path.GetFileName).ToArray();

            cbPlantillas.Items.Add("Seleccionar una plantilla...");

            if (nombrePlantillas.Length > 0)
            {
                foreach (var nombre in nombrePlantillas)
                {
                    var plantilla = nombre.Replace(".txt", "");

                    cbPlantillas.Items.Add(plantilla);
                }
            }

            cbPlantillas.SelectedIndex = 0;
            cbPlantillas.Focus();
        }

        private void cbPlantillas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var indice = cbPlantillas.SelectedIndex;
            var nombre = cbPlantillas.SelectedItem.ToString();

            if (indice > 0)
            {
                var plantilla = nombre + ".bmp";
                var rutaPlantilla = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Plantillas\" + plantilla;

                panelEtiqueta.BorderStyle = BorderStyle.None;
                panelEtiqueta.BackgroundImage = Image.FromFile(rutaPlantilla);
            }
            else
            {
                panelEtiqueta.BackgroundImage = null;
                panelEtiqueta.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Obtenemos el nombre de la plantilla seleccionada para buscar el .txt con los datos
            var indice = cbPlantillas.SelectedIndex;
            var plantilla = cbPlantillas.SelectedItem.ToString();

            if (indice > 0)
            {
                var nombreArchivo = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Plantillas\" + plantilla + ".txt";

                using (var leer = new StreamReader(nombreArchivo))
                {
                    foreach (var producto in productos)
                    {
                        // Buscar datos del producto
                        //var datosProducto = cn.BuscarProducto(producto.Key, FormPrincipal.userID);


                        var linea = string.Empty;

                        while ((linea = leer.ReadLine()) != null)
                        {
                            var datos = linea.Split('|');

                            var nombreFuente = datos[0];
                            var tamanoFuente = float.Parse(datos[1]);
                            var posicionX = datos[2];
                            var posicionY = datos[3];
                            var nombrePropiedad = datos[4];

                            // Cuando es codigo de barras el que se va agregar
                            if (nombrePropiedad == "Codigo")
                            {

                            }
                            else
                            {
                                Font fuente = new Font(nombreFuente, tamanoFuente);

                                Label lbCustom = new Label();
                                lbCustom.Text = "";
                                lbCustom.Font = fuente;

                                var infoTexto = TextRenderer.MeasureText(lbCustom.Text, new Font(lbCustom.Font.FontFamily, lbCustom.Font.Size));

                                lbCustom.Width = infoTexto.Width;
                                lbCustom.Height = infoTexto.Height;

                                lbCustom.Location = new Point(Convert.ToInt32(posicionX), Convert.ToInt32(posicionY));

                                panelEtiqueta.Controls.Add(lbCustom);
                            }
                        }
                    }
                }
            }
        }
    }
}
