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
        decimal stockTot = 0;
        int celdaCellClick;
        string tipoDato;
        string colDato="Nombre";
        string colID;
        string cat;

        List<string> idsADesHabilitar = new List<string>();

        DateTimePicker dateTimePicker1;

        DataTable dtDetallesSubdetalle = new DataTable();
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
            cat = categoria;
            idsADesHabilitar.Clear();
            dtDetallesSubdetalle.Clear();

            groupBox3.Visible = true;
            groupBox4.Visible = true;
            btnGuardar.Enabled = false;
            dtDetallesSubdetalle = cn.CargarDatos($"SELECT detallesubdetalle.ID, IF(subdetallesdeproducto.TipoDato = 0, detallesubdetalle.Fecha, IF( subdetallesdeproducto.TipoDato = 1, detallesubdetalle.Valor, detallesubdetalle.Nombre)) AS Valor, detallesubdetalle.Stock, productos.Stock AS TotalStock,subdetallesdeproducto.TipoDato,subdetallesdeproducto.ID AS SubID FROM subdetallesdeproducto LEFT JOIN detallesubdetalle ON (detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID AND detallesubdetalle.Estado=1) INNER JOIN productos ON subdetallesdeproducto.IDProducto = productos.ID WHERE subdetallesdeproducto.Categoria = '{categoria}'");
            
            dgvDetallesSubdetalle.DataSource = dtDetallesSubdetalle;
            stockTot = Convert.ToDecimal(dtDetallesSubdetalle.Rows[0]["TotalStock"]);
            decimal total = 0;
            if (Decimal.TryParse(dtDetallesSubdetalle.Rows[0]["Stock"].ToString(), out total))
            {
                total = dgvDetallesSubdetalle.Rows.Cast<DataGridViewRow>()
                    .Sum(t => Convert.ToDecimal(t.Cells[3].Value));
            }
            
            lblStockRestanteNum.Text = (stockTot - total).ToString();
            tipoDato = dtDetallesSubdetalle.Rows[0]["TipoDato"].ToString();
            colID = dtDetallesSubdetalle.Rows[0]["SubID"].ToString();
            switch (tipoDato)
            {
                case "0":
                    dgvDetallesSubdetalle.Columns[2].DefaultCellStyle.Format = "yyyy-MM-dd";
                    colDato = "Fecha";
                    break;
                case "1":
                    dgvDetallesSubdetalle.Columns[2].DefaultCellStyle.Format = "N2";
                    colDato = "Valor";
                    break;
                default:
                    dgvDetallesSubdetalle.Columns[2].DefaultCellStyle.Format = "";
                    colDato = "Nombre";
                    break;
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


        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            foreach (DataRow registroDetalle in dtDetallesSubdetalle.Rows)
            {
                if (!string.IsNullOrEmpty(registroDetalle["Valor"].ToString()) && !string.IsNullOrEmpty(registroDetalle["Stock"].ToString()))
                {


                    if (string.IsNullOrEmpty(registroDetalle["ID"].ToString()))
                    {
                        string consulta = $"INSERT INTO detallesubdetalle (IDsubdetalle, {colDato}, Stock)";
                        consulta += $"VALUES ('{colID}', '{registroDetalle["Valor"].ToString()}', '{registroDetalle["Stock"].ToString()}')";

                        cn.EjecutarConsulta(consulta);
                    }
                    else
                    {
                        string consulta = $"UPDATE detallesubdetalle SET {colDato}='{registroDetalle["Valor"].ToString()}', Stock={registroDetalle["Stock"].ToString()} WHERE ID = {registroDetalle["ID"].ToString()}";

                        cn.EjecutarConsulta(consulta);
                    }
                }
                else
                {
                    MessageBox.Show($"No puedes dejar espacios en blanco", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (!idsADesHabilitar.Count.Equals(0))
            {
                foreach (string id in idsADesHabilitar)
                {
                    string consulta = $"UPDATE detallesubdetalle SET Estado = 0 WHERE ID = {id}";
                    cn.EjecutarConsulta(consulta);
                }
            }

            cargarsubCategorias(cat);
        }

        private void btnAgregarSubDetalle_Click(object sender, EventArgs e)
        {
            if (!dtDetallesSubdetalle.Rows.Count.Equals(0))
            {
                if (!string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[(dtDetallesSubdetalle.Rows.Count) - 1]["Valor"].ToString()) && !string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[(dtDetallesSubdetalle.Rows.Count) - 1]["Stock"].ToString()))
                {
                    dtDetallesSubdetalle.Rows.Add();
                }
                else
                {
                    MessageBox.Show($"Termina de llenar el registro anterior antes de crear uno nuevo.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                dtDetallesSubdetalle.Rows.Add();
            }
        }

        private void dgvDetallesSubdetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"El formato introducido no es valido", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        private void dgvDetallesSubdetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 3)
            //{
            //    decimal test;
            //    celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;

            //    if (decimal.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[3].Value.ToString(), out test))
            //    {
                    decimal total = dgvDetallesSubdetalle.Rows.Cast<DataGridViewRow>()
                    .Sum(t => Convert.ToDecimal(t.Cells[3].Value));
                    lblStockRestanteNum.Text = (stockTot - total).ToString();
                    if (total == stockTot)
                    {
                        btnGuardar.Enabled = true;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                    }
            //    }
            //    else
            //    {
            //        dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[3].Value = "0";
            //    }
            //}

            if (e.ColumnIndex == 2)
            {
                switch (tipoDato)
                {
                    case "0":
                        DateTime DTparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!DateTime.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[2].Value.ToString(), out DTparser))
                        {
                            MessageBox.Show($"El formato introducido no es valido  no podras terminar el proceso sin corregir (solamente se aceptan fechas)", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "1":
                        decimal DCparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!Decimal.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[2].Value.ToString(), out DCparser))
                        {
                            MessageBox.Show($"El formato introducido no es valido, no podras terminar el proceso sin corregir (solamente se aceptan valores numericos)", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void lblStockRestanteNum_TextChanged(object sender, EventArgs e)
        {
            if (lblStockRestanteNum.Text.Contains('-'))
            {
                lblStockRestanteNum.ForeColor = Color.Red;
            }
            else
            {
                lblStockRestanteNum.ForeColor = Color.Black;
            }
        }

        private void dgvDetallesSubdetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && tipoDato=="0")
            {
                //Creamos el control por código
                dateTimePicker1 = new DateTimePicker();

                //Agregamos el control de fecha dentro del DataGridView 
                dgvDetallesSubdetalle.Controls.Add(dateTimePicker1);

                // Hacemos que el control sea invisible (para que no moleste visualmente)
                dateTimePicker1.Visible = false;

                // Establecemos el formato (depende de tu localización en tu PC)
                dateTimePicker1.Format = DateTimePickerFormat.Short;  

                // Agregamos el evento para cuando seleccionemos una fecha
                dateTimePicker1.TextChanged += new EventHandler(dateTimePicker1_OnTextChange);

                // Lo hacemos visible
                dateTimePicker1.Visible = true;

                // Creamos un rectángulo que representa el área visible de la celda
                Rectangle rectangle1 = dgvDetallesSubdetalle.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                //Establecemos el tamaño del control DateTimePicker (que sería el tamaño total de la celda)
                dateTimePicker1.Size = new Size(rectangle1.Width, rectangle1.Height);

                // Establecemos la ubicación del control
                dateTimePicker1.Location = new Point(rectangle1.X, rectangle1.Y);

                // Generamos el evento de cierre del control fecha
                dateTimePicker1.CloseUp += new EventHandler(dateTimePicker1_CloseUp);
            }
            if (e.ColumnIndex == 0)
            {
                if (!string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[e.RowIndex]["ID"].ToString()))
                {
                    idsADesHabilitar.Add(dtDetallesSubdetalle.Rows[e.RowIndex]["ID"].ToString());
                }
                dtDetallesSubdetalle.Rows.RemoveAt(e.RowIndex);

                decimal total = dgvDetallesSubdetalle.Rows.Cast<DataGridViewRow>()
                    .Sum(t => Convert.ToDecimal(t.Cells[3].Value));
                lblStockRestanteNum.Text = (stockTot - total).ToString();
            }
        }

        private void dateTimePicker1_OnTextChange(object sender, EventArgs e)
        {
            //Asignamos a la celda el valor de la feha seleccionada
            dgvDetallesSubdetalle.CurrentCell.Value = DateTime.Parse(dateTimePicker1.Text.ToString()).ToString("yyyy-MM-dd");
        }

        void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            //Volvemos a colocar en invisible el control
            dateTimePicker1.Visible = false;
        }
    }
}

