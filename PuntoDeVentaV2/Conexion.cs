using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregados
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace PuntoDeVentaV2
{
    class Conexion
    {
        //Variables iniciales
        public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        //Se necesita para saber si la computadora tiene conexion a internet
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


        public void Conectarse()
        {
            sql_con = new SQLiteConnection("Data source=|DataDirectory|\\pudveDB.db; Version=3; New=False;Compress=True;");
        }

        //Sirve para los INSERT, UPDATE, DELETE
        public int EjecutarConsulta(string consulta)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            int resultado = sql_cmd.ExecuteNonQuery();
            sql_con.Close();

            return resultado;
        }

        //Sirve para los SELECT solamente
        public bool EjecutarSelect(string consulta)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            int contador = 0;

            while (dr.Read())
            {
                contador++;
            }

            if (contador > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable CargarDatos(string consulta)
        {
            DataTable db = new DataTable();
            Conectarse();
            sql_con.Open();
            SQLiteCommand com = new SQLiteCommand(consulta, sql_con);
            SQLiteDataAdapter adap = new SQLiteDataAdapter(com);
            adap.Fill(db);
            sql_con.Close();

            return db;
        }
    }
}
