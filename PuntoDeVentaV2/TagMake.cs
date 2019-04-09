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
    public partial class TagMake : Form
    {
        // variables internas para el manejo de datos para la etiqueta
        public static string NombreProdFinal;
        public static string PrecioProdFinal;
        public static string CodigoBarProdFinal;

        // variables publicas para que ellas obtengan los datos de la forma de productos
        public string NombreProd { set; get; }
        public string PrecioProd { set; get; }
        public string CodigoBarProd { set; get; }

        // Path para guardar el pdf generado con la etiqueta del Producto
        string saveDirectoryPdf = Properties.Settings.Default.rutaDirectorio + @"\PdfTag\";

        // metodo haecho para que muestre la vista previa de como seria la etiqueta de codigo de barras
        public void cargarDatos()
        {
            try // intentamos hacer el proceso
            {
                NombreProdFinal = NombreProd;
                PrecioProdFinal = PrecioProd;
                CodigoBarProdFinal = CodigoBarProd;
                lblNombreProd.Text = NombreProdFinal;
                lblCodBarPrecio.Text = CodigoBarProdFinal + " - $" + PrecioProdFinal;
            }
            catch (Exception ex)
            {
                // muestra mensaje que tal ves no tengas permiso sobre ese Path en especifico
                MessageBox.Show("Se ha producido un error al intentar convertir el texto a PDF: " + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public TagMake()
        {
            InitializeComponent();
        }

        private void TagMake_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
