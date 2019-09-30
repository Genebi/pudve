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

        List<string> optionList;

        string[] listaProveedores = new string[] { };
        string[] listaCategorias = new string[] { };
        string[] listaUbicaciones = new string[] { };

        bool habilitarComboBoxes = false;

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

            FlowLayoutPanel panelTitulo = new FlowLayoutPanel();
            panelTitulo.Name = "panelTitulo" + id;
            panelTitulo.Width = 276;
            panelTitulo.Height = 30;
            panelTitulo.HorizontalScroll.Visible = false;

            Label Titulo = new Label();
            Titulo.Name = "Title";
            Titulo.Width = 276;
            Titulo.Height = 20;
            Titulo.Text = "Detalle de producto:";
            Titulo.Location = new Point(0, 0);

            panelTitulo.Controls.Add(Titulo);

            fLPLateralConcepto.Controls.Add(panelTitulo);

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

                CheckBox check = new CheckBox();
                check.Name = chkDetalleProductoTxt;
                check.Text = chkDetalleProductoTxt;
                check.Width = 130;
                check.Height = 24;
                check.Location = new Point(0, 0);
                check.CheckedChanged += checkBox_CheckedChanged;

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
                    bt.Location = new Point(135, 0);
                    panelContenedor.Controls.Add(bt);
                    panelHijo.Controls.Add(panelContenedor);

                    if (row < chkDatabase.Items.Count)
                    {
                        chkSettingVariableTxt = chkDatabase.Items[row].Text.ToString();
                        CheckBox checkSetting = new CheckBox();
                        checkSetting.Name = chkSettingVariableTxt;
                        checkSetting.Width = 20;
                        checkSetting.Height = 24;
                        checkSetting.Location = new Point(165, 0);
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
            string name = string.Empty, value = string.Empty;
            if (chekBoxClickDetalle.Checked == true)
            {
                name = chekBoxClickDetalle.Name.ToString();
                value = chekBoxClickDetalle.Checked.ToString();
            }
            else if (chekBoxClickDetalle.Checked == false)
            {
                name = chekBoxClickDetalle.Name.ToString();
                value = chekBoxClickDetalle.Checked.ToString();
            }
            UpdateKey(name, value);
            RefreshAppSettings();
            loadFormConfig();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDetalle_Click(object sender, EventArgs e)
        {
            int XPos = 0, YPos = 0;
            string nvoDetalle = string.Empty, nvoValor = string.Empty;
            nvoValor = "false";
            XPos = this.Width / 2;
            YPos = this.Height / 2;
            nvoDetalle = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Nuevo Detalle para Agregar:", "Agregar Nuevo Detalle a Mostrar", "Escriba aquí su Nuevo Detalle", XPos, YPos);
            AddKey(nvoDetalle, nvoValor);
            RefreshAppSettings();
            loadFormConfig();
            BuscarTextoListView(settingDatabases);
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
