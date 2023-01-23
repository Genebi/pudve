using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Agregados
using System.Data;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Specialized;
using MySql.Data.MySqlClient;

namespace PuntoDeVentaV2
{
    class ConexionAPPWEB
    {
        //Variables iniciales
        //rutaLocal es la variable que se debe usar cuando se haga el instalador
        //public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

        private MySqlConnection sql_con;
        private MySqlCommand sql_cmd;
        private MySqlDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        // pasamos variables entre la calse de conexion con la de consultas
        public static MySqlConnection cs_sql_con;
        public static MySqlCommand cs_sql_cmd;
        public static MySqlDataAdapter cs_DB;
        public static DataSet cs_DS = new DataSet();
        public static DataTable cs_DT = new DataTable();

        public ConexionAPPWEB()
        {
            cs_sql_con = sql_con;
            cs_sql_cmd = sql_cmd;
            cs_DB = DB;
            cs_DS = DS;
            cs_DT = DT;
        }


        public void Conectarse(bool ignorar = false)
        {
            sql_con = new MySqlConnection("datasource=74.208.135.60;port=3306;username=app;password=12Steroids12;database=pudve;");
        }

        //Sirve para los INSERT, UPDATE, DELETE
        public int EjecutarConsulta(string consulta, bool ignorar = false, bool regresarID = false)
        {
            int resultado = 0;
            Conectarse(ignorar);
            sql_con.Open();

            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            resultado = sql_cmd.ExecuteNonQuery();
            sql_con.Close();

            if (regresarID)
            {
                resultado = Convert.ToInt32(sql_cmd.LastInsertedId);
            }

            return resultado;
        }


        public DataTable CargarDatos(string consulta)
        {
            DataTable db = new DataTable();
            Conectarse();
            sql_con.Open();
            MySqlCommand com = new MySqlCommand(consulta, sql_con);
            MySqlDataAdapter adap = new MySqlDataAdapter(com);
            adap.Fill(db);
            sql_con.Close();
            return db;
        }



    }
}
