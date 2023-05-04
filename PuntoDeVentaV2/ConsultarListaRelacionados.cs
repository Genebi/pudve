using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataGrid;

namespace PuntoDeVentaV2
{
    public partial class ConsultarListaRelacionados : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public string tipoOperacion = string.Empty;
        string tipo = string.Empty;
        public List<string> listaServCombo = new List<string>();
        public List<string> listaProd = new List<string>();

        public int DatosSourceFinal = 0;
        public int idProdEdit = 0;

        int row = 0, column = 0;
        int idReg = 0;  // ID regitrado en la tabla de ProductosDeServicios
        int idProdTemp = 0; // IDProducto temporal antes del guardado final del ComboServicio
        string Concepto = string.Empty; // Nombre del Producto, Servicio ó Combo 

        private void cargarDatos()
        {
            tipo = tipoOperacion;
        }

        private void verificarTipoYLlenadoDataGridView()
        {
            var primeraLetraMayuscula = string.Empty;
            var restoPalabra = string.Empty;
            var complementoTituloProducto = " relaciónado con Combo(s) / Servicio(s)";
            var complementoTituloComboServicio = " relaciónado con Producto(s)";

            if (tipo.Equals("AGREGAR PRODUCTO"))
            {
                primeraLetraMayuscula = tipo.Remove(0, 8).Remove(1);
                restoPalabra = tipo.Remove(0, 8).Remove(0, 1);
                lblTitulo.Text = primeraLetraMayuscula.ToUpper() + restoPalabra.ToLower() + complementoTituloProducto.ToLower();
                llenarDatosProductos();
            }
            if (tipo.Equals("AGREGAR COMBOS"))
            {
                primeraLetraMayuscula = tipo.Remove(0, 8).Remove(1);
                restoPalabra = tipo.Remove(0, 8).Remove(0, 1).Remove(4);
                lblTitulo.Text = primeraLetraMayuscula.ToUpper() + restoPalabra.ToLower() + complementoTituloComboServicio.ToLower();
                llenarDatosServicioCombo();
            }
            if (tipo.Equals("AGREGAR SERVICIOS"))
            {
                primeraLetraMayuscula = tipo.Remove(0, 8).Remove(1);
                restoPalabra = tipo.Remove(0, 8).Remove(0, 1).Remove(7);
                lblTitulo.Text = primeraLetraMayuscula.ToUpper() + restoPalabra.ToLower() + complementoTituloComboServicio.ToLower();
                llenarDatosServicioCombo();
            }

            if (tipo.Equals("EDITAR PRODUCTO") || tipo.Equals("COPIAR PRODUCTO"))
            {
                primeraLetraMayuscula = tipo.Remove(0, 7).Remove(1);
                restoPalabra = tipo.Remove(0, 7).Remove(0, 1);
                lblTitulo.Text = primeraLetraMayuscula.ToUpper() + restoPalabra.ToLower() + complementoTituloProducto.ToLower();
                llenarDatosProductos();
            }

            if (tipo.Equals("EDITAR COMBOS") || tipo.Equals("COPIAR COMBOS"))
            {
                primeraLetraMayuscula = tipo.Remove(0, 7).Remove(1);
                restoPalabra = tipo.Remove(0, 7).Remove(0, 1).Remove(4);
                lblTitulo.Text = primeraLetraMayuscula.ToUpper() + restoPalabra.ToLower() + complementoTituloComboServicio.ToLower();
                llenarDatosServicioCombo();
            }

            if (tipo.Equals("EDITAR SERVICIOS") || tipo.Equals("COPIAR SERVICIOS"))
            {
                primeraLetraMayuscula = tipo.Remove(0, 7).Remove(1);
                restoPalabra = tipo.Remove(0, 7).Remove(0, 1).Remove(7);
                lblTitulo.Text = primeraLetraMayuscula.ToUpper() + restoPalabra.ToLower() + complementoTituloComboServicio.ToLower();
                llenarDatosServicioCombo();
            }
        }

