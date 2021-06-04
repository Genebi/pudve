﻿using System;
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

            var query = cn.CargarDatos($"SELECT ID FROM Empleados WHERE Nombre LIKE '%{name}%'");

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

        public string BuscadorDeInventario(string datoBuscado, string primerFecha, string segundaFecha)
        {
            var consulta = $"SELECT NoRevision, NameUsr, Fecha FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NameUsr LIKE '%{datoBuscado}%' AND (Fecha BETWEEN CAST('{primerFecha}' AS DATE) AND CAST('{segundaFecha}' AS DATE)) GROUP BY NoRevision ORDER BY Fecha DESC";

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
                query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE IDUsuario = {FormPrincipal.userID}");
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

            if (!query.Rows.Count.Equals(0)) { result = query.Rows[0]["ID"].ToString(); }

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

        public string GuardarVenta(string[] datos, int operacion = 0)
        {
            string consulta = null;

            if (operacion == 0)
            {
                //Insertar nueva venta
                consulta = "INSERT INTO Ventas (IDUsuario, IDCliente, IDSucursal, Subtotal, IVA16, Total, Descuento, DescuentoGeneral, Anticipo, Folio, Serie, Status, FechaOperacion, IDClienteDescuento, IDEmpleado, FormaPago)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}')";
            }
            else
            {
                //Actualizar venta guardada
                consulta = $"UPDATE Ventas SET IDCliente = '{datos[1]}', Subtotal = '{datos[3]}', IVA16 = '{datos[4]}', Total = '{datos[5]}', Descuento = '{datos[6]}', DescuentoGeneral = '{datos[7]}', Status = '{datos[11]}', FechaOperacion = '{datos[12]}', IDClienteDescuento = '{datos[13]}' WHERE ID = '{operacion}'";
            }

            return consulta;
        }

        public string GuardarProductosVenta(string[] datos)
        {
            // Se agrega campo descuento individual para efectos de facturación

            string consulta = "INSERT INTO ProductosVenta (IDVenta, IDProducto, Nombre, Cantidad, Precio, descuento, TipoDescuento)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[6]}', '{datos[12]}')";

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

        public string Ventas(int id)
        {
            return $"SELECT * FROM Ventas WHERE IDusuario = '{id}'";
        }

        public string ActualizarVenta(int IDVenta, int status, int IDUsuario)
        {
            return $"UPDATE Ventas SET Status = {status} WHERE ID = '{IDVenta}' AND IDUsuario = {IDUsuario}";
        }

        public string DevolverVentaCanceladaSiNoHayDinero(int IDVenta, int status, int IDUsuario)
        {
            return $"UPDATE Ventas SET STatus = {status} WHERE = '{IDVenta}' AND IDUsuario = {IDUsuario}";
        }

        public string GuardarAnticipo(string[] datos)
        {
            string consulta = $"INSERT INTO Anticipos (IDUsuario, Concepto, Importe, Cliente, FormaPago, Comentarios, Status, Fecha, ImporteOriginal)";
            consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[2]}')";

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
            var consulta = $"SELECT * FROM DetalleGeneral WHERE IDUsuario = {idUser} AND ChckName = '{chkNombre}'";

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
                consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo, IdEmpleado, NumFolio)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}')";
            }
            else
            {
                consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}')";
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
                cons = $"UPDATE Empleados SET p_anticipo='{datos[2]}', p_caja='{datos[3]}', p_cliente='{datos[4]}', p_config='{datos[5]}', p_empleado='{datos[6]}', p_empresa='{datos[7]}', p_factura='{datos[8]}', p_inventario='{datos[9]}', p_mdatos='{datos[10]}', p_producto='{datos[11]}', p_proveedor='{datos[12]}', p_reporte='{datos[13]}', p_venta='{datos[14]}' WHERE ID='{datos[1]}' AND IDUsuario='{datos[0]}'";
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
                modif += "e_rfc, e_razon_social, e_regimen,  e_correo, e_telefono, e_cp, e_estado, e_municipio, e_colonia, e_calle, e_num_ext, e_num_int,";
                modif += "r_rfc, r_razon_social, r_nombre_comercial, r_correo, r_telefono, r_pais, r_estado, r_municipio, r_localidad, r_cp, r_colonia, r_calle, r_num_ext, r_num_int,";
                modif += "folio, serie, tipo_comprobante)";
                modif += $" VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', '{datos[20]}', '{datos[21]}', '{datos[22]}', '{datos[23]}', '{datos[24]}', '{datos[25]}', '{datos[26]}', '{datos[27]}', '{datos[28]}', '{datos[29]}', '{datos[30]}', '{datos[31]}', '{datos[32]}', '{datos[33]}', '{datos[34]}', '{datos[35]}', '{datos[36]}', 'I')";
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
            var consulta = $"SELECT * FROM FiltroDinamico WHERE IDUsuario = {idUser}";

            return consulta;
        }

        public string ReiniciarFiltroDinamico(int userID, string searchFiltroDinamico)
        {
            var consulta = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{userID}' AND concepto = '{searchFiltroDinamico}'";

            return consulta;
        }

        public string ReiniciarFiltroDinamicoTresCampos(int valueIntFiltroDinamico, string valueStrFiltroDinamico, int idUser, string searchFiltroDinamico)
        {
            var consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = '{valueIntFiltroDinamico}', textComboBoxConcepto = '{valueStrFiltroDinamico}', textCantidad = '{valueIntFiltroDinamico}' WHERE IDUsuario = '{idUser}' AND concepto = '{searchFiltroDinamico}'";

            return consulta;
        }

        public string ReiniciarFiltroDinamicoDosCampos(int valueIntFiltroDinamico, string valueStrFiltroDinamico, int idUser, string searchFiltroDinamico)
        {
            var consulta = $"UPDATE FiltroProducto SET checkBoxConcepto = '{valueIntFiltroDinamico}', textComboBoxConcepto = '{valueStrFiltroDinamico}' WHERE IDUsuario = '{idUser}' AND concepto = '{searchFiltroDinamico}'";

            return consulta;
        }

        public string VerificarContenidoFiltroProducto(int idUsuario)
        {
            var consulta = $"SELECT * FROM FiltroProducto WHERE IDUsuario = '{idUsuario}'";

            return consulta;
        }

        public string IniciarFiltroConSinFiltroAvanzado(int userID)
        {
            var consulta = $@"SELECT P.* FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = u.ID WHERE U.ID = '{userID}' AND P.Status = 1";

            return consulta;
        }

        public string VerificarVentanaFiltros(int userID)
        {
            var consulta = $"SELECT * FROM FiltrosDinamicosVetanaFiltros WHERE IDUsuario = '{userID}'";

            return consulta;
        }

        public string GuardarVentanaFiltros(string valueChkBox, string conceptoChkBox, string textComboBox, int idUsuario)
        {
            var consulta = "INSERT INTO FiltrosDinamicosVetanaFiltros (checkBoxValue, concepto, strFiltro, IDUsuario)";
            consulta += $"VALUES ('{valueChkBox}', '{conceptoChkBox}', '{textComboBox}', '{idUsuario}')";

            return consulta;
        }

        public string BuscarDatoEnVentanaFiltros(string strConcepto, int userID)
        {
            var consulta = $"SELECT * FROM FiltrosDinamicosVetanaFiltros WHERE concepto = '{strConcepto}' AND IDUsuario = '{userID}'";

            return consulta;
        }

        public string ActualizarDatoVentanaFiltros(string valueChkBox, string conceptoChkBox, string textComboBox, int idUsuario)
        {
            var consulta = $"UPDATE FiltrosDinamicosVetanaFiltros SET checkBoxValue = '{valueChkBox}', strFiltro = '{textComboBox}' WHERE concepto = '{conceptoChkBox}' AND IDUsuario = '{idUsuario}'";

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
            var consulta = "INSERT INTO DGVAumentarInventario(IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, IdUsuario, IDEmpleado) VALUES";
            consulta += $"('{datosAumentarInventario[0]}', '{datosAumentarInventario[1]}', '{datosAumentarInventario[2]}', '{datosAumentarInventario[3]}', '{datosAumentarInventario[4]}', '{datosAumentarInventario[5]}', '{datosAumentarInventario[6]}', '{datosAumentarInventario[7]}', '{datosAumentarInventario[8]}', '{datosAumentarInventario[9]}', '{datosAumentarInventario[10]}', '{datosAumentarInventario[11]}', '{datosAumentarInventario[12]}', '{datosAumentarInventario[13]}', '{datosAumentarInventario[14]}', '{datosAumentarInventario[15]}')";

            return consulta;
        }

        public string GetAumentarInventario()
        {
            var consultar = $"SELECT IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario FROM DGVAumentarInventario WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

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
            var consulta = "INSERT INTO DGVDisminuirInventario(IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario, IdUsuario, IDEmpleado) VALUES";
            consulta += $"('{datosDisminuirInventario[0]}', '{datosDisminuirInventario[1]}', '{datosDisminuirInventario[2]}', '{datosDisminuirInventario[3]}', '{datosDisminuirInventario[4]}', '{datosDisminuirInventario[5]}', '{datosDisminuirInventario[6]}', '{datosDisminuirInventario[7]}', '{datosDisminuirInventario[8]}', '{datosDisminuirInventario[9]}', '{datosDisminuirInventario[10]}', '{datosDisminuirInventario[11]}', '{datosDisminuirInventario[12]}', '{datosDisminuirInventario[13]}', '{datosDisminuirInventario[14]}', '{datosDisminuirInventario[15]}')";

            return consulta;
        }

        public string GetDisminuirInventario()
        {
            var consulta = $"SELECT IdProducto, NombreProducto, StockActual, DiferenciaUnidades, NuevoStock, Precio, Clave, Codigo, Fecha, NoRevision, StatusActualizacion, NombreEmisor, Comentarios, ValorUnitario FROM DGVDisminuirInventario WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string UpdateStatusActualizacionDisminuirInventario()
        {
            var consulta = $"UPDATE DGVDisminuirInventario SET StatusActualizacion = 0 WHERE StatusActualizacion = 1 AND IdUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string SearchDGVAumentarInventario(int NoRev)
        {
            var consulta = $"SELECT * FROM DGVAumentarInventario WHERE NoRevision = {NoRev} AND StatusActualizacion = 1 AND IDUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string NomEmisorComentariosHistorialCompras(string IdProducto)
        {
            var consulta = $"SELECT ID, NomEmisor, Comentarios, ValorUnitario FROM HistorialCompras WHERE IDProducto = {IdProducto} AND IDUsuario = {FormPrincipal.userID} GROUP BY ID DESC LIMIT 1";

            return consulta;
        }

        public string SearchDGVDisminuirInventario(int NoRev)
        {
            var consulta = $"SELECT * FROM DGVDisminuirInventario WHERE NoRevision = {NoRev} AND StatusActualizacion = 1 AND IDUsuario = {FormPrincipal.userID}";

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

        public string getIdCliente(string nombreCliente)
        {
            var consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND RazonSocial = '{nombreCliente}'";

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

        public string setBAsculaPrederterminada(int idBascula)
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
            var consulta = $@"SELECT DISTINCT Prod.ID, Prod.Nombre, Prod.Stock FROM detallesventa AS SaleDetail INNER JOIN ventas AS Sale ON Sale.ID = SaleDetail.IDVenta INNER JOIN usuarios AS Usr ON Usr.ID = Sale.IDUsuario INNER JOIN productosventa AS SaleProd ON SaleProd.IDVenta = Sale.ID  INNER JOIN productos AS Prod ON Prod.ID = SaleProd.IDProducto WHERE SaleDetail.IDVenta = '{idVenta}' GROUP BY Prod.ID";

            return consulta;
        }

        public string aumentarStockVentaCancelada(int idProd, float cantidad)
        {
            var consulta = $"UPDATE Productos SET Stock = '{cantidad}' WHERE ID = '{idProd}' AND IDUsuario = {FormPrincipal.userID}";

            return consulta;
        }

        public string searchProductList(string typeToSearch, string busqueda)
        {
            var consulta = $"SELECT DISTINCT P.ID, P.Nombre, P.Stock, P.Precio, IF(P.Categoria = 'PAQUETES', 'SERVICIOS', P.Categoria) AS Categoria, P.ClaveInterna, P.CodigoBarras FROM Productos AS P INNER JOIN Usuarios AS U ON P.IDUsuario = U.ID WHERE U.ID = '{FormPrincipal.userID}' AND P.STATUS = '1' AND ({typeToSearch}) AND ( P.Nombre LIKE '%{busqueda}%' OR P.NombreAlterno1 LIKE '%{busqueda}%' OR P.NombreAlterno2 LIKE '%{busqueda}%' ) GROUP BY P.Nombre ASC ";

            return consulta;
        }

        public string searchSaleProduct(string busqueda)
        {
            var consulta = $"SELECT DISTINCT Prod.Nombre, Prod.Stock, Prod.Precio, Prod.ClaveInterna, Prod.CodigoBarras, Prod.Tipo, IFNULL( ProdDetail.Proveedor, 'N/A' ) AS Proveedor, GralDetail.ChckName, GralDetail.Descripcion FROM Productos AS Prod LEFT JOIN Usuarios AS Usr ON Usr.ID = Prod.IDUsuario LEFT JOIN DetallesProducto AS ProdDetail ON ProdDetail.IDProducto = Prod.ID LEFT JOIN Proveedores AS Prov ON Prov.ID = ProdDetail.IDProveedor LEFT JOIN DetallesProductoGenerales AS GralProdDetail ON GralProdDetail.IDProducto = Prod.ID LEFT JOIN DetalleGeneral AS GralDetail ON GralDetail.ID = GralProdDetail.IDDetalleGral LEFT JOIN AppSettings AS AppSet ON AppSet.IDUsuario = GralDetail.IDUsuario WHERE Usr.ID = '{FormPrincipal.userID}' AND Prod.`Status` = '1' AND ( Prod.Tipo = 'P' OR Prod.Tipo = 'PQ' OR Prod.Tipo = 'S' ) AND ( Prod.Nombre LIKE '%{busqueda}%' OR Prod.NombreAlterno1 LIKE '%{busqueda}%' OR Prod.NombreAlterno2 LIKE '%{busqueda}%' OR Prod.ClaveInterna LIKE '{busqueda}' OR Prod.CodigoBarras LIKE '{busqueda}' ) ORDER BY Prod.Nombre ASC; ";

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

        public string registroSesiones(string nombre, string fecha)
        {
            var consulta = $"INSERT INTO iniciosDeSesion (Usuario, Fecha) VALUES ('{nombre}', '{fecha}')";
            return consulta;
        }

        public string consultaInicios(string nombre)
        {
            var consulta = $"SELECT * FROM `iniciosdesesion` WHERE Usuario LIKE '%'{nombre}'%'";
            return consulta;
        }

        public string busquedaEmpleado(string busqueda,int status)
        {
            var consulta = $"SELECT ID, nombre, usuario FROM `empleados` WHERE usuario LIKE '%{busqueda}%' AND estatus = '{status}' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string deshabilitarEmpleado(string usuario)
        {
            var consulta = $"UPDATE `empleados` SET estatus = 0 WHERE usuario = '{usuario}' AND IDUsuario = '{FormPrincipal.userID}'";
            return consulta;
        }

        public string habilitarEmpleado(string usuario)
        {
            var consulta = $"UPDATE `empleados` SET estatus = 1 WHERE usuario = '{usuario}' AND IDUsuario = '{FormPrincipal.userID}'";
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
            var consulta = $"SELECT DISTINCT ServProds.ID, ServProds.Fecha, ServProds.IDServicio NoServicio, Prod.Nombre ServicioCombo, ServProds.IDProducto NoProducto, ServProds.NombreProducto Producto, ServProds.Cantidad, IF(Prod.Tipo = 'S', 'SERVICIO', 'COMBO') AS Tipo FROM productosdeservicios AS ServProds INNER JOIN Productos AS Prod ON Prod.ID = ServProds.IDServicio WHERE ServProds.IDProducto = '{idProd}' ORDER BY Prod.Nombre ASC; ";

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

        public string permisosAsignar(List<int> opciones,string empleado)
        { 
            var consulta = $@"UPDATE empleadospermisos 
            SET mensajeVentas = '{opciones[0]}',
            mensajeInventario = '{opciones[1]}',
            stock = '{opciones[2]}',
            stockMinimo = '{opciones[3]}',
            stockMaximo = '{opciones[4]}',
            precio = '{opciones[5]}',
            numeroRevision = '{opciones[6]}',
            tipoIVA = '{opciones[7]}',
            claveProducto = '{opciones[8]}',
            claveUnidad = '{opciones[9]}',
            correos = '{opciones[10]}' 
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

        public string validarUsuario (string usuario)
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
            var consulta = $"SELECT * FROM appSettings WHERE IDUsuario = '{idUsuario}' AND Mostrar = 1 AND checkBoxConcepto = 1";

            return consulta;
        }

        public string consultaRelacionServicioParaProducto(string idServicio)
        {
            var consulta = $"SELECT Nombre, IF ( Tipo = 'S', 'SERVICIO', 'COMBO' ) AS Tipo FROM Productos WHERE ID = '{idServicio}'";

            return consulta;
        }
    }
}  
