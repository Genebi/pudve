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
    public partial class Empresas : Form
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
        string buscar, buscarempresa;

        // variables para poder hacer el recorrido y asignacion de los valores que estan el base de datos
        int index;
        DataTable dt, dtgv;
        DataRow row, rows;

        // variables para poder tomar el valor de los TxtBox y tambien hacer las actualizaciones
        // del valor que proviene de la base de datos ó tambien actualizar la Base de Datos
        string id;
        string nomComp; string rfc;

        // Variable para poder saber que tipo de persona 
        // es el cliente que inicio sesion en el Pudve
        string tipoPersona;

        private void Empresas_Load(object sender, EventArgs e)
        {
            // asignamos el valor de userName que sea
            // el valor que tiene userNickUsuario en el formulario Principal
            userName = FormPrincipal.userNickName;
            passwordUser = FormPrincipal.userPass;
            consulta();

            // para agregar dinamicamente el boton en el DataGridView
            DataGridViewButtonColumn btnClm = new DataGridViewButtonColumn();
            btnClm.Name = "Entrar";

            // agregamos el boton en la ultima columna
            DGVListaEmpresas.Columns.Add(btnClm);
        }

        private void DGVListaEmpresas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // nos aseguramos que el DataGridView tenga por lo menos una fila en sus registros
            if (e.ColumnIndex >= 0 && this.DGVListaEmpresas.Columns[e.ColumnIndex].Name == "Entrar" && e.RowIndex >= 0)
            {
                // aqui indicamos que repinte el DataGridView 
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                // aqui agregamos el boton en la columna llamada Entrar
                DataGridViewButtonCell celBoton = this.DGVListaEmpresas.Rows[e.RowIndex].Cells["Entrar"] as DataGridViewButtonCell;

                // aqui tomamos un archivo .ico y lo insertamos en el boton
                //Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"\" + "iconfinder_Import.ico");
                // aqui le configuramos los margenes 
                //e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                // aqui se aplica los margenes en el icono del boton
                //this.DGVListaEmpresas.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
                //this.DGVListaEmpresas.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;

                e.Handled = true;
            }
        }

        private void btnNvaEmpresa_Click(object sender, EventArgs e)
        {
            NvaEmpresa nvaEmp = new NvaEmpresa();
            nvaEmp.ShowDialog();
        }

        // funcion para poder cargar los datos segun corresponda en el TxtBox que corresponda
        public void data()
        {
            // ponemos el index en 0 para asi aobtener siempre
            // la primera fila de la tabla y asi asignar los
            // valores a las variables y presentarlas en los
            // Label correspondientes.
            index = 0;

            /****************************************************
            *   obtenemos los datos almacenados en el dt        *
            *   y se los asignamos a cada uno de los variables  *
            ****************************************************/
            nomComp = dt.Rows[index]["NombreCompleto"].ToString();
            rfc = dt.Rows[index]["RFC"].ToString();
            tipoPersona = dt.Rows[index]["TipoPersona"].ToString();

            /****************************************
            *   ponemos los datos en los Label      *
            *   almancenados en las variables       *
            *   para mostrarlos en la Forma         *
            ****************************************/
            LblNombreUsr.Text = nomComp;
            LblTipoPersona.Text = tipoPersona;
            LblRFC.Text = rfc;
        }

        public void consulta()
        {
            index = 0;
            DGVListaEmpresas.Rows.Clear();
            buscar = "SELECT * FROM Usuarios WHERE Usuario = '" + userName + "' AND Password = '" + passwordUser + "'";
            // almacenamos el resultado de la Funcion CargarDatos
            // que esta en la calse Consultas
            dt = cn.CargarDatos(buscar);
            // almacenamos el resultado de la consulta en el dt
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
            // almacenamos el ID del usuario que inicio sesion
            // en el sistema para poder hacer la relacion de
            // el con las empresas dadas de alta con su usuario
            id = dt.Rows[index]["ID"].ToString();
            // llamamos la funcion data()
            data();
            // String para hacer la consulta filtrada sobre
            // el usuario que inicia la sesion y tambien saber
            // que empresas son las que ha registrado este usuario
            buscarempresa = @"  SELECT 
	                                emp.ID_Empresa AS 'Identificador de la Empresa',
	                                emp.Usuario AS 'Usuario', 
	                                emp.NombreCompleto AS 'Nombre Comercial', 
	                                emp.RFC AS 'R.F.C.' 
                                FROM 
	                                Usuarios u 
                                LEFT JOIN 
	                                Empresas emp 
                                ON 
	                                u.ID LIKE emp.ID_Usuarios AND u.ID = '" + id + "'";
            // Llenamos el contenido del DataGridView
            // con el resultado de la consulta
            DGVListaEmpresas.DataSource = cn.GetEmpresas(buscarempresa);
        }

        public Empresas()
        {
            InitializeComponent();
        }
    }
}
