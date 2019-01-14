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
    public partial class MisDatos : Form
    {
        Conexion cn = new Conexion();

        // declaramos la variable que almacenara el valor de userNickName
        public string userName;
        public string passwordUser;

        // variables para poder hacer las consulta y actualizacion
        string buscar;
        string actualizar;

        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string nomComp;     string rfc;
        string calle;       string numExt;
        string numInt;      string colonia;
        string mpio;        string estado;
        string codPostal;   string email;
        string telefono;    string regimen;

        // variables para poder hacer el recorrido y asignacion de los valores que estan el base de datos
        int index;
        DataTable dt;
        DataRow row;

        public MisDatos()
        {
            InitializeComponent();
        }

        // funcion para poder cargar los datos segun corresponda en el TxtBox que corresponda
        public void data()
        {
            for (index = 0; index < dt.Rows.Count; index++)
            {
                id = dt.Rows[index]["ID"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                nomComp = dt.Rows[index]["NombreCompleto"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                rfc = dt.Rows[index]["RFC"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                calle = dt.Rows[index]["Calle"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                numExt = dt.Rows[index]["NoExterior"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                numInt = dt.Rows[index]["NoInterior"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                colonia = dt.Rows[index]["Colonia"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                mpio = dt.Rows[index]["Municipio"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                estado = dt.Rows[index]["Estado"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                codPostal = dt.Rows[index]["CodigoPostal"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                email = dt.Rows[index]["Email"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                telefono = dt.Rows[index]["Telefono"].ToString();
            }
            for (index = 0; index < dt.Rows.Count; index++)
            {
                regimen = dt.Rows[index]["Regimen"].ToString();
            }

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
        }

        // funcion para poder cargar los datos al abrir la forma
        public void consulta()
        {
            buscar = "SELECT * FROM Usuarios WHERE Usuario = '" + userName + "' AND Password = '" + passwordUser + "'";
            dt = cn.CargarDatos(buscar);
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
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
                                             +"' WHERE ID = '"+ id +"'";

                cn.EjecutarConsulta(actualizar);
                consulta();
            }
        }
    }
}
