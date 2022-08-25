using CustomControlPUDVE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Crear_factura : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public static string procedencia { get; set; }

        string[] datos; 

        Dictionary<string, string> clientes = new Dictionary<string, string>();

        string mensajeClientes = "Seleccionar cliente",
               mensajeSinClientes = "No hay clientes para mostrar. Ir a registrar cliente.";

        int con_id_cliente = 0;
        int n_filas = 0;
        int id_venta = 0;
        int p_incompletos = 0;
        int paso = 1;
        string[][] arr_dproductos;
        decimal cantidd_productos = 0;
        int excede_montomax_xproducto = 0;

        string RazonS, RFC, Telefono, Nombre, Correo, Pais, Estado, Municipio, Localidad, CP, Colonia, Calle, NumInt, NumExt,NumeroCliente;

        private void RemoveText(object sender, EventArgs e)
        {
            if (cmb_bx_clientes.Text.Equals(mensajeClientes) || cmb_bx_clientes.Text.Equals(mensajeSinClientes))
            {
                cmb_bx_clientes.Text = String.Empty;
                cmb_bx_clientes.ForeColor = SystemColors.WindowText;
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmb_bx_clientes.Text))
            {
                var isEmpty = estaSinRegistros(clientes);

                if (isEmpty.Equals(true))
                {
                    cmb_bx_clientes.Text = mensajeSinClientes;
                }
                else if (isEmpty.Equals(false))
                {
                    cmb_bx_clientes.Text = mensajeClientes;
                }
                cmb_bx_clientes.ForeColor = Color.DarkGray;
            }
        }

        private object estaSinRegistros(Dictionary<string, string> clientes)
        {
            bool isEmpty = false;

            if (clientes.Count.Equals(0) || clientes.Count <= 1)
            {
                isEmpty = true;
            }
            else if (clientes.Count >= 2)
            {
                isEmpty = false;
            }

            return isEmpty;
        }

        public Crear_factura(int sin_cliente, int n_f, int id_v)
        {
            InitializeComponent();

            con_id_cliente = sin_cliente; 
            n_filas = n_f;
            id_venta = id_v;

            cmb_bx_clientes.GotFocus += new System.EventHandler(RemoveText);
            cmb_bx_clientes.LostFocus += new System.EventHandler(AddText);
        }

        private void Crear_factura_Load(object sender, EventArgs e)
        {
            cmb_bx_clientes.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cmb_bx_forma_pago.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cmb_bx_metodo_pago.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cmb_bx_moneda.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cmb_bx_uso_cfdi.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
           
            // Obtiene los clientes

            DataTable d_clientes;
            
            var indice = 0;
            int c = 1;

            d_clientes = cn.CargarDatos(cs.cargar_datos_venta_xml(7, 0, Convert.ToInt32(FormPrincipal.userID)));

            if(d_clientes.Rows.Count > 0)
            {
                clientes.Add("0", mensajeClientes);
                foreach(DataRow r_clientes in d_clientes.Rows)
                {
                    if (!r_clientes["RFC"].ToString().Equals(""))
                    {
                        clientes.Add(r_clientes["ID"].ToString().Trim(), r_clientes["RazonSocial"].ToString().Trim() + " - " + r_clientes["RFC"].ToString().Trim());
                    }
                    else
                    {
                        clientes.Add(r_clientes["ID"].ToString().Trim(), r_clientes["RazonSocial"].ToString().Trim());
                    }

                    if (Convert.ToInt32(r_clientes["ID"]) == con_id_cliente)
                    {
                        indice = c;
                    }
                    c++;
                }
            }
            else
            {
                clientes.Add("0", mensajeSinClientes);
            }

            cmb_bx_clientes.DataSource = clientes.ToArray();
            cmb_bx_clientes.DisplayMember = "Value";
            cmb_bx_clientes.ValueMember = "Key";
            cmb_bx_clientes.SelectedIndex = 0;
            cmb_bx_clientes.ForeColor = Color.DarkGray;

            cmb_bx_clientes.MatchingMethod = StringMatchingMethod.NoWildcards;

            // Método de pago

            Dictionary<string, string> metodo_pago = new Dictionary<string, string>();
            metodo_pago.Add("PUE", "Pago en una sola exhibición");
            metodo_pago.Add("PPD", "Pago en parcialidades o diferido");

            cmb_bx_metodo_pago.DataSource = metodo_pago.ToArray();
            cmb_bx_metodo_pago.DisplayMember = "Value";
            cmb_bx_metodo_pago.ValueMember = "Key";
            
            // Forma de pago

            Dictionary<string, string> forma_pago = new Dictionary<string, string>();
            forma_pago.Add("01", "01 - Efectivo");
            forma_pago.Add("02", "02 - Cheque nominativo");
            forma_pago.Add("03", "03 - Transferencia electrónica de fondos");
            forma_pago.Add("04", "04 - Tarjeta de crédito");
            forma_pago.Add("05", "05 - Monedero electrónico");
            forma_pago.Add("06", "06 - Dinero electrónico");
            forma_pago.Add("08", "08 - Vales de despensa");
            forma_pago.Add("12", "12 - Dación en pago");
            forma_pago.Add("13", "13 - Pago por subrogación");
            forma_pago.Add("14", "14 - Pago por consignación");
            forma_pago.Add("15", "15 - Condonación");
            forma_pago.Add("17", "17 - Compensación");
            forma_pago.Add("23", "23 - Novación");
            forma_pago.Add("24", "24 - Confusión");
            forma_pago.Add("25", "25 - Remisión de deuda");
            forma_pago.Add("26", "26 - Prescripción o caducidad");
            forma_pago.Add("27", "27 - A satisfacción del acreedor");
            forma_pago.Add("28", "28 - Tarjeta de débito");
            forma_pago.Add("29", "29 - Tarjeta de servicios");
            forma_pago.Add("30", "30 - Aplicación de anticipos");
            forma_pago.Add("99", "99 - Por definir");
            
            cmb_bx_forma_pago.DataSource = forma_pago.ToArray();
            cmb_bx_forma_pago.DisplayMember = "Value";
            cmb_bx_forma_pago.ValueMember = "Key";
            
            //Busca la forma de pago de la venta
            DataTable d_detallesvnt = cn.CargarDatos(cs.consulta_dventa(2, id_venta));

            int f_pg_default = 0;

            if (d_detallesvnt.Rows.Count > 0)
            {
                DataRow r_detallesvnt = d_detallesvnt.Rows[0];
                                
                int cant_fpg = 0;
                decimal[] arr_f_pg = new decimal[3];

                if (Convert.ToInt32(r_detallesvnt["Efectivo"]) > 0) { f_pg_default = 0; cant_fpg++; }

                if (Convert.ToInt32(r_detallesvnt["Cheque"]) > 0) { f_pg_default = 1; cant_fpg++; }

                if (Convert.ToInt32(r_detallesvnt["Transferencia"]) > 0) { f_pg_default = 2; cant_fpg++; }

                if (Convert.ToInt32(r_detallesvnt["Vales"]) > 0) { f_pg_default = 6; cant_fpg++; }

                if (cant_fpg > 1)
                {
                    decimal efectivo = Convert.ToDecimal(r_detallesvnt["Efectivo"]);
                    decimal cheque = Convert.ToDecimal(r_detallesvnt["Cheque"]);
                    decimal transf = Convert.ToDecimal(r_detallesvnt["Transferencia"]);
                    decimal vales = Convert.ToDecimal(r_detallesvnt["Vales"]);

                    if (efectivo > cheque & efectivo > transf & efectivo > vales)
                    {
                        f_pg_default = 0;
                    }
                    if (cheque > efectivo & cheque > transf & cheque > vales)
                    {
                        f_pg_default = 1;
                    }
                    if (transf > efectivo & transf > cheque & transf > vales)
                    {
                        f_pg_default = 2;
                    }
                    if (vales > efectivo & vales > cheque & vales > transf)
                    {
                        f_pg_default = 6;
                    }
                }
            }
                        
            cmb_bx_forma_pago.SelectedIndex = f_pg_default;


            // Número de cuenta

            txt_cuenta.GotFocus += new EventHandler(encampo_cuenta);
            txt_cuenta.LostFocus += new EventHandler(scampo_cuenta);

            if(f_pg_default > 0)
            {
                string clave = cmb_bx_forma_pago.SelectedValue.ToString();

                mforma_pago(clave);
            }

            // Moneda

            Dictionary<string, string> moneda = new Dictionary<string, string>();
            DataTable d_moneda;

            d_moneda = cn.CargarDatos(cs.cargar_datos_venta_xml(6, 0, 0));

            moneda.Add("EUR", "EUR - Euro");
            moneda.Add("MXN", "MXN - Peso Mexicano");
            moneda.Add("USD", "USD - Dolar americano");

            foreach (DataRow r_moneda in d_moneda.Rows)
            {
                if(r_moneda["clave_moneda"].ToString() != "EUR" & r_moneda["clave_moneda"].ToString() != "MXN" & r_moneda["clave_moneda"].ToString() != "USD")
                {
                    moneda.Add(r_moneda["clave_moneda"].ToString(), r_moneda["clave_moneda"].ToString() + " - " + r_moneda["descripcion"].ToString());
                }
            }

            cmb_bx_moneda.DataSource = moneda.ToArray();
            cmb_bx_moneda.DisplayMember = "Value";
            cmb_bx_moneda.ValueMember = "Key";
            cmb_bx_moneda.SelectedIndex = 1;
            
            // Productos
           
            int location_y = 5;
            
            if (ListadoVentas.faltantes_productos.Length > 0)
            {
                for (int i = 1; i < n_filas; i++)
                {
                    cantidd_productos += Convert.ToDecimal(ListadoVentas.faltantes_productos[i][5]);

                    if (Convert.ToInt32(ListadoVentas.faltantes_productos[i][0]) == 1)
                    {
                        int id_producto = Convert.ToInt32(ListadoVentas.faltantes_productos[i][1]);
                        string des_habilitar = ListadoVentas.faltantes_productos[i][0];
                        string c_producto = ListadoVentas.faltantes_productos[i][2];
                        string c_unidad = ListadoVentas.faltantes_productos[i][3];
                        string descripcion = ListadoVentas.faltantes_productos[i][4];


                        TextBox txt_clave_unidad = new TextBox();
                        txt_clave_unidad.Name = "txt_clave_u" + i;
                        txt_clave_unidad.Location = new Point(8, location_y);
                        txt_clave_unidad.Size = new Size(49, 22);
                        txt_clave_unidad.MaxLength = 3;
                        txt_clave_unidad.TextAlign = HorizontalAlignment.Center;
                        txt_clave_unidad.CharacterCasing = CharacterCasing.Upper;
                        txt_clave_unidad.Leave += new EventHandler(valida_clave_unidad);
                        txt_clave_unidad.KeyPress += new KeyPressEventHandler(tdatos_cunidad);

                        TextBox txt_clave_producto = new TextBox();
                        txt_clave_producto.Name = "txt_clave_p" + i;
                        txt_clave_producto.Location = new Point(78, location_y);
                        txt_clave_producto.Size = new Size(87, 22);
                        txt_clave_producto.MaxLength = 8;
                        txt_clave_producto.TextAlign = HorizontalAlignment.Center;
                        txt_clave_producto.Leave += new EventHandler(valida_clave_producto);
                        txt_clave_producto.KeyPress += new KeyPressEventHandler(solo_numeros_cproducto);

                        TextBox txt_descripcion = new TextBox();
                        txt_descripcion.Name = "txt_descripcion" + i;
                        txt_descripcion.Location = new Point(186, location_y);
                        txt_descripcion.Size = new Size(390, 22);
                        txt_descripcion.MaxLength = 1000;
                        txt_descripcion.TextAlign = HorizontalAlignment.Center;
                        txt_descripcion.ReadOnly = true;

                        TextBox txt_idproducto = new TextBox();
                        txt_idproducto.Name = "txt_idprod" + i;
                        txt_idproducto.Location = new Point(591, location_y);
                        txt_idproducto.Size = new Size(40, 22);
                        txt_idproducto.MaxLength = 30;
                        txt_idproducto.TextAlign = HorizontalAlignment.Center;
                        txt_idproducto.Visible = false;

                        pnl_productos.Controls.Add(txt_clave_unidad);
                        pnl_productos.Controls.Add(txt_clave_producto);
                        pnl_productos.Controls.Add(txt_descripcion);
                        pnl_productos.Controls.Add(txt_idproducto);

                        location_y = location_y + 28;


                        txt_clave_producto.Text = c_producto;
                        txt_clave_unidad.Text = c_unidad;
                        txt_descripcion.Text = descripcion;
                        txt_idproducto.Text = Convert.ToString(id_producto);

                        p_incompletos++;

                        /*if(des_habilitar == "0") // Datos completos
                        {
                            txt_clave_producto.ReadOnly = true;
                            txt_clave_unidad.ReadOnly = true;
                        }
                        if (des_habilitar == "1") // Falta algún dato
                        {
                            if(c_producto != "")
                            {
                                txt_clave_producto.ReadOnly = true;
                            }
                            if (c_producto != "")
                            {
                                txt_clave_unidad.ReadOnly = true;
                            }
                        }*/

                        groupb_productos.Visible = false;
                    }
                }

                if(p_incompletos == 0)
                {
                    groupb_productos.Visible = false;
                }
            }
            else
            {
                groupb_productos.Visible = false;
            }

            groupb_monto_max.Visible = false;
            groupb_pago.Visible = false;

            // Total

            double total_v = Convert.ToDouble(cn.EjecutarSelect($"SELECT Total FROM Ventas WHERE ID='{id_venta}'", 14));
            lb_total_n.Text = total_v.ToString();


            // Verifica si se asigno un cliente a la venta.
            // Si la nota fue hecha a público en general, entonces agrega esos datos por default
            var detalles = mb.ObtenerDetallesVenta(id_venta, FormPrincipal.userID);

            if (detalles.Length > 0)
            {
                // Sin cliente asignado
                if (Convert.ToInt32(detalles[0]) == 0)
                {
                    txt_razon_social.Text = "Público en general";
                    txt_rfc.Text = "XAXX010101000";

                    Dictionary<string, string> usoCFDI = new Dictionary<string, string>();
                    usoCFDI.Add("G01", "Adquisición de mercancias");
                    usoCFDI.Add("G02", "Devoluciones, descuentos o bonificaciones");
                    usoCFDI.Add("G03", "Gastos en general");
                    usoCFDI.Add("I01", "Construcciones");
                    usoCFDI.Add("I02", "Mobilario y equipo de oficina por inversiones");
                    usoCFDI.Add("I03", "Equipo de transporte");
                    usoCFDI.Add("I04", "Equipo de computo y accesorios");
                    usoCFDI.Add("I05", "Dados, troqueles, moldes, matrices y herramental");
                    usoCFDI.Add("I06", "Comunicaciones telefónica");
                    usoCFDI.Add("I07", "Comunicaciones satelitale");
                    usoCFDI.Add("I08", "Otra maquinaria y equipo");
                    usoCFDI.Add("P01", "Por definir");

                    cmb_bx_uso_cfdi.DataSource = usoCFDI.ToArray();
                    cmb_bx_uso_cfdi.DisplayMember = "Value";
                    cmb_bx_uso_cfdi.ValueMember = "Key";
                    cmb_bx_uso_cfdi.SelectedValue = "G03";
                }
                else
                {
                    int id_c = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDCliente FROM Ventas WHERE ID='{id_venta}'", 6));

                    // Con cliente asignado
                    if (id_c > 0)
                    {
                        cargar_datos_cliente(id_c);
                    }
                }

                pnl_datos_cliente.Visible = true;
                btn_facturar.Enabled = true;
            }

            datos = obtenerDatosIniciales();
        }


        private void ir_a_clientes(object sender, EventArgs e)
        {
            
            procedencia = "timbrado Factura";
            Clientes ir_clientes = new Clientes();

            ir_clientes.FormClosed += delegate
            {
                if (Clientes.idClienteParaFacturas > 0)
                {
                    limpiar_campos();
                    llenarCaposDesdeClientes(Clientes.idClienteParaFacturas);
                    Clientes.idClienteParaFacturas = 0;
                }
                else
                {
                    txt_razon_social.Text = datos[0];
                    txt_rfc.Text = datos[1];
                    txt_telefono.Text = datos[2];
                    txt_correo.Text = datos[3];
                    txt_nombre_comercial.Text = datos[4];
                    txt_pais.Text = datos[5];
                    txt_estado.Text = datos[6];
                    txt_municipio.Text = datos[7];
                    txt_localidad.Text = datos[8];
                    txt_cp.Text = datos[9];
                    txt_colonia.Text = datos[10];
                    txt_calle.Text = datos[11];
                    txt_num_ext.Text = datos[12];
                    txt_num_int.Text = datos[13];

                    cmb_bx_forma_pago.Text = datos[14];
                    lb_total_n.Text = datos[15];
                }
            };

            //this.Dispose();
            ir_clientes.llamadoDesdeListadoVentasParaFacturar = 1;
            ir_clientes.ShowDialog();
        }

        private string[] obtenerDatosIniciales()
        {
            List<string> datosIniciales = new List<string>();
            datosIniciales.Add(txt_razon_social.Text);
            datosIniciales.Add(txt_rfc.Text);
            datosIniciales.Add(txt_telefono.Text);
            datosIniciales.Add(txt_correo.Text);
            datosIniciales.Add(txt_nombre_comercial.Text);
            datosIniciales.Add(txt_pais.Text);
            datosIniciales.Add(txt_estado.Text);
            datosIniciales.Add(txt_municipio.Text);
            datosIniciales.Add(txt_localidad.Text);
            datosIniciales.Add(txt_cp.Text);
            datosIniciales.Add(txt_colonia.Text);
            datosIniciales.Add(txt_calle.Text);
            datosIniciales.Add(txt_num_ext.Text);
            datosIniciales.Add(txt_num_int.Text);

            datosIniciales.Add(cmb_bx_forma_pago.Text);
            datosIniciales.Add(lb_total_n.Text);

            return datosIniciales.ToArray();
        }
        private void llenarCaposDesdeClientes (int id)
        {
            pnl_datos_cliente.Visible = true;
            var query = cn.CargarDatos($"SELECT * FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");
            var queryVenta = cn.CargarDatos($"SELECT Total, FormaPago FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{ListadoVentas.obtenerIDVentaTimbrado}'");

            if (!query.Rows.Count.Equals(0))
            {
                //cmb_bx_clientes.Text = query.Rows[0]["RazonSocial"].ToString()+" - "+query.Rows[0]["RFC"].ToString();
                txt_razon_social.Text = query.Rows[0]["RazonSocial"].ToString();
                txt_rfc.Text = query.Rows[0]["RFC"].ToString();
                txt_telefono.Text = query.Rows[0]["Telefono"].ToString();
                txt_correo.Text = query.Rows[0]["Email"].ToString();
                txt_nombre_comercial.Text = query.Rows[0]["NombreComercial"].ToString();
                txt_pais.Text = query.Rows[0]["Pais"].ToString();
                txt_estado.Text = query.Rows[0]["Estado"].ToString();
                txt_municipio.Text = query.Rows[0]["Municipio"].ToString();
                txt_localidad.Text = query.Rows[0]["Localidad"].ToString();
                txt_cp.Text = query.Rows[0]["CodigoPostal"].ToString();
                txt_colonia.Text = query.Rows[0]["Colonia"].ToString();
                txt_calle.Text = query.Rows[0]["Calle"].ToString();
                txt_num_ext.Text = query.Rows[0]["NoExterior"].ToString();
                txt_num_int.Text = query.Rows[0]["NoInterior"].ToString();
                cmb_bx_uso_cfdi.Text = buscarTipoCFDI(query.Rows[0]["UsoCFDI"].ToString());


                //cmb_bx_forma_pago.Text = query.Rows[0]["FormaPago"].ToString();
            }

            
            if (!queryVenta.Rows.Count.Equals(0))
            {
                var totalCantidadVenta = (float)double.Parse(queryVenta.Rows[0]["Total"].ToString());
                lb_total_n.Text = totalCantidadVenta.ToString("0.00");
                var formaPago = queryVenta.Rows[0]["FormaPago"].ToString();

                if (formaPago.Equals("Efectivo"))
                {
                    formaPago = "01 - Efectivo";
                }
                else if (formaPago.Equals("Tarjeta"))
                {
                    formaPago = "04 - Tarjeta de crédito";
                }
                else if (formaPago.Equals("Vales"))
                {
                    formaPago = "08 - Vales de despensa";
                }
                else if (formaPago.Equals("Cheque"))
                {
                    formaPago = "02 - Cheque nominativo";
                }
                else if (formaPago.Equals("Transferencia"))
                {
                    formaPago = "03 - Transferencia electrónica de fondos";
                }

                cmb_bx_forma_pago.Text = formaPago.ToString();
            }
        }

        private string buscarTipoCFDI(string tipo)
        {
            var result = string.Empty;

            if (tipo.Equals("G01"))
            {
                result = "Adquisición de mercancias";
            }
            else if (tipo.Equals("G02"))
            {
                result = "Devoluciones, descuentos o bonificaciones";

            }
            else if (tipo.Equals("G03"))
            {
                result = "Gastos en general";

            }
            else if (tipo.Equals("I01"))
            {
                result = "Construcciones";

            }
            else if (tipo.Equals("I02"))
            {
                result = "Mobilario y equipo de oficina por inversiones";

            }
            else if (tipo.Equals("I03"))
            {
                result = "Equipo de transporte";

            }
            else if (tipo.Equals("I04"))
            {
                result = "Equipo de computo y accesorios";

            }
            else if (tipo.Equals("I05"))
            {
                result = "Dados, troqueles, moldes, matrices y herramental";

            }
            else if (tipo.Equals("I06"))
            {
                result = "Comunicaciones telefónica";

            }
            else if (tipo.Equals("I07"))
            {
                result = "Comunicaciones satelitale";

            }
            else if (tipo.Equals("I08"))
            {
                result = "Otra maquinaria y equipo";

            }
            else if (tipo.Equals("P01"))
            {
                result = "Por definir";

            }

            return result;
        }

        private void sel_clientes(object sender, EventArgs e)
        {
            string clave = cmb_bx_clientes.SelectedValue.ToString();

            clave = clave.Replace("[", string.Empty).Replace("]", string.Empty);

            string[] words = clave.Split(',');

            if (!words[0].Equals("0"))
            {
                limpiar_campos_dcliente();
                pnl_datos_cliente.Visible = true;

                cargar_datos_cliente(Convert.ToInt32(words[0].ToString()));

                btn_facturar.Enabled = true;
            }
            else
            {
                pnl_datos_cliente.Visible = false;
                limpiar_campos_dcliente();

                btn_facturar.Enabled = false;
            }
        }

        private void limpiar_campos_dcliente()
        {
            txt_razon_social.Text = string.Empty;
            txt_rfc.Text = string.Empty;
            txt_telefono.Text = string.Empty;
            txt_correo.Text = string.Empty;
            txt_nombre_comercial.Text = string.Empty;
            txt_pais.Text = string.Empty;
            txt_estado.Text = string.Empty;
            txt_municipio.Text = string.Empty;
            txt_localidad.Text = string.Empty;
            txt_cp.Text = string.Empty;
            txt_colonia.Text = string.Empty;
            txt_calle.Text = string.Empty;
            txt_num_ext.Text = string.Empty;
            txt_num_int.Text = string.Empty;
        }

        private void limpiar_campos()
        {
            cmb_bx_clientes.SelectedIndex = 0;
            pnl_datos_cliente.Visible = false;
            cmb_bx_forma_pago.SelectedIndex = 0;
            cmb_bx_moneda.SelectedIndex = 1;

            //btn_crear_cliente.Enabled = true;
            //btn_crear_cliente.Cursor = Cursors.No;

            txt_cuenta.ReadOnly = true;
            txt_cuenta.Text = string.Empty;
            txt_cuenta.Text = "(Opcional) No. cuenta";
            txt_tipo_cambio.ReadOnly = true;
            txt_tipo_cambio.Text = "1.000000";
            lb_total_n.Text = "0.0";

            limpiar_campos_dcliente();
        }

        private void sel_forma_pago(object sender, EventArgs e)
        {
            string clave = cmb_bx_forma_pago.SelectedValue.ToString();

            mforma_pago(clave);
        }

        private void mforma_pago(string clave)
        {
            if (clave == "02" | clave == "03" | clave == "04" | clave == "28" | clave == "29" | clave == "05" | clave == "06" | clave == "99")
            {
                txt_cuenta.ReadOnly = false;
                txt_cuenta.Text = string.Empty;

                if (clave == "02" | clave == "03") // Dígitos. Cheque y transferencia
                {
                    txt_cuenta.MaxLength = 18;
                }
                if (clave == "04" | clave == "28" | clave == "29") // Dígitos. Tarjeta de crédito, debito y servicios
                {
                    txt_cuenta.MaxLength = 16;
                }
                if (clave == "05") // Alfanumérica. Monedero electrónico
                {
                    txt_cuenta.MaxLength = 50;
                }
                if (clave == "06") // Dígitos. Dinero electrónico
                {
                    txt_cuenta.MaxLength = 10;
                }
                if (clave == "99") // Alfanumérica. Por definir
                {
                    txt_cuenta.MaxLength = 30;
                }
            }
            else
            {
                txt_cuenta.Text = string.Empty;
                txt_cuenta.Text = "(Opcional) No. cuenta";
                txt_cuenta.ReadOnly = true;
            }
        }

        private void tipo_de_datos(object sender, KeyPressEventArgs e)
        {
            string clave = cmb_bx_forma_pago.SelectedValue.ToString();

            if (clave == "02" | clave == "03" | clave == "04" | clave == "28" | clave == "29" | clave == "06") // Dígitos
            {
                if (Char.IsNumber(e.KeyChar) | Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }

            if (clave == "05" | clave == "99") // Alfanumérica
            {
                if(Char.IsLetterOrDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void sel_moneda(object sender, EventArgs e)
        {
            string clave = cmb_bx_moneda.SelectedValue.ToString();

            if(clave == "MXN")
            {
                txt_tipo_cambio.Text = "1.000000";
                txt_tipo_cambio.ReadOnly = true;
            }
            if(clave == "XXX")
            {
                txt_tipo_cambio.Text = string.Empty;
                txt_tipo_cambio.ReadOnly = true;
            }
            if(clave != "MXN" & clave != "XXX")
            {
                txt_tipo_cambio.Text = string.Empty;
                txt_tipo_cambio.ReadOnly = false;
            }
        }

        private void solo_numeros_tcambio(object sender, KeyPressEventArgs e)
        {
            if(Char.IsDigit(e.KeyChar) | Char.IsControl(e.KeyChar) | Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            limpiar_campos();
            this.Dispose();
        }

        private void valida_clave_unidad(object sender, EventArgs e)
        {
            TextBox txt_obtiene_dato = (TextBox)sender;
            var clave_u = txt_obtiene_dato.Text;

            int r= valida_claves_producto_unidad(1, clave_u);

            if(r == 1)
            {
                MessageBox.Show("La clave de unidad es obligatoria, no debe estar vacía.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(r == 2)
            {
                MessageBox.Show("La clave de unidad no existe.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void valida_clave_producto(object sender, EventArgs e)
        {
            TextBox txt_obtiene_dato = (TextBox)sender;
            var clave_p = txt_obtiene_dato.Text;

            int r = valida_claves_producto_unidad(2, clave_p);

            if (r == 1)
            {
                MessageBox.Show("La clave de producto es obligatoria, no debe estar vacía.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (r == 2)
            {
                MessageBox.Show("La clave de producto no existe.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int valida_claves_producto_unidad(int opc, string clave)
        {
            if(opc == 1) // Clave de unidad
            {
                if (clave != "")
                {
                    bool r = (bool)cn.EjecutarSelect($"SELECT * FROM CatalogoUnidadesMedida WHERE ClaveUnidad='{clave}'");

                    if (r == false)
                    {
                        return 2;
                    }
                }
                else
                {
                    return 1;
                }
            }

            if(opc == 2) // Clave de producto
            {
                if (clave != "")
                {
                    bool r = (bool)cn.EjecutarSelect($"SELECT * FROM Catalogo_claves_producto WHERE clave='{clave}'");

                    if (r == false)
                    {
                        return 2;
                    }
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }

        private void solo_numeros_cproducto(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tdatos_cunidad(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void btn_facturar_Click(object sender, EventArgs e)
        {
            // Si se tiene una conexión a internet procede a realizar la factura.
            if (Conexion.ConectadoInternet())
            {
                if (!string.IsNullOrEmpty(txt_rfc.Text) && !string.IsNullOrEmpty(txt_razon_social.Text) && !string.IsNullOrEmpty(cmb_bx_uso_cfdi.Text))
                {
                    // MUESTRA DATOS DE FORMA DE PAGO Y PRODUCTOS

                    if (paso == 1)
                    {
                        tabPage1.Text = "Forma de pago y productos";

                        btn_facturar.Text = "Facturar";
                        btn_anterior.Enabled = true;

                        pnl_datos_cliente.Visible = false;
                        cmb_bx_clientes.Visible = false;
                        btn_crear_cliente.Visible = false;

                        groupb_monto_max.Visible = true;
                        groupb_pago.Visible = true;

                        lb_total.Visible = true;
                        lb_total_n.Visible = true;

                        if (ListadoVentas.faltantes_productos.Length > 0)
                        {
                            if (p_incompletos == 0)
                            {
                                groupb_productos.Visible = false;
                            }
                            else
                            {
                                groupb_productos.Visible = true;
                            }
                        }
                        else
                        {
                            groupb_productos.Visible = false;
                        }
                    }
                    RazonS = txt_razon_social.Text;
                    RFC = txt_rfc.Text;
                    Telefono = txt_telefono.Text;
                    Nombre = txt_nombre_comercial.Text;
                    var CFDI = cmb_bx_uso_cfdi.SelectedValue;
                    Correo = txt_correo.Text;
                    Pais = txt_pais.Text;
                    Estado = txt_estado.Text;
                    Municipio = txt_municipio.Text;
                    Localidad = txt_localidad.Text;
                    CP = txt_cp.Text;
                    Colonia = txt_colonia.Text;
                    Calle = txt_calle.Text;
                    NumInt = txt_num_int.Text;
                    NumExt = txt_num_ext.Text;
                    DateTime FechaHoy = DateTime.Now;
                    string FechaFin = FechaHoy.ToString("yyyy-MM-dd hh:mm:ss");
                    NumeroCliente = GenerarNumeroCliente();


                    // CREAR LA FACTURA

                    if (paso == 2)
                    {
                        // .....................  
                        // .   Validar datos   .
                        // .....................


                        // Cliente

                        int cant_clientes = cmb_bx_clientes.Items.Count;
                        int cliente_valido = 0;

                        if (txt_razon_social.Text.Trim() != "") { cliente_valido++; }

                        if (txt_rfc.Text.Trim() != "") { cliente_valido++; }


                        if (cant_clientes > 0 | cliente_valido == 2)
                        {
                            if (cmb_bx_clientes.SelectedValue.ToString() == "0" & cliente_valido == 0)
                            {
                                MessageBox.Show("Se debe elegir un cliente para poder facturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                botones_visibles(2);
                                return;
                            }
                            else
                            {
                                if (txt_razon_social.Text == "")
                                {
                                    MessageBox.Show("La razón social no debe estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    botones_visibles(2);
                                    return;
                                }

                                if (txt_rfc.Text == "")
                                {
                                    MessageBox.Show("El RCF no debe estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    botones_visibles(2);
                                    return;
                                }
                                else
                                {
                                    string formato_rfc = "^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";

                                    Regex exp = new Regex(formato_rfc);

                                    if (exp.IsMatch(txt_rfc.Text))
                                    {
                                    }
                                    else
                                    {
                                        MessageBox.Show("El formato del RFC no es valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No tiene ningún cliente registrado para facturarle. Primero vaya al apartado de clientes a registrar uno, posterior regresar a timbrar la factura y elegir el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            botones_visibles(2);
                            return;
                        }
                        // Monto máximo de cada factura

                        /*if(txt_cantidad_max.Text.Trim() == "" | txt_cantidad_max.Text.Trim() == "0")
                        {
                            MessageBox.Show("La cantidad máxima en cada factura debe ser mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            botones_visibles(2);
                            return;
                        }*/

                        // Tipo cambio

                        string clave = cmb_bx_moneda.SelectedValue.ToString();

                        if (clave != "MXN" & clave != "XXX")
                        {
                            if (txt_tipo_cambio.Text == "")
                            {
                                MessageBox.Show("El tipo de cambio es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                botones_visibles(2);
                                return;
                            }
                        }

                        // Productos

                        string info_productos = "";
                        int clave_vacia = 0;
                        int clave_inc = 0;

                        if (p_incompletos > 0)
                        {
                            if (n_filas > 0)
                            {
                                foreach (Control panel in pnl_productos.Controls)
                                {
                                    if (panel.Name.Contains("txt_clave_u"))
                                    {
                                        info_productos += "#" + panel.Text;

                                        int r = valida_claves_producto_unidad(1, panel.Text);

                                        if (r == 1) { clave_vacia++; }
                                        if (r == 2) { clave_inc++; }
                                    }
                                    if (panel.Name.Contains("txt_clave_p"))
                                    {
                                        info_productos += "-" + panel.Text;

                                        int r = valida_claves_producto_unidad(2, panel.Text);

                                        if (r == 1) { clave_vacia++; }
                                        if (r == 2) { clave_inc++; }
                                    }
                                    if (panel.Name.Contains("txt_idprod"))
                                    {
                                        info_productos += "-" + panel.Text;
                                    }
                                }
                            }
                            if (clave_vacia > 0)
                            {
                                MessageBox.Show("Las claves de unidad y producto no deben estar vacías.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                botones_visibles(2);
                                return;
                            }
                            if (clave_inc > 0)
                            {
                                MessageBox.Show("Alguna clave de unidad o producto es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                botones_visibles(2);
                                return;
                            }
                        }

                        using (DataTable ConsultaCliente = cn.CargarDatos($"SELECT * FROM clientes WHERE RazonSocial = '{RazonS}' AND RFC = '{RFC}'"))
                        {
                            if (ConsultaCliente.Rows.Count.Equals(0))
                            {
                                cn.EjecutarConsulta($"INSERT INTO clientes ( RazonSocial, RFC, Telefono, NombreComercial, UsoCFDI, Email, Pais, Estado, Municipio, Localidad, CodigoPostal, Colonia, Calle, NoInterior, NoExterior, IDUsuario,NumeroCliente,FechaOperacion)VALUES( '{RazonS}', '{RFC}', '{Telefono}', '{Nombre}', '{CFDI.ToString()}', '{Correo}', '{Pais}', '{Estado}', '{Municipio}', '{Localidad}', '{CP}', '{Colonia}', '{Calle}', '{NumInt}', '{NumExt}', '{FormPrincipal.userID}','{NumeroCliente}','{FechaFin}')");
                            }
                        }


                        // .....................
                        // .   Crear factura   .
                        // .....................


                        bool partir_factura = false;
                        decimal total_venta = 0;
                        decimal monto_xfactura = 0;

                        if (txt_cantidad_max.Text.Trim() != "")
                        {
                            monto_xfactura = Convert.ToDecimal(txt_cantidad_max.Text.Trim());
                        }


                        // Obtiene el total de la venta
                        DataTable d_ventas = cn.CargarDatos(cs.consulta_dventa(1, id_venta));
                        DataRow r_ventas = d_ventas.Rows[0];
                        total_venta = Convert.ToDecimal(r_ventas["Total"].ToString());


                        if (monto_xfactura > 0)
                        {
                            if ((n_filas - 1) == 1)
                            {
                                decimal resp_total_factura = 0;

                                // Primera validación
                                // Busca si hay más de un impuesto
                                bool mas_dimpuesto = (bool)cn.EjecutarSelect($"SELECT * FROM DetallesFacturacionProductos WHERE IDProducto='{ListadoVentas.faltantes_productos[1][1]}'");


                                // Segunda validación
                                resp_total_factura = obtener_productos_a_facturar(monto_xfactura);

                                decimal cantidad_productos = Convert.ToDecimal(ListadoVentas.faltantes_productos[1][5]);
                                decimal unproducto = resp_total_factura / cantidad_productos;

                                if (cantidad_productos > 1 & resp_total_factura > monto_xfactura & unproducto <= monto_xfactura)
                                {
                                    partir_factura = true;
                                }

                                if (excede_montomax_xproducto > 0)
                                {
                                    MessageBox.Show("Alguno de los productos excede el monto máximo establecido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                if (unproducto > monto_xfactura & mas_dimpuesto == false)
                                {
                                    MessageBox.Show("El total de la factura es mayor al monto máximo establecido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                decimal resp_total_factura = obtener_productos_a_facturar(monto_xfactura);

                                if (resp_total_factura > monto_xfactura)
                                {
                                    partir_factura = true;
                                }
                            }
                        }
                        else
                        {
                            partir_factura = false;
                        }

                        if (excede_montomax_xproducto > 0)
                        {
                            MessageBox.Show("Alguno de los productos excede el monto máximo establecido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }



                        // Inicia creación de factura(s)


                        string id_usuario = FormPrincipal.userID.ToString();
                        string id_empleado = FormPrincipal.id_empleado.ToString();
                        List<int> list_id_facturas = new List<int>();

                        botones_visibles(1);


                        // Actualiza datos del cliente en tabla clientes

                        string id_cliente = cmb_bx_clientes.SelectedValue.ToString();
                        string uso_cfdi = cmb_bx_uso_cfdi.SelectedValue.ToString();

                        string[] datos_c = new string[]
                        {
                    id_cliente, txt_razon_social.Text, txt_rfc.Text, txt_telefono.Text, txt_correo.Text, txt_nombre_comercial.Text, txt_pais.Text, txt_estado.Text, txt_municipio.Text, txt_localidad.Text, txt_cp.Text, txt_colonia.Text, txt_calle.Text, txt_num_ext.Text, txt_num_int.Text, uso_cfdi
                        };

                        cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(1, datos_c));


                        //  Guarda clave de unidad y producto, en tabla Productos

                        if (p_incompletos > 0)
                        {
                            string[] filas = info_productos.Split('#');
                            string[] datos_p;

                            for (int f = 1; f < filas.Length; f++)
                            {
                                string[] celda = filas[f].Split('-');

                                datos_p = new string[]
                                {
                        celda[0], celda[1], celda[2]
                                };

                                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(3, datos_p));
                            }
                        }


                        // Consulta datos del emisor
                        DataTable d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, Convert.ToInt32(id_usuario)));
                        DataRow r_emisor = d_emisor.Rows[0];



                        // Aplica para notas donde solo se hará una sola factura

                        #region Se crea tabla Facturas, Facturas_productos, Facturas_impuestos
                        if (partir_factura == false)
                        {
                            // Consulta serie y folio
                            DataTable d_venta = cn.CargarDatos(cs.cargar_datos_venta_xml(9, id_venta, Convert.ToInt32(id_usuario)));
                            DataRow r_venta = d_venta.Rows[0];


                            string[] datos_f = new string[]
                            {
                         id_usuario, id_venta.ToString(), id_empleado, cmb_bx_metodo_pago.SelectedValue.ToString(), cmb_bx_forma_pago.SelectedValue.ToString(), txt_cuenta.Text,
                         cmb_bx_moneda.SelectedValue.ToString(), txt_tipo_cambio.Text, uso_cfdi,
                         r_emisor["RFC"].ToString(), r_emisor["RazonSocial"].ToString(), r_emisor["Regimen"].ToString(), r_emisor["Email"].ToString(), r_emisor["Telefono"].ToString(), r_emisor["CodigoPostal"].ToString(),
                         r_emisor["Estado"].ToString(), r_emisor["Municipio"].ToString(), r_emisor["Colonia"].ToString(), r_emisor["Calle"].ToString(), r_emisor["NoExterior"].ToString(), r_emisor["NoInterior"].ToString(),
                         txt_rfc.Text, txt_razon_social.Text, txt_nombre_comercial.Text, txt_correo.Text, txt_telefono.Text, txt_pais.Text, txt_estado.Text, txt_municipio.Text, txt_localidad.Text, txt_cp.Text, txt_colonia.Text, txt_calle.Text, txt_num_ext.Text, txt_num_int.Text,
                         r_venta["Folio"].ToString(), r_venta["Serie"].ToString(), r_emisor["nombre_comercial"].ToString()
                            };

                            cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(5, datos_f));


                            // Guarda productos en nueva tabla "Facturas productos"

                            // Consulta el ultimo id de tabla facturas
                            int id_factura = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas WHERE id_venta='{id_venta}' ORDER BY ID DESC LIMIT 1", 1));

                            // Busca los productos de la venta
                            DataTable d_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(4, id_venta, 0));

                            if (d_productos.Rows.Count > 0)
                            {
                                foreach (DataRow r_productos in d_productos.Rows)
                                {
                                    int id_p = Convert.ToInt32(r_productos["IDProducto"]);

                                    // Busca los datos restantes en tabla principal de productos
                                    DataTable d_tb_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(5, id_p, 0));
                                    DataRow r_tb_producto = d_tb_productos.Rows[0];


                                    // Obtiene descuento
                                    string descuento_xproducto = "";
                                    string descuento = r_productos["descuento"].ToString();


                                    if (descuento != "" & descuento != "0" & descuento != "0.00")
                                    {
                                        var tip_desc = (descuento).IndexOf("-");

                                        if (tip_desc >= 0)
                                        {
                                            descuento_xproducto = descuento.Substring(tip_desc + 1);
                                        }
                                        else
                                        {
                                            descuento_xproducto = descuento;
                                        }
                                    }

                                    // Realiza calculos para obtener base, precio unitario, IVA
                                    string mbase = "";
                                    string miva = "";
                                    string timpuesto = "";

                                    if (r_tb_producto["Base"].ToString() == "" | r_tb_producto["Impuesto"].ToString() == "")
                                    {
                                        decimal precio = Convert.ToDecimal(r_productos["Precio"].ToString());

                                        decimal b = precio / 1.16m;
                                        decimal bs = precio - b;

                                        mbase = Convert.ToString(dos_seis_decimales(b, 6));
                                        miva = Convert.ToString(dos_seis_decimales(bs, 6));
                                        timpuesto = "16%";
                                    }
                                    else
                                    {
                                        timpuesto = r_tb_producto["Impuesto"].ToString();

                                        decimal precio = Convert.ToDecimal(r_productos["Precio"].ToString());
                                        decimal tasacuota = 0;
                                        decimal b = 0;
                                        decimal bs = 0;

                                        if (timpuesto == "16%" | timpuesto == "8%")
                                        {
                                            if (timpuesto == "16%") { tasacuota = 1.16m; }
                                            if (timpuesto == "8%") { tasacuota = 1.08m; }

                                            b = precio / tasacuota;
                                            bs = precio - b;
                                        }
                                        else
                                        {
                                            b = precio;
                                        }

                                        mbase = Convert.ToString(dos_seis_decimales(b, 6));
                                        miva = Convert.ToString(dos_seis_decimales(bs, 6));
                                    }

                                    // Si el producto tiene descuento, se hará una modificación del valor del campo base
                                    if (descuento_xproducto != "")
                                    {
                                        var tip_desc = (descuento_xproducto).IndexOf("%");
                                        decimal cantidad = Convert.ToDecimal(r_productos["Cantidad"].ToString());
                                        decimal precio_unit = Convert.ToDecimal(r_productos["Precio"].ToString());

                                        if (tip_desc >= 0)
                                        {
                                            // Descuento en porcentaje

                                            string porc_desc = descuento_xproducto.Substring(0, (descuento_xproducto.Length - 1));

                                            decimal porcent_desc = Convert.ToDecimal(porc_desc);

                                            if (porcent_desc > 1)
                                            {
                                                porcent_desc = porcent_desc / 100;
                                            }

                                            decimal desc_encant = precio_unit * porcent_desc;
                                            decimal pu_desc = precio_unit - dos_seis_decimales(desc_encant, 6);
                                            decimal nbase = pu_desc;

                                            if (timpuesto == "16%")
                                            {
                                                nbase = pu_desc / 1.16m;
                                            }
                                            if (timpuesto == "8%")
                                            {
                                                nbase = pu_desc / 1.08m;
                                            }

                                            nbase = dos_seis_decimales(nbase, 6);

                                            mbase = Convert.ToString(nbase);
                                        }
                                        else
                                        {
                                            // Descuento en cantidad $

                                            decimal descuento_xcantidad = Convert.ToDecimal(descuento_xproducto) / cantidad;

                                            decimal pu_desc = precio_unit - dos_seis_decimales(descuento_xcantidad, 6);
                                            decimal nbase = pu_desc;

                                            if (timpuesto == "16%")
                                            {
                                                nbase = pu_desc / 1.16m;
                                            }
                                            if (timpuesto == "8%")
                                            {
                                                nbase = pu_desc / 1.08m;
                                            }

                                            nbase = dos_seis_decimales(nbase, 6);

                                            mbase = Convert.ToString(nbase);
                                        }
                                    }


                                    string[] datos_fp = new string[]
                                    {
                                 id_factura.ToString(), r_tb_producto["UnidadMedida"].ToString(), r_tb_producto["ClaveProducto"].ToString(), r_productos["Nombre"].ToString(), r_productos["Cantidad"].ToString(), r_productos["Precio"].ToString(), mbase, timpuesto, miva, descuento_xproducto.Trim()
                                    };

                                    cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(6, datos_fp));

                                    // Consulta si tiene más de un impuesto
                                    // Si existen, los guarda en nueva tabla Facturas_impuestos
                                    DataTable d_impuestos = cn.CargarDatos(cs.cargar_datos_venta_xml(8, id_p, Convert.ToInt32(id_usuario)));

                                    if (d_impuestos.Rows.Count > 0)
                                    {
                                        // Consulta el ID del producto en curso
                                        int id_fp = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas_productos WHERE id_factura='{id_factura}' ORDER BY ID DESC LIMIT 1", 1));

                                        foreach (DataRow r_impuestos in d_impuestos.Rows)
                                        {
                                            string[] datos_i = new string[]
                                            {
                                         id_fp.ToString(), r_impuestos["Tipo"].ToString(), r_impuestos["Impuesto"].ToString(), r_impuestos["TipoFactor"].ToString(), r_impuestos["TasaCuota"].ToString(), r_impuestos["Definir"].ToString(), r_impuestos["Importe"].ToString()
                                            };

                                            cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(7, datos_i));
                                        }
                                    }
                                }
                            }

                            list_id_facturas.Add(id_factura);
                        }
                        #endregion


                        // Aplica para la nota en donde será facturada en varios documentos

                        #region Se crea tabla Facturas, Facturas_productos, Faccturas_imuestos
                        if (partir_factura == true)
                        {

                            int num_fct = Convert.ToInt32(cantidd_productos);
                            int num_factura = 1;

                            decimal xftotal_suma = 0;
                            decimal xftotal_resta = 0;
                            decimal xftotal_produc = 0;
                            decimal xftotal_total = 0;
                            string[][] arr_productos_xguardar = new string[num_fct][];
                            string[][] arr_productos_pendientes = new string[num_fct][];
                            string[] arr_cantidad_productos = new string[n_filas];
                            List<int> xflist_cantidad_productos = new List<int>();
                            int g = 0;
                            bool inc = false;

                            // Primero se obtiene el número de productos por factura
                            for (int s = 1; s < n_filas; s++)
                            {
                                int cant_producto = Convert.ToInt32(Convert.ToDecimal(arr_dproductos[s][4]));
                                int sum_cant_productos = 0;
                                var indice = 0;
                                var indice_sig = 0;

                                xftotal_suma += Convert.ToDecimal(arr_dproductos[s][6]) + Convert.ToDecimal(arr_dproductos[s][8]);
                                xftotal_suma += Convert.ToDecimal(arr_dproductos[s][10]) + Convert.ToDecimal(arr_dproductos[s][12]);

                                xftotal_resta += Convert.ToDecimal(arr_dproductos[s][9]) + Convert.ToDecimal(arr_dproductos[s][11]);
                                xftotal_resta += Convert.ToDecimal(arr_dproductos[s][13]);

                                xftotal_produc = xftotal_suma - xftotal_resta;


                                for (int c = 0; c < cant_producto; c++)
                                {
                                    xftotal_total += xftotal_produc;


                                    if (xftotal_total <= monto_xfactura)
                                    {
                                        sum_cant_productos++;
                                    }
                                    else
                                    {
                                        xftotal_total = 0;

                                        if (xflist_cantidad_productos.Count > 0)
                                        {
                                            indice_sig++;
                                        }
                                    }

                                    if (xflist_cantidad_productos.Count > 0 & indice == indice_sig)
                                    {
                                        xflist_cantidad_productos.RemoveAt(indice);
                                        xflist_cantidad_productos.Insert(indice, sum_cant_productos);
                                    }

                                    if (xftotal_total == 0)
                                    {
                                        bool no_agrega = false;
                                        /*if((n_filas - 1) == 1)
                                        {
                                            sum_cant_productos = 1;
                                        }*/
                                        if (sum_cant_productos > 0)
                                        {
                                            no_agrega = true;
                                        }
                                        if (sum_cant_productos == 0)
                                        {
                                            sum_cant_productos = 1;
                                            no_agrega = true;
                                        }

                                        xflist_cantidad_productos.Add(sum_cant_productos);

                                        if (cant_producto > 1)
                                        {
                                            sum_cant_productos = 1;
                                        }


                                        xftotal_total += xftotal_produc;
                                        indice = indice_sig;

                                        if (xflist_cantidad_productos.Count > 0 & (cant_producto - 1) == c & no_agrega == false)
                                        {
                                            xflist_cantidad_productos.Add(sum_cant_productos);
                                        }
                                    }
                                }

                                // Si el tamaño del arreglo es igual a 1 signifca que se puede guardar el producto con toda la cantidad de 
                                // productos que tiene agregados 
                                if (xflist_cantidad_productos.Count == 0)
                                {
                                    arr_productos_xguardar[g] = new string[11];

                                    if (inc == true)
                                    {
                                        num_factura++;
                                        inc = false;
                                    }

                                    arr_productos_xguardar[g][0] = "factura" + num_factura;
                                    arr_productos_xguardar[g][1] = arr_dproductos[s][0]; // id producto
                                    arr_productos_xguardar[g][2] = arr_dproductos[s][1]; // clave unidad
                                    arr_productos_xguardar[g][3] = arr_dproductos[s][2]; // clave producto
                                    arr_productos_xguardar[g][4] = arr_dproductos[s][3]; // nombre
                                    arr_productos_xguardar[g][5] = arr_dproductos[s][4]; // cantidad
                                    arr_productos_xguardar[g][6] = arr_dproductos[s][5]; // precio
                                    arr_productos_xguardar[g][7] = arr_dproductos[s][6]; // base
                                    arr_productos_xguardar[g][8] = arr_dproductos[s][7]; // tipo impuesto
                                    arr_productos_xguardar[g][9] = arr_dproductos[s][8]; // cant. IVA
                                    arr_productos_xguardar[g][10] = arr_dproductos[s][9]; // descuento

                                    g++;
                                }

                                // Si el arreglo tiene mas de uno, significa que el producto será repartido en varias facturas cuando la cantidad sobrepase el limite
                                // de igual manera será para cuando se tenga que iniciar la nueva cuenta para una nueva factura
                                if (xflist_cantidad_productos.Count > 0)
                                {
                                    int cant_elementos = xflist_cantidad_productos.Sum();
                                    int xguardar = cant_producto - cant_elementos;

                                    if (xguardar > 0)
                                    {
                                        arr_productos_xguardar[g] = new string[11];

                                        arr_productos_xguardar[g][0] = "factura" + num_factura;
                                        arr_productos_xguardar[g][1] = arr_dproductos[s][0]; // id producto
                                        arr_productos_xguardar[g][2] = arr_dproductos[s][1]; // clave unidad
                                        arr_productos_xguardar[g][3] = arr_dproductos[s][2]; // clave producto
                                        arr_productos_xguardar[g][4] = arr_dproductos[s][3]; // nombre
                                        arr_productos_xguardar[g][5] = xguardar.ToString(); // cantidad
                                        arr_productos_xguardar[g][6] = arr_dproductos[s][5]; // precio
                                        arr_productos_xguardar[g][7] = arr_dproductos[s][6]; // base
                                        arr_productos_xguardar[g][8] = arr_dproductos[s][7]; // tipo impuesto
                                        arr_productos_xguardar[g][9] = arr_dproductos[s][8]; // cant. IVA
                                        arr_productos_xguardar[g][10] = arr_dproductos[s][9]; // descuento

                                        g++;
                                    }

                                    foreach (var cant_p in xflist_cantidad_productos)
                                    {
                                        if (arr_productos_xguardar[0] != null)
                                        {
                                            num_factura++;
                                        }

                                        arr_productos_xguardar[g] = new string[11];

                                        arr_productos_xguardar[g][0] = "factura" + num_factura;
                                        arr_productos_xguardar[g][1] = arr_dproductos[s][0]; // id producto
                                        arr_productos_xguardar[g][2] = arr_dproductos[s][1]; // clave producto
                                        arr_productos_xguardar[g][3] = arr_dproductos[s][2]; // clave unidad
                                        arr_productos_xguardar[g][4] = arr_dproductos[s][3]; // nombre
                                        arr_productos_xguardar[g][5] = cant_p.ToString(); // cantidad
                                        arr_productos_xguardar[g][6] = arr_dproductos[s][5]; // precio
                                        arr_productos_xguardar[g][7] = arr_dproductos[s][6]; // base
                                        arr_productos_xguardar[g][8] = arr_dproductos[s][7]; // tipo impuesto
                                        arr_productos_xguardar[g][9] = arr_dproductos[s][8]; // cant. IVA
                                        arr_productos_xguardar[g][10] = arr_dproductos[s][9]; // descuento

                                        g++;
                                        inc = true;
                                    }

                                    xflist_cantidad_productos.Clear();
                                    indice = 0;
                                    indice_sig = 0;
                                    xftotal_total = 0;
                                }

                                xftotal_suma = 0;
                                xftotal_resta = 0;

                                /*Console.WriteLine("----------------------------------------");
                                Console.WriteLine("[nf]" + arr_productos_xguardar[g - 1][0]);
                                Console.WriteLine("[n]" + arr_productos_xguardar[g - 1][4]);
                                Console.WriteLine("[c]" + arr_productos_xguardar[g - 1][5]);
                                Console.WriteLine("----------------------------------------");*/
                            }


                            // Inicia creación de facturas
                            for (var nf = 1; nf <= num_factura; nf++)
                            {
                                string numero_factura = "factura" + nf;

                                // Consulta serie y folio
                                DataTable d_venta = cn.CargarDatos(cs.cargar_datos_venta_xml(9, id_venta, Convert.ToInt32(id_usuario)));
                                DataRow r_venta = d_venta.Rows[0];


                                string no_cuenta = txt_cuenta.Text;

                                if (txt_cuenta.Text == "(Opcional) No. cuenta")
                                {
                                    no_cuenta = "";
                                }

                                string[] datos_f = new string[]
                                {
                            id_usuario, id_venta.ToString(), id_empleado, cmb_bx_metodo_pago.SelectedValue.ToString(), cmb_bx_forma_pago.SelectedValue.ToString(), no_cuenta,
                            cmb_bx_moneda.SelectedValue.ToString(), txt_tipo_cambio.Text, uso_cfdi,
                            r_emisor["RFC"].ToString(), r_emisor["RazonSocial"].ToString(), r_emisor["Regimen"].ToString(), r_emisor["Email"].ToString(), r_emisor["Telefono"].ToString(), r_emisor["CodigoPostal"].ToString(),
                            r_emisor["Estado"].ToString(), r_emisor["Municipio"].ToString(), r_emisor["Colonia"].ToString(), r_emisor["Calle"].ToString(), r_emisor["NoExterior"].ToString(), r_emisor["NoInterior"].ToString(),
                            txt_rfc.Text, txt_razon_social.Text, txt_nombre_comercial.Text, txt_correo.Text, txt_telefono.Text, txt_pais.Text, txt_estado.Text, txt_municipio.Text, txt_localidad.Text, txt_cp.Text, txt_colonia.Text, txt_calle.Text, txt_num_ext.Text, txt_num_int.Text,
                            r_venta["Folio"].ToString(), r_venta["Serie"].ToString(), r_emisor["nombre_comercial"].ToString()
                                };

                                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(5, datos_f));

                                // Consulta el ultimo id de tabla facturas
                                int id_factura = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas WHERE id_venta='{id_venta}' ORDER BY ID DESC LIMIT 1", 1));



                                // Guarda productos en nueva tabla "Facturas productos"

                                for (int w = 0; w < arr_productos_xguardar.Length; w++)
                                {
                                    if (arr_productos_xguardar[w] != null)
                                    {
                                        if (numero_factura == arr_productos_xguardar[w][0])
                                        {
                                            string[] datos_fp = new string[]
                                            {
                                    id_factura.ToString(), arr_productos_xguardar[w][3], arr_productos_xguardar[w][2], arr_productos_xguardar[w][4], arr_productos_xguardar[w][5],
                                    arr_productos_xguardar[w][6], arr_productos_xguardar[w][7], arr_productos_xguardar[w][8], arr_productos_xguardar[w][9], arr_productos_xguardar[w][10]
                                            };

                                            cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(6, datos_fp));


                                            // Consulta si tiene más de un impuesto
                                            // Si existen, los guarda en nueva tabla Facturas_impuestos
                                            DataTable d_impuestos = cn.CargarDatos(cs.cargar_datos_venta_xml(8, Convert.ToInt32(arr_productos_xguardar[w][1]), Convert.ToInt32(id_usuario)));

                                            if (d_impuestos.Rows.Count > 0)
                                            {
                                                // Consulta el ID del producto en curso
                                                int id_fp = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas_productos WHERE id_factura='{id_factura}' ORDER BY ID DESC LIMIT 1", 1));

                                                foreach (DataRow r_impuestos in d_impuestos.Rows)
                                                {
                                                    string[] datos_i = new string[]
                                                    {
                                            id_fp.ToString(), r_impuestos["Tipo"].ToString(), r_impuestos["Impuesto"].ToString(), r_impuestos["TipoFactor"].ToString(), r_impuestos["TasaCuota"].ToString(), r_impuestos["Definir"].ToString(), r_impuestos["Importe"].ToString()
                                                    };

                                                    cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(7, datos_i));
                                                }
                                            }
                                        }
                                    }

                                }


                                list_id_facturas.Add(id_factura);
                            }
                        }
                        #endregion






                        // ........................................ 
                        // .   Llamar al timbrado de la factura   .
                        // ........................................        

                        //Loading load = new Loading();
                        //load.Show();
                        decimal[][] vacio = new decimal[][] { };
                        int exito = 0;
                        int error = 0;
                        string mnsjerror = "";

                       
                        Generar_XML xml = new Generar_XML();
                        
                        foreach (var id_facturaa in list_id_facturas)
                        {
                            try
                            {
                                string respuesta_xml = xml.obtener_datos_XML(id_facturaa, id_venta, 0, vacio);

                                if (respuesta_xml == "")
                                {
                                    exito++;
                                }
                                else
                                {
                                    error++;
                                    mnsjerror += respuesta_xml + "\n";
                                }
                            }
                            catch(Exception ex)
                            {
                                var r= MessageBox.Show("Ocurrio un error al momento de timbrar y/o generar su(s) factura(s). \n Le invitamos a tomar en cuenta los siguientes puntos: \n - Contar con conexión a internet durante el proceso de timbrado. \n - Sus archivos digitales no deben estar caducos.  \n - Sus archivos CSD deben haberse subido correctamente. \n - Sus CSD deben encontrarse en la lista LCO. \n\n " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                if (r == DialogResult.OK)
                                {
                                    this.Dispose();
                                }                                
                            }
                            
                        }


                        if (exito > 0 & error == 0)
                        {
                            lb_facturando.Visible = false;

                            var r = MessageBox.Show("Su(s) factura(s) han sido \ncreadas y timbradas con éxito.", "Aviso del Sistema", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                            if (r == DialogResult.OK)
                            {
                                this.Dispose();
                            }
                        }
                        if (error > 0 & exito == 0)
                        {
                            botones_visibles(2);
                            MessageBox.Show(mnsjerror, "Error", MessageBoxButtons.OK);
                        }
                        if (error > 0 & exito > 0)
                        {
                            lb_facturando.Visible = false;

                            var r = MessageBox.Show("Factura(s) creadas con éxito. \n Algunas facturas fueron timbradas con éxito, pero hubo un error en alguna de ellas. \n Error: \n " + mnsjerror, "Advertencia", MessageBoxButtons.OK);

                            if (r == DialogResult.OK)
                            {
                                this.Dispose();
                            }
                        }
                    }


                    if (paso == 1) { paso = 2; }
                }
                else
                {
                    MessageBox.Show("Complete los campos obligatorios para continuar.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Sin conexión a internet. Esta accción requiere una conexión.", "", MessageBoxButtons.OK);
            }
        }

        private string GenerarNumeroCliente()
        {
            var auxiliar = mb.ObtenerNumeroCliente();

            if (string.IsNullOrWhiteSpace(auxiliar))
            {
                auxiliar = "0";
            }
            else
            {
                auxiliar = auxiliar.TrimStart('0');
            }

            var numero = Convert.ToInt16(auxiliar) + 1;

            return numero.ToString("D6");
        }

        public void botones_visibles(int opc)
        {
            if(opc == 1) // Oculta botones Cancelar y Facturar
            {
                btn_cancelar.Visible = false;
                btn_facturar.Visible = false;
                btn_anterior.Visible = false;
                lb_facturando.Visible = true;
                //btn_facturando.Visible = true;
            }
            if(opc == 2) // Oculta botón Facturando
            {
                lb_facturando.Visible = false;
                //btn_facturando.Visible = false;
                btn_cancelar.Visible = true;
                btn_facturar.Visible = true;
                btn_anterior.Visible = true;
            }
        }

        private void btn_anterior_Click(object sender, EventArgs e)
        {
            if(paso == 2)
            {
                tabPage1.Text = "Cliente";

                btn_facturar.Text = "Siguiente";
                btn_anterior.Enabled = false;

                pnl_datos_cliente.Visible = true;
                cmb_bx_clientes.Visible = true;
                btn_crear_cliente.Visible = true;

                groupb_monto_max.Visible = false;
                groupb_pago.Visible = false;
                groupb_productos.Visible = false;

                lb_total.Visible = false;
                lb_total_n.Visible = false;

                paso = 1;
            }
        }

        private decimal dos_seis_decimales(decimal c, int cant)
        {
            decimal cantidad = Decimal.Round(c, cant);

            return cantidad;
        }
        
        private void encampo_cuenta(object sender, EventArgs e)
        {
            if (txt_cuenta.Text == "(Opcional) No. cuenta")
            {
                txt_cuenta.Text = "";
            }
        }

        private void scampo_cuenta(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_cuenta.Text))
            {
                txt_cuenta.Text = "(Opcional) No. cuenta";
            }
        }

        private void cargar_datos_cliente(int clave)
        {
            DataTable d_datos_clientes = cn.CargarDatos(cs.cargar_datos_venta_xml(3, clave, 0));
            DataRow r_datos_clientes = d_datos_clientes.Rows[0];

            txt_razon_social.Text = r_datos_clientes["RazonSocial"].ToString();
            txt_rfc.Text = r_datos_clientes["RFC"].ToString();
            txt_telefono.Text = r_datos_clientes["Telefono"].ToString();
            txt_correo.Text = r_datos_clientes["Email"].ToString();
            txt_nombre_comercial.Text = r_datos_clientes["NombreComercial"].ToString();
            txt_pais.Text = r_datos_clientes["Pais"].ToString();
            txt_estado.Text = r_datos_clientes["Estado"].ToString();
            txt_municipio.Text = r_datos_clientes["Municipio"].ToString();
            txt_localidad.Text = r_datos_clientes["Localidad"].ToString();
            txt_cp.Text = r_datos_clientes["CodigoPostal"].ToString();
            txt_colonia.Text = r_datos_clientes["Colonia"].ToString();
            txt_calle.Text = r_datos_clientes["Calle"].ToString();
            txt_num_ext.Text = r_datos_clientes["NoExterior"].ToString();
            txt_num_int.Text = r_datos_clientes["NoInterior"].ToString();


            Dictionary<string, string> usoCFDI = new Dictionary<string, string>();
            usoCFDI.Add("G01", "Adquisición de mercancias");
            usoCFDI.Add("G02", "Devoluciones, descuentos o bonificaciones");
            usoCFDI.Add("G03", "Gastos en general");
            usoCFDI.Add("I01", "Construcciones");
            usoCFDI.Add("I02", "Mobilario y equipo de oficina por inversiones");
            usoCFDI.Add("I03", "Equipo de transporte");
            usoCFDI.Add("I04", "Equipo de computo y accesorios");
            usoCFDI.Add("I05", "Dados, troqueles, moldes, matrices y herramental");
            usoCFDI.Add("I06", "Comunicaciones telefónica");
            usoCFDI.Add("I07", "Comunicaciones satelitale");
            usoCFDI.Add("I08", "Otra maquinaria y equipo");
            usoCFDI.Add("P01", "Por definir");

            cmb_bx_uso_cfdi.DataSource = usoCFDI.ToArray();
            cmb_bx_uso_cfdi.DisplayMember = "Value";
            cmb_bx_uso_cfdi.ValueMember = "Key";
            cmb_bx_uso_cfdi.SelectedValue = r_datos_clientes["UsoCFDI"];
        }

        private decimal obtener_productos_a_facturar(decimal monto_max)
        {
            arr_dproductos = new string[n_filas][];

            decimal vtotal_base = 0;
            decimal vtotal_ivag = 0;
            decimal vtotal_descuento = 0;
            decimal vtotal_traslado = 0;
            decimal vtotal_retencion = 0;
            decimal vtotal_ltraslado = 0;
            decimal vtotal_lretenido = 0;
            excede_montomax_xproducto = 0;


            for (var z = 1; z < n_filas; z++)
            {
                decimal vprecio = Convert.ToDecimal(ListadoVentas.faltantes_productos[z][6]);
                decimal vcantidad = Convert.ToDecimal(ListadoVentas.faltantes_productos[z][5]);
                decimal vtotal = vprecio * vcantidad;
                decimal vbase = Convert.ToDecimal(ListadoVentas.faltantes_productos[z][9]);
                string vimpuesto = ListadoVentas.faltantes_productos[z][10];
                string vdescuento = ListadoVentas.faltantes_productos[z][7];

                string vdescuento_xproducto = "";
                decimal vtotal_xp_traslado = 0;
                decimal vtotal_xp_retencion = 0;
                decimal vtotal_xp_ltraslado = 0;
                decimal vtotal_xp_lretenido = 0;


                // Comprueba si tiene descuento
                if (vdescuento != "" & vdescuento != "0" & vdescuento != "0.00")
                {
                    var tip_desc = (vdescuento).IndexOf("-");

                    if (tip_desc >= 0)
                    {
                        vdescuento_xproducto = vdescuento.Substring(tip_desc + 1);
                    }
                    else
                    {
                        vdescuento_xproducto = vdescuento;
                    }
                }

                // Realiza calculos para obtener base, precio unitario, IVA
                string mbase = "";
                string miva = "";
                string timpuesto = "";

                if (vbase.ToString() == "" | vimpuesto == "")
                {
                    decimal b = vprecio / 1.16m;
                    decimal bs = vprecio - b;

                    mbase = Convert.ToString(dos_seis_decimales(b, 6));
                    miva = Convert.ToString(dos_seis_decimales(bs, 6));
                    timpuesto = "16%";
                }
                else
                {
                    timpuesto = vimpuesto;

                    decimal tasacuota = 0;
                    decimal b = 0;
                    decimal bs = 0;

                    if (timpuesto == "16%" | timpuesto == "8%")
                    {
                        if (timpuesto == "16%") { tasacuota = 1.16m; }
                        if (timpuesto == "8%") { tasacuota = 1.08m; }

                        b = vprecio / tasacuota;
                        bs = vprecio - b;
                    }
                    else
                    {
                        b = vprecio;
                    }

                    mbase = Convert.ToString(dos_seis_decimales(b, 6));
                    miva = Convert.ToString(dos_seis_decimales(bs, 6));
                }

                // Si el producto tiene descuento, se hará una modificación del valor del campo base
                if (vdescuento_xproducto != "")
                {
                    var tip_desc = (vdescuento_xproducto).IndexOf("%");

                    if (tip_desc >= 0)
                    {
                        // Descuento en porcentaje

                        string porc_desc = vdescuento_xproducto.Substring(0, (vdescuento_xproducto.Length - 1));

                        decimal porcent_desc = Convert.ToDecimal(porc_desc);

                        if (porcent_desc > 1)
                        {
                            porcent_desc = porcent_desc / 100;
                        }

                        decimal desc_encant = vprecio * porcent_desc;
                        decimal pu_desc = vprecio - dos_seis_decimales(desc_encant, 6);
                        decimal nbase = pu_desc;

                        if (timpuesto == "16%")
                        {
                            nbase = pu_desc / 1.16m;
                        }
                        if (timpuesto == "8%")
                        {
                            nbase = pu_desc / 1.08m;
                        }

                        nbase = dos_seis_decimales(nbase, 6);

                        mbase = Convert.ToString(nbase);
                    }
                    else
                    {
                        // Descuento en cantidad $

                        decimal descuento_xcantidad = Convert.ToDecimal(vdescuento_xproducto) / vcantidad;

                        decimal pu_desc = vprecio - dos_seis_decimales(descuento_xcantidad, 2);
                        decimal nbase = pu_desc;

                        if (timpuesto == "16%")
                        {
                            nbase = pu_desc / 1.16m;
                        }
                        if (timpuesto == "8%")
                        {
                            nbase = pu_desc / 1.08m;
                        }

                        nbase = dos_seis_decimales(nbase, 6);

                        mbase = Convert.ToString(nbase);
                    }
                }


                // Guardamos los datos para usarlos al momento de guardar en la BD 
                // y no volver a hacer todo el procedimiento

                arr_dproductos[z] = new string[15];

                arr_dproductos[z][0] = ListadoVentas.faltantes_productos[z][1]; //id producto
                arr_dproductos[z][1] = ListadoVentas.faltantes_productos[z][2]; // clave p
                arr_dproductos[z][2] = ListadoVentas.faltantes_productos[z][3]; // clave u
                arr_dproductos[z][3] = ListadoVentas.faltantes_productos[z][4]; // nombre
                arr_dproductos[z][4] = ListadoVentas.faltantes_productos[z][5]; // cantidad
                arr_dproductos[z][5] = ListadoVentas.faltantes_productos[z][6]; // precio
                arr_dproductos[z][6] = mbase;
                arr_dproductos[z][7] = timpuesto;
                arr_dproductos[z][8] = miva;
                arr_dproductos[z][9] = vdescuento_xproducto.Trim();

                if (vdescuento_xproducto.Trim() == "")
                {
                    arr_dproductos[z][9] = "0";
                }

                

                // Buscamos si hay más impuestos

                int idp = Convert.ToInt32(ListadoVentas.faltantes_productos[z][1]);


                DataTable d_otros_impuestos = cn.CargarDatos(cs.cargar_datos_venta_xml(8, idp, FormPrincipal.userID));

                if (d_otros_impuestos.Rows.Count > 0)
                {
                    foreach (DataRow r_otros_impuestos in d_otros_impuestos.Rows)
                    {
                        // Traslado y retención

                        if (r_otros_impuestos["Tipo"].ToString() == "Traslado" | r_otros_impuestos["Tipo"].ToString() == "Retención")
                        {
                            if (r_otros_impuestos["TipoFactor"].ToString() == "Tasa")
                            {

                                decimal vporcentaje = 0;

                                // Obtener el porcentaje
                                if (r_otros_impuestos["TasaCuota"].ToString() == "Definir %")
                                {
                                    vporcentaje = Convert.ToDecimal(r_otros_impuestos["Definir"]);

                                    if (Convert.ToDecimal(r_otros_impuestos["Definir"]) > 1)
                                    {
                                        vporcentaje = Convert.ToDecimal(r_otros_impuestos["Definir"]) / 100;
                                    }
                                }
                                else
                                {
                                    vporcentaje = Convert.ToDecimal(r_otros_impuestos["TasaCuota"]);

                                    if (Convert.ToDecimal(r_otros_impuestos["TasaCuota"]) > 1)
                                    {
                                        vporcentaje = Convert.ToDecimal(r_otros_impuestos["TasaCuota"]) / 100;
                                    }
                                }

                                // Obtener el monto del impuesto
                                decimal vmonto_impuesto = Convert.ToDecimal(mbase) * vporcentaje;

                                // Sumarlo al traslado o retenido
                                if (r_otros_impuestos["Tipo"].ToString() == "Traslado")
                                {
                                    vtotal_traslado += dos_seis_decimales(vmonto_impuesto, 2);
                                }
                                if (r_otros_impuestos["Tipo"].ToString() == "Retención")
                                {
                                    vtotal_retencion += dos_seis_decimales(vmonto_impuesto, 2);
                                }
                            }

                            if (r_otros_impuestos["TipoFactor"].ToString() == "Cuota")
                            {
                                // Sumarlo al traslado o retenido
                                if (r_otros_impuestos["Tipo"].ToString() == "Traslado")
                                {
                                    vtotal_traslado += dos_seis_decimales(vcantidad * Convert.ToDecimal(r_otros_impuestos["Definir"].ToString()), 2);
                                    vtotal_xp_traslado += dos_seis_decimales(vcantidad * Convert.ToDecimal(r_otros_impuestos["Definir"].ToString()), 2);
                                }
                                if (r_otros_impuestos["Tipo"].ToString() == "Retención")
                                {
                                    vtotal_retencion += dos_seis_decimales(vcantidad * Convert.ToDecimal(r_otros_impuestos["Definir"].ToString()), 2);
                                    vtotal_xp_retencion += dos_seis_decimales(vcantidad * Convert.ToDecimal(r_otros_impuestos["Definir"].ToString()), 2);
                                }
                            }
                        }


                        // Impuestos locales retenidos y traslados

                        if (r_otros_impuestos["Tipo"].ToString() == "Loc. Traslado" | r_otros_impuestos["Tipo"].ToString() == "Loc. Retenido")
                        {
                            decimal vporcentaje = 0;

                            // Obtener el porcentaje
                            if (r_otros_impuestos["TasaCuota"].ToString() == "Definir %")
                            {
                                vporcentaje = Convert.ToDecimal(r_otros_impuestos["Definir"]);

                                if (Convert.ToDecimal(r_otros_impuestos["Definir"]) > 1)
                                {
                                    vporcentaje = Convert.ToDecimal(r_otros_impuestos["Definir"]) / 100;
                                }
                            }
                            else
                            {
                                vporcentaje = Convert.ToDecimal(r_otros_impuestos["TasaCuota"]);

                                if (Convert.ToDecimal(r_otros_impuestos["TasaCuota"]) > 1)
                                {
                                    vporcentaje = Convert.ToDecimal(r_otros_impuestos["TasaCuota"]) / 100;
                                }
                            }

                            // Obtener la monto del impuesto
                            decimal vmonto_impuesto = Convert.ToDecimal(mbase) * vporcentaje;

                            // Hacer la sumatoria
                            if (r_otros_impuestos["Tipo"].ToString() == "Loc. Traslado")
                            {
                                vtotal_ltraslado += dos_seis_decimales((vmonto_impuesto * vcantidad), 2);
                                vtotal_xp_ltraslado += dos_seis_decimales((vmonto_impuesto * vcantidad), 2);
                            }
                            if (r_otros_impuestos["Tipo"].ToString() == "Loc. Retenido")
                            {
                                vtotal_lretenido += dos_seis_decimales((vmonto_impuesto * vcantidad), 2);
                                vtotal_xp_lretenido += dos_seis_decimales((vmonto_impuesto * vcantidad), 2);
                            }
                        }
                    }


                    arr_dproductos[z][10] = vtotal_xp_traslado.ToString();
                    arr_dproductos[z][11] = vtotal_xp_retencion.ToString();
                    arr_dproductos[z][12] = vtotal_xp_ltraslado.ToString();
                    arr_dproductos[z][13] = vtotal_xp_lretenido.ToString();

                    /*if (vtotal_xp_retencion == 0)
                    {
                        arr_dproductos[z][11] = "0";
                    }    arr_dproductos[z][13] = "0";
                    }*/

                    vtotal_xp_traslado = 0;
                    vtotal_xp_retencion = 0;
                    vtotal_xp_ltraslado = 0;
                    vtotal_xp_lretenido = 0;
                }
                else
                {
                    arr_dproductos[z][10] = "0";
                    arr_dproductos[z][11] = "0";
                    arr_dproductos[z][12] = "0";
                    arr_dproductos[z][13] = "0";
                }



                vtotal_base += Convert.ToDecimal(mbase) * vcantidad;
                vtotal_ivag += Convert.ToDecimal(miva) * vcantidad;

                if (vdescuento_xproducto.Trim() != "")
                {
                    vtotal_descuento += Convert.ToDecimal(vdescuento_xproducto.Trim()) * vcantidad;
                }


                // Valida que el precio unitario del producto no exceda la cantidad máxima a facturar

                if(vdescuento_xproducto.Trim() == "")
                {
                    vdescuento_xproducto = "0";
                }

                decimal vt_suma = Convert.ToDecimal(mbase) + Convert.ToDecimal(miva) + vtotal_xp_traslado + vtotal_xp_ltraslado;

                decimal vt_resta = Convert.ToDecimal(vdescuento_xproducto) + vtotal_xp_retencion + vtotal_xp_lretenido;

                decimal vt_xproducto = (vt_suma - vt_resta) / vcantidad;

                if(vt_xproducto > monto_max)
                {
                    excede_montomax_xproducto++;
                }
            }

            decimal vtotal_suma = vtotal_base + vtotal_ivag + vtotal_traslado + vtotal_ltraslado;
            decimal vtotal_resta = vtotal_descuento + vtotal_retencion + vtotal_lretenido;
            decimal vtotal_factura = vtotal_suma - vtotal_resta;


            return vtotal_factura;
        }

        private void solo_numeros_facturamax(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) | Char.IsControl(e.KeyChar) | Char.IsPunctuation(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        
        private void cmb_bx_clientes_TextChanged(object sender, EventArgs e)
        {
            if (cmb_bx_clientes.Text.Equals("") || !cmb_bx_clientes.Text.Equals(""))
            {
                cmb_bx_clientes.DroppedDown = false;
            }
        }
        


        /*private void btn_facturar_Click(object sender, EventArgs e)
        {
            // MUESTRA DATOS DE FORMA DE PAGO Y PRODUCTOS

            if (paso == 1)
            {
                tabPage1.Text = "Forma de pago y productos";

                btn_facturar.Text = "Facturar";
                btn_anterior.Enabled = true;

                pnl_datos_cliente.Visible = false;
                cmb_bx_clientes.Visible = false;
                btn_crear_cliente.Visible = false;

                groupb_pago.Visible = true;

                if (ListadoVentas.faltantes_productos.Length > 0)
                {
                    if (p_incompletos == 0)
                    {
                        groupb_productos.Visible = false;
                    }
                    else
                    {
                        groupb_productos.Visible = true;
                    }
                }
                else
                {
                    groupb_productos.Visible = false;
                }
            }


            // CREAR LA FACTURA

            if (paso == 2)
            {
                // .....................  
                // .   Validar datos   .
                // .....................


                // Cliente

                int cant_clientes = cmb_bx_clientes.Items.Count;
                int cliente_valido = 0;

                if (txt_razon_social.Text.Trim() != "") { cliente_valido++; }

                if (txt_rfc.Text.Trim() != "") { cliente_valido++; }


                if (cant_clientes > 0 | cliente_valido == 2)
                {
                    if (cmb_bx_clientes.SelectedValue.ToString() == "0" & cliente_valido == 0)
                    {
                        MessageBox.Show("Se debe elegir un cliente para poder facturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        botones_visibles(2);
                        return;
                    }
                    else
                    {
                        if (txt_razon_social.Text == "")
                        {
                            MessageBox.Show("La razón social no debe estar vacía.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            botones_visibles(2);
                            return;
                        }

                        if (txt_rfc.Text == "")
                        {
                            MessageBox.Show("El RCF no debe estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            botones_visibles(2);
                            return;
                        }
                        else
                        {
                            string formato_rfc = "^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";

                            Regex exp = new Regex(formato_rfc);

                            if (exp.IsMatch(txt_rfc.Text))
                            {
                            }
                            else
                            {
                                MessageBox.Show("El formato del RFC no es valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tiene ningún cliente registrado para facturarle. Primero vaya al apartado de clientes a registrar uno, posterior regresar a timbrar la factura y elegir el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    botones_visibles(2);
                    return;
                }

                // Tipo cambio

                string clave = cmb_bx_moneda.SelectedValue.ToString();

                if (clave != "MXN" & clave != "XXX")
                {
                    if (txt_tipo_cambio.Text == "")
                    {
                        MessageBox.Show("El tipo de cambio es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        botones_visibles(2);
                        return;
                    }
                }

                // Productos

                string info_productos = "";
                int clave_vacia = 0;
                int clave_inc = 0;

                if (p_incompletos > 0)
                {
                    if (n_filas > 0)
                    {
                        foreach (Control panel in pnl_productos.Controls)
                        {
                            if (panel.Name.Contains("txt_clave_u"))
                            {
                                info_productos += "#" + panel.Text;

                                int r = valida_claves_producto_unidad(1, panel.Text);

                                if (r == 1) { clave_vacia++; }
                                if (r == 2) { clave_inc++; }
                            }
                            if (panel.Name.Contains("txt_clave_p"))
                            {
                                info_productos += "-" + panel.Text;

                                int r = valida_claves_producto_unidad(2, panel.Text);

                                if (r == 1) { clave_vacia++; }
                                if (r == 2) { clave_inc++; }
                            }
                            if (panel.Name.Contains("txt_idprod"))
                            {
                                info_productos += "-" + panel.Text;
                            }
                        }
                    }
                    if (clave_vacia > 0)
                    {
                        MessageBox.Show("Las claves de unidad y producto no deben estar vacías.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        botones_visibles(2);
                        return;
                    }
                    if (clave_inc > 0)
                    {
                        MessageBox.Show("Alguna clave de unidad o producto es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        botones_visibles(2);
                        return;
                    }
                }



                // .....................
                // .   Crear factura   .
                // .....................

                string id_usuario = FormPrincipal.userID.ToString();
                string id_empleado = FormPrincipal.id_empleado.ToString();

                botones_visibles(1);


                // Actualiza datos del cliente en tabla clientes

                string id_cliente = cmb_bx_clientes.SelectedValue.ToString();
                string uso_cfdi = cmb_bx_uso_cfdi.SelectedValue.ToString();

                string[] datos_c = new string[]
                {
                id_cliente, txt_razon_social.Text, txt_rfc.Text, txt_telefono.Text, txt_correo.Text, txt_nombre_comercial.Text, txt_pais.Text, txt_estado.Text, txt_municipio.Text, txt_localidad.Text, txt_cp.Text, txt_colonia.Text, txt_calle.Text, txt_num_ext.Text, txt_num_int.Text, uso_cfdi
                };

                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(1, datos_c));


                // Guarda datos cliente, sección forma de pago en nueva tabla (Facturas)

                // Consulta datos del emisor
                DataTable d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, Convert.ToInt32(id_usuario)));
                DataRow r_emisor = d_emisor.Rows[0];
                // Consulta serie y folio
                DataTable d_venta = cn.CargarDatos(cs.cargar_datos_venta_xml(9, id_venta, Convert.ToInt32(id_usuario)));
                DataRow r_venta = d_venta.Rows[0];


                string[] datos_f = new string[]
                {
                    id_usuario, id_venta.ToString(), id_empleado, cmb_bx_metodo_pago.SelectedValue.ToString(), cmb_bx_forma_pago.SelectedValue.ToString(), txt_cuenta.Text,
                    cmb_bx_moneda.SelectedValue.ToString(), txt_tipo_cambio.Text, uso_cfdi,
                    r_emisor["RFC"].ToString(), r_emisor["RazonSocial"].ToString(), r_emisor["Regimen"].ToString(), r_emisor["Email"].ToString(), r_emisor["Telefono"].ToString(), r_emisor["CodigoPostal"].ToString(),
                    r_emisor["Estado"].ToString(), r_emisor["Municipio"].ToString(), r_emisor["Colonia"].ToString(), r_emisor["Calle"].ToString(), r_emisor["NoExterior"].ToString(), r_emisor["NoInterior"].ToString(),
                    txt_rfc.Text, txt_razon_social.Text, txt_nombre_comercial.Text, txt_correo.Text, txt_telefono.Text, txt_pais.Text, txt_estado.Text, txt_municipio.Text, txt_localidad.Text, txt_cp.Text, txt_colonia.Text, txt_calle.Text, txt_num_ext.Text, txt_num_int.Text,
                    r_venta["Folio"].ToString(), r_venta["Serie"].ToString()
                };

                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(5, datos_f));


                //  Guarda clave de unidad y producto, en tabla Productos

                if (p_incompletos > 0)
                {
                    string[] filas = info_productos.Split('#');
                    string[] datos_p;

                    for (int f = 1; f < filas.Length; f++)
                    {
                        string[] celda = filas[f].Split('-');

                        datos_p = new string[]
                        {
                        celda[0], celda[1], celda[2]
                        };

                        cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(3, datos_p));
                    }
                }


                // Guarda produtos en nueva tabla "Facturas productos"

                // Consulta el ultimo id de tabla facturas
                int id_factura = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas WHERE id_venta='{id_venta}' ORDER BY ID DESC LIMIT 1", 1));

                // Busca los productos de la venta
                DataTable d_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(4, id_venta, 0));

                if (d_productos.Rows.Count > 0)
                {
                    foreach (DataRow r_productos in d_productos.Rows)
                    {
                        int id_p = Convert.ToInt32(r_productos["IDProducto"]);

                        // Busca los datos restantes en tabla principal de productos
                        DataTable d_tb_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(5, id_p, 0));
                        DataRow r_tb_producto = d_tb_productos.Rows[0];


                        // Obtiene descuento
                        string descuento_xproducto = "";
                        string descuento = r_productos["descuento"].ToString();

                        //if (Convert.ToDecimal(descuento) > 0) //!= ""
                        if (descuento != "" & descuento != "0" & descuento != "0.00")
                        {
                            var tip_desc = (descuento).IndexOf("-");

                            if (tip_desc >= 0)
                            {
                                descuento_xproducto = descuento.Substring(tip_desc + 1);
                            }
                            else
                            {
                                descuento_xproducto = descuento;
                            }
                        }

                        // Realiza calculos para obtener base, precio unitario, IVA
                        string mbase = "";
                        string miva = "";
                        string timpuesto = "";

                        if (r_tb_producto["Base"].ToString() == "" | r_tb_producto["Impuesto"].ToString() == "")
                        {
                            decimal precio = Convert.ToDecimal(r_productos["Precio"].ToString());

                            decimal b = precio / 1.16m;
                            decimal bs = precio - b;

                            mbase = Convert.ToString(dos_seis_decimales(b, 6));
                            miva = Convert.ToString(dos_seis_decimales(bs, 6));
                            timpuesto = "16%";
                        }
                        else
                        {
                            //mbase = r_tb_producto["Base"].ToString();
                            //miva = r_tb_producto["IVA"].ToString();
                            timpuesto = r_tb_producto["Impuesto"].ToString();

                            decimal precio = Convert.ToDecimal(r_productos["Precio"].ToString());
                            decimal tasacuota = 0;
                            decimal b = 0;
                            decimal bs = 0;

                            if (timpuesto == "16%" | timpuesto == "8%")
                            {
                                if (timpuesto == "16%") { tasacuota = 1.16m; }
                                if (timpuesto == "8%") { tasacuota = 1.08m; }

                                b = precio / tasacuota;
                                bs = precio - b;
                            }
                            else
                            {
                                b = precio;
                            }

                            mbase = Convert.ToString(dos_seis_decimales(b, 6));
                            miva = Convert.ToString(dos_seis_decimales(bs, 6));
                        }

                        // Si el producto tiene descuento, se hará una modificación del valor del campo base
                        if (descuento_xproducto != "")
                        {
                            var tip_desc = (descuento_xproducto).IndexOf("%");
                            decimal cantidad = Convert.ToDecimal(r_productos["Cantidad"].ToString());
                            decimal precio_unit = Convert.ToDecimal(r_productos["Precio"].ToString());

                            if (tip_desc >= 0)
                            {
                                // Descuento en porcentaje

                                string porc_desc = descuento_xproducto.Substring(0, (descuento_xproducto.Length - 1));

                                decimal porcent_desc = Convert.ToDecimal(porc_desc);

                                if (porcent_desc > 1)
                                {
                                    porcent_desc = porcent_desc / 100;
                                }

                                decimal desc_encant = precio_unit * porcent_desc;
                                decimal pu_desc = precio_unit - dos_seis_decimales(desc_encant, 6);
                                decimal nbase = pu_desc;

                                if (timpuesto == "16%")
                                {
                                    nbase = pu_desc / 1.16m;
                                }
                                if (timpuesto == "8%")
                                {
                                    nbase = pu_desc / 1.08m;
                                }

                                nbase = dos_seis_decimales(nbase, 6);

                                mbase = Convert.ToString(nbase);
                            }
                            else
                            {
                                // Descuento en cantidad $

                                decimal descuento_xcantidad = Convert.ToDecimal(descuento_xproducto) / cantidad;

                                decimal pu_desc = precio_unit - dos_seis_decimales(descuento_xcantidad, 2);
                                decimal nbase = pu_desc;

                                if (timpuesto == "16%")
                                {
                                    nbase = pu_desc / 1.16m;
                                }
                                if (timpuesto == "8%")
                                {
                                    nbase = pu_desc / 1.08m;
                                }

                                nbase = dos_seis_decimales(nbase, 6);

                                mbase = Convert.ToString(nbase);
                            }
                        }


                        string[] datos_fp = new string[]
                        {
                            id_factura.ToString(), r_tb_producto["UnidadMedida"].ToString(), r_tb_producto["ClaveProducto"].ToString(), r_productos["Nombre"].ToString(), r_productos["Cantidad"].ToString(), r_productos["Precio"].ToString(), mbase, timpuesto, miva, descuento_xproducto.Trim()
                        };

                        cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(6, datos_fp));

                        // Consulta si tiene más de un impuesto
                        // Si existen, los guarda en nueva tabla Facturas_impuestos
                        DataTable d_impuestos = cn.CargarDatos(cs.cargar_datos_venta_xml(8, id_p, Convert.ToInt32(id_usuario)));

                        if (d_impuestos.Rows.Count > 0)
                        {
                            // Consulta el ID del producto en curso
                            int id_fp = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas_productos WHERE id_factura='{id_factura}' ORDER BY ID DESC LIMIT 1", 1));

                            foreach (DataRow r_impuestos in d_impuestos.Rows)
                            {
                                string[] datos_i = new string[]
                                {
                                id_fp.ToString(), r_impuestos["Tipo"].ToString(), r_impuestos["Impuesto"].ToString(), r_impuestos["TipoFactor"].ToString(), r_impuestos["TasaCuota"].ToString(), r_impuestos["Definir"].ToString(), r_impuestos["Importe"].ToString()
                                };

                                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(7, datos_i));
                            }
                        }
                    }
                }




                // ........................................ 
                // .   Llamar al timbrado de la factura   .
                // ........................................        

                //Loading load = new Loading();
                //load.Show();
                decimal[][] vacio = new decimal[][] { };

                Generar_XML xml = new Generar_XML();
                string respuesta_xml = xml.obtener_datos_XML(id_factura, id_venta, 0, vacio);

                if (respuesta_xml == "")
                {
                    lb_facturando.Visible = false;

                    var r = MessageBox.Show("Su factura ha sido creada y timbrada con éxito.", "Éxito", MessageBoxButtons.OK);

                    if (r == DialogResult.OK)
                    {
                        this.Dispose();
                    }
                }
                else
                {
                    botones_visibles(2);
                    MessageBox.Show(respuesta_xml, "Error", MessageBoxButtons.OK);
                }
            }


            if (paso == 1) { paso = 2; }

        }
        */
    }
}
