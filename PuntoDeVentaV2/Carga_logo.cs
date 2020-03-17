using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class Carga_logo
    {
        public string cargar_imagen()
        {
            Conexion cn = new Conexion();
            
            string logo = "";

            string nom_img = cn.EjecutarSelect($"SELECT LogoTipo FROM Usuarios WHERE ID='{FormPrincipal.userID}'", 11).ToString();


            if(nom_img == "" | nom_img == null)
            {
                logo = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\MisDatos\Usuarios\blanco.jpg";
            }
            else
            {
                logo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + nom_img;
            }
            

            return logo;
        }
    }
}
