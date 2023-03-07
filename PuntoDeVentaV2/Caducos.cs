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
    public partial class Caducos : Form
    {
        int idusuario;
        Conexion cn = new Conexion();
        private FormPrincipal mainForm = null;
        Button btnCad = null;
        DataTable monosas;
        public Caducos(int userID,Form callingForm = null,Button boton=null)
        {
            idusuario = userID;
            InitializeComponent();
            this.ShowInTaskbar = false;
            if (callingForm != null)
            {
                mainForm = callingForm as FormPrincipal;
            }

            if (callingForm != null)
            {
                btnCad = boton;
            }

        }

        private void chismoso_Tick(object sender, EventArgs e)
        {
            reloadDGV();
        }

        private void Caducos_Load(object sender, EventArgs e)
        {
            reloadDGV();
        }

        private void reloadDGV()
        {
            using (DataTable Dias = cn.CargarDatos($"SELECT diasCaducidad,correoCaducidad FROM configuracion WHERE IDUsuario= {idusuario}"))
            {


                 monosas = cn.CargarDatos($"SELECT productos.Nombre, detallesubdetalle.Stock AS Cantidad, detallesubdetalle.Fecha, IF(detallesubdetalle.Fecha>'{DateTime.Now.ToString("yyyy-MM-dd")}',DATEDIFF(detallesubdetalle.Fecha,'{DateTime.Now.ToString("yyyy-MM-dd")}'),'CADUCADO') AS restantes FROM detallesubdetalle	INNER JOIN subdetallesdeproducto ON (subdetallesdeproducto.id = detallesubdetalle.IDSubDetalle AND subdetallesdeproducto.Activo = 1 AND esCaducidad = 1) LEFT JOIN productos on (subdetallesdeproducto.IDProducto = productos.id AND productos.`Status`=1) WHERE productos.IDUsuario={idusuario} AND DATEDIFF( detallesubdetalle.Fecha, '{DateTime.Now.ToString("yyyy-MM-dd")}' ) <= {Dias.Rows[0][0].ToString()}");
            
                if (!monosas.Rows.Count.Equals(0))
                {
                    dgvProductos.DataSource = monosas;
                    if (btnCad.InvokeRequired)
                    {
                        btnCad.Invoke(new Action(() => btnCad.Visible = true));
                    }

                        if (Dias.Rows[0][1].ToString().Equals("1"))
                        {
                            using (DataTable enviarCorreo = cn.CargarDatos($"SELECT ID FROM caducadoCorreos WHERE IDUsuario = {idusuario} AND fecha = '{DateTime.Today.ToString("yyyy-MM-dd")}'"))
                            {
                                if (enviarCorreo.Rows.Count.Equals(0))
                                {
                                    enviarCorreoCaducidad();
                                    cn.EjecutarConsulta($"INSERT INTO caducadocorreos(IDUsuario, Fecha) VALUES ({idusuario}, '{DateTime.Today.ToString("yyyy-MM-dd")}')");
                                }
                            }
                        }
                }
                else
                {
                    if (btnCad.InvokeRequired)
                    {
                        btnCad.Invoke(new Action(() => btnCad.Visible = false));
                    }
                }
            
            }
        }

        private void enviarCorreoCaducidad()
        {
            var correo = FormPrincipal.datosUsuario[9];
            var asunto = "Reporte diario de caducidad";
            var html = string.Empty;
            html += @"
                    <div>
                        <h4 style='text-align: center;'>Reporte diario de caducidad</h4><hr>
 
                        <table style= 'width:100%'>
                            <tr>
                                <th style='text-align: center;'>Producto</th>
                                <th style='text-align: center;'>Cantidad</th>
                                <th style='text-align: center;'>Fecha de caducidad</th>
                                <th style='text-align: center;'>Días restantes para caducar</th>
                            </tr>";
            foreach (DataRow dataRow in monosas.Rows)
            {

            
            html += $@"<tr>
                       <td style = 'text-align: center;'>
                                        <span style='color: blue;'>{dataRow[0]}</span>
                                    </td>
                                    <td style = 'text-align: center;'>
                                        <span style='color: blue;'>{dataRow[1]}</span>
                                    </td>
                                    <td style = 'text-align: center;'>
                                        <span style='color: black;'><b>{DateTime.Parse(dataRow[2].ToString()).ToString("yyyy-MM-dd")}</b></span>
                                    </td>
                                    <td style = 'text-align: center;'>
                                        <span style='color: blue;'><b>{dataRow[3]}</b></span>
                                    </td>
                                </tr>";
            }
            Utilidades.EnviarEmail(html, asunto, correo);
        }

        private void Caducos_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel=true;
            this.Opacity = 0;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            CaducidadReporte repCad = new CaducidadReporte(monosas);
            repCad.ShowDialog();
        }
    }
}
