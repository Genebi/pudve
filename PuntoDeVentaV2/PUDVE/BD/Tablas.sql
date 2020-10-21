-- 01 Tabla de usuarios
CREATE TABLE IF NOT EXISTS Usuarios (
    ID  INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Usuario TEXT NOT NULL,
    Password  TEXT NOT NULL,
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
    Referencia_ID TEXT,
    VerificacionNS INTEGER DEFAULT (0),
    num_certificado TEXT,
    fecha_caducidad_cer TEXT,
    password_cer TEXT,
    FechaHoy DATE,
    timbres INTEGER (10) DEFAULT (0) 
);

-- 02 Tabla de Productos
CREATE TABLE IF NOT EXISTS Productos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Nombre TEXT NOT NULL,
    Stock DECIMAL (16, 2) NOT NULL DEFAULT (0),
    Precio DECIMAL (16, 2) NOT NULL DEFAULT (0),
    Categoria TEXT,
    ClaveInterna TEXT,
    CodigoBarras TEXT,
    ClaveProducto TEXT,
    UnidadMedida TEXT,
    TipoDescuento INTEGER DEFAULT (0),
    IDUsuario INTEGER,
    Status INT NOT NULL DEFAULT (1),
    ProdImage TEXT,
    Tipo TEXT NOT NULL DEFAULT ('P'),
    Base DECIMAL (16, 2) DEFAULT (0),
    IVA DECIMAL (16, 2) DEFAULT (0),
    Impuesto TEXT,
    NombreAlterno1 TEXT,
    NombreAlterno2 TEXT,
    NumeroRevision INTEGER DEFAULT (0),
    StockNecesario INTEGER DEFAULT (0),
    StockMinimo INTEGER DEFAULT (0),
    PrecioCompra DECIMAL (16, 2) DEFAULT (0),
    PrecioMayoreo DECIMAL (16, 2) DEFAULT (0),
    FOREIGN KEY (IDUsuario)
    REFERENCES USuarios (ID) 
);

