using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PuntoDeVentaV2
{
    public partial class MisDatos : Form
    {
        // creamos un objeto para poder usar las  
        // consultas que estan en esta clase
        Conexion cn = new Conexion();
        // creamos un objeto para poder usar las  
        // consultas que estan en esta clase 
        Consultas cs = new Consultas();

        // declaramos la variable que almacenara el valor de userNickName
        public string userName;
        public string passwordUser;

        // variables para poder hacer las consulta y actualizacion
        string buscar;
        string actualizar;
        string llenarCB;

        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string nomComp; string rfc;
        string calle; string numExt;
        string numInt; string colonia;
        string mpio; string estado;
        string codPostal; string email;
        string telefono; string regimen;

        // Variable para poder saber que tipo de persona 
        // es el cliente que inicio sesion en el Pudve
        string tipoPersona;

        // variables para poder hacer el recorrido y asignacion de los valores que estan el base de datos
        int index;
        DataTable dt, dtcb, cbreg;
        DataRow row, rows;

        public MisDatos()
        {
            InitializeComponent();
        }
        public void cargarTxtBox()
        {
            index = 0;

            /****************************************************
            *   obtenemos los datos almacenados en el dt        *
            *   y se los asignamos a cada uno de los variables  *
            ****************************************************/
            id = dt.Rows[index]["ID"].ToString();
            nomComp = dt.Rows[index]["NombreCompleto"].ToString();
            rfc = dt.Rows[index]["RFC"].ToString();
            calle = dt.Rows[index]["Calle"].ToString();
            numExt = dt.Rows[index]["NoExterior"].ToString();
            numInt = dt.Rows[index]["NoInterior"].ToString();
            colonia = dt.Rows[index]["Colonia"].ToString();
            mpio = dt.Rows[index]["Municipio"].ToString();
            estado = dt.Rows[index]["Estado"].ToString();
            codPostal = dt.Rows[index]["CodigoPostal"].ToString();
            email = dt.Rows[index]["Email"].ToString();
            telefono = dt.Rows[index]["Telefono"].ToString();
            regimen = dt.Rows[index]["Regimen"].ToString();
            tipoPersona = dt.Rows[index]["TipoPersona"].ToString();

            /****************************************
            *   ponemos los datos en los TxtBox     *
            *   almancenados en las variables       *
            *   para mostrarlos en la Forma         *
            ****************************************/
            txtNombre.Text = nomComp;
            txtRFC.Text = rfc;
            txtCalle.Text = calle;
            txtNoExt.Text = numExt;
            txtNoInt.Text = numInt;
            txtColonia.Text = colonia;
            txtMpio.Text = mpio;
            txtEstado.Text = estado;
            txtCodPost.Text = codPostal;
            txtEmail.Text = email;
            txtTelefono.Text = telefono;
            cbRegimen.Text = regimen;

            /********************************
            *   Relleno de los RadioButton  *
            ********************************/
            if (tipoPersona == "Física")
            {
                // Radio Button para persona Física
                rbPersonaFisica.Checked = true;
            }
            if (tipoPersona == "Moral")
            {
                // Radio Button para persona Física
                rbPersonaMoral.Checked = true;
            }
            // hacemos que el comboBox tenga la carga  
            // de valores de la tabla RegimenFiscal
            cargarComboBox();
        }

        public void cargarComboBox()
        {
            cbRegimen.Items.Clear();
            /********************************
            *   Relleno de los RadioButton  *
            ********************************/

            // si es que no estan marcados los radio buttons
            if (rbPersonaFisica.Checked && rbPersonaMoral.Checked)
            {
                index = 0;
                /************************************************ 
                *   hacemos el recorrido para poder agregar     * 
                *   los registros en el ComboBoxText            *
                ************************************************/
                for (index = 0; index < dtcb.Rows.Count; index++)
                {
                    cbRegimen.Items.Add(dtcb.Rows[index]["Descripcion"].ToString());
                }
            }

            // si es que esta marcado el persona Fisica
            if (rbPersonaFisica.Checked)
            {
                index = 0;
                // String para llenar el ComboBox
                llenarCB = "SELECT CodigoRegimen, Descripcion FROM RegimenFiscal WHERE AplicaFisica LIKE 'Sí'";
                cbreg = cn.cargarCBRegimen(llenarCB);
                /************************************************ 
                *   hacemos el recorrido para poder agregar     * 
                *   los registros en el ComboBoxText            *
                ************************************************/
                for (index = 0; index < cbreg.Rows.Count; index++)
                {
                    cbRegimen.Items.Add(cbreg.Rows[index]["Descripcion"].ToString());
                }
            }

            // si es que esta marcado el persona Fisica
            if (rbPersonaMoral.Checked)
            {
                index = 0;
                // String para llenar el ComboBox
                llenarCB = "SELECT CodigoRegimen, Descripcion FROM RegimenFiscal WHERE AplicaMoral LIKE 'Sí'";
                cbreg = cn.cargarCBRegimen(llenarCB);
                /************************************************ 
                *   hacemos el recorrido para poder agregar     * 
                *   los registros en el ComboBoxText            *
                ************************************************/
                for (index = 0; index < cbreg.Rows.Count; index++)
                {
                    cbRegimen.Items.Add(cbreg.Rows[index]["Descripcion"].ToString());
                }
            }
        }
    
        public void actualizarVariables()
        {
            // actualizamos los valores de la variables con los
            // nuevos valores que modifica el suarios de sus
            // datos para realizar el UpDate
            nomComp = txtNombre.Text;
            rfc = txtRFC.Text;
            calle = txtCalle.Text;
            numExt = txtNoExt.Text;
            numInt = txtNoInt.Text;
            colonia = txtColonia.Text;
            mpio = txtMpio.Text;
            estado = txtEstado.Text;
            codPostal = txtCodPost.Text;
            email = txtEmail.Text;
            telefono = txtTelefono.Text;
            regimen = cbRegimen.Text;
        }

        // funcion para poder cargar los datos segun corresponda en el TxtBox que corresponda
        public void data()
        {
            // hacemos que los txtBox tengan los valores de la consulta
            cargarTxtBox();
        }

        // funcion para poder cargar los datos al abrir la forma
        public void consulta()
        {
            index = 0;

            // String para hacer la consulta filtrada sobre
            // el usuario que inicia la sesion
            buscar = "SELECT * FROM Usuarios WHERE Usuario = '" + userName + "' AND Password = '" + passwordUser + "'";

            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.CargarDatos(buscar);

            // almacenamos el resultado de la consulta en el dt
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }

            // almacenamos el resultado de la Funcion ConsultaRegimenFiscal
            // que esta en la calse Consultas
            dtcb = cn.ConsultaRegimenFiscal();

            // almacenamos el resultado de la consulta en el dtcb
            while (index < dtcb.Rows.Count)
            {
                rows = dtcb.Rows[index];
                index++;
            }

            // llamamos la funcion data()
            data();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MisDatos_Load(object sender, EventArgs e)
        {
            // asignamos el valor de userName que sea
            // el valor que tiene userNickUsuario en el formulario Principal
            userName = FormPrincipal.userNickName;
            passwordUser = FormPrincipal.userPass;
            // realizamos la carga de los datos del usuario
            consulta();
        }

        private void btnActualizarDatos_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Actualizara los datos del registro?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // mandamos llamar la funcion actualizarVariables()
                actualizarVariables();

                actualizar = "UPDATE Usuarios SET RFC = '"+ rfc 
                                             +"', Telefono = '"+ telefono 
                                             +"', Email = '"+ email 
                                             +"', NombreCompleto = '"+ nomComp 
                                             +"', Calle = '"+ calle 
                                             +"', NoExterior = '"+ numExt 
                                             +"', NoInterior = '"+ numInt 
                                             +"', Colonia = '"+ colonia 
                                             +"', Municipio = '"+ mpio 
                                             +"', Estado = '"+ estado 
                                             +"', CodigoPostal = '"+ codPostal 
                                             +"', Regimen ='"+ regimen
                                             + "', TipoPersona = '" + tipoPersona
                                             + "' WHERE ID = '" + id +"'";

                cn.EjecutarConsulta(actualizar);
                consulta();
            }
        }
        
        private void txtNoExt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtNoInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void rbPersonaFisica_Click(object sender, EventArgs e)
        {
            // se le asigna a la variable tipoPersona
            // el valor que es tipo de Física
            tipoPersona = "Física";
        }

        private void rbPersonaMoral_Click(object sender, EventArgs e)
        {
            // se le asigna a la variable tipoPersona
            // el valor que es tipo de Moral
            tipoPersona = "Moral";
        }

        private void rbPersonaFisica_CheckedChanged(object sender, EventArgs e)
        {
            cargarComboBox();
        }

        private void txtCodPost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si No Es Numero  
            if (!(char.IsNumber(e.KeyChar)) &&
                // y Si No es Backspace(Borrar)
                (e.KeyChar != (char)Keys.Back) &&
                // y si no es Enter(Entrar) 
                (e.KeyChar != (char)Keys.Enter) &&
                // y si no es Return(Entrar o Intro del Teclado Numerico) 
                (e.KeyChar != (char)Keys.Return) &&
                // y si no es Delete(Del o Suprimir, Supr)  
                (e.KeyChar != (char)Keys.Delete))
            {
                // Entonces 
                // Lanzamos Mensaje diciendo que solo permite numeros 
                MessageBox.Show("Solo se permiten numeros en el Campo Codigo Postal",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Habilitamos el Handled = true para que no escriba nada si no son las teclas validadas 
                e.Handled = true;
                //Retornamos return; 
            }
            // entonces si las teclas son las permitidas Validamos
            // si se presiona Enter 
            else if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                // mandamos un Tab para ir al siguiene campo 
            }
            // si se presiona Return (Intro del Teclado Numerico) 
            else if (e.KeyChar == (char)Keys.Return)
            {
                SendKeys.Send("{TAB}");
                // mandamos un Tab para ir al siguiene campo 
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si No Es Numero  
            if (!(char.IsNumber(e.KeyChar)) &&
                // y Si No es Backspace(Borrar)
                (e.KeyChar != (char)Keys.Back) &&
                // y si no es Enter(Entrar) 
                (e.KeyChar != (char)Keys.Enter) &&
                // y si no es Return(Entrar o Intro del Teclado Numerico) 
                (e.KeyChar != (char)Keys.Return) &&
                // y si no es Delete(Del o Suprimir, Supr)  
                (e.KeyChar != (char)Keys.Delete))
            {
                // Entonces 
                // Lanzamos Mensaje diciendo que solo permite numeros 
                MessageBox.Show("Solo se permiten numeros en el Campo Telefono",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Habilitamos el Handled = true para que no escriba nada si no son las teclas validadas 
                e.Handled = true;
                //Retornamos return; 
            }
            // entonces si las teclas son las permitidas Validamos
            // si se presiona Enter 
            else if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                // mandamos un Tab para ir al siguiene campo 
            }
            // si se presiona Return (Intro del Teclado Numerico) 
            else if (e.KeyChar == (char)Keys.Return)
            {
                SendKeys.Send("{TAB}");
                // mandamos un Tab para ir al siguiene campo 
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                Regex reg = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!reg.IsMatch(txtEmail.Text))
                {
                    MessageBox.Show("Formato no valido de Email", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}
