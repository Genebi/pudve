using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarEditarBascula : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string firtsItemBasculasRegistradas = "REGISTRADAS...";

        bool isOpen = false, 
             isExists = false,
             saveEdit = false;

        #region DISPOSITIVO-LECTOR BASCULA
        public SerialPort PuertoSerieBascula;
        public static string informacionBascula;

        public void InicializaPuertoBascula(string puerto, int baud)
        {
            if (puerto != "" && puerto != string.Empty)
            {
                PuertoSerieBascula = new SerialPort(puerto, baud);

                if (!PuertoSerieBascula.IsOpen)
                {
                    if (!cbParidad.Text.Equals(string.Empty) || !cbParidad.Equals("Ningun dato de Paridad encontrado"))
                    {
                        PuertoSerieBascula.Parity = (Parity)Enum.Parse(typeof(Parity), cbParidad.Text.ToString());
                    }
                    else
                    {
                        PuertoSerieBascula.Parity = Parity.None;
                    }

                    if (!cbStopBits.Text.Equals(string.Empty) || !cbStopBits.Equals("Ningun StopBits encontrado"))
                    {
                        PuertoSerieBascula.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text.ToString());
                    }
                    else
                    {
                        PuertoSerieBascula.StopBits = StopBits.One;
                    }

                    if (!cbDatos.Text.Equals("No se encontraron DataBit") || !cbDatos.Text.Equals(string.Empty))
                    {
                        PuertoSerieBascula.DataBits = (int)Int32.Parse(cbDatos.Text.ToString().Replace(" bit", string.Empty));
                    }
                    else
                    {
                        PuertoSerieBascula.DataBits = 8;
                    }

                    if (!cbHandshake.Text.Equals("Ningun Handshake encontrado") || !cbHandshake.Text.Equals(string.Empty))
                    {
                        PuertoSerieBascula.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cbHandshake.Text.ToString());
                    }
                    else
                    {
                        PuertoSerieBascula.Handshake = Handshake.None;
                    }

                    PuertoSerieBascula.ReadTimeout = 4800;

                    PuertoSerieBascula.DataReceived += new SerialDataReceivedEventHandler(this.leerBascula);

                    PuertoSerieBascula.ErrorReceived += new SerialErrorReceivedEventHandler(PuertoSerieBascula_ErrorReceived);

                    try
                    {
                        PuertoSerieBascula.Open();
                        isExists = true;
                    }
                    catch (Exception error)
                    {
                        isExists = false;
                        MessageBox.Show("Error al abrir el dispositivo (Bascula) ...\r" + error.Message.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("El puerto está abierto...", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void PuertoSerieBascula_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialError.Frame:
                    MessageBox.Show("Error de Trama...", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case SerialError.Overrun:
                    MessageBox.Show("Saturación de buffer...", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case SerialError.RXOver:
                    MessageBox.Show("Desboradamiento de buffer de entrada", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case SerialError.RXParity:
                    MessageBox.Show("Error de paridad...", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case SerialError.TXFull:
                    MessageBox.Show("Buffer lleno...", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

            throw new NotImplementedException();
        }

        private void leerBascula(object sender, SerialDataReceivedEventArgs e)
        {
            informacionBascula += PuertoSerieBascula.ReadExisting();
            this.Invoke(new EventHandler(ponteTextoBascula));
        }

        private void ponteTextoBascula(object sender, EventArgs e)
        {
            string[] words = informacionBascula.Trim().Split('\r');
            lblPeso.Text = words[words.Count() - 1].Trim();
        }
        #endregion

        #region Campos ComboBox y TextBox
        private void buscarBascula()
        {
            DGVListaBasculas.DataSource = PopulateDataGridViewWithParameter();
        }

        private void getTodasLasBasculas()
        {
            //string nombreColumna = "Nombre de Bascula";

            //DGVListaBasculas.Rows.Clear();

            //using (DataTable dtGetAllBasculas = cn.CargarDatos(cs.getTodasLasBasculas()))
            //{
            //    if (!dtGetAllBasculas.Rows.Count.Equals(0))
            //    {
            //        DGVListaBasculas.DataSource = dtGetAllBasculas;
            //    }
            //}

            //DGVListaBasculas.Columns[0].HeaderText = nombreColumna;
            DGVListaBasculas.DataSource = PopulateDataGridView();
        }

        private void getBasculasRegistradas()
        {
            //cbBasculaRegistrada.Items.Clear();
            //cbBasculaRegistrada.Items.Add(firtsItemBasculasRegistradas);

            //using (DataTable dtBasculas = cn.CargarDatos(cs.getBasculasRegistradas(FormPrincipal.userID)))
            //{
            //    if (!dtBasculas.Rows.Count.Equals(0))
            //    {
            //        foreach (DataRow drBascula in dtBasculas.Rows)
            //        {
            //            cbBasculaRegistrada.Items.Add(drBascula["nombreBascula"].ToString());
            //        }
            //    }
            //}

            //cbBasculaRegistrada.SelectedIndex = 0;
        }

        private void getComPortNames()
        {
            string[] portNames = SerialPort.GetPortNames();

            cbPuerto.Items.Clear();

            if (!portNames.Count().Equals(0))
            {
                foreach (var portName in portNames)
                {
                    cbPuerto.Items.Add(portName);
                }
                cbPuerto.Sorted = true;
                cbPuerto.SelectedIndex = 0;
            }
            else
            {
                cbPuerto.Items.Add("No se encontraron puertos Activos");
                cbPuerto.SelectedIndex = 0;
            }
        }

        private void getBaudRate()
        {
            string[] rangeBaudRate = new string[] { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "115200" };

            cbBaudRate.Items.Clear();

            if (!rangeBaudRate.Count().Equals(0))
            {
                foreach (var baudRate in rangeBaudRate)
                {
                    cbBaudRate.Items.Add(baudRate);
                }
                cbBaudRate.SelectedIndex = 0;
            }
            else
            {
                cbBaudRate.Items.Add("No se encontraron rangos de BaudRate");
                cbBaudRate.SelectedIndex = 0;
            }
        }

        private void getDataBits()
        {
            string[] rangeDataBit = new string[] { "8", "7" };

            cbDatos.Items.Clear();

            if (!rangeDataBit.Count().Equals(0))
            {
                foreach (var dataBit in rangeDataBit)
                {
                    cbDatos.Items.Add(dataBit + " bit");
                }
                cbDatos.Sorted = true;
                cbDatos.SelectedIndex = 0;
            }
            else
            {
                cbDatos.Items.Add("No se encontraron DataBit");
                cbDatos.SelectedIndex = 0;
            }
        }

        private void getParidadData()
        {
            string[] rangeParidadData = new string[] { "Even", "Mark", "None", "Odd", "Space" };

            cbParidad.Items.Clear();

            if (!rangeParidadData.Count().Equals(0))
            {
                foreach (var paridadData in rangeParidadData)
                {
                    cbParidad.Items.Add(paridadData);
                }
                cbParidad.SelectedIndex = 0;
            }
            else
            {
                cbParidad.Items.Add("Ningun dato de Paridad encontrado");
                cbParidad.SelectedIndex = 0;
            }
        }

        private void getHandshake()
        {
            string[] rangeHandshake = new string[] { "None", "RequestToSend", "RequestToSendXOnXOff", "XOnXOff" };

            cbHandshake.Items.Clear();

            if (!rangeHandshake.Count().Equals(0))
            {
                foreach (var handshakeData in rangeHandshake)
                {
                    cbHandshake.Items.Add(handshakeData);
                }
                cbHandshake.SelectedIndex = 0;
            }
            else
            {
                cbHandshake.Items.Add("Ningun Handshake encontrado");
                cbHandshake.SelectedIndex = 0;
            }
        }

        private void getStopBits()
        {
            string[] rangeStopBits = new string[] { "None", "One", "OnePointFive", "Two" };
            cbStopBits.Items.Clear();
            if (!rangeStopBits.Count().Equals(0))
            {
                foreach (var stopBits in rangeStopBits)
                {
                    cbStopBits.Items.Add(stopBits);
                }
                cbStopBits.SelectedIndex = 0;
            }
            else
            {
                cbStopBits.Items.Add("Ningun StopBits encontrado");
                cbStopBits.SelectedIndex = 0;
            }
        }
        #endregion

        #region Procesos de coneccion
        private void doConecction()
        {
            string puertoCom = string.Empty;
            int baudRate = 0;

            if (!cbPuerto.Text.ToString().Equals(string.Empty) ||
               !cbPuerto.Text.ToString().Equals("No se encontraron rangos de BaudRate"))
            {
                puertoCom = cbPuerto.Text;
            }
            else
            {
                MessageBox.Show("Selecciona un puerto especifico", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPuerto.Focus();
                return;
            }

            if (!cbBaudRate.Text.ToString().Equals(string.Empty) ||
               !cbBaudRate.Text.ToString().Equals("No se encontraron rangos de BaudRate"))
            {
                baudRate = Convert.ToInt32(cbBaudRate.Text.ToString());
            }
            else
            {
                MessageBox.Show("Selecciona un BaudRate especifico", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBaudRate.Focus();
                return;
            }

            InicializaPuertoBascula(puertoCom, baudRate);

            isOpen = true;
        }
        #endregion

        public AgregarEditarBascula()
        {
            InitializeComponent();
        }

        private DataTable PopulateDataGridView()
        {
            string sql_con = string.Empty;

            string query = cs.getTodasLasBasculas();

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                sql_con = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            using (MySqlConnection con = new MySqlConnection(sql_con))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con)) 
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        private DataTable PopulateDataGridViewWithParameter()
        {
            string sql_con = string.Empty;
            string usuario = string.Empty;

            usuario = FormPrincipal.userID.ToString();
            string query = $"SELECT nombreBascula FROM basculas WHERE nombreBascula LIKE '%{txtBuscarBascula.Text.Trim()}%' AND idUsuario = '" + usuario + "'";

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                sql_con = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            using (MySqlConnection con = new MySqlConnection(sql_con))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }


        private void datosBascula(string valorCelda)
        {
            using (DataTable dtDataBascula = cn.CargarDatos(cs.getDatosBasculaRegistrada(valorCelda)))
            {
                if (!dtDataBascula.Rows.Count.Equals(0))
                {
                    foreach(DataRow drDataBascula in dtDataBascula.Rows)
                    {
                        txtNameBascula.Text = drDataBascula["nombreBascula"].ToString();
                        cbPuerto.Text = drDataBascula["puerto"].ToString();
                        cbBaudRate.Text = drDataBascula["baudRate"].ToString();
                        cbDatos.Text = drDataBascula["dataBits"].ToString();
                        cbHandshake.Text = drDataBascula["handshake"].ToString();
                        cbParidad.Text = drDataBascula["parity"].ToString();
                        cbStopBits.Text = drDataBascula["stopBits"].ToString();
                        txtSendData.Text = drDataBascula["sendData"].ToString();
                    }
                    saveEdit = true;
                    validarBotones();
                }
            }
        }

        private void validarBotones()
        {
            if (!saveEdit)
            {
                btnAddBascula.Enabled = true;
                btnSaveEdit.Text = "Guardar";
            }
            else
            {
                btnAddBascula.Enabled = false;
                btnSaveEdit.Text = "Editar";
            }
        }

        private void btnBuscarBascula_Click(object sender, EventArgs e)
        {
            buscarBascula();
        }

        private void txtBuscarBascula_KeyUp(object sender, KeyEventArgs e)
        {
            buscarBascula();
        }

        private void DGVListaBasculas_Click(object sender, EventArgs e)
        {
            DataGridViewRow GridRow = DGVListaBasculas.CurrentRow;
            string valorCelda = Convert.ToString(GridRow.Cells[0].Value);
            datosBascula(valorCelda);
        }
        
        private void DGVListaBasculas_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow GridRow = DGVListaBasculas.CurrentRow;
            string valorCelda = Convert.ToString(GridRow.Cells[0].Value);
            datosBascula(valorCelda);
        }

        private void AgregarEditarBascula_Load(object sender, EventArgs e)
        {
            //llenamos los ComboBox
            //getBasculasRegistradas();   //Basculas Preconfiguradas
            getComPortNames();          //Puertos Activos
            getBaudRate();              //Rango BaudRate
            getDataBits();              //Rango DataBits
            getParidadData();           //Rango ParidadData
            getHandshake();             //Rango Handshake
            getStopBits();              //Rango StopBits
            getTodasLasBasculas();      //LLenar listado de basculas registradas

            validarBotones();
        }
    }
}
