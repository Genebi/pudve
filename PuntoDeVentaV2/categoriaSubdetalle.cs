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
    public partial class categoriaSubdetalle : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        string operacion = string.Empty;
        public string subdetalle = string.Empty;
        public bool cambio = false;
        public categoriaSubdetalle(string accion = "Nuevo",string detalle = "")
        {
            InitializeComponent();
            operacion = accion;
            subdetalle = detalle;
        }

        private void categoriaSubdetalle_Load(object sender, EventArgs e)
        {
            cbTipoDeDatos.SelectedIndex = 0;
            if (operacion=="Editar")
            {
                using (DataTable datosEditar = cn.CargarDatos($"SELECT * FROM subdetallesdeproducto WHERE ID = {subdetalle}"))
                {
                    txtSubDetalle.Text = datosEditar.Rows[0]["Categoria"].ToString();
                    cbTipoDeDatos.SelectedIndex = Int32.Parse(datosEditar.Rows[0]["TipoDato"].ToString());
                    cbTipoDeDatos.Enabled = false;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSubDetalle.Text))
            {
                MessageBox.Show($"Es necesario nombrar el Sub Detallle", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var datos = cn.CargarDatos($"SELECT stock FROM productos WHERE ID = '{Productos.idProductoAgregarSubdetalle}' AND IDUsuario = '{FormPrincipal.userID}' AND `Status` = 1");
            decimal stockActual = 0;

            if (!datos.Rows.Count.Equals(0))
            {
                stockActual = Convert.ToDecimal(datos.Rows[0]["stock"].ToString());
            }
            var subDetalle = cn.CargarDatos($"SELECT Categoria FROM subdetallesdeproducto WHERE IDProducto = '{Productos.idProductoAgregarSubdetalle}' AND IDUsuario = '{FormPrincipal.userID}' AND Categoria = '{txtSubDetalle.Text}'");
            if (!subDetalle.Rows.Count.Equals(0))
            {
                MessageBox.Show("Este producto ya cuenta con esta Categoria");
                return;
            }
            else
            {
                if (operacion == "Nuevo")
                {
                    cn.EjecutarConsulta($"INSERT INTO subdetallesdeproducto (IDProducto, IDUsuario, Categoria, Subdetalle, Stock, TipoDato) VALUES ('{Productos.idProductoAgregarSubdetalle}', '{FormPrincipal.userID}', '{txtSubDetalle.Text}', '{"NA"}', '{stockActual}', '{cbTipoDeDatos.SelectedIndex}')");
                }
                else
                {
                    cn.EjecutarConsulta($"UPDATE subdetallesdeproducto SET Categoria = '{txtSubDetalle.Text}' WHERE ID = {subdetalle}");
                    subdetalle = txtSubDetalle.Text;
                    cambio = true;
                }
                
            }
            this.Close();
        }

    }
}
