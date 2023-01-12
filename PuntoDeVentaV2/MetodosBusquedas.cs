using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    class MetodosBusquedas
    {
        public static float efectivoInicial { get; set; }
        public static float tarjetaInicial { get; set; }
        public static float valesInicial { get; set; }
        public static float chequeInicial { get; set; }
        public static float transInicial { get; set; }
        public static float totalSInicial { get; set; }

        public static int idFiltrado { get; set; }

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosGenerales mg = new MetodosGenerales();
        private MySqlConnection sql_con;
        private MySqlCommand sql_cmd;

        public void Conexion(bool ignorar = false)
        {
            if (ignorar == true)
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                {
                    sql_con = new MySqlConnection("datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;");
                }
                else
                {
                    sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
                }
            }
        }

        public string[] ObtenerDetallesVenta(int idVenta, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesVenta WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                //ID del cliente, Cliente, Credito
                lista.Add(dr[10].ToString());
                lista.Add(dr[11].ToString());
                lista.Add(dr[8].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        //public List<string> stockMinimoMaximo(string id)
        //{
        //     DatosConexion($"SELECT StockMinimo, StockNecesario FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND");
        //}


        public string ObtenerFechaUltimoCorte()
        {
            string fechaCorte = string.Empty;
            DatosConexion($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();   

            if (dr.Read())
            {
                fechaCorte = dr[0].ToString();
            }


            return fechaCorte;
        }

        public int buscarIDConFolio(string folio)
        {
            int idVenta = 0;
            DatosConexion($"SELECT ID FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio = '{folio}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                idVenta = Convert.ToInt32(dr[0].ToString());
            }

            return idVenta;
        }

        public string ObtenerFechaVenta(int id)
        {
            string fechaVenta = string.Empty;
            DatosConexion($"SELECT FechaOperacion FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fechaVenta = dr[0].ToString();
            }

            dr.Close();
            CerrarConexion();

            return fechaVenta;
        }

        public int ObtenerStatusVenta(string folio)
        {
            var result = 0;
            var query = cn.CargarDatos($"SELECT `Status` AS estatus FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio = '{folio}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = Convert.ToInt32(query.Rows[0]["estatus"].ToString());
            }

            return result;
        }

        public float ObtenerTotalAbonado(int idVenta, int idUsuario)
        {
            float cantidad = 0f;

            DatosConexion($"SELECT SUM(Total) AS Total FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                cantidad = float.Parse(dr["Total"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return cantidad;
        }

        public int obtenerIdDelFolio(string folio)
        {
            var idObtenido = 0;
            var id = string.Empty;

            DatosConexion($"SELECT ID FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio = '{folio}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                id = dr[0].ToString();
            }

            idObtenido = Convert.ToInt32(id);

            dr.Close();
            CerrarConexion();

            return idObtenido;
        }

        public float[] ObtenerFormasPagoVenta(int idVenta, int idUsuario)
        {
            List<float> lista = new List<float>();

            DatosConexion($"SELECT * FROM DetallesVenta WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(float.Parse(dr["Efectivo"].ToString()));
                lista.Add(float.Parse(dr["Tarjeta"].ToString()));
                lista.Add(float.Parse(dr["Vales"].ToString()));
                lista.Add(float.Parse(dr["Cheque"].ToString()));
                lista.Add(float.Parse(dr["Transferencia"].ToString()));
                lista.Add(float.Parse(dr["Credito"].ToString()));
                lista.Add(float.Parse(dr["Anticipo"].ToString()));
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerDatosCliente(int idCliente, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Clientes WHERE ID = {idCliente} AND IDUsuario = {idUsuario}");

             MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[2].ToString()); //Razon social         0
                lista.Add(dr[4].ToString()); //RFC                  1
                lista.Add(dr[3].ToString()); //Nombre comercial     2
                lista.Add(dr[5].ToString()); //Uso CFDI             3
                lista.Add(dr[6].ToString()); //Pais                 4
                lista.Add(dr[7].ToString()); //Estado               5
                lista.Add(dr[8].ToString()); //Municipio            6
                lista.Add(dr[9].ToString()); //Localidad            7
                lista.Add(dr[10].ToString()); //Codigo postal       8
                lista.Add(dr[11].ToString()); //Colonia             9
                lista.Add(dr[12].ToString()); //Calle               10
                lista.Add(dr[13].ToString()); //No. exterior        11
                lista.Add(dr[14].ToString()); //No. interior        12
                //lista.Add(dr[15].ToString()); //Regimen fiscal    
                lista.Add(dr[16].ToString()); //Email               13
                lista.Add(dr[17].ToString()); //Telefono            14
                lista.Add(dr[18].ToString()); //Forma de pago       15
                lista.Add(dr[21].ToString()); //TipoCliente         16
                lista.Add(dr[22].ToString()); //NumeroCliente       17
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerClientes(int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Clientes WHERE IDUsuario = {idUsuario} AND Status = 1");
            
            MySqlDataReader dr = sql_cmd.ExecuteReader();

            int cant = dr.FieldCount;

            if (cant > 0 & cant > 1)
            {
                lista.Add("Seleccione un cliente");
            }

            while (dr.Read())
            {
                lista.Add(dr["RazonSocial"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public int ExisteClientePublicoGeneral(int usuarioId)
        {
            int clienteId = 0;

            DatosConexion($"SELECT * FROM Clientes WHERE IDUsuario = {usuarioId} AND RazonSocial = 'PUBLICO GENERAL' AND RFC = 'XAXX010101000' AND Status = 1");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                clienteId = Convert.ToInt32(dr["Id"]);
            }

            return clienteId;
        }

        public string[] ObtenerDetalleGral(int idDetalleGral, int idUsr)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE ID = '{idDetalleGral}' AND IDUsuario = '{idUsr}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString()); //ID
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //ChckName
                lista.Add(dr[3].ToString().Replace("_", " ")); //Descripcion
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerDatosProveedor(int idProveedor, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Proveedores WHERE ID = {idProveedor} AND IDUsuario = {idUsuario} ORDER BY Nombre ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[2].ToString()); //Nombre
                lista.Add(dr[3].ToString()); //RFC
                lista.Add(dr[4].ToString()); //Calle
                lista.Add(dr[5].ToString()); //No. exterior
                lista.Add(dr[6].ToString()); //No. interior
                lista.Add(dr[7].ToString()); //Colonia
                lista.Add(dr[8].ToString()); //Municipio
                lista.Add(dr[9].ToString()); //Estado
                lista.Add(dr[10].ToString()); //Codigo postal
                lista.Add(dr[11].ToString()); //Email
                lista.Add(dr[12].ToString()); //Telefono
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerDatosCategoria(int idCategoria, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Categorias WHERE ID = {idCategoria} AND IDUsuario = {idUsuario} ORDER BY Nombre ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //Nombre
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerDatosUbicacion(int idUbicacion, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Ubicaciones WHERE ID = {idUbicacion} AND IDUsuario = {idUsuario} ORDER BY Descripcion ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //Descripcion
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerDatosDetalleGral(int idDetalleGral, int idUsuario, int idProducto)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario} AND IDDetalleGral = {idDetalleGral} ORDER BY IDDetalleGral ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1].ToString()); //IDProducto
                lista.Add(dr[2].ToString()); //IDUsuario
                lista.Add(dr[3].ToString()); //IDDetalleGral
                lista.Add(dr[4].ToString()); //StatusDetalleGral
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] obtenerUnDetalleProductoGenerales(string idProducto, string idUsuario, string panel)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = '{idProducto}' AND IDUsuario = '{idUsuario}' AND panelContenido = '{panel}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString()); // ID
                lista.Add(dr[1].ToString()); // IDProducto
                lista.Add(dr[2].ToString()); // IDUsuario
                lista.Add(dr[3].ToString()); // IDDetalleGral
                lista.Add(dr[4].ToString()); // StatusDetalleGral
                lista.Add(dr[5].ToString()); // panelContenido
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] obtenerIdDetalleGeneral(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND Descripcion = '{Descripcion}' ORDER BY Descripcion ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString()); //ID
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //ChckName
                lista.Add(dr[3].ToString()); //Descripcion
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] obtenerIdDetallesProveedor(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Proveedores WHERE IDUsuario = '{idUsuario}' AND Nombre = '{Descripcion}' ORDER BY Nombre ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID
                lista.Add(dr[1].ToString());  //IDUsuario
                lista.Add(dr[2].ToString());  //Nombre
                lista.Add(dr[3].ToString());  //RFC
                lista.Add(dr[4].ToString());  //Calle
                lista.Add(dr[5].ToString());  //NoExterior
                lista.Add(dr[6].ToString());  //NoInterior
                lista.Add(dr[7].ToString());  //Colonia
                lista.Add(dr[8].ToString());  //Municipio
                lista.Add(dr[9].ToString());  //Estado
                lista.Add(dr[10].ToString()); //CodigoPostal
                lista.Add(dr[11].ToString()); //Email
                lista.Add(dr[12].ToString()); //Telefono
                lista.Add(dr[13].ToString()); //FechaOperacion
                lista.Add(dr[14].ToString()); //Status
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] obtenerIdDetallesCategorias(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Categorias WHERE IDUsuario = '{idUsuario}' AND Nombre = '{Descripcion}' ORDER BY Nombre ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID
                lista.Add(dr[1].ToString());  //IDUsuario
                lista.Add(dr[2].ToString());  //Nombre
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] obtenerIdDetallesUbicacion(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Ubicaciones WHERE IDUsuario = '{idUsuario}' AND Descripcion = '{Descripcion}' ORDER BY Descripcion ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID
                lista.Add(dr[1].ToString());  //IDUsuario
                lista.Add(dr[2].ToString());  //Descripcion
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] DetallesProducto(int idProducto, int idUsuario)
        {
            List<string> lista = new List<string>();

                DatosConexion($"SELECT * FROM DetallesProducto WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ID"].ToString());
                lista.Add(dr["IDProveedor"].ToString());
                lista.Add(dr["Proveedor"].ToString());
                lista.Add(dr["IDCategoria"].ToString());
                lista.Add(dr["Categoria"].ToString());
                lista.Add(dr["IDUbicacion"].ToString());
                lista.Add(dr["Ubicacion"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] DetallesProductoGralPorPanel(string namePanel, int idUsuario, int idProducto)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDUsuario = '{idUsuario}' AND panelContenido = '{namePanel}' AND IDProducto = '{idProducto}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ID"].ToString());
                lista.Add(dr["IDProducto"].ToString());
                lista.Add(dr["IDUsuario"].ToString());
                lista.Add(dr["IDDetalleGral"].ToString());
                lista.Add(dr["StatusDetalleGral"].ToString());
                lista.Add(dr["panelContenido"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] DetallesProductoGral(int idUsuario, int idProdDetailGral)
        {
            List<string> lista = new List<string>();

            //DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario}");
            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND ID = '{idProdDetailGral}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["IDUsuario"].ToString());
                lista.Add(dr["ChckName"].ToString());
                lista.Add(dr["Descripcion"].ToString().Replace("_"," "));
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] GetDetalleGeneral(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND Descripcion = '{Descripcion}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ID"].ToString());
                lista.Add(dr["IDUsuario"].ToString());
                lista.Add(dr["ChckName"].ToString());
                lista.Add(dr["Descripcion"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }
        
        public int BuscarProductoInventario(string producto)
        {
            int idProducto = 0;

            string consulta = string.Empty;

            //Busqueda por codigo de barra y/o clave
            consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND (CodigoBarras = '{producto}' OR ClaveInterna = '{producto}')";

            DatosConexion(consulta);

            MySqlDataReader datos = sql_cmd.ExecuteReader();

            if (datos.Read())
            {
                idProducto = Convert.ToInt32(datos["ID"].ToString());
            }

            datos.Close();
            CerrarConexion();

            return idProducto;
        }

        public int BuscarComboInventario(string Combo)
        {
            int idCombo = 0;

            string consulta = string.Empty;
            
            //Busqueda por codigo de barra y/o clave
            consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'PQ' AND (CodigoBarras = '{Combo}' OR ClaveInterna = '{Combo}')";

            DatosConexion(consulta);

            MySqlDataReader datos = sql_cmd.ExecuteReader();

            if (datos.Read())
            {
                idCombo = Convert.ToInt32(datos["ID"].ToString());
            }

            datos.Close();
            CerrarConexion();

            return idCombo;
        }

        public string[] BuscarProductosDeServicios(string idCombo)
        {
            List<string> lista = new List<string>();

           string consulta = string.Empty;

            //Buscar por Codigo de Combo en la tabla ProductosDeServicios
            consulta = $"SELECT * FROM ProductosDeServicios WHERE IDServicio ={idCombo}";

            DatosConexion(consulta);

            MySqlDataReader datos = sql_cmd.ExecuteReader();

            if (datos.HasRows)
            {
                while (datos.Read())
                {
                    if (!string.IsNullOrWhiteSpace(datos["IDServicio"].ToString()))
                    {
                        lista.Add(datos["IDServicio"].ToString() + "|" + datos["IDProducto"].ToString() + "|" + datos["NombreProducto"].ToString() + "|" + datos["Cantidad"].ToString());
                    }
                }
            }

            datos.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerAnticipo(int idAnticipo, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Anticipos WHERE ID = {idAnticipo} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["Concepto"].ToString());
                lista.Add(dr["Importe"].ToString());
                lista.Add(dr["Cliente"].ToString());
                lista.Add(dr["FormaPago"].ToString());
                lista.Add(dr["Comentarios"].ToString());
                lista.Add(dr["Fecha"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        /// <summary>
        /// Metodo de Obtener Codigo de barras Extras
        /// </summary>
        /// <param name="idProducto">Número con que esta registrado el producto</param>
        /// <param name="opcion">Opcion=0(Llamado de las formas PedidoPorProducto y Revisar Inventario) Opcion=1(Llamado del Form OpcionesReporteProducto)</param>
        /// <returns></returns>
        public string[] ObtenerCodigoBarrasExtras(int idProducto, int opcion = 0)
        {
            //PONER COMO SEGUNDO PARAMETRO AL UTILIZAR UN METODO (1)
            /*
             opcion = 0 (Llamado de las formas PedidoPorProducto y Revisar Inventario)
             opcion = 1 (Llamado del Form OpcionesReporteProducto)
             */
            List<string> lista = new List<string>();

            if (opcion.Equals(0))
            {
                DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = {idProducto} AND (CodigoBarras != '' OR ClaveInterna != '')");
            }
            else if (opcion.Equals(1))
            {
                DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = {idProducto}");
            }

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var codigo = dr["CodigoBarraExtra"].ToString().Trim();

                    if (!string.IsNullOrWhiteSpace(codigo))
                    {
                        lista.Add(codigo);
                    }
                }
            }
            
            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public bool ComprobarCodigoClave(string codigoClave, int idUsuario, int idProductoTMP = 0)
        {
            string[] codigos = new string[] { };
            // Si es un producto, servicio o paquete que se esta editando
            if (idProductoTMP > 0)
            {
                List<string> lista = new List<string>();

                // Obtenemos todos los codigos de barra y clave que tenga registrado
                DatosConexion($"SELECT ClaveInterna, CodigoBarras FROM Productos WHERE ID = {idProductoTMP} AND IDUsuario = {idUsuario}");

                MySqlDataReader dr = sql_cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (!string.IsNullOrWhiteSpace(dr["ClaveInterna"].ToString()))
                    {
                        lista.Add(dr["ClaveInterna"].ToString());
                    }

                    if (!string.IsNullOrWhiteSpace(dr["CodigoBarras"].ToString()))
                    {
                        lista.Add(dr["CodigoBarras"].ToString());
                    }
                }

                dr.Close();
                CerrarConexion();

                // Obtener todos los codigos de la tabla de codigos de barra extra
                DatosConexion($"SELECT CB.CodigoBarraExtra FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {idUsuario} AND CB.IDProducto = {idProductoTMP}");

                MySqlDataReader info = sql_cmd.ExecuteReader();

                if (info.HasRows)
                {
                    while (info.Read())
                    {
                        if (!string.IsNullOrWhiteSpace(info["CodigoBarraExtra"].ToString()))
                        {
                            lista.Add(info["CodigoBarraExtra"].ToString());
                        }
                    }
                }

                info.Close();
                CerrarConexion();

                codigos = lista.ToArray();
            }

            if (codigos.Length > 0)
            {
                if (codigos.Contains(codigoClave))
                {
                    return false;
                }
            }

            bool respuesta = false;

            if (!string.IsNullOrWhiteSpace(codigoClave))
            {
                codigoClave = codigoClave.Trim();

                DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {idUsuario} AND Status = 1 AND (CodigoBarras  = '{codigoClave}' OR ClaveInterna = '{codigoClave}')");

                MySqlDataReader dr = sql_cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = true;
                }
                else
                {
                    //DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigoClave}'");
                    DatosConexion($"SELECT CB.IDProducto FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {idUsuario} AND CB.CodigoBarraExtra = '{codigoClave}'");

                    MySqlDataReader info = sql_cmd.ExecuteReader();

                    if (info.HasRows)
                    {
                        //Comprobar el ID del producto y el ID del Usuario
                        while (info.Read())
                        {
                            var idProducto = Convert.ToInt32(info["IDProducto"].ToString());

                            DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {idUsuario} AND Status = 1");

                            MySqlDataReader info2 = sql_cmd.ExecuteReader();

                            if (info2.Read())
                            {
                                respuesta = true;
                            }

                            info2.Close();
                            CerrarConexion();
                        }
                    }
                    else
                    {
                        respuesta = false;
                    }

                    info.Close();
                    CerrarConexion();
                }

                dr.Close();
                CerrarConexion();
            }
            
            return respuesta;
        }

        public string[] ObtenerClaveCodigosProducto(int idProducto, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                if (!string.IsNullOrWhiteSpace(dr["ClaveInterna"].ToString()))
                {
                    lista.Add(dr["ClaveInterna"].ToString());
                }

                if (!string.IsNullOrWhiteSpace(dr["CodigoBarras"].ToString()))
                {
                    lista.Add(dr["CodigoBarras"].ToString());
                } 
            }

            dr.Close();
            CerrarConexion();


            DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = {idProducto}");

            MySqlDataReader info = sql_cmd.ExecuteReader();

            if (info.HasRows)
            {
                while (info.Read())
                {
                    if (!string.IsNullOrWhiteSpace(info["CodigoBarraExtra"].ToString()))
                    {
                        lista.Add(info["CodigoBarraExtra"].ToString());
                    }
                }
            }
            
            info.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerCategorias(int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Categorias WHERE IDUsuario = {idUsuario} ORDER BY Nombre ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["ID"] + "|" + dr["Nombre"]);
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerUbicaciones(int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Ubicaciones WHERE IDUsuario = {idUsuario} ORDER BY Descripcion ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["ID"] + "|" + dr["Descripcion"]);
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerDetallesGral(int idUsuario, string detalle)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND ChckName = '{detalle}' AND Mostrar = '1' ORDER BY Descripcion ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["ID"] + "|" + dr["Descripcion"]);
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string ObtenerMaximoFolio(int idUsuario)
        {
            var folio = string.Empty;

            DatosConexion($"SELECT MAX(Folio) AS Folio FROM Ventas WHERE IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                folio = dr["Folio"].ToString();

                if (string.IsNullOrWhiteSpace(folio))
                {
                    folio = "1";
                }
            }

            dr.Close();
            CerrarConexion();

            return folio;
        }

        public string[] ObtenerRevisionInventario(int idRevision, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM RevisarInventario WHERE ID = {idRevision} AND IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["IDAlmacen"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerPaqueteServicioAsignado(int prodID, int usrID)
        {
            List<string> lista = new List<string>();

            DatosConexion($@"SELECT 
                                -- Fields of Users
                                usr.ID as 'Num_Usuario',
                                usr.Usuario as 'Usuario',
                                -- Fields of Products
                                prod.ID as 'Num_Prod',
                                prod.Nombre as 'Nombre_Producto',
                                prod.Tipo as 'Tipo_Prod',
                                prod.NumeroRevision as 'Num_Revision',
                                -- Fields of Service of Products
                                ProdServ.ID as 'Num_Relacion',
                                ProdServ.IDServicio as 'Num_ServProd'
                             FROM Productos AS prod
                             INNER JOIN Usuarios AS usr
                             ON usr.ID = prod.IDUsuario
                             INNER JOIN ProductosDeServicios AS ProdServ
                             ON ProdServ.IDProducto = prod.ID
                             WHERE prod.ID = '{prodID}'
                             AND usr.ID = '{usrID}'
                             AND prod.Tipo = 'P'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["Num_Usuario"].ToString());
                lista.Add(dr["Usuario"].ToString());
                lista.Add(dr["Num_Prod"].ToString());
                lista.Add(dr["Nombre_Producto"].ToString());
                lista.Add(dr["Num_Revision"].ToString());
                lista.Add(dr["Num_Relacion"].ToString());
                lista.Add(dr["Num_ServProd"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] BuscarCodigoBarrasExtra(string codigo)
        {
            List<string> lista = new List<string>();
            string[] codigosABuscar;

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                codigosABuscar = codigo.Split(' ');

                foreach (var searchCodBar in codigosABuscar)
                {
                    //DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigo}'");
                    DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{searchCodBar}'");

                    MySqlDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (!string.IsNullOrWhiteSpace(dr["IDProducto"].ToString()))
                            {
                                lista.Add(dr["IDProducto"].ToString());
                            }
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }

            return lista.ToArray();
        }

        public string[] BuscarCodigoBarrasExtra2(string codigo,string tipo)
        {
            List<string> lista = new List<string>();
            string[] codigosABuscar;

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                codigosABuscar = codigo.Split(' ');

                foreach (var searchCodBar in codigosABuscar)
                {
                    if (tipo.Equals("Todos"))
                    {
                        DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{searchCodBar}'");
                    }
                    else
                    {
                        DatosConexion($"SELECT CodigoBarraExtra, IDProducto FROM codigobarrasextras AS CBE INNER JOIN productos AS P ON (P.ID = CBE.IDProducto) WHERE CodigoBarraExtra = '{searchCodBar}' AND P.IDUsuario = {FormPrincipal.userID} AND P.Tipo = '{tipo}'");
                    }

                    MySqlDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (!string.IsNullOrWhiteSpace(dr["IDProducto"].ToString()))
                            {
                                lista.Add(dr["IDProducto"].ToString());
                            }
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }

            return lista.ToArray();
        }

        public string[] BuscarCodigoBarrasExtraFormProductos(string codigo, bool especial = false)
        {
            List<string> lista = new List<string>();
            string[] codigosABuscar;

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                codigosABuscar = codigo.Split(' ');

                foreach (var searchCodBar in codigosABuscar)
                {
                    //DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigo}'");
                     DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{searchCodBar}'");

                    MySqlDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (!string.IsNullOrWhiteSpace(dr["IDProducto"].ToString()))
                            {
                                lista.Add("1|" + dr["IDProducto"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (!especial)
                        {
                            lista.Add("0|" + searchCodBar);
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }

            return lista.ToArray();
        }

        public string ObtenerCBGenerado(int idUsuario)
        {
            string codigo = string.Empty;

            DatosConexion($"SELECT * FROM CodigoBarrasGenerado WHERE IDUsuario = {idUsuario}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                codigo = dr["CodigoBarras"].ToString();
            }

            dr.Close();
            CerrarConexion();

            return codigo;
        }


        public float SaldoInicialCaja(int idUsuario)
        {
            float saldo = 0f;

            DatosConexion($"SELECT ID FROM Caja WHERE IDUsuario = {idUsuario} AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var idCaja = Convert.ToInt32(dr["ID"].ToString());

                DatosConexion($"SELECT * FROM Caja WHERE IDUsuario = {idUsuario} AND Operacion = 'venta' AND ID > {idCaja} ORDER BY ID LIMIT 1");

                MySqlDataReader info = sql_cmd.ExecuteReader();

                if (info.Read())
                {
                    efectivoInicial = float.Parse(info["Efectivo"].ToString());
                    tarjetaInicial = float.Parse(info["Tarjeta"].ToString());
                    valesInicial = float.Parse(info["Vales"].ToString());
                    chequeInicial = float.Parse(info["Cheque"].ToString());
                    transInicial = float.Parse(info["Transferencia"].ToString());
                    
                    totalSInicial = (efectivoInicial + tarjetaInicial + valesInicial + chequeInicial + transInicial);

                    saldo += float.Parse(info["Efectivo"].ToString());
                    saldo += float.Parse(info["Tarjeta"].ToString());
                    saldo += float.Parse(info["Vales"].ToString());
                    saldo += float.Parse(info["Cheque"].ToString());
                    saldo += float.Parse(info["Transferencia"].ToString());
                    //saldo += float.Parse(info["Credito"].ToString());
                }

                info.Close();
            }

            dr.Close();
            CerrarConexion();

            return saldo;
        }

        public float SaldoInicialCajaReportes(int idUsuario, int id)
        {
            float saldo = 0f;

            DatosConexion($"SELECT ID FROM Caja WHERE IDUsuario = {idUsuario} AND ID < '{id}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var idCaja = Convert.ToInt32(dr["ID"].ToString());

                DatosConexion($"SELECT * FROM Caja WHERE IDUsuario = {idUsuario} AND Operacion = 'venta' AND ID > {idCaja} ORDER BY ID LIMIT 1");

                MySqlDataReader info = sql_cmd.ExecuteReader();

                if (info.Read())
                {
                    efectivoInicial = float.Parse(info["Efectivo"].ToString());
                    tarjetaInicial = float.Parse(info["Tarjeta"].ToString());
                    valesInicial = float.Parse(info["Vales"].ToString());
                    chequeInicial = float.Parse(info["Cheque"].ToString());
                    transInicial = float.Parse(info["Transferencia"].ToString());
                    totalSInicial = (efectivoInicial + tarjetaInicial + valesInicial + chequeInicial + transInicial);

                    saldo += float.Parse(info["Efectivo"].ToString());
                    saldo += float.Parse(info["Tarjeta"].ToString());
                    saldo += float.Parse(info["Vales"].ToString());
                    saldo += float.Parse(info["Cheque"].ToString());
                    saldo += float.Parse(info["Transferencia"].ToString());
                    //saldo += float.Parse(info["Credito"].ToString());
                }

                info.Close();
            }

            dr.Close();
            CerrarConexion();

            return saldo;
        }

        public Dictionary<int, string> ObtenerOpcionesPropiedad(int idUsuario, string propiedad)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = {idUsuario} AND ChckName = '{propiedad}' AND Mostrar = 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lista.Add(Convert.ToInt32(dr["ID"]), dr["Descripcion"].ToString().Replace("_"," "));
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public Dictionary<int, int> BusquedaCoincidencias(string frase, int status)
        {
            Dictionary<int, int> coincidencias = new Dictionary<int, int>();

            string[] palabras = frase.TrimEnd().Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    //DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR CodigoBarras LIKE '%{palabra}%' OR ClaveInterna LIKE '%{palabra}%')");

                    if (status == 0)// Status = 0 busca solo en lo productos habilitaods
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR CodigoBarras LIKE '%{palabra}%' OR ClaveInterna LIKE '%{palabra}%')");
                    }
                    else if (status == 1)// Status = 1 busca solo en lo productos deshabilitados
                    {
                         DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 0  AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR CodigoBarras LIKE '%{palabra}%' OR ClaveInterna LIKE '%{palabra}%')");
                    }
                    else if (status == 2)// Status = 2 busca en todos los prodcutos 
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR CodigoBarras LIKE '%{palabra}%' OR ClaveInterna LIKE '%{palabra}%')");
                    }
                    

                   MySqlDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var id = Convert.ToInt32(dr["ID"].ToString());

                            if (coincidencias.ContainsKey(id))
                            {
                                coincidencias[id] += 1;
                            }
                            else
                            {
                                coincidencias.Add(id, 1);
                            }
                        }
                    }
                    dr.Close();
                    CerrarConexion();
                }
            }
            return coincidencias;
        }

        public string[] BusquedaCodigosBarrasClaveInterna(string codigo, int opcionBusqueda = 1, bool especial = false)
        {
            List<string> lista = new List<string>();

            string[] palabras = codigo.TrimEnd().Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    if (opcionBusqueda.Equals(1))   // buscar en Activos
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (CodigoBarras = '{palabra}' OR ClaveInterna = '{palabra}') AND Status = '{opcionBusqueda}'");
                    }
                    else if (opcionBusqueda.Equals(0))  // buscar en Inactivos
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (CodigoBarras = '{palabra}' OR ClaveInterna = '{palabra}') AND Status = '{opcionBusqueda}'");
                    }
                    else if (opcionBusqueda.Equals(2))  // buscar en todos
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (CodigoBarras = '{palabra}' OR ClaveInterna = '{palabra}')");
                    }

                    MySqlDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (!string.IsNullOrWhiteSpace(dr["ID"].ToString()))
                            {
                                lista.Add("1|" + dr["ID"].ToString());
                            }
                        }
                    }
                    else
                    {
                        if (!especial)
                        {
                            lista.Add("0|" + palabra);
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }

            return lista.ToArray();
        }

        public Dictionary<int, string> BusquedaCoincidenciasVentas(string frase, string filtro, int mPrecio = 0, int mCB = 0)
        {
            var lista = new Dictionary<int, string>();

             var coincidencias = new Dictionary<int, Tuple<int, string>>();

            string[] palabras = frase.Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    if (filtro.Equals("Todos"))
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR ClaveInterna = '{palabra}' OR CodigoBarras = '{palabra}')");
                    }
                    else
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Tipo = '{filtro}' AND Status = 1 AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR ClaveInterna = '{palabra}' OR CodigoBarras = '{palabra}')");
                    }
                   

                    var dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var id = Convert.ToInt32(dr["ID"].ToString());
                            var precio = Convert.ToDouble(dr["Precio"].ToString());
                            var codigo = dr["CodigoBarras"].ToString();
                            var nombre = dr["Nombre"].ToString();

                            if (mPrecio == 1 && !string.IsNullOrWhiteSpace(precio.ToString()))
                            {
                                nombre += $" --- ${precio.ToString("0.00")}";
                            }

                            if (mCB == 1 && !string.IsNullOrWhiteSpace(codigo))
                            {
                                nombre += $" --- CB: {codigo}";
                            }

                            if (coincidencias.ContainsKey(id))
                            {
                                coincidencias[id] = Tuple.Create(coincidencias[id].Item1 + 1, nombre);
                            }
                            else
                            {
                                coincidencias.Add(id, new Tuple<int, string>(1, nombre));
                            }
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }
            
            if (coincidencias.Count > 0)
            {
                var listaCoincidencias = from entry in coincidencias orderby entry.Value.Item1 descending select entry;

                foreach (var producto in listaCoincidencias)
                {
                    lista.Add(producto.Key, producto.Value.Item2);
                }
            }

            return lista;
        }

        public Dictionary<int, string> BusquedaCoincidenciaExacta(string frase, string filtro, int mPrecio = 0, int mCB = 0)
        {
            var lista = new Dictionary<int, string>();

            var coincidencias = new Dictionary<int, Tuple<int, string>>();

            string[] palabras = frase.Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    if (filtro.Equals("Todos"))
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND CodigoBarras = '{palabra}'");
                    }
                    else
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Tipo = '{filtro}' AND Status = 1 AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR ClaveInterna = '{palabra}' OR CodigoBarras = '{palabra}')");
                    }


                    var dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var id = Convert.ToInt32(dr["ID"].ToString());
                            var precio = Convert.ToDouble(dr["Precio"].ToString());
                            var codigo = dr["CodigoBarras"].ToString();
                            var nombre = dr["Nombre"].ToString();

                            if (mPrecio == 1 && !string.IsNullOrWhiteSpace(precio.ToString()))
                            {
                                nombre += $" --- ${precio.ToString("0.00")}";
                            }

                            if (mCB == 1 && !string.IsNullOrWhiteSpace(codigo))
                            {
                                nombre += $" --- CB: {codigo}";
                            }

                            if (coincidencias.ContainsKey(id))
                            {
                                coincidencias[id] = Tuple.Create(coincidencias[id].Item1 + 1, nombre);
                            }
                            else
                            {
                                coincidencias.Add(id, new Tuple<int, string>(1, nombre));
                            }
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }

            if (coincidencias.Count > 0)
            {
                var listaCoincidencias = from entry in coincidencias orderby entry.Value.Item1 descending select entry;

                foreach (var producto in listaCoincidencias)
                {
                    lista.Add(producto.Key, producto.Value.Item2);
                }
            }

            return lista;
        }

        public Dictionary<int, string> BusquedaCoincidenciasInventario(string frase)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            Dictionary<int, Tuple<int, string>> coincidencias = new Dictionary<int, Tuple<int, string>>();

            string[] palabras = frase.Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR ClaveInterna LIKE '%{palabra}%' OR CodigoBarras LIKE '%{palabra}%')");

                    MySqlDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var id = Convert.ToInt32(dr["ID"].ToString());
                            var nombre = dr["Nombre"].ToString();

                            if (coincidencias.ContainsKey(id))
                            {
                                coincidencias[id] = Tuple.Create(coincidencias[id].Item1 + 1, nombre);
                            }
                            else
                            {
                                coincidencias.Add(id, new Tuple<int, string>(1, nombre));
                            }
                        }
                    }

                    dr.Close();
                    CerrarConexion();
                }
            }

            if (coincidencias.Count > 0)
            {
                var listaCoincidencias = from entry in coincidencias orderby entry.Value.Item1 descending select entry;

                foreach (var producto in listaCoincidencias)
                {
                    lista.Add(producto.Key, producto.Value.Item2);
                }
            }

            return lista;
        }

        public string UltimaFechaCorte()
        {
            string fecha = DateTime.Now.ToString();

            DatosConexion($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fecha = dr["FechaOperacion"].ToString();
            }

            dr.Close();
            CerrarConexion();

            return fecha;
        }

        public string[] BuscarCodigoInventario(string codigo, bool filtro = false)
        {
            string[] datos = new string[] { };

            int idProducto = 0;

            var consulta = string.Empty;

            if (!filtro)
            {
                consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (CodigoBarras = '{codigo}' OR ClaveInterna = '{codigo}') AND Status = 1 AND (CodigoBarras != '' OR ClaveInterna != '')";
            }
            else
            {
                consulta = codigo;
            }

            DatosConexion(consulta);

            MySqlDataReader info = sql_cmd.ExecuteReader();

            if (info.Read())
            {
                idProducto = Convert.ToInt32(info["ID"]);

                idFiltrado = idProducto;

                info.Close();
            }
            else
            {
                // Si el filtro es false por default hace esto
                if (!filtro)
                {
                    var infoProducto = BuscarCodigoBarrasExtra(codigo);

                    if (infoProducto.Length > 0)
                    {
                        foreach (var id in infoProducto)
                        {
                            DatosConexion($"SELECT * FROM Productos WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                            MySqlDataReader dr = sql_cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                idProducto = Convert.ToInt32(id);
                            }

                            dr.Close();
                            CerrarConexion();
                        }
                    }
                }
            }

            CerrarConexion();

            if (idProducto > 0)
            {
                DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                MySqlDataReader dr = sql_cmd.ExecuteReader();

                if (dr.Read())
                {
                    datos = new string[] {
                        dr["Nombre"].ToString(),
                        dr["Stock"].ToString(),
                        dr["Precio"].ToString(),
                        dr["ClaveInterna"].ToString(),
                        dr["CodigoBarras"].ToString(),
                        idProducto.ToString(),
                        dr["Tipo"].ToString(),
                        dr["StockNecesario"].ToString(),
                        dr["StockMinimo"].ToString()
                    };
                }

                dr.Close();
                CerrarConexion();
            }

            return datos;
        }

        public string[] DatosRevisionInventario()
        {
            string[] datos = new string[] { };

            var fecha = string.Empty;
            var revision = string.Empty;

            DatosConexion($"SELECT * FROM CodigoBarrasGenerado WHERE IDUsuario = {FormPrincipal.userID}", true);

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fecha = dr["FechaInventario"].ToString();
                revision = dr["NoRevision"].ToString();

                datos = new string[] { fecha, revision };
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }

        public string[] DatosProductoInventariado(int idProducto)
        {
            string[] datos = new string[] { };

            DatosConexion($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                datos = new string[]
                {
                    dr["StockFisico"].ToString(),
                    dr["StockAlmacen"].ToString(),
                    dr["Fecha"].ToString()
                };
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }

        public Dictionary<int, Tuple<string, string>> ProductosServicio(int idServPQ)
        {
            Dictionary<int, Tuple<string, string>> datos = new Dictionary<int, Tuple<string, string>>();

            DatosConexion($"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idServPQ}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var producto = dr["NombreProducto"].ToString();

                    if (!string.IsNullOrEmpty(producto))
                    {
                        var id = Convert.ToInt32(dr["IDProducto"].ToString());
                        var cantidad = dr["Cantidad"].ToString();

                        datos.Add(id, new Tuple<string, string>(producto, cantidad));
                    }
                }
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }
        
        public string MensajeInventario(int idProducto, int tipo = 0)
        {
            string mensaje = string.Empty;
            string consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = $"SELECT * FROM MensajesInventario WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {idProducto}";
            }

            if (tipo == 1)
            {
                consulta = $"SELECT * FROM MensajesInventario WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {idProducto} AND Activo = 1";
            }

            DatosConexion(consulta);

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                mensaje = dr["Mensaje"].ToString();
            }

            dr.Close();
            CerrarConexion();

            return mensaje;
        }

        public Dictionary<string, string> ProductoConsultadoVentas(int idProducto, List<string> propiedades)
        {
            Dictionary<string, string> datos = new Dictionary<string, string>();

            DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                datos.Add("Nombre", dr["Nombre"].ToString());
                datos.Add("Stock", dr["Stock"].ToString());
                datos.Add("Precio", dr["Precio"].ToString());
                datos.Add("Clave", dr["ClaveInterna"].ToString());
                datos.Add("Codigo", dr["CodigoBarras"].ToString());
                datos.Add("Tipo", dr["Tipo"].ToString());
            }

            dr.Close();
            CerrarConexion();

            // Obtener proveedor
            DatosConexion($"SELECT * FROM DetallesProducto WHERE IDProducto = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr2 = sql_cmd.ExecuteReader();

            if (dr2.Read())
            {
                datos.Add("Proveedor", dr2["Proveedor"].ToString());

                dr2.Close();
            }
            else
            {
                datos.Add("Proveedor", "N/A");
            }

            dr2.Close();
            CerrarConexion();


            // Obtener datos de las propiedades

            Dictionary<int, string> detallesID = new Dictionary<int, string>();

            if (propiedades.Count > 0)
            {
                foreach (var propiedad in propiedades)
                {
                    // Obtener ID del detalle general del producto
                    DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                    MySqlDataReader dr3 = sql_cmd.ExecuteReader();

                    if (dr3.HasRows)
                    {
                        while (dr3.Read())
                        {
                            // ID del detalle
                            var idDetalle = Convert.ToInt32(dr3["IDDetalleGral"].ToString());

                            if (!detallesID.ContainsKey(idDetalle))
                            {
                                detallesID.Add(idDetalle, propiedad);
                            }
                        }
                    }
                    else
                    {
                        datos.Add(propiedad, "N/A");
                    }

                    dr3.Close();
                    CerrarConexion();
                }
            }


            if (detallesID.Count > 0)
            {
                foreach (var detalle in detallesID)
                {
                    // Obtener la descripcion
                    DatosConexion($"SELECT * FROM DetalleGeneral WHERE ID = {detalle.Key} AND IDUsuario = {FormPrincipal.userID} AND ChckName = '{detalle.Value}'");

                    MySqlDataReader dr4 = sql_cmd.ExecuteReader();

                    if (dr4.Read())
                    {
                        var descripcion = dr4["Descripcion"].ToString();

                        if (!datos.ContainsKey(detalle.Value))
                        {
                            datos.Add(detalle.Value, descripcion);
                        }
                    }
                    else
                    {
                        if (!datos.ContainsKey(detalle.Value))
                        {
                            datos.Add(detalle.Value, "N/A");
                        }
                    }

                    dr4.Close();
                    CerrarConexion();
                }
            }

            return datos;
        }

        public double CalcularCapital()
        {
            double total = 0f;

            DatosConexion($"SELECT Stock, Precio FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var stock = float.Parse(dr["Stock"].ToString());
                    var precio = float.Parse(dr["Precio"].ToString());

                    var resultado = stock * precio;

                    total += resultado;
                }
            }

            dr.Close();
            CerrarConexion();

            return total;
        }

        public string DatosDetallesProducto(int idProducto, string propiedad)
        {
            var resultado = string.Empty;

            // Obtener ID del detalle general del producto
            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    // ID del detalle
                    var idDetalle = Convert.ToInt32(dr["IDDetalleGral"].ToString());

                    // Obtener la descripcion
                    DatosConexion($"SELECT * FROM DetalleGeneral WHERE ID = {idDetalle} AND IDUsuario = {FormPrincipal.userID} AND ChckName = '{propiedad}'");

                    MySqlDataReader dr2 = sql_cmd.ExecuteReader();

                    if (dr2.Read())
                    {
                        var descripcion = dr2["Descripcion"].ToString().Replace("_"," ");

                        resultado = descripcion;
                    }

                    dr2.Close();
                    CerrarConexion();
                }
            }

            if (string.IsNullOrEmpty(resultado))
            {
                resultado = "N/A";
            }

            dr.Close();
            CerrarConexion();

            return resultado;
        }

        public string[] DatosConfiguracion()
        {
            string[] datos = new string[] { };

            DatosConexion($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var ticketVenta = dr["TicketVenta"].ToString();
                var stockNegativo = dr["StockNegativo"].ToString();

                datos = new string[] { ticketVenta, stockNegativo };
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }

        public string ObtenerNumeroCliente()
        {
            string numeroCliente = string.Empty;

            DatosConexion($"SELECT NumeroCliente FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC LIMIT 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                numeroCliente = dr["NumeroCliente"].ToString();
            }

            dr.Close();
            CerrarConexion();

            return numeroCliente;
        }

        public int CantidadFiltroInventario(string consulta)
        {
            int cantidad = 0;

            DatosConexion(consulta);

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                cantidad = Convert.ToInt32(dr["Total"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return cantidad;
        }

        public Dictionary<int, string> ObtenerTipoClientes(int tipo = 1, bool extra = false)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            DatosConexion($"SELECT * FROM TipoClientes WHERE IDUsuario = {FormPrincipal.userID} AND Habilitar = {tipo}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (extra)
            {
                lista.Add(0, "Seleccionar...");
            }

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var opcion = string.Empty;
                    var nombre = dr["Nombre"].ToString();
                    var porcentaje = dr["DescuentoPorcentaje"].ToString();

                    if (extra)
                    {
                        opcion = $"{nombre} -- {porcentaje}%";
                    }
                    else
                    {
                        opcion = nombre;
                    }

                    lista.Add(Convert.ToInt32(dr["ID"]), opcion);
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public string[] ObtenerTipoCliente(int idTipoCliente)
        {
            var datos = new string[] { };

            DatosConexion($"SELECT * FROM TipoClientes WHERE ID = {idTipoCliente} AND IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var nombre = dr.GetValue(dr.GetOrdinal("Nombre")).ToString();
                var descuento = dr.GetValue(dr.GetOrdinal("DescuentoPorcentaje")).ToString();

                datos = new string[] { nombre, descuento };
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }

        public Dictionary<int, string> ObtenerConceptosDinamicos(int tipo = 1, string origen = "", bool reporte = false)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            DatosConexion($"SELECT * FROM ConceptosDinamicos WHERE IDUsuario = {FormPrincipal.userID} AND Origen = '{origen}' AND Status = {tipo}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();


            if (tipo == 1 && !reporte)
            {
                lista.Add(0, "Seleccionar concepto...");
            }

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));
                    var concepto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();

                    lista.Add(id, concepto);
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public Dictionary<string, string> CargarClavesUnidades()
        {
            Dictionary<string, string> clavesUnidades = new Dictionary<string, string>();

            DatosConexion("SELECT * FROM CatalogoUnidadesMedida ORDER BY LOWER(Nombre) ASC");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                clavesUnidades.Add("NoAplica", "Seleccionar unidad de medida...");

                while (dr.Read())
                {
                    var clave = dr["ClaveUnidad"].ToString();
                    var nombreAux = dr["Nombre"].ToString();
                    var nombre = $"{clave} - {nombreAux}";

                    clavesUnidades.Add(clave, nombre);
                }
            }

            dr.Close();
            CerrarConexion();

            return clavesUnidades;
        }

        public ArrayList ComprobarConfiguracion()
        {
            var config = new ArrayList();

            DatosConexion($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var correoPrecio = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoPrecioProducto")));
                var correoStock = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoStockProducto")));
                var correoStockMinimo = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoStockMinimo")));
                var correoVentaProducto = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoVentaProducto")));
                var ticketVenta = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("TicketVenta")));
                var iniciarProceso = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("IniciarProceso")));
                var mostrarPrecio = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("MostrarPrecioProducto")));
                var mostrarCB = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("MostrarCodigoProducto")));
                var porcentajeProducto = dr.GetValue(dr.GetOrdinal("PorcentajePrecio")).ToString();
                var precioMayoreo = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("PrecioMayoreo")));
                var minimoMayoreo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("MinimoMayoreo")));
                var checkNoVendidos = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("checkNoVendidos")));
                var diasNoVendidos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("diasNoVendidos")));
                var correoDineroAgregadoCaja = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoAgregarDineroCaja")));
                var correoDineroRetiradoCaja = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoRetiroDineroCaja")));
                var correoCerrarVentanaVentas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoCerrarVentanaVentas")));
                var correoRestarProductoVentas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoRestarProductoVentas")));
                var correoEliminarProductoVentas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoEliminarProductoVentas")));
                var correoEliminarUltimoProductoAgregadoVentas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoEliminarUltimoProductoAgregadoVentas")));
                var correoEliminarListaProductoVentas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoEliminarListaProductoVentas")));
                var correoCorteDeCaja = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoCorteDeCaja")));
                var correoVenta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoVenta")));
                var correoIniciar = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoIniciarSesion")));
                var correoDescuento = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoVentaDescuento")));
                var correoRespaldo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoRespaldo")));
                var correoTicketVentas = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("HabilitarTicketVentas")));
                var cerrarSesion = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CerrarSesionAuto")));
                var PermisoCorreoAnticipo = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoAnticipo")));
                var PermisoCorreoVentaClienteDescuento= Convert.ToInt32(dr.GetValue(dr.GetOrdinal("CorreoVentaClienteDescuento")));
                var Traspasos = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("traspasos")));
                var EnvioSaldoInicial = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("EnvioCorreoSaldoIncial")));
                config.Add(correoPrecio);                   // 0
                config.Add(correoStock);                    // 1
                config.Add(correoStockMinimo);              // 2
                config.Add(correoVentaProducto);            // 3
                config.Add(ticketVenta);                    // 4
                config.Add(iniciarProceso);                 // 5
                config.Add(mostrarPrecio);                  // 6
                config.Add(mostrarCB);                      // 7
                config.Add(porcentajeProducto);             // 8
                config.Add(precioMayoreo);                  // 9
                config.Add(minimoMayoreo);                  // 10
                config.Add(checkNoVendidos);                // 11
                config.Add(diasNoVendidos);                 // 12
                config.Add(correoDineroAgregadoCaja);       // 13
                config.Add(correoDineroRetiradoCaja);       // 14
                config.Add(correoCerrarVentanaVentas);      // 15
                config.Add(correoRestarProductoVentas);     // 16
                config.Add(correoEliminarProductoVentas);   // 17
                config.Add(correoEliminarUltimoProductoAgregadoVentas);     // 18
                config.Add(correoEliminarListaProductoVentas);  // 19
                config.Add(correoCorteDeCaja);              // 20
                config.Add(correoVenta); // 21
                config.Add(correoIniciar); // 22
                config.Add(correoDescuento); // 23
                config.Add(correoRespaldo); // 24
                config.Add(correoTicketVentas); //25
                config.Add(cerrarSesion);//26
                config.Add(PermisoCorreoAnticipo);//27
                config.Add(PermisoCorreoVentaClienteDescuento); ;//28
                config.Add(Traspasos); //Hehehe ahi te lo dejo goofy 29
                config.Add(EnvioSaldoInicial);//30
            }

            dr.Close();
            CerrarConexion();

            return config;
        }

        public List<int> ComprobarCorreoProducto(int idProducto)
        {
            var config = new List<int>();

            DatosConexion($"SELECT * FROM CorreosProducto WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {idProducto}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var correoPrecio = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoPrecioProducto")));
                var correoStock = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoStockProducto")));
                var correoStockMinimo = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoStockMinimo")));
                var correoVentaProducto = Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CorreoVentaProducto")));

                config.Add(correoPrecio);
                config.Add(correoStock);
                config.Add(correoStockMinimo);
                config.Add(correoVentaProducto);
            }

            dr.Close();
            CerrarConexion();

            return config;
        }

        public List<string> ConceptosAppSettings()
        {
            var lista = new List<string>();

            DatosConexion($"SELECT DISTINCT * FROM appSettings WHERE Mostrar = 1 AND IDUsuario = {FormPrincipal.userID}"); 

            var dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var concepto = dr.GetValue(dr.GetOrdinal("concepto")).ToString().Replace("_"," ");

                    lista.Add(concepto);
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public string[] RecuperarPassword(string usuario)
        {
            var datos = new string[] { };

            DatosConexion($"SELECT * FROM Usuarios WHERE Usuario = '{usuario}'");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var razon = dr.GetValue(dr.GetOrdinal("RazonSocial")).ToString();
                var email = dr.GetValue(dr.GetOrdinal("Email")).ToString();
                var password = dr.GetValue(dr.GetOrdinal("Password")).ToString();

                datos = new string[] { razon, email, password };
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }

        public bool TieneProductos()
        {
            var respuesta = false;

            DatosConexion($"SELECT COUNT(ID) AS Total FROM Productos WHERE IDUsuario = {FormPrincipal.userID}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var total = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Total")));

                if (total > 0)
                {
                    respuesta = true;
                }
            }

            dr.Close();
            CerrarConexion();

            return respuesta;
        }

        public List<int> ProductosActivos()
        {
            var lista = new List<int>();

            DatosConexion($"SELECT ID FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P'");

            var dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var id = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("ID")));

                    lista.Add(id);
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public DateTime ObtenerFechaProductoRegistro(int idProducto)
        {
            DateTime fecha = new DateTime();

            DatosConexion($"SELECT FechaLarga FROM HistorialCompras WHERE IDProducto = {idProducto} AND IDUsuario = {FormPrincipal.userID} AND (TipoAjuste = 0 OR TIpoAjuste = 1) LIMIT 1");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaLarga")));
            }

            dr.Close();
            CerrarConexion();

            return fecha;
        }

        public List<KeyValuePair<int, int>> ObtenerIDVentas(int idProducto)
        {
            var lista = new List<KeyValuePair<int, int>>();

            DatosConexion($"SELECT IDVenta, Cantidad FROM ProductosVenta WHERE IDProducto = {idProducto}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var venta = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDVenta")));
                    var cantidad = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("Cantidad")));

                    if (venta > 0)
                    {
                        lista.Add(new KeyValuePair<int, int>(venta, cantidad));
                    }
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public Dictionary<int, string> ProductosGuardados(int idVenta)
        {
            var datos = new Dictionary<int, string>();

            DatosConexion($"SELECT * FROM ProductosVenta WHERE IDVenta = {idVenta}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                    var descuento = dr.GetValue(dr.GetOrdinal("descuento")).ToString();

                    datos.Add(idProducto, descuento);
                }
            }

            dr.Close();
            CerrarConexion();

            return datos;
        }

        public DateTime ObtenerFechaVentaProducto(int idVenta)
        {
            DateTime fecha = new DateTime();

            DatosConexion($"SELECT FechaOperacion FROM Ventas WHERE ID = {idVenta} AND IDUsuario = {FormPrincipal.userID}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fecha = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion")));
            }

            dr.Close();
            CerrarConexion();

            return fecha;
        }

        public float ObtenerImpuestoProducto(int idProducto)
        {
            var impuesto = 0f;

            DatosConexion($"SELECT * FROM DetallesFacturacionProductos WHERE Impuesto = 'IEPS' AND IDProducto = {idProducto}");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                impuesto = float.Parse(dr.GetValue(dr.GetOrdinal("Importe")).ToString());
            }

            dr.Close();
            CerrarConexion();

            return impuesto;
        }

        public string[] obtener_permisos_empleado(int id_empleado, int id_usuario)
        {
            List<string> list = new List<string>();

            DatosConexion($"SELECT * FROM Empleados WHERE ID={id_empleado} AND IDUsuario = {id_usuario}");
            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                list.Add(dr[6].ToString()); // anticipo
                list.Add(dr[7].ToString()); // caja
                list.Add(dr[8].ToString());
                list.Add(dr[9].ToString());
                list.Add(dr[10].ToString()); // empleado
                list.Add(dr[11].ToString());
                list.Add(dr[12].ToString()); // factura
                list.Add(dr[13].ToString());
                list.Add(dr[14].ToString());
                list.Add(dr[15].ToString()); // producto
                list.Add(dr[16].ToString());
                list.Add(dr[17].ToString()); // reporte
                list.Add(dr[18].ToString());
                list.Add(dr[19].ToString()); // venta
                list.Add(dr[20].ToString());// Bascula
                list.Add(dr[2].ToString()); // Nombre
                list.Add(dr[3].ToString()); // Usuario
                list.Add(dr[4].ToString()); // Contraseña
                list.Add(dr[21].ToString());//Consulta Producto

            }

            dr.Close();
            CerrarConexion();
             
            return list.ToArray();
        }

        #region Operaciones Datos Dinamicos
        public string[] ObtenerDatosFiltro(string chkConcepto, int idUsuario, string username)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM FiltroProducto WHERE concepto = '{chkConcepto}' AND IDUsuario = '{idUsuario}' AND Username = '{username}'");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());
                lista.Add(dr[1].ToString());
                lista.Add(dr[2].ToString());
                lista.Add(dr[3].ToString());
                lista.Add(dr[4].ToString());
                lista.Add(dr[5].ToString());
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }
        #endregion

        public int[] ObtenerPermisosEmpleado(int idEmpleado, string seccion)
        {
            var lista = new List<int>();

            DatosConexion($"SELECT * FROM EmpleadosPermisos WHERE IDEmpleado = {idEmpleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{seccion}'");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion1"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion2"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion3"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion4"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion5"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion6"))));//5
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion7"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion8"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion9"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion10"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion11"))));//10
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion12"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion13"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion14"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion15"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion16"))));//15
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion17"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion18"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion19"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion20"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion21"))));//20
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion22"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion23"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Opcion24"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("mensajeVentas"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("mensajeInventario"))));//25
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("stock"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("stockMinimo"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("stockMaximo"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("precio"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("numeroRevision"))));//30
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("tipoIVA"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("claveProducto"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("claveUnidad"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("correos"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("editarTicket"))));//35
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("EnvioCorreo"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("confiGeneral"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("porcentajeGanancia"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("tipoMoneda"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("RespaldarInfo"))));//40
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("MensajeVentas"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("MensajeInventario"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("PermisoVentaClienteDescuento"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("PermisoVentaClienteDescuentoSinAutorizacion"))));
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Agregar_Descuento"))));//45
                lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("Eliminar_Descuento"))));

                using (DataTable dtPermisosDinamicos = cn.CargarDatos(cs.VerificarContenidoDinamico(FormPrincipal.userID)))
                {

                    foreach (DataRow drConcepto in dtPermisosDinamicos.Rows)
                    {
                        var concepto = drConcepto["concepto"].ToString();

                        var normalizacionCadena = Regex.Replace(concepto, @"[^a-zA-Z0-9]+", "");
                        //var normalizacionCadena = mg.quitarTildesYÑ(concepto);

                        if (ExisteColumna(normalizacionCadena))
                        {
                            lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal(normalizacionCadena))));
                        }
                        else if (ExisteColumna(concepto))
                        {
                            lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal(concepto))));
                        }
                    }
                }
            }
            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public bool ExisteColumna(string columna)
        {
            bool respuesta = false;

            DatosConexion($"SHOW COLUMNS FROM EmpleadosPermisos LIKE '{columna}'");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                respuesta = true;
            }

            dr.Close();
            CerrarConexion();

            return respuesta;
        }
         
        public int[] PermisosEmpleadoConfiguracion(string concepto, int idEmpleado)
        {
            var lista = new List<int>();

            if (FormPrincipal.id_empleado.Equals(0))
            {
                DatosConexion($"SELECT {concepto} FROM permisosconfiguracion WHERE IDUsuario = {FormPrincipal.userID}");
            }
            else
            {
                DatosConexion($"SELECT {concepto} FROM permisosconfiguracion WHERE IDEmpleado = {idEmpleado} AND IDUsuario = {FormPrincipal.userID}");

            }
            

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var datosLista = concepto.Split(',');
                foreach (var item in datosLista)
                {
                    lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal(item.Trim()))));
                }
                //lista.Add(Convert.ToInt16(dr.GetValue(dr.GetOrdinal("CodigoBarrasTicketVenta"))));
            }
            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }
        public int obtener_id_empleado(int id_venta)
        {
            int id_empleado = 0;

            DatosConexion($"SELECT IDEmpleado FROM Ventas WHERE ID={id_venta}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                id_empleado = Convert.ToInt32(dr["IDEmpleado"].ToString());
            }

            dr.Close();
            CerrarConexion();

            return id_empleado;
        }

        public bool tiene_productos_habilitados()
        {
            var r = false;

            DatosConexion($"SELECT COUNT(ID) AS cantidad FROM Productos WHERE IDUsuario='{FormPrincipal.userID}' AND Status='1'");

            var dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var p_habilitados = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("cantidad")));

                if (p_habilitados > 0)
                {
                    r = true;
                }
            }

            dr.Close();
            CerrarConexion();

            return r;
        }

        public int obtener_cantidad_timbres()
        {
            int timbres = 0;

            DatosConexion($"SELECT timbres FROM Usuarios WHERE ID={FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                timbres = Convert.ToInt32(dr["timbres"].ToString());
            }

            dr.Close();
            CerrarConexion();
            //timbres = 3;
            return timbres;
        }

        public int TieneDetallesProducto(int idProducto)
        {
            int resultado = 0;

            DatosConexion($"SELECT COUNT(*) AS total FROM DetallesProductoGenerales AS DE INNER JOIN DetalleGeneral AS GE ON DE.IDDetalleGral = GE.ID WHERE DE.IDProducto = {idProducto} and DE.IDUsuario = {FormPrincipal.userID}");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                resultado = Convert.ToInt32(dr["total"]);
            }

            dr.Close();
            CerrarConexion();

            return resultado;
        }

        public string[] ObtenerDatosProductoPaqueteServicio(int IdProd, int IdUser)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Productos WHERE ID = {IdProd} AND IDUsuario = {IdUser} AND Status = 1");

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ID"].ToString());         // 0
                lista.Add(dr["Nombre"].ToString());     // 1
                lista.Add(dr["Categoria"].ToString());  // 2
                lista.Add(dr["Status"].ToString());     // 3
                lista.Add(dr["Tipo"].ToString());       // 4
            }

            dr.Close();
            CerrarConexion();

            return lista.ToArray();
        }

        public string[] ObtenerFechaComprobacionInternet(string usuario, string password = "")
        {
            List<string> datosFecha = new List<string>();

            string consulta = $"SELECT * FROM usuarios WHERE Usuario = '{usuario}'";

            if (!string.IsNullOrWhiteSpace(password))
            {
                consulta = $"SELECT * FROM usuarios WHERE Usuario = '{usuario}' AND Password = '{password}'";
            }

            DatosConexion(consulta);

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                datosFecha.Add(dr.GetValue(dr.GetOrdinal("FechaConexionInternet")).ToString());
                datosFecha.Add(dr.GetValue(dr.GetOrdinal("FechaConexionLimite")).ToString());
                datosFecha.Add(dr.GetValue(dr.GetOrdinal("DiasVerificacionInternet")).ToString());
                datosFecha.Add(dr.GetValue(dr.GetOrdinal("UltimaVerificacion")).ToString());
                datosFecha.Add(dr.GetValue(dr.GetOrdinal("Licencia")).ToString());
            }

            dr.Close();
            CerrarConexion();

            return datosFecha.ToArray();
        }

        public bool ExisteVentaDatosRepetidos(string[] datos)
        {
            bool existe = false;

            string consulta = $"SELECT * FROM Ventas WHERE IDUsuario = {datos[0]} AND Folio = '{datos[9]}' AND Total = '{datos[5]}' AND FechaOperacion = '{datos[12]}'";

            DatosConexion(consulta);

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                existe = true;
            }

            dr.Close();
            CerrarConexion();

            return existe;
        }

        public List<string> ObtenerProductosSinFiltrosDinamicos(string[] filtros)
        {
            List<string> lista = new List<string>();

            string consulta = $"SELECT P.ID AS ID FROM Productos AS P LEFT JOIN DetallesProductoGenerales AS DPG ON (P.ID = DPG.IDProducto AND P.IDUsuario = DPG.IDUsuario AND {filtros[0]}) LEFT JOIN DetalleGeneral DG ON (DPG.IDDetalleGral = DG.ID AND DPG.IDUsuario = DG.IDUsuario AND DG.Mostrar = 1) WHERE P.IDUsuario = {filtros[1]} AND P.Status = {filtros[2]} AND DG.Mostrar IS NOT NULL";

            DatosConexion(consulta);

            MySqlDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!lista.Contains(dr["ID"].ToString()))
                    {
                        lista.Add(dr["ID"].ToString());
                    }
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }

        public static string ObtenerResponsable()
        {
            string responsable = string.Empty;

            // Datos de quien realiza el cambio
            if (FormPrincipal.id_empleado > 0)
            {
                MetodosBusquedas mb = new MetodosBusquedas();
                var datosEmpleado =  mb.obtener_permisos_empleado(FormPrincipal.id_empleado, FormPrincipal.userID);

                string nombreEmpleado = datosEmpleado[15];
                string usuarioEmpleado = datosEmpleado[16];

                var infoEmpleado = usuarioEmpleado.Split('@');

                responsable = $" - EMPLEADO {nombreEmpleado} ({infoEmpleado[1]})";
            }
            else
            {
                responsable = $" - ADMIN {FormPrincipal.userNickName}";
            }

            return responsable;
        }

        private void DatosConexion(string consulta, bool ignorar = false)
        {
            Conexion(ignorar);
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();
        }

        private void CerrarConexion()
        {
            sql_con.Close();
        }

        public string correoUsuario()
        {
            var dato = string.Empty;
            var consulta = cn.CargarDatos($"SELECT Email FROM Usuarios WHERE ID = '{FormPrincipal.userID}' AND Usuario = '{FormPrincipal.userNickName}'"); 
            if (!consulta.Rows.Count.Equals(0))
            {
                dato = consulta.Rows[0]["Email"].ToString();
            }

            return dato;
        }

        public List<string> ConceptosAppSettingsBusqueda()
        {
            var lista = new List<string>();

            DatosConexion($"SELECT * FROM appSettings WHERE IDUsuario = {FormPrincipal.userID} AND Mostrar = 1 AND checkBoxConcepto = 1");

            var dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var concepto = dr.GetValue(dr.GetOrdinal("concepto")).ToString();

                    lista.Add(concepto);
                }
            }

            dr.Close();
            CerrarConexion();

            return lista;
        }
    }
}
