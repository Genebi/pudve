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
            NombreProdFinal = NombreProd;
            PrecioProdFinal = PrecioProd;
            CodigoBarProdFinal = CodigoBarProd;
            lblNombreProd.Text = NombreProdFinal;
            lblCodBarPrecio.Text = CodigoBarProdFinal + " - " + PrecioProdFinal;
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
