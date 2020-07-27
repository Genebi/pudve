using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class ConexionDataDictionary
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        // pasamos variables entre la calse de conexion con la de consultas
        public static SQLiteConnection cs_sql_con;
        public static SQLiteCommand cs_sql_cmd;
        public static SQLiteDataAdapter cs_DB;
        public static DataSet cs_DS = new DataSet();
        public static DataTable cs_DT = new DataTable();

        public ConexionDataDictionary()
        {
            cs_sql_con = sql_con;
            cs_sql_cmd = sql_cmd;
            cs_DB = DB;
            cs_DS = DS;
            cs_DT = DT;
        }

        public void Conectarse(bool ignorar = false)
        {
            if (ignorar == true)
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\DataDictionary.db; Version=3; New=False;Compress=True;", true);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                {
                    sql_con = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\DataDictionary.db; Version=3; New=False;Compress=True;", true);
                }
                else
                {
                    sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\DataDictionary.db; Version=3; New=False;Compress=True;", true);
                }
            }
        }

        //Sirve para los INSERT, UPDATE, DELETE
        public int EjecutarConsulta(string consulta, bool ignorar = false)
        {
            Conectarse(ignorar);
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
                    respuesta = dr["ID"]; //ID del usuario por ejemplo
                }
                if (tipo == 2)
                {
                    respuesta = dr["IDProveedor"]; //IDProveedor de la tabla DetallesProducto
                }
                if (tipo == 3)
                {
                    respuesta = dr["IDUsuario"]; // ID del usuario principal
                }
                if (tipo == 4)
                {
                    respuesta = dr["Password"]; // Password del usuario principal
                }
                if (tipo == 5)
                {
                    respuesta = dr["ID"]; // ID del empleado
                }
                if (tipo == 6)
                {
                    respuesta = dr["IDCliente"]; // IDCliente de tabla DetallesVenta
                }
                if (tipo == 7)
                {
                    respuesta = dr["ClaveProducto"] + "-" + dr["UnidadMedida"]; // Claves de unidad y producto de tabla Productos
                }
                if (tipo == 8)
                {
                    respuesta = dr["Timbrada"];
                }
                if (tipo == 10)
                {
                    respuesta = dr["uuid"];
                }
                if (tipo == 11)
                {
                    respuesta = dr["LogoTipo"];
                }
                if (tipo == 12)
                {
                    respuesta = dr["CodigoRegimen"];
                }
                if (tipo == 13)
                {
                    respuesta = dr["UUID"];
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

            dr.Close();

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
    }
}
