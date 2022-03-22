using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ImportarExportarCSV : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public ImportarExportarCSV()
        {
            InitializeComponent();
        }

        private void ImportarExportarCSV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCsv_Click(object sender, EventArgs e)
        {
            using (DataTable dtDatosProductos = cn.CargarDatos(cs.ExportarTodosLosDatosDeProductosACSV()))
            {
                if (!dtDatosProductos.Rows.Count.Equals(0))
                {
                    StringBuilder sb = new StringBuilder();

                    string[] columnNames = dtDatosProductos.Columns.Cast<DataColumn>().
                                                      Select(column => column.ColumnName).
                                                      ToArray();
                    sb.AppendLine(string.Join(";", columnNames));

                    foreach (DataRow row in dtDatosProductos.Rows)
                    {
                        string[] fields = row.ItemArray.Select(field => field.ToString()).
                                                        ToArray();
                        sb.AppendLine(string.Join(";", fields));
                    }
                    var directorio = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\ProductosCSV\";

                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }
                    File.WriteAllText($@"{directorio}Productos.csv", sb.ToString(),Encoding.UTF8);
                    MessageBox.Show($@"Guardado satisfactoriamente en: {directorio}Productos.csv");
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
