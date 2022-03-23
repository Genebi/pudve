using MySql.Data.MySqlClient;
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
        int index=0;

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

        private void btnImportar_Click(object sender, EventArgs e)
        {
            abrirArchivoCSV();
        }

        private void leerArchivoCSV(string directorio)
        {
            using (StreamReader sr = new StreamReader(directorio))
            {
                List<string> productos = new List<string>();
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    productos.Add(line);

                }
                //foreach (var item in sr.ReadLine())
                //{
                //try
                //{

                //}
                //catch (MySqlException ex)
                //{
                //    MessageBox.Show($"Error: {ex.Message}");
                //}

                //List<string> productos = new List<string>();
                //string line = string.Empty;
                //while ((line = sr.ReadLine())!=null)
                //{
                //    productos.Add(line);
                //}
                //}
                actualizacionStock(productos);
            }
        }

        private void actualizacionStock(List<string> productos)
        {
            if (!productos.Count.Equals(0))
            {
                for (int i = 0; i < productos.Count; i++)
                {
                    if (i.Equals(0))
                    {
                        string[] conceptos = productos[i].ToString().Split(';');
                        index = Array.IndexOf(conceptos,"Stock");
                    }
                    else
                    {
                        string[] conceptos = productos[i].ToString().Split(';');
                        MessageBox.Show($"ID: {conceptos[0]}, Nombre: {conceptos[1]} Stock: {conceptos[index]}");
                        cn.EjecutarConsulta(cs.ImportarProductosDeCSV(conceptos[0], conceptos[index]));
                    }
                }
            }
        }

        private void abrirArchivoCSV()
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Abrir Archivo CSV";
            dialog.Filter = "CSV Files (*.csv)|*.csv";

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                fileName = dialog.FileName;
                leerArchivoCSV(fileName);
            }
        }
    }
}