-- 03 Tabla de ProductoRelacionadoXML
CREATE TABLE IF NOT EXISTS ProductoRelacionadoXML (
    IDProductoRelacionadoXML INTEGER  PRIMARY KEY AUTO_INCREMENT NOT NULL,
    NombreXML TEXT NOT NULL,
    Fecha DATETIME NOT NULL,
    IDProducto INTEGER,
    IDUsuario INTEGER,
    FOREIGN KEY (IDUsuario)
    REFERENCES USuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 04 Tabla de HistorialCompras
CREATE TABLE IF NOT EXISTS HistorialCompras (
    ID INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Concepto TEXT NOT NULL,
    Cantidad DECIMAL (16, 2) NOT NULL DEFAULT (0),
    ValorUnitario DECIMAL (16, 2) DEFAULT (0),
    Descuento DECIMAL (16, 2) DEFAULT (0),
    Precio DECIMAL (16, 2) DEFAULT (0),
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
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE (ID ASC)
);

-- 05 Tabla de HistorialModificacionRecordProduct
CREATE TABLE IF NOT EXISTS HistorialModificacionRecordProduct (
    ID INTEGER  PRIMARY KEY AUTO_INCREMENT NOT NULL,
    IDUsuario INTEGER,
    IDRecordProd INTEGER,
    FechaEditRecord DATETIME NOT NULL,
    FOREIGN KEY (IDRecordProd)
    REFERENCES HistorialCompras (ID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 06 Tabla de HistorialPrecios
CREATE TABLE IF NOT EXISTS HistorialPrecios (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
    IDEmpleado INTEGER DEFAULT (0),
    IDProducto INTEGER NOT NULL,
    PrecioAnterior DECIMAL (16, 2) DEFAULT (0),
    PrecioNuevo DECIMAL (16, 2) DEFAULT (0),
    Origen TEXT,
    FechaOperacion DATETIME
);

-- 07 Tabla de MensajesInventario
CREATE TABLE IF NOT EXISTS MensajesInventario (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
    IDProducto INTEGER NOT NULL,
    Mensaje TEXT,
    Activo INTEGER DEFAULT (0) 
);

-- 08 Tabla de ProductMessage
CREATE TABLE IF NOT EXISTS ProductMessage (
    ID INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,
    IDProducto INTEGER,
    ProductOfMessage TEXT,
    ProductMessageActivated BOOLEAN DEFAULT (0),
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 09 Tabla de ProductosDeServicios
CREATE TABLE IF NOT EXISTS ProductosDeServicios (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Fecha DATETIME NOT NULL,
    IDServicio INTEGER,
    IDProducto INTEGER,
    NombreProducto TEXT NOT NULL,
    Cantidad DECIMAL (16, 2) NOT NULL DEFAULT (0),
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDServicio)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 10 Tabla de Clientes
CREATE TABLE IF NOT EXISTS Clientes (
    ID INTEGER  PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
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
    NumeroCliente TEXT,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) 
);

-- 11 Tabla de Empleados
CREATE TABLE IF NOT EXISTS Empleados (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
    nombre TEXT,
    usuario TEXT NOT NULL,
    contrasena TEXT NOT NULL,
    estatus INTEGER DEFAULT (1),
    p_anticipo INTEGER DEFAULT (1),
    p_caja INTEGER DEFAULT (1),
    p_cliente INTEGER DEFAULT (1),
    p_config INTEGER DEFAULT (1),
    p_empleado INTEGER DEFAULT (1),
    p_empresa INTEGER DEFAULT (1),
    p_factura INTEGER DEFAULT (1),
    p_inventario INTEGER DEFAULT (1),
    p_mdatos INTEGER DEFAULT (1),
    p_producto INTEGER DEFAULT (1),
    p_proveedor INTEGER DEFAULT (1),
    p_reporte INTEGER DEFAULT (1),
    p_servicio INTEGER DEFAULT (1),
    p_venta INTEGER DEFAULT (1) 
);

-- 12 Tabla de Ventas
CREATE TABLE IF NOT EXISTS Ventas (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    IDUsuario INTEGER NOT NULL,
    IDCliente INTEGER NOT NULL DEFAULT (0),
    IDEmpleado INTEGER DEFAULT (0),
    IDSucursal INTEGER DEFAULT (0),
    Subtotal DECIMAL (16, 2) DEFAULT (0),
    IVA16 DECIMAL (16, 2) DEFAULT (0),
    IVA8 DECIMAL (16, 2) DEFAULT (0),
    Total DECIMAL (16, 2) DEFAULT (0),
    Descuento DECIMAL (16, 2) DEFAULT (0),
    DescuentoGeneral DECIMAL (16, 2) DEFAULT (0),
    Anticipo DECIMAL (16, 2) DEFAULT (0),
    Folio INTEGER DEFAULT (0),
    Serie CHAR DEFAULT ('A'),
    Status INTEGER DEFAULT (0),
    MetodoPago TEXT,
    Comentario TEXT,
    Timbrada INTEGER DEFAULT (0),
    Cancelada INTEGER DEFAULT (0),
    FechaOperacion DATETIME,
    FormaPago TEXT,
    num_cuenta TEXT,
    moneda TEXT,
    tipo_cambio DOUBLE,
    Cliente TEXT,
    RFC TEXT,
    IDClienteDescuento INTEGER DEFAULT (0) 
);

-- 13 Tabla de ProductosVenta
CREATE TABLE IF NOT EXISTS ProductosVenta (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    IDVenta INTEGER NOT NULL,
    IDProducto INTEGER NOT NULL,
    Nombre TEXT,
    Cantidad DECIMAL (16, 2) DEFAULT (0),
    Precio DECIMAL (16, 2) DEFAULT (0),
    descuento TEXT,
    TipoDescuento INTEGER DEFAULT (0),
    FOREIGN KEY (IDVenta)
    REFERENCES Ventas (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 14 Tabla de Proveedores
CREATE TABLE IF NOT EXISTS Proveedores (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
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
    Status INTEGER  DEFAULT (1) 
);

-- 15 Tabla de DetallesProducto
CREATE TABLE IF NOT EXISTS DetallesProducto (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDProducto INTEGER NOT NULL,
    IDUsuario INTEGER NOT NULL,
    Proveedor TEXT,
    IDProveedor INTEGER DEFAULT (0),
    Categoria TEXT,
    IDCategoria INTEGER DEFAULT (0),
    Ubicacion TEXT,
    IDUbicacion INTEGER DEFAULT (0) 
);

-- 16 Tabla de DetalleGeneral
CREATE TABLE IF NOT EXISTS DetalleGeneral (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
    ChckName TEXT NOT NULL,
    Descripcion TEXT NOT NULL,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 17 Tabla de DetallesProductoGenerales
CREATE TABLE IF NOT EXISTS DetallesProductoGenerales (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDProducto INTEGER NOT NULL,
    IDUsuario INTEGER NOT NULL,
    IDDetalleGral INTEGER NOT NULL,
    StatusDetalleGral INTEGER NOT NULL DEFAULT (1),
    panelContenido TEXT NOT NULL DEFAULT ('panelContenido'),
    FOREIGN KEY (IDDetalleGral)
    REFERENCES DetalleGeneral (ID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 18 Tabla de DetallesVenta
CREATE TABLE IF NOT EXISTS DetallesVenta (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDVenta INTEGER NOT NULL,
    IDUsuario INTEGER,
    Efectivo DECIMAL (16, 2) DEFAULT (0),
    Tarjeta DECIMAL (16, 2) DEFAULT (0),
    Vales DECIMAL (16, 2) DEFAULT (0),
    Cheque DECIMAL (16, 2) DEFAULT (0),
    Transferencia DECIMAL (16, 2) DEFAULT (0),
    Credito DECIMAL (16, 2) DEFAULT (0),
    Referencia TEXT,
    IDCliente INTEGER DEFAULT (0),
    Cliente TEXT,
    Cuenta TEXT,
    Anticipo DECIMAL (16, 2) DEFAULT (0) 
);

-- 19 Tabla de Abonos
CREATE TABLE IF NOT EXISTS Abonos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDVenta INTEGER  NOT NULL,
    IDUsuario INTEGER,
    Total DECIMAL (16, 2) DEFAULT (0),
    Efectivo DECIMAL (16, 2) DEFAULT (0),
    Tarjeta DECIMAL (16, 2) DEFAULT (0),
    Vales DECIMAL (16, 2) DEFAULT (0),
    Cheque DECIMAL (16, 2) DEFAULT (0),
    Transferencia DECIMAL (16, 2) DEFAULT (0),
    Referencia TEXT,
    FechaOperacion DATETIME NOT NULL
);

-- 20 Tabla de Anticipos
CREATE TABLE IF NOT EXISTS Anticipos (
    ID INTEGER  PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL DEFAULT (0),
    IDEmpleado INTEGER NOT NULL DEFAULT (0),
    Concepto TEXT NOT NULL,
    Importe DECIMAL (16, 2) NOT NULL DEFAULT (0),
    Cliente TEXT NOT NULL,
    FormaPago TEXT NOT NULL,
    Comentarios TEXT,
    Status INT (1) NOT NULL DEFAULT (0),
    Fecha DATETIME NOT NULL,
    IDVenta INTEGER DEFAULT (0),
    ImporteOriginal DECIMAL (16, 2) NOT NULL DEFAULT (0),
    AnticipoAplicado DECIMAL (16, 2) DEFAULT (0) 
);

-- 21 Tabla de appSettings
CREATE TABLE IF NOT EXISTS appSettings (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    concepto TEXT,
    checkBoxConcepto INTEGER NOT NULL DEFAULT (0),
    textComboBoxConcepto TEXT,
    checkBoxComboBoxConcepto INTEGER NOT NULL DEFAULT (0),
    IDUsuario INTEGER NOT NULL,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 22 Tabla de Caja
CREATE TABLE IF NOT EXISTS Caja (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    Operacion TEXT NOT NULL,
    Cantidad DECIMAL (16, 2) NOT NULL DEFAULT (0),
    Saldo DECIMAL (16, 2) NOT NULL DEFAULT (0),
    Concepto TEXT,
    FechaOperacion DATETIME NOT NULL,
    IDUsuario INTEGER NOT NULL,
    Efectivo DECIMAL (16, 2) DEFAULT (0),
    Tarjeta DECIMAL (16, 2) DEFAULT (0),
    Vales DECIMAL (16, 2) DEFAULT (0),
    Cheque DECIMAL (16, 2) DEFAULT (0),
    Transferencia DECIMAL (16, 2) DEFAULT (0),
    Credito DECIMAL (16, 2) DEFAULT (0),
    Anticipo DECIMAL (16, 2) DEFAULT (0) 
);

-- 23 Tabla de Catalogo_claves_producto
CREATE TABLE IF NOT EXISTS Catalogo_claves_producto (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    clave TEXT,
    descripcion TEXT
);

-- 24 Tabla de Catalogo_monedas
CREATE TABLE IF NOT EXISTS Catalogo_monedas (
    clave_moneda VARCHAR(50) NOT NULL,
    descripcion    TEXT,
    cant_decimales INTEGER
);

-- 25 Tabla de CatalogoUnidadesMedida
CREATE TABLE IF NOT EXISTS CatalogoUnidadesMedida (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    ClaveUnidad TEXT,
    Nombre      TEXT
);

/* 26 Tabla de Categorias
//tables.Add(@"CREATE TABLE IF NOT EXISTS Categorias (
//                ID INTEGER PRIMARY KEY AUTO_INCREMENT,
//                IDUsuario INTEGER NOT NULL,
//                Nombre TEXT NOT NULL
//            );");*/

-- 27 Tabla de CodigoBarrasExtras
CREATE TABLE IF NOT EXISTS CodigoBarrasExtras (
    IDCodBarrExt INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    CodigoBarraExtra TEXT,
    IDProducto INTEGER,
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 28 Tabla de CodigoBarrasGenerado
CREATE TABLE IF NOT EXISTS CodigoBarrasGenerado (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER,
    CodigoBarras TEXT,
    FechaInventario TEXT,
    NoRevision INTEGER DEFAULT (1) 
);

-- 29 Tabla de ConceptosDinamicos
CREATE TABLE IF NOT EXISTS ConceptosDinamicos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER  NOT NULL,
    IDEmpleado INTEGER  DEFAULT (0),
    Concepto TEXT,
    Origen TEXT,
    Status INTEGER  DEFAULT (1),
    FechaOperacion DATETIME
);

-- 30 Tabla de Configuracion
CREATE TABLE IF NOT EXISTS Configuracion (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDUsuario INTEGER NOT NULL,
    TicketVenta INTEGER DEFAULT (0),
    StockNegativo INTEGER DEFAULT (0),
    CorreoPrecioProducto INTEGER DEFAULT (0),
    CorreoStockProducto INTEGER DEFAULT (0),
    CorreoStockMinimo INTEGER DEFAULT (0),
    CorreoVentaProducto INTEGER DEFAULT (0),
    IniciarProceso INTEGER DEFAULT (0),
    MostrarPrecioProducto INTEGER DEFAULT (0),
    MostrarCodigoProducto INTEGER DEFAULT (0),
    PorcentajePrecio DECIMAL (16, 2) DEFAULT 1.6,
    PrecioMayoreo INTEGER DEFAULT (0),
    MinimoMayoreo INTEGER DEFAULT (0),
    checkNoVendidos INTEGER DEFAULT (0),
    diasNoVendidos INTEGER DEFAULT (0) 
);

-- 31 Tabla de CorreosProducto
CREATE TABLE IF NOT EXISTS CorreosProducto (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    IDUsuario INTEGER NOT NULL,
    IDEmpleado INTEGER DEFAULT (0),
    IDProducto INTEGER NOT NULL,
    CorreoPrecioProducto INTEGER DEFAULT (0),
    CorreoStockProducto INTEGER DEFAULT (0),
    CorreoStockMinimo INTEGER DEFAULT (0),
    CorreoVentaProducto INTEGER DEFAULT (0) 
);

-- 32 Tabla de DescuentoCliente
CREATE TABLE IF NOT EXISTS DescuentoCliente (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    PrecioProducto DECIMAL (16, 2) DEFAULT (0),
    PorcentajeDescuento DECIMAL (16, 2) DEFAULT (0),
    PrecioDescuento DECIMAL (16, 2) DEFAULT (0),
    Descuento DECIMAL (16, 2) DEFAULT (0),
    IDProducto INTEGER,
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) 
);

-- 33 Tabla de DescuentoMayoreo
CREATE TABLE IF NOT EXISTS DescuentoMayoreo (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    RangoInicial TEXT,
    RangoFinal TEXT,
    Precio DECIMAL (16, 2) DEFAULT (0),
    Checkbox INTEGER,
    IDProducto INTEGER,
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) 
);

-- 34 Tabla de DetallesFacturacionProductos
CREATE TABLE IF NOT EXISTS DetallesFacturacionProductos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Tipo TEXT,
    Impuesto TEXT,
    TipoFactor TEXT,
    TasaCuota DECIMAL (16, 2) DEFAULT (0),
    Definir DECIMAL (16, 2) DEFAULT (0),
    Importe DECIMAL (16, 2) DEFAULT (0),
    IDProducto INTEGER,
    FOREIGN KEY (IDProducto)
    REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 35 Tabla de EmpleadosPermisos
CREATE TABLE IF NOT EXISTS EmpleadosPermisos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDEmpleado INTEGER NOT NULL,
    IDUsuario INTEGER NOT NULL,
    Seccion TEXT,
    Opcion1 INTEGER DEFAULT (1),
    Opcion2 INTEGER DEFAULT (1),
    Opcion3 INTEGER DEFAULT (1),
    Opcion4 INTEGER DEFAULT (1),
    Opcion5 INTEGER DEFAULT (1),
    Opcion6 INTEGER DEFAULT (1),
    Opcion7 INTEGER DEFAULT (1),
    Opcion8 INTEGER DEFAULT (1),
    Opcion9 INTEGER DEFAULT (1),
    Opcion10 INTEGER DEFAULT (1),
    Opcion11 INTEGER DEFAULT (1),
    Opcion12 INTEGER DEFAULT (1),
    Opcion13 INTEGER DEFAULT (1),
    Opcion14 INTEGER DEFAULT (1),
    Opcion15 INTEGER DEFAULT (1),
    Opcion16 INTEGER DEFAULT (1),
    Opcion17 INTEGER DEFAULT (1),
    Opcion18 INTEGER DEFAULT (1),
    Opcion19 INTEGER DEFAULT (1),
    Opcion20 INTEGER DEFAULT (1),
    Opcion21 INTEGER DEFAULT (1),
    Opcion22 INTEGER DEFAULT (1),
    Opcion23 INTEGER DEFAULT (1),
    Opcion24 INTEGER DEFAULT (1) 
);

-- 36 Tabla de Empresas
CREATE TABLE IF NOT EXISTS Empresas (
    ID_Empresa INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
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
    ID_Usuarios INTEGER,
    FOREIGN KEY (ID_Usuarios)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 37 Tabla de Facturas
CREATE TABLE IF NOT EXISTS Facturas (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_usuario INTEGER,
    id_venta INTEGER,
    id_empleado INTEGER,
    timbrada INTEGER (1) DEFAULT (0),
    cancelada INTEGER (1) DEFAULT (0),
    fecha_certificacion DATETIME,
    UUID VARCHAR(100),
    rfc_pac VARCHAR(50),
    sello_sat TEXT,
    sello_cfd TEXT,
    metodo_pago VARCHAR (3),
    forma_pago VARCHAR (2),
    num_cuenta VARCHAR(100),
    moneda VARCHAR (3),
    tipo_cambio DOUBLE,
    folio VARCHAR(10),
    serie VARCHAR(10),
    tipo_comprobante VARCHAR (1),
    uso_cfdi VARCHAR (3),
    total DOUBLE DEFAULT (0),
    e_rfc VARCHAR (13),
    e_razon_social VARCHAR(100),
    e_regimen VARCHAR (3),
    e_correo VARCHAR(50),
    e_telefono VARCHAR (10),
    e_cp VARCHAR (5),
    e_estado VARCHAR(50),
    e_municipio VARCHAR(100),
    e_colonia VARCHAR(100),
    e_calle VARCHAR(100),
    e_num_ext VARCHAR(10),
    e_num_int VARCHAR(10),
    r_rfc VARCHAR (13),
    r_razon_social VARCHAR(100),
    r_nombre_comercial VARCHAR(100),
    r_correo VARCHAR(50),
    r_telefono VARCHAR(10),
    r_pais VARCHAR(200),
    r_estado VARCHAR(100),
    r_municipio VARCHAR(100),
    r_localidad VARCHAR(100),
    r_cp VARCHAR (10),
    r_colonia VARCHAR(100),
    r_calle VARCHAR(100),
    r_num_ext VARCHAR(10),
    r_num_int VARCHAR(10),
    con_complementos INTEGER (1) DEFAULT (0),
    fecha_hora_cpago DATETIME,
    monto_cpago DOUBLE,
    resta_cpago DOUBLE,
    f_enviada INTEGER (1) DEFAULT (0),
    id_emp_envia INTEGER (1) DEFAULT (0),
    id_emp_cancela INTEGER (1) DEFAULT (0),
    FOREIGN KEY (id_venta) REFERENCES Ventas (ID),
    FOREIGN KEY (id_usuario) REFERENCES Usuarios (ID) 
);

-- 38 Tabla de Facturas_complemento_pago
CREATE TABLE IF NOT EXISTS Facturas_complemento_pago (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_factura INTEGER NOT NULL,
    id_factura_principal INTEGER NOT NULL,
    uuid VARCHAR (38),
    moneda VARCHAR(10),
    tipo_cambio VARCHAR(10),
    metodo_pago VARCHAR(50),
    num_parcialidad INTEGER (2),
    saldo_anterior DOUBLE,
    importe_pagado DOUBLE,
    saldo_insoluto DOUBLE,
    timbrada INTEGER (1) DEFAULT (0),
    cancelada INTEGER (1) DEFAULT (0),
    FOREIGN KEY (id_factura)
    REFERENCES Facturas (ID) 
);

-- 39 Tabla de Facturas_productos
CREATE TABLE IF NOT EXISTS Facturas_productos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_factura INTEGER,
    clave_unidad VARCHAR (3),
    clave_producto VARCHAR (8),
    descripcion VARCHAR(100),
    cantidad VARCHAR(20),
    precio_u DOUBLE,
    base DOUBLE,
    tasa_cuota VARCHAR(20),
    importe_iva DOUBLE,
    descuento TEXT,
    FOREIGN KEY (id_factura)
    REFERENCES Facturas (ID) 
);

-- 40 Tabla de Facturas_impuestos
CREATE TABLE IF NOT EXISTS Facturas_impuestos (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    id_factura_producto INTEGER NOT NULL,
    tipo VARCHAR (10),
    impuesto VARCHAR (4),
    tipo_factor VARCHAR (6),
    tasa_cuota VARCHAR(20),
    definir VARCHAR(20),
    importe DOUBLE,
    FOREIGN KEY (id_factura_producto)
    REFERENCES Facturas_productos (id_factura) 
);

-- 41 Tabla de FiltroDinamico
CREATE TABLE IF NOT EXISTS FiltroDinamico (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    concepto TEXT,
    checkBoxConcepto INTEGER NOT NULL DEFAULT (0),
    textCantidad TEXT,
    IDUsuario INTEGER NOT NULL,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 42 Tabla de FiltroProducto
CREATE TABLE IF NOT EXISTS FiltroProducto (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    concepto TEXT,
    checkBoxConcepto INTEGER NOT NULL DEFAULT (0),
    textComboBoxConcepto TEXT,
    textCantidad TEXT DEFAULT (0),
    IDUsuario INTEGER NOT NULL,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 43 Tabla de FiltrosDinamicosVetanaFiltros
CREATE TABLE IF NOT EXISTS FiltrosDinamicosVetanaFiltros (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    checkBoxValue INTEGER NOT NULL DEFAULT (0),
    concepto      TEXT,
    strFiltro     TEXT,
    IDUsuario     INTEGER NOT NULL,
    FOREIGN KEY (IDUsuario)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 44 Tabla de RegimenFiscal
CREATE TABLE IF NOT EXISTS RegimenFiscal (
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    CodigoRegimen INTEGER NOT NULL,
    Descripcion TEXT NOT NULL,
    AplicaFisica TEXT NOT NULL,
    AplicaMoral TEXT NOT NULL,
    InicioVigencia DATE,
    FinVigencia DATE
);

-- 45 Tabla de RegimenDeUsuarios
CREATE TABLE IF NOT EXISTS RegimenDeUsuarios (
    Usuario_ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
    Regimen_ID INTEGER NOT NULL,
    FOREIGN KEY (Usuario_ID)
    REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE,
        FOREIGN KEY (Regimen_ID)
    REFERENCES RegimenFiscal (ID) ON UPDATE CASCADE ON DELETE CASCADE
);

-- 46 Tabla de RevisarInventario
CREATE TABLE IF NOT EXISTS RevisarInventario (
    ID INTEGER  PRIMARY KEY AUTO_INCREMENT NOT NULL,
    IDAlmacen          TEXT     NOT NULL,
    Nombre             TEXT     NOT NULL,
    ClaveInterna       TEXT,
    CodigoBarras       TEXT,
    StockAlmacen       DECIMAL (16, 2) DEFAULT (0),
    StockFisico        DECIMAL (16, 2) DEFAULT (0),
    NoRevision         INT      DEFAULT (0),
    Fecha              DATETIME,
    Vendido            INT      DEFAULT (0),
    Diferencia         INT      DEFAULT (0),
    IDUsuario          INTEGER,
    Tipo               TEXT,
    StatusRevision     INT      DEFAULT (0),
    StatusInventariado INT      DEFAULT (0),
    PrecioProducto     DECIMAL (16, 2) DEFAULT (0),
    IDComputadora      TEXT,
    FOREIGN KEY (IDUsuario)
    REFERENCES USuarios (ID) 
);

-- 47 Tabla de TipoClientes
CREATE TABLE IF NOT EXISTS TipoClientes (
    ID                  INTEGER  PRIMARY KEY AUTO_INCREMENT,
    IDUsuario           INTEGER  NOT NULL DEFAULT (0),
    Nombre              TEXT     NOT NULL,
    DescuentoPorcentaje DECIMAL (16, 2) DEFAULT (0),
    Habilitar           INTEGER  DEFAULT (1),
    FechaOperacion      DATETIME
);

/* 48 Tabla de Ubicaciones
//tables.Add(@"CREATE TABLE IF NOT EXISTS Ubicaciones (
//                ID INTEGER PRIMARY KEY AUTO_INCREMENT,
//                IDUsuario INTEGER NOT NULL,
//                Descripcion TEXT
//            );");*/

/* 49 Tabla de Devoluciones
CREATE TABLE IF NOT EXISTS Devoluciones(
    ID INTEGER PRIMARY KEY AUTO_INCREMENT,
    IDVenta INTEGER  NOT NULL,
    IDUsuario INTEGER,
    Total DECIMAL (16, 2) DEFAULT (0),
    Efectivo DECIMAL (16, 2) DEFAULT (0),
    Tarjeta DECIMAL (16, 2) DEFAULT (0),
    Vales DECIMAL (16, 2) DEFAULT (0),
    Cheque DECIMAL (16, 2) DEFAULT (0),
    Transferencia DECIMAL (16, 2) DEFAULT (0),
    Referencia TEXT,
    FechaOperacion DATETIME NOT NULL
);