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
        bool restando = false;

        public List<string> subdetallesVenta = new List<string>();
        public List<string> updates = new List<string>();

        string accion;

        List<string> idsADesHabilitar = new List<string>();

        DateTimePicker dateTimePicker1;

        DataTable dtDetallesSubdetalle = new DataTable();
        public subDetallesDeProducto(string producto, string operacion = "Nuevo",decimal cantidad=0)
        {
            InitializeComponent();
            idProducto = producto;
            accion = operacion;
            stockTot = cantidad;
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
            
            if (accion=="Venta" || accion == "Inventario")
            {
                groupBox1.Visible = false;
                if (stockTot < 0)
                {
                    restando = true;
                    stockTot *= -1;
                }
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
                    btnGuardar.Enabled = false;
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
                    break;
                case "Inventario":
                    btnGuardar.Enabled = false;
                    string ignoresIn = string.Empty;
                    if (idsADesHabilitar.Count > 0)
                    {
                        foreach (string ID in idsADesHabilitar)
                        {
                            ignoresIn += $"AND (NOT subdetallesdeproducto.ID={ID}) ";
                        }
                    }
                    datosCategoria = cn.CargarDatos($"SELECT Categoria FROM subdetallesdeproducto INNER JOIN detallesubdetalle ON subdetallesdeproducto.ID = detallesubdetalle.IDSubDetalle WHERE IDProducto = '{idProducto}' AND IDUsuario = '{FormPrincipal.userID}' AND Activo = 1 {ignoresIn} GROUP BY Categoria");
                    if (datosCategoria.Rows.Count.Equals(0))
                    {
                        finalizado = true;
                        this.Close();
                    }
                    break;
                default:
                    break;
            }
            

            foreach (DataRow item in datosCategoria.Rows)
            {
                Label Categoria = new Label();
                Label espacio = new Label();

                Categoria.Click += new EventHandler(LB_Click);
                Categoria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                Categoria.BackColor = Color.LightGray;
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
            if (dgvDetallesSubdetalle.Visible)
            {
                if (btnGuardar.Enabled)
                {
                    guardarDatos(false);
                }
            }
            cargarsubCategorias(lbl.Text);
        }

        private void cargarsubCategorias(string categoria)
        {
            cat = categoria;

            flowLayoutPanel1.Visible = true;
            
            dtDetallesSubdetalle.Clear();
            dgvDetallesSubdetalle.Visible = true;

            groupBox4.Visible = true;

            switch (accion)
            {
                case "Nuevo":
                    idsADesHabilitar.Clear();
                    dtDetallesSubdetalle = cn.CargarDatos($"SELECT detallesubdetalle.ID, IF(subdetallesdeproducto.TipoDato = 0, detallesubdetalle.Fecha, IF( subdetallesdeproducto.TipoDato = 1, detallesubdetalle.Valor, detallesubdetalle.Nombre)) AS Valor, detallesubdetalle.Stock, productos.Stock AS TotalStock,subdetallesdeproducto.TipoDato,subdetallesdeproducto.ID AS SubID FROM subdetallesdeproducto LEFT JOIN detallesubdetalle ON (detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID AND detallesubdetalle.Estado=1) INNER JOIN productos ON subdetallesdeproducto.IDProducto = productos.ID WHERE subdetallesdeproducto.Categoria = '{categoria}' AND productos.id = {idProducto} AND subdetallesdeproducto.Activo = 1");

                    pboxEditar.Visible = true;
                    dgvDetallesSubdetalle.DataSource = dtDetallesSubdetalle;

                    if (string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[0]["Stock"].ToString()))
                    {
                        dtDetallesSubdetalle.Rows[0]["Stock"] = "0";
                    }


                    groupBox3.Visible = true;
                    stockTot = Convert.ToDecimal(dtDetallesSubdetalle.Rows[0]["TotalStock"]);

                    break;
                case "Venta":
                    dtDetallesSubdetalle = cn.CargarDatos($"SELECT detallesubdetalle.ID, IF(subdetallesdeproducto.TipoDato = 0, detallesubdetalle.Fecha, IF( subdetallesdeproducto.TipoDato = 1, detallesubdetalle.Valor, detallesubdetalle.Nombre)) AS Valor, detallesubdetalle.Stock, subdetallesdeproducto.TipoDato,subdetallesdeproducto.ID AS SubID, 0 FROM subdetallesdeproducto LEFT JOIN detallesubdetalle ON (detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID AND detallesubdetalle.Estado=1) INNER JOIN productos ON subdetallesdeproducto.IDProducto = productos.ID WHERE subdetallesdeproducto.Categoria = '{categoria}' AND subdetallesdeproducto.Activo = 1 AND productos.id = {idProducto} AND detallesubdetalle.Stock > 0 ");

                    dgvDetallesSubdetalle.Columns[3].Visible = true;
                    dgvDetallesSubdetalle.Columns[1].Visible = false;
                    groupBox1.Visible = false;
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
                case "Inventario":
                    dtDetallesSubdetalle = cn.CargarDatos($"SELECT detallesubdetalle.ID, IF(subdetallesdeproducto.TipoDato = 0, detallesubdetalle.Fecha, IF( subdetallesdeproducto.TipoDato = 1, detallesubdetalle.Valor, detallesubdetalle.Nombre)) AS Valor, detallesubdetalle.Stock, productos.Stock AS TotalStock,subdetallesdeproducto.TipoDato,subdetallesdeproducto.ID AS SubID,0 FROM subdetallesdeproducto LEFT JOIN detallesubdetalle ON (detallesubdetalle.IDSubDetalle = subdetallesdeproducto.ID AND detallesubdetalle.Estado=1) INNER JOIN productos ON subdetallesdeproducto.IDProducto = productos.ID WHERE subdetallesdeproducto.Categoria = '{categoria}' AND productos.id = {idProducto} AND subdetallesdeproducto.Activo = 1");

                    dgvDetallesSubdetalle.DataSource = dtDetallesSubdetalle;

                    dgvDetallesSubdetalle.Columns[0].Visible = false;
                    dgvDetallesSubdetalle.Columns[7].Visible = true;
                    

                    if (restando)
                    {
                        dgvDetallesSubdetalle.Columns[7].HeaderText = "Cantidad a disminuir";
                        
                        lblStockRestanteText.Text = "Por disminuir";
                    }
                    else
                    {
                        dgvDetallesSubdetalle.Columns[7].HeaderText = "Cantidad a aumentar";
                        lblStockRestanteText.Text = "Por aumentar";
                    }

                    dgvDetallesSubdetalle.Columns[3].ReadOnly = true;

                    groupBox1.Visible = false;
                    groupBox3.Visible = true;

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
                    total =suma("stock");
                    break;

                case "Inventario":
                case "Venta":
                    total =suma("0");
                    break;
                default:
                    break;
            }
            if ((stockTot - total)<0)
            {
                lblStockRestanteNum.Text =$"{(stockTot - total)}";
                lblStockRestanteNum.ForeColor = Color.Red;
            }
            else
            {
                lblStockRestanteNum.Text = (stockTot - total).ToString();
                lblStockRestanteNum.ForeColor = Color.Black;
            }
            
            if (total == stockTot)
            {
                btnGuardar.Enabled = true;
                if (accion != "Nuevo")
                {
                    guardarDatos(false);
                }
            }
            else
            {
                btnGuardar.Enabled = false;
            }
        }

        private decimal suma(string col)
        {
            decimal total = 0;
            foreach (DataRow dataRow in dtDetallesSubdetalle.Rows)
            {
                if (!string.IsNullOrEmpty(dataRow[col].ToString()))
                {
                    total += decimal.Parse(dataRow[col].ToString());
                }
            }
            return total;
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
            guardarDatos();
            this.Close();
        }

        private void guardarDatos(bool boton = true)
        {
            switch (accion)
            {
                case "Nuevo":

                    

                    foreach (DataRow registroDetalle in dtDetallesSubdetalle.Rows)
                    {
                        if (tipoDato=="0")
                        {
                            
                        }
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
                            if (boton)
                            {
                                MessageBox.Show($"No puedes dejar espacios en blanco", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
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
                        if (Convert.ToDecimal(registroDetalle["0"].ToString()) > 0)
                        {
                            string consultaGuardada = $"INSERT INTO DetalleSubDetalleVenta (IDDetalleSubDetalle, Cantidad,IDVenta)";
                            consultaGuardada += $"VALUES ('{registroDetalle["ID"].ToString()}', '{registroDetalle["0"].ToString()}',";
                            subdetallesVenta.Add(consultaGuardada);

                            string updateGuardado = $"UPDATE detallesubdetalle SET Stock = Stock -{registroDetalle["0"].ToString()} WHERE ID = {registroDetalle["ID"].ToString()}";
                            updates.Add(updateGuardado);
                        }
                    }
                    updates.Add("UPDATE detallesubdetalle SET Estado = 0 WHERE Stock = 0 ");
                    idsADesHabilitar.Add(dtDetallesSubdetalle.Rows[0]["SubID"].ToString());
                    fLPLateralCategorias.Controls.Clear();
                    dgvDetallesSubdetalle.Visible = false;
                    cargarCategorias();

                    break;
                case "Inventario":

                    foreach (DataRow registroDetalle in dtDetallesSubdetalle.Rows)
                    {
                        if (!string.IsNullOrEmpty(registroDetalle["Valor"].ToString()) && !string.IsNullOrEmpty(registroDetalle["Stock"].ToString()))
                        {


                            if (string.IsNullOrEmpty(registroDetalle["ID"].ToString()))
                            {
                                string consulta = $"INSERT INTO detallesubdetalle (IDsubdetalle, {colDato}, Stock)";
                                consulta += $"VALUES ('{colID}', '{registroDetalle["Valor"].ToString()}', '{registroDetalle["Cantidad"].ToString()}')";

                                updates.Add(consulta);
                            }
                            else
                            {
                                string symbol="+";
                                if (restando)
                                {
                                    symbol = "-";
                                }
                                string updateGuardado = $"UPDATE detallesubdetalle SET {colDato}='{registroDetalle["Valor"].ToString()}',Stock = Stock {symbol}{registroDetalle["0"].ToString()} WHERE ID = {registroDetalle["ID"].ToString()}";
                                updates.Add(updateGuardado);
                            }
                        }
                        else
                        {
                            if (boton)
                            {
                                MessageBox.Show($"No puedes dejar espacios en blanco", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            
                        }
                    }

                    updates.Add("UPDATE detallesubdetalle SET Estado = 0 WHERE Stock = 0 ");
                    idsADesHabilitar.Add(dtDetallesSubdetalle.Rows[0]["SubID"].ToString());
                    fLPLateralCategorias.Controls.Clear();
                    dgvDetallesSubdetalle.Visible = false;
                    cargarCategorias();
                    break;
                default:
                    break;
            }
        }

        private bool validarunique(string validate)
        {
            bool buleanomachin = true;
            int counter = 0;
            List<string> fechasuso = new List<string>();

                foreach (DataRow dataRow1 in dtDetallesSubdetalle.Rows)
                {
                fechasuso.Add(dataRow1["Valor"].ToString());
                }
            foreach (string item in fechasuso)
            {
                
                if (validate==item)
                {
                    counter++;
                }
                if (counter>1)
                {
                    buleanomachin = false;
                }
            }
            return buleanomachin;
        }

        private void btnAgregarSubDetalle_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            if (!dtDetallesSubdetalle.Rows.Count.Equals(0))
            {
                if (!string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[(dtDetallesSubdetalle.Rows.Count) - 1]["Valor"].ToString()))
                {
                    dtDetallesSubdetalle.Rows.Add();
                    dtDetallesSubdetalle.Rows[dtDetallesSubdetalle.Rows.Count-1]["Stock"] = "0.00";
                    

                    if (accion == "Inventario")
                    {
                        dtDetallesSubdetalle.Rows[dtDetallesSubdetalle.Rows.Count - 1]["0"] = "0.00";
                        dgvDetallesSubdetalle.CurrentCell = dgvDetallesSubdetalle.Rows[dgvDetallesSubdetalle.RowCount - 1].Cells[7];
                        dgvDetallesSubdetalle.Rows[dgvDetallesSubdetalle.CurrentRow.Index].Cells["Stock"].ReadOnly = true;
                    }
                    else
                    {
                        dgvDetallesSubdetalle.CurrentCell = dgvDetallesSubdetalle.Rows[dgvDetallesSubdetalle.RowCount - 1].Cells[3];
                    }

                    if (tipoDato == "0" && accion == "Nuevo")
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
                        Rectangle rectangle1 = dgvDetallesSubdetalle.GetCellDisplayRectangle(dgvDetallesSubdetalle.CurrentCell.ColumnIndex, dgvDetallesSubdetalle.CurrentCell.RowIndex, true);

                        //Establecemos el tamaño del control DateTimePicker (que sería el tamaño total de la celda)
                        dateTimePicker1.Size = new Size(rectangle1.Width, rectangle1.Height);

                        // Establecemos la ubicación del control
                        dateTimePicker1.Location = new Point(rectangle1.X, rectangle1.Y);

                        // Generamos el evento de cierre del control fecha
                        dateTimePicker1.CloseUp += new EventHandler(dateTimePicker1_CloseUp);

                        // Generamos el evento de cierre del control fecha
                        dateTimePicker1.LostFocus += new EventHandler(dateTimePicker1_loseFocus);
                    }

                }
                else
                {
                    MessageBox.Show($"Termina de llenar el registro anterior antes de crear uno nuevo.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvDetallesSubdetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"El formato introducido no es valido", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        private void dgvDetallesSubdetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            fLPLateralCategorias.Enabled = true;
            decimal test; celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;

            if (accion!= "Nuevo")
            {
                if (decimal.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[7].Value?.ToString(), out test))
                {
                    if (dgvDetallesSubdetalle.CurrentCell.ColumnIndex == 7)
                    {
                        if (accion == "Venta" || restando)
                        {
                            if (Convert.ToDecimal(dtDetallesSubdetalle.Rows[e.RowIndex]["0"].ToString()) > Convert.ToDecimal(dtDetallesSubdetalle.Rows[e.RowIndex]["Stock"].ToString()))
                            {
                                MessageBox.Show($"No existe suficiente stock", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dtDetallesSubdetalle.Rows[e.RowIndex]["0"] = dtDetallesSubdetalle.Rows[e.RowIndex]["Stock"];
                                calcularRestante();
                                return;
                            }
                        }
                    }
                }
            }


            if (e.ColumnIndex == 3 && accion == "Nuevo")
            {
                switch (tipoDato)
                {
                    case "0":
                        DateTime DTparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!DateTime.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[3].Value.ToString(), out DTparser))
                        {
                            dtDetallesSubdetalle.Rows[e.RowIndex]["Valor"] = "";
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

            if (e.ColumnIndex == 2 && accion == "Inventario")
            {
                switch (tipoDato)
                {
                    case "0":
                        DateTime DTparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!DateTime.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[2].Value.ToString(), out DTparser))
                        {
                            
                            dtDetallesSubdetalle.Rows[e.RowIndex]["Valor"] = DateTime.Now.ToString("");
                            return;
                        }
                        break;
                    case "1":
                        decimal DCparser;
                        celdaCellClick = dgvDetallesSubdetalle.CurrentCell.RowIndex;
                        if (!Decimal.TryParse(dgvDetallesSubdetalle.Rows[celdaCellClick].Cells[2].Value.ToString(), out DCparser))
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
                case "Inventario":
                case "Venta":
                    col = "0";
                    break;
                default:
                    break;
            }
            if (dtDetallesSubdetalle.Rows[e.RowIndex][col].ToString().Contains('-'))
            {
                dtDetallesSubdetalle.Rows[e.RowIndex][col] = (decimal.Parse(dtDetallesSubdetalle.Rows[e.RowIndex][col].ToString()) * -1).ToString();
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
            if (e.ColumnIndex == 3 && tipoDato=="0" && accion== "Nuevo")
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

                // Generamos el evento de cierre del control fecha
                dateTimePicker1.LostFocus += new EventHandler(dateTimePicker1_loseFocus);
            }
            if (e.ColumnIndex == 1 && accion=="Nuevo")
            {
                if (dtDetallesSubdetalle.Rows.Count>0)
                {
                    if (!string.IsNullOrEmpty(dtDetallesSubdetalle.Rows[e.RowIndex]["ID"].ToString()))
                    {
                        idsADesHabilitar.Add(dtDetallesSubdetalle.Rows[e.RowIndex]["ID"].ToString());
                    }
                    dtDetallesSubdetalle.Rows.RemoveAt(e.RowIndex);
                    calcularRestante();
                }
            }

            if (e.ColumnIndex == 2 && tipoDato == "0" && accion == "Inventario")
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

                // Generamos el evento de cierre del control fecha
                dateTimePicker1.LostFocus += new EventHandler(dateTimePicker1_loseFocus);
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

        void dateTimePicker1_loseFocus(object sender, EventArgs e)
        {
            //Volvemos a colocar en invisible el control
            if (string.IsNullOrEmpty(dgvDetallesSubdetalle.CurrentCell.Value.ToString()))
            {
                dgvDetallesSubdetalle.CurrentCell.Value = DateTime.Today.ToString("yyyy-MM-dd");
            }
            dateTimePicker1.Visible = false;
        }

        private void pboxEditar_Click(object sender, EventArgs e)
        {
            using (DataTable dtBuscarIDSubdetalle = cn.CargarDatos($"SELECT ID FROM subdetallesdeproducto WHERE IDUsuario = {FormPrincipal.userID} AND Activo = 1 AND IDProducto = {idProducto} AND Categoria = '{cat}'"))
            {
                categoriaSubdetalle editarCategoria = new categoriaSubdetalle("Editar", dtBuscarIDSubdetalle.Rows[0]["ID"].ToString());
                editarCategoria.FormClosed += delegate
                {
                    if (editarCategoria.cambio)
                    {
                        dgvDetallesSubdetalle.Visible = false;
                        fLPLateralCategorias.Controls.Clear();
                        flowLayoutPanel1.Visible = false;
                        groupBox3.Visible = false;
                        groupBox4.Visible = false;
                        cargarCategorias();
                    }
                };
                editarCategoria.ShowDialog();
            }
        }

        private void dgvDetallesSubdetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            fLPLateralCategorias.Enabled = false;
        }

        private void dgvDetallesSubdetalle_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            if (!validarunique(dtDetallesSubdetalle.Rows[e.RowIndex]["Valor"].ToString()))
            {
                MessageBox.Show($"No puedes utilizar el mismo valor para dos subdetalles", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtDetallesSubdetalle.Rows[e.RowIndex]["Valor"] = "";
                return;
            }
        }
    }
}

