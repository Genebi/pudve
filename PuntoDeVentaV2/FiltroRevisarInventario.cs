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
    public partial class FiltroRevisarInventario : Form
    {
        // Instanciar clase Conexion y Consultas
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        // Para el filtro de inventario
        #region Variables para el Filtro de Inventario
        public string tipoFiltro { get; set; }
        public string operadorFiltro { get; set; }
        public int cantidadFiltro { get; set; }
        public string textoFiltroDinamico { get; set; }
        public static string datoCbo { get; set; }

        Dictionary<string, string> filtros = new Dictionary<string, string>();
        Dictionary<string, string> operadores = new Dictionary<string, string>();
        Dictionary<string, string> filtroDinamico = new Dictionary<string, string>();
        List<string> strFiltroDinamico = new List<string>();
        #endregion Fin de Variables para el Filtro de Invetario

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

        public FiltroRevisarInventario()
        {
            InitializeComponent();
        }

        private void FiltroRevisarInventario_Load(object sender, EventArgs e)
        {   
            //Esta condicion es para que solo se muestren todos los tipos de filtros para los primeros usuarios y para los demas solo tenga el filtro normal
            if (FormPrincipal.clave == 1)
            {
                filtros.Add("Normal", "Revision Normal");
                filtros.Add("Stock", "Stock");
                filtros.Add("StockMinimo", "Stock Mínimo");
                filtros.Add("StockNecesario", "Stock Máximo");
                filtros.Add("NumeroRevision", "Número de Revisión");
                filtros.Add("Filtros", "Por Filtros");

                operadores.Add("NA", "Seleccionar opción...");
                operadores.Add(">=", "Mayor o igual que");
                operadores.Add("<=", "Menor o igual que");
                operadores.Add("=", "Igual que");
                operadores.Add(">", "Mayor que");
                operadores.Add("<", "Menor que");
            }
            else
            {
                filtros.Add("Normal", "Revision Normal");

            }
            

            filtroDinamico.Add("NA", "Selecciona filtro...");

            using (DataTable dtFiltroDinamico = cn.CargarDatos(cs.LlenarFiltroDinamicoComboBox(FormPrincipal.userID)))
            {
                if (!dtFiltroDinamico.Rows.Count.Equals(0))
                {
                    foreach (DataRow drFiltroDinamico in dtFiltroDinamico.Rows)
                    {
                        if (!filtroDinamico.ContainsKey(drFiltroDinamico["concepto"].ToString()))
                        {
                            filtroDinamico.Add(drFiltroDinamico["concepto"].ToString(), drFiltroDinamico["concepto"].ToString().Remove(0, 3));
                        }
                    }
                }
            }

            cbFiltro.DataSource = filtros.ToArray();
            cbFiltro.DisplayMember = "Value";
            cbFiltro.ValueMember = "Key";

            txtCantidad.KeyPress += new KeyPressEventHandler(SoloDecimales);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var filtro = cbFiltro.SelectedValue.ToString();
            
            tipoFiltro = filtro;
            datoCbo = string.Empty;
            if (filtro == "Normal")
            {
                operadorFiltro = "NA";
                cantidadFiltro = 0;
                datoCbo = "Normal";
            }
            else if (filtro == "Filtros")
            {
                var fieldTable = cbOperadores.SelectedValue.ToString();
                var strFiltro = cbFiltroDinamico.SelectedItem.ToString();

                if (fieldTable == "NA")
                {
                    MessageBox.Show("Seleccione una opción de las condiciones para el filtro", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbOperadores.Focus();
                    return;
                }

                string palabra = "Selecciona";
                bool foundWord = strFiltro.Contains(palabra);

                if (foundWord)
                {
                    MessageBox.Show("Seleccione una opción de la\nlista de Fitros comó Proveedor, etc;\nque estan en la lista deslegable", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbFiltroDinamico.Focus();
                    cbFiltroDinamico.DroppedDown = true;
                    return;
                }

                operadorFiltro = fieldTable;
                textoFiltroDinamico = strFiltro;
            }
            else
            {
                var operador = cbOperadores.SelectedValue.ToString();
                var cantidad = txtCantidad.Text.Trim();


                if (operador == "NA")
                {
                    MessageBox.Show("Seleccione una opción de las condiciones para el filtro", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbOperadores.Focus();
                    cbOperadores.DroppedDown = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(cantidad))
                {
                    MessageBox.Show("Es necesario ingresar una cantidad", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Focus();
                    return;
                }

                operadorFiltro = operador;
                cantidadFiltro = Convert.ToInt32(cantidad);
            }

            //DialogResult = DialogResult.OK;
            Inventario.aceptarFiltro = true;
            Close();
        }

        private void cbFiltro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var filtro = cbFiltro.SelectedValue.ToString();

            if (filtro == "Normal")
            {
                cbOperadores.Visible = false;
                txtCantidad.Visible = false;
                cbFiltroDinamico.Visible = false;
            }
            else if (filtro != "Filtros" && filtro != "Normal")
            {

                cbOperadores.DataSource = operadores.ToArray();
                cbOperadores.DisplayMember = "Value";
                cbOperadores.ValueMember = "Key";

                cbOperadores.Visible = true;
                txtCantidad.Visible = true;
                cbFiltroDinamico.Visible = false;
            }
            else if (filtro == "Filtros")
            {
                cbOperadores.DataSource = filtroDinamico.ToArray();
                cbOperadores.DisplayMember = "Value";
                cbOperadores.ValueMember = "Key";

                cbOperadores.Visible = true;
                cbFiltroDinamico.Visible = true;
                txtCantidad.Visible = false;
            }
        }

        private void cbOperadores_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var filtroDinamico = cbOperadores.SelectedValue.ToString();
            string filtroSeleccionado = string.Empty;

            strFiltroDinamico.Clear();

            if (!filtroDinamico.Equals("NA"))
            {
                if (!filtroDinamico.Equals(">="))
                {
                    if (!filtroDinamico.Equals("<="))
                    {
                        if (!filtroDinamico.Equals("="))
                        {
                            if (!filtroDinamico.Equals(">"))
                            {
                                if (!filtroDinamico.Equals("<"))
                                {
                                    filtroSeleccionado = filtroDinamico.Remove(0, 3);
                                    string inicioStr = "Selecciona " + filtroSeleccionado + "...";
                                    string sinFiltro = "Sin " + filtroSeleccionado;

                                    strFiltroDinamico.Add(inicioStr.ToUpper());
                                    strFiltroDinamico.Add(sinFiltro.ToUpper());

                                    if (filtroSeleccionado.Equals("Proveedor"))
                                    {
                                        using (DataTable dtStrFiltroDinamico = cn.CargarDatos(cs.ListarProveedores(FormPrincipal.userID)))
                                        {
                                            if (!dtStrFiltroDinamico.Rows.Count.Equals(0))
                                            {
                                                foreach (DataRow drStrFiltroDinamico in dtStrFiltroDinamico.Rows)
                                                {
                                                    strFiltroDinamico.Add(drStrFiltroDinamico["Nombre"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    else if (!filtroSeleccionado.Equals("Proveedor"))
                                    {
                                        using (DataTable dtStrFiltroDinamico = cn.CargarDatos(cs.ListarDetalleGeneral(FormPrincipal.userID, filtroSeleccionado)))
                                        {
                                            if (!dtStrFiltroDinamico.Rows.Count.Equals(0))
                                            {
                                                foreach (DataRow drStrFiltroDinamico in dtStrFiltroDinamico.Rows)
                                                {
                                                    strFiltroDinamico.Add(drStrFiltroDinamico["Descripcion"].ToString());
                                                }
                                            }
                                        }
                                    }

                                    if (cbFiltroDinamico.Visible.Equals(true))
                                    {
                                        cbFiltroDinamico.DataSource = strFiltroDinamico.ToArray();
                                    }   
                                }
                            }
                        }
                    }
                }
            }
            else if (filtroDinamico.Equals("NA"))
            {
                if (cbFiltroDinamico.Visible.Equals(true))
                {
                    cbFiltroDinamico.DataSource = strFiltroDinamico.ToArray();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
