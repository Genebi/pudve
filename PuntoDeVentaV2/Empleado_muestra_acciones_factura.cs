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
    public partial class Empleado_muestra_acciones_factura : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int id_fct = 0;
        int opc_tipo_factura = 0;


        public Empleado_muestra_acciones_factura(int idf, int tipo_factura)
        {
            InitializeComponent();

            id_fct = idf;
            opc_tipo_factura = tipo_factura;

            cargar_datos();
        }

        private void cargar_datos()
        {
            DataTable d_factura = cn.CargarDatos(cs.cargar_datos_venta_xml(1, id_fct, FormPrincipal.userID));
            DataRow r_factura = d_factura.Rows[0];

            int[] id_empleados = new int[3];
            string nom_e_timbra= "";
            string nom_e_envia = "";
            string nom_e_cancela = "";
            int factura_enviada = Convert.ToInt32(r_factura["f_enviada"]);

            id_empleados[0] = Convert.ToInt32(r_factura["id_empleado"]);
            id_empleados[1] = Convert.ToInt32(r_factura["id_emp_envia"]);
            id_empleados[2] = Convert.ToInt32(r_factura["id_emp_cancela"]);
            

            for(var x=0; x< id_empleados.Length; x++)
            {
                if(id_empleados[x] > 0)
                {
                    var nom = cn.EjecutarSelect($"SELECT usuario FROM Empleados WHERE ID='{id_empleados[x]}'", 9);
                    string nombre= nom.ToString();

                    var pos = nombre.IndexOf("@");
                    nombre = nombre.Substring(pos + 1, nombre.Length - (pos + 1));

                    if (x == 0) { nom_e_timbra = nombre; }
                    if (x == 1) { nom_e_envia = nombre; }
                    if (x == 2) { nom_e_cancela = nombre; }
                }
            }

            if(nom_e_cancela == "" & (opc_tipo_factura == 1 | opc_tipo_factura == 2))
            {
                nom_e_cancela = "--";
            }
            if(nom_e_cancela == "" & opc_tipo_factura == 3)
            {
                nom_e_cancela = "Administrador";
            }

            if(nom_e_envia == "" & factura_enviada == 0)
            {
                nom_e_envia = "--";
            }
            if (nom_e_envia == "" & factura_enviada == 1)
            {
                nom_e_envia = "Administrador";
            }

            Label lb_timbrar = new Label();
            lb_timbrar.Location = new Point(10, 19);
            lb_timbrar.Size = new Size(50, 18);
            lb_timbrar.Text = "Timbrar:";
            lb_timbrar.AutoSize = true;
            
            Label lb_timbrar_n = new Label();
            lb_timbrar_n.Location = new Point(129, 19);
            lb_timbrar_n.Size = new Size(129, 18);
            lb_timbrar_n.Text = nom_e_timbra;
            lb_timbrar_n.AutoSize = true;

            Label lb_enviar = new Label();
            lb_enviar.Location = new Point(10, 55);
            lb_enviar.Size = new Size(50, 18);
            lb_enviar.Text = "Enviar correo:";
            lb_enviar.AutoSize = true;

            Label lb_enviar_n = new Label();
            lb_enviar_n.Location = new Point(129, 55);
            lb_enviar_n.Size = new Size(50, 18);
            lb_enviar_n.Text = nom_e_envia;
            lb_enviar_n.AutoSize = true;

            Label lb_cancelar = new Label();
            lb_cancelar.Location = new Point(10, 94);
            lb_timbrar.Size = new Size(50, 18);
            lb_cancelar.Text = "Cancelar:";
            lb_cancelar.AutoSize = true;

            Label lb_cancelar_n = new Label();
            lb_cancelar_n.Location = new Point(129, 94);
            lb_cancelar_n.Size = new Size(50, 18);
            lb_cancelar_n.Text = nom_e_cancela;
            lb_cancelar_n.AutoSize = true;

            pnl_info.Controls.Add(lb_timbrar);
            pnl_info.Controls.Add(lb_timbrar_n);
            pnl_info.Controls.Add(lb_enviar);
            pnl_info.Controls.Add(lb_enviar_n);
            pnl_info.Controls.Add(lb_cancelar);
            pnl_info.Controls.Add(lb_cancelar_n);
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
