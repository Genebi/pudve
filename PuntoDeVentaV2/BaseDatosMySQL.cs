using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class BaseDatosMySQL
    {
        Conexion cn = new Conexion();

        List<string> dataBase = new List<string>();

        string connectionString = string.Empty;

        public BaseDatosMySQL()
        {

        }

        public void buildListDataBase()
        {
            dataBase.Add("CREATE DATABASE IF NOT EXISTS pudve CHARACTER SET utf8 COLLATE utf8_general_ci;");
        }

        public void buildDataBase()
        {
            buildListDataBase();

            connectionString = cn.getStringConnection();
            // Prepara la conexión
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                foreach (var item in dataBase)
                {
                    MySqlCommand command = new MySqlCommand(item, connection);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (MySqlException mysqlex)
            {
                System.Windows.Forms.MessageBox.Show("Excepción de MySQL al Crear la Base de datos: " + mysqlex.Message.ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Excepción general: " + ex.Message.ToString());
            }
        }
    }
}
