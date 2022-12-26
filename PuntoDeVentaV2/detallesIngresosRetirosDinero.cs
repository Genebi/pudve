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
    public partial class detallesIngresosRetirosDinero : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public detallesIngresosRetirosDinero()
        {
            InitializeComponent();

        }

        private void detallesIngresosRetirosDinero_Load(object sender, EventArgs e)
        {
            DataTable ultimoCorteCaja = new DataTable();
            if (!FormPrincipal.userNickName.Contains("@") && CajaN.usuarioEmpleado.Equals("usuario"))
            {
                ultimoCorteCaja = cn.CargarDatos($"SELECT FechaOperacion FROM historialcortesdecaja WHERE  IDUsuario = {FormPrincipal.userID} AND idEmpleado = {FormPrincipal.id_empleado} ORDER BY ID DESC LIMIT 1");
            }
            else if (FormPrincipal.userNickName.Contains("@"))
            {
                ultimoCorteCaja = cn.CargarDatos($"SELECT FechaOperacion FROM historialcortesdecaja WHERE  IDUsuario = {FormPrincipal.userID} AND idEmpleado = {FormPrincipal.id_empleado} ORDER BY ID DESC LIMIT 1");
            }
            else
            {
                ultimoCorteCaja = cn.CargarDatos($"SELECT FechaOperacion FROM historialcortesdecaja WHERE  IDUsuario = {FormPrincipal.userID} AND idEmpleado = {FormPrincipal.id_empleado} ORDER BY ID DESC LIMIT 1");
            }
             

            if (!ultimoCorteCaja.Rows.Count.Equals(0))
            {
                DataTable DatosDeposito, DatosRetiro = new DataTable();
                DateTime fechaUltimoCorte = Convert.ToDateTime(ultimoCorteCaja.Rows[0]["FechaOperacion"].ToString());
                var formatoFecha = fechaUltimoCorte.ToString("yyyy-MM-dd HH:mm:ss");


                DatosDeposito = cn.CargarDatos($"SELECT * FROM caja WHERE IDUsuario = {FormPrincipal.userID} AND idEmpleado = '{FormPrincipal.id_empleado}' AND FechaOperacion >= '{formatoFecha}' AND Operacion = 'deposito' OR Operacion = 'deposito' AND FechaOperacion >= '{formatoFecha}' AND Concepto = 'Insert primer saldo inicial'");

                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    DatosRetiro = cn.CargarDatos($"SELECT * FROM caja WHERE IDUsuario = {FormPrincipal.userID} AND idEmpleado = '{CajaN.opcionComboBoxFiltroAdminEmp}' AND FechaOperacion >= '{formatoFecha}' AND Operacion = 'retiro'");
                }
                else
                {
                    DatosRetiro = cn.CargarDatos($"SELECT * FROM caja WHERE IDUsuario = {FormPrincipal.userID} AND idEmpleado = '{FormPrincipal.id_empleado}' AND FechaOperacion >= '{formatoFecha}' AND Operacion = 'retiro'");
                }
              
                if (!DatosDeposito.Rows.Count.Equals(0) && CajaN.detalleAbonoRetiro.Equals("abono"))
                {
                    cargarDatos(DatosDeposito);
                }
                else if (!DatosRetiro.Rows.Count.Equals(0) && CajaN.detalleAbonoRetiro.Equals("retiro"))
                {
                    cargarDatos(DatosRetiro);
                }
            }
        }


        public void cargarDatos(DataTable datos)
        {
            DGDetalles.Rows.Clear();

            if (!datos.Rows.Count.Equals(0))
            {
                string usuario = string.Empty;
                var idemp = datos.Rows[0]["idEmpleado"].ToString();
                var idusr = datos.Rows[0]["IDUsuario"].ToString(); 
                if (CajaN.usuarioEmpleado.Equals("empleado"))
                {
                    var nombUsr = cn.CargarDatos($"SELECT usuario FROM empleados WHERE IDUsuario = {idusr} AND ID = {idemp}");
                    usuario = nombUsr.Rows[0]["usuario"].ToString();
                }
                else
                {
                    usuario = FormPrincipal.userNickName;
                }
                
                Image imprimir = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\print.png");
                foreach (DataRow item in datos.Rows)
                {
                    int drow = DGDetalles.Rows.Add();
                    DataGridViewRow row = DGDetalles.Rows[drow];

                    row.Cells["id"].Value = item["ID"].ToString();
                    row.Cells["tipodemovimiento"].Value = item["Operacion"].ToString();
                    row.Cells["concepto"].Value = item["Concepto"].ToString();
                    row.Cells["cantidad"].Value = item["Cantidad"].ToString();
                    row.Cells["efectivo"].Value = item["Efectivo"].ToString();
                    row.Cells["tarjeta"].Value = item["Tarjeta"].ToString();
                    row.Cells["vales"].Value = item["Vales"].ToString();
                    row.Cells["cheque"].Value = item["Cheque"].ToString();
                    row.Cells["transferencia"].Value = item["Transferencia"].ToString();
                    row.Cells["nombredeusuario"].Value = usuario;
                    row.Cells["fechadeoperacion"].Value = item["FechaOperacion"].ToString();
                    row.Cells["imprimir"].Value = imprimir;

                }
            }

            //DGDetalles.DataSource = DGDetalles;
            //DGDetalles.Columns["id"].Visible = false;
            DGDetalles.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void DGDetalles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string valor = "";
            valor = DGDetalles.Rows[DGDetalles.CurrentRow.Index].Cells[0].Value.ToString();

            if (e.ColumnIndex == 11 && !FormPrincipal.userNickName.Contains("@") && CajaN.usuarioEmpleado.Equals("usuario"))
            {
                if (CajaN.detalleAbonoRetiro.Equals("abono"))
                {
                    using (ImprimirTicketDepositarDineroCaja8cm imprimirTicketDineroAgregado = new ImprimirTicketDepositarDineroCaja8cm())
                    {
                        imprimirTicketDineroAgregado.idDineroAgregado = Convert.ToInt32(valor);
                        imprimirTicketDineroAgregado.ShowDialog();
                    }
                }
                else
                {
                    using (ImprimirTicketRetirarDineroCaja8cm imprimirTicketDineroRetirado = new ImprimirTicketRetirarDineroCaja8cm())
                    {
                        imprimirTicketDineroRetirado.idDineroRetirado = Convert.ToInt32(valor);
                        imprimirTicketDineroRetirado.ShowDialog();
                    }
                }

            }
            else if (e.ColumnIndex == 11 && FormPrincipal.userNickName.Contains("@") || e.ColumnIndex == 11 && CajaN.usuarioEmpleado.Equals("empleado"))
            {
                if (CajaN.detalleAbonoRetiro.Equals("abono"))
                {
                    using (imprimirTicketDineroAgregadoEmpleado imprimirTicketDineroAgregado = new imprimirTicketDineroAgregadoEmpleado())
                    {
                        imprimirTicketDineroAgregado.idDineroAgregado = Convert.ToInt32(valor);
                        imprimirTicketDineroAgregado.ShowDialog();
                    }
                }
                else
                {
                    imprimirTicketDineroRetiradoEmpleado imprimirTicketDineroRetirado = new imprimirTicketDineroRetiradoEmpleado();
                    imprimirTicketDineroRetirado.idDineroRetirado = Convert.ToInt32(valor);
                    imprimirTicketDineroRetirado.ShowDialog();
                }

            }
        }
    }
}
