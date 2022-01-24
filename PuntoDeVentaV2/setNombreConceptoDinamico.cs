using MySql.Data.MySqlClient;
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
    public partial class setNombreConceptoDinamico : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public int idRegConcept { get; set; }

        #region cargar Dato Concepto Dinamico
        private void cargarDato()
        {
            using(DataTable dtConceptoDinamico = cn.CargarDatos(cs.BusarContenidoDinamicoHabilitado(idRegConcept)))
            {
                if (!dtConceptoDinamico.Rows.Count.Equals(0))
                {
                    foreach(DataRow filaDatos in dtConceptoDinamico.Rows)
                    {
                        txtConceptoActual.Text = filaDatos["Concepto"].ToString().Replace("_"," ");
                    }
                }
            }
        }
        #endregion

        public setNombreConceptoDinamico()
        {
            InitializeComponent();
        }

        private void setNombreConceptoDinamico_Load(object sender, EventArgs e)
        {
            cargarDato();
            //txtConceptoNuevo.Focus();
            txtConceptoNuevo.Select();
        }

        private void txtConceptoNuevo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string editDetalleNvo = txtConceptoNuevo.Text.Replace(" ", "_");
            string editDetelle = txtConceptoActual.Text.Replace(" ","_");

            if (!editDetalleNvo.Equals(string.Empty))
            {
                try
                {
                    bool found = false;

                    using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(editDetalleNvo, FormPrincipal.userID)))
                    {
                        if (!dtItemDinamicos.Rows.Count.Equals(0))
                        {
                            found = true;
                        }
                        else if (dtItemDinamicos.Rows.Count.Equals(0))
                        {
                            found = false;
                        }
                    }

                    if (found.Equals(false))
                    {
                        string tableSource = string.Empty;

                        try
                        {
                            tableSource = "appSettings";
                            var UpdateDatoDinamico = cn.EjecutarConsulta(cs.ActualizarDatoDinamico(editDetelle, editDetalleNvo, FormPrincipal.userID));
                        }
                        catch (MySqlException exMySql)
                        {
                            MessageBox.Show($"Ocurrio una irregularidad al intentar\nActualizar Nombre del Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar actualizar registro de Detalle Dinamico...\nError: " + ex.Message.ToString() + $"\n({tableSource})", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        try
                        {
                            tableSource = "FiltroDinamico";
                            var UpdateDatoFiltroDinamico = cn.EjecutarConsulta(cs.ActualizarNombreDatoFiltroDinamico(editDetelle, editDetalleNvo, FormPrincipal.userID));
                        }
                        catch (MySqlException exMySql)
                        {
                            MessageBox.Show($"Ocurrio una irregularidad al intentar\nActualizar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar actualizar registro de Filtro Dinamico...\nError: " + ex.Message.ToString() + $"\n({tableSource})", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        try
                        {
                            tableSource = "DetallesProductoGenerales";
                            var UpdateNombreDatoFiltroDinamico = cn.EjecutarConsulta(cs.RenombrarDetallesProductoGenerales(editDetalleNvo, editDetelle, FormPrincipal.userID));
                        }
                        catch (MySqlException exMySql)
                        {
                            MessageBox.Show($"Ocurrio una irregularidad al intentar\nActualizar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar actualizar registro de Filtro Dinamico...\nError: " + ex.Message.ToString() + $"\n({tableSource})", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        try
                        {
                            tableSource = "DetalleGeneral";
                            var UdapteDatosDelFiltroDinamico = cn.EjecutarConsulta(cs.RenombrarDatosDelFiltroDinamico(editDetalleNvo, editDetelle, FormPrincipal.userID));
                        }
                        catch (MySqlException exMySql)
                        {
                            MessageBox.Show($"Ocurrio una irregularidad al intentar\nActualizar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al intentar actualizar registro de Filtro Dinamico...\nError: " + ex.Message.ToString() + $"\n({tableSource})", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        this.Close();
                    }
                    else if (found.Equals(true))
                    {
                        MessageBox.Show("Nombre del concepto ya esta siendo utilizado", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtConceptoNuevo.Clear();
                        txtConceptoNuevo.Select();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Intentar Renombrar un Concepto Dinamico:\n" + ex.Message.ToString(), "Error al Renombrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConceptoNuevo.Clear();
                    txtConceptoNuevo.Select();
                }
            }
            else if (editDetalleNvo.Equals(string.Empty))
            {
                MessageBox.Show("Favor de llenar el campo de nuevo", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void setNombreConceptoDinamico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
