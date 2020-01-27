using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Crear_factura : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int con_id_cliente = 0;
        int n_filas = 0;
        int id_venta = 0;


        public Crear_factura(int sin_cliente, int n_f, int id_v)
        {
            InitializeComponent();

            con_id_cliente = sin_cliente; 
            n_filas = n_f;
            id_venta = id_v;
        }

        private void Crear_factura_Load(object sender, EventArgs e)
        {
            // Obtiene los clientes

            DataTable d_clientes;
            Dictionary<string, string> clientes = new Dictionary<string, string>();
            var indice = 0;
            int c = 1;

            d_clientes = cn.CargarDatos(cs.cargar_datos_venta_xml(7, 0, Convert.ToInt32(FormPrincipal.userID)));

            if(d_clientes.Rows.Count > 0)
            {
                btn_crear_cliente.Enabled = false;
                btn_crear_cliente.Cursor = Cursors.No;

                clientes.Add("0", "Seleccionar cliente");
                foreach(DataRow r_clientes in d_clientes.Rows)
                {
                    clientes.Add(r_clientes["ID"].ToString() + "|" + r_clientes["RazonSocial"].ToString(), r_clientes["RFC"].ToString() + " - " + r_clientes["RazonSocial"].ToString());

                    if(Convert.ToInt32(r_clientes["ID"]) == con_id_cliente)
                    {
                        indice = c;
                    }
                    c++;
                }
            }
            else
            {
                clientes.Add("0", "No hay clientes para mostrar. Ir a registrar cliente.");
                btn_crear_cliente.Enabled = true;
                btn_crear_cliente.Cursor = Cursors.Hand;
            }

            cmb_bx_clientes.DataSource = clientes.ToArray();
            cmb_bx_clientes.DisplayMember = "Value";
            cmb_bx_clientes.ValueMember = "Key";

            if(con_id_cliente > 0)
            {
                cmb_bx_clientes.SelectedIndex = indice;
            }
            else
            {
                cmb_bx_clientes.SelectedIndex = 0;
            }
            
            
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
            cmb_bx_forma_pago.SelectedIndex = 0;

            // Moneda

            Dictionary<string, string> moneda = new Dictionary<string, string>();
            DataTable d_moneda;

            d_moneda = cn.CargarDatos(cs.cargar_datos_venta_xml(6, 0, 0));

            foreach(DataRow r_moneda in d_moneda.Rows)
            {
                moneda.Add(r_moneda["clave_moneda"].ToString(), r_moneda["clave_moneda"].ToString() + " - " + r_moneda["descripcion"].ToString());
            }

            cmb_bx_moneda.DataSource = moneda.ToArray();
            cmb_bx_moneda.DisplayMember = "Value";
            cmb_bx_moneda.ValueMember = "Key";
            cmb_bx_moneda.SelectedIndex = 1;

            // Tipo de venta

            if(ListadoVentas.tipo_venta == 1)
            {
                cmb_bx_metodo_pago.SelectedIndex = 0;
            }
            if (ListadoVentas.tipo_venta == 4)
            {
                cmb_bx_metodo_pago.SelectedIndex = 1;
            }

            // Productos
           
            int location_y = 5;

            if (ListadoVentas.faltantes_productos.Length > 0)
            {
                for (int i = 1; i < n_filas; i++)
                {
                    int id_producto = Convert.ToInt32(ListadoVentas.faltantes_productos[i][1]);
                    string des_habilitar = ListadoVentas.faltantes_productos[i][0];
                    string c_producto = ListadoVentas.faltantes_productos[i][2];
                    string c_unidad = ListadoVentas.faltantes_productos[i][3];
                    string descripcion= ListadoVentas.faltantes_productos[i][4];


                    TextBox txt_clave_unidad = new TextBox();
                    txt_clave_unidad.Name = "txt_clave_u" + i;
                    txt_clave_unidad.Location = new Point(3, location_y);
                    txt_clave_unidad.Size = new Size(49, 22);
                    txt_clave_unidad.MaxLength = 3;
                    txt_clave_unidad.TextAlign = HorizontalAlignment.Center;
                    txt_clave_unidad.CharacterCasing = CharacterCasing.Upper;
                    txt_clave_unidad.Leave += new EventHandler(valida_clave_unidad);
                    txt_clave_unidad.KeyPress += new KeyPressEventHandler(tdatos_cunidad);

                    TextBox txt_clave_producto = new TextBox();
                    txt_clave_producto.Name = "txt_clave_p" + i;
                    txt_clave_producto.Location = new Point(64, location_y);
                    txt_clave_producto.Size = new Size(87, 22);
                    txt_clave_producto.MaxLength = 8;
                    txt_clave_producto.TextAlign = HorizontalAlignment.Center;
                    txt_clave_producto.Leave += new EventHandler(valida_clave_producto);
                    txt_clave_producto.KeyPress += new KeyPressEventHandler(solo_numeros_cproducto);

                    TextBox txt_descripcion = new TextBox();
                    txt_descripcion.Name = "txt_descripcion" + i;
                    txt_descripcion.Location = new Point(163, location_y);
                    txt_descripcion.Size = new Size(344, 22);
                    txt_descripcion.MaxLength = 1000;
                    txt_descripcion.TextAlign = HorizontalAlignment.Center;
                    txt_descripcion.ReadOnly = true;

                    TextBox txt_idproducto = new TextBox();
                    txt_idproducto.Name = "txt_idprod" + i;
                    txt_idproducto.Location = new Point(513, location_y);
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
                }
            }
            else
            {
                groupb_productos.Visible = false;
            }
        }

        private void ir_a_clientes(object sender, EventArgs e)
        {
            Clientes ir_clientes = new Clientes();

            ir_clientes.FormClosed += delegate
            {
                 limpiar_campos();
            };

            ir_clientes.ShowDialog();
        }
        
        private void limpiar_campos()
        {
            cmb_bx_clientes.SelectedIndex = 0;
            cmb_bx_forma_pago.SelectedIndex = 0;
            cmb_bx_moneda.SelectedValue = "MXN";

            btn_crear_cliente.Enabled = true;
            btn_crear_cliente.Cursor = Cursors.No;

            txt_cuenta.ReadOnly = true;
            txt_cuenta.Text = string.Empty;
            txt_tipo_cambio.ReadOnly = true;
            txt_tipo_cambio.Text = "1.000000";
        }

        private void sel_forma_pago(object sender, EventArgs e)
        {
            string clave = cmb_bx_forma_pago.SelectedValue.ToString();


            if (clave == "02" | clave == "03" | clave == "04" | clave == "28" | clave == "29" | clave == "05" | clave == "06" | clave == "99")
            {
                txt_cuenta.ReadOnly = false;

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
            // .....................  
            // .   Validar datos   .
            // .....................

            
            // Cliente

            int cant_clientes = cmb_bx_clientes.Items.Count;
            
            if(cant_clientes > 0)
            {
                if (cmb_bx_clientes.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Se debe elegir un cliente para poder facturar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("No tiene ningún cliente registrado para facturarle. Primero vaya al aartado de clientes a registrar uno, posterior regresar a timbrar la factura y elegir el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tipo cambio

            string clave = cmb_bx_moneda.SelectedValue.ToString();

            if (clave != "MXN" & clave != "XXX")
            {
                if (txt_tipo_cambio.Text == "")
                {
                    MessageBox.Show("El tipo de cambio es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Productos

            string info_productos = "";
            int clave_vacia = 0;
            int clave_inc = 0;

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
                return;
            }
            if (clave_inc > 0)
            {
                MessageBox.Show("Alguna clave de unidad o producto es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // .....................  
            // .   Crear factura   .
            // .....................


            // Guarda id del cliente
            
            string id_select = cmb_bx_clientes.SelectedValue.ToString();
            string[] datos_s = id_select.Split('|');

            string id_cliente = datos_s[0];
            string nombre_cliente = datos_s[1];

            string[] datos_c = new string[]
            {
                id_venta.ToString(), id_cliente, nombre_cliente
            };

            cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(1, datos_c));


            // Guarda forma y método de pago, moneda y tipo de cambio

            string metodo_pago = "";

            string forma_pago = cmb_bx_forma_pago.SelectedValue.ToString(); 
            string num_cuenta = txt_cuenta.Text;

            if (ListadoVentas.tipo_venta == 1) { metodo_pago = "PUE"; }
            if (ListadoVentas.tipo_venta == 4) { metodo_pago = "PPD"; }

            string moneda = cmb_bx_moneda.SelectedValue.ToString();
            string tipo_cambio = txt_tipo_cambio.Text;


            string[] datos_f = new string[]
            {
                id_venta.ToString(), metodo_pago, forma_pago, num_cuenta, moneda, tipo_cambio
            };

            cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(2, datos_f));


            //  Guarda clave de unidad y producto
                        
            string[] filas = info_productos.Split('#');
            string[] datos_p;

            for (int f=1; f<filas.Length; f++)
            {
                string[] celda = filas[f].Split('-');
                Console.WriteLine("c0= " + celda[0] + "c1= " + celda[1] + "c2= " + celda[2]);
                datos_p = new string[] 
                {
                    celda[0], celda[1], celda[2]
                };

                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(3, datos_p));
            }


            // ........................................ 
            // .   Llamar al timbrado de la factura   .
            // ........................................        


           /* Generar_XML xml = new Generar_XML();
            string respuesta_xml = xml.obtener_datos_XML(id_venta);

            if(respuesta_xml == "")
            {
                MessageBox.Show("Su factura ha sido creada y timbrada con éxito.", "Éxito", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(respuesta_xml, "Error", MessageBoxButtons.OK);
            }*/
        }
    }
}
