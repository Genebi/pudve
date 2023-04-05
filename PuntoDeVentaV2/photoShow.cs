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

        int index = 0;
        string buscar;
        string imgPath;
        string pathString = string.Empty;
        string idEditarProducto = string.Empty;

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
            //var servidor = Properties.Settings.Default.Hosting;

            //if (!string.IsNullOrWhiteSpace(servidor))
            //{
            //    pathString = $@"\\{servidor}\pudve\Productos\";
            //}
            //else
            //{
            //    pathString = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
            //}

            DataTable dt;

            //NombreProdFinal = NombreProd;
            //if (StockProd.Equals("N/A"))
            //{
            //    StockProdFinal = "0";
            //}
            //else
            //{
            //    StockProdFinal = StockProd;
            //}
            PrecioProdFinal = PrecioProd;
            ClaveInternaFinal = ClaveInterna;
            CodigoBarrasFinal = CodigoBarras;
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{NombreProdFinal}' AND Stock = '{StockProdFinal}' AND Precio = '{PrecioProdFinal}' AND ClaveInterna = '{ClaveInternaFinal}' AND CodigoBarras = '{CodigoBarrasFinal}'";
            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.CargarDatos(buscar);
            idEditarProducto = dt.Rows[index]["ID"].ToString();
            //imgPath = dt.Rows[index]["ProdImage"].ToString();
            

            //if (System.IO.File.Exists(pathString + imgPath))
            //{
            //    using (File = new FileStream(pathString + imgPath, FileMode.Open, FileAccess.Read))
            //    {
            //        // cargamos la imagen en el PictureBox
            //        lblNombreProducto.Text = NombreProdFinal;
            //        pictureBoxProducto.Image = Image.FromStream(File);
            //    }
            //}//En caso de que no exista la imagen por cualquier caso
            //else if (!System.IO.File.Exists(pathString + imgPath))
            //{
            //    pictureBoxProducto.Image = null;
            //    lblNombreProducto.Text = string.Empty;
            //    btnImagen.Enabled = false;
            //    MessageBox.Show("No se encontro la imagen para este producto\nfavor de agregarla desde editar","Aviso de sistema",MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cn.EjecutarConsulta($"UPDATE productos SET ProdImage = '{string.Empty}' WHERE ID = {idEditarProducto}");
            //    this.Close();
            //}
        }

        //private void btnImagen_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!imgPath.Equals("") || !imgPath.Equals(null))
        //        {
        //            btnImagen.Visible = true;

        //            string path = string.Empty, 
        //                   queryDeleteImageProd = string.Empty, 
        //                   DeleteImage = string.Empty;

        //            // ponemos la direccion y nombre de la imagen
        //            path = pathString + imgPath;
                    
        //            // Liberamos el pictureBox para poder borrar su imagen
        //            pictureBoxProducto.Image.Dispose();
                    
        //            // Establecemos a Nothing el valor de la propiedad Image
        //            // del control PictureBox
        //            pictureBoxProducto.Image = null;
                    
        //            // borramos el archivo de la imagen
        //            System.IO.File.Delete(path);

        //            //var idProducto = Convert.ToInt32(idEditarProducto);
        //            queryDeleteImageProd = $"UPDATE Productos SET ProdImage ='{DeleteImage}' WHERE ID = {idEditarProducto}";
        //            try
        //            {
        //                cn.EjecutarConsulta(queryDeleteImageProd);
        //                imgPath = string.Empty;
        //                this.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error al borrar el nombre de la imagen\nde la base de datos:\n" + ex.Message.ToString(), "información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        else if (imgPath.Equals("") || imgPath.Equals(null))
        //        {
        //            btnImagen.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // si el nombre del archivo esta en blanco
        //        // si no selecciona un archivo valido o ningun archivo muestra este mensaje
        //        MessageBox.Show("selecciona una Imagen", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        private void photoShow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
