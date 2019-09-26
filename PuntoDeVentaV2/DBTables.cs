using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class DBTables
    {
        #region VariablesTablas
        public static int Anticipos;
        public static int Caja;
        public static int CatalogoUnidadesMedida;
        public static int Categorias;
        public static int CodigoBarrasExtras;
        public static int DescuentoCliente;
        public static int DescuentoMayoreo;
        public static int DetallesFacturacionProductos;
        public static int DetallesProducto;
        public static int Empresas;
        public static int HisotorialCompras;
        public static int HistorialModificacionRecordProduct;
        public static int ProductoRelacionadoXML;
        public static int Productos;
        public static int ProductosDeServicios;
        public static int ProductosVenta;
        public static int Proveedores;
        public static int RegimenDeUsuarios;
        public static int RegimenFiscal;
        public static int Usuarios;
        public static int Ventas;
        public static int Clientes;
        public static int RevisarInventario;
        public static int DetallesVenta;
        public static int Abonos;
        public static int Ubicaciones;
        #endregion VariablesTablas

        public DBTables()
        {
            #region InicializarVariables
            Anticipos = 11;
            Caja = 14;
            CatalogoUnidadesMedida = 3;
            CodigoBarrasExtras = 3;
            DescuentoCliente = 6;
            DescuentoMayoreo = 6;
            DetallesFacturacionProductos = 8;
            DetallesProducto = 9;
            Empresas = 19;
            HisotorialCompras = 17;
            HistorialModificacionRecordProduct = 4;
            ProductoRelacionadoXML = 5;
            Productos = 17;
            ProductosDeServicios = 6;
            ProductosVenta = 6;
            Proveedores = 15;
            RegimenDeUsuarios = 2;
            RegimenFiscal = 7;
            Usuarios = 20;
            Ventas = 21;
            Clientes = 21;
            RevisarInventario = 15;
            DetallesVenta = 14;
            Abonos = 11;
            Categorias = 3;
            Ubicaciones = 3;
            #endregion InicializarVariables
        }

        // Tabla de Anticipos 01
        #region TablaAnticipos
        public int GetAnticipos()
        {
            return Anticipos;
        }

        public string PragmaTablaAnticipos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameAnticipos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaAnticipos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID          INTEGER  PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              IDUsuario   INTEGER  NOT NULL DEFAULT (0),
                                              IDEmpleado  INTEGER  NOT NULL DEFAULT (0),
                                              Concepto    TEXT     NOT NULL,
                                              Importe     DECIMAL  NOT NULL DEFAULT (0),
                                              Cliente     TEXT     NOT NULL,
                                              FormaPago   TEXT     NOT NULL,
                                              Comentarios TEXT,
                                              Status      INT (1)  DEFAULT (0)  NOT NULL,
                                              Fecha       DATETIME NOT NULL,
                                              IDVenta     INTEGER  DEFAULT (0));";
        }

        public string QueryUpdateTablaAnticipos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                            IDUsuario,
                                            IDEmpleado,
                                            Concepto,
                                            Importe,
                                            Cliente,
                                            FormaPago,
                                            Comentarios,
                                            Status,
                                            Fecha)
                                    SELECT  ID,
                                            IDUsuario,
                                            IDEmpleado,
                                            Concepto,
                                            Importe,
                                            Cliente,
                                            FormaPago,
                                            Comentarios,
                                            Status,
                                            Fecha 
                                    FROM '{tabla}_temp';";
        }

        public string DropTablaAnticipos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaAnticipos

        // Tabla de Caja 02
        #region TablaCaja
        public int GetCaja()
        {
            return Caja;
        }

        public string PragmaTablaCaja(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCaja(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCaja(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER  PRIMARY KEY AUTOINCREMENT,
                                              Operacion      TEXT     NOT NULL,
                                              Cantidad       REAL     NOT NULL,
                                              Saldo          REAL     NOT NULL,
                                              Concepto       TEXT,
                                              FechaOperacion DATETIME NOT NULL,
                                              IDUsuario      INTEGER  NOT NULL,
                                              Efectivo       DECIMAL  DEFAULT (0),
                                              Tarjeta        DECIMAL  DEFAULT (0),
                                              Vales          DECIMAL  DEFAULT (0),
                                              Cheque         DECIMAL  DEFAULT (0),
                                              Transferencia  DECIMAL  DEFAULT (0),
                                              Credito        DECIMAL  DEFAULT (0),
                                              Anticipo       DECIMAL  DEFAULT (0))";
        }

        public string QueryUpdateTablaCaja(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                            Operacion,
                                            Cantidad,
                                            Saldo,
                                            Concepto ,
                                            FechaOperacion,
                                            IDUsuario,
                                            Efectivo,
                                            Tarjeta,
                                            Vales,
                                            Cheque,
                                            Transferencia,
                                            Credito) 
                                     SELECT ID,
                                            Operacion,
                                            Cantidad,
                                            Saldo,
                                            Concepto ,
                                            FechaOperacion,
                                            IDUsuario,
                                            Efectivo,
                                            Tarjeta,
                                            Vales,
                                            Cheque,
                                            Transferencia,
                                            Credito 
                                     FROM '{tabla}_temp';";
        }

        public string DropTablaCaja(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaCaja

        // Tabla de CatalogoUnidadesMedida 03
        #region TablaCatalogoUnidadesMedida
        public int GetCatalogoUnidadesMedida()
        {
            return CatalogoUnidadesMedida;
        }

        public string PragmaTablaCatalogoUnidadesMedida(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCatalogoUnidadesMedida(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCatalogoUnidadesMedida(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              ClaveUnidad TEXT,
                                              Nombre TEXT);";
        }

        public string QueryUpdateTablaCatalogoUnidadesMedida(string tabla)
        {
            return $"INSERT INTO '{tabla}' (ID, ClaveUnidad, Nombre) SELECT ID, ClaveUnidad, Nombre FROM '{tabla}_temp';";
        }

        public string DropTablaCatalogoUnidadesMedida(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaCatalogoUnidadesMedida

        // Tabla de CodigoBarrasExtras 04
        #region TablaCodigoBarrasExtras
        public int GetCodigoBarrasExtras()
        {
            return CodigoBarrasExtras;
        }

        public string PragmaTablaCodigoBarrasExtras(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCodigoBarrasExtras(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCodigoBarrasExtras(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (IDCodBarrExt INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              CodigoBarraExtra TEXT,
                                              IDProducto INTEGER,
                                              FOREIGN KEY (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (IDCodBarrExt ASC));";
        }

        public string QueryUpdateTablaCodigoBarrasExtras(string tabla)
        {
            return $"INSERT INTO '{tabla}' (IDCodBarrExt, CodigoBarraExtra, IDProducto) SELECT IDCodBarrExt, CodigoBarraExtra, IDProducto FROM '{tabla}_temp';";
        }

        public string DropTablaCodigoBarrasExtras(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaCodigoBarrasExtras

        // Tabla de DescuentoCliente 05
        #region TablaDescuentoCliente
        public int GetDescuentoCliente()
        {
            return DescuentoCliente;
        }

        public string PragmaTablaDescuentoCliente(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDescuentoCliente(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDescuentoCliente(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              PrecioProducto REAL,
                                              PorcentajeDescuento REAL,
                                              PrecioDescuento REAL,
                                              Descuento REAL,
                                              IDProducto INTEGER,
                                              FOREIGN KEY (IDProducto)REFERENCES Productos (ID));";
        }

        public string QueryUpdateTablaDescuentoCliente(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID, 
                                             PrecioProducto, 
                                             PorcentajeDescuento, 
                                             PrecioDescuento, 
                                             Descuento, 
                                             IDProducto) 
                                      SELECT ID, 
                                             PrecioProducto, 
                                             PorcentajeDescuento, 
                                             PrecioDescuento, 
                                             Descuento, 
                                             IDProducto 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaDescuentoCliente(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaDescuentoCliente

        // Tabla de DescuentoMayoreo 06
        #region TablaDescuentoMayoreo
        public int GetDescuentoMayoreo()
        {
            return DescuentoMayoreo;
        }

        public string PragmaTablaDescuentoMayoreo(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDescuentoMayoreo(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDescuentoMayoreo(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              RangoInicial TEXT,
                                              RangoFinal TEXT,
                                              Precio REAL,
                                              Checkbox  INTEGER,
                                              IDProducto INTEGER,
                                              FOREIGN KEY (IDProducto) REFERENCES Productos (ID) );";
        }

        public string QueryUpdateTablaDescuentoMayoreo(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             RangoInicial,
                                             RangoFinal,
                                             Precio,
                                             Checkbox,
                                             IDProducto)
                                      SELECT ID, 
                                             RangoInicial, 
                                             RangoFinal, 
                                             Precio, 
                                             Checkbox, 
                                             IDProducto 
                                      FROM '{tabla}_temp';";
        }

        public string DropTablaDescuentoMayoreo(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaDescuentoMayoreo

        // Tabla de DetallesFacturacionProductos 07
        #region TablaDetallesFacturacionProductos
        public int GetDetallesFacturacionProductos()
        {
            return DetallesFacturacionProductos;
        }

        public string PragmaTablaDetallesFacturacionProductos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDetallesFacturacionProductos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDetallesFacturacionProductos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              Tipo TEXT,
                                              Impuesto TEXT,
                                              TipoFactor TEXT,
                                              TasaCuota REAL,
                                              Definir REAL,
                                              Importe REAL,
                                              IDProducto INTEGER,
                                              FOREIGN KEY (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (ID ASC));";
        }

        public string QueryUpdateTablaDetallesFacturacionProductos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             Tipo,
                                             Impuesto,
                                             TipoFactor,
                                             TasaCuota,
                                             Definir,
                                             Importe,
                                             IDProducto) 
                                      SELECT ID,
                                             Tipo,
                                             Impuesto,
                                             TipoFactor,
                                             TasaCuota,
                                             Definir,
                                             Importe,
                                             IDProducto 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaDetallesFacturacionProductos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaDetallesFacturacionProductos

        // Tabla de DetallesProducto 08
        #region TablaDetallesProducto
        public int GetDetallesProducto()
        {
            return DetallesProducto;
        }

        public string PragmaTablaDetallesProducto(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDetallesProducto(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDetallesProducto(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDProducto  INTEGER NOT NULL,
                                              IDUsuario   INTEGER NOT NULL,
                                              Proveedor   TEXT,
                                              IDProveedor INTEGER DEFAULT (0),
                                              Categoria   TEXT,
                                              IDCategoria INTEGER DEFAULT (0),
                                              Ubicacion   TEXT,
                                              IDUbicacion INTEGER DEFAULT (0));";
        }

        public string QueryUpdateTablaDetallesProducto(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDProducto,
                                             IDUsuario,
                                             Proveedor,
                                             IDProveedor) 
                                      SELECT ID,
                                             IDProducto,
                                             IDUsuario,
                                             Proveedor,
                                             IDProveedor 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaDetallesProducto(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaDetallesProducto

        // Tabla de Empresas 09
        #region TablaEmpresas
        public int GetEmpresas()
        {
            return Empresas;
        }

        public string PragmaTablaEmpresas(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameEmpresas(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaEmpresas(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID_Empresa INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              Usuario TEXT NOT NULL,
                                              Password TEXT NOT NULL,
                                              RazonSocial TEXT NOT NULL,
                                              RFC TEXT,
                                              Telefono TEXT,
                                              Email TEXT NOT NULL,
                                              NombreCompleto TEXT,
                                              Calle TEXT,
                                              NoExterior TEXT,
                                              NoInterior TEXT,
                                              Colonia TEXT,
                                              Municipio TEXT,
                                              Estado TEXT,
                                              CodigoPostal TEXT,
                                              Regimen TEXT,
                                              TipoPersona TEXT,
                                              LogoTipo TEXT,
                                              ID_Usuarios TEXT,
                                              FOREIGN KEY (ID_Usuarios) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (ID_Empresa ASC));";
        }

        public string QueryUpdateTablaEmpresas(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID_Empresa,
                                             Usuario,
                                             Password,
                                             RazonSocial,
                                             RFC,
                                             Telefono,
                                             Email,
                                             NombreCompleto,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             Colonia,
                                             Municipio,
                                             Estado,
                                             CodigoPostal,
                                             Regimen,
                                             TipoPersona,
                                             LogoTipo,
                                             ID_Usuarios) 
                                      SELECT ID_Empresa,
                                             Usuario,
                                             Password,
                                             RazonSocial,
                                             RFC,
                                             Telefono,
                                             Email,
                                             NombreCompleto,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             Colonia,
                                             Municipio,
                                             Estado,
                                             CodigoPostal,
                                             Regimen,
                                             TipoPersona,
                                             LogoTipo,
                                             ID_Usuarios 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaEmpresas(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaEmpresas

        // Tabla de HisotorialCompras 10
        #region TablaHistorialCompras
        public int GetHistorialCompras()
        {
            return HisotorialCompras;
        }

        public string PragmaTablaHistorialCompras(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameHistorialCompras(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaHistorialCompras(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              Concepto TEXT NOT NULL,
                                              Cantidad INTEGER NOT NULL,
                                              ValorUnitario REAL,
                                              Descuento REAL DEFAULT (0),
                                              Precio REAL NOT NULL,
                                              FechaLarga DATETIME,
                                              Folio TEXT,
                                              RFCEmisor TEXT,
                                              NomEmisor TEXT,
                                              ClaveProdEmisor TEXT,
                                              Comentarios TEXT,
                                              TipoAjuste INTEGER DEFAULT (0),
                                              FechaOperacion DATETIME,
                                              IDReporte INTEGER,
                                              IDProducto INTEGER,
                                              IDUsuario INTEGER,
                                              FOREIGN KEY (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              FOREIGN KEY (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (ID ASC));";
        }

        public string QueryUpdateTablaHistorialCompras(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             Concepto,
                                             Cantidad,
                                             ValorUnitario,
                                             Descuento,
                                             Precio,
                                             FechaLarga,
                                             Folio,
                                             RFCEmisor,
                                             NomEmisor,
                                             ClaveProdEmisor,
                                             Comentarios,
                                             TipoAjuste,
                                             FechaOperacion,
                                             IDReporte,
                                             IDProducto,
                                             IDUsuario) 
                                      SELECT ID,
                                             Concepto,
                                             Cantidad,
                                             ValorUnitario,
                                             Descuento,
                                             Precio,
                                             FechaLarga,
                                             Folio,
                                             RFCEmisor,
                                             NomEmisor,
                                             ClaveProdEmisor,
                                             Comentarios,
                                             TipoAjuste,
                                             FechaOperacion,
                                             IDReporte,
                                             IDProducto,
                                             IDUsuario 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaHistorialCompras(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaHistorialCompras

        // Tabla de HistorialModificacionRecordProduct 11
        #region TablaHistorialModificacionRecordProduct
        public int GetHistorialModificacionRecordProduct()
        {
            return HistorialModificacionRecordProduct;
        }

        public string PragmaTablaHistorialModificacionRecordProduct(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameHistorialModificacionRecordProduct(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaHistorialModificacionRecordProduct(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario INTEGER,
                                              IDRecordProd INTEGER,
                                              FechaEditRecord DATETIME NOT NULL,
                                              FOREIGN KEY (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              FOREIGN KEY (IDRecordProd) REFERENCES HistorialCompras (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (ID ASC));";
        }

        public string QueryUpdateTablaHistorialModificacionRecordProduct(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             IDRecordProd,
                                             FechaEditRecord) 
                                      SELECT ID,
                                             IDUsuario,
                                             IDRecordProd,
                                             FechaEditRecord 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaHistorialModificacionRecordProduct(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaHistorialModificacionRecordProduct

        // Tabla de ProductoRelacionadoXML 12
        #region TablaProductoRelacionadoXML
        public int GetProductoRelacionadoXML()
        {
            return ProductoRelacionadoXML;
        }

        public string PragmaTablaProductoRelacionadoXML(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameProductoRelacionadoXML(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaProductoRelacionadoXML(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (IDProductoRelacionadoXML INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              NombreXML TEXT NOT NULL,
                                              Fecha DATETIME NOT NULL,
                                              IDProducto INTEGER,
                                              IDUsuario INTEGER,
                                              FOREIGN KEY (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              FOREIGN KEY (IDUsuario) REFERENCES USuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (IDProductoRelacionadoXML ASC),
                                              UNIQUE (NombreXML ASC));";
        }

        public string QueryUpdateTablaProductoRelacionadoXML(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (IDProductoRelacionadoXML,
                                             NombreXML,
                                             Fecha,
                                             IDProducto,
                                             IDUsuario) 
                                      SELECT IDProductoRelacionadoXML,
                                             NombreXML,
                                             Fecha,
                                             IDProducto,
                                             IDUsuario 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaProductoRelacionadoXML(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaProductoRelacionadoXML

        // Tabla de Productos 13
        #region TablaProductos
        public int GetProductos()
        {
            return Productos;
        }

        public string PragmaTablaProductos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameProductos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaProductos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID            INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              Nombre        TEXT    NOT NULL,
                                              Stock         REAL    NOT NULL DEFAULT (0),
                                              Precio        REAL    NOT NULL,
                                              Categoria     TEXT,
                                              ClaveInterna  TEXT,
                                              CodigoBarras  TEXT,
                                              ClaveProducto TEXT,
                                              UnidadMedida  TEXT,
                                              TipoDescuento INTEGER DEFAULT 0,
                                              IDUsuario     INTEGER,
                                              Status        INT     NOT NULL DEFAULT (1),
                                              ProdImage     TEXT,
                                              Tipo          TEXT    NOT NULL DEFAULT P,
                                              Base          DECIMAL DEFAULT (0),
                                              IVA           DECIMAL DEFAULT (0),
                                              Impuesto      TEXT,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES USuarios (ID));";
        }

        public string QueryUpdateTablaProductos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             Nombre,
                                             Stock,
                                             Precio,
                                             Categoria,
                                             ClaveInterna,
                                             CodigoBarras,
                                             ClaveProducto,
                                             UnidadMedida,
                                             TipoDescuento,
                                             IDUsuario,
                                             Status,
                                             ProdImage,
                                             Tipo,
                                             Base,
                                             IVA,
                                             Impuesto) 
                                      SELECT ID,
                                             Nombre,
                                             Stock,
                                             Precio,
                                             Categoria,
                                             ClaveInterna,
                                             CodigoBarras,
                                             ClaveProducto,
                                             UnidadMedida,
                                             TipoDescuento,
                                             IDUsuario,
                                             Status,
                                             ProdImage,
                                             Tipo,
                                             Base,
                                             IVA,
                                             Impuesto 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaProductos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaProductos

        // Tabla de ProductosDeServicios 14
        #region TablaProductosDeServicios
        public int GetProductosDeServicios()
        {
            return ProductosDeServicios;
        }

        public string PragmaTablaProductosDeServicios(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameProductosDeServicios(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaProductosDeServicios(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID             INTEGER  NOT NULL PRIMARY KEY AUTOINCREMENT,
                                              Fecha          DATETIME NOT NULL,
                                              IDServicio     INTEGER,
                                              IDProducto     INTEGER,
                                              NombreProducto TEXT     NOT NULL,
                                              Cantidad       REAL     NOT NULL DEFAULT (0),
                                              FOREIGN KEY (IDServicio)
                                              REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              FOREIGN KEY (IDProducto)
                                              REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              UNIQUE (ID ASC));";
        }

        public string QueryUpdateTablaProductosDeServicios(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             Fecha,
                                             IDServicio,
                                             IDProducto,
                                             NombreProducto,
                                             Cantidad) 
                                      SELECT ID,
                                             Fecha,
                                             IDServicio,
                                             IDProducto,
                                             NombreProducto,
                                             Cantidad 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaProductosDeServicios(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaProductos

        // Tabla de ProductosVenta 15
        #region TablaProductosVenta
        public int GetProductosVenta()
        {
            return ProductosVenta;
        }

        public string PragmaTablaProductosVenta(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameProductosVenta(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaProductosVenta(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE NOT NULL,
                                              IDVenta INTEGER NOT NULL,
                                              IDProducto INTEGER NOT NULL,
                                              Nombre     STRING,
                                              Cantidad   INTEGER DEFAULT (0),
                                              Precio     DECIMAL DEFAULT (0),
                                              FOREIGN KEY (IDVenta) REFERENCES Ventas (ID) ON DELETE CASCADE ON UPDATE CASCADE);";
        }

        public string QueryUpdateTablaProductosVenta(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDVenta,
                                             IDProducto,
                                             Nombre,
                                             Cantidad,
                                             Precio) 
                                      SELECT ID,
                                             IDVenta,
                                             IDProducto,
                                             Nombre,
                                             Cantidad,
                                             Precio 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaProductosVenta(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaProductosVenta

        // Tabla de Proveedores 16
        #region TablaProveedores
        public int GetProveedores()
        {
            return Proveedores;
        }

        public string PragmaTablaProveedores(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameProveedores(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaProveedores(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER  PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario INTEGER  NOT NULL,
                                              Nombre TEXT NOT NULL,
                                              RFC TEXT NOT NULL,
                                              Calle TEXT,
                                              NoExterior TEXT,
                                              NoInterior TEXT,
                                              Colonia TEXT,
                                              Municipio TEXT,
                                              Estado TEXT,
                                              CodigoPostal TEXT,
                                              Email TEXT,
                                              Telefono TEXT,
                                              FechaOperacion DATETIME,
                                              Status INTEGER  DEFAULT (1));";
        }

        public string QueryUpdateTablaProveedores(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             Nombre,
                                             RFC,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             Colonia,
                                             Municipio,
                                             Estado,
                                             CodigoPostal,
                                             Email,
                                             Telefono,
                                             FechaOperacion) 
                                      SELECT ID,
                                             IDUsuario,
                                             Nombre,
                                             RFC,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             Colonia,
                                             Municipio,
                                             Estado,
                                             CodigoPostal,
                                             Email,
                                             Telefono,
                                             FechaOperacion 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaProveedores(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaProveedores

        // Tabla de RegimenDeUsuarios 17
        #region TablaRegimenDeUsuarios
        public int GetRegimenDeUsuarios()
        {
            return RegimenDeUsuarios;
        }

        public string PragmaTablaRegimenDeUsuarios(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameRegimenDeUsuarios(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaRegimenDeUsuarios(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (Usuario_ID INTEGER NOT NULL,
                                              Regimen_ID INTEGER NOT NULL,
                                              PRIMARY KEY (Usuario_ID, Regimen_ID),
                                              FOREIGN KEY (Usuario_ID) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              FOREIGN KEY (Regimen_ID) REFERENCES RegimenFiscal (ID) ON DELETE CASCADE ON UPDATE CASCADE);";
        }

        public string QueryUpdateTablaRegimenDeUsuarios(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (Usuario_ID,
                                             Regimen_ID) 
                                      SELECT Usuario_ID, 
                                             Regimen_ID 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaRegimenDeUsuarios(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaRegimenDeUsuarios

        // Tabla de RegimenFiscal 18
        #region TablaRegimenFiscal
        public int GetRegimenFiscal()
        {
            return RegimenFiscal;
        }

        public string PragmaTablaRegimenFiscal(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameRegimenFiscal(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaRegimenFiscal(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              CodigoRegimen INTEGER NOT NULL,
                                              Descripcion TEXT NOT NULL,
                                              AplicaFisica TEXT NOT NULL,
                                              AplicaMoral TEXT NOT NULL,
                                              InicioVigencia DATE,
                                              FinVigencia DATE);";
        }

        public string QueryUpdateTablaRegimenFiscal(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             CodigoRegimen,
                                             Descripcion,
                                             AplicaFisica,
                                             AplicaMoral,
                                             InicioVigencia,
                                             FinVigencia) 
                                      SELECT ID,
                                             CodigoRegimen,
                                             Descripcion,
                                             AplicaFisica,
                                             AplicaMoral,
                                             InicioVigencia,
                                             FinVigencia 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaRegimenFiscal(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaRegimenFiscal

        // Tabla de Usuarios 19
        #region TablaUsuarios
        public int GetUsuarios()
        {
            return Usuarios;
        }

        public string PragmaTablaUsuarios(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameUsuarios(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaUsuarios(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID             INTEGER NOT NULL   PRIMARY KEY AUTOINCREMENT   UNIQUE,
                                              Usuario        TEXT    NOT NULL,
                                              Password       TEXT    NOT NULL,
                                              RazonSocial    TEXT    NOT NULL,
                                              RFC            TEXT,
                                              Telefono       TEXT,
                                              Email          TEXT    NOT NULL,
                                              NombreCompleto TEXT,
                                              Calle          TEXT,
                                              NoExterior     TEXT,
                                              NoInterior     TEXT,
                                              Colonia        TEXT,
                                              Municipio      TEXT,
                                              Estado         TEXT,
                                              CodigoPostal   TEXT,
                                              Regimen        TEXT,
                                              TipoPersona    TEXT,
                                              LogoTipo       TEXT,
                                              Referencia_ID  TEXT,
                                              VerificacionNS INTEGER DEFAULT (0));";
        }

        public string QueryUpdateTablaUsuarios(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             Usuario,
                                             Password,
                                             RazonSocial,
                                             RFC,
                                             Telefono,
                                             Email,
                                             NombreCompleto,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             Colonia,
                                             Municipio,
                                             Estado,
                                             CodigoPostal,
                                             Regimen,
                                             TipoPersona,
                                             LogoTipo,
                                             Referencia_ID) 
                                      SELECT ID,
                                             Usuario,
                                             Password,
                                             RazonSocial,
                                             RFC,
                                             Telefono,
                                             Email,
                                             NombreCompleto,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             Colonia,
                                             Municipio,
                                             Estado,
                                             CodigoPostal,
                                             Regimen,
                                             TipoPersona,
                                             LogoTipo,
                                             Referencia_ID 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaUsuarios(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaUsuarios

        // Tabla de Ventas 20
        #region TablaVentas
        public int GetVentas()
        {
            return Ventas;
        }

        public string PragmaTablaVentas(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameVentas(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaVentas(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
                                              IDUsuario INTEGER NOT NULL,
                                              IDCliente INTEGER NOT NULL DEFAULT (0),
                                              IDEmpleado INTEGER DEFAULT (0),
                                              IDSucursal INTEGER DEFAULT (0),
                                              Subtotal DECIMAL DEFAULT (0),
                                              IVA16 DECIMAL DEFAULT (0),
                                              IVA8 DECIMAL DEFAULT (0),
                                              Total DECIMAL DEFAULT (0),
                                              Descuento DECIMAL DEFAULT (0),
                                              DescuentoGeneral DECIMAL DEFAULT (0),
                                              Anticipo DECIMAL DEFAULT (0),
                                              Folio INTEGER DEFAULT (0),
                                              Serie CHAR DEFAULT A,
                                              Status INTEGER DEFAULT (0),
                                              MetodoPago STRING,
                                              Comentario STRING,
                                              Timbrada INTEGER DEFAULT (0),
                                              Cancelada INTEGER DEFAULT (0),
                                              FechaOperacion DATETIME,
                                              FormaPago TEXT);";
        }

        public string QueryUpdateTablaVentas(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             IDCliente,
                                             IDEmpleado,
                                             IDSucursal,
                                             Subtotal,
                                             IVA16,
                                             IVA8,
                                             Total,
                                             Descuento,
                                             DescuentoGeneral,
                                             Anticipo,
                                             Folio,
                                             Serie,
                                             Status,
                                             MetodoPago,
                                             Comentario,
                                             Timbrada,
                                             Cancelada,
                                             FechaOperacion) 
                                      SELECT ID,
                                             IDUsuario,
                                             IDCliente,
                                             IDEmpleado,
                                             IDSucursal,
                                             Subtotal,
                                             IVA16,
                                             IVA8,
                                             Total,
                                             Descuento,
                                             DescuentoGeneral,
                                             Anticipo,
                                             Folio,
                                             Serie,
                                             Status,
                                             MetodoPago,
                                             Comentario,
                                             Timbrada,
                                             Cancelada,
                                             FechaOperacion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaVentas(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaVentas

        // Tabla de Clientes 21
        #region TablaClientes
        public int GetClientes()
        {
            return Clientes;
        }

        public string PragmaTablaClientes(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameClientes(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaClientes(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario INTEGER REFERENCES Usuarios (ID) NOT NULL,
                                              RazonSocial TEXT,
                                              NombreComercial TEXT,
                                              RFC TEXT NOT NULL,
                                              UsoCFDI TEXT,
                                              Pais TEXT,
                                              Estado TEXT,
                                              Municipio TEXT,
                                              Localidad TEXT,
                                              CodigoPostal TEXT,
                                              Colonia TEXT,
                                              Calle TEXT,
                                              NoExterior TEXT,
                                              NoInterior TEXT,
                                              RegimenFiscal TEXT,
                                              Email TEXT,
                                              Telefono TEXT,
                                              FormaPago TEXT,
                                              FechaOperacion DATETIME,
                                              Status INTEGER DEFAULT (1));";
        }

        public string QueryUpdateTablaClientes(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             RazonSocial,
                                             NombreComercial,
                                             RFC,
                                             UsoCFDI,
                                             Pais,
                                             Estado,
                                             Municipio,
                                             Localidad,
                                             CodigoPostal,
                                             Colonia,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             RegimenFiscal,
                                             Email,
                                             Telefono,
                                             FormaPago,
                                             FechaOperacion) 
                                      SELECT ID,
                                             IDUsuario,
                                             RazonSocial,
                                             NombreComercial,
                                             RFC,
                                             UsoCFDI,
                                             Pais,
                                             Estado,
                                             Municipio,
                                             Localidad,
                                             CodigoPostal,
                                             Colonia,
                                             Calle,
                                             NoExterior,
                                             NoInterior,
                                             RegimenFiscal,
                                             Email,
                                             Telefono,
                                             FormaPago,
                                             FechaOperacion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaClientes(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaClientes

        // Tabla de RevisarInventario 22
        #region TablaRevisarInventario
        public int GetRevisarInventario()
        {
            return RevisarInventario;
        }

        public string PragmaTablaRevisarInventario(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameRevisarInventario(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaRevisarInventario(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                              IDAlmacen TEXT NOT NULL,
                                              Nombre TEXT NOT NULL,
                                              ClaveInterna TEXT,
                                              CodigoBarras TEXT,
                                              StockAlmacen INTEGER NOT NULL DEFAULT (0),
                                              StockFisico INTEGER NOT NULL DEFAULT (0),
                                              NoRevision INT DEFAULT (0),
                                              Fecha DATETIME,
                                              Vendido INT DEFAULT (0),
                                              Diferencia INT DEFAULT (0),
                                              IDUsuario INTEGER,
                                              Tipo TEXT,
                                              StatusRevision INT DEFAULT (0),
                                              StatusInventariado INT DEFAULT (0),
                                              FOREIGN KEY (IDUsuario) REFERENCES USuarios (ID));";
        }

        public string QueryUpdateTablaRevisarInventario(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (IDAlmacen,
                                             Nombre,
                                             ClaveInterna,
                                             CodigoBarras,
                                             StockFisico,
                                             Fecha,
                                             IDUsuario,
                                             Tipo) 
                                      SELECT ID,
                                             Nombre,
                                             ClaveInterna,
                                             CodigoBarras,
                                             Stock,
                                             Fecha,
                                             IDUsuario,
                                             'P' 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaRevisarInventario(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaRevisarInventario

        // Tabla de DetallesVenta 23
        #region TablaDetallesVenta
        public int GetDetallesVenta()
        {
            return DetallesVenta;
        }

        public string PragmaTablaDetallesVenta(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDetallesVenta(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDetallesVenta(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDVenta       INTEGER NOT NULL,
                                              IDUsuario     INTEGER,
                                              Efectivo      DECIMAL DEFAULT (0),
                                              Tarjeta       DECIMAL DEFAULT (0),
                                              Vales         REAL    DEFAULT (0),
                                              Cheque        REAL    DEFAULT (0),
                                              Transferencia REAL    DEFAULT (0),
                                              Credito       REAL    DEFAULT (0),
                                              Referencia    TEXT,
                                              IDCliente     INTEGER DEFAULT (0),
                                              Cliente       TEXT,
                                              Cuenta        TEXT,
                                              Anticipo      DECIMAL DEFAULT (0));";
        }

        public string QueryUpdateTablaDetallesVenta(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDVenta,
                                             IDUsuario,
                                             Efectivo,
                                             Tarjeta,
                                             Vales,
                                             Cheque,
                                             Transferencia,
                                             Credito,
                                             Referencia,
                                             IDCliente,
                                             Cliente,
                                             Cuenta) 
                                      SELECT ID,
                                             IDVenta,
                                             IDUsuario,
                                             Efectivo,
                                             Tarjeta,
                                             Vales,
                                             Cheque,
                                             Transferencia,
                                             Credito,
                                             Referencia,
                                             IDCliente,
                                             Cliente, 
                                             Cuenta
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaDetallesVenta(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaDetallesVenta

        // Tabla de Abonos 24
        #region TablaAbonos
        public int GetAbonos()
        {
            return Abonos;
        }

        public string PragmaTablaAbonos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameAbonos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaAbonos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDVenta INTEGER NOT NULL,
                                              IDUsuario INTEGER,
                                              Total DECIMAL DEFAULT (0),
                                              Efectivo DECIMAL DEFAULT (0),
                                              Tarjeta DECIMAL DEFAULT (0),
                                              Vales DECIMAL DEFAULT (0),
                                              Cheque DECIMAL DEFAULT (0),
                                              Transferencia DECIMAL DEFAULT (0),
                                              Referencia TEXT,
                                              FechaOperacion DATETIME NOT NULL);";
        }

        public string QueryUpdateTablaAbonos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDVenta,
                                             IDUsuario,
                                             Total,
                                             Efectivo,
                                             Tarjeta,
                                             Vales,
                                             Cheque,
                                             Transferencia,
                                             Referencia,
                                             FechaOperacion) 
                                      SELECT ID,
                                             IDVenta,
                                             IDUsuario,
                                             Total,
                                             Efectivo,
                                             Tarjeta,
                                             Vales,
                                             Cheque,
                                             Transferencia,
                                             Referencia,
                                             FechaOperacion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaAbonos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaAbonos

        // Tabla de Categorias 25
        #region TablaCategorias
        public int GetCategorias()
        {
            return Categorias;
        }

        public string PragmaTablaCategorias(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCategorias(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCategorias(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID        INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario INTEGER NOT NULL,
                                              Nombre    TEXT    NOT NULL);";
        }

        public string QueryUpdateTablaCategorias(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             Nombre) 
                                      SELECT ID,
                                             IDUsuario,
                                             Nombre 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaCategorias(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaCategorias

        // Tabla de Ubicaciones 26
        #region TablaCategorias
        public int GetUbicaciones()
        {
            return Ubicaciones;
        }

        public string PragmaTablaUbicaciones(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameUbicaciones(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaUbicaciones(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario   INTEGER NOT NULL,
                                              Descripcion TEXT);";
        }

        public string QueryUpdateTablaUbicaciones(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             Descripcion) 
                                      SELECT ID,
                                             IDUsuario,
                                             Descripcion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaUbicaciones(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion TablaUbicaciones
    }
}
