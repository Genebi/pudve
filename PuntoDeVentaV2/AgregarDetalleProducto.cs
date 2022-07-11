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
using System.IO;
using Microsoft.Win32;

using System.Configuration;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;

namespace PuntoDeVentaV2
{
    public partial class AgregarDetalleProducto : Form
    {
        public static string nameProveedor { get; set; }
        public static string rfc { get; set; }

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private bool eventoMensajeInventario = false;

        List<string> datosAppSettings;

        string[] datosAppSettingToDB;

        public string nameXMLProveedor = string.Empty;

        #region Variables Globales

        List<string> infoDetalle,
                        infoDetailProdGral;

        Dictionary<string, string> proveedores,
                                    categorias,
                                    ubicaciones,
                                    detallesGral;

        Dictionary<int, Tuple<string, string, string, string>> diccionarioDetallesGeneral = new Dictionary<int, Tuple<string, string, string, string>>(),
                                                               diccionarioDetalleBasicos = new Dictionary<int, Tuple<string, string, string, string>>();

        DataTable dtProdMessg;
        DataRow drProdMessg;

        string[] datosProveedor,
                    datosCategoria,
                    datosUbicacion,
                    datosDetalleGral,
                    separadas,
                    guardar;

        string[] listaProveedores = new string[] { },
                    listaCategorias = new string[] { },
                    listaUbicaciones = new string[] { },
                    listaDetalleGral = new string[] { };

        int XPos = 0,
            YPos = 0,
            contadorIndex = 0,
            idProveedor = 0,
            idCategoria = 0,
            idUbicacion = 0,
            idProductoDetalleGral;

        string nvoDetalle = string.Empty,
                nvoValor = string.Empty,
                editValor = string.Empty,
                deleteDetalle = string.Empty,
                nombreProveedor = string.Empty,
                nombreCategoria = string.Empty,
                nombreUbicacion = string.Empty,
                mensajeDetalleProducto = string.Empty;

        public string getIdProducto { get; set; }
        public int origenProdServCombo { get; set; }

        public static string finalIdProducto = string.Empty;
        public static int finalOrigenProdServCombo;

        string editDetelle = string.Empty,
                editDetalleNvo = string.Empty;

        #endregion Variables Globales

        #region Modifying Configuration Settings at Runtime

        XmlDocument xmlDoc = new XmlDocument();
        XmlNode appSettingsNode, newChild;
        ListView chkDatabase = new ListView();  // ListView para los CheckBox de solo detalle
        ListView settingDatabases = new ListView(); // ListView para los CheckBox de Sistema
        ListViewItem lvi;
        string connStr, keyName;
        int found = 0;
        NameValueCollection appSettings;

        string mensajeMostrar = string.Empty,
                tituloVentana = string.Empty,
                mensajeDefault = string.Empty,
                conceptoProductoAgregar = string.Empty,
                conceptoProductoEliminar = string.Empty;

