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
    public partial class TestModificacion : Form
    {
        public TestModificacion()
        {
            InitializeComponent();
        }

        private void TestModificacion_Load(object sender, EventArgs e)
        {
            cbCategorias.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbProveedores.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbUbicaciones.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            comboBox1.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            comboBox2.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            comboBox3.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            comboBox4.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
          
        }
    }
}
