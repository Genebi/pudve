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
        int caducidad = 0;
        public categoriaSubdetalle(string accion = "Nuevo", string detalle = "")
        {
            InitializeComponent();
            operacion = accion;
            subdetalle = detalle;
        }

        private void categoriaSubdetalle_Load(object sender, EventArgs e)
        {
            cbTipoDeDatos.SelectedIndex = 0;
            if (operacion == "Editar")
            {
                using (DataTable datosEditar = cn.CargarDatos($"SELECT * FROM subdetallesdeproducto WHERE ID = {subdetalle}"))
                {
                    txtSubDetalle.Text = datosEditar.Rows[0]["Categoria"].ToString();
                    cbTipoDeDatos.SelectedIndex = Int32.Parse(datosEditar.Rows[0]["TipoDato"].ToString());
                    cbTipoDeDatos.Enabled = false;
                    if (datosEditar.Rows[0]["esCaducidad"].ToString().Equals("1"))
                    {
                        chbCaducidad.Visible = true;
                        chbCaducidad.Checked = true;
                    }
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
            var subDetalle = cn.CargarDatos($"SELECT Categoria FROM subdetallesdeproducto WHERE IDProducto = '{Productos.idProductoAgregarSubdetalle}' AND IDUsuario = '{FormPrincipal.userID}' AND Categoria = '{txtSubDetalle.Text}' AND Activo = 1");
            


                if (operacion == "Nuevo")
                {
                if (!subDetalle.Rows.Count.Equals(0))
                {
                    MessageBox.Show("Este producto ya cuenta con esta Categoria");
                    return;
                }
                cn.EjecutarConsulta($"INSERT INTO subdetallesdeproducto (IDProducto, IDUsuario, Categoria, Subdetalle, Stock, TipoDato, esCaducidad) VALUES ('{Productos.idProductoAgregarSubdetalle}', '{FormPrincipal.userID}', '{txtSubDetalle.Text}', '{"NA"}', '{stockActual}', '{cbTipoDeDatos.SelectedIndex}',{caducidad})");
                }
                else
                {
                    cn.EjecutarConsulta($"UPDATE subdetallesdeproducto SET Categoria = '{txtSubDetalle.Text}' esCaducidad = {caducidad} WHERE ID = {subdetalle}");
                    subdetalle = txtSubDetalle.Text;
                    cambio = true;
                }
            this.Close();
        }

        private void pboxBorrar_Click(object sender, EventArgs e)
        {
            cn.EjecutarConsulta($"UPDATE subdetallesdeproducto SET Activo = 0 WHERE ID = {subdetalle}");
            cambio = true;
            this.Close();
        }

        private void cbTipoDeDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            chbCaducidad.Checked = false;
            gbCad.Visible = false;
            chbCaducidad.Visible = false;
            if (cbTipoDeDatos.SelectedIndex.Equals(0))
            {
                using (DataTable dt = cn.CargarDatos($"SELECT ID FROM subdetallesdeproducto WHERE esCaducidad = 1 AND IDProducto = {Productos.idProductoAgregarSubdetalle} AND IDUsuario ={FormPrincipal.userID} AND Activo = 1"))
                {
                    if (dt.Rows.Count.Equals(0))
                    {
                        gbCad.Visible = true;
                        chbCaducidad.Visible = true;
                    }
                }
            }
        }

        private void chbCaducidad_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCaducidad.Checked)
            {
                caducidad = 1;
                gbCad.Enabled = true;
            }
            else
            {
                caducidad = 0;
                gbCad.Enabled = false;
            }
        }
    }
}
