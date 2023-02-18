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
            CBTipo.SelectedIndex = 0;
            using (DataTable lmao = cn.CargarDatos($"SELECT COUNT(ID) FROM detallegeneral WHERE IDUsuario = {FormPrincipal.userID} AND ChckName = 'Venta_fácil' AND Mostrar = 1"))
            {
                decimal ayy;
                ayy = decimal.Parse(lmao.Rows[0][0].ToString());
                ayy = Math.Floor(ayy / 4);                                   
                
            }
        }

        private void cargarDatos(string busqueda, string filtro)
        {
            decimal ayy = 0;
            decimal sincho = 0;

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
            using (DataTable lmao = cn.CargarDatos($"SELECT COUNT(ID) FROM productos WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                ayy = decimal.Parse(lmao.Rows[0][0].ToString());
                if (ayy % 4 > 0)
                {
                    sincho = ayy % 4;
                }
                ayy = Math.Floor(ayy / 4);
            }

            DataTable monosas1 = cn.CargarDatos($"SELECT ID AS IDC1,ProdImage,Nombre FROM Productos WHERE IDUsuario = {FormPrincipal.userID} {filtro} AND Status = 1 LIMIT {(ayy)}");
            DataTable monosas2 = cn.CargarDatos($"SELECT ID AS IDC2,ProdImage,Nombre FROM Productos WHERE IDUsuario = {FormPrincipal.userID} {filtro} AND Status = 1 LIMIT {ayy},{ayy}");
            DataTable monosas3 = cn.CargarDatos($"SELECT ID AS IDC3,ProdImage,Nombre FROM Productos WHERE IDUsuario = {FormPrincipal.userID} {filtro} AND Status = 1 LIMIT {ayy * 2},{ayy}");
            DataTable monosas4 = cn.CargarDatos($"SELECT ID AS IDC4,ProdImage,Nombre FROM Productos WHERE IDUsuario = {FormPrincipal.userID} {filtro} AND Status = 1 LIMIT {ayy * 3},{ayy}");


            for (int i = 0; i < monosas1.Rows.Count; i++)
            {
                Image img1;
                if (string.IsNullOrEmpty(monosas1.Rows[i][1].ToString()))
                {
                    img1 = Image.FromFile(getFile("no-image.png"));
                }
                else
                {
                    img1 = Image.FromFile(getFile(monosas1.Rows[i][1].ToString()));
                }

                Image img2;
                if (string.IsNullOrEmpty(monosas2.Rows[i][1].ToString()))
                {
                    img2 = Image.FromFile(getFile("no-image.png"));
                }
                else
                {
                    img2 = Image.FromFile(getFile(monosas2.Rows[i][1].ToString()));
                }

                Image img3;
                if (string.IsNullOrEmpty(monosas3.Rows[i][1].ToString()))
                {
                    img3 = Image.FromFile(getFile("no-image.png"));
                }
                else
                {
                    img3 = Image.FromFile(getFile(monosas3.Rows[i][1].ToString()));
                }

                Image img4;
                if (string.IsNullOrEmpty(monosas4.Rows[i][1].ToString()))
                {
                    img4 = Image.FromFile(getFile("no-image.png"));
                }
                else
                {
                    img4 = Image.FromFile(getFile(monosas4.Rows[i][1].ToString()));
                }

                DataRow dr = oachango.NewRow();
                dr["Name1"] = monosas1.Rows[i][2].ToString();
                dr["Col1"] = imageToByteArray(img1);                
                dr["IDC1"] = monosas1.Rows[i][0].ToString();
                dr["Name2"] = monosas2.Rows[i][2].ToString();
                dr["Col2"] = imageToByteArray(img2);                
                dr["IDC2"] = monosas2.Rows[i][0].ToString();
                dr["Name3"] = monosas3.Rows[i][2].ToString();
                dr["Col3"] = imageToByteArray(img3);                
                dr["IDC3"] = monosas3.Rows[i][0].ToString();
                dr["Name4"] = monosas4.Rows[i][2].ToString();
                dr["Col4"] = imageToByteArray(img4);                
                dr["IDC4"] = monosas4.Rows[i][0].ToString();
                oachango.Rows.Add(dr);
            }
            dgvFast.RowTemplate.Height = 100;
            dgvFast.DataSource = oachango;
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
            Font font = new Font("Microsoft Sans Serif", 6.0f, FontStyle.Bold);
                e.Graphics.DrawString(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex-1].Value.ToString(), font, Brushes.Black,
                                                e.CellBounds.X, e.CellBounds.Y);
                e.Handled = true;
            
        }

        private void dgvFast_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            canditadPVF cant = new canditadPVF("Cantidad","Cantidad","");
            cant.FormClosed += delegate
            {
                dgvLista.Rows.Add(dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex-1].Value.ToString(),cant.cantidad,dgvFast.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString());
            };
            cant.ShowDialog();
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
            this.Close();
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
    }
}
