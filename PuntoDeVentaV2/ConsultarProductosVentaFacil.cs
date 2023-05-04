using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class ConsultarProductosVentaFacil : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        DataTable oachango = new DataTable();
        string busqueda = string.Empty;
        string filtro = string.Empty;
        public static bool AcepOCanc = false;
        public DataTable ayylmao = new DataTable();
        List<CategoryCellData> cellDataList = new List<CategoryCellData>();

        public ConsultarProductosVentaFacil()
        {
            InitializeComponent();
        }

        private void ConsultarProductosVentaFacil_Load(object sender, EventArgs e)
        {
            oachango.Columns.Add("Name1", typeof(string));
            oachango.Columns.Add("Col1", typeof(byte[]));
            oachango.Columns.Add("IDC1", typeof(string));
            oachango.Columns.Add("Name2", typeof(string));
            oachango.Columns.Add("Col2", typeof(byte[]));
            oachango.Columns.Add("IDC2", typeof(string));
            oachango.Columns.Add("Name3", typeof(string));
            oachango.Columns.Add("Col3", typeof(byte[]));
            oachango.Columns.Add("IDC3", typeof(string));
            oachango.Columns.Add("Name4", typeof(string));
            oachango.Columns.Add("Col4", typeof(byte[]));
            oachango.Columns.Add("IDC4", typeof(string));
            dgvFast.RowTemplate.Height = 100;
            dgvFast.DataSource = oachango;

            CBTipo.SelectedIndex = 0;
            this.Focus();
            using (DataTable lmao = cn.CargarDatos($"SELECT COUNT(ID) FROM detallegeneral WHERE IDUsuario = {FormPrincipal.userID} AND ChckName = 'venta_facil' AND Mostrar = 1"))
            {
                decimal ayy;
                ayy = decimal.Parse(lmao.Rows[0][0].ToString());
                dgvOpciones.RowTemplate.Height = 100;
                DataTable mosaicos = new DataTable();
                mosaicos.Columns.Add("Cat1", typeof(string));
                mosaicos.Columns.Add("Cat1Img", typeof(Image));
                mosaicos.Columns.Add("Cat2", typeof(string));
                mosaicos.Columns.Add("Cat2Img", typeof(Image));
                mosaicos.Columns.Add("Cat3", typeof(string));
                mosaicos.Columns.Add("Cat3Img", typeof(Image));
                mosaicos.Columns.Add("Cat4", typeof(string));
                mosaicos.Columns.Add("Cat4Img", typeof(Image));
                if (!ayy.Equals(0))
                {
                    DataTable cols = cn.CargarDatos($"SELECT Descripcion,ID from detallegeneral WHERE ChckName = 'venta_facil' AND IDUsuario={FormPrincipal.userID} AND Mostrar = 1");

                    int l = 0;

                    while (l < cols.Rows.Count)
                    {
                        DataRow dr = mosaicos.NewRow();

                        dr["Cat1Img"] = Image.FromFile(getFile("naiden.png"));
                        dr["Cat2Img"] = Image.FromFile(getFile("naiden.png"));
                        dr["Cat3Img"] = Image.FromFile(getFile("naiden.png"));
                        dr["Cat4Img"] = Image.FromFile(getFile("naiden.png"));

                        dr["Cat1"] = cols.Rows[l][0].ToString();
                        dr["Cat1Img"] = cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}"));
                        if (cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}")) == null)
                        {
                            dr["Cat1Img"] = Image.FromFile(getFile("no-image.png"));
                        }
                        l++;
                        if (l < cols.Rows.Count)
                        {
                            dr["Cat2"] = cols.Rows[l][0].ToString();
                            dr["Cat2Img"] = cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}"));

                            if (cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}")) == null)
                            {
                                dr["Cat2Img"] = Image.FromFile(getFile("no-image.png"));
                            }
                            l++;
                            if (l < cols.Rows.Count)
                            {
                                dr["Cat3"] = cols.Rows[l][0].ToString();
                                dr["Cat3Img"] = cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}"));
                                if (cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}")) == null)
                                {
                                    dr["Cat3Img"] = Image.FromFile(getFile("no-image.png"));
                                }
                                l++;
                                if (l < cols.Rows.Count)
                                {
                                    dr["Cat4"] = cols.Rows[l][0].ToString();
                                    dr["Cat4Img"] = cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}"));
                                    if (cn.readImage(($"SELECT ImgNew from detallegeneral WHERE id = {cols.Rows[l][1].ToString()}")) == null)
                                    {
                                        dr["Cat4Img"] = Image.FromFile(getFile("no-image.png"));
                                    }
                                    l++;
                                }
                            }
                        }

                        mosaicos.Rows.Add(dr);
                    }

                    dgvOpciones.DataSource = mosaicos;
                }

            }
        }

        private void cargarDatos(string filtro)
        {
            oachango.Clear();
            DataTable monosas4 = cn.CargarDatos($"SELECT Productos.ID AS ID, ProdImage, Nombre FROM Productos INNER JOIN detallesproductogenerales ON detallesproductogenerales.IDProducto = Productos.ID INNER JOIN detallegeneral ON (detallesproductogenerales.IDDetalleGral=detallegeneral.id AND detallegeneral.Descripcion='{filtro}') WHERE Productos.IDUsuario = {FormPrincipal.userID} AND STATUS = 1");
            int rows = monosas4.Rows.Count;
            int i = 0;
            while (i < rows)
            {
                DataRow dr = oachango.NewRow();
                Image img1 = Image.FromFile(getFile("naiden.png"));
                if (cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}")) != null)
                {
                    img1 = cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}"));
                }
                else
                {
                    img1 = Image.FromFile(getFile("no-image.png"));
                }

                dr["Name1"] = monosas4.Rows[i][2].ToString();
                dr["Col1"] = imageToByteArray(img1);
                dr["IDC1"] = monosas4.Rows[i][0].ToString();
                i++;
                if (i < rows)
                {
                    Image img2 = Image.FromFile(getFile("naiden.png"));
                    if (cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}")) != null)
                    {
                        img2 = cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}"));
                    }
                    else
                    {
                        img2 = Image.FromFile(getFile("no-image.png"));
                    }

                    dr["Name2"] = monosas4.Rows[i][2].ToString();
                    dr["Col2"] = imageToByteArray(img2);
                    dr["IDC2"] = monosas4.Rows[i][0].ToString();
                    i++;

                    if (i < rows)
                    {
                        Image img3 = Image.FromFile(getFile("naiden.png"));
                        if (cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}")) != null)
                        {
                            img3 = cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}"));
                        }
                        else
                        {
                            img3 = Image.FromFile(getFile("no-image.png"));
                        }

                        dr["Name3"] = monosas4.Rows[i][2].ToString();
                        dr["Col3"] = imageToByteArray(img3);
                        dr["IDC3"] = monosas4.Rows[i][0].ToString();
                        i++;
                        if (i < rows)
                        {
                            Image img4 = Image.FromFile(getFile("naiden.png"));
                            if (cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}")) != null)
                            {
                                img4 = cn.readImage(($"SELECT ImgNew from productos WHERE id = {monosas4.Rows[i][0].ToString()}"));
                            }
                            else
                            {
                                img4 = Image.FromFile(getFile("no-image.png"));
                            }

                            dr["Name4"] = monosas4.Rows[i][2].ToString();
                            dr["Col4"] = imageToByteArray(img4);
                            dr["IDC4"] = monosas4.Rows[i][0].ToString();
                            i++;
                        }
                        else
                        {
                            dr["Col4"] = imageToByteArray(Image.FromFile(getFile("naiden.png")));
                        }

                    }
                    else
                    {
                        dr["Col3"] = imageToByteArray(Image.FromFile(getFile("naiden.png")));
                        dr["Col4"] = imageToByteArray(Image.FromFile(getFile("naiden.png")));
                    }
                }
                else
                {
                    dr["Col2"] = imageToByteArray(Image.FromFile(getFile("naiden.png")));
                    dr["Col3"] = imageToByteArray(Image.FromFile(getFile("naiden.png")));
                    dr["Col4"] = imageToByteArray(Image.FromFile(getFile("naiden.png")));
                }
                oachango.Rows.Add(dr);
            }
        }

        private string getFile(string v)
        {
            string pathString = string.Empty;
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                pathString = $@"\\{servidor}\pudve\Productos\";
            }
            else
            {
                pathString = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
            }

            return pathString + v;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void dgvFast_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                Font font = new Font("Microsoft Sans Serif", 8.0f, FontStyle.Bold);

                // Get the text to be displayed and limit the length if necessary
                string text = dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                if (text.Length > 18) // set a limit of 10 characters
                {
                    text = text.Substring(0, 18);
                }

                // Create a white rectangle behind the text
                RectangleF rect = new RectangleF(e.CellBounds.X + 10, e.CellBounds.Y + 5, e.CellBounds.Width - 20, 15);
                e.Graphics.FillRectangle(Brushes.White, rect);

                // Draw a black border around the rectangle
                Pen pen = new Pen(Brushes.Black);
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);

                // Draw the text on top of the rectangle
                e.Graphics.DrawString(text, font, Brushes.Black, rect, stringFormat);
                e.Handled = true;
            }
        }

        private void CBTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CBTipo.SelectedIndex)
            {
                case 1:
                    filtro = $"AND Tipo = 'P'";
                    break;
                case 2:
                    filtro = $"AND Tipo = 'S'";
                    break;
                case 3:
                    filtro = $"AND Tipo = 'PQ'";
                    break;
                default:
                    filtro = string.Empty;
                    break;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (btnCerrar.Text == "Atras")
            {
                dgvOpciones.Visible = true;
                btnCerrar.Text = "Cancelar";
            }
            else
            {
                dgvLista.Rows.Clear();
                this.Close();
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ayylmao.Columns.Add("ID", typeof(string));
            ayylmao.Columns.Add("Cantidad", typeof(decimal));

            foreach (DataGridViewRow item in dgvLista.Rows)
            {
                ayylmao.Rows.Add(item.Cells["ID"].Value.ToString(), decimal.Parse(item.Cells["Cantidad"].Value.ToString()));
            }
            this.Close();
        }

        private void dgvOpciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvOpciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                cargarDatos(dgvOpciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());
                dgvOpciones.Visible = false;
                btnCerrar.Text = "Atras";
                // Disable all controls on the form
                this.Enabled = false;
                Thread.Sleep(400);

                // Enable all controls on the form
                this.Enabled = true;
            }
        }


        private void dgvFast_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                bool encontrado = dgvLista.Rows.Cast<DataGridViewRow>()
                            .Any(row => row.Cells["Producto"].Value != null && row.Cells["Producto"].Value.ToString() == dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString());
                if (encontrado == false)
                {
                    dgvLista.Rows.Add(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), "1", dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
                }
                else
                {
                    foreach (DataGridViewRow item in dgvLista.Rows)
                    {
                        if (item.Cells["Producto"].Value.ToString().Equals(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
                        {
                            item.Cells["Cantidad"].Value = (Convert.ToDecimal(item.Cells["Cantidad"].Value) + 1).ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void dgvFast_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            decimal cantidadPuesta = 1;
            if (!string.IsNullOrEmpty(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                if (!dgvLista.Rows.Count.Equals(0))
                {
                    foreach (DataGridViewRow item in dgvLista.Rows)
                    {
                        if (item.Cells["Producto"].Value.ToString().Equals(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
                        {
                            if (Convert.ToDecimal(item.Cells["Cantidad"].Value) > 1)
                            {
                                cantidadPuesta = Convert.ToDecimal(item.Cells["Cantidad"].Value) - 1;
                            }
                            else
                            {
                                cantidadPuesta = Convert.ToDecimal(item.Cells["Cantidad"].Value);
                            }

                        }
                    }
                }
                canditadPVF cant = new canditadPVF("Cantidad de productos", "Cantidad", "", cantidadPuesta);
                cant.FormClosed += delegate
                {
                    if (!cant.cantidad.Equals("Cancelar"))
                    {
                        dgvLista.Rows.RemoveAt(dgvLista.Rows.Count - 1);
                        dgvLista.Rows.Add(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), cant.cantidad, dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
                        btnOk.Focus();
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dgvLista.Rows)
                        {
                            if (item.Cells["Producto"].Value.ToString().Equals(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
                            {
                                if (Convert.ToDecimal(item.Cells["Cantidad"].Value) > 1)
                                {
                                    item.Cells["Cantidad"].Value = Convert.ToDecimal(item.Cells["Cantidad"].Value) - 1;
                                }
                            }
                        }
                    }
                };
                cant.ShowDialog();
            }
        }

        private void dgvOpciones_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvOpciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                e.PaintBackground(e.ClipBounds, true);
                e.PaintContent(e.ClipBounds);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                Font font = new Font("Microsoft Sans Serif", 8.0f, FontStyle.Bold);

                // Get the text to be displayed and limit the length if necessary
                string text = dgvOpciones.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString();
                if (text.Length > 18) // set a limit of 10 characters
                {
                    text = text.Substring(0, 10);
                }

                // Create a white rectangle behind the text
                RectangleF rect = new RectangleF(e.CellBounds.X + 10, e.CellBounds.Y + 5, e.CellBounds.Width - 20, 15);
                e.Graphics.FillRectangle(Brushes.White, rect);

                // Draw a black border around the rectangle
                Pen pen = new Pen(Brushes.Black);
                e.Graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);

                // Draw the text on top of the rectangle
                e.Graphics.DrawString(text, font, Brushes.Black, rect, stringFormat);
                e.Handled = true;
            }
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)
                {
                    DataGridViewRow row = this.dgvLista.Rows[e.RowIndex];
                    this.dgvLista.Rows.Remove(row);
                }
            }
        }

        private void ConsultarProductosVentaFacil_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }
        }
    }

    public class CategoryCellData
    {
        public Image Image { get; set; }
        public string CategoryName { get; set; }
    }
}
