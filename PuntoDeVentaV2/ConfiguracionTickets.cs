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
    public partial class ConfiguracionTickets : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        List<String> confiGeneral;
        bool valorCambioCheckBox = false;
        bool guardado = false;
        string[] ArrayImprimir = new string[] { "AbrirCajaVentas", "AbrirCajaGuardada", "AbrirCajaCancelada" , "AbrirCajaCredito" , "AbrirCajaAnticipos" ,"AbrirCajaAbonos", "AbrirCajaCorte", "AbrirCajaAgregar", "AbrirCajaRetirar" };
        int contadorArray = 0;
        public ConfiguracionTickets()
        {
            InitializeComponent();
        }

        private void ConfiguracionTickets_Load(object sender, EventArgs e)
        {
            CargarCheckBoxs();
        }

        private void CargarCheckBoxs()
        {
            using (var dt = cn.CargarDatos($"SELECT HabilitarTicketVentas,PreguntarTicketVenta,TicketOPDF FROM configuracion WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                if (dt.Rows[0]["HabilitarTicketVentas"].Equals(true))
                {
                    CBVentas.Checked = true;
                    CBPreguntar.Checked = false;
                    CBPreguntar.Enabled = false;
                    cbkAbrirVenta.Enabled = false;
                    cbkAbrirVenta.Checked = false;
                }
                else if (dt.Rows[0]["PreguntarTicketVenta"].Equals(1))
                {
                    CBVentas.Checked = false;
                    CBPreguntar.Checked = true;
                    CBPreguntar.Enabled = true;
                    cbkAbrirVenta.Enabled = false;
                    cbkAbrirVenta.Checked = false;
                }
                else
                {
                    using (var DT = cn.CargarDatos($"SELECT AbrirCajaVentas FROM configuraciondetickets WHERE IDUsuario ={FormPrincipal.userID}"))
                    {
                        if (DT.Rows[0][0].Equals(1))
                        {
                            cbkAbrirVenta.Enabled = true;
                            cbkAbrirVenta.Checked = true;
                        }
                    }      
                }

                if (dt.Rows[0]["TicketOPDF"].Equals(1))
                {
                    RBTicketVentas.Checked = true;
                    RBTicketVentas.Enabled = false;
                }
                else
                {
                    RBPDFVentas.Checked = true;
                    RBPDFVentas.Enabled = false;
                }
            }
            using (var dt2 =  cn.CargarDatos($"SELECT * FROM configuraciondetickets where IDUsuario = {FormPrincipal.userID}"))
            {
                #region Guadadas
                if (dt2.Rows[0]["TicketPresupuesto"].Equals(1))
                {
                    cbImprimirGuardada.Checked = true;
                    cbPreguntarGuardad.Checked = false;
                    cbPreguntarGuardad.Enabled = false;
                    cbkAbrirGGuardadas.Enabled = false;
                    cbkAbrirGGuardadas.Checked = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketPresupuesto"].Equals(1))
                {
                    cbImprimirGuardada.Checked = false;
                    cbPreguntarGuardad.Checked = true;
                    cbPreguntarGuardad.Enabled = true;
                    cbkAbrirGGuardadas.Enabled = false;
                    cbkAbrirGGuardadas.Checked = false;
                }
                else if (dt2.Rows[0]["AbrirCajaGuardada"].Equals(1))
                {
                    cbkAbrirGGuardadas.Enabled = true;
                    cbkAbrirGGuardadas.Checked = true;
                }

                if (dt2.Rows[0]["TicketOPDFPresupuesto"].Equals(1))
                {
                    rbTicketGuardada.Checked = true;
                    rbTicketGuardada.Enabled = false;
                }
                else
                {
                    rbPDFGuardada.Checked = true;
                    rbPDFGuardada.Enabled = false;
                }
                #endregion
                #region Canceladas
                if (dt2.Rows[0]["TicketVentaCancelada"].Equals(1))
                {
                    cbimprimirCancelada.Checked = true;
                    cbPreguntarCANCELADA.Checked = false;
                    cbPreguntarCANCELADA.Enabled = false;
                    cbkAbrirCanceladas.Checked = false;
                    cbkAbrirCanceladas.Enabled = false;
                }
                else if (dt2.Rows[0]["PregutarTicketVentaCancelada"].Equals(1))
                {
                    cbimprimirCancelada.Checked = false;
                    cbPreguntarCANCELADA.Checked = true;
                    cbPreguntarCANCELADA.Enabled = true;
                    cbkAbrirCanceladas.Checked = false;
                    cbkAbrirCanceladas.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaCancelada"].Equals(1))
                {
                    cbkAbrirCanceladas.Checked = true;
                    cbkAbrirCanceladas.Enabled = true;
                }

                if (dt2.Rows[0]["TicketOPDFTicketVentaCancelada"].Equals(1))
                {
                    rbTicketCanceada.Checked = true;
                    rbTicketCanceada.Enabled = false;
                }
                else
                {
                    rcPDFCancelada.Checked = true;
                    rcPDFCancelada.Enabled = false;
                }
                #endregion
                #region Credito
                if (dt2.Rows[0]["CreditoRealizado"].Equals(1))
                {
                    CBVentaCredito.Checked = true;
                    CBPregutnarVentaCrediro.Checked = false;
                    CBPregutnarVentaCrediro.Enabled = false;
                    cbkAbrirCredito.Checked = false;
                    cbkAbrirCredito.Enabled = false;

                }
                else if (dt2.Rows[0]["PreguntarCreditoRealizado"].Equals(1))
                {
                    CBVentaCredito.Checked = false;
                    CBPregutnarVentaCrediro.Checked = true;
                    CBPregutnarVentaCrediro.Enabled = true;
                    cbkAbrirCredito.Checked = false;
                    cbkAbrirCredito.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaCredito"].Equals(1))
                {
                    cbkAbrirCredito.Checked = true;
                    cbkAbrirCredito.Enabled = true;
                }

                if (dt2.Rows[0]["TicketOPDFCreditoRealizado"].Equals(1))
                {
                    RBTicketCredito.Checked = true;
                    RBTicketCredito.Enabled = false;
                }
                else
                {
                    RBPDFCredito.Checked = true;
                    RBPDFCredito.Enabled = false;
                }
                #endregion
                #region Venta Global
                if (dt2.Rows[0]["TicketVentaGlobal"].Equals(1))
                {
                    cbImprimirVentaGlobal.Checked = true;
                    cbPreguntarGobal.Checked = false;
                    cbPreguntarGobal.Enabled = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketVentaGlobal"].Equals(1))
                {
                    cbImprimirVentaGlobal.Checked = false;
                    cbPreguntarGobal.Checked = true;
                    cbPreguntarGobal.Enabled = true;
                }

                if (dt2.Rows[0]["TicketOPDFVentaGlobal"].Equals(1))
                {
                    radioButton2.Checked = true;
                    radioButton2.Enabled = false;
                }
                else
                {
                    radioButton1.Checked = true;
                    radioButton1.Enabled = false;
                }
                #endregion
                #region Abbono
                if (dt2.Rows[0]["TicketAbono"].Equals(1))
                {
                    cbImprimirAbonos.Checked = true;
                    cbpreguntarAbonos.Checked = false;
                    cbpreguntarAbonos.Enabled = false;
                    cbkAbrirAbonos.Checked = false;
                    cbkAbrirAbonos.Enabled = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketAbono"].Equals(1))
                {
                    cbimprimirCancelada.Checked = false;
                    cbpreguntarAbonos.Checked = true;
                    cbpreguntarAbonos.Enabled = true;
                    cbkAbrirAbonos.Checked = false;
                    cbkAbrirAbonos.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaAbonos"].Equals(1))
                {
                    cbkAbrirAbonos.Checked = true;
                    cbkAbrirAbonos.Enabled = true;
                }
                #endregion
                #region Corte de Caja
                if (dt2.Rows[0]["TicketCorteDeCaja"].Equals(1))
                {
                    cbImprimirCorte.Checked = true;
                    cbPreguntarCorte.Checked = false;
                    cbPreguntarCorte.Enabled = false;
                    cbkAbrirCorte.Checked = false;
                    cbkAbrirCorte.Enabled = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketCorteDeCaja"].Equals(1))
                {
                    cbImprimirCorte.Checked = false;
                    cbPreguntarCorte.Checked = true;
                    cbPreguntarCorte.Enabled = true;
                    cbkAbrirCorte.Checked = false;
                    cbkAbrirCorte.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaCorte"].Equals(1))
                {
                    cbkAbrirCorte.Checked = true;
                    cbkAbrirCorte.Enabled = true;
                }

                if (dt2.Rows[0]["TicketOPDFCorteDeCaja"].Equals(1))
                {
                    RBTicketCorte.Checked = true;
                    RBTicketCorte.Enabled = false;
                }
                else
                {
                    RBPDFCorte.Checked = true;
                    RBPDFCorte.Enabled = false;
                }
                #endregion
                #region Agregar dinero
                if (dt2.Rows[0]["TicketDineroAgregado"].Equals(1))
                {
                    cbImprimirAgregar.Checked = true;
                    cbPreguntarAgregar.Checked = false;
                    cbPreguntarAgregar.Enabled = false;
                    cbkAbrirAgregar.Checked = false;
                    cbkAbrirAgregar.Enabled = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketDineroAgregado"].Equals(1))
                {
                    cbImprimirAgregar.Checked = false;
                    cbPreguntarAgregar.Checked = true;
                    cbPreguntarAgregar.Enabled = true;
                    cbkAbrirAgregar.Checked = false;
                    cbkAbrirAgregar.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaAgregar"].Equals(1))
                {
                    cbkAbrirAgregar.Checked = true;
                    cbkAbrirAgregar.Enabled = true;
                }
                #endregion
                #region retirar Dinero 
                if (dt2.Rows[0]["TicketRetiradoAgregado"].Equals(1))
                {
                    cbImprimirRetirar.Checked = true;
                    cbPreguntarRetirar.Checked = false;
                    cbPreguntarRetirar.Enabled = false;
                    cbkAbrirRetirar.Checked = false;
                    cbkAbrirRetirar.Enabled = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketRetiradoAgregado"].Equals(1))
                {
                    cbImprimirRetirar.Checked = false;
                    cbPreguntarRetirar.Checked = true;
                    cbPreguntarRetirar.Enabled = true;
                    cbkAbrirRetirar.Checked = false;
                    cbkAbrirRetirar.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaRetirar"].Equals(1))
                {
                    cbkAbrirRetirar.Checked = true;
                    cbkAbrirRetirar.Enabled = true;
                }
                #endregion
                #region Anticipos
                if (dt2.Rows[0]["TicketAnticipo"].Equals(1))
                {
                    CBImprimirAnticipos.Checked = true;
                    cbPreguntarAnticipos.Checked = false;
                    cbPreguntarAnticipos.Enabled = false;
                    cbkAbrirAnticipos.Checked = false;
                    cbkAbrirAnticipos.Enabled = false;
                }
                else if (dt2.Rows[0]["PreguntarTicketAnticipo"].Equals(1))
                {
                    CBImprimirAnticipos.Checked = false;
                    cbPreguntarAnticipos.Checked = true;
                    cbPreguntarAnticipos.Enabled = true;
                    cbkAbrirAnticipos.Checked = false;
                    cbkAbrirAnticipos.Enabled = false;
                }
                else if (dt2.Rows[0]["AbrirCajaAnticipos"].Equals(1))
                {
                    cbkAbrirAnticipos.Checked = true;
                    cbkAbrirAnticipos.Enabled = true;
                }
                #endregion
            }
            confiGeneral = new List<string>();
        }


        private void ConfiguracionTickets_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (guardado.Equals(false) && !confiGeneral.Count.Equals(0))
            {
                DialogResult mensaje = MessageBox.Show("¿Desea guardar los cambios?", "Aviso del Ssitema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mensaje.Equals(DialogResult.Yes))
                {
                    this.Close();
                }
            }
        }


        ///IMPRIMIR TICKET
        private void CBVentas_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = CBVentas.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (CBVentas.Checked.Equals(true))
            {
                CBPreguntar.Checked = false;
                CBPreguntar.Enabled = false;
                cbkAbrirVenta.Enabled = false;
                cbkAbrirVenta.Checked = false;
                string consulta1 = $"UPDATE configuracion SET PreguntarTicketVenta = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaVentas = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                confiGeneral.Add(consulta2);
            }
            else
            {
                CBPreguntar.Enabled = true;
                cbkAbrirVenta.Enabled = true;
                string consulta1 = $"UPDATE configuracion SET PreguntarTicketVenta = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaVentas = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                confiGeneral.Add(consulta2);
            }
        }
        ///PREGUNTAR TICKET
        private void CBPreguntar_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = CBPreguntar.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirVenta.Enabled = false;
                cbkAbrirVenta.Checked = false;
                string consulta2 = $"UPDATE configuracion SET AbrirCajaVentas = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                habilitado = false;
                cbkAbrirVenta.Enabled = true;
            }
            string consulta = $"UPDATE configuracion SET PreguntarTicketVenta = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
        }
        ///TICKET
        private void RBTicketVentas_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (RBTicketVentas.Checked.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 2;
            }

            string consulta = $"UPDATE Configuracion SET TicketOPDF = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            RBTicketVentas.Enabled = false;
            RBPDFVentas.Enabled = true;
        }
        ///PDF
        private void RBPDFVentas_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (RBPDFVentas.Checked.Equals(true))
            {
                habilitado = 2;
            }
            else
            {
                habilitado = 1;
            }

            string consulta = $"UPDATE Configuracion SET TicketOPDF = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            RBTicketVentas.Enabled = true;
            RBPDFVentas.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado, "editarTicket"))) { 
                if (!permisoEmpleado.Rows.Count.Equals(0)) {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    { 
                        if (item[0].ToString().Equals("1")) 
                        {
                            EditarTicket editTicket = new EditarTicket(); 
                            editTicket.ShowDialog();
                        } else { 
                            MessageBox.Show("No tienes permisos para modificar esta opcion"); return;
                        } 
                    }
                } else 
                { 
                    EditarTicket editTicket = new EditarTicket(); editTicket.ShowDialog();
                }
            }
        }

        private void cbImprimirGuardada_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbImprimirGuardada.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketPresupuesto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbImprimirGuardada.Checked.Equals(true))
            {
                cbPreguntarGuardad.Checked = false;
                cbPreguntarGuardad.Enabled = false;
                cbkAbrirGGuardadas.Enabled = false;
                cbkAbrirGGuardadas.Checked = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketPresupuesto = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaGuardada = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
                confiGeneral.Add(consulta1);
            }
            else
            {
                cbPreguntarGuardad.Enabled = true;
                cbkAbrirGGuardadas.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketPresupuesto = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarGuardad_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarGuardad.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirGGuardadas.Enabled = false;
                cbkAbrirGGuardadas.Checked = false;
            }
            else
            {
                habilitado = false;
                cbkAbrirGGuardadas.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketPresupuesto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaGuardada = 0 WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta2);
            confiGeneral.Add(consulta);
        }

        private void rbTicketGuardada_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (rbTicketGuardada.Checked.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 2;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFPresupuesto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            rbTicketGuardada.Enabled = false;
            rbPDFGuardada.Enabled = true;
        }

        private void rbPDFGuardada_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (rbPDFGuardada.Checked.Equals(true))
            {
                habilitado = 2;
            }
            else
            {
                habilitado = 1;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFPresupuesto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            rbTicketGuardada.Enabled = true;
            rbPDFGuardada.Enabled = false;
        }

        private void cbimprimirCancelada_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbimprimirCancelada.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketVentaCancelada = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbimprimirCancelada.Checked.Equals(true))
            {
                cbPreguntarCANCELADA.Checked = false;
                cbPreguntarCANCELADA.Enabled = false;
                cbkAbrirCanceladas.Checked = false;
                cbkAbrirCanceladas.Enabled = false;

                string consulta1 = $"UPDATE configuraciondetickets SET PregutarTicketVentaCancelada = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaCancelada = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
                confiGeneral.Add(consulta1);
            }
            else
            {
                cbPreguntarCANCELADA.Enabled = true;
                cbkAbrirCanceladas.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PregutarTicketVentaCancelada = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarCANCELADA_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarCANCELADA.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirCanceladas.Checked = false;
                cbkAbrirCanceladas.Enabled = false;
            }
            else
            {
                habilitado = false;
                cbkAbrirCanceladas.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PregutarTicketVentaCancelada = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaCancelada = 0 WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta2);
            confiGeneral.Add(consulta);
        }

        private void rbTicketCanceada_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (rbTicketCanceada.Checked.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 2;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFTicketVentaCancelada = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            rbTicketCanceada.Enabled = false;
            rcPDFCancelada.Enabled = true;
        }

        private void rcPDFCancelada_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (rcPDFCancelada.Checked.Equals(true))
            {
                habilitado = 2;
            }
            else
            {
                habilitado = 1;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFTicketVentaCancelada = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            rbTicketCanceada.Enabled = true;
            rcPDFCancelada.Enabled = false;
        }

        private void CBVentaCredito_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = CBVentaCredito.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET CreditoRealizado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (CBVentaCredito.Checked.Equals(true))
            {
                CBPregutnarVentaCrediro.Checked = false;
                CBPregutnarVentaCrediro.Enabled = false;
                cbkAbrirCredito.Checked = false;
                cbkAbrirCredito.Enabled = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarCreditoRealizado = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaCredito = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
                confiGeneral.Add(consulta1);
            }
            else
            {
                CBPregutnarVentaCrediro.Enabled = true;
                cbkAbrirCredito.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarCreditoRealizado = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void CBPregutnarVentaCrediro_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = CBPregutnarVentaCrediro.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirCredito.Checked = false;
                cbkAbrirCredito.Enabled = false;
            }
            else
            {
                habilitado = false;
                cbkAbrirCredito.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarCreditoRealizado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaCredito = 0 WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta2);
            confiGeneral.Add(consulta);
        }

        private void RBTicketCredito_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (RBTicketCredito.Checked.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 2;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFCreditoRealizado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            RBTicketCredito.Enabled = false;
            RBPDFCredito.Enabled = true;
        }

        private void RBPDFCredito_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (RBPDFCredito.Checked.Equals(true))
            {
                habilitado = 2;
            }
            else
            {
                habilitado = 1;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFCreditoRealizado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            RBTicketCredito.Enabled = true;
            RBPDFCredito.Enabled = false;
        }

        private void cbImprimirVentaGlobal_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbImprimirVentaGlobal.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketVentaGlobal = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbImprimirVentaGlobal.Checked.Equals(true))
            {
                cbPreguntarGobal.Checked = false;
                cbPreguntarGobal.Enabled = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketVentaGlobal = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
            else
            {
                cbPreguntarGobal.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketVentaGlobal = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarGobal_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarGobal.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketVentaGlobal = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (radioButton2.Checked.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 2;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFVentaGlobal = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            radioButton2.Enabled = false;
            radioButton1.Enabled = true;
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (radioButton1.Checked.Equals(true))
            {
                habilitado = 2;
            }
            else
            {
                habilitado = 1;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFVentaGlobal = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            radioButton2.Enabled = true;
            radioButton1.Enabled = false;
        }

        private void cbImprimirAbonos_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbImprimirAbonos.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketAbono = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbImprimirAbonos.Checked.Equals(true))
            {
                cbpreguntarAbonos.Checked = false;
                cbpreguntarAbonos.Enabled = false;
                cbkAbrirAbonos.Checked = false;
                cbkAbrirAbonos.Enabled = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketAbono = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                string consulta2 = $"UPDATE configuracion SET AbrirCajaAbonos = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                cbpreguntarAbonos.Enabled = true;
                cbkAbrirAbonos.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketAbono = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbpreguntarAbonos_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbpreguntarAbonos.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirAbonos.Checked = false;
                cbkAbrirAbonos.Enabled = false;
            }
            else
            {
                habilitado = false;
                cbkAbrirAbonos.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketAbono = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaAbonos = 0 WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta2);
        }

        private void cbImprimirCorte_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbImprimirCorte.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbImprimirCorte.Checked.Equals(true))
            {
                cbPreguntarCorte.Checked = false;
                cbPreguntarCorte.Enabled = false;
                cbkAbrirCorte.Checked = false;
                cbkAbrirCorte.Enabled = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketCorteDeCaja = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaCorte = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                cbPreguntarCorte.Enabled = true;
                cbkAbrirCorte.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketCorteDeCaja = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarCorte_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarCorte.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirCorte.Checked = false;
                cbkAbrirCorte.Enabled = false;

            }
            else
            {
                habilitado = false;
                cbkAbrirCorte.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaCorte = 0 WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta2);
        }

        private void RBTicketCorte_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (RBTicketCorte.Checked.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 2;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            RBTicketCorte.Enabled = false;
            RBPDFCorte.Enabled = true;
        }

        private void RBPDFCorte_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = 0;

            if (RBPDFCorte.Checked.Equals(true))
            {
                habilitado = 2;
            }
            else
            {
                habilitado = 1;
            }

            string consulta = $"UPDATE configuraciondetickets SET TicketOPDFCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
            RBTicketCorte.Enabled = true;
            RBPDFCorte.Enabled = false;
        }

        private void cbImprimirAgregar_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbImprimirAgregar.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketDineroAgregado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbImprimirAgregar.Checked.Equals(true))
            {
                cbPreguntarAgregar.Checked = false;
                cbPreguntarAgregar.Enabled = false;
                cbkAbrirAgregar.Enabled = false;
                cbkAbrirAgregar.Checked = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketDineroAgregado = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaAgregar = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                cbPreguntarAgregar.Enabled = true;
                cbkAbrirAgregar.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketDineroAgregado = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarAgregar_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarAgregar.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirAgregar.Enabled = false;
                cbkAbrirAgregar.Checked = false;

                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaAgregar = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                habilitado = false;
                cbkAbrirAgregar.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketDineroAgregado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
        }

        private void cbImprimirRetirar_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbImprimirRetirar.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
            }
            else
            {
                habilitado = false;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketRetiradoAgregado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (cbImprimirRetirar.Checked.Equals(true))
            {
                cbPreguntarRetirar.Checked = false;
                cbPreguntarRetirar.Enabled = false;
                cbkAbrirRetirar.Checked = false;
                cbkAbrirRetirar.Enabled = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketRetiradoAgregado = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaRetirar = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                cbPreguntarRetirar.Enabled = true;
                cbkAbrirRetirar.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketRetiradoAgregado = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarRetirar_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarRetirar.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirRetirar.Checked = false;
                cbkAbrirRetirar.Enabled = false;
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaRetirar = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                habilitado = false;
                cbkAbrirRetirar.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketRetiradoAgregado = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

        }

        private void CBImprimirAnticipos_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = CBImprimirAnticipos.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirAnticipos.Enabled = false;
                cbkAbrirAnticipos.Checked = false;
            }
            else
            {
                habilitado = false;
                cbkAbrirAnticipos.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET TicketAnticipo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);

            if (CBImprimirAnticipos.Checked.Equals(true))
            {
                cbPreguntarAnticipos.Checked = false;
                cbPreguntarAnticipos.Enabled = false;
                cbkAbrirAnticipos.Enabled = false;
                cbkAbrirAnticipos.Checked = false;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketAnticipo = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaAnticipos = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                cbPreguntarAnticipos.Enabled = true;
                cbkAbrirAnticipos.Enabled = true;
                string consulta1 = $"UPDATE configuraciondetickets SET PreguntarTicketAnticipo = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta1);
            }
        }

        private void cbPreguntarAnticipos_MouseClick(object sender, MouseEventArgs e)
        {
            var habilitado = false;

            valorCambioCheckBox = cbPreguntarAnticipos.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = true;
                cbkAbrirAnticipos.Enabled = false;
                cbkAbrirAnticipos.Checked = false;
                string consulta2 = $"UPDATE configuraciondetickets SET AbrirCajaAnticipos = 0 WHERE IDUsuario = {FormPrincipal.userID}";
                confiGeneral.Add(consulta2);
            }
            else
            {
                habilitado = false;
                cbkAbrirAnticipos.Enabled = true;
            }
            string consulta = $"UPDATE configuraciondetickets SET PreguntarTicketAnticipo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var item in confiGeneral)
            {
                cn.EjecutarConsulta(item);
            }

            MessageBox.Show("Configuracion Guardada con exito", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            guardado = true;
            this.Close();
        }

        private void GuardarCambios(object sender, MouseEventArgs e) 
        {
            var habilitado = 0;

            CheckBox cb = (CheckBox)sender;
            valorCambioCheckBox = cb.Checked;

            if (valorCambioCheckBox.Equals(true))
            {
                habilitado = 1;
            }
            else
            {
                habilitado = 0;
            }
            string consulta = $"UPDATE configuraciondetickets SET {ArrayImprimir[contadorArray]} = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
            confiGeneral.Add(consulta);
        }

        private void cbkAbrirVenta_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 0;
            GuardarCambios(sender, e);
        }


        private void cbkAbrirGGuardadas_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 1;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirCanceladas_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 2;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirCredito_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 3;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirAnticipos_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 4;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirAbonos_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 5;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirCorte_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 6;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirAgregar_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 7;
            GuardarCambios(sender, e);
        }

        private void cbkAbrirRetirar_MouseClick(object sender, MouseEventArgs e)
        {
            contadorArray = 8;
            GuardarCambios(sender, e);
        }
    }
}

