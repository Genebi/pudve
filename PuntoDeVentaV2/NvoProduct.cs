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

        float stockNvo, precioNvo;

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
            cargarPrecio();
            txtPrecioProducto.Text = precioNvo.ToString("N2");
            txtCategoriaProducto.Text = ProdCategoriaFin;
            txtClaveProducto.Text = ProdClaveInternaFin;
            txtCodigoBarras.Text = ProdCodBarrasFin;
        }
        
        private void txtStockProducto_Leave(object sender, EventArgs e)
        {
            cargarPrecio();
            txtPrecioProducto.Text = precioNvo.ToString("N2");
        }

        private void txtCategoriaProducto_TextChanged(object sender, EventArgs e)
        {
            txtCategoriaProducto.CharacterCasing = CharacterCasing.Upper;
        }

        private void cargarPrecio()
        {
            stockNvo = (float)Convert.ToDouble(txtStockProducto.Text);
            precioNvo = (float)Convert.ToDouble(ProdPrecioFin) / stockNvo;
        }
    }
}
