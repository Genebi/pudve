using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class WinQueryString : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        bool filtroStock,
                filtroPrecio,
                filtroProveedor;

        string strFiltroStock = string.Empty,
                strFiltroPrecio = string.Empty,
                strFiltroProveedor = string.Empty,
                strOpcionCBStock = string.Empty,
                strOpcionCBPrecio = string.Empty,
                strOpcionCBProveedor = string.Empty,
                strTxtStock = string.Empty,
                strTxtPrecio = string.Empty;

        string[] listaProveedores = new string[] { },
                    listaCategorias = new string[] { },
                    listaUbicaciones = new string[] { },
                    listaDetalleGral = new string[] { };

        string[] datosProveedor,
                    datosCategoria,
                    datosUbicacion,
                    datosDetalleGral,
                    separadas,
                    guardar;

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
                nombreUbicacion = string.Empty;

        Dictionary<string, string> proveedores,
                                   categorias,
                                   ubicaciones,
                                   detallesGral;

        static public List<string> detalleProductoBasico = new List<string>();
        static public List<string> detalleProductoGeneral = new List<string>();

        Dictionary<string, Tuple<string, string, string, string>> diccionarioDetalleBasicos = new Dictionary<string, Tuple<string, string, string, string>>();

        string saveDirectoryFile;

        DataTable dtProveedor;

        public WinQueryString()
        {
            InitializeComponent();
        }

        private void txtCantStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Soló son permitidos numeros\nen este campo de Stock",
                                "Error de captura del Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actualizarVistaDetallesProducto()
        {
            loadFormConfig();
            BuscarChkBoxListView(chkDatabase);
            bool isEmpty = !detalleProductoBasico.Any();
            if (!isEmpty)
            {
                // Cuando se da click en la opcion editar producto
                //if (DatosSourceFinal == 1)
                //{
                    string Descripcion = string.Empty,
                            name = string.Empty,
                            value = string.Empty,
                            namegral = string.Empty;

                    for (int i = 0; i < chkDatabase.Items.Count; i++)
                    {
                        name = chkDatabase.Items[i].Text.ToString();
                        Text.ToString();
                        value = chkDatabase.Items[i].SubItems[1].Text.ToString();
                        foreach (Control contHijo in fLPDetalleProducto.Controls)
                        {
                            foreach (Control contSubHijo in contHijo.Controls)
                            {
                                if (contSubHijo.Name.Equals("panelContenido" + name) && value.Equals("true"))
                                {
                                    foreach (Control contItemSubHijo in contSubHijo.Controls)
                                    {
                                        if (contItemSubHijo is Label)
                                        {
                                            contItemSubHijo.Text = detalleProductoBasico[2].ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < chkDatabase.Items.Count; i++)
                    {
                        name = chkDatabase.Items[i].Text.ToString().Remove(0, 3);
                        value = chkDatabase.Items[i].SubItems[1].Text.ToString();
                        foreach (Control contHijo in fLPDetalleProducto.Controls)
                        {
                            foreach (Control contSubHijo in contHijo.Controls)
                            {
                                if (contSubHijo.Name.Equals("panelContenido" + name) && value.Equals("true"))
                                {
                                    for (int j = 0; j < detalleProductoGeneral.Count; j++)
                                    {
                                        namegral = detalleProductoGeneral[j].ToString();
                                        if (namegral.Equals(name) &&
                                                    contSubHijo.Name.Equals("panelContenido" + name))
                                        {
                                            foreach (Control contItemSubHijo in contSubHijo.Controls)
                                            {
                                                if (contItemSubHijo is Label)
                                                {
                                                    contItemSubHijo.Text = detalleProductoGeneral[j + 2].ToString();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                //}
            }
        }

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
                    MessageBox.Show("Lectura App.Config/AppSettings: La Sección de AppSettings está vacia",
                                    "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(),
                                "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarChkBoxListView(ListView lstListView)
        {
            int id = 0,
                row = 0;

            string  nameChk = string.Empty,
                    valorChk = string.Empty,
                    chkSettingVariableTxt = string.Empty,
                    chkSettingVariableVal = string.Empty,
                    name = string.Empty,
                    value = string.Empty,
                    nombrePanelContenedor = string.Empty,
                    nombrePanelContenido = string.Empty;

            fLPDetalleProducto.Controls.Clear();

            for (int i = 0; i < lstListView.Items.Count; i++)
            {
                name = lstListView.Items[i].Text.ToString();
                value = lstListView.Items[i].SubItems[1].Text.ToString();

                if (name.Equals("chkProveedor") && value.Equals("true"))
                {
                    nombrePanelContenedor = "panelContenedor" + name;
                    nombrePanelContenido = "panelContenido" + name;

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 480;
                    panelContenedor.Height = 40;
                    panelContenedor.Name = nombrePanelContenedor;
                    //panelContenedor.BackColor = Color.Aqua;

                    chkSettingVariableTxt = lstListView.Items[i].Text.ToString();
                    chkSettingVariableVal = lstListView.Items[i].SubItems[1].Text.ToString();

                    if (chkSettingVariableVal.Equals("true"))
                    {
                        name = chkSettingVariableTxt;
                        value = chkSettingVariableVal;
                        Panel panelContenido = new Panel();
                        panelContenido.Name = nombrePanelContenido;
                        panelContenido.Width = 476;
                        panelContenido.Height = 38;
                        //panelContenido.BackColor = Color.Red;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarProveedores();

                        ComboBox cbProveedor = new ComboBox();
                        cbProveedor.Name = "cb" + name;
                        cbProveedor.Width = 336;
                        cbProveedor.Height = 21;
                        cbProveedor.Location = new Point(119, 10);
                        cbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
                        cbProveedor.SelectedIndexChanged += new System.EventHandler(comboBoxProveedor_SelectValueChanged);

                        if (listaProveedores.Length > 0)
                        {
                            cbProveedor.DataSource = proveedores.ToArray();
                            cbProveedor.DisplayMember = "Value";
                            cbProveedor.ValueMember = "Key";
                            cbProveedor.SelectedValue = "0";
                        }
                        else if (listaProveedores.Length < 0)
                        {
                            cbProveedor.Items.Add("Selecciona un Proveedor");
                            cbProveedor.SelectedIndex = 0;
                        }
                        else if (cbProveedor.Items.Count == 0)
                        {
                            cbProveedor.Items.Add("Selecciona un Proveedor");
                            cbProveedor.SelectedIndex = 0;
                        }
                        panelContenido.Controls.Add(cbProveedor);
                        panelContenedor.Controls.Add(panelContenido);
                        fLPDetalleProducto.Controls.Add(panelContenedor);

                        CheckBox check = new CheckBox();
                        check.Name = "chkBox" + name;
                        check.Text = name.Remove(0, 3);
                        check.Width = 90;
                        check.Height = 24;
                        check.Location = new Point(10, 10);
                        if (value.Equals("true"))
                        {
                            check.Checked = false;
                        }
                        check.CheckedChanged += checkBoxProveedor_CheckedChanged;
                        panelContenido.Controls.Add(check);
                        panelContenedor.Controls.Add(panelContenido);
                        fLPDetalleProducto.Controls.Add(panelContenedor);
                    }
                } // aqui se continua con los demas else if
                else if (!name.Equals("chkProveedor") && value.Equals("true"))// cualquier otro 
                {
                    nombrePanelContenedor = "panelContenedor" + name.ToString().Remove(0, 3);
                    nombrePanelContenido = "panelContenido" + name.ToString().Remove(0, 3);

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 480;
                    panelContenedor.Height = 40;
                    panelContenedor.Name = nombrePanelContenedor;

                    chkSettingVariableTxt = lstListView.Items[i].Text.ToString();
                    chkSettingVariableVal = lstListView.Items[i].SubItems[1].Text.ToString();

                    if (chkSettingVariableVal.Equals("true"))
                    {
                        name = chkSettingVariableTxt;
                        value = chkSettingVariableVal;

                        Panel panelContenido = new Panel();
                        panelContenido.Name = nombrePanelContenido;
                        panelContenido.Width = 476;
                        panelContenido.Height = 38;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarDetallesGral(name.ToString().Remove(0, 3));

                        ComboBox cbDetalleGral = new ComboBox();
                        cbDetalleGral.Name = "cb" + name;
                        cbDetalleGral.Width = 336;
                        cbDetalleGral.Height = 21;
                        cbDetalleGral.Location = new Point(119, 10);
                        cbDetalleGral.DropDownStyle = ComboBoxStyle.DropDownList;
                        cbDetalleGral.SelectedIndexChanged += new System.EventHandler(ComboBoxDetalleGral_SelectValueChanged);

                        if (listaDetalleGral.Length > 0)
                        {
                            cbDetalleGral.DataSource = detallesGral.ToArray();
                            cbDetalleGral.DisplayMember = "value";
                            cbDetalleGral.ValueMember = "Key";
                            cbDetalleGral.SelectedValue = "0";
                        }
                        else if (cbDetalleGral.Items.Count == 0)
                        {
                            cbDetalleGral.Items.Add(name.ToString().Remove(0, 3) + "...");
                            cbDetalleGral.SelectedIndex = 0;
                        }
                        panelContenido.Controls.Add(cbDetalleGral);
                        panelContenedor.Controls.Add(panelContenido);
                        fLPDetalleProducto.Controls.Add(panelContenedor);

                        CheckBox checkDetalleGral = new CheckBox();
                        checkDetalleGral.Name = "chkBox" + name;
                        checkDetalleGral.Text = name.Remove(0, 3);
                        checkDetalleGral.Width = 90;
                        checkDetalleGral.Height = 24;
                        checkDetalleGral.Location = new Point(10, 10);
                        if (value.Equals("true"))
                        {
                            checkDetalleGral.Checked = false;
                        }
                        checkDetalleGral.CheckedChanged += checkBoxDetalleGral_CheckedChanged;
                        
                        panelContenido.Controls.Add(checkDetalleGral);
                        panelContenedor.Controls.Add(panelContenido);
                        fLPDetalleProducto.Controls.Add(panelContenedor);
                    }
                }
            }
        }

        private void checkBoxDetalleGral_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkGralDetail = sender as CheckBox;
            string nameChkBox = string.Empty;

            nameChkBox = chkGralDetail.Name.ToString().Remove(0, 9);

            if (chkGralDetail.Checked.Equals(true))
            {
                foreach (Control controlHijo in fLPDetalleProducto.Controls)
                {
                    if (controlHijo.Name.Equals("panelContenedor" + nameChkBox))
                    {
                        foreach (Control subControlHijo in controlHijo.Controls)
                        {
                            if (subControlHijo.Name.Equals("panelContenido" + nameChkBox))
                            {
                                foreach (Control intoSubControlHijo in subControlHijo.Controls)
                                {
                                    if (intoSubControlHijo is ComboBox)
                                    {
                                        intoSubControlHijo.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (chkGralDetail.Checked.Equals(false))
            {
                foreach (Control controlHijo in fLPDetalleProducto.Controls)
                {
                    if (controlHijo.Name.Equals("panelContenedor" + nameChkBox))
                    {
                        foreach (Control subControlHijo in controlHijo.Controls)
                        {
                            if (subControlHijo.Name.Equals("panelContenido" + nameChkBox))
                            {
                                foreach (Control intoSubControlHijo in subControlHijo.Controls)
                                {
                                    if (intoSubControlHijo is ComboBox)
                                    {
                                        intoSubControlHijo.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void checkBoxProveedor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbProvider = sender as CheckBox;
            if (cbProvider.Checked.Equals(true))
            {
                foreach (Control controlHijo in fLPDetalleProducto.Controls)
                {
                    if ((controlHijo.Name.Equals("panelContenedorchkProveedor")) && (controlHijo is Panel))
                    {
                        foreach (Control subControlHijo in controlHijo.Controls)
                        {
                            if ((subControlHijo.Name.Equals("panelContenidochkProveedor")) && (subControlHijo is Panel))
                            {
                                foreach (Control intoSubControlHijo in subControlHijo.Controls)
                                {
                                    if ((intoSubControlHijo.Name.Equals("cbchkProveedor")) && (intoSubControlHijo is ComboBox))
                                    {
                                        intoSubControlHijo.Enabled = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cbProvider.Checked.Equals(false))
            {
                foreach (Control controlHijo in fLPDetalleProducto.Controls)
                {
                    if ((controlHijo.Name.Equals("panelContenedorchkProveedor")) && (controlHijo is Panel))
                    {
                        foreach (Control subControlHijo in controlHijo.Controls)
                        {
                            if ((subControlHijo.Name.Equals("panelContenidochkProveedor")) && (subControlHijo is Panel))
                            {
                                foreach (Control intoSubControlHijo in subControlHijo.Controls)
                                {
                                    if ((intoSubControlHijo.Name.Equals("cbchkProveedor")) && (intoSubControlHijo is ComboBox))
                                    {
                                        intoSubControlHijo.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CargarProveedores()
        {
            // Asignamos el Array con los nombres de los proveedores al comboBox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            proveedores = new Dictionary<string, string>();

            // Comprobar que ya exista al menos un Proveedor
            if (listaProveedores.Length > 0)
            {
                proveedores.Add("0", "Selecciona un Proveedor");

                foreach (var proveedor in listaProveedores)
                {
                    var tmp = proveedor.Split('-');
                    proveedores.Add(tmp[0].Trim(), tmp[1].Trim());
                }
            }
            else
            {
                proveedores.Add("0", "Selecciona un Proveedor");
            }
        }

        private void comboBoxProveedor_SelectValueChanged(object sender, EventArgs e)
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

        private void ComboBoxDetalleGral_SelectValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cargarDatosProveedor(int idProveedor)
        {
            // Para que no de error ya que nunca va a existir un proveedor en ID = 0
            if (idProveedor > 0)
            {
                datosProveedor = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);
            }
        }

        private void llenarDatosProveedor(string textoBuscado)
        {
            string namePanel = string.Empty;

            namePanel = "panelContenedor" + textoBuscado;

            foreach (Control contHijo in fLPDetalleProducto.Controls.OfType<Control>())
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
        #endregion Modifying Configuration Settings at Runtime

        #region Dictionary Values
        int usrNo = 0;

        string line = string.Empty,
                path = string.Empty,
                pathTemp = string.Empty,
                fileName = "DiccionarioDetalleBasicos.txt",
                fileTempName = "tempDicBasic.txt",
                rutaCompletaFile = string.Empty,
                rutaCompletaTempFile = string.Empty,
                chkName = string.Empty,
                chkValue = string.Empty,
                itemCB = string.Empty,
                cbName = string.Empty;

        string[] words;

        char[] delimiter = { '|' };

        private void saveDictionary()
        {
            usrNo = FormPrincipal.userID;
            path += saveDirectoryFile + usrNo + @"\";
            pathTemp += saveDirectoryFile + usrNo + @"\";
            if (usrNo > 0)
            {
                if (!File.Exists(path + fileName))
                {
                    Directory.CreateDirectory(path);
                    using (File.Create(path + fileName)) { }
                }
                if (File.Exists(path + fileName))
                {
                    rutaCompletaFile = path + fileName;
                    rutaCompletaTempFile = pathTemp + fileTempName;
                    if (File.Exists(rutaCompletaFile))
                    {
                        string text = File.ReadAllText(rutaCompletaFile);
                        text.Replace("\r\n", "");
                        if (!text.Length.Equals(0))
                        {
                            //if ()
                            //{

                            //}
                        }
                        else if (text.Length.Equals(0))
                        {
                            foreach (Control itemControl in fLPDetalleProducto.Controls)
                            {
                                foreach (Control subItemControl in itemControl.Controls)
                                {
                                    foreach (Control intoSubItemControl in subItemControl.Controls)
                                    {
                                        if (intoSubItemControl is CheckBox)
                                        {
                                            CheckBox chk = (CheckBox)intoSubItemControl;
                                            if (chk.Name.Equals("chkBoxchkProveedor"))
                                            {
                                                chkName = chk.Name.ToString();
                                                chkValue = chk.Checked.ToString();
                                            }
                                            else if (!chk.Name.Equals("chkBoxchkProveedor"))
                                            {
                                                chkName = chk.Name.ToString();
                                                chkValue = chk.Checked.ToString();
                                            }
                                        }
                                        if (intoSubItemControl is ComboBox)
                                        {
                                            ComboBox cb = (ComboBox)intoSubItemControl;
                                            if (cb.Name.Equals("cbchkProveedor"))
                                            {
                                                itemCB = cb.Text;
                                                cbName = cb.Name;
                                            }
                                            else if (!cb.Name.Equals("cbchkProveedor"))
                                            {
                                                itemCB = cb.Text;
                                                cbName = cb.Name;
                                            }
                                        }
                                    }
                                    diccionarioDetalleBasicos.Add(usrNo.ToString(), new Tuple<string, string, string, string>(chkName, chkValue, itemCB, cbName));
                                    usrNo++;
                                }
                            }
                            using (StreamWriter file = new StreamWriter(rutaCompletaFile))
                            {
                                foreach (var entry in diccionarioDetalleBasicos)
                                {
                                    file.WriteLine("{0}|{1}|{2}|{3}|{4}", entry.Key, entry.Value.Item1, entry.Value.Item2, entry.Value.Item3, entry.Value.Item4);
                                }
                            }
                        }
                    }
                }
            }
            else if (usrNo.Equals(0))
            {
                MessageBox.Show("Favor de Seleccionar un valor\ndiferente o Mayor a 0 en Campo Usuario", "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion Dictionary Values

        private void WinQueryString_Load(object sender, EventArgs e)
        {
            saveDirectoryFile = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\settings\Dictionary\";

            if (Properties.Settings.Default.chkFiltroStock.Equals(true))
            {
                string strOperadorAndCant;
                string[] strList;
                char[] separator = { ' ' };
                chkBoxStock.Checked = Properties.Settings.Default.chkFiltroStock;
                strOperadorAndCant = Properties.Settings.Default.strFiltroStock;
                if (!strOperadorAndCant.Equals(""))
                {
                    strList = strOperadorAndCant.Split(separator);
                    txtCantStock.Text = strList[2].ToString();
                    if (strList[1].ToString().Equals(">="))
                    {
                        cbTipoFiltroStock.SelectedIndex = 1;
                    }
                    else if (strList[1].ToString().Equals("<="))
                    {
                        cbTipoFiltroStock.SelectedIndex = 2;
                    }
                    else if (strList[1].ToString().Equals("="))
                    {
                        cbTipoFiltroStock.SelectedIndex = 3;
                    }
                    else if (strList[1].ToString().Equals(">"))
                    {
                        cbTipoFiltroStock.SelectedIndex = 4;
                    }
                    else if (strList[1].ToString().Equals("<"))
                    {
                        cbTipoFiltroStock.SelectedIndex = 5;
                    }
                }
            }
            else if (Properties.Settings.Default.chkFiltroStock.Equals(false))
            {
                chkBoxStock.Checked = false;
                cbTipoFiltroStock.SelectedIndex = 0;
                cbTipoFiltroStock_SelectedIndexChanged(sender, e);
                validarChkBoxStock();
            }
            if (Properties.Settings.Default.chkFiltroPrecio.Equals(true))
            {
                string strOperadorAndCant;
                string[] strList;
                char[] separator = { ' ' };
                chkBoxPrecio.Checked = Properties.Settings.Default.chkFiltroPrecio;
                strOperadorAndCant = Properties.Settings.Default.strFiltroPrecio;
                if (!strOperadorAndCant.Equals(""))
                {
                    strList = strOperadorAndCant.Split(separator);
                    if (strList.Length > 1)
                    {
                        txtCantPrecio.Text = strList[2].ToString();

                        if (strList[1].ToString().Equals(">="))
                        {
                            cbTipoFiltroPrecio.SelectedIndex = 1;
                        }
                        else if (strList[1].ToString().Equals("<="))
                        {
                            cbTipoFiltroPrecio.SelectedIndex = 2;
                        }
                        else if (strList[1].ToString().Equals("="))
                        {
                            cbTipoFiltroPrecio.SelectedIndex = 3;
                        }
                        else if (strList[1].ToString().Equals(">"))
                        {
                            cbTipoFiltroPrecio.SelectedIndex = 4;
                        }
                        else if (strList[1].ToString().Equals("<"))
                        {
                            cbTipoFiltroPrecio.SelectedIndex = 5;
                        }
                    }
                }
            }
            else if (Properties.Settings.Default.chkFiltroPrecio.Equals(false))
            {
                chkBoxPrecio.Checked = false;
                cbTipoFiltroPrecio.SelectedIndex = 0;
                cbTipoFiltroPrecio_SelectedIndexChanged(sender, e);
                validarChkBoxPrecio();
            }

            loadFormConfig();
            BuscarChkBoxListView(chkDatabase);

            verificarChkBoxDinamicos();

            saveDictionary();
        }

        private void verificarChkBoxDinamicos()
        {
            bool verificado = false;
            foreach (Control controlHijo in fLPDetalleProducto.Controls)
            {
                if (controlHijo is Panel)
                {
                    foreach (Control subControlHijo in controlHijo.Controls)
                    {
                        if (subControlHijo is Panel)
                        {
                            foreach (Control intoSubControlHijo in subControlHijo.Controls)
                            {
                                if (intoSubControlHijo is CheckBox)
                                {
                                    CheckBox chk;
                                    chk = (CheckBox)intoSubControlHijo;
                                    if (chk.Checked.Equals(true))
                                    {
                                        verificado = chk.Checked;
                                    }
                                    else if (chk.Checked.Equals(false))
                                    {
                                        verificado = chk.Checked;
                                    }
                                }
                                if (intoSubControlHijo is ComboBox)
                                {
                                    ComboBox cb;
                                    cb = (ComboBox)intoSubControlHijo;
                                    if (verificado.Equals(true))
                                    {
                                        cb.Enabled = true;
                                    }
                                    else if (verificado.Equals(false))
                                    {
                                        cb.Enabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void validarChkBoxStock()
        {
            cbTipoFiltroStock.SelectedIndex = 0;
            if (chkBoxStock.Checked.Equals(true))
            {
                filtroStock = Convert.ToBoolean(chkBoxStock.Checked);

                Properties.Settings.Default.chkFiltroStock = filtroStock;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantStock.Enabled = true;
                cbTipoFiltroStock.Enabled = true;
                txtCantStock.Text = "0.0";
                txtCantStock.Focus();
            }
            else if (chkBoxStock.Checked.Equals(false))
            {
                filtroStock = Convert.ToBoolean(chkBoxStock.Checked);

                Properties.Settings.Default.chkFiltroStock = filtroStock;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantStock.Enabled = false;
                cbTipoFiltroStock.Enabled = false;
                txtCantStock.Text = "0.0";
            }
        }

        private void validarChkBoxPrecio()
        {
            cbTipoFiltroPrecio.SelectedIndex = 0;
            if (chkBoxPrecio.Checked.Equals(true))
            {
                filtroPrecio = Convert.ToBoolean(chkBoxPrecio.Checked);

                Properties.Settings.Default.chkFiltroPrecio = filtroPrecio;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantPrecio.Enabled = true;
                cbTipoFiltroPrecio.Enabled = true;
                txtCantPrecio.Text = "0.0";
                txtCantPrecio.Focus();
            }
            else if (chkBoxPrecio.Checked.Equals(false))
            {
                filtroPrecio = Convert.ToBoolean(chkBoxPrecio.Checked);

                Properties.Settings.Default.chkFiltroPrecio = filtroPrecio;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantPrecio.Enabled = false;
                cbTipoFiltroPrecio.Enabled = false;
                txtCantPrecio.Text = "0.0";
            }
        }

        private void chkBoxStock_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBoxStock();
        }

        private void cbTipoFiltroPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtroPrecio = Properties.Settings.Default.chkFiltroPrecio;

            if (filtroPrecio.Equals(true))
            {
                strOpcionCBPrecio = Convert.ToString(cbTipoFiltroPrecio.SelectedItem);

                strTxtPrecio = txtCantPrecio.Text;

                strFiltroPrecio = "Precio ";

                if (!strTxtPrecio.Equals(""))
                {
                    if (strOpcionCBPrecio.Equals("No Aplica"))
                    {
                        strFiltroPrecio = "";
                    }
                    else if (strOpcionCBPrecio.Equals("Mayor o Igual Que"))
                    {
                        strFiltroPrecio += ">= ";
                    }
                    else if (strOpcionCBPrecio.Equals("Menor o Igual Que"))
                    {
                        strFiltroPrecio += "<= ";
                    }
                    else if (strOpcionCBPrecio.Equals("Igual Que"))
                    {
                        strFiltroPrecio += "= ";
                    }
                    else if (strOpcionCBPrecio.Equals("Mayor Que"))
                    {
                        strFiltroPrecio += "> ";
                    }
                    else if (strOpcionCBPrecio.Equals("Menor Que"))
                    {
                        strFiltroPrecio += "< ";
                    }
                }
                else if (strTxtPrecio.Equals(""))
                {
                    strFiltroPrecio = "";
                }
            }
        }

        private void cbTipoFiltroStock_Click(object sender, EventArgs e)
        {
            cbTipoFiltroStock.DroppedDown = true;
        }

        private void txtCantPrecio_Click(object sender, EventArgs e)
        {

        }

        private void cbTipoFiltroPrecio_Click(object sender, EventArgs e)
        {
            cbTipoFiltroPrecio.DroppedDown = true;
        }

        private void chkBoxPrecio_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBoxPrecio();
        }

        private void txtCantPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Soló son permitidos numeros\nen este campo de Precio",
                                "Error de captura del Precio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            cbTipoFiltroStock_SelectedIndexChanged(sender, e);
            filtroStock = Properties.Settings.Default.chkFiltroStock;
            cbTipoFiltroPrecio_SelectedIndexChanged(sender, e);
            filtroPrecio = Properties.Settings.Default.chkFiltroPrecio;

            DialogResult result = MessageBox.Show("Desea Guardar el Filtro\no editar su elección",
                                                  "Guardado del Filtro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (filtroStock.Equals(true))
                {
                    strTxtStock = txtCantStock.Text;

                    if (!strTxtStock.Equals(""))
                    {
                        if (!strOpcionCBStock.Equals("No Aplica"))
                        {
                            strFiltroStock += strTxtStock;

                            Properties.Settings.Default.strFiltroStock = strFiltroStock;
                            Properties.Settings.Default.Save();
                            Properties.Settings.Default.Reload();
                            //MessageBox.Show("Query Construido es: " + Properties.Settings.Default.strFiltroStock,
                            //            "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Properties.Settings.Default.strFiltroStock = string.Empty;
                            //MessageBox.Show("Query Construido es: " + Properties.Settings.Default.strFiltroStock,
                            //            "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (strTxtStock.Equals(""))
                    {
                        strFiltroStock = "";
                        MessageBox.Show("Favor de Introducir una\nCantidad en el Campo de Stock",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantStock.Focus();
                    }
                }
                else if (filtroStock.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Stock.");
                }
                if (filtroPrecio.Equals(true))
                {
                    strTxtPrecio = txtCantPrecio.Text;

                    if (!strTxtPrecio.Equals(""))
                    {
                        if (!strOpcionCBPrecio.Equals("No Aplica"))
                        {
                            strFiltroPrecio += strTxtPrecio;

                            Properties.Settings.Default.strFiltroPrecio = strFiltroPrecio;
                            Properties.Settings.Default.Save();
                            Properties.Settings.Default.Reload();
                            //MessageBox.Show("Query Construido es: " + Properties.Settings.Default.strFiltroStock,
                            //            "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Properties.Settings.Default.strFiltroPrecio = string.Empty;
                            //MessageBox.Show("Query Construido es: " + Properties.Settings.Default.strFiltroStock,
                            //            "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (strTxtPrecio.Equals(""))
                    {
                        strFiltroPrecio = "";
                        MessageBox.Show("Favor de Introducir una\nCantidad en el Campo de Precio",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantPrecio.Focus();
                    }
                }
                else if (filtroPrecio.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Precio.");
                }
                else if (filtroProveedor.Equals(true))
                {
                    if (!strOpcionCBProveedor.Equals(""))
                    {
                        Properties.Settings.Default.strFiltroProveedor = strFiltroProveedor;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();
                    }
                    else
                    {
                        Properties.Settings.Default.strFiltroProveedor = string.Empty;
                    }
                }
                else if (filtroPrecio.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Proveedor.");
                }
                this.Close();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }
            else if (result == DialogResult.Cancel)
            {
                txtCantStock.Focus();
            }
        }

        private void cbTipoFiltroStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtroStock = Properties.Settings.Default.chkFiltroStock;

            if (filtroStock.Equals(true))
            {
                strOpcionCBStock = Convert.ToString(cbTipoFiltroStock.SelectedItem);

                strTxtStock = txtCantStock.Text;

                strFiltroStock = "Stock ";

                if (!strTxtStock.Equals(""))
                {
                    if (strOpcionCBStock.Equals("No Aplica"))
                    {
                        strFiltroStock = "";
                    }
                    else if (strOpcionCBStock.Equals("Mayor o Igual Que"))
                    {
                        strFiltroStock += ">= ";
                    }
                    else if (strOpcionCBStock.Equals("Menor o Igual Que"))
                    {
                        strFiltroStock += "<= ";
                    }
                    else if (strOpcionCBStock.Equals("Igual Que"))
                    {
                        strFiltroStock += "= ";
                    }
                    else if (strOpcionCBStock.Equals("Mayor Que"))
                    {
                        strFiltroStock += "> ";
                    }
                    else if (strOpcionCBStock.Equals("Menor Que"))
                    {
                        strFiltroStock += "< ";
                    }
                }
                else if (strTxtStock.Equals(""))
                {
                    strFiltroStock = "";
                }
            }
        }

        private void WinQueryString_FormClosed(object sender, FormClosedEventArgs e)
        {
            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.actualizarBtnFiltro();
                producto.CargarDatos();
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.inicializarVariablesFiltro();
            }

            this.Close();
        }
    }
}
