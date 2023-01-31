using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
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
    public partial class BuscarReporteCajaPorFecha : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        CargarDatosCaja cdc = new CargarDatosCaja();

        System.Drawing.Image pdf = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");

        private Paginar p;
        // Variables de tipo String
        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "Caja";
        string busqueda = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 10;
        int clickBoton = 0;

        bool conBusqueda = false;

        string mensajeParaMostrar = string.Empty;

        public BuscarReporteCajaPorFecha()
        {
            InitializeComponent();
        }

        private void BuscarReporteCajaPorFecha_Load(object sender, EventArgs e)
        {
            //cargarDGVInicial();
            DateTime date = DateTime.Now;
            DateTime PrimerDia;
            if (!date.Month.Equals(1))
            {
                PrimerDia = new DateTime(date.Year, date.Month - 1, 1);
            }
            else
            {
                PrimerDia = new DateTime(date.Year - 1, date.Month + 11, 1);
            }
            primerDatePicker.Value = PrimerDia;
            segundoDatePicker.Value = DateTime.Now;
            DGVReporteCaja.Rows.Clear();

            conBusqueda = true;

            var datoBuscar = txtBuscador.Text.ToString().Replace("\r\n", string.Empty);
            var primerFecha = primerDatePicker.Value.ToString("yyyy-MM-dd");
            var segundaFecha = segundoDatePicker.Value.AddDays(1).ToString("yyyy-MM-dd");

            var cantidadCortes = validarNewCuentas();
            var primerId = obtenerPrimerCorte();
            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var nombreUser = string.Empty;
            //filtroConSinFiltroAvanzado = cs.BuscadorDeReportesCaja(datoBuscar, primerFecha, segundaFecha, primerId);       
            if (!FormPrincipal.userNickName.Contains("@"))
            {
                filtroConSinFiltroAvanzado = cs.BuscadorReportesCorteDeCajaAdministrador(primerFecha, segundaFecha, datoBuscar);
            }
            else
            {
                filtroConSinFiltroAvanzado = cs.BuscadorReporteCorteDeCajaEmpleado(primerFecha, segundaFecha, datoBuscar);
            }
            txtBuscador.Text = string.Empty;
            txtBuscador.Focus();
            CargarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conBusqueda = true;

            var datoBuscar = txtBuscador.Text.ToString().Replace("\r\n", string.Empty);
            var primerFecha = primerDatePicker.Value.ToString("yyyy-MM-dd");
            var segundaFecha = segundoDatePicker.Value.AddDays(1).ToString("yyyy-MM-dd");

            var cantidadCortes = validarNewCuentas();
            var primerId = obtenerPrimerCorte();

            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var nombreUser = string.Empty;
            //var querry = cn.CargarDatos(cs.BuscadorDeReportesCaja(datoBuscar, primerFecha, segundaFecha, primerId));

            //filtroConSinFiltroAvanzado = cs.BuscadorDeReportesCaja(datoBuscar, primerFecha, segundaFecha, primerId);

            if (!FormPrincipal.userNickName.Contains("@"))
            {
                filtroConSinFiltroAvanzado = cs.BuscadorReportesCorteDeCajaAdministrador(primerFecha, segundaFecha, datoBuscar);
            }
            else
            {
                filtroConSinFiltroAvanzado = cs.BuscadorReporteCorteDeCajaEmpleado(primerFecha, segundaFecha, datoBuscar);
            }

            //if (!querry.Rows.Count.Equals(0))
            //{
            //    foreach (DataRow iterar in querry.Rows)
            //    {
            //        idCorte = iterar["ID"].ToString();
            //        fecha = iterar["FechaOPeracion"].ToString();
            //        idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
            //        empleado = iterar["nombre"].ToString();
            //        nombreUser = iterar["Usuario"].ToString();

            //        if (idEmpleado > 0 )//Cuando es Empleado
            //        {
            //            name = empleado;
            //            DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);

            //        }
            //        else if (idEmpleado.Equals(0)) //Cuando es Admin
            //        {
            //            name = $"ADMIN ({nombreUser})";
            //            DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
            //        }
            //    }
            txtBuscador.Text = string.Empty;
            txtBuscador.Focus();
            //}
            //else
            //{
            //    MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtBuscador.Text = string.Empty;
            //    txtBuscador.Focus();
            //}
            CargarDatos();

            if (DGVReporteCaja.Rows.Count.Equals(0))
            {
                datoBuscar = string.Empty;

                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    filtroConSinFiltroAvanzado = cs.BuscadorReportesCorteDeCajaAdministrador(primerFecha, segundaFecha, datoBuscar);
                }
                else
                {
                    filtroConSinFiltroAvanzado = cs.BuscadorReporteCorteDeCajaEmpleado(primerFecha, segundaFecha, datoBuscar);
                }

                CargarDatos();
            }
        }

        private void cargarDGVInicial()
        {//Cargar el DGV al Abrir el Form
            var primerFecha = DateTime.Today.AddDays(-7).ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.AddDays(1).ToString("yyyy/MM/dd");

            var cantidadCortes = validarNewCuentas();
            var primerId = obtenerPrimerCorte();

            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var fechaOp = string.Empty;
            //var consulta = cn.CargarDatos(cs.CargarDatosIniciarFormReportesCaja(primerFecha, segundaFecha, primerId));
            filtroConSinFiltroAvanzado = cs.CargarDatosIniciarFormReportesCaja(primerFecha, segundaFecha, primerId);
            //if (cantidadCortes > 1)
            //{
            //    if (!consulta.Rows.Count.Equals(0))
            //    {
            //        foreach (DataRow iterar in consulta.Rows)
            //        {
            //            idCorte = iterar["ID"].ToString();
            //            fecha = iterar["FechaOPeracion"].ToString();
            //            idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
            //            empleado = iterar["nombre"].ToString();
            //            fechaOp = iterar["FechaOperacion"].ToString();

            //            if (idEmpleado > 0)
            //            {
            //                name = empleado;
            //            }
            //            else
            //            {
            //                name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
            //            }

            //            //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

            //            DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf, fechaOp);
            //        }
            //    }
            //}
            CargarDatos();
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private int validarNewCuentas()
        {
            var cantidad = 0;
            var validarNewCuenta = cn.CargarDatos($"SELECT COUNT(Operacion) AS Num FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte'");

            if (!validarNewCuenta.Rows.Count.Equals(0))
            {
                cantidad = Convert.ToInt32(validarNewCuenta.Rows[0]["Num"].ToString());
            }

            return cantidad;
        }

        private int obtenerPrimerCorte()
        {
            var result = 0;

            var query = cn.CargarDatos($"SELECT ID FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' ORDER BY FechaOperacion ASC LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                result = Convert.ToInt32(query.Rows[0]["ID"].ToString());
            }

            return result;
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscador.Text.Contains("\'"))
            {
                string producto = txtBuscador.Text.Replace("\'", ""); ;
                txtBuscador.Text = producto;
                txtBuscador.Select(txtBuscador.Text.Length, 0);
            }
            txtBuscador.CharacterCasing = CharacterCasing.Upper;
        }

        private void BuscarReporteCajaPorFecha_Shown(object sender, EventArgs e)
        {
            txtBuscador.Focus();
        }

        private void DGVReporteCaja_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(DGVReporteCaja.CurrentRow.Cells[0].Value.ToString());

            if (e.ColumnIndex.Equals(3))//Corte de Caja
            {
                ////var dato = traerDatosCaja(id);
                //var dato = cdc.CargarSaldo("Reportes", id);
                //GenerarReporte(dato, "CORTE DE CAJA", id);
                //recargarReporteExistente(id);
                var intervaloIDCaja = string.Empty;
                var auxIntervaloIDCaja = string.Empty;
                var IDCajaInicio = 0;
                var IDCajaFin = 0;

                DataTable dtEncabezado = null;
                DataTable dtVenta = null;
                DataTable dtAnticipo = null;
                DataTable dtDineroAgregado = null;
                DataTable dtDineroRetirado = null;
                DataTable dtTotalCaja = null;

                using (DataTable dtContenidoEncabezado = cn.CargarDatos(cs.encabezadoCorteDeCaja(id)))
                {
                    dtEncabezado = dtContenidoEncabezado;
                }

                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    var idEmpleado = 0;

                    using (DataTable dtVerificarSiEsCorteDeEmpleado = cn.CargarDatos(cs.VerificarSiEsCorteDeEmpleado(id)))
                    {
                        if (!dtVerificarSiEsCorteDeEmpleado.Rows.Count.Equals(0))
                        {
                            DataRow drVerificarSiEsEmpleado = dtVerificarSiEsCorteDeEmpleado.Rows[0];
                            idEmpleado = Convert.ToInt32(drVerificarSiEsEmpleado["IdEmpleado"].ToString());
                        }
                    }

                    if (idEmpleado.Equals(0))
                    {
                        using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloVentasRealizadasAdministrador(id)))
                        {
                            if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                                {
                                    auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                                }
                                intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                                var IDsCaja = intervaloIDCaja.Split('|');

                                if (IDsCaja.Length > 0)
                                {
                                    if (IDsCaja.Length.Equals(2))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                        IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                    }
                                    else if (IDsCaja.Length.Equals(1))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    }
                                }

                                var fechaLimiteSuperior = string.Empty;
                                var fechaLimiteInferior = string.Empty;

                                using (DataTable dtRangoFechasAbonos = cn.CargarDatos(cs.intervaloFechasAbonosRealizadosAdministrador(IDCajaInicio, IDCajaFin)))
                                {
                                    if (!dtRangoFechasAbonos.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow item in dtRangoFechasAbonos.Rows)
                                        {
                                            var fecha1 = Convert.ToDateTime(item["LimiteSuperior"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                            var fecha2 = Convert.ToDateTime(item["LimiteInferior"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                            fechaLimiteSuperior = fecha1;
                                            fechaLimiteInferior = fecha2;
                                        }
                                    }
                                }

                                DataTable dtVentasRealizadas = cn.CargarDatos(cs.tablaVentasRealizadasAdministrador(fechaLimiteSuperior, fechaLimiteInferior, IDCajaInicio, IDCajaFin));

                                dtVenta = dtVentasRealizadas;
                            }
                        }

                        using (DataTable dtAnticiposRecibidos = cn.CargarDatos(cs.tablaAnticiposRecibidosAdministrador(IDCajaInicio, IDCajaFin)))
                        {
                            dtAnticipo = dtAnticiposRecibidos;
                        }

                        using (DataTable dtDineroAgregadoCaja = cn.CargarDatos(cs.tablaDineroAgregadoAdministrador(IDCajaInicio, IDCajaFin)))
                        {
                            dtDineroAgregado = dtDineroAgregadoCaja;
                        }

                        using (DataTable dtDineroRetiradoCaja = cn.CargarDatos(cs.tablaDineroRetiradoAdministrador(IDCajaInicio, IDCajaFin)))
                        {
                            dtDineroRetirado = dtDineroRetiradoCaja;
                        }

                        using (DataTable dtTotalDeCajaAlCorte = cn.CargarDatos(cs.tablaTotalDeCajaAlCorteAdministrador(IDCajaInicio, IDCajaFin)))
                        {
                            dtTotalCaja = dtTotalDeCajaAlCorte;
                        }
                    }
                    else if (idEmpleado > 0)
                    {
                        using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloVentasRealizadasEnEmpleadoDesdeAdministrador(idEmpleado, id)))
                        {
                            if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                                {
                                    auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                                }
                                intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                                var IDsCaja = intervaloIDCaja.Split('|');

                                if (IDsCaja.Length > 0)
                                {
                                    if (IDsCaja.Length.Equals(2))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                        IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                    }
                                    else if (IDsCaja.Length.Equals(1))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    }
                                }

                                var fechaLimiteSuperior = string.Empty;
                                var fechaLimiteInferior = string.Empty;

                                using (DataTable dtRangoFechasAbonos = cn.CargarDatos(cs.intervaloFechasAbonosRealizadosEnEmpleadoDesdeAdministrador(idEmpleado, IDCajaInicio, IDCajaFin)))
                                {
                                    if (!dtRangoFechasAbonos.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow item in dtRangoFechasAbonos.Rows)
                                        {
                                            var fecha1 = Convert.ToDateTime(item["LimiteSuperior"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                            var fecha2 = Convert.ToDateTime(item["LimiteInferior"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                            fechaLimiteSuperior = fecha1;
                                            fechaLimiteInferior = fecha2;
                                        }
                                    }
                                }

                                DataTable dtVentasRealizadas = cn.CargarDatos(cs.tablaVentasRealizadasEnEmpleadoDesdeAdministrador(idEmpleado, fechaLimiteSuperior, fechaLimiteInferior, IDCajaInicio, IDCajaFin));

                                dtVenta = dtVentasRealizadas;
                            }
                        }

                        using (DataTable dtAnticiposRecibidos = cn.CargarDatos(cs.tablaAnticiposRecibidosEnEmpleadoDesdeAdministrador(idEmpleado, IDCajaInicio, IDCajaFin)))
                        {
                            dtAnticipo = dtAnticiposRecibidos;
                        }

                        using (DataTable dtDineroAgregadoCaja = cn.CargarDatos(cs.tablaDineroAgregadoEnEmpleadoDesdeAdministrador(idEmpleado, IDCajaInicio, IDCajaFin)))
                        {
                            dtDineroAgregado = dtDineroAgregadoCaja;
                        }

                        using (DataTable dtDineroRetiradoCaja = cn.CargarDatos(cs.tablaDineroRetiradoEnEmpleadoDesdeAdministrador(idEmpleado, IDCajaInicio, IDCajaFin)))
                        {
                            dtDineroRetirado = dtDineroRetiradoCaja;
                        }

                        using (DataTable dtTotalDeCajaAlCorte = cn.CargarDatos(cs.tablaTotalDeCajaAlCorteEnEmpleadoDesdeAdministrador(idEmpleado, IDCajaInicio, IDCajaFin)))
                        {
                            dtTotalCaja = dtTotalDeCajaAlCorte;
                        }
                    }
                }
                else if (FormPrincipal.userNickName.Contains("@"))
                {
                    using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloVentasRealizadasEmpleado(id)))
                    {
                        if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                            {
                                auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                            }
                            intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                            var IDsCaja = intervaloIDCaja.Split('|');

                            if (IDsCaja.Length > 0)
                            {
                                if (IDsCaja.Length.Equals(2))
                                {
                                    IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                }
                                else if (IDsCaja.Length.Equals(1))
                                {
                                    IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                }
                            }

                            var fechaLimiteSuperior = string.Empty;
                            var fechaLimiteInferior = string.Empty;

                            using (DataTable dtRangoFechasAbonos = cn.CargarDatos(cs.intervaloFechasAbonosRealizadosEmpleado(IDCajaInicio, IDCajaFin)))
                            {
                                if (!dtRangoFechasAbonos.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow item in dtRangoFechasAbonos.Rows)
                                    {
                                        var fecha1 = Convert.ToDateTime(item["LimiteSuperior"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                        var fecha2 = Convert.ToDateTime(item["LimiteInferior"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                        fechaLimiteSuperior = fecha1;
                                        fechaLimiteInferior = fecha2;
                                    }
                                }
                            }

                            DataTable dtVentasRealizadas = cn.CargarDatos(cs.tablaVentasRealizadasEmpleado(fechaLimiteSuperior, fechaLimiteInferior, IDCajaInicio, IDCajaFin));

                            dtVenta = dtVentasRealizadas;
                        }
                    }

                    using (DataTable dtAnticiposRecibidos = cn.CargarDatos(cs.tablaAnticiposRecibidosEmpleado(IDCajaInicio, IDCajaFin)))
                    {
                        dtAnticipo = dtAnticiposRecibidos;
                    }

                    using (DataTable dtDineroAgregadoCaja = cn.CargarDatos(cs.tablaDineroAgregadoEmpleado(IDCajaInicio, IDCajaFin)))
                    {
                        dtDineroAgregado = dtDineroAgregadoCaja;
                    }

                    using (DataTable dtDineroRetiradoCaja = cn.CargarDatos(cs.tablaDineroRetiradoEmpleado(IDCajaInicio, IDCajaFin)))
                    {
                        dtDineroRetirado = dtDineroRetiradoCaja;
                    }

                    using (DataTable dtTotalDeCajaAlCorte = cn.CargarDatos(cs.tablaTotalDeCajaAlCorteEmpleado(IDCajaInicio, IDCajaFin)))
                    {
                        dtTotalCaja = dtTotalDeCajaAlCorte;
                    }
                }

                using (visualizadorReimprimirCorteDeCaja form = new visualizadorReimprimirCorteDeCaja())
                {
                    form.FormClosed += delegate
                    {
                        dtEncabezado.Dispose();
                        dtEncabezado = null;
                        dtVenta.Dispose();
                        dtVenta = null;
                        dtAnticipo.Dispose();
                        dtAnticipo = null;
                        dtDineroAgregado.Dispose();
                        dtDineroAgregado = null;
                        dtDineroRetirado.Dispose();
                        dtDineroRetirado = null;
                        dtTotalCaja.Dispose();
                        dtTotalCaja = null;
                    };

                    form.dtEncabezado = dtEncabezado;
                    form.dtVentasRealizadas = dtVenta;
                    form.dtAnticiposRecibidos = dtAnticipo;
                    form.dtDineroAgregado = dtDineroAgregado;
                    form.dtDineroRetirado = dtDineroRetirado;
                    form.dtTotalCorteDeCaja = dtTotalCaja;
                    form.ShowDialog();
                }
            }
            else if (e.ColumnIndex.Equals(4))//Dinero Agregado
            {
                //var dato = obtenerDatosReporte(id, "deposito");
                //var dato = cdc.obtenerDepositosRetiros("Reportes", "deposito", id);
                //if (!dato.Rows.Count.Equals(0))
                //{
                //    GenerarReporteAgregarRetirar("DINERO AGREGADO", dato, id);
                //}
                //else
                //{
                //    MessageBox.Show("No existe información para generar reporte", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                var intervaloIDCaja = string.Empty;
                var auxIntervaloIDCaja = string.Empty;
                var IDCajaInicio = 0;
                var IDCajaFin = 0;

                DataTable dtEncabezado = null;
                DataTable dtDepositos = null;
                DataTable dtDepositosSuma = null;

                using (DataTable dtContenidoEncabezado = cn.CargarDatos(cs.encabezadoCorteDeCaja(id)))
                {
                    dtEncabezado = dtContenidoEncabezado;
                }

                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    var idEmpleado = 0;

                    using (DataTable dtVerificarSiEsCorteDeEmpleado = cn.CargarDatos(cs.VerificarSiEsCorteDeEmpleado(id)))
                    {
                        if (!dtVerificarSiEsCorteDeEmpleado.Rows.Count.Equals(0))
                        {
                            DataRow drVerificarSiEsEmpleado = dtVerificarSiEsCorteDeEmpleado.Rows[0];
                            idEmpleado = Convert.ToInt32(drVerificarSiEsEmpleado["IdEmpleado"].ToString());
                        }
                    }

                    if (idEmpleado.Equals(0))
                    {
                        using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloMovimientosRealizadasAdministrador(id)))
                        {
                            if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                                {
                                    auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                                }
                                intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                                var IDsCaja = intervaloIDCaja.Split('|');

                                if (IDsCaja.Length > 0)
                                {
                                    if (IDsCaja.Length.Equals(2))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                        IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                    }
                                    else if (IDsCaja.Length.Equals(1))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    }
                                }
                            }
                        }

                        using (DataTable dtDepositosRealizados = cn.CargarDatos(cs.ReimprimirHistorialDepositosAdminsitrador(IDCajaInicio, IDCajaFin)))
                        {
                            dtDepositos = dtDepositosRealizados;
                        }

                        using (DataTable dtSumaDepositosRealizados = cn.CargarDatos(cs.ReimprimirCargarHistorialDepositosAdministradorSumaTotal(IDCajaInicio, IDCajaFin)))
                        {
                            dtDepositosSuma = dtSumaDepositosRealizados;
                        }
                    }
                    else if (idEmpleado > 0)
                    {
                        using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloMovimientosRealizadasEnEmpleadoDesdeAdministrador(idEmpleado, id)))
                        {
                            if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                                {
                                    auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                                }
                                intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                                var IDsCaja = intervaloIDCaja.Split('|');

                                if (IDsCaja.Length > 0)
                                {
                                    if (IDsCaja.Length.Equals(2))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                        IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                    }
                                    else if (IDsCaja.Length.Equals(1))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    }
                                }
                            }
                        }

                        using (DataTable dtDepositosRealizados = cn.CargarDatos(cs.ReimprimirHistorialDepositosEmpleadoDesdeAdministrador(IDCajaInicio, IDCajaFin, idEmpleado)))
                        {
                            dtDepositos = dtDepositosRealizados;
                        }

                        using (DataTable dtSumaDepositosRealizados = cn.CargarDatos(cs.ReimprimirCargarHistorialdepositosEmpleadoSumaTotalDesdeAdministrador(IDCajaInicio, IDCajaFin, idEmpleado)))
                        {
                            dtDepositosSuma = dtSumaDepositosRealizados;
                        }
                    }
                }
                else if (FormPrincipal.userNickName.Contains("@"))
                {
                    using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloMovimientosRealizadasEmpleado(id)))
                    {
                        if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                            {
                                auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                            }
                            intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                            var IDsCaja = intervaloIDCaja.Split('|');

                            if (IDsCaja.Length > 0)
                            {
                                if (IDsCaja.Length.Equals(2))
                                {
                                    IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                }
                                else if (IDsCaja.Length.Equals(1))
                                {
                                    IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                }
                            }
                        }
                    }

                    using (DataTable dtDepositosRealizados = cn.CargarDatos(cs.ReimprimirHistorialDepositosEmpleado(IDCajaInicio, IDCajaFin)))
                    {
                        dtDepositos = dtDepositosRealizados;
                    }

                    using (DataTable dtSumaDepositosRealizados = cn.CargarDatos(cs.ReimprimirCargarHistorialdepositosEmpleadoSumaTotal(IDCajaInicio, IDCajaFin)))
                    {
                        dtDepositosSuma = dtSumaDepositosRealizados;
                    }
                }

                using (visualizadorReimprimirDepositosRealizados form = new visualizadorReimprimirDepositosRealizados())
                {
                    form.FormClosed += delegate
                    {
                        dtEncabezado.Dispose();
                        dtEncabezado = null;
                        dtDepositos.Dispose();
                        dtDepositos = null;
                        dtDepositosSuma.Dispose();
                        dtDepositosSuma = null;
                    };

                    form.dtEncabezado = dtEncabezado;
                    form.dtDepositosRealizados = dtDepositos;
                    form.dtSumaDepositosRealizados = dtDepositosSuma;
                    form.ShowDialog();
                }
            }
            else if (e.ColumnIndex.Equals(5))//Dinero Retirado
            {
                //var dato = obtenerDatosReporte(id, "retiro");
                //var dato = cdc.obtenerDepositosRetiros("Reportes", "retiro", id);
                //if (!dato.Rows.Count.Equals(0))
                //{
                //    GenerarReporteAgregarRetirar("DINERO RETIRADO", dato, id);
                //}
                //else
                //{
                //    MessageBox.Show("No existe información para generar reporte", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
                var intervaloIDCaja = string.Empty;
                var auxIntervaloIDCaja = string.Empty;
                var IDCajaInicio = 0;
                var IDCajaFin = 0;

                DataTable dtEncabezado = null;
                DataTable dtRetiros = null;
                DataTable dtRetirosSuma = null;

                using (DataTable dtContenidoEncabezado = cn.CargarDatos(cs.encabezadoCorteDeCaja(id)))
                {
                    dtEncabezado = dtContenidoEncabezado;
                }

                if (!FormPrincipal.userNickName.Contains("@"))
                {
                    var idEmpleado = 0;

                    using (DataTable dtVerificarSiEsCorteDeEmpleado = cn.CargarDatos(cs.VerificarSiEsCorteDeEmpleado(id)))
                    {
                        if (!dtVerificarSiEsCorteDeEmpleado.Rows.Count.Equals(0))
                        {
                            DataRow drVerificarSiEsEmpleado = dtVerificarSiEsCorteDeEmpleado.Rows[0];
                            idEmpleado = Convert.ToInt32(drVerificarSiEsEmpleado["IdEmpleado"].ToString());
                        }
                    }

                    if (idEmpleado.Equals(0))
                    {
                        using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloMovimientosRealizadasAdministrador(id)))
                        {
                            if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                                {
                                    auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                                }
                                intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                                var IDsCaja = intervaloIDCaja.Split('|');

                                if (IDsCaja.Length > 0)
                                {
                                    if (IDsCaja.Length.Equals(2))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                        IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                    }
                                    else if (IDsCaja.Length.Equals(1))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    }
                                }
                            }
                        }

                        using (DataTable dtRetirosRealizados = cn.CargarDatos(cs.ReimprmirHistorialRetirosAdminsitrador(IDCajaInicio, IDCajaFin)))
                        {
                            dtRetiros = dtRetirosRealizados;
                        }

                        using (DataTable dtSumaRetirosRealizados = cn.CargarDatos(cs.ReimprimirCargarHistorialRetirosAdministradorSumaTotal(IDCajaInicio, IDCajaFin)))
                        {
                            dtRetirosSuma = dtSumaRetirosRealizados;
                        }
                    }
                    else if (idEmpleado > 0)
                    {
                        using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloMovimientosRealizadasEnEmpleadoDesdeAdministrador(idEmpleado, id)))
                        {
                            if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                                {
                                    auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                                }
                                intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                                var IDsCaja = intervaloIDCaja.Split('|');

                                if (IDsCaja.Length > 0)
                                {
                                    if (IDsCaja.Length.Equals(2))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                        IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                    }
                                    else if (IDsCaja.Length.Equals(1))
                                    {
                                        IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    }
                                }
                            }
                        }

                        using (DataTable dtRetirosRealizados = cn.CargarDatos(cs.ReimprimirHistorialRetirosEmpleadoDesdeAdministrador(IDCajaInicio, IDCajaFin, idEmpleado)))
                        {
                            dtRetiros = dtRetirosRealizados;
                        }

                        using (DataTable dtSumaRetirosRealizados = cn.CargarDatos(cs.ReimprimirCargarHistorialRetirosEmpleadoSumaTotalDesdeAdministrador(IDCajaInicio, IDCajaFin, idEmpleado)))
                        {
                            dtRetirosSuma = dtSumaRetirosRealizados;
                        }
                    }
                }
                else if (FormPrincipal.userNickName.Contains("@"))
                {
                    using (DataTable dtIntervaloDeIDCorteDeCaja = cn.CargarDatos(cs.intervaloMovimientosRealizadasEmpleado(id)))
                    {
                        if (!dtIntervaloDeIDCorteDeCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtIntervaloDeIDCorteDeCaja.Rows)
                            {
                                auxIntervaloIDCaja += $"{item["IDCorteDeCaja"].ToString()}|";
                            }
                            intervaloIDCaja = auxIntervaloIDCaja.Substring(0, auxIntervaloIDCaja.Length - 1);

                            var IDsCaja = intervaloIDCaja.Split('|');

                            if (IDsCaja.Length > 0)
                            {
                                if (IDsCaja.Length.Equals(2))
                                {
                                    IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                    IDCajaFin = Convert.ToInt32(IDsCaja[1].ToString());
                                }
                                else if (IDsCaja.Length.Equals(1))
                                {
                                    IDCajaInicio = Convert.ToInt32(IDsCaja[0].ToString());
                                }
                            }
                        }
                    }

                    using (DataTable dtRetirosRealizados = cn.CargarDatos(cs.ReimprimirHistorialRetirosEmpleado(IDCajaInicio, IDCajaFin)))
                    {
                        dtRetiros = dtRetirosRealizados;
                    }

                    using (DataTable dtSumaRetirosRealizados = cn.CargarDatos(cs.ReimprimirCargarHistorialRetirosEmpleadoSumaTotal(IDCajaInicio, IDCajaFin)))
                    {
                        dtRetirosSuma = dtSumaRetirosRealizados;
                    }
                }

                using (visualizadorReimprimirRetirosRealizados form = new visualizadorReimprimirRetirosRealizados())
                {
                    form.FormClosed += delegate
                    {
                        dtEncabezado.Dispose();
                        dtEncabezado = null;
                        dtRetiros.Dispose();
                        dtRetiros = null;
                        dtRetirosSuma.Dispose();
                        dtRetirosSuma = null;
                    };

                    form.dtEncabezado = dtEncabezado;
                    form.dtRetirosRealizados = dtRetiros;
                    form.dtSumaRetirosRealizados = dtRetirosSuma;
                    form.ShowDialog();
                }
            }
        }

        private void recargarReporteExistente(int id)
        {
            var rutaArchivo = string.Empty;

            var query = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");
            var fechaReporte = string.Empty;
            var fecha = string.Empty;

            if (!query.Rows.Count.Equals(0))
            {
                fecha = query.Rows[0]["FechaOperacion"].ToString();
            }

            DateTime date = DateTime.Parse(fecha);

            fechaReporte = date.ToString("yyyyMMddHHmmss");

            //fechaReporte = fechaReporte.Replace("/", "");
            //fechaReporte = fechaReporte.Replace(":", "");
            //fechaReporte = fechaReporte.Replace(" ", "");

            //rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Caja\reporte_corte_{fechaReporte}.pdf";

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\reporte_corte_{fechaReporte}.pdf";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Caja\reporte_corte_{fechaReporte}.pdf";
            }

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }

        private string[] traerDatosCaja(int id)
        {
            List<string> lista = new List<string>();

            var fechas = obtenerFechas(id);
            DateTime fecha1 = Convert.ToDateTime(fechas[1]);
            DateTime fecha2 = Convert.ToDateTime(fechas[0]);

            var saldoInicial = mb.SaldoInicialCajaReportes(FormPrincipal.userID, id);

            var cantidadFinal = 0f;
            var consultaUltimoCorte = cn.CargarDatos($"SELECT CantidadRetiradaCorte FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' AND ID  = '{id}' ORDER BY FechaOperacion DESC");


            //var consultaAlterna = $"SELECT IFNULL(SUM(Cantidad), 0) AS Total FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion != 'retiro' AND Operacion != 'corte' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')";

            //Consulta caja
            var totalCantidadCaja = 0f;
            var consultaRetiradoCorte = cn.CargarDatos(/*consultaAlterna*/$"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion != 'retiro' AND Operacion != 'corte' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");

            //Consulta Ventas
            var totalC = string.Empty; var efectivoC = string.Empty; var tarjetaC = string.Empty; var valesC = string.Empty; var chequeC = string.Empty; var transC = string.Empty; var creditoC = string.Empty;
            var consulta = cn.CargarDatos($"SELECT Cantidad, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'venta' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");

            //Consulta Abonos
            var totalA = string.Empty; var efectivoA = string.Empty; var tarjetaA = string.Empty; var valesA = string.Empty; var chequeA = string.Empty; var transA = string.Empty;
            var segundaConsulta = cn.CargarDatos($"SELECT IFNULL(SUM(Total),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");

            //Consulta Devoluciones
            var totalDevol = string.Empty;
            var consultaDevoluciones = cn.CargarDatos($"SELECT IFNULL(SUM(Total),0) AS Total FROM Devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");

            //Consulta dinero Retirado
            var totalR = string.Empty; var efectivoR = string.Empty; var tarjetaR = string.Empty; var valesR = string.Empty; var chequeR = string.Empty; var transR = string.Empty; var anticiposR = string.Empty;
            var consultaRetiro = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans, IFNULL(SUM(Credito),0) AS Credito, IFNULL(SUM(Anticipo),0) AS Anticipo FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'retiro' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");

            //Consulta Anticipos
            var totalAnt = string.Empty; var efectivoAnt = string.Empty; var tarjetaAnt = string.Empty; var valesAnt = string.Empty; var chequeAnt = string.Empty; var transAnt = string.Empty;
            var consultaAnticipos = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'anticipo' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");


            //Consulta Dinero Agregado
            var totalAg = string.Empty; var efectivoAg = string.Empty; var tarjetaAg = string.Empty; var valesAg = string.Empty; var chequeAg = string.Empty; var transAg = string.Empty; var anticiposA = string.Empty;
            var consultaDineroAgregado = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans, IFNULL(SUM(Credito),0) AS Credito, IFNULL(SUM(Anticipo),0) AS Anticipo FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'deposito' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd HH:mm:ss")}')");



            var dTotal = 0f; var dEfectivo = 0f; var dTarjeta = 0f; var dVales = 0f; var dCheque = 0f; var dTrans = 0f; var dCredito = 0f;
            var saltar = 0;
            if (!consulta.Rows.Count.Equals(0))//Datos caja
            {
                //totalC = consulta.Rows[0]["Total"].ToString();
                //efectivoC = consulta.Rows[0]["Efectivo"].ToString();
                //tarjetaC = consulta.Rows[0]["Tarjeta"].ToString();
                //valesC = consulta.Rows[0]["Vales"].ToString();
                //chequeC = consulta.Rows[0]["Cheque"].ToString();
                //transC = consulta.Rows[0]["Trans"].ToString();
                //creditoC = consulta.Rows[0]["Credito"].ToString();

                foreach (DataRow iterador in consulta.Rows)
                {
                    if (saltar.Equals(0))
                    {
                        saltar++;
                        continue;
                    }

                    dTotal += (float)Convert.ToDouble(iterador["Cantidad"].ToString());
                    dEfectivo += (float)Convert.ToDouble(iterador["Efectivo"].ToString());
                    dTarjeta += (float)Convert.ToDouble(iterador["Tarjeta"].ToString());
                    dVales += (float)Convert.ToDouble(iterador["Vales"].ToString());
                    dCheque += (float)Convert.ToDouble(iterador["Cheque"].ToString());
                    dTrans += (float)Convert.ToDouble(iterador["Transferencia"].ToString());
                    dCredito += (float)Convert.ToDouble(iterador["Credito"].ToString());
                }
                totalC = dTotal.ToString();
                efectivoC = dEfectivo.ToString();
                tarjetaC = dTarjeta.ToString();
                valesC = dVales.ToString();
                chequeC = dCheque.ToString();
                transC = dTrans.ToString();
                creditoC = dCredito.ToString();
            }
            else
            {
                totalC = dTotal.ToString();
                efectivoC = dEfectivo.ToString();
                tarjetaC = dTarjeta.ToString();
                valesC = dVales.ToString();
                chequeC = dCheque.ToString();
                transC = dTrans.ToString();
                creditoC = dCredito.ToString();
            }

            if (!segundaConsulta.Rows.Count.Equals(0))//Datos abonos
            {
                totalA = segundaConsulta.Rows[0]["Total"].ToString();
                efectivoA = segundaConsulta.Rows[0]["Efectivo"].ToString();
                tarjetaA = segundaConsulta.Rows[0]["Tarjeta"].ToString();
                valesA = segundaConsulta.Rows[0]["Vales"].ToString();
                chequeA = segundaConsulta.Rows[0]["Cheque"].ToString();
                transA = segundaConsulta.Rows[0]["Trans"].ToString();
            }

            if (!consultaDevoluciones.Rows.Count.Equals(0))
            {
                totalDevol = consultaDevoluciones.Rows[0]["Total"].ToString();
            }

            if (!consultaRetiro.Rows.Count.Equals(0))//Dinero Retirado
            {
                totalR = consultaRetiro.Rows[0]["Total"].ToString();
                efectivoR = consultaRetiro.Rows[0]["Efectivo"].ToString();
                tarjetaR = consultaRetiro.Rows[0]["Tarjeta"].ToString();
                valesR = consultaRetiro.Rows[0]["Vales"].ToString();
                chequeR = consultaRetiro.Rows[0]["Cheque"].ToString();
                transR = consultaRetiro.Rows[0]["Trans"].ToString();
                anticiposR = consultaRetiro.Rows[0]["Anticipo"].ToString();
            }

            if (!consultaAnticipos.Rows.Count.Equals(0))//Anticipos
            {
                totalAnt = consultaAnticipos.Rows[0]["Total"].ToString();
                efectivoAnt = consultaAnticipos.Rows[0]["Efectivo"].ToString();
                tarjetaAnt = consultaAnticipos.Rows[0]["Tarjeta"].ToString();
                valesAnt = consultaAnticipos.Rows[0]["Vales"].ToString();
                chequeAnt = consultaAnticipos.Rows[0]["Cheque"].ToString();
                transAnt = consultaAnticipos.Rows[0]["Trans"].ToString();

            }

            if (!consultaDineroAgregado.Rows.Count.Equals(0))//Dinero Agregado
            {
                totalAg = consultaDineroAgregado.Rows[0]["Total"].ToString();
                efectivoAg = consultaDineroAgregado.Rows[0]["Efectivo"].ToString();
                tarjetaAg = consultaDineroAgregado.Rows[0]["Tarjeta"].ToString();
                valesAg = consultaDineroAgregado.Rows[0]["Vales"].ToString();
                chequeAg = consultaDineroAgregado.Rows[0]["Cheque"].ToString();
                transAg = consultaDineroAgregado.Rows[0]["Trans"].ToString();
                anticiposA = consultaDineroAgregado.Rows[0]["Anticipo"].ToString();
            }

            if (!consultaRetiradoCorte.Rows.Count.Equals(0))
            {
                totalCantidadCaja = float.Parse(consultaRetiradoCorte.Rows[0]["Total"].ToString());
            }

            if (!consultaUltimoCorte.Rows.Count.Equals(0))
            {
                cantidadFinal = float.Parse(consultaUltimoCorte.Rows[0]["CantidadRetiradaCorte"].ToString());
            }

            var dineroRetiradoCorte = (/*(totalCantidadCaja - float.Parse(totalR)) - */cantidadFinal);

            //Sumar cantidades
            var ventas = ((float)Convert.ToDecimal(efectivoC) + (float)Convert.ToDecimal(tarjetaC) + (float)Convert.ToDecimal(valesC) + (float)Convert.ToDecimal(chequeC) + (float)Convert.ToDecimal(transC) + (float)Convert.ToDecimal(creditoC) + (float)Convert.ToDecimal(totalA) + (float)Convert.ToDecimal(anticiposA));

            var anticipos = ((float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(transAnt));

            var agregado = ((float)Convert.ToDecimal(efectivoAg) + (float)Convert.ToDecimal(tarjetaAg) + (float)Convert.ToDecimal(valesAg) + (float)Convert.ToDecimal(chequeAg) + (float)Convert.ToDecimal(transAg));

            var retirado = ((float)Convert.ToDecimal(efectivoR) + (float)Convert.ToDecimal(tarjetaR) + (float)Convert.ToDecimal(valesR) + (float)Convert.ToDecimal(chequeR) + (float)Convert.ToDecimal(transR) + (float)Convert.ToDecimal(anticiposR) + (float)Convert.ToDecimal(totalDevol));

            var totalAnticipos = ((float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(transAnt));


            var rowEfectivo = (((float)Convert.ToDecimal(efectivoC) + (float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(efectivoAg) + (float)Convert.ToDecimal(efectivoA)) - (float)Convert.ToDecimal(efectivoR));
            var rowTarjeta = (((float)Convert.ToDecimal(tarjetaC) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(tarjetaAg) + (float)Convert.ToDecimal(tarjetaA)) - (float)Convert.ToDecimal(tarjetaR));
            var rowVales = (((float)Convert.ToDecimal(valesC) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(valesAg) + (float)Convert.ToDecimal(valesA)) - (float)Convert.ToDecimal(valesR));
            var rowCheque = (((float)Convert.ToDecimal(chequeC) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(chequeAg) + (float)Convert.ToDecimal(chequeA)) - (float)Convert.ToDecimal(chequeR));
            var rowTransferencia = (((float)Convert.ToDecimal(transC) + (float)Convert.ToDecimal(transAnt) + (float)Convert.ToDecimal(transAg) + (float)Convert.ToDecimal(transA)) - (float)Convert.ToDecimal(transR));

            var totalAntesCorte = ((rowEfectivo + rowTarjeta + rowVales + rowCheque + rowTransferencia + saldoInicial /*+ (float)Convert.ToDecimal(creditoC)*/) - dineroRetiradoCorte);

            var total = /*(*/(rowEfectivo + rowTarjeta + rowVales + rowCheque + rowTransferencia + saldoInicial /*+ (float)Convert.ToDecimal(creditoC)*/)/* - dineroRetiradoCorte)*/;

            var totEfectivo = (((float)Convert.ToDecimal(efectivoC) + (float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(efectivoAg) + (float)Convert.ToDecimal(efectivoA)) - (float)Convert.ToDecimal(efectivoR));
            if (totEfectivo < 0) { totEfectivo = 0; }
            lista.Add("Efectivo:|" + Convert.ToDecimal(efectivoC).ToString("C") + "|Efectivo:|" + Convert.ToDecimal(efectivoAnt).ToString("C") + "|Efectivo:|" + Convert.ToDecimal(efectivoAg).ToString("C") + "|Efectivo:|" + Convert.ToDecimal(efectivoR).ToString("C") + "|Efectivo:|" + totEfectivo.ToString("C"));
            //lista.Add("Efectivo:" + "|Efectivo:" + "|Efectivo:" + "|Efectivo:" + "|Efectivo:" );

            //lista.Add($"{efectivoC} | {efectivoAnt} | {efectivoAg} | {efectivoR} | {((float)Convert.ToDecimal(efectivoC) + (float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(efectivoAg) + (float)Convert.ToDecimal(efectivoR))}");

            var totTarjeta = (((float)Convert.ToDecimal(tarjetaC) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(tarjetaAg) + (float)Convert.ToDecimal(tarjetaA)) - (float)Convert.ToDecimal(tarjetaR));
            if (totTarjeta < 0) { totTarjeta = 0; }
            lista.Add("Tarjeta:|" + Convert.ToDecimal(tarjetaC).ToString("C") + "|Tarjeta:|" + Convert.ToDecimal(tarjetaAnt).ToString("C") + "|Tarjeta:|" + Convert.ToDecimal(tarjetaAg).ToString("C") + "|Tarjeta:|" + Convert.ToDecimal(tarjetaR).ToString("C") + "|Tarjeta:|" + totTarjeta.ToString("C"));

            var totVales = (((float)Convert.ToDecimal(valesC) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(valesAg) + (float)Convert.ToDecimal(valesA)) - (float)Convert.ToDecimal(valesR));
            if (totVales < 0) { totVales = 0; }
            lista.Add("Vales:|" + Convert.ToDecimal(valesC).ToString("C") + "|Vales:|" + Convert.ToDecimal(valesAnt).ToString("C") + "|Vales:|" + Convert.ToDecimal(valesAg).ToString("C") + "|Vales:|" + Convert.ToDecimal(valesR).ToString("C") + "|Vales:|" + totVales.ToString("C"));

            var totCheque = (((float)Convert.ToDecimal(chequeC) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(chequeAg) + (float)Convert.ToDecimal(chequeA)) - (float)Convert.ToDecimal(chequeR));
            if (totCheque < 0) { totCheque = 0; }
            lista.Add("Cheque:|" + Convert.ToDecimal(chequeC).ToString("C") + "|Cheque:|" + Convert.ToDecimal(chequeAnt).ToString("C") + "|Cheque:|" + Convert.ToDecimal(chequeAg).ToString("C") + "|Cheque:|" + Convert.ToDecimal(chequeR).ToString("C") + "|Cheque:|" + totCheque.ToString("C"));

            var totTrans = (((float)Convert.ToDecimal(transC) + (float)Convert.ToDecimal(transAnt) + (float)Convert.ToDecimal(transAg) + (float)Convert.ToDecimal(transA)) - (float)Convert.ToDecimal(transR));
            if (totTrans < 0) { totTrans = 0; }
            lista.Add("Transferencia:|" + Convert.ToDecimal(transC).ToString("C") + "|Transferencia:|" + Convert.ToDecimal(transAnt).ToString("C") + "|Transferencia:|" + Convert.ToDecimal(transAg).ToString("C") + "|Transferencia:|" + Convert.ToDecimal(transR).ToString("C") + "|Transferencia:|" + totTrans.ToString("C"));

            lista.Add("Crédito:|" + Convert.ToDecimal(creditoC).ToString("C") + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|Anticipos Utilizados:|" + Convert.ToDecimal(anticiposR).ToString("C") + "|Saldo Inicial:|" + saldoInicial.ToString("C"));

            lista.Add("Abonos:|" + Convert.ToDecimal(totalA).ToString("C") + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|Devoluciones:|" + Convert.ToDecimal(totalDevol).ToString("C") + "|Crédito:|" + Convert.ToDecimal(creditoC).ToString("C"));

            lista.Add("Anticipos Utilizados:|" + Convert.ToDecimal(anticiposA).ToString("C") + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty);

            lista.Add(string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + "Cantidad retirada al corte:" + "|" + cantidadFinal.ToString("C"));

            var restoCash = (total - cantidadFinal);

            lista.Add(string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + string.Empty + "|" + "Total en Caja antes del corte:" + "|" + /*totalAntesCorte*/total.ToString("C"));

            lista.Add("Total Ventas:|" + ventas.ToString("C") + "|Total Anticipos:|" + anticipos.ToString("C") + "|Total Agregado:|" + agregado.ToString("C") + "|Total Retirado:|" + retirado.ToString("C") + "|Total en Caja despues del corte:|" + restoCash.ToString("C"));



            return lista.ToArray();
        }



        private string[] obtenerFechas(int id)
        {
            List<string> lista = new List<string>();

            var fecha = string.Empty;

            var primerFecha = string.Empty; var segundaFecha = string.Empty;
            var query = cn.CargarDatos($"SELECT DISTINCT FechaOperacion FROM Caja  WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");
            var segundaQuery = cn.CargarDatos($"SELECT DISTINCT FechaOperacion FROM Caja  WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' AND ID < '{id}' ORDER BY FechaOperacion DESC LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                segundaFecha = query.Rows[0]["FechaOperacion"].ToString();

                if (!segundaQuery.Rows.Count.Equals(0))
                {
                    primerFecha = segundaQuery.Rows[0]["FechaOperacion"].ToString();
                }
                else
                {
                    primerFecha = $"<{segundaFecha}";
                }

                lista.Add(segundaFecha.ToString());
                lista.Add(primerFecha);
            }

            //if (tipoBusqueda.Equals("Corte"))//Cuando es corte de caja
            //{

            //}
            //else if (tipoBusqueda.Equals("DA"))//Cuanto es dinero agregado
            //{

            //}
            //else if (tipoBusqueda.Equals("DR"))//Cuando es dinero retirado
            //{

            //}
            return lista.ToArray();
        }

        #region Generar reporte de corte de caja
        private void GenerarReporte(string[] datosCaja, string reportType, int id)
        {
            var mostrarClave = FormPrincipal.clave;

            //Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            //Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;

            //Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\caja.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";
            }

            var fechaHoy = DateTime.Now;
            //rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(reportType, fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph Empleado = new Paragraph("");

            Paragraph NumeroFolio = new Paragraph("");

            //string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            //Encabezado del reporte
            encabezadoTipoReporte = reportType;


            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();

            var numFolio = obtenerFolio(id);

            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteNormal);

            Paragraph subTitulo = new Paragraph($"REPORTE DE CAJA\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            if (!string.IsNullOrEmpty(UsuarioActivo)) { Empleado.Alignment = Element.ALIGN_CENTER; }
            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[] { 60f, 40f, 60f, 40f, 60f, 40f, 60f, 40f, 60f, 40f };

            //Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(10);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            //PdfPCell colNum = new PdfPCell(new Phrase("No:", fuenteNegrita));
            //colNum.BorderWidth = 1;
            //colNum.BackgroundColor = new BaseColor(Color.SkyBlue);
            //colNum.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colVentas = new PdfPCell(new Phrase("VENTAS", fuenteNegrita));
            colVentas.BorderWidth = 1;
            colVentas.Colspan = 2;
            colVentas.BackgroundColor = new BaseColor(Color.SkyBlue);
            colVentas.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colAnticipos = new PdfPCell(new Phrase("ANTICIPOS RECIBIDOS", fuenteTotales));
            colAnticipos.BorderWidth = 1;
            colAnticipos.Colspan = 2;
            colAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticipos.Padding = 3;
            colAnticipos.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colAgregado = new PdfPCell(new Phrase("DINERO AGREGADO", fuenteTotales));
            colAgregado.BorderWidth = 1;
            colAgregado.Colspan = 2;
            colAgregado.HorizontalAlignment = Element.ALIGN_CENTER;
            colAgregado.Padding = 3;
            colAgregado.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRetirado = new PdfPCell(new Phrase("DINERO RETIRADO", fuenteTotales));
            colRetirado.BorderWidth = 1;
            colRetirado.Colspan = 2;
            colRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetirado.Padding = 3;
            colRetirado.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL DE CAJA", fuenteTotales));
            colTotal.BorderWidth = 1;
            colTotal.Colspan = 2;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;
            colTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

            //tablaInventario.AddCell(colNum);
            tablaInventario.AddCell(colVentas);
            tablaInventario.AddCell(colAnticipos);
            tablaInventario.AddCell(colAgregado);
            tablaInventario.AddCell(colRetirado);
            tablaInventario.AddCell(colTotal);

            foreach (var iterador in datosCaja)
            {
                string[] words = iterador.ToString().Split('|');
                //numRow += 1;

                //PdfPCell colNoFila = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                //colNoFila.BorderWidthLeft = 0;
                //colNoFila.BorderWidthTop = 0;
                //colNoFila.BorderWidthBottom = 0;
                //colNoFila.BorderWidthRight = 0;
                //colNoFila.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colVentasTemp = new PdfPCell(new Phrase(words[0].ToString(), fuenteNormal));
                PdfPCell colVentasCantidad = new PdfPCell(new Phrase(words[1].ToString(), fuenteNormal));

                if (words[0].ToString().Equals("Total Ventas:"))
                {
                    colVentasTemp = new PdfPCell(new Phrase(words[0].ToString(), fuenteNegrita));
                    colVentasCantidad = new PdfPCell(new Phrase(words[1].ToString(), fuenteNegrita));
                }
                //colVentasTemp = new PdfPCell(new Phrase(words[0].ToString(), fuenteNormal));
                colVentasTemp.BorderWidthLeft = 0;
                colVentasTemp.BorderWidthTop = 0;
                colVentasTemp.BorderWidthBottom = 0;
                colVentasTemp.BorderWidthRight = 0;
                colVentasTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                //colVentasCantidad = new PdfPCell(new Phrase(words[1].ToString(), fuenteNormal));
                colVentasCantidad.BorderWidthRight = 0;
                colVentasCantidad.BorderWidthTop = 0;
                colVentasCantidad.BorderWidthBottom = 0;
                colVentasCantidad.BorderWidthLeft = 0;
                colVentasCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                //    PdfPCell colAnticiposTemp = new PdfPCell(new Phrase(words[1].ToString(), fuenteNormal));
                //    colAnticiposTemp.BorderWidth = 1;
                //    colAnticiposTemp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colAnticiposTemp = new PdfPCell(new Phrase(words[2].ToString(), fuenteNormal));
                PdfPCell colAnticiposCantidad = new PdfPCell(new Phrase(words[3].ToString(), fuenteNormal));

                if (words[2].ToString().Equals("Total Anticipos:"))
                {
                    colAnticiposTemp = new PdfPCell(new Phrase(words[2].ToString(), fuenteNegrita));
                    colAnticiposCantidad = new PdfPCell(new Phrase(words[3].ToString(), fuenteNegrita));
                }
                //PdfPCell colAnticiposTemp = new PdfPCell(new Phrase(words[2].ToString(), fuenteNormal));
                colAnticiposTemp.BorderWidthLeft = 0;
                colAnticiposTemp.BorderWidthTop = 0;
                colAnticiposTemp.BorderWidthBottom = 0;
                colAnticiposTemp.BorderWidthRight = 0;
                colAnticiposTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colAnticiposCantidad = new PdfPCell(new Phrase(words[3].ToString(), fuenteNormal));
                colAnticiposCantidad.BorderWidthRight = 0;
                colAnticiposCantidad.BorderWidthTop = 0;
                colAnticiposCantidad.BorderWidthBottom = 0;
                colAnticiposCantidad.BorderWidthLeft = 0;
                colAnticiposCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colAgregadoTemp = new PdfPCell(new Phrase(words[4].ToString(), fuenteNormal));
                PdfPCell colAgregadoCantidad = new PdfPCell(new Phrase(words[5].ToString(), fuenteNormal));

                if (words[4].ToString().Equals("Total Agregado:"))
                {
                    colAgregadoTemp = new PdfPCell(new Phrase(words[4].ToString(), fuenteNegrita));
                    colAgregadoCantidad = new PdfPCell(new Phrase(words[5].ToString(), fuenteNegrita));
                }
                //PdfPCell colAgregadoTemp = new PdfPCell(new Phrase(words[4].ToString(), fuenteNormal));
                colAgregadoTemp.BorderWidthLeft = 0;
                colAgregadoTemp.BorderWidthTop = 0;
                colAgregadoTemp.BorderWidthBottom = 0;
                colAgregadoTemp.BorderWidthRight = 0;
                colAgregadoTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colAgregadoCantidad = new PdfPCell(new Phrase(words[5].ToString(), fuenteNormal));
                colAgregadoCantidad.BorderWidthRight = 0;
                colAgregadoCantidad.BorderWidthTop = 0;
                colAgregadoCantidad.BorderWidthBottom = 0;
                colAgregadoCantidad.BorderWidthLeft = 0;
                colAgregadoCantidad.HorizontalAlignment = Element.ALIGN_CENTER;


                PdfPCell colRetiradoTEmp = new PdfPCell(new Phrase(words[6].ToString(), fuenteNormal));
                PdfPCell colRetiradoCantidad = new PdfPCell(new Phrase(words[7].ToString(), fuenteNormal));

                if (words[6].ToString().Equals("Total Retirado:") || words[6].ToString().Equals("Cantidad retirada al corte:") || words[6].ToString().Equals("Total en Caja antes del corte:"))
                {
                    colRetiradoTEmp = new PdfPCell(new Phrase(words[6].ToString(), fuenteNegrita));
                    colRetiradoCantidad = new PdfPCell(new Phrase(words[7].ToString(), fuenteNegrita));
                }

                //PdfPCell colRetiradoTEmp = new PdfPCell(new Phrase(words[6].ToString(), fuenteNormal));
                colRetiradoTEmp.BorderWidthLeft = 0;
                colRetiradoTEmp.BorderWidthTop = 0;
                colRetiradoTEmp.BorderWidthBottom = 0;
                colRetiradoTEmp.BorderWidthRight = 0;
                colRetiradoTEmp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colRetiradoCantidad = new PdfPCell(new Phrase(words[7].ToString(), fuenteNormal));
                colRetiradoCantidad.BorderWidthRight = 0;
                colRetiradoCantidad.BorderWidthTop = 0;
                colRetiradoCantidad.BorderWidthBottom = 0;
                colRetiradoCantidad.BorderWidthLeft = 0;
                colRetiradoCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                //    PdfPCell colTotalTemp = new PdfPCell(new Phrase(words[4].ToString(), fuenteNormal));
                //    colTotalTemp.BorderWidth = 1;
                //    colTotalTemp.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfPCell colTotalTemp = new PdfPCell(new Phrase(words[8].ToString(), fuenteNormal));
                PdfPCell colTotalCantidad = new PdfPCell(new Phrase(words[9].ToString(), fuenteNormal));

                if (words[8].ToString().Equals("Total en Caja despues del corte:"))
                {
                    colTotalTemp = new PdfPCell(new Phrase(words[8].ToString(), fuenteNegrita));
                    colTotalCantidad = new PdfPCell(new Phrase(words[9].ToString(), fuenteNegrita));
                }

                //colTotalTemp = new PdfPCell(new Phrase(words[8].ToString(), fuenteNormal));
                colTotalTemp.BorderWidthLeft = 0;
                colTotalTemp.BorderWidthTop = 0;
                colTotalTemp.BorderWidthBottom = 0;
                colTotalTemp.BorderWidthRight = 0;
                colTotalTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                //colTotalCantidad = new PdfPCell(new Phrase(words[9].ToString(), fuenteNormal));
                colTotalCantidad.BorderWidthRight = 0;
                colTotalCantidad.BorderWidthTop = 0;
                colTotalCantidad.BorderWidthBottom = 0;
                colTotalCantidad.BorderWidthLeft = 0;
                colTotalCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

                //tablaInventario.AddCell(colNoConceptoTmp);
                //tablaInventario.AddCell(colNoFila);
                tablaInventario.AddCell(colVentasTemp);
                tablaInventario.AddCell(colVentasCantidad);
                tablaInventario.AddCell(colAnticiposTemp);
                tablaInventario.AddCell(colAnticiposCantidad);
                tablaInventario.AddCell(colAgregadoTemp);
                tablaInventario.AddCell(colAgregadoCantidad);
                tablaInventario.AddCell(colRetiradoTEmp);
                tablaInventario.AddCell(colRetiradoCantidad);
                tablaInventario.AddCell(colTotalTemp);
                tablaInventario.AddCell(colTotalCantidad);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (!string.IsNullOrEmpty(UsuarioActivo)) { reporte.Add(Empleado); }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO ===
            //================================

            //reporte.AddTitle("Reporte Caja");
            //reporte.AddAuthor("PUDVE");
            //reporte.Close();
            reporte.Add(linea);

            var fechas = obtenerFechas(id);
            DateTime primerFecha = Convert.ToDateTime(fechas[1]);
            DateTime segundaFecha = Convert.ToDateTime(fechas[0]);

            anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f };

            Paragraph tituloDepositos = new Paragraph("HISTORIAL DE DEPOSITOS\n\n", fuenteGrande);
            tituloDepositos.Alignment = Element.ALIGN_CENTER;

            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'deposito' AND (FechaOperacion BETWEEN '{primerFecha.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{segundaFecha.ToString("yyyy-MM-dd HH:mm:ss")}')", sql_con);
            dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                PdfPTable tablaDepositos = new PdfPTable(7);
                tablaDepositos.WidthPercentage = 100;
                tablaDepositos.SetWidths(anchoColumnas);

                PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                colEmpleado.BorderWidth = 0;
                colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleado.Padding = 3;

                PdfPCell colDepositoEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                colDepositoEfectivo.BorderWidth = 0;
                colDepositoEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoEfectivo.Padding = 3;

                PdfPCell colDepositoTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                colDepositoTarjeta.BorderWidth = 0;
                colDepositoTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTarjeta.Padding = 3;

                PdfPCell colDepositoVales = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                colDepositoVales.BorderWidth = 0;
                colDepositoVales.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoVales.Padding = 3;

                PdfPCell colDepositoCheque = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                colDepositoCheque.BorderWidth = 0;
                colDepositoCheque.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoCheque.Padding = 3;

                PdfPCell colDepositoTrans = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                colDepositoTrans.BorderWidth = 0;
                colDepositoTrans.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTrans.Padding = 3;

                PdfPCell colDepositoFecha = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                colDepositoFecha.BorderWidth = 0;
                colDepositoFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoFecha.Padding = 3;

                tablaDepositos.AddCell(colEmpleado);
                tablaDepositos.AddCell(colDepositoEfectivo);
                tablaDepositos.AddCell(colDepositoTarjeta);
                tablaDepositos.AddCell(colDepositoVales);
                tablaDepositos.AddCell(colDepositoCheque);
                tablaDepositos.AddCell(colDepositoTrans);
                tablaDepositos.AddCell(colDepositoFecha);

                while (dr.Read())
                {
                    var efectivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                    var tarjeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                    var vales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                    var cheque = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                    var trans = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                    var fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                    int usuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdEmpleado")));

                    var autorMovimiento = string.Empty;
                    if (usuario != 0)
                    {
                        autorMovimiento = cs.BuscarEmpleadoCaja(usuario);
                    }
                    else
                    {
                        autorMovimiento = "ADMIN (";
                        autorMovimiento += cs.validarEmpleadoPorID();
                        autorMovimiento += ")";
                    }

                    PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(autorMovimiento, fuenteNormal));
                    colEmpleadoTmp.BorderWidth = 0;
                    colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoEfectivoTmp = new PdfPCell(new Phrase("$" + efectivo, fuenteNormal));
                    colDepositoEfectivoTmp.BorderWidth = 0;
                    colDepositoEfectivoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTarjetaTmp = new PdfPCell(new Phrase("$" + tarjeta, fuenteNormal));
                    colDepositoTarjetaTmp.BorderWidth = 0;
                    colDepositoTarjetaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoValesTmp = new PdfPCell(new Phrase("$" + vales, fuenteNormal));
                    colDepositoValesTmp.BorderWidth = 0;
                    colDepositoValesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoChequeTmp = new PdfPCell(new Phrase("$" + cheque, fuenteNormal));
                    colDepositoChequeTmp.BorderWidth = 0;
                    colDepositoChequeTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTransTmp = new PdfPCell(new Phrase("$" + trans, fuenteNormal));
                    colDepositoTransTmp.BorderWidth = 0;
                    colDepositoTransTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                    colDepositoFechaTmp.BorderWidth = 0;
                    colDepositoFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaDepositos.AddCell(colEmpleadoTmp);
                    tablaDepositos.AddCell(colDepositoEfectivoTmp);
                    tablaDepositos.AddCell(colDepositoTarjetaTmp);
                    tablaDepositos.AddCell(colDepositoValesTmp);
                    tablaDepositos.AddCell(colDepositoChequeTmp);
                    tablaDepositos.AddCell(colDepositoTransTmp);
                    tablaDepositos.AddCell(colDepositoFechaTmp);
                }

                reporte.Add(tituloDepositos);
                reporte.Add(tablaDepositos);
                reporte.Add(linea);
            }

            dr.Close();
            sql_con.Close();


            Paragraph tituloRetiros = new Paragraph("HISTORIAL DE RETIROS\n\n", fuenteGrande);
            tituloRetiros.Alignment = Element.ALIGN_CENTER;

            anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, /*100f,*/ 100f };

            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            sql_con.Open();

            var visualizarConuslta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'retiro' AND Cantidad != '0.00' AND (FechaOperacion BETWEEN '{primerFecha.ToString("yyyy-MM-dd HH:mm:ss")}' AND  '{segundaFecha.ToString("yyyy-MM-dd HH:mm:ss")}')";

            sql_cmd = new MySqlCommand(visualizarConuslta, sql_con);
            dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                PdfPTable tablaRetiros = new PdfPTable(7);
                tablaRetiros.WidthPercentage = 100;
                tablaRetiros.SetWidths(anchoColumnas);

                PdfPCell colEmpleadoR = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                colEmpleadoR.BorderWidth = 0;
                colEmpleadoR.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleadoR.Padding = 3;

                PdfPCell colDepositoEfectivoR = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                colDepositoEfectivoR.BorderWidth = 0;
                colDepositoEfectivoR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoEfectivoR.Padding = 3;

                PdfPCell colDepositoTarjetaR = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                colDepositoTarjetaR.BorderWidth = 0;
                colDepositoTarjetaR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTarjetaR.Padding = 3;

                PdfPCell colDepositoValesR = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                colDepositoValesR.BorderWidth = 0;
                colDepositoValesR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoValesR.Padding = 3;

                PdfPCell colDepositoChequeR = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                colDepositoChequeR.BorderWidth = 0;
                colDepositoChequeR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoChequeR.Padding = 3;

                PdfPCell colDepositoTransR = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                colDepositoTransR.BorderWidth = 0;
                colDepositoTransR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTransR.Padding = 3;

                //PdfPCell colDepositoCreditoR = new PdfPCell(new Phrase("CRÉDITO", fuenteNegrita));
                //colDepositoCreditoR.BorderWidth = 0;
                //colDepositoCreditoR.HorizontalAlignment = Element.ALIGN_CENTER;
                //colDepositoCreditoR.Padding = 3;

                PdfPCell colDepositoFechaR = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                colDepositoFechaR.BorderWidth = 0;
                colDepositoFechaR.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoFechaR.Padding = 3;

                tablaRetiros.AddCell(colEmpleadoR);
                tablaRetiros.AddCell(colDepositoEfectivoR);
                tablaRetiros.AddCell(colDepositoTarjetaR);
                tablaRetiros.AddCell(colDepositoValesR);
                tablaRetiros.AddCell(colDepositoChequeR);
                tablaRetiros.AddCell(colDepositoTransR);
                //tablaRetiros.AddCell(colDepositoCreditoR);
                tablaRetiros.AddCell(colDepositoFechaR);

                //MySqlCommand sql_cmd;
                //MySqlDataReader dr;

                while (dr.Read())
                {
                    var efectivo = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                    var tarjeta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                    var vales = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                    var cheque = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                    var trans = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                    //var credito = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Credito"))).ToString("0.00");
                    var fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                    int usuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IdEmpleado")));

                    var autorMovimiento = string.Empty;
                    if (usuario != 0)
                    {
                        autorMovimiento = cs.BuscarEmpleadoCaja(usuario);
                    }
                    else
                    {
                        autorMovimiento = "ADMIN (";
                        autorMovimiento += cs.validarEmpleadoPorID();
                        autorMovimiento += ")";
                    }

                    PdfPCell colEmpleadoTmpR = new PdfPCell(new Phrase(autorMovimiento, fuenteNormal));
                    colEmpleadoTmpR.BorderWidth = 0;
                    colEmpleadoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoEfectivoTmpR = new PdfPCell(new Phrase("$" + efectivo, fuenteNormal));
                    colDepositoEfectivoTmpR.BorderWidth = 0;
                    colDepositoEfectivoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTarjetaTmpR = new PdfPCell(new Phrase("$" + tarjeta, fuenteNormal));
                    colDepositoTarjetaTmpR.BorderWidth = 0;
                    colDepositoTarjetaTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoValesTmpR = new PdfPCell(new Phrase("$" + vales, fuenteNormal));
                    colDepositoValesTmpR.BorderWidth = 0;
                    colDepositoValesTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoChequeTmpR = new PdfPCell(new Phrase("$" + cheque, fuenteNormal));
                    colDepositoChequeTmpR.BorderWidth = 0;
                    colDepositoChequeTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTransTmpR = new PdfPCell(new Phrase("$" + trans, fuenteNormal));
                    colDepositoTransTmpR.BorderWidth = 0;
                    colDepositoTransTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    //PdfPCell colDepositoCreditoTmpR = new PdfPCell(new Phrase("$" + credito, fuenteNormal));
                    //colDepositoCreditoTmpR.BorderWidth = 0;
                    //colDepositoCreditoTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoFechaTmpR = new PdfPCell(new Phrase(fecha, fuenteNormal));
                    colDepositoFechaTmpR.BorderWidth = 0;
                    colDepositoFechaTmpR.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaRetiros.AddCell(colEmpleadoTmpR);
                    tablaRetiros.AddCell(colDepositoEfectivoTmpR);
                    tablaRetiros.AddCell(colDepositoTarjetaTmpR);
                    tablaRetiros.AddCell(colDepositoValesTmpR);
                    tablaRetiros.AddCell(colDepositoChequeTmpR);
                    tablaRetiros.AddCell(colDepositoTransTmpR);
                    //tablaRetiros.AddCell(colDepositoCreditoTmpR);
                    tablaRetiros.AddCell(colDepositoFechaTmpR);
                }

                reporte.Add(tituloRetiros);
                reporte.Add(tablaRetiros);
            }

            dr.Close();
            sql_con.Close();


            reporte.AddTitle("Reporte Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }
        #endregion

        private string obtenerAutorCorte(int idCorte)
        {
            var result = string.Empty;

            var query = cn.CargarDatos($"SELECT EMP.nombre AS Name FROM Caja AS CJ RIGHT JOIN empleados AS EMP ON CJ.IDUsuario = EMP.IDUsuario WHERE CJ.IDUsuario = '{FormPrincipal.userID}' AND CJ.ID = '{idCorte}' AND CJ.IdEmpleado != 0");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["Name"].ToString();
            }
            //else
            //{
            //    result = FormPrincipal.userNickName.ToString();

            //}

            return result;
        }

        private DataTable obtenerDatosReporte(int id, string tipoBusqueda)
        {
            List<string> lista = new List<string>();
            DateTime date = DateTime.Parse(DGVReporteCaja.CurrentRow.Cells[2].Value.ToString());
            var idFinal = Convert.ToInt32(DGVReporteCaja.CurrentRow.Cells[0].Value.ToString());

            var fechaParametro1 = string.Empty; var idInicio = string.Empty;
            var obtenerFechaCorteAnterior = cn.CargarDatos($"SELECT FechaOperacion, id FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' AND ID < '{id}' ORDER BY FechaOperacion DESC LIMIT 1");
            if (!obtenerFechaCorteAnterior.Rows.Count.Equals(0))
            {
                fechaParametro1 = obtenerFechaCorteAnterior.Rows[0]["FechaOperacion"].ToString();
                idInicio = obtenerFechaCorteAnterior.Rows[0]["ID"].ToString();

            }

            DateTime datePrimera = DateTime.Parse(fechaParametro1);

            var total = string.Empty; var efectivo = string.Empty; var tarjeta = string.Empty; var vales = string.Empty; var cheque = string.Empty; var transferencia = string.Empty; var credit = string.Empty;
            //var query = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad), 0.00) AS Total, IFNULL(SUM(Efectivo), 0.00) AS Efectivo, IFNULL(SUM(Tarjeta),0.00) AS Tarjeta, IFNULL(SUM(Vales),0.00) AS Vales, IFNULL(SUM(Cheque),0.00) AS Cheque, IFNULL(SUM(Transferencia),0.00) AS Transferencia FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = '{tipoBusqueda}' AND (FechaOperacion BETWEEN '{datePrimera.ToString("yyyy-MM-dd hh:mm:ss")}' AND '{date.ToString("yyyy-MM-dd hh:mm:ss")}')");
            //var query = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad), 0.00) AS Total, IFNULL(SUM(Efectivo), 0.00) AS Efectivo, IFNULL(SUM(Tarjeta),0.00) AS Tarjeta, IFNULL(SUM(Vales),0.00) AS Vales, IFNULL(SUM(Cheque),0.00) AS Cheque, IFNULL(SUM(Transferencia),0.00) AS Transferencia FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = '{tipoBusqueda}' AND (ID > '{idInicio}' AND ID < '{idFinal}')");

            var query = cn.CargarDatos($"SELECT Cantidad, Efectivo, Tarjeta, Vales, Cheque, Transferencia, FechaOperacion, IdEmpleado FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}'AND Cantidad != '0.00'  AND Operacion = '{tipoBusqueda}' AND (ID > '{idInicio}' AND ID < '{idFinal}')");


            if (!query.Rows.Count.Equals(0))
            {
                //total = query.Rows[0]["Total"].ToString();
                //efectivo = query.Rows[0]["Efectivo"].ToString();
                //tarjeta = query.Rows[0]["Tarjeta"].ToString();
                //vales = query.Rows[0]["Vales"].ToString();
                //cheque = query.Rows[0]["Cheque"].ToString();
                //transferencia = query.Rows[0]["Transferencia"].ToString();
            }

            //lista.Add(total);
            //lista.Add(efectivo);
            //lista.Add(tarjeta);
            //lista.Add(vales);
            //lista.Add(cheque);
            //lista.Add(transferencia);

            return query;
        }

        #region Generar reporte de agregar y retirar dinero
        private void GenerarReporteAgregarRetirar(string tipoReporte, DataTable datoCantidad, int id)
        {
            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            DateTime dateReporte = DateTime.Parse(DGVReporteCaja.CurrentRow.Cells[2].Value.ToString());

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteBlanca);

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = string.Empty;
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\caja.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";
            }

            Paragraph Usuario = new Paragraph();
            Paragraph Empleado = new Paragraph();

            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();

            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);
            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            var numFolio = obtenerFolio(id);

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(tipoReporte, fuenteGrande);


            Paragraph NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteGrande);

            Paragraph subTitulo = new Paragraph($"{tipoReporte}\nFecha:   {dateReporte.ToString("dddd, dd MMMM yyyy HH:mm:ss")}  \n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            if (!string.IsNullOrEmpty(UsuarioActivo)) { Empleado.Alignment = Element.ALIGN_CENTER; }
            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (!string.IsNullOrEmpty(UsuarioActivo)) { reporte.Add(Empleado); }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);

            //=====================================
            //===    TABLA DE Reporte General   ===
            //=====================================
            #region Tabla de Dinero Retirado
            float[] anchoColumnas = new float[] { 100f, 120f, 100f, 100f, 100f, 100f, 100f, 100f };

            Paragraph tituloDineroRetirado = new Paragraph($"HISTORIAL DE {tipoReporte}\n\n", fuenteGrande);
            tituloDineroRetirado.Alignment = Element.ALIGN_CENTER;

            reporte.Add(tituloDineroRetirado);

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            reporte.Add(linea);

            PdfPTable tablaDineroRetirado = new PdfPTable(8);
            tablaDineroRetirado.WidthPercentage = 100;
            tablaDineroRetirado.SetWidths(anchoColumnas);

            PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
            colEmpleado.BorderWidth = 1;
            colEmpleado.BorderWidthTop = 0;
            colEmpleado.BackgroundColor = new BaseColor(Color.SkyBlue);
            colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
            colEmpleado.Padding = 3;

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
            colFecha.BorderWidth = 1;
            colFecha.BorderWidthTop = 0;
            colFecha.BorderWidthLeft = 0;
            colFecha.BorderWidthRight = 0;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;

            PdfPCell colRetiroEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
            colRetiroEfectivo.BorderWidth = 1;
            colRetiroEfectivo.BorderWidthTop = 0;
            colRetiroEfectivo.BorderWidthRight = 0;
            colRetiroEfectivo.BackgroundColor = new BaseColor(Color.SkyBlue);
            colRetiroEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiroEfectivo.Padding = 3;

            PdfPCell colRetiroTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
            colRetiroTarjeta.BorderWidth = 1;
            colRetiroTarjeta.BorderWidthTop = 0;
            colRetiroTarjeta.BorderWidthRight = 0;
            colRetiroTarjeta.BackgroundColor = new BaseColor(Color.SkyBlue);
            colRetiroTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiroTarjeta.Padding = 3;

            PdfPCell colRetiroVales = new PdfPCell(new Phrase("VALES", fuenteNegrita));
            colRetiroVales.BorderWidth = 1;
            colRetiroVales.BorderWidthTop = 0;
            colRetiroVales.BorderWidthRight = 0;
            colRetiroVales.BackgroundColor = new BaseColor(Color.SkyBlue);
            colRetiroVales.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiroVales.Padding = 3;

            PdfPCell colRetiroCheque = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
            colRetiroCheque.BorderWidth = 1;
            colRetiroCheque.BorderWidthTop = 0;
            colRetiroCheque.BorderWidthRight = 0;
            colRetiroCheque.BackgroundColor = new BaseColor(Color.SkyBlue);
            colRetiroCheque.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiroCheque.Padding = 3;

            PdfPCell colRetiroTrans = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
            colRetiroTrans.BorderWidth = 1;
            colRetiroTrans.BorderWidthTop = 0;
            colRetiroTrans.BorderWidthRight = 0;
            colRetiroTrans.BackgroundColor = new BaseColor(Color.SkyBlue);
            colRetiroTrans.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiroTrans.Padding = 3;

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL", fuenteNegrita));
            colTotal.BorderWidth = 1;
            colTotal.BorderWidthTop = 0;
            colTotal.BackgroundColor = new BaseColor(Color.SkyBlue);
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;

            tablaDineroRetirado.AddCell(colEmpleado);
            tablaDineroRetirado.AddCell(colFecha);
            tablaDineroRetirado.AddCell(colRetiroEfectivo);
            tablaDineroRetirado.AddCell(colRetiroTarjeta);
            tablaDineroRetirado.AddCell(colRetiroVales);
            tablaDineroRetirado.AddCell(colRetiroCheque);
            tablaDineroRetirado.AddCell(colRetiroTrans);
            tablaDineroRetirado.AddCell(colTotal);

            double Cantidad = 0.00,
                    Efectivo = 0.00,
                    Tarjeta = 0.00,
                    Vales = 0.00,
                    Cheque = 0.00,
                    Transferencia = 0.00;


            var usuario = string.Empty;
            foreach (DataRow datosRecorrer in datoCantidad.Rows)
            {
                var nombreEmpleado = cs.BuscarEmpleadoCaja(Convert.ToInt32(datosRecorrer["IdEmpleado"].ToString()));
                if (string.IsNullOrEmpty(nombreEmpleado)) { usuario = $"ADMIN"; } else { usuario = nombreEmpleado; }

                PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(usuario, fuenteNormal));
                colEmpleadoTmp.BorderWidth = 0;
                colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(datosRecorrer["FechaOperacion"].ToString(), fuenteNormal));
                colFechaTmp.BorderWidth = 0;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRetiroEfectivoTmp = new PdfPCell(new Phrase("$ " + datosRecorrer["Efectivo"].ToString(), fuenteNormal));
                colRetiroEfectivoTmp.BorderWidth = 0;
                colRetiroEfectivoTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                Efectivo += (float)Convert.ToDouble(datosRecorrer["Efectivo"].ToString());

                PdfPCell colRetiroTarjetaTmp = new PdfPCell(new Phrase("$ " + datosRecorrer["Tarjeta"].ToString(), fuenteNormal));
                colRetiroTarjetaTmp.BorderWidth = 0;
                colRetiroTarjetaTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                Tarjeta += (float)Convert.ToDouble(datosRecorrer["Tarjeta"].ToString());

                PdfPCell colRetiroValesTmp = new PdfPCell(new Phrase("$ " + datosRecorrer["Vales"].ToString(), fuenteNormal));
                colRetiroValesTmp.BorderWidth = 0;
                colRetiroValesTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                Vales += (float)Convert.ToDouble(datosRecorrer["Vales"].ToString());

                PdfPCell colRetiroChequeTmp = new PdfPCell(new Phrase("$ " + datosRecorrer["Cheque"].ToString(), fuenteNormal));
                colRetiroChequeTmp.BorderWidth = 0;
                colRetiroChequeTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                Cheque += (float)Convert.ToDouble(datosRecorrer["Cheque"].ToString());

                PdfPCell colRetiroTransTmp = new PdfPCell(new Phrase("$ " + datosRecorrer["Transferencia"].ToString(), fuenteNormal));
                colRetiroTransTmp.BorderWidth = 0;
                colRetiroTransTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                Transferencia += (float)Convert.ToDouble(datosRecorrer["Transferencia"].ToString());

                PdfPCell colTotalCant = new PdfPCell(new Phrase("$ " + datosRecorrer["Cantidad"].ToString(), fuenteNormal));
                colTotalCant.BorderWidth = 0;
                colTotalCant.HorizontalAlignment = Element.ALIGN_CENTER;
                Cantidad += (float)Convert.ToDouble(datosRecorrer["Cantidad"].ToString());

                tablaDineroRetirado.AddCell(colEmpleadoTmp);
                tablaDineroRetirado.AddCell(colFechaTmp);
                tablaDineroRetirado.AddCell(colRetiroEfectivoTmp);
                tablaDineroRetirado.AddCell(colRetiroTarjetaTmp);
                tablaDineroRetirado.AddCell(colRetiroValesTmp);
                tablaDineroRetirado.AddCell(colRetiroChequeTmp);
                tablaDineroRetirado.AddCell(colRetiroTransTmp);
                tablaDineroRetirado.AddCell(colTotalCant);
            }

            List<string> listaTotales = new List<string>();
            listaTotales.Add($"{string.Empty} | {string.Empty} | {string.Empty} | {string.Empty} | {string.Empty} | {string.Empty}");
            listaTotales.Add($"{Efectivo.ToString("C")} | {Tarjeta.ToString("C")} | {Vales.ToString("C")} | {Cheque.ToString("C")} | {Transferencia.ToString("C")} | {Cantidad.ToString("C")}");

            var recorrerListaTotales = listaTotales.ToArray();
            int saltar = 0;

            foreach (var iterador in recorrerListaTotales)
            {
                string[] separador = iterador.Split('|');
                string totalDinamico = string.Empty;

                //Aqui se agregan los campos de la suma de los totales
                if (saltar.Equals(0)) { saltar++; } else { totalDinamico = "TOTAL"; }
                PdfPCell colEmpleadoTotal = new PdfPCell(new Phrase(totalDinamico, fuenteNegrita));
                colEmpleadoTotal.BorderWidth = 0;
                colEmpleadoTotal.Colspan = 2;
                colEmpleadoTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRetiroEfectivoTotal = new PdfPCell(new Phrase(separador[0].ToString(), fuenteNegrita));
                colRetiroEfectivoTotal.BorderWidth = 0;
                colRetiroEfectivoTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRetiroTarjetaTotal = new PdfPCell(new Phrase(separador[1].ToString(), fuenteNegrita));
                colRetiroTarjetaTotal.BorderWidth = 0;
                colRetiroTarjetaTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRetiroValesTotal = new PdfPCell(new Phrase(separador[2].ToString(), fuenteNegrita));
                colRetiroValesTotal.BorderWidth = 0;
                colRetiroValesTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRetiroChequeTotal = new PdfPCell(new Phrase(separador[3].ToString(), fuenteNegrita));
                colRetiroChequeTotal.BorderWidth = 0;
                colRetiroChequeTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRetiroTransTotal = new PdfPCell(new Phrase(separador[4].ToString(), fuenteNegrita));
                colRetiroTransTotal.BorderWidth = 0;
                colRetiroTransTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalCantTotal = new PdfPCell(new Phrase(separador[5].ToString(), fuenteNegrita));
                colTotalCantTotal.BorderWidth = 0;
                colTotalCantTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaDineroRetirado.AddCell(colEmpleadoTotal);
                tablaDineroRetirado.AddCell(colRetiroEfectivoTotal);
                tablaDineroRetirado.AddCell(colRetiroTarjetaTotal);
                tablaDineroRetirado.AddCell(colRetiroValesTotal);
                tablaDineroRetirado.AddCell(colRetiroChequeTotal);
                tablaDineroRetirado.AddCell(colRetiroTransTotal);
                tablaDineroRetirado.AddCell(colTotalCantTotal);
            }

            reporte.Add(tablaDineroRetirado);
            reporte.Add(linea);

            //reporte.Add(tablaTotalesDineroRetirado);
            #endregion Tabla de Dinero Agregado
            //=====================================
            //=== FIN TABLA DE Reporte General  ===
            //=====================================
            reporte.AddTitle("Reporte Dinero Retirado");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
        #endregion

        private string obtenerFolio(int idDato)
        {
            var result = string.Empty;
            var query = cn.CargarDatos($"SELECT NumFolio FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND  ID = '{idDato}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["NumFolio"].ToString();
            }

            return result;
        }


        private void actualizar()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            lblCantidadRegistros.Text = p.countRow().ToString();

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = System.Drawing.Color.White;
            linkLblPaginaActual.BackColor = System.Drawing.Color.Black;

            BeforePage = p.numPag() - 1;
            AfterPage = p.numPag() + 1;
            LastPage = p.countPag();

            if (Convert.ToInt32(linkLblPaginaActual.Text) >= 2)
            {
                linkLblPaginaAnterior.Text = BeforePage.ToString();
                linkLblPaginaAnterior.Visible = true;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                }
            }
            else if (BeforePage < 1)
            {
                linkLblPrimeraPagina.Visible = false;
                linkLblPaginaAnterior.Visible = false;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                    linkLblUltimaPagina.Visible = false;
                }
            }

            txtMaximoPorPagina.Text = p.limitRow().ToString();
        }

        public void CargarDatos(int status = 1, string busquedaEnProductos = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnProductos;

            if (DGVReporteCaja.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }
            else if (DGVReporteCaja.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            DGVReporteCaja.Rows.Clear();

            if (conBusqueda.Equals(true))
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        var idCorte = filaDatos["ID"].ToString();
                        var fecha = filaDatos["FechaOPeracion"].ToString();
                        var idEmpleado = Convert.ToInt32(filaDatos["IdEmpleado"].ToString());
                        var empleado = filaDatos["nombre"].ToString();
                        var fechaOp = filaDatos["FechaOperacion"].ToString();
                        var name = string.Empty;

                        if (idEmpleado > 0)
                        {
                            name = empleado;
                        }
                        else
                        {
                            name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
                        }

                        //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                        DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf, fechaOp);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBuscador.Text = string.Empty;
                    txtBuscador.Focus();
                }
            }
            else
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        var idCorte = filaDatos["ID"].ToString();
                        var fecha = filaDatos["FechaOPeracion"].ToString();
                        var idEmpleado = Convert.ToInt32(filaDatos["IdEmpleado"].ToString());
                        var empleado = filaDatos["nombre"].ToString();
                        var fechaOp = filaDatos["FechaOperacion"].ToString();
                        var name = string.Empty;

                        if (idEmpleado > 0)
                        {
                            name = empleado;
                        }
                        else
                        {
                            name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
                        }

                        //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                        DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf, fechaOp);
                    }
                }

                //var numeroFilas = DGVReporteCaja.Rows.Count;

                //string Nombre = filaDatos["Nombre"].ToString();
                //string Stock = filaDatos["Stock"].ToString();
                //string Precio = filaDatos["Precio"].ToString();
                //string Clave = filaDatos["ClaveInterna"].ToString();
                //string Codigo = filaDatos["CodigoBarras"].ToString();
                //string Tipo = filaDatos["Tipo"].ToString();
                //string Proveedor = filaDatos["Proveedor"].ToString();
                //string chckName = filaDatos["ChckName"].ToString();
                //string Descripcion = filaDatos["Descripcion"].ToString();

                //if (DGVReporteCaja.Rows.Count.Equals(0))
                //{
                //    bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVReporteCaja);

                //    if (encontrado.Equals(false))
                //    {
                //        var number_of_rows = DGVReporteCaja.Rows.Add();
                //        DataGridViewRow row = DGVReporteCaja.Rows[number_of_rows];

                //        row.Cells["Nombre"].Value = Nombre;     // Columna Nombre
                //        row.Cells["Stock"].Value = Stock;       // Columna Stock
                //        row.Cells["Precio"].Value = Precio;     // Columna Precio
                //        row.Cells["Clave"].Value = Clave;       // Columna Clave
                //        row.Cells["Codigo"].Value = Codigo;     // Columna Codigo

                //        // Columna Tipo
                //        if (Tipo.Equals("P"))
                //        {
                //            row.Cells["Tipo"].Value = "PRODUCTO";
                //        }
                //        else if (Tipo.Equals("S"))
                //        {
                //            row.Cells["Tipo"].Value = "SERVICIO";
                //        }
                //        else if (Tipo.Equals("PQ"))
                //        {
                //            row.Cells["Tipo"].Value = "COMBO";
                //        }

                //        row.Cells["Proveedor"].Value = Proveedor;   // Columna Proveedor

                //        if (DGVReporteCaja.Columns.Contains(chckName))
                //        {
                //            row.Cells[chckName].Value = Descripcion;
                //        }
                //    }
                //}
                //else if (!DGVReporteCaja.Rows.Count.Equals(0))
                //{
                //    foreach (DataGridViewRow Row in DGVReporteCaja.Rows)
                //    {
                //        bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVReporteCaja);

                //        if (encontrado.Equals(true))
                //        {
                //            var Fila = Row.Index;
                //            // Columnas Dinamicos
                //            if (DGVReporteCaja.Columns.Contains(chckName))
                //            {
                //                DGVReporteCaja.Rows[Fila].Cells[chckName].Value = Descripcion;
                //            }
                //        }
                //        else if (encontrado.Equals(false))
                //        {
                //            var number_of_rows = DGVReporteCaja.Rows.Add();
                //            DataGridViewRow row = DGVReporteCaja.Rows[number_of_rows];

                //            row.Cells["Nombre"].Value = Nombre;         // Columna Nombre
                //            row.Cells["Stock"].Value = Stock;           // Columna Stock
                //            row.Cells["Precio"].Value = Precio;         // Columna Precio
                //            row.Cells["Clave"].Value = Clave;           // Columna Clave
                //            row.Cells["Codigo"].Value = Codigo;         // Columna Codigo

                //            // Columna Tipo
                //            if (Tipo.Equals("P"))
                //            {
                //                row.Cells["Tipo"].Value = "PRODUCTO";
                //            }
                //            else if (Tipo.Equals("S"))
                //            {
                //                row.Cells["Tipo"].Value = "SERVICIO";
                //            }
                //            else if (Tipo.Equals("PQ"))
                //            {
                //                row.Cells["Tipo"].Value = "COMBO";
                //            }

                //            // Columna Proveedor
                //            row.Cells["Proveedor"].Value = Proveedor;

                //            // Columnas Dinamicos
                //            if (DGVReporteCaja.Columns.Contains(chckName))
                //            {
                //                row.Cells[chckName].Value = Descripcion;
                //            }
                //        }
                //    }
                //}
            }

            actualizar();

            clickBoton = 0;
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void linkLblPaginaSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

            if (cantidadAMostrar <= 0)
            {
                mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                return;
            }

            maximo_x_pagina = cantidadAMostrar;
            p.actualizarTope(maximo_x_pagina);
            CargarDatos();
            actualizar();

            //if (string.IsNullOrEmpty(txtMaximoPorPagina.ToString()))
            //{
            //    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            //}

            //maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            //p.actualizarTope(maximo_x_pagina);
            //CargarDatos();
            //actualizar();
        }

        private void txtMaximoPorPagina_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaximoPorPagina.ToString()))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }

            maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            p.actualizarTope(maximo_x_pagina);
            CargarDatos();
            actualizar();
        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

                if (cantidadAMostrar <= 0)
                {
                    mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                    Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                    return;
                }

                maximo_x_pagina = cantidadAMostrar;
                p.actualizarTope(maximo_x_pagina);
                CargarDatos();
                actualizar();

                //if (string.IsNullOrEmpty(txtMaximoPorPagina.ToString()))
                //{
                //    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                //}
                //maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                //p.actualizarTope(maximo_x_pagina);
                //CargarDatos();
                //actualizar();
            }
        }

        private void BuscarReporteCajaPorFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void primerDatePicker_ValueChanged(object sender, EventArgs e)
        {
            //DateTime date = DateTime.Now;
            //DateTime PrimerDia = new DateTime(date.Year, date.Month, 1);
            //primerDatePicker.Value = PrimerDia;
        }

        private void segundoDatePicker_ValueChanged(object sender, EventArgs e)
        {
            //segundoDatePicker.Value = DateTime.Now;
        }
    }
}
