﻿using MySql.Data.MySqlClient;
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
        DataTable dtVentas = new DataTable("Ventas a importar desde la página");

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
        private DataTable actualizacionVentas(List<string> ventas)
        {
            DataColumn column1 = new DataColumn("IDProducto");
            DataColumn column2 = new DataColumn("Stock");
            DataColumn column3 = new DataColumn("PrecioOriginal");
            DataColumn column4 = new DataColumn("DescuentoTipo");
            DataColumn column5 = new DataColumn("TipoPs");
            DataColumn column6 = new DataColumn("Cantidad");
            DataColumn column7 = new DataColumn("Precio");
            DataColumn column8 = new DataColumn("Descripción");
            DataColumn column9 = new DataColumn("Descuento");
            DataColumn column10 = new DataColumn("Importe");
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
            if (!ventas.Count.Equals(0))
            {
                
                for (int i = 0; i < ventas.Count; i++)
                {
                    List<int> indices = new List<int>();
                    if (i.Equals(0))
                    {
                        string[] conceptos = ventas[i].ToString().Split(';');
                        
                        indices.Add(Array.IndexOf(conceptos, "SKU"));
                        indices.Add(Array.IndexOf(conceptos, "Stock"));
                        indices.Add(Array.IndexOf(conceptos, "Precio del producto"));
                        indices.Add(Array.IndexOf(conceptos, "tipo de descuento?"));
                        indices.Add(Array.IndexOf(conceptos, "TIPO PS?"));
                        indices.Add(Array.IndexOf(conceptos, "Cantidad del producto"));
                        indices.Add(Array.IndexOf(conceptos, "Precio del producto"));
                        indices.Add(Array.IndexOf(conceptos, "Descripción?"));
                        indices.Add(Array.IndexOf(conceptos, "Descuento"));
                        indices.Add(Array.IndexOf(conceptos, "importe?"));
                    }
                    else
                    {
                        string[] conceptos = ventas[i].ToString().Split(';');
                        for (int n = 0; n < 11; n++)
                        {
                            dtVentas.Rows.Add(conceptos[indices[n]]);
                        }
                        
                            
                    }
                }
            }
            return dtVentas;
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
                        cn.EjecutarConsulta(cs.ImportarProductosDeCSV(conceptos[0], conceptos[index]));
                    }
                }
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
            try
            {

                leerArchivoCSVVenta(abrirArchivoCSV());
            }
            catch (Exception)
            {

            }
        }
    }
}
