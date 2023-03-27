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
    public partial class Elegir_moneda : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();


        public Elegir_moneda()
        {
            InitializeComponent();
        }

        private void Elegir_moneda_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> moneda = new Dictionary<string, string>();
            DataTable d_moneda;

            d_moneda = cn.CargarDatos(cs.cargar_datos_venta_xml(6, 0, 0));

            moneda.Add("EUR", "EUR - Euro");
            moneda.Add("MXN", "MXN - Peso Mexicano");
            moneda.Add("USD", "USD - Dolar americano");

            foreach (DataRow r_moneda in d_moneda.Rows)
            {
                if (r_moneda["clave_moneda"].ToString() != "EUR" & r_moneda["clave_moneda"].ToString() != "MXN" & 
                    r_moneda["clave_moneda"].ToString() != "USD" & r_moneda["clave_moneda"].ToString() != "XXX")
                {
                    moneda.Add(r_moneda["clave_moneda"].ToString(), r_moneda["clave_moneda"].ToString() + " - " + r_moneda["descripcion"].ToString());
                }
            }

            cmb_bx_moneda.DataSource = moneda.ToArray();
            cmb_bx_moneda.DisplayMember = "Value";
            cmb_bx_moneda.ValueMember = "Key";
            cmb_bx_moneda.SelectedIndex = 1;

            Complemento_pago.ban_moneda = false;
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            Complemento_pago.clave_moneda = cmb_bx_moneda.SelectedValue.ToString();
            Complemento_pago.ban_moneda = true;

            this.Dispose();
        }
    }
}
