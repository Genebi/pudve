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

        string connectionString = string.Empty;

        public BaseDatosMySQL()
        {

        }

        public async Task buildDataBase()
        {
            connectionString = cn.getStringConnection();
            // Prepara la conexión
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                MySqlCommand command = new MySqlCommand("CREATE DATABASE IF NOT EXISTS pudve CHARACTER SET utf8 COLLATE utf8_general_ci;", connection);
                await command.ExecuteNonQueryAsync();
                
                connection.Close();
            }
            catch (MySqlException mysqlex)
            {
                //System.Windows.Forms.MessageBox.Show("Excepción de MySQL al crear la base de datos: " + mysqlex.Message.ToString());
                System.Windows.Forms.MessageBox.Show("Ha ocurrido un error en la creación de la base de datos\nrelacionado a problemas de Internet, póngase en contacto\ncon servicio técnico en https://sifo.com.mx/ - Código [1001].", "Mensaje del sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Excepción general: " + ex.Message.ToString());
                System.Windows.Forms.MessageBox.Show("Ha ocurrido un error en la creación de la base de datos\nrelacionado a problemas de Internet, póngase en contacto\ncon servicio técnico en https://sifo.com.mx/ - Código [1002].", "Mensaje del sistema", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
