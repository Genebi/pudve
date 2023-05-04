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
using System.Drawing;

namespace PuntoDeVentaV2
{
    class Conexion
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

        //Se necesita para saber si la computadora tiene conexion a internet
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Descripcion, int ValorReservado);

        public static bool ConectadoInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        public Conexion()
        {
            cs_sql_con = sql_con;
            cs_sql_cmd = sql_cmd;
            cs_DB = DB;
            cs_DS = DS;
            cs_DT = DT;
        }

        public void SincronizarProductos()
        {
            if (ConectadoInternet())
            {
                //Para el posible webservice
            }
        }


        public void Conectarse(bool ignorar = false)
        {
            if (ignorar == true)
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                {
                    sql_con = new MySqlConnection("datasource="+ Properties.Settings.Default.Hosting +";port=6666;username=root;password=;database=pudve;");
                }
                else
                {
                    sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
                }
            }
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

            MySqlDataReader dr = sql_cmd.ExecuteReader();

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
                if(tipo == 3)
                {
                    respuesta = dr["IDUsuario"]; // ID del usuario principal
                }
                if(tipo == 4)
                {
                    respuesta = dr["Password"]; // Password del usuario principal
                }
                if(tipo == 5)
                {
                    respuesta = dr["ID"]; // ID del empleado
                }
                if(tipo == 6)
                {
                    respuesta = dr["IDCliente"]; // IDCliente de tabla DetallesVenta
                }
                if (tipo == 7)
                {
                    respuesta = dr["ClaveProducto"] + "-" + dr["UnidadMedida"]; // Claves de unidad y producto de tabla Productos
                }
                if(tipo == 8)
                {
                    respuesta = dr["Timbrada"];
                }
                if (tipo == 9)
                {
                    respuesta = dr["usuario"]; // Nombre de usuario de un empleado
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
                if (tipo == 14)
                {
                    respuesta = dr["Total"];
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
            sql_con.Close();

            return respuesta;
        }

        public Image readImage(string query)
        {
            Conectarse();
            Image image = null;
            if (sql_con.State == ConnectionState.Closed)
                sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = query;
            MySqlDataReader dr = sql_cmd.ExecuteReader();
            if (dr.Read())
            {
                if (!DBNull.Value.Equals(dr["ImgNew"]))
                {
                    byte[] image_bytes = (byte[])dr["ImgNew"];
                    MemoryStream ms = new MemoryStream(image_bytes);
                    image = Image.FromStream(ms);
                }
            }
            dr.Close();
            if (sql_con.State == ConnectionState.Open)
                sql_con.Close();
            return image;
        }
        //public DataTable CargarDatos(string consulta)
        //{
        //    DataTable db = new DataTable();
        //    Conectarse();
        //    sql_con.Open();
        //    MySqlCommand com = new MySqlCommand(consulta, sql_con);
        //    MySqlDataAdapter adap = new MySqlDataAdapter(com);
        //    adap.Fill(db);
        //    sql_con.Close();
        //    return db;
        //}

        public DataTable CargarDatos(string consulta)
        {
            DataTable db = new DataTable();
            Conectarse();
            //sql_con.Close(); // close the connection before executing the query
            sql_con.Open();
            MySqlCommand com = new MySqlCommand(consulta, sql_con);
            MySqlDataAdapter adap = new MySqlDataAdapter(com);
            adap.Fill(db);
            sql_con.Close();
            return db;
        }

        public DataTable ConsultaRegimenFiscal()
        {
            string consulta = "SELECT CodigoRegimen, Descripcion FROM RegimenFiscal";
            DataTable dbcb = new DataTable();
            Conectarse();
            sql_con.Open();
            MySqlCommand com = new MySqlCommand(consulta, sql_con);
            MySqlDataAdapter adap = new MySqlDataAdapter(com);
            adap.Fill(dbcb);
            sql_con.Close();
            return dbcb;
        }
        public DataTable cargarCBRegimen(string consulta)
        {
            DataTable dbcbreg = new DataTable();
            Conectarse();
            sql_con.Open();
            MySqlCommand com = new MySqlCommand(consulta, sql_con);
            MySqlDataAdapter adap = new MySqlDataAdapter(com);
            adap.Fill(dbcbreg);
            sql_con.Close();
            return dbcbreg;
        }

        public DataTable GetEmpresas(string consulta)
        {
            Conectarse();
            sql_con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(consulta, sql_con);
            sda.Fill(DT);
            sql_con.Close();
            return DT;
        }

        public DataTable GetStockProd(string consulta)
        {
            Conectarse();
            sql_con.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter(consulta, sql_con);
            sda.Fill(DT);
            sql_con.Close();
            return DT;
        }

        public string[] ObtenerVentaGuardada(int IDUsuario, int IDFolio)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT D.IDProducto, D.Nombre, D.Precio, P.TipoDescuento, P.Stock, P.Tipo, D.Cantidad, D.IDVenta FROM Ventas V INNER JOIN ProductosVenta D INNER JOIN Productos P ON V.ID = D.IDVenta AND D.IDProducto = P.ID WHERE V.IDUsuario = {IDUsuario} AND V.Status = '2' AND V.Folio = {IDFolio}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID producto
                lista.Add(dr[1].ToString());  //Nombre
                lista.Add(dr[2].ToString());  //Precio
                lista.Add(dr[3].ToString());  //Tipo descuento
                lista.Add(dr[4].ToString());  //Stock
                lista.Add(dr[5].ToString());  //Tipo (producto o servicio)
                lista.Add(dr[6].ToString());  //Cantidad de la venta
                lista.Add(dr[7].ToString());  //ID Venta
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] BuscarProducto(int IDProducto, int IDUsuario, bool especial = false)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Productos WHERE ID = {IDProducto} AND IDUsuario = {IDUsuario}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ID"].ToString());                 // ID producto  0
                lista.Add(dr["Nombre"].ToString());             // Nombre   1
                lista.Add(dr["Precio"].ToString());             // Precio   2
                lista.Add(dr["TipoDescuento"].ToString());      // Tipo descuento    3
                lista.Add(dr["Stock"].ToString());              // Stock 4
                lista.Add(dr["Tipo"].ToString());               // Tipo (producto o servicio) 5
                lista.Add(dr["ClaveInterna"].ToString());       // Clave  6
                lista.Add(dr["CodigoBarras"].ToString());       // Codigo de barras   7
                lista.Add(dr["StockNecesario"].ToString());     // Stock Maximo 8
                lista.Add(dr["ProdImage"].ToString());          // Imagen    9
                lista.Add(dr["StockMinimo"].ToString());        // Stock Minimo    10
                lista.Add(dr["PrecioCompra"].ToString());       // Precio Compra    11
                lista.Add(dr["PrecioMayoreo"].ToString());      // Precio Mayoreo   12
                lista.Add(dr["Impuesto"].ToString());           // Impuesto 13
                lista.Add(dr["Categoria"].ToString());          // Categoria    14
                lista.Add(dr["ProdImage"].ToString());          // Imagen de Producto   15
                lista.Add(dr["ClaveProducto"].ToString());      // Clave de Producto    16
                lista.Add(dr["UnidadMedida"].ToString());       // Unidad de Medida 17

                if (especial)
                {
                    lista.Add(dr["SoloRenta"].ToString()); // Solo Renta 18
                }
                lista.Add(dr["incluye_impuestos"].ToString());  // 18
                lista.Add(dr["nombre_ctercero"].ToString());    // 19
                lista.Add(dr["rfc_ctercero"].ToString());       // 20
                lista.Add(dr["cp_ctercero"].ToString());        // 21
                lista.Add(dr["regimen_ctercero"].ToString());   // 22
                lista.Add(dr["ImgNew"].ToString());             // 23
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] BuscarDescuento(int tipo, int IDProducto)
        {
            List<string> lista = new List<string>();

            string consulta = null;

            if (tipo == 1)
            {
                consulta = "SELECT * FROM DescuentoCliente WHERE IDProducto = " + IDProducto;
            }

            if (tipo == 2)
            {
                consulta = "SELECT * FROM DescuentoMayoreo WHERE IDProducto = " + IDProducto + " ORDER BY ID DESC";
            }

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                //Descuento Cliente
                if (tipo == 1)
                {
                    var cadena = $"{dr["PrecioProducto"]}-{dr["PorcentajeDescuento"]}-{dr["PrecioDescuento"]}-{dr["Descuento"]}";

                    lista.Add(cadena);
                }

                //Descuento Mayoreo
                if (tipo == 2)
                {
                    lista.Add(dr[1].ToString() + "-" + dr[2].ToString() + "-" + dr[3].ToString() + "-" + dr[4].ToString());
                }
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[]  BuscarVentaGuardada(int IDVenta, int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Ventas WHERE ID = {IDVenta} AND IDUsuario = {IDUsuario}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["Subtotal"].ToString());
                lista.Add(dr["IVA16"].ToString());
                lista.Add(dr["Total"].ToString());
                lista.Add(dr["Descuento"].ToString());
                lista.Add(dr["DescuentoGeneral"].ToString());
                lista.Add(dr["Folio"].ToString());
                lista.Add(dr["Serie"].ToString());
                lista.Add(dr["FechaOperacion"].ToString());
                lista.Add(dr["IDClienteDescuento"].ToString());
                lista.Add(dr["Cliente"].ToString());
                lista.Add(dr["IDCliente"].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] BuscarAnticipo(int IDVenta, int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Anticipos WHERE IDVenta = {IDVenta} AND IDUsuario = {IDUsuario}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["Concepto"].ToString());
                lista.Add(dr["Importe"].ToString());
                lista.Add(dr["Cliente"].ToString());
                lista.Add(dr["Comentarios"].ToString());
                lista.Add(dr["Fecha"].ToString());
                lista.Add(dr["ImporteOriginal"].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerProductosVenta(int IDVenta)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM ProductosVenta WHERE IDVenta = {IDVenta}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                //IDProducto, Nombre, Cantidad, descuento, TipoDescuento

                var datos = $"{dr["IDProducto"]}|{dr["Nombre"]}|{dr["Cantidad"]}|{dr["descuento"]}|{dr["TipoDescuento"]}";

                lista.Add(datos);
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] DatosUsuario(int IDUsuario = 0, int tipo = 0, string usuario = "", string password = "")
        {
            string consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = $"SELECT * FROM Usuarios WHERE ID = '{IDUsuario}'";
            }

            if (tipo == 1)
            {
                consulta = $"SELECT * FROM Usuarios WHERE Usuario = '{usuario}' AND Password = '{password}'";
            }

            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[7].ToString());                    // 0  Nombre completo
                lista.Add(dr[8].ToString());                    // 1  Calle
                lista.Add(dr[9].ToString());                    // 2  No. Exterior
                lista.Add(dr[10].ToString());                   // 3  No. Interior
                lista.Add(dr[12].ToString());                   // 4  Municipio
                lista.Add(dr[13].ToString());                   // 5  Estado
                lista.Add(dr[11].ToString());                   // 6  Colonia
                lista.Add(dr[14].ToString());                   // 7  Codigo Postal
                lista.Add(dr[4].ToString());                    // 8  RFC
                lista.Add(dr[6].ToString());                    // 9  Email
                lista.Add(dr[5].ToString());                    // 10 Telefono
                lista.Add(dr[17].ToString());                   // 11 Logo
                lista.Add(dr["VerificacionNS"].ToString());     // 12 Verificación de Licencia
                lista.Add(dr["ID"].ToString());                 // 13 Número de Usuario
                lista.Add(dr["Password"].ToString());           // 14 Contraseña
                lista.Add(dr["FechaHoy"].ToString());           // 15 FechaHoy
            }

            dr.Close();

            return lista.ToArray();
        }

        public string Capitalizar(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
            {
                return string.Empty;
            }

            return char.ToUpper(cadena[0]) + cadena.Substring(1);
        }

        public string[] ObtenerProductosServicio(int IDServicio)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = {IDServicio}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr[3] + "|" + dr[5]); //ID producto y cantidad
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerProveedores(int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Proveedores WHERE IDUsuario = {IDUsuario} AND Status = 1";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr[0] + " - " + dr[2]); //ID y Nombre
            }

            dr.Close();

            return lista.ToArray();
        }

        internal void EjecutarConsulta(object actualizarCompraMinimaMultiple)
        {
            throw new NotImplementedException();
        }

        public string[] ObtenerProveedor(int idProveedor, int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Proveedores WHERE ID = {idProveedor} AND IDUsuario = {IDUsuario}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[2].ToString()); //Nombre
                lista.Add(dr[3].ToString()); //RFC
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] VerificarStockProducto(int IDProducto, int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Productos WHERE ID = {IDProducto} AND IDUsuario = {IDUsuario}";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1] + "|" + dr[2]); //Nombre y Stock
            }

            dr.Close();

            return lista.ToArray();
        }


        public int ObtenerUltimoIdReporte(int IDUsuario)
        {
            int idReporte = 0;

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT MAX(IDReporte) AS ID FROM HistorialCompras WHERE IDUsuario = {IDUsuario} AND IDReporte <> ''";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr["ID"] == DBNull.Value || string.IsNullOrWhiteSpace(dr["ID"].ToString()))
                {
                    idReporte = 0;
                }
                else
                {
                    idReporte = Convert.ToInt32(dr["ID"]);
                }
            }

            dr.Close();

            return idReporte;
        }

        public float ObtenerSaldoActual(int IDUsuario)
        {
            float saldo = 0f;

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT Saldo FROM Caja WHERE IDUsuario = {IDUsuario} ORDER BY FechaOperacion DESC LIMIT 1";
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                if (dr["Saldo"] == DBNull.Value || dr["Saldo"].ToString() == "")
                {
                    saldo = 0;
                }
                else
                {
                    saldo = float.Parse(dr["Saldo"].ToString());
                }
            }

            dr.Close();

            return saldo;
        }

        public int CountColumnasTabla(string consulta)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            int contador = 0;

            while (dr.Read())
            {
                contador++;
            }
            dr.Close();

            return contador;
        }

        public string ForeginKeysOff()
        {
            string query = "PRAGMA foreign_keys = OFF;";
            //Conectarse();
            //sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //sql_cmd.CommandText = query;
            //sql_cmd.ExecuteNonQuery();
            return query;
        }

        public string BeginTransaction()
        {
            string query = "BEGIN TRANSACTION;";
            //Conectarse();
            //sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //sql_cmd.CommandText = query;
            //sql_cmd.ExecuteNonQuery();
            return query;
        }

        public void renameTable(string Query)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = Query;
            sql_cmd.ExecuteNonQuery();
        }

        public void CrearTabla(string Query)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = Query;
            sql_cmd.ExecuteNonQuery();
        }

        public string Commit()
        {
            string query = "COMMIT;";
            //Conectarse();
            //sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //sql_cmd.CommandText = query;
            //sql_cmd.ExecuteNonQuery();
            return query;
        }

        public string ForeginKeysOn()
        {
            string query = "PRAGMA foreign_keys = ON;";
            //Conectarse();
            //sql_con.Open();
            //sql_cmd = sql_con.CreateCommand();
            //sql_cmd.CommandText = query;
            //sql_cmd.ExecuteNonQuery();
            return query;
        }

        public void insertDataIntoTable(string Query)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = Query;
            sql_cmd.ExecuteNonQuery();
        }

        public void dropOldTable(string Query)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = Query;
            sql_cmd.ExecuteNonQuery();
        }

        public bool IsEmptyTable(string Consulta)
        {
            bool TieneRegistros = false;

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = Consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                TieneRegistros = true;
            }
            else
            {
                TieneRegistros = false;
            }

            dr.Close();

            return TieneRegistros;
        }

        public string getStringConnection()
        {
            var stringConnection = "datasource=127.0.0.1;port=6666;username=root;password=;";

            return stringConnection;
        }

        public void crearViewDinamica(string consulta)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void metergoella(string consulta, string idEmpleado, byte[] huella)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.Parameters.Add("IDUsuario", MySqlDbType.Int32).Value = FormPrincipal.userID;
            sql_cmd.Parameters.Add("IDEmpleado", MySqlDbType.Int32).Value = idEmpleado;
            sql_cmd.Parameters.Add("Huella", MySqlDbType.VarBinary, 65000).Value = huella;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void insertarHuellaCliente(string consulta, string idCliente, byte[] huella,string nombre)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.Parameters.Add("IDUsuario", MySqlDbType.Int32).Value = FormPrincipal.userID;
            sql_cmd.Parameters.Add("IDEmpleado", MySqlDbType.Int32).Value = idCliente;
            sql_cmd.Parameters.Add("Huella", MySqlDbType.VarBinary, 65000).Value = huella;
            sql_cmd.Parameters.Add("Nombre", MySqlDbType.Text).Value = nombre;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public List<byte[]> sacargoella()
        {
            string consulta = $"SELECT * FROM detalleschecadorempleados WHERE idUsuario = '{FormPrincipal.userID}'";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<byte[]> lista = new List<byte[]>();

            while (dr.Read())
            {
                byte[] huella = (byte[])dr["Huella"];
                lista.Add(huella);
            }

            dr.Close();
            return lista;
        }

        public List<byte[]> buscarMuestrasClientesHuella(string idCliente)
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE IDUsuario = '{FormPrincipal.userID}' AND IDCliente = {idCliente}";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<byte[]> lista = new List<byte[]>();

            while (dr.Read())
            {
                byte[] huella = (byte[])dr["Huella"];
                lista.Add(huella);
            }

            dr.Close();
            return lista;
        }

        public List<byte[]> buscarMuestrasClientesHuella()
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE IDUsuario = '{FormPrincipal.userID}'";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<byte[]> lista = new List<byte[]>();

            while (dr.Read())
            {
                byte[] huella = (byte[])dr["Huella"];
                lista.Add(huella);
            }

            dr.Close();
            return lista;
        }

        public List<string> sacarUsuariosgoella()
        {
            string consulta = $"SELECT * FROM detalleschecadorempleados WHERE idUsuario = '{FormPrincipal.userID}'";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<string> nombres = new List<string>();

            while (dr.Read())
            {
                nombres.Add(dr["IDEmpleado"].ToString());
            }

            dr.Close();
            return nombres;
        }

        public List<string> buscarClientesHuella(string idCliente)
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE idUsuario = '{FormPrincipal.userID}' AND IDCliente = {idCliente}";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<string> nombres = new List<string>();

            while (dr.Read())
            {
                nombres.Add(dr["IDCliente"].ToString());
            }

            dr.Close();
            return nombres;
        }

        public List<string> buscarClientesHuella()
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE idUsuario = '{FormPrincipal.userID}'";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<string> nombres = new List<string>();

            while (dr.Read())
            {
                nombres.Add(dr["IDCliente"].ToString());
            }

            dr.Close();
            return nombres;
        }

        public List<string> buscarNombresHuella(string idCliente)
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE idUsuario = '{FormPrincipal.userID}' AND IDCliente = {idCliente}";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<string> nombres = new List<string>();

            while (dr.Read())
            {
                nombres.Add(dr["Nombre"].ToString());
            }

            dr.Close();
            return nombres;
        }

        public List<string> buscarNombresHuella()
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE idUsuario = '{FormPrincipal.userID}'";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<string> nombres = new List<string>();

            while (dr.Read())
            {
                nombres.Add(dr["Nombre"].ToString());
            }

            dr.Close();
            return nombres;
        }

        public List<string> buscarIdsHuellaClientes(string idCliente)
        {
            string consulta = $"SELECT * FROM huellasclientes WHERE idUsuario = '{FormPrincipal.userID}' AND IDCliente = {idCliente}";
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();

            MySqlDataReader dr = sql_cmd.ExecuteReader();
            List<string> nombres = new List<string>();

            while (dr.Read())
            {
                nombres.Add(dr["ID"].ToString());
            }

            dr.Close();
            return nombres;
        }



        public void insertarUnPincheTextoAcaTremendoAaaaaa(string datos,DateTime fecha)
        {
            string consulta = "INSERT INTO WebRespaldosBuilder(IDCliente, Datos, Fecha)";
            consulta += $"VALUES(@IDCliente,@Datos,@Fecha)";

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.Parameters.Add("IDCliente", MySqlDbType.String).Value = FormPrincipal.userNickName.Split('@')[0];
            sql_cmd.Parameters.Add("Datos", MySqlDbType.LongText).Value = datos;
            sql_cmd.Parameters.Add("Fecha", MySqlDbType.DateTime).Value = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }


        public void insertImage(string consulta, byte[] imageData)
        {
            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.Parameters.Add("ImageData", MySqlDbType.VarBinary, 65000).Value = imageData;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
    }
}
