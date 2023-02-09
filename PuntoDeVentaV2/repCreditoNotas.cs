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
    public partial class repCreditoNotas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        string id_Cliente;
        public repCreditoNotas(string idCliente)
        {
            InitializeComponent();
            id_Cliente = idCliente;
        }

        private void repCreditoNotas_Load(object sender, EventArgs e)
        {
            DataTable datos = cn.CargarDatos($"SELECT ID,Folio, Serie, Total, FechaOperacion AS Fecha From ventas WHERE IDCliente = {id_Cliente} AND IDUsuario={FormPrincipal.userID} AND `Status` = 4");
            DGVMonosas.DataSource = datos;
            using (DataTable nombreC = cn.CargarDatos($"SELECT RazonSocial FROM Clientes WHERE ID = {id_Cliente}"))
            {
                lblTitle.Text += $" {nombreC.Rows[0][0].ToString()}";
            }
        }

        private void DGVMonosas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <0)
            {
                return;
            }
            if (e.ColumnIndex.Equals(0))
            {
                if (FormPrincipal.userNickName.Split('@')[0].Equals("HOUSEDEPOTAUTLAN"))
                {
                    FormNotaVentaHDA fndv = new FormNotaVentaHDA(Int32.Parse(DGVMonosas.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    fndv.ShowDialog();
                }
                else
                {
                    FormNotaDeVenta formNota = new FormNotaDeVenta(Int32.Parse(DGVMonosas.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    formNota.ShowDialog();
                }
                
            }
            if (e.ColumnIndex.Equals(1))
            {
                var Folio = string.Empty;
                var Serie = string.Empty;
                int idVenta = Int32.Parse(DGVMonosas.Rows[e.RowIndex].Cells[2].Value.ToString());

                using (DataTable dtDatosVentas = cn.CargarDatos(cs.DatosVentaParaLaNota(idVenta)))
                {
                    if (!dtDatosVentas.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtDatosVentas.Rows)
                        {
                            Folio = item["Folio"].ToString();
                            Serie = item["Serie"].ToString();

                            //if (Folio.Equals("0"))
                            //{
                            //    MessageBox.Show($"En esta operación se realizo la apertura de la Caja\nRealizada por el Usuario: {item["Usuario"].ToString()}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                        }
                    }
                }

                if (Folio.Equals("0"))
                {
                    var usuarioActivo = FormPrincipal.userNickName;

                    using (DataTable dtConfiguracionTipoTicket = cn.CargarDatos(cs.tipoDeTicket()))
                    {
                        if (!dtConfiguracionTipoTicket.Rows.Count.Equals(0))
                        {
                            var ticket8cm = 0;
                            var ticket6cm = 0;

                            foreach (DataRow item in dtConfiguracionTipoTicket.Rows)
                            {
                                ticket6cm = Convert.ToInt32(item["ticket58mm"].ToString());
                                ticket8cm = Convert.ToInt32(item["ticket80mm"].ToString());
                            }

                            var tipoDeBusqueda = 0;

                            tipoDeBusqueda = verTipoDeBusqueda();

                            if (tipoDeBusqueda.Equals(1))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    if (usuarioActivo.Contains("@"))
                                    {
                                        using (VerTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new VerTicketCajaAbiertaEmpleado8cmListadoVentas())
                                        {
                                            imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                            imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                            imprimirTicketVenta.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        using (VerTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new VerTicketCajaAbierta8cmListadoVentas())
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
                                        using (VerTicketCajaAbiertaEmpleado8cmListadoVentas imprimirTicketVenta = new VerTicketCajaAbiertaEmpleado8cmListadoVentas())
                                        {
                                            imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
                                            imprimirTicketVenta.idEmpleado = FormPrincipal.id_empleado;
                                            imprimirTicketVenta.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        using (VerTicketCajaAbierta8cmListadoVentas imprimirTicketVenta = new VerTicketCajaAbierta8cmListadoVentas())
                                        {
                                            imprimirTicketVenta.idVentaRealizada = Convert.ToInt32(idVenta);
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

                            var tipoDeBusqueda = 0;

                            tipoDeBusqueda = verTipoDeBusqueda();

                            // Ventas Realizadas
                            if (tipoDeBusqueda.Equals(1))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (VerTicket80mmListadoVentas imprimirTicketVenta = new VerTicket80mmListadoVentas())
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
                                        imprimirTicketVenta.Referencia = referencia;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                                else if (ticket8cm.Equals(1))
                                {
                                    using (VerTicket80mmListadoVentas imprimirTicketVenta = new VerTicket80mmListadoVentas())
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
                                        imprimirTicketVenta.Referencia = referencia;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                            }
                            // Venta Guardada
                            if (tipoDeBusqueda.Equals(2) || tipoDeBusqueda.Equals(5))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (VerTicketPresupuesto8cmListadoVentas imprimirTicketVenta = new VerTicketPresupuesto8cmListadoVentas(tipoDeBusqueda))
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
                                        imprimirTicketVenta.Referencia = referencia;
                                        //imprimirTicketVenta.tipoImpresion = tipoDeBusqueda;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                                else if (ticket8cm.Equals(1))
                                {
                                    using (VerTicketPresupuesto8cmListadoVentas imprimirTicketVenta = new VerTicketPresupuesto8cmListadoVentas(tipoDeBusqueda))
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
                                        imprimirTicketVenta.Referencia = referencia;
                                        //imprimirTicketVenta.tipoImpresion = tipoDeBusqueda;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                            }
                            // Venta Cancelada
                            if (tipoDeBusqueda.Equals(3))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (VerTicketCancelado8cmListadoVentas imprimirTicketVenta = new VerTicketCancelado8cmListadoVentas())
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
                                        imprimirTicketVenta.Referencia = referencia;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                                else if (ticket8cm.Equals(1))
                                {
                                    using (VerTicketCancelado8cmListadoVentas imprimirTicketVenta = new VerTicketCancelado8cmListadoVentas())
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
                                        imprimirTicketVenta.Referencia = referencia;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                            }
                            // Venta a Credito
                            if (tipoDeBusqueda.Equals(4))
                            {
                                if (ticket6cm.Equals(1))
                                {
                                    using (VerTicketCredito8cmListadoVentas imprimirTicketVenta = new VerTicketCredito8cmListadoVentas())
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
                                        imprimirTicketVenta.Referencia = referencia;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                                else if (ticket8cm.Equals(1))
                                {
                                    using (VerTicketCredito8cmListadoVentas imprimirTicketVenta = new VerTicketCredito8cmListadoVentas())
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
                                        imprimirTicketVenta.Referencia = referencia;

                                        imprimirTicketVenta.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private int verTipoDeBusqueda()
        {
            var estado = 4;
            return estado;
        }
    }
}