        private void llenarDatosProductos()
        {
            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
            {
                if (!listaServCombo.Count().Equals(0))
                {
                    DGVProdServCombo.Rows.Clear();
                    foreach (var item in listaServCombo)
                    {
                        var words = item.Split('|');
                        var numberOfRows = DGVProdServCombo.Rows.Add();
                        DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                        string Fecha = words[0].ToString();
                        string IDServicio = words[1].ToString();
                        string IDProducto = words[2].ToString();
                        string NombreProducto = words[3].ToString();
                        string Cantidad = words[4].ToString();
                        var ImageDelete = global::PuntoDeVentaV2.Properties.Resources.window_close;

                        row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                        row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                        using (DataTable dtServComb = cn.CargarDatos(cs.obtenerServicioCombo(Convert.ToInt32(IDServicio))))
                        {
                            if (!dtServComb.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drServComb in dtServComb.Rows)
                                {
                                    row.Cells["ServicioCombo"].Value = drServComb["Nombre"].ToString();
                                    row.Cells["Type"].Value = drServComb["Tipo"].ToString();
                                }
                            }
                            else if (dtServComb.Rows.Count.Equals(0))
                            {
                                using (DataTable dtProducto = cn.CargarDatos(cs.consultaRelacionServicioParaProducto(IDServicio)))
                                {
                                    if (!dtProducto.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow drServComb in dtProducto.Rows)
                                        {
                                            row.Cells["ServicioCombo"].Value = drServComb["Nombre"].ToString();
                                            row.Cells["Type"].Value = drServComb["Tipo"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                        row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                        row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                        row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                        row.Cells["Eliminar"].Value = ImageDelete;
                    }
                }
                if (listaServCombo.Count().Equals(0))
                {
                    DGVProdServCombo.Rows.Clear();
                }
            }
            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            {
                string ID = string.Empty;
                string Fecha = string.Empty;
                string IDServicio = string.Empty;
                string CombServ = string.Empty;
                string IDProducto = string.Empty;
                string NombreProducto = string.Empty;
                string Cantidad = string.Empty;
                string Tipo = string.Empty;
                DataTable rowsDGV = cn.CargarDatos(cs.buscarProdIntoServComb(idProdEdit));
                if (idProdEdit == 0)
                {
                    rowsDGV.Clear();
                }
                using (DataTable dtProdServ = cn.CargarDatos(cs.buscarProdIntoServComb(idProdEdit)))
                {
                    if (idProdEdit == 0)
                    {
                        dtProdServ.Clear();
                    }
                    if (!dtProdServ.Rows.Count.Equals(0))
                    {
                        DGVProdServCombo.Rows.Clear();
                        foreach (DataRow drProdServ in dtProdServ.Rows)
                        {
                            var numberOfRows = DGVProdServCombo.Rows.Add();
                            DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                            ID = drProdServ["ID"].ToString();
                            Fecha = drProdServ["Fecha"].ToString();
                            IDServicio = drProdServ["NoServicio"].ToString();
                            CombServ = drProdServ["ServicioCombo"].ToString();
                            IDProducto = drProdServ["NoProducto"].ToString();
                            NombreProducto = drProdServ["Producto"].ToString();
                            Cantidad = drProdServ["Cantidad"].ToString();
                            Tipo = drProdServ["Tipo"].ToString();
                            var ImageDelete = global::PuntoDeVentaV2.Properties.Resources.window_close;

                            row.Cells["ID"].Value = ID;
                            row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                            row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                            row.Cells["ServicioCombo"].Value = CombServ;
                            row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                            row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                            row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                            row.Cells["Type"].Value = Tipo;
                            row.Cells["Eliminar"].Value = ImageDelete;
                        }
                    }
                }
                if (!listaServCombo.Count().Equals(0))
                {
                    foreach (var item in listaServCombo)
                    {
                        var words = item.Split('|');
                        var numberOfRows = DGVProdServCombo.Rows.Add();
                        DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                        Fecha = words[0].ToString();
                        IDServicio = words[1].ToString();
                        CombServ = words[2].ToString();
                        IDProducto = words[3].ToString();
                        NombreProducto = words[4].ToString();
                        if (!words[5].ToString().Equals("0"))
                        {
                            Cantidad = words[5].ToString();
                        }
                        var ImageDelete = global::PuntoDeVentaV2.Properties.Resources.window_close;

                        row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                        row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                        row.Cells["ServicioCombo"].Value = CombServ;
                        using (DataTable dtServComb = cn.CargarDatos(cs.nombreTipoDelProducto(Convert.ToInt32(IDServicio))))
                        {
                            if (!dtServComb.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drServComb in dtServComb.Rows)
                                {
                                    row.Cells["Type"].Value = drServComb["Tipo"].ToString();
                                }
                            }
                            else
                            {
                                var dt = cn.CargarDatos($"SELECT IF ( Tipo = 'PQ', 'COMBO', 'SEVICIO') AS 'Tipo' FROM productos WHERE ID ={IDServicio} AND IDUsuario = {FormPrincipal.userID}");
                                row.Cells["Type"].Value = dt.Rows[0][0];
                            }
                        }
                        row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                        row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                        row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                        row.Cells["Eliminar"].Value = ImageDelete;
                    }
                }
                else if (listaServCombo.Count().Equals(0) && DGVProdServCombo.Rows.Count.Equals(0))
                {
                    DGVProdServCombo.Rows.Clear();
                }
                else if(rowsDGV.Rows.Count.Equals(0))
                {
                    this.Close();
                }

            }

            DGVProdServCombo.Columns[0].Visible = false;
            DGVProdServCombo.Columns[1].Visible = false;
            DGVProdServCombo.Columns[2].Visible = false;
            DGVProdServCombo.Columns[4].Visible = false;
            DGVProdServCombo.Columns[5].Visible = false;
            DGVProdServCombo.Columns[6].Visible = false;

            notSortableDataGridView();
        }

        private void llenarDatosServicioCombo()
        {
            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
            {
                //listaProd = addEditProd.ProductosDeServicios;
                if (!listaProd.Count.Equals(0))
                {
                    DGVProdServCombo.Rows.Clear();
                    foreach (var item in listaProd)
                    {
                        var words = item.Split('|');
                        var numberOfRows = DGVProdServCombo.Rows.Add();
                        DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                        string Fecha = words[0].ToString();
                        string IDServicio = words[1].ToString();
                        string IDProducto = words[2].ToString();
                        string NombreProducto = words[3].ToString();
                        string Cantidad = words[4].ToString();
                        var ImageDelete = global::PuntoDeVentaV2.Properties.Resources.window_close;

                        row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                        row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                        row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                        row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                        row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                        row.Cells["Eliminar"].Value = ImageDelete;
                    }
                }
                else if (listaProd.Count.Equals(0))
                {
                    DGVProdServCombo.Rows.Clear();
                }
            }
            else if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            {
                using (DataTable dtProdServ = cn.CargarDatos(cs.buscarEditProductosDeServicios(idProdEdit)))
                {
                    if (!dtProdServ.Rows.Equals(0))
                    {
                        DGVProdServCombo.Rows.Clear();
                        foreach (DataRow drProdServ in dtProdServ.Rows)
                        {
                            if (!drProdServ["NoProducto"].Equals(0))
                            {
                                var numberOfRows = DGVProdServCombo.Rows.Add();
                                DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                                string ID = drProdServ["ID"].ToString();
                                string Fecha = drProdServ["Fecha"].ToString();
                                string IDServicio = drProdServ["NoServicio"].ToString();
                                string CombServ = drProdServ["ServicioCombo"].ToString();
                                string IDProducto = drProdServ["NoProducto"].ToString();
                                string NombreProducto = drProdServ["Producto"].ToString();
                                string Cantidad = drProdServ["Cantidad"].ToString();
                                string Tipo = drProdServ["Tipo"].ToString();
                                var ImageDelete = global::PuntoDeVentaV2.Properties.Resources.window_close;

                                row.Cells["ID"].Value = ID;
                                row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                                row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                                row.Cells["ServicioCombo"].Value = CombServ;            // Columna ServicioCombo
                                row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                                row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                                row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                                row.Cells["Type"].Value = Tipo;
                                row.Cells["Eliminar"].Value = ImageDelete;
                            }
                        }
                        if (!listaProd.Count.Equals(0))
                        {
                            foreach (var item in listaProd)
                            {
                                var words = item.Split('|');
                                var numberOfRows = DGVProdServCombo.Rows.Add();
                                DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                                string Fecha = words[0].ToString();
                                string IDServicio = words[1].ToString();
                                string IDProducto = words[2].ToString();
                                string NombreProducto = words[3].ToString();
                                string Cantidad = words[4].ToString();
                                var ImageDelete = global::PuntoDeVentaV2.Properties.Resources.window_close;

                                row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                                row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                                row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                                row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                                row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                                row.Cells["Eliminar"].Value = ImageDelete;
                            }
                        }
                    }
                    else if (dtProdServ.Rows.Equals(0))
                    {
                        DGVProdServCombo.Rows.Clear();
                    }
                }
            }

            DGVProdServCombo.Columns[0].Visible = false;
            DGVProdServCombo.Columns[1].Visible = false;
            DGVProdServCombo.Columns[2].Visible = false;
            DGVProdServCombo.Columns[3].Visible = false;
            DGVProdServCombo.Columns[4].Visible = false;
            DGVProdServCombo.Columns[7].Visible = false;

            notSortableDataGridView();
        }

        private void notSortableDataGridView()
        {

            foreach (DataGridViewColumn column in DGVProdServCombo.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public ConsultarListaRelacionados()
        {
            InitializeComponent();
        }

        private void ConsultarListaRelacionados_Load(object sender, EventArgs e)
        {
            cargarDatos();
            verificarTipoYLlenadoDataGridView();
        }

        private void DGVProdServCombo_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(6) || e.ColumnIndex.Equals(8))
            {
                DGVProdServCombo.Cursor = Cursors.Hand;
            }
            else
            {
                DGVProdServCombo.Cursor = Cursors.Default;
            }
        }

        private void DGVProdServCombo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            column = e.ColumnIndex;

            //if (e.ColumnIndex.Equals(6))
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        if (DatosSourceFinal.Equals(1))
            //        {
            //            idProdTemp = Convert.ToInt32(DGVProdServCombo.Rows[row].Cells[4].Value.ToString());
            //            Concepto = DGVProdServCombo.Rows[row].Cells[5].Value.ToString();
            //        }
            //        if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            //        {
            //            idReg = Convert.ToInt32(DGVProdServCombo.Rows[row].Cells[0].Value.ToString());
            //        }
            //    }
            //}

            if (e.ColumnIndex.Equals(8))
            {
                if (e.RowIndex >= 0)
                {
                    if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
                    {
                        if (!listaServCombo.Count().Equals(0))
                        {
                            for (int i = 0; i < listaServCombo.Count(); i++)
                            {
                                if (listaServCombo[i].Contains(DGVProdServCombo.Rows[row].Cells[1].Value.ToString()) &&
                                    listaServCombo[i].Contains(DGVProdServCombo.Rows[row].Cells[2].Value.ToString()) &&
                                    listaServCombo[i].Contains(DGVProdServCombo.Rows[row].Cells[4].Value.ToString()) &&
                                    listaServCombo[i].Contains(DGVProdServCombo.Rows[row].Cells[5].Value.ToString()) &&
                                    listaServCombo[i].Contains(DGVProdServCombo.Rows[row].Cells[6].Value.ToString()))
                                {
                                    listaServCombo.RemoveAll(x => x == listaServCombo[i]);
                                }
                            }
                        }
                        if (!listaProd.Count().Equals(0))
                        {
                            for (int i = 0; i < listaProd.Count(); i++)
                            {
                                if (listaProd[i].Contains(DGVProdServCombo.Rows[row].Cells[1].Value.ToString()) &&
                                    listaProd[i].Contains(DGVProdServCombo.Rows[row].Cells[2].Value.ToString()) &&
                                    listaProd[i].Contains(DGVProdServCombo.Rows[row].Cells[4].Value.ToString()) &&
                                    listaProd[i].Contains(DGVProdServCombo.Rows[row].Cells[5].Value.ToString()) &&
                                    listaProd[i].Contains(DGVProdServCombo.Rows[row].Cells[6].Value.ToString()))
                                {
                                    listaProd.RemoveAll(x => x == listaProd[i]);
                                }
                            }
                        }
                    }
                    if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
                    {
                        var contenido = DGVProdServCombo.Rows[row].Cells[0].FormattedValue.ToString();
                        var fechaDGV = DGVProdServCombo.Rows[row].Cells[1].FormattedValue.ToString();
                        var idServicioDGV = DGVProdServCombo.Rows[row].Cells[2].FormattedValue.ToString();
                        var conceptoServicioDGV = DGVProdServCombo.Rows[row].Cells[3].FormattedValue.ToString();
                        var idProductoDGV = DGVProdServCombo.Rows[row].Cells[4].FormattedValue.ToString();
                        var conceptoProductoDGV = DGVProdServCombo.Rows[row].Cells[5].FormattedValue.ToString();
                        var cantidadDGV = DGVProdServCombo.Rows[row].Cells[6].FormattedValue.ToString();
                        var tipoDGV = DGVProdServCombo.Rows[row].Cells[7].FormattedValue.ToString();

                        if (contenido.Equals(string.Empty))
                        {
                            if (!listaServCombo.Count().Equals(0))
                            {
                                for (int i = 0; i < DGVProdServCombo.Rows.Count; i++)
                                {
                                    for (int z = 0; z < listaServCombo.Count(); z++)
                                    {
                                        var existeFecha = listaServCombo[z].Contains(fechaDGV);
                                        var existeIdServicio = listaServCombo[z].Contains(idServicioDGV);
                                        var existeIdProducto = listaServCombo[z].Contains(idProductoDGV);
                                        var existeConceptoProducto = listaServCombo[z].Contains(conceptoProductoDGV);
                                        var existeCantidad = listaServCombo[z].Contains(cantidadDGV);

                                        if (existeFecha.Equals(true) && 
                                            existeIdServicio.Equals(true) && 
                                            existeIdProducto.Equals(true) && 
                                            existeConceptoProducto.Equals(true) && 
                                            existeCantidad.Equals(true))
                                        {
                                            listaServCombo.RemoveAll(x => x == listaServCombo[z]);
                                        }
                                    }
                                }
                            }
                            if (!listaProd.Count().Equals(0))
                            {
                                for (int i = 0; i < DGVProdServCombo.Rows.Count; i++)
                                {
                                    for (int z = 0; z < listaProd.Count(); z++)
                                    {
                                        var existeFecha = listaProd[z].Contains(fechaDGV);
                                        var existeIdServicio = listaProd[z].Contains(idServicioDGV);
                                        var existeIdProducto = listaProd[z].Contains(idProductoDGV);
                                        var existeConceptoProducto = listaProd[z].Contains(conceptoProductoDGV);
                                        var existeCantidad = listaProd[z].Contains(cantidadDGV);

                                        if (existeFecha.Equals(true) &&
                                            existeIdServicio.Equals(true) &&
                                            existeIdProducto.Equals(true) &&
                                            existeConceptoProducto.Equals(true) &&
                                            existeCantidad.Equals(true))
                                            {
                                                listaProd.RemoveAll(x => x == listaProd[z]);
                                            }
                                    }
                                }
                            }
                        }
                        else if (!contenido.Equals(string.Empty))
                        {
                            bool exitoConvertirEntero = Int32.TryParse(contenido, out idReg);

                            if (exitoConvertirEntero)
                            {
                                DialogResult dialogResult = MessageBox.Show("Quitara la relación existente\nesta usted totalmente seguro de realizar esta acción", "Aviso del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                                if (dialogResult.Equals(DialogResult.Yes))
                                {
                                    try
                                    {
                                        var resultado = cn.EjecutarConsulta(cs.borrarRelacionProdComboServicio(idReg));
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("algo paso al tratar de quitar la relación", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                    DGVProdServCombo.Rows.Clear();
                    cargarDatos();
                    verificarTipoYLlenadoDataGridView();
                }
            }
        }


        private void DGVProdServCombo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hitTestInfo = DGVProdServCombo.HitTest(e.X, e.Y);
                int indexColumn = hitTestInfo.ColumnIndex;
                int indexRow = hitTestInfo.RowIndex;

                if ((hitTestInfo.Type == DataGridViewHitTestType.Cell) && indexColumn.Equals(6))
                {
                    DGVProdServCombo.BeginEdit(true);
                    if (indexColumn.Equals(6))
                    {
                        decimal cantidadAnterior = Convert.ToDecimal(DGVProdServCombo.Rows[indexRow].Cells[indexColumn].Value.ToString());
                    }
                }
                else
                {
                    DGVProdServCombo.EndEdit();
                }
            }
        }

        private void DGVProdServCombo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column6_KeyPress);
            if (DGVProdServCombo.CurrentCell.ColumnIndex == 6)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column6_KeyPress);
                }
            }
        }

        private void DGVProdServCombo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //float cantidad = 0;

            //string[] words = { };

            //var valor = DGVProdServCombo.Rows[row].Cells[column].Value.ToString();

            //if (!valor.Equals(string.Empty))
            //{
            //    words = valor.Split('.');
            //    if (words.Count() > 1)
            //    {
            //        if (words[1].Equals(string.Empty))
            //        {
            //            words[1] = "00";
            //        }
            //        else if (!words[1].Equals(string.Empty))
            //        {
            //            if (words[1].Length.Equals(1))
            //            {
            //                var contenido = words[1];
            //                words[1] = $"{contenido}0";
            //            }
            //        }
            //        DGVProdServCombo.Rows[row].Cells[column].Value = $"{words[0]}.{words[1]}";
            //        cantidad = (float)Convert.ToDecimal(DGVProdServCombo.Rows[row].Cells[column].Value.ToString());
            //    }
            //    if (words.Count() == 1)
            //    {
            //        DGVProdServCombo.Rows[row].Cells[column].Value = $"{words[0]}.00";
            //        cantidad = (float)Convert.ToDecimal(DGVProdServCombo.Rows[row].Cells[column].Value.ToString());
            //    }
            //}
            //if (!valor.Equals(string.Empty))
            //{
            //    words = valor.Split('.');
            //    if (words.Count() > 1)
            //    {
            //        if (words[1].Equals(string.Empty))
            //        {
            //            words[1] = "00";
            //        }
            //        else if (!words[1].Equals(string.Empty))
            //        {
            //            if (words[1].Length.Equals(1))
            //            {
            //                var contenido = words[1];
            //                words[1] = $"{contenido}0";
            //            }
            //        }
            //        DGVProdServCombo.Rows[row].Cells[column].Value = $"{words[0]}.{words[1]}";
            //        cantidad = (float)Convert.ToDecimal(DGVProdServCombo.Rows[row].Cells[column].Value.ToString());
            //    }
            //    if (words.Count() == 1)
            //    {
            //        DGVProdServCombo.Rows[row].Cells[column].Value = $"{words[0]}.00";
            //        bool esDecimal = false;
            //        esDecimal = float.TryParse(DGVProdServCombo.Rows[row].Cells[column].Value.ToString(), out cantidad);
            //        if (esDecimal)
            //        {
            //            cantidad = (float)Convert.ToDecimal(DGVProdServCombo.Rows[row].Cells[column].Value.ToString());
            //        }
                   
            //    }
            //}

            //if (DatosSourceFinal.Equals(1))
            //{
            //    using (AgregarEditarProducto addEditProd = new AgregarEditarProducto())
            //    {
            //        if (!listaProd.Count().Equals(0))
            //        {
            //            if (!listaProd[0].ToString().Equals(string.Empty))
            //            {
            //                foreach (var item in listaProd)
            //                {
            //                    words = item.Split('|');
            //                    if (words[2].Equals(Convert.ToString(idProdTemp)) && words[3].Equals(Concepto))
            //                    {
            //                        words[4] = cantidad.ToString();
            //                        break;
            //                    }
            //                }
            //                for (int i = 0; i < listaProd.Count; i++)
            //                {
            //                    if (listaProd[i].Contains(Concepto) && listaProd[i].Contains(idProdTemp.ToString()))
            //                    {
            //                        listaProd[i] = $"{words[0]}|{words[1]}|{words[2]}|{words[3]}|{words[4]}";
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //if (DatosSourceFinal.Equals(2) || DatosSourceFinal.Equals(4))
            //{
            //    try
            //    {
            //        var resultado = cn.EjecutarConsulta(cs.actualizarRelacionProdComboServicio(idReg, cantidad));
            //        verificarTipoYLlenadoDataGridView();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Algo paso al actualizar la relación.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
        }

        private void Column6_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtContenido = sender as TextBox;

            // allow 0-9, BackSpace, and Decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if (txtContenido.Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
