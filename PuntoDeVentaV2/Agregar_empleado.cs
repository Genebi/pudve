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
    public partial class Agregar_empleado : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int tipo = 0;
        private int empleado = 0;

        private string nombre;
        private string usuario;
        private string password;
        int actualizar_contraseña = 0;
        public static int IDPlantilla = 0;

        string[] datosPermisosSeleccionados;

        public Agregar_empleado(int tipo = 1, int empleado = 0)
        {
            InitializeComponent();

            this.tipo = tipo;
            this.empleado = empleado;

            Agregar_empleado form = this;
            Utilidades.EjecutarAtajoKeyPreviewDown(Agregar_empleado_PreviewKeyDown, form);

        }

        private void Agregar_empleado_Load(object sender, EventArgs e)
        {
            if (tipo == 1)
            {
                Text = "Agregar empleado";
                //lbTitulo.Text = "NUEVO EMPLEADO";
                lbContraseñaParaConfirmar.Text = "Asignar permisos";
                txt_autorizar.Visible = false;
                cmb_bx_permisos.Visible = true;

                cmb_bx_permisos.SelectedIndex = 0;
                picturebx_editar.Visible = false;
            }

            if (tipo == 2)
            {
                Text = "Editar empleado";
                ///lbTitulo.Text = "EDITAR EMPLEADO";

                lbContraseñaParaConfirmar.Visible = false;
                cmb_bx_permisos.Visible = false;
                picturebx_editar.Visible = true;
                txtPassword.Enabled = false;

                var datos = mb.obtener_permisos_empleado(empleado, FormPrincipal.userID);

                if (datos.Length > 0)
                {
                    var tmp = datos[16].Split('@');

                    nombre = datos[15];
                    usuario = tmp[0];
                    password = datos[17];

                    txt_nombre.Text = nombre;
                    txt_usuario.Text = usuario;
                    txtPassword.Text = password;

                    //lb_usuario.Visible = true;

                    //lb_usuario_completo.Text = FormPrincipal.userNickName;/* +"@" + usuario*/
                    //lb_usuario_completo.Visible = true;

                    txt_usuario.Enabled = false;
                    cmb_bx_permisos.Visible = false;
                }
            }
        }

        private void solo_letras_digitos(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) | char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void muestra_usuarioc(object sender, KeyEventArgs e)
        {
            if (txt_usuario.Text != "")
            {
                lb_usuario.Visible = true;
                lb_usuario_completo.Visible = true;

                string n_completo = FormPrincipal.userNickName + "@" + txt_usuario.Text;
                lb_usuario_completo.Text = n_completo;
            }
            else
            {
                lb_usuario.Visible = false;
                lb_usuario_completo.Visible = false;
                lb_usuario_completo.Text = string.Empty;
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            btn_aceptar.Enabled = false;
            btn_cancelar.Enabled = false;


            string mnsj_er = "";
            string error = "0";

            string val_campos = valida_campos();
            string[] m_e = val_campos.Split('-');
            string permisos = Convert.ToString(cmb_bx_permisos.SelectedIndex);
            int id_empleado = 0;

            mnsj_er = m_e[0];
            error = m_e[1];

            if (error == "0")
            {
                if (tipo == 1)
                {
                    
                    string[] datos = new string[]
                    {
                        FormPrincipal.userID.ToString(), txt_nombre.Text, lb_usuario_completo.Text, txtPassword.Text, permisos
                    };

                    int r = cn.EjecutarConsulta(cs.guardar_editar_empleado(datos, 1));

                    // Obtiene id del empleado
                     id_empleado = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Empleados WHERE IDUsuario='{FormPrincipal.userID}' ORDER BY ID DESC LIMIT 1", 5));

                    Utilidades.registrarNuevoEmpleadoPermisosConfiguracion(id_empleado);//Se registra el nuevo empleado en la tabla permisosConfiguracion

                    // Crea registro en tabla EmpleadosPermisos
                    crear_registro_empleados_permisos(id_empleado);

                    if (cmb_bx_permisos.SelectedIndex == 0)
                    {
                        cn.EjecutarConsulta($"UPDATE empleados SET p_empleado ='1',ConsultaPrecio = 1 WHERE ID='{id_empleado}'");
                    }

                    
                    // Limitados
                    if (cmb_bx_permisos.SelectedIndex == 1)
                    {
                        // Anticipos
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion2='0' WHERE Seccion='Anticipos' AND IDEmpleado='{id_empleado}'");
                        // Caja
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion8='0', Opcion11='0' WHERE Seccion='Caja' AND IDEmpleado='{id_empleado}'");
                        // Clientes
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion2='0', Opcion3='0' WHERE Seccion='Clientes' AND IDEmpleado='{id_empleado}'");
                        // Configuración
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion3='0', Opcion4='0', Opcion5='0', Opcion6='0', Opcion7='0', Opcion8='0', Opcion9='0', Opcion11='0', Opcion14='0', Opcion15='0' WHERE Seccion='Configuracion' AND IDEmpleado='{id_empleado}'");
                        // Empleados
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='0', Opcion2='0', Opcion3='0' WHERE Seccion='Empleados' AND IDEmpleado='{id_empleado}'");
                        // Facturas
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion3='0' WHERE Seccion='Facturas' AND IDEmpleado='{id_empleado}'");
                        // Inventario
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='0', Opcion2='0', Opcion3='0', Opcion4='0', Opcion5='0' WHERE Seccion='Inventario' AND IDEmpleado='{id_empleado}'");
                        // Mis datos
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='0', Opcion2='0', Opcion3='0', Opcion4='0', Opcion5='0' WHERE Seccion='MisDatos' AND IDEmpleado='{id_empleado}'");
                        // Proveedores
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='0', Opcion2='0' WHERE Seccion='Proveedores' AND IDEmpleado='{id_empleado}'");
                        // Reportes
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='0', Opcion2='0' WHERE Seccion='Reportes' AND IDEmpleado='{id_empleado}'");
                        // Productos
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='0', Opcion2='0', Opcion3='0', Opcion4='0', Opcion5='0', Opcion6='0', Opcion7='0', Opcion8='0', Opcion9='0', Opcion10='0', Opcion11='0', Opcion12='0', Opcion13='0', Opcion14='0', Opcion15='0', Opcion16='0', Opcion17='0', Opcion18='0', Opcion19='0', Opcion20='0', Opcion21='0', Opcion22='0' WHERE Seccion='Productos' AND IDEmpleado='{id_empleado}'");

                        // Tabla Empleados
                        cn.EjecutarConsulta($"UPDATE Empleados SET p_empleado='0', p_empresa='0', p_inventario='0', p_mdatos='0', p_producto='0', p_proveedor='0', p_reporte='0' WHERE ID='{id_empleado}'");
                        //Bascula
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion1='1' WHERE Seccion='Bascula' AND IDEmpleado='{id_empleado}'");

                        /* 
                         * - Anticipos
                         *    opcion2 --> habilitar/inahibilitar 
                         * - Facturas 
                         *    opcion3 --> Cancelar factura
                         * - Caja 
                         *    opcion8 --> Mostrar panel ventas
                         *    opcion11 --> Mostrar panel total caja
                         * - Clientes 
                         *    opcion2 --> Nuevo tipo cliente
                         *    opcion3 --> Listado tipo cliente
                         * - Empresas,  Mis datos 1-5,  Reportes 1y2 
                         *    Deshabilitar todo
                         * - Productos 1-22,  Empleados 1-3,  Inventario 1-5,  Proveedores 1y2
                         *    Deshabilitar todo
                         * - Configuración
                         *    opcion3 --> Porcentaje ganancia
                         *    opcion4 --> Respaldar informacion
                         *    opcion5 --> Correo modificar precio
                         *    opcion6 --> Correo modificar stock
                         *    opcion7 --> Correo stock minimo
                         *    opcion8 --> Correo vender producto
                         *    opcion9 --> Permitir stock negativo
                         *    opcion11 --> Informacion pagina web
                         *    opcion14 --> Activar precio mayoreo
                         *    opcion15 --> Avisar productos no vendidos */
                    }

                    if (cmb_bx_permisos.SelectedIndex == 2)
                    {
                        var DTPlantillaPermisos = cn.CargarDatos($"SELECT Anticipo,Caja,clientes,configuracion,empleado,empresa,factura,inventario,misdatos,productos,proveedor,reportes,ventas, bascula,configuracion FROM plantillapermisos WHERE IDUsuario = {FormPrincipal.userID} AND ID = {IDPlantilla}");
                        string PermisosJusntos = string.Empty;
                        int contador = 0;
                        foreach (var item in DTPlantillaPermisos.Rows)
                        {
                            foreach (var itemxd in DTPlantillaPermisos.Columns)
                            {
                                PermisosJusntos += DTPlantillaPermisos.Rows[0][contador].ToString()+",";
                                contador++;
                            }
                        }
                        string[] listapermisos = PermisosJusntos.Split(',');
                        cn.EjecutarConsulta($"UPDATE empleados SET IDUsuario = {FormPrincipal.userID},p_anticipo ={listapermisos[0]}, p_caja ={listapermisos[1]}, p_cliente = {listapermisos[2]}, p_config ={listapermisos[3]}, p_empleado = {listapermisos[4]}, p_empresa = {listapermisos[5]}, p_factura = {listapermisos[6]}, p_inventario = {listapermisos[7]}, p_mdatos = {listapermisos[8]}, p_producto = {listapermisos[9]}, p_proveedor ={listapermisos[10]}, p_reporte= {listapermisos[11]}, p_venta = {listapermisos[12]}, Bascula = {listapermisos[13]}, ConsultaPrecio = {listapermisos[14]} WHERE IDUsuario = {FormPrincipal.userID} AND ID = {id_empleado}");
                    }
                    
                    btn_aceptar.Enabled = true;
                    btn_cancelar.Enabled = true;

                    this.Close();

                    //// Elegidos
                    //if (cmb_bx_permisos.SelectedIndex == 2)
                    //{
                    //    Agregar_empleado_permisos elegir_permisos = new Agregar_empleado_permisos(id_empleado);
                    //    elegir_permisos.ShowDialog();
                    //}
                }

                if (tipo == 2)
                {
                    //lb_usuario_completo.Text.Trim(),
                    int resultado = 0;

                    string[] datos = new string[] {
                        empleado.ToString(), txt_nombre.Text.Trim(), txtPassword.Text.Trim()
                    };

                    if (actualizar_contraseña == 1)
                    {
                        resultado = cn.EjecutarConsulta(cs.guardar_editar_empleado(datos, 4));
                    }
                    else
                    {
                        resultado = cn.EjecutarConsulta(cs.guardar_editar_empleado(datos, 5));
                    }
                    
                    if (resultado > 0)
                    {
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show(mnsj_er, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btn_aceptar.Enabled = true;
                btn_cancelar.Enabled = true;
            }

        }
        
        public string valida_campos()
        {
            string mnsj = "";
            int error = 0;
            var usuario = FormPrincipal.userNickName.ToString();


            if(tipo == 1 | actualizar_contraseña == 1)
            {
                if (txtPassword.Text.Trim() == "")
                {
                    error = 1;
                    mnsj = "La contraseña es obligatoria.";
                }
            }
            
            // Aplica solo para agregar
            if (tipo == 1)
            {
                if (txt_usuario.Text.Trim() == "")
                {
                    error = 1;
                    mnsj = "El usuario es obligatorio";
                }
                else
                {
                    bool existe = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE usuario='{lb_usuario_completo.Text}' AND IDUsuario='{FormPrincipal.userID}' AND estatus = {1}");

                    if (existe == true)
                    {
                        error = 1;
                        mnsj = "Ya existe ese nombre de usuario, elegir otro.";
                    }
                }
            }
                
            if (txt_nombre.Text.Trim() == "")
            {
                error = 1;
                mnsj = "El nombre es obligatorio.";
            }

            // Aplica para editar
            if(tipo == 2 & actualizar_contraseña == 1)
            {
                if(txt_autorizar.Text.Trim() == "" )
                {
                    error = 1;
                    mnsj = "La contraseña que autoriza el cambio de contraseña del empleado no puede estar vacía.";
                }
                else if (txtPassword.Text.Trim() != txtConfirmeSuPassword.Text.Trim())
                {
                    error = 1;
                    mnsj = "Verificar que las contraseñas nuevas coincidan.";
                }
                else if(usuario.Contains("@"))
                {
                    // Validar que la contraseña del empleado sea valida

                    bool valida_contraseña = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE ID='{FormPrincipal.id_empleado}' AND contrasena='{txt_autorizar.Text}'");

                    if(valida_contraseña == false)
                    {
                        error = 1;
                        mnsj = "La contraseña del usuario" + usuario + " para confirmar es incorrecta.";
                    }
                }
                else if (!usuario.Contains("@"))
                {
                    // Validar que la contraseña del Usuario sea valida

                    bool valida_contraseña = (bool)cn.EjecutarSelect($"SELECT * FROM Usuarios WHERE ID='{FormPrincipal.userID}' AND Password ='{txt_autorizar.Text}'");

                    if (valida_contraseña == false)
                    {
                        error = 1;
                        mnsj = "La contraseña del usuario" +usuario +" (ADMIN) "+ "para confirmar es incorrecta.";
                    }
                }
            }


            return mnsj + "-" + error;
        }

        private void verifica_usuario_empleado(object sender, EventArgs e)
        {
            bool existe = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE usuario='{lb_usuario_completo.Text}' AND IDUsuario='{FormPrincipal.userID}' AND estatus = 1");

            if (existe == true)
            {
                if (tipo == 1)
                {
                    MessageBox.Show("Ya existe ese nombre de usuario, elegir otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (tipo == 2)
                {
                    if (!usuario.Equals(txt_usuario.Text.Trim()))
                    {
                        MessageBox.Show("Ya existe ese nombre de usuario, elegir otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void crear_registro_empleados_permisos(int id_e)
        {
            var secciones = new string[] {
                "Caja", "Ventas", "Inventario", "Anticipos",
                "MisDatos", "Facturas", "Configuracion", "Reportes",
                "Clientes", "Proveedores", "Empleados", "Productos","Bascula"
            };

            foreach (var seccion in secciones)
            {
                cn.EjecutarConsulta($"INSERT INTO EmpleadosPermisos (IDEmpleado, IDUsuario, Seccion) VALUES ('{id_e}', '{FormPrincipal.userID}', '{seccion}')");
            }
        }

        private void click_editar_contraseña(object sender, EventArgs e)
        {
            lbContraseñaNueva.Text = "Ingrese La Nueva Contraseña";
            txtPassword.Enabled = true;
            lbContraseñaParaConfirmar.Visible = true;
            txt_autorizar.Visible = true;
            txtConfirmeSuPassword.Visible = true;
            lbConfrimarContraseña.Visible = true;
            actualizar_contraseña = 1;

            Size = new Size(341, 395);
            //lbContraseñaNueva.Location = new Point(65, 119);

            picturebx_editar.Visible = false;
            txtPassword.Text = "";
            txtPassword.Focus();

            string usuario = FormPrincipal.userNickName.ToString();

            if (usuario.Contains("@") == true)
            {
                lbContraseñaParaConfirmar.Text = "Ingresa la contraseña del Usuario: \n" + FormPrincipal.userNickName + " para confirmar.";
            }
            else
            {
                lbContraseñaParaConfirmar.Text = "Ingresa la contraseña del Usuario: \n" + FormPrincipal.userNickName + " (Admin) " + "\npara confirmar.";
            }

        }

        private void txt_autorizar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_aceptar.PerformClick();
            }
        }

        private void Agregar_empleado_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btn_aceptar.PerformClick();
            }
        }

        private void txt_conttraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_aceptar.PerformClick();
            }
        }

        private void Agregar_empleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cmb_bx_permisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_bx_permisos.SelectedIndex == 1)
            {
                Agregar_empleado_permisos AEP = new Agregar_empleado_permisos(0);
                AEP.ShowDialog();
            }
        }
    }
}
