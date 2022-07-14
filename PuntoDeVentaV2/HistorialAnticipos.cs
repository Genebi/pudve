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
    public partial class HistorialAnticipos : Form
    {
        public DataTable datosHistoria { get; set; }
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        

        public HistorialAnticipos()
        {
            InitializeComponent();
        }

        private void HistorialAnticipos_Load(object sender, EventArgs e)
        {
            int number_of_rows;
            System.Drawing.Image imprimir = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\print.png");
            if (!datosHistoria.Rows.Count.Equals(0))
            {
                //DGVAnticipos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //DGVAnticipos.DataSource = datosHistoria;
                foreach (DataRow item in datosHistoria.Rows)
                {
                    number_of_rows = DGVAnticipos.Rows.Add();
                    DataGridViewRow row = DGVAnticipos.Rows[number_of_rows];

                    row.Cells["idventa"].Value = item["ID Venta"].ToString();
                    row.Cells["empleado"].Value = item["Empleado"].ToString();
                    row.Cells["concepto"].Value = item["Concepto"].ToString();
                    row.Cells["cliente"].Value = item["Cliente"].ToString();
                    row.Cells["comentarios"].Value = item["Comentarios"].ToString();
                    row.Cells["totalrecibido"].Value = item["Total Recibido"].ToString();
                    row.Cells["anticipoaplicado"].Value = item["Anticipo Aplicado"].ToString();
                    row.Cells["saldorestante"].Value = item["Saldo Restante"].ToString();
                    row.Cells["fechaoperacion"].Value = item["Fecha Operacion"].ToString();
                    
                    row.Cells["imagen"].Value = imprimir;

                }
                notSortableDataGridView();
            }
        }

        private void notSortableDataGridView()
        {
            foreach (DataGridViewColumn column in DGVAnticipos.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void DGVAnticipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex.Equals(9))
                {
                    var idVenta = Convert.ToInt32(DGVAnticipos.Rows[e.RowIndex].Cells["idventa"].Value.ToString());
                    var id = cn.CargarDatos($"SELECT IDAnticipo FROM Ventas WHERE ID = '{idVenta}'");
                    var idAnticipo = Convert.ToInt32(id.Rows[0]["IDAnticipo"].ToString());
                    ImpresionTicketAnticipo anticipo = new ImpresionTicketAnticipo();
                    anticipo.idAnticipo = idAnticipo;
                    anticipo.ShowDialog();
                }
            }
        }
    }
}
