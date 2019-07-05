using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class MetodosBusquedas
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;

        public void Conexion()
        {
            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
        }

        public string[] ObtenerDetallesVenta(int idVenta, int idUsuario)
        {
            List<string> lista = new List<string>();

            Conexion();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM DetallesVenta WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}";
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                //ID del cliente, Cliente
                lista.Add(dr[10].ToString());
                lista.Add(dr[11].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerDatosCliente(int idCliente, int idUsuario)
        {
            List<string> lista = new List<string>();

            Conexion();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Clientes WHERE ID = {idCliente} AND IDUsuario = {idUsuario}";
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                //Razon social, RFC
                lista.Add(dr[2].ToString());
                lista.Add(dr[4].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }
    }
}
