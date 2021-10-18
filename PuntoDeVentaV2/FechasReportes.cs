﻿using System;
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
    public partial class FechasReportes : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();

        public string concepto { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }

        private string origen = string.Empty;

        public static string idEncontrado { get; set; }
        public static string lugarProcedencia { get; set; }

        public FechasReportes(string origen = "")
        {
            InitializeComponent();

            this.origen = origen;
        }

        private void FechasReportes_Load(object sender, EventArgs e)
        {
            primerDatePicker.Value = DateTime.Today.AddDays(-30);

            if (!string.IsNullOrEmpty(origen))
            {
                var conceptos = mb.ObtenerConceptosDinamicos(origen: origen);

                //cbConceptos.Visible = true;
                cbConceptos.DataSource = conceptos.ToArray();
                cbConceptos.DisplayMember = "Value";
                cbConceptos.ValueMember = "Key";
            }

            cargarDatosCombo();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            fechaInicial = primerDatePicker.Value.ToString("yyyy-MM-dd");
            fechaFinal = segundoDatePicker.Value.ToString("yyyy-MM-dd");

            var tipoBusqurda = cbEmpleados.SelectedItem.ToString();

            var existencia = verificarExistencia(tipoBusqurda);

            if (existencia)
            {
                HistorialPrecioBuscador hpBuscador = new HistorialPrecioBuscador(tipoBusqurda, fechaInicial, fechaFinal);

                if (tipoBusqurda.Equals("Seleccionar Empleado/Producto") || tipoBusqurda.Equals("Reporte general"))
                {
                    terminarOperaciones();
                }
                else
                {
                    hpBuscador.FormClosed += delegate
                    {
                        var idBusqueda = HistorialPrecioBuscador.idEmpleadoObtenido;
                        if (!string.IsNullOrEmpty(idBusqueda))
                        {
                            terminarOperaciones();
                        }
                    };

                    hpBuscador.ShowDialog();
                }
            }else
            {
                MessageBox.Show($"No cuenta con ningun {tipoBusqurda}", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool verificarExistencia(string empleadoProducto)
        {
            var result = false;
            var tipoEstado = string.Empty;
            //if (origen.Equals("Productos"))
            //{

            if (empleadoProducto.Equals("Reporte general"))
            {
                empleadoProducto = "Seleccionar Empleado/Producto";
            }
                empleadoProducto = "Productos";
                tipoEstado = "`Status`";

                if (empleadoProducto.Equals("Empleados"))
                {
                    tipoEstado = "estatus";
                }
                else if (empleadoProducto.Equals("Productos"))
                {
                    tipoEstado = "`Status`";
                }
                else if (empleadoProducto.Equals("Seleccionar Empleado/Producto"))
                {
                    result = true;
                }
            //}

            var query = cn.CargarDatos($"SELECT * FROM {empleadoProducto} WHERE IDUsuario = '{FormPrincipal.userID}' AND {tipoEstado} = 1");

            if (!query.Rows.Count.Equals(0))
            {
                result = true;
            }

            return result;
        }

        private void cargarDatosCombo()
        {
            if (!origen.Equals("Productos"))
            {
                cbEmpleados.Items.Add("Seleccionar Empleado/Producto");
            }
            else
            {
                cbEmpleados.Items.Add("Reporte general");
            }

            cbEmpleados.Items.Add("Empleados");

            if (!origen.Equals("Productos"))
            {
                cbEmpleados.Items.Add("Productos");
            }

            cbEmpleados.SelectedIndex = 0;
            //var query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}'");
            //cbEmpleados.Items.Add("Seleccionar concepto...");
            //if (!query.Rows.Count.Equals(0))
            //{
            //    foreach (DataRow empleados in query.Rows)
            //    {
            //        cbEmpleados.Items.Add(empleados["Nombre"].ToString());
            //    }
            //}
        }

        private void terminarOperaciones()
        {
            concepto = cbConceptos.GetItemText(cbConceptos.SelectedItem);
            fechaInicial = primerDatePicker.Value.ToString("yyyy-MM-dd");
            fechaFinal = segundoDatePicker.Value.ToString("yyyy-MM-dd");

            idEncontrado = HistorialPrecioBuscador.idEmpleadoObtenido;
            lugarProcedencia = HistorialPrecioBuscador.procedencia;

            DialogResult = DialogResult.OK;
            Reportes.botonAceptar = true;
            Close();
        }

        private void FechasReportes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
