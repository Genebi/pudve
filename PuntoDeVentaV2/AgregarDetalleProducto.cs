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
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private bool eventoMensajeInventario = false;

        List<string> datosAppSettings;

        string[] datosAppSettingToDB;

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

        public static string finalIdProducto = string.Empty;

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
                chkDetalleProductoTxt = lstListView.Items[i].Text.ToString();
                chkDetalleProductoVal = lstListView.Items[i].SubItems[1].Text.ToString();
                //chkDetalleProductoVal = lstListView.Items[i].Text.ToString();
                //chkDetalleProductoTxt = lstListView.Items[i].SubItems[1].Text.ToString();

                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGenerado" + id;
                panelHijo.Width = 258;
                panelHijo.Height = 29;
                panelHijo.HorizontalScroll.Visible = false;

                Panel panelContenedor = new Panel();
                panelContenedor.Width = 250;
                panelContenedor.Height = 23;
                panelContenedor.Name = "panel" + chkDetalleProductoTxt;

                CheckBox check = new CheckBox();
                check.Name = chkDetalleProductoTxt;
                check.Text = chkDetalleProductoTxt;
                check.Width = 110;
                check.Height = 24;
                check.Location = new Point(0, 0);
                check.CheckedChanged += checkBox_CheckedChanged;

                if (chkDetalleProductoVal.Equals("true") || chkDetalleProductoVal.Equals("false"))
                {
                    check.Checked = Convert.ToBoolean(chkDetalleProductoVal);
                    panelContenedor.Controls.Add(check);
                    panelHijo.Controls.Add(panelContenedor);

                    // Agregamos el Botón de agregar item Más
                    Button bt = new Button();
                    bt.Name = "bt" + chkDetalleProductoTxt;
                    bt.Cursor = Cursors.Hand;
                    bt.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
                    bt.Height = 23;
                    bt.Width = 23;
                    bt.BackColor = ColorTranslator.FromHtml("#5DADE2");
                    bt.ForeColor = ColorTranslator.FromHtml("white");
                    bt.FlatStyle = FlatStyle.Flat;
                    bt.Anchor = AnchorStyles.Top;
                    bt.Click += new EventHandler(bt_Click);
                    bt.Location = new Point(115, 0);
                    panelContenedor.Controls.Add(bt);
                    panelHijo.Controls.Add(panelContenedor);

                    if (row < chkDatabase.Items.Count)
                    {
                        chkSettingVariableTxt = chkDatabase.Items[row].Text.ToString();
                        chkSettingVariableVal = chkDatabase.Items[row].SubItems[1].Text.ToString();
                        //chkSettingVariableVal = chkDatabase.Items[row].Text.ToString();
                        //chkSettingVariableTxt = chkDatabase.Items[row].SubItems[1].Text.ToString();

                        CheckBox checkSetting = new CheckBox();
                        checkSetting.Name = chkSettingVariableTxt;
                        checkSetting.Width = 20;
                        checkSetting.Height = 24;
                        checkSetting.Location = new Point(155, 0);
                        checkSetting.CheckedChanged += checkBoxSetting_CheckedChanged;

                        
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

        private void bt_Click(object sender, EventArgs e)
        {
            Button botonPrecionado = sender as Button;
            string nameBt = string.Empty, textoBuscado = string.Empty;
            nameBt = botonPrecionado.Name;
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
                addDetailGral.ShowDialog();
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
                nombrePanelContenedor = "panelContenedor" + name;
                panelContenedor.Name = nombrePanelContenedor;
                nombrePanelContenido = "panelContenido" + name;

                if (panelContenedor.Name == "panelContenedorProveedor")
                {
                    panelContenedor.Width = 600;
                    panelContenedor.Height = 63;
                    panelContenedor.BackColor = Color.LightGray;

                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 594;
                    panelContenido.Height = 55;

                    Label lblNombreProveedor = new Label();
                    lblNombreProveedor.Name = "lblNombre" + name;
                    lblNombreProveedor.Width = 320;
                    lblNombreProveedor.Height = 20;
                    lblNombreProveedor.Location = new Point(3, 32);
                    lblNombreProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombreProveedor.BackColor = Color.White;

                    Label lblRFCProveedor = new Label();
                    lblRFCProveedor.Name = "lblRFC" + name;
                    lblRFCProveedor.Width = 110;
                    lblRFCProveedor.Height = 20;
                    lblRFCProveedor.Location = new Point(360, 32);
                    lblRFCProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblRFCProveedor.BackColor = Color.White;

                    Label lblTelProveedor = new Label();
                    lblTelProveedor.Name = "lblTel" + name;
                    lblTelProveedor.Width = 90;
                    lblTelProveedor.Height = 20;
                    lblTelProveedor.Location = new Point(500, 32);
                    lblTelProveedor.TextAlign = ContentAlignment.MiddleCenter;
                    lblTelProveedor.BackColor = Color.White;

                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    CargarProveedores();

                    ComboBox cbProveedor = new ComboBox();
                    cbProveedor.Name = "cb" + name;
                    cbProveedor.Width = 400;
                    cbProveedor.Height = 30;
                    cbProveedor.Location = new Point(XcbProv - (cbProveedor.Width / 2), 5);
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
                                if (!idProveedor[1].Equals(""))
                                {
                                    if (Convert.ToInt32(idProveedor[1].ToString()) > 0)
                                    {
                                        cargarDatosProveedor(Convert.ToInt32(idProveedor[1]));
                                        if (!datosProveedor.Equals(null))
                                        {
                                            lblNombreProveedor.Text = datosProveedor[0];
                                            lblRFCProveedor.Text = datosProveedor[1];
                                            lblTelProveedor.Text = datosProveedor[10];
                                            diccionarioDetalleBasicos.Add(contadorIndex, new Tuple<string, string, string, string>(idProveedor[0].ToString(), nombrePanelContenido, idProveedor[0].ToString(), datosProveedor[0].ToString()));
                                            contadorIndex++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (listaProveedores.Length < 0)
                    {
                        cbProveedor.Items.Add("Proveedores...");
                        cbProveedor.SelectedIndex = 0;
                    }
                    else if (cbProveedor.Items.Count == 0)
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
                    fLPCentralDetalle.Controls.Add(panelContenedor);
                }
                else
                {
                    nombrePanelContenido = "panelContenido" + name;

                    panelContenedor.Width = 196;
                    panelContenedor.Height = 63;
                    panelContenedor.BackColor = Color.LightGray;

                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 185;
                    panelContenido.Height = 55;

                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    Label lblNombreDetalleGral = new Label();
                    lblNombreDetalleGral.Name = "lblNombre" + name;
                    lblNombreDetalleGral.Width = 170;
                    lblNombreDetalleGral.Height = 20;
                    lblNombreDetalleGral.Location = new Point(XcbProv - (lblNombreDetalleGral.Width / 2), 32);
                    lblNombreDetalleGral.TextAlign = ContentAlignment.MiddleCenter;
                    lblNombreDetalleGral.BackColor = Color.White;

                    CargarDetallesGral(name);

                    ComboBox cbDetalleGral = new ComboBox();
                    cbDetalleGral.Name = "cb" + name;
                    cbDetalleGral.Width = 170;
                    cbDetalleGral.Height = 30;
                    cbDetalleGral.Location = new Point(XcbProv - (cbDetalleGral.Width / 2), 5);
                    cbDetalleGral.SelectedIndexChanged += new System.EventHandler(cbDetalleGral_SelectIndexChanged);
                    cbDetalleGral.DropDownStyle = ComboBoxStyle.DropDownList;

                    if (listaDetalleGral.Length > 0)
                    {
                        cbDetalleGral.DataSource = detallesGral.ToArray();
                        cbDetalleGral.DisplayMember = "Value";
                        cbDetalleGral.ValueMember = "Key";
                        cbDetalleGral.SelectedValue = "0";
                    }
                    else if (cbDetalleGral.Items.Count == 0)
                    {
                        cbDetalleGral.Items.Add(chekBoxClickDetalle.Name.ToString() + "...");
                        cbDetalleGral.SelectedIndex = 0;
                    }

                    panelContenido.Controls.Add(cbDetalleGral);
                    panelContenido.Controls.Add(lblNombreDetalleGral);
                    panelContenedor.Controls.Add(panelContenido);
                    fLPCentralDetalle.Controls.Add(panelContenedor);

                    // Cuando se da click en la opcion editar producto
                    if (AgregarEditarProducto.DatosSourceFinal == 2)
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
                                                    diccionarioDetallesGeneral.Add(contadorIndex, new Tuple<string, string, string, string>(DetalleGralPorPanel[0].ToString(), nombrePanelContenido, idDetailGral.ToString(), idDetalleGral[2].ToString()));
                                                    contadorIndex++;
                                                    break;
                                                }
                                            }

                                            idDetalleGral = new string[] { };
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (chekBoxClickDetalle.Checked == false)
            {
                name = chekBoxClickDetalle.Name.ToString();
                value = chekBoxClickDetalle.Checked.ToString();

                encontrarPanel("panelContenedor" + name);
            }

            //int valorDatoDinamico = -1;

            //if (value.Equals("True"))
            //{
            //    valorDatoDinamico = 1;
            //}
            //else if (value.Equals("False"))
            //{
            //    valorDatoDinamico = 0;
            //}

            //var UpdateDatoValueDinamico = cn.EjecutarConsulta(cs.ActualizarDatoValueDinamico(name, valorDatoDinamico, FormPrincipal.userID));

            //loadFromConfigDB();

            UpdateKey(name, value);
            RefreshAppSettings();
            loadFormConfig();

            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    saveConfigIntoDB();
            //}
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
                }

                if (idDetalleGral > 0)
                {
                    //cargarDatosProveedor(Convert.ToInt32(idCategoria));
                    llenarDatosDetalleGral(namePanel);
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
                                    contLblHijo.Text = separadas[1].ToString();
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
                }
                else if (comboBoxIndex <= 0)
                {
                    idProveedor = 0;
                }

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

            //loadFromConfigDB();

            UpdateKey(name, value);
            RefreshAppSettings();
            loadFormConfig();

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
                detallesGral.Add("0", concepto + "...");

                foreach (var DetailGral in listaDetalleGral)
                {
                    var auxiliar = DetailGral.Split('|');

                    detallesGral.Add(auxiliar[0], auxiliar[1]);
                }
            }
            else
            {
                detallesGral.Add("0", concepto + "...");
            }
        }

        #endregion Proveedores Categorias Ubicaciones

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            limpiarStockMinimoMaximo();

            finalIdProducto = getIdProducto;

            var servidor = Properties.Settings.Default.Hosting;

            loadFormConfig();
            //loadFromConfigDB();
            BuscarTextoListView(settingDatabases);

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    VerificarFiltroDinamico();
            //}

            txtStockNecesario.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtStockMinimo.KeyPress += new KeyPressEventHandler(SoloDecimales);

            if (!string.IsNullOrEmpty(AgregarEditarProducto.stockNecesario))
            {
                int stockTmp = Convert.ToInt32(AgregarEditarProducto.stockNecesario);

                if (stockTmp > 0)
                {
                    txtStockNecesario.Text = stockTmp.ToString();
                }
            }

            if (!string.IsNullOrEmpty(AgregarEditarProducto.stockMinimo))
            {
                int stockTmp = Convert.ToInt32(AgregarEditarProducto.stockMinimo);

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
            else if (finalIdProducto.Equals(""))
            {
                txtStockNecesario.Text = "0";
                txtStockMinimo.Text = "0";
            }
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
                                                catch(Exception ex)
                                                {
                                                    MessageBox.Show("Error al intentar Actualizar " + nameChkBox.Remove(0,3) + "\nen la Base de Datos\nERROR: " + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            RegistroAgregado = cn.EjecutarConsulta(cs.InsertaDatoDinamico("chkProveedor", 0, FormPrincipal.userID));
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
            txtStockNecesario.Text = string.Empty;
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
            XPos = this.Width / 2;
            YPos = this.Height / 2;
            deleteDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el Detalle a Eliminar:", "Detalle a Eliminar", "Escriba aquí su Detalle a Eliminar", XPos, YPos);
            try
            {
                fLPCentralDetalle.Controls.Clear();
                if (nvoDetalle.Equals("Escriba aquí su Detalle a Eliminar"))
                {
                    RefreshAppSettings();
                    loadFormConfig();
                    //loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                    MessageBox.Show("Error al eliminar detalle\nVerifique que el campo Eliminar Detalle a Mostrar\nTenga un nombre valido", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!deleteDetalle.Equals(""))
                {
                    if (deleteDetalle.Equals("Proveedor"))
                    {
                        var mensaje = deleteDetalle;

                        MessageBox.Show("No se puede Renombrar ó Eliminar\n(" + mensaje + ")\nya que es la configuración basica\nUsted esta Intentando realizar dicha operacion\nsobre la configuración: " + deleteDetalle.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RefreshAppSettings();
                        loadFormConfig();
                        //loadFromConfigDB();
                        BuscarTextoListView(settingDatabases);
                    }
                    else
                    {
                        //int found = -1;
                        //using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(deleteDetalle, FormPrincipal.userID)))
                        //{
                        //    if (!dtItemDinamicos.Rows.Count.Equals(0))
                        //    {
                        //        found = 1;
                        //    }
                        //    else if (dtItemDinamicos.Rows.Count.Equals(0))
                        //    {
                        //        found = 0;
                        //    }
                        //}

                        //if (found.Equals(1))
                        //{
                        //    fLPCentralDetalle.Controls.Clear();

                        //    var DeleteDatoDinamicos = cn.EjecutarConsulta(cs.BorrarDatoDinamico(deleteDetalle, FormPrincipal.userID));
                        //    if (DeleteDatoDinamicos.Equals(1))
                        //    {
                        //        MessageBox.Show("Dato Borrado con exito de la base de datos", "Dato Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    else if (DeleteDatoDinamicos.Equals(0))
                        //    {
                        //        //MessageBox.Show("Error al Borrar Dato de la base de datos", "Error de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    }

                        //    var DeleteDatoFiltroDinamico = cn.EjecutarConsulta(cs.BorrarDatoFiltroDinamico(deleteDetalle, FormPrincipal.userID));
                        //    if (DeleteDatoFiltroDinamico.Equals(1))
                        //    {
                        //        MessageBox.Show("Dato Borrado con exito de la base de datos", "Dato Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    else if (DeleteDatoFiltroDinamico.Equals(0))
                        //    {
                        //        //MessageBox.Show("Error al Borrar Dato de la base de datos", "Error de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    }

                        //    loadFormConfig();
                        //    //loadFromConfigDB();
                        //    BuscarTextoListView(settingDatabases);
                        //    deleteDetalle = string.Empty;
                        //}
                        //else if (found.Equals(0))
                        //{
                        //    RefreshAppSettings();
                        //    loadFormConfig();
                        //    //loadFromConfigDB();
                        //    BuscarTextoListView(settingDatabases);
                        //    MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}

                        if (KeyExist(deleteDetalle))
                        {
                            DeleteKey(deleteDetalle);
                            RefreshAppSettings();
                            //loadFormConfig();

                            //var DeleteDatoDinamicos = cn.EjecutarConsulta(cs.BorrarDatoDinamico(deleteDetalle, FormPrincipal.userID));
                            //if (DeleteDatoDinamicos.Equals(1))
                            //{
                            //    MessageBox.Show("Dato Borrado con exito de la base de datos", "Dato Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //else if (DeleteDatoDinamicos.Equals(0))
                            //{
                            //    //MessageBox.Show("Error al Borrar Dato de la base de datos", "Error de Borrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //}

                            fLPCentralDetalle.Controls.Clear();
                            loadFormConfig();
                            //loadFromConfigDB();
                            BuscarTextoListView(settingDatabases);
                            deleteDetalle = string.Empty;
                        }
                        else
                        {
                            RefreshAppSettings();
                            loadFormConfig();
                            //loadFromConfigDB();
                            BuscarTextoListView(settingDatabases);
                            MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //var DeleteDatoDinamico = cn.EjecutarConsulta(cs.BorrarDatoDinamico(deleteDetalle, FormPrincipal.userID));
                        //if (DeleteDatoDinamico.Equals(1))
                        //{
                        //    //MessageBox.Show("Dato Borrado con exito de la base de datos", "Dato Borrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (DeleteDatoDinamico.Equals(0))
                        //{
                        //    MessageBox.Show("El Detalle: " + deleteDetalle + " a eliminar no se encuentra en los registros", "Error al Eliminar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}

                        //loadFromConfigDB();
                        //BuscarTextoListView(settingDatabases);
                        deleteDetalle = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                RefreshAppSettings();
                loadFormConfig();
                //loadFromConfigDB();
                BuscarTextoListView(settingDatabases);
                MessageBox.Show("Error al eliminar el Detalle: " + deleteDetalle + " en los registros", "Error Try Catch Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDetalle_Click(object sender, EventArgs e)
        {
            nvoValor = "false";
            XPos = this.Width / 2;
            YPos = this.Height / 2;
            nvoDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Nuevo Detalle para Agregar:", "Agregar Nuevo Detalle a Mostrar", "Escriba aquí su Nuevo Detalle", XPos, YPos);
            try
            {
                int found = -1;
                fLPCentralDetalle.Controls.Clear();
                if (nvoDetalle.Equals("Escriba aquí su Nuevo Detalle"))
                {
                    RefreshAppSettings();
                    loadFormConfig();
                    //loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                    MessageBox.Show("Error al intentar Agregar\nVerifique que el campo Agregar Nuevo Detalle a Mostrar\nTenga un nombre valido", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!nvoDetalle.Equals(""))
                {
                    AddKey(nvoDetalle, nvoValor);
                    RefreshAppSettings();
                    loadFormConfig();
                    using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(nvoDetalle, FormPrincipal.userID)))
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
                        MessageBox.Show("El Registro que Intenra ya esta registrado\nfavor de verificar o intentar con otro Detalle", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (found.Equals(0))
                    {
                        int nvoValorNumerico = 0;
                        int RegistroAgregado = -1;

                        RegistroAgregado = cn.EjecutarConsulta(cs.InsertaDatoDinamico(nvoDetalle, nvoValorNumerico, FormPrincipal.userID));
                        if (RegistroAgregado.Equals(1))
                        {
                            //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (RegistroAgregado.Equals(0))
                        {
                            MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        RegistroAgregado = cn.EjecutarConsulta(cs.InsertarDatoFiltroDinamico("chk" + nvoDetalle, nvoValorNumerico, FormPrincipal.userID));
                        if (RegistroAgregado.Equals(1))
                        {
                            //MessageBox.Show("Registro de Detalle Dinamico\nExitoso...", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (RegistroAgregado.Equals(0))
                        {
                            MessageBox.Show("Error al Intentar Agregar Registro de Detalle Dinamico...\nEn la tabla FiltroPrducto", "Registro Fallido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                    editDetelle = string.Empty;
                    editDetalleNvo = string.Empty;
                }
                else if (nvoDetalle.Equals(""))
                {
                    RefreshAppSettings();
                    loadFormConfig();
                    //loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                    MessageBox.Show("Error al intentar Agregar\nVerifique que el campo Agregar Nuevo Detalle a Mostrar\nNo este Vacio por favor", "Error al Agregar Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                RefreshAppSettings();
                loadFormConfig();
                //loadFromConfigDB();
                BuscarTextoListView(settingDatabases);
                MessageBox.Show("Error al intentar Agregar: " + ex.Message.ToString(), "Error Try Catch Nuevo Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRenameDetalle_Click(object sender, EventArgs e)
        {
            RenombrarDetalle renameDetail = new RenombrarDetalle();
            renameDetail.nombreDetalle += new RenombrarDetalle.pasarOldNameNewName(ejecutar);
            renameDetail.ShowDialog();
            //try
            //{
            //    int found = -1;
            //    using (DataTable dtItemDinamicos = cn.CargarDatos(cs.VerificarDatoDinamico(editDetalleNvo, FormPrincipal.userID)))
            //    {
            //        if (dtItemDinamicos.Rows.Count.Equals(0))
            //        {
            //            found = 1;
            //        }
            //        else if (dtItemDinamicos.Rows.Count.Equals(0))
            //        {
            //            found = 0;
            //        }
            //    }
            //    if (found.Equals(1))
            //    {
            //        if (editDetelle.Equals("Proveedor"))
            //        {
            //            var mensaje = editDetelle;

            //            MessageBox.Show("No se puede Renombrar ó Eliminar\n(" + mensaje + ")\nya que es la configuración basica\nUsted esta Intentando realizar dicha operacion\nsobre la configuración: " + editDetelle.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            fLPCentralDetalle.Controls.Clear();
            //            //RefreshAppSettings();
            //            //loadFormConfig();
            //            loadFromConfigDB();
            //            BuscarTextoListView(settingDatabases);
            //        }
            //        else
            //        {
            //            fLPCentralDetalle.Controls.Clear();
            //            //ReadKey(editDetelle);
            //            //RenameKey(editDetelle, editValor);
            //            //RefreshAppSettings();
            //            //loadFormConfig();

            //            var UpdateDatoDinamico = cn.EjecutarConsulta(cs.ActualizarDatoDinamico(editDetelle, editDetalleNvo, FormPrincipal.userID));
            //            if (UpdateDatoDinamico.Equals(1))
            //            {
            //                //MessageBox.Show("Actualización de Detalle Dinamico\nExitoso...", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //            else if (UpdateDatoDinamico.Equals(0))
            //            {
            //                MessageBox.Show("Error al Intentar Actualizar Registro de Detalle Dinamico...", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }

            //            var UpdateDatoFiltroDinamico = cn.EjecutarConsulta(cs.ActualizarNombreDatoFiltroDinamico(editDetelle, editDetalleNvo, FormPrincipal.userID));
            //            if (UpdateDatoFiltroDinamico.Equals(1))
            //            {
            //                //MessageBox.Show("Actualización de Detalle Dinamico\nExitoso...", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            }
            //            else if (UpdateDatoFiltroDinamico.Equals(0))
            //            {
            //                MessageBox.Show("Error al Intentar Actualizar Registro de Filtro Dinamico...", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }

            //            loadFromConfigDB();
            //            BuscarTextoListView(settingDatabases);
            //        }
            //    }
            //    else if (found.Equals(0))
            //    {
            //        //RefreshAppSettings();
            //        //loadFormConfig();
            //        loadFromConfigDB();
            //        BuscarTextoListView(settingDatabases);
            //        MessageBox.Show("Error al intentar Renombrar\nVerifique que el Nombre del Detalle\nNo este en uso, por favor", "Error al Renombrar Detalle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al Intentar Renombrar un Concepto Dinamico:\n" + ex.Message.ToString(), "Error al Renombrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //editDetelle = string.Empty;
            //editDetalleNvo = string.Empty;
            if (!KeyExist(editDetalleNvo))
            {
                if (editDetelle.Equals("Proveedor"))
                {
                    var mensaje = editDetelle;

                    MessageBox.Show("No se puede Renombrar ó Eliminar\n(" + mensaje + ")\nya que es la configuración basica\nUsted esta Intentando realizar dicha operacion\nsobre la configuración: " + editDetelle.ToString(),
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fLPCentralDetalle.Controls.Clear();
                    RefreshAppSettings();
                    loadFormConfig();
                    //loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                }
                else
                {
                    fLPCentralDetalle.Controls.Clear();
                    ReadKey(editDetelle);
                    RenameKey(editDetelle, editValor);
                    RefreshAppSettings();
                    loadFormConfig();
                    //loadFromConfigDB();
                    BuscarTextoListView(settingDatabases);
                    var UpdateDatoDinamico = cn.EjecutarConsulta(cs.ActualizarDatoDinamico(editDetelle, editDetalleNvo, FormPrincipal.userID));
                    if (UpdateDatoDinamico.Equals(1))
                    {
                        //MessageBox.Show("Actualización de Detalle Dinamico\nExitoso...", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (UpdateDatoDinamico.Equals(0))
                    {
                        MessageBox.Show("Error al Intentar Actualizar Registro de Detalle Dinamico...", "Actualización Fallida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                RefreshAppSettings();
                loadFormConfig();
                //loadFromConfigDB();
                BuscarTextoListView(settingDatabases);
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

            AgregarEditarProducto.stockNecesario = stockNecesario;
            AgregarEditarProducto.stockMinimo = stockMinimo;

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
            // Variables para poder hacer el control 
            // de registro y actualizacion de la tabla
            string  Descripcion = string.Empty,
                    panel = string.Empty;

            // lista para almacenar los datos
            // para el nvo registro o actualizacion
            infoDetailProdGral = new List<string>();

            // Recorremos los controles en el FlowLayoutPanelCentral(Principal)
            foreach (Control contHijo in fLPCentralDetalle.Controls)
            {
                // Obtenemos el nombre del del Control = PanelContenedor
                panel = contHijo.Name;
                // Verificamos si el nombre del PanelContenedor no sea Igual a panelContenedorProveedor
                if (!contHijo.Name.Equals("panelContenedorProveedor"))
                {
                    // Recorremos los controles en el PanelContenedor
                    foreach (Control contSubHijo in contHijo.Controls)
                    {
                        // Verificamos si el PanelContenido ya existe en el Diccionario de Detalle General
                        // sí ya esta el registro del PanelContenido la variable sera true
                        // sí no esta el registro del PanelContenido la variable sera false
                        bool alreadyStoredPanelConteido = diccionarioDetallesGeneral.Any(x => x.Value.Item2 == contSubHijo.Name);
                        // sí es True
                        if (alreadyStoredPanelConteido)
                        {
                            infoDetailProdGral.Clear();
                            // Recorremos los controles en el PanelContenido
                            foreach (Control contSubItemHijo in contSubHijo.Controls)
                            {
                                // Verificamos si el control es de tipo Label
                                if (contSubItemHijo is Label)
                                {
                                    // Verificamos si la Descripcion ya existe en el Diccionario de Detalle General
                                    // sí ya esta el registro de la Descripcion la variable sera true
                                    // sí no esta el registro de la Descripcion la variable sera false
                                    bool alreadyStoredDescripcion = diccionarioDetallesGeneral.Any(x => x.Value.Item4 == contSubItemHijo.Text);
                                    // sí es True
                                    if (alreadyStoredDescripcion)
                                    {
                                        // No se han detectado cambios en los registros
                                        break;
                                    }
                                    // sí es False
                                    else if (!alreadyStoredDescripcion)
                                    {
                                        // Se han detectado cambios en alguno de los registros
                                        Descripcion = contSubItemHijo.Text;
                                        // recorremos el diccionario Detalles Generales
                                        foreach (var item in diccionarioDetallesGeneral)
                                        {
                                            // comparamos si el nombre del Panel es el mismo que el del Diccionario
                                            if (item.Value.Item2.Equals(contSubHijo.Name))
                                            {
                                                // verificamos que sea diferente la Descripcion
                                                if (!item.Value.Item4.Equals(Descripcion))
                                                {
                                                    // agregamos el ID
                                                    infoDetailProdGral.Add(item.Value.Item1.ToString());
                                                    break;
                                                }
                                            }
                                        }
                                        // Obtenemos todos los datos del Detalle en General
                                        var idFound = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, Descripcion);
                                        // Obtenemos el ID del Detalle General
                                        infoDetailProdGral.Add(idFound[0].ToString());
                                        // Ejecutamos el proceso de guardado
                                        try
                                        {
                                            guardar = infoDetailProdGral.ToArray();
                                            cn.EjecutarConsulta(cs.ActualizarDetallesProductoGenerales(guardar));
                                            infoDetailProdGral.Clear();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("El proceso de actualizacion de Detalles Del Producto Generales\nocurrio un error:\n" + ex.Message.ToString(), "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        // sí es False
                        else if (!alreadyStoredPanelConteido)
                        {
                            infoDetailProdGral.Clear();
                            // Se ha detectado un nuevo Registro
                            // Recorremos los controles en el PanelContenido
                            foreach (Control contSubItemHijo in contSubHijo.Controls)
                            {
                                // Verificamos si el control es de tipo Label
                                if (contSubItemHijo is Label)
                                {
                                    if (!finalIdProducto.Equals(""))
                                    {
                                        // Se almacenan los datos para 
                                        // el posterior registro
                                        infoDetailProdGral.Add(finalIdProducto);
                                        infoDetailProdGral.Add(FormPrincipal.userID.ToString());
                                        Descripcion = contSubItemHijo.Text;
                                        var idFound = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, Descripcion);
                                        if (idFound.Length > 0)
                                        {
                                            infoDetailProdGral.Add(idFound[0].ToString());
                                            infoDetailProdGral.Add("1");
                                            infoDetailProdGral.Add("panelContenido" + idFound[2].ToString());
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
                                            break;
                                        }
                                    }
                                    else if (finalIdProducto.Equals(""))
                                    {
                                        //MessageBox.Show("Pasar la informacion a Agregar/Editar Productos", "En Construcción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        infoDetailProdGral.Add(finalIdProducto);
                                        infoDetailProdGral.Add(Convert.ToString(FormPrincipal.userID));
                                        Descripcion = contSubItemHijo.Text;
                                        var idFound = mb.obtenerIdDetalleGeneral(FormPrincipal.userID, Descripcion);

                                        if (idFound.Length > 0)
                                        {
                                            infoDetailProdGral.Add(idFound[2].ToString()); // Obtenemos el ChckName Detalles Basicos
                                            infoDetailProdGral.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                            infoDetailProdGral.Add(idFound[3].ToString()); // Obtenemos la Descripcion del Detalles Basicos
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

        private void saveProdDetail()
        {
            // Variables para poder hacer el control 
            // de registro y actualizacion de la tabla
            string  Descripcion = string.Empty,
                    panel = string.Empty;

            // lista para almacenar los datos
            // para el nvo registro o actualizacion
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
                        // Verificamos si el PanelContenido ya existe en el Diccionario de Detalles Basicos
                        // sí ya esta el registro del PanelContenido la variable sera true
                        // sí no esta el registro del PanelContenido la variable sera false
                        bool alreadyStoredPanelConteido = diccionarioDetalleBasicos.Any(x => x.Value.Item2 == contSubHijo.Name);
                        // sí es True
                        if (alreadyStoredPanelConteido)
                        {
                            // verificamos que sea el PanelContenido sea el de Proveedor
                            if (contSubHijo.Name.Equals("panelContenidoProveedor"))
                            {
                                // Recorremos los controles en el PanelContenido
                                foreach (Control contSubItemHijo in contSubHijo.Controls)
                                {
                                    // Verificamos si el control es de tipo Label
                                    if (contSubItemHijo is Label)
                                    {
                                        // Verificamos si el nombre del label es lblNombreProveedor
                                        // esto soló para que haga este proceso en el label del
                                        // nombre y no en el resto de los label
                                        if (contSubItemHijo.Name.Equals("lblNombreProveedor"))
                                        {
                                            // Verificamos si la Descripcion ya existe en el Diccionario de Detalles Basicos
                                            // sí ya esta el registro de la Descripcion la variable sera true
                                            // sí no esta el registro de la Descripcion la variable sera false
                                            bool alreadyStoredDescripcion = diccionarioDetalleBasicos.Any(x => x.Value.Item4 == contSubItemHijo.Text);
                                            // sí es True
                                            if (alreadyStoredDescripcion)
                                            {
                                                // No se han detectado cambios en los registros
                                                break;
                                            }
                                            // sí es False
                                            else if (!alreadyStoredDescripcion)
                                            {
                                                // recorremos el diccionario Detalles Basicos
                                                foreach (var item in diccionarioDetalleBasicos)
                                                {
                                                    // verificamos que la descripcion no es igual
                                                    if (!item.Value.Item4.Equals(contSubHijo.Text))
                                                    {
                                                        // Se han detectado cambios en alguno de los registros
                                                        Descripcion = contSubItemHijo.Text;
                                                        // comparamos si el nombre del Panel es el mismo que el del Diccionario
                                                        if (item.Value.Item2.Equals(contSubHijo.Name))
                                                        {
                                                            // verificamos que sea diferente la Descripcion
                                                            if (!item.Value.Item4.Equals(Descripcion))
                                                            {

                                                                // agregamos el ID
                                                                infoDetalle.Add(item.Value.Item1.ToString());
                                                            }
                                                            // Obtenemos todos los datos del Detalles en Basicos
                                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, Descripcion);
                                                            infoDetalle.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                            infoDetalle.Add(idFound[2].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                            // Ejecutamos el proceso de guardado
                                                            try
                                                            {
                                                                // Convertimos la Lista lo que tengan almacenado
                                                                // a un Array para guardarlo en la variable guardar
                                                                guardar = infoDetalle.ToArray();
                                                                // usamos la variable guardar para hacer el
                                                                // query para poder hacer la actualizacion
                                                                cn.EjecutarConsulta(cs.ActualizarProveedorDetallesDelProducto(guardar));
                                                                infoDetalle.Clear(); // limpiamos de informacion la Lista
                                                                cn.EjecutarConsulta(cs.ActualizarTextConceptoFiltroDinamico("chkProveedor", FormPrincipal.userID, idFound[2].ToString()));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                // si hubo algun error de actualización se envia un mensaje al usuario
                                                                MessageBox.Show("El proceso de actualizacion de Detalles Del Producto\nocurrio un error:\n" + ex.Message.ToString(), "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
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
                        // sí es False
                        else if (!alreadyStoredPanelConteido)
                        {
                            bool resultadoConsulta;
                            try
                            {
                                resultadoConsulta = (bool)cn.EjecutarSelect(cs.VerificarDetallesProducto(finalIdProducto, Convert.ToString(FormPrincipal.userID)));
                                if (resultadoConsulta == false)
                                {
                                    // verificamos que sea el PanelContenido sea el de Proveedor
                                    if (contSubHijo.Name.Equals("panelContenidoProveedor"))
                                    {
                                        // Recorremos los controles en el PanelContenido
                                        foreach (Control contSubItemHijo in contSubHijo.Controls)
                                        {
                                            // Verificamos si el control es de tipo Label
                                            if (contSubItemHijo is Label)
                                            {
                                                // Verificamos si el nombre del label es lblNombreProveedor
                                                // esto soló para que haga este proceso en el label del
                                                // nombre y no en el resto de los label
                                                if (contSubItemHijo.Name.Equals("lblNombreProveedor"))
                                                {
                                                    if (!contSubItemHijo.Text.Equals(""))
                                                    {
                                                        if (!finalIdProducto.Equals(""))
                                                        {
                                                            infoDetalle.Add(finalIdProducto);
                                                            infoDetalle.Add(Convert.ToString(FormPrincipal.userID));
                                                            Descripcion = contSubItemHijo.Text;
                                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, Descripcion);
                                                            infoDetalle.Add(idFound[2].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                            infoDetalle.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                            // Ejecutamos el proceso de guardado
                                                            try
                                                            {
                                                                // Convertimos la Lista lo que tengan almacenado
                                                                // a un Array para guardarlo en la variable guardar
                                                                guardar = infoDetalle.ToArray();
                                                                // usamos la variable guardar para hacer el
                                                                // query para poder hacer la actualizacion
                                                                cn.EjecutarConsulta(cs.GuardarProveedorDetallesDelProducto(guardar));
                                                                infoDetalle.Clear(); // limpiamos de informacion la Lista
                                                                cn.EjecutarConsulta(cs.GuardarTextConceptoFiltroDinamico("chkProveedor", 0, idFound[2].ToString(), FormPrincipal.userID));
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                // si hubo algun error de actualización se envia un mensaje al usuario
                                                                MessageBox.Show("El proceso de actualizacion de Detalles Del Producto\nocurrio un error:\n" + ex.Message.ToString(), "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                        }
                                                        else if (finalIdProducto.Equals(""))
                                                        {
                                                            //MessageBox.Show("Pasar la informacion a Agregar/Editar Productos", "En Construcción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            infoDetalle.Add(finalIdProducto);
                                                            infoDetalle.Add(Convert.ToString(FormPrincipal.userID));
                                                            Descripcion = contSubItemHijo.Text;
                                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, Descripcion);
                                                            infoDetalle.Add(idFound[2].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                            infoDetalle.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                            AgregarEditarProducto.detalleProductoBasico = infoDetalle;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (resultadoConsulta == true)
                                {
                                    // Verificamos si el nombre del PanelContenedor que sea Igual a panelContenedorProveedor
                                    if (contHijo.Name.Equals("panelContenedorProveedor"))
                                    {
                                        // Recorremos los controles en el PanelContenedor
                                        foreach (Control contSubHijoUpdate in contHijo.Controls)
                                        {
                                            // verificamos que sea el PanelContenido sea el de Proveedor
                                            if (contSubHijo.Name.Equals("panelContenidoProveedor"))
                                            {
                                                // Recorremos los controles en el PanelContenido
                                                foreach (Control contSubItemHijo in contSubHijo.Controls)
                                                {
                                                    // Verificamos si el control es de tipo Label
                                                    if (contSubItemHijo is Label)
                                                    {
                                                        // Verificamos si el nombre del label es lblNombreProveedor
                                                        // esto soló para que haga este proceso en el label del
                                                        // nombre y no en el resto de los label
                                                        if (contSubItemHijo.Name.Equals("lblNombreProveedor"))
                                                        {
                                                            // recorremos el diccionario Detalles Basicos
                                                            Descripcion = contSubItemHijo.Text;
                                                            // Obtenemos todos los datos del Detalles en Basicos
                                                            var idFound = mb.obtenerIdDetallesProveedor(FormPrincipal.userID, Descripcion);
                                                            infoDetalle.Add(idFound[0].ToString()); // Obtenemos el ID del Detalles Basicos
                                                            infoDetalle.Add(idFound[2].ToString()); // Obtenemos la Descripcion del Detalles Basicos
                                                            try
                                                            {
                                                                // Convertimos la Lista lo que tengan almacenado
                                                                // a un Array para guardarlo en la variable guardar
                                                                guardar = infoDetalle.ToArray();
                                                                // usamos la variable guardar para hacer el
                                                                // query para poder hacer la actualizacion
                                                                cn.EjecutarConsulta(cs.ActualizarProveedorDetallesDelProducto(guardar));
                                                                infoDetalle.Clear(); // limpiamos de informacion la Lista
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                // si hubo algun error de actualización se envia un mensaje al usuario
                                                                MessageBox.Show("El proceso de actualizacion de Detalles Del Producto\nocurrio un error:\n" + ex.Message.ToString(), "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // si hubo algun error de actualización se envia un mensaje al usuario
                                MessageBox.Show("En el proceso de registrar Nuevo de Detalles Del Producto\nocurrio un error:\n" + ex.Message.ToString(), "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    }
}
