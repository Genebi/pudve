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
using System.IO;
using System.Collections.Specialized;

namespace PuntoDeVentaV2
{
    class Conexion
    {
        //Variables iniciales
        //rutaLocal es la variable que se debe usar cuando se haga el instalador
        //public string rutaLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));

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


        public void Conectarse()
        {
            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            //sql_con = new SQLiteConnection("Data source=" + rutaLocal + @"\pudveDB.db; Version=3; New=False;Compress=True;");
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
                    respuesta = dr["ID"]; //ID del usuario por ejemplo
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

        public DataTable ConsultaRegimenFiscal()
        {
            string consulta = "SELECT CodigoRegimen, Descripcion FROM RegimenFiscal";
            DataTable dbcb = new DataTable();
            Conectarse();
            sql_con.Open();
            SQLiteCommand com = new SQLiteCommand(consulta, sql_con);
            SQLiteDataAdapter adap = new SQLiteDataAdapter(com);
            adap.Fill(dbcb);
            sql_con.Close();

            return dbcb;
        }
        public DataTable cargarCBRegimen(string consulta)
        {
            DataTable dbcbreg = new DataTable();
            Conectarse();
            sql_con.Open();
            SQLiteCommand com = new SQLiteCommand(consulta, sql_con);
            SQLiteDataAdapter adap = new SQLiteDataAdapter(com);
            adap.Fill(dbcbreg);
            sql_con.Close();

            return dbcbreg;
        }

        public DataTable GetEmpresas(string consulta)
        {
            Conectarse();
            sql_con.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(consulta, sql_con);
            sda.Fill(DT);
            sql_con.Close();
            return DT;
        }

        public DataTable GetStockProd(string consulta)
        {
            Conectarse();
            sql_con.Open();
            SQLiteDataAdapter sda = new SQLiteDataAdapter(consulta, sql_con);
            sda.Fill(DT);
            sql_con.Close();
            return DT;
        }

        public NameValueCollection ObtenerProductos(int IDUsuario)
        {
            NameValueCollection lista = new NameValueCollection();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "SELECT * FROM Productos WHERE IDUsuario = " + IDUsuario;
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                //dr[0] = ID del producto
                //dr[1] = Nombre producto
                lista.Add(dr[0].ToString(), dr[1].ToString().ToLower());
            }

            dr.Close();

            return lista;
        }

