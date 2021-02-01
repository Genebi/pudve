using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public class checarVersion
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public void printProductVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                    string servidor = string.Empty;
                    string versionLocal = string.Empty;
                    string versionServidor = string.Empty;
                    string[] words;

                    versionLocal = ad.CurrentVersion.ToString();

                    

                    words = versionLocal.Split('.');

                    servidor = Properties.Settings.Default.Hosting;

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        using (DataTable dtRetriveVersion = cn.CargarDatos(cs.getRetriveVersion()))
                        {
                            if (!dtRetriveVersion.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drVersion in dtRetriveVersion.Rows)
                                {
                                    versionServidor = drVersion["AppVersion"].ToString();
                                }

                                if (!versionLocal.Equals(versionServidor))
                                {
                                    MessageBox.Show("La computadora a la cual intentas conectarte no tiene la misma version del sistema\n\nSe recomienda que verifique su conexión a internet cierre su sistema totalmente,\ny ejecutelo de nuevo para su recibir todas las actualizaciones de su sistema.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(servidor))
                    {
                        using (DataTable dtTablaEncontrada = cn.CargarDatos(cs.isExistsTable("AppVersionRecord")))
                        {
                            if (!dtTablaEncontrada.Rows.Count.Equals(0))
                            {
                                string resultadoObtenio = string.Empty;

                                foreach (DataRow drResultado in dtTablaEncontrada.Rows)
                                {
                                    resultadoObtenio = drResultado["Resultado"].ToString();
                                }

                                if (resultadoObtenio.Equals("1"))
                                {
                                    using (DataTable dtRetriveVersion = cn.CargarDatos(cs.getRetriveVersion()))
                                    {
                                        if (!dtRetriveVersion.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow drVersion in dtRetriveVersion.Rows)
                                            {
                                                versionServidor = drVersion["AppVersion"].ToString();
                                            }

                                            if (!versionLocal.Equals(versionServidor))
                                            {
                                                cn.EjecutarConsulta(cs.insertAppVersion(Application.ProductName.ToString(), versionLocal, words[0].ToString(), words[1].ToString(), words[2].ToString(), words[3].ToString(), DateTime.Now.ToString("yyyyy-MM-dd")));
                                            }
                                        }
                                        if (dtRetriveVersion.Rows.Count.Equals(0))
                                        {
                                            cn.EjecutarConsulta(cs.insertAppVersion(Application.ProductName.ToString(), versionLocal, words[0].ToString(), words[1].ToString(), words[2].ToString(), words[3].ToString(), DateTime.Now.ToString("yyyy-MM-dd")));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Aviso de la operacion\nde optención de la versión del sistema\n\nReferencia: " + ex.Message.ToString(), "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
