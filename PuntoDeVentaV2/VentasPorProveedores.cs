using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class VentasPorProveedores : Form
    {
        Conexion cn = new Conexion();
        DataTable DTFinal = new DataTable();
        DataTable DTSinProvedor = new DataTable();
        List<int> ListaIdsProducto = new List<int>();
        string IDEmpleado;
        
        public VentasPorProveedores(string IDEmp)
        {
            InitializeComponent();
            this.IDEmpleado = IDEmp;
        }

        private void VentasPorProveedores_Load(object sender, EventArgs e)
        {
            DTFinal.Columns.Add("Proveedor", typeof(String));
            DTFinal.Columns.Add("Total", typeof(String));
            if (IDEmpleado.Equals("All"))
            {
                CargarDatosProveedores();
            }
            else
            {
                CargarDatosProveedoresPorEmpleado();
            }
            Cargarlbls();
            CargarRDCL();
            panel2.Visible = false;
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatosProveedoresPorEmpleado()
        {
            string FechaInicial = string.Empty;
            using (var DTFecha = cn.CargarDatos($"SELECT FechaOperacion FROM historialcortesdecaja WHERE IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1"))
            {
                FechaInicial = DTFecha.Rows[0]["FechaOperacion"].ToString();
            }
            DateTime FechaInicialForm = Convert.ToDateTime(FechaInicial);
            string FechaFinal = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string IDSVentas = string.Empty;

            using (var DTIdsVenta = cn.CargarDatos($"SELECT ID FROM VENTAS WHERE FechaOperacion BETWEEN '{FechaInicialForm.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{FechaFinal}' AND IDUsuario = {FormPrincipal.userID} AND Cliente != 'Apertura de Caja' AND IDEmpleado = {IDEmpleado}"))
            {
                int contador = 0;
                foreach (var item in DTIdsVenta.Rows)
                {
                    IDSVentas += DTIdsVenta.Rows[contador]["ID"].ToString() + ",";
                    contador++;
                }
            }
            IDSVentas = IDSVentas.TrimEnd(',');
            using (var DTDatos = cn.CargarDatos($"SELECT PV.IDProducto, SUM( PV.Cantidad ) * PV.Precio AS 'PrecioTotal' FROM productosventa AS PV INNER JOIN ventas AS VEN ON (PV.IDVenta = VEN.ID) WHERE VEN.IDUsuario = {FormPrincipal.userID} AND IDVenta IN ({IDSVentas}) GROUP BY IDProducto"))
            {
                int contador = 0;
                foreach (var item in DTDatos.Rows)
                {
                    ListaIdsProducto.Add(Convert.ToInt32(DTDatos.Rows[contador]["IDProducto"]));
                    contador++;
                }
            }
            string IDs = string.Empty;
            foreach (var item in ListaIdsProducto)
            {
                IDs += item + ",";
            }
            IDs = IDs.TrimEnd(',');
            using (var DTIDSproducto = cn.CargarDatos($"SELECT PV.IDProducto, SUM(PV.Cantidad) * PV.Precio AS 'PrecioTotal', DP.Proveedor FROM productosventa AS PV INNER JOIN detallesproducto AS DP ON ( PV.IDProducto = DP.IDProducto ) INNER JOIN ventas AS VEN ON (PV.IDVenta = VEN.ID) WHERE VEN.IDUsuario = {FormPrincipal.userID} AND PV.IDProducto IN ( {IDs}) AND PV.IDVenta IN({IDSVentas}) GROUP BY IDProducto ORDER BY IDProveedor"))
            {
                if (!DTIDSproducto.Rows.Count.Equals(0))
                {
                    int contador = 0;
                    foreach (var item in DTIDSproducto.Rows)
                    {
                        ListaIdsProducto.Remove(Convert.ToInt32(DTIDSproducto.Rows[contador]["IDProducto"]));
                        contador++;
                    }
                    contador = 0;
                    int contador2 = 0; ;
                    foreach (var item in DTIDSproducto.Rows)
                    {
                        if (!DTFinal.Rows.Count.Equals(0))
                        {
                            if (DTFinal.Rows[contador2]["Proveedor"].Equals(DTIDSproducto.Rows[contador]["Proveedor"]))
                            {
                                decimal valorAnterior = Convert.ToDecimal(DTFinal.Rows[contador2]["Total"]);
                                decimal ValorNuevo = Convert.ToDecimal(DTIDSproducto.Rows[contador]["PrecioTotal"]);
                                decimal total = valorAnterior + ValorNuevo;
                                DTFinal.Rows[contador2]["Total"] = total.ToString("0.00");
                            }
                            else
                            {
                                DTFinal.Rows.Add();
                                decimal total = Convert.ToDecimal(DTIDSproducto.Rows[contador]["PrecioTotal"]);
                                DTFinal.Rows[contador2 + 1]["Proveedor"] = DTIDSproducto.Rows[contador]["Proveedor"].ToString();
                                DTFinal.Rows[contador2 + 1]["Total"] = total.ToString("0.00");
                                contador2++;
                            }
                        }
                        else
                        {
                            DTFinal.Rows.Add();
                            decimal total = Convert.ToDecimal(DTIDSproducto.Rows[contador]["PrecioTotal"]);
                            DTFinal.Rows[contador2]["Proveedor"] = DTIDSproducto.Rows[contador]["Proveedor"].ToString();
                            DTFinal.Rows[contador2]["Total"] = total.ToString("0.00");
                        }

                        contador++;
                    }
                }

                if (!ListaIdsProducto.Count.Equals(0))
                {
                    string IDSProducSinProveedor = string.Empty;
                    foreach (var item in ListaIdsProducto)
                    {
                        IDSProducSinProveedor += item.ToString() + ",";
                    }
                    IDSProducSinProveedor = IDSProducSinProveedor.TrimEnd(',');

                    using (var DTSinProveedor = cn.CargarDatos($"SELECT Cantidad * Precio AS 'Total' FROM productosventa WHERE IDProducto IN ({IDSProducSinProveedor}) AND IDVenta IN ({IDSVentas})"))
                    {
                        int contador = 0;
                        decimal total = 0;
                        foreach (var item in DTSinProveedor.Rows)
                        {
                            total += Convert.ToDecimal(DTSinProveedor.Rows[contador]["Total"]);
                            contador++;
                        }

                        DTFinal.Rows.Add();
                        int Rows = DTFinal.Rows.Count - 1;
                        DTFinal.Rows[Rows]["Proveedor"] = "Productos sin Proveedor";
                        DTFinal.Rows[Rows]["Total"] = total.ToString("0.00");
                    }
                }
            }
        }

        private void Cargarlbls()
        {
            int contador = 0;
            foreach (var item in DTFinal.Rows)
            {
                Panel panel = new Panel();
                Label lbl1 = new Label();
                Label lbl2 = new Label();
                lbl1.Size = new Size(200, 25);
                lbl2.Size = new Size(200, 25);
                lbl1.Font = new System.Drawing.Font(lbl1.Font, FontStyle.Bold);
                lbl2.Font = new System.Drawing.Font(lbl2.Font, FontStyle.Bold);
                panel.Size = new Size(410, 35);
                panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                lbl1.Text = DTFinal.Rows[contador]["Proveedor"].ToString();
                decimal cantidad = Convert.ToDecimal(DTFinal.Rows[contador]["Total"]);
                lbl2.Text = cantidad.ToString("C2");
                lbl1.Location = new Point(0, 4);
                lbl2.Location = new Point(209, 4);
                lbl1.TextAlign = ContentAlignment.MiddleCenter;
                lbl2.TextAlign = ContentAlignment.MiddleCenter;
                panel.Controls.Add(lbl1);
                panel.Controls.Add(lbl2);
                flowLayoutPanel1.Controls.Add(panel);
                contador++; 
            }
        }

        private void CargarDatosProveedores()
        {
            string FechaInicial = string.Empty;
            using (var DTFecha = cn.CargarDatos($"SELECT FechaOperacion FROM historialcortesdecaja WHERE IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1"))
            {
                FechaInicial = DTFecha.Rows[0]["FechaOperacion"].ToString();
            }
            DateTime FechaInicialForm = Convert.ToDateTime(FechaInicial);
            string FechaFinal = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string IDSVentas = string.Empty;

            using (var DTIdsVenta = cn.CargarDatos($"SELECT ID FROM VENTAS WHERE FechaOperacion BETWEEN '{FechaInicialForm.ToString("yyyy-MM-dd HH:mm:ss")}' AND '{FechaFinal}' AND IDUsuario = {FormPrincipal.userID} AND Cliente != 'Apertura de Caja'"))
            {
                int contador = 0;
                foreach (var item in DTIdsVenta.Rows)
                {
                    IDSVentas += DTIdsVenta.Rows[contador]["ID"].ToString() + ",";
                    contador++;
                }
            }
            IDSVentas = IDSVentas.TrimEnd(',');
            using (var DTDatos = cn.CargarDatos($"SELECT PV.IDProducto, SUM( PV.Cantidad ) * PV.Precio AS 'PrecioTotal' FROM productosventa AS PV INNER JOIN ventas AS VEN ON (PV.IDVenta = VEN.ID) WHERE VEN.IDUsuario = 10 AND IDVenta IN ({IDSVentas}) GROUP BY IDProducto"))
            {
                int contador = 0;
                foreach (var item in DTDatos.Rows)
                {
                    ListaIdsProducto.Add(Convert.ToInt32(DTDatos.Rows[contador]["IDProducto"]));
                    contador++;
                }
            }
            string IDs = string.Empty;
            foreach (var item in ListaIdsProducto)
            {
                IDs += item + ",";
            }
            IDs = IDs.TrimEnd(',');
            using (var DTIDSproducto = cn.CargarDatos($"SELECT PV.IDProducto, SUM(PV.Cantidad) * PV.Precio AS 'PrecioTotal', DP.Proveedor FROM productosventa AS PV INNER JOIN detallesproducto AS DP ON ( PV.IDProducto = DP.IDProducto ) INNER JOIN ventas AS VEN ON (PV.IDVenta = VEN.ID) WHERE VEN.IDUsuario = {FormPrincipal.userID} AND PV.IDProducto IN ( {IDs}) AND PV.IDVenta IN({IDSVentas}) GROUP BY IDProducto ORDER BY IDProveedor"))
            {
                if (!DTIDSproducto.Rows.Count.Equals(0))
                {
                    int contador = 0;
                    foreach (var item in DTIDSproducto.Rows)
                    {
                        ListaIdsProducto.Remove(Convert.ToInt32(DTIDSproducto.Rows[contador]["IDProducto"]));
                        contador++;
                    }
                    contador = 0;
                    int contador2 = 0; ;
                    foreach (var item in DTIDSproducto.Rows)
                    {
                        if (!DTFinal.Rows.Count.Equals(0))
                        {
                            if (DTFinal.Rows[contador2]["Proveedor"].Equals(DTIDSproducto.Rows[contador]["Proveedor"]))
                            {
                                decimal valorAnterior = Convert.ToDecimal(DTFinal.Rows[contador2]["Total"]);
                                decimal ValorNuevo = Convert.ToDecimal(DTIDSproducto.Rows[contador]["PrecioTotal"]);
                                decimal total = valorAnterior + ValorNuevo;
                                DTFinal.Rows[contador2]["Total"] = total.ToString("0.00");
                            }
                            else
                            {
                                DTFinal.Rows.Add();
                                decimal total = Convert.ToDecimal(DTIDSproducto.Rows[contador]["PrecioTotal"]);
                                DTFinal.Rows[contador2+1]["Proveedor"] = DTIDSproducto.Rows[contador]["Proveedor"].ToString();
                                DTFinal.Rows[contador2+1]["Total"] = total.ToString("0.00");
                                contador2++;
                            }
                        }
                        else
                        {
                            DTFinal.Rows.Add();
                            decimal total = Convert.ToDecimal(DTIDSproducto.Rows[contador]["PrecioTotal"]);
                            DTFinal.Rows[contador2]["Proveedor"] = DTIDSproducto.Rows[contador]["Proveedor"].ToString();
                            DTFinal.Rows[contador2]["Total"] = total.ToString("0.00");
                        }
                       
                        contador++;
                    }
                }

                if (!ListaIdsProducto.Count.Equals(0))
                {
                    string IDSProducSinProveedor = string.Empty;
                    foreach (var item in ListaIdsProducto)
                    {
                        IDSProducSinProveedor += item.ToString() + ",";
                    }
                    IDSProducSinProveedor = IDSProducSinProveedor.TrimEnd(',');

                    using (var DTSinProveedor = cn.CargarDatos($"SELECT Cantidad * Precio AS 'Total' FROM productosventa WHERE IDProducto IN ({IDSProducSinProveedor}) AND IDVenta IN ({IDSVentas})"))
                    {
                        int contador = 0;
                        decimal total = 0;
                        foreach (var item in DTSinProveedor.Rows)
                        {
                            total += Convert.ToDecimal(DTSinProveedor.Rows[contador]["Total"]);
                            contador++;
                        }

                        DTFinal.Rows.Add();
                        int Rows = DTFinal.Rows.Count - 1;
                        DTFinal.Rows[Rows]["Proveedor"] = "Productos sin Proveedor";
                        DTFinal.Rows[Rows]["Total"] = total.ToString("0.00");
                    }
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CargarRDCL()
        {
            string cadenaConn = string.Empty;
            string queryVentas = string.Empty;


            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }



            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = cadenaConn;

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\GraficaVentasProveedor\ReporteGraficaProveedor.rdlc";

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource NotasVENTAS = new ReportDataSource("DTProveedor", DTFinal);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(NotasVENTAS);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            DTFinal.Clear();
            flowLayoutPanel1.Controls.Clear();
            panel1.Visible = true;
            panel2.Visible = false;
            if (IDEmpleado.Equals("All"))
            {
                CargarDatosProveedores();
            }
            else
            {
                CargarDatosProveedoresPorEmpleado();
            }
            Cargarlbls();
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            DTFinal.Clear();
            panel2.Visible = true;
            panel1.Visible = false;
            if (IDEmpleado.Equals("All"))
            {
                CargarDatosProveedores();
            }
            else
            {
                CargarDatosProveedoresPorEmpleado();
            }
            CargarRDCL();
        }

        private void VentasPorProveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
