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

namespace PuntoDeVentaV2
{
    public partial class AgregarDetalleProducto : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        #region Variables Globales

        List<string> optionList;

        Dictionary<string, string> proveedores;
        Dictionary<string, string> categorias;

        string[] datos;
        string[] separadas;

        string[] listaProveedores = new string[] { };
        string[] listaCategorias = new string[] { };
        string[] listaUbicaciones = new string[] { };

        bool habilitarComboBoxes = false;

        int XPos = 0, YPos = 0;
        string nvoDetalle = string.Empty, nvoValor = string.Empty, editValor = string.Empty, deleteDetalle = string.Empty;

        public string getOldValue { get; set; }
        public string getNewValue { get; set; }

        string editDetelle = string.Empty, editDetalleNvo = string.Empty;

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

        // this code will add a listviewtem
        // to a listview for each database entry
        // in the appSettings section of an App.config file.
        private void loadFormConfig()
        {
            //xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + @"..\..\App.config");
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
                    //throw new ArgumentException("Nombre clave: <" + strKey + "> ya existe en la configuración.");
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
                //throw ex;
                MessageBox.Show("Tipo de error: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Updates a key within the App.config
        private void UpdateKey(string strKey, string newValue)
        {
            if (!KeyExist(strKey))
            {
                //throw new ArgumentNullException("Nombre clave", " <" + strKey + "> no existe en la configuración. Actualización fallida.");
                MessageBox.Show("Nombre clave <" + strKey + "> no existe en la configuración. Actualización fallida.", "Error Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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
                        }
                        if (!editDetalleNvo.Equals("") && strKey.Equals(""))
                        {
                            childNode.Attributes["key"].Value = strKey;
                        }
                        //txtNombre.Text = childNode.Attributes["key"].Value.ToString();
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
                        }
                        if (!editDetalleNvo.Equals("") && strKey.Equals(""))
                        {
                            childNode.Attributes["key"].Value = "chk" + strKey;
                        }
                        //txtNombre.Text = childNode.Attributes["key"].Value.ToString();
                        break;
                    }
                }

                //string path = string.Empty;
                try
                {
                    if (Properties.Settings.Default.TipoEjecucion == 1)
                    {
                        //path = Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo;
                        //MessageBox.Show("Path del Archivo de configuración: " + path, "Direccion de archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }

                    if (Properties.Settings.Default.TipoEjecucion == 2)
                    {
                        //path = Properties.Settings.Default.baseDirectory +Properties.Settings.Default.archivo;
                        //MessageBox.Show("Path del Archivo de configuración: " + path, "Direccion de archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xmlDoc.Save(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
                    }
                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al Intentar actualizar el archivo de configuración: " + e.Message.ToString(), "Error de archivo de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                //ReadKey(strKey);
            }
        }

        // Read a key within the App.config
        public void ReadKey(string strKey)
        {
            if (!KeyExist(strKey))
            {
                //throw new ArgumentNullException("Nombre clave", " <" + strKey + "> no existe en la configuración. Actualización fallida.");
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
                        //checkBox1.Text = childNode.Attributes["key"].Value;
                        //checkBox1.Checked = Convert.ToBoolean(childNode.Attributes["value"].Value.ToLower());
                        //txtValor.Text = childNode.Attributes["value"].Value.ToLower().ToString();
                        //MessageBox.Show("Nombre clave: " + childNode.Attributes["key"].Value + " Su valor: " + childNode.Attributes["value"].Value.ToLower().ToString(), "Valor y Clave", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                throw new ArgumentNullException("Nombre clave", "<" + strKey + "> no existe en la configuración. Imposible Borrar.");
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
                //xmlDoc.Save(baseDirectory + @"\" + archivo);
                //xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                loadFormConfig();
                //ReadKey(strKey);
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

            for (int i = 0; i < settingDatabases.Items.Count; i++)
            {
                chkDetalleProductoTxt = settingDatabases.Items[i].Text.ToString();

                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Width = 258;
                panelHijo.Height = 29;
                panelHijo.HorizontalScroll.Visible = false;

                Panel panelContenedor = new Panel();
                panelContenedor.Width = 250;
                panelContenedor.Height = 23;
                panelContenedor.Name = "panel" + chkDetalleProductoTxt;
                //panelContenedor.BackColor = Color.Aquamarine;

                CheckBox check = new CheckBox();
                check.Name = chkDetalleProductoTxt;
                check.Text = chkDetalleProductoTxt;
                check.Width = 110;
                check.Height = 24;
                check.Location = new Point(0, 0);
                check.CheckedChanged += checkBox_CheckedChanged;
                //check.BackColor = Color.Aquamarine;

                chkDetalleProductoVal = settingDatabases.Items[i].SubItems[1].Text.ToString();
                if (chkDetalleProductoVal.Equals("true") || chkDetalleProductoVal.Equals("false"))
                {
                    check.Checked = Convert.ToBoolean(chkDetalleProductoVal);
                    panelContenedor.Controls.Add(check);
                    panelHijo.Controls.Add(panelContenedor);

                    // Agregamos el Botón de agregar item Más
                    Button bt = new Button();
                    bt.Cursor = Cursors.Hand;
                    //bt.Text = "+";
                    //bt.Font = new Font(bt.Font.FontFamily, 11);
                    bt.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
                    bt.Name = "btnGenerado" + id;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#5DADE2");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    //bt.TextAlign = ContentAlignment.MiddleCenter;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(ClickBotonesProductos);
                    bt.Location = new Point(115, 0);
                    panelContenedor.Controls.Add(bt);
                    panelHijo.Controls.Add(panelContenedor);

                    if (row < chkDatabase.Items.Count)
                    {
                        chkSettingVariableTxt = chkDatabase.Items[row].Text.ToString();
                        CheckBox checkSetting = new CheckBox();
                        checkSetting.Name = chkSettingVariableTxt;
                        checkSetting.Width = 20;
                        checkSetting.Height = 24;
                        checkSetting.Location = new Point(155, 0);
                        checkSetting.CheckedChanged += checkBoxSetting_CheckedChanged;

                        chkSettingVariableVal = chkDatabase.Items[row].SubItems[1].Text.ToString();
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
            CheckBox chekBoxClickDetalle = sender as CheckBox;
            FlowLayoutPanel panelContenedor = new FlowLayoutPanel();
            Panel panelContenido = new Panel();
            string name = string.Empty, value = string.Empty;
            string nombrePanelContenedor = string.Empty;
            string nombrePanelContenido = string.Empty;

            if (chekBoxClickDetalle.Checked == true)
            {
                name = chekBoxClickDetalle.Name.ToString();
                value = chekBoxClickDetalle.Checked.ToString();

                nombrePanelContenedor = "panelContenedor" + chekBoxClickDetalle.Name.ToString();

                panelContenedor.Name = nombrePanelContenedor;

                if (panelContenedor.Name == "panelContenedorProveedor")
                {
                    nombrePanelContenido = "panelContenido" + chekBoxClickDetalle.Name.ToString();

                    panelContenedor.Width = 600;
                    panelContenedor.Height = 60;
                    //panelContenedor.BackColor = Color.Aqua;
                    panelContenedor.BackColor = Color.LightGray;

                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 594;
                    panelContenido.Height = 55;
                    //panelContenido.BackColor = Color.Brown;

                    Label lblNombreProveedor = new Label();
                    lblNombreProveedor.Name = "lblNombre" + chekBoxClickDetalle.Name.ToString();
                    lblNombreProveedor.Width = 320;
                    lblNombreProveedor.Height = 20;
                    lblNombreProveedor.Location = new Point(3, 32);
                    lblNombreProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombreProveedor.BackColor = Color.White;

                    Label lblRFCProveedor = new Label();
                    lblRFCProveedor.Name = "lblRFC" + chekBoxClickDetalle.Name.ToString();
                    lblRFCProveedor.Width = 110;
                    lblRFCProveedor.Height = 20;
                    lblRFCProveedor.Location = new Point(360, 32);
                    lblRFCProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblRFCProveedor.BackColor = Color.White;

                    Label lblTelProveedor = new Label();
                    lblTelProveedor.Name = "lblTel" + chekBoxClickDetalle.Name.ToString();
                    lblTelProveedor.Width = 90;
                    lblTelProveedor.Height = 20;
                    lblTelProveedor.Location = new Point(500, 32);
                    lblTelProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblTelProveedor.BackColor = Color.White;

                    int XcbProv = 0;
                    CargarProveedores();
                    XcbProv = panelContenido.Width / 2;

                    ComboBox cbProveedor = new ComboBox();
                    cbProveedor.Name = "cb" + chekBoxClickDetalle.Name.ToString();
                    cbProveedor.Width = 400;
                    cbProveedor.Height = 30;
                    cbProveedor.Location = new Point(XcbProv-(cbProveedor.Width/2), 5);
                    cbProveedor.SelectedIndexChanged += new System.EventHandler(cbProveedor_SelectValueChanged);

                    if (listaProveedores.Length > 0)
                    {
                        cbProveedor.DataSource = proveedores.ToArray();
                        cbProveedor.DisplayMember = "Value";
                        cbProveedor.ValueMember = "Key";
                        cbProveedor.SelectedValue = "0";

                        // Cuando se da click en la opcion editar producto
                        if (AgregarEditarProducto.DatosSourceFinal == 2)
                        {
                            var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                            var idProveedor = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                            int cantidad = idProveedor.Length;

                            if (cantidad > 0)
                            {
                                if (Convert.ToInt32(idProveedor[0].ToString()) > 0)
                                {
                                    cbProveedor.SelectedValue = idProveedor;
                                    cargarDatosProveedor(Convert.ToInt32(idProveedor[0]));
                                    if (!datos.Equals(null))
                                    {
                                        lblNombreProveedor.Text = datos[0];
                                        lblRFCProveedor.Text = datos[1];
                                        lblTelProveedor.Text = datos[10];
                                        cbProveedor.Text = datos[0];
                                    }
                                }
                            }
                        }
                    }
                    else if(cbProveedor.Items.Count == 0)
                    {
                        cbProveedor.Items.Add("Proveedores...");
                        cbProveedor.SelectedIndex = 0;
                    }
                    cbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;

                    panelContenido.Controls.Add(cbProveedor);
                    panelContenido.Controls.Add(lblNombreProveedor);
                    panelContenido.Controls.Add(lblRFCProveedor);
                    panelContenido.Controls.Add(lblTelProveedor);

                    panelContenedor.Controls.Add(panelContenido);
                }
                else if (panelContenedor.Name == "panelContenedorCategoria")
                {
                    nombrePanelContenido = "panelContenido" + chekBoxClickDetalle.Name.ToString();

                    panelContenedor.Width = 196;
                    panelContenedor.Height = 60;
                    //panelContenedor.BackColor = Color.Aqua;
                    panelContenedor.BackColor = Color.LightGray;

                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 190;
                    panelContenido.Height = 55;
                    //panelContenido.BackColor = Color.Brown;

                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    Label lblNombreCategoria = new Label();
                    lblNombreCategoria.Name = "lblNombre" + chekBoxClickDetalle.Name.ToString();
                    lblNombreCategoria.Width = 170;
                    lblNombreCategoria.Height = 20;
                    lblNombreCategoria.Location = new Point(XcbProv - (lblNombreCategoria.Width / 2), 32);
                    lblNombreCategoria.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombreCategoria.BackColor = Color.White;
                    
                    CargarCategorias();
                    
                    ComboBox cbCategoria = new ComboBox();
                    cbCategoria.Name = "cb" + chekBoxClickDetalle.Name.ToString();
                    cbCategoria.Width = 170;
                    cbCategoria.Height = 30;
                    cbCategoria.Location = new Point(XcbProv - (cbCategoria.Width / 2), 5);
                    cbCategoria.SelectedIndexChanged += new System.EventHandler(cbCategoria_SelectIndexChanged);

                    if (listaProveedores.Length > 0)
                    {
                        cbCategoria.DataSource = categorias.ToArray();
                        cbCategoria.DisplayMember = "Value";
                        cbCategoria.ValueMember = "Key";
                        cbCategoria.SelectedValue = "0";

                        // Cuando se da click en la opcion editar producto
                        if (AgregarEditarProducto.DatosSourceFinal == 2)
                        {
                            var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                            var idCategoria = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                            int cantidad = idCategoria.Length;

                            if (cantidad > 0)
                            {
                                if (Convert.ToInt32(idCategoria[2].ToString()) > 0)
                                {
                                    var opcion = Convert.ToInt32(cbCategoria.SelectedValue.ToString());

                                    if (opcion > 0)
                                    {
                                        lblNombreCategoria.Text = separadas[1].ToString();
                                    }
                                }
                            }
                        }
                    }
                    else if (cbCategoria.Items.Count == 0)
                    {
                        cbCategoria.Items.Add("Proveedores...");
                        cbCategoria.SelectedIndex = 0;
                    }
                    cbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

                    panelContenido.Controls.Add(cbCategoria);
                    panelContenido.Controls.Add(lblNombreCategoria);

                    panelContenedor.Controls.Add(panelContenido);
                }
                else
                {
                    nombrePanelContenido = "panelContenido" + chekBoxClickDetalle.Name.ToString();

                    panelContenedor.Width = 196;
                    panelContenedor.Height = 60;
                    //panelContenedor.BackColor = Color.AliceBlue;
                    panelContenedor.BackColor = Color.LightGray;

                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 185;
                    panelContenido.Height = 55;
                    //panelContenido.BackColor = Color.Brown;

                    panelContenedor.Controls.Add(panelContenido);
                }
                fLPCentralDetalle.Controls.Add(panelContenedor);
            }
            else if (chekBoxClickDetalle.Checked == false)
            {
                name = chekBoxClickDetalle.Name.ToString();
                value = chekBoxClickDetalle.Checked.ToString();

                encontrarPanel("panelContenedor" + chekBoxClickDetalle.Name.ToString());
            }
            UpdateKey(name, value);
            RefreshAppSettings();
            loadFormConfig();
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
                int idCategoria = 0;

                if (comboBoxIndex > 0)
                {
                    cadena = string.Join("", listaCategorias[comboBoxIndex - 1]);
                    separadas = cadena.Split(delimiterChars);
                    idCategoria = Convert.ToInt32(separadas[0]);
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

            namePanel = comboBox.Name.ToString().Remove(0, 2);

            if (listaProveedores.Length > 0)
            {
                cadena = string.Join("",listaProveedores);
                separadas = cadena.Split('-');
                var idProveedor = Convert.ToInt32(separadas[0]);

                if (idProveedor > 0)
                {
                    cargarDatosProveedor(Convert.ToInt32(idProveedor));
                    llenarDatosProveedor(namePanel);
                }
            }
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
                                    contLblHijo.Text = datos[0];
                                }
                                if (contLblHijo.Name == "lblNombre" + textoBuscado)
                                {
                                    contLblHijo.Text = datos[0];
                                }
                                else if (contLblHijo.Name == "lblRFC" + textoBuscado)
                                {
                                    contLblHijo.Text = datos[1];
                                }
                                else if (contLblHijo.Name == "lblTel" + textoBuscado)
                                {
                                    contLblHijo.Text = datos[10];
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
                datos = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);
            }
        }


        private void encontrarPanel(string panelBuscado)
        {
            foreach (Control contHijo in fLPCentralDetalle.Controls.OfType<Control>())
            {
                if(contHijo.Name == panelBuscado)
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
            UpdateKey(name, value);
            RefreshAppSettings();
            loadFormConfig();
        }

        private void ClickBotonesProductos(object sender, EventArgs e)
        {

        }

        #endregion Modifying Configuration Settings at Runtime

        #region Proveedores Categorias Ubicaciones

        private void CargarProveedores()
        {
            // Asignamos el Array con los nombres de los proveedores al comboBox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            // Comprobar que ya exista al menos un Proveedor
            if (listaProveedores.Length > 0)
            {
                proveedores = new Dictionary<string, string>();

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

        #endregion Proveedores Categorias Ubicaciones

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            loadFormConfig();

            BuscarTextoListView(settingDatabases);

            //verificarCheckboxLista();
            //MessageBox.Show("Variable de Setting:\nPath: " + Properties.Settings.Default.PathDebug + "\nArchivo: " + Properties.Settings.Default.FileDebug, "Variables Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteDetalle_Click(object sender, EventArgs e)
        {
            fLPCentralDetalle.Controls.Clear();
            XPos = this.Width / 2;
            YPos = this.Height / 2;
            deleteDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el Detalle a Eliminar:", "Detalle a Eliminar", "Escriba aquí su Detalle a Eliminar", XPos, YPos);
            try
            {
                if (!deleteDetalle.Equals(""))
                {
                    if (KeyExist(deleteDetalle))
                    {
                        DeleteKey(deleteDetalle);
                        RefreshAppSettings();
                        loadFormConfig();
                        BuscarTextoListView(settingDatabases);
                        deleteDetalle = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el Detalle: " + deleteDetalle + " en los registros", "Error Try Catch Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDetalle_Click(object sender, EventArgs e)
        {
            fLPCentralDetalle.Controls.Clear();
            nvoValor = "false";
            XPos = this.Width / 2;
            YPos = this.Height / 2;
            nvoDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Nuevo Detalle para Agregar:", "Agregar Nuevo Detalle a Mostrar", "Escriba aquí su Nuevo Detalle", XPos, YPos);
            try
            {
                if (!nvoDetalle.Equals(""))
                {
                    AddKey(nvoDetalle, nvoValor);
                    RefreshAppSettings();
                    loadFormConfig();
                    BuscarTextoListView(settingDatabases);
                    editDetelle = string.Empty;
                    editDetalleNvo = string.Empty;
                }
                else if (nvoDetalle.Equals(""))
                {
                    MessageBox.Show("Error al intentar Agregar\nVerifique que el campo Agregar Nuevo Detalle a Mostrar\nNo este Vacio por favor", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar Agregar: " + ex.Message.ToString(), "Error Try Catch Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRenameDetalle_Click(object sender, EventArgs e)
        {
            fLPCentralDetalle.Controls.Clear();
            //XPos = this.Width / 2;
            //YPos = this.Height / 2;
            //editDetelle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Nuevo Detalle para Agregar:", "Agregar Nuevo Detalle a Mostrar", "Escriba aquí su Nuevo Detalle", XPos, YPos);
            RenombrarDetalle renameDetail = new RenombrarDetalle();
            renameDetail.nombreDetalle += new RenombrarDetalle.pasarOldNameNewName(ejecutar);
            renameDetail.ShowDialog();
            if (!KeyExist(editDetalleNvo))
            {
                ReadKey(editDetelle);
                UpdateKey(editDetelle, editValor);
                RefreshAppSettings();
                loadFormConfig();
                BuscarTextoListView(settingDatabases);
            }
            else
            {
                MessageBox.Show("Error al intentar Renombrar\nVerifique que el Nombre del Detalle\nNo este en uso, por favor", "Error al Renombrar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            editDetelle = string.Empty;
            editDetalleNvo = string.Empty;
        }

        private void ejecutar(string oldName, string newName)
        {
            editDetelle = oldName;
            editDetalleNvo = newName;
        }

        //private void verificarCheckboxLista()
        //{
        //    optionList = new List<string>();
        //    foreach (Control cComponente in panelMenu.Controls)
        //    {
        //        if (cComponente is CheckBox)
        //        {
        //            CheckBox chk;
        //            chk = (CheckBox)cComponente;
        //            if (chk.Checked == true)
        //            {
        //                optionList.Add(chk.Text+"-"+chk.Checked.ToString());
        //            }
        //        }
        //    }
        //}

        private void btnGuardarDetalles_Click(object sender, EventArgs e)
        {
            
        }
        
        private void AgregarDetalleProducto_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
