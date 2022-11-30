using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace PuntoDeVentaV2
{
    class Consultas
    {
        Conexion cn = new Conexion();

        public Consultas()
        {

        }

        public string Productos(int id)
        {
            return $"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna AS 'Clave Interna', P.CodigoBarras AS 'Código de Barras', P.Status AS 'Activo', P.ProdImage AS 'Path', P.Tipo FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '{id}'";
        }

        public string StatusProductos(string idUser, string status)
        {
            return $"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna AS 'Clave Interna', P.CodigoBarras AS 'Código de Barras', P.Status AS 'Activo', P.ProdImage AS 'Path', P.Tipo FROM Productos P INNER JOIN Usuarios U ON P.IDUsuario = U.ID WHERE U.ID = '{idUser}' AND P.Status = '{status}'";
        }

        public string SetUpPrecioProductos(int id, float precio, int idUsuario, int tipo = 0)
        {
            var consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = $"UPDATE Productos SET Precio = '{precio}' WHERE ID = '{id}' AND IDUsuario = {idUsuario}";
            }

            if (tipo == 1)
            {
                consulta = $"UPDATE Productos SET PrecioCompra = '{precio}' WHERE ID = '{id}' AND IDUsuario = {idUsuario}";
            }

            return consulta;
        }



        public string ActualizarStatusProducto(int status, int idProducto, int idUsuario)
        {
            return $"UPDATE Productos SET Status = {status} WHERE ID = {idProducto} AND IDUsuario = {idUsuario}";
        }

        public string GuardarProducto(string[] datos, int id)
        {
            string consulta = "INSERT INTO Productos(Nombre, Stock, Precio, Categoria, ClaveInterna, CodigoBarras, ClaveProducto, UnidadMedida, TipoDescuento, IDUsuario, ProdImage, Tipo, Base, IVA, Impuesto, NombreAlterno1, NombreAlterno2, StockNecesario, StockMinimo, PrecioCompra, PrecioMayoreo)";
            consulta += $"VALUES('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', '{datos[20]}')";

            return consulta;
        }


        public string BuscarEmpleadoCaja(int idEmpleado)
        {
            string nameEmpleado = string.Empty;

            var query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE ID = '{idEmpleado}'");

            if (!query.Rows.Count.Equals(0)) { nameEmpleado = query.Rows[0]["Nombre"].ToString(); }

            return nameEmpleado;
        }

        public string ObtenerCodigosAsociados(int id)
        {
            var result = string.Empty;
            int count = -1;

            var query = cn.CargarDatos($"SELECT P.CodigoBarras AS Codigos FROM Productos AS P	LEFT JOIN ProductosDeServicios AS PS ON P.ID = PS.IDServicio WHERE	P.IDUsuario = '{FormPrincipal.userID}'	AND PS.IDServicio = '{id}'");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow iterados in query.Rows)
                {
                    count++;
                    if (count.Equals(0))
                    {
                        result += (iterados["Codigos"].ToString());
                    }
                    else
                    {
                        result += (",\n" + iterados["Codigos"].ToString());
                    }
                }
            }

            return result;
        }

        public string GuardarNvaImagen(int idProducto, string imgProducto)
        {
            string consulta = $"UPDATE Productos SET ProdImage = '{imgProducto}' WHERE ID = '{idProducto}'";

            return consulta;
        }

        public string ListarProductosProveedor(int idUser, string NombreProveedor, int statusProducto)
        {
            // Script para INNER JOIN de Proveedores
            var consulta = $"SELECT DISTINCT Prod.* FROM Productos AS Prod INNER JOIN Usuarios AS Usr ON Prod.IDUsuario = Usr.ID INNER JOIN DetallesProducto AS DetailProd ON Prod.ID = DetailProd.IDProducto INNER JOIN Proveedores AS Prov ON Prov.ID = DetailProd.IDProveedor WHERE Prov.IDUsuario = '{idUser}' AND Prov.Nombre = '{NombreProveedor}' AND Prod.Status = '{statusProducto}' AND Prod.Tipo = 'P' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string validarEmpleado(string name, bool reportes = false)
        {
            var result = string.Empty;

            if (reportes.Equals(false))
            {
                result = FormPrincipal.userNickName;
            }

            var query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND Usuario = '{name}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["Nombre"].ToString();
            }

            return result;
        }

        public string validarEmpleadoPorID()
        {
            var result = FormPrincipal.userID.ToString();

            var query = cn.CargarDatos($"SELECT Usuario FROM Usuarios WHERE ID = '{FormPrincipal.userID}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["Usuario"].ToString();
            }

            return result;
        }

        public string NombreEmpleado(string name)
        {
            var result = string.Empty;

            var query = cn.CargarDatos($"SELECT ID FROM Empleados WHERE Nombre LIKE '%{name}%' AND IDUsuario = '{FormPrincipal.userID}' AND estatus = '1'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["ID"].ToString();
            }

            return result;
        }

        public string ListarProductosSinProveedor(int idUser, int statusProducto)
        {
            // Script para INNER JOIN de Proveedores
            var consulta = $"SELECT Prod.* FROM Productos AS Prod WHERE NOT EXISTS (SELECT * FROM DetallesProducto AS DetailProd WHERE Prod.ID = DetailProd.IDProducto) AND Prod.IDUsuario = '{idUser}' AND Prod.Tipo = 'P' AND Prod.Status = '{statusProducto}' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string CantidadListaProductosProveedor(int idUser, string NombreProveedor, int statusProducto)
        {
            // Script para INNER JOIN de Proveedores
            var consulta = $"SELECT COUNT(Prod.ID) AS Total FROM Productos AS Prod INNER JOIN Usuarios AS Usr ON Prod.IDUsuario = Usr.ID INNER JOIN DetallesProducto AS DetailProd ON Prod.ID = DetailProd.IDProducto INNER JOIN Proveedores AS Prov ON Prov.ID = DetailProd.IDProveedor WHERE Prov.IDUsuario = '{idUser}' AND Prov.Nombre = '{NombreProveedor}' AND Prod.Status = '{statusProducto}' AND Prod.Tipo = 'P' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string CantidadListaProductosSinProveedor(int idUser, int statusProducto)
        {
            // Script para INNER JOIN de Proveedores
            var consulta = $"SELECT COUNT(Prod.ID) AS Total FROM Productos AS Prod WHERE NOT EXISTS (SELECT * FROM DetallesProducto AS DetailProd WHERE Prod.ID = DetailProd.IDProducto) AND Prod.IDUsuario = '{idUser}' AND Prod.Tipo = 'P' AND Prod.Status = '{statusProducto}' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string ListarProductosConceptoDinamico(int idUser, string ConceptoDinamico, int statusProducto)
        {
            // Script para INNER JOIN de Detalle Dinamico
            var consulta = $"SELECT DISTINCT Prod.* FROM Productos AS Prod INNER JOIN Usuarios AS Usr ON Prod.IDUsuario = Usr.ID INNER JOIN DetallesProductoGenerales AS DetailGralProd ON Prod.ID = DetailGralProd.IDProducto INNER JOIN DetalleGeneral AS DetailGral ON DetailGralProd.IDDetalleGral = DetailGral.ID WHERE DetailGral.IDUsuario = '{idUser}' AND DetailGral.Descripcion = '{ConceptoDinamico}' AND Prod.Status = '{statusProducto}' AND Prod.Tipo = 'P' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string ListarProductosSinConceptoDinamico(int idUser, string ConceptoDinamico, int statusProducto)
        {
            // Script para INNER JOIN de Detalle Dinamico
            var consulta = $"SELECT Prod.* FROM Productos AS Prod WHERE NOT EXISTS (SELECT * FROM DetallesProductoGenerales AS DetailProdGral WHERE Prod.ID = DetailProdGral.IDProducto AND DetailProdGral.panelContenido = 'panelContenido{ConceptoDinamico}') AND Prod.IDUsuario = '{idUser}' AND Prod.Tipo = 'P' AND Prod.Status = '{statusProducto}' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string CantidadListarProductosConceptoDinamico(int idUser, string ConceptoDinamico, int statusProducto)
        {
            // Script para INNER JOIN de Detalle Dinamico
            var consulta = $"SELECT COUNT(Prod.ID) AS Total FROM Productos AS Prod INNER JOIN Usuarios AS Usr ON Prod.IDUsuario = Usr.ID INNER JOIN DetallesProductoGenerales AS DetailGralProd ON Prod.ID = DetailGralProd.IDProducto INNER JOIN DetalleGeneral AS DetailGral	ON DetailGralProd.IDDetalleGral = DetailGral.ID	WHERE DetailGral.IDUsuario = '{idUser}' AND DetailGral.Descripcion = '{ConceptoDinamico}'	AND Prod.Status = '{statusProducto}' AND Prod.Tipo = 'P' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string BuscadorDeReportesCaja(string datoBuscar, string primerFecha, string segundaFecha, int idNoMostrar)
        {
            var consulta = $"SELECT CJ.ID, CJ.FechaOperacion, CJ.IdEmpleado, EMP.nombre, USR.Usuario FROM Caja AS CJ LEFT JOIN Empleados AS EMP ON CJ.IdEmpleado = EMP.ID LEFT JOIN Usuarios AS USR ON USR.ID = CJ.IDUsuario WHERE CJ.IDUsuario = '{FormPrincipal.userID}' AND CJ.Operacion = 'corte' AND CJ.ID != '{idNoMostrar}' AND((USR.Usuario LIKE '%{datoBuscar}%' AND CJ.IdEmpleado = 0) OR EMP.nombre LIKE '%{datoBuscar}%') AND(CJ.FechaOperacion BETWEEN CAST('{primerFecha}' AS DATE) AND CAST('{segundaFecha}' AS DATE)) ORDER BY CJ.FechaOperacion DESC";

            return consulta;
        }



        public string BuscadorDeInventario(string datoBuscado, string primerFecha, string segundaFecha, string revisarInventrario)
        {
            var consulta = string.Empty;
            if (revisarInventrario.Equals("RInventario"))
            {
                consulta = $"SELECT NumFolio, NoRevision, NameUsr, Fecha FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NameUsr LIKE '%{datoBuscado}%' AND (Fecha BETWEEN CAST('{primerFecha}' AS DATE) AND CAST('{segundaFecha}' AS DATE)) GROUP BY NoRevision ORDER BY Fecha DESC";
            }
            else
            {
                var tablaBusqueda = string.Empty;
                if (revisarInventrario.Equals("AIAumentar"))
                {
                    tablaBusqueda = "dgvaumentarinventario";
                    consulta = $"SELECT NoRevision, NombreProducto, NameUsr, IDEmpleado, Fecha, Folio FROM {tablaBusqueda} WHERE IDUsuario = '{FormPrincipal.userID}'AND Folio != 0 AND NameUsr LIKE '%{datoBuscado}%' AND (Fecha BETWEEN CAST('{primerFecha}' AS DATE) AND CAST('{segundaFecha}' AS DATE)) GROUP BY NoRevision ORDER BY Fecha DESC";
                }
                else
                {
                    tablaBusqueda = "dgvdisminuirinventario";
                    consulta = $"SELECT NoRevision, NombreProducto, IF ( NameUse IS NULL, 'ADMINISTRADOR', NameUse ) AS 'NameUse', IDEmpleado, Fecha, Folio FROM {tablaBusqueda} WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio != 0 AND ( NameUse LIKE '%{datoBuscado}%' OR NameUse IS NULL ) AND ( Fecha BETWEEN CAST( '{primerFecha}' AS DATE ) AND CAST( '{segundaFecha}' AS DATE ) ) GROUP BY NoRevision ORDER BY Fecha DESC";
                }

                //consulta = $"SELECT NoRevision, NombreProducto, NameUse, IDEmpleado, Fecha, Folio FROM {tablaBusqueda} WHERE IDUsuario = '{FormPrincipal.userID}'AND Folio != 0 AND NameUse LIKE '%{datoBuscado}%' AND (Fecha BETWEEN CAST('{primerFecha}' AS DATE) AND CAST('{segundaFecha}' AS DATE)) GROUP BY NoRevision ORDER BY Fecha DESC";
            }


            return consulta;
        }
         
        public string CargarDatosIniciarFormReportesCaja(string primerFecha, string segundaFecha, int idNoMostrar)
        {
            var consulta = $"SELECT CJ.ID, CJ.FechaOperacion, CJ.IdEmpleado, EMP.nombre FROM Caja AS CJ LEFT JOIN Empleados AS EMP ON CJ.IdEmpleado = EMP.ID WHERE CJ.IDUsuario = '{FormPrincipal.userID}' AND CJ.Operacion = 'corte' AND CJ.ID != '{idNoMostrar}'  AND(CJ.FechaOperacion BETWEEN CAST('{primerFecha}' AS DATE) AND CAST('{segundaFecha}' AS DATE)) ORDER BY CJ.FechaOperacion DESC";

            return consulta;
        }

        public string consultarUsuarioEmpleado(int id, string usuario)
        {
            string result = string.Empty;
            string parametros = string.Empty;
            DataTable query = new DataTable();

            if (usuario.Equals("empleado"))
            {
                query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE IDUsuario = {FormPrincipal.userID} AND ID = {id}");
                parametros = "Nombre";
            }
            else if (usuario.Equals("admin"))
            {
                query = cn.CargarDatos($"SELECT Usuario FROM Usuarios WHERE ID = {FormPrincipal.userID}");
                parametros = "Usuario";
            }

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0][parametros].ToString();
            }


            return result;
        }

        public string buscarNombreCliente(string name)
        {
            string result = string.Empty;

            var query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE Usuario = '{name}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["Nombre"].ToString();
            }

            return result;
        }


        public string buscarIDEmpleado(string nombre)
        {
            var result = string.Empty;

            var query = cn.CargarDatos($"SELECT ID FROM Empleados WHERE Usuario = '{nombre}'");

            if (!query.Rows.Count.Equals(0))
            { 
                result = query.Rows[0]["ID"].ToString(); 
            }

            return result;
        }

        public string CantidadListarProductosSinConceptoDinamico(int idUser, string ConceptoDinamico, int statusProducto)
        {
            // Script para INNER JOIN de Detalle Dinamico
            var consulta = $"SELECT COUNT(Prod.ID) AS Total FROM Productos AS Prod WHERE NOT EXISTS (SELECT * FROM DetallesProductoGenerales AS DetailProdGral WHERE Prod.ID = DetailProdGral.IDProducto AND DetailProdGral.panelContenido = 'panelContenido{ConceptoDinamico}') AND Prod.IDUsuario = '{idUser}' AND Prod.Tipo = 'P' AND Prod.Status = '{statusProducto}' AND (CodigoBarras != '' OR ClaveInterna != '')";

            return consulta;
        }

        public string GuardarDetallesProducto(string[] datos, int idProducto)
        {
            string consulta = "INSERT INTO DetallesFacturacionProductos (Tipo, Impuesto, TipoFactor, TasaCuota, Definir, Importe, IDProducto)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{idProducto}')";

            return consulta;
        }

        public string GuardarDescuentoCliente(string[] datos, int idProducto)
        {
            string consulta = "INSERT INTO DescuentoCliente (PrecioProducto, PorcentajeDescuento, PrecioDescuento, Descuento, IDProducto)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{idProducto}')";

            return consulta;
        }

        public string GuardarDescuentoMayoreo(string[] datos, int idProducto)
        {
            string consulta = "INSERT INTO DescuentoMayoreo (RangoInicial, RangoFinal, Precio, Checkbox, IDProducto)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{idProducto}')";

            return consulta;
        }

        public string GuardarVenta(string[] datos, int operacion = 0, int idAnticipo = 0)
        {
            string consulta = null;

            if (operacion.Equals(0))
            {
                //Insertar nueva venta
                consulta = "INSERT INTO Ventas (IDUsuario, IDCliente, IDSucursal, Subtotal, IVA16, Total, Descuento, DescuentoGeneral, Anticipo, Folio, Serie, Status, FechaOperacion, IDClienteDescuento, IDEmpleado, FormaPago,IDAnticipo)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}','{idAnticipo}')";
            }
            else
            {
                //Actualizar venta guardada
                consulta = $"UPDATE Ventas SET IDCliente = '{datos[1]}', Subtotal = '{datos[3]}', IVA16 = '{datos[4]}', Total = '{datos[5]}', Descuento = '{datos[6]}', DescuentoGeneral = '{datos[7]}', Status = '{datos[11]}', FechaOperacion = '{datos[12]}', IDClienteDescuento = '{datos[13]}' WHERE ID = '{operacion}'";
            }

            return consulta;
        }
        public string GuardarAperturaDeCaja(string[] datos, int operacion = 0)
        {
            string consulta = null;

            if (operacion.Equals(0))
            {
                //Insertar nueva venta
                consulta = "INSERT INTO Ventas (IDUsuario, IDCliente, IDSucursal, Subtotal, IVA16, Total, Descuento, DescuentoGeneral, Anticipo, Folio, RFC, Status, FechaOperacion, Cliente, IDEmpleado, FormaPago)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}')";
            }
            else
            {
                //Actualizar venta guardada
                consulta = $"UPDATE Ventas SET IDCliente = '{datos[1]}', Subtotal = '{datos[3]}', IVA16 = '{datos[4]}', Total = '{datos[5]}', Descuento = '{datos[6]}', DescuentoGeneral = '{datos[7]}', Status = '{datos[11]}', FechaOperacion = '{datos[12]}', IDClienteDescuento = '{datos[13]}' WHERE ID = '{operacion}'";
            }

            return consulta;
        }

        public string GuardarProductosVenta(string[] datos, int opcion = 0)
        {
            // Se agrega campo descuento individual para efectos de facturación
            var consulta = string.Empty;

            if (opcion.Equals(0))
            {
                consulta = "INSERT INTO ProductosVenta (IDVenta, IDProducto, Nombre, Cantidad, Precio, descuento, TipoDescuento)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[6]}', '{datos[12]}')";
            }
            else
            {
                consulta = $"UPDATE ProductosVenta SET Nombre = '{datos[2]}', Cantidad = '{datos[3]}', Precio = '{datos[4]}', descuento = '{datos[6]}', TipoDescuento = '{datos[12]}' WHERE IDVenta = '{datos[0]}' AND IDProducto = '{datos[1]}'";
            }

            return consulta;
        }

        public string EliminarProductosVenta(int IDVenta)
        {
            string consulta = $"DELETE FROM ProductosVenta WHERE IDVenta = '{IDVenta}'";

            return consulta;
        }

        public string ActualizarStockProductos(string[] datos)
        {
            return $"UPDATE Productos SET Stock = '{datos[1]}' WHERE ID = '{datos[0]}' AND IDUsuario = {datos[2]}";
        }

        public string StatusVenta(int idVenta)
        {
            return $"SELECT Status FROM Ventas WHERE IDusuario = '{FormPrincipal.userID}' AND ID = '{idVenta}'";
        }

        public string ActualizarVenta(int IDVenta, int status, int IDUsuario)
        {
            return $"UPDATE Ventas SET Status = {status} WHERE ID = '{IDVenta}' AND IDUsuario = {IDUsuario}";
        }

        public string DevolverVentaCanceladaSiNoHayDinero(int IDVenta, int status, int IDUsuario)
        {
            return $"UPDATE Ventas SET STatus = {status} WHERE = '{IDVenta}' AND IDUsuario = {IDUsuario}";
        }

        public string GuardarAnticipo(string[] datos, int idEmpleado)
        {
            string consulta = string.Empty;

            if (!idEmpleado.Equals(0))
            {
                consulta = $"INSERT INTO Anticipos (IDUsuario, IDEmpleado ,Concepto, Importe, Cliente, FormaPago, Comentarios, Status, Fecha, ImporteOriginal)";
                consulta += $"VALUES ('{datos[0]}', '{idEmpleado}','{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[2]}')";
            }
            else
            {
                consulta = $"INSERT INTO Anticipos (IDUsuario, Concepto, Importe, Cliente, FormaPago, Comentarios, Status, Fecha, ImporteOriginal)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[2]}')";
            }

            return consulta;
        }

        public string CambiarStatusAnticipo(int status, int IDAnticipo, int IDUsuario)
        {
            return $"UPDATE Anticipos SET Importe = ImporteOriginal, Status = {status} WHERE ID = {IDAnticipo} AND IDUsuario = {IDUsuario}";
        }

        public string ProductosDeServicios(int idServ)
        {
            return $"SELECT POfSerPaq.Fecha, POfSerPaq.IDServicio, POfSerPaq.IDProducto, POfSerPaq.NombreProducto, POfSerPaq.Cantidad FROM ProductosDeServicios POfSerPaq WHERE POfSerPaq.IDServicio = '{idServ}'";
        }

        public string GuardarProductosServPaq(string[] datos)
        {
            string consulta = "INSERT INTO ProductosDeServicios (Fecha, IDServicio, IDProducto, NombreProducto, Cantidad)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}')";

            return consulta;
        }

        public string ActualizarProductosServPaq(string[] datos)
        {
            string consulta = $"UPDATE SET ProductosDeServicios Fecha = '{datos[0]}', IDProducto = '{datos[2]}', NombreProducto = '{datos[3]}', Cantidad = '{datos[4]}' WHERE IDServicio = '{datos[1]}'";

            return consulta;
        }

        public string ObtenerProductosServPaq(string idServPQ)
        {
            string consulta = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idServPQ}'";
            return consulta;
        }

        public string GuardarDetallesDelProducto(int idProducto, int idUsuario, string nombreProveedor, int idProveedor)
        {
            string consulta = "INSERT INTO DetallesProducto (IDProducto, IDUsuario, Proveedor, IDProveedor)";
            consulta += $"VALUES ('{idProducto}', '{idUsuario}', '{nombreProveedor}', '{idProveedor}')";
            return consulta;
        }

        public string GuardarProveedor(string[] datos, int tipo = 0)
        {
            string consulta = string.Empty;

            //Insertar
            if (tipo == 0)
            {
                consulta = "INSERT INTO Proveedores (IDUsuario, Nombre, RFC, Calle, NoExterior, NoInterior, Colonia, Municipio, Estado, CodigoPostal, Email, Telefono, FechaOperacion)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}')";
            }

            //Actualizar
            if (tipo == 1)
            {
                consulta = $"UPDATE Proveedores SET Nombre = '{datos[1]}', RFC = '{datos[2]}', Calle = '{datos[3]}', NoExterior = '{datos[4]}', NoInterior = '{datos[5]}', Colonia = '{datos[6]}', Municipio = '{datos[7]}', Estado = '{datos[8]}', CodigoPostal = '{datos[9]}', Email = '{datos[10]}', Telefono = '{datos[11]}', FechaOperacion = '{datos[12]}' WHERE ID = {datos[13]} AND IDUsuario = {datos[0]}";
            }

            //Deshabilitar
            if (tipo == 2)
            {
                consulta = $"UPDATE Proveedores SET Status = {datos[2]} WHERE ID = {datos[0]} AND IDUsuario = {datos[1]}";
            }

            return consulta;
        }

        public string ListarProveedores(int idUser)
        {
            var consulta = $"SELECT * FROM Proveedores WHERE IDUsuario = {idUser}";

            return consulta;
        }

        public string verificarExistenciaUusario(int idUsuario)
        {
            var consulta = $"SELECT * FROM Configuracion WHERE IDUsuario = '{idUsuario}'";
            return consulta;
        }

        public string GuardarProveedorProducto(string[] datos, int tipo = 0)
        {
            string consulta = string.Empty;

            //Insertar
            if (tipo == 0)
            {
                consulta = "INSERT INTO DetallesProducto (IDProducto, IDUsuario, Proveedor, IDProveedor)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}')";

            }

            //Actualizar
            if (tipo == 1)
            {
                consulta = $"UPDATE DetallesProducto SET Proveedor = '{datos[2]}', IDProveedor = '{datos[3]}' WHERE IDProducto = {datos[0]} AND IDUsuario = {datos[1]}";
            }

            return consulta;
        }

        public string ActualizarProveedorDetallesDelProducto(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE DetallesProducto SET IDProveedor = '{datos[1]}', Proveedor = '{datos[2]}' WHERE ID = '{datos[0]}'";
            return consulta;
        }

        public string ActualizarProvedorDetallesProd(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE detallesProducto SET IDProveedor = '{datos[1]}', Proveedor = '{datos[2]}' WHERE IDProducto = '{datos[0]}'";
            return consulta;
        }

        public string ActualizarProveedorDetallesDeCategoria(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE DetallesProducto SET IDCategoria = '{datos[1]}', Categoria = '{datos[2]}' WHERE ID = '{datos[0]}'";
            return consulta;
        }

        public string ActualizarProveedorDetallesDeUbicacion(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE DetallesProducto SET IDUbicacion = '{datos[1]}', Ubicacion = '{datos[2]}' WHERE ID = '{datos[0]}'";
            return consulta;
        }

        public string GuardarAppSettings(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $@"INSERT INTO appSettings(concepto, checkBoxConcepto, textComboBoxConcepto, checkBoxComboBoxConcepto, IDUsuario) 
                                           VALUES('{datos[1]}','{datos[0]}','{datos[3]}','{datos[2]}','{datos[4]}')";
            return consulta;
        }

        public string GuardarProveedorDetallesDelProducto(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"INSERT INTO DetallesProducto(IDProducto, IDUsuario, Proveedor, IDProveedor) VALUES('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}')";
            return consulta;
        }

        public string GuardarCategoriaDetallesDelProducto(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"INSERT INTO DetallesProducto(IDProducto, IDUsuario, Categoria, IDCategoria) VALUES('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}')";
            return consulta;
        }

        public string GuardarUbicacionDetallesDelProducto(string[] datos)
        {
            string consulta = string.Empty;
            consulta = $"INSERT INTO DetallesProducto(IDProducto, IDUsuario, Ubicacion, IDUbicacion) VALUES('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}')";
            return consulta;
        }

        public string VerificarDetallesProductoGenerales(string IDProducto, int IDUsuario, string panelContenido)
        {
            var consulta = $"SELECT * FROM DetallesProductoGenerales WHERE IDProducto = '{IDProducto}' AND IDUsuario = '{IDUsuario}' AND panelContenido = '{panelContenido}'";

            return consulta;
        }

        public string GuardarDetallesProductoGenerales(string[] datos)
        {
            string consulta = string.Empty;

            consulta = "INSERT INTO DetallesProductoGenerales (IDProducto, IDUsuario, IDDetalleGral, StatusDetalleGral, panelContenido)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}')";

            return consulta;
        }

        public string GuardarDetallesProductoGeneralesComboBox(string[] datos)
        {
            string consulta = string.Empty;

            consulta = "INSERT INTO DetallesProductoGenerales (IDProducto, IDUsuario, IDDetalleGral, StatusDetalleGral, panelContenido)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[3]}', '{datos[4]}', '{datos[2]}')";

            return consulta;
        }

        public string ActualizarDetallesProductoGenerales(string[] datos)
        {
            string consulta = string.Empty;

            consulta = $"UPDATE DetallesProductoGenerales SET IDDetalleGral = '{datos[1]}' WHERE ID = '{datos[0]}'";

            return consulta;
        }

        public string GuardarDetallesProductoGeneralesDesdeAgregarEditarProducto(string[] datos)
        {
            string consulta = string.Empty;

            consulta = $"UPDATE DetallesProductoGenerales SET IDDetalleGral = '{datos[3]}' WHERE IDProducto = '{datos[0]}' AND IDUsuario = '{datos[1]}' AND panelContenido = '{datos[2]}'";

            return consulta;
        }

        public string RenombrarDetallesProductoGenerales(string nvoConcepto, string viejoConcepto, int idUser)
        {
            var consulta = $"UPDATE DetallesProductoGenerales SET panelContenido = 'panelContenido{nvoConcepto}' WHERE IDUsuario = '{idUser}' AND panelContenido = 'panelContenido{viejoConcepto}'";

            return consulta;
        }

        public string ListarDetalleGeneral(int idUser, string chkNombre)
        {
            var consulta = $"SELECT * FROM DetalleGeneral WHERE IDUsuario = {idUser} AND ChckName = '{chkNombre}' AND Mostrar = 1";

            return consulta;
        }

        public string UpdateDetalleGeneral(string oldNameSetting, string newNameSetting)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE DetalleGeneral SET ChckName = '{newNameSetting}' WHERE ChckName = '{oldNameSetting}'";
            return consulta;
        }

        public string UpdateDetallesProductoGenerales(string oldNamePanel, string newNamePanel)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE DetallesProductoGenerales SET panelContenido = '{newNamePanel}' WHERE panelContenido = '{oldNamePanel}'";
            return consulta;
        }

        public string AgruparDetallesProductoGenerales(string panelContenido, string IdProducto)
        {
            var consulta = $"SELECT * FROM DetallesProductoGenerales WHERE panelContenido IN( SELECT panelContenido FROM DetallesProductoGenerales WHERE panelContenido = '{panelContenido}' GROUP BY panelContenido HAVING COUNT(*) > 1) AND IDUsuario = '{FormPrincipal.userID}' AND IDProducto = '{IdProducto}' ORDER BY panelContenido, ID ASC";

            return consulta;
        }

        public string BorrarDetallesProductoGenerales(string IdDetalle)
        {
            var consulta = $"DELETE FROM DetallesProductoGenerales WHERE ID = '{IdDetalle}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string BorrarDetallesProductoGeneralesPorConcepto(string panelContenido, string IdProducto)
        {
            var consulta = $"DELETE FROM DetallesProductoGenerales WHERE panelContenido = '{panelContenido}' AND IDProducto = '{IdProducto}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string VerificarDetallesProducto(string idProducto, string idUsuario)
        {
            string consulta = string.Empty;
            consulta = $"SELECT * FROM DetallesProducto WHERE IDProducto = '{idProducto}' AND IDUsuario = '{idUsuario}'";
            return consulta;
        }

        public string GuardarProductMessage(string idProducto, string ProductMessage, string ProductMessageActivated)
        {
            string consulta = string.Empty;
            consulta = "INSERT INTO ProductMessage (IDProducto, ProductOfMessage, ProductMessageActivated)";
            consulta += $"VALUES ('{idProducto}', '{ProductMessage}', '{ProductMessageActivated}')";
            return consulta;
        }

        public string ObtenerAllProductMessage(string idProducto)
        {
            string consulta = string.Empty;
            consulta = $"SELECT * FROM ProductMessage WHERE IDProducto = '{idProducto}'";
            return consulta;
        }

        public string ObtenerProductMessage(string idProducto)
        {
            string consulta = string.Empty;
            consulta = $"SELECT * FROM ProductMessage WHERE IDProducto = '{idProducto}' AND ProductMessageActivated = 1";
            return consulta;
        }

        public string UpdateProductMessage(string Messg, string idMessg)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE ProductMessage SET ProductOfMessage = '{Messg}', ProductMessageActivated = 1 WHERE ID = '{idMessg}'";
            return consulta;
        }

        public string DesactivarProductMessage(string idProdMessg)
        {
            string consulta = string.Empty;
            consulta = $"UPDATE ProductMessage SET ProductMessageActivated = 0 WHERE ID = '{idProdMessg}'";
            return consulta;
        }

        public string AjustarProducto(string[] datos, int tipo)
        {
            string consulta = string.Empty;

            //Producto comprado
            if (tipo == 1)
            {
                consulta = "INSERT INTO HistorialCompras (Concepto, Cantidad, ValorUnitario, Precio, FechaLarga, RFCEmisor, NomEmisor, Comentarios, TipoAjuste, FechaOperacion, IDReporte, IDProducto, IDUsuario)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}')";
            }

            //Ajustar producto
            if (tipo == 2)
            {
                consulta = "INSERT INTO HistorialCompras (Concepto, Cantidad, Precio, Comentarios, TipoAjuste, FechaOperacion, IDProducto, IDUsuario, Folio, RFCEmisor, NomEmisor, FechaLarga, ConceptoExtra)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}')";
            }

            return consulta;
        }

        public string OperacionCaja(string[] datos, bool corte = false)
        {
            var consulta = string.Empty;
            if (corte.Equals(true))
            {
                consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, IdEmpleado, NumFolio, CantidadRetiradaCorte)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}')";
            }
            else
            {
                consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, IdEmpleado)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}')";
            }


            return consulta;
        }

        public string OperacionDevoluciones(string[] datos)
        {
            string consulta = "INSERT INTO Devoluciones (IDVenta, IDUsuario, Total, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Referencia, FechaOperacion)";
            consulta += $"VALUES('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}')";

            return consulta;
        }

        public string GuardarCliente(string[] datos, int tipo = 0)
        {
            //Este metodo sirve para insertar cliente, actualizar y deshabilitar al cliente
            string consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = "INSERT INTO Clientes (IDUsuario, RazonSocial, NombreComercial, RFC, UsoCFDI, Pais, Estado, Municipio, Localidad, CodigoPostal, Colonia, Calle, NoExterior, NoInterior, RegimenFiscal, Email, Telefono, FormaPago, FechaOperacion, TipoCliente, NumeroCliente)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[20]}', '{datos[21]}')";
            }

            if (tipo == 1)
            {
                //consulta = $@"UPDATE Clientes SET RazonSocial = '{datos[1]}', NombreComercial = '{datos[2]}', RFC = '{datos[3]}', UsoCFDI = '{datos[4]}', Pais = '{datos[5]}', Estado = '{datos[6]}', Municipio = '{datos[7]}', Localidad = '{datos[8]}', 
                //CodigoPostal = '{datos[9]}', Colonia = '{datos[10]}', Calle = '{datos[11]}', NoExterior = '{datos[12]}', NoInterior = '{datos[13]}', RegimenFiscal = '{datos[14]}', Email = '{datos[15]}', Telefono = '{datos[16]}',
                //FormaPago = '{datos[17]}', FechaOperacion = '{datos[18]}', TipoCliente = '{datos[20]}' WHERE IDUsuario = {datos[0]} AND RFC = '{datos[3]}' AND ID = '{datos[19]}'";
                consulta = $@"UPDATE Clientes SET RazonSocial = '{datos[1]}', NombreComercial = '{datos[2]}', RFC = '{datos[3]}', UsoCFDI = '{datos[4]}', Pais = '{datos[5]}', Estado = '{datos[6]}', Municipio = '{datos[7]}', Localidad = '{datos[8]}', 
                            CodigoPostal = '{datos[9]}', Colonia = '{datos[10]}', Calle = '{datos[11]}', NoExterior = '{datos[12]}', NoInterior = '{datos[13]}', RegimenFiscal = '{datos[14]}', Email = '{datos[15]}', Telefono = '{datos[16]}',
                            FormaPago = '{datos[17]}', FechaOperacion = '{datos[18]}', TipoCliente = '{datos[20]}' WHERE IDUsuario = {datos[0]} AND ID = '{datos[19]}'";
            }

            if (tipo == 2)
            {
                consulta = $"UPDATE Clientes SET Status = {datos[2]} WHERE ID = {datos[0]} AND IDUsuario = {datos[1]}";
            }

            return consulta;
        }

        public string GuardarDetallesVenta(string[] datos, int tipo = 0)
        {
            string consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = "INSERT INTO DetallesVenta (IDVenta, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Referencia, IDCliente, Cliente)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}')";
            }

            if (tipo == 1)
            {
                consulta = $"UPDATE DetallesVenta SET IDCliente = {datos[0]}, Cliente = '{datos[1]}' WHERE IDVenta = {datos[2]} AND IDUsuario = {datos[3]}";
            }

            return consulta;
        }

        public string GuardarAbonos(string[] datos)
        {
            string consulta = "INSERT INTO Abonos (IDVenta, IDUsuario, Total, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Referencia, FechaOperacion)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}')";
            return consulta;
        }

        public string GuardarAbonosEmpleados(string[] datos)
        {
            string consulta = $"INSERT INTO Abonos (IDVenta, IDUsuario, Total, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Referencia, FechaOperacion, IDEmpleado) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}')";

            return consulta;
        }

        public string CargarHistorialDeCompras(string idProducto)
        {
            string consulta = $"SELECT * FROM HistorialCompras WHERE IDProducto = '{idProducto}' ORDER BY FechaLarga DESC LIMIT 1";

            return consulta;
        }

        public string ActualizarCBGenerado(string codigo, int idUsuario)
        {
            string consulta = $"UPDATE CodigoBarrasGenerado SET CodigoBarras = '{codigo}' WHERE IDUsuario = {idUsuario}";

            return consulta;
        }

        public string ActualizarClienteVenta(string[] datos)
        {
            var consulta = $"UPDATE Ventas SET Cliente = '{datos[0]}', RFC = '{datos[1]}' WHERE ID = {datos[2]} AND IDUsuario = {datos[3]}";

            return consulta;
        }

        public string guardar_editar_empleado(string[] datos, int opc = 0)
        {
            string cons = "";

            // Guardar
            if (opc == 1)
            {
                cons = "INSERT INTO Empleados (IDUsuario, nombre, usuario, contrasena)";
                cons += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}')";


            }

            // Ajustar permisos
            if (opc == 2)
            {
                cons = $"UPDATE Empleados SET p_anticipo='{datos[2]}', p_caja='{datos[3]}', p_cliente='{datos[4]}', p_config='{datos[5]}', p_empleado='{datos[6]}', p_empresa='{datos[7]}', p_factura='{datos[8]}', p_inventario='{datos[9]}', p_mdatos='{datos[10]}', p_producto='{datos[11]}', p_proveedor='{datos[12]}', p_reporte='{datos[13]}', p_venta='{datos[14]}', Bascula='{datos[15]}', ConsultaPrecio ='{datos[16]}' WHERE ID='{datos[1]}' AND IDUsuario='{datos[0]}'";
            }

            // Obtener usuario
            if (opc == 3)
            {
                cons = $"SELECT nombre, usuario FROM Empleados WHERE ID='{datos[0]}'";
            }
            // Editar 
            if (opc == 4)
            {
                cons = $"UPDATE Empleados SET nombre = '{datos[1]}', contrasena = '{datos[2]}' WHERE ID = {datos[0]} AND IDUsuario = {FormPrincipal.userID}";
            }

            if (opc == 5)
            {
                cons = $"UPDATE Empleados SET nombre = '{datos[1]}'  WHERE ID = {datos[0]} AND IDUsuario = {FormPrincipal.userID}";
            }

            return cons;
        }

        public string archivos_digitales(string[] datos, int opc = 0)
        {
            string cons = "";

            // Guardar/borrar fecha vencimiento y número del certificado
            if (opc == 1)
            {
                cons = $"UPDATE Usuarios SET num_certificado='{datos[1]}', fecha_caducidad_cer='{datos[2]}', password_cer='{datos[3]}' WHERE ID='{datos[0]}'";
            }
            if (opc == 2)
            {
                cons = $"SELECT RFC, fecha_caducidad_cer, password_cer FROM Usuarios WHERE ID='{datos[0]}'";
            }
            if (opc == 3)
            {
                cons = $"UPDATE Usuarios SET password_cer='{datos[1]}' WHERE ID='{datos[0]}'";
            }

            return cons;
        }

        public string GuardarRevisarInventario(string[] datos, int tipo = 0)
        {
            string consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = "INSERT INTO RevisarInventario (IDAlmacen, Nombre, ClaveInterna, CodigoBarras, StockAlmacen, StockFisico, NoRevision, Fecha, Vendido, Diferencia, IDUsuario, Tipo, StatusRevision, StatusInventariado, PrecioProducto, IDComputadora)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}')";
            }

            if (tipo == 1)
            {

            }

            return consulta;
        }

        public string cargar_info_certificado(string id_usuario)
        {
            string cons = $"SELECT num_certificado, password_cer, CodigoPostal FROM Usuarios WHERE ID='{id_usuario}'";

            return cons;
        }

        public string cargar_datos_venta_xml(int opc, int id, int id_usuario)
        {
            string cons = "";
            // Facturas
            if (opc == 1)
            {
                cons = $"SELECT * FROM Facturas WHERE ID='{id}' AND id_usuario='{id_usuario}'";
            }

            // Emisor
            if (opc == 2)
            {
                cons = $"SELECT * FROM Usuarios WHERE ID='{id_usuario}'";
            }

            // Receptor
            if (opc == 3)
            {
                cons = $"SELECT * FROM Clientes WHERE ID='{id}'";
            }

            // Consulta los productos de la venta en tabla ProductosVenta
            if (opc == 4)
            {
                cons = $"SELECT * FROM ProductosVenta WHERE IDVenta='{id}'";
            }
            // Tabla productos
            if (opc == 5)
            {
                cons = $"SELECT * FROM Productos WHERE ID='{id}'";
            }
            // Catalogo monedas
            if (opc == 6)
            {
                cons = $"SELECT * FROM catalogo_monedas";
            }
            // Consulta clientes
            if (opc == 7)
            {
                cons = $"SELECT * FROM Clientes WHERE IDUsuario='{id_usuario}'";
            }
            // Consulta impuestos de la venta en tabla DetallesFacturacionProductos
            if (opc == 8)
            {
                cons = $"SELECT * FROM DetallesFacturacionProductos WHERE IDProducto='{id}'";
            }

            // Consulta folio y serie de tabla ventas 
            if (opc == 9)
            {
                cons = $"SELECT Folio, Serie FROM Ventas WHERE ID='{id}'";
            }

            // Facturas_productos 
            if (opc == 10)
            {
                cons = $"SELECT * FROM Facturas_productos WHERE id_factura='{id}'";
            }

            // Consulta todos los impuestos diferente de 16, 8 y 0 porcientos 10
            if (opc == 11)
            {
                cons = $"SELECT * FROM Facturas_impuestos WHERE id_factura_producto='{id}'";
            }

            return cons;
        }

        public string guarda_datos_faltantes_xml(int opc, string[] datos)
        {
            string modif = "";

            // GUarda id del cliente
            /*if(opc == 1)
            {
                modif = $"UPDATE DetallesVenta SET IDCliente='{datos[1]}', Cliente='{datos[2]}' WHERE IDVenta='{datos[0]}'";
            }*/
            // Actualiza datos del cliente 
            if (opc == 1)
            {
                modif = $"UPDATE Clientes SET RazonSocial='{datos[1]}', RFC='{datos[2]}', Telefono='{datos[3]}', Email='{datos[4]}', NombreComercial='{datos[5]}', Pais='{datos[6]}', Estado='{datos[7]}', Municipio='{datos[8]}', Localidad='{datos[9]}', CodigoPostal='{datos[10]}', Colonia='{datos[11]}', Calle='{datos[12]}', NoExterior='{datos[13]}', NoInterior='{datos[14]}', UsoCFDI='{datos[15]}' WHERE ID='{datos[0]}'";
            }
            // Guarda método y forma de pago, moneda y tipo de cambio
            if (opc == 2)
            { //
                modif = $"UPDATE Ventas SET MetodoPago='{datos[1]}', FormaPago='{datos[2]}', num_cuenta='{datos[3]}', moneda='{datos[4]}', tipo_cambio='{datos[5]}' WHERE ID='{datos[0]}'";
            }
            // Guarda claves de unidad y producto
            if (opc == 3)
            {
                modif = $"UPDATE Productos SET ClaveProducto='{datos[1]}', UnidadMedida='{datos[0]}' WHERE ID='{datos[2]}'";
            }

            // Cambia a timbrada la nota
            if (opc == 4)
            {
                modif = $"UPDATE Ventas SET Timbrada='1' WHERE ID='{datos[0]}'";
            }

            // Guarda datos de pago, emisor y receptor
            if (opc == 5)
            {
                modif = "INSERT INTO Facturas (id_usuario, id_venta, id_empleado, metodo_pago, forma_pago, num_cuenta, moneda, tipo_cambio, uso_cfdi,";
                modif += "e_rfc, e_razon_social, e_regimen,  e_correo, e_telefono, e_cp, e_estado, e_municipio, e_colonia, e_calle, e_num_ext, e_num_int, e_nombre_comercial,";
                modif += "r_rfc, r_razon_social, r_nombre_comercial, r_correo, r_telefono, r_pais, r_estado, r_municipio, r_localidad, r_cp, r_colonia, r_calle, r_num_ext, r_num_int,";
                modif += "folio, serie, tipo_comprobante)";
                modif += $" VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', '{datos[20]}', '{datos[37]}', '{datos[21]}', '{datos[22]}', '{datos[23]}', '{datos[24]}', '{datos[25]}', '{datos[26]}', '{datos[27]}', '{datos[28]}', '{datos[29]}', '{datos[30]}', '{datos[31]}', '{datos[32]}', '{datos[33]}', '{datos[34]}', '{datos[35]}', '{datos[36]}', 'I')";
            }

            // Guarda los productos 
            if (opc == 6)
            {
                modif = $"INSERT INTO Facturas_productos (id_factura, clave_unidad, clave_producto, descripcion, cantidad, precio_u, base, tasa_cuota, importe_iva, descuento) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}')";
            }

            // Guarda los impuestos diferentes de IVA 16, 0, 8 y exento
            if (opc == 7)
            {
                modif = $"INSERT INTO Facturas_impuestos (id_factura_producto, tipo, impuesto, tipo_factor, tasa_cuota, definir, importe) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}')";
            }

            // Cambia a timbrada la factura
            if (opc == 8)
            {
                modif = $"UPDATE Facturas SET timbrada='1' WHERE ID='{datos[0]}'";
            }

            // Agrega datos que se obtienen del XML ya timbrado
            if (opc == 9)
            {
                modif = $"UPDATE Facturas SET fecha_certificacion='{datos[1]}', UUID='{datos[2]}', rfc_pac='{datos[3]}', sello_sat='{datos[4]}', sello_cfd='{datos[5]}', total='{datos[6]}' WHERE ID='{datos[0]}'";
            }

            return modif;
        }

        public string GuardarHistorialPrecios(string[] datos)
       {
            var consulta = "INSERT INTO HistorialPrecios (IDUsuario, IDEmpleado, IDProducto, PrecioAnterior, PrecioNuevo, Origen, FechaOperacion)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}')";

            return consulta;
        }

        #region Procesos de Datos Dinamicos

        public string VerificarContenidoDinamico(int idUsuario)
        {
            var consulta = $"SELECT * FROM appSettings WHERE IDUsuario = '{idUsuario}' AND Mostrar = 1";

            return consulta;
        }

        public string VerificarDatoDinamico(string claveAgregar, int idUsuario)
        {
            var consulta = $"SELECT * FROM appSettings WHERE concepto = '{claveAgregar}' AND IDUsuario = '{idUsuario}' AND Mostrar = 1";

            return consulta;
        }

        public string VerificarDatoDinamicoCompleto(string claveConcepto, string textoConcepto, int userID)
        {
            var consulta = $"SELECT *  FROM appSettings WHERE concepto = '{claveConcepto}' AND textComboBoxConcepto = '{textoConcepto}' AND IDUsuario = '{userID}'";

            return consulta;
        }

        public string InsertaDatoDinamico(string claveAgregar, int claveValor, int idUsuario)
        {
            var consulta = "INSERT INTO appSettings (concepto, checkBoxConcepto, textComboBoxConcepto, checkBoxComboBoxConcepto, IDUsuario)";
            consulta += $"VALUES ('{claveAgregar}', '{claveValor}', 'chk{claveAgregar}', '{claveValor}', '{idUsuario}')";

            return consulta;
        }

        public string ActualizarDatoDinamico(string claveAntigua, string claveNueva, int idUsuario)
        {
            var consulta = $"UPDATE appSettings SET concepto = '{claveNueva}', textComboBoxConcepto = 'chk{claveNueva}' WHERE concepto = '{claveAntigua}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string ActualizarDatoDinamicoFiltroProducto(string claveBuscar, int valorClave, string textClaveBuscar, int idUsuario)
        {
            var consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = '{valorClave}', textComboBoxConcepto = '{claveBuscar} = ''{textClaveBuscar}''' WHERE concepto = '{claveBuscar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string BorrarDatoDinamico(string claveBorrar, int idUsuario)
        {
            //var consulta = $"DELETE FROM appSettings WHERE concepto = '{claveBorrar}' AND IDUsuario = '{idUsuario}'";
            var consulta = $"UPDATE appSettings SET Mostrar = 0 WHERE concepto = '{claveBorrar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string ActualizarDatoValueDinamico(string claveBuscar, int valueDato, int idUsuario)
        {
            var consulta = $"UPDATE appSettings SET checkBoxConcepto = '{valueDato}' WHERE concepto = '{claveBuscar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string ActualizarDatoValueDinamicoShow(string claveBuscar, int valueDato, int idUsuario)
        {
            var consulta = $"UPDATE appSettings SET checkBoxComboBoxConcepto = '{valueDato}' WHERE textComboBoxConcepto = '{claveBuscar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string VerificarDatoFiltroDinamico(string claveAgregar, int idUsuario)
        {
            var consulta = $"SELECT * FROM FiltroDinamico WHERE concepto = '{claveAgregar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string InsertarDatoFiltroDinamico(string claveAgregar, int claveValor, int idUsuario)
        {
            var consulta = "INSERT INTO FiltroDinamico (concepto, checkBoxConcepto, IDUsuario)";
            consulta += $"VALUES ('{claveAgregar}', '{claveValor}', '{idUsuario}')";

            return consulta;
        }

        public string InsertarDatoFiltroDinamicoCompleto(string claveAgregar, string textoConcepto, int idUsuario)
        {
            var consulta = "INSERT INTO FiltroDinamico (concepto, checkBoxConcepto, textCantidad, IDUsuario)";
            consulta += $"VALUES ('{claveAgregar}', '{0}', '{textoConcepto}', '{idUsuario}')";

            return consulta;
        }

        public string ActualizarDatoFiltroDinamico(string claveBuscar, int valueDato, int idUsuario)
        {
            var consulta = $"UPDATE FiltroDinamico SET checkBoxConcepto = '{valueDato}' WHERE concepto = '{claveBuscar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string ActualizarNombreDatoFiltroDinamico(string claveAntigua, string claveNueva, int idUsuario)
        {
            var consulta = $"UPDATE FiltroDinamico SET concepto = 'chk{claveNueva}' WHERE concepto = 'chk{claveAntigua}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string BorrarDatoFiltroDinamico(string claveBorrar, int idUsuario)
        {
            var consulta = $"DELETE FROM FiltroDinamico WHERE concepto = '{claveBorrar}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string ActualizarTextConceptoFiltroDinamico(string textConcepto, int idUsuario, string textDescripcion)
        {
            var consulta = $"UPDATE FiltroDinamico SET textCantidad = '{textDescripcion}' WHERE concepto = '{textConcepto}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string ActualizarTextConceptoFiltroDinamicoGral(string idDetailGral, string idDetailProdGral)
        {
            var consulta = $"UPDATE DetallesProductoGenerales SET IDDetalleGral = '{idDetailGral}' WHERE ID = '{idDetailProdGral}'";

            return consulta;
        }

        public string VerificarTextoConceptoFiltroDinamico(string searchConcepto, int idUser)
        {
            var consulta = $"SELECT * FROM FiltroDinamico WHERE concepto = 'chk{searchConcepto}' AND IDUsuario = '{idUser}'";

            return consulta;
        }

        public string LlenarFiltroDinamicoComboBox(int idUser)
        {
            var consulta = $"SELECT * FROM FiltroDinamico WHERE IDUsuario = {idUser} AND checkBoxConcepto = 1";

            return consulta;
        }

        public string ReiniciarFiltroDinamico(int userID, string searchFiltroDinamico, string username)
        {
            var consulta = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{userID}' AND concepto = '{searchFiltroDinamico}' AND Username = '{username}'";

            return consulta;
        }

        public string ReiniciarFiltroDinamicoTresCampos(int valueIntFiltroDinamico, string valueStrFiltroDinamico, int idUser, string searchFiltroDinamico, string username)
        {
            var consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = '{valueIntFiltroDinamico}', textComboBoxConcepto = '{valueStrFiltroDinamico}', textCantidad = '{valueIntFiltroDinamico}' WHERE IDUsuario = '{idUser}' AND concepto = '{searchFiltroDinamico}' AND Username = '{username}'";

            return consulta;
        }

        public string ReiniciarFiltroDinamicoDosCampos(int valueIntFiltroDinamico, string valueStrFiltroDinamico, int idUser, string searchFiltroDinamico)
        {
            var consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = '{valueIntFiltroDinamico}', textComboBoxConcepto = '{valueStrFiltroDinamico}' WHERE IDUsuario = '{idUser}' AND concepto = '{searchFiltroDinamico}'";

            return consulta;
        }

        public string VerificarContenidoFiltroProducto(int idUsuario, string username)
        {
            var consulta = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{idUsuario}' AND Username = '{username}'";

            return consulta;
        }

        public string IniciarFiltroConSinFiltroAvanzado(int userID)
        {
            var consulta = $@"SELECT P.* FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = u.ID WHERE U.ID = '{userID}' AND P.Status = 1";

            return consulta;
        }

        public string VerificarVentanaFiltros(int userID, string username)
        {
            var consulta = $"SELECT * FROM FiltrosDinamicosVetanaFiltros WHERE IDUsuario = '{userID}' AND Username = '{username}'";

            return consulta;
        }

        public string GuardarVentanaFiltros(string valueChkBox, string conceptoChkBox, string textComboBox, int idUsuario, string username)
        {
            var consulta = "INSERT INTO FiltrosDinamicosVetanaFiltros (checkBoxValue, concepto, strFiltro, IDUsuario, Username)";
            consulta += $"VALUES ('{valueChkBox}', '{conceptoChkBox}', '{textComboBox}', '{idUsuario}', '{username}')";

            return consulta;
        }

        public string BuscarDatoEnVentanaFiltros(string strConcepto, int userID, string username)
        {
            var consulta = $"SELECT * FROM FiltrosDinamicosVetanaFiltros WHERE concepto = '{strConcepto}' AND IDUsuario = '{userID}' AND Username = '{username}'";

            return consulta;
        }

        public string ActualizarDatoVentanaFiltros(string valueChkBox, string conceptoChkBox, string textComboBox, int idUsuario, string username)
        {
            var consulta = $"UPDATE FiltrosDinamicosVetanaFiltros SET checkBoxValue = '{valueChkBox}', strFiltro = '{textComboBox}' WHERE concepto = '{conceptoChkBox}' AND IDUsuario = '{idUsuario}' AND Username = '{username}'";

            return consulta;
        }

        public string ActualizarNombreDatoVentanaFiltros(string newNombreConcepto, string oldNombreConcepto, int userID)
        {
            var consulta = $"UPDATE FiltrosDinamicosVetanaFiltros SET concepto = '{newNombreConcepto}', strFiltro = 'Selecciona {newNombreConcepto}' WHERE concepto = '{oldNombreConcepto}' AND IDUsuario = '{userID}'";

            return consulta;
        }

        public string BorrarDatoVentanaFiltros(string strConcepto, int userID)
        {
            var consulta = $"DELETE FROM FiltrosDinamicosVetanaFiltros WHERE concepto = '{strConcepto}' AND IDUsuario = '{userID}'";

            return consulta;
        }

        #endregion

        #region Procesos de Filtro de Stock, Precio, Revision, Tipo, Imagen

        public string VerificarChk(string chkBoxConcepto, int idUsuario)
        {
            var consulta = $"SELECT ID, concepto, checkBoxConcepto, IDUsuario FROM FiltroProducto WHERE concepto = '{chkBoxConcepto}' AND IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string InsertarChk(string chkBoxConcepto, int chkBoxValor)
        {
            var consulta = "INSERT INTO FiltroProducto(concepto, checkBoxConcepto, IDUsuario) ";
            consulta += $"VALUES('{chkBoxConcepto}', '{chkBoxValor}', '{FormPrincipal.userID}')";

            return consulta;
        }

        public string ActualizarChk(string chkBoxConcepto, int chkBoxValor)
        {
            var consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = '{chkBoxValor}' WHERE IDUsuario = '{FormPrincipal.userID}' AND concepto = '{chkBoxConcepto}'";

            return consulta;
        }

        public string BuscarFiltroProducto(int userID)
        {
            var consulta = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{userID}'";

            return consulta;
        }

        public string InsertarTextCBConceptoCantidad(string txtCBConcepto, string txtCantidad)
        {
            var consulta = "INSERT INTO FiltroProducto(textComboBoxConcepto, textCantidad, IDUsuario) ";
            consulta += $"VALUES('{txtCBConcepto}', '{txtCantidad}', '{FormPrincipal.userID}')";

            return consulta;
        }

        public string ActualizarTextCBConceptoCantidad(int idFiltro, string txtCBConcepto, string txtCantidad)
        {
            var consulta = $"UPDATE FiltroProducto SET textComboBoxConcepto = '{txtCBConcepto}', textCantidad = '{txtCantidad}' WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idFiltro}'";

            return consulta;
        }

        #endregion

        public string obtener_datos_para_gcpago(int opc, int dato)
        {
            string cons = "";

            if (opc == 1)
            {
                cons = $"SELECT * FROM Facturas WHERE ID='{dato}'";
            }
            // Carga todos los complementos de pago a timbrar
            if (opc == 2)
            {
                cons = $"SELECT * FROM Facturas_complemento_pago WHERE id_factura='{dato}' AND timbrada=0";
            }
            if (opc == 3)
            {
                cons = $"SELECT importe_pagado FROM Facturas_complemento_pago WHERE id_factura_principal='{dato}' AND timbrada=1 AND cancelada=0";
            }
            if (opc == 4)
            {
                cons = $"SELECT id_factura_principal, importe_pagado FROM Facturas_complemento_pago WHERE id_factura='{dato}'";
            }
            if (opc == 5)
            {
                cons = $"SELECT ID FROM Facturas_complemento_pago WHERE id_factura_principal='{dato}' AND timbrada=1 AND cancelada=0";
            }

            return cons;
        }

        public string crear_complemento_pago(int opc, string[] datos)
        {
            string crea = "";

            // Crea registro en tabla facturas
            if (opc == 1)
            {
                crea = "INSERT INTO Facturas (id_usuario, id_empleado, forma_pago, num_cuenta, moneda, tipo_cambio, folio, serie, tipo_comprobante, uso_cfdi, fecha_hora_cpago,";
                crea += "r_rfc, r_razon_social, r_nombre_comercial, r_correo, r_telefono, r_pais, r_estado, r_municipio, r_localidad, r_cp, r_colonia, r_calle, r_num_ext, r_num_int,";
                crea += "e_rfc, e_razon_social, e_regimen, e_correo, e_telefono, e_cp, e_estado, e_municipio, e_colonia, e_calle, e_num_ext, e_num_int)";
                crea += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[32]}', 'MXN', '1.000000', '{datos[3]}','{datos[4]}', 'P', 'P01', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', '{datos[20]}', '{datos[21]}', '{datos[22]}', '{datos[23]}', '{datos[24]}', '{datos[25]}', '{datos[26]}', '{datos[27]}', '{datos[28]}', '{datos[29]}', '{datos[30]}', '{datos[31]}')";
            }

            // Crea registro en Facturas_productos
            if (opc == 2)
            {
                crea = $"INSERT INTO Facturas_productos (id_factura, clave_unidad, clave_producto, descripcion, cantidad, precio_u) VALUES ('{datos[0]}', 'ACT', '84111506', 'Pago', '1', '0')";
            }

            // Crea registro en tabla complemento de pago
            if (opc == 3)
            {
                crea = $"INSERT INTO Facturas_complemento_pago (id_factura, id_factura_principal, uuid, moneda, metodo_pago, num_parcialidad, saldo_anterior, importe_pagado, saldo_insoluto) VALUES ('{datos[0]}', '{datos[1]}', '{datos[6]}', 'MXN', 'PPD', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}')";
            }

            // Cambia variable a 1 para indicar que la factura principal tienen complementos de pago
            if (opc == 4)
            {
                crea = $"UPDATE Facturas SET con_complementos='1', resta_cpago='{datos[1]}' WHERE ID='{datos[0]}'";
            }
            if (opc == 5)
            {
                crea = $"UPDATE Facturas_complemento_pago SET timbrada=1 WHERE id_factura={datos[0]}";
            }

            // Agrega el monto pagado del complemento
            if (opc == 6)
            {
                crea = $"UPDATE Facturas SET monto_cpago='{datos[1]}' WHERE ID='{datos[0]}'";
            }


            return crea;
        }

        public string obtiene_cpagos_dfactura_princ(int idf, int opc)
        {
            string cons = "";

            if (opc == 1)
            {
                cons = $"SELECT f.total, fp.id_factura, fp.importe_pagado, f.fecha_certificacion, f.id_empleado FROM Facturas AS f INNER JOIN Facturas_complemento_pago AS fp ON f.ID=fp.id_factura_principal WHERE fp.id_factura_principal='{idf}' AND fp.timbrada=1 AND fp.cancelada=0";
            }
            if (opc == 2)
            {
                cons = $"SELECT ID, folio, serie, r_rfc, r_razon_social, total FROM Facturas WHERE ID='{idf}'";
            }

            return cons;
        }

        public string consulta_dventa(int opc, int id)
        {
            string cons = "";

            if (opc == 1)
            {
                cons = $"SELECT * FROM Ventas WHERE ID='{id}'";
            }
            if (opc == 2)
            {
                cons = $"SELECT * FROM DetallesVenta WHERE IDVenta='{id}'";
            }


            return cons;
        }

        public string descontar_timbres(int id)
        {
            string ed = $"UPDATE Usuarios SET timbres= timbres - 1 WHERE ID='{id}'";

            return ed;
        }

        public string RenombrarDatosDelFiltroDinamico(string nvoConcepto, string viejoConcepto, int idUser)
        {
            var consulta = $"UPDATE DetalleGeneral SET ChckName = '{nvoConcepto}' WHERE IDUsuario = '{idUser}' AND ChckName = '{viejoConcepto}'";

            return consulta;
        }

        public string BorrarDetalleGeneralPorConcepto(string deleteDetalle, int userID)
        {
            var consulta = $"DELETE FROM DetalleGeneral WHERE IDUsuario = '{userID}' AND ChckName = '{deleteDetalle}'";

            return consulta;
        }

        public string ReimprimirTicket(int idVenta)
        {
            var consulta = $@"SELECT DISTINCT SaleDetail.IDVenta AS idVenta, SaleDetail.Referencia AS Referencia, SaleDetail.Cliente AS Cliente, SaleProd.IDProducto AS IDProducto, Prod.Nombre AS Nombre, SaleProd.Cantidad AS Cantidad, SaleProd.Precio AS Precio, Sale.DescuentoGeneral AS DescuentoGeneral, Sale.Descuento AS DescuentoIndividual, Prod.Precio AS ImporteIndividual, SaleProd.descuento AS Descuento, Sale.Folio AS Folio, Sale.Anticipo AS AnticipoUtilizado, Prod.TipoDescuento AS TipoDescuento, Sale.FormaPago AS formaDePagoDeVenta, Sale.FechaOperacion AS FechaOperacion FROM detallesventa AS SaleDetail INNER JOIN ventas AS Sale ON Sale.ID = SaleDetail.IDVenta INNER JOIN usuarios AS Usr ON Usr.ID = Sale.IDUsuario INNER JOIN productosventa AS SaleProd ON SaleProd.IDVenta = Sale.ID INNER JOIN productos AS Prod ON Prod.ID = SaleProd.IDProducto WHERE SaleDetail.IDVenta = {idVenta} GROUP BY Prod.ID";

            return consulta;
        }

        public string cargar_impuestos_en_editar_producto(int id)
        {
            string cons = $"SELECT * FROM detallesfacturacionproductos WHERE IDProducto = {id}";

            return cons;
        }

        public string InsertIntoNoRevAumentarInventario(string NoRev)
        {
            var consulta = string.Empty;

            using (DataTable dtNoRevision = cn.CargarDatos($"SELECT * FROM NoRevisionAumentarInventario WHERE IdUsuario = {FormPrincipal.userID}"))
            {
                if (!dtNoRevision.Rows.Count.Equals(0))
                {
                    consulta = $"UPDATE NoRevisionAumentarInventario SET NoRevisionAumentarInventario = {NoRev} WHERE id = 1 AND IdUsuario = {FormPrincipal.userID}";
                }
                else if (dtNoRevision.Rows.Count.Equals(0))
                {
                    consulta = $"INSERT INTO NoRevisionAumentarInventario (NoRevisionAumentarInventario, IdUsuario) VALUES ('{NoRev}', '{FormPrincipal.userID}')";
                }
            }

            return consulta;
        }

        public string GetNoRevAumentarInventario()
        {
            var NoRevision = string.Empty;

            using (DataTable dtNoRevision = cn.CargarDatos($"SELECT * FROM NoRevisionAumentarInventario WHERE IdUsuario = {FormPrincipal.userID}"))
            {
                if (!dtNoRevision.Rows.Count.Equals(0))
                {
                    foreach (DataRow drNoRev in dtNoRevision.Rows)
                    {
                        NoRevision = drNoRev["NoRevisionAumentarInventario"].ToString();
                    }
                }
                else if (dtNoRevision.Rows.Count.Equals(0))
                {
                    var consulta = string.Empty;
                    NoRevision = "0";

                    consulta = $"INSERT INTO NoRevisionAumentarInventario (NoRevisionAumentarInventario, IdUsuario) VALUES ('{NoRevision}', '{FormPrincipal.userID}')";

                    cn.EjecutarConsulta(consulta);
                }
            }

            return NoRevision;
        }

        public string UpdateNoRevAumentarInventario(int NoRev)
        {
            var consulta = $"UPDATE NoRevisionAumentarInventario SET NoRevisionAumentarInventario = {NoRev} WHERE IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string InsertIntoAumentarInventario(string[] datosAumentarInventario)
        {
            var consulta = "INSERT INTO DGVAumentarInventario(IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, IdUsuario, IDEmpleado, NameUsr) VALUES";
            consulta += $"('{datosAumentarInventario[0]}', '{datosAumentarInventario[1]}', '{datosAumentarInventario[2]}', '{datosAumentarInventario[3]}', '{datosAumentarInventario[4]}', '{datosAumentarInventario[5]}', '{datosAumentarInventario[6]}', '{datosAumentarInventario[7]}', '{datosAumentarInventario[8]}', '{datosAumentarInventario[9]}', '{datosAumentarInventario[10]}', '{datosAumentarInventario[11]}', '{datosAumentarInventario[12]}', '{datosAumentarInventario[13]}', '{datosAumentarInventario[14]}', '{datosAumentarInventario[15]}', '{datosAumentarInventario[16]}')";

            return consulta;
        }

        //public string UpdateAumentarInventario(string[] datosAumentarInventario)
        //{
        //    var consulta = $"UPDATE DGVAumentarInventario SET StockActual = '{datosAumentarInventario[2]}', DiferenciaUnidades = '{datosAumentarInventario[3]}', NuevoStock = '{datosAumentarInventario[4]}', Precio =  '{datosAumentarInventario[5]}', Clave = '{datosAumentarInventario[6]}', Codigo = '{datosAumentarInventario[7]}', Fecha = '{datosAumentarInventario[8]}', NoRevision = '{datosAumentarInventario[9]}', StatusActualizacion = '{datosAumentarInventario[10]}', NombreEmisor = '{datosAumentarInventario[11]}', Comentarios = '{datosAumentarInventario[12]}', ValorUnitario = '{datosAumentarInventario[13]}', IdUsuario = '{datosAumentarInventario[14]}', IDEmpleado ='{datosAumentarInventario[15]}' , NameUsr = '{datosAumentarInventario[16]}' WHERE Codigo = '{datosAumentarInventario[7]}' AND StatusActualizacion = '1'";
        //    return consulta;
        //}

        public string GetAumentarInventario()
        {
            var consultar = $"SELECT IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, ID FROM DGVAumentarInventario WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

            return consultar;
        }

        public string UpdateStatusActualizacionAumentarInventario()
        {
            var consulta = $"UPDATE DGVAumentarInventario SET StatusActualizacion = 0 WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string InsertIntoNoRevDisminuirInvntario(string NoRev)
        {
            var consulta = string.Empty;

            using (DataTable dtNoRevision = cn.CargarDatos($"SELECT * FROM DGVDisminuirInventario WHERE IdUsuario = {FormPrincipal.userID}"))
            {
                if (!dtNoRevision.Rows.Count.Equals(0))
                {
                    consulta = $"UPDATE NoRevisionDisminuirInventario SET NoRevisionDisminuirInventario = {NoRev} WHERE id = 1 AND IdUsuario = {FormPrincipal.userID}";
                }
                else if (dtNoRevision.Rows.Count.Equals(0))
                {
                    consulta = $"INSERT INTO NoRevisionDisminuirInventario (NoRevisionDisminuirInventario, IdUsuario) VALUES ('{NoRev}', '{FormPrincipal.userID}')";
                }
            }

            return consulta;
        }

        public string GetNoRevDisminuirInventario()
        {
            var NoRevision = string.Empty;

            using (DataTable dtNoRevision = cn.CargarDatos($"SELECT * FROM NoRevisionDisminuirInventario WHERE IdUsuario = {FormPrincipal.userID}"))
            {
                if (!dtNoRevision.Rows.Count.Equals(0))
                {
                    foreach (DataRow drNoRev in dtNoRevision.Rows)
                    {
                        NoRevision = drNoRev["NoRevisionDisminuirInventario"].ToString();
                    }
                }
                else if (dtNoRevision.Rows.Count.Equals(0))
                {
                    var consulta = string.Empty;
                    NoRevision = "0";

                    consulta = $"INSERT INTO NoRevisionDisminuirInventario (NoRevisionDisminuirInventario, IdUsuario) VALUES ('{NoRevision}', '{FormPrincipal.userID}')";

                    cn.EjecutarConsulta(consulta);
                }
            }

            return NoRevision;
        }

        public string UpdateNoRevDisminuirInventario(int NoRev)
        {
            var consulta = $"UPDATE NoRevisionDisminuirInventario SET NoRevisionDisminuirInventario = {NoRev} WHERE IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string InsertarIntoDisminuirInventario(string[] datosDisminuirInventario)
        {
            var consulta = "INSERT INTO DGVDisminuirInventario(IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, IdUsuario, IDEmpleado,NameUse) VALUES";
            consulta += $"('{datosDisminuirInventario[0]}', '{datosDisminuirInventario[1]}', '{datosDisminuirInventario[2]}', '{datosDisminuirInventario[3]}', '{datosDisminuirInventario[4]}', '{datosDisminuirInventario[5]}', '{datosDisminuirInventario[6]}', '{datosDisminuirInventario[7]}', '{datosDisminuirInventario[8]}', '{datosDisminuirInventario[9]}', '{datosDisminuirInventario[10]}', '{datosDisminuirInventario[11]}', '{datosDisminuirInventario[12]}', '{datosDisminuirInventario[13]}', '{datosDisminuirInventario[14]}', '{datosDisminuirInventario[15]}' , '{datosDisminuirInventario[16]}')";

            return consulta;
        }

        //public string UpdateIntoDisminuirInventario(string[] datosDisminuirInventario)
        //{
        //    var consulta = $"UPDATE DGVDisminuirInventario SET StockActual = '{datosDisminuirInventario[2]}', DiferenciaUnidades = '{datosDisminuirInventario[3]}', NuevoStock = '{datosDisminuirInventario[4]}', Precio =  '{datosDisminuirInventario[5]}', Clave = '{datosDisminuirInventario[6]}', Codigo = '{datosDisminuirInventario[7]}', Fecha = '{datosDisminuirInventario[8]}', NoRevision = '{datosDisminuirInventario[9]}', StatusActualizacion = '{datosDisminuirInventario[10]}', NombreEmisor = '{datosDisminuirInventario[11]}', Comentarios = '{datosDisminuirInventario[12]}', ValorUnitario = '{datosDisminuirInventario[13]}', IdUsuario = '{datosDisminuirInventario[14]}', IDEmpleado ='{datosDisminuirInventario[15]}' WHERE Codigo = '{datosDisminuirInventario[7]}' AND StatusActualizacion = '1'";
        //    return consulta;
        //}

        public string GetDisminuirInventario()
        {
            var consulta = $"SELECT IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, ID FROM DGVDisminuirInventario WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string UpdateStatusActualizacionDisminuirInventario()
        {
            var consulta = $"UPDATE DGVDisminuirInventario SET StatusActualizacion = 0 WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string SearchDGVAumentarInventario(int NoRev)
        {
            //var consulta = $"SELECT * FROM DGVAumentarInventario WHERE NoRevision = {NoRev} AND StatusActualizacion = 1 AND IDUsuario = {FormPrincipal.userID}";

            var consulta = string.Empty;
            var queryAppSetting = $"SELECT * FROM appsettings WHERE IDUsuario = '{FormPrincipal.userID}' AND checkBoxConcepto = '1' AND Mostrar = '1' AND concepto <> 'Proveedor';";
            var queryRepAumenInv = "SELECT * FROM ReporteAumentarInventario;";
            var queryRepAumenInvExtended = "SELECT * FROM ReporteAumentarInventarioExtended";
            var queryCrearRemplazarView = string.Empty;

            List<string> columnasDinamicas = new List<string>();

            consulta = $"CREATE OR REPLACE VIEW ReporteAumentarInventario AS SELECT PlusInv.*, GralDetail.ID AS IDConcepto, IFNULL( GralDetail.ChckName, \"N/A\" ) AS Concepto, IFNULL( GralDetail.Descripcion, \"N/A\" ) AS Descripcion FROM dgvaumentarinventario AS PlusInv LEFT JOIN detallesproductogenerales AS GralDetailProd ON GralDetailProd.IDProducto = PlusInv.IdProducto LEFT JOIN detallegeneral AS GralDetail ON GralDetail.ID = GralDetailProd.IDDetalleGral WHERE PlusInv.IdUsuario = '{FormPrincipal.userID}' AND PlusInv.StatusActualizacion = '1' AND PlusInv.NoRevision = '{NoRev}' ORDER BY PlusInv.IDProducto;";

            cn.crearViewDinamica(consulta);

            using (DataTable dtConceptosDinamicosVisiblesActivos = cn.CargarDatos(queryAppSetting))
            {
                if (!dtConceptosDinamicosVisiblesActivos.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtConceptosDinamicosVisiblesActivos.Rows)
                    {
                        columnasDinamicas.Add(item["concepto"].ToString());
                    }

                    bool isEmpty = !columnasDinamicas.Any();

                    int limite = 0;
                    var count = 0;

                    if (!isEmpty)
                    {
                        limite = columnasDinamicas.Count;
                    }

                    using (DataTable dtReporteAumentarInventario = cn.CargarDatos(queryRepAumenInv))
                    {
                        if (!dtReporteAumentarInventario.Rows.Count.Equals(0))
                        {
                            queryCrearRemplazarView = "CREATE OR REPLACE VIEW ReporteAumentarInventarioExtended AS SELECT repAumInv.id, repAumInv.IdProducto AS No, repAumInv.NombreProducto AS Producto, repAumInv.NombreEmisor AS Proveedor, repAumInv.DiferenciaUnidades AS Unidades_Compradas, repAumInv.ValorUnitario AS Precio_Compra, repAumInv.Precio AS Precio_Venta, repAumInv.NuevoStock AS Stock_Anterior, repAumInv.StockActual AS Stock_Actual, repAumInv.Fecha AS Fecha_Compra, repAumInv.Comentarios AS Comentarios, /* repAumInv.Clave, repAumInv.Codigo, repAumInv.NoRevision, repAumInv.StatusActualizacion, repAumInv.IdUsuario, repAumInv.IDEmpleado, repAumInv.NameUsr,repAumInv.Folio, repAumInv.IDConcepto, */ ";

                            foreach (var item in columnasDinamicas)
                            {
                                queryCrearRemplazarView += $" GROUP_CONCAT( DISTINCT IF ( repAumInv.Concepto = \"{item.ToString()}\", repAumInv.Descripcion, NULL ) ) AS {item.ToString()},";
                            }

                            queryCrearRemplazarView = queryCrearRemplazarView.Remove(queryCrearRemplazarView.Length - 1);

                            queryCrearRemplazarView += " FROM reporteaumentarinventario AS repAumInv GROUP BY repAumInv.id ORDER BY repAumInv.NombreProducto, repAumInv.StockActual DESC;";
                        }
                    }
                }
                else
                {
                    using (DataTable dtReporteAumentarInventario = cn.CargarDatos(queryRepAumenInv))
                    {
                        if (!dtReporteAumentarInventario.Rows.Count.Equals(0))
                        {
                            queryCrearRemplazarView = "CREATE OR REPLACE VIEW ReporteAumentarInventarioExtended AS SELECT repAumInv.id, repAumInv.IdProducto AS No, repAumInv.NombreProducto AS Producto, repAumInv.NombreEmisor AS Proveedor, repAumInv.DiferenciaUnidades AS Unidades_Compradas, repAumInv.ValorUnitario AS Precio_Compra, repAumInv.Precio AS Precio_Venta, repAumInv.NuevoStock AS Stock_Anterior, repAumInv.StockActual AS Stock_Actual, repAumInv.Fecha AS Fecha_Compra, repAumInv.Comentarios AS Comentarios ";

                            foreach (var item in columnasDinamicas)
                            {
                                queryCrearRemplazarView += $" GROUP_CONCAT( DISTINCT IF ( repAumInv.Concepto = \"{item.ToString()}\", repAumInv.Descripcion, NULL ) ) AS {item.ToString()},";
                            }

                            queryCrearRemplazarView = queryCrearRemplazarView.Remove(queryCrearRemplazarView.Length - 1);

                            queryCrearRemplazarView += " FROM reporteaumentarinventario AS repAumInv GROUP BY repAumInv.id ORDER BY repAumInv.NombreProducto, repAumInv.StockActual DESC;";
                        }
                    }
                }

                cn.crearViewDinamica(queryCrearRemplazarView);

                consulta = queryRepAumenInvExtended;
            }

            return consulta;
        }

        public string NomEmisorComentariosHistorialCompras(string IdProducto)
        {
            var consulta = $"SELECT ID, NomEmisor, Comentarios, ValorUnitario FROM HistorialCompras WHERE IDProducto = {IdProducto} AND IDUsuario = {FormPrincipal.userID} GROUP BY ID DESC LIMIT 1";

            return consulta;
        }

        public string SearchDGVDisminuirInventario(int NoRev)
        {
            //var consulta = $"SELECT * FROM DGVDisminuirInventario WHERE NoRevision = {NoRev} AND StatusActualizacion = 1 AND IDUsuario = {FormPrincipal.userID}";
            var consulta = string.Empty;
            var queryAppSetting = $"SELECT * FROM appsettings WHERE IDUsuario = '{FormPrincipal.userID}' AND checkBoxConcepto = '1' AND Mostrar = '1' AND concepto <> 'Proveedor';";
            var queryRepDisminInv = "SELECT * FROM ReporteDisminuirInventario;";
            var queryRepDisminInvExtended = "SELECT * FROM ReporteDisminuirInventarioExtended";
            var queryCrearRemplazarView = string.Empty;

            List<string> columnasDinamicas = new List<string>();

            consulta = $"CREATE OR REPLACE VIEW ReporteDisminuirInventario AS SELECT LessInv.*, GralDetail.ID AS IDConcepto, IFNULL( GralDetail.ChckName, \"N/A\" ) AS Concepto, IFNULL( GralDetail.Descripcion, \"N/A\" ) AS Descripcion FROM dgvdisminuirinventario AS LessInv LEFT JOIN detallesproductogenerales AS GralDetailProd ON GralDetailProd.IDProducto = LessInv.IdProducto LEFT JOIN detallegeneral AS GralDetail ON GralDetail.ID = GralDetailProd.IDDetalleGral WHERE LessInv.IdUsuario = '{FormPrincipal.userID}' AND LessInv.StatusActualizacion = '1' AND LessInv.NoRevision = '{NoRev}' ORDER BY  LessInv.IDProducto;";

            cn.crearViewDinamica(consulta);

            using (DataTable dtConceptosDinamicosVisiblesActivos = cn.CargarDatos(queryAppSetting))
            {
                if (!dtConceptosDinamicosVisiblesActivos.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtConceptosDinamicosVisiblesActivos.Rows)
                    {
                        columnasDinamicas.Add(item["concepto"].ToString());
                    }

                    bool isEmpty = !columnasDinamicas.Any();
                    int limite = 0;
                    var count = 0;

                    if (!isEmpty)
                    {
                        limite = columnasDinamicas.Count;
                    }

                    using (DataTable dtReporteDisminuirInvetario = cn.CargarDatos(queryRepDisminInv))
                    {
                        queryCrearRemplazarView = "CREATE OR REPLACE VIEW ReporteDisminuirInventarioExtended AS SELECT repDisInv.id, repDisInv.IdProducto AS No, repDisInv.NombreProducto AS Producto, repDisInv.NombreEmisor AS Proveedor, repDisInv.DiferenciaUnidades AS Unidades_Compradas, repDisInv.ValorUnitario AS Precio_Compra, repDisInv.Precio AS Precio_Venta, repDisInv.NuevoStock AS Stock_Anterior, repDisInv.StockActual AS Stock_Actual, repDisInv.Fecha AS Fecha_Compra, repDisInv.Comentarios AS Comentarios, /* repDisInv.Clave, repDisInv.Codigo, repDisInv.NoRevision, repDisInv.StatusActualizacion, repDisInv.IdUsuario, repDisInv.IDEmpleado, repDisInv.NameUsr,repDisInv.Folio, repDisInv.IDConcepto, */ ";

                        foreach (var item in columnasDinamicas)
                        {
                            queryCrearRemplazarView += $" GROUP_CONCAT( DISTINCT IF ( repDisInv.Concepto = \"{item.ToString()}\", repDisInv.Descripcion, NULL ) ) AS {item.ToString()},";
                        }

                        queryCrearRemplazarView = queryCrearRemplazarView.Remove(queryCrearRemplazarView.Length - 1);

                        queryCrearRemplazarView += " FROM ReporteDisminuirInventario AS repDisInv GROUP BY repDisInv.id ORDER BY repDisInv.NombreProducto, repDisInv.StockActual DESC;";
                    }
                }
                else
                {
                    using (DataTable dtReporteDisminuirInvetario = cn.CargarDatos(queryRepDisminInv))
                    {
                        queryCrearRemplazarView = "CREATE OR REPLACE VIEW ReporteDisminuirInventarioExtended AS SELECT repDisInv.id, repDisInv.IdProducto AS No, repDisInv.NombreProducto AS Producto, repDisInv.NombreEmisor AS Proveedor, repDisInv.DiferenciaUnidades AS Unidades_Compradas, repDisInv.ValorUnitario AS Precio_Compra, repDisInv.Precio AS Precio_Venta, repDisInv.NuevoStock AS Stock_Anterior, repDisInv.StockActual AS Stock_Actual, repDisInv.Fecha AS Fecha_Compra, repDisInv.Comentarios AS Comentarios ";

                        foreach (var item in columnasDinamicas)
                        {
                            queryCrearRemplazarView += $" GROUP_CONCAT( DISTINCT IF ( repDisInv.Concepto = \"{item.ToString()}\", repDisInv.Descripcion, NULL ) ) AS {item.ToString()},";
                        }

                        queryCrearRemplazarView = queryCrearRemplazarView.Remove(queryCrearRemplazarView.Length - 1);

                        queryCrearRemplazarView += " FROM ReporteDisminuirInventario AS repDisInv GROUP BY repDisInv.id ORDER BY repDisInv.NombreProducto, repDisInv.StockActual DESC;";
                    }
                }

                cn.crearViewDinamica(queryCrearRemplazarView);

                consulta = queryRepDisminInvExtended;
            }

            return consulta;
        }

        public string getRetriveVersion()
        {
            var consulta = $"SELECT * FROM AppVersionRecord ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string insertAppVersion(string appName, string appVersion, string appMajorVersion, string appMinorVersion, string appBuildNumber, string appRevision, string appDateVersion)
        {
            var consulta = $"INSERT INTO AppVersionRecord(AppName, AppVersion, AppMajorVersion, AppMinorVersion, AppBuildNumber, AppRevision, AppDateVersion) VALUES('{appName}','{appVersion}','{appMajorVersion}','{appMinorVersion}','{appBuildNumber}','{appRevision}','{appDateVersion}')";

            return consulta;
        }

        public string isExistsTable(string tableName)
        {
            var consulta = $@"SELECT COUNT( TABLE_NAME ) Resultado FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'pudve' AND TABLE_NAME = '{tableName}';";

            return consulta;
        }

        public string getRazonNombreRfcCliente(string idCliente)
        {
            var consulta = $"SELECT * FROM Clientes WHERE ID = {idCliente}";

            return consulta;
        }

        public string getDatosClienteVentas(string nombreCliente, string fechaInicial, string fechaFinal)
        {
            var consulta = $"SELECT Cliente, RFC FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND ( Cliente LIKE '%{nombreCliente}%' OR RFC LIKE '%{nombreCliente}%' ) LIMIT 1";

            return consulta;
        }

        public string UsuarioRazonSocialNombreCompleto(string _idUsr)
        {
            var consulta = $"SELECT Usuario, RazonSocial, NombreCompleto FROM Usuarios WHERE ID = {_idUsr}";

            return consulta;
        }

        public string getDatosServCombo(string idServcio)
        {
            var consulta = $"SELECT DISTINCT servProd.Fecha, servProd.IDServicio, servProd.Cantidad FROM productosdeservicios AS servProd WHERE servProd.IDServicio = '{idServcio}' ORDER BY servProd.Fecha DESC LIMIT 1;";

            return consulta;
        }

        public string getBasculasRegistradas(int IdUsr)
        {
            var consulta = $"SELECT nombreBascula FROM basculas WHERE IdUsuario = '{IdUsr}' ORDER BY nombreBascula ASC";

            return consulta;
        }


        public string getDatosProducto(string idProducto)
        {
            var consulta = $"SELECT prod.ID, prod.Nombre FROM productos AS prod WHERE prod.ID = '{idProducto}' AND prod.IDUsuario = '{FormPrincipal.userID}' AND prod.`Status` = '1' AND prod.Tipo = 'P'";

            return consulta;
        }

        public string getDatosBasculaRegistrada(string nameWeighingMachine)
        {
            var consulta = $"SELECT * FROM basculas WHERE IdUsuario = '{FormPrincipal.userID}' AND nombreBascula = '{nameWeighingMachine}'";

            return consulta;
        }

        public string insertarProductosServicios(string[] datos)
        {
            var consulta = $"INSERT INTO productosdeservicios (Fecha, IDServicio, IDProducto, NombreProducto, Cantidad) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}')";

            return consulta;
        }


        public string getTodasLasBasculas()
        {
            var consulta = $"SELECT idBascula, nombreBascula FROM basculas WHERE IdUsuario = '{FormPrincipal.userID}' ORDER BY nombreBascula ASC";

            return consulta;
        }

        public string gardarBascula(string[] datos)
        {
            var consulta = $"INSERT INTO basculas (nombreBascula, puerto, baudRate, dataBits, handshake, parity, stopBits, sendData, idUsuario) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}')";

            return consulta;
        }

        public string editarBascula(string[] datos, int idBascula)
        {
            var consulta = $"UPDATE basculas SET nombreBascula = '{datos[0]}', puerto = '{datos[1]}', baudRate = '{datos[2]}', dataBits = '{datos[3]}', handshake = '{datos[4]}', parity = '{datos[5]}', stopBits = '{datos[6]}', sendData = '{datos[7]}' WHERE idUsuario = '{datos[8]}' AND idBascula = '{idBascula}'";

            return consulta;
        }

        public string getBasculaPredeterminada()
        {
            var consulta = $"SELECT * FROM basculas WHERE idUsuario = '{FormPrincipal.userID}' AND predeterminada = '1'";

            return consulta;
        }

        public string resetBasculaPredeterminada()
        {
            var consulta = $"UPDATE basculas SET predeterminada = '0' WHERE idUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string setBAsculaPredeterminada(int idBascula)
        {
            var consulta = $"UPDATE basculas SET predeterminada = '1' WHERE idUsuario = '{FormPrincipal.userID}' AND idBascula = '{idBascula}'";

            return consulta;
        }

        public string productos_relacionados(int id)
        {
            string cons = $"SELECT IDProducto, Cantidad FROM productosdeservicios WHERE IDServicio='{id}'";

            return cons;
        }

        public string borrarProdRelBlanco(int idServCombo)
        {
            var consulta = $"DELETE FROM productosdeservicios WHERE IDServicio = '{idServCombo}' AND IDProducto = '0' AND NombreProducto = ''";

            return consulta;
        }

        public string registrarBasculas(int IdUsuario)
        {
            var consulta = $@"INSERT IGNORE INTO basculas ( nombreBascula, puerto, baudRate, dataBits, handshake, parity, stopBits, sendData, idUsuario, predeterminada ) VALUES 	( 'TORREY L-PCR-20 KG', 'COM3', '115200', '8 bit', 'None', 'None', 'One', 'P', {IdUsuario}, 0 ), 	( 'TORREY L-PCR-40 KG', 'COM3', '115200', '8 bit', 'None', 'None', 'One', 'P', {IdUsuario}, 1 )";

            return consulta;
        }

        public string ObtenerProdDeLaVenta(int idVenta)
        {
            var consulta = $@"SELECT DISTINCT Prod.ID, Prod.Nombre, Prod.Stock, SaleProd.Cantidad FROM detallesventa AS SaleDetail INNER JOIN ventas AS Sale ON Sale.ID = SaleDetail.IDVenta INNER JOIN usuarios AS Usr ON Usr.ID = Sale.IDUsuario INNER JOIN productosventa AS SaleProd ON SaleProd.IDVenta = Sale.ID  INNER JOIN productos AS Prod ON Prod.ID = SaleProd.IDProducto WHERE SaleDetail.IDVenta = '{idVenta}' GROUP BY Prod.ID";

            return consulta;
        }

        public string aumentarStockVentaCancelada(int idProd, float cantidad)
        {
            var consulta = $"UPDATE Productos SET Stock = '{cantidad}' WHERE ID = '{idProd}' AND IDUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string searchProductList(string typeToSearch, string busqueda)
        {
            var consulta = $"SELECT DISTINCT P.ID, P.Nombre, P.Stock, P.Precio, IF ( P.Categoria = 'PAQUETES', 'SERVICIOS', P.Categoria ) AS Categoria, P.CodigoBarras FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = U.ID LEFT JOIN CodigoBarrasExtras AS BarCodExt ON BarCodExt.IDProducto = P.ID WHERE U.ID = '{FormPrincipal.userID}' AND P.STATUS = '1' AND ( { typeToSearch } ) AND ( P.Nombre LIKE '%{busqueda}%' OR P.NombreAlterno1 LIKE '%{busqueda}%' OR P.NombreAlterno2 LIKE '%{busqueda}%' OR P.CodigoBarras LIKE '%{busqueda}%' OR BarCodExt.CodigoBarraExtra LIKE '%{busqueda}%' ) GROUP BY P.Nombre ASC; ";

            return consulta;
        }

        public string searchSaleProduct(string busqueda, string tipo)
        {
            var consulta = $"SELECT DISTINCT  Prod.ID, Prod.Nombre, Prod.Stock, Prod.Precio, Prod.ClaveInterna, Prod.CodigoBarras, Prod.Tipo, IFNULL( ProdDetail.Proveedor, 'N/A' ) AS Proveedor, GralDetail.ChckName, GralDetail.Descripcion FROM Productos AS Prod LEFT JOIN Usuarios AS Usr ON Usr.ID = Prod.IDUsuario LEFT JOIN DetallesProducto AS ProdDetail ON ProdDetail.IDProducto = Prod.ID LEFT JOIN Proveedores AS Prov ON Prov.ID = ProdDetail.IDProveedor LEFT JOIN DetallesProductoGenerales AS GralProdDetail ON GralProdDetail.IDProducto = Prod.ID LEFT JOIN DetalleGeneral AS GralDetail ON GralDetail.ID = GralProdDetail.IDDetalleGral LEFT JOIN AppSettings AS AppSet ON AppSet.IDUsuario = GralDetail.IDUsuario WHERE Usr.ID = '{FormPrincipal.userID}' AND Prod.`Status` = '1' AND Tipo = '{tipo}' AND ( Prod.Nombre LIKE '%{busqueda}%' OR Prod.NombreAlterno1 LIKE '%{busqueda}%' OR Prod.NombreAlterno2 LIKE '%{busqueda}%' OR Prod.ClaveInterna LIKE '%{busqueda}%' OR Prod.CodigoBarras LIKE '%{busqueda}%' ) ORDER BY Prod.Nombre ASC; ";

            return consulta;
        }
        public string searchSaleProductAll(string busqueda)
        {
            var consulta = $"SELECT DISTINCT  Prod.ID, Prod.Nombre, Prod.Stock, Prod.Precio, Prod.ClaveInterna, Prod.CodigoBarras, Prod.Tipo, IFNULL( ProdDetail.Proveedor, 'N/A' ) AS Proveedor, GralDetail.ChckName, GralDetail.Descripcion FROM Productos AS Prod LEFT JOIN Usuarios AS Usr ON Usr.ID = Prod.IDUsuario LEFT JOIN DetallesProducto AS ProdDetail ON ProdDetail.IDProducto = Prod.ID LEFT JOIN Proveedores AS Prov ON Prov.ID = ProdDetail.IDProveedor LEFT JOIN DetallesProductoGenerales AS GralProdDetail ON GralProdDetail.IDProducto = Prod.ID LEFT JOIN DetalleGeneral AS GralDetail ON GralDetail.ID = GralProdDetail.IDDetalleGral LEFT JOIN AppSettings AS AppSet ON AppSet.IDUsuario = GralDetail.IDUsuario WHERE Usr.ID = '{FormPrincipal.userID}' AND Prod.`Status` = '1' AND ( Prod.Tipo = 'P' OR Prod.Tipo = 'PQ' OR Prod.Tipo = 'S' ) AND ( Prod.Nombre LIKE '%{busqueda}%' OR Prod.NombreAlterno1 LIKE '%{busqueda}%' OR Prod.NombreAlterno2 LIKE '%{busqueda}%' OR Prod.ClaveInterna LIKE '%{busqueda}%' OR Prod.CodigoBarras LIKE '%{busqueda}%' ) ORDER BY Prod.Nombre ASC; ";

            return consulta;
        }

        public string VerificarContenidoDinamicoInhabilitado(int idUsuario)
        {
            var consulta = $"SELECT ID, concepto AS Concepto, IDUsuario AS Usuario FROM appSettings WHERE IDUsuario = '{idUsuario}' AND Mostrar = 0 ORDER BY Concepto ASC;";

            return consulta;
        }

        public string verificarConSinClaveInterna(int idUsuario)
        {
            var consulta = $"SELECT ID, Usuario, SinClaveInterna FROM usuarios WHERE ID = '{idUsuario}'; ";

            return consulta;
        }

        public string habilitarConceptoDinamico(int idRegistro)
        {
            var consulta = $"UPDATE appSettings SET Mostrar = 1 WHERE ID = '{idRegistro}'";

            return consulta;
        }

        public string VerificarContenidoDinamicoHabilitado(int idUsuario)
        {
            var consulta = $"SELECT ID, concepto AS Concepto, IDUsuario AS Usuario FROM appSettings WHERE IDUsuario = '{idUsuario}' AND Mostrar = 1 AND concepto != 'Proveedor' ORDER BY Concepto ASC;";

            return consulta;
        }

        public string inhabilitarConceptoDinamico(int idRegistro)
        {
            var consulta = $"UPDATE appSettings SET Mostrar = 0 WHERE ID = '{idRegistro}'";

            return consulta;
        }

        public string BusarContenidoDinamicoHabilitado(int idConcepto)
        {
            var consulta = $"SELECT ID, concepto AS Concepto, IDUsuario AS Usuario FROM appSettings WHERE ID = '{idConcepto}' AND IDUsuario = '{FormPrincipal.userID}' AND Mostrar = 1 ORDER BY Concepto ASC;";

            return consulta;
        }

        public string aumentoContadorSesiones(int id)
        {
            var consulta = $"UPDATE usuarios SET ConteoInicioDeSesion = ConteoInicioDeSesion +1 WHERE ID = '{id}'";
            return consulta;
        }

        public string registroSesiones(string nombre, string fecha, string correo)
        {
            var consulta = $"INSERT INTO iniciosDeSesion (Usuario, Fecha, Correo) VALUES ('{nombre}', '{fecha}', '{correo}')";
            return consulta;
        }

        public string consultaInicios(string nombre)
        {
            var consulta = $"SELECT * FROM `iniciosdesesion` WHERE Usuario LIKE '%'{nombre}'%'";
            return consulta;
        }

        public string busquedaEmpleado(string busqueda, int status)
        {
            var consulta = $"SELECT ID, nombre, usuario FROM `empleados` WHERE usuario LIKE '%{busqueda}%' AND estatus = '{status}' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string deshabilitarEmpleado(string usuario, string idEmpleado)
        {
            var consulta = $"UPDATE `empleados` SET estatus = 0 WHERE usuario = '{usuario}' AND IDUsuario = '{FormPrincipal.userID}' AND ID = '{idEmpleado}'";
            return consulta;
        }

        public string habilitarEmpleado(string usuario, string idEmpleado)
        {
            var consulta = $"UPDATE `empleados` SET estatus = 1 WHERE usuario = '{usuario}' AND IDUsuario = '{FormPrincipal.userID}' AND ID = '{idEmpleado}'";
            return consulta;
        }

        public string mostrarUsuarios(string estado)
        {
            var consulta = $"SELECT ID, nombre, usuario FROM `empleados` WHERE estatus = '{estado}' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string buscarProductosDeServicios(int idProd)
        {
            var consulta = $"SELECT DISTINCT ID, IDServicio, Cantidad FROM productosdeservicios WHERE IDServicio = '{idProd}';";

            return consulta;
        }

        public string obtenerServicioCombo(int idServCombo)
        {
            var consulta = $"SELECT DISTINCT Prod.Nombre, IF(Prod.Tipo = 'S', 'SERVICIO', 'COMBO') AS Tipo  FROM productos AS Prod INNER JOIN ProductosDeServicios AS ServicesProd ON Prod.ID = ServicesProd.IDServicio WHERE ServicesProd.IDServicio = '{idServCombo}';";

            return consulta;
        }

        public string buscarProdIntoServComb(int idProd)
        {
            var consulta = $"SELECT DISTINCT ServProds.ID, ServProds.Fecha, ServProds.IDServicio NoServicio, Prod.Nombre ServicioCombo, ServProds.IDProducto NoProducto, ServProds.NombreProducto Producto, ServProds.Cantidad, IF(Prod.Tipo = 'S', 'SERVICIO', 'COMBO') AS Tipo FROM productosdeservicios AS ServProds INNER JOIN Productos AS Prod ON Prod.ID = ServProds.IDServicio WHERE ServProds.IDServicio = '{idProd}' OR ServProds.IDProducto = '{idProd}'  ORDER BY Prod.Nombre ASC; ";

            return consulta;
        }

        public string buscarEditProductosDeServicios(int idServ)
        {
            var consulta = $"SELECT DISTINCT ServProd.ID, ServProd.Fecha, ServProd.IDServicio NoServicio, Prod.Nombre ServicioCombo, ServProd.IDProducto NoProducto, ServProd.NombreProducto Producto, ServProd.Cantidad, IF(Prod.Tipo = 'PQ', 'COMBO', 'SERVICIO') AS Tipo FROM productosdeservicios AS ServProd INNER JOIN Productos AS Prod ON Prod.ID = ServProd.IDServicio WHERE ServProd.IDServicio = '{idServ}' ORDER BY ServProd.NombreProducto ASC; ";

            return consulta;
        }

        public string actualizarRelacionProdComboServicio(int idRelacion, float cantidadRelacion)
        {
            var consulta = $"UPDATE ProductosDeServicios SET Cantidad = '{cantidadRelacion}' WHERE ID = '{idRelacion}';";

            return consulta;
        }

        public string borrarRelacionProdComboServicio(int idReg)
        {
            var consulta = $"DELETE FROM ProductosDeServicios WHERE ID = '{idReg}';";

            return consulta;
        }

        public string verSiExisteRelacionRegistrada(string idServicio, string idProducto)
        {
            var consulta = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idServicio}' AND IDProducto = '{idProducto}'; ";
            return consulta;

        }

        public string permisosAsignar(List<int> opciones, string empleado)
        {
            var consulta = $@"UPDATE empleadospermisos 
            SET mensajeVentas = '{opciones[1]}',
            mensajeInventario = '{opciones[2]}',
            stock = '{opciones[3]}',
            stockMinimo = '{opciones[4]}',
            stockMaximo = '{opciones[5]}',
            precio = '{opciones[6]}',
            numeroRevision = '{opciones[7]}',
            tipoIVA = '{opciones[8]}',
            claveProducto = '{opciones[9]}',
            claveUnidad = '{opciones[10]}',
            correos = '{opciones[11]}' 
            WHERE
	        IDUsuario = '{FormPrincipal.userID}' 
	        AND IDEmpleado = '{empleado}'";
            return consulta;
        }

        public string condicionAsignar(string nomAsignar, string idEmpleado)
        {
            var consulta = $"SELECT COUNT({nomAsignar}) AS total FROM empleadospermisos WHERE IDEmpleado = '{idEmpleado}' AND IDUsuario = '{FormPrincipal.userID}' AND {nomAsignar} = 1 ";
            return consulta;
        }

        public string buscarDatoDinamicoPorIDRegistro(int idReg)
        {
            var consulta = $"SELECT * FROM appSettings WHERE ID = '{idReg}'";

            return consulta;
        }

        public string validarUsuario(string usuario)
        {
            var consulta = $"SELECT usuario FROM usuarios WHERE usuario = '{usuario}'";
            return consulta;
        }

        public string checarSiExisteRelacionProducto(int idProd)
        {
            var consulta = $"SELECT IDServicio, IDProducto, NombreProducto FROM productosdeservicios WHERE IDProducto = '{idProd}'; ";

            return consulta;
        }

        
        public string checarProductoEstaActivo(string idProd)
        {
            var consulta = $"SELECT ID, Nombre, Precio, `Status` FROM Productos WHERE ID = '{idProd}' AND `Status` = '1' AND IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public int buscarEstadoProducto(int idPRoducto)
        {
            int result = 0;

            var query = cn.CargarDatos($"SELECT Status FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idPRoducto}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = Convert.ToInt32(query.Rows[0]["Status"].ToString());
            }

            return result;


        }

        public string checarSiExisteRelacionComboServ(int idProdComSer, int idSeleccionado)
        {
            var consulta = $"SELECT * FROM productosdeservicios AS servProd WHERE servProd.IDServicio = '{idProdComSer}' AND servProd.IDProducto = '{idSeleccionado}'; ";
            return consulta;
        }
        public string agregarDetalleProductoPermisosDinamicos(string detalle)
        {
            var consulta = $"ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS {detalle.ToString()} int DEFAULT 1";
            return consulta;
        }

        public string permisisAsignarDinamicos(string concepto, int value, string idEmpleado)
        {
            var consulta = $"UPDATE empleadospermisos SET {concepto} = '{value}' WHERE IDEmpleado = '{idEmpleado}' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string verificarPermisosDinamicos(int idUsuario)
        {
            var consulta = $"SELECT * FROM appSettings WHERE IDUsuario = '{idUsuario}' AND Mostrar = 1 ";

            return consulta;
        }

        public string consultaRelacionServicioParaProducto(string idServicio)
        {
            var consulta = $"SELECT Nombre, IF ( Tipo = 'S', 'SERVICIO', 'COMBO' ) AS Tipo FROM Productos WHERE ID = '{idServicio}'";

            return consulta;
        }

        public string RegistroIniciosDeSesiones()
        {
            var consulta = "SELECT * FROM iniciosdesesion ORDER BY Fecha DESC";
            return consulta;
        }

        public string busquedaIniciosDeSesion(string busqueda)
        {
            var consulta = $"SELECT * FROM iniciosdesesion WHERE usuario LIKE '%{busqueda}%' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string encontrarProductoComboServicio(int idProd)
        {
            var consulta = $"SELECT ID, Nombre, Status, Tipo FROM Productos WHERE ID = '{idProd}' AND Status = '1' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string ObtenerServPaqRelacionados(string idServPQ)
        {
            var consulta = $"SELECT * FROM ProductosDeServicios WHERE IDServicio = '{idServPQ}' AND IDProducto <> 0";

            return consulta;
        }

        public string obtenerProdRelacionados(string idProd)
        {
            var consulta = $"SELECT * FROM ProductosDeServicios WHERE IDProducto = '{idProd}'";

            return consulta;
        }

        public string obtenerIDUsuario(string usuario, string contraseña)
        {
            var consulta = $"SELECT ID FROM Usuarios WHERE Usuario = '{usuario}' AND Password = '{contraseña}'";
            return consulta;
        }

        public bool validarInformacion(string procedencia, string idEmp, string fechaInicial, string fechaFinal)
        {
            bool result = false;

            var consulta = string.Empty;

            if (procedencia.Equals("Seleccionar Empleado/Producto"))//consulta Normal
            {
                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion DESC";
            }
            else if (procedencia.Equals("Empleados"))// Consulta segun empleado
            {
                var validarId = string.Empty;
                if (!string.IsNullOrEmpty(idEmp))
                {
                    validarId = idEmp;
                }

                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDEmpleado IN ({validarId}) ORDER BY FechaOperacion DESC";
            }
            else if (procedencia.Equals("Productos"))//Consulta por producto
            {
                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDProducto IN ({idEmp}) ORDER BY FechaOperacion DESC";
            }

            var datosVerificar = cn.CargarDatos(consulta);

            if (!datosVerificar.Rows.Count.Equals(0))
            {
                result = true;
            }
            else
            {
                result = false;
            }


            return result;
        }

        public string ventaGuardadaEstaTimbrada(int idVenta)
        {
            var consulta = $"SELECT ID, IDUsuario, IDCliente, IDEmpleado, IDSucursal, Folio, Serie, `Status`, Timbrada, Cancelada, FechaOperacion FROM ventas WHERE ID = '{idVenta}' AND IDUsuario = '{FormPrincipal.userID}' AND `Status` = '2';";

            return consulta;
        }

        public string checarProductosVenta(string idVenta)
        {
            var consulta = $"SELECT * FROM ProductosVenta WHERE IDVenta='{idVenta}'";

            return consulta;
        }

        public string consultarBuscarProductoXML()
        {
            var consulta = $"SELECT u.ID, u.Usuario, u.Password, u.RFC FROM Usuarios u WHERE u.ID = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string IDUsuarioSinContraseña(string usuario)
        {
            var consulta = $"SELECT ID FROM Usuarios WHERE Usuario = '{usuario}'";
            return consulta;
        }

        public string quitarSimboloRaroEspacios()
        {
            var comillas = '"';
            var abrirLlave = '{';
            var cerrarLlave = '}';
            var quitarTildeEnNMayuscula = 'Ñ';
            var quitarTildeEnNMinuscula = 'ñ';
            var consulta = $@"UPDATE AppSettings SET concepto = REPLACE ( concepto, '\'', '' ) WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '\\', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '?', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '/', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '.', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '>', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '<', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, ':', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '|', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, ']', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '[', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '+', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '=', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, ')', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '(', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '*', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '&', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '^', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '%', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '$', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '#', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '@', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '!', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '~', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '`', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE AppSettings SET concepto = REPLACE(concepto, '\'', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE AppSettings SET concepto = REPLACE(concepto, '{comillas}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '{abrirLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, '{cerrarLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE AppSettings SET concepto = REPLACE(concepto, ' ', '_') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '\'', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '\\', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '?', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '/', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '.', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '>', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '<', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, ':', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '|', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, ']', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '[', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '+', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '=', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, ')', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '(', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '*', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '&', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '^', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '%', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '$', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '#', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '@', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '!', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '~', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '`', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '\'', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '{comillas}', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '{abrirLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, '{cerrarLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE AppSettings SET textComboBoxConcepto = REPLACE(textComboBoxConcepto, ' ', '_') WHERE IDUsuario = '{FormPrincipal.userID}'; 
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '\'', '' ) WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '\\', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '?', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '/', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '.', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '>', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '<', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, ':', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '|', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, ']', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '[', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '+', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '=', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, ')', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '(', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '*', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '&', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '^', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '%', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '$', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '#', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '@', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '!', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '~', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '`', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '\'', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '{comillas}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '{abrirLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, '{cerrarLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetallesProductoGenerales SET panelContenido = REPLACE ( panelContenido, ' ', '_') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '\'', '' ) WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '\\', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '?', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '/', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '.', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '>', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '<', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, ':', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '|', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, ']', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '[', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '+', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '=', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, ')', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '(', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '*', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '&', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '^', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '%', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '$', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '#', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '@', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '!', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '~', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '`', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '\'', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '{comillas}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '{abrirLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, '{cerrarLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetalleGeneral SET ChckName = REPLACE ( ChckName, ' ', '_') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '\'', '' ) WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '\\', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '?', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '/', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '.', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '>', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '<', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, ':', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, ',', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '|', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, ']', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '[', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '+', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '=', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, ')', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '(', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '*', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '&', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '^', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '%', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '$', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '#', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '@', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '!', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '~', '') WHERE IDUsuario = '{FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '`', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '\'', '') WHERE IDUsuario = '{FormPrincipal.userID}';
			UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '{comillas}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '{abrirLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, '{cerrarLlave}', '') WHERE IDUsuario = '{ FormPrincipal.userID}';
            UPDATE DetalleGeneral SET Descripcion = REPLACE ( Descripcion, ' ', '_') WHERE IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public string verificarCuentaVieja()
        {
            var consulta = $"SELECT * FROM Usuarios WHERE ID = '{FormPrincipal.userID}' AND SinClaveInterna = '1';";

            return consulta;
        }

        public string moverClaveInternaHaciaCodigoBarraExtra()
        {
            var consulta = $"INSERT INTO codigobarrasextras ( CodigoBarraExtra, IDProducto ) SELECT Prod.ClaveInterna, Prod.ID FROM productos AS Prod INNER JOIN usuarios AS Usr ON Prod.IDUsuario = Usr.ID WHERE Usr.ID = '{FormPrincipal.userID}' AND Usr.SinClaveInterna = '1' AND Prod.ClaveInterna <> '';";

            return consulta;
        }

        public string quitarContenidoClaveInterna()
        {
            var consulta = $"UPDATE productos SET ClaveInterna = '' WHERE IDUsuario = '{FormPrincipal.userID}' AND ClaveInterna <> '';";

            return consulta;
        }

        public string configurarUsuarioParaObtenerCuentaNueva()
        {
            var consulta = $"UPDATE usuarios SET SinClaveInterna = 0 WHERE ID = '{FormPrincipal.userID}' AND SinClaveInterna = '1';";

            return consulta;
        }

        public string quitarCodigoBarraExtraVacios()
        {
            var consulta = $"DELETE FROM codigobarrasextras WHERE CodigoBarraExtra IS NULL OR CodigoBarraExtra = '';";

            return consulta;
        }

        public string buscarProductoDesdeXML(string userId, string busca_claveinterna)
        {
            var consulta = $"SELECT prod.ID,prod.Nombre,prod.Stock,prod.ClaveInterna, prod.CodigoBarras,prod.Precio,prod.Tipo,prod.Status, codbarext.CodigoBarraExtra,codbarext.IDProducto FROM Productos prod LEFT JOIN CodigoBarrasExtras codbarext ON codbarext.IDProducto = prod.ID WHERE prod.IDUsuario = '{userId}' AND prod.Status = 1 AND (prod.CodigoBarras = '{busca_claveinterna}' OR prod.ClaveInterna = '{busca_claveinterna}' OR codbarext.CodigoBarraExtra = '{busca_claveinterna}')";

            return consulta;
        }

        public string actualizarStockProdServCombo(decimal Cantidad, int IdProducto)
        {
            var consulta = $"UPDATE Productos SET Stock = Stock + {Cantidad} WHERE ID = '{IdProducto}';";

            return consulta;
        }

        public string obtenerIDProveedor(string nombreProveedor)
        {
            var consulta = $"SELECT * FROM Proveedores WHERE Nombre = '{nombreProveedor}' AND IDUsuario = '{FormPrincipal.userID}' AND Status = '1'";

            return consulta;
        }

        public string obtenerIDDetalleGeneral(string nombreDetalleGeneral)
        {
            var consulta = $"SELECT * FROM DetalleGeneral WHERE Descripcion = '{nombreDetalleGeneral}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string reporteActualizarInventarioDesdeXML(int IdUsuario, int IdReporte)
        {
            var consulta = $"SELECT Concepto AS Producto, Cantidad AS UnidadesCompradas, ValorUnitario AS PrecioCompra, Descuento, Precio AS PrecioVenta, FechaOperacion, NomEmisor As Proveedor, RFCEmisor AS RFC, IDReporte AS Reporte FROM HistorialCompras WHERE IDUsuario = {IdUsuario} AND IDReporte = {IdReporte};";

            return consulta;
        }

        public string cantidadDeComboServicio(int IdComboServicio)
        {
            var consulta = $"SELECT * FROM productosdeservicios WHERE IDServicio = '{IdComboServicio}' ORDER BY Fecha ASC LIMIT 1;";

            return consulta;
        }

        public string nombreTipoDelProducto(int idProducto)
        {
            var consulta = $"SELECT DISTINCT Prod.Nombre, IF( Prod.Tipo = 'PQ', 'COMBO', 'SERVICIO' ) AS Tipo FROM productos AS Prod INNER JOIN ProductosDeServicios AS ServicesProd ON Prod.ID = ServicesProd.IDServicio WHERE ServicesProd.IDServicio = '{idProducto}'; ";

            return consulta;
        }

        public string insertarMensajeDeTicket(int idUsuario, string mensaje)
        {
            string consulta = "INSERT INTO editarticket (IDUsuario, mensajeTicket)";
            consulta += $"VALUES ('{idUsuario}', '{mensaje}')";
            return consulta;
        }

        public string editarMensajeDeTicket(int idUsuario, string mensaje)
        {
            string consulta = $"UPDATE editarticket SET MensajeTicket = '{mensaje}' where IDUsuario = '{idUsuario}'";
            return consulta;
        }

        public string consultarMensajeTicket(int idUsuario)
        {
            string consulta = $"SELECT COUNT(MensajeTicket) AS mensaje FROM editarticket WHERE IDUsuario = '{idUsuario}'; ";
            return consulta;
        }

        public string MensajeTicket(int idUsuario)
        {
            string consulta = $"SELECT MensajeTicket FROM editarticket WHERE IDUsuario = '{idUsuario}'; ";
            return consulta;
        }

        public string lotipoTicket(int logo)
        {
            var consulta = $"UPDATE editarticket SET logo = {logo} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string nombreusTicket(int nombre)
        {
            var consulta = $"UPDATE editarticket SET Usuario = {nombre} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }
        public string direccionTicket(int direccion)
        {
            var consulta = $"UPDATE editarticket SET Direccion = {direccion} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string colycpTicket(int colycp)
        {
            var consulta = $"UPDATE editarticket SET ColyCP = {colycp} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string rfcTicket(int RFC)
        {
            var consulta = $"UPDATE editarticket SET RFC = {RFC} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string correoTicket(int Correo)
        {
            var consulta = $"UPDATE editarticket SET Correo = {Correo} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string telTicket(int telefono)
        {
            var consulta = $"UPDATE editarticket SET Telefono = {telefono} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }
        //Updates cliente

        public string nombreCTicket(int nombre)
        {
            var consulta = $"UPDATE editarticket SET NombreC = {nombre} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string domicilioCTicket(int domicilio)
        {
            var consulta = $"UPDATE editarticket SET DomicilioC = {domicilio} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string rfcCTicket(int rfc)
        {
            var consulta = $"UPDATE editarticket SET RFCC = {rfc} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string correoCTicket(int correo)
        {
            var consulta = $"UPDATE editarticket SET CorreoC = {correo} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string telefonoCTicket(int telefono)
        {
            var consulta = $"UPDATE editarticket SET TelefonoC = {telefono} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string nombreComercial(int nomComercial)
        {
            var consulta = $"UPDATE editarticket SET NombreComercial = {nomComercial} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string formapagoCTicket(int formapago)
        {
            var consulta = $"UPDATE editarticket SET FormaPagoC = {formapago} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string colycpCTicket(int colycp)
        {
            var consulta = $"UPDATE editarticket SET ColyCPC = {colycp} WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string checkboxTicket()
        {
            var consulta = $"SELECT * FROM `editarticket` WHERE IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }
        public string consultarEstadoTicket(string apartado, int user)
        {
            var consulta = $"SELECT '{apartado}' FROM `editarticket` WHERE IDUsuario = '{FormPrincipal.userID}';";
            return consulta;
        }

        public string verConceptosDinamicosActivos()
        {
            var consulta = $"SELECT * FROM appsettings WHERE IDUsuario = '{FormPrincipal.userID}' AND checkBoxConcepto = '1' AND Mostrar = '1' AND concepto <> 'Proveedor';";

            return consulta;
        }
        public string CerrarSesionConCorte()
        {
            var consulta = $"SELECT CerrarSesionAuto FROM `configuracion` WHERE IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string actualizarDatosVenta(float dineroRecibido, float CambioTotal, int IdVenta)
        {
            var consulta = $"UPDATE ventas SET DineroRecibido = '{dineroRecibido}', CambioTotal = '{CambioTotal}' WHERE ID = '{IdVenta}' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string validarCerrarSesionCorteCaja()
        {
            var consulta = $"SELECT IDUsuario, CerrarSesionAuto FROM configuracion WHERE IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public string cargarDatosDeConfiguracion()
        {
            var consulta = $"SELECT IDUsuario, TicketVenta, IniciarProceso, MostrarCodigoProducto, CerrarSesionAuto, MostrarPrecioProducto, StockNegativo, HabilitarTicketVentas, PrecioMayoreo, checkNoVendidos FROM Configuracion WHERE IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string PermisosConfiguracionEmpleados(int idEmpleado)
        {
            var consulta = $"SELECT IDEmpleado from permisosconfiguracion WHERE IDEmpleado = {idEmpleado}";
            return consulta;
        }

        public string permisosEmpleado(string datosPermisos, int id_empleado)
        {
            var consulta = $"SELECT {datosPermisos} FROM permisosconfiguracion WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1";
            return consulta;
        }

        public string PermisosEmpleadosSetupPudve(int idEmpleado, string dato)
        {
            var consulta = $"SELECT {dato} FROM empleadospermisos WHERE IDEmpleado = '{idEmpleado}' AND IDUsuario = '{FormPrincipal.userID}' AND Seccion = 'Configuracion'";
            return consulta;
        }

        public string actualizarCantidadRelacionProdComboServicio(int idRelacion, float cantidadRelacion, string idProd)
        {
            var consulta = $"UPDATE ProductosDeServicios SET Cantidad = '{cantidadRelacion}' WHERE IDServicio = '{idRelacion}' AND  IDProducto = '{idProd}'";

            return consulta;
        }

        public string obtenerNombreDelProducto(string idProducto)
        {
            var consulta = $"SELECT ID, Nombre FROM productos WHERE ID = '{idProducto}';";

            return consulta;
        }

        public string obtenerCantidadProductosDeServicios(string idComboServ)
        {
            var consulta = $"SELECT ID, Cantidad FROM ProductosDeServicios WHERE IDServicio = '{idComboServ}'";

            return consulta;
        }

        public string obtenerStcokNegativoConfiguracion()
        {
            var consulta = $"SELECT IDUsuario, StockNegativo FROM configuracion WHERE IDUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string consultaReporteGeneralRevisarInventario()
        {
            var consulta = $"SELECT NumFolio, NoRevision, NameUsr, Fecha FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' GROUP BY NoRevision ORDER BY Fecha DESC";

            return consulta;
        }

        public string mensajeInventario(int idProductoSeleccionado)
        {
            var consulta = $"SELECT Mensaje FROM `mensajesinventario` WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {idProductoSeleccionado}";

            return consulta;
        }

        public string consultaReporteGeneralAumentarInventario()
        {
            var consulta = $"SELECT NoRevision, IDEmpleado, Fecha, Folio FROM dgvaumentarinventario WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio != 0 GROUP BY NoRevision ORDER BY Fecha DESC";
            return consulta;

        }

        public string mensajeVentas(int idProductoSeleccionado)
        {
            var consulta = $"SELECT ProductOfMessage FROM productmessage WHERE IDProducto = {idProductoSeleccionado}";

            return consulta;
        }

        public string consultaReporteGeneralDisminuirInventario()
        {
            var consulta = $"SELECT NoRevision, IDEmpleado, Fecha, Folio FROM dgvdisminuirinventario WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio != 0 GROUP BY NoRevision ORDER BY Fecha DESC";

            return consulta;
        }

        public string actualizarMensajeInventario(int idProductoSeleccionado, string mensaje, int estado)
        {
            var consulta = $"UPDATE mensajesInventario SET Mensaje = '{mensaje}', Activo = '{estado}' WHERE IDProducto = {idProductoSeleccionado}";

            return consulta;
        }

        public string insertarMensajeInventario(int idProductoSeleccionado, string mensaje, int estado)
        {
            var consulta = $"INSERT INTO mensajesinventario (IDUsuario, IDProducto, mensaje, Activo) VALUES ('{FormPrincipal.userID}', '{idProductoSeleccionado}', '{mensaje}', {estado});";

            return consulta;
        }

        public string regenerarReporteGeneralRevisarInventario(int numeroRevision, int idUsuario, int numeroFolio)
        {
            var consulta = $"SELECT * FROM revisarinventarioreportes WHERE IDUsuario = '{idUsuario}' AND NoRevision = '{numeroRevision}' AND NumFolio = '{numeroFolio}' ORDER BY Fecha DESC;";

            return consulta;
        }
        public string actualizarMensajeVentas(int idProductoSeleccionado, string mensaje, int estado)
        {
            var consulta = $"UPDATE productmessage SET ProductOfMessage = '{mensaje}', ProductMessageActivated = {estado} WHERE IDProducto = {idProductoSeleccionado}";

            return consulta;
        }

        public string insertarMensajeVenta(int idProducto, int estado, string mensaje)
        {
            var consulta = $"INSERT INTO productmessage (ProductOfMessage, ProductMessageActivated, IDProducto) VALUES ('{mensaje}',{estado} ,'{idProducto}')";

            return consulta;
        }

        public string sacarFechaReporte(int numeroRevision, int numeroFolio)
        {
            var consulta = $"SELECT RevInvReport.Fecha, RevInvReport.TipoRevision FROM revisarinventarioreportes AS RevInvReport WHERE RevInvReport.IDUsuario = '{FormPrincipal.userID}' AND RevInvReport.NoRevision = '{numeroRevision}' AND RevInvReport.NumFolio = '{numeroFolio}' ORDER BY Fecha DESC LIMIT 1";

            return consulta;
        }

        public string permisosMensajeVentasInventario()
        {
            var consulta = $"SELECT ProductMessageActivated FROM productmessage";

            return consulta;
        }

        public string permisoRealizarInventario()
        {
            var consulta = $"SELECT Activo FROM mensajesinventario";

            return consulta;
        }

        public string quitarComillasSimplesDeProductos()
        {
            var consulta = $@"UPDATE productos SET Nombre = REGEXP_REPLACE ( Nombre, '\'', '´' ) WHERE IDUsuario = '{FormPrincipal.userID}'; UPDATE productos SET NombreAlterno1 = REGEXP_REPLACE ( NombreAlterno1, '\'', '´' ) WHERE IDUsuario = '{FormPrincipal.userID}'; UPDATE productos SET NombreAlterno2 = REGEXP_REPLACE ( NombreAlterno2, '\'', '´' ) WHERE IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public string viewMensajeVentas(int codProducto)
        {
            var consulta = $"SELECT ProductOfMessage FROM productmessage WHERE IDProducto = {codProducto}";

            return consulta;
        }

        public string viewMensajeInventario(int codProducto)
        {
            var consulta = $"SELECT Mensaje FROM mensajesInventario WHERE IDProducto = {codProducto}";

            return consulta;
        }

        public string mostrarMensajeInventario(int codProducto)
        {
            var consulta = $"SELECT Mensaje FROM mensajesInventario WHERE IDProducto = {codProducto} AND Activo = 1";

            return consulta;
        }

        public string insertarCompraMinima(int idProducto, int cantidad)
        {
            var consulta = $"INSERT INTO productmessage (CantidadMinimaDeCompra, IDProducto) VALUES ('{cantidad}', '{idProducto}')";

            return consulta;
        }

        public string actualizarCompraMinima(int idProductoSeleccionado, string mensaje)
        {
            var consulta = $"UPDATE productmessage SET CantidadMinimaDeCompra = {mensaje} WHERE IDProducto = {idProductoSeleccionado}";

            return consulta;
        }

        public string cantidadCompraMinima(int idProductoSeleccionado)
        {
            var consulta = $"SELECT CantidadMinimaDeCompra FROM productmessage WHERE IDProducto = {idProductoSeleccionado}";

            return consulta;
        }

        public string verificarMensajesProductosVentas(int idProducto)
        {
            var consulta = $"SELECT * FROM  productmessage WHERE IDProducto = '{idProducto}'";

            return consulta;
        }

        public string CargarHistorialDeVentas(string fechaInicial, string fechaFinal, int idProducto)
        {
            var consulta = $"SELECT * FROM ( SELECT Vent.Folio, Vent.Serie, Vent.Total, Vent.FechaOperacion, ProdVent.IDVenta, ProdVent.Nombre, ProdVent.Cantidad, ProdVent.Precio, IF ( Prod.Tipo = 'P', 'Individual', 'Combo / Servicio' ) AS Vendido, Vent.IDEmpleado, ProdVent.IDProducto FROM productos AS Prod INNER JOIN productosventa AS ProdVent ON ( ProdVent.IDProducto = Prod.ID ) INNER JOIN ventas AS Vent ON ( Vent.ID = ProdVent.IDVenta ) WHERE Prod.ID = '{idProducto}' AND Prod.IDUsuario = '{FormPrincipal.userID}' AND DATE( Vent.FechaOperacion ) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND Vent.`Status` = 1 UNION SELECT Vent.Folio, Vent.Serie, Vent.Total, Vent.FechaOperacion, ProdVent.IDVenta, ProdServ.NombreProducto, ProdVent.Cantidad, ProdVent.Precio, IF ( Prod.Tipo = 'P', 'Individual', 'Combo / Servicio' ) AS Vendido, Vent.IDEmpleado, ProdVent.IDProducto FROM productosdeservicios AS ProdServ INNER JOIN productos AS Prod ON ( Prod.ID = ProdServ.IDServicio ) INNER JOIN productosventa AS ProdVent ON ( ProdVent.IDProducto = Prod.ID ) INNER JOIN ventas AS Vent ON ( Vent.ID = ProdVent.IDVenta ) WHERE ProdServ.IDProducto = '{idProducto}' AND Prod.IDUsuario = '{FormPrincipal.userID}' AND DATE( Vent.FechaOperacion ) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND Vent.`Status` = 1 ) AS resultado ORDER BY resultado.Folio,resultado.Serie ASC ";

            return consulta;
        }

        public string cambiarEstadoMensaje(int idProducto, int status)
        {
            var consulta = $"UPDATE productmessage SET ProductMessageActivated = '{status}' WHERE IDProducto = '{idProducto}'";

            return consulta;
        }

        public string cambiarEstadoMensajeInventario(int idProducto, int status)
        {
            var consulta = $"UPDATE mensajesinventario SET Activo = '{status}' WHERE IDProducto = '{idProducto}'";

            return consulta;
        }

        public string verificarEstadoCheckbox(int idProducto)
        {
            var consulta = $"SELECT ProductMessageActivated FROM productmessage WHERE IDProducto = '{idProducto}'";

            return consulta;
        }

        public string verificarEstadoCheckboxInventario(int idProducto)
        {
            var consulta = $"SELECT Activo FROM mensajesinventario WHERE IDProducto = '{idProducto}'";

            return consulta;
        }

        public string actualizarCompraMinimaMultiple(int idProducto, int cantidad)
        {
            var consulta = $"UPDATE productmessage SET CantidadMinimaDeCompra = '{cantidad}' WHERE IDProducto = {idProducto}";

            return consulta;
        }

        public string DatosVentaParaLaNota(int idVenta)
        {
            var consulta = $"SELECT Vent.ID, Vent.Folio, Vent.Serie, Usr.Usuario FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) WHERE Vent.ID = '{idVenta}'";

            return consulta;
        }

        public string obtenerEspecificacionesActivasDetalleDinamico(string chkName)
        {
            var consulta = $"SELECT REPLACE(Descripcion, '_', ' ') AS Concepto FROM DetalleGeneral WHERE IDUsuario = '{FormPrincipal.userID}' AND ChckName = '{chkName}' AND Mostrar = '1' ORDER BY Descripcion ASC";

            return consulta;
        }

        public string agregarEspecificacionAlDetalleDinamico(string detalleDeProducto, string especificacionDetalle)
        {
            var consulta = $"INSERT INTO DetalleGeneral (IDUsuario, ChckName, Descripcion) VALUES ('{FormPrincipal.userID}', '{detalleDeProducto}', '{especificacionDetalle}')";

            return consulta;
        }

        public string especificacionesDetalleDinamicoParaQuitar(string conceptoDinamico)
        {
            var consulta = $"SELECT ID, Descripcion AS Concepto, IDUsuario AS Usuario FROM DetalleGeneral WHERE IDUsuario = '{FormPrincipal.userID}' AND ChckName = '{conceptoDinamico}' AND Mostrar = '1' ORDER BY Concepto ASC";

            return consulta;
        }

        public string inhabilitarEspecificacionConceptoDinamico(int idRegistro)
        {
            var consulta = $"UPDATE DetalleGeneral SET Mostrar = 0 WHERE ID = '{idRegistro}'";

            return consulta;
        }

        public string especificacionesDetalleDinamicoParaActivar(string conceptoDinamico)
        {
            var consulta = $"SELECT ID, Descripcion AS Concepto, IDUsuario AS Usuario FROM DetalleGeneral WHERE IDUsuario = '{FormPrincipal.userID}' AND ChckName = '{conceptoDinamico}' AND Mostrar = '0' ORDER BY Concepto ASC";

            return consulta;
        }

        public string habilitarEspecificacionConceptoDinamico(int idRegistro)
        {
            var consulta = $"UPDATE DetalleGeneral SET Mostrar = 1 WHERE ID = '{idRegistro}'";

            return consulta;
        }

        public string limpiarEspecificacionDetalleProducto(string idProducto, string panelConetnido, string especificacionSelecionada)
        {
            var consulta = $"SELECT * FROM detallesproductogenerales WHERE IDProducto = '{idProducto}' AND panelContenido = '{panelConetnido}' AND IDUsuario = '{FormPrincipal.userID}'";
            //var consulta = $"SELECT DetProdGral.* FROM detallesproductogenerales AS DetProdGral INNER JOIN detallegeneral AS DetGral ON ( DetGral.ID = DetProdGral.IDDetalleGral ) WHERE DetProdGral.IDProducto = '{idProducto}' AND DetProdGral.panelContenido = '{panelConetnido}' AND DetProdGral.IDUsuario = '{FormPrincipal.userID}' AND DetGral.Descripcion = '{especificacionSelecionada}';";

            return consulta;
        }

        public string borrarEspecificacionDetalleProducto(string idDetProdGral)
        {
            var consulta = $"DELETE FROM detallesproductogenerales WHERE ID = '{idDetProdGral}'";

            return consulta;
        }

        public string productosMenosVendidosIncluidoVentasEnCero(string fechaInicio, string fechaFinal)
        {
            var consulta = $"SELECT Prod.ID, Prod.Nombre AS 'ARTICULO', SUM( IF ( ProdVent.Cantidad IS NULL, 0, ProdVent.Cantidad ) ) AS 'VENDIDOS', Prod.CodigoBarras AS 'CODIGO DE BARRAS', ( CASE WHEN Prod.Tipo = 'P' THEN 'PRODUCTO' WHEN Prod.Tipo = 'PQ' THEN 'COMBO' WHEN Prod.Tipo = 'S' THEN 'SERVICIO' END ) AS 'CATEGORIA', IF ( Vent.FechaOperacion IS NULL, 'N/A sin venta registrada', Vent.FechaOperacion ) AS 'ULTIMA VENTA', Prod.Stock AS 'STOCK', Prod.Precio AS 'PRECIO', Prod.`Status`, Prod.IDUsuario FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON ( ProdVent.IDProducto = Prod.ID ) LEFT JOIN Ventas AS Vent ON ( Vent.ID = ProdVent.IDVenta ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{fechaInicio}.999999' AND '{fechaFinal}.999999' AND ( Prod.Tipo = 'P' || Prod.Tipo = 'S' || Prod.Tipo = 'PQ' ) AND Prod.`Status` = 1 OR ( ProdVent.Cantidad IS NULL AND Prod.`Status` = 1 AND Prod.IDUsuario = '{FormPrincipal.userID}' ) GROUP BY Prod.ID ORDER BY VENDIDOS ASC;";

            return consulta;
        }

        public string productosMenosVendidosSinIncluidoVentasEnCero(string fechaInicio, string fechaFinal)
        {
            var consulta = $"SELECT Prod.ID, Prod.Nombre AS 'ARTICULO', SUM( IF ( ProdVent.Cantidad IS NULL, 0, ProdVent.Cantidad ) ) AS 'VENDIDOS', Prod.CodigoBarras AS 'CODIGO DE BARRAS', ( CASE WHEN Prod.Tipo = 'P' THEN 'PRODUCTO' WHEN Prod.Tipo = 'PQ' THEN 'COMBO' WHEN Prod.Tipo = 'S' THEN 'SERVICIO' END ) AS 'CATEGORIA', IF ( Vent.FechaOperacion IS NULL, 'N/A sin venta registrada', Vent.FechaOperacion ) AS 'ULTIMA VENTA', Prod.Stock AS 'STOCK', Prod.Precio AS 'PRECIO' FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON ( ProdVent.IDProducto = Prod.ID ) LEFT JOIN Ventas AS Vent ON ( Vent.ID = ProdVent.IDVenta ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{fechaInicio}.999999' AND '{fechaFinal}.999999' AND ( Prod.Tipo = 'P' || Prod.Tipo = 'S' || Prod.Tipo = 'PQ' ) AND Prod.IDUsuario = '{FormPrincipal.userID}' AND Prod.`Status` = 1 GROUP BY Prod.ID ORDER BY VENDIDOS ASC;";

            return consulta;
        }

        public string productosMasVendidosSinIncluirVentasEnCero(string fechaInicio, string fechaFinal)
        {
            var consulta = $"SELECT Prod.ID, Prod.Nombre AS 'ARTICULO', SUM( IF ( ProdVent.Cantidad IS NULL, 0, ProdVent.Cantidad ) ) AS 'VENDIDOS', Prod.CodigoBarras AS 'CODIGO DE BARRAS', ( CASE WHEN Prod.Tipo = 'P' THEN 'PRODUCTO' WHEN Prod.Tipo = 'PQ' THEN 'COMBO' WHEN Prod.Tipo = 'S' THEN 'SERVICIO' END ) AS 'CATEGORIA', IF ( Vent.FechaOperacion IS NULL, 'N/A sin venta registrada', Vent.FechaOperacion ) AS 'ULTIMA VENTA', Prod.Stock AS 'STOCK', Prod.Precio AS 'PRECIO' FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON ( ProdVent.IDProducto = Prod.ID ) LEFT JOIN Ventas AS Vent ON ( Vent.ID = ProdVent.IDVenta ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{fechaInicio}.999999' AND '{fechaFinal}.999999' AND ( Prod.Tipo = 'P' || Prod.Tipo = 'S' || Prod.Tipo = 'PQ' ) AND Prod.IDUsuario = '{FormPrincipal.userID}' AND Prod.`Status` = 1 GROUP BY Prod.ID ORDER BY VENDIDOS DESC;";

            return consulta;
        }

        public string productoInactivo(string idProducto)
        {
            var consulta = $"SELECT ID, Nombre, `Status` FROM productos WHERE ID = '{idProducto}' AND `Status` = 0 ";

            return consulta;
        }

        public string OperacionCajaEmpleado(string[] datos, bool corte = false)
        {
            var consulta = string.Empty;
            if (corte.Equals(true))
            {
                consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, IdEmpleado, NumFolio, CantidadRetiradaCorte, IdEmpleado)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}')";
            }
            else
            {
                consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, IdEmpleado)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}')";
            }


            return consulta;
        }

        public string SaberSiEsComboServicio(int idComboServ)
        {
            var consulta = $"SELECT ID, Nombre, Categoria, Tipo, `Status` FROM productos WHERE ID = '{idComboServ}' AND IDUsuario = '{FormPrincipal.userID}' AND ( Tipo <> 'P' )";

            return consulta;
        }

        public string SaberSiEstaActivoNoActivo(int idProdCombServ)
        {
            var consulta = $"SELECT ID, Nombre, Categoria, Tipo, `Status` FROM productos WHERE ID = '{idProdCombServ}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string ActualizarRegimenFiscal()
        {
            var consulta = "UPDATE RegimenFiscal SET AplicaFisica = REPLACE ( AplicaFisica, '?', 'í' ), AplicaMoral = REPLACE ( AplicaMoral, '?', 'í' )";

            return consulta;
        }

        #region Listado Ventas desde Usuario sin busqueda 
        public string VerComoAdministradorTodasLasVentasPagadas(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' AND Emp.nombre IS NULL ORDER BY ID DESC ";

            return consulta;
        }

        public string SimbolosDePreguntaRegimenFiscalEnDescripcion()
        {
            var consulta = "UPDATE RegimenFiscal SET Descripcion = REPLACE ( Descripcion, 'R?gimen', 'Régimen' ) WHERE CodigoRegimen = 625; UPDATE RegimenFiscal SET Descripcion = REPLACE ( Descripcion, 'trav?s', 'través' ) WHERE CodigoRegimen = 625; UPDATE RegimenFiscal SET Descripcion = REPLACE ( Descripcion, 'Tecnol?gicas', 'Tecnológicas' ) WHERE CodigoRegimen = 625; UPDATE RegimenFiscal SET Descripcion = REPLACE ( Descripcion, 'R?gimen', 'Régimen' ) WHERE CodigoRegimen = 626;";

            return consulta;
        }

        public string VerComoAdministradorTodasLasVentasGuardadas(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC ";

            return consulta;
        }

        public string VerComoAdministradorTodasLasVentasCanceladas(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Emp.nombre IS NULL AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC ";

            return consulta;
        }

        public string VerComoAdministradorTodasLasVentasACredito(int estado, string ultimaFechaDeCorte, string rangoFechaLimite, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC ";

            return consulta;
        }
        #endregion

        #region Listado Ventas desde Empleado sin busqueda
        public string VerComoEmpleadoTodasMisVentasPagadas(int estado, int idEmpleado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDEmpleado = '{idEmpleado}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEmpleadoTodasLasVentasGuardadas(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC ";

            return consulta;
        }

        public string VerComoEmpleadoTodasMisVentasCanceladas(int estado, int idEmpleado, string ultimaFechaDeCorte, string rangoFechaLimite, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDEmpleado = '{idEmpleado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEmpleadoTodasLasVentasACredito(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC ";

            return consulta;
        }
        #endregion

        #region Listado Ventas desde Usuario sin texto que buscar pero con rango de fechas
        public string VerComoAdministradorTodasLaVentasMiasPagadasPorFechas(int status, string FechaInicial, string FechaFinal, string formaPago)
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND ( Emp.nombre IS NULL ) AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasPagadasPorFechas(int status, string FechaInicial, string FechaFinal)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND DATE( Vent.FechaOperacion ) BETWEEN '{FechaInicial}' AND '{FechaFinal}' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasGuardadasPorFechas(int status, string FechaInicial, string FechaFinal, string formaPago)
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasCanceladasMiasPorFechas(int status, string FechaInicial, string FechaFinal, string formaPago)
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND ( Emp.nombre IS NULL ) AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' AND Emp.nombre IS NULL ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasACreditoPorFechas(int status, string FechaInicial, string FechaFinal)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }
        #endregion

        #region Listado Ventas desde Empleado sin texto que buscar pero con rango de fechas
        public string VerComoEpleadoTodasMisVentasPagadasPorFechas(int status,int idEmplado, string FechaInicial, string FechaFinal, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDEmpleado = '{idEmplado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEpleadoTodasLaVentasGuardadasPorFechas(int status, string FechaInicial, string FechaFinal, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEpleadoTodasMisVentasCanceladasPorFechas(int status, int idEmplado, string FechaInicial, string FechaFinal, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDEmpleado = '{idEmplado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEpleadoTodasLaVentasACreditoPorFechas(int status, string FechaInicial, string FechaFinal, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' ORDER BY Vent.ID DESC";

            return consulta;  
        }
        #endregion

        #region Listado Ventas desde Usuario con texto que buscar pero con rango de fechas
        public string NombreAdministrador(string name)
        {
            var result = string.Empty;

            var query = cn.CargarDatos($"SELECT ID FROM Usuarios WHERE Usuario LIKE '%{name}%' AND ID = '{FormPrincipal.userID}'");

            if (!query.Rows.Count.Equals(0))
            {
                result = query.Rows[0]["ID"].ToString();
            }

            return result;
        }

        public string ParametroDeBusquedaIdUsuarioSiendoAdministrador(string IDAdministrador, string fechaInicial, string fechaFinal)
        {
            var consulta = $"SELECT * FROM ventas WHERE IDUsuario = '{IDAdministrador}' AND FechaOperacion BETWEEN '{fechaInicial}.999999' AND '{fechaFinal}.999999' LIMIT 1";

            return consulta;
        }

        public string ParametroDeBusquedaIdEmpleadoSiendoAdministrador(string IDEmpleado, string fechaInicial, string fechaFinal)
        {
            var consulta = $"SELECT * FROM ventas WHERE IDEmpleado = '{IDEmpleado}' AND FechaOperacion BETWEEN '{fechaInicial}.999999' AND '{fechaFinal}.999999' LIMIT 1";

            return consulta;
        }

        public string ParametroDeBusquedaFolioSiendoAdministrador(string campoFolio, int tipo = 1)
        {
            var consulta = string.Empty;

            if (tipo == 1)
            {
                consulta = $"AND Folio LIKE '%{campoFolio}%' ";
            }

            if (tipo == 2)
            {
                consulta = $"AND Folio IN ({campoFolio}) ";
            }

            return consulta;
        }

        public string ParametrosDeBusquedaNombreRFCSiendoAdministrador(string campoNombreRFC)
        {
            var consulta = $" Vent.Cliente LIKE '%{campoNombreRFC}%' OR Vent.RFC LIKE '%{campoNombreRFC}%' ";

            return consulta;
        }

        public string ParametrosDeBusquedaDeEmpleadoSiendoAdministrador(string campoIDEmpleado)
        {
            var consulta = $" Emp.nombre LIKE '%{campoIDEmpleado}%' ";

            return consulta;
        }

        public string ParametrosDeBusquedaDeUsuarioSiendoAdministrador()
        {
            var consulta = $" Emp.nombre IS NULL ";

            return consulta;
        }

        public string ParametrosDeBusquedaDeUsuarioSiendoAdministradorI()
        {
            var consulta = $" Emp.nombre IS NULL ";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasPagadasPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' AND Emp.Nombre IS NULL {busqueda} ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasGuardadasPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' {busqueda} ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasCanceladasPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Emp.nombre IS NULL AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' {busqueda} ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLaVentasACreditoPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' {busqueda} ORDER BY Vent.ID DESC";

            return consulta;
        }
        #endregion

        #region Listado Ventas desde Empleado con texto que buscar pero con rango de fechas
        public string VerComoEmpleadoTodasMisVentasPagadasPorFechasYBusqueda(int status, int idEmpleado, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDEmpleado = '{idEmpleado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' { busqueda } ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoDesdeOtroEmpleadoVentasPagadasPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' { busqueda } ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEmpleadoTodasLasVentasGuardadasPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' AND Vent.IDUsuario = '{FormPrincipal.userID}' { busqueda } ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEmpleadoTodasMisVentasCanceladasPorFechasYBusqueda(int status, int idEmpleado, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDEmpleado = '{idEmpleado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' { busqueda } ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string VerComoEmpleadoTodasLasVentasACreditoPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}'  AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' { busqueda } ORDER BY Vent.ID DESC";

            return consulta;
        }
        #endregion

        public string obtenerEmpleados(int idAdmin)
        {
            var consulta = $"SELECT Emp.ID, Emp.nombre FROM empleados AS Emp WHERE Emp.IDUsuario = '{idAdmin}' AND Emp.p_venta = '1' AND Emp.estatus = '1' ORDER BY Emp.nombre ASC";

            return consulta;
        }

        public string obtenerDatosDeAdministrador(int idAdmin)
        {
            var consulta = $"SELECT ID, Usuario, RazonSocial FROM usuarios WHERE ID = '{idAdmin}'";

            return consulta;
        }

        public string obtenerDatosDeEmpleado(int idEmpleado)
        {
            var consulta = $"SELECT ID, IDUsuario, nombre FROM empleados WHERE ID = '{idEmpleado}' AND estatus = '1'";

            return consulta;
        }


        /// <summary>
        /// Consulta para mostrar ventas pagadas por empleado
        /// </summary>
        /// <param name="estado">ventas pagadas</param>
        /// <param name="idEmpleado">id del empleado</param>
        /// <param name="ultimaFechaDeCorte">ultima Fecha del corte de caja</param>
        /// <param name="rangoFechaLimite">rango de la fecha puesta por el usuario</param>
        /// <returns>retorna la consulta para mostar por usuario</returns>
        public string filtroPorEmpleadoDesdeAdministrador(int estado, int idEmpleado, string ultimaFechaDeCorte, string rangoFechaLimite, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDEmpleado = '{idEmpleado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC";

            return consulta;
        }

        public string filtroMostrarTodasLasVentasPagadasEnAdministrador(int estado, string ultimaFechaDeCorte, string rangoFechaLimite, string formaPago)
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC";

            return consulta;
        }

        public string verComoAdministradorTodasMisVentasGuardadas(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' AND Emp.nombre IS NULL ORDER BY ID DESC ";

            return consulta;
        }

        public string verComoAdministradorTodasVentasGuardadasPorEmpleado(int estado, int idEmpleado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF(Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial) AS 'Consumidor', IF(Emp.nombre IS NULL, CONCAT(Usr.Usuario, ' (ADMIN)'), CONCAT(Emp.nombre, ' (EMPLEADO)') ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDEmpleado = '{idEmpleado}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC ";

            return consulta;
        }

        public string verVentasCanceladasDelEmpleadoDesdeAdministrador(int estado, int idEmpleado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDEmpleado = '{idEmpleado}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC";

            return consulta;
        }

        public string verVentasCanceladasDeTodosDesdeAdministrador(int estado, string ultimaFechaDeCorte, string rangoFechaLimite, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC";

            return consulta;
        }

        public string verVentasCreditoDelAdministrador(int estado, string ultimaFechaDeCorte, string rangoFechaLimite)
        {
            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' AND Emp.nombre IS NULL  ORDER BY ID DESC";

            return consulta;
        }

        public string verVentasCreditoPorEmpleadoDesdeAdministrador(int estado, int idEmpleado, string ultimaFechaDeCorte, string rangoFechaLimite, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDEmpleado = '{idEmpleado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' ORDER BY ID DESC";

            return consulta;
        }

        public string filtroPorEmpleadoDesdeAdministradorConParamettro(int status, int idEmpleado, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDEmpleado = '{idEmpleado}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' {busqueda} ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string filtroMostrarTodasLasVentasPagadasEnAdministradorConParametro(int estado, string ultimaFechaDeCorte, string rangoFechaLimite, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF( Emp.nombre IS NULL, CONCAT( Usr.Usuario, ' (ADMIN)' ), CONCAT( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{estado}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{ultimaFechaDeCorte}.999999' AND '{rangoFechaLimite}.999999' {busqueda} ORDER BY ID DESC";

            return consulta;
        }

        public string VerComoAdministradorTodasLasVentasCanceladasPorFechasYBusqueda(int status, string FechaInicial, string FechaFinal, string busqueda, string formaPago = "NA")
        {
            if (formaPago.Equals("NA"))
            {
                formaPago = string.Empty;
            }
            else
            {
                formaPago = $"AND Vent.`FormaPago` = '{formaPago}'";
            }

            var consulta = $"SELECT Vent.*, Usr.Usuario, IF ( Clte.RazonSocial IS NULL, 'PUBLICO GENERAL', Clte.RazonSocial ) AS 'Consumidor', IF ( Emp.nombre IS NULL, CONCAT ( Usr.Usuario, ' (ADMIN)' ), CONCAT ( Emp.nombre, ' (EMPLEADO)' ) ) AS 'Vendedor' FROM Ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '{status}' AND Vent.IDUsuario = '{FormPrincipal.userID}' {formaPago} AND Vent.FechaOperacion BETWEEN '{FechaInicial}.999999' AND '{FechaFinal}.999999' { busqueda } ORDER BY Vent.ID DESC";

            return consulta;
        }

        public string ExportarTodosLosDatosDeProductosACSV()
        {
            var consulta = $"SELECT ID AS 'Identificador URL', ID AS 'SKU', Nombre, Stock, Precio, CodigoBarras AS 'Codigo de barras' FROM productos WHERE IDUsuario	= '{FormPrincipal.userID}' AND	`Status`=	'1' AND Tipo = 'P'";

            return consulta;
        }

        public string fechaUltimoCorteDecaja()
        {
            var consulta = $"SELECT ID FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1";

            return consulta;
        }

        public string ImportarProductosDeCSV( string id, string stock)
        {
            var consulta = $"UPDATE productos SET Stock = {stock} WHERE ID = {id} AND IDUsuario	= '{FormPrincipal.userID}' AND	`Status`=	'1' AND Tipo = 'P'";
            return consulta;
        }

        public string LLamarDatosNoIncluidosEnElArchivoCSVExportableDeVentas(string ID)
        {
            var consulta = $"SELECT Nombre, Precio, Stock  FROM productos WHERE IDUsuario = {FormPrincipal.userID} AND id = {ID}";
            return consulta;
        }

        public string ProductosParaFiltrarCSV(string EnWeb)
        {
            var consulta = $"SELECT ID AS 'Identificador URL', ID AS 'SKU', Nombre, Stock, Precio, CodigoBarras AS 'Codigo de barras' FROM productos WHERE IDUsuario	= '{FormPrincipal.userID}' AND	`Status`= '1' AND Tipo = 'P' {EnWeb}";

            return consulta;
        }

        public string totalCantidadesVentasAdministrador(string idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', SUM( Credito ) AS 'Credito', SUM( Anticipo ) AS 'Anticipo', ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) + SUM( Credito ) + SUM( Anticipo ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'venta'";

            return consulta;
        }

        public string EstadoDeRegistroDeProductoComoEnWeb(string ID, string estadoWeb)
        {
            var consulta = $"UPDATE productos SET EnWeb = {estadoWeb} WHERE ID = {ID} AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string totalCantidadesVentasEmpleado(string idUltimoCorteDeCaja, string idEmpleado)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', SUM( Credito ) AS 'Credito', SUM( Anticipo ) AS 'Anticipo', ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) + SUM( Credito ) + SUM( Anticipo ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'venta'";

            return consulta;
        }

        public string ProductosParaFiltrarCSVFiltroSiEstaEnWeb(string EnWeb)
        {
            var consulta = $"SELECT ID, Nombre, Stock FROM productos WHERE IDUsuario	= '{FormPrincipal.userID}' AND	`Status`= '1' AND Tipo = 'P' {EnWeb}";

            return consulta;
        }

        public string totalCantidadesVentasTodos(string IDUsuario, string IDEmpleado, string IDCaja)
        {
            var consulta = $"SELECT Operacion, IDUsuario, IdEmpleado, IF ( SUM( Efectivo ) IS NULL, 0, SUM( Efectivo ) ) AS 'Efectivo', IF ( SUM( Tarjeta ) IS NULL, 0, SUM( Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Vales ) IS NULL, 0, SUM( Vales ) ) AS 'Vales', IF ( SUM( Cheque ) IS NULL, 0, SUM( Cheque ) ) AS 'Cheque', IF ( SUM( Transferencia ) IS NULL, 0, SUM( Transferencia ) ) AS 'Transferencia', IF ( SUM( Credito ) IS NULL, 0, SUM( Credito ) ) AS 'Credito', IF ( SUM( Anticipo ) IS NULL, 0, SUM( Anticipo ) ) AS 'Anticipo', IF (  ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) + SUM( Credito ) + SUM( Anticipo ) ) IS NULL, 0, ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) + SUM( Credito ) + SUM( Anticipo ) ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{IDUsuario}' AND IDEmpleado = '{IDEmpleado}' AND ID > '{IDCaja}' AND Operacion = 'venta'";

            return consulta;
        }

        public string agregarAnticipoCajaEmpleado(string[] datos)
        {
            var consulta = $"INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, IdEmpleado) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}')";

            return consulta;
        }

        public string totalCantidadesAnticiposAdministrador(string idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalAnticipos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'anticipo'";

            return consulta;
        }

        public string totalCantiadesAnticiposEmpleado(string idUltimoCorteDeCaja, string idEmpleado)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalAnticipos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'anticipo'";

            return consulta;
        }

        public string totalCantidadesAnticposTodos(string IDUsuario, string IDEmpleado, string IDCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalAnticipos' FROM caja WHERE IDUsuario = '{IDUsuario}' AND IDEmpleado = '{IDEmpleado}' AND ID > '{IDCaja}' AND Operacion = 'anticipo'";

            return consulta;
        }

        public string totalCantidadesDepositosAdministrador(string idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalDepositos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'deposito'";

            return consulta;
        }

        public string totalCantiadesDepositosEmpleado(string idUltimoCorteDeCaja, string idEmpleado)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalDepositos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'deposito'";

            return consulta;
        }

        public string totalCantidadesDepositosTodos(string IDUsuario, string IDEmpleado, string IDCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalDepositos' FROM caja WHERE IDUsuario = '{IDUsuario}' AND IdEmpleado = '{IDEmpleado}' AND ID > '{IDCaja}' AND Operacion = 'deposito'";

            return consulta;
        }

        public string totalCantidadesRetirosAdministrador(string idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string totalCantiadesRetirosEmpleado(string idUltimoCorteDeCaja, string idEmpleado)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, SUM( Efectivo ) AS 'Efectivo', SUM( Tarjeta ) AS 'Tarjeta', SUM( Vales ) AS 'Vales', SUM( Cheque ) AS 'Cheque', SUM( Transferencia ) AS 'Transferencia', (  SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' AND ID > '{idUltimoCorteDeCaja}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string totalCantidadesRetirosTodos(string IDUsuario, string IDEmpleado, string IDCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, IF ( SUM( Efectivo ) IS NULL, 0, SUM( Efectivo ) ) AS 'Efectivo', IF ( SUM( Tarjeta ) IS NULL, 0, SUM( Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Vales ) IS NULL, 0, SUM( Vales ) ) AS 'Vales', IF ( SUM( Cheque ) IS NULL, 0, SUM( Cheque ) ) AS 'Cheque', IF ( SUM( Transferencia ) IS NULL, 0, SUM( Transferencia ) ) AS 'Transferencia', IF ( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) IS NULL, 0, ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{IDUsuario}'  AND IdEmpleado = '{IDEmpleado}' AND ID > '{IDCaja}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string cargarFechaUltimoCorterealizado(string idUltimoCorte)
        {
            var consulta = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idUltimoCorte}' AND Operacion = 'corte'";

            return consulta;
        }

        public string cargarAbonosDesdeUltimoCorteRealizadoAdministrador(string idUsuario, string ultimaFechaDeCorte)
        {
            var consulta = $"( /* ver solo abonos que pertenecen a ventas a credito solo de administrador */ SELECT Abono.ID, IF ( Abono.IDEmpleado = '0', 'Propia', 'Ajena' ) AS 'Propia', Abono.IDVenta, IF ( Abono.IDEmpleado = '' OR Abono.IDEmpleado IS NULL, '0', Abono.IDEmpleado ) AS 'IDEmpleado', FORMAT( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ), 2 ) AS 'Efectivo', FORMAT( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ), 2 ) AS 'Tarjeta', FORMAT( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ), 2 ) AS 'Vales', FORMAT( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ), 2 ) AS 'Cheque', FORMAT( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ) ) + ( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ) ) + ( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ) ) + ( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ) ) + ( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ) ) ), 2 ) AS 'Total' FROM ventas AS Vent INNER JOIN abonos AS Abono ON ( Abono.IDVenta = Vent.ID ) WHERE Vent.IDUsuario = '{idUsuario}' AND Vent.IDEmpleado = '0' AND Vent.FormaPago = 'Crédito' AND Vent.FechaOperacion > '{ultimaFechaDeCorte}' AND Abono.IDVenta IN ( SELECT ID FROM ventas WHERE IDUsuario = '{idUsuario}' AND IDEmpleado = '0' AND `Status` = '4' AND FechaOperacion > '{ultimaFechaDeCorte}' ) ) UNION ( /* ver solo abonos que pertenecen a ventas a credito solo de administrador pero abonados desde otro usuario */ SELECT Abono.ID, IF ( Emp.nombre = '' OR Emp.nombre IS NULL, 'Propia', 'Ajena' ) AS 'Propia', Abono.IDVenta, IF ( Abono.IDEmpleado = '' OR Abono.IDEmpleado IS NULL, '0', Abono.IDEmpleado ) AS 'IDEmpleado', FORMAT( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ), 2 ) AS 'Efectivo', FORMAT( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ), 2 ) AS 'Tarjeta', FORMAT( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ), 2 ) AS 'Vales', FORMAT( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ), 2 ) AS 'Cheque', FORMAT( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ) ) + ( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ) ) + ( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ) ) + ( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ) ) + ( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ) ) ), 2 ) AS 'Total' FROM ventas AS Vent INNER JOIN abonos AS Abono ON ( Abono.IDVenta = Vent.ID ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Abono.IDEmpleado ) WHERE Vent.IDUsuario = '{idUsuario}' AND Vent.IDEmpleado != '0' AND Vent.FormaPago = 'Crédito' AND Vent.FechaOperacion > '{ultimaFechaDeCorte}' AND Abono.IDVenta IN ( SELECT ID FROM ventas WHERE IDUsuario = '{idUsuario}' AND IDEmpleado = '0' AND `Status` = '4' AND FechaOperacion > '{ultimaFechaDeCorte}' ) ) UNION ( /* ver solo abonos que pertenecen a ventas a credito que ya fueron pagadas en su totalidad solo de administrador */ SELECT Abono.ID, IF ( Abono.IDEmpleado = '0', 'Propia', 'Ajena' ) AS 'Propia', Abono.IDVenta, IF ( Abono.IDEmpleado = '' OR Abono.IDEmpleado IS NULL, '0', Abono.IDEmpleado ) AS 'IDEmpleado', FORMAT( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ), 2 ) AS 'Efectivo', FORMAT( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ), 2 ) AS 'Tarjeta', FORMAT( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ), 2 ) AS 'Vales', FORMAT( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ), 2 ) AS 'Cheque', FORMAT( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ) ) + ( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ) ) + ( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ) ) + ( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ) ) + ( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ) ) ), 2 ) AS 'Total' FROM ventas AS Vent INNER JOIN abonos AS Abono ON ( Abono.IDVenta = Vent.ID ) WHERE Vent.IDUsuario = '{idUsuario}' AND Vent.IDEmpleado = '0' AND Vent.FormaPago = 'Crédito' AND Vent.FechaOperacion > '{ultimaFechaDeCorte}' AND Abono.IDVenta IN ( SELECT DISTINCT Vent.ID FROM ventas AS Vent INNER JOIN abonos AS Abono ON ( Vent.ID = Abono.IDVenta ) WHERE Vent.IDUsuario = '{idUsuario}' AND Vent.IDEmpleado = '0' AND Vent.`Status` = '1' AND Vent.FechaOperacion > '{ultimaFechaDeCorte}' ) ) UNION ( /* ver solo abonos que pertenecen a ventas a credito que ya fueron pagadas en su totalidad, solo de administrador pero abonados desde otro usuario */ SELECT Abono.ID, IF ( Emp.nombre = '' OR Emp.nombre IS NULL, 'Propia', 'Ajena' ) AS 'Propia', Abono.IDVenta, IF ( Abono.IDEmpleado = '' OR Abono.IDEmpleado IS NULL, '0', Abono.IDEmpleado ) AS 'IDEmpleado', FORMAT( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ), 2 ) AS 'Efectivo', FORMAT( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ), 2 ) AS 'Tarjeta', FORMAT( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ), 2 ) AS 'Vales', FORMAT( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ), 2 ) AS 'Cheque', FORMAT( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ) ) + ( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ) ) + ( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ) ) + ( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ) ) + ( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ) ) ), 2 ) AS 'Total' FROM ventas AS Vent INNER JOIN abonos AS Abono ON ( Abono.IDVenta = Vent.ID ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Abono.IDEmpleado ) WHERE Vent.IDUsuario = '{idUsuario}' AND Vent.IDEmpleado != '0' AND Vent.FormaPago = 'Crédito' AND Vent.FechaOperacion > '{ultimaFechaDeCorte}' AND Abono.IDVenta IN ( SELECT DISTINCT Vent.ID FROM ventas AS Vent INNER JOIN abonos AS Abono ON ( Vent.ID = Abono.IDVenta ) WHERE Vent.IDUsuario = '{idUsuario}' AND Vent.IDEmpleado != '0' AND Vent.`Status` = '1' AND Vent.FechaOperacion > '{ultimaFechaDeCorte}' ) )";

            return consulta;
        }

        public string cargarAbonosDesdeUltimoCorteRealizadoEmpleado(string idEmpleado, string ultimaFechaDeCorte)
        {
            var consulta = $"SELECT Abono.ID, Abono.IDUsuario, Abono.IDEmpleado, Abono.IDVenta, FORMAT( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ), 2 ) AS 'Efectivo', FORMAT( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ), 2 ) AS 'Tarjeta', FORMAT( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ), 2 ) AS 'Vales', FORMAT( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ), 2 ) AS 'Cheque', FORMAT( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ) ) + ( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ) ) + ( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ) ) + ( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ) ) + ( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ) ) ), 2 ) AS 'Total' FROM Abonos AS Abono WHERE Abono.IDVenta IN ( SELECT ID FROM ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idEmpleado}' AND FormaPago = 'Crédito' AND FechaOperacion > '{ultimaFechaDeCorte}' )";
             
            return consulta;
        }

        public string cargarAbonosDesdeUltimoCorteRealizadoDesdeOtrosUsuarios(string idEmpleado, string ultimaFechaDeCorte)
        {
            var consulta = $"SELECT Abono.ID, Abono.IDUsuario, Abono.IDEmpleado, Abono.IDVenta, FORMAT( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ), 2 ) AS 'Efectivo', FORMAT( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ), 2 ) AS 'Tarjeta', FORMAT( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ), 2 ) AS 'Vales', FORMAT( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ), 2 ) AS 'Cheque', FORMAT( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( Abono.Efectivo = '' OR Abono.Efectivo IS NULL, '0', Abono.Efectivo ) ) + ( IF ( Abono.Tarjeta = '' OR Abono.Tarjeta IS NULL, '0', Abono.Tarjeta ) ) + ( IF ( Abono.Vales = '' OR Abono.Vales IS NULL, '0', Abono.Vales ) ) + ( IF ( Abono.Cheque = '' OR Abono.Cheque IS NULL, '0', Abono.Cheque ) ) + ( IF ( Abono.Transferencia = '' OR Abono.Transferencia IS NULL, '0', Abono.Transferencia ) ) ), 2 ) AS 'Total' FROM Abonos AS Abono WHERE Abono.ID IN ( SELECT ID FROM abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idEmpleado}' AND FechaOperacion > '{ultimaFechaDeCorte}' AND IDVenta NOT IN ( SELECT ID FROM ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idEmpleado}' AND FormaPago = 'Crédito' AND FechaOperacion > '{ultimaFechaDeCorte}' ) )";

            return consulta;
        }

        public string cargarAbonosDesdeUltimoCorteRealizadoTodos(string ultimaFechaDeCorte)
        {
            var consulta = $"SELECT sum( Efectivo ) AS Efectivo, sum( Tarjeta ) AS Tarjeta, sum( Vales ) AS Vales, sum( Cheque ) AS Cheque, sum( Transferencia ) AS Transferencia, ( sum( Efectivo ) + sum( Tarjeta ) + sum( Vales ) + sum( Cheque ) + sum( Transferencia ) ) AS Total FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{ultimaFechaDeCorte}'";

            return consulta;
        }

        public string AbonosCreditoDesdeUltimoCorteRealizadoTodos(string ultimaFechaDeCorte)
        {
            var consulta = $"SELECT IF ( SUM(Abono.Efectivo) = '' OR SUM( Abono.Efectivo ) IS NULL, 0, SUM( Abono.Efectivo ) ) AS 'Efectivo', IF ( SUM(Abono.Tarjeta) = '' OR SUM( Abono.Tarjeta ) IS NULL, 0, SUM( Abono.Tarjeta ) ) AS 'Tarjeta', IF ( SUM(Abono.Vales) = '' OR SUM( Abono.Vales ) IS NULL, 0, SUM( Abono.Vales ) ) AS 'Vales', IF ( SUM(Abono.Cheque) = '' OR SUM( Abono.Cheque ) IS NULL, 0, SUM( Abono.Cheque ) ) AS 'Cheque', IF ( SUM(Abono.Transferencia) = '' OR SUM( Abono.Transferencia ) IS NULL, 0, SUM( Abono.Transferencia ) ) AS 'Transferencia', ( IF ( SUM(Abono.Efectivo) = '' OR SUM( Abono.Efectivo ) IS NULL, 0, SUM( Abono.Efectivo ) ) + IF ( SUM(Abono.Tarjeta) = '' OR SUM( Abono.Tarjeta ) IS NULL, 0, SUM( Abono.Tarjeta ) ) + IF ( SUM(Abono.Vales) = '' OR SUM( Abono.Vales ) IS NULL, 0, SUM( Abono.Vales ) ) + IF ( SUM(Abono.Cheque) = '' OR SUM( Abono.Cheque ) IS NULL, 0, SUM( Abono.Cheque ) ) + IF ( SUM(Abono.Transferencia) = '' OR SUM( Abono.Transferencia ) IS NULL, 0, SUM( Abono.Transferencia ) ) ) AS 'Total' FROM Abonos AS Abono INNER JOIN ventas AS Vent ON ( Vent.ID = Abono.IDVenta ) WHERE Abono.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '4' AND Abono.FechaOperacion > '{ultimaFechaDeCorte}'";

            return consulta;
        }

        public string cargarIDInicialDeAbonos(string ultimaFechaDeCorte)
        {
            var consulta = $"SELECT ID FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{ultimaFechaDeCorte}' ORDER BY ID ASC LIMIT 1";

            return consulta;
        }

        public string cargarAbonosDeLaVentaACredito(int IDVentas)
        {
            var consulta = $"SELECT * FROM Abonos WHERE IDVenta = '{IDVentas}' AND IDUsuario = '{FormPrincipal.userID}'";


            return consulta;
        }

        public string obtenerUltimoIDInsertadoEnCaja()
        {
            var consulta = $"SELECT MAX(ID) AS ID FROM caja";

            return consulta;
        }

        public string guardarHistorialCorteDeCaja(string[] datos)
        {
            var consulta = $"INSERT INTO historialcortesdecaja ( IDCorteDeCaja, IDUsuario, IDEmpleado, FechaOperacion, SaldoInicialEfectivo, SaldoInicialTarjeta, SaldoInicialVales, SaldoInicialCheque, SaldoInicialTransferencia, SaldoInicialCredito, SaldoInicialAnticipo, CantidadRetiradaDelCorte ) VALUES ( '{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}' )";

            return consulta;
        }

        public string cargarSaldoInicialAdministrador()
        {
            var consulta = $"SELECT IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha', SaldoInicialEfectivo AS 'Efectivo', SaldoInicialTarjeta AS 'Tarjeta', SaldoInicialVales AS 'Vales', SaldoInicialCheque AS 'Cheque', SaldoInicialTransferencia AS 'Transferencia', SaldoInicialCredito AS 'Credito', SaldoInicialAnticipo AS 'Anticipo', CantidadRetiradaDelCorte AS 'CantidadRetirada', ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string cargarSaldoInicialTodos()
        {
            var consulta = $"SELECT IDUsuario, IDEmpleado, MAX(IDCorteDeCaja) AS 'IDCaja', SUM( SaldoInicialEfectivo ) AS 'Efectivo', SUM( SaldoInicialTarjeta ) AS 'Tarjeta', SUM( SaldoInicialVales ) AS 'Vales', SUM( SaldoInicialCheque ) AS 'Cheque', SUM( SaldoInicialTransferencia ) AS 'Transferencia', SUM( SaldoInicialCredito ) AS 'Credito', SUM( SaldoInicialAnticipo ) AS 'Anticipo', SUM( CantidadRetiradaDelCorte ) AS 'CantidadRetirada', ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' GROUP BY IDEmpleado ORDER BY ID";

            return consulta;
        }

        public string cargarSaldoInicialEmpleado(string idEmpleado)
        {
            var consulta = $"SELECT IDUsuario, IDEmpleado, IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha', SaldoInicialEfectivo AS 'Efectivo', SaldoInicialTarjeta AS 'Tarjeta', SaldoInicialVales AS 'Vales', SaldoInicialCheque AS 'Cheque', SaldoInicialTransferencia AS 'Transferencia', SaldoInicialCredito AS 'Credito', SaldoInicialAnticipo AS 'Anticipo', CantidadRetiradaDelCorte AS 'CantidadRetirada', ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idEmpleado}' ORDER BY IDCorteDeCaja DESC LIMIT 1";

            return consulta;
        }

        public string resultadoConcentradooHistorialCorteDeCaja(int[] numerosIDCarteDeCaja)
        {
            var cadenaNumeros = String.Join(",", numerosIDCarteDeCaja);

            var consulta = $"SELECT SUM( SaldoInicialEfectivo ) AS 'Efectivo', SUM( SaldoInicialTarjeta ) AS 'Tarjeta', SUM( SaldoInicialVales ) AS 'Vales', SUM( SaldoInicialCheque ) AS 'Cheque', SUM( SaldoInicialTransferencia ) AS 'Transferencia', SUM( SaldoInicialCredito ) AS 'Credito', SUM( SaldoInicialAnticipo ) AS 'Anticipo', SUM( CantidadRetiradaDelCorte ) AS 'CantidadRetirada', ( SUM( SaldoInicialEfectivo ) + SUM( SaldoInicialTarjeta ) + SUM( SaldoInicialVales ) + SUM( SaldoInicialCheque ) + SUM( SaldoInicialTransferencia ) ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDCorteDeCaja IN ( {cadenaNumeros} )";

            return consulta;
        }

        public string corteHistorialCortesDeCaja(string idCodigoCaja)
        {
            var consulta = $"SELECT IDCorteDeCaja, FechaOperacion FROM historialcortesdecaja WHERE IDCorteDeCaja = '{idCodigoCaja}'";

            return consulta;
        }

        public string estatusFinalizacionPagoCredito(int idVenta)
        {
            var consulta = $"UPDATE Ventas SET Status = 1 WHERE ID = '{idVenta}' AND IDUsuario = {FormPrincipal.userID} ";

            return consulta;
        }
        public string verificarSiTieneCorteDeCajaDesdeCaja(int idEmpleado)
        {
            var consulta = $"SELECT * FROM caja WHERE Operacion = 'corte' AND IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string registroInicialCorteDeCaja(string[] datos)
        {
            var consulta = $"INSERT INTO caja ( IDUsuario, IdEmpleado, FechaOperacion, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, CantidadRetiradaCorte, Operacion ) VALUES ( '{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}' )";

            return consulta;
        }

        public string ultimaIdInsertadaDeCaja(int idEmpleado)
        {
            var consulta = $"SELECT ID AS ID FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' AND Operacion = 'corte' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string obtenerUltimoIDSaldoInicialDelAdministrador()
        {
            var consulta = $"SELECT ID FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string obtenerUltimoIDSaldoInicialDelEmpleado(string idEmpleado)
        {
            var consulta = $"SELECT ID FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idEmpleado}' ORDER BY IDCorteDeCaja DESC LIMIT 1";

            return consulta;
        }

        public string obtenerSaldoInicialPorIDDelHistorialCorteDeCaja(int idHistorialCorteDeCaja)
        {
            var consulta = $"SELECT * FROM historialcortesdecaja WHERE ID = '{idHistorialCorteDeCaja}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string actualizarSaldoInicialDeEfectivo(int idHistorialCorteDeCaja, decimal montoEfectivo)
        {
            var consulta = $"UPDATE historialcortesdecaja SET SaldoInicialEfectivo = '{montoEfectivo}' WHERE ID = '{idHistorialCorteDeCaja}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string actualizarSaldoInicialDeTarjeta(int idHistorialCorteDeCaja, decimal montoTarjeta)
        {
            var consulta = $"UPDATE historialcortesdecaja SET SaldoInicialTarjeta = '{montoTarjeta}' WHERE ID = '{idHistorialCorteDeCaja}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string actualizarSaldoInicialDeVales(int idHistorialCorteDeCaja, decimal montoVales)
        {
            var consulta = $"UPDATE historialcortesdecaja SET SaldoInicialVales = '{montoVales}' WHERE ID = '{idHistorialCorteDeCaja}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string actualizarSaldoInicialDeCheque(int idHistorialCorteDeCaja, decimal montoCheque)
        {
            var consulta = $"UPDATE historialcortesdecaja SET SaldoInicialCheque = '{montoCheque}' WHERE ID = '{idHistorialCorteDeCaja}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string actualizarSaldoInicialDeTransferencia(int idHistorialCorteDeCaja, decimal montoTransferencia)
        {
            var consulta = $"UPDATE historialcortesdecaja SET SaldoInicialTransferencia = '{montoTransferencia}' WHERE ID = '{idHistorialCorteDeCaja}' AND IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string BuscarPublicaGeneral()
        {
            var consula = $"SELECT ID , RazonSocial FROM clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND RFC = 'XAXX010101000' AND RazonSocial = 'PUBLICO GENERAL'";

            return consula;
        }

        public string AgregarPublicoGeneral(string numeroCliente)
        {
            var consulta = $"INSERT INTO clientes (IDUsuario,RazonSocial,NombreComercial,RFC,UsoCFDI,Pais,Estado,Municipio,Localidad,CodigoPostal,Colonia,Calle,NoExterior,NoInterior,regimenfiscal,Email,Telefono,FormaPago,FechaOperacion,Status,TipoCliente,NumeroCliente )VALUES( '{FormPrincipal.userID}', 'PUBLICO GENERAL', '','XAXX010101000','G01', '','','','','','','','','','','','','01','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','1','0','{numeroCliente}' )";

            return consulta;
        }

        public string ObtenerDatosClientePublicoGeneral()
        {
            var consulta = $"SELECT ID, RazonSocial FROM clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND RFC = 'XAXX010101000' AND RazonSocial = 'PUBLICO GENERAL' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string UltimoNumerodeCliente()
        {
            var consulta = $"SELECT NumeroCliente FROM clientes WHERE IDUsuario = '{FormPrincipal.userID}' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string cargarIDsDeEmpleados()
        {
            var consulta = $"SELECT ID FROM empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND estatus = '1' AND p_venta = '1'";

            return consulta;
        }

        public string cargarNuevoSaldoInicial(string losIDsDeEmpleados)
        {
            var consulta = $"(SELECT IDUsuario, IDEmpleado, IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha', SaldoInicialEfectivo AS 'Efectivo', SaldoInicialTarjeta AS 'Tarjeta', SaldoInicialVales AS 'Vales', SaldoInicialCheque AS 'Cheque', SaldoInicialTransferencia AS 'Transferencia', SaldoInicialCredito AS 'Credito', SaldoInicialAnticipo AS 'Anticipo', CantidadRetiradaDelCorte AS 'CantidadRetirada', (SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' GROUP BY IDUsuario DESC, IDEmpleado DESC, IDCorteDeCaja DESC ORDER BY IDCorteDeCaja DESC LIMIT 1) UNION(SELECT IDUsuario, IDEmpleado, MAX( IDCorteDeCaja ) AS 'IDCaja', FechaOperacion AS 'Fecha', SaldoInicialEfectivo AS 'Efectivo', SaldoInicialTarjeta AS 'Tarjeta', SaldoInicialVales AS 'Vales', SaldoInicialCheque AS 'Cheque', SaldoInicialTransferencia AS 'Transferencia', SaldoInicialCredito AS 'Credito', SaldoInicialAnticipo AS 'Anticipo', CantidadRetiradaDelCorte AS 'CantidadRetirada', (SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDEmpleado IN( {losIDsDeEmpleados} ) AND IDUsuario = '{FormPrincipal.userID}' GROUP BY IDEmpleado HAVING ( IDEmpleado ) ORDER BY IDEmpleado )";

            return consulta;
        }

        public string CargarSaldoInicialSinAbrirCaja(int idUsario, int idEmpleado)
        {
            var consulta = $"SELECT IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha', IF ( SaldoInicialEfectivo IS NULL, 0, SaldoInicialEfectivo ) AS 'Efectivo', IF ( SaldoInicialTarjeta IS NULL, 0, SaldoInicialTarjeta ) AS 'Tarjeta', IF ( SaldoInicialVales IS NULL, 0, SaldoInicialVales ) AS 'Vales', IF ( SaldoInicialCheque IS NULL, 0, SaldoInicialCheque ) AS 'Cheque', IF ( SaldoInicialTransferencia IS NULL, 0, SaldoInicialTransferencia ) AS 'Transferencia', IF ( SaldoInicialCredito IS NULL, 0, SaldoInicialCredito ) AS 'Credito', IF ( SaldoInicialAnticipo IS NULL, 0, SaldoInicialAnticipo ) AS 'Anticipo', IF ( CantidadRetiradaDelCorte IS NULL, 0, CantidadRetiradaDelCorte ) AS 'CantidadRetirada', IF ( ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) IS NULL, 0, ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{idUsario}' AND IDEmpleado = '{idEmpleado}' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string SaldoVentasDepositos(int idUsario, int idEmpleado, int idCorteDeCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, IF ( SUM( Efectivo ) IS NULL, 0, SUM( Efectivo ) ) AS 'Efectivo', IF ( SUM( Tarjeta ) IS NULL, 0, SUM( Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Vales ) IS NULL, 0, SUM( Vales ) ) AS 'Vales', IF ( SUM( Cheque ) IS NULL, 0, SUM( Cheque ) ) AS 'Cheque', IF ( SUM( Transferencia ) IS NULL, 0, SUM( Transferencia ) ) AS 'Transferencia', IF ( SUM( Credito ) IS NULL, 0, SUM( Credito ) ) AS 'Credito', IF ( SUM( Anticipo ) IS NULL, 0, SUM( Anticipo ) ) AS 'Anticipo', IF ( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) + SUM( Credito ) + SUM( Anticipo ) ) IS NULL, 0, ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) + SUM( Credito ) + SUM( Anticipo ) ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{idUsario}' AND IdEmpleado = '{idEmpleado}' AND ID > '{idCorteDeCaja}' AND ( Operacion = 'venta' OR Operacion = 'deposito' OR Operacion = 'anticipo' )";

            return consulta;
        }

        public string SaldoInicialRetiros(int idUsario, int idEmpleado, int idCorteDeCaja)
        {
            var consulta = $"SELECT ID, Operacion, IDUsuario, IdEmpleado, IF ( SUM( Efectivo ) IS NULL, 0, SUM( Efectivo ) ) AS 'Efectivo', IF ( SUM( Tarjeta ) IS NULL, 0, SUM( Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Vales ) IS NULL, 0, SUM( Vales ) ) AS 'Vales', IF ( SUM( Cheque ) IS NULL, 0, SUM( Cheque ) ) AS 'Cheque', IF ( SUM( Transferencia ) IS NULL, 0, SUM( Transferencia ) ) AS 'Transferencia', IF ( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) IS NULL, 0, ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{idUsario}' AND IdEmpleado = '{idEmpleado}' AND ID > '{idCorteDeCaja}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string HistorialDepositosAdminsitrador(int idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT Usr.Usuario AS 'Realizo', IF ( Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia',  Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string cargarHistorialdepositosAdministradorSumaTotal(int idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT IF ( SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string HistorialRetirosAdminsitrador(int idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT Usr.Usuario AS 'Realizo', IF ( Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia',  Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string cargarHistorialRetirosAdministradorSumaTotal(int idUltimoCorteDeCaja)
        {
            var consulta = $"SELECT IF ( SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string HistorialDepositosEmpleado(int idUltimoCorteDeCaja, int idEmpleado)
        {
            var consulta = $"SELECT Usr.nombre AS 'Realizo', IF ( Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia',  Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN empleados AS Usr ON ( Usr.ID = Box.IdEmpleado ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string cargarHistorialdepositosEmpleadoSumaTotal(int idUltimoCorteDeCaja, int idEmpleado)
        {
            var consulta = $"SELECT IF ( SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string HistorialRetirosEmpleado(int idUltimoCorteDeCaja, int idEmpleado)
        {
            var consulta = $"SELECT Usr.nombre AS 'Realizo', IF ( Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia',  Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN empleados AS Usr ON ( Usr.ID = Box.IdEmpleado ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string cargarHistorialRetirosEmpleadoSumaTotal(int idUltimoCorteDeCaja, int idEmpleado)
        {
            var consulta = $"SELECT IF ( SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID > '{idUltimoCorteDeCaja}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string cargarPenultimoSaldoInicialAdministrador()
        {
            var consulta = $"SELECT IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha', SaldoInicialEfectivo AS 'Efectivo', SaldoInicialTarjeta AS 'Tarjeta', SaldoInicialVales AS 'Vales', SaldoInicialCheque AS 'Cheque', SaldoInicialTransferencia AS 'Transferencia', SaldoInicialCredito AS 'Credito', SaldoInicialAnticipo AS 'Anticipo', CantidadRetiradaDelCorte AS 'CantidadRetirada', ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' ORDER BY ID DESC LIMIT 1, 1";

            return consulta;
        }

        public string cargarPenultimaSaldoInicialEmpleado(string idEmpleado)
        {
            var consulta = $"SELECT IDUsuario, IDEmpleado, IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha', SaldoInicialEfectivo AS 'Efectivo', SaldoInicialTarjeta AS 'Tarjeta', SaldoInicialVales AS 'Vales', SaldoInicialCheque AS 'Cheque', SaldoInicialTransferencia AS 'Transferencia', SaldoInicialCredito AS 'Credito', SaldoInicialAnticipo AS 'Anticipo', CantidadRetiradaDelCorte AS 'CantidadRetirada', ( SaldoInicialEfectivo + SaldoInicialTarjeta + SaldoInicialVales + SaldoInicialCheque + SaldoInicialTransferencia ) AS 'SaldoInicial' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idEmpleado}' ORDER BY IDCorteDeCaja DESC LIMIT 1, 1";

            return consulta;
        }

        public string ConsultarAbonosVentaACredito(int idVenta)
        {
            var consulta = $"SELECT IF ( SUM( abonos.Efectivo ) IS NULL, 0, SUM( abonos.Efectivo ) ) AS 'Efectivo', IF ( SUM( abonos.Tarjeta ) IS NULL, 0, SUM( abonos.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( abonos.Vales ) IS NULL, 0, SUM( abonos.Vales ) ) AS 'Vales', IF ( SUM( abonos.Cheque ) IS NULL, 0, SUM( abonos.Cheque ) ) AS 'Cheque', IF ( SUM( abonos.Transferencia ) IS NULL, 0, SUM( abonos.Transferencia ) ) AS 'Transferencia', IF ((SUM( abonos.Efectivo ) + SUM( abonos.Tarjeta ) + SUM( abonos.Vales ) + SUM( abonos.Cheque ) + SUM( abonos.Transferencia )) IS NULL, 0, (SUM( abonos.Efectivo ) + SUM( abonos.Tarjeta ) + SUM( abonos.Vales ) + SUM( abonos.Cheque ) + SUM( abonos.Transferencia ))) AS 'Total de abonos' FROM abonos INNER JOIN ventas ON ( ventas.ID = abonos.IDVenta ) WHERE ventas.ID = '{idVenta}'";

            return consulta;
        }

        public string ChecarSiHayRelacion(int idServicio)
        {
            var consulta = $"SELECT IDServicio, IDProducto, NombreProducto FROM productosdeservicios WHERE IDServicio = '{idServicio}'";

            return consulta;

        }

        public string verificarLaVentaSiTieneAnticiposAplicados(int idVenta)
        {
            var consulta = $"SELECT ID, Importe, Concepto, Cliente, FormaPago, IDVenta, IDUsuario FROM anticipos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDVenta = '{idVenta}'";

            return consulta;
        }

        public string BuscarAnticiposPorTexto(string Filtro)
        {
            var consulta = $"SELECT * FROM anticipos WHERE IDUsuario = {FormPrincipal.userID} AND Cliente LIKE '%{Filtro}%'";

            return consulta;
        }


        public  string BuscarFechaDeExpiracion(int Usuario)
        {
            var consulta = $"SELECT FechaFinLicencia FROM usuarios where ID = {Usuario}";
            return consulta;
        }

        public string BuscarCorreoDelUsuario(int usuario)
        {
            var consulta = $"SELECT Email FROM usuarios WHERE ID = {usuario}";
            return consulta;
        }

        public string BuscarLicenciaDelUsuario(int idusuario)
        {
            var consulta = $"SELECT Licencia FROM usuarios WHERE ID = {idusuario}";
            return consulta;
        }   
        public string BuscarNombreDelUsuario(int idusuario)
        {
            var consulta = $"SELECT NombreCompleto FROM usuarios WHERE ID = {idusuario}";
            return consulta;
        }
        public string eliminarDetalleDinamico(string idusr, string idDetailProdGral)
        {
            var consulta = $"DELETE FROM detallesproductogenerales WHERE '{idusr}' AND ID = '{idDetailProdGral}'";

            return consulta;
        }

        public string BusquedaFechaExpiracionDocumentosSCD(int usuario)
        {
            var consuta = $"SELECT IF(fecha_caducidad_cer = '' OR fecha_caducidad_cer IS NULL,'',fecha_caducidad_cer)AS 'fechaCSD' FROM usuarios WHERE ID ={usuario}";
            return consuta;
        }

        public string BuscarNumeroCertificado(int ususario)
        {
            var consulta = $"SELECT num_certificado FROM usuarios WHERE ID ={ususario}";
            return consulta;
        }

        public string BuscarProductoPorCodigoDeBarras(string codigo)
        {
            var consulta = $"SELECT Nombre, Precio FROM productos WHERE `Status` = 1 AND CodigoBarras ='{codigo}'";
            return consulta;
        }

        public string buscarNombreLogoTipo(int idusuario)
        {
            var consulta = $"SELECT LogoTipo FROM usuarios WHERE ID = {idusuario}";
            return consulta;
        }

        public string imprimirTicketRealizada(int idVentaRealizada)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT DISTINCT CONCAT( DATE_FORMAT( Vent.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( Vent.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeCompra', CONCAT( 'Folio: ', Vent.Folio ) AS 'FolioTicket', IF ( Usr.LogoTipo = '' OR Usr.LogoTipo IS NULL, '', Usr.LogoTipo ) AS 'LogoTipo', IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.nombre_comercial = '' OR Usr.nombre_comercial IS NULL, '', CONCAT( 'NOMBRE COMERCIAL: ', Usr.nombre_comercial ) ) AS 'NombreComercial', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Direccion1', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'Direccion2', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Clte.RazonSocial = '' OR Clte.RazonSocial IS NULL, IF ( Vent.IDCliente = '0', 'PUBLICO GENERAL', '' ), Clte.RazonSocial ) AS 'ClienteNombre', IF ( Clte.RFC = '' OR Clte.RFC IS NULL, IF ( Vent.IDCliente = '0', 'RFC: XAXX010101000', '' ), CONCAT( 'RFC: ', Clte.RFC ) ) AS 'ClienteRFC', CONCAT( IF ( Clte.Calle = '' OR Clte.Calle IS NULL, '', CONCAT( 'DOMICILIO: CALLE/AV.: ', Clte.Calle ) ), IF ( Clte.NoExterior = '' OR Clte.NoExterior IS NULL, '', CONCAT( ' #', Clte.NoExterior ) ), IF ( Clte.NoInterior = '' OR Clte.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Clte.NoInterior ) ), IF ( Clte.Localidad = '' OR Clte.Localidad IS NULL, '', CONCAT( ', LOCALIDAD: ', Clte.Localidad ) ), IF ( Clte.Municipio = '' OR Clte.Municipio IS NULL, '', CONCAT( ', MUNICIPIO: ', Clte.Municipio ) ) ) AS 'ClienteDomicilio', CONCAT( IF ( Clte.Colonia = '' OR Clte.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Clte.Colonia ) ), IF ( Clte.CodigoPostal = '' OR Clte.CodigoPostal IS NULL, '', CONCAT( ', C.P.: ', Clte.CodigoPostal ) ) ) AS 'ClienteColoniaCodigoPostal', IF ( Clte.Email = '' OR Clte.Email IS NULL, '', CONCAT( 'CORREO: ', Clte.Email ) ) AS 'ClienteCorreo', IF ( Clte.Telefono = '' OR Clte.Telefono IS NULL, '', CONCAT( 'TELEFONO: ', Clte.Telefono ) ) AS 'ClienteTelefono', IF ( Vent.FormaPago = '' OR Vent.FormaPago IS NULL, '', CONCAT( 'FORMA DE PAGO: ', UPPER( Vent.FormaPago ) ) ) AS 'FormaDePago', IF ( DetVent.Referencia = '' OR DetVent.Referencia IS NULL, '', CONCAT( 'REFERENCIA: ', UPPER( DetVent.Referencia ) ) ) AS 'Referencia', ProdVent.Cantidad AS 'ProductoCantidad', ProdVent.Nombre AS 'ProductoNombre', CONCAT( '$ ', FORMAT( ProdVent.Precio, 2 ) ) AS 'ProductoPrecio', CONCAT( '$ ', FORMAT( ProdVent.descuento, 2 ) ) AS 'ProductoDescuento', IF ( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ) IS NULL, CONCAT( '$', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ), 2 ) ) ) AS 'ProductoImporte', CONCAT( 'Descuento productos: ', IF ( Vent.Descuento IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Vent.Descuento, 2 ) ) ) ) AS 'DescuentosDeProductos', CONCAT( 'TOTAL: ', IF ( Vent.Total IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ), ' ' ), CONCAT( '$ ', FORMAT( Vent.Total, 2 ), ' ' ) ) ) AS 'TotalGeneral', IF ( editarticket.MensajeTicket IS NULL OR editarticket.MensajeTicket = '.' OR editarticket.MensajeTicket = '', '', editarticket.MensajeTicket ) AS 'MensajeDelTicket', IF ( Vent.Folio = '' OR Vent.Folio IS NULL, '', Vent.Folio ) AS 'CodigoBarrasTicketVenta' FROM productosventa AS ProdVent INNER JOIN ventas AS Vent ON ( ProdVent.IDVenta = Vent.ID ) INNER JOIN detallesventa AS DetVent ON ( DetVent.IDVenta = Vent.ID ) INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) INNER JOIN configuracion AS Config ON ( Config.IDUsuario = Usr.ID ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN editarticket ON ( editarticket.IDUsuario = Usr.ID ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '1' AND ProdVent.IDVenta = '{idVentaRealizada}'";

            return consulta;
        }

        public string ImprimirReporteCaja(string[] datos)
        {
            string primeraFecha = datos[0];
            string segundaFecha = datos[1];
            string conceptos = datos[2];
            string operacion = datos[3];
            string status = datos[4];

            var consulta = $"SELECT SUM(CA.Cantidad) AS Cantidad, SUM(CA.Efectivo) AS Efectivo, SUM(CA.Tarjeta) AS Tarjeta, SUM(CA.Vales) AS Vales, SUM(CA.Cheque) AS Cheque, SUM(CA.Transferencia) AS Transferencia, SUM(CA.Credito) AS Credito, SUM(CA.Anticipo) AS Anticipo, CA.Operacion AS Operacion, CA.Concepto AS Concepto, CA.FechaOperacion AS FechaOperacion FROM Caja CA INNER JOIN ConceptosDinamicos CD ON (CA.IDUsuario = CD.IDUsuario AND CA.Concepto = CD.Concepto) WHERE CA.IDUsuario = {FormPrincipal.userID} AND CA.Operacion = '{operacion}' AND CD.Status = {status} AND CA.Concepto {conceptos} AND CA.FechaOperacion BETWEEN '{primeraFecha}' AND '{segundaFecha}' GROUP BY CD.ID";

            return consulta;
        }

        public string tipoDeTicket()
        {
            var consulta = $"SELECT EditTicket.Usuario, EditTicket.Direccion, EditTicket.ColyCP, EditTicket.RFC, EditTicket.Correo, EditTicket.Telefono, EditTicket.NombreC, EditTicket.DomicilioC, EditTicket.RFCC, EditTicket.CorreoC, EditTicket.TelefonoC, EditTicket.ColyCPC, EditTicket.FormaPagoC, EditTicket.logo, EditTicket.NombreComercial, EditTicket.ticket58mm, EditTicket.ticket80mm, Conf.TicketVenta, EditTicket.Referencia FROM editarticket AS EditTicket INNER JOIN configuracion AS Conf ON ( Conf.IDUsuario = EditTicket.IDUsuario ) WHERE EditTicket.IDUsuario = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string ImpresionTicketAbono(int idVenta)
        {
            var consulta = $"SELECT usr.RazonSocial, CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'ColyCP', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono',IF ( Clte.RazonSocial = '' OR Clte.RazonSocial IS NULL, IF ( Vent.IDCliente = '0', 'PUBLICO GENERAL', '' ), Clte.RazonSocial ) AS 'ClienteNombre',IF ( Clte.RFC = '' OR Clte.RFC IS NULL, IF ( Vent.IDCliente = '0', 'RFC: XAXX010101000', '' ), CONCAT( 'RFC: ', Clte.RFC ) ) AS 'ClienteRFC', CONCAT( IF ( Clte.Calle = '' OR Clte.Calle IS NULL, '', CONCAT( 'DOMICILIO: CALLE/AV.: ', Clte.Calle ) ), IF ( Clte.NoExterior = '' OR Clte.NoExterior IS NULL, '', CONCAT( ' #', Clte.NoExterior ) ), IF ( Clte.NoInterior = '' OR Clte.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Clte.NoInterior ) ), IF ( Clte.Localidad = '' OR Clte.Localidad IS NULL, '', CONCAT( ', LOCALIDAD: ', Clte.Localidad ) ), IF ( Clte.Municipio = '' OR Clte.Municipio IS NULL, '', CONCAT( ', MUNICIPIO: ', Clte.Municipio ) ) ) AS 'ClienteDomicilio', CONCAT( IF ( Clte.Colonia = '' OR Clte.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Clte.Colonia ) ), IF ( Clte.CodigoPostal = '' OR Clte.CodigoPostal IS NULL, '', CONCAT( ', C.P.: ', Clte.CodigoPostal ) ) ) AS 'ClienteColoniaCodigoPostal', IF ( Clte.Email = '' OR Clte.Email IS NULL, '', CONCAT( 'CORREO: ', Clte.Email ) ) AS 'ClienteCorreo', IF ( Clte.Telefono = '' OR Clte.Telefono IS NULL, '', CONCAT( 'TELEFONO: ', Clte.Telefono ) ) AS 'ClienteTelefono', vent.ID AS 'IDVenta', CONCAT( '$ ', FORMAT( vent.Total, 2 ) ) AS 'TotalOriginal', IF ( SUM( abonos.Total ) = '' OR SUM( abonos.Total ) IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( vent.Total - ( SELECT SUM( Total ) AS 'Total' FROM abonos WHERE ID != ( SELECT MAX( ID ) FROM abonos WHERE IDVenta = '{idVenta}' ) AND IDVenta = '{idVenta}' ) ), 2 ) ) ) AS 'SaldoAnterior', IF ( SUM( abonos.Total ) = '' OR SUM( abonos.Total ) IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( SELECT Total FROM abonos WHERE IDVenta = '{idVenta}' ORDER BY ID DESC LIMIT 1 ), 2 ) ) ) AS 'CantidadAbonada', IF ( SUM( abonos.Total ) = '' OR SUM( abonos.Total ) IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( vent.Total - SUM( abonos.Total ), 2 ) ) ) AS 'CantidadRestante', ( SELECT FechaOperacion FROM abonos WHERE IDVenta = '{idVenta}' ORDER BY ID DESC LIMIT 1 ) AS 'FechaUltimoAbono', 'Comprobante de Abono' AS 'comprobante' FROM `ventas` AS vent INNER JOIN usuarios AS usr ON ( usr.ID = vent.IDUsuario ) INNER JOIN abonos ON ( abonos.IDVenta = vent.ID ) INNER JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) WHERE IDVenta = '{idVenta}'";

            return consulta;
        }
         
        public string visualizadorTicketAbono(int idVenta, int idAbono)
        {
            var consulta = $"SELECT usr.RazonSocial, CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'ColyCP', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Clte.RazonSocial = '' OR Clte.RazonSocial IS NULL, IF ( Vent.IDCliente = '0', 'PUBLICO GENERAL', '' ), Clte.RazonSocial ) AS 'ClienteNombre', IF ( Clte.RFC = '' OR Clte.RFC IS NULL, IF ( Vent.IDCliente = '0', 'RFC: XAXX010101000', '' ), CONCAT( 'RFC: ', Clte.RFC ) ) AS 'ClienteRFC', CONCAT( IF ( Clte.Calle = '' OR Clte.Calle IS NULL, '', CONCAT( 'DOMICILIO: CALLE/AV.: ', Clte.Calle ) ), IF ( Clte.NoExterior = '' OR Clte.NoExterior IS NULL, '', CONCAT( ' #', Clte.NoExterior ) ), IF ( Clte.NoInterior = '' OR Clte.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Clte.NoInterior ) ), IF ( Clte.Localidad = '' OR Clte.Localidad IS NULL, '', CONCAT( ', LOCALIDAD: ', Clte.Localidad ) ), IF ( Clte.Municipio = '' OR Clte.Municipio IS NULL, '', CONCAT( ', MUNICIPIO: ', Clte.Municipio ) ) ) AS 'ClienteDomicilio', CONCAT( IF ( Clte.Colonia = '' OR Clte.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Clte.Colonia ) ), IF ( Clte.CodigoPostal = '' OR Clte.CodigoPostal IS NULL, '', CONCAT( ', C.P.: ', Clte.CodigoPostal ) ) ) AS 'ClienteColoniaCodigoPostal', IF ( Clte.Email = '' OR Clte.Email IS NULL, '', CONCAT( 'CORREO: ', Clte.Email ) ) AS 'ClienteCorreo', IF ( Clte.Telefono = '' OR Clte.Telefono IS NULL, '', CONCAT( 'TELEFONO: ', Clte.Telefono ) ) AS 'ClienteTelefono', vent.ID AS 'IDVenta', CONCAT( '$ ', FORMAT( vent.Total, 2 ) ) AS 'TotalOriginal', IF ( SUM( abonos.Total ) = '' OR SUM( abonos.Total ) IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( SELECT CASE WHEN ( SELECT MIN( ID ) FROM abonos WHERE IDVenta = '{idVenta}' ) = '{idAbono}' THEN Vent.Total WHEN ( SELECT MIN( ID ) FROM abonos WHERE IDVenta = '{idVenta}' ) != '{idAbono}' THEN ( vent.Total - ( SELECT SUM( Total ) AS 'Total' FROM abonos WHERE ID BETWEEN ( SELECT MIN( ID ) FROM abonos WHERE IDVenta = '{idVenta}' ) AND ( '{idAbono}' - 1 ) ) ) ELSE SUM( abonos.Total ) END AS 'Saldo Anterior' FROM abonos INNER JOIN ventas AS Vent ON ( Vent.ID = abonos.IDVenta ) WHERE abonos.IDVenta = '{idVenta}' ), 2 ) ) ) AS 'SaldoAnterior', IF ( SUM( abonos.Total ) = '' OR SUM( abonos.Total ) IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( SELECT Total FROM abonos WHERE IDVenta = '{idVenta}' AND ID = '{idAbono}' ORDER BY ID DESC LIMIT 1 ), 2 ) ) ) AS 'CantidadAbonada', IF ( SUM( abonos.Total ) = '' OR SUM( abonos.Total ) IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( vent.Total - ( SELECT SUM( Total ) AS 'Total' FROM abonos WHERE ID BETWEEN ( SELECT MIN( ID ) FROM abonos WHERE IDVenta = '{idVenta}' ) AND ( '{idAbono}' ) ) ), 2 ) ) ) AS 'CantidadRestante', ( SELECT FechaOperacion FROM abonos WHERE IDVenta = '{idVenta}' AND ID = '{idAbono}' ORDER BY ID DESC LIMIT 1 ) AS 'FechaUltimoAbono', 'Comprobante de Abono' AS 'comprobante' FROM `ventas` AS vent INNER JOIN usuarios AS usr ON ( usr.ID = vent.IDUsuario ) INNER JOIN abonos ON ( abonos.IDVenta = vent.ID ) INNER JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) WHERE IDVenta = '{idVenta}'";

            return consulta;
        }

        public string folioDeLaVentaParaElTicket(int idVenta)
        {
            var consulta = $"SELECT Folio FROM ventas WHERE ID = '{idVenta}'";

            return consulta;
        }

        public string getLogoTipoUsuario()
        {
            var consulta = $"SELECT LogoTipo FROM usuarios WHERE ID = '{FormPrincipal.userID}'";

            return consulta;
        }

        public string ticket58mm(int ticket58)
        {
            var consulta = $"UPDATE editarticket SET ticket58mm = {ticket58} WHERE IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public string ticket80mm(int ticket80)
        {
            var consulta = $"UPDATE editarticket SET ticket80mm = {ticket80} WHERE IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public string referenciaTicket(int referencia)
        {
            var consulta = $"UPDATE editarticket SET Referencia = {referencia} WHERE IDUsuario = '{FormPrincipal.userID}';";

            return consulta;
        }

        public string RangosDePrecioConDescuento(int idProducto)
        {
            var consulta = $"SELECT RangoInicial,RangoFinal,Precio FROM descuentomayoreo WHERE IDProducto ={idProducto}";
            return consulta;
        }

        public string BuscarIDPreductoPorCodigoDeBarras(string Codigo)
        {
            var consulta = $"SELECT ID FROM productos WHERE CodigoBarras = '{Codigo}' AND `Status` = 1";

            return consulta;
        }

        public string BuscarDescuentosPorMayoreo(string IDProducto)
        {
            var consulta = $"SELECT RangoInicial, RangoFinal, Precio FROM descuentomayoreo WHERE IDProducto = {IDProducto}";
            return consulta;
        }


        public string BuscarPrecioPorIDdelProducto(string IDproducto)
        {
            var consuta = $"SELECT Precio FROM productos WHERE ID = {IDproducto}";
            return consuta;
        }

        public string imprimirTicketPresupuesto(int idPresupuestoRealizado, int estado = 2)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT DISTINCT CONCAT( DATE_FORMAT( Vent.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( Vent.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeCompra', CONCAT( 'Folio: ', Vent.Folio ) AS 'FolioTicket', IF ( Usr.LogoTipo = '' OR Usr.LogoTipo IS NULL, '', Usr.LogoTipo ) AS 'LogoTipo', IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.nombre_comercial = '' OR Usr.nombre_comercial IS NULL, '', CONCAT( 'NOMBRE COMERCIAL: ', Usr.nombre_comercial ) ) AS 'NombreComercial', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Direccion1', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'Direccion2', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Clte.RazonSocial = '' OR Clte.RazonSocial IS NULL, IF ( Vent.IDCliente = '0', 'PUBLICO GENERAL', '' ), Clte.RazonSocial ) AS 'ClienteNombre', IF ( Clte.RFC = '' OR Clte.RFC IS NULL, IF ( Vent.IDCliente = '0', 'RFC: XAXX010101000', '' ), CONCAT( 'RFC: ', Clte.RFC ) ) AS 'ClienteRFC', CONCAT( IF ( Clte.Calle = '' OR Clte.Calle IS NULL, '', CONCAT( 'DOMICILIO: CALLE/AV.: ', Clte.Calle ) ), IF ( Clte.NoExterior = '' OR Clte.NoExterior IS NULL, '', CONCAT( ' #', Clte.NoExterior ) ), IF ( Clte.NoInterior = '' OR Clte.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Clte.NoInterior ) ), IF ( Clte.Localidad = '' OR Clte.Localidad IS NULL, '', CONCAT( ', LOCALIDAD: ', Clte.Localidad ) ), IF ( Clte.Municipio = '' OR Clte.Municipio IS NULL, '', CONCAT( ', MUNICIPIO: ', Clte.Municipio ) ) ) AS 'ClienteDomicilio', CONCAT( IF ( Clte.Colonia = '' OR Clte.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Clte.Colonia ) ), IF ( Clte.CodigoPostal = '' OR Clte.CodigoPostal IS NULL, '', CONCAT( ', C.P.: ', Clte.CodigoPostal ) ) ) AS 'ClienteColoniaCodigoPostal', IF ( Clte.Email = '' OR Clte.Email IS NULL, '', CONCAT( 'CORREO: ', Clte.Email ) ) AS 'ClienteCorreo', IF ( Clte.Telefono = '' OR Clte.Telefono IS NULL, '', CONCAT( 'TELEFONO: ', Clte.Telefono ) ) AS 'ClienteTelefono', IF ( Vent.FormaPago = '' OR Vent.FormaPago IS NULL, '', CONCAT( 'FORMA DE PAGO: ', UPPER( Vent.FormaPago ) ) ) AS 'FormaDePago', IF ( DetVent.Referencia = '' OR DetVent.Referencia IS NULL, '', CONCAT( 'REFERENCIA: ', UPPER( DetVent.Referencia ) ) ) AS 'Referencia', ProdVent.Cantidad AS 'ProductoCantidad', ProdVent.Nombre AS 'ProductoNombre', CONCAT( '$ ', FORMAT( ProdVent.Precio, 2 ) ) AS 'ProductoPrecio', CONCAT( '$ ', FORMAT( ProdVent.descuento, 2 ) ) AS 'ProductoDescuento', IF ( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ) IS NULL, CONCAT( '$', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ), 2 ) ) ) AS 'ProductoImporte', CONCAT( 'Descuento productos: ', IF ( Vent.Descuento IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Vent.Descuento, 2 ) ) ) ) AS 'DescuentosDeProductos', CONCAT( 'TOTAL: ', IF ( Vent.Total IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ), ' ' ), CONCAT( '$ ', FORMAT( Vent.Total, 2 ), ' ' ) ) ) AS 'TotalGeneral', IF ( editarticket.MensajeTicket IS NULL OR editarticket.MensajeTicket = '.' OR editarticket.MensajeTicket = '', '', editarticket.MensajeTicket ) AS 'MensajeDelTicket', IF ( Vent.Folio = '' OR Vent.Folio IS NULL, '', Vent.Folio ) AS 'CodigoBarrasTicketVenta' FROM productosventa AS ProdVent INNER JOIN ventas AS Vent ON ( ProdVent.IDVenta = Vent.ID ) INNER JOIN detallesventa AS DetVent ON ( DetVent.IDVenta = Vent.ID ) INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) INNER JOIN configuracion AS Config ON ( Config.IDUsuario = Usr.ID ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN editarticket ON ( editarticket.IDUsuario = Usr.ID ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '{estado}' AND ProdVent.IDVenta = '{idPresupuestoRealizado}'";

            return consulta;
        }

        public string imprimirTicketCancelado(int idCencelacionRealizada)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT DISTINCT CONCAT( DATE_FORMAT( Vent.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( Vent.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeCompra', CONCAT( 'Folio: ', Vent.Folio ) AS 'FolioTicket', IF ( Usr.LogoTipo = '' OR Usr.LogoTipo IS NULL, '', Usr.LogoTipo ) AS 'LogoTipo', IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.nombre_comercial = '' OR Usr.nombre_comercial IS NULL, '', CONCAT( 'NOMBRE COMERCIAL: ', Usr.nombre_comercial ) ) AS 'NombreComercial', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Direccion1', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'Direccion2', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Clte.RazonSocial = '' OR Clte.RazonSocial IS NULL, IF ( Vent.IDCliente = '0', 'PUBLICO GENERAL', '' ), Clte.RazonSocial ) AS 'ClienteNombre', IF ( Clte.RFC = '' OR Clte.RFC IS NULL, IF ( Vent.IDCliente = '0', 'RFC: XAXX010101000', '' ), CONCAT( 'RFC: ', Clte.RFC ) ) AS 'ClienteRFC', CONCAT( IF ( Clte.Calle = '' OR Clte.Calle IS NULL, '', CONCAT( 'DOMICILIO: CALLE/AV.: ', Clte.Calle ) ), IF ( Clte.NoExterior = '' OR Clte.NoExterior IS NULL, '', CONCAT( ' #', Clte.NoExterior ) ), IF ( Clte.NoInterior = '' OR Clte.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Clte.NoInterior ) ), IF ( Clte.Localidad = '' OR Clte.Localidad IS NULL, '', CONCAT( ', LOCALIDAD: ', Clte.Localidad ) ), IF ( Clte.Municipio = '' OR Clte.Municipio IS NULL, '', CONCAT( ', MUNICIPIO: ', Clte.Municipio ) ) ) AS 'ClienteDomicilio', CONCAT( IF ( Clte.Colonia = '' OR Clte.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Clte.Colonia ) ), IF ( Clte.CodigoPostal = '' OR Clte.CodigoPostal IS NULL, '', CONCAT( ', C.P.: ', Clte.CodigoPostal ) ) ) AS 'ClienteColoniaCodigoPostal', IF ( Clte.Email = '' OR Clte.Email IS NULL, '', CONCAT( 'CORREO: ', Clte.Email ) ) AS 'ClienteCorreo', IF ( Clte.Telefono = '' OR Clte.Telefono IS NULL, '', CONCAT( 'TELEFONO: ', Clte.Telefono ) ) AS 'ClienteTelefono', IF ( Vent.FormaPago = '' OR Vent.FormaPago IS NULL, '', CONCAT( 'FORMA DE PAGO: ', UPPER( Vent.FormaPago ) ) ) AS 'FormaDePago', IF ( DetVent.Referencia = '' OR DetVent.Referencia IS NULL, '', CONCAT( 'REFERENCIA: ', UPPER( DetVent.Referencia ) ) ) AS 'Referencia', ProdVent.Cantidad AS 'ProductoCantidad', ProdVent.Nombre AS 'ProductoNombre', CONCAT( '$ ', FORMAT( ProdVent.Precio, 2 ) ) AS 'ProductoPrecio', CONCAT( '$ ', FORMAT( ProdVent.descuento, 2 ) ) AS 'ProductoDescuento', IF ( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ) IS NULL, CONCAT( '$', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ), 2 ) ) ) AS 'ProductoImporte', CONCAT( 'Descuento productos: ', IF ( Vent.Descuento IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Vent.Descuento, 2 ) ) ) ) AS 'DescuentosDeProductos', CONCAT( 'TOTAL: ', IF ( Vent.Total IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ), ' ' ), CONCAT( '$ ', FORMAT( Vent.Total, 2 ), ' ' ) ) ) AS 'TotalGeneral', IF ( editarticket.MensajeTicket IS NULL OR editarticket.MensajeTicket = '.' OR editarticket.MensajeTicket = '', '', editarticket.MensajeTicket ) AS 'MensajeDelTicket', IF ( Vent.Folio = '' OR Vent.Folio IS NULL, '', Vent.Folio ) AS 'CodigoBarrasTicketVenta' FROM productosventa AS ProdVent INNER JOIN ventas AS Vent ON ( ProdVent.IDVenta = Vent.ID ) INNER JOIN detallesventa AS DetVent ON ( DetVent.IDVenta = Vent.ID ) INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) INNER JOIN configuracion AS Config ON ( Config.IDUsuario = Usr.ID ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN editarticket ON ( editarticket.IDUsuario = Usr.ID ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '3' AND ProdVent.IDVenta = '{idCencelacionRealizada}'";

            return consulta;
        }

        public string imprimirTicketCredito(int idCreditoOtorgado)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT DISTINCT CONCAT( DATE_FORMAT( Vent.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( Vent.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeCompra', CONCAT( 'Folio: ', Vent.Folio ) AS 'FolioTicket', IF ( Usr.LogoTipo = '' OR Usr.LogoTipo IS NULL, '', Usr.LogoTipo ) AS 'LogoTipo', IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.nombre_comercial = '' OR Usr.nombre_comercial IS NULL, '', CONCAT( 'NOMBRE COMERCIAL: ', Usr.nombre_comercial ) ) AS 'NombreComercial', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Direccion1', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'Direccion2', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Clte.RazonSocial = '' OR Clte.RazonSocial IS NULL, IF ( Vent.IDCliente = '0', 'PUBLICO GENERAL', '' ), Clte.RazonSocial ) AS 'ClienteNombre', IF ( Clte.RFC = '' OR Clte.RFC IS NULL, IF ( Vent.IDCliente = '0', 'RFC: XAXX010101000', '' ), CONCAT( 'RFC: ', Clte.RFC ) ) AS 'ClienteRFC', CONCAT( IF ( Clte.Calle = '' OR Clte.Calle IS NULL, '', CONCAT( 'DOMICILIO: CALLE/AV.: ', Clte.Calle ) ), IF ( Clte.NoExterior = '' OR Clte.NoExterior IS NULL, '', CONCAT( ' #', Clte.NoExterior ) ), IF ( Clte.NoInterior = '' OR Clte.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Clte.NoInterior ) ), IF ( Clte.Localidad = '' OR Clte.Localidad IS NULL, '', CONCAT( ', LOCALIDAD: ', Clte.Localidad ) ), IF ( Clte.Municipio = '' OR Clte.Municipio IS NULL, '', CONCAT( ', MUNICIPIO: ', Clte.Municipio ) ) ) AS 'ClienteDomicilio', CONCAT( IF ( Clte.Colonia = '' OR Clte.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Clte.Colonia ) ), IF ( Clte.CodigoPostal = '' OR Clte.CodigoPostal IS NULL, '', CONCAT( ', C.P.: ', Clte.CodigoPostal ) ) ) AS 'ClienteColoniaCodigoPostal', IF ( Clte.Email = '' OR Clte.Email IS NULL, '', CONCAT( 'CORREO: ', Clte.Email ) ) AS 'ClienteCorreo', IF ( Clte.Telefono = '' OR Clte.Telefono IS NULL, '', CONCAT( 'TELEFONO: ', Clte.Telefono ) ) AS 'ClienteTelefono', IF ( Vent.FormaPago = '' OR Vent.FormaPago IS NULL, '', CONCAT( 'FORMA DE PAGO: ', UPPER( Vent.FormaPago ) ) ) AS 'FormaDePago', IF ( DetVent.Referencia = '' OR DetVent.Referencia IS NULL, '', CONCAT( 'REFERENCIA: ', UPPER( DetVent.Referencia ) ) ) AS 'Referencia', ProdVent.Cantidad AS 'ProductoCantidad', ProdVent.Nombre AS 'ProductoNombre', CONCAT( '$ ', FORMAT( ProdVent.Precio, 2 ) ) AS 'ProductoPrecio', CONCAT( '$ ', FORMAT( ProdVent.descuento, 2 ) ) AS 'ProductoDescuento', IF ( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ) IS NULL, CONCAT( '$', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( ( ( ProdVent.Precio * ProdVent.Cantidad ) - ProdVent.descuento ), 2 ) ) ) AS 'ProductoImporte', CONCAT( 'Descuento productos: ', IF ( Vent.Descuento IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Vent.Descuento, 2 ) ) ) ) AS 'DescuentosDeProductos', CONCAT( 'TOTAL: ', IF ( Vent.Total IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ), ' ' ), CONCAT( '$ ', FORMAT( Vent.Total, 2 ), ' ' ) ) ) AS 'TotalGeneral', IF ( editarticket.MensajeTicket IS NULL OR editarticket.MensajeTicket = '.' OR editarticket.MensajeTicket = '', '', editarticket.MensajeTicket ) AS 'MensajeDelTicket', IF ( Vent.Folio = '' OR Vent.Folio IS NULL, '', Vent.Folio ) AS 'CodigoBarrasTicketVenta' FROM productosventa AS ProdVent INNER JOIN ventas AS Vent ON ( ProdVent.IDVenta = Vent.ID ) INNER JOIN detallesventa AS DetVent ON ( DetVent.IDVenta = Vent.ID ) INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) INNER JOIN configuracion AS Config ON ( Config.IDUsuario = Usr.ID ) LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) LEFT JOIN editarticket ON ( editarticket.IDUsuario = Usr.ID ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '4' AND ProdVent.IDVenta = '{idCreditoOtorgado}'";

            return consulta;
        }

        public string TicketAperturaCajaComoAdministrador(int idVenta)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.LogoTipo = '' OR Usr.LogoTipo IS NULL, '', Usr.LogoTipo ) AS 'LogoTipo', IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.nombre_comercial = '' OR Usr.nombre_comercial IS NULL, '', CONCAT( 'NOMBRE COMERCIAL: ', Usr.nombre_comercial ) ) AS 'NombreComercial', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Direccion1', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'Direccion2', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Vent.IDCliente = '0', '* CAJA ABIERTA *', Vent.Cliente ) AS 'TipoDeMovimiento', CONCAT( IF ( Emp.nombre = '' OR Emp.usuario IS NULL, IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', CONCAT( 'Nombre: ', Usr.RazonSocial ) ), CONCAT( 'Nombre: ', Emp.nombre ) ) ) AS 'NombreEmpleado', CONCAT( IF ( Emp.usuario = '' OR Emp.usuario IS NULL, IF ( Usr.Usuario = '' OR Usr.Usuario IS NULL, '', CONCAT( 'Usuario: ', Usr.Usuario ) ), CONCAT( 'Usuario: ', Emp.usuario ) ) ) AS 'UsuarioEmpleado', CONCAT( DATE_FORMAT( Vent.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( Vent.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeAperturaCaja' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '1' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.ID = '{idVenta}'";

            return consulta;
        }

        public string TicketAperturaCajaComoEmpleado(int idEmpleado, int idVenta)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.LogoTipo = '' OR Usr.LogoTipo IS NULL, '', Usr.LogoTipo ) AS 'LogoTipo', IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.nombre_comercial = '' OR Usr.nombre_comercial IS NULL, '', CONCAT( 'NOMBRE COMERCIAL: ', Usr.nombre_comercial ) ) AS 'NombreComercial', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Direccion1', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'Direccion2', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( Vent.IDCliente = '0', '* CAJA ABIERTA *', Vent.Cliente ) AS 'TipoDeMovimiento', CONCAT( IF ( Emp.nombre = '' OR Emp.usuario IS NULL, IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', CONCAT( 'Nombre: ', Usr.RazonSocial ) ), CONCAT( 'Nombre: ', Emp.nombre ) ) ) AS 'NombreEmpleado', CONCAT( IF ( Emp.usuario = '' OR Emp.usuario IS NULL, IF ( Usr.Usuario = '' OR Usr.Usuario IS NULL, '', CONCAT( 'Usuario: ', Usr.Usuario ) ), CONCAT( 'Usuario: ', Emp.usuario ) ) ) AS 'UsuarioEmpleado', CONCAT( DATE_FORMAT( Vent.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( Vent.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeAperturaCaja' FROM ventas AS Vent INNER JOIN usuarios AS Usr ON ( Usr.ID = Vent.IDUsuario ) LEFT JOIN empleados AS Emp ON ( Emp.ID = Vent.IDEmpleado ) WHERE Vent.`Status` = '1' AND Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.IDEmpleado = '{idEmpleado}' AND Vent.ID = '{idVenta}'";

            return consulta;
        }

        public string obtenerIdUltimoDepositoDeDineroComoAdministrador()
        {
            var consulta = $"SELECT ID FROM caja WHERE Operacion = 'deposito' AND IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string obtenerIdUltimoDepositoDeDineroComoEmpleado(int idEmpleado)
        {
            var consulta = $"SELECT ID FROM caja WHERE Operacion = 'deposito' AND IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string obtenerDatosTicketAgregarDinero(int IdAgregarDinero)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Box.Operacion = '' OR Box.Operacion IS NULL, '', 'Ticket DEPOSITO' ) AS 'TipoTicket', IF ( Box.FechaOperacion = '' OR Box.FechaOperacion IS NULL, '', CONCAT( 'Fecha: ', ( CONCAT( DATE_FORMAT( Box.FechaOperacion, '%W - %e/%M/%Y' ), '', TIME_FORMAT( Box.FechaOperacion, '%h:%i:%s %p' ) ) ) ) ) AS 'FechaDeposito', IF ( Usr.Usuario = '' OR Usr.Usuario IS NULL, '', CONCAT( 'Empleado: ', Usr.Usuario ) ) AS 'Empleado', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Efectivo, 2 ) ) ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Tarjeta, 2 ) ) ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Vales, 2 ) ) ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cheque, 2 ) ) ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Transferencia, 2 ) ) ) AS 'Transferencia', IF ( Box.Cantidad = '' OR Box.Cantidad IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cantidad, 2 ) ) ) AS 'Total', IF ( Box.Concepto = '' OR Box.Concepto IS NULL, '', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID = '{IdAgregarDinero}'";

            return consulta;
        }

        public string obtenerDatosTicketAgregarDineroEmpleado(int IdAgregarDinero)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Box.Operacion = '' OR Box.Operacion IS NULL, '', 'Ticket DEPOSITO' ) AS 'TipoTicket', IF ( Box.FechaOperacion = '' OR Box.FechaOperacion IS NULL, '', CONCAT( 'Fecha: ', ( CONCAT( DATE_FORMAT( Box.FechaOperacion, '%W - %e/%M/%Y' ), '', TIME_FORMAT( Box.FechaOperacion, '%h:%i:%s %p' ) ) ) ) ) AS 'FechaDeposito', IF ( Emp.nombre = '' OR Emp.nombre IS NULL, '', CONCAT( 'Empleado: ', Emp.nombre ) ) AS 'Empleado', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Efectivo, 2 ) ) ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Tarjeta, 2 ) ) ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Vales, 2 ) ) ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cheque, 2 ) ) ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Transferencia, 2 ) ) ) AS 'Transferencia', IF ( Box.Cantidad = '' OR Box.Cantidad IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cantidad, 2 ) ) ) AS 'Total', IF ( Box.Concepto = '' OR Box.Concepto IS NULL, '', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) INNER JOIN empleados AS Emp ON ( Emp.ID = Box.IdEmpleado ) WHERE Box.ID = '{IdAgregarDinero}'";

            return consulta;
        }

        public string obtenerIdUltimoRetiroDeDineroComoAdministrador()
        {
            var consulta = $"SELECT ID FROM caja WHERE Operacion = 'retiro' AND IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string obtenerIdUltimoRetiroDeDineroComoEmpleado(int idEmpleado)
        {
            var consulta = $"SELECT ID FROM caja WHERE Operacion = 'retiro' AND IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{idEmpleado}' ORDER BY ID DESC LIMIT 1";

            return consulta;
        }

        public string obtenerDatosTicketRetirarDinero(int idRetiroDinero)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Box.Operacion = '' OR Box.Operacion IS NULL, '', 'Ticket Retiro' ) AS 'TipoTicket', IF ( Box.FechaOperacion = '' OR Box.FechaOperacion IS NULL, '', CONCAT( 'Fecha: ', ( CONCAT( DATE_FORMAT( Box.FechaOperacion, '%W - %e/%M/%Y' ), '', TIME_FORMAT( Box.FechaOperacion, '%h:%i:%s %p' ) ) ) ) ) AS 'FechaDeposito', IF ( Usr.Usuario = '' OR Usr.Usuario IS NULL, '', CONCAT( 'Empleado: ', Usr.Usuario ) ) AS 'Empleado', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Efectivo, 2 ) ) ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Tarjeta, 2 ) ) ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Vales, 2 ) ) ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cheque, 2 ) ) ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Transferencia, 2 ) ) ) AS 'Transferencia', IF ( Box.Cantidad = '' OR Box.Cantidad IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cantidad, 2 ) ) ) AS 'Total', IF ( Box.Concepto = '' OR Box.Concepto IS NULL, '', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID = '{idRetiroDinero}'";

            return consulta;
        }

        public string obtenerDatosTicketRetirarDineroEmpleado(int IdRetiroDinero)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Box.Operacion = '' OR Box.Operacion IS NULL, '', 'Ticket RETIRO' ) AS 'TipoTicket', IF ( Box.FechaOperacion = '' OR Box.FechaOperacion IS NULL, '', CONCAT( 'Fecha: ', ( CONCAT( DATE_FORMAT( Box.FechaOperacion, '%W - %e/%M/%Y' ), '', TIME_FORMAT( Box.FechaOperacion, '%h:%i:%s %p' ) ) ) ) ) AS 'FechaDeposito', IF ( Emp.nombre = '' OR Emp.nombre IS NULL, '', CONCAT( 'Empleado: ', Emp.nombre ) ) AS 'Empleado', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Efectivo, 2 ) ) ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Tarjeta, 2 ) ) ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Vales, 2 ) ) ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cheque, 2 ) ) ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Transferencia, 2 ) ) ) AS 'Transferencia', IF ( Box.Cantidad = '' OR Box.Cantidad IS NULL, CONCAT( '$ ', FORMAT( 0, 2 ) ), CONCAT( '$ ', FORMAT( Box.Cantidad, 2 ) ) ) AS 'Total', IF ( Box.Concepto = '' OR Box.Concepto IS NULL, '', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) INNER JOIN empleados AS Emp ON ( Emp.ID = Box.IdEmpleado ) WHERE Box.ID = '{IdRetiroDinero}'";

            return consulta;
        }

        public string busquedaPorClientePorFolioVentasGuardadas(string textoPorBuscar)
        {
            var consulta = $"AND ( Vent.Cliente LIKE '%{textoPorBuscar}%' OR Vent.Folio LIKE '%{textoPorBuscar}%' )";

            return consulta;
        }

        public string buscarVentasGuardadasConSinParametroDeBusqueda(string parametroBusqueda)
        {
            var consulta = $"SELECT Vent.ID, Vent.Folio, IF ( Vent.IDCliente = 0, 'Público General', Vent.Cliente ) AS 'Cliente', Vent.Total AS 'Importe', Vent.FechaOperacion AS 'Fecha', Vent.IDCliente FROM ventas AS Vent LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '2' ORDER BY Vent.FechaOperacion DESC";

            if (!string.IsNullOrWhiteSpace(parametroBusqueda))
            {
                consulta = $"SELECT Vent.ID, Vent.Folio, IF ( Vent.IDCliente = 0, 'Público General', Vent.Cliente ) AS 'Cliente', Vent.Total AS 'Importe', Vent.FechaOperacion AS 'Fecha', Vent.IDCliente FROM ventas AS Vent LEFT JOIN clientes AS Clte ON ( Clte.ID = Vent.IDCliente ) WHERE Vent.IDUsuario = '{FormPrincipal.userID}' AND Vent.`Status` = '2' {parametroBusqueda} ORDER BY Vent.FechaOperacion DESC";
            }

            return consulta;
        }

        public string estaHabilitadoElProductoDeLaVentaGuardada(int idProducto)
        {
            var consulta = $"SELECT * FROM Productos WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID} AND Status = 1";
            return consulta;
        }

        public string obtenerProductoPorCodigoExtra(string codigoExtra)
        {
            var consulta = $"SELECT IDProducto FROM codigobarrasextras WHERE CodigoBarraExtra ='{codigoExtra}'";
            return consulta;
        }

        public string BuscarProductoPorCodigoDeBarrasExtra(string id)
        {
            var consulta = $"SELECT Nombre,Precio FROM productos WHERE `Status` = 1 AND ID = {id}";

            return consulta;
        }

        public string impresionTicketAnticipo(int idAnticpo, int idventa = 0)
        {
            string consulta;
            if (idventa == 0)
            {
                consulta = $"SELECT usr.RazonSocial, CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'ColyCP', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', vent.ID AS 'IDVenta', IF ( vent.IDEmpleado != 0, 'Empleado', 'Administrador' ) AS 'Empleado', ant.Concepto, ant.Cliente, ant.Comentarios, ant.ImporteOriginal AS 'TotalRecibido', ( ( vent.Subtotal + vent.IVA16 + vent.IVA8 ) ) AS 'AnticipoAplicado', vent.anticipo - ( vent.Subtotal + vent.IVA16 + vent.IVA8 - vent.Total ) AS 'SaldoRestante', vent.FechaOperacion AS 'FechaOperacion' FROM anticipos AS ant INNER JOIN ventas AS vent ON ( Vent.IDAnticipo = ant.ID ) INNER JOIN usuarios AS usr ON ( usr.ID = vent.IDUsuario ) WHERE vent.IDAnticipo = '{idAnticpo}' ORDER BY vent.ID DESC";
            }
            else
            {
                consulta = $"SELECT usr.RazonSocial, CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'ColyCP', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', vent.ID AS 'IDVenta', IF ( vent.IDEmpleado != 0, 'Empleado', 'Administrador' ) AS 'Empleado', ant.Concepto, ant.Cliente, ant.Comentarios, ant.ImporteOriginal AS 'TotalRecibido', ( ( vent.Subtotal + vent.IVA16 + vent.IVA8 ) ) AS 'AnticipoAplicado', vent.anticipo - ( vent.Subtotal + vent.IVA16 + vent.IVA8 - vent.Total ) AS 'SaldoRestante', vent.FechaOperacion AS 'FechaOperacion' FROM anticipos AS ant INNER JOIN ventas AS vent ON ( Vent.IDAnticipo = ant.ID ) INNER JOIN usuarios AS usr ON ( usr.ID = vent.IDUsuario ) WHERE vent.IDAnticipo = '{idAnticpo}' AND vent.ID = '{idventa}' ORDER BY vent.ID DESC";
            }
            

            return consulta;
        }

        public string visualizadorTicketAnticipo(int idAnticpo)
        {
            var consulta = $"SELECT usr.RazonSocial, CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', CONCAT( 'DIRECCION: ', Usr.Calle ) ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', CONCAT( IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', CONCAT( 'COLONIA: ', Usr.Colonia ) ), IF ( Usr.CodigoPostal = '' OR Usr.CodigoPostal IS NULL, '', CONCAT( ', C.P.:', Usr.CodigoPostal ) ) ) AS 'ColyCP', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'Telefono', IF ( ant.IDVenta = '' OR ant.IDVenta IS NULL, 'N/A', ant.IDVenta ) AS 'IDVenta', IF ( ant.IDEmpleado != 0, 'Empleado', 'Administrador' ) AS 'Empleado', ant.Concepto, ant.Cliente, ant.Comentarios, ant.ImporteOriginal AS 'TotalRecibido', IF ( vent.IDAnticipo = 0 OR vent.IDAnticipo IS NULL, 'N/A', vent.IDAnticipo ) AS 'AnticipoAplicado', IF ( vent.IDAnticipo = 0, '', ant.ImporteOriginal ) AS 'SaldoRestante', ant.Fecha AS 'fechaOperacion' FROM anticipos AS ant INNER JOIN usuarios AS usr ON ( usr.ID = ant.IDUsuario ) LEFT JOIN ventas AS vent ON ( vent.ID = ant.IDVenta ) WHERE ant.ID = '{idAnticpo}'";

            return consulta;
        }

        public string NombreClientePorID(string ID)
        {
            var consulta = $"SELECT RazonSocial FROM clientes WHERE ID = {ID} AND IDUsuario = {FormPrincipal.userID}";
                return  consulta;
        }
        public string PDFNotaDeVentas(int IDVenta)
        {
            var consulta = $"SELECT Usr.RazonSocial, IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Regimen = '' OR Usr.Regimen IS NULL, '', Usr.Regimen ) AS 'Regimen', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', Usr.Calle ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', Usr.Colonia ) AS 'Colonia', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'telefono', CONCAT( 'Folio: ', vent.Folio, ' Serie: ', vent.Serie ) AS 'folio', vent.FechaOperacion AS 'fecha', vent.FormaPago AS 'FormadePago', PV.Cantidad AS 'Cantidad', PV.Nombre AS 'Nombre', CONCAT( '$', FORMAT( PV.Precio, 2 ) ) AS 'precioUnidad', CONCAT( '$', FORMAT( PV.Precio * PV.Cantidad, 2 ) ) AS 'importe', SUBSTRING_INDEX( PV.descuento, '-', 1 ) AS 'DescuentoDirectoProducto', SUBSTRING_INDEX( PV.descuento, '-', - 1 ) AS 'DescuentoPorCiento', vent.Descuento + vent.Total AS 'SubTotal', vent.Descuento AS 'Descuento', vent.Total AS 'total', CONCAT( ' Debo y pagaré a la orden ', Usr.RazonSocial, ' en esta ciudad ____________________________________ antes del dia ', vent.FechaOperacion, ', la cantidad de ', vent.Total, ' valor de la mercancía recibida a mi entera satisfacción. Este pagaré es mercantil y está regido por la Ley General de Títulos y operaciones de Crédito en su artículo 173 y correlativo por no ser pagaré domiciliado. Si este documento no es pagado en su vencimiento causará intereses moratorios del _______% mensual. Acepto(mos): _________________________________________________' ) AS 'Mensaje', IF ( cli.RazonSocial = '' OR cli.RazonSocial IS NULL, 'PUBLICO GENERAL', cli.RazonSocial ) AS 'NombreCliente', IF ( cli.RFC = '' OR cli.RFC IS NULL, 'XAXX010101000', cli.RFC ) AS 'RFCCliente', CONCAT ( IF ( cli.Calle = '' OR cli.Calle IS NULL, '', cli.Calle ), IF ( cli.NoExterior = '' OR cli.NoExterior IS NULL, '', CONCAT( ' #', cli.NoExterior ) ), IF ( cli.NoInterior = '' OR cli.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', cli.NoInterior ) ), IF ( cli.Municipio = '' OR cli.Municipio IS NULL, '', CONCAT( ', ', cli.Municipio ) ), IF ( cli.Estado = '' OR cli.Estado IS NULL, '', CONCAT( ', ', cli.Estado ) ) ) AS 'DomicilioCliente', IF ( cli.Pais = '' OR cli.Pais IS NULL, '', cli.Pais ) AS 'Pais', IF ( cli.Localidad = '' OR cli.Localidad IS NULL, '', cli.Localidad ) AS 'Localidad', IF ( cli.CodigoPostal = '' OR cli.CodigoPostal IS NULL, '', cli.CodigoPostal ) AS 'CP', IF ( cli.Colonia = '' OR cli.Colonia IS NULL, '', cli.Colonia ) AS 'ColoniaCliente', IF ( cli.Email = '' OR cli.Email IS NULL, '', cli.Email ) AS 'CorreoCliente', IF ( cli.Telefono = '' OR cli.Telefono IS NULL, '', cli.Telefono ) AS 'telefonoCliente' FROM ventas AS vent INNER JOIN usuarios AS Usr ON ( usr.ID = vent.IDUsuario ) INNER JOIN productosventa AS PV ON ( PV.IDVenta = vent.ID ) INNER JOIN clientes AS cli ON ( cli.ID = vent.IDCliente ) WHERE usr.ID = '{FormPrincipal.userID}' AND vent.ID = '{IDVenta}'";
            return consulta;
        }
        public string PDFNotaDeVentasHDA(int IDVenta)
        {
            var consulta = $"SELECT IF ( Usr.RazonSocial = '' OR Usr.RazonSocial IS NULL, '', Usr.RazonSocial ) AS 'RazonSocial', IF ( Usr.RFC = '' OR Usr.RFC IS NULL, '', Usr.RFC ) AS 'RFC', IF ( Usr.Regimen OR Usr.Regimen, '', Usr.Regimen ) AS 'Regimen', CONCAT( IF ( Usr.Calle = '' OR Usr.Calle IS NULL, '', Usr.Calle ), IF ( Usr.NoExterior = '' OR Usr.NoExterior IS NULL, '', CONCAT( ' #', Usr.NoExterior ) ), IF ( Usr.NoInterior = '' OR Usr.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', Usr.NoInterior ) ), IF ( Usr.Municipio = '' OR Usr.Municipio IS NULL, '', CONCAT( ', ', Usr.Municipio ) ), IF ( Usr.Estado = '' OR Usr.Estado IS NULL, '', CONCAT( ', ', Usr.Estado ) ) ) AS 'Domicilio', IF ( Usr.Colonia = '' OR Usr.Colonia IS NULL, '', Usr.Colonia ) AS 'Colonia', IF ( Usr.Email = '' OR Usr.Email IS NULL, '', Usr.Email ) AS 'Correo', IF ( Usr.Telefono = '' OR Usr.Telefono IS NULL, '', Usr.Telefono ) AS 'telefono', CONCAT( 'Folio: ', vent.Folio, ' Serie: ', vent.Serie ) AS 'folio', vent.FechaOperacion AS 'fecha', vent.FormaPago AS 'FormadePago', PV.Cantidad AS 'Cantidad', PV.Nombre AS 'Nombre', CONCAT( '$', FORMAT( PV.Precio, 2 ) ) AS 'precioUnidad', CONCAT( '$', FORMAT( PV.Precio * PV.Cantidad, 2 ) ) AS 'importe', SUBSTRING_INDEX( PV.descuento, '-', 1 ) AS 'DescuentoDirectoProducto', SUBSTRING_INDEX( PV.descuento, '-', - 1 ) AS 'DescuentoPorCiento', CONCAT( '$', TRUNCATE ( ( ( PV.Precio * PV.Cantidad ) - SUBSTRING_INDEX( PV.descuento, '-', 1 ) ) / PV.Cantidad, 2 ) ) AS 'PrecioConDescuento', vent.Descuento + vent.Total AS 'SubTotal', vent.Descuento AS 'Descuento', vent.Total AS 'total', CONCAT( ' Debo y pagaré a la orden ', Usr.RazonSocial, ' en esta ciudad ____ antes del dia ', vent.FechaOperacion, ', la cantidad de ', vent.Total, ' valor de la mercancía recibida a mi entera satisfacción. Este pagaré es mercantil y está regido por la Ley General de Títulos y operaciones de Crédito en su artículo 173 y correlativo por no ser pagaré domiciliado. Si este documento no es pagado en su vencimiento causará intereses moratorios del _% mensual. Acepto(mos): _______' ) AS 'Mensaje', IF ( cli.RazonSocial = '' OR cli.RazonSocial IS NULL, 'PUBLICO GENERAL', cli.RazonSocial ) AS 'NombreCliente', IF ( cli.RFC = '' OR cli.RFC IS NULL, 'XAXX010101000', cli.RFC ) AS 'RFCCliente', CONCAT ( IF ( cli.Calle = '' OR cli.Calle IS NULL, '', cli.Calle ), IF ( cli.NoExterior = '' OR cli.NoExterior IS NULL, '', CONCAT( ' #', cli.NoExterior ) ), IF ( cli.NoInterior = '' OR cli.NoInterior IS NULL, '', CONCAT( ', INTERIOR: ', cli.NoInterior ) ), IF ( cli.Municipio = '' OR cli.Municipio IS NULL, '', CONCAT( ', ', cli.Municipio ) ), IF ( cli.Estado = '' OR cli.Estado IS NULL, '', CONCAT( ', ', cli.Estado ) ) ) AS 'DomicilioCliente', IF ( cli.Pais = '' OR cli.Pais IS NULL, '', cli.Pais ) AS 'Pais', IF ( cli.Localidad = '' OR cli.Localidad IS NULL, '', cli.Localidad ) AS 'Localidad', IF ( cli.CodigoPostal = '' OR cli.CodigoPostal IS NULL, '', cli.CodigoPostal ) AS 'CP', IF ( cli.Colonia = '' OR cli.Colonia IS NULL, '', cli.Colonia ) AS 'ColoniaCliente', IF ( cli.Email = '' OR cli.Email IS NULL, '', cli.Email ) AS 'CorreoCliente', IF ( cli.Telefono = '' OR cli.Telefono IS NULL, '', cli.Telefono ) AS 'telefonoCliente' FROM ventas AS vent INNER JOIN usuarios AS Usr ON ( usr.ID = vent.IDUsuario ) INNER JOIN productosventa AS PV ON ( PV.IDVenta = vent.ID ) INNER JOIN clientes AS cli ON ( cli.ID = vent.IDCliente ) WHERE usr.ID = '{FormPrincipal.userID}' AND vent.ID = '{IDVenta}'";
            return consulta;
        }

        public string NombreUsuarioActivo(int idAdministrador)
        {
            var consulta = $"SELECT Usuario FROM usuarios WHERE ID = '{idAdministrador}'";

            return consulta;
        }
        public string buscarNombreLogoTipo2(int idusuario)
        {
            var consulta = $"SELECT IF(LogoTipo ='' OR LogoTipo IS NULL,NULL,LogoTipo)as 'Logo' FROM usuarios WHERE ID = {idusuario}";
            return consulta;
        }
        public string PermisoEnviarCorreoAnticipo(int IDEmpleado, int IDUsuario)
        {
            var consulta = $"SELECT PermisoCorreoAnticipo FROM permisosconfiguracion WHERE IDEmpleado = {IDEmpleado} AND IDUsuario = {IDUsuario} ORDER BY ID DESC LIMIT 1";
            return consulta;
        }
        public string ConsultaIDProveedor(string Nombre, int idUsuario)
        {
            var consulta = $"SELECT ID FROM proveedores WHERE IDUsuario = {idUsuario} AND Nombre = '{Nombre}'";
            return consulta;
        }

        public string ConsultaNombreYLicenciaUsuaio(string Usuario)
        {
            var consulta = $"SELECT NombreCompleto,Licencia FROM usuarios WHERE Usuario = '{Usuario}'";
            return consulta;
        }

        public string encabezadoCorteDeCaja(int IDCorteDeCaja)
        {
            var consulta = $"SET lc_time_names = 'es_MX'; SELECT Usr.RazonSocial, CONCAT( 'USUARIO (', IF ( Emp.nombre = '' OR Emp.nombre IS NULL, IF ( Usr.Usuario = '' OR Usr.Usuario IS NULL, '', Usr.Usuario ), Emp.nombre ), ')' ) AS 'Usuario', CONCAT( 'No Folio: ', Box.NumFolio ) AS 'Folio', 'CORTE DE CAJA' AS 'Movimiento', CONCAT( 'Fecha: ', DATE_FORMAT( HistCorteCaja.FechaOperacion, '%W - %e/%M/%Y' ), ' ', TIME_FORMAT( HistCorteCaja.FechaOperacion, '%h:%i:%s %p' ) ) AS 'FechaDeCorte' FROM historialcortesdecaja AS HistCorteCaja INNER JOIN caja AS Box ON ( Box.ID = HistCorteCaja.IDCorteDeCaja ) INNER JOIN usuarios AS Usr ON ( Usr.ID = HistCorteCaja.IDUsuario ) LEFT JOIN empleados AS Emp ON ( Emp.ID = HistCorteCaja.IDEmpleado ) WHERE HistCorteCaja.IDCorteDeCaja = '{IDCorteDeCaja}'";

            return consulta;
        }

        public string VerificarSiEsCorteDeEmpleado(int IDCorteCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDEmpleado AS 'IdEmpleado' FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDCorteDeCaja = '{IDCorteCaja}'";

            return consulta;
        }

        public string BuscadorReportesCorteDeCajaAdministrador(string fechaInicio, string fechaFinal, string busqueda)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja AS 'ID', HistCorteCaja.FechaOperacion, HistCorteCaja.IDEmpleado AS 'IdEmpleado', Emp.nombre, Usr.Usuario FROM historialcortesdecaja AS HistCorteCaja INNER JOIN caja AS Box ON ( Box.ID = HistCorteCaja.IDCorteDeCaja ) INNER JOIN usuarios AS Usr ON ( Usr.ID = HistCorteCaja.IDUsuario ) LEFT JOIN empleados AS Emp ON ( Emp.ID = HistCorteCaja.IDEmpleado ) WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.FechaOperacion >= '{fechaInicio} 00:00:00' AND HistCorteCaja.FechaOperacion <= '{fechaFinal} 23:59:59' AND ( ( Usr.Usuario LIKE '%{busqueda}%' AND Emp.nombre IS NULL ) OR ( Emp.nombre LIKE '%{busqueda}%' ) ) ORDER BY HistCorteCaja.FechaOperacion DESC";

            return consulta;
        }

        public string intervaloFechasAbonosRealizadosAdministrador(int idCajaMayor, int idCajaMenor)
        {
            var consulta = $"SELECT MAX(FechaOperacion) AS 'LimiteSuperior', MIN(FechaOperacion) AS 'LimiteInferior' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID <= '{idCajaMayor}' AND ID >= '{idCajaMenor}'";

            return consulta;
        }

        public string intervaloVentasRealizadasAdministrador(int IDCorteDeCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '0' AND HistCorteCaja.IDCorteDeCaja <= '{IDCorteDeCaja}' ORDER BY ID DESC LIMIT 2";

            return consulta;
        }

        public string tablaVentasRealizadasAdministrador(string fechaLimiteSuperior, string fechaLimiteInferior, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', FORMAT( IF(SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo )), 2 ) ) AS 'Efectivo', CONCAT( '$ ', FORMAT( IF(SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta )), 2 ) ) AS 'Tarjeta', CONCAT( '$ ', FORMAT( IF(SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales )), 2 ) ) AS 'Vales', CONCAT( '$ ', FORMAT( IF(SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque )), 2 ) ) AS 'Cheque', CONCAT( '$ ', FORMAT( IF(SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia )), 2 ) ) AS 'Transferencia', CONCAT( '$ ', FORMAT( IF(SUM( Credito ) = '' OR SUM( Credito ) IS NULL, '0', SUM( Credito )), 2 ) ) AS 'Credito', ( CONCAT( '$ ', FORMAT( CONVERT ( ( SELECT IF ( Abono.Total = '' OR Abono.Total IS NULL, '0', SUM( Abono.Total ) ) FROM abonos AS Abono WHERE Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado = '0' AND Abono.FechaOperacion >= '{fechaLimiteInferior}' AND Abono.FechaOperacion <= '{fechaLimiteSuperior}' ), DECIMAL ), 2 ) ) ) AS 'Abonos', CONCAT( '$ ', FORMAT( IF(SUM( Anticipo ) = '' OR SUM( Anticipo ) IS NULL, '0', SUM( Anticipo )), 2 ) ) AS 'Anticipo', CONCAT( '$ ', FORMAT( ( IF(SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo )) + IF(SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta )) + IF(SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales )) + IF(SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque )) + IF(SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia )) + ( ( IF(SUM( Credito ) = '' OR SUM( Credito ) IS NULL, '0', SUM( Credito )) ) ) + IF(SUM( Anticipo ) = '' OR SUM( Anticipo ) IS NULL, '0', SUM( Anticipo )) ), 2 ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta'";

            return consulta;
        }

        public string tablaAnticiposRecibidosAdministrador(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) = '' OR ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ), 2 ) ) ) AS 'TotalAnticipos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo'";

            return consulta;
        }

        public string tablaDineroAgregadoAdministrador(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) = '' OR ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ), 2 ) ) ) AS 'TotalDepositos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito'";

            return consulta;
        }

        public string tablaDineroRetiradoAdministrador(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) = '' OR ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ), 2 ) ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string tablaTotalDeCajaAlCorteAdministrador(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Efectivo', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Tarjeta', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Vales', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Cheque', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Transferencia', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Total', CONCAT( '$ ', FORMAT( ( SELECT IF ( CantidadRetiradaDelCorte = '' OR CantidadRetiradaDelCorte IS NULL, '0', CantidadRetiradaDelCorte ) AS 'Retiro' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaInicio}' ), 2 ) ) AS 'Retiro', CONCAT( '$ ', FORMAT( ( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaFin}' ) ) ) - ( SELECT IF ( CantidadRetiradaDelCorte = '' OR CantidadRetiradaDelCorte IS NULL, '0', CantidadRetiradaDelCorte ) AS 'Retiro' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND IDCorteDeCaja = '{IDCajaInicio}' ), 2 ) ) AS 'Resto'";

            return consulta;
        }

        public string intervaloVentasRealizadasEnEmpleadoDesdeAdministrador(int IDEmpleado, int IDCorteDeCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '{IDEmpleado}' AND HistCorteCaja.IDCorteDeCaja <= '{IDCorteDeCaja}' ORDER BY ID DESC LIMIT 2";

            return consulta;
        }

        public string intervaloFechasAbonosRealizadosEnEmpleadoDesdeAdministrador(int IDEmpleado, int idCajaMayor, int idCajaMenor)
        {
            var consulta = $"SELECT MAX(FechaOperacion) AS 'LimiteSuperior', MIN(FechaOperacion) AS 'LimiteInferior' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{IDEmpleado}' AND ID <= '{idCajaMayor}' AND ID >= '{idCajaMenor}'";

            return consulta;
        }

        public string tablaVentasRealizadasEnEmpleadoDesdeAdministrador(int IDEmpleado, string fechaLimiteSuperior, string fechaLimiteInferior, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', FORMAT( IF(SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo )), 2 ) ) AS 'Efectivo', CONCAT( '$ ', FORMAT( IF(SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta )), 2 ) ) AS 'Tarjeta', CONCAT( '$ ', FORMAT( IF(SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales )), 2 ) ) AS 'Vales', CONCAT( '$ ', FORMAT( IF(SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque )), 2 ) ) AS 'Cheque', CONCAT( '$ ', FORMAT( IF(SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia )), 2 ) ) AS 'Transferencia', CONCAT( '$ ', FORMAT( IF(SUM( Credito ) = '' OR SUM( Credito ) IS NULL, '0', SUM( Credito )), 2 ) ) AS 'Credito', ( CONCAT( '$ ', FORMAT( CONVERT ( ( SELECT IF ( Abono.Total = '' OR Abono.Total IS NULL, '0', SUM( Abono.Total ) ) FROM abonos AS Abono WHERE Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado = '{IDEmpleado}' AND Abono.FechaOperacion >= '{fechaLimiteInferior}' AND Abono.FechaOperacion <= '{fechaLimiteSuperior}' ), DECIMAL ), 2 ) ) ) AS 'Abonos', CONCAT( '$ ', FORMAT( IF(SUM( Anticipo ) = '' OR SUM( Anticipo ) IS NULL, '0', SUM( Anticipo )), 2 ) ) AS 'Anticipo', CONCAT( '$ ', FORMAT( ( IF(SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo )) + IF(SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta )) + IF(SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales )) + IF(SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque )) + IF(SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia )) + ( ( IF(SUM( Credito ) = '' OR SUM( Credito ) IS NULL, '0', SUM( Credito )) ) ) + IF(SUM( Anticipo ) = '' OR SUM( Anticipo ) IS NULL, '0', SUM( Anticipo )) ), 2 ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta'";

            return consulta;
        }

        public string tablaAnticiposRecibidosEnEmpleadoDesdeAdministrador(int IDEmpleado, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) = '' OR ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ), 2 ) ) ) AS 'TotalAnticipos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo'";

            return consulta;
        }

        public string tablaDineroAgregadoEnEmpleadoDesdeAdministrador(int IDEmpleado, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) = '' OR ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ), 2 ) ) ) AS 'TotalDepositos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito'";

            return consulta;
        }

        public string tablaDineroRetiradoEnEmpleadoDesdeAdministrador(int IDEmpleado, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) = '' OR ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) ), 2 ) ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IdEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string tablaTotalDeCajaAlCorteEnEmpleadoDesdeAdministrador(int IDEmpleado, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Efectivo', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Tarjeta', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Vales', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Cheque', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Transferencia', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Total', CONCAT( '$ ', FORMAT( ( SELECT IF ( CantidadRetiradaDelCorte = '' OR CantidadRetiradaDelCorte IS NULL, '0', CantidadRetiradaDelCorte ) AS 'Retiro' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaInicio}' ), 2 ) ) AS 'Retiro', CONCAT( '$ ', FORMAT( ( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) ) - ( SELECT IF ( CantidadRetiradaDelCorte = '' OR CantidadRetiradaDelCorte IS NULL, '0', CantidadRetiradaDelCorte ) AS 'Retiro' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{IDEmpleado}' AND IDCorteDeCaja = '{IDCajaInicio}' ), 2 ) ) AS 'Resto'";

            return consulta;
        }

        public string BuscadorReporteCorteDeCajaEmpleado(string fechaInicio, string fechaFinal, string busqueda)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja AS 'ID', HistCorteCaja.FechaOperacion, HistCorteCaja.IDEmpleado AS 'IdEmpleado', Emp.nombre, Usr.Usuario FROM historialcortesdecaja AS HistCorteCaja INNER JOIN caja AS Box ON ( Box.ID = HistCorteCaja.IDCorteDeCaja ) INNER JOIN usuarios AS Usr ON ( Usr.ID = HistCorteCaja.IDUsuario ) LEFT JOIN empleados AS Emp ON ( Emp.ID = HistCorteCaja.IDEmpleado ) WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '{FormPrincipal.id_empleado}' AND HistCorteCaja.FechaOperacion >= '{fechaInicio} 00:00:00' AND HistCorteCaja.FechaOperacion <= '{fechaFinal} 23:59:59' AND ( Usr.Usuario LIKE '%{busqueda}%' OR Emp.nombre LIKE '%{busqueda}%' ) ORDER BY HistCorteCaja.FechaOperacion DESC";

            return consulta;
        }

        public string intervaloVentasRealizadasEmpleado(int IDCorteDeCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '{FormPrincipal.id_empleado}' AND HistCorteCaja.IDCorteDeCaja <= '{IDCorteDeCaja}' ORDER BY ID DESC LIMIT 2";

            return consulta;
        }

        public string intervaloFechasAbonosRealizadosEmpleado(int idCajaMayor, int idCajaMenor)
        {
            var consulta = $"SELECT MAX(FechaOperacion) AS 'LimiteSuperior', MIN(FechaOperacion) AS 'LimiteInferior' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{idCajaMayor}' AND ID >= '{idCajaMenor}'";

            return consulta;
        }

        public string tablaVentasRealizadasEmpleado(string fechaLimiteSuperior, string fechaLimiteInferior, int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', FORMAT( IF ( Efectivo = '' OR Efectivo IS NULL, '0', SUM( Efectivo ) ), 2 ) ) AS 'Efectivo', CONCAT( '$ ', FORMAT( IF ( Tarjeta = '' OR Tarjeta IS NULL, '0', SUM( Tarjeta ) ), 2 ) ) AS 'Tarjeta', CONCAT( '$ ', FORMAT( IF ( Vales = '' OR Vales IS NULL, '0', SUM( Vales ) ), 2 ) ) AS 'Vales', CONCAT( '$ ', FORMAT( IF ( Cheque = '' OR Cheque IS NULL, '0', SUM( Cheque ) ), 2 ) ) AS 'Cheque', CONCAT( '$ ', FORMAT( IF ( Transferencia = '' OR Transferencia IS NULL, '0', SUM( Transferencia ) ), 2 ) ) AS 'Transferencia', CONCAT( '$ ', FORMAT( ( ( IF ( SUM( Credito ) = '' OR SUM( Credito ) IS NULL, '0', SUM( Credito ) ) ) ), 2 ) ) AS 'Credito', ( CONCAT( '$ ', FORMAT( CONVERT ( ( SELECT IF ( SUM( Abono.Total ) = '' OR SUM( Abono.Total ) IS NULL, '0', SUM( Abono.Total ) ) FROM abonos AS Abono WHERE Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado = '{FormPrincipal.id_empleado}' AND Abono.FechaOperacion >= '{fechaLimiteInferior}' AND Abono.FechaOperacion <= '{fechaLimiteSuperior}' ), DECIMAL ), 2 ) ) ) AS 'Abonos', CONCAT( '$ ', FORMAT( IF ( SUM( Anticipo ) = '' OR SUM( Anticipo ) IS NULL, '0', SUM( Anticipo ) ), 2 ) ) AS 'Anticipo', CONCAT( '$ ', FORMAT( ( IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) + IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) + IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) + IF ( Cheque = '' OR Cheque IS NULL, '0', SUM( Cheque ) ) + IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) + ( ( IF ( SUM( Credito ) = '' OR SUM( Credito ) IS NULL, '0', SUM( Credito ) ) - ( SELECT IF ( SUM( Abono.Total ) = '' OR SUM( Abono.Total ) IS NULL, '0', SUM( Abono.Total ) ) FROM abonos AS Abono WHERE Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado = '{FormPrincipal.id_empleado}' AND Abono.FechaOperacion >= '{fechaLimiteInferior}' AND Abono.FechaOperacion <= '{fechaLimiteSuperior}' ) ) + ( SELECT IF ( SUM( Abono.Total ) = '' OR SUM( Abono.Total ) IS NULL, '0', SUM( Abono.Total ) ) FROM abonos AS Abono WHERE Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado = '{FormPrincipal.id_empleado}' AND Abono.FechaOperacion >= '{fechaLimiteInferior}' AND Abono.FechaOperacion <= '{fechaLimiteSuperior}' ) ) + IF ( SUM( Anticipo ) = '' OR SUM( Anticipo ) IS NULL, '0', SUM( Anticipo ) ) ), 2 ) ) AS 'TotalVentas' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta'";

            return consulta;
        }

        public string tablaAnticiposRecibidosEmpleado(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( Efectivo = '' OR Efectivo IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( Tarjeta = '' OR Tarjeta IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( Vales = '' OR Vales IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( Cheque = '' OR Cheque IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( Transferencia = '' OR Transferencia IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) = '' OR ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ), 2 ) ) ) AS 'TotalAnticipos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo'";

            return consulta;
        }

        public string tablaDineroAgregadoEmpleado(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( Efectivo = '' OR Efectivo IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( Tarjeta = '' OR Tarjeta IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( Vales = '' OR Vales IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( Cheque = '' OR Cheque IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( Transferencia = '' OR Transferencia IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) = '' OR ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ), 2 ) ) ) AS 'TotalDepositos' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito'";

            return consulta;
        }

        public string tablaDineroRetiradoEmpleado(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', IF ( Efectivo = '' OR Efectivo IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Efectivo ), 2 ) ) ) AS 'Efectivo', CONCAT( '$ ', IF ( Tarjeta = '' OR Tarjeta IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Tarjeta ), 2 ) ) ) AS 'Tarjeta', CONCAT( '$ ', IF ( Vales = '' OR Vales IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Vales ), 2 ) ) ) AS 'Vales', CONCAT( '$ ', IF ( Cheque = '' OR Cheque IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Cheque ), 2 ) ) ) AS 'Cheque', CONCAT( '$ ', IF ( Transferencia = '' OR Transferencia IS NULL, FORMAT( '0', 2 ), FORMAT( SUM( Transferencia ), 2 ) ) ) AS 'Transferencia', CONCAT( '$ ', IF ( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) = '' OR ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ) IS NULL, FORMAT( '0', 2 ), FORMAT( ( SUM( Efectivo ) + SUM( Tarjeta ) + SUM( Vales ) + SUM( Cheque ) + SUM( Transferencia ) ), 2 ) ) ) AS 'TotalRetiros' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro'";

            return consulta;
        }

        public string tablaTotalDeCajaAlCorteEmpleado(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Efectivo', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Tarjeta', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Vales', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Cheque', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Transferencia', CONCAT( '$ ', FORMAT( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ), 2 ) ) AS 'Total', CONCAT( '$ ', FORMAT( ( SELECT IF ( CantidadRetiradaDelCorte = '' OR CantidadRetiradaDelCorte IS NULL, '0', CantidadRetiradaDelCorte ) AS 'Retiro' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaInicio}' ), 2 ) ) AS 'Retiro', CONCAT( '$ ', FORMAT( ( ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Efectivo ) = '' OR SUM( Efectivo ) IS NULL, '0', SUM( Efectivo ) ) AS 'Efectivo' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialEfectivo = '' OR SaldoInicialEfectivo IS NULL, '0', SaldoInicialEfectivo ) AS 'Efectivo' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Tarjeta ) = '' OR SUM( Tarjeta ) IS NULL, '0', SUM( Tarjeta ) ) AS 'Tarjeta' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTarjeta = '' OR SaldoInicialTarjeta IS NULL, '0', SaldoInicialTarjeta ) AS 'Tarjeta' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Vales ) = '' OR SUM( Vales ) IS NULL, '0', SUM( Vales ) ) AS 'Vales' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialVales = '' OR SaldoInicialVales IS NULL, '0', SaldoInicialVales ) AS 'Vales' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Cheque ) = '' OR SUM( Cheque ) IS NULL, '0', SUM( Cheque ) ) AS 'Cheque' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialCheque = '' OR SaldoInicialCheque IS NULL, '0', SaldoInicialCheque ) AS 'Cheque' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) + ( ( ( ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'venta' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'anticipo' ), DECIMAL ) ) + ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'deposito' ), DECIMAL ) ) ) - ( CONVERT ( ( SELECT IF ( SUM( Transferencia ) = '' OR SUM( Transferencia ) IS NULL, '0', SUM( Transferencia ) ) AS 'Transferencia' FROM caja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND ID <= '{IDCajaInicio}' AND ID >= '{IDCajaFin}' AND Operacion = 'retiro' ), DECIMAL ) ) ) + ( SELECT IF ( SaldoInicialTransferencia = '' OR SaldoInicialTransferencia IS NULL, '0', SaldoInicialTransferencia ) AS 'Transferencia' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaFin}' ) ) ) - ( SELECT IF ( CantidadRetiradaDelCorte = '' OR CantidadRetiradaDelCorte IS NULL, '0', CantidadRetiradaDelCorte ) AS 'Retiro' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{FormPrincipal.id_empleado}' AND IDCorteDeCaja = '{IDCajaInicio}' ), 2 ) ) AS 'Resto'";

            return consulta;
        }

        public string intervaloMovimientosRealizadasAdministrador(int IDCorteDeCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '0' AND HistCorteCaja.IDCorteDeCaja <= '{IDCorteDeCaja}' ORDER BY ID DESC LIMIT 2";

            return consulta;
        }

        public string ReimprimirHistorialDepositosAdminsitrador(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT Usr.Usuario AS 'Realizo', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia', Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string ReimprimirCargarHistorialDepositosAdministradorSumaTotal(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT IF ( SUM(Box.Efectivo) = '' OR SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM(Box.Tarjeta) = '' OR SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM(Box.Vales) = '' OR SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM(Box.Cheque) = '' OR SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM(Box.Transferencia) = '' OR SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string ReimprmirHistorialRetirosAdminsitrador(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT Usr.Usuario AS 'Realizo', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia', Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string ReimprimirCargarHistorialRetirosAdministradorSumaTotal(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT IF ( SUM(Box.Efectivo) = '' OR SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM(Box.Tarjeta) = '' OR SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM(Box.Vales) = '' OR SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM(Box.Cheque) = '' OR SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM(Box.Transferencia) = '' OR SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '0'";

            return consulta;
        }

        public string intervaloMovimientosRealizadasEnEmpleadoDesdeAdministrador(int IDEmpleado, int IDCorteDeCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '{IDEmpleado}' AND HistCorteCaja.IDCorteDeCaja <= '{IDCorteDeCaja}' ORDER BY ID DESC LIMIT 2";

            return consulta;
        }

        public string ReimprimirHistorialDepositosEmpleadoDesdeAdministrador(int IDCajaInicio, int IDCajaFin, int idEmpleado)
        {
            var consulta = $"SELECT Usr.nombre AS 'Realizo', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia', Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN empleados AS Usr ON ( Usr.ID = Box.IdEmpleado ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string ReimprimirCargarHistorialdepositosEmpleadoSumaTotalDesdeAdministrador(int IDCajaInicio, int IDCajaFin, int idEmpleado)
        {
            var consulta = $"SELECT IF ( SUM(Box.Efectivo) = '' OR SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM(Box.Tarjeta) = '' OR SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM(Box.Vales) = '' OR SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM(Box.Cheque) = '' OR SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM(Box.Transferencia) = '' OR SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string ReimprimirHistorialRetirosEmpleadoDesdeAdministrador(int IDCajaInicio, int IDCajaFin, int idEmpleado)
        {
            var consulta = $"SELECT Usr.nombre AS 'Realizo', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia', Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN empleados AS Usr ON ( Usr.ID = Box.IdEmpleado ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string ReimprimirCargarHistorialRetirosEmpleadoSumaTotalDesdeAdministrador(int IDCajaInicio, int IDCajaFin, int idEmpleado)
        {
            var consulta = $"SELECT IF ( SUM( Box.Efectivo ) = '' OR SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM( Box.Tarjeta ) = '' OR SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Box.Vales ) = '' OR SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM( Box.Cheque ) = '' OR SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM( Box.Transferencia ) = '' OR SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{idEmpleado}'";

            return consulta;
        }

        public string intervaloMovimientosRealizadasEmpleado(int IDCorteDeCaja)
        {
            var consulta = $"SELECT HistCorteCaja.IDCorteDeCaja FROM historialcortesdecaja AS HistCorteCaja WHERE HistCorteCaja.IDUsuario = '{FormPrincipal.userID}' AND HistCorteCaja.IDEmpleado = '{FormPrincipal.id_empleado}' AND HistCorteCaja.IDCorteDeCaja <= '{IDCorteDeCaja}' ORDER BY ID DESC LIMIT 2";

            return consulta;
        }

        public string ReimprimirHistorialDepositosEmpleado(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT Usr.nombre AS 'Realizo', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia', Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN empleados AS Usr ON ( Usr.ID = Box.IdEmpleado ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{FormPrincipal.id_empleado}'";

            return consulta;
        }

        public string ReimprimirCargarHistorialdepositosEmpleadoSumaTotal(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT IF ( SUM(Box.Efectivo) = '' OR SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM(Box.Tarjeta) = '' OR SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM(Box.Vales) = '' OR SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM(Box.Cheque) = '' OR SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM(Box.Transferencia) = '' OR SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'deposito' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{FormPrincipal.id_empleado}'";

            return consulta;
        }

        public string ReimprimirHistorialRetirosEmpleado(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT Usr.nombre AS 'Realizo', IF ( Box.Efectivo = '' OR Box.Efectivo IS NULL, 0, Box.Efectivo ) AS 'Efectivo', IF ( Box.Tarjeta = '' OR Box.Tarjeta IS NULL, 0, Box.Tarjeta ) AS 'Tarjeta', IF ( Box.Vales = '' OR Box.Vales IS NULL, 0, Box.Vales ) AS 'Vales', IF ( Box.Cheque = '' OR Box.Cheque IS NULL, 0, Box.Cheque ) AS 'Cheque', IF ( Box.Transferencia = '' OR Box.Transferencia IS NULL, 0, Box.Transferencia ) AS 'Transferencia', Box.FechaOperacion AS 'Fecha', IF ( Box.Concepto IS NULL OR Box.Concepto = '', 'N/A', Box.Concepto ) AS 'Concepto' FROM caja AS Box INNER JOIN empleados AS Usr ON ( Usr.ID = Box.IdEmpleado ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{FormPrincipal.id_empleado}'";

            return consulta;
        }

        public string ReimprimirCargarHistorialRetirosEmpleadoSumaTotal(int IDCajaInicio, int IDCajaFin)
        {
            var consulta = $"SELECT IF ( SUM( Box.Efectivo ) = '' OR SUM( Box.Efectivo ) IS NULL, 0, SUM( Box.Efectivo ) ) AS 'Efectivo', IF ( SUM( Box.Tarjeta ) = '' OR SUM( Box.Tarjeta ) IS NULL, 0, SUM( Box.Tarjeta ) ) AS 'Tarjeta', IF ( SUM( Box.Vales ) = '' OR SUM( Box.Vales ) IS NULL, 0, SUM( Box.Vales ) ) AS 'Vales', IF ( SUM( Box.Cheque ) = '' OR SUM( Box.Cheque ) IS NULL, 0, SUM( Box.Cheque ) ) AS 'Cheque', IF ( SUM( Box.Transferencia ) = '' OR SUM( Box.Transferencia ) IS NULL, 0, SUM( Box.Transferencia ) ) AS 'Transferencia' FROM caja AS Box INNER JOIN usuarios AS Usr ON ( Usr.ID = Box.IDUsuario ) WHERE Box.ID >= '{IDCajaFin}' AND Box.ID <= '{IDCajaInicio}' AND Box.Operacion = 'retiro' AND Box.IDUsuario = '{FormPrincipal.userID}' AND Box.IdEmpleado = '{FormPrincipal.id_empleado}'";

            return consulta;
        }
        public string ContreseñaDeUsuarioPorID(int idUsuario)
        {
            var consulta = $"SELECT `Password` FROM usuarios WHERE ID ={idUsuario}";
            return consulta;
        }

        public string PermissoVentaClienteDescuento(int IDUsuario, int IDEmpleado)
        {
            var consulta = $"SELECT PermisoVentaClienteDescuento,PermisoVentaClienteDescuentoSinAutorizacion FROM empleadospermisoS WHERE IDUsuario = {IDUsuario} AND IDEmpleado ={IDEmpleado} AND Seccion = 'ventas'";
            return consulta;
        }

        public string EliminarFiltrosProductos(int idUsuario, string username, int tipo)
        {
            var consulta = string.Empty;

            if (tipo == 1)
            {
                consulta = $"DELETE FROM FiltroProducto WHERE IDUsuario = {idUsuario} AND Username = '{username}'";
            }

            if (tipo == 2)
            {
                consulta = $"DELETE FROM FiltrosDinamicosVetanaFiltros WHERE IDUsuario = {idUsuario} AND Username = '{username}'";
            }

            return consulta;
        }

        public string obtenerDescuentosMayoreoParaCopiar(int idprod)
        {
            var consulta = $"SELECT * FROM descuentomayoreo WHERE IDProducto = {idprod}";
            return consulta;
        }

        public string obtenerDescuentosProductoParaCopiar(int idprod)
        {
            var consulta = $"SELECT * FROM descuentocliente WHERE IDProducto = {idprod}";
            return consulta;
        }

        public string obtenerDetallesFacturacionParaCopiar(int idprod)
        {
            var consulta = $"SELECT * FROM detallesfacturacionproductos WHERE IDProducto = {idprod}";
            return consulta;
        }

        public string obtenerDetallesProductoParaCopiar(int idprod)
        {
            var consulta = $"SELECT * FROM detallesproductogenerales WHERE IDProducto = {idprod}";
            return consulta;
        }

        public string obtenerDatosFacturacionProductoParaCopiar(int idprod)
        {
            var consulta = $"SELECT * FROM detallesfacturacionproductos WHERE IDProducto = {idprod}";

            return consulta;
        }

        public string agregarSaldosIniciales(int idHistorialCorteDeCaja, decimal[] cantidadesIniciales)
        {
            var consulta = $"UPDATE historialcortesdecaja SET SaldoInicialEfectivo = SaldoInicialEfectivo + '{cantidadesIniciales[0]}', SaldoInicialTarjeta = SaldoInicialTarjeta + '{cantidadesIniciales[1]}', SaldoInicialVales = SaldoInicialVales + '{cantidadesIniciales[2]}', SaldoInicialCheque = SaldoInicialCheque + '{cantidadesIniciales[3]}', SaldoInicialTransferencia = SaldoInicialTransferencia + '{cantidadesIniciales[4]}' WHERE ID = '{idHistorialCorteDeCaja}'";

            return consulta;
        }

        public string copiarMensajeVentas(int idprod)
        {
            var consulta = $"SELECT ProductOfMessage, ProductMessageActivated, CantidadMinimaDeCompra FROM productmessage WHERE IDProducto = {idprod}";

            return consulta;
        }

        public string CargarAbonosTodos(string losIDsDeEmpleados)
        {
            var consulta = $"( SELECT IDUsuario, IDEmpleado, IDCorteDeCaja AS 'IDCaja', FechaOperacion AS 'Fecha' FROM historialcortesdecaja WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' GROUP BY IDUsuario DESC, IDEmpleado DESC, IDCorteDeCaja DESC ORDER BY IDCorteDeCaja DESC LIMIT 1 ) UNION ( SELECT IDUsuario, IDEmpleado, MAX( IDCorteDeCaja ) AS 'IDCaja', MAX( FechaOperacion ) AS 'Fecha' FROM historialcortesdecaja WHERE IDEmpleado IN ( { losIDsDeEmpleados } ) AND IDUsuario = '{FormPrincipal.userID}' GROUP BY IDEmpleado HAVING ( IDEmpleado ) ORDER BY IDEmpleado )";

            return consulta;
        }

        public string copiarMensajeInventario(int idprod)
        {
            var consulta = $"SELECT Mensaje, Activo FROM mensajesinventario WHERE IDProducto = {idprod}";

            return consulta;
        }

        public string CargarAbonosTodosAdministrador(string idUsuario, string fechaOperacion)
        {
            var consulta = $"SELECT MAX(Abono.ID) AS 'ID', IF(Abono.IDEmpleado = '' OR Abono.IDEmpleado IS NULL, '{idUsuario}', Abono.IDEmpleado) AS 'IDEmpleado', FORMAT( IF ( sum( Abono.Efectivo ) = '' OR sum( Abono.Efectivo ) IS NULL, '0', sum( Abono.Efectivo ) ), 2 ) AS 'Efectivo', FORMAT( IF ( sum( Abono.Tarjeta ) = '' OR sum( Abono.Tarjeta ) IS NULL, '0', sum( Abono.Tarjeta ) ), 2 ) AS 'Tarjeta', FORMAT( IF ( sum( Abono.Vales ) = '' OR sum( Abono.Vales ) IS NULL, '0', sum( Abono.Vales ) ), 2 ) AS 'Vales', FORMAT( IF ( sum( Abono.Cheque ) = '' OR sum( Abono.Cheque ) IS NULL, '0', sum( Abono.Cheque ) ), 2 ) AS 'Cheque', FORMAT( IF ( sum( Abono.Transferencia ) = '' OR sum( Abono.Transferencia ) IS NULL, '0', sum( Abono.Transferencia ) ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( sum( Abono.Efectivo ) = '' OR sum( Abono.Efectivo ) IS NULL, '0', sum( Abono.Efectivo ) ) ) + ( IF ( sum( Abono.Tarjeta ) = '' OR sum( Abono.Tarjeta ) IS NULL, '0', sum( Abono.Tarjeta ) ) ) + ( IF ( sum( Abono.Vales ) = '' OR sum( Abono.Vales ) IS NULL, '0', sum( Abono.Vales ) ) ) + ( IF ( sum( Abono.Cheque ) = '' OR sum( Abono.Cheque ) IS NULL, '0', sum( Abono.Cheque ) ) ) + ( IF ( sum( Abono.Transferencia ) = '' OR sum( Abono.Transferencia ) IS NULL, '0', sum( Abono.Transferencia ) ) ) ), 2 ) AS 'Total' FROM Abonos AS Abono INNER JOIN Ventas AS Vent ON ( Vent.ID = Abono.IDVenta ) WHERE Abono.IDVenta IN ( SELECT IDVenta FROM abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idUsuario}' AND FechaOperacion > '{fechaOperacion}' ) AND Abono.IDEmpleado = '{idUsuario}'";

            return consulta;
        }

        public string CargarAbonosTodosEmpleado(string idUsuario,string fechaOperacion)
        {
            var consulta = $"SELECT MAX(Abono.ID) AS 'ID', IF(Abono.IDEmpleado = '' OR Abono.IDEmpleado IS NULL, '{idUsuario}', Abono.IDEmpleado) AS 'IDEmpleado', FORMAT( IF ( sum( Abono.Efectivo ) = '' OR sum( Abono.Efectivo ) IS NULL, '0', sum( Abono.Efectivo ) ), 2 ) AS 'Efectivo', FORMAT( IF ( sum( Abono.Tarjeta ) = '' OR sum( Abono.Tarjeta ) IS NULL, '0', sum( Abono.Tarjeta ) ), 2 ) AS 'Tarjeta', FORMAT( IF ( sum( Abono.Vales ) = '' OR sum( Abono.Vales ) IS NULL, '0', sum( Abono.Vales ) ), 2 ) AS 'Vales', FORMAT( IF ( sum( Abono.Cheque ) = '' OR sum( Abono.Cheque ) IS NULL, '0', sum( Abono.Cheque ) ), 2 ) AS 'Cheque', FORMAT( IF ( sum( Abono.Transferencia ) = '' OR sum( Abono.Transferencia ) IS NULL, '0', sum( Abono.Transferencia ) ), 2 ) AS 'Transferencia', FORMAT( ( ( IF ( sum( Abono.Efectivo ) = '' OR sum( Abono.Efectivo ) IS NULL, '0', sum( Abono.Efectivo ) ) ) + ( IF ( sum( Abono.Tarjeta ) = '' OR sum( Abono.Tarjeta ) IS NULL, '0', sum( Abono.Tarjeta ) ) ) + ( IF ( sum( Abono.Vales ) = '' OR sum( Abono.Vales ) IS NULL, '0', sum( Abono.Vales ) ) ) + ( IF ( sum( Abono.Cheque ) = '' OR sum( Abono.Cheque ) IS NULL, '0', sum( Abono.Cheque ) ) ) + ( IF ( sum( Abono.Transferencia ) = '' OR sum( Abono.Transferencia ) IS NULL, '0', sum( Abono.Transferencia ) ) ) ), 2 ) AS 'Total' FROM Abonos AS Abono INNER JOIN Ventas AS Vent ON ( Vent.ID = Abono.IDVenta ) WHERE Abono.ID IN ( SELECT ID FROM abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '{idUsuario}' AND FechaOperacion > '{fechaOperacion}' )";

            return consulta;
        }

        public string AbonosRealizadosRecientementeAdministrador(string fechaOperacion)
        {
            var consulta = $"SELECT * FROM abonos AS Abono WHERE Abono.ID IN ( SELECT ID FROM abonos WHERE IDVenta <> ( SELECT MAX(ID) AS 'ID' FROM ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND `Status` = '4' AND FechaOperacion > '{fechaOperacion}' ) AND IDEmpleado = '0' ORDER BY ID DESC ) AND Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado = '0' AND Abono.FechaOperacion > '{fechaOperacion}'";

            return consulta;
        }

        public string AbonosRealizadosDeOtrosUsuariosAMisVentasACredito(string fechaOperacion)
        {
            var consulta = $"SELECT * FROM abonos AS Abono WHERE Abono.IDVenta IN ( SELECT ID FROM ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND IDEmpleado = '0' AND `Status` = '4' AND FechaOperacion > '{fechaOperacion}' ) AND Abono.IDUsuario = '{FormPrincipal.userID}' AND Abono.IDEmpleado != '0'";

            return consulta;
        }

        public string idsProductos(string idVenta)
        {
            var consulta = $"SELECT * FROM productosventa WHERE IDVenta = {ListadoVentas.idGananciaVenta}";

            return consulta;
        }
    }
}   
