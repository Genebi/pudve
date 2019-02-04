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
    public partial class Ventas : Form
    {
        public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        public Ventas()
        {
            InitializeComponent();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            tituloSeccion.Focus();
            txtBuscadorProducto.Text = "Buscar producto...";
            txtBuscadorProducto.GotFocus += new EventHandler(BuscarTieneFoco);
            txtBuscadorProducto.LostFocus += new EventHandler(BuscarPierdeFoco);

            btnEliminarUltimo.BackgroundImage = Image.FromFile(rutaDirectorio + @"\icon\black\trash.png");
            btnEliminarTodos.BackgroundImage = Image.FromFile(rutaDirectorio + @"\icon\black\trash.png");

            btnEliminarUltimo.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarTodos.BackgroundImageLayout = ImageLayout.Center;
        }

        private void BuscarTieneFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "Buscar producto...")
            {
                txtBuscadorProducto.Text = "";
            }
        }

        private void BuscarPierdeFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "")
            {
                txtBuscadorProducto.Text = "Buscar producto...";
            }
        }
    }
}
