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
        string idProducto;
        int celdaCellClick;
        string tipoDato;
        string colDato="Nombre";
        string colID;
        string cat;

        public bool finalizado = false;


        public List<string> subdetallesVenta = new List<string>();
        public List<string> updatesVenta = new List<string>();

        string accion;

        List<string> idsADesHabilitar = new List<string>();

        DateTimePicker dateTimePicker1;

        DataTable dtDetallesSubdetalle = new DataTable();
        public subDetallesDeProducto(string producto, string operacion = "Nuevo",decimal cantidadVenta=0)
        {
            InitializeComponent();
            idProducto = producto;
            accion = operacion;
            stockTot = cantidadVenta;
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
            using (DataTable dtNombreProducto = cn.CargarDatos($"SELECT Nombre FROM Productos WHERE ID = {idProducto} AND Status = 1 AND IDUsuario = {FormPrincipal.userID}"))
            {
                lblNombreProducto.Text = dtNombreProducto.Rows[0]["Nombre"].ToString();
            }
            

            //if (AgregarEditarProducto.DatosSourceFinal == 1)
            //{
            //    //MessageBox.Show("Agregar");
            //}
            //else if (AgregarEditarProducto.DatosSourceFinal == 2)
            //{

            //    //MessageBox.Show("Editar");
            //}
            if (accion=="Venta")
            {
                groupBox1.Visible = false;
            }
        }

        private void cargarCategorias()
        {
            DataTable datosCategoria = new DataTable();
            switch (accion)
            {
                case "Nuevo":
                    datosCategoria = cn.CargarDatos($"SELECT Categoria FROM subdetallesdeproducto WHERE IDProducto = '{idProducto}' AND IDUsuario = '{FormPrincipal.userID}' AND Activo = 1");
                    break;
                case "Venta":
                    string ignores=string.Empty;
                    if (idsADesHabilitar.Count>0)
                    {
                        foreach (string ID in idsADesHabilitar)
                        {
                            ignores += $"AND (NOT subdetallesdeproducto.ID={ID}) ";
                        }
                    }
                    datosCategoria = cn.CargarDatos($"SELECT Categoria FROM subdetallesdeproducto INNER JOIN detallesubdetalle ON subdetallesdeproducto.ID = detallesubdetalle.IDSubDetalle WHERE IDProducto = '{idProducto}' AND IDUsuario = '{FormPrincipal.userID}' AND Activo = 1 {ignores} GROUP BY Categoria");
                    if (datosCategoria.Rows.Count.Equals(0))
                    {
                        finalizado = true;
                        this.Close();
                    }
                    btnGuardarDetalles.Enabled = false;
                    break;
                default:
                    break;
            }
            

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
            
            dtDetallesSubdetalle.Clear();
            dgvDetallesSubdetalle.Visible = true;

            groupBox4.Visible = true;
            btnGuardar.Enabled = false;

            switch (accion)
            {
                case "Nuevo":
                    idsADesHabilitar.Clear();
                    dtDetallesSubdetalle = cn.CargarDatos($"SELECT detallesubdetalle.ID, IF(subdetallesdeproducto.TipoDato = 0, detallesubdetalle.Fecha, IF( subdetallesdeproducto.TipoDato = 1, detallesubdetalle.Valor, detallesubdetalle.Nombre)) AS Valor, detallesubdetalle.Stock, productos.Stock AS TotalStock,subdetallesdeproducto.TipoDato,subdetallesdeproducto.ID AS SubID FROM subdetallesdeproducto LEFT JOIN detallesubdetalle ON (detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID AND detallesubdetalle.Estado=1) INNER JOIN productos ON subdetallesdeproducto.IDProducto = productos.ID WHERE subdetallesdeproducto.Categoria = '{categoria}' AND productos.id = {idProducto} AND subdetallesdeproducto.Activo = 1");

                    dgvDetallesSubdetalle.DataSource = dtDetallesSubdetalle;

                    if (string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[0]["Stock"].ToString()))
                    {
                        dtDetallesSubdetalle.Rows[0]["Stock"] = "0";
                    }


                    groupBox3.Visible = true;
                    stockTot = Convert.ToDecimal(dtDetallesSubdetalle.Rows[0]["TotalStock"]);

                    break;
                case "Venta":
                    dtDetallesSubdetalle = cn.CargarDatos($"SELECT detallesubdetalle.ID, IF(subdetallesdeproducto.TipoDato = 0, detallesubdetalle.Fecha, IF( subdetallesdeproducto.TipoDato = 1, detallesubdetalle.Valor, detallesubdetalle.Nombre)) AS Valor, detallesubdetalle.Stock, subdetallesdeproducto.TipoDato,subdetallesdeproducto.ID AS SubID, 0 FROM subdetallesdeproducto LEFT JOIN detallesubdetalle ON (detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID AND detallesubdetalle.Estado=1) INNER JOIN productos ON subdetallesdeproducto.IDProducto = productos.ID WHERE subdetallesdeproducto.Categoria = '{categoria}' AND subdetallesdeproducto.Activo = 1 AND productos.id = {idProducto}");

                    dgvDetallesSubdetalle.Columns[3].Visible = true;
                    dgvDetallesSubdetalle.Columns[1].Visible = false;
                    groupBox3.Visible = false;
                    dgvDetallesSubdetalle.DataSource = dtDetallesSubdetalle;
                    btnGuardar.Enabled = false;


                    dgvDetallesSubdetalle.Columns[4].ReadOnly = true;
                    dgvDetallesSubdetalle.Columns[3].ReadOnly = true;
                    if (!dgvDetallesSubdetalle.Columns[3].Visible)
                    {
                        cargarsubCategorias(categoria);
                    }
                    break;
                default:

                    break;
            }


            calcularRestante();
            
            

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

        private void calcularRestante()
        {
            decimal total=0;
            if (dtDetallesSubdetalle.Rows.Count<1)
            {
                return;
            }
            switch (accion)
            {
                case "Nuevo":
                        total = dgvDetallesSubdetalle.Rows.Cast<DataGridViewRow>()
                            .Sum(t => Convert.ToDecimal(t.Cells[4].Value));
                    break;
                case "Venta":
                        total = dgvDetallesSubdetalle.Rows.Cast<DataGridViewRow>()
                            .Sum(t => Convert.ToDecimal(t.Cells[7].Value));
                    break;
                default:
                    break;
            }
            
            lblStockRestanteNum.Text = (stockTot - total).ToString();

            if (total == stockTot)
            {
                btnGuardar.Enabled = true;
            }
            else
            {
                btnGuardar.Enabled = false;
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

            switch (accion)
            {
                case "Nuevo":
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
                    break;
                case "Venta":
                    foreach (DataRow registroDetalle in dtDetallesSubdetalle.Rows)
                    {
                        if (Convert.ToDecimal(registroDetalle["0"].ToString())>0)
                        {
                            string consultaGuardada = $"INSERT INTO DetalleSubDetalleVenta (IDDetalleSubDetalle, Cantidad,IDVenta)";
                            consultaGuardada += $"VALUES ('{registroDetalle["ID"].ToString()}', '{registroDetalle["0"].ToString()}',";
                            subdetallesVenta.Add(consultaGuardada);

                            string updateGuardado = $"UPDATE detallesubdetalle SET Stock = Stock -{registroDetalle["0"].ToString()} WHERE ID = {registroDetalle["ID"].ToString()}";
                            updatesVenta.Add(updateGuardado);
                        }
                    }
                    idsADesHabilitar.Add(dtDetallesSubdetalle.Rows[0]["SubID"].ToString());
                    fLPLateralCategorias.Controls.Clear();
                    dgvDetallesSubdetalle.Visible = false;
                    cargarCategorias();
                    
                    break;
                default:
                    break;
            }
            
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

            decimal test; celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;

            if (decimal.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[7].Value?.ToString(), out test))
            {
                if (dgvDetallesSubdetalle.CurrentCell.ColumnIndex == 7 && accion == "Venta")
                {
                    if (Convert.ToDecimal(dtDetallesSubdetalle.Rows[e.RowIndex]["0"].ToString()) > Convert.ToDecimal(dtDetallesSubdetalle.Rows[e.RowIndex]["Stock"].ToString()))
                    {
                        MessageBox.Show($"No existe suficientes stock", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dtDetallesSubdetalle.Rows[e.RowIndex]["0"] = dtDetallesSubdetalle.Rows[e.RowIndex]["Stock"];
                        return;
                    }
                }
            }

            if (e.ColumnIndex == 3)
            {
                switch (tipoDato)
                {
                    case "0":
                        DateTime DTparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!DateTime.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[3].Value.ToString(), out DTparser))
                        {
                            MessageBox.Show($"El formato introducido no es valido  no podras terminar el proceso sin corregir (solamente se aceptan fechas)", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtDetallesSubdetalle.Rows[e.RowIndex]["Valor"] = DateTime.Now.ToString("yyyy-MM-dd");
                            return;
                        }
                        break;
                    case "1":
                        decimal DCparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!Decimal.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[3].Value.ToString(), out DCparser))
                        {
                            MessageBox.Show($"El formato introducido no es valido, no podras terminar el proceso sin corregir (solamente se aceptan valores numericos)", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dtDetallesSubdetalle.Rows[e.RowIndex]["Valor"] = "0.00";
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
            string col = string.Empty;
            switch (accion)
            {
                case "Nuevo":
                    col = "Stock";
                    break;
                case "Venta":
                    col = "0";
                    break;
                default:
                    break;
            }
            if (Decimal.TryParse(dtDetallesSubdetalle.Rows[e.RowIndex][col].ToString(), out test))
            {
                calcularRestante();
            }
            else
            {
                return;
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
            if (e.ColumnIndex == 3 && tipoDato=="0")
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
            if (e.ColumnIndex == 1)
            {
                if (dtDetallesSubdetalle.Rows.Count>1)
                {
                    if (!string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[e.RowIndex]["ID"].ToString()))
                    {
                        idsADesHabilitar.Add(dtDetallesSubdetalle.Rows[e.RowIndex]["ID"].ToString());
                    }
                    dtDetallesSubdetalle.Rows.RemoveAt(e.RowIndex);
                    calcularRestante();
                }
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

