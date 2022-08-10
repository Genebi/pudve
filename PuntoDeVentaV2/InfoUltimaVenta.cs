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
    public partial class InfoUltimaVenta : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        Moneda oMoneda = new Moneda();

        int botonCerrar = 0, botonImprimir = 0;

        float Total;
        float DineroRecibido;
        float CambioTotal;
       
        public InfoUltimaVenta()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(InfoUltimaVenta_KeyDown);
        }

        private void InfoUltimaVenta_Load(object sender, EventArgs e)
                {
            var ticketTemporal = cn.CargarDatos("Select Total, DineroRecibido, CambioTotal FROM ventas WHERE ID ORDER BY ID DESC LIMIT 1");

            foreach (DataRow item in ticketTemporal.Rows)
            {
                Total = (float)Convert.ToDouble(item[0]);
                DineroRecibido = (float)Convert.ToDouble(item[1]);
                CambioTotal = (float)Convert.ToDouble(item[2]);
            }
            lbSucambio.Text = "Su cambio es de:";
            lbCambio.Text = DetalleVenta.cambio.ToString("C2");
            //lbCambio.Text = CambioTotal.ToString("C");
            lbTotalAPagar.Text = "El total a pagar es: " ;
            lbDineroRecibido.Text ="Recibio la cantidad de: ";
            lbTotalAPagar2.Text = DetalleVenta.restante.ToString("C2");
            //lbTotalAPagar2.Text = Total.ToString("C");
            lbDineroRecibido2.Text = DineroRecibido.ToString("C");
            lbUltimaCompra.Text = "Ultima venta realizada: ";
            lbFechaVenta.Text = ""+DateTime.Now;        

            

            string resultado = oMoneda.Convertir(DetalleVenta.cambio.ToString(), true, "PESOS");
            lbCambioTexto.Text = resultado;

            SendKeys.Send("{TAB}");
            SendKeys.Send("{TAB}");

        }

        private void InfoUltimaVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(27))
            {
                this.Close();
            }
            if (e.KeyCode.Equals(37))
            {
                SendKeys.Send("{TAB}");
                botonImprimir = 1;
                botonCerrar = 0;
                //btnCerrar.BorderColor = Color.FromArgb(33, 97, 140);
            }
            if (e.KeyCode.Equals(39))
            {
                SendKeys.Send("{TAB}");
                botonCerrar = 1;
                botonImprimir = 0;
                //botonRedondo1.BorderColor = Color.FromArgb(33, 97, 140);
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();         
            

        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            //Ventas ventas = Application.OpenForms.OfType<Ventas>().FirstOrDefault();

            //if (ventas != null)
            //{
            //    ventas.btnUltimoTicket.PerformClick();
            //}

            imprimirUltimoTicket();
        }

        private void imprimirUltimoTicket()
        {
            var Folio = string.Empty;
            var Serie = string.Empty;
            var StatusUltimoTicket = string.Empty;
            var usuarioActivo = FormPrincipal.userNickName;
            var ticket8cm = 0;
            var ticket6cm = 0;

            using (DataTable dtConfiguracionTipoTicket = cn.CargarDatos(cs.tipoDeTicket()))
            {
                if (!dtConfiguracionTipoTicket.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtConfiguracionTipoTicket.Rows)
                    {
                        ticket6cm = Convert.ToInt32(item["ticket58mm"].ToString());
                        ticket8cm = Convert.ToInt32(item["ticket80mm"].ToString());
                    }
                }
            }

            if (usuarioActivo.Contains("@"))
            {
                var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND IDEmpleado = {FormPrincipal.id_empleado} AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

                using (DataTable dtDatosVentas = cn.CargarDatos($"SELECT Folio, Serie, Status FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'"))
                {
                    if (!dtDatosVentas.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtDatosVentas.Rows)
                        {
                            Folio = item["Folio"].ToString();
                            Serie = item["Serie"].ToString();
                            StatusUltimoTicket = item["Status"].ToString();
                        }
                    }
                }

                if (Folio.Equals("0"))
                {
                    if (StatusUltimoTicket.Equals("1"))
                    {
                        if (ticket6cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                        else if (ticket8cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                    }
                }
                else
                {
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
                            var codigoBarraTicket = 0;

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
                            }

                            // Ventas Realizadas
                            if (StatusUltimoTicket.Equals("1"))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
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
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
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
            }
            else
            {
                var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND IDEmpleado = 0 AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

                using (DataTable dtDatosVentas = cn.CargarDatos($"SELECT Folio, Serie, Status FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'"))
                {
                    if (!dtDatosVentas.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtDatosVentas.Rows)
                        {
                            Folio = item["Folio"].ToString();
                            Serie = item["Serie"].ToString();
                            StatusUltimoTicket = item["Status"].ToString();
                        }
                    }
                }

                if (Folio.Equals("0"))
                {
                    if (StatusUltimoTicket.Equals("1"))
                    {
                        if (ticket6cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                        else if (ticket8cm.Equals(1))
                        {
                            if (usuarioActivo.Contains("@"))
                            {
                                using (ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbiertaEmpleado8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                            else
                            {
                                using (ImprimirTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new ImprimirTicketCajaAbierta8cmListadoVentas())
                                {
                                    imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                    imprimirTicketVenta.ShowDialog();
                                }
                            }
                        }
                    }
                }
                else
                {
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
                            var codigoBarraTicket = 0;

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
                            }

                            // Ventas Realizadas
                            if (StatusUltimoTicket.Equals("1"))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
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
                                    using (imprimirTicket8cm imprimirTicketVenta = new imprimirTicket8cm())
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
            }
        }

        private void botonRedondo1_Enter(object sender, EventArgs e)
        {
            //if (botonImprimir == 1)
            //{
            //    btnCerrar.BackColor = Color.Red;
            //}
            btnCerrar.BackColor = Color.FromArgb(33, 97, 140);

            botonRedondo1.BackColor = Color.White;
        }

        private void InfoUltimaVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void btnCerrar_Enter(object sender, EventArgs e)
        {
            //if (botonCerrar == 1)
            //{
            //    btnCerrar.BackColor = Color.Red;
            //}
            botonRedondo1.BackColor = Color.FromArgb(33, 97, 140);

            btnCerrar.BackColor = Color.White;
        }
    }
}
