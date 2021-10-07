﻿using System;
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
                lb_permisos_contraseña.Text = "Asignar permisos";
                txt_autorizar.Visible = false;
                cmb_bx_permisos.Visible = true;

                cmb_bx_permisos.SelectedIndex = 0;
            }

            if (tipo == 2)
            {
                Text = "Editar empleado";
                ///lbTitulo.Text = "EDITAR EMPLEADO";

                lb_permisos_contraseña.Visible = false;
                cmb_bx_permisos.Visible = false;
                picturebx_editar.Visible = true;
                txt_conttraseña.Enabled = false;

                var datos = mb.obtener_permisos_empleado(empleado, FormPrincipal.userID);

                if (datos.Length > 0)
                {
                    var tmp = datos[16].Split('@');

                    nombre = datos[15];
                    usuario = tmp[1];
                    password = datos[17];

                    txt_nombre.Text = nombre;
                    txt_usuario.Text = usuario;
                    txt_conttraseña.Text = password;

                    lb_usuario.Visible = true;

                    lb_usuario_completo.Text = FormPrincipal.userNickName + "@" + usuario;
                    lb_usuario_completo.Visible = true;

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
                        FormPrincipal.userID.ToString(), txt_nombre.Text, lb_usuario_completo.Text, txt_conttraseña.Text, permisos
                    };

                    int r = cn.EjecutarConsulta(cs.guardar_editar_empleado(datos, 1));

                    // Obtiene id del empleado
                     id_empleado = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Empleados WHERE IDUsuario='{FormPrincipal.userID}' ORDER BY ID DESC LIMIT 1", 5));

                    Utilidades.registrarNuevoEmpleadoPermisosConfiguracion(id_empleado);//Se registra el nuevo empleado en la tabla permisosConfiguracion

                    // Crea registro en tabla EmpleadosPermisos
                    crear_registro_empleados_permisos(id_empleado);

                    if (cmb_bx_permisos.SelectedIndex == 0)
                    {
                        cn.EjecutarConsulta($"UPDATE empleados SET p_empleado ='0' WHERE ID='{id_empleado}'");
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


                    btn_aceptar.Enabled = true;
                    btn_cancelar.Enabled = true;

                    this.Close();

                    // Elegidos
                    if (cmb_bx_permisos.SelectedIndex == 2)
                    {
                        Agregar_empleado_permisos elegir_permisos = new Agregar_empleado_permisos(id_empleado);
                        elegir_permisos.ShowDialog();
                    }
                }

                if (tipo == 2)
                {
                    //lb_usuario_completo.Text.Trim(),
                    int resultado = 0;

                    string[] datos = new string[] {
                        empleado.ToString(), txt_nombre.Text.Trim(), txt_conttraseña.Text.Trim()
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


            if(tipo == 1 | actualizar_contraseña == 1)
            {
                if (txt_conttraseña.Text.Trim() == "")
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
                    bool existe = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE usuario='{lb_usuario_completo.Text}' AND IDUsuario='{FormPrincipal.userID}'");

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
                if(txt_autorizar.Text.Trim() == "")
                {
                    error = 1;
                    mnsj = "La contraseña que autoriza el cambio de contraseña del empleado no puede estar vacía.";
                }
                else
                {
                    // Validar que la contraseña del usuario sea valida

                    bool valida_contraseña = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE ID='{empleado}' AND contrasena='{txt_autorizar.Text}'");

                    if(valida_contraseña == false)
                    {
                        error = 1;
                        mnsj = "La contraseña del usuario es incorrecta.";
                    }
                }
            }


            return mnsj + "-" + error;
        }

        private void verifica_usuario_empleado(object sender, EventArgs e)
        {
            bool existe = (bool)cn.EjecutarSelect($"SELECT * FROM Empleados WHERE usuario='{lb_usuario_completo.Text}' AND IDUsuario='{FormPrincipal.userID}'");

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
                "Clientes", "Proveedores", "Empleados", "Productos"
            };

            foreach (var seccion in secciones)
            {
                cn.EjecutarConsulta($"INSERT INTO EmpleadosPermisos (IDEmpleado, IDUsuario, Seccion) VALUES ('{id_e}', '{FormPrincipal.userID}', '{seccion}')");
            }
        }

        private void click_editar_contraseña(object sender, EventArgs e)
        {
            lb_permisos_contraseña.Text = "Autorizar con \n contraseña del \n usuario";
            txt_conttraseña.Enabled = true;
            lb_permisos_contraseña.Visible = true;
            txt_autorizar.Visible = true;

            actualizar_contraseña = 1;
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
    }
}
