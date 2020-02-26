﻿using System;
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
    public partial class SetUpPUDVE : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int numeroRevision = 0;
        private int checkTicketVenta = 0;
        private int checkStockNegativo = 0;

        public static bool recargarDatos = false;

        public SetUpPUDVE()
        {
            InitializeComponent();
        }

        private void SetUpPUDVE_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.StockNegativo)
            {
                cbStockNegativo.Checked = true;
            }

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                txtNombreServidor.Text = Properties.Settings.Default.Hosting;
            }

            VerificarDatosInventario();
            VerificarConfiguracion();

            txtNumeroRevision.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNumeroRevision.Text = numeroRevision.ToString();

            if (checkTicketVenta == 1)
            {
                checkCBVenta.Checked = true;
            }
        }

        private void VerificarDatosInventario()
        {
            // Numero de revision inventario
            var datosInventario = mb.DatosRevisionInventario();

            // Si existe un registro en la tabla obtiene los datos de lo contrario hace un insert para
            // que exista la configuracion necesaria
            if (datosInventario.Length > 0)
            {
                datosInventario = mb.DatosRevisionInventario();
                numeroRevision = Convert.ToInt32(datosInventario[1]);
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, FechaInventario, NoRevision) VALUES ('{FormPrincipal.userID}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '1')", true);

                datosInventario = mb.DatosRevisionInventario();
                numeroRevision = Convert.ToInt32(datosInventario[1]);
            }
        }

        private void VerificarConfiguracion()
        {
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (existe)
            {
                var datosConfig = mb.DatosConfiguracion();

                checkTicketVenta = Convert.ToInt16(datosConfig[0]);
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{FormPrincipal.userID}')");
            }
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            guardarArchivo.Filter = "SQL (*.db)|*.db";
            guardarArchivo.FilterIndex = 1;
            guardarArchivo.RestoreDirectory = true;

            if (guardarArchivo.ShowDialog() == DialogResult.OK)
            {
                string archivoBD = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db";
                string copiaDB = guardarArchivo.FileName;
                //string fecha = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");

                try
                {
                    File.Copy(archivoBD, copiaDB);

                    MessageBox.Show("Información respaldada exitosamente", 
                                    "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbStockNegativo_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.StockNegativo = cbStockNegativo.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void btnGuardarServidor_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Hosting = txtNombreServidor.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            MessageBox.Show("Información guardada", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuardarRevision_Click(object sender, EventArgs e)
        {
            var numeroRevision = txtNumeroRevision.Text;

            if (string.IsNullOrWhiteSpace(numeroRevision))
            {
                MessageBox.Show("Es necesario asignar un número de revisión", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var respuesta = cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET NoRevision = {numeroRevision} WHERE IDUsuario = {FormPrincipal.userID}", true);

            if (respuesta > 0)
            {
                MessageBox.Show("Información guardada", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiarTabla_Click(object sender, EventArgs e)
        {
            int resultado = cn.EjecutarConsulta($"DELETE FROM RevisarInventario WHERE IDUsuario = {FormPrincipal.userID}");

            if (resultado > 0)
            {
                MessageBox.Show("Operación terminada", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void SetUpPUDVE_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                VerificarDatosInventario();
                txtNumeroRevision.Text = numeroRevision.ToString();
                recargarDatos = false;
            }
        }


        private void checkCBVenta_CheckedChanged(object sender, EventArgs e)
        {
            var ticketVenta = 0;

            if (checkCBVenta.Checked)
            {
                ticketVenta = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {ticketVenta} WHERE IDUsuario = {FormPrincipal.userID}");
        }
    }
}
<<<<<<< HEAD
=======

//25/02/20

/*
  if (!txtTimerSetUp.Text.Equals(""))
            {
                if (rbSegundos.Checked.Equals(true))
                {

                    Properties.Settings.Default.tiempoTimerAndroid = 1000 * Convert.ToInt32(txtTimerSetUp.Text);
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();

                   // MessageBox.Show("Tiempo asignado" + Properties.Settings.Default.tiempoTimerAndroid);

                  

                    FormCollection formulariosApp = Application.OpenForms;
                    foreach (Form frm in formulariosApp)
                    {
                        if (frm.Name != "FormPrincipal" && frm.Name != "Login" && frm.Name != "SetUpPUDVE")
                        {
                            frm.Close();
                        }
                    }

                    this.Close();
                    

                }

                if (rbMinutos.Checked.Equals(true))
                {

                    Properties.Settings.Default.tiempoTimerAndroid = 60000 * Convert.ToInt32(txtTimerSetUp.Text);
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();

                    // MessageBox.Show("Tiempo asignado" + Properties.Settings.Default.tiempoTimerAndroid);



                    FormCollection formulariosApp = Application.OpenForms;
                    foreach (Form frm in formulariosApp)
                    {
                        if (frm.Name != "FormPrincipal" && frm.Name != "Login" && frm.Name != "SetUpPUDVE")
                        {
                            frm.Close();
                        }
                    }

                    this.Close();


                }
            }
 * */
>>>>>>> ajustes
