using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    class MetodosBusquedas
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;

        public void Conexion(bool ignorar = false)
        {
            if (ignorar == true)
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                {
                    //MessageBox.Show("Hosting: " + Properties.Settings.Default.Hosting.ToString(), "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sql_con = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
                }
                else
                {
                    //MessageBox.Show("Hosting: " + Properties.Settings.Default.Hosting.ToString(), "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
                }
            }
        }

        public string[] ObtenerDetallesVenta(int idVenta, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesVenta WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                //ID del cliente, Cliente, Credito
                lista.Add(dr[10].ToString());
                lista.Add(dr[11].ToString());
                lista.Add(dr[8].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }


        public float ObtenerTotalAbonado(int idVenta, int idUsuario)
        {
            float cantidad = 0f;

            DatosConexion($"SELECT SUM(Total) AS Total FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                cantidad = float.Parse(dr["Total"].ToString());
            }

            dr.Close();

            return cantidad;
        }

        public float[] ObtenerFormasPagoVenta(int idVenta, int idUsuario)
        {
            List<float> lista = new List<float>();

            DatosConexion($"SELECT * FROM DetallesVenta WHERE IDVenta = {idVenta} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(float.Parse(dr["Efectivo"].ToString()));
                lista.Add(float.Parse(dr["Tarjeta"].ToString()));
                lista.Add(float.Parse(dr["Vales"].ToString()));
                lista.Add(float.Parse(dr["Cheque"].ToString()));
                lista.Add(float.Parse(dr["Transferencia"].ToString()));
                lista.Add(float.Parse(dr["Credito"].ToString()));
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerDatosCliente(int idCliente, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Clientes WHERE ID = {idCliente} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[2].ToString()); //Razon social
                lista.Add(dr[4].ToString()); //RFC
                lista.Add(dr[3].ToString()); //Nombre comercial
                lista.Add(dr[5].ToString()); //Uso CFDI
                lista.Add(dr[6].ToString()); //Pais
                lista.Add(dr[7].ToString()); //Estado
                lista.Add(dr[8].ToString()); //Municipio
                lista.Add(dr[9].ToString()); //Localidad
                lista.Add(dr[10].ToString()); //Codigo postal
                lista.Add(dr[11].ToString()); //Colonia
                lista.Add(dr[12].ToString()); //Calle
                lista.Add(dr[13].ToString()); //No. exterior
                lista.Add(dr[14].ToString()); //No. interior
                //lista.Add(dr[15].ToString()); //Regimen fiscal
                lista.Add(dr[16].ToString()); //Email
                lista.Add(dr[17].ToString()); //Telefono
                lista.Add(dr[18].ToString()); //Forma de pago
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerClientes(int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Clientes WHERE IDUsuario = {idUsuario} AND Status = 1");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["RazonSocial"].ToString());
            }

            return lista.ToArray();
        }

        public string[] ObtenerDetalleGral(int idDetalleGral, int idUsr)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE ID = '{idDetalleGral}' AND IDUsuario = '{idUsr}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString()); //ID
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //ChckName
                lista.Add(dr[3].ToString()); //Descripcion
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerDatosProveedor(int idProveedor, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Proveedores WHERE ID = {idProveedor} AND IDUsuario = {idUsuario} ORDER BY Nombre ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] ObtenerDatosCategoria(int idCategoria, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Categorias WHERE ID = {idCategoria} AND IDUsuario = {idUsuario} ORDER BY Nombre ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //Nombre
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerDatosUbicacion(int idUbicacion, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Ubicaciones WHERE ID = {idUbicacion} AND IDUsuario = {idUsuario} ORDER BY Descripcion ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //Descripcion
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerDatosDetalleGral(int idDetalleGral, int idUsuario, int idProducto)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario} AND IDDetalleGral = {idDetalleGral} ORDER BY IDDetalleGral ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[1].ToString()); //IDProducto
                lista.Add(dr[2].ToString()); //IDUsuario
                lista.Add(dr[3].ToString()); //IDDetalleGral
                lista.Add(dr[4].ToString()); //StatusDetalleGral
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] obtenerUnDetalleProductoGenerales(string idProducto, string idUsuario, string panel)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = '{idProducto}' AND IDUsuario = '{idUsuario}' AND panelContenido = '{panel}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] obtenerIdDetalleGeneral(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND Descripcion = '{Descripcion}' ORDER BY Descripcion ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString()); //ID
                lista.Add(dr[1].ToString()); //IDUsuario
                lista.Add(dr[2].ToString()); //ChckName
                lista.Add(dr[3].ToString()); //Descripcion
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] obtenerIdDetallesProveedor(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Proveedores WHERE IDUsuario = '{idUsuario}' AND Nombre = '{Descripcion}' ORDER BY Nombre ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] obtenerIdDetallesCategorias(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Categorias WHERE IDUsuario = '{idUsuario}' AND Nombre = '{Descripcion}' ORDER BY Nombre ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID
                lista.Add(dr[1].ToString());  //IDUsuario
                lista.Add(dr[2].ToString());  //Nombre
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] obtenerIdDetallesUbicacion(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Ubicaciones WHERE IDUsuario = '{idUsuario}' AND Descripcion = '{Descripcion}' ORDER BY Descripcion ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr[0].ToString());  //ID
                lista.Add(dr[1].ToString());  //IDUsuario
                lista.Add(dr[2].ToString());  //Descripcion
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] DetallesProducto(int idProducto, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetallesProducto WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] DetallesProductoGralPorPanel(string namePanel, int idUsuario, int idProducto)
        {
            List<string> lista = new List<string>();

            //DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario}");
            DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDUsuario = '{idUsuario}' AND panelContenido = '{namePanel}' AND IDProducto = '{idProducto}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] DetallesProductoGral(int idUsuario, int idProdDetailGral)
        {
            List<string> lista = new List<string>();

            //DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {idUsuario}");
            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND ID = '{idProdDetailGral}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["IDUsuario"].ToString());
                lista.Add(dr["ChckName"].ToString());
                lista.Add(dr["Descripcion"].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] GetDetalleGeneral(int idUsuario, string Descripcion)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND Descripcion = '{Descripcion}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ID"].ToString());
                lista.Add(dr["IDUsuario"].ToString());
                lista.Add(dr["ChckName"].ToString());
                lista.Add(dr["Descripcion"].ToString());
            }

            dr.Close();

            return lista.ToArray();
        }
        
        public int BuscarProductoInventario(string producto)
        {
            int idProducto = 0;

            string consulta = string.Empty;

            //Busqueda por codigo de barra y/o clave
            consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND (CodigoBarras = '{producto}' OR ClaveInterna = '{producto}')";

            DatosConexion(consulta);

            SQLiteDataReader datos = sql_cmd.ExecuteReader();

            if (datos.Read())
            {
                idProducto = Convert.ToInt32(datos["ID"].ToString());
            }

            datos.Close();

            return idProducto;
        }

        public string[] ObtenerAnticipo(int idAnticipo, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Anticipos WHERE ID = {idAnticipo} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] ObtenerCodigoBarrasExtras(int idProducto)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = {idProducto}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                //lista.Add(dr["IDCodBarrExt"].ToString());
                lista.Add(dr["CodigoBarraExtra"].ToString());
                //lista.Add(dr["IDProducto"].ToString());
            }

            dr.Close();

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

                SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

                // Obtener todos los codigos de la tabla de codigos de barra extra
                DatosConexion($"SELECT CB.CodigoBarraExtra FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {idUsuario} AND CB.IDProducto = {idProductoTMP}");

                SQLiteDataReader info = sql_cmd.ExecuteReader();

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

                SQLiteDataReader dr = sql_cmd.ExecuteReader();

                if (dr.Read())
                {
                    respuesta = true;
                }
                else
                {
                    //DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigoClave}'");
                    DatosConexion($"SELECT CB.IDProducto FROM CodigoBarrasExtras CB INNER JOIN Productos P ON P.ID = CB.IDProducto WHERE P.IDUsuario = {idUsuario} AND CB.CodigoBarraExtra = '{codigoClave}'");

                    SQLiteDataReader info = sql_cmd.ExecuteReader();

                    if (info.HasRows)
                    {
                        //Comprobar el ID del producto y el ID del Usuario
                        while (info.Read())
                        {
                            var idProducto = Convert.ToInt32(info["IDProducto"].ToString());

                            DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {idUsuario} AND Status = 1");

                            SQLiteDataReader info2 = sql_cmd.ExecuteReader();

                            if (info2.Read())
                            {
                                respuesta = true;
                            }

                            info2.Close();
                        }
                    }
                    else
                    {
                        respuesta = false;
                    }

                    info.Close();
                }

                dr.Close();
            }
            
            return respuesta;
        }

        public string[] ObtenerClaveCodigosProducto(int idProducto, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["ClaveInterna"].ToString());
                lista.Add(dr["CodigoBarras"].ToString());

                DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE IDProducto = {idProducto}");

                SQLiteDataReader info = sql_cmd.ExecuteReader();

                while (info.Read())
                {
                    lista.Add(info["CodigoBarraExtra"].ToString());
                }

                info.Close();
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerCategorias(int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Categorias WHERE IDUsuario = {idUsuario} ORDER BY Nombre ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["ID"] + "|" + dr["Nombre"]);
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerUbicaciones(int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM Ubicaciones WHERE IDUsuario = {idUsuario} ORDER BY Descripcion ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["ID"] + "|" + dr["Descripcion"]);
            }

            dr.Close();

            return lista.ToArray();
        }

        public string[] ObtenerDetallesGral(int idUsuario, string detalle)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = '{idUsuario}' AND ChckName = '{detalle}' ORDER BY Descripcion ASC");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                lista.Add(dr["ID"] + "|" + dr["Descripcion"]);
            }

            dr.Close();

            return lista.ToArray();
        }

        public string ObtenerMaximoFolio(int idUsuario)
        {
            var folio = string.Empty;

            DatosConexion($"SELECT MAX(Folio) AS Folio FROM Ventas WHERE IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                folio = dr["Folio"].ToString();

                if (string.IsNullOrWhiteSpace(folio))
                {
                    folio = "1";
                }
            }

            dr.Close();

            return folio;
        }

        public string[] ObtenerRevisionInventario(int idRevision, int idUsuario)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM RevisarInventario WHERE ID = {idRevision} AND IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["IDAlmacen"].ToString());
            }

            dr.Close();

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

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return lista.ToArray();
        }

        public string[] BuscarCodigoBarrasExtra(string codigo)
        {
            List<string> lista = new List<string>();

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigo}'");

                SQLiteDataReader dr = sql_cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lista.Add(dr["IDProducto"].ToString());
                    }
                }

                dr.Close();
            }

            return lista.ToArray();
        }

        public string ObtenerCBGenerado(int idUsuario)
        {
            string codigo = string.Empty;

            DatosConexion($"SELECT * FROM CodigoBarrasGenerado WHERE IDUsuario = {idUsuario}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                codigo = dr["CodigoBarras"].ToString();
            }

            dr.Close();

            return codigo;
        }


        public float SaldoInicialCaja(int idUsuario)
        {
            float saldo = 0f;

            DatosConexion($"SELECT ID FROM Caja WHERE IDUsuario = {idUsuario} AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                var idCaja = Convert.ToInt32(dr["ID"].ToString());

                DatosConexion($"SELECT * FROM Caja WHERE IDUsuario = {idUsuario} AND Operacion = 'venta' AND ID > {idCaja} ORDER BY ID LIMIT 1");

                SQLiteDataReader info = sql_cmd.ExecuteReader();

                if (info.Read())
                {
                    saldo += float.Parse(info["Efectivo"].ToString());
                    saldo += float.Parse(info["Tarjeta"].ToString());
                    saldo += float.Parse(info["Vales"].ToString());
                    saldo += float.Parse(info["Cheque"].ToString());
                    saldo += float.Parse(info["Transferencia"].ToString());
                    saldo += float.Parse(info["Credito"].ToString());
                }

                info.Close();
            }

            dr.Close();

            return saldo;
        }

        public Dictionary<int, string> ObtenerOpcionesPropiedad(int idUsuario, string propiedad)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            DatosConexion($"SELECT * FROM DetalleGeneral WHERE IDUsuario = {idUsuario} AND ChckName = '{propiedad}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lista.Add(Convert.ToInt32(dr["ID"]), dr["Descripcion"].ToString());
                }
            }

            return lista;
        }

        public Dictionary<int, int> BusquedaCoincidencias(string frase)
        {
            Dictionary<int, int> coincidencias = new Dictionary<int, int>();

            string[] palabras = frase.Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%' OR CodigoBarras LIKE '%{palabra}%' OR ClaveInterna LIKE '%{palabra}%')");

                    SQLiteDataReader dr = sql_cmd.ExecuteReader();

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
                }
            }

            return coincidencias;
        }

        public Dictionary<int, string> BusquedaCoincidenciasVentas(string frase)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            Dictionary<int, Tuple<int, string>> coincidencias = new Dictionary<int, Tuple<int, string>>();

            string[] palabras = frase.Split(' ');

            if (palabras.Length > 0)
            {
                foreach (var palabra in palabras)
                {
                    DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND (Nombre LIKE '%{palabra}%' OR NombreAlterno1 LIKE '%{palabra}%' OR NombreAlterno2 LIKE '%{palabra}%')");

                    SQLiteDataReader dr = sql_cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            var id = Convert.ToInt32(dr["ID"].ToString());
                            var precio = Convert.ToDouble(dr["Precio"].ToString());
                            var nombre = dr["Nombre"].ToString() + " --- $" + precio.ToString("0.00");

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

                    SQLiteDataReader dr = sql_cmd.ExecuteReader();

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
            string fecha = DateTime.MinValue.ToString();

            DatosConexion($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fecha = dr["FechaOperacion"].ToString();
            }

            dr.Close();

            return fecha;
        }

        public string[] BuscarCodigoInventario(string codigo)
        {
            string[] datos = new string[] { };

            int idProducto = 0;

            DatosConexion($"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND (CodigoBarras = '{codigo}' OR ClaveInterna = '{codigo}') AND Status = 1");

            SQLiteDataReader info = sql_cmd.ExecuteReader();

            if (info.Read())
            {
                idProducto = Convert.ToInt32(info["ID"]);

                info.Close();
            }
            else
            {
                var infoProducto = BuscarCodigoBarrasExtra(codigo);

                if (infoProducto.Length > 0)
                {
                    foreach (var id in infoProducto)
                    {
                        DatosConexion($"SELECT * FROM Productos WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                        SQLiteDataReader dr = sql_cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            idProducto = Convert.ToInt32(id);
                        }

                        dr.Close();
                    }
                }
            }


            if (idProducto > 0)
            {
                DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                SQLiteDataReader dr = sql_cmd.ExecuteReader();

                if (dr.Read())
                {
                    datos = new string[] {
                        dr["Nombre"].ToString(),
                        dr["Stock"].ToString(),
                        dr["Precio"].ToString(),
                        dr["ClaveInterna"].ToString(),
                        dr["CodigoBarras"].ToString(),
                        idProducto.ToString(),
                        dr["Tipo"].ToString()
                    };
                }

                dr.Close();
            }

            return datos;
        }

        public string[] DatosRevisionInventario()
        {
            string[] datos = new string[] { };

            var fecha = string.Empty;
            var revision = string.Empty;

            DatosConexion($"SELECT * FROM CodigoBarrasGenerado WHERE IDUsuario = {FormPrincipal.userID}", true);

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                fecha = dr["FechaInventario"].ToString();
                revision = dr["NoRevision"].ToString();

                datos = new string[] { fecha, revision };
            }

            dr.Close();

            return datos;
        }

        public string[] DatosProductoInventariado(int idProducto)
        {
            string[] datos = new string[] { };

            DatosConexion($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return datos;
        }

        public Dictionary<int, Tuple<string, string>> ProductosServicio(int idServPQ)
        {
            Dictionary<int, Tuple<string, string>> datos = new Dictionary<int, Tuple<string, string>>();

            DatosConexion($"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idServPQ}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                mensaje = dr["Mensaje"].ToString();
            }

            dr.Close();

            return mensaje;
        }

        public Dictionary<string, string> ProductoConsultadoVentas(int idProducto, List<string> propiedades)
        {
            Dictionary<string, string> datos = new Dictionary<string, string>();

            DatosConexion($"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                datos.Add("Nombre", dr["Nombre"].ToString());
                datos.Add("Stock", dr["Stock"].ToString());
                datos.Add("Precio", dr["Precio"].ToString());
                datos.Add("Clave", dr["ClaveInterna"].ToString());
                datos.Add("Codigo", dr["CodigoBarras"].ToString());
                datos.Add("Tipo", dr["Tipo"].ToString());

                // Obtener proveedor
                DatosConexion($"SELECT * FROM DetallesProducto WHERE IDProducto = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                SQLiteDataReader dr2 = sql_cmd.ExecuteReader();

                if (dr2.Read())
                {
                    datos.Add("Proveedor", dr2["Proveedor"].ToString());

                    dr2.Close();
                }
                else
                {
                    datos.Add("Proveedor", "N/A");
                }

                // Obtener datos de las propiedades
                if (propiedades.Count > 0)
                {
                    foreach (var propiedad in propiedades)
                    {
                        // Obtener ID del detalle general del producto
                        DatosConexion($"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        SQLiteDataReader dr3 = sql_cmd.ExecuteReader();

                        if (dr3.HasRows)
                        {
                            while (dr3.Read())
                            {
                                // ID del detalle
                                var idDetalle = Convert.ToInt32(dr3["IDDetalleGral"].ToString());

                                // Obtener la descripcion
                                DatosConexion($"SELECT * FROM DetalleGeneral WHERE ID = {idDetalle} AND IDUsuario = {FormPrincipal.userID} AND ChckName = '{propiedad}'");

                                SQLiteDataReader dr4 = sql_cmd.ExecuteReader();

                                if (dr4.Read())
                                {
                                    var descripcion = dr4["Descripcion"].ToString();

                                    datos.Add(propiedad, descripcion);
                                }
                                else
                                {
                                    datos.Add(propiedad, "N/A");
                                }

                                dr4.Close();
                            }
                        }
                        else
                        {
                            datos.Add(propiedad, "N/A");
                        }

                        dr3.Close();
                    }
                }

                dr.Close();
            }

            return datos;
        }

        public float CalcularCapital()
        {
            float total = 0f;

            DatosConexion($"SELECT Stock, Precio FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

            return total;
        }

        private void DatosConexion(string consulta, bool ignorar = false)
        {
            Conexion(ignorar);
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();
        }

        public string[] obtener_permisos_empleado(int id_empleado, int id_usuario)
        {
            List<string> list = new List<string>();

            DatosConexion($"SELECT * FROM Empleados WHERE ID={id_empleado} AND IDUsuario = {id_usuario}");
            SQLiteDataReader dr = sql_cmd.ExecuteReader();

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
            }

            dr.Close();

            return list.ToArray();
        }
    }
}
