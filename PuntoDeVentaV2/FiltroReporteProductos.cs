using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class FiltroReporteProductos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        Dictionary<string, string> opcionesDefault;
        private Dictionary<string, Tuple<string, float>> filtros;
        private int origen = 0;

        // origen 1 = OpcionesReporteProducto.cs
        // origen 2 = Productos.cs
        public FiltroReporteProductos(int origen = 1)
        {
            InitializeComponent();

            this.origen = origen;

            if (origen == 1)
            {
                this.Text = "PUDVE - Filtro del Reporte";
            }

            if (origen == 2)
            {
                this.Text = "PUDVE - Filtro Avanzado de Productos";
            }
        }

        private void FiltroReporteProductos_Load(object sender, EventArgs e)
        {
            cbCantidadPedir.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbImagen.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbNumeroRevision.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbPrecio.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbPrecioCompra.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbProveedor.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbStock.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbStockMinimo.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbStockNecesario.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipo.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbDescuento.MouseWheel += new MouseEventHandler (Utilidades.ComboBox_Quitar_MouseWheel);
            
            if (!OpcionesReporteProducto.filtroAbierto || origen == 2)
            {
                opcionesDefault = new Dictionary<string, string>();
                filtros = new Dictionary<string, Tuple<string, float>>();

                // Datos para cargar el combobox de Stock y Precio
                var sourceOpciones = new Dictionary<string, string>();
                sourceOpciones.Add("NA", "No aplica");
                sourceOpciones.Add(">=", "Mayor o igual que");
                sourceOpciones.Add("<=", "Menor o igual que");
                sourceOpciones.Add("==", "Igual que");
                sourceOpciones.Add(">", "Mayor que");
                sourceOpciones.Add("<", "Menor que");

                cbStock.DataSource = sourceOpciones.ToArray();
                cbStock.ValueMember = "Key";
                cbStock.DisplayMember = "Value";

                cbStockMinimo.DataSource = sourceOpciones.ToArray();
                cbStockMinimo.ValueMember = "Key";
                cbStockMinimo.DisplayMember = "Value";

                cbStockNecesario.DataSource = sourceOpciones.ToArray();
                cbStockNecesario.ValueMember = "Key";
                cbStockNecesario.DisplayMember = "Value";

                cbPrecio.DataSource = sourceOpciones.ToArray();
                cbPrecio.ValueMember = "Key";
                cbPrecio.DisplayMember = "Value";

                cbPrecioCompra.DataSource = sourceOpciones.ToArray();
                cbPrecioCompra.ValueMember= "Key";
                cbPrecioCompra.DisplayMember= "Value";

                cbCantidadPedir.DataSource = sourceOpciones.ToArray();
                cbCantidadPedir.ValueMember = "Key";
                cbCantidadPedir.DisplayMember = "Value";

                cbNumeroRevision.DataSource = sourceOpciones.ToArray();
                cbNumeroRevision.ValueMember = "Key";
                cbNumeroRevision.DisplayMember = "Value";

                sourceOpciones = new Dictionary<string, string>();
                sourceOpciones.Add("NA", "No aplica");
                sourceOpciones.Add("P", "Productos");
                sourceOpciones.Add("S", "Servicios");
                sourceOpciones.Add("PQ", "Combos");

                cbTipo.DataSource = sourceOpciones.ToArray();
                cbTipo.ValueMember = "Key";
                cbTipo.DisplayMember = "Value";

                sourceOpciones = new Dictionary<string, string>();
                sourceOpciones.Add("NA", "No aplica");
                sourceOpciones.Add("1", "Con imagen");
                sourceOpciones.Add("0", "Sin imagen");

                cbImagen.DataSource = sourceOpciones.ToArray();
                cbImagen.ValueMember = "Key";
                cbImagen.DisplayMember = "Value";

                sourceOpciones = new Dictionary<string, string>();
                sourceOpciones.Add("NA", "No aplica");
                sourceOpciones.Add("1", "Con descuento");
                sourceOpciones.Add("0", "Sin descuento");

                cbDescuento.DataSource = sourceOpciones.ToArray();
                cbDescuento.ValueMember = "Key";
                cbDescuento.DisplayMember = "Value";

                // Cargar los proveedores para el combobox
                var proveedores = cn.ObtenerProveedores(FormPrincipal.userID);

                if (proveedores.Length > 0)
                {
                    var ultimoAux = string.Empty;

                    Dictionary<int, string> dicProveedores = new Dictionary<int, string>();

                    dicProveedores.Add(0, "Seleccionar proveedor...");
                    dicProveedores.Add(-1, "SIN PROVEEDOR");

                    foreach (string proveedor in proveedores)
                    {
                        var info = proveedor.Split('-');

                        dicProveedores.Add(Convert.ToInt32(info[0].Trim()), info[1].Trim());

                        ultimoAux = info[0].Trim();
                    }

                    cbProveedor.DataSource = dicProveedores.ToArray();
                    cbProveedor.ValueMember = "Key";
                    cbProveedor.DisplayMember = "Value";
                }
                else
                {
                    Dictionary<int, string> dicProveedores = new Dictionary<int, string>();

                    dicProveedores.Add(0, "No hay proveedores registrados");
                    cbProveedor.DataSource = dicProveedores.ToArray();
                    cbProveedor.ValueMember = "Key";
                    cbProveedor.DisplayMember = "Value";
                }

                // Obtiene los detalles guardados en App.config y los muestra en el form
                ObtenerDetalles();
                VisualizarDetalles();

                // Inicializamos el metodo "SoloDecimales"
                txtStock.KeyPress += new KeyPressEventHandler(SoloDecimales);
                txtStockMinimo.KeyPress += new KeyPressEventHandler(SoloDecimales);
                txtStockNecesario.KeyPress += new KeyPressEventHandler(SoloDecimales);
                txtPrecio.KeyPress += new KeyPressEventHandler(SoloDecimales);
                txtCantidadPedir.KeyPress += new KeyPressEventHandler(SoloDecimales);

                // Inicializamos los checkbox estaticos por defecto
                checkStock.CheckedChanged += checkEstaticos_CheckedChanged;
                checkStockMinimo.CheckedChanged += checkEstaticos_CheckedChanged;
                checkStockNecesario.CheckedChanged += checkEstaticos_CheckedChanged;
                checkPrecio.CheckedChanged += checkEstaticos_CheckedChanged;
                checkPrecioCompra.CheckedChanged+= checkEstaticos_CheckedChanged;
                checkCantidadPedir.CheckedChanged += checkEstaticos_CheckedChanged;
                checkNumeroRevision.CheckedChanged += checkEstaticos_CheckedChanged;
            }
            
            if (origen != 2)
            {
                OpcionesReporteProducto.filtroAbierto = true;
            }
            
        }

        private void ObtenerDetalles()
        {
            var conceptos = mb.ConceptosAppSettings();

            if (conceptos.Count > 0)
            {
                foreach (var concepto in conceptos)
                {
                    if (concepto == "Proveedor")
                    {
                        continue;
                    }

                    opcionesDefault.Add(concepto, concepto);
                }
            }
        }

        private void VisualizarDetalles()
        {
            if (opcionesDefault.Count > 0)
            {
                Font fuente = new Font("Century Gothic", 9.0f);

                int alturaEjeY = 460;

                foreach (var opcion in opcionesDefault)
                {
                    CheckBox checkCustom = new CheckBox();
                    checkCustom.Name = "check" + opcion.Key;
                    checkCustom.Text = opcion.Key;
                    checkCustom.Tag = "check" + opcion.Key;
                    checkCustom.AutoSize = true;
                    checkCustom.Font = fuente;
                    checkCustom.Location = new Point(10, alturaEjeY);
                    checkCustom.CheckAlign = ContentAlignment.MiddleLeft;
                    checkCustom.CheckedChanged += checkCustom_CheckedChanged;

                    var valorAux = opcion.Key.Replace(' ', '_');

                    // Consultar los datos para cada detalle de producto agregado
                    var datosPropiedad = mb.ObtenerOpcionesPropiedad(FormPrincipal.userID, valorAux);
                    var sourceCBCustom = new Dictionary<string, string>();

                    if (datosPropiedad.Count > 0)
                    {
                        sourceCBCustom.Add("NA", $"Seleccione {opcion.Key}");
                        sourceCBCustom.Add("SIN", $"SIN {opcion.Key}");

                        foreach (var valor in datosPropiedad)
                        {
                            if (!sourceCBCustom.ContainsKey(valor.Value))
                            {
                                sourceCBCustom.Add(valor.Value, valor.Value);
                            }
                        }
                    }
                    else
                    {
                        sourceCBCustom.Add("NA", $"No existe {valorAux.ToLower()} registrado");
                    }

                    ComboBox cbCustom = new ComboBox();
                    cbCustom.Name = "cb" + opcion.Key;
                    cbCustom.Font = fuente;
                    cbCustom.Location = new Point(138, alturaEjeY);
                    cbCustom.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbCustom.Enabled = false;
                    cbCustom.Width = 336;
                    cbCustom.DataSource = sourceCBCustom.ToArray();
                    cbCustom.DisplayMember = "Value";
                    cbCustom.ValueMember = "Key";
                    //cbCustom.SelectionChangeCommitted += cbCustom_SelectionChangeCommitted;

                    panelContenedor.Controls.Add(checkCustom);
                    panelContenedor.Controls.Add(cbCustom);

                    alturaEjeY += 40;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (filtros.Count > 0)
            {
                filtros = new Dictionary<string, Tuple<string, float>>();
            }

            foreach (var item in panelContenedor.Controls)
            {
                if (item is CheckBox)
                {
                    var checkCustom = (CheckBox)item;

                    if (checkCustom.Checked)
                    {
                        var nombreCB = checkCustom.Name.Replace("check", "");

                        var comboCustom = (ComboBox)Controls.Find("cb" + nombreCB, true).FirstOrDefault();

                        var opcion = comboCustom.SelectedValue.ToString();

                        if (opcion != "NA")
                        {
                            if (nombreCB == "Stock" || nombreCB == "StockMinimo" || nombreCB == "StockNecesario")
                            {
                                var txtCustom = (TextBox)Controls.Find("txt" + nombreCB, true).FirstOrDefault();

                                if (string.IsNullOrWhiteSpace(txtCustom.Text))
                                {
                                    MessageBox.Show($"Es necesario agregar cantidad para el campo {checkCustom.Text}", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (txtCustom.Text.Equals("."))
                                {
                                    txtCustom.Text = "0.0";
                                }
                                var cantidad = float.Parse(txtCustom.Text);

                                filtros.Add(nombreCB, new Tuple<string, float>(opcion, cantidad));
                            }
                            else if (nombreCB.Equals("Precio") || nombreCB.Equals("PrecioCompra") || nombreCB.Equals("CantidadPedir") || nombreCB.Equals("NumeroRevision"))
                            {
                                var txtCustom = (TextBox)Controls.Find("txt" + nombreCB, true).FirstOrDefault();

                                if (string.IsNullOrWhiteSpace(txtCustom.Text))
                                {
                                    MessageBox.Show($"Es necesario agregar cantidad para el campo {checkCustom.Text}", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                var cantidad = float.Parse(txtCustom.Text);

                                filtros.Add(nombreCB, new Tuple<string, float>(opcion, cantidad));
                            }
                            else if (nombreCB == "Proveedor" || nombreCB == "Tipo" || nombreCB == "Imagen" || nombreCB == "Descuento")
                            {
                                filtros.Add(nombreCB, new Tuple<string, float>(opcion, 0));
                            }
                            else
                            {
                                filtros.Add(nombreCB, new Tuple<string, float>(opcion, 0));
                            }
                        }
                    }
                }
            }

            if (filtros.Count > 0)
            {
                if (origen == 1)
                {
                    OpcionesReporteProducto.filtros = filtros;

                    Hide();
                }

                if (origen == 2)
                {
                    // Actualizar tablas de filtros para las etiquetas
                    string[] conceptosEstaticos = new string[] { "Stock", "StockMinimo", "StockNecesario", "Precio", "PrecioCompra", "NumeroRevision", "CantidadPedir", "Imagen", "Tipo", "Descuento" };

                    foreach (var filtro in filtros)
                    {
                        var consulta = string.Empty;

                        if (conceptosEstaticos.Contains(filtro.Key))
                        {
                            var concepto = filtro.Key.Equals("NumeroRevision") ? "Revision" : filtro.Key;
                            var operador = filtro.Value.Item1.Equals("==") ? "=" : filtro.Value.Item1;
                            var nombreConcepto = $"{filtro.Key} {operador} ";

                            if (filtro.Key.Equals("Imagen"))
                            {
                                var auxiliar = string.Empty;

                                if (filtro.Value.Item1.Equals("1"))
                                {
                                    auxiliar = "<>";
                                }
                                else
                                {
                                    auxiliar = "=";
                                }

                                nombreConcepto = $"ProdImage {auxiliar} \\'\\' ";
                            }

                            if (filtro.Key.Equals("Tipo"))
                            {
                                nombreConcepto = $"{filtro.Key} = {filtro.Value.Item1} ";
                            }

                            
                            var nombreCheckbox = $"chkBox{concepto}";

                            var existenConceptos = mb.ObtenerDatosFiltro(nombreCheckbox, FormPrincipal.userID, FormPrincipal.userNickName);

                            if (existenConceptos.Count() > 0)
                            {
                                consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = 1, textComboBoxConcepto = '{nombreConcepto}', textCantidad = '{filtro.Value.Item2}' WHERE concepto = '{nombreCheckbox}' AND IDUsuario = {FormPrincipal.userID} AND Username = '{FormPrincipal.userNickName}'";
                                cn.EjecutarConsulta(consulta);
                            }
                            else
                            {
                                consulta = "INSERT INTO FiltroProducto (concepto, checkBoxConcepto, textComboBoxConcepto, textCantidad, IDUsuario, Username) VALUES";
                                consulta += $"('{nombreCheckbox}', 1, '{nombreConcepto}', '{filtro.Value.Item2}', '{FormPrincipal.userID}', '{FormPrincipal.userNickName}')";
                                cn.EjecutarConsulta(consulta);
                            }
                        }
                        else
                        {
                            var valorFiltro = filtro.Value.Item1;

                            if (filtro.Key.Equals("Proveedor"))
                            {
                                var proveedorAux = mb.ObtenerDatosProveedor(Convert.ToInt32(valorFiltro), FormPrincipal.userID);
                                
                                if (proveedorAux.Count() > 0)
                                {
                                    valorFiltro = proveedorAux[0];
                                }
                            }

                            var existenConceptos = (bool)cn.EjecutarSelect(cs.BuscarDatoEnVentanaFiltros(filtro.Key, FormPrincipal.userID, FormPrincipal.userNickName));
                            
                            if (existenConceptos)
                            {
                                cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros("1", filtro.Key, valorFiltro, FormPrincipal.userID, FormPrincipal.userNickName));
                            }
                            else
                            {
                                cn.EjecutarConsulta(cs.GuardarVentanaFiltros("1", filtro.Key, valorFiltro, FormPrincipal.userID, FormPrincipal.userNickName));
                            }
                        }
                    }

                    Productos.filtros = filtros;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Es necesario seleccionar al menos un filtro", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void checkEstaticos_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkCustom = (CheckBox)sender;

            var auxiliar = checkCustom.Name.Substring(5);

            ComboBox cbCustom = (ComboBox)Controls.Find("cb" + auxiliar, true).FirstOrDefault();
            TextBox tbCustom = (TextBox)Controls.Find("txt" + auxiliar, true).FirstOrDefault();

            if (checkCustom.Checked)
            {
                cbCustom.Enabled = true;
                tbCustom.Enabled = true;
            }
            else
            {
                cbCustom.SelectedValue = "NA";
                cbCustom.Enabled = false;
                tbCustom.Enabled = false;
                tbCustom.Text = "0";
            }
        }

        private void checkProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProveedor.Checked)
            {
                cbProveedor.Enabled = true;
            }
            else
            {
                cbProveedor.SelectedValue = 0;
                cbProveedor.Enabled = false;
            }
        }

        private void checkTipo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTipo.Checked)
            {
                cbTipo.Enabled = true;
            }
            else
            {
                cbTipo.SelectedValue = "NA";
                cbTipo.Enabled = false;
            }
        }

        private void checkImagen_CheckedChanged(object sender, EventArgs e)
        {
            if (checkImagen.Checked)
            {
                cbImagen.Enabled = true;
            }
            else
            {
                cbImagen.SelectedValue = "NA";
                cbImagen.Enabled = false;
            }
        }

        private void checkCustom_CheckedChanged(object sender, EventArgs e)
        {
            var checkCustom = (CheckBox)sender;
            var nombreCB = checkCustom.Name;

            nombreCB = nombreCB.Replace("check", "");

            var comboBoxCustom = (ComboBox)Controls.Find("cb" + nombreCB, true).FirstOrDefault();

            if (checkCustom.Checked)
            {
                comboBoxCustom.Enabled = true;
            }
            else
            {
                comboBoxCustom.SelectedValue = "NA";
                comboBoxCustom.Enabled = false;
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

        private void FiltroReporteProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void checkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDescuento.Checked)
            {
                cbDescuento.Enabled = true;
            }
            else
            {
                cbDescuento.SelectedValue = "NA";
                cbDescuento.Enabled = false;
            }
        }
    }
}
