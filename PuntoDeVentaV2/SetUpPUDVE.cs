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
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int numeroRevision = 0;

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

            txtMinimoMayoreo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNoVendidos.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtPorcentajeProducto.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNumeroRevision.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNumeroRevision.Text = numeroRevision.ToString();
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
                var datosConfig = mb.ComprobarConfiguracion();

                checkCBVenta.Checked = Convert.ToBoolean(datosConfig[4]);
                cbCorreoPrecioProducto.Checked = Convert.ToBoolean(datosConfig[0]);
                cbCorreoStockProducto.Checked = Convert.ToBoolean(datosConfig[1]);
                cbCorreoStockMinimo.Checked = Convert.ToBoolean(datosConfig[2]);
                cbCorreoVenderProducto.Checked = Convert.ToBoolean(datosConfig[3]);
                pagWeb.Checked = Convert.ToBoolean(datosConfig[5]);
                cbMostrarPrecio.Checked = Convert.ToBoolean(datosConfig[6]);
                cbMostrarCB.Checked = Convert.ToBoolean(datosConfig[7]);
                txtPorcentajeProducto.Text = datosConfig[8].ToString();
                checkMayoreo.Checked = Convert.ToBoolean(datosConfig[9]);
                txtMinimoMayoreo.Text = datosConfig[10].ToString();
                checkNoVendidos.Checked = Convert.ToBoolean(datosConfig[11]);
                txtNoVendidos.Text = datosConfig[12].ToString();
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

                    MessageBox.Show("Información respaldada exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuardarRevision_Click(object sender, EventArgs e)
        {
            var numeroRevision = txtNumeroRevision.Text;

            if (string.IsNullOrWhiteSpace(numeroRevision))
            {
                MessageBox.Show("Es necesario asignar un número de revisión", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var respuesta = cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET NoRevision = {numeroRevision} WHERE IDUsuario = {FormPrincipal.userID}", true);

            if (respuesta > 0)
            {
                MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiarTabla_Click(object sender, EventArgs e)
        {
            int resultado = cn.EjecutarConsulta($"DELETE FROM RevisarInventario WHERE IDUsuario = {FormPrincipal.userID}");

            if (resultado > 0)
            {
                MessageBox.Show("Operación terminada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cbCorreoPrecioProducto_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoPrecioProducto.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoStockProducto_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoStockProducto.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoStockProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoStockMinimo_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoStockMinimo.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoStockMinimo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoVenderProducto_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoVenderProducto.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoVentaProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void pagWeb_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (pagWeb.Checked)
            {
                habilitado = 1; 
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET IniciarProceso = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
            FormPrincipal.pasar = habilitado;
        }

        private void cbMostrarPrecio_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbMostrarPrecio.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbMostrarCB_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbMostrarCB.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarCodigoProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void btnGuardarPorcentaje_Click(object sender, EventArgs e)
        {
            var porcentaje = txtPorcentajeProducto.Text.Trim();

            if (string.IsNullOrWhiteSpace(porcentaje))
            {
                MessageBox.Show("Ingrese la cantidad de porcentaje", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPorcentajeProducto.Focus();
                return;
            }

            var respuesta = cn.EjecutarConsulta($"UPDATE Configuracion SET PorcentajePrecio = {porcentaje} WHERE IDUsuario = {FormPrincipal.userID}");

            if (respuesta > 0)
            {
                MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void checkMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (checkMayoreo.Checked)
            {
                habilitado = 1;
                txtMinimoMayoreo.Enabled = true;
                txtMinimoMayoreo.Focus();
                cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
            }
            else
            {
                txtMinimoMayoreo.Enabled = false;
                txtMinimoMayoreo.Text = string.Empty;
                cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado}, MinimoMayoreo = 0 WHERE IDUsuario = {FormPrincipal.userID}");
            } 
        }

        private void txtMinimoMayoreo_KeyUp(object sender, KeyEventArgs e)
        {
            var cantidad = txtMinimoMayoreo.Text.Trim();

            if (string.IsNullOrWhiteSpace(cantidad))
            {
                cantidad = "0";
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET MinimoMayoreo = {cantidad} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void checkNoVendidos_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNoVendidos.Checked)
            {
                txtNoVendidos.Enabled = true;
                txtNoVendidos.Focus();
                cn.EjecutarConsulta($"UPDATE Configuracion SET checkNoVendidos = 1 WHERE IDUsuario = {FormPrincipal.userID}");
            }
            else
            {
                txtNoVendidos.Enabled = false;
                txtNoVendidos.Text = string.Empty;
                cn.EjecutarConsulta($"UPDATE Configuracion SET checkNoVendidos = 0, diasNoVendidos = 0 WHERE IDUsuario = {FormPrincipal.userID}");
            }
        }

        private void txtNoVendidos_KeyUp(object sender, KeyEventArgs e)
        {
            var cantidad = txtNoVendidos.Text.Trim();

            if (string.IsNullOrWhiteSpace(cantidad))
            {
                cantidad = "0";
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET diasNoVendidos = {cantidad} WHERE IDUsuario = {FormPrincipal.userID}");
        }
    }
}