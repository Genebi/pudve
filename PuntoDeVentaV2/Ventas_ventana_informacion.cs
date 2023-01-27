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

    public partial class Ventas_ventana_informacion : Form
    {

        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();


        public Ventas_ventana_informacion(int id_v)
        {
            InitializeComponent();

            cargar_datos(id_v);
        }

        private void Ventas_ventana_informacion_Load(object sender, EventArgs e)
        {

        }

        private void cargar_datos(int idv)
        {
            string fpago = "";
            string nombre = "";

            int id_empleado = mb.obtener_id_empleado(idv);
            var forma_pago = mb.ObtenerFormasPagoVenta(idv, FormPrincipal.userID);

            if(forma_pago.Length > 0)
            {
                if(Convert.ToDecimal(forma_pago[0].ToString()) > 0)
                {
                    fpago = "Efectivo";
                }
                if (Convert.ToDecimal(forma_pago[1].ToString()) > 0)
                {
                    if (fpago != "") { fpago += ", "; }
                    fpago += "Tarjeta";
                }
                if (Convert.ToDecimal(forma_pago[2].ToString()) > 0)
                {
                    if (fpago != "") { fpago += ", "; }
                    fpago += "Vales";
                }
                if (Convert.ToDecimal(forma_pago[3].ToString()) > 0)
                {
                    if (fpago != "") { fpago += ", "; }
                    fpago += "Cheque";
                }
                if (Convert.ToDecimal(forma_pago[4].ToString()) > 0)
                {
                    if (fpago != "") { fpago += ", "; }
                    fpago += "Transferencia";
                }
            }

            // Busca nombre de empleado
            if(id_empleado > 0)
            {
                var nom = cn.EjecutarSelect($"SELECT usuario FROM Empleados WHERE ID='{id_empleado}'", 9);
                nombre = nom.ToString();

                var pos = nombre.IndexOf("@");
                nombre = nombre.Substring(pos + 1, nombre.Length - (pos + 1));
            }
            else
            {
                nombre = "Administrador";
            }

            

            Label lb_empleado = new Label();
            lb_empleado.Location = new Point(9, 18);
            lb_empleado.Size = new Size(70, 18);
            lb_empleado.Text = "Empleado:";
            lb_empleado.AutoSize = true;
            
            Label lb_fpago = new Label();
            lb_fpago.Location = new Point(9, 60);
            lb_fpago.Size = new Size(103, 18);
            lb_fpago.Text = "Forma(s) de pago:";
            lb_fpago.AutoSize = true;

            Label lb_empleado_n = new Label();
            lb_empleado_n.Location = new Point(128, 18);
            lb_empleado_n.Text = nombre;
            lb_empleado_n.AutoSize = true;

            Label lb_fpago_n = new Label();
            lb_fpago_n.Location = new Point(128, 60);
            lb_fpago_n.Text = fpago;
            lb_fpago_n.AutoSize = true;

            /*Label lb_cancelar = new Label();
            lb_cancelar.Location = new Point(20, 105);
            lb_timbrar.Size = new Size(36, 18);
            lb_cancelar.Text = "Caja:";
            lb_cancelar.AutoSize = true;

            Label lb_cancelar_n = new Label();
            lb_cancelar_n.Location = new Point(139,1054);
            lb_cancelar_n.Text = nom_e_cancela;
            lb_cancelar_n.AutoSize = true;*/

            pnl_info.Controls.Add(lb_empleado);
            pnl_info.Controls.Add(lb_empleado_n);
            pnl_info.Controls.Add(lb_fpago);
            pnl_info.Controls.Add(lb_fpago_n);
            //pnl_info.Controls.Add(lb_cancelar);
            //pnl_info.Controls.Add(lb_cancelar_n);
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
