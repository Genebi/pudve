using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class importarExcelProductos : Form
    {
        List<string> valores = new List<string>();
        string excelPathfile;

        public importarExcelProductos(List<string> valoresA, string pathfile)
        {
            this.valores = valoresA;
            valores.Add("Omitir");
            this.excelPathfile = pathfile;

            InitializeComponent();
        }

        private void importarExcelProductos_Load(object sender, EventArgs e)
        {
            BindingSource bs1 = new BindingSource();
            bs1.DataSource = valores;

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = valores;

            BindingSource bs3 = new BindingSource();
            bs3.DataSource = valores;

            BindingSource bs4 = new BindingSource();
            bs4.DataSource = valores;

            BindingSource bs5 = new BindingSource();
            bs5.DataSource = valores;

            BindingSource bs6 = new BindingSource();
            bs6.DataSource = valores; 

            BindingSource bs7 = new BindingSource();
            bs7.DataSource = valores;

            BindingSource bs8 = new BindingSource();
            bs8.DataSource = valores;

            BindingSource bs9 = new BindingSource();
            bs9.DataSource = valores;

            CBNombre.DataSource = bs1;
            CBCodigo.DataSource = bs2;
            CBClaveSat.DataSource = bs3;
            CBPrecioCompra.DataSource = bs4;
            CBPrecioVenta.DataSource = bs5;
            CBStock.DataSource = bs6;
            CBStockMax.DataSource = bs7;
            CBStockMin.DataSource = bs8;
            CBUnidadM.DataSource = bs9;


            CBNombre.SelectedIndex=valores.Count-1;
            CBCodigo.SelectedIndex = valores.Count - 1;
            CBClaveSat.SelectedIndex = valores.Count - 1;
            CBPrecioCompra.SelectedIndex = valores.Count - 1;
            CBPrecioVenta.SelectedIndex = valores.Count - 1;
            CBStock.SelectedIndex = valores.Count - 1;
            CBStockMax.SelectedIndex = valores.Count - 1;
            CBStockMin.SelectedIndex = valores.Count - 1;
            CBUnidadM.SelectedIndex = valores.Count - 1;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string avisoFinal = ""; 
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbooks xlWorkbookS = xlApp.Workbooks;

            Excel.Workbook xlWorkbook = xlWorkbookS.Open(excelPathfile);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            if (checarDatosObligatorios())
            {

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;
                int inserts = 0;
                avisoFinal = $"Se ha finalizado la operación sin errores, se insertaron {rowCount-1} articulos";
                MessageBoxTemporal.Show($"Se han detectado {rowCount-1} Articulos.\nTus articulos se estan importando...", "Aviso del Sistema", 5, true);

                Conexion cn = new Conexion();
                Consultas cs = new Consultas();
                List<string> filas = new List<string>();
                List<string> errores = new List<string>();

                //indices
                string clavesat = "", codigo, nombre="", stockmax = "0", stockmin = "0", stock = "0", preciocompra = "0", precioventa = "", unidadmedida = "";
                for (int i = 2; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            filas.Add(xlRange.Cells[i, j].Value2.ToString());
                    }


                    decimal number;

                    nombre = filas[valores.IndexOf(CBNombre.Text)];
                    codigo = filas[valores.IndexOf(CBCodigo.Text)];
                    precioventa = filas[valores.IndexOf(CBPrecioVenta.Text)];

                    if (!Decimal.TryParse(precioventa, out number))
                    {
                        errores.Add($"Fila n{i}: Solo se pueden agregar datos decimales al precio de venta ({precioventa}), se salto la linea");
                        filas.Clear();
                        continue;
                    }

                    if (nombre=="")
                    {
                        errores.Add($"Fila n{i}: El nombre es obligatorio ({nombre}), se salto la linea");
                        filas.Clear();
                        continue;
                    }

                    if (!cn.CargarDatos(cs.validarUniqueCodigoBarras(codigo)).Rows.Count.Equals(0))
                    {
                        errores.Add($"Fila n{i}: Este código ya esta registrado y debe ser único ({codigo}), se salto la linea");
                        filas.Clear();
                        continue;
                    }


                    if (CBStockMin.Text != "Omitir")
                    {
                        stockmin = filas[valores.IndexOf(CBStockMin.Text)];
                        if (!Decimal.TryParse(stockmin, out number))
                        {
                            errores.Add($"Fila n{i}: Solo se pueden agregar datos decimales al stock mínimo ({stockmin}), default automático a 0");
                            stockmin = "0";
                        }
                    }

                    if (CBStockMax.Text != "Omitir")
                    {
                        stockmax = filas[valores.IndexOf(CBStockMax.Text)];
                        if (!Decimal.TryParse(stockmax, out number))
                        {
                           errores.Add($"Fila n{i}: Solo se pueden agregar datos decimales al stock máximo ({stockmax}), default automático a 0");
                            stockmax = "0";
                        }
                    }

                    if (CBStock.Text != "Omitir")
                    {
                        stock = filas[valores.IndexOf(CBStock.Text)];
                        if (!Decimal.TryParse(stock.TrimStart('.'), out number))
                        {
                            errores.Add($"Fila n{i}: Solo se pueden agregar datos decimales al stock ({stock}), default automático a 0");
                            stock = "0";
                        }
                    }

                    if (CBPrecioCompra.Text != "Omitir")
                    {
                        preciocompra = filas[valores.IndexOf(CBPrecioCompra.Text)];
                        if (!Decimal.TryParse(preciocompra, out number))
                        {
                            errores.Add($"Fila n{i}: Solo se pueden agregar datos decimales al precio de compra ({preciocompra}), default automático a 0");
                            preciocompra = "0"; 
                        }
                    }

                    if (CBClaveSat.Text != "Omitir")
                    {

                        clavesat = filas[valores.IndexOf(CBClaveSat.Text)];
                        if (cn.CargarDatos(cs.validarExisteClaveSat(clavesat)).Rows.Count.Equals(0))
                        {
                            errores.Add($"Fila n{i}: La clave no se reconocio ({clavesat}), se ignorá la celda");
                            clavesat = "";
                        }
                    }

                    if (CBUnidadM.Text != "Omitir")
                    {
                        unidadmedida = filas[valores.IndexOf(CBUnidadM.Text)];
                        if (cn.CargarDatos(cs.validarExisteClaveUnidad(unidadmedida)).Rows.Count.Equals(0))
                        {
                            errores.Add($"Fila n{i}: La unidad de medida no se reconocio ({unidadmedida}), se ignorá la celda");
                            unidadmedida = "";
                        }
                    }

                    cn.EjecutarConsulta(cs.GuardarProductoDesdeUnExcel(nombre, stock, precioventa, codigo,clavesat, unidadmedida, stockmax, stockmin, preciocompra));
                    inserts++;
                    filas.Clear();
                }

                if (errores.Count>0)
                {
                    avisoFinal = $"La operación finalizo y se han insertado {inserts} articulos.\nSe registraron las siguientes advertencias:";
                    foreach (var error in errores)
                    {
                        avisoFinal = $"{avisoFinal}\n{error}";
                    }
                }
                

            }
            

            //Extra cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            MessageBox.Show(avisoFinal,"Aviso del sistema");
            this.Close();
        }

        private bool checarDatosObligatorios()
        {

            bool datosCorrectos = false;
            if (CBNombre.Text == "Omitir")
            {
                MessageBox.Show("El nombre del producto es obligatorio!");
                CBNombre.Focus();
                return datosCorrectos = false;
            }

            

            if (CBCodigo.Text == "Omitir")
            {
                MessageBox.Show("El código del producto es obligatorio!");
                CBCodigo.Focus();

                return datosCorrectos = false;
            }
            if (CBPrecioVenta.Text == "Omitir")
            {
                MessageBox.Show("El precio de venta del producto es obligatorio!");
                CBPrecioVenta.Focus();
                return datosCorrectos = false;
            }

            

            return datosCorrectos = true;
        }
    }
}
