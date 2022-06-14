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
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        bool filtroStock,
                filtroPrecio,
                filtroProveedor,
                filtroTipo,
                filtroRevision,
                filtroImagen;

        string  strFiltroStock = string.Empty,
                strFiltroPrecio = string.Empty,
                strFiltroProveedor = string.Empty,
                strFiltroCombProdServ = string.Empty,
                strFiltroNoRevision = string.Empty,
                strFiltroImagen = string.Empty,
                strOpcionCBStock = string.Empty,
                strOpcionCBPrecio = string.Empty,
                strOpcionCBProveedor = string.Empty,
                strOpcionCBCombProdServ = string.Empty,
                strOpcionCBNoRevision = string.Empty,
                strOpcionCBImagen = string.Empty,
                strTxtStock = string.Empty,
                strTxtPrecio = string.Empty,
                strTxtNoRevision = string.Empty,
                servidor = string.Empty;

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
        Dictionary<string, Tuple<string, string, string>> setUpFiltroDinamicos = new Dictionary<string, Tuple<string, string, string>>();

        string saveDirectoryFile;

        DataTable dtProveedor;

        string nameChkBox = string.Empty;

        int chkValor = -1,
            foundChkBox = -1;


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

        private void chkBoxImagen_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBoxImagen();
        }

        private void txtNoRevision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void validarChkBoxImagen()
        {
            nameChkBox = chkBoxImagen.Name;

            if (chkBoxImagen.Checked.Equals(true))
            {
                filtroImagen = Convert.ToBoolean(chkBoxImagen.Checked);

                if (filtroImagen.Equals(true))
                {
                    chkValor = 1;
                }

                //Properties.Settings.Default.chkFiltroImagen = filtroImagen;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                cbTipoFiltroImagen.Enabled = true;
                cbTipoFiltroImagen.Focus();
            }
            else if (chkBoxImagen.Checked.Equals(false))
            {
                filtroImagen = Convert.ToBoolean(chkBoxImagen.Checked);

                if (filtroImagen.Equals(false))
                {
                    chkValor = 0;
                }

                //Properties.Settings.Default.chkFiltroImagen = filtroImagen;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                cbTipoFiltroImagen.SelectedIndex = 0;
                cbTipoFiltroImagen.Enabled = false;
            }

            using (DataTable dtItemChckImagen = cn.CargarDatos(cs.VerificarChk(nameChkBox, FormPrincipal.userID)))
            {
                if (!dtItemChckImagen.Rows.Count.Equals(0))
                {
                    foundChkBox = 1;
                }
                else if (dtItemChckImagen.Rows.Count.Equals(0))
                {
                    foundChkBox = 0;
                }
            }

            if (foundChkBox.Equals(1))
            {
                try
                {
                    var updateChkBoxImagen = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de Imagen\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (foundChkBox.Equals(0))
            {
                try
                {
                    var insertChkBoxImagen = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de Imagen\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbTipoFiltroImagen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtroImagen = Properties.Settings.Default.chkFiltroImagen;
            filtroImagen = chkBoxImagen.Checked;

            if (filtroImagen.Equals(true))
            {
                if (!Convert.ToString(cbTipoFiltroImagen.SelectedItem).Equals(""))
                {
                    strFiltroImagen = "ProdImage ";
                    if (Convert.ToString(cbTipoFiltroImagen.SelectedItem).ToString().Equals("No Aplica"))
                    {
                        strFiltroImagen = "";
                    }
                    else if (Convert.ToString(cbTipoFiltroImagen.SelectedItem).ToString().Equals("Con Imagen"))
                    {
                        strFiltroImagen += "<> ''";
                    }
                    else if (Convert.ToString(cbTipoFiltroImagen.SelectedItem).ToString().Equals("Sin Imagen"))
                    {
                        strFiltroImagen += "= ''";
                    }
                }
                else if (Convert.ToString(cbTipoFiltroImagen.SelectedItem).Equals(""))
                {
                    strFiltroImagen = "No Aplica";
                }
            }
            else if (filtroImagen.Equals(false))
            {
                strFiltroImagen = "No Aplica";
            }
            //MessageBox.Show("String Almacenado:\n" + strFiltroImagen);
        }

        NameValueCollection appSettings;

        private void chkBoxTipo_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBoxTipo();
        }

        private void cbTipoFiltroRevision_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtroRevision = Properties.Settings.Default.chkFiltroRevisionInventario;
            filtroRevision = chkBoxRevision.Checked;

            if (filtroRevision.Equals(true))
            {
                strOpcionCBNoRevision = Convert.ToString(cbTipoFiltroRevision.SelectedItem);

                strTxtNoRevision = txtNoRevision.Text;

                strFiltroNoRevision = "NumeroRevision ";

                if (!strTxtNoRevision.Equals(""))
                {
                    if (strOpcionCBNoRevision.Equals("No Aplica"))
                    {
                        strFiltroNoRevision = "";
                    }
                    else if (strOpcionCBNoRevision.Equals("Mayor o Igual Que"))
                    {
                        strFiltroNoRevision += ">= ";
                    }
                    else if (strOpcionCBNoRevision.Equals("Menor o Igual Que"))
                    {
                        strFiltroNoRevision += "<= ";
                    }
                    else if (strOpcionCBNoRevision.Equals("Igual Que"))
                    {
                        strFiltroNoRevision += "= ";
                    }
                    else if (strOpcionCBNoRevision.Equals("Mayor Que"))
                    {
                        strFiltroNoRevision += "> ";
                    }
                    else if (strOpcionCBNoRevision.Equals("Menor Que"))
                    {
                        strFiltroNoRevision += "< ";
                    }
                }
            }
            else if (filtroRevision.Equals(false))
            {
                strFiltroNoRevision = "No Aplica";
            }
            //MessageBox.Show("String Almacenado:\n" + strFiltroNoRevision.ToString());
        }

        private void chkBoxRevision_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBoxRevision();
        }

        private void validarChkBoxRevision()
        {
            nameChkBox = chkBoxRevision.Name;

            if (chkBoxRevision.Checked.Equals(true))
            {
                filtroRevision = Convert.ToBoolean(chkBoxRevision.Checked);

                if (filtroRevision.Equals(true))
                {
                    chkValor = 1;
                }

                Properties.Settings.Default.chkFiltroRevisionInventario = filtroRevision;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                cbTipoFiltroRevision.Enabled = true;
                txtNoRevision.Enabled = true;
                cbTipoFiltroRevision.Focus();
            }
            else if (chkBoxRevision.Checked.Equals(false))
            {
                filtroRevision = Convert.ToBoolean(chkBoxRevision.Checked);

                if (filtroRevision.Equals(false))
                {
                    chkValor = 0;
                }

                Properties.Settings.Default.chkFiltroRevisionInventario = filtroRevision;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                cbTipoFiltroRevision.SelectedIndex = 0;
                cbTipoFiltroRevision.Enabled = false;
                txtNoRevision.Text = "0";
                txtNoRevision.Enabled = false;
            }

            using (DataTable dtItemChckRevision = cn.CargarDatos(cs.VerificarChk(nameChkBox, FormPrincipal.userID)))
            {
                if (!dtItemChckRevision.Rows.Count.Equals(0))
                {
                    foundChkBox = 1;
                }
                else if (dtItemChckRevision.Rows.Count.Equals(0))
                {
                    foundChkBox = 0;
                }
            }

            if (foundChkBox.Equals(1))
            {
                try
                {
                    var updateChkBoxPrecio = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de No de Revisión\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (foundChkBox.Equals(0))
            {
                try
                {
                    var insertChkBoxPrecio = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de No de Revisión\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbTipoFiltroCombProdServ_Click(object sender, EventArgs e)
        {
            cbTipoFiltroCombProdServ.DroppedDown = true;
        }

        private void cbTipoFiltroCombProdServ_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtroTipo = Properties.Settings.Default.chkFiltroCombProdServ;
            filtroTipo = chkBoxTipo.Checked;

            if (filtroTipo.Equals(true))
            {
                //strOpcionCBCombProdServ = Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem);

                if (!Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem).Equals(""))
                {
                    strFiltroCombProdServ = "Tipo ";

                    if (Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem).Equals("No Aplica"))
                    {
                        strFiltroCombProdServ = "";
                    }
                    else if (Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem).Equals("Combo"))
                    {
                        strFiltroCombProdServ += "= PQ";
                    }
                    else if (Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem).Equals("Producto"))
                    {
                        strFiltroCombProdServ += "= P";
                    }
                    else if (Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem).Equals("Servicio"))
                    {
                        strFiltroCombProdServ += "= S";
                    }
                }
                else if (Convert.ToString(cbTipoFiltroCombProdServ.SelectedItem).Equals(""))
                {
                    strFiltroCombProdServ = "No Aplica";
                }
            }
            else if (filtroTipo.Equals(false))
            {
                strFiltroCombProdServ = "No Aplica";
            }
        }

        private void validarChkBoxTipo()
        {
            nameChkBox = chkBoxTipo.Name;

            //cbTipoFiltroCombProdServ.SelectedIndex = 0;
            if (chkBoxTipo.Checked.Equals(true))
            {
                filtroTipo = Convert.ToBoolean(chkBoxTipo.Checked);

                if (filtroTipo.Equals(true))
                {
                    chkValor = 1;
                }

                //Properties.Settings.Default.chkFiltroCombProdServ = filtroTipo;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                cbTipoFiltroCombProdServ.Enabled = true;
                cbTipoFiltroCombProdServ.Focus();
            }
            else if (chkBoxTipo.Checked.Equals(false))
            {
                filtroTipo = Convert.ToBoolean(chkBoxTipo.Checked);

                if (filtroTipo.Equals(false))
                {
                    chkValor = 0;
                }

                //Properties.Settings.Default.chkFiltroCombProdServ = filtroTipo;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                cbTipoFiltroCombProdServ.SelectedIndex = 0;
                cbTipoFiltroCombProdServ.Enabled = false;
            }

            using (DataTable dtItemChckTipo = cn.CargarDatos(cs.VerificarChk(nameChkBox, FormPrincipal.userID)))
            {
                if (!dtItemChckTipo.Rows.Count.Equals(0))
                {
                    foundChkBox = 1;
                }
                else if (dtItemChckTipo.Rows.Count.Equals(0))
                {
                    foundChkBox = 0;
                }
            }

            if (foundChkBox.Equals(1))
            {
                try
                {
                    var updateChkBoxPrecio = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de Tipo\n\nCombo\nProducto\nServicio\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (foundChkBox.Equals(0))
            {
                try
                {
                    var insertChkBoxPrecio = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de Tipo\n\nCombo\nProducto\nServicio\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // this code will add a listviewtem
        // to a listview for each database entry
        // in the appSettings section of an App.config file.
        private void loadFormConfig()
        {
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                string queryLoadProduct = string.Empty;
                queryLoadProduct = $"SELECT * FROM appSettings WHERE IDUsuario = {FormPrincipal.userID.ToString()}";

                chkDatabase.Items.Clear();
                settingDatabases.Items.Clear();

                lvi = new ListViewItem();

                try
                {
                    using (DataTable tlbAppSettings = cn.CargarDatos(queryLoadProduct))
                    {
                        if (tlbAppSettings.Rows.Count > 0)
                        {
                            foreach (DataRow row in tlbAppSettings.Rows)
                            {
                                connStr = "chk" + row["concepto"].ToString();
                                keyName = row["checkBoxConcepto"].ToString();
                                string valorChk = string.Empty;
                                lvi = new ListViewItem(keyName);
                                if (keyName.Equals("1"))
                                {
                                    valorChk = "true";
                                }
                                else if (keyName.Equals("0"))
                                {
                                    valorChk = "false";
                                }
                                lvi.SubItems.Add(valorChk);
                                chkDatabase.Items.Add(lvi);
                            }
                            foreach (DataRow row in tlbAppSettings.Rows)
                            {
                                connStr = row["textComboBoxConcepto"].ToString();
                                keyName = row["checkBoxComboBoxConcepto"].ToString();
                                string valorChk = string.Empty;
                                lvi = new ListViewItem(keyName.Remove(0, 3));
                                if (keyName.Equals("1"))
                                {
                                    valorChk = "true";
                                }
                                else if (keyName.Equals("0"))
                                {
                                    valorChk = "false";
                                }
                                lvi.SubItems.Add(valorChk);
                                settingDatabases.Items.Add(lvi);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lectura App.Config/AppSettings: {0}" + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (string.IsNullOrWhiteSpace(servidor))
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
                        MessageBox.Show("Lectura App.Config/appSettings: La Sección de appSettings está vacia", "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(), "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //if (Properties.Settings.Default.TipoEjecucion == 1)
            //{
            //    xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            //}

            //if (Properties.Settings.Default.TipoEjecucion == 2)
            //{
            //    xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            //}

            //appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            //chkDatabase.Items.Clear();
            //settingDatabases.Items.Clear();

            //lvi = new ListViewItem();

            //try
            //{
            //    chkDatabase.Clear();
            //    settingDatabases.Clear();

            //    appSettings = ConfigurationManager.AppSettings;

            //    if (appSettings.Count == 0)
            //    {
            //        MessageBox.Show("Lectura App.Config/AppSettings: La Sección de AppSettings está vacia",
            //                        "Archivo Vacio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    if (appSettings.Count > 0)
            //    {
            //        for (int i = 0; i < appSettings.Count; i++)
            //        {
            //            connStr = appSettings[i];
            //            keyName = appSettings.GetKey(i);
            //            found = keyName.IndexOf("chk", 0, 3);
            //            if (found >= 0)
            //            {
            //                lvi = new ListViewItem(keyName);
            //                lvi.SubItems.Add(connStr);
            //                chkDatabase.Items.Add(lvi);
            //            }
            //        }

            //        for (int i = 0; i < appSettings.Count; i++)
            //        {
            //            string foundSetting = string.Empty;
            //            connStr = appSettings[i];
            //            keyName = appSettings.GetKey(i);
            //            found = keyName.IndexOf("chk", 0, 3);
            //            if (found <= -1)
            //            {
            //                lvi = new ListViewItem(keyName);
            //                lvi.SubItems.Add(connStr);
            //                settingDatabases.Items.Add(lvi);
            //            }
            //        }
            //    }
            //}
            //catch (ConfigurationException e)
            //{
            //    MessageBox.Show("Lectura App.Config/AppSettings: {0}" + e.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void BuscarChkBoxListView(ListView lstListView)
        {
            int id = 0,
                row = 0;

            string nameChk = string.Empty,
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
                //name = lstListView.Items[i].Text.ToString();
                //value = lstListView.Items[i].SubItems[1].Text.ToString();
                name = lstListView.Items[i].SubItems[1].Text.ToString();
                value = lstListView.Items[i].Text.ToString();

                if (name.Equals("chkProveedor") && value.Equals("true"))
                {
                    nombrePanelContenedor = "panelContenedor" + name;
                    nombrePanelContenido = "panelContenido" + name;

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 500;
                    panelContenedor.Height = 40;
                    panelContenedor.Name = nombrePanelContenedor;
                    //panelContenedor.BackColor = Color.Aqua;

                    //chkSettingVariableTxt = lstListView.Items[i].Text.ToString();
                    //chkSettingVariableVal = lstListView.Items[i].SubItems[1].Text.ToString();
                    chkSettingVariableTxt = lstListView.Items[i].SubItems[1].Text.ToString();
                    chkSettingVariableVal = lstListView.Items[i].Text.ToString();

                    if (chkSettingVariableVal.Equals("true"))
                    {
                        name = chkSettingVariableTxt;
                        value = chkSettingVariableVal;
                        Panel panelContenido = new Panel();
                        panelContenido.Name = nombrePanelContenido;
                        panelContenido.Width = 495;
                        panelContenido.Height = 38;
                        //panelContenido.BackColor = Color.Red;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarProveedores();

                        ComboBox cbProveedor = new ComboBox();
                        cbProveedor.Name = "cb" + name;
                        cbProveedor.Width = 370;
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
                            cbProveedor.Items.Add("Selecciona Proveedor");
                            cbProveedor.SelectedIndex = 0;
                        }
                        else if (cbProveedor.Items.Count == 0)
                        {
                            cbProveedor.Items.Add("Selecciona Proveedor");
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
                    panelContenedor.Width = 500;
                    panelContenedor.Height = 40;
                    panelContenedor.Name = nombrePanelContenedor;

                    //chkSettingVariableTxt = lstListView.Items[i].Text.ToString();
                    //chkSettingVariableVal = lstListView.Items[i].SubItems[1].Text.ToString();
                    chkSettingVariableTxt = lstListView.Items[i].SubItems[1].Text.ToString();
                    chkSettingVariableVal = lstListView.Items[i].Text.ToString();

                    if (chkSettingVariableVal.Equals("true"))
                    {
                        name = chkSettingVariableTxt;
                        value = chkSettingVariableVal;

                        Panel panelContenido = new Panel();
                        panelContenido.Name = nombrePanelContenido;
                        panelContenido.Width = 495;
                        panelContenido.Height = 38;

                        int XcbProv = 0;
                        XcbProv = panelContenido.Width / 2;

                        CargarDetallesGral(name.ToString().Remove(0, 3));

                        ComboBox cbDetalleGral = new ComboBox();
                        cbDetalleGral.Name = "cb" + name;
                        cbDetalleGral.Width = 370;
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
                            cbDetalleGral.Items.Add("Selecciona " + name.ToString().Remove(0, 3));
                            cbDetalleGral.SelectedIndex = 0;
                        }
                        panelContenido.Controls.Add(cbDetalleGral);
                        panelContenedor.Controls.Add(panelContenido);
                        fLPDetalleProducto.Controls.Add(panelContenedor);

                        CheckBox checkDetalleGral = new CheckBox();
                        checkDetalleGral.Name = "chkBox" + name;
                        checkDetalleGral.Text = name.Remove(0, 3).Replace("_"," ");
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
            int valorChkBox = -1;

            nameChkBox = chkGralDetail.Name.ToString().Remove(0, 9);

            if (chkGralDetail.Checked.Equals(true))
            {
                valorChkBox = 1;
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
                                        ComboBox ComBo = (ComboBox)intoSubControlHijo;

                                        using (DataTable dtItemChckStok = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(nameChkBox, FormPrincipal.userID)))
                                        {
                                            if (!dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 1;
                                            }
                                            else if (dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 0;
                                            }
                                        }

                                        if (foundChkBox.Equals(1))
                                        {
                                            try
                                            {
                                                //var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, valorChkBox));
                                                var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(valorChkBox.ToString(), nameChkBox, ComBo.Text, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else if (foundChkBox.Equals(0))
                                        {
                                            try
                                            {
                                                //var insertChkBoxStock = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, valorChkBox));
                                                var insertChkBoxStock = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(valorChkBox.ToString(), nameChkBox, ComBo.Text, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (chkGralDetail.Checked.Equals(false))
            {
                valorChkBox = 0;
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
                                        ComboBox comBox = (ComboBox)intoSubControlHijo;
                                        comBox.SelectedIndex = 0;
                                        comBox.Enabled = false;

                                        using (DataTable dtItemChckStok = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(nameChkBox, FormPrincipal.userID)))
                                        {
                                            if (!dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 1;
                                            }
                                            else if (dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 0;
                                            }
                                        }

                                        if (foundChkBox.Equals(1))
                                        {
                                            try
                                            {
                                                var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(valorChkBox.ToString(), nameChkBox, comBox.Text, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else if (foundChkBox.Equals(0))
                                        {
                                            try
                                            {
                                                var insertChkBoxStock = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(valorChkBox.ToString(), nameChkBox, comBox.Text, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void checkBoxProveedor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbProvider = sender as CheckBox;
            string nameChkBox = string.Empty;
            int valorChkBox = -1;

            nameChkBox = cbProvider.Name.ToString().Remove(0, 9);

            if (cbProvider.Checked.Equals(true))
            {
                valorChkBox = 1;
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
                                        string textoComboBox = string.Empty;

                                        intoSubControlHijo.Enabled = true;

                                        ComboBox ComBox = (ComboBox)intoSubControlHijo;

                                        textoComboBox = ComBox.Text;

                                        using (DataTable dtItemChckStok = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(nameChkBox, FormPrincipal.userID)))
                                        {
                                            if (!dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 1;
                                            }
                                            else if (dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 0;
                                            }
                                        }

                                        if (foundChkBox.Equals(1))
                                        {
                                            try
                                            {
                                                //var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, valorChkBox));
                                                var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(valorChkBox.ToString(), nameChkBox, textoComboBox, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else if (foundChkBox.Equals(0))
                                        {
                                            try
                                            {
                                                var insertChkBoxStock = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(valorChkBox.ToString(), nameChkBox, textoComboBox, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (cbProvider.Checked.Equals(false))
            {
                valorChkBox = 0;
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
                                        ComboBox comBox = (ComboBox)intoSubControlHijo;
                                        comBox.SelectedIndex = 0;
                                        comBox.Enabled = false;

                                        using (DataTable dtItemChckStok = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(nameChkBox, FormPrincipal.userID)))
                                        {
                                            if (!dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 1;
                                            }
                                            else if (dtItemChckStok.Rows.Count.Equals(0))
                                            {
                                                foundChkBox = 0;
                                            }
                                        }

                                        if (foundChkBox.Equals(1))
                                        {
                                            try
                                            {
                                                //var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, valorChkBox));
                                                var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(valorChkBox.ToString(), nameChkBox, comBox.Text, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else if (foundChkBox.Equals(0))
                                        {
                                            try
                                            {
                                                //var insertChkBoxStock = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, valorChkBox));
                                                var insertChkBoxStock = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(valorChkBox.ToString(), nameChkBox, comBox.Text, FormPrincipal.userID));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de " + nameChkBox + "\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CargarProveedores()
        {
            // Asignamos el Array con los nombres de los proveedores al comboBox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            proveedores = new Dictionary<string, string>();

            // Comprobar que ya exista al menos un Proveedor
            if (listaProveedores.Length > 0)
            {
                proveedores.Add("0", "Selecciona Proveedor");

                foreach (var proveedor in listaProveedores)
                {
                    var tmp = proveedor.Split('-');
                    proveedores.Add(tmp[0].Trim(), tmp[1].Trim());
                }
            }
            else
            {
                proveedores.Add("0", "Selecciona Proveedor");
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
                detallesGral.Add("0", "Selecciona " + concepto.Replace("_", " "));
            
                foreach (var DetailGral in listaDetalleGral)
                {
                    var auxiliar = DetailGral.Split('|');

                    detallesGral.Add(auxiliar[0], auxiliar[1].Replace("_"," "));
                }
            }
            else
            {
                detallesGral.Add("0", "Selecciona " + concepto.Replace("_"," "));
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
            string caption = string.Empty, name = string.Empty;

            usrNo = FormPrincipal.userID;
            //path += usrNo + @"\";
            pathTemp = saveDirectoryFile + usrNo + @"\";
            if (usrNo > 0)
            {
                if (!File.Exists(pathTemp + fileName))
                {
                    Directory.CreateDirectory(pathTemp);
                    using (File.Create(pathTemp + fileName)) { }
                }
                if (File.Exists(pathTemp + fileName))
                {
                    rutaCompletaFile = pathTemp + fileName;
                    rutaCompletaTempFile = pathTemp + fileTempName;
                    if (File.Exists(rutaCompletaFile))
                    {
                        string text = File.ReadAllText(rutaCompletaFile);
                        text.Replace("\r\n", "");
                        if (!text.Length.Equals(0))
                        {
                            diccionarioDetalleBasicos.Clear();
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
                                                caption = itemCB;

                                                cbName = cb.Name;
                                                name = cbName.Remove(0, 5);
                                            }
                                            else if (!cb.Name.Equals("cbchkProveedor"))
                                            {
                                                itemCB = cb.Text;
                                                caption = itemCB;

                                                cbName = cb.Name;
                                                name = cbName.Remove(0, 5);
                                            }
                                        }
                                    }
                                    diccionarioDetalleBasicos.Add(usrNo.ToString(), new Tuple<string, string, string, string>(chkName, chkValue, itemCB, cbName));
                                    usrNo++;
                                }
                                if (caption.Equals("Selecciona " + name) && chkValue.Equals("True"))
                                {
                                    MessageBox.Show("Debe de Elegir una Opción\nde la Lista de " + name,
                                                    "Opción " + name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            using (StreamWriter file = new StreamWriter(rutaCompletaFile))
                            {
                                foreach (var entry in diccionarioDetalleBasicos)
                                {
                                    file.WriteLine("{0}|{1}|{2}|{3}|{4}", entry.Key, entry.Value.Item1, entry.Value.Item2, entry.Value.Item3, entry.Value.Item4);
                                }
                                file.Close();
                            }
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
                                file.Close();
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

        public void FiltroDinamicoLoad()
        {
            string strFiltro = string.Empty;
            int num = 0;

            setUpFiltroDinamicos.Clear();

            strFiltro = $"SELECT * FROM FiltroDinamico WHERE IDUsuario = '{FormPrincipal.userID}'";

            using (DataTable dtFiltroDinamico = cn.CargarDatos(strFiltro))
            {
                foreach (DataRow dtRow in dtFiltroDinamico.Rows)
                {
                    setUpFiltroDinamicos.Add(Convert.ToString(num), new Tuple<string, string, string>(dtRow["concepto"].ToString(), dtRow["checkBoxConcepto"].ToString(), dtRow["textCantidad"].ToString()));
                    num++;
                }
            }
        }

        private void dictionaryLoad()
        {
            bool isEmpty = (diccionarioDetalleBasicos.Count == 0);
            usrNo = FormPrincipal.userID;
            if (usrNo > 0)
            {
                if (isEmpty)
                {
                    //MessageBox.Show("El Diccionario esta vacio", "Archivo no tiene contenido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    path = saveDirectoryFile + usrNo + @"\";
                    if (!File.Exists(path + fileName))
                    {
                        Directory.CreateDirectory(path);
                        using (File.Create(path + fileName)) { }
                    }
                    else if (File.Exists(path + fileName))
                    {
                        if (new FileInfo(path + fileName).Length > 0)
                        {
                            diccionarioDetalleBasicos.Clear();
                            using (StreamReader file = new StreamReader(path + @"\" + fileName))
                            {
                                while ((line =  file.ReadLine()) != null)
                                {
                                    words = line.Split(delimiter);
                                    diccionarioDetalleBasicos.Add(words[0], new Tuple<string, string, string, string>(words[1], words[2], words[3], words[4]));
                                }
                                file.Close();
                            }
                            if (!string.IsNullOrWhiteSpace(servidor))
                            {
                                addChkBoxDiccionarioDetalleBasico();
                                setUpChkBoxDetalleProducto();
                            }
                            else if (string.IsNullOrWhiteSpace(servidor))
                            {
                                fillFieldsBasicsDetail();
                                setUpChkBoxDetalleProducto();
                            }
                        }
                        else if (new FileInfo(path + fileName).Length < 0)
                        {
                            diccionarioDetalleBasicos.Clear();
                        }
                    }
                }
                else if (!isEmpty)
                {
                    diccionarioDetalleBasicos.Clear();
                    using (StreamReader file = new StreamReader(path + usrNo + @"\" + fileName))
                    {
                        while ((line = file.ReadLine()) != null)
                        {
                            words = line.Split(delimiter);
                            diccionarioDetalleBasicos.Add(words[0], new Tuple<string, string, string, string>(words[1], words[2], words[3], words[4]));
                        }
                        file.Close();
                    }
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        addChkBoxDiccionarioDetalleBasico();
                        setUpChkBoxDetalleProducto();
                    }
                    else if (string.IsNullOrWhiteSpace(servidor))
                    {
                        fillFieldsBasicsDetail();
                        setUpChkBoxDetalleProducto();
                    }
                }
            }
            else if (usrNo.Equals(0))
            {
                MessageBox.Show("Favor de Seleccionar un valor\ndiferente o Mayor a 0 en Campo Usuario", 
                                "Error de Lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addChkBoxDiccionarioDetalleBasico()
        {
            string name = string.Empty,
                        value = string.Empty,
                        nombrePanelContenedor = string.Empty,
                        nombrePanelContenido = string.Empty;

            fLPDetalleProducto.Controls.Clear();

            foreach (var itemDiccionario in diccionarioDetalleBasicos)
            {
                name = itemDiccionario.Value.Item1.Remove(0, 6);
                value = itemDiccionario.Value.Item2;

                if (name.Equals("chkProveedor") && value.Equals("True"))
                {
                    nombrePanelContenedor = "panelContenedor" + name;
                    nombrePanelContenido = "panelContenido" + name;

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 500;
                    panelContenedor.Height = 40;
                    panelContenedor.Name = nombrePanelContenedor;
                    //panelContenedor.BackColor = Color.Aqua;

                    Panel panelContenido = new Panel();
                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 495;
                    panelContenido.Height = 38;
                    //panelContenido.BackColor = Color.Red;

                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    ComboBox cbProveedor = new ComboBox();
                    cbProveedor.Name = "cb" + name;
                    cbProveedor.Width = 370;
                    cbProveedor.Height = 21;
                    cbProveedor.Location = new Point(119, 10);
                    cbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbProveedor.Items.Add("Selecciona Proveedor");
                    cbProveedor.Items.Add(itemDiccionario.Value.Item3);
                    cbProveedor.SelectedIndex = 1;
                    cbProveedor.SelectedIndexChanged += new System.EventHandler(comboBoxProveedor_SelectValueChanged);

                    panelContenido.Controls.Add(cbProveedor);
                    panelContenedor.Controls.Add(panelContenido);
                    fLPDetalleProducto.Controls.Add(panelContenedor);

                    CheckBox check = new CheckBox();
                    check.Name = "chkBox" + name;
                    check.Text = name.Remove(0, 3);
                    check.Width = 90;
                    check.Height = 24;
                    check.Location = new Point(10, 10);
                    check.Checked = Convert.ToBoolean(itemDiccionario.Value.Item2);
                    check.CheckedChanged += checkBoxProveedor_CheckedChanged;

                    panelContenido.Controls.Add(check);
                    panelContenedor.Controls.Add(panelContenido);
                    fLPDetalleProducto.Controls.Add(panelContenedor);
                }
                else if (!name.Equals("chkProveedor") && value.Equals("true"))
                {
                    nombrePanelContenedor = "panelContenedor" + name.ToString().Remove(0, 3);
                    nombrePanelContenido = "panelContenido" + name.ToString().Remove(0, 3);

                    Panel panelContenedor = new Panel();
                    panelContenedor.Width = 500;
                    panelContenedor.Height = 40;
                    panelContenedor.Name = nombrePanelContenedor;

                    Panel panelContenido = new Panel();
                    panelContenido.Name = nombrePanelContenido;
                    panelContenido.Width = 495;
                    panelContenido.Height = 38;

                    int XcbProv = 0;
                    XcbProv = panelContenido.Width / 2;

                    ComboBox cbDetalleGral = new ComboBox();
                    cbDetalleGral.Name = "cb" + name;
                    cbDetalleGral.Width = 370;
                    cbDetalleGral.Height = 21;
                    cbDetalleGral.Location = new Point(119, 10);
                    cbDetalleGral.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbDetalleGral.Items.Add("Selecciona " + name.ToString().Remove(0, 3));
                    cbDetalleGral.Items.Add(itemDiccionario.Value.Item3);
                    cbDetalleGral.SelectedIndex = 1;
                    cbDetalleGral.SelectedIndexChanged += new System.EventHandler(ComboBoxDetalleGral_SelectValueChanged);

                    panelContenido.Controls.Add(cbDetalleGral);
                    panelContenedor.Controls.Add(panelContenido);
                    fLPDetalleProducto.Controls.Add(panelContenedor);

                    CheckBox checkDetalleGral = new CheckBox();
                    checkDetalleGral.Name = "chkBox" + name;
                    checkDetalleGral.Text = name.Remove(0, 3);
                    checkDetalleGral.Width = 90;
                    checkDetalleGral.Height = 24;
                    checkDetalleGral.Location = new Point(10, 10);
                    checkDetalleGral.Checked = Convert.ToBoolean(itemDiccionario.Value.Item2);
                    checkDetalleGral.CheckedChanged += checkBoxDetalleGral_CheckedChanged;

                    panelContenido.Controls.Add(checkDetalleGral);
                    panelContenedor.Controls.Add(panelContenido);
                    fLPDetalleProducto.Controls.Add(panelContenedor);
                }
            }
        }

        private void setUpChkBoxDetalleProducto()
        {
            var nombre = string.Empty;
            foreach (Control itemControl in fLPDetalleProducto.Controls)
            {
                nombre = itemControl.Name;
                foreach (Control subItemControl in itemControl.Controls)
                {
                    nombre = subItemControl.Name;
                    foreach (Control intoSubItemControl in subItemControl.Controls)
                    {
                        nombre = intoSubItemControl.Name;
                        foreach (var item in diccionarioDetalleBasicos)
                        {
                            if (intoSubItemControl is CheckBox && intoSubItemControl.Name.Equals(item.Value.Item1))
                            {
                                CheckBox chk = (CheckBox)intoSubItemControl;
                                chk.Checked = true;
                                chk.Checked = Convert.ToBoolean(item.Value.Item2);
                            }
                        }
                    }
                }
            }
        }

        private void fillFieldsBasicsDetail()
        {
            var nombre = string.Empty;
            foreach (Control itemControl in fLPDetalleProducto.Controls)
            {
                nombre = itemControl.Name;
                foreach (Control subItemControl in itemControl.Controls)
                {
                    nombre = subItemControl.Name;
                    foreach (Control intoSubItemControl in subItemControl.Controls)
                    {
                        nombre = intoSubItemControl.Name;
                        foreach (var item in diccionarioDetalleBasicos)
                        {
                            if (intoSubItemControl is CheckBox && intoSubItemControl.Name.Equals(item.Value.Item1))
                            {
                                CheckBox chk = (CheckBox)intoSubItemControl;
                                chk.Checked = Convert.ToBoolean(item.Value.Item2);
                            }
                            if (intoSubItemControl is ComboBox && intoSubItemControl.Name.Equals(item.Value.Item4))
                            {
                                ComboBox cb = (ComboBox)intoSubItemControl;
                                cb.Text = item.Value.Item3;
                            }
                        }
                    }
                }
            }
        }
        #endregion Dictionary Values

        private void WinQueryString_Load(object sender, EventArgs e)
        {
            cbTipoFiltroCombProdServ.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipoFiltroImagen.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipoFiltroPrecio.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipoFiltroRevision.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipoFiltroStock.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            
            validarConexionServidor();
            path = saveDirectoryFile;

            validarCheckBoxFijos();

            loadFromConfigDB();
            BuscarChkBoxListView(chkDatabase);

            validarCheckBoxFiltrosDinamicos();

            LLenarFiltrosDinamicosDeVetanaFiltros();
        }

        private void validarConexionServidor()
        {
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                DirectoryInfo dirDicc = new DirectoryInfo($@"\\{servidor}\PUDVE\settings\Dictionary");

                if (dirDicc.Exists)
                {
                    saveDirectoryFile = $@"\\{servidor}\PUDVE\settings\Dictionary\";
                }
            }
            else
            {
                saveDirectoryFile = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\settings\Dictionary\";
            }
        }

        private void validarCheckBoxFijos()
        {
            string strOperadorAndCant;
            string[] strList;
            char[] separator = { ' ' };
            bool valueCheckBox = false;

            using (DataTable dtFiltroProducto = cn.CargarDatos(cs.BuscarFiltroProducto(FormPrincipal.userID)))
            {
                // Condiciones para valorar los procesos del CheckBox de Stock
                #region Verificar CheckBox
                // Verificamos si la tabla tiene registros
                if (!dtFiltroProducto.Rows.Count.Equals(0))
                {
                    // Recorremos la tabla
                    foreach (DataRow drFiltroProducto in dtFiltroProducto.Rows)
                    {
                        // Tomamos el valor si es true o false
                        if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("1"))
                        {
                            valueCheckBox = true;
                        }
                        else if (drFiltroProducto["checkBoxConcepto"].ToString().Equals("0"))
                        {
                            valueCheckBox = false;
                        }

                        if (valueCheckBox.Equals(true))
                        {
                            // Verificamos si es Stock, Precio y Revision
                            if (drFiltroProducto["concepto"].Equals("chkBoxStock"))
                            {
                                chkBoxStock.Checked = valueCheckBox;

                                strOperadorAndCant = drFiltroProducto["textComboBoxConcepto"].ToString() + drFiltroProducto["textCantidad"].ToString();

                                if (!strOperadorAndCant.Equals(""))
                                {
                                    strList = strOperadorAndCant.Split(separator);
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
                                    txtCantStock.Text = strList[2].ToString();
                                }
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxPrecio"))
                            {
                                chkBoxPrecio.Checked = valueCheckBox;

                                strOperadorAndCant = drFiltroProducto["textComboBoxConcepto"].ToString() + drFiltroProducto["textCantidad"].ToString();

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
                            else if (drFiltroProducto["concepto"].Equals("chkBoxRevision"))
                            {
                                chkBoxRevision.Checked = valueCheckBox;

                                strOperadorAndCant = drFiltroProducto["textComboBoxConcepto"].ToString() + drFiltroProducto["textCantidad"].ToString();

                                if (!strOperadorAndCant.Equals(""))
                                {
                                    strList = strOperadorAndCant.Split(separator);
                                    if (strList.Length > 1)
                                    {
                                        txtNoRevision.Text = strList[2].ToString();
                                        if (strList[1].ToString().Equals(">="))
                                        {
                                            cbTipoFiltroRevision.SelectedIndex = 1;
                                        }
                                        else if (strList[1].ToString().Equals("<="))
                                        {
                                            cbTipoFiltroRevision.SelectedIndex = 2;
                                        }
                                        else if (strList[1].ToString().Equals("="))
                                        {
                                            cbTipoFiltroRevision.SelectedIndex = 3;
                                        }
                                        else if (strList[1].ToString().Equals(">"))
                                        {
                                            cbTipoFiltroRevision.SelectedIndex = 4;
                                        }
                                        else if (strList[1].ToString().Equals("<"))
                                        {
                                            cbTipoFiltroRevision.SelectedIndex = 5;
                                        }
                                    }
                                }
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxImagen"))
                            {
                                string textoCombo = string.Empty;

                                chkBoxImagen.Checked = valueCheckBox;

                                textoCombo = drFiltroProducto["textComboBoxConcepto"].ToString();

                                if (!textoCombo.Equals(""))
                                {
                                    strList = textoCombo.Split(separator);

                                    if (strList[1].ToString().Equals("<>"))
                                    {
                                        cbTipoFiltroImagen.SelectedIndex = 1;
                                    }
                                    else if (strList[1].ToString().Equals("="))
                                    {
                                        cbTipoFiltroImagen.SelectedIndex = 2;
                                    }
                                }
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxTipo"))
                            {
                                string textoCombo = string.Empty;

                                chkBoxTipo.Checked = valueCheckBox;

                                textoCombo = drFiltroProducto["textComboBoxConcepto"].ToString().Remove(0, 7);

                                cbTipoFiltroCombProdServ.Enabled = true;

                                if (textoCombo.Equals("No Aplica") || textoCombo.Equals(""))
                                {
                                    cbTipoFiltroCombProdServ.SelectedIndex = 0;
                                }
                                else if (textoCombo.Equals("PQ"))
                                {
                                    cbTipoFiltroCombProdServ.SelectedIndex = 1;
                                }
                                else if (textoCombo.Equals("P"))
                                {
                                    cbTipoFiltroCombProdServ.SelectedIndex = 2;
                                }
                                else if (textoCombo.Equals("S"))
                                {
                                    cbTipoFiltroCombProdServ.SelectedIndex = 3;
                                }
                            }
                        }
                        else if (valueCheckBox.Equals(false))
                        {
                            // Verificamos si es Stock, Precio y Revision
                            if (drFiltroProducto["concepto"].Equals("chkBoxStock"))
                            {
                                chkBoxStock.Checked = true;
                                chkBoxStock.Checked = valueCheckBox;
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxPrecio"))
                            {
                                chkBoxPrecio.Checked = true;
                                chkBoxPrecio.Checked = valueCheckBox;
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxRevision"))
                            {
                                chkBoxRevision.Checked = true;
                                chkBoxRevision.Checked = valueCheckBox;
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxImagen"))
                            {
                                chkBoxImagen.Checked = true;
                                chkBoxImagen.Checked = valueCheckBox;
                            }
                            else if (drFiltroProducto["concepto"].Equals("chkBoxTipo"))
                            {
                                chkBoxTipo.Checked = true;
                                chkBoxTipo.Checked = valueCheckBox;
                            }
                        }
                    }
                }
                else if (dtFiltroProducto.Rows.Count.Equals(0))
                {
                    // CheckBox de Stock
                    chkBoxStock.Checked = true;
                    chkBoxStock.Checked = false;
                    // CheckBox de Precio
                    chkBoxPrecio.Checked = true;
                    chkBoxPrecio.Checked = false;
                    // CheckBox de Revision
                    chkBoxRevision.Checked = true;
                    chkBoxRevision.Checked = false;
                    // CheckBox de Imagen
                    chkBoxImagen.Checked = true;
                    chkBoxImagen.Checked = false;
                    // CheckBox de Tipo
                    chkBoxTipo.Checked = true;
                    chkBoxTipo.Checked = false;
                }
                #endregion Verificar CheckBox
            }
        }

        private void validarCheckBoxFiltrosDinamicos()
        {
            using (DataTable dtValidarCheckBoxFiltrosDinamicos = cn.CargarDatos(cs.VerificarVentanaFiltros(FormPrincipal.userID)))
            {
                if (!dtValidarCheckBoxFiltrosDinamicos.Rows.Count.Equals(0))
                {
                    foreach (DataRow drValidarCheckBox in dtValidarCheckBoxFiltrosDinamicos.Rows)
                    {
                        // Recorremos el Panel Principal de Detalles Dinamicos
                        foreach (Control itemfLPDetalleProducto in fLPDetalleProducto.Controls)
                        {
                            if (itemfLPDetalleProducto is Panel)
                            {
                                // Recorremos el control que encuentre y sus elementos que contiene
                                foreach (Control subItemfLDetalleProducto in itemfLPDetalleProducto.Controls)
                                {
                                    if (subItemfLDetalleProducto is Panel)
                                    {
                                        string NombrePanel = string.Empty, NombreConcepto = string.Empty, NombreConcepto1 = string.Empty;

                                        NombrePanel = subItemfLDetalleProducto.Name.ToString();
                                        NombreConcepto = "panelContenidochk" + drValidarCheckBox["concepto"].ToString();
                                        NombreConcepto1 = "panelContenido" + drValidarCheckBox["concepto"].ToString();

                                        if (NombrePanel.Equals(NombreConcepto) || NombrePanel.Equals(NombreConcepto1))
                                        {
                                            // recorremos el Panel y sus elementos
                                            foreach (Control intoSubItemfLDetalleProducto in subItemfLDetalleProducto.Controls)
                                            {
                                                if (intoSubItemfLDetalleProducto is ComboBox)
                                                {
                                                    ComboBox ComBo = (ComboBox)intoSubItemfLDetalleProducto;
                                                    ComBo.Text = drValidarCheckBox["strFiltro"].ToString();
                                                }
                                                else if (intoSubItemfLDetalleProducto is CheckBox)
                                                {
                                                    CheckBox ChkBox = (CheckBox)intoSubItemfLDetalleProducto;
                                                    if (drValidarCheckBox["checkBoxValue"].ToString().Equals("1"))
                                                    {
                                                        ChkBox.Checked = false;
                                                        ChkBox.Checked = true;
                                                    }
                                                    else if (drValidarCheckBox["checkBoxValue"].ToString().Equals("0"))
                                                    {
                                                        ChkBox.Checked = true;
                                                        ChkBox.Checked = false;
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

        private void LLenarFiltrosDinamicosDeVetanaFiltros()
        {
            List<string> OpcionesVentanaFiltros = new List<string>();
            string cadFiltro = string.Empty;
            string[] strArray, auxArray;
            int EmptyFuel = -1;

            foreach (Control itemPanelDetalleProducto in fLPDetalleProducto.Controls)
            {
                if (itemPanelDetalleProducto is Panel)
                {
                    foreach (Control subItemPanelDetalleProducto in itemPanelDetalleProducto.Controls)
                    {
                        if (subItemPanelDetalleProducto is Panel)
                        {
                            foreach (Control intoSubItemPanelDetalleProducto in subItemPanelDetalleProducto.Controls)
                            {
                                if (intoSubItemPanelDetalleProducto is ComboBox)
                                {
                                    ComboBox ComBo = (ComboBox)intoSubItemPanelDetalleProducto;
                                    cadFiltro += ComBo.Text;
                                }
                                else if (intoSubItemPanelDetalleProducto is CheckBox)
                                {
                                    CheckBox chkBox = (CheckBox)intoSubItemPanelDetalleProducto;
                                    string valueChkBox = string.Empty;

                                    if (chkBox.Checked.Equals(true))
                                    {
                                        valueChkBox = "1";
                                    }
                                    else if (chkBox.Checked.Equals(false))
                                    {
                                        valueChkBox = "0";
                                    }
                                    cadFiltro += "|" + valueChkBox + "|" + chkBox.Text + "¬";
                                }
                            }
                        }
                    }
                }
            }

            using (DataTable dtVerificarVentanaFiltros = cn.CargarDatos(cs.VerificarVentanaFiltros(FormPrincipal.userID)))
            {
                if (dtVerificarVentanaFiltros.Rows.Count.Equals(0))
                {
                    EmptyFuel = 0;
                }
                else if (!dtVerificarVentanaFiltros.Rows.Count.Equals(0))
                {
                    EmptyFuel = 1;
                }
            }

            if (!cadFiltro.Equals(""))
            {
                strArray = cadFiltro.Remove(cadFiltro.Length - 1).Split('¬');

                foreach (var item in strArray)
                {
                    auxArray = item.Split('|');
                    if (EmptyFuel.Equals(0))
                    {
                        try
                        {
                            var agregarVentanaFiltros = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(auxArray[1].ToString(), auxArray[2].ToString(), auxArray[0].ToString(), FormPrincipal.userID));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al Intentar Guardar los Filtros Dinamicos :" + ex.Message.ToString(), "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (EmptyFuel.Equals(1))
                    {
                        using (DataTable dtChecarDatoDelFiltro = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(auxArray[2].ToString(), FormPrincipal.userID)))
                        {
                            if (dtChecarDatoDelFiltro.Rows.Count.Equals(0))
                            {
                                try
                                {
                                    var agregarVentanaFiltros = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(auxArray[1].ToString(), auxArray[2].ToString(), auxArray[0].ToString(), FormPrincipal.userID));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Intentar Guardar los Filtros Dinamicos :" + ex.Message.ToString(), "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (!dtChecarDatoDelFiltro.Rows.Count.Equals(0))
                            {
                                try
                                {
                                    var ActualizarVentanaFiltros = cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(auxArray[1].ToString(), auxArray[2].ToString(), auxArray[0].ToString(), FormPrincipal.userID));
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al Intentar Actualizar los Filtros Dinamicos :" + ex.Message.ToString(), "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void loadFromConfigDB()
        {
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
                        MessageBox.Show("No cuenta con Cofiguración en su sistema", "Sin Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de lectura de los Datos Dinamicos: {0}" + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //var servidor = Properties.Settings.Default.Hosting;

            //if (string.IsNullOrWhiteSpace(servidor))
            //{
            //    chkDatabase.Items.Clear();
            //    settingDatabases.Items.Clear();

            //    lvi = new ListViewItem();

            //    try
            //    {
            //        chkDatabase.Clear();
            //        settingDatabases.Clear();

            //        using (DataTable dtChecarSihayDatosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
            //        {
            //            if (dtChecarSihayDatosDinamicos.Rows.Count > 0)
            //            {
            //                foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
            //                {
            //                    connStr = row["textComboBoxConcepto"].ToString();
            //                    if (row["checkBoxComboBoxConcepto"].ToString().Equals("1"))
            //                    {
            //                        keyName = "true";
            //                    }
            //                    else if (row["checkBoxComboBoxConcepto"].ToString().Equals("0"))
            //                    {
            //                        keyName = "false";
            //                    }
            //                    lvi = new ListViewItem(keyName);
            //                    lvi.SubItems.Add(connStr);
            //                    chkDatabase.Items.Add(lvi);
            //                }
            //                foreach (DataRow row in dtChecarSihayDatosDinamicos.Rows)
            //                {
            //                    connStr = row["concepto"].ToString();
            //                    if (row["checkBoxConcepto"].ToString().Equals("1"))
            //                    {
            //                        keyName = "true";
            //                    }
            //                    else if (row["checkBoxConcepto"].ToString().Equals("0"))
            //                    {
            //                        keyName = "false";
            //                    }
            //                    lvi = new ListViewItem(keyName);
            //                    lvi.SubItems.Add(connStr);
            //                    settingDatabases.Items.Add(lvi);
            //                }
            //            }
            //            else if (dtChecarSihayDatosDinamicos.Rows.Count == 0)
            //            {
            //                MessageBox.Show("No cuenta con Cofiguración en su sistema", "Sin Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error de lectura de los Datos Dinamicos: {0}" + ex.Message.ToString(), "Error de Lecturas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
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
                            }
                        }
                    }
                }
            }
        }

        private void validarChkBoxStock()
        {
            nameChkBox = chkBoxStock.Name;

            cbTipoFiltroStock.SelectedIndex = 0;

            if (chkBoxStock.Checked.Equals(true))
            {
                filtroStock = Convert.ToBoolean(chkBoxStock.Checked);

                if (filtroStock.Equals(true))
                {
                    chkValor = 1;
                }

                //Properties.Settings.Default.chkFiltroStock = filtroStock;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                txtCantStock.Enabled = true;
                cbTipoFiltroStock.Enabled = true;
                txtCantStock.Text = "0.0";
                txtCantStock.Focus();
            }
            else if (chkBoxStock.Checked.Equals(false))
            {
                filtroStock = Convert.ToBoolean(chkBoxStock.Checked);

                if (filtroStock.Equals(false))
                {
                    chkValor = 0;
                }

                //Properties.Settings.Default.chkFiltroStock = filtroStock;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                txtCantStock.Text = "0.0";
                txtCantStock.Enabled = false;
                cbTipoFiltroStock.SelectedIndex = 0;
                cbTipoFiltroStock.Enabled = false;
            }

            using (DataTable dtItemChckStok = cn.CargarDatos(cs.VerificarChk(nameChkBox, FormPrincipal.userID)))
            {
                if (!dtItemChckStok.Rows.Count.Equals(0))
                {
                    foundChkBox = 1;
                }
                else if (dtItemChckStok.Rows.Count.Equals(0))
                {
                    foundChkBox = 0;
                }
            }

            if (foundChkBox.Equals(1))
            {
                try
                {
                    var updateChkBoxStock = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de Stock\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (foundChkBox.Equals(0))
            {
                try
                {
                    var insertChkBoxStock = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de Stock\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void validarChkBoxPrecio()
        {
            nameChkBox = chkBoxPrecio.Name;

            cbTipoFiltroPrecio.SelectedIndex = 0;
            if (chkBoxPrecio.Checked.Equals(true))
            {
                filtroPrecio = Convert.ToBoolean(chkBoxPrecio.Checked);

                if (filtroPrecio.Equals(true))
                {
                    chkValor = 1;
                }

                //Properties.Settings.Default.chkFiltroPrecio = filtroPrecio;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                txtCantPrecio.Enabled = true;
                cbTipoFiltroPrecio.Enabled = true;
                txtCantPrecio.Text = "0.0";
                txtCantPrecio.Focus();
            }
            else if (chkBoxPrecio.Checked.Equals(false))
            {
                filtroPrecio = Convert.ToBoolean(chkBoxPrecio.Checked);

                if (filtroPrecio.Equals(false))
                {
                    chkValor = 0;
                }

                //Properties.Settings.Default.chkFiltroPrecio = filtroPrecio;
                //Properties.Settings.Default.Save();
                //Properties.Settings.Default.Reload();

                txtCantPrecio.Enabled = false;
                cbTipoFiltroPrecio.SelectedIndex = 0;
                cbTipoFiltroPrecio.Enabled = false;
                txtCantPrecio.Text = "0.0";
            }

            using (DataTable dtItemChckPrecio = cn.CargarDatos(cs.VerificarChk(nameChkBox, FormPrincipal.userID)))
            {
                if (!dtItemChckPrecio.Rows.Count.Equals(0))
                {
                    foundChkBox = 1;
                }
                else if (dtItemChckPrecio.Rows.Count.Equals(0))
                {
                    foundChkBox = 0;
                }
            }

            if (foundChkBox.Equals(1))
            {
                try
                {
                    var updateChkBoxPrecio = cn.EjecutarConsulta(cs.ActualizarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar actualizar la configuración\nde la casilla de Verificación de Precio\n" + ex.Message.ToString(), "Error de actualización de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (foundChkBox.Equals(0))
            {
                try
                {
                    var insertChkBoxPrecio = cn.EjecutarConsulta(cs.InsertarChk(nameChkBox, chkValor));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar guardar la configuración\nde la casilla de Verificación de Precio\n" + ex.Message.ToString(), "Error de guardado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkBoxStock_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBoxStock();
        }

        private void cbTipoFiltroPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtroPrecio = Properties.Settings.Default.chkFiltroPrecio;
            filtroPrecio = chkBoxPrecio.Checked;

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
            }
            else if (filtroPrecio.Equals(false))
            {
                strFiltroPrecio = "No Aplica";
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
            filtroStock = chkBoxStock.Checked;

            cbTipoFiltroPrecio_SelectedIndexChanged(sender, e);
            filtroPrecio = chkBoxPrecio.Checked;

            cbTipoFiltroCombProdServ_SelectedIndexChanged(sender, e);
            filtroTipo = chkBoxTipo.Checked;

            cbTipoFiltroRevision_SelectedIndexChanged(sender, e);
            filtroRevision = chkBoxRevision.Checked;

            cbTipoFiltroImagen_SelectedIndexChanged(sender, e);
            filtroImagen = chkBoxImagen.Checked;

            //DialogResult result = MessageBox.Show("Desea Guardar el Filtro\no editar su elección", "Guardado del Filtro", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //if (result == DialogResult.Yes)
            //{
                if (filtroStock.Equals(true))
                {
                    strTxtStock = txtCantStock.Text;

                    if (!strTxtStock.Equals(""))
                    {
                        if (!strOpcionCBStock.Equals("No Aplica"))
                        {
                            nameChkBox = chkBoxStock.Name;

                            var datosFiltroStock = mb.ObtenerDatosFiltro(nameChkBox, FormPrincipal.userID);

                            if (!datosFiltroStock.Count().Equals(0))
                            {
                                cn.EjecutarConsulta(cs.ActualizarTextCBConceptoCantidad(Convert.ToInt32(datosFiltroStock[0].ToString()), strFiltroStock, strTxtStock));
                            }
                            else if (datosFiltroStock.Count().Equals(0))
                            {
                                cn.EjecutarConsulta(cs.InsertarTextCBConceptoCantidad(strFiltroStock, strTxtStock));
                            }

                            strFiltroStock += strTxtStock;
                        }
                        else if (strOpcionCBStock.Equals("No Aplica"))
                        {
                            //Properties.Settings.Default.strFiltroStock = string.Empty;
                            MessageBox.Show("Debe de Elegir una Opción\ndel Campo de Stock", "Selección Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCantStock.Focus();
                            return;
                        }
                    }
                    else if (strTxtStock.Equals(""))
                    {
                        strFiltroStock = "";
                        MessageBox.Show("Favor de Introducir una\nCantidad en el Campo de Stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantStock.Focus();
                        return;
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
                            nameChkBox = chkBoxPrecio.Name;

                            var datosFiltrosPrecio = mb.ObtenerDatosFiltro(nameChkBox, FormPrincipal.userID);

                            if (!datosFiltrosPrecio.Count().Equals(0))
                            {
                                cn.EjecutarConsulta(cs.ActualizarTextCBConceptoCantidad(Convert.ToInt32(datosFiltrosPrecio[0].ToString()), strFiltroPrecio, strTxtPrecio));
                            }
                            else if (datosFiltrosPrecio.Count().Equals(0))
                            {
                                cn.EjecutarConsulta(cs.InsertarTextCBConceptoCantidad(strFiltroPrecio, strTxtPrecio));
                            }

                            strFiltroPrecio += strTxtPrecio;
                        }
                        else if (strOpcionCBPrecio.Equals("No Aplica"))
                        {
                            //Properties.Settings.Default.strFiltroPrecio = string.Empty;
                            MessageBox.Show("Debe de Elegir una Opción\ndel Campo de Precio", "Selección Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCantPrecio.Focus();
                            return;
                        }
                    }
                    else if (strTxtPrecio.Equals(""))
                    {
                        strFiltroPrecio = "";
                        MessageBox.Show("Favor de Introducir una\nCantidad en el Campo de Precio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantPrecio.Focus();
                        return;
                    }
                }
                else if (filtroPrecio.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Precio.");
                }
                if (filtroTipo.Equals(true))
                {
                    if (strFiltroCombProdServ.Equals("No Aplica") || strFiltroCombProdServ.Equals(""))
                    {
                        MessageBox.Show("Debe de Elegir una Opción\ndel Campo de Tipo", "Selección Tipo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbTipoFiltroCombProdServ.Focus();
                        return;
                    }
                    else if (!strFiltroCombProdServ.Equals("No Aplica") || !strFiltroCombProdServ.Equals("Tipo "))
                    {
                        nameChkBox = chkBoxTipo.Name;

                        var datosFiltrosTipo = mb.ObtenerDatosFiltro(nameChkBox, FormPrincipal.userID);

                        if (!datosFiltrosTipo.Count().Equals(0))
                        {
                            string[] words = strFiltroCombProdServ.Split(' ');
                            string filtro = string.Empty;

                            filtro += words[0].ToString() + " " + words[1].ToString() + " " + words[2].ToString().Replace("'", string.Empty);
                            cn.EjecutarConsulta(cs.ActualizarTextCBConceptoCantidad(Convert.ToInt32(datosFiltrosTipo[0].ToString()), filtro, string.Empty));
                        }
                        else if (datosFiltrosTipo.Count().Equals(0))
                        {
                            string[] words = strFiltroCombProdServ.Split(' ');
                            string filtro = string.Empty;

                            filtro += words[0].ToString() + " " + words[1].ToString() + " " + words[2].ToString().Replace("'", string.Empty);
                            cn.EjecutarConsulta(cs.InsertarTextCBConceptoCantidad(nameChkBox, filtro));
                        }
                    }
                }
                else if (filtroTipo.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Tipo.");
                }
                if (filtroRevision.Equals(true))
                {
                    if (strFiltroNoRevision.Equals("No Aplica") || strFiltroNoRevision.Equals(""))
                    {
                        MessageBox.Show("Debe de Elegir una Opción\ndel Campo de Revisión", "Selección Revisión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbTipoFiltroRevision.Focus();
                        return;
                    }
                    else if (!strFiltroNoRevision.Equals("No Aplica") || !strFiltroNoRevision.Equals(""))
                    {
                        strTxtNoRevision = txtNoRevision.Text;

                        nameChkBox = chkBoxRevision.Name;

                        var datosFiltrosRevision = mb.ObtenerDatosFiltro(nameChkBox, FormPrincipal.userID);

                        if (!datosFiltrosRevision.Count().Equals(0))
                        {
                            cn.EjecutarConsulta(cs.ActualizarTextCBConceptoCantidad(Convert.ToInt32(datosFiltrosRevision[0].ToString()), strFiltroNoRevision, strTxtNoRevision));
                        }
                        else if (datosFiltrosRevision.Count().Equals(0))
                        {
                            cn.EjecutarConsulta(cs.InsertarTextCBConceptoCantidad(strFiltroNoRevision, strTxtNoRevision));
                        }

                        //strFiltroNoRevision
                        strFiltroNoRevision += $"{strTxtNoRevision}";
                    }
                }
                else if (filtroRevision.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Revisón.");
                }
                if (filtroImagen.Equals(true))
                {
                    if (strFiltroImagen.Equals("No Aplica") || strFiltroImagen.Equals(""))
                    {
                        MessageBox.Show("Debe de Elegit una Opción\ndel Campo de Imagen", "Selección Imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cbTipoFiltroImagen.Focus();
                        return;
                    }
                    else if (!strFiltroImagen.Equals("No Aplica") || !strFiltroImagen.Equals(""))
                    {
                        nameChkBox = chkBoxImagen.Name;

                        var datosFiltroImagen = mb.ObtenerDatosFiltro(nameChkBox, FormPrincipal.userID);

                        if (!datosFiltroImagen.Count().Equals(0))
                        {
                            string[] words = strFiltroImagen.Split(' ');
                            string filtro = string.Empty;

                            filtro += words[0].ToString() + " " + words[1].ToString() + " ''''";
                            //MessageBox.Show("Filtro: " + filtro.ToString());
                            cn.EjecutarConsulta(cs.ActualizarTextCBConceptoCantidad(Convert.ToInt32(datosFiltroImagen[0].ToString()), filtro, string.Empty));
                        }
                        else if (datosFiltroImagen.Count().Equals(0))
                        {
                            string[] words = strFiltroImagen.Split(' ');
                            string filtro = string.Empty;

                            filtro += words[0].ToString() + " " + words[1].ToString() + " ''''";
                            //MessageBox.Show("Filtro: " + filtro.ToString());
                            cn.EjecutarConsulta(cs.InsertarTextCBConceptoCantidad(nameChkBox, filtro));
                        }
                    }
                }
                else if (filtroImagen.Equals(false))
                {
                    //MessageBox.Show("Que Paso...\nFalta Seleccionar Imagen.");
                }

                saveConfigGarlDB();

                saveDictionary();

                this.Close();
            //}
            //else if (result == DialogResult.No)
            //{
            //    this.Close();
            //}
            //else if (result == DialogResult.Cancel)
            //{
            //    txtCantStock.Focus();
            //}
        }

        private void saveConfigGarlDB()
        {
            List<string> tuplaConfigDB = new List<string>();

            string concepto = string.Empty,
                    checkBoxConcepto = string.Empty,
                    textComboBoxConcepto = string.Empty;

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
                                    CheckBox chkBox = (CheckBox)intoSubControlHijo;
                                    concepto = chkBox.Name;
                                    if (chkBox.Checked.Equals(true))
                                    {
                                        checkBoxConcepto = "1";
                                    }
                                    else if (chkBox.Checked.Equals(false))
                                    {
                                        checkBoxConcepto = "0";
                                    }
                                }
                                if (intoSubControlHijo is ComboBox)
                                {
                                    ComboBox comBox = (ComboBox)intoSubControlHijo;
                                    textComboBoxConcepto = comBox.Text;
                                }
                            }
                            tuplaConfigDB.Add(checkBoxConcepto + "|" + concepto.Remove(0, 9) + "|" + textComboBoxConcepto + "|" + Convert.ToString(FormPrincipal.userID));
                        }
                    }
                }
            }
            foreach (var itemRow in tuplaConfigDB)
            {
                int foudDatoDinamico = -1;
                string[] words;

                words = itemRow.Split('|');

                using (DataTable dtFiltrosDinamicosVetanaFiltros = cn.CargarDatos(cs.BuscarDatoEnVentanaFiltros(words[1].ToString(), FormPrincipal.userID)))
                {
                    if (!dtFiltrosDinamicosVetanaFiltros.Rows.Count.Equals(0))
                    {
                        foudDatoDinamico = 1;
                    }
                    else if (dtFiltrosDinamicosVetanaFiltros.Rows.Count.Equals(0))
                    {
                        foudDatoDinamico = 0;
                    }
                }

                if (foudDatoDinamico.Equals(1))
                {
                    try
                    {
                        var UpdateDatoDinamico = cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(words[0].ToString(), words[1].ToString(), words[2].ToString(), Convert.ToInt32(words[3].ToString())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Actualizar Filtros Dinamicos: Error: " + ex.Message.ToString(), "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (foudDatoDinamico.Equals(0))
                {
                    try
                    {
                        var AgregarDatoDinamico = cn.EjecutarConsulta(cs.GuardarVentanaFiltros(words[0].ToString(), words[1].ToString(), words[2].ToString(), Convert.ToInt32(words[3].ToString())));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al Agregar Filtros Dinamicos: Error: " + ex.Message.ToString(), "Error de Guardardo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cbTipoFiltroStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtroStock = Properties.Settings.Default.chkFiltroStock;
            filtroStock = chkBoxStock.Checked;

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
            }
            else if (filtroStock.Equals(false))
            {
                strFiltroStock = "No Aplica";
            }
        }

        private void WinQueryString_FormClosed(object sender, FormClosedEventArgs e)
        {
            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.actualizarBtnFiltro();
                producto.borrarVariablesStockPrecio();
                producto.CargarDatos();
                producto.cargarListaSetUpVaribale();
                producto.borrarEtiquetasDinamicasSetUpDinamicos();

                var servidor = Properties.Settings.Default.Hosting;
                if (!servidor.Equals(""))
                {
                    producto.dictionaryLoad();
                }
                else if (servidor.Equals(""))
                {
                    producto.FiltroDinamicoLoad();
                }

                producto.verificarBotonLimpiarTags();
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            LimpiarCheckBoxDinamicos();
            saveDictionary();

            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.inicializarVariablesFiltro();
            }

            this.Close();
        }

        private void LimpiarCheckBoxDinamicos()
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
                                if ((intoSubControlHijo.Name.Equals("chkBoxchkProveedor")) && (intoSubControlHijo is CheckBox))
                                {
                                    CheckBox chkBox = (CheckBox)intoSubControlHijo;
                                    chkBox.Checked = false;

                                    string  valorCheckBox = string.Empty, 
                                            conceptoCheckBox = string.Empty, 
                                            cadenaFiltro = string.Empty;
                                    
                                    if (chkBox.Checked.Equals(true))
                                    {
                                        valorCheckBox = "1";
                                        conceptoCheckBox = intoSubControlHijo.Name.ToString().Remove(0, 9);
                                        cadenaFiltro = "Selecciona " + conceptoCheckBox;
                                    }
                                    else if (chkBox.Checked.Equals(false))
                                    {
                                        valorCheckBox = "0";
                                        conceptoCheckBox = intoSubControlHijo.Name.ToString().Remove(0, 9);
                                        cadenaFiltro = "Selecciona " + conceptoCheckBox;
                                    }
                                    cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(valorCheckBox, conceptoCheckBox, cadenaFiltro, FormPrincipal.userID));
                                }
                            }
                        }
                    }
                }
            }
            foreach (Control controlHijo in fLPDetalleProducto.Controls)
            {
                if ((!controlHijo.Name.Equals("panelContenedorchkProveedor")) && (controlHijo is Panel))
                {
                    foreach (Control subControlHijo in controlHijo.Controls)
                    {
                        if ((!subControlHijo.Name.Equals("panelContenidochkProveedor")) && (subControlHijo is Panel))
                        {
                            foreach (Control intoSubControlHijo in subControlHijo.Controls)
                            {
                                if ((!intoSubControlHijo.Name.Equals("chkBoxchkProveedor")) && (intoSubControlHijo is CheckBox))
                                {
                                    CheckBox chkBox = (CheckBox)intoSubControlHijo;
                                    chkBox.Checked = false;


                                    string valorCheckBox = string.Empty,
                                            conceptoCheckBox = string.Empty,
                                            cadenaFiltro = string.Empty;

                                    if (chkBox.Checked.Equals(true))
                                    {
                                        valorCheckBox = "1";
                                        conceptoCheckBox = intoSubControlHijo.Name.ToString().Remove(0, 9);
                                        cadenaFiltro = "Selecciona " + conceptoCheckBox;
                                    }
                                    else if (chkBox.Checked.Equals(false))
                                    {
                                        valorCheckBox = "0";
                                        conceptoCheckBox = intoSubControlHijo.Name.ToString().Remove(0, 9);
                                        cadenaFiltro = "Selecciona " + conceptoCheckBox;
                                    }
                                    cn.EjecutarConsulta(cs.ActualizarDatoVentanaFiltros(valorCheckBox, conceptoCheckBox, cadenaFiltro, FormPrincipal.userID));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
