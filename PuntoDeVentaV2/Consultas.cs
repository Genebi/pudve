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
            string consulta = "INSERT INTO Productos(Nombre, Stock, Precio, Categoria, ClaveInterna, CodigoBarras, ClaveProducto, UnidadMedida, TipoDescuento, IDUsuario, ProdImage, Tipo, Base, IVA, Impuesto, NombreAlterno1, NombreAlterno2, StockNecesario, StockMinimo, PrecioCompra)";
                   consulta += $"VALUES('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}')";

            return consulta;
        }

        public string GuardarNvaImagen(int idProducto, string imgProducto)
        {
            string consulta = $"UPDATE Productos SET ProdImage = '{imgProducto}' WHERE ID = '{idProducto}'";

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
                consulta = "INSERT INTO Ventas (IDUsuario, IDCliente, IDSucursal, Subtotal, IVA16, Total, Descuento, DescuentoGeneral, Anticipo, Folio, Serie, Status, FechaOperacion)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}')";
            }
            else
            {
                //Actualizar venta guardada
                consulta = $"UPDATE Ventas SET IDCliente = '{datos[1]}', Subtotal = '{datos[3]}', IVA16 = '{datos[4]}', Total = '{datos[5]}', Descuento = '{datos[6]}', DescuentoGeneral = '{datos[7]}', Status = '{datos[11]}', FechaOperacion = '{datos[12]}' WHERE ID = '{operacion}'";
            }

            return consulta;
        }

        public string GuardarProductosVenta(string[] datos)
        {
            string consulta = "INSERT INTO ProductosVenta (IDVenta, IDProducto, Nombre, Cantidad, Precio)";
                   consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}')";

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

        public string GuardarAnticipo(string[] datos)
        {
            string consulta = $"INSERT INTO Anticipos (IDUsuario, Concepto, Importe, Cliente, FormaPago, Comentarios, Status, Fecha)";
                   consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}')";

            return consulta;
        }

        public string CambiarStatusAnticipo(int status, int IDAnticipo, int IDUsuario)
        {
            return $"UPDATE Anticipos SET Status = {status} WHERE ID = {IDAnticipo} AND IDUsuario = {IDUsuario}";
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
            string consulta =   "INSERT INTO DetallesProducto (IDProducto, IDUsuario, Proveedor, IDProveedor)";
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
                consulta = $"UPDATE Proveedores SET Status = 2 WHERE ID = {datos[0]} AND IDUsuario = {datos[1]}";
            }

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
                consulta = "INSERT INTO HistorialCompras (Concepto, Cantidad, Precio, Comentarios, TipoAjuste, FechaOperacion, IDProducto, IDUsuario, Folio, RFCEmisor, NomEmisor, FechaLarga)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}')";
            }

            return consulta;
        }

        public string OperacionCaja(string[] datos)
        {
            string consulta = "INSERT INTO Caja (Operacion, Cantidad, Saldo, Concepto, FechaOperacion, IDUsuario, Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, Anticipo)";
                   consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}')";

            return consulta;
        }

        public string GuardarCliente(string[] datos, int tipo = 0)
        {
            //Este metodo sirve para insertar cliente, actualizar y deshabilitar al cliente
            string consulta = string.Empty;

            if (tipo == 0)
            {
                consulta = "INSERT INTO Clientes (IDUsuario, RazonSocial, NombreComercial, RFC, UsoCFDI, Pais, Estado, Municipio, Localidad, CodigoPostal, Colonia, Calle, NoExterior, NoInterior, RegimenFiscal, Email, Telefono, FormaPago, FechaOperacion)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}')";
            }
            
            if (tipo == 1)
            {
                consulta = $"UPDATE Clientes SET RazonSocial = '{datos[1]}', NombreComercial = '{datos[2]}', RFC = '{datos[3]}', UsoCFDI = '{datos[4]}', Pais = '{datos[5]}', Estado = '{datos[6]}', Municipio = '{datos[7]}', Localidad = '{datos[8]}', CodigoPostal = '{datos[9]}', Colonia = '{datos[10]}', Calle = '{datos[11]}', NoExterior = '{datos[12]}', NoInterior = '{datos[13]}', RegimenFiscal = '{datos[14]}', Email = '{datos[15]}', Telefono = '{datos[16]}', FormaPago = '{datos[17]}', FechaOperacion = '{datos[18]}' WHERE IDUsuario = {datos[0]} AND RFC = '{datos[3]}' AND ID = '{datos[19]}'";
            }

            if (tipo == 2)
            {
                consulta = $"UPDATE Clientes SET Status = 2 WHERE ID = {datos[0]} AND IDUsuario = {datos[1]}";
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

        public string guardar_editar_empleado(string[] datos, int opc= 0)
        {
            string cons = "";

            // Guardar

            if(opc == 1)
            {
                cons = "INSERT INTO Empleados (IDUsuario, nombre, usuario, contrasena)";
                cons += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}')";
            }

            // Ajustar permisos

            if(opc == 2)
            {
                cons = $"UPDATE Empleados SET p_anticipo='{datos[2]}', p_caja='{datos[3]}', p_cliente='{datos[4]}', p_config='{datos[5]}', p_empleado='{datos[6]}', p_empresa='{datos[7]}', p_factura='{datos[8]}', p_inventario='{datos[9]}', p_mdatos='{datos[10]}', p_producto='{datos[11]}', p_proveedor='{datos[12]}', p_reporte='{datos[13]}', p_servicio='{datos[14]}', p_venta='{datos[15]}' WHERE ID='{datos[1]}' AND IDUsuario='{datos[0]}'";

            }

            return cons;
        }

        public string archivos_digitales(string[] datos, int opc= 0)
        {
            string cons = "";

            // Guardar/borrar fecha vencimiento y número del certificado
            if(opc == 1)
            {
                cons = $"UPDATE Usuarios SET num_certificado='{datos[1]}', fecha_caducidad_cer='{datos[2]}', password_cer='{datos[3]}' WHERE ID='{datos[0]}'";
            }
            if(opc == 2)
            {
                cons = $"SELECT RFC, fecha_caducidad_cer, password_cer FROM Usuarios WHERE ID='{datos[0]}'";
            }
            if(opc == 3)
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
                consulta = "INSERT INTO RevisarInventario (IDAlmacen, Nombre, ClaveInterna, CodigoBarras, StockAlmacen, StockFisico, NoRevision, Fecha, Vendido, Diferencia, IDUsuario, Tipo, StatusRevision, StatusInventariado, PrecioProducto)";
                consulta += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}')";
            }

            if (tipo == 1)
            {

            }

            return consulta;
        }

        public string cargar_info_certificado(string id_usuario)
        {
            string cons = $"SELECT num_certificado, password_cer FROM Usuarios WHERE ID='{id_usuario}'";

            return cons;
        }

        public string cargar_datos_venta_xml(int opc, int id, int id_usuario)
        {
            string cons = "";
            // Facturas
            if(opc == 1)
            {
                cons = $"SELECT * FROM Facturas WHERE ID='{id}' AND id_usuario='{id_usuario}'";
            }

            // Emisor
            if(opc == 2)
            {
                cons = $"SELECT * FROM Usuarios WHERE ID='{id_usuario}'";
            }

            // Receptor
            if(opc == 3)
            {
                cons = $"SELECT * FROM Clientes WHERE ID='{id}'";
            }

            // Consulta los productos de la venta en tabla ProductosVenta
            if(opc == 4)
            {
                cons = $"SELECT * FROM ProductosVenta WHERE IDVenta='{id}'";
            }
            // Tabla productos
            if(opc == 5)
            {
                cons = $"SELECT * FROM Productos WHERE ID='{id}'";
            }
            // Catalogo monedas
            if (opc == 6)
            {
                cons = $"SELECT * FROM catalogo_monedas";
            }
            // Consulta clientes
            if(opc == 7)
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
            if(opc == 1)
            {
                modif = $"UPDATE Clientes SET RazonSocial='{datos[1]}', RFC='{datos[2]}', Telefono='{datos[3]}', Email='{datos[4]}', NombreComercial='{datos[5]}', Pais='{datos[6]}', Estado='{datos[7]}', Municipio='{datos[8]}', Localidad='{datos[9]}', CodigoPostal='{datos[10]}', Colonia='{datos[11]}', Calle='{datos[12]}', NoExterior='{datos[13]}', NoInterior='{datos[14]}', UsoCFDI='{datos[15]}' WHERE ID='{datos[0]}'";
            }
            // Guarda método y forma de pago, moneda y tipo de cambio
            if (opc == 2)
            { //
                modif = $"UPDATE Ventas SET MetodoPago='{datos[1]}', FormaPago='{datos[2]}', num_cuenta='{datos[3]}', moneda='{datos[4]}', tipo_cambio='{datos[5]}' WHERE ID='{datos[0]}'";
            }
            // Guarda claves de unidad y producto
            if(opc == 3)
            {
                modif = $"UPDATE Productos SET ClaveProducto='{datos[1]}', UnidadMedida='{datos[0]}' WHERE ID='{datos[2]}'";                
            }

            // Cambia a timbrada la nota
            if (opc == 4)
            {
                modif = $"UPDATE Ventas SET Timbrada='1' WHERE ID='{datos[0]}'";
            }

            // Guarda datos de pago, emisor y receptor
            if(opc == 5)
            {
                modif = "INSERT INTO Facturas (id_usuario, id_venta, id_empleado, metodo_pago, forma_pago, num_cuenta, moneda, tipo_cambio, uso_cfdi,";
                modif += "e_rfc, e_razon_social, e_regimen,  e_correo, e_telefono, e_cp, e_estado, e_municipio, e_colonia, e_calle, e_num_ext, e_num_int,";
                modif += "r_rfc, r_razon_social, r_nombre_comercial, r_correo, r_telefono, r_pais, r_estado, r_municipio, r_localidad, r_cp, r_colonia, r_calle, r_num_ext, r_num_int,";
                modif += "folio, serie, tipo_comprobante)";
                modif += $" VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', '{datos[20]}', '{datos[21]}', '{datos[22]}', '{datos[23]}', '{datos[24]}', '{datos[25]}', '{datos[26]}', '{datos[27]}', '{datos[28]}', '{datos[29]}', '{datos[30]}', '{datos[31]}', '{datos[32]}', '{datos[33]}', '{datos[34]}', '{datos[35]}', '{datos[36]}', 'I')";
            }

            // Guarda los productos 
            if(opc == 6)
            {
                modif = $"INSERT INTO Facturas_productos (id_factura, clave_unidad, clave_producto, descripcion, cantidad, precio_u, base, tasa_cuota, importe_iva) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}')";
            }

            // Guarda los impuestos diferentes de IVA 16, 0, 8 y exento
            if(opc == 7)
            {
                modif = $"INSERT INTO Facturas_impuestos (id_factura_producto, tipo, impuesto, tipo_factor, tasa_cuota, definir, importe) VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', '{datos[3]}', '{datos[4]}', '{datos[5]}', '{datos[6]}')";
            }

            // Cambia a timbrada la factura
            if (opc == 8)
            {
                modif = $"UPDATE Facturas SET timbrada='1' WHERE ID='{datos[0]}'";
            }

            // Agrega datos que se obtienen del XML ya timbrado
            if(opc == 9)
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

        public string obtener_datos_para_gcpago(int opc, int dato)
        {
            string cons = "";

            if (opc == 1)
            {
                cons = $"SELECT * FROM Facturas WHERE ID='{dato}'";
            }
            // Carga todos los complementos de pago a timbrar
            if(opc == 2)
            {
                cons = $"SELECT * FROM Facturas_complemento_pago WHERE id_factura='{dato}' AND timbrada=0";
            }
            
            return cons;
        }

        public string crear_complemento_pago(int opc, string[] datos)
        {
            string crea = "";

            // Crea registro en tabla facturas
            if (opc == 1)
            {
                crea = "INSERT INTO Facturas (id_usuario, id_empleado, forma_pago, moneda, tipo_cambio, folio, serie, tipo_comprobante, uso_cfdi, fecha_hora_cpago,";
                crea += "r_rfc, r_razon_social, r_nombre_comercial, r_correo, r_telefono, r_pais, r_estado, r_municipio, r_localidad, r_cp, r_colonia, r_calle, r_num_ext, r_num_int,";
                crea += "e_rfc, e_razon_social, e_regimen, e_correo, e_telefono, e_cp, e_estado, e_municipio, e_colonia, e_calle, e_num_ext, e_num_int)";
                crea += $"VALUES ('{datos[0]}', '{datos[1]}', '{datos[2]}', 'MXN', '1.000000', '{datos[3]}','{datos[4]}', 'P', 'P01', '{datos[5]}', '{datos[6]}', '{datos[7]}', '{datos[8]}', '{datos[9]}', '{datos[10]}', '{datos[11]}', '{datos[12]}', '{datos[13]}', '{datos[14]}', '{datos[15]}', '{datos[16]}', '{datos[17]}', '{datos[18]}', '{datos[19]}', '{datos[20]}', '{datos[21]}', '{datos[22]}', '{datos[23]}', '{datos[24]}', '{datos[25]}', '{datos[26]}', '{datos[27]}', '{datos[28]}', '{datos[29]}', '{datos[30]}', '{datos[31]}')";
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
            if(opc == 5)
            {
                crea = $"UPDATE Facturas_complemento_pago SET timbrada=1 WHERE id_factura={datos[0]}";
            }

            // Agrega el monto pagado del complemento
            if(opc == 6)
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
                cons = $"SELECT f.total, fp.id_factura, fp.importe_pagado FROM Facturas AS f INNER JOIN Facturas_complemento_pago AS fp ON f.ID=fp.id_factura_principal WHERE fp.id_factura_principal='{idf}' AND fp.timbrada=1 AND f.cancelada=0";
            }
            if (opc == 2)
            {
                cons = $"SELECT ID, folio, serie, r_rfc, r_razon_social, total FROM Facturas WHERE ID='{idf}'";
            }

            return cons;
        }
    }
}
