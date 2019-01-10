using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregados
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace PuntoDeVentaV2
{
    class Conexion
    {

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Descripcion, int ValorReservado);

        public static bool ConectadoInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        public Conexion()
        {
            
        }

        public void SincronizarProductos()
        {
            if (ConectadoInternet())
            {
                //Para el posible webservice
            }
        }
    }
}
