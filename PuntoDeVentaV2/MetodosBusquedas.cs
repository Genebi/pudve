﻿using System;
using System.Collections.Generic;
using System.Data;
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
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
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
        
        public string[] BuscarProductoInventario(string producto, int idUsuario, int tipo = 1)
        {
            List<string> lista = new List<string>();

            string consulta = string.Empty;

            //Busqueda por codigo de barra y/o clave
            if (tipo == 1)
            {
                consulta = $"SELECT ID FROM Productos WHERE IDUsuario = {idUsuario} AND (CodigoBarras = '{producto}' OR ClaveInterna = '{producto}') AND Status = '1'";
                consulta += $" UNION SELECT IDProducto AS ID FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{producto}'";

                DatosConexion(consulta);

                SQLiteDataReader datos = sql_cmd.ExecuteReader();

                if (datos.Read())
                {
                    var idProducto = Convert.ToInt32(datos["ID"].ToString());

                    consulta = $"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {idUsuario} AND Status = '1'";

                    DatosConexion(consulta);

                    SQLiteDataReader info = sql_cmd.ExecuteReader();

                    if (info.Read())
                    {
                        lista.Add(idProducto.ToString()); //ID
                        lista.Add(info[1].ToString()); //Nombre
                        lista.Add(info[2].ToString()); //Stock
                        lista.Add(info[3].ToString()); //Precio
                        lista.Add(info[4].ToString()); //Categoria
                        lista.Add(info[5].ToString()); //Clave interna
                        lista.Add(info[6].ToString()); //Codigo barras
                    }

                    datos.Close();
                    info.Close();
                }
            }

            //Busqueda por nombre
            if (tipo == 2)
            {
                consulta = $"SELECT * FROM Productos WHERE IDUsuario = {idUsuario} AND Nombre LIKE '%{producto}%' AND Status = '1'";

                DatosConexion(consulta);

                SQLiteDataReader datos = sql_cmd.ExecuteReader();

                while (datos.Read())
                {
                    lista.Add(datos[0] + " - " + datos[1]);
                }
            }

            return lista.ToArray();
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

        public bool ComprobarCodigoClave(string codigoClave, int idUsuario)
        {
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
                    DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigoClave}'");

                    SQLiteDataReader info = sql_cmd.ExecuteReader();

                    if (info.Read())
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

        public string[] BuscarCodigoBarrasExtra(string codigo)
        {
            List<string> lista = new List<string>();

            DatosConexion($"SELECT * FROM CodigoBarrasExtras WHERE CodigoBarraExtra = '{codigo}'");

            SQLiteDataReader dr = sql_cmd.ExecuteReader();

            if (dr.Read())
            {
                lista.Add(dr["IDProducto"].ToString());
            }

            dr.Close();

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

        private void DatosConexion(string consulta)
        {
            Conexion();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = consulta;
            sql_cmd.ExecuteNonQuery();
        }
    }
}
