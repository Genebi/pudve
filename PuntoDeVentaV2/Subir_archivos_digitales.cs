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
using System.Security;
using System.Security.Cryptography;
using System.IO.Compression;

namespace PuntoDeVentaV2
{

    public partial class Subir_archivos_digitales : Form
    {
        // Ruta donde se guardaran los archivos digitales
        string ruta_guardar_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\";
        // Variables para el certificado
        string num_certificado, x, fecha_caducidad, z;
        /* 
        string nom_cer = "", nom_key = "";
        string nom_cer_pem = "", nom_key_pem = "";*/
        

        OpenFileDialog ofd = new OpenFileDialog();
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        
    
        public string error = "";
        public string mensajeExito = "";
        public bool ban = false;

        public string RFCUsuario = string.Empty;
        


        public Subir_archivos_digitales()
        {
            InitializeComponent();            
        }

        private void cargar_datos()
        {
            if (!string.IsNullOrWhiteSpace(RFCUsuario))
            {
                txt_rfc.Text = RFCUsuario;
            }

            // Si la variable es false entonces el nombre de la carpeta cambiará.
            if (MisDatos.usuario_ini == false)
            {
                ruta_guardar_archivos = MisDatos.ruta_archivos_guadados;
            }


            if (Directory.Exists(ruta_guardar_archivos))
            {
                //string[] nombres = new string[2];
                //int i = 0;
                string[] id_usuario = new string[] { FormPrincipal.userID.ToString() };
                DataTable result;
                DataRow row;


                DirectoryInfo dir = new DirectoryInfo(ruta_guardar_archivos);

                foreach (var arch in dir.GetFiles())
                {
                    string extencion = arch.Name.Substring(arch.Name.Length - 4, 4);
                    string extencion_pem = arch.Name.Substring(arch.Name.Length - 8, 8);

                    if (extencion == ".cer")
                    {
                        txt_certificado.Text = arch.Name;
                    }
                    if (extencion == ".key")
                    {
                        txt_llave.Text = arch.Name;
                    }
                    if (extencion_pem == ".cer.pem")
                    {
                        txt_certificado_pem.Text = arch.Name;
                    }
                    if (extencion_pem == ".key.pem")
                    {
                        txt_llave_pem.Text = arch.Name;
                    }
                    /*nombres[i]= arch.Name;

                    i++;*/
                }


                if (txt_certificado.Text == "" & txt_llave.Text == "")
                {
                    btn_subir_archivos.Enabled = true;
                }
                else
                {
                    btn_subir_archivos.Enabled = false;
                }


                // Obtiene rfc, fecha de caducidad y contraseña
                result = cn.CargarDatos(cs.archivos_digitales(id_usuario, 2));

                if (result.Rows.Count != 0)
                {
                    row = result.Rows[0];

                    //txt_rfc.Text = row["RFC"].ToString();
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
                // Obtiene la ruta completa del archivo
                string ruta_origen = openfiled_archivos.FileName;
                string ruta_origen_sinzip = "";
                int opc = 0;


                // Obtiene la ruta donde esta almacenado el zip.
                string[] rt= ruta_origen.Split('\\');

                for(int i=0; i<rt.Length; i++)
                {
                    if(i < (rt.Length - 1))
                    {
                        ruta_origen_sinzip += rt[i] + "\\";
                    }
                }

                // Verifica si la carpeta ya fue creada o no, de no ser asi, la crea.                
                if (!Directory.Exists(ruta_guardar_archivos))
                {
                    Directory.CreateDirectory(ruta_guardar_archivos);
                }


                // Descomprime el zip y lo mueve solo si es el archivo correcto
                var ruta_origen_pem = ruta_origen_sinzip + @"Pudve_gpem";

                if (Directory.Exists(ruta_origen_pem + @"\"))
                {
                    Directory.Delete(ruta_origen_pem + @"\", true);
                }

                if (!Directory.Exists(ruta_origen_pem))
                {
                    //ZipFile.ExtractToDirectory(ruta_origen, ruta_guardar_archivos);
                    ZipFile.ExtractToDirectory(ruta_origen, ruta_origen_sinzip);
                    
                    if (Directory.Exists(ruta_origen_pem))
                    {
                        Directory.Move(ruta_origen_pem, ruta_guardar_archivos + @"Pudve_gpem\");
                    }
                    else
                    {
                        MessageBox.Show("Archivo comprimido no valido. No incluye archivos CSD.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Si ya existe una carpeta con el mismo nombre procede a eliminarla. 
                }

                // Saca los archivos que estan en la carpeta pudve_gpem y la anidada
                var ruta_carpeta_pem = ruta_guardar_archivos + @"Pudve_gpem\";
                
                DirectoryInfo dir = new DirectoryInfo(ruta_carpeta_pem);

                foreach (var arch in dir.GetDirectories())
                {
                    //string nom_carpeta_anidada = ;
                    string ruta_carpeta_anidada = ruta_carpeta_pem + arch.Name;

                    DirectoryInfo dir_ani = new DirectoryInfo(ruta_carpeta_anidada);

                    foreach (var arch_ani in dir_ani.GetFiles())
                    {
                        string nombre_archivo = arch_ani.Name;
                        string ruta_archivo_csd = ruta_carpeta_anidada + @"\" + nombre_archivo;

                        File.Move(ruta_archivo_csd, ruta_guardar_archivos + nombre_archivo);
                    }

                    arch.Delete();
                }

                // Eliminar carpeta Pudve_gpem
                DirectoryInfo dir_csd = new DirectoryInfo(ruta_guardar_archivos);

                foreach (var arch in dir_csd.GetDirectories())
                {
                    if (arch.Name.Equals("Pudve_gpem"))
                    {
                        arch.Delete();
                    }
                }

                // Obtiene key del txt y acomoda archivos 
                DirectoryInfo dirar = new DirectoryInfo(ruta_guardar_archivos);

                foreach (var arch in dirar.GetFiles())
                {
                    string extencion = arch.Name.Substring(arch.Name.Length - 4, 4);
                    string extencion_pem = arch.Name.Substring(arch.Name.Length - 8, 8);

                    if (extencion == ".cer")
                    {
                        txt_certificado.Text = arch.Name;

                        // Guarda número de certificado y fecha de vencimiento

                        tipo_validacion(1, ruta_guardar_archivos + arch.Name);
                    }
                    if (extencion == ".key")
                    {
                        txt_llave.Text = arch.Name;
                    }
                    if (extencion_pem == ".cer.pem")
                    {
                        txt_certificado_pem.Text = arch.Name;
                    }
                    if (extencion_pem == ".key.pem")
                    {
                        txt_llave_pem.Text = arch.Name;
                    }

                    if (extencion == ".txt")
                    {
                        string key = "";

                        try
                        {
                            StreamReader sr = new StreamReader(ruta_guardar_archivos + arch.Name);
                            key = sr.ReadLine();

                            sr.Close();
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine("Exception: " + ee.Message);
                        }


                        // Guarda contraseña de los archivos

                        string[] datos = new string[]
                        {
                                FormPrincipal.userID.ToString(), key
                        };

                        cn.EjecutarConsulta(cs.archivos_digitales(datos, 3));


                        txt_password.Text = key;
                    }
                }

                MessageBox.Show("Archivo subido corrrectamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txt_subir_archivos.Text = string.Empty;
            }


            // Si los datos no son correctos activa bandera para eliminar todos los archivos
            // y evitar que de errores al momento de querer timbrar o cancelar un CFDI.
            if (txt_certificado.Text == "" | txt_certificado_pem.Text == "" | txt_llave.Text == "" | txt_llave_pem.Text == "")
            {
                ban = true;
            }
            else
            {
                ban = false;
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
            /*var r = MessageBox.Show("Los cambios realizados se perderán si cancela, ¿desea continuar?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                if (Directory.Exists(ruta_guardar_archivos))
                {
                    borrar_datos_archivos();
                }

                this.Dispose();
            }*/
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
                mnsj = "Debe agregar la contraseña. La contraseña debe ser la perteneciente a sus archivos digitales. Elimine sus archivos y vuelva a subir su archivo comprimido.";
            }
            if (txt_llave_pem.Text == "")
            {
                mnsj = "No ha subido su llave digital con extención .pem(.key.pem).";
            }
            if (txt_llave.Text == "")
            {
                mnsj = "No ha subido su llave digital (.key).";
            }
            if (txt_certificado_pem.Text == "")
            {
                mnsj = "No ha subido su certificado con extención .pem (.cer.pem).";
            }
            if (txt_certificado.Text == "")
            {
                mnsj = "No ha subido su certificado (.cer).";
            }
            
            if (mnsj == "")
            {
                ban = false;
                ////btn_vnt_subir_archivos.PerformClick();
                this.Dispose();
                
            }
            else
            {
                ban = true;
                MessageBox.Show(mnsj, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ir_a_sifo(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://sifo.com.mx/pudve_genpem.php");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al abrir el enlace: " + ex, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardar_password(object sender, EventArgs e)
        {
            if(txt_password.Text != "")
            {
                /*if(nom_cer != "" & nom_key != "")
                {
                    string cer = ruta_guardar_archivos + nom_cer;
                    string key = ruta_guardar_archivos + nom_key;
                    string clave = txt_password.Text;

                    bool r = CFDI.SelloDigital.validarCERKEY(cer, key, clave);

                    Console.WriteLine("RESULTADO" + r);
                }*/
                

                // Genera archivos PEM
                /*bool r= genera_pem(txt_password.Text);

                if (r)
                    Console.WriteLine("Archivo creado con éxito");
                else
                    Console.WriteLine(r);*/



                // Guarda contraseña de los archivos

                string[] datos = new string[]
                {
                    FormPrincipal.userID.ToString(), txt_password.Text
                };

                cn.EjecutarConsulta(cs.archivos_digitales(datos, 3));
            }
        }

        private void validar_correspondecia(object sender, EventArgs e)
        {
           
        }

        private void cerrando(object sender, FormClosingEventArgs e)
        {
            MisDatos md = new MisDatos();
            md.cargar_archivos();
        }

        private void Subir_archivos_digitales_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MisDatos.ruta_archivos_guadados))
            {
                ruta_guardar_archivos = MisDatos.ruta_archivos_guadados;
            }
            cargar_datos();
            label19.Font = new Font(label19.Font, FontStyle.Underline);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

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
            txt_certificado_pem.Text = string.Empty;
            lb_fecha_caducidad.Text = string.Empty;
            txt_llave.Text = string.Empty;
            txt_llave_pem.Text = string.Empty;
            txt_password.Text = string.Empty;
            txt_password.ReadOnly = true;
            btn_subir_archivos.Enabled = true;

            if (txt_rfc.ReadOnly == false)
            {
                txt_rfc.Text = string.Empty;
                txt_rfc.ReadOnly = true;
            }
        }

        /*private void btn_subir_archivos_Click(object sender, EventArgs e)
        {
            if (openfiled_archivos.ShowDialog() == DialogResult.OK)
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
                    string extencion_pem = solo_nombre.Substring(solo_nombre.Length - 8, 8);

                    if (extencion == ".cer")
                    {
                        opc = 1;
                        nom_cer = solo_nombre;
                        openfiled_archivos.Filter = "Archivo KEY(*.key) | *.key";
                    }
                    if (extencion == ".key")
                    {
                        opc = 2;
                        nom_key = solo_nombre;
                        //btn_subir_archivos.Enabled = false;
                        openfiled_archivos.Filter = "Archivo CER.PEM(*.cer.pem) | *.cer.pem";
                    }
                    if (extencion_pem == ".cer.pem")
                    {
                        nom_cer_pem = solo_nombre;
                        openfiled_archivos.Filter = "Archivo KEY.PEM(*.key.pem) | *.key.pem";
                    }
                    if (extencion_pem == ".key.pem")
                    {
                        nom_key_pem = solo_nombre;
                        btn_subir_archivos.Enabled = false;
                    }


                    // Guarda número de certificado y fecha de vencimiento
                    if (opc > 0)
                    {
                        tipo_validacion(opc, ruta_destino);
                    }


                    txt_certificado.Text = nom_cer;
                    txt_llave.Text = nom_key;
                    txt_certificado_pem.Text = nom_cer_pem;
                    txt_llave_pem.Text = nom_key_pem;


                    MessageBox.Show("Archivo subido corrrectamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txt_subir_archivos.Text = string.Empty;

                }
                else
                {
                    MessageBox.Show("El archivo" + solo_nombre + " ya existe.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            // Si los datos no son correctos activa bandera para eliminar todos los archivos
            // y evitar que de errores al momento de querer timbrar o cancelar un CFDI.
            if (txt_certificado.Text == "" | txt_certificado_pem.Text == "" | txt_llave.Text == "" | txt_llave_pem.Text == "")
            {
                ban = true;
            }
            else
            {
                ban = false;
            }
        }
        */
        /*public bool genera_pem(string clave)
        {
           
            string cer = ruta_guardar_archivos + nom_cer;
            string key = ruta_guardar_archivos + nom_key;
            string clavePrivada = clave;

            string ArchivoKPEM = ruta_guardar_archivos + nom_key + ".pem";
            string ArchivoCPEM = ruta_guardar_archivos + nom_cer + ".pem";
            string ArchivoPFX = ruta_guardar_archivos;



            bool exito = false;

            //validaciones
            if (!File.Exists(cer))
            {
                error = "No existe el archivo cer en el sistema";
                return false;
            }
            if (!File.Exists(key))
            {
                error = "No existe el archivo key en el sistema";
                return false;
            }
            if (clavePrivada.Trim().Equals(""))
            {
                error = "No existe una clave privada aun en el sistema";
                return false;
            }

            //creamos objetos Process
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            System.Diagnostics.Process proc2 = new System.Diagnostics.Process();
            System.Diagnostics.Process proc3 = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc2.EnableRaisingEvents = false;
            proc3.EnableRaisingEvents = false;

            //openssl x509 -inform DER -in certificado.cer -out certificado.pem
            proc.StartInfo.FileName = "openssl";
            proc.StartInfo.Arguments = "x509 -inform DER -in \"" + cer + "\" -out \"" + ArchivoCPEM + "\"";
            proc.StartInfo.WorkingDirectory = @"C:\openssl-win32\bin\";
            proc.Start();
            proc.WaitForExit();

            //openssl pkcs8 -inform DER -in llave.key -passin pass:a0123456789 -out llave.pem
            proc2.StartInfo.FileName = "openssl";
            proc2.StartInfo.Arguments = "pkcs8 -inform DER -in \"" + key + "\" -passin pass:" + clavePrivada + " -out \"" + ArchivoKPEM + "\"";
            proc2.StartInfo.WorkingDirectory = @"C:\openssl-win32\bin\";
            proc2.Start();
            proc2.WaitForExit();

            //openssl pkcs12 -export -out archivopfx.pfx -inkey llave.pem -in certificado.pem -passout pass:clavedesalida
            proc3.StartInfo.FileName = "openssl";
            proc3.StartInfo.Arguments = "pkcs12 -export -out \"" + ArchivoPFX + "\" -inkey \"" + ArchivoKPEM + "\" -in \"" + ArchivoCPEM + "\" -passout pass:" + clavePrivada;
            proc3.StartInfo.WorkingDirectory = @"C:\openssl-win32\bin\";
            proc3.Start();
            proc3.WaitForExit();

            proc.Dispose();
            proc2.Dispose();
            proc3.Dispose();

            //enviamos mensaje exitoso
            if (System.IO.File.Exists(ArchivoPFX))
                mensajeExito = "Se ha creado el archivo PFX ";
            else
            {
                error = "Error al crear el archivo PFX, puede ser que el cer o el key no sean archivos con formato correcto";
                return false;
            }

            //eliminamos los archivos pem
            //if (System.IO.File.Exists(ArchivoCPEM)) System.IO.File.Delete(ArchivoCPEM);
            //if (System.IO.File.Exists(ArchivoKPEM)) System.IO.File.Delete(ArchivoKPEM);

            exito = true;

            return exito;
        }*/
    }
}
