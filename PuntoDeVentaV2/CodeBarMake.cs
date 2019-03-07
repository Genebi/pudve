using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BarcodeLib;
using System.Drawing.Imaging;

namespace PuntoDeVentaV2
{
    public partial class CodeBarMake : Form
    {
        public static string NombreProdFinal;
        public static string PrecioProdFinal;
        public static string CodigoBarProdFinal;

        public string NombreProd { set; get; }
        public string PrecioProd { set; get; }
        public string CodigoBarProd { set; get; }

        public void cargarDatos()
        {
            try
            {
                BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();

                NombreProdFinal = NombreProd;
                PrecioProdFinal = PrecioProd;
                CodigoBarProdFinal = CodigoBarProd;
                lblNombreProd.Text = NombreProdFinal;
                lblCodBarPrecio.Text = CodigoBarProdFinal + " - $" + PrecioProdFinal;

                panelResultado.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, CodigoBarProdFinal, Color.Black, Color.White, 118, 23);

                Image imgFinal = (Image)panelResultado.BackgroundImage.Clone();
                SaveFileDialog CajaDeDialogoGuardar = new SaveFileDialog();

                CajaDeDialogoGuardar.AddExtension = true;
                CajaDeDialogoGuardar.Filter = "Image PNG (*.png)|*.png";
                CajaDeDialogoGuardar.FileName = Properties.Settings.Default.rutaDirectorio + @"\BarCode\" + NombreProdFinal + " " + CodigoBarProdFinal + ".png";
                if (CajaDeDialogoGuardar.CheckFileExists)
                {
                    System.IO.File.Delete(Properties.Settings.Default.rutaDirectorio + @"\BarCode\" + NombreProdFinal + " " + CodigoBarProdFinal + ".png");
                }
                else if (!CajaDeDialogoGuardar.CheckFileExists)
                {
                    imgFinal.Save(CajaDeDialogoGuardar.FileName, ImageFormat.Png);
                }
                imgFinal.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error al intentar convertir el texto a PDF: " + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public CodeBarMake()
        {
            InitializeComponent();
        }

        private void CodeBarMake_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
