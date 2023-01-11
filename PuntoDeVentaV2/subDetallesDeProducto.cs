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
    public partial class subDetallesDeProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public subDetallesDeProducto()
        {
            InitializeComponent();
            //TIPOS DE DATO PARA LA DB
            //--0 DATE
            //--1 Decimal
            //--2 String
        }

        private void subDetallesDeProducto_Load(object sender, EventArgs e)
        {
            fLPLateralCategorias.AutoScroll = true;
            fLPLateralCategorias.HorizontalScroll.Enabled = false;
            fLPLateralCategorias.HorizontalScroll.Visible = false;
            fLPLateralCategorias.VerticalScroll.Enabled = true;
            fLPLateralCategorias.VerticalScroll.Visible = true;

            cargarCategorias();

            if (AgregarEditarProducto.DatosSourceFinal == 1)
            {
                //MessageBox.Show("Agregar");
            }
            else if (AgregarEditarProducto.DatosSourceFinal == 2)
            {
                lblNombreProducto.Text = AgregarEditarProducto.nombreProdSubDetalles;
                //MessageBox.Show("Editar");
            }
        }

        private void cargarCategorias()
        {
            var datosCategoria = cn.CargarDatos($"SELECT Categoria FROM subdetallesdeproducto WHERE IDProducto = '{Productos.idProductoAgregarSubdetalle}' AND IDUsuario = '{FormPrincipal.userID}' AND Activo = 1");

            foreach (DataRow item in datosCategoria.Rows)
            {
                Label Categoria = new Label();
                Label espacio = new Label();

                Categoria.Click += new EventHandler(LB_Click);
                Categoria.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                Categoria.TextAlign = ContentAlignment.MiddleCenter;
                Categoria.Font = new Font("Arial", 11);
                Categoria.Size = new Size(224, 20);


                espacio.AutoSize = true;

                Categoria.Text = item[0].ToString();
                fLPLateralCategorias.Controls.Add(espacio);
                fLPLateralCategorias.Controls.Add(Categoria);
            }
        }

        private void LB_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            LbNombreCategoria.Text = "Categoria: " + lbl.Text;
            cargarsubCategorias(lbl.Text);
        }

        private void cargarsubCategorias(string categoria)
        {
            using (DataTable dtDetallesSubdetalle = cn.CargarDatos($"SELECT * FROM detallesubdetalle INNER JOIN subdetallesdeproducto ON detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID WHERE subdetallesdeproducto.Categoria = '{categoria}'"))
            {
                if (!dtDetallesSubdetalle.Rows.Count.Equals(0))
                {
                    dgvDetallesSubdetalle.DataSource = dtDetallesSubdetalle;
                }
            }
        }

        private void btnAddDetalle_Click(object sender, EventArgs e)
        {
            categoriaSubdetalle subdetalle = new categoriaSubdetalle();
            subdetalle.ShowDialog();
            fLPLateralCategorias.Controls.Clear();
            cargarCategorias();
        }

        private void btnGuardarDetalles_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
