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
    public partial class NvoProduct : Form
    {
        public string ProdNombre { get; set; }
        public string ProdStock { get; set; }
        public string ProdPrecio { get; set; }
        public string ProdCategoria { get; set; }
        public string ProdClaveInterna { get; set; }
        public string ProdCodBarras { get; set; }

        static public string ProdNombreFin = "";
        static public string ProdStockFin = "";
        static public string ProdPrecioFin = "";
        static public string ProdCategoriaFin = "";
        static public string ProdClaveInternaFin = "";
        static public string ProdCodBarrasFin = "";

        public NvoProduct()
        {
            InitializeComponent();
        }

        private void NvoProduct_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            ProdNombreFin = ProdNombre;
            ProdStockFin = ProdStock;
            ProdPrecioFin = ProdPrecio;
            ProdCategoriaFin = ProdCategoria;
            ProdClaveInternaFin = ProdClaveInterna;
            ProdCodBarrasFin = ProdCodBarras;

            txtNombreProducto.Text = ProdNombreFin;
            txtStockProducto.Text = ProdStockFin;
            txtPrecioProducto.Text = "0";
            txtCategoriaProducto.Text = ProdCategoriaFin;
            txtClaveProducto.Text = ProdClaveInternaFin;
            txtCodigoBarras.Text = ProdCodBarrasFin;
        }
    }
}
