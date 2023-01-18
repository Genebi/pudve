-- ------------------------------------------
-- -- Inicia sección de Tablas del sistema --
-- ------------------------------------------

-- 01 Tabla de usuarios
CREATE TABLE 
IF 
	NOT EXISTS Usuarios (
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
CREATE TABLE 
IF 
	NOT EXISTS Productos (
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
CREATE TABLE 
IF 
	NOT EXISTS ProductoRelacionadoXML (
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
CREATE TABLE 
IF 
	NOT EXISTS HistorialCompras (
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
CREATE TABLE 
IF 
	NOT EXISTS HistorialModificacionRecordProduct (
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
CREATE TABLE 
IF 
	NOT EXISTS HistorialPrecios (
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
CREATE TABLE 
IF 
	NOT EXISTS MensajesInventario (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDUsuario INTEGER NOT NULL,
		IDProducto INTEGER NOT NULL,
		Mensaje TEXT,
		Activo INTEGER DEFAULT (0) 
	);

-- 08 Tabla de ProductMessage
CREATE TABLE 
IF 
	NOT EXISTS ProductMessage (
		ID INTEGER NOT NULL PRIMARY KEY AUTO_INCREMENT UNIQUE,
		IDProducto INTEGER,
		ProductOfMessage TEXT,
		ProductMessageActivated BOOLEAN DEFAULT (0),
		FOREIGN KEY (IDProducto)
		REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 09 Tabla de ProductosDeServicios
CREATE TABLE 
IF 
	NOT EXISTS ProductosDeServicios (
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
CREATE TABLE 
IF 
	NOT EXISTS Clientes (
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
CREATE TABLE 
IF 
	NOT EXISTS Empleados (
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
CREATE TABLE 
IF 
	NOT EXISTS Ventas (
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
CREATE TABLE 
IF 
	NOT EXISTS ProductosVenta (
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
CREATE TABLE 
IF 
	NOT EXISTS Proveedores (
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
CREATE TABLE 
IF 
	NOT EXISTS DetallesProducto (
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
CREATE TABLE 
IF 
	NOT EXISTS DetalleGeneral (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDUsuario INTEGER NOT NULL,
		ChckName TEXT NOT NULL,
		Descripcion TEXT NOT NULL,
		FOREIGN KEY (IDUsuario)
		REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 17 Tabla de DetallesProductoGenerales
CREATE TABLE 
IF 
	NOT EXISTS DetallesProductoGenerales (
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
CREATE TABLE 
IF 
	NOT EXISTS DetallesVenta (
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
CREATE TABLE 
IF 
	NOT EXISTS Abonos (
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
CREATE TABLE 
IF 
	NOT EXISTS Anticipos (
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
CREATE TABLE 
IF 
	NOT EXISTS appSettings (
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
CREATE TABLE 
IF 
	NOT EXISTS Caja (
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
CREATE TABLE 
IF 
	NOT EXISTS Catalogo_claves_producto (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		clave TEXT,
		descripcion TEXT
	);

-- 24 Tabla de Catalogo_monedas
CREATE TABLE 
IF 
	NOT EXISTS Catalogo_monedas (
		clave_moneda VARCHAR(50) NOT NULL,
		descripcion    TEXT,
		cant_decimales INTEGER
	);

-- 25 Tabla de CatalogoUnidadesMedida
CREATE TABLE 
IF 
	NOT EXISTS CatalogoUnidadesMedida (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		ClaveUnidad TEXT,
		Nombre      TEXT
	);

-- 26 Tabla de CodigoBarrasExtras
CREATE TABLE 
IF 
	NOT EXISTS CodigoBarrasExtras (
		IDCodBarrExt INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		CodigoBarraExtra TEXT,
		IDProducto INTEGER,
		FOREIGN KEY (IDProducto)
		REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 27 Tabla de CodigoBarrasGenerado
CREATE TABLE 
IF 
	NOT EXISTS CodigoBarrasGenerado (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDUsuario INTEGER,
		CodigoBarras TEXT,
		FechaInventario TEXT,
		NoRevision INTEGER DEFAULT (1) 
	);

-- 28 Tabla de ConceptosDinamicos
CREATE TABLE 
IF 
	NOT EXISTS ConceptosDinamicos (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDUsuario INTEGER  NOT NULL,
		IDEmpleado INTEGER  DEFAULT (0),
		Concepto TEXT,
		Origen TEXT,
		Status INTEGER  DEFAULT (1),
		FechaOperacion DATETIME
	);

-- 29 Tabla de Configuracion
CREATE TABLE 
IF 
	NOT EXISTS Configuracion (
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

-- 30 Tabla de CorreosProducto
CREATE TABLE 
IF 
	NOT EXISTS CorreosProducto (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		IDUsuario INTEGER NOT NULL,
		IDEmpleado INTEGER DEFAULT (0),
		IDProducto INTEGER NOT NULL,
		CorreoPrecioProducto INTEGER DEFAULT (0),
		CorreoStockProducto INTEGER DEFAULT (0),
		CorreoStockMinimo INTEGER DEFAULT (0),
		CorreoVentaProducto INTEGER DEFAULT (0) 
	);

-- 31 Tabla de DescuentoCliente
CREATE TABLE 
IF 
	NOT EXISTS DescuentoCliente (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		PrecioProducto DECIMAL (16, 2) DEFAULT (0),
		PorcentajeDescuento DECIMAL (16, 2) DEFAULT (0),
		PrecioDescuento DECIMAL (16, 2) DEFAULT (0),
		Descuento DECIMAL (16, 2) DEFAULT (0),
		IDProducto INTEGER,
		FOREIGN KEY (IDProducto)
		REFERENCES Productos (ID) 
	);

-- 32 Tabla de DescuentoMayoreo
CREATE TABLE 
IF 
	NOT EXISTS DescuentoMayoreo (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		RangoInicial TEXT,
		RangoFinal TEXT,
		Precio DECIMAL (16, 2) DEFAULT (0),
		Checkbox INTEGER,
		IDProducto INTEGER,
		FOREIGN KEY (IDProducto)
		REFERENCES Productos (ID) 
	);

-- 33 Tabla de DetallesFacturacionProductos

CREATE TABLE 
IF 
	NOT EXISTS DetallesFacturacionProductos (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		Tipo TEXT,
		Impuesto TEXT,
		TipoFactor TEXT,
		TasaCuota TEXT,
		Definir DECIMAL (16, 2) DEFAULT (0),
		Importe DECIMAL (16, 2) DEFAULT (0),
		IDProducto INTEGER,
		FOREIGN KEY (IDProducto)
		REFERENCES Productos (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 34 Tabla de EmpleadosPermisos
CREATE TABLE 
IF 
	NOT EXISTS EmpleadosPermisos (
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

-- 35 Tabla de Empresas
CREATE TABLE 
IF 
	NOT EXISTS Empresas (
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

-- 36 Tabla de Facturas
CREATE TABLE 
IF 
	NOT EXISTS Facturas (
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
		e_razon_social VARCHAR(300),
		e_regimen VARCHAR (40),
		e_correo VARCHAR(100),
		e_telefono VARCHAR (10),
		e_cp VARCHAR (5),
		e_estado VARCHAR(50),
		e_municipio VARCHAR(100),
		e_colonia VARCHAR(100),
		e_calle VARCHAR(100),
		e_num_ext VARCHAR(50),
		e_num_int VARCHAR(50),
		r_rfc VARCHAR (13),
		r_razon_social VARCHAR(300),
		r_nombre_comercial VARCHAR(200),
		r_correo VARCHAR(100),
		r_telefono VARCHAR(10),
		r_pais VARCHAR(200),
		r_estado VARCHAR(100),
		r_municipio VARCHAR(100),
		r_localidad VARCHAR(100),
		r_cp VARCHAR (10),
		r_colonia VARCHAR(100),
		r_calle VARCHAR(100),
		r_num_ext VARCHAR(50),
		r_num_int VARCHAR(50),
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

-- 37 Tabla de Facturas_complemento_pago
CREATE TABLE 
IF 
	NOT EXISTS Facturas_complemento_pago (
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

-- 38 Tabla de Facturas_productos
CREATE TABLE 
IF 
	NOT EXISTS Facturas_productos (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		id_factura INTEGER,
		clave_unidad VARCHAR (3),
		clave_producto VARCHAR (8),
		descripcion VARCHAR(200),
		cantidad VARCHAR(20),
		precio_u DOUBLE,
		base DOUBLE,
		tasa_cuota VARCHAR(20),
		importe_iva DOUBLE,
		descuento TEXT,
		FOREIGN KEY (id_factura)
		REFERENCES Facturas (ID) 
	);

-- 39 Tabla de Facturas_impuestos
CREATE TABLE 
IF 
	NOT EXISTS Facturas_impuestos (
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

-- 40 Tabla de FiltroDinamico
CREATE TABLE 
IF 
	NOT EXISTS FiltroDinamico (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		concepto TEXT,
		checkBoxConcepto INTEGER NOT NULL DEFAULT (0),
		textCantidad TEXT,
		IDUsuario INTEGER NOT NULL,
		FOREIGN KEY (IDUsuario)
		REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 41 Tabla de FiltroProducto
CREATE TABLE 
IF 
	NOT EXISTS FiltroProducto (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		concepto TEXT,
		checkBoxConcepto INTEGER NOT NULL DEFAULT (0),
		textComboBoxConcepto TEXT,
		textCantidad TEXT DEFAULT (0),
		IDUsuario INTEGER NOT NULL,
		FOREIGN KEY (IDUsuario)
		REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 42 Tabla de FiltrosDinamicosVetanaFiltros
CREATE TABLE 
IF 
	NOT EXISTS FiltrosDinamicosVetanaFiltros (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		checkBoxValue INTEGER NOT NULL DEFAULT (0),
		concepto      TEXT,
		strFiltro     TEXT,
		IDUsuario     INTEGER NOT NULL,
		FOREIGN KEY (IDUsuario)
		REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 43 Tabla de RegimenFiscal
CREATE TABLE 
IF 
	NOT EXISTS RegimenFiscal (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		CodigoRegimen INTEGER NOT NULL,
		Descripcion TEXT NOT NULL,
		AplicaFisica TEXT NOT NULL,
		AplicaMoral TEXT NOT NULL,
		InicioVigencia DATE,
		FinVigencia DATE
	);

-- 44 Tabla de RegimenDeUsuarios
CREATE TABLE 
IF 
	NOT EXISTS RegimenDeUsuarios (
		Usuario_ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		Regimen_ID INTEGER NOT NULL,
		FOREIGN KEY (Usuario_ID)
		REFERENCES Usuarios (ID) ON UPDATE CASCADE ON DELETE CASCADE,
			FOREIGN KEY (Regimen_ID)
		REFERENCES RegimenFiscal (ID) ON UPDATE CASCADE ON DELETE CASCADE
	);

-- 45 Tabla de RevisarInventario
CREATE TABLE 
IF 
	NOT EXISTS RevisarInventario (
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

	-- 45 Tabla de RevisarInventarioReportes
CREATE TABLE 
IF 
	NOT EXISTS RevisarInventarioReportes (
		ID INTEGER  PRIMARY KEY AUTO_INCREMENT NOT NULL,
		NameUsr            TEXT     NOT NULL, 
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

-- 46 Tabla de TipoClientes
CREATE TABLE 
IF 
	NOT EXISTS TipoClientes (
		ID                  INTEGER  PRIMARY KEY AUTO_INCREMENT,
		IDUsuario           INTEGER  NOT NULL DEFAULT (0),
		Nombre              TEXT     NOT NULL,
		DescuentoPorcentaje DECIMAL (16, 2) DEFAULT (0),
		Habilitar           INTEGER  DEFAULT (1),
		FechaOperacion      DATETIME
	);

-- 47 Tabla de Devoluciones
CREATE TABLE 
IF 
	NOT EXISTS Devoluciones(
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

-- 48 Tabla de DGVAumentarInventario
CREATE TABLE 
IF 
	NOT EXISTS DGVAumentarInventario(
		id	INTEGER	PRIMARY KEY	AUTO_INCREMENT,
		IdProducto	VARCHAR(10)	NOT NULL	DEFAULT(0),
		NombreProducto	VARCHAR(200)	NOT NULL,
		StockActual	VARCHAR(10)	NOT NULL	DEFAULT(0),
		DiferenciaUnidades	VARCHAR(10)	NOT NULL	DEFAULT(0),
		NuevoStock	VARCHAR(10)	NOT NULL	DEFAULT(0),
		Precio	VARCHAR(10)	NOT NULL	DEFAULT(0),
		Clave	VARCHAR(20)	NOT NULL	DEFAULT(0),
		Codigo	VARCHAR(20)	NOT NULL	DEFAULT(0),
		Fecha	VARCHAR(20)	NOT NULL,
		NoRevision	VARCHAR(10)	NOT NULL	DEFAULT(0),
		StatusActualizacion	VARCHAR(10)	NOT NULL	DEFAULT(0)
	);

-- 49 Tabla de NoRevisionAumentarInventario
CREATE TABLE 
IF 
	NOT EXISTS NoRevisionAumentarInventario(
		id INTEGER	PRIMARY KEY	AUTO_INCREMENT,
		NoRevisionAumentarInventario INTEGER NOT NULL DEFAULT(0)
	);

-- 50 Tabla de DGVDisminuirInventario
CREATE TABLE 
IF 
	NOT EXISTS DGVDisminuirInventario(
		id	INTEGER	PRIMARY KEY	AUTO_INCREMENT,
		IdProducto	VARCHAR(10)	NOT NULL	DEFAULT(0),
		NombreProducto	VARCHAR(200)	NOT NULL,
		StockActual	VARCHAR(10)	NOT NULL	DEFAULT(0),
		DiferenciaUnidades	VARCHAR(10)	NOT NULL	DEFAULT(0),
		NuevoStock	VARCHAR(10)	NOT NULL	DEFAULT(0),
		Precio	VARCHAR(10)	NOT NULL	DEFAULT(0),
		Clave	VARCHAR(20)	NOT NULL	DEFAULT(0),
		Codigo	VARCHAR(20)	NOT NULL	DEFAULT(0),
		Fecha	VARCHAR(20)	NOT NULL,
		NoRevision	VARCHAR(10)	NOT NULL	DEFAULT(0),
		StatusActualizacion	VARCHAR(10)	NOT NULL	DEFAULT(0)
	);

-- 51 Tabla de NoRevisionDisminuirInventario
CREATE TABLE 
IF 
	NOT EXISTS NoRevisionDisminuirInventario(
		id INTEGER	PRIMARY KEY	AUTO_INCREMENT,
		NoRevisionDisminuirInventario INTEGER NOT NULL DEFAULT(0)
	);

-- 52 Tabla de AppVersionRecord
CREATE TABLE
IF
	NOT EXISTS AppVersionRecord (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		AppName VARCHAR ( 200 ) NULL DEFAULT ( '' ),
		AppVersion VARCHAR ( 90 ) NOT NULL DEFAULT ( '0.0.0.0' ),
		AppMajorVersion VARCHAR ( 20 ) NOT NULL DEFAULT ( '0' ),
		AppMinorVersion VARCHAR ( 20 ) NOT NULL DEFAULT ( '0' ),
		AppBuildNumber VARCHAR ( 20 ) NOT NULL DEFAULT ( '0' ),
		AppRevision VARCHAR ( 20 ) NOT NULL DEFAULT ( '0' ),
		AppDateVersion DATE 
	);

-- 53 Tabla de Basculas
CREATE TABLE
IF
	NOT EXISTS basculas (
		idBascula INT ( 11 ) NOT NULL AUTO_INCREMENT,
		nombreBascula VARCHAR ( 255 ) NOT NULL,
		puerto VARCHAR ( 100 ) NOT NULL,
		baudRate VARCHAR ( 100 ) NOT NULL,
		dataBits VARCHAR ( 50 ) NOT NULL,
		handshake VARCHAR ( 100 ) NOT NULL,
		parity VARCHAR ( 100 ) NOT NULL,
		stopBits VARCHAR ( 100 ) NOT NULL,
		sendData VARCHAR ( 100 ) NOT NULL,
		idUsuario INT ( 11 ) NOT NULL,
		predeterminada INT ( 11 ) NOT NULL DEFAULT 0,
		PRIMARY KEY ( idBascula ),
		UNIQUE ( nombreBascula, idUsuario, predeterminada ),
	CONSTRAINT Basculas_IdUsuario FOREIGN KEY ( idUsuario ) REFERENCES usuarios ( ID ) ON DELETE CASCADE ON UPDATE CASCADE 
	);


-- ------------------------------------------
-- -- Final sección de Tablas del sistema --
-- ------------------------------------------


-- -----------------------------------------------------------
-- -- Inicio sección de Index, Unique Index, y Foreign Key  --
-- -----------------------------------------------------------

-- Desactivamos Claves Primarias
SET FOREIGN_KEY_CHECKS = 0;

-- ---------------------------
-- Creación de Index de Tablas
-- ---------------------------

-- Index Unico de Usuarios
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_Usuarios ON Usuarios (ID);

-- Index y FullText Productos
CREATE FULLTEXT INDEX 
IF 
	NOT EXISTS BUSQUEDA_TEXTO_CAMPOS_NOMBRES_PRODUCTOS ON Productos (Nombre, NombreAlterno1, NombreAlterno2);

-- Index Unico de Prveedores
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_Proveedores ON Proveedores (ID);

CREATE FULLTEXT INDEX 
IF 
	NOT EXISTS Nombre_Proveedores ON Proveedores (Nombre);

-- Index Unico de DetallesProducto
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_DetallesProducto ON DetallesProducto (ID);

-- Index Unico de DetallesProductoGenerales
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_DetallesProductoGenerales ON DetallesProductoGenerales (ID);

-- Index Ventas
CREATE INDEX 
IF 
	NOT EXISTS idx_VentasGenerales ON Ventas(IDUsuario, Status, FechaOperacion);

-- Index Unico de DetallsVenta
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_DetallesVenta ON DetallesVenta (ID);

-- Index Unico de Abonos
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_Abonos ON Abonos (ID);

-- Index Unico de Anticipos
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_Anticipos ON Anticipos (ID);

-- Index Unico de CodigoBarrasGenerado
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_CodigoBarrasGenerado ON CodigoBarrasGenerado (ID);

-- Index Unico de ConceptosDinamicos
CREATE UNIQUE INDEX 
IF 
	NOT EXISTS ID_Unico_ConceptosDinamicos ON ConceptosDinamicos (ID);

------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Index para reportes de clientes con productos
--CREATE UNIQUE INDEX IF NOT EXISTS BUSQUEDA_REPORTE_CLIENTES_PRODUCTOS_NO_COMPRADOS ON Productos (Nombre, Precio, Precio); 

-- Index de Caja
CREATE INDEX 
IF 
	NOT EXISTS IN_FechaOperacion_Caja ON Caja (Operacion); 
CREATE INDEX 
IF 
	NOT EXISTS INDEX_FechaOperacion_Caja ON Caja (FechaOperacion);
CREATE INDEX 
IF 
	NOT EXISTS INDEX_sum_Total_Caja ON Caja (Cantidad);
CREATE INDEX 
IF 
	NOT EXISTS INDEX_sum_Efectivo_Caja ON Caja (Efectivo);
CREATE INDEX 
IF 
	NOT EXISTS INDEX_sum_Tarjeta_Caja ON Caja (Tarjeta);
CREATE INDEX 
IF 
	NOT EXISTS INDEX_sum_Vales_Caja ON Caja (Vales);
CREATE INDEX 
IF 
	NOT EXISTS INDEX_sum_Cheque_Caja ON Caja (Cheque);
CREATE INDEX 
IF 
	NOT EXISTS INDEX_sum_Trans_Caja ON Caja (Transferencia);

-- Index de DGVAumentarInventario
CREATE INDEX 
IF 
	NOT EXISTS SEARCH_CHECKNUMBER_STATUS_AumentarInventario ON DGVAumentarInventario (NoRevision, StatusActualizacion);

-- Index de DGVDisminuirInventario
CREATE INDEX 
IF 
	NOT EXISTS SEARCH_CHECKNUMBER_STATUS_DisminuirInventario ON DGVDisminuirInventario (NoRevision, StatusActualizacion);

-- Index Unico de AppVersionRecord
CREATE UNIQUE INDEX
IF
	NOT EXISTS Num_Version_App ON AppVersionRecord ( AppVersion, AppMajorVersion, AppMinorVersion, AppBuildNumber, AppRevision );

-- Index de Asignar Precio en Productos
CREATE INDEX 
IF
	NOT EXISTS INSERT_NEW_PRICE ON HistorialPrecios (IDEmpleado);
	CREATE INDEX 
IF
	NOT EXISTS INSERT_NEW_PRICE ON HistorialPrecios (PrecioAnterior);
	CREATE INDEX 
IF
	NOT EXISTS INSERT_NEW_PRICE ON HistorialPrecios (PrecioNuevo);
	CREATE INDEX 
IF
	NOT EXISTS INSERT_NEW_PRICE ON HistorialPrecios (Origen);

-- Agregari indice para no duplicar consulta de la tabla catalogo_monedas
ALTER IGNORE TABLE catalogo_monedas ADD UNIQUE INDEX IF NOT EXISTS (clave_moneda, descripcion, cant_decimales);

-- -------------------------------------
-- Creación de Claves Foraneas de tablas
-- -------------------------------------

-- HistorialPrecios 
ALTER TABLE HistorialPrecios ADD CONSTRAINT FK_HistorialPrecios_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE HistorialPrecios ADD CONSTRAINT FK_HistorialPrecios_IDProducto FOREIGN KEY 
IF 
	NOT EXISTS (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- MensajesInventario 
ALTER TABLE MensajesInventario ADD CONSTRAINT FK_MensajesInventario_IDProducto FOREIGN KEY 
IF 
	NOT EXISTS (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE MensajesInventario ADD CONSTRAINT FK_MensajesInventario_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- Proveedores 
ALTER TABLE Proveedores ADD CONSTRAINT FK_Proveedores_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- DetallesProducto 
ALTER TABLE DetallesProducto ADD CONSTRAINT FK_DetallesProducto_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE DetallesProducto ADD CONSTRAINT FK_DetallesProducto_IDProducto FOREIGN KEY 
IF 
	NOT EXISTS (IDProducto) REFERENCES Productos (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- DetallesVenta 
ALTER TABLE DetallesVenta ADD CONSTRAINT FK_DetallesVenta_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE DetallesVenta ADD CONSTRAINT FK_DetallesVenta_IDVenta FOREIGN KEY 
IF 
	NOT EXISTS (IDVenta) REFERENCES Ventas (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- Abonos 
ALTER TABLE Abonos ADD CONSTRAINT FK_Abonos_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

ALTER TABLE Abonos ADD CONSTRAINT FK_Abonos_IDVenta FOREIGN KEY 
IF 
	NOT EXISTS (IDVenta) REFERENCES Ventas (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- ConceptosDinamicos
ALTER TABLE ConceptosDinamicos ADD CONSTRAINT FK_ConceptosDinamicos_IDUsuario FOREIGN KEY 
IF 
	NOT EXISTS (IDUsuario) REFERENCES Usuarios (ID) ON DELETE CASCADE ON UPDATE CASCADE;

-- Desactivamos Claves Primarias
SET FOREIGN_KEY_CHECKS = 1;

-- -----------------------------------------------------------
-- -- Final sección de Index, Unique Index, y Foreign Key  --
-- -----------------------------------------------------------

-- Agregar Columna (CorreoAgregarDineroCaja) a la tabla de Configuracion si es que no tiene dicha columna 
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoAgregarDineroCaja tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoAgregarDineroCaja) a la tabla de Configuracion si es que no tiene dicha columna 
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoRetiroDineroCaja tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoCerrarVentanaVentas) a la tabla de Configuracion si es que no tiene dicha columna
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoCerrarVentanaVentas tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoRestarProductoVentas) a la tabla de Configuracion si es que no tiene dicha columna
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoRestarProductoVentas tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoEliminarProductoVentas) a la tabla de Configuracion si es que no tiene dicha columna
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoEliminarProductoVentas tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoEliminarUltimoProductoAgregadoVentas) a la tabla de Configuracion si es que no tiene dicha columna
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoEliminarUltimoProductoAgregadoVentas tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoEliminarListaProductoVentas) a la tabla de Configuracion si s que no tiene dicha columna
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoEliminarListaProductoVentas tinyint(1) DEFAULT 0;

-- Agregar Columna () a la tabla de Configuracion si es que no tiene dicha columna
ALTER TABLE configuracion ADD COLUMN 
IF 
	NOT EXISTS CorreoCorteDeCaja tinyint(1) DEFAULT 0;

-- Agregar Columna (CorreoRespaldo) a la tabla configuracion 
ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS CorreoRespaldo tinyint(1) DEFAULT 0;

-- Agregar Columna (HabilitarTicketVentas) a la tabla configuracion
ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS HabilitarTicketVentas tinyint(1) DEFAULT 1;

-- Editar el tipo de dato de la Columna FechaHoy en la tabla de usuarios
ALTER TABLE usuarios MODIFY COLUMN FechaHoy DateTime;

-- Agregar Columna (NombreEmisor) a la tabla DGVAumentarInventario si es que no tiene dicha columna
ALTER TABLE DGVAumentarInventario ADD COLUMN 
IF 
	NOT EXISTS NombreEmisor VARCHAR(150);

-- Agregar Columna (Comentarios) a la tabla DGVAumentarInventario si es que no tiene dicha columna 
ALTER TABLE DGVAumentarInventario ADD COLUMN 
IF 
	NOT EXISTS Comentarios VARCHAR(150);

-- Agregar Columna (ValorUnitario) a la tabla DGVAumentarInventario si es que no tiene dicha columna
ALTER TABLE DGVAumentarInventario ADD COLUMN 
IF 
	NOT EXISTS ValorUnitario VARCHAR(100) DEFAULT ('0');

-- Modificar la Columna de Fecha a DateTime en la tabla de DGVAumentarInventario
ALTER TABLE DGVAumentarInventario MODIFY Fecha datetime;

-- Agregar Columna (NombreEmisor) a la tabla DGVDisminuirInventario si es que no tiene dicha columna
ALTER TABLE DGVDisminuirInventario ADD COLUMN 
IF 
	NOT EXISTS NombreEmisor VARCHAR(150);

-- Agregar Columna (Comntarios) a la tabla DGVDisminuirInventario si es que no tiene dicha columna
ALTER TABLE DGVDisminuirInventario ADD COLUMN 
IF 
	NOT EXISTS Comentarios VARCHAR(150);

-- Agregar Columna (ValorUnitario) a la tabla DGVDisminuirInventario si es que no tiene dicha columna
ALTER TABLE DGVDisminuirInventario ADD COLUMN 
IF 
	NOT EXISTS ValorUnitario VARCHAR(100) DEFAULT ('0');

-- Modificar la Columna de Fecha a DateTime en la tabla de DGVDisminuirInventario
ALTER TABLE DGVDisminuirInventario MODIFY Fecha datetime;

-- Agregar Columna (IdUsuario) a la tabla DGVAumentarInventario si es que no tiene dicha columna
ALTER TABLE DGVAumentarInventario ADD COLUMN 
IF 
	NOT EXISTS IdUsuario VARCHAR(20);

-- Agregar Columna (IdUsuario) a la tabla DGVDisminuirInventario si es que no tiene dicha columna
ALTER TABLE DGVDisminuirInventario ADD COLUMN 
IF 
	NOT EXISTS IdUsuario VARCHAR(20);

-- Agregar Columna (IdUsuario) a la tabla NoRevisionAumentarInventario si es que no tiene dicha columna
ALTER TABLE NoRevisionAumentarInventario ADD COLUMN 
IF 
	NOT EXISTS IdUsuario VARCHAR(20);

-- Agregar Columna (IdUsuario) a la tabla NoRevisionDisminuirInventario si es que no tiene dicha columna
ALTER TABLE NoRevisionDisminuirInventario ADD COLUMN 
IF 
	NOT EXISTS IdUsuario VARCHAR(20);

-- Agregar Columna (SinClaveInterna) a la tabla de usuarios para solo dejar clave interna a usuarios que ya existian
ALTER TABLE Usuarios ADD COLUMN 
IF 
	NOT EXISTS SinClaveInterna INT DEFAULT (1);

 -- Agregar Columna (CantidadPedir) a la tabla de Productos para hacer las revisiones de inventario
 ALTER TABLE Productos ADD COLUMN IF NOT EXISTS CantidadPedir DECIMAL (16, 2) DEFAULT (0.00); 

 -- Agregar columna (empleado) a la tabla de Caja para los reportes de caja
 ALTER TABLE Caja ADD COLUMN IF NOT EXISTS IdEmpleado int DEFAULT (0);
 
 -- Agregar Columna (IDEmpleado) a la tabla de RevisarInventarioReportes
 ALTER TABLE revisarInventarioReportes ADD COLUMN IF NOT EXISTS IDEmpleado INTEGER(11) DEFAULT (0);

 -- Agregar Columna (NumFolio) a la tabla de RevisarInventarioReportes
 ALTER TABLE revisarinventarioreportes ADD COLUMN IF NOT EXISTS NumFolio INTEGER(11) DEFAULT(0);

 -- Agregar Columna (NumFolio) a la tabla de Caja
 ALTER TABLE Caja ADD COLUMN IF NOT EXISTS NumFolio INTEGER(11) DEFAULT(0);

 -- Agregar Columna (CantidadRetiradaCorte) a la tabla de Caja
 ALTER TABLE Caja ADD COLUMN IF NOT EXISTS CantidadRetiradaCorte DECIMAL (16, 2) DEFAULT (0.00);

 -- Agregar Columna (IDEmpleado) a la tabla dgvaumentarinventario 
 ALTER TABLE dgvaumentarinventario ADD COLUMN IF NOT EXISTS IDEmpleado INT DEFAULT (0);

 -- Agregar Columna (IDEmpleado) a la tabla dgvdisminuirinventario
 ALTER TABLE dgvdisminuirinventario ADD COLUMN IF NOT EXISTS IDEmpleado INT DEFAULT (0);

  -- Agregar Columna (IDEmpleado) a la tabla dgvaumentarinventario 
 ALTER TABLE dgvaumentarinventario ADD COLUMN IF NOT EXISTS NameUsr VARCHAR(40);

 -- Agregar Columna (IDEmpleado) a la tabla dgvdisminuirinventario
 ALTER TABLE dgvdisminuirinventario ADD COLUMN IF NOT EXISTS NameUse VARCHAR(40);

   -- Agregar Columna (IDEmpleado) a la tabla dgvaumentarinventario 
 ALTER TABLE dgvaumentarinventario ADD COLUMN IF NOT EXISTS Folio INTEGER(11) DEFAULT (0);

 -- Agregar Columna (IDEmpleado) a la tabla dgvdisminuirinventario
 ALTER TABLE dgvdisminuirinventario ADD COLUMN IF NOT EXISTS Folio INTEGER(11) DEFAULT (0);

 -- 
 --

--
--

-- Borrado de CONSTRAINT 
ALTER TABLE productosdeservicios DROP CONSTRAINT
IF
	EXISTS productosdeservicios_ibfk_1;

-- Borrado de KEY
ALTER TABLE productosdeservicios DROP KEY
IF
	EXISTS IDProducto;

-- Agregar Columna (CorreoVenta) a la tabla de Configuracion para enviar correo al hacer una venta
ALTER TABLE Configuracion ADD COLUMN IF NOT EXISTS CorreoVenta tinyint(1) DEFAULT 0;

-- Agregar mensaje en Nombre si el prducto se guardo sin nombre en la tabla producto
UPDATE productos 
SET Nombre = 'PRODUCTO SIN NOMBRE', 
NombreAlterno1 = 'PRODUCTO SIN NOMBRE', 
NombreAlterno2 = 'PRODUCTO SIN NOMBRE' 
WHERE 
	Nombre = '' 
	AND NombreAlterno1 = '' 
	AND NombreAlterno2 = '' 
	AND NombreAlterno2 = ''; 

-- Quitar espacios creados al inicio y final de un producto sea con la tecla de control TAB
UPDATE productos 
SET Nombre = REPLACE ( Nombre, '\t', '' );

UPDATE productos 
SET NombreAlterno1 = REPLACE ( NombreAlterno1, '\t', '' );

UPDATE productos 
SET NombreAlterno2 = REPLACE ( NombreAlterno2, '\t', '' );

-- Quitar espacios creados por algun enter en el nombre del producto
UPDATE productos 
SET Nombre = REPLACE ( Nombre, '\n', '' );

UPDATE productos 
SET NombreAlterno1 = REPLACE ( NombreAlterno1, '\n', '' );

UPDATE productos 
SET NombreAlterno2 = REPLACE ( NombreAlterno2, '\n', '' );

-- Agregar Columna (CorreoIniciarSesion) a la tabla de Configuracion para enviar correo al iniciar sesion en el programa
ALTER TABLE Configuracion ADD COLUMN IF NOT EXISTS CorreoIniciarSesion tinyint(1) DEFAULT 0;

-- Agregar Columna (Mostrar) a la tabla de AppSettings para mostrar en Ventana Detalle Productos
ALTER TABLE AppSettings ADD COLUMN IF NOT EXISTS Mostrar tinyint(1) DEFAULT 1;

-- Agregar Columna (CorreoVentaDescuento) a la tabla de Configuracion para enviar correo al hacer venta con descuento
ALTER TABLE Configuracion ADD COLUMN IF NOT EXISTS CorreoVentaDescuento tinyint(1) DEFAULT 0;

--Agregar Columna (ConteoDeIniciosDeSesion) a la tabla usuarios para llevar un registro de cuantas veces se inicia sesion el cliente
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS ConteoInicioDeSesion int DEFAULT 0;

-- Tabla Detalle de Inicios de Sesion
CREATE TABLE 
IF 
	NOT EXISTS iniciosDeSesion (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDUsuario TEXT, 
		Usuario TEXT,
		Fecha DATETIME 
	);

--Agregar Columna (Mensaje Ventas) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS mensajeVentas int DEFAULT 1;

--Agregar Columna (Mensaje Inventario) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS mensajeInventario int DEFAULT 1;

--Agregar Columna (Stock) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS stock int DEFAULT 1;

--Agregar Columna (Stock Minimo) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS stockMinimo int DEFAULT 1;

--Agregar Columna (Stock Maximo) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS stockMaximo int DEFAULT 1;

--Agregar Columna (Precio) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS precio int DEFAULT 1;

--Agregar Columna (Número Revisión) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS numeroRevision int DEFAULT 1;

--Agregar Columna (Tipo de IVA) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS tipoIVA int DEFAULT 1;

--Agregar Columna (Clave Producto) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS claveProducto int DEFAULT 1;

--Agregar Columna (Clave Unidad) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS claveUnidad int DEFAULT 1;

--Agregar Columna (Correos) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS correos int DEFAULT 1;

--Agregar Columna (Bascula) a la tabla "empleadospermisos" para permitir o denegar el acceso a esta opcion.
ALTER TABLE Empleados ADD COLUMN IF NOT EXISTS Bascula int DEFAULT 1;


--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN mensajeVentas MensajeVentas INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN mensajeInventario MensajeInventario INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN stock Stock INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN stockMinimo StockMinimo INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN stockMaximo StockMaximo INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN precio Precio INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN numeroRevision NumeroRevision INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN tipoIVA TipoIVA INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN claveProducto ClaveProducto INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN claveUnidad ClaveUnidad INT(11);
--Renombrar Columna de tabla empleadosPermisos
ALTER TABLE empleadospermisos CHANGE COLUMN correos Correos INT(11);

ALTER TABLE empleadospermisos ALTER MensajeVentas SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER MensajeInventario SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER Stock SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER StockMinimo SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER StockMaximo SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER Precio SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER NumeroRevision SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER TipoIVA SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER ClaveProducto SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER ClaveUnidad SET DEFAULT 1;

ALTER TABLE empleadospermisos ALTER Correos SET DEFAULT 1;

-- Agregar columna de fecha inicial de licencia para tabla usuarios
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS FechaInicioLicencia DATE DEFAULT '0001-01-01';
-- Agregar columna de fecha final de licencia para tabla usuarios

ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS FechaFinLicencia DATE DEFAULT '0001-01-01';

-- Agregar columna para el estado de la licencia (pagada, vencida, demo)
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS EstadoLicencia INT(1) DEFAULT 3;

-- Agregar columna de fecha para la verificacion de internet cada mes
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS FechaConexionInternet DATE DEFAULT '2021-06-01';

-- Agregar columna de fecha limite para comprobar los dias sin conectarse a internet
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS FechaConexionLimite DATE DEFAULT '2050-12-31';

-- Agregar columna para guardar los dias que han pasado para poder verificar la conexion a internet
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS DiasVerificacionInternet INT(2) DEFAULT 0;

-- Agregar columna para guardar la ultima fecha en la que se hizo verificacion de la conexion a internet
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS UltimaVerificacion DATE DEFAULT '0001-01-01';

ALTER TABLE iniciosdesesion ADD COLUMN IF NOT EXISTS Correo TEXT;

-- Agregar columna para guardar la licencia
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS Licencia VARCHAR(30) DEFAULT NULL;

-- Tabla de editar ticket 
CREATE TABLE 
IF 
	NOT EXISTS editarticket (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDUsuario INT, 
		MensajeTicket TEXT 
	);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS Usuario INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS Direccion INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS ColyCP INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS RFC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS Correo INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS Telefono INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS NombreC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS DomicilioC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS RFCC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS CorreoC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS TelefonoC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS ColyCPC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS FormaPagoC INT DEFAULT (1);

	ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS logo INT DEFAULT (1);

	ALTER TABLE facturas MODIFY e_regimen VARCHAR(100);

	ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS CerrarSesionAuto INT DEFAULT (0);

	ALTER TABLE ventas ADD COLUMN IF NOT EXISTS DineroRecibido FLOAT DEFAULT (0);

	ALTER TABLE ventas ADD COLUMN IF NOT EXISTS CambioTotal FLOAT DEFAULT (0);

-- Agregar columna de nombre_comercial para tabla usuarios
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS nombre_comercial TEXT DEFAULT NULL;

-- Agregar columna de e_nombre_comercial para tabla facturas
ALTER TABLE facturas ADD COLUMN IF NOT EXISTS e_nombre_comercial VARCHAR(300) DEFAULT NULL;

ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS editarTicket INT DEFAULT (1);

ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS EnvioCorreo INT DEFAULT (1);

ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS confiGeneral INT DEFAULT (1);

ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS porcentajeGanancia INT DEFAULT (1);

ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS tipoMoneda INT DEFAULT (1);

ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS RespaldarInfo INT DEFAULT (1);

CREATE TABLE 
IF 
	NOT EXISTS permisosConfiguracion (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDEmpleado INTEGER NOT NULL,
		IDUsuario INTEGER NOT NULL,
		EditarTicket INTEGER DEFAULT (1),
		AgregarDineroCaja INTEGER DEFAULT (1),
		RetirarDineroCaja INTEGER DEFAULT (1),
		HacerCorteCaja INTEGER DEFAULT (1),
		LlegarStockMinimo INTEGER DEFAULT (1),
		ModificarPrecio INTEGER DEFAULT (1),
		ModificarStock INTEGER DEFAULT (1),
		VenderseProducto INTEGER DEFAULT (1),
		HacerVenta INTEGER DEFAULT (1),
		CerrarVentanaVentasCuandoSeTienenProductos INTEGER DEFAULT (1),
		EliminarProductosVentas INTEGER DEFAULT (1),
		IniciarSesion INTEGER DEFAULT (1),
		HacerVentaDescuento INTEGER DEFAULT (1),
		EviarRespaldoCerrarSesion INTEGER DEFAULT (1),
		CodigoBarrasTicketVenta INTEGER DEFAULT (1),
		HabilitarInfoPaginaWeb INTEGER DEFAULT (1),
		MostrarCodigoProductoVentas INTEGER DEFAULT (1),
		CerrarSesionCorteCaja INTEGER DEFAULT (1),
		MostrarPrecioProductoVentas INTEGER DEFAULT (1),
		PermitirStockNegativo INTEGER DEFAULT (1),
		GenerarTicketAlRealizarVenta INTEGER DEFAULT (1),
		AvisarProductosNoVendidos INTEGER DEFAULT (1),
		PorcentajeGanancia INTEGER DEFAULT (1),
		ActivarPrecioMayoreoVentas INTEGER DEFAULT (1),
		TipoDeMoneda INTEGER DEFAULT (1),
		RespaldarInformacion INTEGER DEFAULT (1)
	);

-- Agregar columna de tipo de revision de inventario para tabla RevisarInventarioReportes
ALTER TABLE RevisarInventarioReportes ADD COLUMN IF NOT EXISTS TipoRevision VARCHAR(100) DEFAULT NULL;

	ALTER TABLE productmessage ADD COLUMN IF NOT EXISTS CantidadMinimaDeCompra INT DEFAULT (null);

	ALTER TABLE productmessage MODIFY COLUMN CantidadMinimaDeCompra int(11) DEFAULT 0;

-- Agregamos columna para mostrar o no en el listado de opciones, a la tabla de DetalleGeneral es donde se carga los datos al ComboBox en Detalles Productos
ALTER TABLE DetalleGeneral ADD COLUMN IF NOT EXISTS Mostrar INT DEFAULT 1;

ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS MensajeVentas INT DEFAULT (1);

ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS MensajeInventario INT DEFAULT (1);

ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS NombreComercial INT DEFAULT (1);

-- -------------------------------------------------------------------
-- Creación de FOREIGN KEY Constraint para la consulta del reporte de Menos vendidos.
-- -------------------------------------------------------------------

-- Creación de FOREIGN KEY Constraint para la tabla de usuarios.
-- -------------------------------------------------------------------
ALTER TABLE ventas ADD CONSTRAINT FK_Ventas_IDUsuario FOREIGN KEY
IF
	NOT EXISTS ( IDUsuario ) REFERENCES Usuarios ( ID ) ON DELETE CASCADE ON UPDATE CASCADE;

-- Creación de FOREIGN KEY Constraint para la tabla de productos.
-- -------------------------------------------------------------------
ALTER TABLE productosventa ADD CONSTRAINT FK_ProductosVenta_IDProducto FOREIGN KEY
IF
	NOT EXISTS ( IDProducto ) REFERENCES Productos ( ID ) ON DELETE CASCADE ON UPDATE CASCADE;

-- -------------------------------------------------------------------
-- Creación de index para la consulta del reporte de Menos vendidos.
-- -------------------------------------------------------------------

-- Creación de index para la tabla de Ventas.
-- -------------------------------------------------------------------

-- Index Fecha Operación en Tabla de ventas
CREATE INDEX
IF
	NOT EXISTS `ReporteMenosVendidos_index_Usuario_FechaOperacion` ON `ventas` ( `IDUsuario`, `FechaOperacion` );
	
-- Creación de index para la tabla de Prodctos.
-- -------------------------------------------------------------------

-- Index Fecha Operación en Tabla de productos
CREATE INDEX
IF
	NOT EXISTS `ReporteMenosVendidos_index_Tipo` ON `productos` ( `tipo` );
	
-- Index Fecha Operación en Tabla de productos
CREATE INDEX
IF
	NOT EXISTS `ReporteMenosVendidos_index_Stock_Precio` ON `productos` ( `Stock`, `Precio` );
	
-- Creación de index para la tabla de ProductosVenta.
-- -------------------------------------------------------------------

-- Index Fecha Operación en Tabla de productosventa
CREATE INDEX
IF
	NOT EXISTS `ReporteMenosVendidos_index_IDVenta_IDProducto_Cantidad` ON `productosventa` ( `IDVenta`, `IDProducto`, `Cantidad` );

CREATE TABLE 
IF 
	NOT EXISTS HistorialStock(
		ID INTEGER PRIMARY KEY AUTO_INCREMENT,
		IDProducto INTEGER,
		TipoDeMovimiento TEXT,
		StockAnterior TEXT,
		StockNuevo TEXT,
		Fecha DATETIME,
		IDUsuario INTEGER
	);

ALTER TABLE historialstock CHANGE COLUMN IF EXISTS IDUsuario NombreUsuario VARCHAR(100);

ALTER TABLE historialstock ADD COLUMN IF NOT EXISTS Cantidad DECIMAL (9,2) DEFAULT (0)  AFTER StockAnterior;
	
ALTER TABLE historialstock MODIFY Cantidad VARCHAR(100);
	
ALTER TABLE historialstock ADD COLUMN IF NOT EXISTS StockInicial VARCHAR(100);

ALTER TABLE Configuracion ADD COLUMN IF NOT EXISTS RespaldoAlCerrarSesion INT DEFAULT 1;


-- Agregar columnas extra para facturacion en tabla Facturas

ALTER TABLE facturas ADD COLUMN IF NOT EXISTS motivo_canc VARCHAR(2) DEFAULT NULL AFTER e_nombre_comercial;

ALTER TABLE facturas ADD COLUMN IF NOT EXISTS uuid_sust VARCHAR(36) DEFAULT NULL AFTER motivo_canc;

ALTER TABLE historialstock ADD COLUMN IF NOT EXISTS tipoDeVenta varchar (2);

ALTER TABLE historialstock ADD COLUMN IF NOT EXISTS idComboServicio  int;

ALTER TABLE productos ADD COLUMN IF NOT EXISTS EnWeb text;

-- Agregar Columna (IDEmpleado) a la tabla "abonos" para saber quien recibe ese abono en el sistema.
ALTER TABLE Abonos ADD COLUMN IF NOT EXISTS IDEmpleado int DEFAULT 0;

ALTER TABLE Empleados ADD COLUMN IF NOT EXISTS ConsultaPrecio int DEFAULT 1;


-- Agregamos tabla de Historial de Cortes de Caja en la base de datos
CREATE TABLE
IF
	NOT EXISTS HistorialCortesDeCaja (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		IDCorteDeCaja INTEGER NOT NULL DEFAULT 0,
		IDUsuario INTEGER NOT NULL DEFAULT 0,
		IDEmpleado INTEGER DEFAULT 0,
		FechaOperacion DATETIME NOT NULL,
		SaldoInicialEfectivo DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		SaldoInicialTarjeta DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		SaldoInicialVales DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		SaldoInicialCheque DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		SaldoInicialTransferencia DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		SaldoInicialCredito DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		SaldoInicialAnticipo DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0, 
		CantidadRetiradaDelCorte DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		FOREIGN KEY ( IDCorteDeCaja ) REFERENCES caja ( ID ) ON UPDATE CASCADE ON DELETE CASCADE,
		FOREIGN KEY ( IDUsuario ) REFERENCES usuarios ( ID ) ON UPDATE CASCADE ON DELETE CASCADE 
	);

-- Agregar columna de 58mm en la tabla de EditarTicket 
ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS ticket58mm INT DEFAULT 0;

-- Agregar columna de 80mm en la tabla de EditarTicket 
ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS ticket80mm INT DEFAULT 1;

-- Se agrego una nueva columna a Ventas para cuando se aplica un anticipo tener una relacion con Anticipos
ALTER TABLE ventas ADD COLUMN IF NOT EXISTS IDAnticipo INT DEFAULT 0;

-- Agregar columna de 80mm en la tabla de EditarTicket 
ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS Referencia INT DEFAULT 1;

-- Agregar Columna de Envio Anticpo a la tabla de configutacion
ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS CorreoAnticipo INT DEFAULT 0 ;

-- Agregar Columna de PermisoAnticipo a la tabla permisosconfiguracion
ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS PermisoCorreoAnticipo INT DEFAULT 1 ;

--Agregar Columande de permiso venta a cliente con descuento en la tabla permisos configuracion
ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS VentaClienteDescuento INT DEFAULT 1 ;

--Agregar Columna de Correo Cliente descuento venta a la tabla de configuracion
ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS CorreoVentaClienteDescuento INT DEFAULT 0 ;

--Agregar Columana de Permiso Venta a Cliente con descuento a la tabla empeladospermisos
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS PermisoVentaClienteDescuento INT DEFAULT 1 ;

--Agrega Columna VentaAClienteDescuentoSinAutorizacion a la tabla de empleadospermisos
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS PermisoVentaClienteDescuentoSinAutorizacion INT DEFAULT 1 ;

-- Agregar columna para descuento por cliente en productos
ALTER TABLE Productos ADD COLUMN IF NOT EXISTS TieneDescuentoCliente TINYINT(1) DEFAULT 0;

-- Agregar columna para descuento por mayoreo en productos
ALTER TABLE Productos ADD COLUMN IF NOT EXISTS TieneDescuentoMayoreo TINYINT(1) DEFAULT 0;

-- Actualizar las columnas TieneDescuentoCliente
UPDATE Productos AS P INNER JOIN DescuentoCliente AS DC ON P.ID = DC.IDProducto SET P.TieneDescuentoCliente = 1;

-- Actualizar las columnas TieneDescuentoMayoreo
UPDATE Productos AS P INNER JOIN (SELECT DM.IDProducto FROM DescuentoMayoreo AS DM GROUP BY DM.IDProducto) AS DM ON P.ID = DM.IDProducto SET TieneDescuentoMayoreo = 1;

-- Agregar columna para guardar el username para filtros de productos
ALTER TABLE FiltroProducto ADD COLUMN IF NOT EXISTS Username VARCHAR(100) DEFAULT NULL;

-- Agregar columna para guardar el username para filtros de dinamicos
ALTER TABLE FiltrosDinamicosVetanaFiltros ADD COLUMN IF NOT EXISTS Username VARCHAR(100) DEFAULT NULL;

-- Agregar Columna de Envio correo cuando la licencia se active
ALTER TABLE usuarios ADD COLUMN IF NOT EXISTS CorreoLicenciaPagada INT DEFAULT 0 ;


--Se crea la tabla de Plantillas Pemrisos
CREATE TABLE
IF
	NOT EXISTS PlantillaPermisos (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		Nombre VARCHAR(100) DEFAULT NULL ,
		IDUsuario INTEGER NOT NULL DEFAULT 0,
		Estatus INTEGER DEFAULT 1,
		Anticipo INTEGER DEFAULT 0,
		Caja INTEGER DEFAULT 0,
		clientes INTEGER DEFAULT 0,
		configuracion INTEGER DEFAULT 0,
		empleado INTEGER DEFAULT 0,
		factura INTEGER DEFAULT 0,
		inventario INTEGER DEFAULT 0,
		misdatos INTEGER DEFAULT 0,
		productos INTEGER DEFAULT 0,
		proveedor INTEGER DEFAULT 0,
		reportes INTEGER DEFAULT 0,
		precio INTEGER DEFAULT 0,
		ventas INTEGER DEFAULT 0,
		bascula INTEGER DEFAULT 0,
		ConsultaPrecio INTEGER DEFAULT 0,
		FOREIGN KEY ( IDUsuario ) REFERENCES usuarios ( ID ) ON UPDATE CASCADE ON DELETE CASCADE 
	);

--Se crea la tabla de Permisos de los Permisos
CREATE TABLE
IF
	NOT EXISTS SubPermisos (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		IDPlantilla INTEGER NOT NULL DEFAULT 0,
		ANT_GenerarTicket INTEGER DEFAULT 1,
		ANT_HabilitarDeshabilitar INTEGER DEFAULT 1,
		ANT_DevolverAnticipo INTEGER DEFAULT 1,
		ANT_BotonBuscar INTEGER DEFAULT 1,
		ANT_NUevoAnticipo INTEGER DEFAULT 1,
		CONF_ConfiguracionGeneral INTEGER DEFAULT 1,
		CONF_EnvioDeCorreo INTEGER DEFAULT 1,
		CONF_EditarTicket INTEGER DEFAULT 1,
		CONF_ProcentajeDeGanancia INTEGER DEFAULT 1,
		CONF_TipoMoneda INTEGER DEFAULT 1,
		CONF_RespaldarInformmacion INTEGER DEFAULT 1,
		FAC_VerFactura INTEGER DEFAULT 1,
		FAC_DescargarFactura INTEGER DEFAULT 1,
		FAC_CaneclarFactura INTEGER DEFAULT 1,
		FAC_VerPagos INTEGER DEFAULT 1,
		FAC_BuscarFacturas INTEGER DEFAULT 1,
		FAC_EnviarFacturas INTEGER DEFAULT 1,
		FAC_GenerarComplemento INTEGER DEFAULT 1,
		PROD_AgregarXML INTEGER DEFAULT 1,
		PROD_CambiarTipo INTEGER DEFAULT 1,
		PROD_BotonAsignar INTEGER DEFAULT 1,
		PROD_BotonEtiqueta INTEGER DEFAULT 1,
		PROD_BotonImprimir INTEGER DEFAULT 1,
		PROD_AgregarCombo INTEGER DEFAULT 1,
		PROD_BotonFiltro INTEGER DEFAULT 1,
		PROD_OpcionEditar INTEGER DEFAULT 1,
		PROD_OpcionHistorial INTEGER DEFAULT 1,
		PROD_CargarImagen INTEGER DEFAULT 1,
		PROD_OpcionCopiar INTEGER DEFAULT 1,
		PROD_DeshabilitarSeleccionados INTEGER DEFAULT 1,
		PROD_MostrarEnLista INTEGER DEFAULT 1,
		PROD_MostrarMosiaco INTEGER DEFAULT 1,
		PROD_BotonReporte INTEGER DEFAULT 1,
		PROD_AgregarProducto INTEGER DEFAULT 1,
		PROD_AgregarServicio INTEGER DEFAULT 1,
		PROD_BotonBorrarFiltro INTEGER DEFAULT 1,
		PROD_EspacioEstado INTEGER DEFAULT 1,
		PROD_GenerarCodigoBarras INTEGER DEFAULT 1,
		PROD_OpcionEtiqueta INTEGER DEFAULT 1,
		PROD_OpcionAjustar INTEGER DEFAULT 1,
		VEN_CancelarVenta INTEGER DEFAULT 1,
		VEN_VerInfoVenta INTEGER DEFAULT 1,
		VEN_BuscarVenta INTEGER DEFAULT 1,
		VEN_GuardarVenta INTEGER DEFAULT 1,
		VEN_VentasGuardadas INTEGER DEFAULT 1,
		VEN_AsignarCliente INTEGER DEFAULT 1,
		VEN_AplicarDescuento INTEGER DEFAULT 1,
		VEN_VerNotaVenta INTEGER DEFAULT 1,
		VEN_TimbrarFactura INTEGER DEFAULT 1,
		VEN_NuevaVenta INTEGER DEFAULT 1,
		VEN_BotonAnticipos INTEGER DEFAULT 1,
		VEN_VerUltimoTicket INTEGER DEFAULT 1,
		VEN_EliminarUltimo INTEGER DEFAULT 1,
		VEN_TerminarVenta INTEGER DEFAULT 1,
		VEN_VerTicketVenta INTEGER DEFAULT 1,
		VEN_BotonEnviarNota INTEGER DEFAULT 1,
		VEN_BotonCancelar INTEGER DEFAULT 1,
		VEN_AbrirCaja INTEGER DEFAULT 1,
		VEN_GuardarPresupuesto INTEGER DEFAULT 1,
		VEN_EliminarTodos INTEGER DEFAULT 1,
		VEN_VentaConDescuento INTEGER DEFAULT 1,
		CAJA_BotonAgregarDinero INTEGER DEFAULT 1,
		CAJA_BotonRetirarDinero INTEGER DEFAULT 1,
		CAJA_BotonAbrirCaja INTEGER DEFAULT 1,
		CAJA_MOstrarSaldoInicial INTEGER DEFAULT 1,
		CAJA_MostrarPanelAnticipos INTEGER DEFAULT 1,
		CAJA_MostrarPanelTotalCaja INTEGER DEFAULT 1,
		CAJA_BotonHistorialDineroAgregado INTEGER DEFAULT 1,
		CAJA_BotonHistorialDineroRetirado INTEGER DEFAULT 1,
		CAJA_BotonCorteDeCaja INTEGER DEFAULT 1,
		CAJA_MostraPanelVentas INTEGER DEFAULT 1,
		CAJA_MostrarPanelDineroAgregado INTEGER DEFAULT 1,
		EMP_NuevoEmpleado INTEGER DEFAULT 1,
		EMP_EditarEmpleado INTEGER DEFAULT 1,
		EMP_PermisosEmpleados INTEGER DEFAULT 1,
		INV_RevisarInventario INTEGER DEFAULT 1,
		INV_ActualizarInventario INTEGER DEFAULT 1,
		INV_ActualizarInventarioXML INTEGER DEFAULT 1,
		INV_BuscarBoton INTEGER DEFAULT 1,
		INV_BotonTerminar INTEGER DEFAULT 1,
		PROVE_botonBuscar INTEGER DEFAULT 1,
		PROVE_NuevoProveedor INTEGER DEFAULT 1,
		PROVE_Deshabilitar INTEGER DEFAULT 1,
		PROVE_Habilitar INTEGER DEFAULT 1,
		CLI_BotonBuscar INTEGER DEFAULT 1,
		CLI_NuevoTipoCliente INTEGER DEFAULT 1,
		CLI_ListadoTipoCliente INTEGER DEFAULT 1,
		CLI_NuevoCliente INTEGER DEFAULT 1,
		CLI_Deshabilitar INTEGER DEFAULT 1,
		CLI_Habilitar INTEGER DEFAULT 1,
		BAS_AgregarEditarPredeterminada INTEGER DEFAULT 1,
		MISDA_GuardarDatos INTEGER DEFAULT 1,
		MISDA_SubirImagen INTEGER DEFAULT 1,
		MISDA_EliminarImagen INTEGER DEFAULT 1,
		MISDA_ActualizarContrasenna INTEGER DEFAULT 1,
		MISDA_ActualizarArchivos INTEGER DEFAULT 1,
		REP_HistorialDePrecios INTEGER DEFAULT 1,
		REP_Caja INTEGER DEFAULT 1,
		REP_ReporteInventario INTEGER DEFAULT 1,
		REP_ReporteVentas INTEGER DEFAULT 1,
		REP_ReporteClientes INTEGER DEFAULT 1,
		REP_ReporteMMasMenos INTEGER DEFAULT 1
	);

	-- Agregar Columna de MostraMensaje a Editarticket
ALTER TABLE editarticket ADD COLUMN IF NOT EXISTS mostrarMensaje INT DEFAULT 0 ;

--Se crea la tabla de permisos Conceptos
CREATE TABLE
IF
	NOT EXISTS PermisosConceptosAgregarRetirarDinero (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		IDUsuario INTEGER NOT NULL DEFAULT 0,
		IDEmpleado INTEGER NOT NULL DEFAULT 0,
		AgregarConcepto INTEGER DEFAULT 1,
		HabilitarConcepto INTEGER DEFAULT 1,
		DeshabilitarConcepto INTEGER DEFAULT 1,
		FOREIGN KEY ( IDUsuario ) REFERENCES usuarios ( ID ) ON UPDATE CASCADE ON DELETE CASCADE ,
		FOREIGN KEY ( IDEmpleado ) REFERENCES empleados ( ID ) ON UPDATE CASCADE ON DELETE CASCADE 
	);

--Se crea la tabla para los detalles de los horarios de empleado y sistema checador
CREATE TABLE
IF
	NOT EXISTS detallesChecadorEmpleados (
		ID INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
		IDUsuario INTEGER NOT NULL DEFAULT 0,
		IDEmpleado INTEGER NOT NULL DEFAULT 0,
		Huella LONGBLOB DEFAULT NULL,
		HorasSemanales INTEGER DEFAULT 0,
		HorasDiarias INTEGER DEFAULT 0,
		MaximoHorasDiarias INTEGER DEFAULT 0,
		HorasObligatorias INTEGER DEFAULT 0,
		Salario DECIMAL ( 16, 2 ) NOT NULL DEFAULT 0,
		TiempoDeSalario INTEGER DEFAULT 0,
		Estatus INTEGER NOT NULL DEFAULT 1,
		DiasLaborales VARCHAR ( 20 ) DEFAULT NULL,
		FOREIGN KEY ( IDUsuario ) REFERENCES usuarios ( ID ) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY ( IDEmpleado ) REFERENCES empleados ( ID ) ON UPDATE CASCADE ON DELETE CASCADE 
	);

	-- Agregar Columna de Agregar Descuento a empleadosPermisos
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS Agregar_Descuento INT DEFAULT 1 ;

	-- Agregar Columna de Eliminar Descuento a empleadosPermisos
ALTER TABLE empleadospermisos ADD COLUMN IF NOT EXISTS Eliminar_Descuento INT DEFAULT 1 ;

ALTER TABLE dgvaumentarinventario MODIFY COLUMN DiferenciaUnidades VARCHAR(100);
ALTER TABLE dgvaumentarinventario MODIFY COLUMN StockActual VARCHAR(100);
ALTER TABLE dgvaumentarinventario MODIFY COLUMN NuevoStock VARCHAR(100);
ALTER TABLE dgvdisminuirinventario MODIFY COLUMN DiferenciaUnidades VARCHAR(100);
ALTER TABLE dgvdisminuirinventario MODIFY COLUMN StockActual VARCHAR(100);
ALTER TABLE dgvdisminuirinventario MODIFY COLUMN NuevoStock VARCHAR(100);


---- Columnas para configuracion de creditos
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoHuella INT DEFAULT 0 ;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoMoratorio INT DEFAULT 0 ;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoPorcentajemoratorio DECIMAl ( 16, 4 ) DEFAULT 10.00;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoAplicarpordefecto INT DEFAULT 1 ;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoPorcentajeinteres DECIMAl ( 16, 4 ) DEFAULT 10.00;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoAplicarpagoinicial INT DEFAULT 0 ;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoPagoinicial DECIMAl ( 16, 2 ) DEFAULT 1;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditomodolimiteventas VARCHAR(100) DEFAULT 'Ninguno';
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditolimiteventas INT DEFAULT 0 ;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditomodototalcredito VARCHAR(100) DEFAULT 'Ninguno';
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditototalcredito DECIMAl ( 16, 2 ) DEFAULT 0;
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditoperiodocobro VARCHAR(100) DEFAULT 'Mensual';
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditomodocobro VARCHAR(100) DEFAULT 'Dias trascurridos';
--ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS creditodiassincobro INT DEFAULT 0 ;

-- Agregar Columna de la ganancia por venta para graficarlo 
ALTER TABLE ventas ADD COLUMN IF NOT EXISTS Ganancia VARCHAR ( 255 ) DEFAULT NULL ;

--Columna de Comenatrios para caja los tickets agregar retirar dinero y de caja
ALTER TABLE caja ADD COLUMN IF NOT EXISTS Comentarios TEXT DEFAULT NULL;

ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS traspasos INTEGER(11) DEFAULT (0);

   -- Se agrego la columna MostrarStockConsultaPrecio a la tabla de Configuracion
ALTER TABLE Configuracion ADD COLUMN IF NOT EXISTS MostrarStockConsultaPrecio INT DEFAULT 1;

   -- Se agrego la columna MostrarStockConsultaPrecio a la tabla de Configuracion
  ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS PermisoStockConsultarPrecio INT DEFAULT 1;

   -- Se agrego la columna PrimerCorte a la tabla de historialCorteDeCaja
  ALTER TABLE historialcortesdecaja ADD COLUMN IF NOT EXISTS PrimerCorte INT DEFAULT 1;

   -- Se agrego la columna PermisoCorreoSaldoInicial a la tabla de permisosconfiguracion
  ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS PermisoCorreoSaldoInicial INT DEFAULT 1;

  -- Se agrego la columna EnvioCorreoSaldoIncial a la tabla de permisosconfiguracion
  ALTER TABLE configuracion ADD COLUMN IF NOT EXISTS EnvioCorreoSaldoIncial INT DEFAULT 1;
ALTER TABLE permisosconfiguracion ADD COLUMN IF NOT EXISTS PermisoStockConsultarPrecio INT DEFAULT 1;

   -- Se agrego la columna PesoAutomatico en Productos para recibir el peso automaticamente. // 0 = Acepta todo tipo de forma venta //1 = Solo se puede vender por Enteros //2 = Toma el peso en automatico de la bascula
ALTER TABLE productos ADD COLUMN IF NOT EXISTS FormatoDeVenta INT DEFAULT 0;

ALTER TABLE EmpleadosPermisos ADD COLUMN IF NOT EXISTS VentasACredito INT DEFAULT 1;

   -- Columna para guardar la clave de traspaso de un movimiento si existe, de manera en que se pueda volver a ver cuando se haga un reporte.
  ALTER TABLE dgvdisminuirinventario ADD COLUMN IF NOT EXISTS claveTraspaso VARCHAR ( 15 ) DEFAULT NULL ;