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
        public DataTable dtVentas = new DataTable("Ventas a importar desde la página");

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
            SeleccionDeProductosParaExportarCSV frmFiltradoParaExportar = new SeleccionDeProductosParaExportarCSV();
            
            frmFiltradoParaExportar.FormClosed += delegate {
                if (!frmFiltradoParaExportar.dt.Rows.Count.Equals(0))
                    {
                        StringBuilder sb = new StringBuilder();

                        string[] columnNames = frmFiltradoParaExportar.dt.Columns.Cast<DataColumn>().
                                                          Select(column => column.ColumnName).
                                                          ToArray();
                        sb.AppendLine(string.Join(";", columnNames));

                        foreach (DataRow row in frmFiltradoParaExportar.dt.Rows)
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
                        File.WriteAllText($@"{directorio}Productos.csv", sb.ToString(), Encoding.UTF8);
                        MessageBox.Show($@"Guardado satisfactoriamente en: {directorio}Productos.csv");
                    
                }
            };
            frmFiltradoParaExportar.ShowDialog();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {

                leerArchivoCSVInventario(abrirArchivoCSV());
            }
            catch (Exception)
            {
            }
        }

        private void leerArchivoCSVInventario(string directorio)
        {
            using (StreamReader sr = new StreamReader(directorio))
            {
                List<string> productos = new List<string>();
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    productos.Add(line);

                }
                actualizacionStock(productos);
            }
        }
        private void leerArchivoCSVVenta(string directorio)
        {
            try
            {
                using (StreamReader sr = new StreamReader(directorio))
                {
                    List<string> ventas = new List<string>();
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        ventas.Add(line);

                    }
                    actualizacionVentas(ventas);
                }
            }
            catch (Exception)
            {

            }
            
        }
        private void actualizacionVentas(List<string> ventas)
        {
            DataColumn column1 = new DataColumn("ID");
            DataColumn column2 = new DataColumn("Nombre");
            DataColumn column3 = new DataColumn("Precio");
            DataColumn column4 = new DataColumn("Descuento Tipo");
            DataColumn column5 = new DataColumn("Stock");
            DataColumn column6 = new DataColumn("Tipo PS");
            DataColumn column7 = new DataColumn("Cantidad");
            DataColumn column8 = new DataColumn("7");
            DataColumn column9 = new DataColumn("8");
            DataColumn column10 = new DataColumn("9");
            DataColumn column11 = new DataColumn("10");
            DataColumn column15 = new DataColumn("11");
            DataColumn column12 = new DataColumn("PrecioMayoreo");
            DataColumn column13 = new DataColumn("Impuesto");
            DataColumn column14 = new DataColumn("Descuento");
            dtVentas.Columns.Add(column1);
            dtVentas.Columns.Add(column2);
            dtVentas.Columns.Add(column3);
            dtVentas.Columns.Add(column4);
            dtVentas.Columns.Add(column5);
            dtVentas.Columns.Add(column6);
            dtVentas.Columns.Add(column7);
            dtVentas.Columns.Add(column8);
            dtVentas.Columns.Add(column9);
            dtVentas.Columns.Add(column10);
            dtVentas.Columns.Add(column11);
            dtVentas.Columns.Add(column15);
            dtVentas.Columns.Add(column12);
            dtVentas.Columns.Add(column13);
            dtVentas.Columns.Add(column14);
            if (!ventas.Count.Equals(0))
            {
                 List<int> indices = new List<int>();
                for (int i = 0; i < ventas.Count; i++)
                {
                   
                    if (i.Equals(0))
                    {
                        string[] conceptos = ventas[i].ToString().Split(';');


                        
                        indices.Add(Array.IndexOf(conceptos, "SKU"));
                        indices.Add(Array.IndexOf(conceptos, "SKU")-1); //Cantidad
                        //indices.Add(Array.IndexOf(conceptos, "Stock"));
                        //indices.Add(Array.IndexOf(conceptos, "Precio del producto"));
                        //indices.Add(Array.IndexOf(conceptos, "tipo de descuento?"));
                        //indices.Add(Array.IndexOf(conceptos, "TIPO PS?"));
                        //indices.Add(Array.IndexOf(conceptos, '"\"Cantidad del producto\""'));
                        //indices.Add(Array.IndexOf(conceptos, "Precio del producto"));
                        //indices.Add(Array.IndexOf(conceptos, "Descripción?"));
                        indices.Add(Array.IndexOf(conceptos, "Descuento"));
                        //indices.Add(Array.IndexOf(conceptos, "importe?"));

                    }
                    else
                    {
                        string[] conceptos = ventas[i].ToString().Split(';');
                        using (DataTable dtDatosProductos = cn.CargarDatos(cs.LLamarDatosNoIncluidosEnElArchivoCSVExportableDeVentas(conceptos[indices[0]])))
                        {
                            var stringArr = dtDatosProductos.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();

                            dtVentas.Rows.Add(
                                conceptos[indices[0]],
                                stringArr[0],
                                stringArr[1],
                                0,
                                stringArr[2],
                                "P",
                                conceptos[indices[1]],
                                "",
                                "",
                                "",
                                "",
                                "",
                                0,
                                0,
                                0
                                );}
                    }
                }
            }
            MessageBox.Show("Se leyó correctamente el CSV! Cargando los productos...");
            this.Close();
        }

        private void actualizacionStock(List<string> productos)
        {
            if (!productos.Count.Equals(0))
            {
                int i;
                for (i = 0; i < productos.Count; i++)
                {
                    if (i.Equals(0))
                    {
                        string[] conceptos = productos[i].ToString().Split(';');
                        index = Array.IndexOf(conceptos,"Stock");
                    }
                    else
                    {
                        string[] conceptos = productos[i].ToString().Split(';');
                        cn.EjecutarConsulta(cs.ImportarProductosDeCSV(conceptos[0], conceptos[index]));
                    }

                   
                }
                MessageBox.Show($"Sincronizado el inventario de {i-1} producto/s");
            }
        }

        private string abrirArchivoCSV()
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Abrir Archivo CSV";
            dialog.Filter = "CSV Files (*.csv)|*.csv";

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                fileName = dialog.FileName;
                
            }return fileName;
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            

                leerArchivoCSVVenta(abrirArchivoCSV());
            
            }
        }
    }