        // this code will add a listviewtem
        // to a listview for each database entry
        // in the appSettings section of an App.config file.
        private void loadFormConfig()
        {
            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            chkDatabase.Items.Clear();
            settingDatabases.Items.Clear();

            lvi = new ListViewItem();

            try
            {
                chkDatabase.Clear();
                settingDatabases.Clear();

                appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    MessageBox.Show("Lectura App.Config/AppSettings: La Sección de AppSettings está vacia", "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (appSettings.Count > 0)
                {
                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        connStr = appSettings[i];
                        keyName = appSettings.GetKey(i);
                        found = keyName.IndexOf("chk", 0, 3);
                        if (found >= 0)
                        {
                            lvi = new ListViewItem(keyName);
                            lvi.SubItems.Add(connStr);
                            chkDatabase.Items.Add(lvi);
                        }
                    }

                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        string foundSetting = string.Empty;
                        connStr = appSettings[i];
                        keyName = appSettings.GetKey(i);
                        found = keyName.IndexOf("chk", 0, 3);
                        if (found <= -1)
                        {
                            lvi = new ListViewItem(keyName);
                            lvi.SubItems.Add(connStr);
                            settingDatabases.Items.Add(lvi);
                        }
                    }
                }
            }
            catch (ConfigurationException e)
            {
                MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Force a reload of the changed section. This 
        // makes the new values available for reading.
        public static void RefreshAppSettings()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Determines if a key exist within the App.config
        public bool KeyExist(string strKey)
        {
            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
            if (appSettingsNode != null)
            {
                // Attempt to locate the requested setting.
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Adds a key and value to the App.config
        public void AddKey(string strKey, string strValue)
        {
            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
            try
            {
                if (KeyExist(strKey))
                {
                    MessageBox.Show("Nombre clave: <" + strKey + "> ya existe en la configuración.", "Setting Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    newChild = appSettingsNode.FirstChild.Clone();
                    newChild.Attributes["key"].Value = strKey;
                    newChild.Attributes["value"].Value = strValue.ToLower();
                    appSettingsNode.AppendChild(newChild);
                    // We have to save the configuration in tow places,
                    // because while we have a root App.config,
                    // we also have an ApplicationName.exe.config.
                    if (Properties.Settings.Default.TipoEjecucion == 1)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }

                    if (Properties.Settings.Default.TipoEjecucion == 2)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                    ReadKey(strKey);

                    newChild = appSettingsNode.FirstChild.Clone();
                    newChild.Attributes["key"].Value = "chk" + strKey;
                    newChild.Attributes["value"].Value = strValue.ToLower();
                    appSettingsNode.AppendChild(newChild);
                    if (Properties.Settings.Default.TipoEjecucion == 1)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }

                    if (Properties.Settings.Default.TipoEjecucion == 2)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tipo de error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Updates a key within the App.config
        private void UpdateKey(string strKey, string newValue)
        {
            // Verificamos si existe esa configuracion
            if (!KeyExist(strKey))
            {
                MessageBox.Show("Nombre clave <" + strKey + "> no existe en la configuración. Actualización fallida.", "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // si es que si existe
            {
                appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                // Attempt to locate the requested settings.
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        childNode.Attributes["value"].Value = newValue.ToLower();
                        if (!strKey.Equals(""))
                        {
                            childNode.Attributes["key"].Value = strKey;
                        }
                        break;
                    }
                }

                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == "chk" + strKey)
                    {
                        ReadKey("chk" + strKey);
                        childNode.Attributes["value"].Value = editValor.ToLower();
                        if (!strKey.Equals(""))
                        {
                            childNode.Attributes["key"].Value = "chk" + strKey;
                        }
                        break;
                    }
                }

                //string path = string.Empty;
                try
                {
                    if (Properties.Settings.Default.TipoEjecucion == 1)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }

                    if (Properties.Settings.Default.TipoEjecucion == 2)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al Intentar actualizar el archivo de configuración: " + e.Message.ToString(), "Error de archivo de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RenameKey(string strKey, string newValue)
        {
            // Verificamos si existe esa configuracion
            if (!KeyExist(strKey))
            {
                MessageBox.Show("Nombre clave <" + strKey + "> no existe en la configuración. Actualización fallida.", "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // si es que si existe
            {
                appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                // Attempt to locate the requested settings.
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        childNode.Attributes["value"].Value = newValue.ToLower();
                        if (!editDetalleNvo.Equals(""))
                        {
                            childNode.Attributes["key"].Value = editDetalleNvo;
                            cn.EjecutarConsulta(cs.UpdateDetalleGeneral(editDetelle, editDetalleNvo));
                            cn.EjecutarConsulta(cs.UpdateDetallesProductoGenerales("panelContenido" + editDetelle, "panelContenido" + editDetalleNvo));
                        }
                        if (!editDetalleNvo.Equals("") && strKey.Equals(""))
                        {
                            childNode.Attributes["key"].Value = strKey;
                        }
                        break;
                    }
                }

                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == "chk" + strKey)
                    {
                        ReadKey("chk" + strKey);
                        childNode.Attributes["value"].Value = editValor.ToLower();
                        if (!editDetalleNvo.Equals(""))
                        {
                            childNode.Attributes["key"].Value = "chk" + editDetalleNvo;
                            cn.EjecutarConsulta(cs.UpdateDetalleGeneral("chk" + editDetelle, "chk" + editDetalleNvo));
                            cn.EjecutarConsulta(cs.UpdateDetallesProductoGenerales("panelContenido" + editDetelle, "panelContenido" + editDetalleNvo));
                        }
                        if (!editDetalleNvo.Equals("") && strKey.Equals(""))
                        {
                            childNode.Attributes["key"].Value = "chk" + strKey;
                        }
                        break;
                    }
                }

                //string path = string.Empty;
                try
                {
                    if (Properties.Settings.Default.TipoEjecucion == 1)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }

                    if (Properties.Settings.Default.TipoEjecucion == 2)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al Intentar actualizar el archivo de configuración: " + e.Message.ToString(), "Error de archivo de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Read a key within the App.config
        public void ReadKey(string strKey)
        {
            if (!KeyExist(strKey))
            {
                MessageBox.Show("Nombre clave <" + strKey + "> no existe en la configuración. Busqueda fallida.", "Error al leer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                // Attempt to locate the requested settings.
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        editValor = childNode.Attributes["value"].Value.ToLower().ToString();
                        break;
                    }
                }
            }
        }

        // Deletes a key from the App.config
        public void DeleteKey(string strKey)
        {
            string chkSettingUsr = "chk" + strKey;

            if (!KeyExist(strKey))
            {
                MessageBox.Show("Nombre clave < " + strKey + " > no existe en la configuración.Imposible Borrar.", "Error de archivo al Borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");
                // Attempt to locate the requested setting.
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == strKey)
                    {
                        appSettingsNode.RemoveChild(childNode);
                        break;
                    }
                }
                foreach (XmlNode childNode in appSettingsNode)
                {
                    if (childNode.Attributes["key"].Value == chkSettingUsr)
                    {
                        appSettingsNode.RemoveChild(childNode);
                        break;
                    }
                }

                try
                {
                    if (Properties.Settings.Default.TipoEjecucion == 1)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }

                    if (Properties.Settings.Default.TipoEjecucion == 2)
                    {
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al Intentar Borrar el Registro de configuración: " + e.Message.ToString(), "Error de archivo al Borrar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                loadFormConfig();
                MessageBox.Show("Nombre clave <" + strKey + "> borrada en la configuración(Setting).", "Borrado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BuscarTextoListView(ListView lstListView)
        {
            int id = 0, row = 0;
            string nameChk = string.Empty,
                   valorChk = string.Empty,
                   chkDetalleProductoTxt = string.Empty,
                   chkDetalleProductoVal = string.Empty,
                   chkSettingVariableTxt = string.Empty,
                   chkSettingVariableVal = string.Empty;

            fLPLateralConcepto.Controls.Clear();

            for (int i = 0; i < lstListView.Items.Count; i++)
            {
                //chkDetalleProductoTxt = lstListView.Items[i].Text.ToString();
                //chkDetalleProductoVal = lstListView.Items[i].SubItems[1].Text.ToString();
                chkDetalleProductoVal = lstListView.Items[i].Text.ToString();
                chkDetalleProductoTxt = lstListView.Items[i].SubItems[1].Text.ToString();

                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Width = 245;
                panelHijo.Height = 29;
                panelHijo.HorizontalScroll.Visible = false;
                //panelHijo.BackColor = Color.LightPink;

                Panel panelContenedor = new Panel();
                panelContenedor.Width = 238;
                panelContenedor.Height = 23;
                panelContenedor.Name = "panel" + chkDetalleProductoTxt;
                //panelContenedor.BackColor = Color.LightGray;

                CheckBox check = new CheckBox();
                check.Name = chkDetalleProductoTxt;
                check.Text = chkDetalleProductoTxt.Replace("_", " ");
                check.Width = 155;
                check.Height = 24;
                check.Location = new Point(0, 0);
                this.toolTip1.SetToolTip(check, "Activar para seleccionar detalles de producto");
                //check.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
                check.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
                //check.BackColor = Color.LightBlue;

                if (chkDetalleProductoVal.Equals("true") || chkDetalleProductoVal.Equals("false"))
                {
                    check.Checked = Convert.ToBoolean(chkDetalleProductoVal);
                    panelContenedor.Controls.Add(check);
                    panelHijo.Controls.Add(panelContenedor);

                    // Agregamos el Botón de agregar item Más
                    Button bt = new Button();
                    bt.Name = "bt" + chkDetalleProductoTxt.Replace("_", " ");
                    bt.Tag = chkDetalleProductoTxt;
                    bt.Cursor = Cursors.Hand;
                    //bt.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
                    bt.Image = global::PuntoDeVentaV2.Properties.Resources.edit;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#5DADE2");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(bt_Click);
                    bt.Location = new Point(162, 0);
                    this.toolTip1.SetToolTip(bt, "Agregar o editar especificaciones");
                    panelContenedor.Controls.Add(bt);
                    panelHijo.Controls.Add(panelContenedor);

                    if (row < chkDatabase.Items.Count)
                    {
                        //chkSettingVariableTxt = chkDatabase.Items[row].Text.ToString();
                        //chkSettingVariableVal = chkDatabase.Items[row].SubItems[1].Text.ToString();
                        chkSettingVariableVal = chkDatabase.Items[row].Text.ToString();
                        chkSettingVariableTxt = chkDatabase.Items[row].SubItems[1].Text.ToString();

                        CheckBox checkSetting = new CheckBox();
                        checkSetting.Name = chkSettingVariableTxt;
                        checkSetting.Width = 20;
                        checkSetting.Height = 24;
                        checkSetting.Location = new Point(215, 0);
                        checkSetting.CheckedChanged += new EventHandler(checkBoxSetting_CheckedChanged);
                        this.toolTip1.SetToolTip(checkSetting, "Activar casilla para, mostrar en ventana (Afuera) de productos");
                        //checkSetting.BackColor = Color.LightCyan;

                        if (chkSettingVariableVal.Equals("true") || chkSettingVariableVal.Equals("false"))
                        {
                            checkSetting.Checked = Convert.ToBoolean(chkSettingVariableVal);
                            panelContenedor.Controls.Add(checkSetting);
                            panelHijo.Controls.Add(panelContenedor);
                        }
                        row++;
                    }
                }
                fLPLateralConcepto.Controls.Add(panelHijo);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            #region Manejo del CheckBox segun sea al que cambiemos estado
            /********************************************************
            *   Al Control de CheckBox que cambiemos el estado      *
            *   sea True ó False este le hacemos un Casting         *
            *   para poder manejarlo                                *
            ********************************************************/
            CheckBox chekBoxClickDetalle = sender as CheckBox;
            #endregion Manejo del CheckBox segun sea al que cambiemos estado

            #region Declaracion de variables
            /************************************************************
            *   Declaracion de variables que usaremos para el tratado   *
            *   de los procesos de actalizacion o de insercion del      *
            *   estado del CheckBox                                     *
            ************************************************************/
            string name = string.Empty,
                    value = string.Empty,
                    nombrePanelContenedor = string.Empty,
                    nombrePanelContenido = string.Empty;
            int valorDatoDinamico = -1;
            var servidor = Properties.Settings.Default.Hosting;
            #endregion  Declaracion de variables

            #region Inicializacion de variables
            if (chekBoxClickDetalle.Checked == true)
            {
                value = chekBoxClickDetalle.Checked.ToString();
            }
            else if (chekBoxClickDetalle.Checked == false)
            {
                value = chekBoxClickDetalle.Checked.ToString();
            }

            if (value.Equals("True"))
            {
                valorDatoDinamico = 1;
            }
            else if (value.Equals("False"))
            {
                valorDatoDinamico = 0;
            }

            name = chekBoxClickDetalle.Name.ToString();
            nombrePanelContenedor = "panelContenedor" + name;
            nombrePanelContenido = "panelContenido" + name;
            #endregion Inicializacion de variables

            #region Inicializacion de Panel's Dinamicos
            FlowLayoutPanel panelContenedor = new FlowLayoutPanel();
            panelContenedor.Name = nombrePanelContenedor;

            Panel panelContenido = new Panel();
            panelContenido.Name = nombrePanelContenido;
            #endregion Inicializacion de Panel's Dinamicos

            #region Verificacion del Estado de CheckBox
            #region Si el CheckBox esta en true
            if (chekBoxClickDetalle.Checked == true)
            {
                #region Si el Nombre del Panel es igual a panelContenedorProveedor
                if (panelContenedor.Name == "panelContenedorProveedor")
                {
                    // Damos caracteristicas Panel Contenedor
                    panelContenedor.Width = 600;
                    panelContenedor.Height = 63;
                    panelContenedor.BackColor = Color.LightGray;

                    // Damos caracteristicas Panel Contenido
                    panelContenido.Width = 594;
                    panelContenido.Height = 55;

                    // Damos Caracteristicas al Label Nombre Proveedor
                    Label lblNombreProveedor = new Label();
                    lblNombreProveedor.Name = "lblNombre" + name;
                    lblNombreProveedor.Width = 320;
                    lblNombreProveedor.Height = 20;
                    lblNombreProveedor.Location = new Point(3, 32);
                    lblNombreProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombreProveedor.BackColor = Color.White;

                    // Damos Caracteristicas al Label RFC Proveedor
                    Label lblRFCProveedor = new Label();
                    lblRFCProveedor.Name = "lblRFC" + name;
                    lblRFCProveedor.Width = 110;
                    lblRFCProveedor.Height = 20;
                    lblRFCProveedor.Location = new Point(360, 32);
                    lblRFCProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblRFCProveedor.BackColor = Color.White;

                    // Damos Caracteristicas al Label Teléfono Proveedor
                    Label lblTelProveedor = new Label();
                    lblTelProveedor.Name = "lblTel" + name;
                    lblTelProveedor.Width = 90;
                    lblTelProveedor.Height = 20;
                    lblTelProveedor.Location = new Point(500, 32);
                    lblTelProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblTelProveedor.BackColor = Color.White;

                    // Variables para centrar el Panel 
                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    // Metodo para cargar todos los proveedores registrados y activos
                    CargarProveedores();

                    //  Damos Caracteristicas al ComboBox Proveedor
                    ComboBox cbProveedor = new ComboBox();
                    cbProveedor.Name = "cb" + name;
                    cbProveedor.Width = 580;
                    cbProveedor.Height = 30;
                    cbProveedor.Location = new Point(XcbProv - (cbProveedor.Width / 2), 5);
                    cbProveedor.SelectedIndexChanged += new System.EventHandler(cbProveedor_SelectValueChanged);
                    cbProveedor.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);

                    // Verificamos que si la Lista Proveedor tiene registros
                    if (listaProveedores.Length > 0)
                    {
                        // Recargamos la Lista del ComboBox con la de Proveedores
                        cbProveedor.DataSource = proveedores.ToArray();
                        cbProveedor.DisplayMember = "Value";
                        cbProveedor.ValueMember = "Key";
                        cbProveedor.SelectedValue = "0";

                        // Cuando se da click en la opcion editar producto
                        if (AgregarEditarProducto.DatosSourceFinal == 2 || AgregarEditarProducto.DatosSourceFinal == 4)
                        {
                            // Obtenemos el idProducto
                            var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                            // Obtenemos la Lista del Detalles de Producto
                            var idProveedor = mb.DetallesProducto(idProducto, FormPrincipal.userID);
                            // Obtenemos la cantidad de Detalles obtenidos
                            int cantidad = idProveedor.Length;

                            // si cantidad es mayor que 0 "Si tiene registros Detalles de Producto"
                            if (cantidad > 0)
                            {
                                // Si el ID DetallesProducto tiene algun registro
                                if (!idProveedor[1].Equals(""))
                                {
                                    // Convertimos el ID DetallesProducto y compramos si es mayor que cero
                                    if (Convert.ToInt32(idProveedor[1].ToString()) > 0)
                                    {
                                        // Llamos metodo de Cargar Datos Proveedor
                                        cargarDatosProveedor(Convert.ToInt32(idProveedor[1]));
                                        if (!datosProveedor.Equals(null))
                                        {
                                            // llenamos los Label de Nombre, RFC y Teléfono del Proveedor
                                            lblNombreProveedor.Text = datosProveedor[0];
                                            lblRFCProveedor.Text = datosProveedor[1];
                                            lblTelProveedor.Text = datosProveedor[10];

                                            try
                                            {
                                                // Hacemos el intento de hacer la actualización del Proveedor
                                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                // Muestra un error al no poder hacer la actualizacion del Proveedor
                                                MessageBox.Show("Error al actualizar Valor del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }

                                            try
                                            {
                                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoFiltroDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al actualizar Valor del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }

                                            /*if (servidor.Equals(""))
                                            {
                                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                                            }
                                            else if (!servidor.Equals(""))
                                            {
                                                diccionarioDetalleBasicos.Add(contadorIndex, new Tuple<string, string, string, string>(idProveedor[0].ToString(), nombrePanelContenido, idProveedor[0].ToString(), datosProveedor[0].ToString()));
                                                contadorIndex++;
                                            }*/
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (listaProveedores.Length < 0)
                    {
                        // Si es que no tiene lista de Proveedores se le agrega
                        // Proveedores...  al inicio y se muestra en la interfaz
                        cbProveedor.Items.Add("Proveedores...");
                        cbProveedor.SelectedIndex = 0;
                        if (AgregarEditarProducto.DatosSourceFinal.Equals(1) || AgregarEditarProducto.DatosSourceFinal.Equals(2))
                        {
                            try
                            {
                                // Hacemos el intento de hacer la actualización del Proveedor
                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                            }
                            catch (Exception ex)
                            {
                                // Muestra un error al no poder hacer la actualizacion del Proveedor
                                MessageBox.Show("Error al actualizar Valor del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            try
                            {
                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoFiltroDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al actualizar Valor del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (cbProveedor.Items.Count == 0)
                    {
                        // Si es que no tiene lista de Proveedores se le agrega
                        // Proveedores...  al inicio y se muestra en la interfaz
                        cbProveedor.Items.Add("Proveedores...");
                        cbProveedor.SelectedIndex = 0;
                        if (AgregarEditarProducto.DatosSourceFinal.Equals(1) || AgregarEditarProducto.DatosSourceFinal.Equals(2))
                        {
                            try
                            {
                                // Hacemos el intento de hacer la actualización del Proveedor
                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                            }
                            catch (Exception ex)
                            {
                                // Muestra un error al no poder hacer la actualizacion del Proveedor
                                MessageBox.Show("Error al actualizar Valor del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            try
                            {
                                var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoFiltroDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al actualizar Valor del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    // hacemos que el ComboBox no sea Editable solo sea de Lectura
                    cbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;

                    // Agregamos el panel al principal de la interfaz
                    panelContenido.Controls.Add(cbProveedor);
                    panelContenido.Controls.Add(lblNombreProveedor);
                    panelContenido.Controls.Add(lblRFCProveedor);
                    panelContenido.Controls.Add(lblTelProveedor);

                    panelContenedor.Controls.Add(panelContenido);
                    fLPCentralDetalle.Controls.Add(panelContenedor);
                }
                #endregion Si el Nombre del Panel es igual a panelContenedorProveedor
                #region Si el Nombre del Panel es diferente a panelContenedorProveedor
                else
                {
                    // Obtenemos como sera el nombre del panel
                    nombrePanelContenido = "panelContenido" + name;

                    // Damos Caracteristicas al panel Contenedor
                    panelContenedor.Width = 600;
                    panelContenedor.Height = 63;
                    panelContenedor.BackColor = Color.LightGray;

                    // Damos Caracteristicas al panel Contenido
                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 594;
                    panelContenido.Height = 55;

                    // Variables para el centrado del panel
                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    // Iniciamos la Etiqueta Nombre
                    Label lblNombreDetalleGral = new Label();
                    lblNombreDetalleGral.Name = "lblNombre" + name;
                    lblNombreDetalleGral.Width = 580;
                    lblNombreDetalleGral.Height = 20;
                    lblNombreDetalleGral.Location = new Point(XcbProv - (lblNombreDetalleGral.Width / 2), 32);
                    lblNombreDetalleGral.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombreDetalleGral.BackColor = Color.White;

                    // Llamamos al metodo Cargar Detalles Generales
                    // Para obtener una lista de ellos
                    CargarDetallesGral(name);

                    // Iniciamos el ComboBox para cargar los Detalles Generales
                    ComboBox cbDetalleGral = new ComboBox();
                    cbDetalleGral.Name = "cb" + name;
                    cbDetalleGral.Width = 580;
                    cbDetalleGral.Height = 30;
                    cbDetalleGral.Location = new Point(XcbProv - (cbDetalleGral.Width / 2), 5);
                    cbDetalleGral.SelectedIndexChanged += new System.EventHandler(cbDetalleGral_SelectIndexChanged);
                    cbDetalleGral.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbDetalleGral.MouseWheel += new MouseEventHandler(ComboBox_Quitar_MouseWheel);

                    // Verificamos si la Lista de Detalles Generales tiene algun registro
                    if (listaDetalleGral.Length > 0)
                    {
                        cbDetalleGral.DataSource = detallesGral.ToArray();
                        cbDetalleGral.DisplayMember = "Value";
                        cbDetalleGral.ValueMember = "Key";
                        cbDetalleGral.SelectedValue = "0";
                    }
                    // Verificamos si la Lista de Detalles Generales no tiene algun registro
                    else if (cbDetalleGral.Items.Count == 0)
                    {
                        cbDetalleGral.Items.Add(chekBoxClickDetalle.Name.ToString().Replace("_", " ") + "...");
                        cbDetalleGral.SelectedIndex = 0;
                    }

                    // Agregamos al Panel Principal de la Interfaz
                    panelContenido.Controls.Add(cbDetalleGral);
                    panelContenido.Controls.Add(lblNombreDetalleGral);
                    panelContenedor.Controls.Add(panelContenido);
                    fLPCentralDetalle.Controls.Add(panelContenedor);

                    // Cuando se da click en la opcion editar producto
                    if (AgregarEditarProducto.DatosSourceFinal == 2 || AgregarEditarProducto.DatosSourceFinal == 4)
                    {
                        string Descripcion = string.Empty;

                        foreach (Control contHijo in fLPCentralDetalle.Controls)
                        {
                            foreach (Control contSubHijo in contHijo.Controls)
                            {
                                if (contSubHijo.Name == nombrePanelContenido)
                                {
                                    foreach (Control contItemSubHijo in contSubHijo.Controls)
                                    {
                                        if (contItemSubHijo is Label)
                                        {
                                            Descripcion = contItemSubHijo.Text;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (Descripcion.Equals("") || Descripcion.Equals(null))
                        {
                            Descripcion = nombrePanelContenido;
                        }
                        else if (!Descripcion.Equals(""))
                        {

                        }

                        idProductoDetalleGral = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);

                        var DetalleGralPorPanel = mb.DetallesProductoGralPorPanel(Descripcion, FormPrincipal.userID, idProductoDetalleGral);

                        int cantidad = DetalleGralPorPanel.Length;

                        if (cantidad > 0)
                        {
                            if (Descripcion.Equals(nombrePanelContenido))
                            {
                                int idDetailGral = 0;
                                idDetailGral = Convert.ToInt32(DetalleGralPorPanel[3].ToString());

                                foreach (Control contHijo in fLPCentralDetalle.Controls)
                                {
                                    foreach (Control contSubHijo in contHijo.Controls)
                                    {
                                        if (contSubHijo.Name == nombrePanelContenido)
                                        {
                                            var idDetalleGral = mb.DetallesProductoGral(FormPrincipal.userID, idDetailGral);

                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                            {
                                                if (contItemSubHijo is Label)
                                                {
                                                    contItemSubHijo.Text = idDetalleGral[2].ToString();

                                                    //if (servidor.Equals(""))
                                                    //{
                                                    //    var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                                                    //}
                                                    //else if (!servidor.Equals(""))
                                                    //{
                                                    //    diccionarioDetallesGeneral.Add(contadorIndex, new Tuple<string, string, string, string>(DetalleGralPorPanel[0].ToString(), nombrePanelContenido, idDetailGral.ToString(), idDetalleGral[2].ToString()));
                                                    //    contadorIndex++;
                                                    //}
                                                    //break;
                                                }
                                            }
                                            idDetalleGral = new string[] { };
                                        }
                                    }
                                }
                            }
                        }
                        try
                        {
                            var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error de Desactivar Concepto Dinamico: " + ex.Message.ToString(), "Error de Actulización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                #endregion Si el Nombre del Panel es diferente a panelContenedorProveedor
            }
            #endregion Si el CheckBox esta en true
            #region Si el CheckBox esta en false
            else if (chekBoxClickDetalle.Checked == false)
            {
                foreach (Control itemPanelContenedor in fLPCentralDetalle.Controls)
                {
                    string namePanelContenedor = string.Empty;
                    namePanelContenedor = itemPanelContenedor.Name.ToString();

                    if (itemPanelContenedor is Panel)
                    {
                        if (namePanelContenedor == nombrePanelContenedor.ToString())
                        {
                            foreach (Control subItemPanelContenedor in itemPanelContenedor.Controls)
                            {
                                string namePanelContenido = string.Empty;
                                namePanelContenido = subItemPanelContenedor.Name.ToString();

                                if (subItemPanelContenedor is Panel)
                                {
                                    if (namePanelContenido == nombrePanelContenido.ToString())
                                    {
                                        fLPCentralDetalle.Controls.Remove(itemPanelContenedor);
                                        itemPanelContenedor.Dispose();
                                        try
                                        {
                                            var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error de Desactivar Concepto Dinamico: " + ex.Message.ToString(), "Error de Actulización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion Si el CheckBox esta en false
            #endregion Verificacion del Estado de CheckBox

            #region Recargar Registros Dinamicos
            loadFromConfigDB();
            #endregion Recargar Registros Dinamicos

            //UpdateKey(name, value);
            //RefreshAppSettings();
            //loadFormConfig();

            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    saveConfigIntoDB();
            //}
        }

        private void ComboBox_Quitar_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void bt_Click(object sender, EventArgs e)
        {
            Button botonPrecionado = sender as Button;
            string nameBt = string.Empty,
                    textoBuscado = string.Empty,
                    nombreConceptoReal = string.Empty;
            nameBt = botonPrecionado.Name;
            nombreConceptoReal = botonPrecionado.Tag.ToString();
            textoBuscado = nameBt.Remove(0, 2);
            if (textoBuscado.Equals("Proveedor"))
            {
                AgregarProveedor ap = new AgregarProveedor();
                ap.FormClosed += delegate
                {
                    fLPCentralDetalle.Controls.Clear();
                    //loadFormConfig();
                    loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                };
                ap.ShowDialog();
            }
            else
            {
                AgregarDetalleGeneral addDetailGral = new AgregarDetalleGeneral();
                addDetailGral.FormClosed += delegate
                {
                    fLPCentralDetalle.Controls.Clear();
                    //loadFormConfig();
                    loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                };
                addDetailGral.getChkName = textoBuscado;
                addDetailGral.getIdUsr = FormPrincipal.userID.ToString();
                addDetailGral.getRealChkName = nombreConceptoReal;
                addDetailGral.ShowDialog();
            }
        }

        private void cbDetalleGral_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { '|' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            namePanel = comboBox.Name.ToString().Remove(0, 2);

            CargarDetallesGral(namePanel);

            if (listaDetalleGral.Length > 0)
            {
                int idDetalleGral = 0;

                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaDetalleGral[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idDetalleGral = Convert.ToInt32(separadas[0]);
                }
                else if (comboBoxIndex <= 0)
                {
                    idDetalleGral = 0;
                    limpiarDatosDetalleGral(namePanel);
                }

                if (idDetalleGral > 0)
                {
                    //cargarDatosProveedor(Convert.ToInt32(idCategoria));
                    llenarDatosDetalleGral(namePanel);
                }
            }
        }

        private void limpiarDatosDetalleGral(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    //contLblHijo.Text = separadas[1].ToString().Replace("_", " ");
                                    //contLblHijo.Text = "En Construcción está sección...";
                                    contLblHijo.Text = string.Empty;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void chkBoxProductMessage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxProductMessage.Checked == true)
            {
                if (dtProdMessg.Rows.Count <= 0)
                {
                    XPos = this.Width / 2;
                    YPos = this.Height / 2;
                    mensajeDetalleProducto = Microsoft.VisualBasic.Interaction.InputBox("AGREGAR MENSAJE AL PRODUCTO ACTUAL\nDE SUGERENCIA PARA QUE AL COMPRADOR\nSE LE LEA AL VENDERSELO", "Mensaje de Sugerencia del Producto", "", XPos, YPos);
                    if (mensajeDetalleProducto.Equals(""))
                    {
                        //MessageBox.Show("El mensaje no tiene que estar vacio\nfavor de proporcionar un mensaje...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (!mensajeDetalleProducto.Equals(""))
                    {
                        cn.EjecutarConsulta(cs.GuardarProductMessage(finalIdProducto, mensajeDetalleProducto, "1"));
                    }
                }
                else if (dtProdMessg.Rows.Count > 0)
                {
                    string Activo = string.Empty;
                    Activo = drProdMessg["ProductMessageActivated"].ToString();
                    if (Activo.Equals("False"))
                    {
                        XPos = this.Width / 2;
                        YPos = this.Height / 2;
                        mensajeDetalleProducto = Microsoft.VisualBasic.Interaction.InputBox("ACTUALIZA EL MENSAJE DEL PRODUCTO\nPARA DAR SUGERENCIA AL COMPRADOR\n QUE SE LE VA VENDER", "Actualizar Sugerencia del Producto", $"{drProdMessg["ProductOfMessage"]}", XPos, YPos);
                        if (mensajeDetalleProducto.Equals(""))
                        {
                            //MessageBox.Show("El mensaje no tiene que estar vacio\nfavor de proporcionar un mensaje...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (!mensajeDetalleProducto.Equals(""))
                        {
                            cn.EjecutarConsulta(cs.UpdateProductMessage(mensajeDetalleProducto, drProdMessg["ID"].ToString()));
                        }
                    }
                }
            }
            else if (chkBoxProductMessage.Checked == false)
            {
                if (dtProdMessg.Rows.Count > 0)
                {
                    cn.EjecutarConsulta(cs.DesactivarProductMessage(drProdMessg["ID"].ToString()));
                }
            }
        }


        private void chkMensajeInventario_CheckedChanged(object sender, EventArgs e)
        {
            // Si esta desmarcado el checkbox o lo desmarcamos, verificamos si existe un
            // registro en la tabla de mensajes para inventario, si existe actualizamos el 
            // campo "Activo" con valor de 0 para que no muestre el mensaje en los apartados que se
            // vaya a utilizar, de lo contrario no hacemos nada

            if (eventoMensajeInventario)
            {
                var activo = chkMensajeInventario.Checked;

                if (activo)
                {
                    var existe = (bool)cn.EjecutarSelect($"SELECT * FROM MensajesInventario WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {finalIdProducto}");

                    if (existe)
                    {
                        cn.EjecutarConsulta($"UPDATE MensajesInventario SET Activo = 1 WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {finalIdProducto}");
                    }

                    using (var mensaje = new AgregarMensajeInventario())
                    {
                        mensaje.idProducto = Convert.ToInt32(finalIdProducto);
                        mensaje.ShowDialog();
                    }
                }
                else
                {
                    var existe = (bool)cn.EjecutarSelect($"SELECT * FROM MensajesInventario WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {finalIdProducto}");

                    if (existe)
                    {
                        cn.EjecutarConsulta($"UPDATE MensajesInventario SET Activo = 0 WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {finalIdProducto}");
                    }
                }
            }
        }


        private void AgregarDetalleProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            limpiarStockMinimoMaximo();

            AgregarEditarProducto addEditProducto = Application.OpenForms.OfType<AgregarEditarProducto>().FirstOrDefault();

            if (addEditProducto != null)
            {
                addEditProducto.actualizarDetallesProducto();
            }
        }

        private void AgregarDetalleProducto_Shown(object sender, EventArgs e)
        {
            verificarProductMessage();

            if (!nameXMLProveedor.Equals(string.Empty))
            {
                foreach (Control panelDinamico in fLPCentralDetalle.Controls)
                {
                    if (panelDinamico is Panel)
                    {
                        Panel pnlDin = (Panel)panelDinamico;

                        var namePanel = pnlDin.Name;

                        if (namePanel.Equals("panelContenedorProveedor"))
                        {
                            foreach (Control pnlContenido in pnlDin.Controls)
                            {
                                if (pnlContenido is Panel)
                                {
                                    if (pnlContenido.Name.Equals("panelContenidoProveedor"))
                                    {
                                        foreach (Control cbProveedor in pnlContenido.Controls)
                                        {
                                            if (cbProveedor is ComboBox)
                                            {
                                                cbProveedor.Text = nameXMLProveedor;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void llenarDatosDetalleGral(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    contLblHijo.Text = separadas[1].ToString().Replace("_", " ");
                                    //contLblHijo.Text = "En Construcción está sección...";
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtStockNecesario_KeyUp(object sender, KeyEventArgs e)
        {
            timerStockMaximo.Stop();
            timerStockMaximo.Start();
        }

        private void timerStockMaximo_Tick(object sender, EventArgs e)
        {
            timerStockMaximo.Stop();
            ValidarStockMaximo();
        }

        private void ValidarStockMaximo()
        {
            var minimoAux = txtStockMinimo.Text.Trim();
            var maximoAux = txtStockNecesario.Text.Trim();

            if (!string.IsNullOrWhiteSpace(minimoAux))
            {
                if (!string.IsNullOrWhiteSpace(maximoAux))
                {
                    var minimo = Convert.ToInt32(minimoAux);
                    var maximo = Convert.ToInt32(maximoAux);

                    if (maximo <= minimo)
                    {
                        MessageBox.Show("El stock máximo no puede ser menor \no igual que stock mínimo", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        txtStockNecesario.Text = string.Empty;
                        txtStockNecesario.Focus();
                    }
                }
            }
        }

        private void cbUbicacion_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { '|' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            namePanel = comboBox.Name.ToString().Remove(0, 2);

            if (listaUbicaciones.Length > 0)
            {
                idUbicacion = 0;
                nombreUbicacion = string.Empty;

                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaUbicaciones[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idUbicacion = Convert.ToInt32(separadas[0]);
                    nombreUbicacion = separadas[1].ToString();
                }
                else if (comboBoxIndex <= 0)
                {
                    idUbicacion = 0;
                }

                if (idUbicacion > 0)
                {
                    //cargarDatosProveedor(Convert.ToInt32(idCategoria));
                    llenarDatosUbicacion(namePanel);
                }
            }
        }

        private void fLPLateralConcepto_Paint(object sender, PaintEventArgs e)
        {

        }

        private void llenarDatosUbicacion(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    contLblHijo.Text = separadas[1].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AgregarDetalleProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void cbCategoria_SelectIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { ' ', '|' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            namePanel = comboBox.Name.ToString().Remove(0, 2);

            if (listaCategorias.Length > 0)
            {
                idCategoria = 0;
                nombreCategoria = string.Empty;

                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaCategorias[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idCategoria = Convert.ToInt32(separadas[0]);
                    nombreCategoria = separadas[1].ToString();
                }
                else if (comboBoxIndex <= 0)
                {
                    idCategoria = 0;
                }

                if (idCategoria > 0)
                {
                    //cargarDatosProveedor(Convert.ToInt32(idCategoria));
                    llenarDatosCategoria(namePanel);
                }
            }
        }

        private void llenarDatosCategoria(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    contLblHijo.Text = separadas[1].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cbProveedor_SelectValueChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string cadena = string.Empty, namePanel = string.Empty;
            char[] delimiterChars = { '-' };
            int comboBoxIndex = 0;

            comboBoxIndex = comboBox.SelectedIndex;
            namePanel = comboBox.Name.ToString().Remove(0, 2);

            if (listaProveedores.Length > 0)
            {
                idProveedor = 0;
                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaProveedores[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idProveedor = Convert.ToInt32(separadas[0]);
                    nombreProveedor = separadas[1];
                    nameProveedor = nombreProveedor.ToString().TrimStart();
                    var obtenerDatosProv = cn.CargarDatos($"SELECT * FROM Proveedores WHERE IDUsuario={FormPrincipal.userID}");

                    for (int i = 0; i < obtenerDatosProv.Rows.Count; i++)
                    {
                        rfc = obtenerDatosProv.Rows[i]["RFC"].ToString();
                    }

                }
                else if (comboBoxIndex <= 0)
                {
                    idProveedor = 0;
                    limpiarDatosProveedor(namePanel);
                }

                if (idProveedor > 0)
                {
                    cargarDatosProveedor(Convert.ToInt32(idProveedor));
                    llenarDatosProveedor(namePanel);
                }
            }
        }

        private void limpiarDatosProveedor(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "cb" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[0];
                                    contLblHijo.Text = string.Empty;
                                }
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[0];
                                    contLblHijo.Text = string.Empty;
                                }
                                else if (contLblHijo.Name == "lblRFC" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[1];
                                    contLblHijo.Text = string.Empty;
                                }
                                else if (contLblHijo.Name == "lblTel" + textoBuscado)
                                {
                                    //contLblHijo.Text = datosProveedor[10];
                                    contLblHijo.Text = string.Empty;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnInhabilitados_Click(object sender, EventArgs e)
        {
            ConceptosInhabilitados conceptosDasactivados = new ConceptosInhabilitados();

            conceptosDasactivados.FormClosing += delegate
            {
                fLPCentralDetalle.Controls.Clear();
                loadFromConfigDB();
                BuscarTextoListView(settingDatabases);
            };

            conceptosDasactivados.ShowDialog();
        }

        private void llenarDatosProveedor(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == namePanel)
                {
                    foreach (Control contSubHijo in contHijo.Controls.OfType<Control>())
                    {
                        namePanel = "panelContenido" + textoBuscado;
                        if (contSubHijo.Name == namePanel)
                        {
                            foreach (Control contLblHijo in contSubHijo.Controls.OfType<Control>())
                            {
                                if (contLblHijo.Name == "cb" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[0];
                                }
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[0];
                                }
                                else if (contLblHijo.Name == "lblRFC" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[1];
                                }
                                else if (contLblHijo.Name == "lblTel" + textoBuscado)
                                {
                                    contLblHijo.Text = datosProveedor[10];
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cargarDatosProveedor(int idProveedor)
        {
            // Para que no de error ya que nunca va a existir un proveedor en ID = 0
            if (idProveedor > 0)
            {
                datosProveedor = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);
            }
        }

        private void cargarDatosCategoria(int idCategoria)
        {
            // Para que no de error ya que nunca va a existir un categoria en ID = 0
            if (idCategoria > 0)
            {
                datosCategoria = mb.ObtenerDatosCategoria(idCategoria, FormPrincipal.userID);
            }
        }

        private void cargarDatosUbicacion(int idUbicacion)
        {
            // Para que no de error ya que nunca va a existir un ubicacion en ID = 0
            if (idUbicacion > 0)
            {
                datosUbicacion = mb.ObtenerDatosUbicacion(idUbicacion, FormPrincipal.userID);
            }
        }

        private void cargarDatosDetalleGral(int idDetalleGral)
        {
            // Para que no de error ya que nunca va a existir un DetalleGral en ID = 0
            if (idDetalleGral > 0)
            {
                datosDetalleGral = mb.ObtenerDatosDetalleGral(idDetalleGral, FormPrincipal.userID, idProductoDetalleGral);
            }
        }

        private void encontrarPanel(string panelBuscado)
        {
            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if (contHijo.Name == panelBuscado)
                {
                    fLPCentralDetalle.Controls.Remove(contHijo);
                    break;
                }
            }
        }

        private void checkBoxSetting_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBoxClickSetting = sender as CheckBox;
            string name = string.Empty, value = string.Empty;
            if (checkBoxClickSetting.Checked == true)
            {
                name = checkBoxClickSetting.Name.ToString();
                value = checkBoxClickSetting.Checked.ToString();
            }
            else if (checkBoxClickSetting.Checked == false)
            {
                name = checkBoxClickSetting.Name.ToString();
                value = checkBoxClickSetting.Checked.ToString();
            }

            int valorDatoDinamico = -1;

            if (value.Equals("True"))
            {
                valorDatoDinamico = 1;
            }
            else if (value.Equals("False"))
            {
                valorDatoDinamico = 0;
            }

            try
            {
                var UpdateDatoDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamicoShow(name, valorDatoDinamico, FormPrincipal.userID));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Actualizar " + name.Remove(0, 3) + "\nen la Base de Datos appSettings\nERROR: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                var UpdateDatoFiltroDinamico = cn.EjecutarConsulta(cs.ActualizarDatoFiltroDinamico(name, valorDatoDinamico, FormPrincipal.userID));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Actualizar " + name.Remove(0, 3) + "\nen la Base de Datos FiltroDinamico\nERROR: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (valorDatoDinamico.Equals(0))
            {
                try
                {
                    var BorrarFiltro = cn.EjecutarConsulta(cs.BorrarDatoVentanaFiltros(name.Remove(0, 3), FormPrincipal.userID));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Desactivar el Filtro Dinamico: " + ex.Message.ToString(), "Error al Desactivar Filtro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (valorDatoDinamico.Equals(1))
            {
                try
                {
                    using (DataTable dtFiltrosDinamicosVetanaFiltros = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(name.Remove(0, 3), FormPrincipal.userID)))
                    {
                        if (dtFiltrosDinamicosVetanaFiltros.Rows.Count.Equals(0))
                        {
                            var Guardarfiltro = cn.EjecutarConsulta(cs.GuardarVentanaFiltros("0", name.Remove(0, 3), "Selecciona " + name.Remove(0, 3), FormPrincipal.userID));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al Activar el Filtro Dinamico: " + ex.Message.ToString(), "Error al Activar Filtro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            loadFromConfigDB();

            //UpdateKey(name, value);
            //RefreshAppSettings();
            //loadFormConfig();

            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    saveConfigIntoDB();
            //}
        }

        #region Guardar Configuracion Dentro de la Base de Datos
        /// <summary>
        /// this code will add a listviewtem	
        /// to a listview for each database entry
        /// in the appSettings section of an App.config file.
        /// </summary>
        private void saveConfigIntoDB()
        {
            string datosAppSetting = string.Empty;

            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            try
            {
                appSettings = ConfigurationManager.AppSettings;
                datosAppSettings = new List<string>();

                if (appSettings.Count == 0)
                {
                    MessageBox.Show("Lectura de la Sección de AppSettings está vacia", "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (appSettings.Count > 0)
                {
                    for (int i = 0; i < appSettings.Count; i++)
                    {
                        connStr = appSettings[i];
                        keyName = appSettings.GetKey(i);
                        found = keyName.IndexOf("chk", 0, 3);
                        if (found >= 0)
                        {
                            datosAppSetting += connStr + "|" + keyName + "|" + FormPrincipal.userID.ToString() + "¬";
                        }
                        if (found <= -1)
                        {
                            datosAppSetting += connStr + "|" + keyName + "|";
                        }
                    }
                    int borrar = 0;
                    string deleteData = string.Empty;
                    deleteData = $"DELETE FROM appSettings WHERE IDUsuario = {FormPrincipal.userID.ToString()}";
                    borrar = cn.EjecutarConsulta(deleteData);
                    string auxAppSetting = string.Empty;
                    string[] str;
                    int insertar = 0;
                    auxAppSetting = datosAppSetting.TrimEnd('¬').TrimEnd();
                    str = auxAppSetting.Split('¬');
                    datosAppSettings.AddRange(str);
                    foreach (var item in datosAppSettings)
                    {
                        datosAppSettingToDB = item.Split('|');
                        for (int i = 0; i < datosAppSettingToDB.Length; i++)
                        {
                            if (datosAppSettingToDB[i].Equals("true"))
                            {
                                datosAppSettingToDB[i] = "1";
                            }
                            else if (datosAppSettingToDB[i].Equals("false"))
                            {
                                datosAppSettingToDB[i] = "0";
                            }
                        }
                        insertar = cn.EjecutarConsulta(cs.GuardarAppSettings(datosAppSettingToDB));
                    }
                }
            }
            catch (ConfigurationException e)
            {
                MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Guardar Configuracion Dentro de la Base de Datos

        private void ClickBotonesProductos(object sender, EventArgs e)
        {

        }

        #endregion Modifying Configuration Settings at Runtime

        #region Proveedores Categorias Ubicaciones

        private void CargarProveedores()
        {
            // Asignamos el Array con los nombres de los proveedores al comboBox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            proveedores = new Dictionary<string, string>();

            // Comprobar que ya exista al menos un Proveedor
            if (listaProveedores.Length > 0)
            {
                proveedores.Add("0", "Proveedores...");

                foreach (var proveedor in listaProveedores)
                {
                    var tmp = proveedor.Split('-');
                    proveedores.Add(tmp[0].Trim(), tmp[1].Trim());
                }
            }
            else
            {
                proveedores.Add("0", "Proveedores...");
            }
        }

        private void CargarCategorias()
        {
            listaCategorias = mb.ObtenerCategorias(FormPrincipal.userID);

            categorias = new Dictionary<string, string>();

            if (listaCategorias.Length > 0)
            {
                categorias.Add("0", "Categorías...");

                foreach (var categoria in listaCategorias)
                {
                    var auxiliar = categoria.Split('|');

                    categorias.Add(auxiliar[0], auxiliar[1]);
                }
            }
            else
            {
                categorias.Add("0", "Categorías...");
            }
        }

        private void CargarUbicaciones()
        {
            listaUbicaciones = mb.ObtenerUbicaciones(FormPrincipal.userID);

            ubicaciones = new Dictionary<string, string>();

            if (listaUbicaciones.Length > 0)
            {
                ubicaciones.Add("0", "Ubicaciónes...");

                foreach (var ubicacion in listaUbicaciones)
                {
                    var auxiliar = ubicacion.Split('|');

                    ubicaciones.Add(auxiliar[0], auxiliar[1]);
                }
            }
            else
            {
                ubicaciones.Add("0", "Ubicaciónes...");
            }
        }

        private void CargarDetallesGral(string textBuscado)
        {
            string concepto = string.Empty;
            detallesGral = new Dictionary<string, string>();

            concepto = textBuscado;

            listaDetalleGral = mb.ObtenerDetallesGral(FormPrincipal.userID, concepto);

            if (listaDetalleGral.Length > 0)
            {
                detallesGral.Add("0", concepto.Replace("_", " ") + "...");

                foreach (var DetailGral in listaDetalleGral)
                {
                    var auxiliar = DetailGral.Split('|');

                    detallesGral.Add(auxiliar[0], auxiliar[1].Replace("_", " "));
                }
            }
            else
            {
                detallesGral.Add("0", concepto.Replace("_", " ") + "...");
            }
        }

        #endregion Proveedores Categorias Ubicaciones

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            fLPLateralConcepto.AutoScroll = true;
            fLPLateralConcepto.HorizontalScroll.Enabled = false;
            fLPLateralConcepto.HorizontalScroll.Visible = false;
            fLPLateralConcepto.VerticalScroll.Enabled = true;
            fLPLateralConcepto.VerticalScroll.Visible = true;

            finalOrigenProdServCombo = origenProdServCombo;
            finalIdProducto = getIdProducto;

            var servidor = Properties.Settings.Default.Hosting;

            if (finalOrigenProdServCombo.Equals(1) || finalOrigenProdServCombo.Equals(3))
            {
                limpiarStockMinimoMaximo();
            }
            else if (finalOrigenProdServCombo.Equals(2) || finalOrigenProdServCombo.Equals(4))
            {
                string queryStockMinMax = string.Empty;
                queryStockMinMax = $"SELECT ID, StockNecesario, StockMinimo FROM Productos WHERE ID = {finalIdProducto}";
                using (DataTable dtStockMinimoMaximo = cn.CargarDatos(queryStockMinMax))
                {
                    txtStockMinimo.Text = string.Empty;
                    txtStockNecesario.Text = string.Empty;

                    foreach (DataRow drStockMinimoMaximo in dtStockMinimoMaximo.Rows)
                    {
                        txtStockMinimo.Text = drStockMinimoMaximo["StockMinimo"].ToString();
                        txtStockNecesario.Text = drStockMinimoMaximo["StockNecesario"].ToString();
                    }
                }
            }

            //loadFormConfig();
            loadFromConfigDB();
            BuscarTextoListView(settingDatabases);

            LimpiarTablaDetallesProductoGenerales();

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    VerificarFiltroDinamico();
            //}

            txtStockNecesario.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtStockMinimo.KeyPress += new KeyPressEventHandler(SoloDecimales);

            if (!string.IsNullOrEmpty(AgregarEditarProducto.stockNecesario))
            {
                var stockTmp = Convert.ToDecimal(AgregarEditarProducto.stockNecesario);

                if (stockTmp > 0)
                {
                    txtStockNecesario.Text = stockTmp.ToString();
                }
            }

            if (!string.IsNullOrEmpty(AgregarEditarProducto.stockMinimo))
            {
                var stockTmp = Convert.ToDecimal(AgregarEditarProducto.stockMinimo);

                if (stockTmp > 0)
                {
                    txtStockMinimo.Text = stockTmp.ToString();
                }
            }

            if (AgregarEditarProducto.typeOfProduct.Equals("P"))
            {
                txtStockNecesario.Enabled = true;
                txtStockMinimo.Enabled = true;
            }
            else if (!AgregarEditarProducto.typeOfProduct.Equals("P"))
            {
                txtStockNecesario.Enabled = false;
                txtStockMinimo.Enabled = false;
            }

            // correcion desde cargar de XML para qe no muestre error
            if (finalOrigenProdServCombo.Equals(2) || finalOrigenProdServCombo.Equals(4))
            {
                if (!finalIdProducto.Equals(""))
                {
                    // Verificar si tiene mensaje para mostrar el checkbox habilitado
                    var mensajeInventario = mb.MensajeInventario(Convert.ToInt32(finalIdProducto), 1);

                    if (!string.IsNullOrEmpty(mensajeInventario))
                    {
                        chkMensajeInventario.Checked = true;
                    }
                    else
                    {
                        chkMensajeInventario.Checked = false;
                    }

                    eventoMensajeInventario = true;
                }
            }

            if (finalOrigenProdServCombo.Equals(1) || finalOrigenProdServCombo.Equals(3))
            {
                if (!AgregarEditarProducto.detalleProductoBasico.Count.Equals(0))
                {
                    if (!fLPCentralDetalle.Controls.Count.Equals(0))
                    {
                        foreach (Control ctrPanelCentral in fLPCentralDetalle.Controls)
                        {
                            if (ctrPanelCentral.Name.Equals("panelContenedorProveedor"))
                            {
                                foreach (Control subCtrPanelCentral in ctrPanelCentral.Controls)
                                {
                                    if (subCtrPanelCentral.Name.Equals("panelContenidoProveedor"))
                                    {
                                        foreach (Control itemSubCtrPanelCentral in subCtrPanelCentral.Controls)
                                        {
                                            if (itemSubCtrPanelCentral is ComboBox)
                                            {
                                                for (var i = 0; i < AgregarEditarProducto.detalleProductoBasico.Count; i++)
                                                {
                                                    if (i.Equals(2))
                                                    {
                                                        itemSubCtrPanelCentral.Text = AgregarEditarProducto.detalleProductoBasico[i].ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (!AgregarEditarProducto.detalleProductoGeneral.Count.Equals(0))
                {
                    if (!fLPCentralDetalle.Controls.Count.Equals(0))
                    {
                        foreach (var item in AgregarEditarProducto.detalleProductoGeneral)
                        {
                            var words = item.Split('|');
                            foreach (Control ctrPanelCentral in fLPCentralDetalle.Controls)
                            {
                                if (ctrPanelCentral is Panel)
                                {
                                    if (!ctrPanelCentral.Name.Equals("panelContenedorProveedor"))
                                    {
                                        foreach (Control subCtrPanelCentral in ctrPanelCentral.Controls)
                                        {
                                            if (subCtrPanelCentral is Panel)
                                            {
                                                if (subCtrPanelCentral.Name.Equals(words[4].ToString()))
                                                {
                                                    foreach (Control itemSubCtrPanelCentral in subCtrPanelCentral.Controls)
                                                    {
                                                        if (itemSubCtrPanelCentral is ComboBox)
                                                        {
                                                            itemSubCtrPanelCentral.Text = words[5].ToString().Replace("_", " ");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //if (!nameXMLProveedor.Equals(string.Empty))
            //{
            //    foreach (Control panelDinamico in fLPCentralDetalle.Controls)
            //    {
            //        if (panelDinamico is Panel)
            //        {
            //            Panel pnlDin = (Panel)panelDinamico;

            //            var namePanel = pnlDin.Name;

            //            if (namePanel.Equals("panelContenedorProveedor"))
            //            {
            //                foreach (Control pnlContenido in pnlDin.Controls)
            //                {
            //                    if (pnlContenido is Panel)
            //                    {
            //                        if (pnlContenido.Name.Equals("panelContenidoProveedor"))
            //                        {
            //                            foreach (Control cbProveedor in pnlContenido.Controls)
            //                            {
            //                                if (cbProveedor is ComboBox)
            //                                {
            //                                    cbProveedor.Text = nameXMLProveedor;
            //                                    break;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void LimpiarTablaDetallesProductoGenerales()
        {
            string nameControl = string.Empty;
            List<string> ControlesPanelContenido = new List<string>();

            foreach (Control panelContenido in fLPCentralDetalle.Controls)
            {
                nameControl = panelContenido.Name.ToString().Remove(0, 15);
                if (!nameControl.Equals("Proveedor"))
                {
                    ControlesPanelContenido.Add("panelContenido" + nameControl);
                }
            }

            if (ControlesPanelContenido.Count() != 0)
            {
                foreach (var item in ControlesPanelContenido)
                {
                    using (DataTable dtDetallesProductoGenerales = cn.CargarDatos(cs.AgruparDetallesProductoGenerales(item.ToString(), finalIdProducto)))
                    {
                        if (!dtDetallesProductoGenerales.Rows.Count.Equals(0))
                        {
                            for (int i = 0; i < dtDetallesProductoGenerales.Rows.Count - 1; i++)
                            {
                                try
                                {
                                    var BorrarDetalleProductoGenerales = cn.EjecutarConsulta(cs.BorrarDetallesProductoGenerales(dtDetallesProductoGenerales.Rows[i]["ID"].ToString()));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Borrar el Filtro Dinamico Duplicado de la Tabla DetallesProductoGenerales:\n" + ex.Message.ToString(), "Error al Borrar Filtro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }

            ControlesPanelContenido.Clear();
        }

        private void VerificarFiltroDinamico()
        {
            foreach (Control itemControl in fLPLateralConcepto.Controls)
            {
                if (itemControl is FlowLayoutPanel)
                {
                    foreach (Control subItemControl in itemControl.Controls)
                    {
                        if (subItemControl is Panel)
                        {
                            foreach (Control intoSubItemControl in subItemControl.Controls)
                            {
                                if (intoSubItemControl is CheckBox)
                                {
                                    CheckBox chkBox = (CheckBox)intoSubItemControl;
                                    string nameChkBox = string.Empty, textSearch = string.Empty;
                                    bool chkBoxValue, found;
                                    int chkValueFound = -1;

                                    textSearch = "chk";

                                    nameChkBox = chkBox.Name;
                                    chkBoxValue = chkBox.Checked;

                                    found = nameChkBox.Contains(textSearch);

                                    if (found.Equals(true))
                                    {
                                        if (chkBoxValue.Equals(true))
                                        {
                                            chkValueFound = 1;
                                        }
                                        else if (chkBoxValue.Equals(false))
                                        {
                                            chkValueFound = 0;
                                        }

                                        using (DataTable dtVerificarDatoFiltroDinamico = cn.CargarDatos(cs.VerificarDatoFiltroDinamico(nameChkBox, FormPrincipal.userID)))
                                        {
                                            if (!dtVerificarDatoFiltroDinamico.Rows.Count.Equals(0))
                                            {
                                                try
                                                {
                                                    var actualizarFiltroDinamico = cn.EjecutarConsulta(cs.ActualizarDatoFiltroDinamico(nameChkBox, chkValueFound, FormPrincipal.userID));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al intentar Actualizar " + nameChkBox.Remove(0, 3) + "\nen la Base de Datos\nERROR: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else if (dtVerificarDatoFiltroDinamico.Rows.Count.Equals(0))
                                            {
                                                try
                                                {
                                                    var insertFiltroDinamico = cn.EjecutarConsulta(cs.InsertarDatoFiltroDinamico("chk" + nameChkBox, chkValueFound, FormPrincipal.userID));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al intentar Insertar " + nameChkBox.Remove(0, 3) + "\nen la Base de Datos\nERROR: " + ex.Message.ToString(), "Error al Insertar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void loadFromConfigDB()
        {
            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            chkDatabase.Items.Clear();
            settingDatabases.Items.Clear();

            lvi = new ListViewItem();

            try
            {
                chkDatabase.Clear();
                settingDatabases.Clear();

                using (DataTable dtChecarSihayDatosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
                {
                    if (dtChecarSihayDatosDinamicos.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
                        {
                            connStr = row["textComboBoxConcepto"].ToString();
                            if (row["checkBoxComboBoxConcepto"].ToString().Equals("1"))
                            {
                                keyName = "true";
                            }
                            else if (row["checkBoxComboBoxConcepto"].ToString().Equals("0"))
                            {
                                keyName = "false";
                            }
                            lvi = new ListViewItem(keyName);
                            lvi.SubItems.Add(connStr);
                            chkDatabase.Items.Add(lvi);
                        }
                        foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
                        {
                            connStr = row["concepto"].ToString();
                            if (row["checkBoxConcepto"].ToString().Equals("1"))
                            {
                                keyName = "true";
                            }
                            else if (row["checkBoxConcepto"].ToString().Equals("0"))
                            {
                                keyName = "false";
                            }
                            lvi = new ListViewItem(keyName);
                            lvi.SubItems.Add(connStr);
                            settingDatabases.Items.Add(lvi);
                        }
                    }
                    else if (dtChecarSihayDatosDinamicos.Rows.Count == 0)
                    {
                        //MessageBox.Show("No cuenta con Cofiguración en su sistema", "Sin Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        int nvoValorNumerico = 0;
                        int RegistroAgregado = -1;
                        RegistroAgregado = cn.EjecutarConsulta(cs.InsertaDatoDinamico("Proveedor", 0, FormPrincipal.userID));
                        if (RegistroAgregado.Equals(1))
                        {
                            //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (RegistroAgregado.Equals(0))
                        {
                            MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        RegistroAgregado = cn.EjecutarConsulta(cs.InsertarDatoFiltroDinamico("chkProveedor", 0, FormPrincipal.userID));
                        if (RegistroAgregado.Equals(1))
                        {
                            //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (RegistroAgregado.Equals(0))
                        {
                            MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...\nEn la tabla FiltroPrducto", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        using (DataTable dtChecarSihayDatosDinamicosRegistrados = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
                        {
                            if (dtChecarSihayDatosDinamicosRegistrados.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtChecarSihayDatosDinamicosRegistrados.Rows)
                                {
                                    connStr = row["textComboBoxConcepto"].ToString();
                                    if (row["checkBoxComboBoxConcepto"].ToString().Equals("1"))
                                    {
                                        keyName = "true";
                                    }
                                    else if (row["checkBoxComboBoxConcepto"].ToString().Equals("0"))
                                    {
                                        keyName = "false";
                                    }
                                    lvi = new ListViewItem(keyName);
                                    lvi.SubItems.Add(connStr);
                                    chkDatabase.Items.Add(lvi);
                                }
                                foreach (DataRow row in dtChecarSihayDatosDinamicosRegistrados.Rows)
                                {
                                    connStr = row["concepto"].ToString();
                                    if (row["checkBoxConcepto"].ToString().Equals("1"))
                                    {
                                        keyName = "true";
                                    }
                                    else if (row["checkBoxConcepto"].ToString().Equals("0"))
                                    {
                                        keyName = "false";
                                    }
                                    lvi = new ListViewItem(keyName);
                                    lvi.SubItems.Add(connStr);
                                    settingDatabases.Items.Add(lvi);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de lectura de los Datos Dinamicos: {0}" + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //}
        }

        private void limpiarStockMinimoMaximo()
        {
            txtStockMinimo.Text = string.Empty;
            txtStockMinimo.Text = "0";
            txtStockNecesario.Text = string.Empty;
            txtStockNecesario.Text = "0";
        }

        private void verificarProductMessage()
        {
            dtProdMessg = cn.CargarDatos(cs.ObtenerAllProductMessage(finalIdProducto));
            if (dtProdMessg.Rows.Count > 0)
            {
                //MessageBox.Show("SI Tiene Datos");
                drProdMessg = dtProdMessg.Rows[0];
                chkBoxProductMessage.Text = "El producto ya tiene mensaje asignado.";
                chkBoxProductMessage.Checked = Convert.ToBoolean(drProdMessg["ProductMessageActivated"]);
                chkBoxProductMessage.BackColor = Color.Green;
                chkBoxProductMessage.ForeColor = Color.White;
            }
            else if (dtProdMessg.Rows.Count <= 0)
            {
                //MessageBox.Show("NO Tiene Datos");
                chkBoxProductMessage.Text = "Agrega un mensaje al producto";
                chkBoxProductMessage.Checked = false;
                chkBoxProductMessage.BackColor = Color.Blue;
                chkBoxProductMessage.ForeColor = Color.White;
            }
        }

        private void btnDeleteDetalle_Click(object sender, EventArgs e)
        {
            InputBoxMessageBoxToDelete inputMessageBox = new InputBoxMessageBoxToDelete();

            inputMessageBox.FormClosing += delegate
            {
                fLPCentralDetalle.Controls.Clear();
                loadFromConfigDB();
                BuscarTextoListView(settingDatabases);
            };

            inputMessageBox.ShowDialog();

            //XPos = this.Width / 2;
            //YPos = this.Height / 2;
            //deleteDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el Detalle a Eliminar:", "Detalle a Eliminar", "Escriba aquí su Detalle a Eliminar", XPos, YPos);
            //try
            //{
            //    fLPCentralDetalle.Controls.Clear();
            //    if (nvoDetalle.Equals("Escriba aquí su Detalle a Eliminar"))
            //    {
            //        loadFromConfigDB();
            //        BuscarTextoListView(settingDatabases);
            //        MessageBox.Show("Error al eliminar detalle\nVerifique que el campo Eliminar Detalle a Mostrar\nTenga un nombre valido", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    else if (!deleteDetalle.Equals(""))
            //    {
            //        if (deleteDetalle.Equals("Proveedor"))
            //        {
            //            var mensaje = deleteDetalle;

            //            MessageBox.Show("No se puede Renombrar ó Eliminar\n(" + mensaje + ")\nya que es la configuración basica\nUsted esta Intentando realizar dicha operacion\nsobre la configuración: " + deleteDetalle.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            loadFromConfigDB();
            //            BuscarTextoListView(settingDatabases);
            //        }
            //        else
            //        {
            //            int found = -1;
            //            using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(deleteDetalle, FormPrincipal.userID)))
            //            {
            //                if (!dtItemDinamicos.Rows.Count.Equals(0))
            //                {
            //                    found = 1;
            //                }
            //                else if (dtItemDinamicos.Rows.Count.Equals(0))
            //                {
            //                    found = 0;
            //                }
            //            }

            //            if (found.Equals(1))
            //            {
            //                string tableSource = string.Empty;
            //                fLPCentralDetalle.Controls.Clear();

            //                try
            //                {
            //                    tableSource = "appSettings";
            //                    var DeleteDatoDinamicos = cn.EjecutarConsulta(cs.BorrarDatoDinamico(deleteDetalle, FormPrincipal.userID));
            //                }
            //                catch (MySqlException exMySql)
            //                {
            //                    MessageBox.Show($"Ocurrio una irregularidad al intentar\nBorrar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Borrado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show("El detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros\nExcepción: " + ex.Message.ToString() + $"({tableSource})", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }

            //                try
            //                {
            //                    tableSource = "FiltroDinamico";
            //                    var DeleteDatoFiltroDinamico = cn.EjecutarConsulta(cs.BorrarDatoFiltroDinamico("chk" + deleteDetalle, FormPrincipal.userID));
            //                }
            //                catch (MySqlException exMySql)
            //                {
            //                    MessageBox.Show($"Ocurrio una irregularidad al intentar\nBorrar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Borrado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show("El detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros\nExcepción: " + ex.Message.ToString() + $"({tableSource})", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }

            //                try
            //                {
            //                    tableSource = "FiltrosDinamicosVetanaFiltros";
            //                    var DeleteDatoFiltroDinamicoVentanaFiltros = cn.EjecutarConsulta(cs.BorrarDatoVentanaFiltros(deleteDetalle, FormPrincipal.userID));
            //                }
            //                catch (MySqlException exMySql)
            //                {
            //                    MessageBox.Show($"Ocurrio una irregularidad al intentar\nBorrar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Borrado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show("El detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros\nExcepción: " + ex.Message.ToString() + $"({tableSource})", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }

            //                try
            //                {
            //                    tableSource = "DetallesProductoGenerales";
            //                    var DeleteDetallesProductoGenerales = cn.EjecutarConsulta(cs.BorrarDetallesProductoGeneralesPorConcepto("panelContenido" + deleteDetalle, finalIdProducto));
            //                }
            //                catch (MySqlException exMySql)
            //                {
            //                    MessageBox.Show($"Ocurrio una irregularidad al intentar\nBorrar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Borrado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show("El detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros\nExcepción: " + ex.Message.ToString() + $"({tableSource})", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }

            //                try
            //                {
            //                    tableSource = "DetalleGeneral";
            //                    var DeleteDetalleGeneral = cn.EjecutarConsulta(cs.BorrarDetalleGeneralPorConcepto(deleteDetalle, FormPrincipal.userID));
            //                }
            //                catch (MySqlException exMySql)
            //                {
            //                    MessageBox.Show($"Ocurrio una irregularidad al intentar\nBorrar Detalle Producto({tableSource})...\nExcepción: " + exMySql.Message.ToString(), "Borrado Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show("El detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros\nExcepción: " + ex.Message.ToString() + $"({tableSource})", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }

            //                loadFromConfigDB();
            //                BuscarTextoListView(settingDatabases);
            //                deleteDetalle = string.Empty;
            //            }
            //            else if (found.Equals(0))
            //            {
            //                loadFromConfigDB();
            //                BuscarTextoListView(settingDatabases);
            //                MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            deleteDetalle = string.Empty;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    loadFromConfigDB();
            //    BuscarTextoListView(settingDatabases);
            //    MessageBox.Show("Error al eliminar el Detalle: " + deleteDetalle + " en los registros", "Error Try Catch Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDetalle_Click(object sender, EventArgs e)
        {
            mensajeMostrar = string.Empty;
            tituloVentana = string.Empty;
            mensajeDefault = string.Empty;

            mensajeMostrar = "Favor de poner el nombre del nuevo concepto del prodcuto, para darlo de alta al sistema.";
            tituloVentana = "Agregar concepto";
            mensajeDefault = "Concepto para Agregar al producto.";

            InputBoxMessageBox inputMessageBox = new InputBoxMessageBox(mensajeMostrar, tituloVentana, mensajeDefault);

            inputMessageBox.FormClosing += delegate
            {
                if (!inputMessageBox.retornoNombreConcepto.Equals(string.Empty))
                {
                    conceptoProductoAgregar = inputMessageBox.retornoNombreConcepto;

                    conceptoProductoAgregar = conceptoProductoAgregar.Replace(" ", "_");

                    //MessageBox.Show("Concepto: " + conceptoProductoAgregar);

                    using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(conceptoProductoAgregar, FormPrincipal.userID)))
                    {
                        if (!dtItemDinamicos.Rows.Count.Equals(0))
                        {
                            found = 1;
                        }
                        else if (dtItemDinamicos.Rows.Count.Equals(0))
                        {
                            found = 0;
                        }
                    }

                    if (found.Equals(1))
                    {
                        MessageBox.Show("El Registro que Intenta agregar ya esta registrado\nfavor de verificar o intentar con otro Detalle", "Aviso del Sistema al Agregar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (found.Equals(0))
                    {
                        int nvoValorNumerico = 0;
                        int RegistroAgregado = -1;

                        RegistroAgregado = cn.EjecutarConsulta(cs.InsertaDatoDinamico(conceptoProductoAgregar, nvoValorNumerico, FormPrincipal.userID));

                        if (RegistroAgregado.Equals(1))
                        {
                            //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (RegistroAgregado.Equals(0))
                        {
                            MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        RegistroAgregado = cn.EjecutarConsulta(cs.InsertarDatoFiltroDinamico("chk" + conceptoProductoAgregar, nvoValorNumerico, FormPrincipal.userID));

                        if (RegistroAgregado.Equals(1))
                        {
                            //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (RegistroAgregado.Equals(0))
                        {
                            MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...\nEn la tabla FiltroPrducto", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    fLPCentralDetalle.Controls.Clear();
                    loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                    editDetelle = string.Empty;
                    editDetalleNvo = string.Empty;
                }
                else
                {
                    fLPCentralDetalle.Controls.Clear();
                    loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                }
                using (DataTable dtPermisosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
                {
                    if (!dtPermisosDinamicos.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drConcepto in dtPermisosDinamicos.Rows)
                        {
                            try
                            {
                                var concepto = drConcepto["concepto"].ToString();
                                cn.EjecutarConsulta(cs.agregarDetalleProductoPermisosDinamicos(concepto));
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            };

            inputMessageBox.ShowDialog();

            //nvoValor = "false";
            //XPos = this.Width / 2;
            //YPos = this.Height / 2;
            //nvoDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Nuevo Detalle para Agregar:", "Agregar Nuevo Detalle a Mostrar", "Escriba aquí su Nuevo Detalle", XPos, YPos);
            //try
            //{
            //    int found = -1;
            //    fLPCentralDetalle.Controls.Clear();
            //    if (nvoDetalle.Equals("Escriba aquí su Nuevo Detalle"))
            //    {
            //        //RefreshAppSettings();
            //        //loadFormConfig();
            //        loadFromConfigDB();
            //        BuscarTextoListView(settingDatabases);
            //        MessageBox.Show("Error al intentar Agregar\nVerifique que el campo Agregar Nuevo Detalle a Mostrar\nTenga un nombre valido", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    else if (!nvoDetalle.Equals(""))
            //    {
            //        //AddKey(nvoDetalle, nvoValor);
            //        //RefreshAppSettings();
            //        //loadFormConfig();
            //        using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(nvoDetalle, FormPrincipal.userID)))
            //        {
            //            if (!dtItemDinamicos.Rows.Count.Equals(0))
            //            {
            //                found = 1;
            //            }
            //            else if (dtItemDinamicos.Rows.Count.Equals(0))
            //            {
            //                found = 0;
            //            }
            //        }
            //        if (found.Equals(1))
            //        {
            //            MessageBox.Show("El Registro que Intenra ya esta registrado\nfavor de verificar o intentar con otro Detalle", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        else if (found.Equals(0))
            //        {
            //            int nvoValorNumerico = 0;
            //            int RegistroAgregado = -1;

            //            RegistroAgregado = cn.EjecutarConsulta(cs.InsertaDatoDinamico(nvoDetalle, nvoValorNumerico, FormPrincipal.userID));
            //            if (RegistroAgregado.Equals(1))
            //            {
            //                //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //            else if (RegistroAgregado.Equals(0))
            //            {
            //                MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }

            //            RegistroAgregado = cn.EjecutarConsulta(cs.InsertarDatoFiltroDinamico("chk" + nvoDetalle, nvoValorNumerico, FormPrincipal.userID));
            //            if (RegistroAgregado.Equals(1))
            //            {
            //                //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //            else if (RegistroAgregado.Equals(0))
            //            {
            //                MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...\nEn la tabla FiltroPrducto", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }

            //        loadFromConfigDB();
            //        BuscarTextoListView(settingDatabases);
            //        editDetelle = string.Empty;
            //        editDetalleNvo = string.Empty;
            //    }
            //    else if (nvoDetalle.Equals(""))
            //    {
            //        //RefreshAppSettings();
            //        //loadFormConfig();
            //        loadFromConfigDB();
            //        BuscarTextoListView(settingDatabases);
            //        MessageBox.Show("Error al intentar Agregar\nVerifique que el campo Agregar Nuevo Detalle a Mostrar\nNo este Vacio por favor", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //RefreshAppSettings();
            //    //loadFormConfig();
            //    loadFromConfigDB();
            //    BuscarTextoListView(settingDatabases);
            //    MessageBox.Show("Error al intentar Agregar: " + ex.Message.ToString(), "Error Try Catch Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnRenameDetalle_Click(object sender, EventArgs e)
        {
            RenombrarDetalle renameDetail = new RenombrarDetalle();

            renameDetail.FormClosing += delegate
            {
                fLPCentralDetalle.Controls.Clear();
                loadFromConfigDB();
                BuscarTextoListView(settingDatabases);
            };

            renameDetail.ShowDialog();
        }

        private void btnGuardarDetalles_Click(object sender, EventArgs e)
        {
            var stockNecesario = txtStockNecesario.Text;
            var stockMinimo = txtStockMinimo.Text;

            if (string.IsNullOrWhiteSpace(stockNecesario))
            {
                if (!string.IsNullOrWhiteSpace(stockMinimo))
                {
                    stockNecesario = stockMinimo;
                }
                else
                {
                    stockNecesario = "0";
                }
            }

            if (string.IsNullOrWhiteSpace(stockMinimo))
            {
                if (!string.IsNullOrWhiteSpace(stockNecesario))
                {
                    stockMinimo = stockNecesario;
                }
                else
                {
                    stockMinimo = "0";
                }
            }

            //AgregarEditarProducto.stockNecesario = stockNecesario;
            //AgregarEditarProducto.stockMinimo = stockMinimo;

            saveProdDetail();
            saveGralProdDetail();
            this.Close();
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

        private void saveGralProdDetail()
        {
            // Variables para poder hacer el control de registro y actualizacion de la tabla
            string Descripcion = string.Empty, panel = string.Empty;

            // lista para almacenar los datos para el nvo registro o actualizacion
            infoDetailProdGral = new List<string>();

            infoDetailProdGral.Clear();

            // Recorremos los controles en el FlowLayoutPanelCentral(Principal)
            foreach (Control contHijo in fLPCentralDetalle.Controls)
            {
                // Obtenemos el nombre del Control = PanelContenedor
                panel = contHijo.Name;

                // Verificamos si el nombre del PanelContenedor no sea Igual a panelContenedorProveedor
                if (!contHijo.Name.Equals("panelContenedorProveedor"))
                {
                    // recorremos los controles en el PanelContenedor
                    foreach (Control contSubHijo in contHijo.Controls)
                    {
                        string nameConcepto = string.Empty, textoConcepto = string.Empty, namepanelContenido = string.Empty;

                        foreach (Control contItemSubHijo in contSubHijo.Controls)
                        {
                            // verificamos si el control es de tipo Label
                            if (contItemSubHijo is Label)
                            {
                                infoDetailProdGral.Clear();

                                bool alreadyStoredDescripcion = false, alreadyStoredDescripcionGral = false;

                                string IdDetalleGral = string.Empty;

                                nameConcepto = contItemSubHijo.Name.ToString().Remove(0, 9);
                                textoConcepto = contItemSubHijo.Text;

                                namepanelContenido = contItemSubHijo.Name.ToString().Remove(0, 9);

                                if (finalOrigenProdServCombo.Equals(1) || finalOrigenProdServCombo.Equals(3) || finalOrigenProdServCombo.Equals(4))
                                {
                                    string rowDataList = string.Empty;
                                    var idFoundNew = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, textoConcepto.Replace(" ", "_"));

                                    int contieneIdFoundNew = idFoundNew.Length;

                                    if (contieneIdFoundNew > 0)
                                    {
                                        rowDataList = finalIdProducto + "|" + Convert.ToString(FormPrincipal.userID) + "|" + idFoundNew[0].ToString() + "|1|panelContenido" + idFoundNew[2].ToString() + "|" + idFoundNew[3].ToString();
                                        AgregarEditarProducto.detalleProductoGeneral.Add(rowDataList);
                                    }
                                }
                                else if (finalOrigenProdServCombo.Equals(2))
                                {
                                    using (DataTable dtDynamicFilters = cn.CargarDatos(cs.VerificarTextoConceptoFiltroDinamico(nameConcepto, FormPrincipal.userID)))
                                    {
                                        if (!dtDynamicFilters.Rows.Count.Equals(0))
                                        {
                                            alreadyStoredDescripcion = true;
                                        }
                                        else if (dtDynamicFilters.Rows.Count.Equals(0))
                                        {
                                            alreadyStoredDescripcion = false;
                                        }
                                    }

                                    var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, textoConcepto.Replace(" ", "_"));

                                    var idProductoBuscar = finalIdProducto;

                                    if (alreadyStoredDescripcion)
                                    {
                                        try
                                        {
                                            cn.EjecutarConsulta(cs.ActualizarTextConceptoFiltroDinamico("chk" + nameConcepto, FormPrincipal.userID, textoConcepto.Replace(" ", "_")));
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error al actualizar Texto del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }

                                        try
                                        {
                                            using (DataTable dtDetallesProductoGenerales = cn.CargarDatos($"SELECT * FROM DetallesProductoGenerales WHERE IDUsuario = '{FormPrincipal.userID}' AND IDProducto = '{idProductoBuscar}' AND panelContenido = 'panelContenido{namepanelContenido}'"))
                                            {
                                                if (!dtDetallesProductoGenerales.Rows.Count.Equals(0))
                                                {
                                                    alreadyStoredDescripcionGral = true;
                                                    foreach (DataRow dtRow in dtDetallesProductoGenerales.Rows)
                                                    {
                                                        IdDetalleGral = dtRow["ID"].ToString();
                                                    }
                                                }
                                                else if (dtDetallesProductoGenerales.Rows.Count.Equals(0))
                                                {
                                                    alreadyStoredDescripcionGral = false;
                                                }
                                            }
                                            //textoConcepto = "";
                                            var idFoundGral = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, textoConcepto.Replace(" ", "_"));

                                            var idProductoBuscarGral = finalIdProducto;

                                            if (alreadyStoredDescripcionGral)
                                            {
                                                try
                                                {
                                                    if (textoConcepto == "")
                                                    {
                                                        cn.EjecutarConsulta(cs.eliminarDetalleDinamico(FormPrincipal.userID.ToString(), IdDetalleGral));
                                                    }
                                                    else
                                                    {
                                                        cn.EjecutarConsulta(cs.ActualizarTextConceptoFiltroDinamicoGral(idFoundGral[0].ToString(), IdDetalleGral));
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al actualizar Texto del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else if (!alreadyStoredDescripcionGral)
                                            {
                                                try
                                                {
                                                    // Se almacenan los datos para 
                                                    // el posterior registro
                                                    infoDetailProdGral.Add(finalIdProducto);
                                                    infoDetailProdGral.Add(FormPrincipal.userID.ToString());
                                                    if (idFoundGral.Length > 0)
                                                    {
                                                        infoDetailProdGral.Add(idFoundGral[0].ToString());
                                                        infoDetailProdGral.Add("1");
                                                        infoDetailProdGral.Add("panelContenido" + idFoundGral[2].ToString());
                                                        // Ejecutamos el proceso de guardado
                                                        try
                                                        {
                                                            guardar = infoDetailProdGral.ToArray();
                                                            cn.EjecutarConsulta(cs.GuardarDetallesProductoGenerales(guardar));
                                                            infoDetailProdGral.Clear();
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            MessageBox.Show("El proceso de guardardo del nuevo Detalle Del Producto Generales\nocurrio un error:\n" + ex.Message.ToString(), "Error al guardar nuevo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al agregar Texto del Concepto Dinamico: " + ex.Message.ToString(), "Error al Agregar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error al actualizar Texto de DetallesProducto: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else if (!alreadyStoredDescripcion)
                                    {
                                        infoDetailProdGral.Clear();
                                        // Verificamos si el control es de tipo Label
                                        if (contItemSubHijo is Label)
                                        {
                                            if (!finalIdProducto.Equals(""))
                                            {
                                                using (DataTable dtDetallesProductoGenerales = cn.CargarDatos(cs.VerificarDetallesProductoGenerales(Convert.ToString(finalIdProducto), FormPrincipal.userID, "panelContenido" + namepanelContenido)))
                                                {
                                                    if (!dtDetallesProductoGenerales.Rows.Count.Equals(0))
                                                    {
                                                        foreach (DataRow drDetallesProductoGenerales in dtDetallesProductoGenerales.Rows)
                                                        {
                                                            var idFoundGral = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, textoConcepto.Replace(" ", "_"));
                                                            try
                                                            {
                                                                infoDetailProdGral.Add(drDetallesProductoGenerales["ID"].ToString());
                                                                infoDetailProdGral.Add(idFoundGral[0].ToString());
                                                                guardar = infoDetailProdGral.ToArray();
                                                                cn.EjecutarConsulta(cs.ActualizarDetallesProductoGenerales(guardar));
                                                                infoDetailProdGral.Clear();
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                MessageBox.Show("Algo Ocurrio al Actualizar registro a la Tabla DetallesProductoGenerales\nError: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                    }
                                                    else if (dtDetallesProductoGenerales.Rows.Count.Equals(0))
                                                    {
                                                        // Se almacenan los datos para 
                                                        // el posterior registro
                                                        infoDetailProdGral.Add(finalIdProducto);
                                                        infoDetailProdGral.Add(FormPrincipal.userID.ToString());
                                                        Descripcion = contItemSubHijo.Text;
                                                        var idFoundNew = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, Descripcion);
                                                        if (idFoundNew.Length > 0)
                                                        {
                                                            infoDetailProdGral.Add(idFoundNew[0].ToString());
                                                            infoDetailProdGral.Add("1");
                                                            infoDetailProdGral.Add("panelContenido" + idFoundNew[2].ToString());
                                                            // Ejecutamos el proceso de guardado
                                                            try
                                                            {
                                                                guardar = infoDetailProdGral.ToArray();
                                                                cn.EjecutarConsulta(cs.GuardarDetallesProductoGenerales(guardar));
                                                                infoDetailProdGral.Clear();
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                MessageBox.Show("El proceso de guardardo del nuevo Detalle Del Producto Generales\nocurrio un error:\n" + ex.Message.ToString(), "Error al guardar nuevo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else if (finalIdProducto.Equals(""))
                                            {
                                                //MessageBox.Show("Pasar la informacion a Agregar/Editar Productos", "En Construcción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                infoDetailProdGral.Add(finalIdProducto);
                                                infoDetailProdGral.Add(Convert.ToString(FormPrincipal.userID));
                                                Descripcion = contItemSubHijo.Text;
                                                var idFoundNew = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, Descripcion);

                                                if (idFoundNew.Length > 0)
                                                {
                                                    infoDetailProdGral.Add(idFoundNew[2].ToString()); // Obtenemos el ChckName Detalles Basicos
                                                    infoDetailProdGral.Add(idFoundNew[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                    infoDetailProdGral.Add(idFoundNew[3].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                    AgregarEditarProducto.detalleProductoGeneral = infoDetailProdGral;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void saveProdDetail()
        {
            // Variables para poder hacer el control de registro y actualizacion de la tabla
            string Descripcion = string.Empty, panel = string.Empty;

            // Lista para almacenar los datos para el nvo registro o actualizacion
            infoDetalle = new List<string>();

            // Recorremos los controles en el FlowLayoutPanelCentral(Principal)
            foreach (Control contHijo in fLPCentralDetalle.Controls)
            {
                // Obtenemos el nombre del del Control = PanelContenedor
                panel = contHijo.Name;

                // Verificamos si el nombre del PanelContenedor que sea Igual a panelContenedorProveedor
                if (contHijo.Name.Equals("panelContenedorProveedor"))
                {
                    // Recorremos los controles en el PanelContenedor
                    foreach (Control contSubHijo in contHijo.Controls)
                    {
                        // Verificamos que sea el PanelContenido sea el de Proveedor
                        if (contSubHijo.Name.Equals("panelContenidoProveedor"))
                        {
                            // Recorremos los controles en el PanelContenido
                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                            {
                                // Verificamos si el control es de tipo Label
                                if (contItemSubHijo is Label)
                                {
                                    // Verificamos si el nombre del Label es lbNombreProveedor
                                    // esto soló para que haga este proceso en el label del
                                    // nombre y no en el resto de los label
                                    if (contItemSubHijo.Name.Equals("lblNombreProveedor"))
                                    {
                                        bool alreadyStoredDescripcion = false;

                                        string nameConcepto = string.Empty, textoConcepto = string.Empty;

                                        nameConcepto = contItemSubHijo.Name.ToString().Remove(0, 9);
                                        textoConcepto = contItemSubHijo.Text;

                                        if (finalOrigenProdServCombo.Equals(1) || finalOrigenProdServCombo.Equals(3) || finalOrigenProdServCombo.Equals(4))
                                        {
                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, textoConcepto);
                                            int contieneDetalleProveedor = idFound.Length;
                                            if (contieneDetalleProveedor > 0)
                                            {
                                                if (AgregarEditarProducto.detalleProductoBasico.Count.Equals(0))
                                                {
                                                    AgregarEditarProducto.detalleProductoBasico.Add(finalIdProducto);
                                                    AgregarEditarProducto.detalleProductoBasico.Add(Convert.ToString(FormPrincipal.userID));
                                                    AgregarEditarProducto.detalleProductoBasico.Add(idFound[2].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                    AgregarEditarProducto.detalleProductoBasico.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                }
                                                else
                                                {
                                                    AgregarEditarProducto.detalleProductoBasico.Clear();
                                                    AgregarEditarProducto.detalleProductoBasico.Add(finalIdProducto);
                                                    AgregarEditarProducto.detalleProductoBasico.Add(Convert.ToString(FormPrincipal.userID));
                                                    AgregarEditarProducto.detalleProductoBasico.Add(idFound[2].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                    AgregarEditarProducto.detalleProductoBasico.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                }
                                            }
                                        }
                                        else if (finalOrigenProdServCombo.Equals(2))
                                        {
                                            // Verificamos si la Descripcion ya existe en el Diccionario de Detalles Basicos
                                            // sí ya esta el registro de la Descripcion la variable sera true
                                            // sí no esta el registro de la Descripcion la variable sera false
                                            using (DataTable dtDynamicFilters = cn.CargarDatos(cs.VerificarTextoConceptoFiltroDinamico(nameConcepto, FormPrincipal.userID)))
                                            {
                                                if (!dtDynamicFilters.Rows.Count.Equals(0))
                                                {
                                                    alreadyStoredDescripcion = true;
                                                }
                                                else if (dtDynamicFilters.Rows.Count.Equals(0))
                                                {
                                                    alreadyStoredDescripcion = false;
                                                }
                                            }

                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, textoConcepto);
                                            var idProductoBuscar = finalIdProducto;

                                            if (alreadyStoredDescripcion)
                                            {
                                                try
                                                {
                                                    cn.EjecutarConsulta(cs.ActualizarTextConceptoFiltroDinamico("chk" + nameConcepto, FormPrincipal.userID, textoConcepto));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al actualizar Texto del Concepto Dinamico: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                try
                                                {
                                                    string queryCargarDatos = string.Empty;

                                                    queryCargarDatos = $"SELECT * FROM DetallesProducto WHERE IDUsuario = '{FormPrincipal.userID}' AND IDProducto = '{idProductoBuscar}'";

                                                    using (DataTable dtDetallesProducto = cn.CargarDatos(queryCargarDatos))
                                                    {
                                                        //infoDetalle.Add(FormPrincipal.userID.ToString());
                                                        if (!dtDetallesProducto.Rows.Count.Equals(0))
                                                        {
                                                            if (idFound.Count() > 0)
                                                            {
                                                                foreach (DataRow dtRow in dtDetallesProducto.Rows)
                                                                {
                                                                    infoDetalle.Add(dtRow["ID"].ToString());
                                                                }
                                                                infoDetalle.Add(idFound[0].ToString());
                                                                infoDetalle.Add(idFound[2].ToString());
                                                                guardar = infoDetalle.ToArray();
                                                                cn.EjecutarConsulta(cs.ActualizarProveedorDetallesDelProducto(guardar));
                                                            }
                                                            else
                                                            {
                                                                infoDetalle.Add(idProductoBuscar);
                                                                infoDetalle.Add("0");
                                                                infoDetalle.Add("");
                                                                guardar = infoDetalle.ToArray();
                                                                cn.EjecutarConsulta(cs.ActualizarProvedorDetallesProd(guardar));
                                                            }
                                                        }
                                                        else if (dtDetallesProducto.Rows.Count.Equals(0))
                                                        {
                                                            if (idFound.Count() > 0)
                                                            {
                                                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(Convert.ToInt32(idProductoBuscar), FormPrincipal.userID, idFound[2].ToString(), Convert.ToInt32(idFound[0].ToString())));
                                                            }
                                                        }
                                                    }

                                                    infoDetalle.Clear(); // limpiamos de informacion la Lista
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al actualizar Texto de DetallesProducto: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else if (!alreadyStoredDescripcion)
                                            {
                                                try
                                                {
                                                    cn.EjecutarConsulta(cs.InsertarDatoFiltroDinamicoCompleto("chk" + nameConcepto, textoConcepto, FormPrincipal.userID));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al registrar Texto del Concepto Dinamico: " + ex.Message.ToString(), "Error al Registrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                                try
                                                {
                                                    string queryCargarDatos = string.Empty;

                                                    queryCargarDatos = $"SELECT * FROM DetallesProducto WHERE IDUsuario = '{FormPrincipal.userID}' AND IDProducto = '{idProductoBuscar}'";

                                                    using (DataTable dtDetallesProducto = cn.CargarDatos(queryCargarDatos))
                                                    {
                                                        //infoDetalle.Add(FormPrincipal.userID.ToString());
                                                        if (!dtDetallesProducto.Rows.Count.Equals(0))
                                                        {
                                                            if (idFound.Count() > 0)
                                                            {
                                                                foreach (DataRow dtRow in dtDetallesProducto.Rows)
                                                                {
                                                                    infoDetalle.Add(dtRow["ID"].ToString());
                                                                }
                                                                infoDetalle.Add(idFound[0].ToString());
                                                                infoDetalle.Add(idFound[2].ToString());
                                                                guardar = infoDetalle.ToArray();
                                                                cn.EjecutarConsulta(cs.ActualizarProveedorDetallesDelProducto(guardar));
                                                            }
                                                        }
                                                        else if (dtDetallesProducto.Rows.Count.Equals(0))
                                                        {
                                                            if (idFound.Count() > 0)
                                                            {
                                                                cn.EjecutarConsulta(cs.GuardarDetallesDelProducto(Convert.ToInt32(idProductoBuscar), FormPrincipal.userID, idFound[2].ToString(), Convert.ToInt32(idFound[0].ToString())));
                                                            }
                                                        }
                                                    }
                                                    infoDetalle.Clear(); // limpiamos de informacion la Lista
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Error al actualizar Texto de DetallesProducto: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
