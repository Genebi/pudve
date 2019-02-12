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
    public partial class RecorrerXMLInventario : Form
    {
        public static DataTable TablaProductos;

        public DataTable Productos { get; set; }

        public void recorrerXML(int index)
        {
            //AgregarStockXML.
        }

        public RecorrerXMLInventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void RecorrerXMLInventario_Load(object sender, EventArgs e)
        {
            TablaProductos = Productos;
        }
    }
}