        public string[] ObtenerVentaGuardada(int IDUsuario, int IDFolio)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT usr.LogoTipo, usr.Usuario, usr.Calle, usr.NoExterior, usr.NoInterior, usr.Municipio, usr.Colonia, usr.CodigoPostal, usr.RFC, usr.Email, usr.Telefono, prod.ID AS 'NoProd', prod.Nombre AS 'NomProd', prod.Precio AS 'CostoProd', prod.TipoDescuento, prod.Stock, prod.Tipo, saleProd.Cantidad, saleProd.Nombre AS 'NomVenta', saleProd.Precio AS 'CostoVenta', sale.FechaOperacion, sale.ID FROM Usuarios AS usr LEFT JOIN Ventas AS sale ON sale.IDUsuario = usr.ID LEFT JOIN ProductosVenta AS saleProd ON saleProd.IDVenta = sale.ID LEFT JOIN Productos AS prod ON prod.ID = saleProd.IDProducto WHERE usr.ID = '{IDUsuario}' AND sale.Status = '2' AND sale.ID = '{IDFolio}'";
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[11].ToString());  //ID producto
                lista.Add(dr[12].ToString());  //Nombre
                lista.Add(dr[13].ToString());  //Precio
                lista.Add(dr[14].ToString());  //Tipo descuento
                lista.Add(dr[15].ToString());  //Stock
                lista.Add(dr[16].ToString());  //Tipo (producto o servicio)
                lista.Add(dr[17].ToString());  //Cantidad de la venta
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] BuscarProducto(int IDProducto, int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "SELECT * FROM Productos WHERE ID = "+ IDProducto +" AND IDUsuario = " + IDUsuario;
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID producto
                lista.Add(dr[1].ToString());  //Nombre
                lista.Add(dr[3].ToString());  //Precio
                lista.Add(dr[9].ToString());  //Tipo descuento
                lista.Add(dr[2].ToString());  //Stock
                lista.Add(dr[13].ToString()); //Tipo (producto o servicio)
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

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                //Descuento Cliente
                if (tipo == 1)
                {

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

        public string[]  BuscarVentaGuardada(int IDVenta)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Ventas WHERE ID = '{IDVenta}'";
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[5].ToString()); //Subtotal
                lista.Add(dr[6].ToString()); //IVA16
                lista.Add(dr[8].ToString()); //Total
                lista.Add(dr[9].ToString()); //Descuento
                lista.Add(dr[10].ToString()); //Descuento general
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
            sql_cmd.CommandText = $"SELECT * FROM ProductosVenta WHERE IDVenta = '{IDVenta}'";
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr[2] + "|" + dr[3] + "|" + dr[4]); //IDProducto, Nombre y Cantidad
            }

            dr.Close();

            return lista.ToArray();
        }

        public float CalcularPorcentaje(string sCantidad)
        {
            int longitud = sCantidad.Length;

            float resultado = 0;

            //Si la cantidad por defecto es una cifra de dos digitos o mas
            if (longitud > 1)
            {
                //Si contiene punto la convertimos en array
                if (sCantidad.Contains('.'))
                {
                    string[] valorTmp = sCantidad.Split('.');

                    //Si es la cantidad de 1.600000 entrara aqui
                    if (valorTmp[0] == "1")
                    {
                        resultado = float.Parse(sCantidad);

                    }
                    else
                    {
                        sCantidad = sCantidad.Replace(".", "");
                        sCantidad = "0." + sCantidad;

                        resultado = float.Parse(sCantidad);
                    }
                }
                else
                {
                    sCantidad = "0." + sCantidad;
                    resultado = float.Parse(sCantidad);
                }
            }
            else
            {
                sCantidad = "0.0" + sCantidad;
                resultado = float.Parse(sCantidad);
            }

            return resultado;
        }

        public string[] DatosUsuario(int IDUsuario)
        {
            List<string> lista = new List<string>();

            Conectarse();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = $"SELECT * FROM Usuarios WHERE ID = '{IDUsuario}'";
            sql_cmd.ExecuteNonQuery();

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[7].ToString()); //Nombre completo
                lista.Add(dr[8].ToString()); //Calle
                lista.Add(dr[9].ToString()); //No. Exterior
                lista.Add(dr[10].ToString()); //No. Interior
                lista.Add(dr[12].ToString()); //Municipio
                lista.Add(dr[13].ToString()); //Estado
                lista.Add(dr[11].ToString()); //Colonia
                lista.Add(dr[14].ToString()); //Codigo Postal
                lista.Add(dr[4].ToString()); //RFC
                lista.Add(dr[6].ToString()); //Email
                lista.Add(dr[5].ToString()); //Telefono
                lista.Add(dr[17].ToString()); //Logo
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

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr[3] + "|" + dr[5]); //ID producto y cantidad
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

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1] + "|" + dr[2]); //Nombre y Stock
            }

            dr.Close();

            return lista.ToArray();
        }

        public void BackUpDB()
        {
            string path = @"C:\Archivos PUDVE\DB\";
            if (!Directory.Exists(path))	// verificamos que si no existe el directorio
            {
                Directory.CreateDirectory(path);	// lo crea para poder almacenar la imagen
            }

            using (var destination = new SQLiteConnection("Data Source=" + path + "BackupDb.db; Version=3; New=False;Compress=True;"))
            {
                Conectarse();
                sql_con.Open();
                destination.Open();
                sql_con.BackupDatabase(destination, "main", "main", -1, null, 0);
                destination.Close();
                sql_con.Close();
            }
        }
    }
}
