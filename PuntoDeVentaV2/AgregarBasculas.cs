﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarBasculas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        bool isOpen = false;

        #region DISPOSITIVO-LECTOR BASCULA
        public SerialPort PuertoSerieBascula;
        public static string informacionBascula;

        public void InicializaPuertoBascula(string puerto, int baud)
        {
            if(puerto != "" && puerto != string.Empty)
            {
                PuertoSerieBascula = new SerialPort(puerto, baud);

                if (!PuertoSerieBascula.IsOpen)
                {
                    if(!cbParidad.Text.Equals(string.Empty) || !cbParidad.Equals("Ningun dato de Paridad encontrado"))
                    {
                        PuertoSerieBascula.Parity = (Parity)Enum.Parse(typeof(Parity), cbParidad.Text.ToString());
                    }
                    else
                    {
                        PuertoSerieBascula.Parity = Parity.None;
                    }

                    if(!cbStopBits.Text.Equals(string.Empty) || !cbStopBits.Equals("Ningun StopBits encontrado"))
                    {
                        PuertoSerieBascula.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbStopBits.Text.ToString());
                    }
                    else
                    {
                        PuertoSerieBascula.StopBits = StopBits.One;
                    }

                    if(!cbDatos.Text.Equals("No se encontraron DataBit") || !cbDatos.Text.Equals(string.Empty))
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
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Error al abrir el dispositivo (Bascula) ...\r" + error.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("El puerto está abierto...");
                }
            }
        }

        private void PuertoSerieBascula_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialError.Frame:
                    MessageBox.Show("Error de Trama...");
                    break;
                case SerialError.Overrun:
                    MessageBox.Show("Saturación de buffer...");
                    break;
                case SerialError.RXOver:
                    MessageBox.Show("Desboradamiento de buffer de entrada");
                    break;
                case SerialError.RXParity:
                    MessageBox.Show("Error de paridad...");
                    break;
                case SerialError.TXFull:
                    MessageBox.Show("Buffer lleno...");
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
        private void getBasculasRegistradas()
        {
            cbBasculaRegistrada.Items.Clear();
            cbBasculaRegistrada.Items.Add("Selecciona una...");

            using (DataTable dtBasculas = cn.CargarDatos(cs.getBasculasRegistradas(FormPrincipal.userID)))
            {
                if (!dtBasculas.Rows.Count.Equals(0))
                {
                    foreach(DataRow drBascula in dtBasculas.Rows)
                    {
                        cbBasculaRegistrada.Items.Add(drBascula["nombreBascula"].ToString());
                    }
                }
            }

            cbBasculaRegistrada.SelectedIndex = 0;
        }

        private void getComPortNames()
        {
            string[] portNames = SerialPort.GetPortNames();

            cbPuerto.Items.Clear();

            if (!portNames.Count().Equals(0))
            {
                foreach(var portName in portNames)
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
                foreach(var stopBits in rangeStopBits)
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
                MessageBox.Show("Selecciona un puerto especifico");
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
                MessageBox.Show("Selecciona un BaudRate especifico");
                cbBaudRate.Focus();
                return;
            }

            InicializaPuertoBascula(puertoCom, baudRate);

            isOpen = true;
        }
        #endregion

        public AgregarBasculas()
        {
            InitializeComponent();
        }

        private void AgregarBasculas_Load(object sender, EventArgs e)
        {
            //llenamos los ComboBox
            getBasculasRegistradas();   //Basculas Preconfiguradas
            getComPortNames();          //Puertos Activos
            getBaudRate();              //Rango BaudRate
            getDataBits();              //Rango DataBits
            getParidadData();           //Rango ParidadData
            getHandshake();             //Rango Handshake
            getStopBits();              //Rango StopBits
        }

        private void btnTomarPeso_Click(object sender, EventArgs e)
        {
            lblPeso.Text = string.Empty;

            try
            {
                if (isOpen.Equals(false))
                {
                    doConecction();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message.ToString());
            }

            try
            {
                if (!txtSendData.Text.Equals(string.Empty))
                {
                    PuertoSerieBascula.Write(txtSendData.Text);
                }
                else
                {
                    MessageBox.Show("Favor de ingresar un valor a enviar al puerto");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message.ToString());
            }
        }
    }
}
