using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class clientesBuscarPorHuella : Form, DPFP.Capture.EventHandler
    {

        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;
        private DPFP.Capture.Capture Capturer;
        //private string IDCliente;
        //public string idCliente = string.Empty;
        public string cliente = string.Empty;
        //public string idregistro = string.Empty;
        delegate void Function();
        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();				// Create a capture operation.

                if (null != Capturer)
                    Capturer.EventHandler = this;                   // Subscribe for capturing events.
                                                                    //else
                                                                    //SetPrompt("No se pudo iniciar la operación de captura");
            }
            catch
            {
                MessageBox.Show("No se pudo iniciar la operación de captura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            Verificator = new DPFP.Verification.Verification();     // Create a fingerprint template verificator
            UpdateStatus(0);
        }

        private void UpdateStatus(int FAR)
        {
            // Show "False accept rate" value
            //SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        }


        //protected void MakeReport(string message)
        //{
        //    this.Invoke(new Function(delegate () {
        //        StatusText.AppendText(message + "\r\n");
        //    }));
        //}


        public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        {
            //MakeReport("La muestra ha sido capturada");
            //SetPrompt("Escanea tu misma huella otra vez");
            Process(Sample);
        }

        public void OnFingerGone(object Capture, string ReaderSerialNumber)
        {
            //MakeReport("La huella fue removida del lector");
        }

        public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
            //MakeReport("El lector fue tocado");
        }

        public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
            //MakeReport("El Lector de huellas ha sido conectado");
        }

        public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
            //MakeReport("El Lector de huellas ha sido desconectado");
        }

        public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        {
            //if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
            //    MakeReport("La calidad de la muestra es BUENA");
            //else
            //    MakeReport("La calidad de la muestra es MALA");
        }

        protected virtual void Process(DPFP.Sample Sample)
        {
            DrawPicture(ConvertSampleToBitmap(Sample));
            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {
                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;

                Conexion cn = new Conexion();
                Consultas cs = new Consultas();

                List<byte[]> byteArray = new List<byte[]>();
                byteArray = cn.buscarMuestrasClientesHuella();
                int count = 0;
                foreach (var huella in byteArray)
                {

                    stream = new MemoryStream(huella);
                    template = new DPFP.Template(stream);

                    Verificator.Verify(features, template, ref result);

                    UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                        //MakeReport();

                        List<string> usuarios = new List<string>();
                        List<string> nombres = new List<string>();

                        usuarios = cn.buscarClientesHuella();       
                        nombres = cn.buscarNombresHuella();

                        string  nombre= nombres[byteArray.IndexOf(huella)];
                        cliente = cs.BuscarClienteHuella(usuarios[byteArray.IndexOf(huella)]);


                        DialogResult dialogResult = MessageBox.Show($"Se detectó la muestra del cliente: {cliente}, con el nombre: {nombre}\nContinuar con dicho resultado?", "Coincidencia biométrica ", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            break;
                        }
                        else
                        {
                            cliente = string.Empty;
                        }

                    }
                }
                this.Invoke((MethodInvoker)delegate
                {
                    // close the form on the forms thread
                    this.Close();
                });
            }
        }
        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate () {
                Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
            }));
        }

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
            Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
            return bitmap;
        }


        public clientesBuscarPorHuella()
        {
            InitializeComponent();
            //IDCliente = id_cliente;
        }

        private void clientesVerificarHuella_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }

        private void clientesVerificarHuellaHuella_Load(object sender, EventArgs e)
        {
            Init();
            Start();
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch
                {
                    //SetPrompt("No se puede terminar la captura");
                }
            }

            //this.Close();
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                    //SetPrompt("Escanea tu huella usando el lector");
                }
                catch
                {
                    //SetPrompt("No se puede iniciar la captura");
                }
            }
        }
    }
}
