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
            using (DataTable lmao = cn.CargarDatos($"SELECT COUNT(ID) FROM detallegeneral WHERE IDUsuario = {FormPrincipal.userID} AND ChckName = 'Venta_fácil' AND Mostrar = 1"))
            {
                decimal ayy;
                ayy = decimal.Parse(lmao.Rows[0][0].ToString());
                dgvOpciones.RowTemplate.Height = 100;
                DataTable mosaicos = new DataTable();
                mosaicos.Columns.Add("Cat1", typeof(string));
                mosaicos.Columns.Add("Cat2", typeof(string));
                mosaicos.Columns.Add("Cat3", typeof(string));
                mosaicos.Columns.Add("Cat4", typeof(string));
                if (!ayy.Equals(0))
                {
                    DataTable cols = cn.CargarDatos($"SELECT Descripcion from detallegeneral WHERE ChckName = 'Venta_fácil' AND IDUsuario={FormPrincipal.userID} AND Mostrar = 1");

                    int l = 0;

                    while (l < cols.Rows.Count)
                    {
                            DataRow dr = mosaicos.NewRow();
                            dr["Cat1"] = cols.Rows[l][0].ToString();
                            l++;
                            if (l<cols.Rows.Count)
                            {
                                dr["Cat2"] = cols.Rows[l][0].ToString();
                                l++;
                                if (l < cols.Rows.Count)
                                {
                                    dr["Cat3"] = cols.Rows[l][0].ToString();
                                    l++;
                                    if (l < cols.Rows.Count)
                                    {
                                        dr["Cat4"] = cols.Rows[l][0].ToString();
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
            while (i <rows)
            {
                DataRow dr = oachango.NewRow();
                Image img1;
                if (string.IsNullOrEmpty(monosas4.Rows[i][1].ToString()))
                {
                    img1 = Image.FromFile(getFile("no-image.png"));
                }
                else
                {
                    img1 = Image.FromFile(getFile(monosas4.Rows[i][1].ToString()));
                }

                dr["Name1"] = monosas4.Rows[i][2].ToString();
                dr["Col1"] = imageToByteArray(img1);
                dr["IDC1"] = monosas4.Rows[i][0].ToString();
                i++;
                if (i<rows)
                {
                    Image img2;
                    if (string.IsNullOrEmpty(monosas4.Rows[i][1].ToString()))
                    {
                        img2 = Image.FromFile(getFile("no-image.png"));
                    }
                    else
                    {
                        img2 = Image.FromFile(getFile(monosas4.Rows[i][1].ToString()));
                    }

                    dr["Name2"] = monosas4.Rows[i][2].ToString();
                    dr["Col2"] = imageToByteArray(img2);
                    dr["IDC2"] = monosas4.Rows[i][0].ToString();
                    i++;

                    if (i < rows)
                    {
                        Image img3;
                        if (string.IsNullOrEmpty(monosas4.Rows[i][1].ToString()))
                        {
                            img3 = Image.FromFile(getFile("no-image.png"));
                        }
                        else
                        {
                            img3 = Image.FromFile(getFile(monosas4.Rows[i][1].ToString()));
                        }

                        dr["Name3"] = monosas4.Rows[i][2].ToString();
                        dr["Col3"] = imageToByteArray(img3);
                        dr["IDC3"] = monosas4.Rows[i][0].ToString();
                        i++;
                        if (i < rows)
                        {
                            Image img4;
                            if (string.IsNullOrEmpty(monosas4.Rows[i][1].ToString()))
                            {
                                img4 = Image.FromFile(getFile("no-image.png"));
                            }
                            else
                            {
                                img4 = Image.FromFile(getFile(monosas4.Rows[i][1].ToString()));
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

            return pathString+v;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void dgvFast_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.PaintBackground(e.ClipBounds, true);
            e.PaintContent(e.ClipBounds);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Font font = new Font("Microsoft Sans Serif", 8.0f, FontStyle.Bold);
            e.Graphics.DrawString(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), font, Brushes.Black,
                                            e.CellBounds.X+e.CellBounds.Width/2, e.CellBounds.Y+10,stringFormat);
            e.Handled = true;
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
                ayylmao.Rows.Add(item.Cells["ID"].Value.ToString(),decimal.Parse(item.Cells["Cantidad"].Value.ToString()));
            }
            this.Close();
        }

        private void dgvOpciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvOpciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
            {
                cargarDatos(dgvOpciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                dgvOpciones.Visible = false;
                btnCerrar.Text = "Atras";
            }
        }


        private void dgvFast_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                dgvLista.Rows.Add(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), "1", dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
            }
        }

        private void dgvFast_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvLista.Rows.RemoveAt(dgvLista.Rows.Count-1);
            if (!string.IsNullOrEmpty(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString()))
            {
                canditadPVF cant = new canditadPVF("Cantidad de productos", "Cantidad", "");
                cant.FormClosed += delegate
                {
                    if (!cant.cantidad.Equals("Cancelar"))
                    {
                        dgvLista.Rows.Add(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString(), cant.cantidad, dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
                        btnOk.Focus();
                    }
                };
                cant.ShowDialog();
            }
        }
    }
}
