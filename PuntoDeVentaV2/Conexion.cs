using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregados
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
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
        //Tipo 0 es por default solo para verificar si existe algun registro en especifico
        //Tipo 1 es para obtener un valor en especifico (Login) por ejemplo el ID de usuario
        public object EjecutarSelect(string consulta, int tipo = 0)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();


            object respuesta = null;

            int contador = 0;

            while (dr.Read())
            {
                contador++;

                if (tipo == 1)
                {
                    respuesta = dr["ID"]; //ID del usuario
                }
            }

            if (tipo == 0)
            {
                if (contador > 0)
                {
                    respuesta = true;
                }
                else
                {
                    respuesta = false;
                }
            }

            return respuesta;
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

        public void CargarInformacion(string consulta, DataGridView dgv)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            DB = new SQLiteDataAdapter(consulta, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dgv.DataSource = DT;
            sql_con.Close();
        }
    }
}
