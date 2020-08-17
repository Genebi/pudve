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
    public partial class photoShow : Form
    {
        Conexion cn = new Conexion();

        FileStream File;

        public string NombreProd { get; set; }
        public string StockProd { get; set; }
        public string PrecioProd { get; set; }
        public string ClaveInterna { get; set; }
        public string CodigoBarras { get; set; }

        public static string NombreProdFinal;
        public static string StockProdFinal;
        public static string PrecioProdFinal;
        public static string ClaveInternaFinal;
        public static string CodigoBarrasFinal;

        
        public photoShow()
        {
            InitializeComponent();
        }

        private void photoShow_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        public void cargarDatos()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string pathString = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                pathString = $@"\\{servidor}\pudve\Productos\";
            }
            else
            {
                pathString = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
            }

            int index = 0;
            string buscar;
            string imgPath;

            DataTable dt;

            NombreProdFinal = NombreProd;
            StockProdFinal = StockProd;
            PrecioProdFinal = PrecioProd;
            ClaveInternaFinal = ClaveInterna;
            CodigoBarrasFinal = CodigoBarras;
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{NombreProdFinal}' AND Stock = '{StockProdFinal}' AND Precio = '{PrecioProdFinal}' AND ClaveInterna = '{ClaveInternaFinal}' AND CodigoBarras = '{CodigoBarrasFinal}'";
            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.CargarDatos(buscar);
            imgPath = dt.Rows[index]["ProdImage"].ToString();
            lblNombreProducto.Text = NombreProdFinal;

            if (System.IO.File.Exists(pathString + imgPath))
            {
                using (File = new FileStream(pathString + imgPath, FileMode.Open, FileAccess.Read))
                {
                    // cargamos la imagen en el PictureBox
                    pictureBoxProducto.Image = Image.FromStream(File);
                }
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {

        }
    }
}
