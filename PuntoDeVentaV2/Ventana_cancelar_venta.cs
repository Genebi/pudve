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
    public partial class Ventana_cancelar_venta : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public int venta_cancelada = 0;

        public Ventana_cancelar_venta()
        {
            InitializeComponent();
            txt_folio_nota.Focus();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string numero_folio = txt_folio_nota.Text;

            CancelarVenta(numero_folio);
        }

        private void CancelarVenta(string numFolio)
        {
            var id = mb.buscarIDConFolio(numFolio);
            var ultimaFechaCorte = mb.ObtenerFechaUltimoCorte();
            var fechaVenta = mb.ObtenerFechaVenta(id);
            var estatusVenta = mb.ObtenerStatusVenta(numFolio);

            if (!string.IsNullOrWhiteSpace(txt_folio_nota.Text) && !string.IsNullOrEmpty(fechaVenta))
            {
                DateTime validarFechaCorte = Convert.ToDateTime(ultimaFechaCorte);
                DateTime validarFechaVenta = Convert.ToDateTime(fechaVenta);

                if (validarFechaVenta > validarFechaCorte)
                {
                    var folio = numFolio.Trim();
                    if (!string.IsNullOrWhiteSpace(folio))
                    {
                        var consulta = $"SELECT ID FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Folio = {folio}";
                        // Verificar y obtener ID de la venta utilizando el folio
                        var existe = (bool)cn.EjecutarSelect(consulta);

                        if (existe)
                        {
                            var respuesta = MessageBox.Show("¿Desea cancelar la venta?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (respuesta == DialogResult.Yes)
                            {
                                txt_folio_nota.Enabled = false;
                                btn_aceptar.Enabled = false;

                                var idVenta = Convert.ToInt32(cn.EjecutarSelect(consulta, 1));
                                Ventas.mostrarVenta = idVenta;

                                // Cancelar la venta
                                var resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

                                //bool seCancelaLaVenta = false;

                                if (resultado > 0)
                                {
                                    //// Regresar la cantidad de producto vendido al stock
                                    //var productos = cn.ObtenerProductosVenta(idVenta);

                                    //if (productos.Length > 0)
                                    //{
                                    //    foreach (var producto in productos)
                                    //    {
                                    //        var info = producto.Split('|');
                                    //        var idProducto = info[0];
                                    //        var cantidad = float.Parse(info[2]);

                                    //        cn.EjecutarConsulta($"UPDATE Productos SET Stock =  Stock + {cantidad} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
                                    //    }
                                    //}

                                    // Agregamos marca de agua al PDF del ticket de la venta cancelada
                                    Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");

                                    DialogResult mensaje = DialogResult.No;
                                    //Revisar si la venta fue a credio
                                    bool fueACredito = ventaCredito(id);

                                    bool tieneAbonos = (bool)cn.EjecutarSelect($"SELECT * FROM Abonos WHERE IDUsuario = {FormPrincipal.userID} AND IDVenta = {idVenta}");

                                    if (fueACredito && tieneAbonos)
                                    {
                                        mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    }

                                    if (!fueACredito)
                                    {
                                        mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    }

                                    if (mensaje == DialogResult.Yes)
                                    {
                                        if (estatusVenta.Equals(1))
                                        {

                                            var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                                            // Operacion para que la devolucion del dinero afecte al apartado Caja
                                            if (formasPago.Length > 0)
                                            {
                                                var total = formasPago.Sum().ToString();
                                                var efectivo = formasPago[0].ToString();
                                                var tarjeta = formasPago[1].ToString();
                                                var vales = formasPago[2].ToString();
                                                var cheque = formasPago[3].ToString();
                                                var transferencia = formasPago[4].ToString();
                                                var credito = formasPago[5].ToString();
                                                var anticipo = "0";

                                                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                var concepto = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";

                                                //    string[] datos = new string[] {
                                                //    "retiro", total, "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                                                //    efectivo, tarjeta, vales, cheque, transferencia, credito, anticipo
                                                //};
                                                string[] datos = new string[]
                                                {
                                                    id.ToString(), FormPrincipal.userID.ToString(), total, efectivo, tarjeta,
                                                    vales, cheque, transferencia, concepto, fechaOperacion
                                                };

                                                cn.EjecutarConsulta(cs.OperacionDevoluciones(datos));

                                                //cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                            }

                                            //seCancelaLaVenta = true;
                                        }
                                        else if (estatusVenta.Equals(4))
                                        {
                                            validarDatos(idVenta);
                                        }
                                    }

                                    venta_cancelada = 1;

                                    //if (seCancelaLaVenta)
                                    //{
                                    // Obtiene el id del combo cancelado
                                    DataTable d_prod_venta = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM ProductosVenta WHERE IDVenta='{idVenta}'");
                                    //var productos = cn.ObtenerProductosVenta(idVenta);

                                    if (d_prod_venta.Rows.Count > 0)
                                    {
                                        DataRow r_prod_venta = d_prod_venta.Rows[0];
                                        int id_prod = Convert.ToInt32(r_prod_venta["IDProducto"]);
                                        decimal cantidad_combo = Convert.ToDecimal(r_prod_venta["Cantidad"]);

                                        // Busca los productos relacionados al combo y trae la cantidad para aumentar el stock
                                        //DataTable dtprod_relacionados = cn.CargarDatos(cs.productos_relacionados(id_prod));

                                        //if (dtprod_relacionados.Rows.Count > 0)//Este caso entra cuando solo se cancelan combos
                                        //{
                                        //    foreach (DataRow drprod_relacionados in dtprod_relacionados.Rows)
                                        //    {
                                        //        decimal cantidad_prod_rel = Convert.ToDecimal(drprod_relacionados["Cantidad"]);
                                        //        decimal cantidad_prod_rel_canc = cantidad_combo * cantidad_prod_rel;

                                        //        cn.EjecutarConsulta($"UPDATE Productos SET Stock = Stock + {cantidad_prod_rel_canc} WHERE ID = {drprod_relacionados["IDProducto"]} AND IDUsuario = {FormPrincipal.userID}");
                                        //    }
                                        //}
                                        ////else if (dtprod_relacionados.Rows.Count.Equals(0))
                                        //{
                                        using (DataTable dtProdVenta = cn.CargarDatos(cs.ObtenerProdDeLaVenta(idVenta)))
                                        {
                                            if (!dtProdVenta.Rows.Count.Equals(0))
                                            {
                                                foreach (DataRow drProdVenta in dtProdVenta.Rows)
                                                {

                                                    var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                                    var datoFolio = cn.CargarDatos($"SELECT Folio FROM ventas WHERE ID = {idVenta}");
                                                    var FolioDeCancelacion = datoFolio.Rows[0]["Folio"];
                                                    decimal stockActual = Convert.ToDecimal(drProdVenta["Stock"].ToString());
                                                    decimal stockNuevo = stockActual + Convert.ToDecimal(drProdVenta["Cantidad"]);
                                                    decimal cantidad = Convert.ToDecimal(drProdVenta["Cantidad"]);
                                                    var idProd = drProdVenta["ID"].ToString();
                                                    var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                                    var consultarComboProductoServicio = cn.CargarDatos($"SELECT Tipo FROM productos WHERE ID = '{idProd}'");
                                                    var comboServicioProducto = consultarComboProductoServicio.Rows[0]["Tipo"].ToString();

                                                    if (comboServicioProducto.Equals("P"))
                                                    {
                                                        cn.EjecutarConsulta($"UPDATE Productos SET Stock = {stockNuevo} WHERE ID = {idProd} AND IDUsuario = {FormPrincipal.userID}");
                                                        cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{idProd}','Venta Cancelada Folio: {folio}','{stockActual}','{stockNuevo}','{FechaOperacion}','{FormPrincipal.userNickName}','+{cantidad}','{"P"}',{"0"})");
                                                    }
                                                    else
                                                    {
                                                        if (comboServicioProducto.Equals("PQ"))
                                                        {
                                                            var productosDePaquete = cn.CargarDatos($"SELECT * FROM productosdeservicios WHERE IDServicio = '{idProd}'");
                                                            foreach (DataRow item in productosDePaquete.Rows)
                                                            {

                                                                var nuevoStock = cantidad * Convert.ToDecimal(item[5]);
                                                                var consultaStock = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {item[3]}");
                                                                var StockProdActual = consultaStock.Rows[0]["Stock"].ToString();
                                                                var stockProdNuevo = Convert.ToDecimal(StockProdActual) + Convert.ToDecimal(nuevoStock);

                                                                cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{item[3]}','Venta Cancelada de combo Folio: {folio}','{StockProdActual}','{stockProdNuevo.ToString("N")}','{FechaOperacion}','{FormPrincipal.userNickName}','+{nuevoStock.ToString("N")}','{"P"}',{"0"})");

                                                                cn.EjecutarConsulta($"UPDATE Productos SET Stock = Stock + {nuevoStock} WHERE ID = {item[3]} AND IDUsuario = {FormPrincipal.userID}");//Aqui se hace la devolucion de Combo
                                                            }
                                                        }
                                                        else
                                                        {
                                                            var productosDePaquete = cn.CargarDatos($"SELECT * FROM productosdeservicios WHERE IDServicio = '{idProd}'");
                                                            foreach (DataRow item in productosDePaquete.Rows)
                                                            {

                                                                var nuevoStock = cantidad * Convert.ToDecimal(item[5]);
                                                                var consultaStock = cn.CargarDatos($"SELECT Stock FROM productos WHERE ID = {item[3]}");
                                                                var StockProdActual = consultaStock.Rows[0]["Stock"].ToString();
                                                                var stockProdNuevo = Convert.ToDecimal(StockProdActual) + Convert.ToDecimal(nuevoStock);

                                                                cn.EjecutarConsulta($"INSERT INTO historialstock(IDProducto, TipoDeMovimiento, StockAnterior, StockNuevo, Fecha, NombreUsuario, Cantidad, tipoDeVenta,idComboServicio) VALUES ('{item[3]}','Venta Cancelada de servicio Folio: {folio}','{StockProdActual}','{stockProdNuevo.ToString("N")}','{FechaOperacion}','{FormPrincipal.userNickName}','+{nuevoStock.ToString("N")}','{"P"}',{"0"})");

                                                                cn.EjecutarConsulta($"UPDATE Productos SET Stock = Stock + {nuevoStock} WHERE ID = {item[3]} AND IDUsuario = {FormPrincipal.userID}");//Aqui se hace la devolucion del Servicio
                                                            }

                                                        }
                                                    }

                                                }
                                            }
                                        }
                                    }

                                    using (DataTable dtConfiguracionTipoTicket = cn.CargarDatos(cs.tipoDeTicket()))
                                    {
                                        if (!dtConfiguracionTipoTicket.Rows.Count.Equals(0))
                                        {
                                            var Usuario = 0;
                                            var NombreComercial = 0;
                                            var Direccion = 0;
                                            var ColyCP = 0;
                                            var RFC = 0;
                                            var Correo = 0;
                                            var Telefono = 0;
                                            var NombreC = 0;
                                            var DomicilioC = 0;
                                            var RFCC = 0;
                                            var CorreoC = 0;
                                            var TelefonoC = 0;
                                            var ColyCPC = 0;
                                            var FormaPagoC = 0;
                                            var logo = 0;
                                            var ticket8cm = 0;
                                            var ticket6cm = 0;
                                            var codigoBarraTicket = 0;
                                            var referencia = 0;

                                            foreach (DataRow item in dtConfiguracionTipoTicket.Rows)
                                            {
                                                Usuario = Convert.ToInt32(item["Usuario"].ToString());
                                                NombreComercial = Convert.ToInt32(item["NombreComercial"].ToString());
                                                Direccion = Convert.ToInt32(item["Direccion"].ToString());
                                                ColyCP = Convert.ToInt32(item["ColyCP"].ToString());
                                                RFC = Convert.ToInt32(item["RFC"].ToString());
                                                Correo = Convert.ToInt32(item["Correo"].ToString());
                                                Telefono = Convert.ToInt32(item["Telefono"].ToString());
                                                NombreC = Convert.ToInt32(item["NombreC"].ToString());
                                                DomicilioC = Convert.ToInt32(item["DomicilioC"].ToString());
                                                RFCC = Convert.ToInt32(item["RFCC"].ToString());
                                                CorreoC = Convert.ToInt32(item["CorreoC"].ToString());
                                                TelefonoC = Convert.ToInt32(item["TelefonoC"].ToString());
                                                ColyCPC = Convert.ToInt32(item["ColyCPC"].ToString());
                                                FormaPagoC = Convert.ToInt32(item["FormaPagoC"].ToString());
                                                logo = Convert.ToInt32(item["logo"].ToString());
                                                ticket6cm = Convert.ToInt32(item["ticket58mm"].ToString());
                                                ticket8cm = Convert.ToInt32(item["ticket80mm"].ToString());
                                                codigoBarraTicket = Convert.ToInt32(item["TicketVenta"].ToString());
                                                referencia = Convert.ToInt32(item["Referencia"].ToString());
                                            }

                                            DialogResult respuestaImpresion = MessageBox.Show("Desea Imprimir El Ticket De La Cancelación", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                            if (respuestaImpresion.Equals(DialogResult.Yes))
                                            {
                                                if (ticket6cm.Equals(1))
                                                {
                                                    using (ImprimirTicketCancelado8cm imprimirTicketVenta = new ImprimirTicketCancelado8cm())
                                                    {
                                                        imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                                            
                                                        imprimirTicketVenta.Logo = logo;
                                                        imprimirTicketVenta.Nombre = Usuario;
                                                        imprimirTicketVenta.NombreComercial = NombreComercial;
                                                        imprimirTicketVenta.DireccionCiudad = Direccion;
                                                        imprimirTicketVenta.ColoniaCodigoPostal = ColyCP;
                                                        imprimirTicketVenta.RFC = RFC;
                                                        imprimirTicketVenta.Correo = Correo;
                                                        imprimirTicketVenta.Telefono = Telefono;
                                                        imprimirTicketVenta.NombreCliente = NombreC;
                                                        imprimirTicketVenta.RFCCliente = RFCC;
                                                        imprimirTicketVenta.DomicilioCliente = DomicilioC;
                                                        imprimirTicketVenta.ColoniaCodigoPostalCliente = ColyCPC;
                                                        imprimirTicketVenta.CorreoCliente = CorreoC;
                                                        imprimirTicketVenta.TelefonoCliente = TelefonoC;
                                                        imprimirTicketVenta.FormaDePagoCliente = FormaPagoC;
                                                        imprimirTicketVenta.CodigoBarra = codigoBarraTicket;

                                                        imprimirTicketVenta.ShowDialog();
                                                    }
                                                }
                                                else if (ticket8cm.Equals(1))
                                                {
                                                    using (ImprimirTicketCancelado8cm imprimirTicketVenta = new ImprimirTicketCancelado8cm())
                                                    {
                                                        imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);

                                                        imprimirTicketVenta.Logo = logo;
                                                        imprimirTicketVenta.Nombre = Usuario;
                                                        imprimirTicketVenta.NombreComercial = NombreComercial;
                                                        imprimirTicketVenta.DireccionCiudad = Direccion;
                                                        imprimirTicketVenta.ColoniaCodigoPostal = ColyCP;
                                                        imprimirTicketVenta.RFC = RFC;
                                                        imprimirTicketVenta.Correo = Correo;
                                                        imprimirTicketVenta.Telefono = Telefono;
                                                        imprimirTicketVenta.NombreCliente = NombreC;
                                                        imprimirTicketVenta.RFCCliente = RFCC;
                                                        imprimirTicketVenta.DomicilioCliente = DomicilioC;
                                                        imprimirTicketVenta.ColoniaCodigoPostalCliente = ColyCPC;
                                                        imprimirTicketVenta.CorreoCliente = CorreoC;
                                                        imprimirTicketVenta.TelefonoCliente = TelefonoC;
                                                        imprimirTicketVenta.FormaDePagoCliente = FormaPagoC;
                                                        imprimirTicketVenta.CodigoBarra = codigoBarraTicket;

                                                        imprimirTicketVenta.ShowDialog();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Folio no encontrado.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No es posible cancelar ventas \nanteriores al corte de caja.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txt_folio_nota.Text))
                {
                    MessageBox.Show("Introduzca un número de folio.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Número de folio no encontrado.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txt_folio_nota.Focus();
            }
        }


        private bool ventaCredito(int idVentaObtenido)
        {
            var result = false;

            var query = cn.CargarDatos($"SELECT Credito FROM DetallesVenta WHERE IDVenta = '{idVentaObtenido}'");

            if (!query.Rows.Count.Equals(0) && query.Rows[0]["Credito"].ToString() != "0.00")
            {
                result = true;
            }

            return result;
        }

        private void solo_digitos(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txt_folio_nota_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_aceptar.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private bool validarDatos(int idVenta)
        {
            var result = false;
            var conceptoCredito = $"DEVOLUCION DINERO VENTA A CREDITO CANCELADA ID {idVenta}";
            var stopCancelar = false;
            var datoResultado = string.Empty;

            //var query = cn.CargarDatos($"SELECT Total FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = {idVenta}");
            //var totalImporte = 0f;

            //if (!query.Rows.Count.Equals(0)) { totalImporte = float.Parse(query.Rows[0]["Total"].ToString()); }

            //Consulta para ver si tiene abono
            var obtenerMontoAbonado = cn.CargarDatos($"SELECT sum(Total) AS Total, sum(Efectivo) AS Efectivo, sum(Tarjeta) AS Tarjeta, sum(Vales) AS Vales, sum(Cheque) AS Cheque, sum(Transferencia) AS Transferencia, FechaOperacion FROM Abonos WHERE IDUsuario = {FormPrincipal.userID} AND IDVenta = '{idVenta}'");
            var obtenerTotalAbonado = string.Empty; var obtenerEfectivoAbonado = string.Empty; var obtenerTarjetaAbonado = string.Empty; var obtenerValesAbonado = string.Empty; var obtenerChequeAbonado = string.Empty; var obtenerTransferenciaAbonado = string.Empty; var obtenerFechaOperacionAbonado = string.Empty;

            string ultimoDate = string.Empty;

            var totalAbonado = 0f;
            if (!obtenerMontoAbonado.Rows.Count.Equals(0))
            {
                foreach (DataRow datosConsulta in obtenerMontoAbonado.Rows)
                {
                    obtenerTotalAbonado = datosConsulta["Total"].ToString();
                    obtenerEfectivoAbonado = datosConsulta["Efectivo"].ToString();
                    obtenerTarjetaAbonado = datosConsulta["Tarjeta"].ToString();
                    obtenerValesAbonado = datosConsulta["Vales"].ToString();
                    obtenerChequeAbonado = datosConsulta["Cheque"].ToString();
                    obtenerTransferenciaAbonado = datosConsulta["Transferencia"].ToString();
                    obtenerFechaOperacionAbonado = datosConsulta["FechaOperacion"].ToString();

                }

                obtenerTotalAbonado = string.IsNullOrWhiteSpace(obtenerTotalAbonado) ? "0" : obtenerTotalAbonado;
                totalAbonado = float.Parse(obtenerTotalAbonado);
            }

            if (totalAbonado > 0)//Si tiene abono la venta a credito
            {
                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                if (!fechaCorteUltima.Rows.Count.Equals(0))
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaDelCorteCaja = DateTime.Parse(ultimoDate);
                }

                DevolverAnticipo da = new DevolverAnticipo(idVenta, float.Parse(obtenerTotalAbonado), 3, 2);
                da.ShowDialog();

                if (DevolverAnticipo.cancel == 1)
                {
                    stopCancelar = true;
                    result = false;
                }
                else
                {
                    stopCancelar = false;
                    result = true;
                }
            }

            if (stopCancelar == false)
            {
                //Obtener Status de la venta
                var obtenerStatusVenta = $"SELECT Status FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'";
                var statusObtenido = cn.CargarDatos(obtenerStatusVenta);

                datoResultado = statusObtenido.Rows[0]["Status"].ToString();

                // Cancelar la venta
                int resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

                if (resultado > 0)
                {
                    // Miri. Modificado.
                    // Obtiene el id del combo cancelado
                    DataTable d_prod_venta = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM ProductosVenta WHERE IDVenta='{idVenta}'");
                    //var productos = cn.ObtenerProductosVenta(idVenta);

                    if (d_prod_venta.Rows.Count > 0)
                    {
                        DataRow r_prod_venta = d_prod_venta.Rows[0];
                        int id_prod = Convert.ToInt32(r_prod_venta["IDProducto"]);
                        decimal cantidad_combo = Convert.ToDecimal(r_prod_venta["Cantidad"]);

                        // Busca los productos relacionados al combo y trae la cantidad para aumentar el stock
                        DataTable dtprod_relacionados = cn.CargarDatos(cs.productos_relacionados(id_prod));

                        if (dtprod_relacionados.Rows.Count > 0)
                        {
                            foreach (DataRow drprod_relacionados in dtprod_relacionados.Rows)
                            {
                                decimal cantidad_prod_rel = Convert.ToDecimal(drprod_relacionados["Cantidad"]);
                                decimal cantidad_prod_rel_canc = cantidad_combo * cantidad_prod_rel;

                                cn.EjecutarConsulta($"UPDATE Productos SET Stock = Stock + {cantidad_prod_rel_canc} WHERE ID = {drprod_relacionados["IDProducto"]} AND IDUsuario = {FormPrincipal.userID}");
                            }
                        }
                        else if (dtprod_relacionados.Rows.Count.Equals(0))
                        {
                            using (DataTable dtProdVenta = cn.CargarDatos(cs.ObtenerProdDeLaVenta(idVenta)))
                            {
                                if (!dtProdVenta.Rows.Count.Equals(0))
                                {
                                    foreach (DataRow drProdVenta in dtProdVenta.Rows)
                                    {
                                        cn.EjecutarConsulta(cs.aumentarStockVentaCancelada(Convert.ToInt32(drProdVenta["ID"].ToString()), (float)(Convert.ToDecimal(drProdVenta["Stock"].ToString()) + Convert.ToDecimal(drProdVenta["Cantidad"].ToString()))));
                                    }
                                }
                            }
                        }
                    }

                    var formasPago2 = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                    var conceptoCreditoC = $"DELOLUVION CREDITO VENTA CANCELADA ID {idVenta}";
                    if (formasPago2.Length > 0)
                    {
                        var total1 = "0";
                        var efectivo1 = "0";
                        var tarjeta1 = "0";
                        var vales1 = "0";
                        var cheque1 = "0";
                        var transferencia1 = "0";
                        var credito1 = formasPago2[5].ToString();
                        //var anticipo1 = "0";

                        var fechaOperacion1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (!DevolverAnticipo.ventaCanceladaCredito.Equals(true))
                        {
                            string[] datos = new string[] {
                                                        "retiro", total1, "0", conceptoCreditoC, fechaOperacion1, FormPrincipal.userID.ToString(),
                                                        efectivo1, tarjeta1, vales1, cheque1, transferencia1, credito1/*"0.00"*/, /*anticipo*/"0",FormPrincipal.id_empleado.ToString()
                                                    };
                            cn.EjecutarConsulta(cs.OperacionCaja(datos));
                        }
                    }

                    // Agregamos marca de agua al PDF del ticket de la venta cancelada
                    Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");


                }
            }
            return result;
        }

        private void Ventana_cancelar_venta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
