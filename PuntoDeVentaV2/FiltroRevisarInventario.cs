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
            cbFiltro.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbFiltroDinamico.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbOperadores.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbProveedor.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipoRevision.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            
            RevisarInventario.mensajeInventario = 0;

            filtros.Add("Normal", "Revision Normal");
            //filtros.Add("Stock", "Stock");
            //filtros.Add("StockMinimo", "Stock Mínimo");
            //filtros.Add("StockNecesario", "Stock Máximo");
            filtros.Add("NumeroRevision", "Número de Revisión");
            filtros.Add("CantidadPedir", "Cantidad a Pedir");
            filtros.Add("Proveedor", "Proveedores");
            filtros.Add("Filtros", "Personalizados");


            operadores.Add("NA", "Seleccionar opción...");
            operadores.Add(">=", "Mayor o igual que");
            operadores.Add("<=", "Menor o igual que");
            operadores.Add("=", "Igual que");
            operadores.Add(">", "Mayor que");
            operadores.Add("<", "Menor que");


            filtroDinamico.Add("NA", "Seleccionar filtro...");

            using (DataTable dtFiltroDinamico = cn.CargarDatos(cs.LlenarFiltroDinamicoComboBox(FormPrincipal.userID)))
            {
                if (!dtFiltroDinamico.Rows.Count.Equals(0))
                {
                    foreach (DataRow drFiltroDinamico in dtFiltroDinamico.Rows)
                    {
                        if (!filtroDinamico.ContainsKey(drFiltroDinamico["concepto"].ToString()))
                        {
                            var nombreFiltro = drFiltroDinamico["concepto"].ToString();

                            if (nombreFiltro.Equals("chkProveedor")) continue;

                            filtroDinamico.Add(nombreFiltro, nombreFiltro.Remove(0, 3));
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
                var strFiltro = string.Empty;

                if (!fieldTable.Equals("NA"))
                {
                    strFiltro = cbFiltroDinamico.SelectedItem.ToString();
                }

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
            else if (filtro.Equals("Proveedor"))
            {
                var idProveedor = ((KeyValuePair<int, string>)cbProveedor.SelectedItem).Key;

                if (idProveedor > 0)
                {
                    var tipoRevision = ((KeyValuePair<int, string>)cbTipoRevision.SelectedItem).Key;

                    tipoFiltro = "Proveedores";
                    operadorFiltro = $"{idProveedor}|{tipoRevision}";

                    var operador = string.Empty;

                    if (tipoRevision.Equals(1))
                    {
                        var consulta = $"UPDATE Productos P INNER JOIN DetallesProducto D ON (P.ID = D.IDProducto AND D.IDProveedor = {idProveedor}) SET P.NumeroRevision = 0 WHERE P.IDUsuario = {FormPrincipal.userID} AND P.Status = 1 AND P.Tipo = 'P' AND (P.CodigoBarras != '' OR P.ClaveInterna != '')";

                        cn.EjecutarConsulta(consulta);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un proveedor para continuar.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbProveedor.Focus();
                    cbProveedor.DroppedDown = true;
                    return;
                }
            }
            else
            {
                
                var operador = cbOperadores.SelectedValue.ToString();
                var cantidad = txtCantidad.Text.Trim();
                txtCantidad.MaxLength = 10;


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
                cbProveedor.Visible = false;
            }
            else if (filtro != "Filtros" && filtro != "Normal" && filtro != "Proveedor")
            {

                cbOperadores.DataSource = operadores.ToArray();
                cbOperadores.DisplayMember = "Value";
                cbOperadores.ValueMember = "Key";

                cbOperadores.Visible = true;
                txtCantidad.Visible = true;
                cbFiltroDinamico.Visible = false;
                cbProveedor.Visible = false;
            }
            else if (filtro == "Filtros")
            {
                cbOperadores.DataSource = filtroDinamico.ToArray();
                cbOperadores.DisplayMember = "Value";
                cbOperadores.ValueMember = "Key";

                cbOperadores.Visible = true;
                cbFiltroDinamico.Visible = true;
                txtCantidad.Visible = false;
                cbProveedor.Visible = false;
            }
            else if (filtro.Equals("Proveedor"))
            {
                cbOperadores.Visible = false;
                txtCantidad.Visible = false;
                cbFiltroDinamico.Visible = false;
                cbProveedor.Visible = true;

                // Cargar los proveedores para el combobox
                var proveedores = cn.ObtenerProveedores(FormPrincipal.userID);

                if (proveedores.Length > 0)
                {
                    Dictionary<int, string> dicProveedores = new Dictionary<int, string>();

                    dicProveedores.Add(0, "Seleccionar proveedor...");

                    foreach (string proveedor in proveedores)
                    {
                        var info = proveedor.Split('-');
                        var id = Convert.ToInt32(info[0].Trim());
                        var nombre = info[1].Trim();

                        dicProveedores.Add(id, nombre);
                    }

                    cbProveedor.DataSource = dicProveedores.ToArray();
                    cbProveedor.ValueMember = "Key";
                    cbProveedor.DisplayMember = "Value";
                }
                else
                {
                    MessageBox.Show("No hay proveedores registrados.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cbOperadores_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var filtroDinamico = cbOperadores.SelectedValue.ToString();
            string filtroSeleccionado = string.Empty;

            string[] listaOperadores = { "NA", ">=", "<=", "=", ">", "<" };

            strFiltroDinamico.Clear();

            if (!listaOperadores.Contains(filtroDinamico))
            {
                filtroSeleccionado = filtroDinamico.Remove(0, 3);
                string inicioStr = "Selecciona " + filtroSeleccionado + "...";
                string sinFiltro = "Sin " + filtroSeleccionado;

                strFiltroDinamico.Add(inicioStr.ToUpper());
                strFiltroDinamico.Add(sinFiltro.ToUpper());

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

                if (cbFiltroDinamico.Visible.Equals(true))
                {
                    cbFiltroDinamico.DataSource = strFiltroDinamico.ToArray();
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

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void FiltroRevisarInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cbProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbTipoRevision.Visible = false;

            var idProveedor = cbProveedor.SelectedIndex;
            
            if (idProveedor > 0)
            {
                // Tipo de revision cuando es proveedores
                Dictionary<int, string> dicTipoRevision = new Dictionary<int, string>();
                dicTipoRevision.Add(1, "INICIAR REVISIÓN DESDE CERO");
                dicTipoRevision.Add(2, "CONTINUAR CON REVISIÓN");

                cbTipoRevision.DataSource = dicTipoRevision.ToArray();
                cbTipoRevision.DisplayMember = "Value";
                cbTipoRevision.ValueMember = "Key";

                cbTipoRevision.Visible = true;
                cbTipoRevision.SelectedIndex = 1; 
            }
        }
    }
}
