using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace PuntoDeVentaV2
{
    public partial class Subir_archivos_digitales : Form
    {
        // Ruta donde se guardaran los archivos digitales
        string ruta_guardar_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\";
        // Variables para el certificado
        string num_certificado, x, fecha_caducidad, z;
        // 
        string nom_cer = "", nom_key = "";

        OpenFileDialog ofd = new OpenFileDialog();

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        

        public Subir_archivos_digitales()
        {
            InitializeComponent();
        }

        private void cargar_datos(object sender, EventArgs e)
        {
            if (Directory.Exists(ruta_guardar_archivos))
            {
                string[] nombres = new string[2];
                int i = 0;
                string[] id_usuario = new string[] { FormPrincipal.userID.ToString() };
                DataTable result;
                DataRow row;


                DirectoryInfo dir = new DirectoryInfo(ruta_guardar_archivos);

                foreach (var arch in dir.GetFiles())
                {
                    nombres[i]= arch.Name;

                    i++;
                }

                txt_certificado.Text = nombres[0];
                txt_llave.Text = nombres[1];
                Console.WriteLine(nombres[0] + "<====>" + nombres[1]);
                if(nombres[0] == "" & nombres[1] == "")
                {
                    btn_subir_archivos.Enabled = false;
                }
                else
                {
                    btn_subir_archivos.Enabled = true;
                }


                // Obtiene rfc, fecha de caducidad y contraseña

                result = cn.CargarDatos(cs.archivos_digitales(id_usuario, 2));

                if(result.Rows.Count != 0)
                {
                    row = result.Rows[0];

                    txt_rfc.Text = row["RFC"].ToString();
                    txt_password.Text = row["password_cer"].ToString();

                    string fecha = row["fecha_caducidad_cer"].ToString();

                    if (fecha != "")
                    {
                        string fech = fecha.Substring(0, 10);

                        lb_fecha_caducidad.Text = fech;
                    }
                }
                
            }
        }

        private void btn_subir_archivos_Click(object sender, EventArgs e)
        {
            if(openfiled_archivos.ShowDialog() == DialogResult.OK)
            {
                
                // Obtiene solo el nombre del archivo, sin la ruta completa
                string solo_nombre = openfiled_archivos.SafeFileName;
                // Obtiene la ruta completa del archivo
                string ruta_origen = openfiled_archivos.FileName;
                // Se establece la ruta y nombre del archivo a guardar el la carpeta
                string ruta_destino = ruta_guardar_archivos + solo_nombre;
                
                int opc = 0;
                

                // Verifica si la carpeta ya fue creada o no, de no ser asi, la crea.

                if (!Directory.Exists(ruta_guardar_archivos))
                {
                    Directory.CreateDirectory(ruta_guardar_archivos);
                }
                
                // Verifica si existe el archivo, si no existe lo agrega
                
                if (!File.Exists(ruta_destino))
                {                    
                    File.Copy(ruta_origen, ruta_destino);
                    txt_subir_archivos.Text = openfiled_archivos.FileName;

                    // Obtiene extención del archivo elegido para determinar el tipo de acción a realizar

                    string extencion = solo_nombre.Substring(solo_nombre.Length - 4, 4);

                    if(extencion == ".cer")
                    {
                        opc = 1;
                        nom_cer = solo_nombre;
                        openfiled_archivos.Filter = "Archivo KEY(*.key) | *.key";
                    }
                    if (extencion == ".key")
                    {
                        opc = 2;
                        nom_key = solo_nombre;
                        btn_subir_archivos.Enabled = false;
                    }

                    tipo_validacion(opc, ruta_destino);


                    txt_certificado.Text = nom_cer;
                    txt_llave.Text = nom_key;
                    

                    MessageBox.Show("Archivo subido corrrectamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txt_subir_archivos.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("El archivo" + solo_nombre + " ya existe.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_borrar_archivos_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("¿Esta seguro de eliminar sus archivos digitales?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(r == DialogResult.Yes)
            {
                if (Directory.Exists(ruta_guardar_archivos))
                {
                    borrar_datos_archivos();

                    MessageBox.Show("Archivos eliminados", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No hay archivos por eliminar.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }          
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            var r = MessageBox.Show("Los cambios realizados se perderán si cancela, ¿desea continuar?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                if (Directory.Exists(ruta_guardar_archivos))
                {
                    borrar_datos_archivos();
                }

                this.Dispose();
            }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string mnsj = "";

            if(txt_rfc.Text == "")
            {
                mnsj = "Debe agregar su RFC.";
            }
            if(txt_password.Text == "")
            {
                mnsj = "Debe agregar la contraseña. La contraseña debe ser la perteneciente a sus archivos digitales.";
            }
            if(txt_llave.Text == "")
            {
                mnsj = "No ha subido su llave digital (.key).";
            }
            if(txt_certificado.Text == "")
            {
                mnsj = "No ha subido su certificado (.cer).";
            }


            if(mnsj == "")
            {
                this.Dispose();
            }
            else
            {
                MessageBox.Show(mnsj, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardar_password(object sender, EventArgs e)
        {
            if(txt_password.Text != "")
            {
                string[] datos = new string[]
                {
                    FormPrincipal.userID.ToString(), txt_password.Text
                };

                cn.EjecutarConsulta(cs.archivos_digitales(datos, 3));
            }
        }

        private void validar_correspondecia(object sender, EventArgs e)
        {
            /*if(txt_password.Text != "")
            {
                string cer = ruta_guardar_archivos + txt_certificado.Text;
                string key = ruta_guardar_archivos + txt_llave.Text;
                string clave = txt_password.Text;

                bool r = CFDI.SelloDigital.validarCERKEY(cer, key, clave);

                Console.WriteLine("RESULTADO" + r); //'C:\Users\Miri\Documents\Visual Studio 2015\Projects\pudve\PuntoDeVentaV2\bin\Debug\CSD_NESTOR_DAVID_NUEZ_SOTO_NUSN900420SS5_20190316_134109s.cer
            }*/
        }

        private void tipo_validacion(int opc, string ruta_destino)
        {
            if(opc == 1)
            {
                // Obtiene el número de certificado
                CFDI.SelloDigital.leerCER(ruta_destino, out x, out fecha_caducidad, out z, out num_certificado);

                string fech_caducidad = fecha_caducidad.Substring(0, 10);

                lb_fecha_caducidad.Text = fech_caducidad;

                // Guardar la fecha y número del certificado

                string[] datos = new string[]
                {
                    FormPrincipal.userID.ToString(), num_certificado, fecha_caducidad, ""
                };

                cn.EjecutarConsulta(cs.archivos_digitales(datos, 1));
            }
            else
            {
                txt_password.Cursor = Cursors.IBeam;
                txt_password.ReadOnly = false;

                if(txt_rfc.Text == "")
                {
                    txt_rfc.ReadOnly = false;
                    txt_rfc.Cursor = Cursors.IBeam;
                }
            }
        }

        private void validar_formato_rfc(object sender, EventArgs e)
        {
            // Valida longitud y formato del RFC
            if (txt_rfc.TextLength < 12)
            {
                MessageBox.Show("La longitud del RFC es incorrecta.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                string formato_rfc = "^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";

                Regex exp = new Regex(formato_rfc);

                if (exp.IsMatch(txt_rfc.Text))
                {
                }
                else
                {
                    MessageBox.Show("El formato del RFC no es valido.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }
        }

        private void borrar_datos_archivos()
        {
            DirectoryInfo dir_info = new DirectoryInfo(ruta_guardar_archivos);

            foreach (FileInfo f in dir_info.GetFiles())
            {
                f.Delete();
            }

            string[] datos = new string[]
            {
                FormPrincipal.userID.ToString(), "", "", ""
            };

            cn.EjecutarConsulta(cs.archivos_digitales(datos, 1));


            // Limpiar campos

            txt_certificado.Text = string.Empty;
            lb_fecha_caducidad.Text = string.Empty;
            txt_llave.Text = string.Empty;
            txt_password.Text = string.Empty;
            txt_password.ReadOnly = true;
            btn_subir_archivos.Enabled = true;

            if (txt_rfc.ReadOnly == false)
            {
                txt_rfc.Text = string.Empty;
                txt_rfc.ReadOnly = true;
            }
        }
    }
}
