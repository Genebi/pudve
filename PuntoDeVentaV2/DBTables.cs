using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class DBTables
    {
    #region Variables Tablas
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
        public static int DetalleGeneral;
        public static int DetallesProductoGenerales;
        public static int ProductMessage;
        public static int CodigoBarrasGenerado;
        public static int Empleados;
        public static int MensajesInventario;
        public static int Catalogo_claves_producto;
        public static int Catalogo_monedas;
        public static int HistorialPrecios;
        public static int appSettings;
        public static int Configuracion;
        public static int TipoClientes;
        public static int FiltroProducto;
        public static int Facturas;
        public static int Facturas_impuestos;
        public static int Facturas_productos;
        public static int Facturas_complemento_pago;
        public static int FiltroDinamico;
        public static int ConceptosDinamicos;
        public static int CorreosProducto;
        public static int FiltrosDinamicosVetanaFiltros;
        public static int EmpleadosPermisos;
    #endregion Variables Tablas

        public DBTables()
        {
        #region Inicializar Variables
            Anticipos = 11;
            Caja = 14;
            CatalogoUnidadesMedida = 3;
            CodigoBarrasExtras = 3;
            DescuentoCliente = 6;
            DescuentoMayoreo = 6;
            DetallesFacturacionProductos = 8;
            DetallesProducto = 9;
            Empresas = 19;
            HisotorialCompras = 18;
            HistorialModificacionRecordProduct = 4;
            ProductoRelacionadoXML = 5;
            Productos = 24;
            ProductosDeServicios = 6;
            ProductosVenta = 7;
            Proveedores = 15;
            RegimenDeUsuarios = 2;
            RegimenFiscal = 7;
            Usuarios = 24;
            Ventas = 26;
            Clientes = 23;
            RevisarInventario = 17;
            DetallesVenta = 14;
            Abonos = 11;
            Categorias = 3;
            Ubicaciones = 3;
            DetalleGeneral = 4;
            DetallesProductoGenerales = 6;
            ProductMessage = 3;
            CodigoBarrasGenerado = 5;
            Empleados = 20;
            MensajesInventario = 5;
            Catalogo_claves_producto = 3;
            Catalogo_monedas = 3;
            HistorialPrecios = 7;
            appSettings = 6;
            Configuracion = 16;
            TipoClientes = 6;
            FiltroProducto = 6;
            Facturas = 51;
            Facturas_impuestos = 8;
            Facturas_productos = 11;
            Facturas_complemento_pago = 13;
            FiltroDinamico = 5;
            ConceptosDinamicos = 7;
            CorreosProducto = 8;
            FiltrosDinamicosVetanaFiltros = 5;
            EmpleadosPermisos = 28;
        #endregion Inicializar Variables
        }

        // Tabla de Anticipos 01
        #region Tabla Anticipos
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
        #endregion Tabla Anticipos

        // Tabla de Caja 02
        #region Tabla Caja
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
        #endregion Tabla Caja

        // Tabla de CatalogoUnidadesMedida 03
        #region Tabla CatalogoUnidadesMedida
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
        #endregion Tabla CatalogoUnidadesMedida

        // Tabla de CodigoBarrasExtras 04
        #region Tabla CodigoBarrasExtras
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
        #endregion Tabla CodigoBarrasExtras

        // Tabla de DescuentoCliente 05
        #region Tabla DescuentoCliente
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
        #endregion Tabla DescuentoCliente

        // Tabla de DescuentoMayoreo 06
        #region Tabla DescuentoMayoreo
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
        #endregion Tabla DescuentoMayoreo

        // Tabla de DetallesFacturacionProductos 07
        #region Tabla DetallesFacturacionProductos
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
        #endregion Tabla DetallesFacturacionProductos

        // Tabla de DetallesProducto 08
        #region Tabla DetallesProducto
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
        #endregion Tabla DetallesProducto

        // Tabla de Empresas 09
        #region Tabla Empresas
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
        #endregion Tabla Empresas

        // Tabla de HisotorialCompras 10
        #region Tabla HistorialCompras
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
                                              ConceptoExtra TEXT,
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
        #endregion Tabla HistorialCompras

        // Tabla de HistorialModificacionRecordProduct 11
        #region Tabla HistorialModificacionRecordProduct
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
        #endregion Tabla HistorialModificacionRecordProduct

        // Tabla de ProductoRelacionadoXML 12
        #region Tabla ProductoRelacionadoXML
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
        #endregion Tabla ProductoRelacionadoXML

        // Tabla de Productos 13
        #region Tabla Productos
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
            return $@"CREATE TABLE '{tabla}' (ID             INTEGER NOT NULL   PRIMARY KEY AUTOINCREMENT   UNIQUE,
                                              Nombre         TEXT    NOT NULL,
                                              Stock          REAL    NOT NULL   DEFAULT (0),
                                              Precio         REAL    NOT NULL,
                                              Categoria      TEXT,
                                              ClaveInterna   TEXT,
                                              CodigoBarras   TEXT,
                                              ClaveProducto  TEXT,
                                              UnidadMedida   TEXT,
                                              TipoDescuento  INTEGER DEFAULT 0,
                                              IDUsuario      INTEGER,
                                              Status         INT     NOT NULL   DEFAULT (1),
                                              ProdImage      TEXT,
                                              Tipo           TEXT    NOT NULL   DEFAULT P,
                                              Base           DECIMAL DEFAULT (0),
                                              IVA            DECIMAL DEFAULT (0),
                                              Impuesto       TEXT,
                                              NombreAlterno1 TEXT,
                                              NombreAlterno2 TEXT,
                                              NumeroRevision INTEGER DEFAULT (0),
                                              StockNecesario INTEGER DEFAULT (0),
                                              StockMinimo    INTEGER DEFAULT (0),
                                              PrecioCompra   REAL    DEFAULT (0),
                                              PrecioMayoreo  REAL    DEFAULT (0),
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
                                             Impuesto,
                                             NombreAlterno1,
                                             NombreAlterno2,
                                             NumeroRevision,
                                             StockNecesario,
                                             StockMinimo,
                                             PrecioCompra) 
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
                                             Impuesto,
                                             NombreAlterno1,
                                             NombreAlterno2,
                                             NumeroRevision,
                                             StockNecesario,
                                             StockMinimo,
                                             PrecioCompra 
                                       FROM '{tabla}_temp';";
        }

        public string DropTablaProductos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Productos

        // Tabla de ProductosDeServicios 14
        #region Tabla ProductosDeServicios
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
        #endregion Tabla Productos

        // Tabla de ProductosVenta 15
        #region Tabla ProductosVenta
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
                                              descuento  TEXT,
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
        #endregion Tabla ProductosVenta

        // Tabla de Proveedores 16
        #region Tabla Proveedores
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
        #endregion Tabla Proveedores

        // Tabla de RegimenDeUsuarios 17
        #region Tabla RegimenDeUsuarios
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
        #endregion Tabla RegimenDeUsuarios

        // Tabla de RegimenFiscal 18
        #region Tabla RegimenFiscal
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
        #endregion Tabla RegimenFiscal

        // Tabla de Usuarios 19
        #region Tabla Usuarios
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
            return $@"CREATE TABLE '{tabla}' (ID                  INTEGER NOT NULL   PRIMARY KEY AUTOINCREMENT   UNIQUE,
                                              Usuario             TEXT    NOT NULL,
                                              Password            TEXT    NOT NULL,
                                              RazonSocial         TEXT    NOT NULL,
                                              RFC                 TEXT,
                                              Telefono            TEXT,
                                              Email               TEXT    NOT NULL,
                                              NombreCompleto      TEXT,
                                              Calle               TEXT,
                                              NoExterior          TEXT,
                                              NoInterior          TEXT,
                                              Colonia             TEXT,
                                              Municipio           TEXT,
                                              Estado              TEXT,
                                              CodigoPostal        TEXT,
                                              Regimen             TEXT,
                                              TipoPersona         TEXT,
                                              LogoTipo            TEXT,
                                              Referencia_ID       TEXT,
                                              VerificacionNS      INTEGER DEFAULT (0),
                                              num_certificado     TEXT,
                                              fecha_caducidad_cer TEXT,
                                              password_cer        TEXT,
                                              FechaHoy            TEXT    DEFAULT [0000-00-00]);";
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
                                             Referencia_ID,
                                             VerificacionNS,
                                             num_certificado,
                                             fecha_caducidad_cer,
                                             password_cer) 
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
                                             Referencia_ID,
                                             VerificacionNS,
                                             num_certificado,
                                             fecha_caducidad_cer,
                                             password_cer 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaUsuarios(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Usuarios

        // Tabla de Ventas 20
        #region Tabla Ventas
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
            return $@"CREATE TABLE '{tabla}' (ID               INTEGER  PRIMARY KEY AUTOINCREMENT  NOT NULL  UNIQUE,
                                              IDUsuario        INTEGER  NOT NULL,
                                              IDCliente        INTEGER  NOT NULL  DEFAULT (0),
                                              IDEmpleado       INTEGER  DEFAULT (0),
                                              IDSucursal       INTEGER  DEFAULT (0),
                                              Subtotal         DECIMAL  DEFAULT (0),
                                              IVA16            DECIMAL  DEFAULT (0),
                                              IVA8             DECIMAL  DEFAULT (0),
                                              Total            DECIMAL  DEFAULT (0),
                                              Descuento        DECIMAL  DEFAULT (0),
                                              DescuentoGeneral DECIMAL  DEFAULT (0),
                                              Anticipo         DECIMAL  DEFAULT (0),
                                              Folio            INTEGER  DEFAULT (0),
                                              Serie            CHAR     DEFAULT A,
                                              Status           INTEGER  DEFAULT (0),
                                              MetodoPago       STRING,
                                              Comentario       STRING,
                                              Timbrada         INTEGER  DEFAULT (0),
                                              Cancelada        INTEGER  DEFAULT (0),
                                              FechaOperacion   DATETIME,
                                              FormaPago        TEXT,
                                              num_cuenta       TEXT,
                                              moneda           TEXT,
                                              tipo_cambio      DOUBLE,
                                              Cliente          TEXT,
                                              RFC              TEXT);";
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
                                             FechaOperacion,
                                             FormaPago) 
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
                                             FechaOperacion,
                                             FormaPago 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaVentas(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Ventas

        // Tabla de Clientes 21
        #region Tabla Clientes
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
                                              Status INTEGER DEFAULT (1),
                                              TipoCliente INTEGER  DEFAULT (0),
                                              NumeroCliente TEXT);";
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
                                             FechaOperacion,
                                             Status) 
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
                                             FechaOperacion,
                                             Status 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaClientes(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Clientes

        // Tabla de RevisarInventario 22
        #region Tabla RevisarInventario
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
            return $@"CREATE TABLE '{tabla}' (ID                 INTEGER  NOT NULL    PRIMARY KEY AUTOINCREMENT    UNIQUE,
                                              IDAlmacen          TEXT     NOT NULL,
                                              Nombre             TEXT     NOT NULL,
                                              ClaveInterna       TEXT,
                                              CodigoBarras       TEXT,
                                              StockAlmacen       INTEGER  NOT NULL    DEFAULT (0),
                                              StockFisico        INTEGER  NOT NULL    DEFAULT (0),
                                              NoRevision         INT      DEFAULT (0),
                                              Fecha              DATETIME,
                                              Vendido            INT      DEFAULT (0),
                                              Diferencia         INT      DEFAULT (0),
                                              IDUsuario          INTEGER,
                                              Tipo               TEXT,
                                              StatusRevision     INT      DEFAULT (0),
                                              StatusInventariado INT      DEFAULT (0),
                                              PrecioProducto     REAL,
                                              IDComputadora      TEXT,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES USuarios (ID));";
        }

        public string QueryUpdateTablaRevisarInventario(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDAlmacen,
                                             Nombre,
                                             ClaveInterna,
                                             CodigoBarras,
                                             StockAlmacen,
                                             StockFisico,
                                             NoRevision,
                                             Fecha,
                                             Vendido,
                                             Diferencia,
                                             IDUsuario,
                                             Tipo,
                                             StatusRevision,
                                             StatusInventariado,
                                             PrecioProducto) 
                                      SELECT ID,
                                             IDAlmacen,
                                             Nombre,
                                             ClaveInterna,
                                             CodigoBarras,
                                             StockAlmacen,
                                             StockFisico,
                                             NoRevision,
                                             Fecha,
                                             Vendido,
                                             Diferencia,
                                             IDUsuario,
                                             Tipo,
                                             StatusRevision,
                                             StatusInventariado,
                                             PrecioProducto 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaRevisarInventario(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla RevisarInventario

        // Tabla de DetallesVenta 23
        #region Tabla DetallesVenta
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
        #endregion Tabla DetallesVenta

        // Tabla de Abonos 24
        #region Tabla Abonos
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
        #endregion Tabla Abonos

        // Tabla de Categorias 25
        #region Tabla Categorias
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
        #endregion Tabla Categorias

        // Tabla de Ubicaciones 26
        #region Tabla Ubicaciones
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
        #endregion Tabla Ubicaciones

        // Tabla de DetalleGeneral 27
        #region Tabla DetalleGeneral
        public int GetDetalleGeneral()
        {
            return DetalleGeneral;
        }

        public string PragmaTablaDetalleGeneral(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDetalleGeneral(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDetalleGeneral(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario   INTEGER NOT NULL,
                                              ChckName    TEXT    NOT NULL,
                                              Descripcion TEXT    NOT NULL,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE);";
        }

        public string QueryUpdateTablaDetalleGeneral(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             ChckName,
                                             Descripcion) 
                                      SELECT ID,
                                             IDUsuario,
                                             ChckName,
                                             Descripcion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaDetalleGeneral(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla DetalleGeneral

        // Tabla de DetallesProductoGenerales 28
        #region Tabla DetallesProductoGenerales
        public int GetDetallesProductoGenerales()
        {
            return DetallesProductoGenerales;
        }

        public string PragmaTablaDetallesProductoGenerales(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameDetallesProductoGenerales(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaDetallesProductoGenerales(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDProducto        INTEGER NOT NULL,
                                              IDUsuario         INTEGER NOT NULL,
                                              IDDetalleGral     INTEGER NOT NULL,
                                              StatusDetalleGral INTEGER NOT NULL  DEFAULT (1),
                                              panelContenido    TEXT    NOT NULL  DEFAULT panelContenido,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
                                              FOREIGN KEY (IDProducto)
                                              REFERENCES Productos (ID) ON DELETE CASCADE  ON UPDATE CASCADE,
                                              FOREIGN KEY (IDDetalleGral)
                                              REFERENCES DetalleGeneral (ID) ON DELETE CASCADE   ON UPDATE CASCADE);";
        }

        public string QueryUpdateTablaDetallesProductoGenerales(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDProducto,
                                             IDUsuario,
                                             IDDetalleGral,
                                             StatusDetalleGral,
                                             panelContenido) 
                                      SELECT ID,
                                             IDProducto,
                                             IDUsuario,
                                             IDDetalleGral,
                                             StatusDetalleGral,
                                             panelContenido 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaDetallesProductoGenerales(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla DetallesProductoGenerales

        // Tabla de ProductMessage 29
        #region Tabla ProductMessage
        public int GetProductMessage()
        {
            return ProductMessage;
        }

        public string PragmaTablaProductMessage(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameProductMessage(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaProductMessage(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                      INTEGER NOT NULL    PRIMARY KEY AUTOINCREMENT    UNIQUE,
                                              IDProducto              INTEGER,
                                              ProductOfMessage        TEXT,
                                              ProductMessageActivated BOOLEAN DEFAULT (0),
                                              FOREIGN KEY (IDProducto)    REFERENCES Productos (ID) ON UPDATE CASCADE  ON DELETE CASCADE
                                             );";
        }

        public string QueryUpdateTablaProductMessage(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDProducto,
                                             ProductOfMessage,
                                             ProductMessageActivated) 
                                      SELECT ID,
                                             IDProducto,
                                             ProductOfMessage,
                                             ProductMessageActivated 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaProductMessage(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla ProductMessage

        // Tabla de CodigoBarrasGenerado 30
        #region Tabla CodigoBarrasGenerado
        public int GetCodigoBarrasGenerado()
        {
            return CodigoBarrasGenerado;
        }

        public string PragmaTablaCodigoBarrasGenerado(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCodigoBarrasGenerado(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCodigoBarrasGenerado(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID              INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario       INTEGER,
                                              CodigoBarras    TEXT,
                                              FechaInventario TEXT,
                                              NoRevision      INTEGER DEFAULT (1)
                                             );";
        }

        public string QueryUpdateTablaCodigoBarrasGenerado(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             CodigoBarras) 
                                      SELECT ID,
                                             IDUsuario,
                                             CodigoBarras 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaCodigoBarrasGenerado(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla CodigoBarrasGenerado

        // Tabla de Empleados 31
        #region Tabla Empleados
        public int GetEmpleados()
        {
            return Empleados;
        }

        public string PragmaTablaEmpleados(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameEmpleados(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaEmpleados(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID           INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario    INTEGER NOT NULL,
                                              nombre       TEXT,
                                              usuario      TEXT    NOT NULL,
                                              contrasena   TEXT    NOT NULL,
                                              estatus      INTEGER DEFAULT 1,
                                              p_anticipo   INTEGER DEFAULT 1,
                                              p_caja       INTEGER DEFAULT 1,
                                              p_cliente    INTEGER DEFAULT 1,
                                              p_config     INTEGER DEFAULT 1,
                                              p_empleado   INTEGER DEFAULT 1,
                                              p_empresa    INTEGER DEFAULT 1,
                                              p_factura    INTEGER DEFAULT 1,
                                              p_inventario INTEGER DEFAULT 1,
                                              p_mdatos     INTEGER DEFAULT 1,
                                              p_producto   INTEGER DEFAULT 1,
                                              p_proveedor  INTEGER DEFAULT 1,
                                              p_reporte    INTEGER DEFAULT 1,
                                              p_servicio   INTEGER DEFAULT 1,
                                              p_venta      INTEGER DEFAULT 1);";
        }

        public string QueryUpdateTablaEmpleados(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             nombre,
                                             usuario,
                                             contrasena,
                                             estatus,
                                             p_anticipo,
                                             p_caja,
                                             p_cliente,
                                             p_config,
                                             p_empleado,
                                             p_empresa,
                                             p_factura,
                                             p_inventario,
                                             p_mdatos,
                                             p_producto,
                                             p_proveedor,
                                             p_reporte,
                                             p_servicio,
                                             p_venta) 
                                      SELECT ID,
                                             IDUsuario,
                                             nombre,
                                             usuario,
                                             contrasena,
                                             estatus,
                                             p_anticipo,
                                             p_caja,
                                             p_cliente,
                                             p_config,
                                             p_empleado,
                                             p_empresa,
                                             p_factura,
                                             p_inventario,
                                             p_mdatos,
                                             p_producto,
                                             p_proveedor,
                                             p_reporte,
                                             p_servicio,
                                             p_venta 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaEmpleados(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Empleados

        // Tabla de MensajesInventario 32
        #region Tabla MensajesInventario
        public int GetMensajesInventario()
        {
            return MensajesInventario;
        }

        public string PragmaTablaMensajesInventario(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameMensajesInventario(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaMensajesInventario(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID         INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario  INTEGER NOT NULL,
                                              IDProducto INTEGER NOT NULL,
                                              Mensaje    TEXT,
                                              Activo     INTEGER DEFAULT (0));";
        }

        public string QueryUpdateTablaMensajesInventario(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             IDProducto,
                                             Mensaje,
                                             Activo) 
                                      SELECT ID,
                                             IDUsuario,
                                             IDProducto,
                                             Mensaje,
                                             Activo 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaMensajesInventario(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla MensajesInventario

        // Tabla de Catalogo_claves_producto 33
        #region Tabla Catalogo_claves_producto
        public int GetCatalogo_claves_producto()
        {
            return Catalogo_claves_producto;
        }

        public string PragmaTablaCatalogo_claves_producto(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCatalogo_claves_producto(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCatalogo_claves_producto(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID          INTEGER PRIMARY KEY AUTOINCREMENT,
                                              clave       TEXT,
                                              descripcion TEXT);";
        }

        public string QueryUpdateTablaCatalogo_claves_producto(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             clave,
                                             descripcion) 
                                      SELECT ID,
                                             clave,
                                             descripcion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaCatalogo_claves_producto(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Catalogo_claves_producto

        // Tabla de Catalogo_monedas 34
        #region Tabla Catalogo_monedas
        public int GetCatalogo_monedas()
        {
            return Catalogo_monedas;
        }

        public string PragmaTablaCatalogo_monedas(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameCatalogo_monedas(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaCatalogo_monedas(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (clave_monedas  TEXT    PRIMARY KEY   NOT NULL,
                                              descripcion    TEXT,
                                              cant_decimales INTEGER);";
        }

        public string QueryUpdateTablaCatalogo_monedas(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (clave_monedas,
                                             descripcion,
                                             cant_decimales) 
                                      SELECT clave_monedas,
                                             descripcion,
                                             cant_decimales 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaCatalogo_monedas(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Catalogo_monedas

        // Tabla de HistorialPrecios 35
        #region Tabla HistorialPrecios
        public int GetHistorialPrecios()
        {
            return HistorialPrecios;
        }

        public string PragmaTablaHistorialPrecios(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameHistorialPrecios(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaHistorialPrecios(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID             INTEGER  PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario      INTEGER  NOT NULL,
                                              IDEmpleado     INTEGER  DEFAULT (0),
                                              IDProducto     INTEGER  NOT NULL,
                                              PrecioAnterior REAL     DEFAULT (0),
                                              PrecioNuevo    REAL     DEFAULT (0),
                                              Origen		   TEXT,
                                              FechaOperacion DATETIME);";
        }

        public string QueryUpdateTablaHistorialPrecios(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             IDEmpleado,
                                             IDProducto,
                                             PrecioAnterior,
                                             PrecioNuevo,
                                             Origen,
                                             FechaOperacion) 
                                      SELECT ID,
                                             IDUsuario,
                                             IDEmpleado,
                                             IDProducto,
                                             PrecioAnterior,
                                             PrecioNuevo,
                                             Origen,
                                             FechaOperacion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaHistorialPrecios(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla HistorialPrecios

        // Tabla de appSettings 36
        #region Tabla appSettings
        public int GetappSettings()
        {
            return appSettings;
        }

        public string PragmaTablaappSettings(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameappSettings(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaappSettings(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                       INTEGER PRIMARY KEY AUTOINCREMENT,
                                              concepto                 TEXT,
                                              checkBoxConcepto         INTEGER NOT NULL DEFAULT (0),
                                              textComboBoxConcepto     TEXT,
                                              checkBoxComboBoxConcepto INTEGER NOT NULL DEFAULT (0),
                                              IDUsuario                INTEGER NOT NULL,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE);";
        }

        public string QueryUpdateTablaappSettings(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             concepto,
                                             checkBoxConcepto,
                                             textComboBoxConcepto,
                                             checkBoxComboBoxConcepto,
                                             IDUsuario) 
                                      SELECT ID,
                                             concepto,
                                             checkBoxConcepto,
                                             textComboBoxConcepto,
                                             checkBoxComboBoxConcepto,
                                             IDUsuario 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaappSettings(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla appSettings

        // Tabla de Configuracion 37
        #region Tabla Configuracion
        public int GetConfiguracion()
        {
            return Configuracion;
        }

        public string PragmaTablaConfiguracion(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameConfiguracion(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaConfiguracion(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                   INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario            INTEGER NOT NULL,
                                              TicketVenta          INTEGER DEFAULT (0),
                                              StockNegativo        INTEGER DEFAULT (0),
                                              CorreoPrecioProducto INTEGER DEFAULT (0),
                                              CorreoStockProducto  INTEGER DEFAULT (0),
                                              CorreoStockMinimo    INTEGER DEFAULT (0),
                                              CorreoVentaProducto  INTEGER DEFAULT (0),
                                              IniciarProceso       INTEGER DEFAULT (0),
                                              MostrarPrecioProducto INTEGER DEFAULT (0),
                                              MostrarCodigoProducto INTEGER DEFAULT (0),
                                              PorcentajePrecio      DECIMAL DEFAULT (1.6),
                                              PrecioMayoreo         INTEGER DEFAULT (0),
                                              MinimoMayoreo         INTEGER DEFAULT (0),
                                              checkNoVendidos       INTEGER DEFAULT (0),
                                              diasNoVendidos        INTEGER DEFAULT (0));";
        }

        public string QueryUpdateTablaConfiguracion(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             TicketVenta,
                                             StockNegativo,
                                             CorreoPrecioProducto,
                                             CorreoStockProducto,
                                             CorreoStockMinimo,
                                             CorreoVentaProducto,
                                             IniciarProceso,
                                             MostrarPrecioProducto,
                                             MostrarCodigoProducto,
                                             PorcentajePrecio,
                                             PrecioMayoreo,
                                             MinimoMayoreo) 
                                      SELECT ID,
                                             IDUsuario,
                                             TicketVenta,
                                             StockNegativo,
                                             CorreoPrecioProducto,
                                             CorreoStockProducto,
                                             CorreoStockMinimo,
                                             CorreoVentaProducto,
                                             IniciarProceso,
                                             MostrarPrecioProducto,
                                             MostrarCodigoProducto,
                                             PorcentajePrecio,
                                             PrecioMayoreo,
                                             MinimoMayoreo 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaConfiguracion(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Configuracion

        // Tabla de TipoClientes 38
        #region Tabla TipoClientes
        public int GetTipoClientes()
        {
            return TipoClientes;
        }

        public string PragmaTablaTipoClientes(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameTipoClientes(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaTipoClientes(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                  INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario           INTEGER NOT NULL DEFAULT (0),
                                              Nombre              TEXT    NOT NULL,
                                              DescuentoPorcentaje REAL    DEFAULT (0),
                                              Habilitar           INTEGER DEFAULT (1),
                                              FechaOperacion      DATETIME);";
        }

        public string QueryUpdateTablaTipoClientes(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             Nombre,
                                             DescuentoPorcentaje,
                                             Habilitar) 
                                      SELECT ID,
                                             IDUsuario,
                                             Nombre,
                                             DescuentoPorcentaje,
                                             Habilitar 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaTipoClientes(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla TipoClientes

        // Tabla de FiltroProducto 39
        #region Tabla FiltroProducto
        public int GetFiltroProducto()
        {
            return FiltroProducto;
        }

        public string PragmaTablaFiltroProducto(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroProducto(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroProducto(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                   INTEGER PRIMARY KEY AUTOINCREMENT,
                                              concepto             TEXT,
                                              checkBoxConcepto     INTEGER NOT NULL DEFAULT (0),
                                              textComboBoxConcepto TEXT,
                                              textCantidad         TEXT    DEFAULT (0),
                                              IDUsuario            INTEGER NOT NULL,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE);";
        }

        public string QueryUpdateTablaFiltroProducto(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             concepto,
                                             checkBoxConcepto,
                                             textComboBoxConcepto,
                                             textCantidad,
                                             IDUsuario) 
                                      SELECT ID,
                                             concepto,
                                             checkBoxConcepto,
                                             textComboBoxConcepto,
                                             textCantidad,
                                             IDUsuario 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroProducto(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla FiltroProducto

        // Tabla de Facturas 40
        #region Tabla Facturas
        public int GetFiltroFacturas()
        {
            return Facturas;
        }

        public string PragmaTablaFiltroFacturas(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroFacturas(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroFacturas(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                  INTEGER      PRIMARY KEY AUTOINCREMENT NOT NULL,
                                              id_usuario          INTEGER      REFERENCES Usuarios (ID),
                                              id_venta            INTEGER      REFERENCES Ventas (ID),
                                              id_empleado         INTEGER,
                                              timbrada            INTEGER (1)  DEFAULT (0),
                                              cancelada           INTEGER (1)  DEFAULT (0),
                                              fecha_certificacion DATETIME,
                                              UUID                VARCHAR,
                                              rfc_pac             VARCHAR,
                                              sello_sat           TEXT,
                                              sello_cfd           TEXT,
                                              metodo_pago         VARCHAR (3),
                                              forma_pago          VARCHAR (2),
                                              num_cuenta          VARCHAR,
                                              moneda              VARCHAR (3),
                                              tipo_cambio         DOUBLE,
                                              folio               VARCHAR,
                                              serie               VARCHAR,
                                              tipo_comprobante    VARCHAR (1),
                                              uso_cfdi            VARCHAR (3),
                                              total               DOUBLE       DEFAULT (0.0),
                                              e_rfc               VARCHAR (13),
                                              e_razon_social      VARCHAR,
                                              e_regimen           VARCHAR (3),
                                              e_correo            VARCHAR,
                                              e_telefono          VARCHAR (10),
                                              e_cp                VARCHAR (5),
                                              e_estado            VARCHAR,
                                              e_municipio         VARCHAR,
                                              e_colonia           VARCHAR,
                                              e_calle             VARCHAR,
                                              e_num_ext           VARCHAR,
                                              e_num_int           VARCHAR,
                                              r_rfc               VARCHAR (13),
                                              r_razon_social      VARCHAR,
                                              r_nombre_comercial  VARCHAR,
                                              r_correo            VARCHAR,
                                              r_telefono          VARCHAR,
                                              r_pais              VARCHAR,
                                              r_estado            VARCHAR,
                                              r_municipio         VARCHAR,
                                              r_localidad         VARCHAR,
                                              r_cp                VARCHAR (10),
                                              r_colonia           VARCHAR,
                                              r_calle             VARCHAR,
                                              r_num_ext           VARCHAR,
                                              r_num_int           VARCHAR,
                                              con_complementos    INTEGER (1)  DEFAULT (0),
                                              fecha_hora_cpago    DATETIME,
                                              monto_cpago         DOUBLE,
                                              resta_cpago         DOUBLE);";
        }

        public string QueryUpdateTablaFiltroFacturas(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             id_usuario,
                                             id_venta,
                                             id_empleado,
                                             timbrada,
                                             cancelada,
                                             fecha_certificacion,
                                             UUID,
                                             rfc_pac,
                                             sello_sat,
                                             sello_cfd,
                                             metodo_pago,
                                             forma_pago,
                                             num_cuenta,
                                             moneda,
                                             tipo_cambio,
                                             folio,
                                             serie,
                                             tipo_comprobante,
                                             uso_cfdi,
                                             total,
                                             e_rfc,
                                             e_razon_social,
                                             e_regimen,
                                             e_correo,
                                             e_telefono,
                                             e_cp,
                                             e_estado,
                                             e_municipio,
                                             e_colonia,
                                             e_calle,
                                             e_num_ext,
                                             e_num_int,
                                             r_rfc,
                                             r_razon_social,
                                             r_nombre_comercial,
                                             r_correo,
                                             r_telefono,
                                             r_pais,
                                             r_estado,
                                             r_municipio,
                                             r_localidad,
                                             r_cp,
                                             r_colonia,
                                             r_calle,
                                             r_num_ext,
                                             r_num_int,
                                             con_complementos,
                                             fecha_hora_cpago,
                                             monto_cpago,
                                             resta_cpago) 
                                      SELECT ID,
                                             id_usuario,
                                             id_venta,
                                             id_empleado,
                                             timbrada,
                                             cancelada,
                                             fecha_certificacion,
                                             UUID,
                                             rfc_pac,
                                             sello_sat,
                                             sello_cfd,
                                             metodo_pago,
                                             forma_pago,
                                             num_cuenta,
                                             moneda,
                                             tipo_cambio,
                                             folio,
                                             serie,
                                             tipo_comprobante,
                                             uso_cfdi,
                                             total,
                                             e_rfc,
                                             e_razon_social,
                                             e_regimen,
                                             e_correo,
                                             e_telefono,
                                             e_cp,
                                             e_estado,
                                             e_municipio,
                                             e_colonia,
                                             e_calle,
                                             e_num_ext,
                                             e_num_int,
                                             r_rfc,
                                             r_razon_social,
                                             r_nombre_comercial,
                                             r_correo,
                                             r_telefono,
                                             r_pais,
                                             r_estado,
                                             r_municipio,
                                             r_localidad,
                                             r_cp,
                                             r_colonia,
                                             r_calle,
                                             r_num_ext,
                                             r_num_int,
                                             con_complementos,
                                             fecha_hora_cpago,
                                             monto_cpago,
                                             resta_cpago 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroFacturas(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Facturas

        // Tabla de Facturas_impuestos 41
        #region Tabla Facturas_impuestos
        public int GetFiltroFacturas_impuestos()
        {
            return Facturas_impuestos;
        }

        public string PragmaTablaFiltroFacturas_impuestos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroFacturas_impuestos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroFacturas_impuestos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                  INTEGER      PRIMARY KEY AUTOINCREMENT NOT NULL,
                                              id_factura_producto INTEGER      REFERENCES Facturas_productos (id_factura) NOT NULL,
                                              tipo                VARCHAR (10),
                                              impuesto            VARCHAR (4),
                                              tipo_factor         VARCHAR (6),
                                              tasa_cuota          VARCHAR,
                                              definir             VARCHAR,
                                              importe             DOUBLE);";
        }

        public string QueryUpdateTablaFiltroFacturas_impuestos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             id_factura_producto,
                                             tipo,
                                             impuesto,
                                             tipo_factor,
                                             tasa_cuota,
                                             definir,
                                             importe) 
                                      SELECT ID,
                                             id_factura_producto,
                                             tipo,
                                             impuesto,
                                             tipo_factor,
                                             tasa_cuota,
                                             definir,
                                             importe 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroFacturas_impuestos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Facturas_impuestos

        // Tabla de Facturas_productos 42
        #region Tabla Facturas_productos
        public int GetFiltroFacturas_productos()
        {
            return Facturas_productos;
        }

        public string PragmaTablaFiltroFacturas_productos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroFacturas_productos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroFacturas_productos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID             INTEGER     PRIMARY KEY AUTOINCREMENT	NOT NULL,
                                              id_factura     INTEGER     REFERENCES Facturas (ID),
                                              clave_unidad   VARCHAR (3),
                                              clave_producto VARCHAR (8),
                                              descripcion    VARCHAR,
                                              cantidad       VARCHAR,
                                              precio_u       DOUBLE,
                                              base           DOUBLE,
                                              tasa_cuota     VARCHAR,
                                              importe_iva    DOUBLE,
                                              descuento      TEXT);";
        }

        public string QueryUpdateTablaFiltroFacturas_productos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             id_factura,
                                             clave_unidad,
                                             clave_producto,
                                             descripcion,
                                             cantidad,
                                             precio_u,
                                             base,
                                             tasa_cuota,
                                             importe_iva) 
                                      SELECT ID,
                                             id_factura,
                                             clave_unidad,
                                             clave_producto,
                                             descripcion,
                                             cantidad,
                                             precio_u,
                                             base,
                                             tasa_cuota,
                                             importe_iva 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroFacturas_productos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Facturas_productos

        // Tabla de Facturas_complemento_pago 43
        #region Tabla Facturas_complemento_pago
        public int GetFiltroFacturas_complemento_pago()
        {
            return Facturas_complemento_pago;
        }

        public string PragmaTablaFiltroFacturas_complemento_pago(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroFacturas_complemento_pago(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroFacturas_complemento_pago(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                   INTEGER      PRIMARY KEY AUTOINCREMENT  NOT NULL,
                                              id_factura           INTEGER      REFERENCES Facturas (ID)  NOT NULL,
                                              id_factura_principal INTEGER      NOT NULL,
                                              uuid                 VARCHAR (38),
                                              moneda               VARCHAR,
                                              tipo_cambio          VARCHAR,
                                              metodo_pago          VARCHAR,
                                              num_parcialidad      INTEGER (2),
                                              saldo_anterior       DOUBLE,
                                              importe_pagado       DOUBLE,
                                              saldo_insoluto       DOUBLE,
                                              timbrada             INTEGER (1)  DEFAULT (0),
                                              cancelada            INTEGER (1)  DEFAULT (0));";
        }

        public string QueryUpdateTablaFiltroFacturas_complemento_pago(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             id_factura,
                                             id_factura_principal,
                                             uuid,
                                             moneda,
                                             tipo_cambio,
                                             metodo_pago,
                                             num_parcialidad,
                                             saldo_anterior,
                                             importe_pagado,
                                             saldo_insoluto,
                                             timbrada,
                                             cancelada) 
                                      SELECT ID,
                                             id_factura,
                                             id_factura_principal,
                                             uuid,
                                             moneda,
                                             tipo_cambio,
                                             metodo_pago,
                                             num_parcialidad,
                                             saldo_anterior,
                                             importe_pagado,
                                             saldo_insoluto,
                                             timbrada,
                                             cancelada 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroFacturas_complemento_pago(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla Facturas_complemento_pago

        // Tabla de FiltroDinamico 44
        #region Tabla FiltroDinamico
        public int GetFiltroFiltroDinamico()
        {
            return FiltroDinamico;
        }

        public string PragmaTablaFiltroFiltroDinamico(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroFiltroDinamico(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroFiltroDinamico(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID               INTEGER PRIMARY KEY AUTOINCREMENT,
                                              concepto         TEXT,
                                              checkBoxConcepto INTEGER NOT NULL DEFAULT (0),
                                              textCantidad     TEXT,
                                              IDUsuario        INTEGER NOT NULL,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE);";
        }

        public string QueryUpdateTablaFiltroFiltroDinamico(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             concepto,
                                             checkBoxConcepto,
                                             textCantidad,
                                             IDUsuario) 
                                      SELECT ID,
                                             concepto,
                                             checkBoxConcepto,
                                             textCantidad,
                                             IDUsuario 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroFiltroDinamico(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla FiltroDinamico

        // Tabla de ConceptosDinamicos 45
        #region Tabla ConceptosDinamicos
        public int GetFiltroConceptosDinamicos()
        {
            return ConceptosDinamicos;
        }

        public string PragmaTablaFiltroConceptosDinamicos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroConceptosDinamicos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroConceptosDinamicos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID             INTEGER  PRIMARY KEY AUTOINCREMENT,
                                              IDUsuario      INTEGER  NOT NULL,
                                              IDEmpleado     INTEGER  DEFAULT (0),
                                              Concepto       TEXT,
                                              Origen         TEXT,
                                              Status         INTEGER  DEFAULT (1),
                                              FechaOperacion DATETIME);";
        }

        public string QueryUpdateTablaFiltroConceptosDinamicos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             IDEmpleado,
                                             Concepto,
                                             Origen,
                                             Status,
                                             FechaOperacion) 
                                      SELECT ID,
                                             IDUsuario,
                                             IDEmpleado,
                                             Concepto,
                                             Origen,
                                             Status,
                                             FechaOperacion 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroConceptosDinamicos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla ConceptosDinamicos

        // Tabla de CorreosProducto 46
        #region Tabla CorreosProducto
        public int GetFiltroCorreosProducto()
        {
            return CorreosProducto;
        }

        public string PragmaTablaFiltroCorreosProducto(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroCorreosProducto(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroCorreosProducto(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID                   INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                                              IDUsuario            INTEGER NOT NULL,
                                              IDEmpleado           INTEGER DEFAULT (0),
                                              IDProducto           INTEGER NOT NULL,
                                              CorreoPrecioProducto INTEGER DEFAULT (0),
                                              CorreoStockProducto  INTEGER DEFAULT (0),
                                              CorreoStockMinimo    INTEGER DEFAULT (0),
                                              CorreoVentaProducto  INTEGER DEFAULT (0));";
        }

        public string QueryUpdateTablaFiltroCorreosProducto(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDUsuario,
                                             IDEmpleado,
                                             IDProducto,
                                             CorreoPrecioProducto,
                                             CorreoStockProducto,
                                             CorreoStockMinimo,
                                             CorreoVentaProducto) 
                                      SELECT ID,
                                             IDUsuario,
                                             IDEmpleado,
                                             IDProducto,
                                             CorreoPrecioProducto,
                                             CorreoStockProducto,
                                             CorreoStockMinimo,
                                             CorreoVentaProducto 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroCorreosProducto(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla CorreosProducto

        // Tabla de FiltrosDinamicosVetanaFiltros 47
        #region Tabla FiltrosDinamicosVetanaFiltros
        public int GetFiltroFiltrosDinamicosVetanaFiltros()
        {
            return FiltrosDinamicosVetanaFiltros;
        }

        public string PragmaTablaFiltroFiltrosDinamicosVetanaFiltros(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroFiltrosDinamicosVetanaFiltros(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroFiltrosDinamicosVetanaFiltros(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID            INTEGER PRIMARY KEY AUTOINCREMENT,
                                              checkBoxValue INTEGER NOT NULL  DEFAULT (0),
                                              concepto      TEXT,
                                              strFiltro     TEXT,
                                              IDUsuario     INTEGER NOT NULL,
                                              FOREIGN KEY (IDUsuario)
                                              REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE);";
        }

        public string QueryUpdateTablaFiltroFiltrosDinamicosVetanaFiltros(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             checkBoxValue,
                                             concepto,
                                             strFiltro,
                                             IDUsuario) 
                                      SELECT ID,
                                             checkBoxValue,
                                             concepto,
                                             strFiltro,
                                             IDUsuario 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroFiltrosDinamicosVetanaFiltros(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla CorreosProducto

        // Tabla de EmpleadosPermisos 48
        #region Tabla EmpleadosPermisos
        public int GetFiltroEmpleadosPermisos()
        {
            return EmpleadosPermisos;
        }

        public string PragmaTablaFiltroEmpleadosPermisos(string tabla)
        {
            return $"PRAGMA table_info('{tabla}');";
        }

        public string QueryRenameFiltroEmpleadosPermisos(string tabla)
        {
            return $"ALTER TABLE '{tabla}' RENAME TO '{tabla}_temp';";
        }

        public string QueryNvaTablaFiltroEmpleadosPermisos(string tabla)
        {
            return $@"CREATE TABLE '{tabla}' (ID         INTEGER PRIMARY KEY AUTOINCREMENT,
                                              IDEmpleado INTEGER NOT NULL,
                                              IDUsuario  INTEGER NOT NULL,
                                              Seccion    TEXT,
                                              Opcion1    INTEGER DEFAULT (1),
                                              Opcion2    INTEGER DEFAULT (1),
                                              Opcion3    INTEGER DEFAULT (1),
                                              Opcion4    INTEGER DEFAULT (1),
                                              Opcion5    INTEGER DEFAULT (1),
                                              Opcion6    INTEGER DEFAULT (1),
                                              Opcion7    INTEGER DEFAULT (1),
                                              Opcion8    INTEGER DEFAULT (1),
                                              Opcion9    INTEGER DEFAULT (1),
                                              Opcion10   INTEGER DEFAULT (1),
                                              Opcion11   INTEGER DEFAULT (1),
                                              Opcion12   INTEGER DEFAULT (1),
                                              Opcion13   INTEGER DEFAULT (1),
                                              Opcion14   INTEGER DEFAULT (1),
                                              Opcion15   INTEGER DEFAULT (1),
                                              Opcion16   INTEGER DEFAULT (1),
                                              Opcion17   INTEGER DEFAULT (1),
                                              Opcion18   INTEGER DEFAULT (1),
                                              Opcion19   INTEGER DEFAULT (1),
                                              Opcion20   INTEGER DEFAULT (1),
                                              Opcion21   INTEGER DEFAULT (1),
                                              Opcion22   INTEGER DEFAULT (1),
                                              Opcion23   INTEGER DEFAULT (1),
                                              Opcion24   INTEGER DEFAULT (1));";
        }

        public string QueryUpdateTablaFiltroEmpleadosPermisos(string tabla)
        {
            return $@"INSERT INTO '{tabla}' (ID,
                                             IDEmpleado,
                                             IDUsuario,
                                             Seccion,
                                             Opcion1,
                                             Opcion2,
                                             Opcion3,
                                             Opcion4,
                                             Opcion5,
                                             Opcion6,
                                             Opcion7,
                                             Opcion8,
                                             Opcion9,
                                             Opcion10,
                                             Opcion11,
                                             Opcion12,
                                             Opcion13,
                                             Opcion14,
                                             Opcion15,
                                             Opcion16,
                                             Opcion17,
                                             Opcion18,
                                             Opcion19,
                                             Opcion20) 
                                      SELECT ID,
                                             IDEmpleado,
                                             IDUsuario,
                                             Seccion,
                                             Opcion1,
                                             Opcion2,
                                             Opcion3,
                                             Opcion4,
                                             Opcion5,
                                             Opcion6,
                                             Opcion7,
                                             Opcion8,
                                             Opcion9,
                                             Opcion10,
                                             Opcion11,
                                             Opcion12,
                                             Opcion13,
                                             Opcion14,
                                             Opcion15,
                                             Opcion16,
                                             Opcion17,
                                             Opcion18,
                                             Opcion19,
                                             Opcion20 
                                        FROM '{tabla}_temp';";
        }

        public string DropTablaFiltroEmpleadosPermisos(string tabla)
        {
            return $"DROP TABLE '{tabla}_temp';";
        }
        #endregion Tabla EmpleadosPermisos
    }
}
