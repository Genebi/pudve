using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class RevisarInventario : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        // Variables iniciales
        string fechaInventario = string.Empty;
        string numeroRevision = string.Empty;
        string nombrePC = string.Empty;

        int idProducto = 0;
        int idProductoAux = 0;
        int countListaCodigosBarras = 0;

        // Variable para verproductos anteriormente revisados
        int anterior = 0;
        int NoRevision = 0;

        // Variables para cuando el filtro es diferente a la revision normal
        string tipoFiltro = string.Empty;
        string operadorFiltro = string.Empty;
        string strFiltroDinamico = string.Empty;
        int cantidadFiltro = 0;
        int cantidadRegistros = 0;
        int cantidadRegistrosAux = 0;

        bool validarAnterior = true;
        int convertirId = 0;
        int numFilasExistentes = 0;

        Dictionary<int, string> listaProductos;

        List<int> idDeProductos = new List<int>();


        public RevisarInventario(string[] datos)
        {
            InitializeComponent();

            tipoFiltro = datos[0];
            operadorFiltro = datos[1];
            if (tipoFiltro.Equals("Filtros"))
            {
                strFiltroDinamico = datos[2];
            }
            else
            {
                cantidadFiltro = Convert.ToInt32(datos[2]);
            }
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            var datosInventario = mb.DatosRevisionInventario();

            listaProductos = new Dictionary<int, string>();

            // Si existe un registro en la tabla obtiene los datos de lo contrario hace un insert para
            // que exista la configuracion necesaria
            if (datosInventario.Length > 0)
            {
                cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET FechaInventario = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE IDUsuario = {FormPrincipal.userID}", true);

                datosInventario = mb.DatosRevisionInventario();
                fechaInventario = datosInventario[0];
                numeroRevision = datosInventario[1];
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, FechaInventario, NoRevision) VALUES ('{FormPrincipal.userID}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '1')", true);

                datosInventario = mb.DatosRevisionInventario();
                fechaInventario = datosInventario[0];
                numeroRevision = datosInventario[1];
            }

            lblNoRevision.Text = numeroRevision;

            // Asignamos el numero de revision para que cargue los productos en el reporte al cerrar el form
            Inventario.NumRevActivo = Convert.ToInt32(numeroRevision);
            NoRevision = Convert.ToInt32(numeroRevision);
            // Obtener el nombre de la computadora
            nombrePC = Environment.MachineName;

            // Ejecutar busqueda de productos cuando hay filtro
            if (tipoFiltro != "Normal")
            {
                if (tipoFiltro != "Filtros")
                {
                    var consulta = $"SELECT COUNT(ID) AS Total FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND {tipoFiltro} {operadorFiltro} {cantidadFiltro}";
                    cantidadRegistros = mb.CantidadFiltroInventario(consulta);
                }
                else
                {
                    if (operadorFiltro.Equals("chkProveedor"))
                    {
                        if (strFiltroDinamico.Equals("SIN PROVEEDOR"))
                        {
                            var consulta = cs.CantidadListaProductosSinProveedor(FormPrincipal.userID, 1);
                            cantidadRegistros = mb.CantidadFiltroInventario(consulta);
                        }
                        else
                        {
                            var consulta = cs.CantidadListaProductosProveedor(FormPrincipal.userID, strFiltroDinamico, 1);
                            cantidadRegistros = mb.CantidadFiltroInventario(consulta);
                        }
                    }
                    else
                    {
                        string Seleccionado = "SIN " + operadorFiltro.ToUpper().Remove(0, 3);
                        if (strFiltroDinamico.Equals(Seleccionado))
                        {
                            var consulta = cs.CantidadListarProductosSinConceptoDinamico(FormPrincipal.userID, operadorFiltro.Remove(0, 3), 1);
                            cantidadRegistros = mb.CantidadFiltroInventario(consulta);
                        }
                        else
                        {
                            var consulta = cs.CantidadListarProductosConceptoDinamico(FormPrincipal.userID, strFiltroDinamico, 1);
                            cantidadRegistros = mb.CantidadFiltroInventario(consulta);
                        }
                    }
                }

                //lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";

                buscarCodigoBarras();
            }
        }

        private string AplicarFiltro(int idProducto)
        {
            var consulta = string.Empty;

            if (tipoFiltro != "Normal")
            {
                if (tipoFiltro != "Filtros")
                {
                    consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND {tipoFiltro} {operadorFiltro} {cantidadFiltro} AND ID > {idProducto} ORDER BY ID ASC LIMIT 1";
                }
                else
                {
                    if (operadorFiltro.Equals("chkProveedor"))
                    {
                        if (strFiltroDinamico.Equals("SIN " + operadorFiltro.ToUpper().Remove(0, 3)))
                        {
                            consulta = cs.ListarProductosSinProveedor(FormPrincipal.userID, 1);
                        }
                        else
                        {
                            consulta = cs.ListarProductosProveedor(FormPrincipal.userID, strFiltroDinamico, 1);
                        }
                    }
                    else
                    {
                        if (strFiltroDinamico.Equals("SIN " + operadorFiltro.ToUpper().Remove(0, 3)))
                        {
                            consulta = cs.ListarProductosSinConceptoDinamico(FormPrincipal.userID, operadorFiltro.Remove(0, 3), 1);
                        }
                        else
                        {
                            consulta = cs.ListarProductosConceptoDinamico(FormPrincipal.userID, strFiltroDinamico, 1);
                        }
                    }
                }
            }

            return consulta;
        }

        private void buscarCodigoBarras()
        {
            var busqueda = txtBoxBuscarCodigoBarras.Text;

            if (tipoFiltro != "Normal")
            {
                busqueda = "auxiliar";
            }

            if (busqueda != string.Empty)
            {
                List<string> listaCodigosBarras = new List<string>();

                var aplicar = false;

                var codigo = txtBoxBuscarCodigoBarras.Text;

                if (tipoFiltro != "Normal")
                {
                    if (tipoFiltro.Equals("Filtros"))
                    {
                        if (operadorFiltro.Equals("chkProveedor"))
                        {
                            listaCodigosBarras.Clear();
                            if (strFiltroDinamico.Equals("SIN " + operadorFiltro.ToUpper().Remove(0, 3)))
                            {
                                using (DataTable dtListaProductosSinProveedor = cn.CargarDatos(cs.ListarProductosSinProveedor(FormPrincipal.userID, 1)))
                                {
                                    if (!dtListaProductosSinProveedor.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow drListaProductosSinProveedor in dtListaProductosSinProveedor.Rows)
                                        {
                                            if (!string.IsNullOrWhiteSpace(drListaProductosSinProveedor["CodigoBarras"].ToString()))
                                            {
                                                listaCodigosBarras.Add(drListaProductosSinProveedor["CodigoBarras"].ToString());
                                            }
                                            else
                                            {
                                                listaCodigosBarras.Add(drListaProductosSinProveedor["ClaveInterna"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (DataTable dtListaProductosProveedor = cn.CargarDatos(cs.ListarProductosProveedor(FormPrincipal.userID, strFiltroDinamico, 1)))
                                {
                                    if (!dtListaProductosProveedor.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow drListaProductosProveedor in dtListaProductosProveedor.Rows)
                                        {
                                            if (!string.IsNullOrWhiteSpace(drListaProductosProveedor["CodigoBarras"].ToString()))
                                            {
                                                listaCodigosBarras.Add(drListaProductosProveedor["CodigoBarras"].ToString());
                                            }
                                            else
                                            {
                                                listaCodigosBarras.Add(drListaProductosProveedor["ClaveInterna"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            listaCodigosBarras.Clear();
                            if (strFiltroDinamico.Equals("SIN " + operadorFiltro.ToUpper().Remove(0, 3)))
                            {
                                using (DataTable dtListaProductosSinConceptoDinamico = cn.CargarDatos(cs.ListarProductosSinConceptoDinamico(FormPrincipal.userID, operadorFiltro.Remove(0, 3), 1)))
                                {
                                    if (!dtListaProductosSinConceptoDinamico.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow drListaProductosSinConceptoDinamico in dtListaProductosSinConceptoDinamico.Rows)
                                        {
                                            if (!string.IsNullOrWhiteSpace(drListaProductosSinConceptoDinamico["CodigoBarras"].ToString()))
                                            {
                                                listaCodigosBarras.Add(drListaProductosSinConceptoDinamico["CodigoBarras"].ToString());
                                            }
                                            else
                                            {
                                                listaCodigosBarras.Add(drListaProductosSinConceptoDinamico["ClaveInterna"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (DataTable dtListaProductosConceptoDinamico = cn.CargarDatos(cs.ListarProductosConceptoDinamico(FormPrincipal.userID, strFiltroDinamico, 1)))
                                {
                                    if (!dtListaProductosConceptoDinamico.Rows.Count.Equals(0))
                                    {
                                        foreach (DataRow drListaProductosConceptoDinamico in dtListaProductosConceptoDinamico.Rows)
                                        {
                                            if (!string.IsNullOrWhiteSpace(drListaProductosConceptoDinamico["CodigoBarras"].ToString()))
                                            {
                                                listaCodigosBarras.Add(drListaProductosConceptoDinamico["CodigoBarras"].ToString());
                                            }
                                            else
                                            {
                                                listaCodigosBarras.Add(drListaProductosConceptoDinamico["ClaveInterna"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        codigo = AplicarFiltro(idProductoAux);
                        aplicar = true;
                    }
                }

                if (listaCodigosBarras.Count > 0)
                {
                    if (countListaCodigosBarras >= 0 && countListaCodigosBarras < listaCodigosBarras.Count)
                    {
                        txtBoxBuscarCodigoBarras.Text = listaCodigosBarras[countListaCodigosBarras].ToString();

                        codigo = txtBoxBuscarCodigoBarras.Text;

                        realizarBusqueda(codigo, aplicar);

                        countListaCodigosBarras++;
                    }
                }
                else
                {
                    // Verifica si el codigo existe en algun producto y si pertenece al usuario
                    // Si existe se trae la informacion del producto
                    var infoProducto = mb.BuscarCodigoInventario(codigo, aplicar);

                    if (infoProducto.Length > 0)
                    {
                        // Para mostrar el numero de registro en el que va el proceso de revision
                        if (tipoFiltro != "Normal")
                        {
                            cantidadRegistrosAux += 1;

                            lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";
                        }

                        txtNombreProducto.Text = infoProducto[0];

                        if (string.IsNullOrEmpty(infoProducto[3]))
                        {
                            txtCodigoBarras.Text = infoProducto[4]; //codigo;
                        }
                        else
                        {
                            txtCodigoBarras.Text = infoProducto[3];
                        }

                        lblPrecioProducto.Text = infoProducto[2];

                        lblStockMinimo.Text = infoProducto[8];
                        lblStockMaximo.Text = infoProducto[7];

                        idProducto = Convert.ToInt32(infoProducto[5]);

                        if (tipoFiltro != "Normal")
                        {
                            idProductoAux = idProducto;
                        }

                        // Verificar si es un producto
                        if (infoProducto[6] == "P")
                        {
                            // Verificar si el producto tiene un mensaje para mostrarse al realizar inventario
                            var mensajeInventario = mb.MensajeInventario(idProducto, 1);

                            if (!string.IsNullOrEmpty(mensajeInventario))
                            {
                                MessageBox.Show(mensajeInventario, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            // Verificar si este producto ya fue inventariado
                            var inventariado = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}'");

                            if (inventariado)
                            {
                                var infoInventariado = mb.DatosProductoInventariado(idProducto);

                                if (infoInventariado.Length > 0)
                                {
                                    var respuesta = MessageBox.Show("Este producto ya fue inventariado\nFecha: " + infoInventariado[2] + " \n\n¿Desea modificarlo?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (respuesta == DialogResult.Yes)
                                    {
                                        //Decrementa el Id utilizar el boton de anterior 
                                        convertirId--;

                                        // Se asigna el stock registrado en la tabla RevisarInventario
                                        txtCantidadStock.Text = infoInventariado[0];
                                    }

                                    if (respuesta == DialogResult.No)
                                    {
                                        LimpiarCampos();
                                        txtBoxBuscarCodigoBarras.Focus();
                                    }
                                }
                            }
                            else
                            {
                                // Se asigna el stock registrado en la tabla Productos
                                txtCantidadStock.Text = infoProducto[1];
                            }

                            txtCantidadStock.Focus();
                            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                        }
                        else
                        {
                            var nombreProductos = string.Empty;

                            var productosRelacionados = mb.ProductosServicio(idProducto);

                            if (productosRelacionados.Count > 0)
                            {
                                nombreProductos += "Contiene los siguientes productos:\n\n";

                                foreach (var relacionado in productosRelacionados)
                                {
                                    nombreProductos += "Producto: " + relacionado.Value.Item1 + " Cantidad: " + relacionado.Value.Item2 + "\n";
                                }
                            }

                            // Verificar si es un servicio o combo y mostrar los productos relacionados
                            if (infoProducto[6] == "S")
                            {
                                MessageBox.Show($"El código de barras pertenece a un SERVICIO\n\n{nombreProductos}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            if (infoProducto[6] == "PQ")
                            {
                                MessageBox.Show($"El código de barras pertenece a un COMBO\n\n{nombreProductos}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            LimpiarCampos();
                            txtBoxBuscarCodigoBarras.Focus();
                        }
                    }
                    else
                    {
                        if (tipoFiltro != "Normal")
                        {
                            MessageBox.Show("No se encontraron productos o no hay más con el filtro aplicado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //btnSiguiente.PerformClick();
                            //btnTerminar.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("Producto no encontrado / Deshabilitado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            //txtCantidadStock.SelectAll();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }

        private void realizarBusqueda(string codigo, bool aplicar)
        {
            var infoProducto = mb.BuscarCodigoInventario(codigo, aplicar);

            if (infoProducto.Length > 0)
            {
                // Para mostrar el numero de registro en el que va el proceso de revision
                if (tipoFiltro != "Normal")
                {
                    cantidadRegistrosAux += 1;

                    lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";
                }

                txtNombreProducto.Text = infoProducto[0];

                if (string.IsNullOrEmpty(infoProducto[3]))
                {
                    txtCodigoBarras.Text = infoProducto[4]; //codigo;
                }
                else
                {
                    txtCodigoBarras.Text = infoProducto[3];
                }

                lblPrecioProducto.Text = infoProducto[2];

                lblStockMinimo.Text = infoProducto[8];
                lblStockMaximo.Text = infoProducto[7];

                idProducto = Convert.ToInt32(infoProducto[5]);

                if (tipoFiltro != "Normal")
                {
                    idProductoAux = idProducto;
                }

                // Verificar si es un producto
                if (infoProducto[6] == "P")
                {
                    // Verificar si el producto tiene un mensaje para mostrarse al realizar inventario
                    var mensajeInventario = mb.MensajeInventario(idProducto, 1);

                    if (!string.IsNullOrEmpty(mensajeInventario))
                    {
                        MessageBox.Show(mensajeInventario, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Verificar si este producto ya fue inventariado
                    var inventariado = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}'");

                    if (inventariado)
                    {
                        var infoInventariado = mb.DatosProductoInventariado(idProducto);

                        if (infoInventariado.Length > 0)
                        {
                            var respuesta = MessageBox.Show("Este producto ya fue inventariado\nFecha: " + infoInventariado[2] + " \n\n¿Desea modificarlo?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (respuesta == DialogResult.Yes)
                            {
                                // Se asigna el stock registrado en la tabla RevisarInventario
                                txtCantidadStock.Text = infoInventariado[0];
                            }

                            if (respuesta == DialogResult.No)
                            {
                                LimpiarCampos();
                                txtBoxBuscarCodigoBarras.Focus();
                            }
                        }
                    }
                    else
                    {
                        // Se asigna el stock registrado en la tabla Productos
                        txtCantidadStock.Text = infoProducto[1];
                    }

                    txtCantidadStock.Focus();
                    txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                }
                else
                {
                    var nombreProductos = string.Empty;

                    var productosRelacionados = mb.ProductosServicio(idProducto);

                    if (productosRelacionados.Count > 0)
                    {
                        nombreProductos += "Contiene los siguientes productos:\n\n";

                        foreach (var relacionado in productosRelacionados)
                        {
                            nombreProductos += "Producto: " + relacionado.Value.Item1 + " Cantidad: " + relacionado.Value.Item2 + "\n";
                        }
                    }

                    // Verificar si es un servicio o combo y mostrar los productos relacionados
                    if (infoProducto[6] == "S")
                    {
                        MessageBox.Show($"El código de barras pertenece a un SERVICIO\n\n{nombreProductos}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (infoProducto[6] == "PQ")
                    {
                        MessageBox.Show($"El código de barras pertenece a un COMBO\n\n{nombreProductos}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LimpiarCampos();
                    txtBoxBuscarCodigoBarras.Focus();
                }
            }
            else
            {
                if (tipoFiltro != "Normal")
                {
                    MessageBox.Show("No se encontraron productos o no hay más con el filtro aplicado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //btnSiguiente.PerformClick();
                    //btnTerminar.PerformClick();
                }
                else
                {
                    MessageBox.Show("Producto no encontrado / Deshabilitado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBoxBuscarCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buscarCodigoBarras();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            var busqueda = txtBoxBuscarCodigoBarras.Text;

            if (tipoFiltro != "Normal")
            {
                busqueda = "auxiliar";
            }

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                {
                    var cantidadStock = double.Parse(txtCantidadStock.Text);

                    if (cantidadStock >= 1000)
                    {
                        var respuesta = MessageBox.Show("¿Estás seguro de agregar esta cantidad al stock?\n\nCantidad: " + cantidadStock, "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (respuesta == DialogResult.No)
                        {
                            txtCantidadStock.Focus();
                            return;
                        }
                    }

                    // Comprobamos si el producto ya fue revisado
                    var existe = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}'");
                    idDeProductos.Add(idProducto);

                    // Si ya fue inventariado el producto actualizamos informacion
                    if (existe)
                    {
                        var info = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                        var datosProducto = mb.DatosProductoInventariado(idProducto);

                        var stockFisico = txtCantidadStock.Text;
                        var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var diferencia = double.Parse(datosProducto[1]) - double.Parse(stockFisico);

                        // Actualizar datos en RevisarInventario
                        cn.EjecutarConsulta($"UPDATE RevisarInventario SET StockAlmacen = '{info[4]}', StockFisico = '{stockFisico}', Fecha = '{fecha}', Diferencia = '{diferencia}' WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}'");

                        // Para envio de correo
                        if (listaProductos.ContainsKey(idProducto))
                        {
                            // Obtenemos los datos del producto para el email
                            var datosProductoAux = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                            var html = $@"<li>
                                            <span style='color: red;'>{datosProductoAux[1]}</span> 
                                            --- <b>STOCK ANTERIOR:</b> 
                                            <span style='color: red;'>{datosProductoAux[4]}</span> 
                                            --- <b>STOCK NUEVO:</b> 
                                            <span style='color: red;'>{stockFisico}</span>
                                        </li>";



                            listaProductos[idProducto] = html;
                        }

                        // Actualizar stock del producto
                        cn.EjecutarConsulta($"UPDATE Productos SET Stock = '{stockFisico}' WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        LimpiarCampos();
                        //txtBoxBuscarCodigoBarras.Focus();

                        if (tipoFiltro == "Normal")
                        {
                            txtBoxBuscarCodigoBarras.Focus();
                        }
                        else
                        {
                            buscarCodigoBarras();
                            txtCantidadStock.Focus();
                        }
                    }
                    else
                    {
                        var info = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                        var stockFisico = txtCantidadStock.Text;
                        var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        var diferencia = double.Parse(info[4]) - double.Parse(stockFisico);

                        var datos = new string[]
                        {
                            idProducto.ToString(), info[1], info[6], info[7], info[4], stockFisico, numeroRevision,
                            fecha, "0", diferencia.ToString(), FormPrincipal.userID.ToString(), info[5], "0", "1", info[2],
                            nombrePC
                        };

                        // Guardamos la informacion en la tabla de RevisarInventario
                        cn.EjecutarConsulta(cs.GuardarRevisarInventario(datos));

                        // Para envio de correo
                        var datosConfig = mb.ComprobarConfiguracion();

                        if (datosConfig.Count > 0)
                        {
                            if (Convert.ToInt16(datosConfig[1]) == 1)
                            {
                                var configProducto = mb.ComprobarCorreoProducto(idProducto);

                                if (configProducto.Count > 0)
                                {
                                    if (configProducto[1] == 1)
                                    {
                                        // Obtenemos los datos del producto para el email
                                        var datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                                        if (datosProducto[4] != stockFisico)
                                        {
                                            var html = $@"<li>
                                                            <span style='color: red;'>{datosProducto[1]}</span> 
                                                            --- <b>STOCK ANTERIOR:</b> 
                                                            <span style='color: red;'>{datosProducto[4]}</span> 
                                                            --- <b>STOCK NUEVO:</b> 
                                                            <span style='color: red;'>{stockFisico}</span>
                                                        </li>";

                                            listaProductos.Add(idProducto, html);
                                        }
                                    }
                                }
                            }
                        }

                        // Actualizar stock del producto
                        cn.EjecutarConsulta($"UPDATE Productos SET Stock = '{stockFisico}' WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        LimpiarCampos();

                        if (tipoFiltro == "Normal")
                        {
                            txtBoxBuscarCodigoBarras.Focus();
                        }
                        else
                        {
                            buscarCodigoBarras();
                            txtCantidadStock.Focus();
                        }
                    }
                }
            }
        }

        private void txtCantidadStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtCantidadStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                var busqueda = txtBoxBuscarCodigoBarras.Text;

                if (tipoFiltro != "Normal")
                {
                    busqueda = "auxiliar";
                }

                if (!string.IsNullOrWhiteSpace(busqueda))
                {
                    if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                    {
                        btnSiguiente.PerformClick();
                        //txtBoxBuscarCodigoBarras.Text = string.Empty;
                        //txtBoxBuscarCodigoBarras.Focus();
                    }
                }
            }
        }

        private void RevisarInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void btnReducirStock_Click(object sender, EventArgs e)
        {
            decimal cantidad = 0;

            if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                cantidad = Convert.ToDecimal(txtCantidadStock.Text);
            }

            cantidad -= 1;

            if (cantidad < 0)
            {
                txtCantidadStock.Text = "0";
                txtCantidadStock.Focus();
                txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);

                MessageBox.Show("La cantidad de stock no puede ser menor a cero", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCantidadStock.Text = cantidad.ToString();
            txtCantidadStock.Focus();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }


        private void btnAumentarStock_Click(object sender, EventArgs e)
        {
            decimal cantidad = 0;

            if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                cantidad = Convert.ToDecimal(txtCantidadStock.Text);
            }

            cantidad += 1;

            txtCantidadStock.Text = cantidad.ToString();
            txtCantidadStock.Focus();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            btnSiguiente.PerformClick();

            DialogResult deseaTernimar = MessageBox.Show("Desea Terminar la Revision", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (deseaTernimar == DialogResult.Yes)
            {
                // Guardamos los dos Datos de las variables del sistema
                //Properties.Settings.Default.InicioFinInventario = 2;
                //Properties.Settings.Default.Save();

                // Actualizar el numero de revision despues de haber terminado el inventario
                var numeroRevisionTmp = Convert.ToInt32(numeroRevision) + 1;

                cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET NoRevision = {numeroRevisionTmp} WHERE IDUsuario = {FormPrincipal.userID}", true);

                // Cambiamos el valor de la variable para eliminar los registros de la tabla RevisarInventario con el numero de revision
                Inventario.limpiarTabla = true;

                if (listaProductos.Count > 0)
                {
                    var html = string.Empty;

                    foreach (var producto in listaProductos)
                    {
                        html += producto.Value;
                    }

                    // Ejecutar hilo para enviar notificacion
                    var datos = new string[] { html, "", "", "", "REVISAR INVENTARIO", "" };

                    Thread notificacion = new Thread(
                        () => Utilidades.CambioStockProductoEmail(datos, 1)
                    );

                    notificacion.Start();

                    listaProductos.Clear();
                }

                this.Hide();
                this.Close();
            }
        }

        private void LimpiarCampos()
        {
            txtBoxBuscarCodigoBarras.Text = string.Empty;
            txtCantidadStock.Text = string.Empty;
            txtNombreProducto.Text = string.Empty;
            txtCodigoBarras.Text = string.Empty;
            lblPrecioProducto.Text = string.Empty;
            lblStockMinimo.Text = string.Empty;
            lblStockMaximo.Text = string.Empty;
        }

        private void RevisarInventario_Shown(object sender, EventArgs e)
        {
            if (tipoFiltro == "Normal")
            {
                txtBoxBuscarCodigoBarras.Focus();
            }
            else
            {
                txtCantidadStock.Focus();
                txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
            }
        }

        private void btnVerCBExtra_Click(object sender, EventArgs e)
        {
            if (idProducto > 0)
            {
                var codigos = mb.ObtenerCodigoBarrasExtras(idProducto);

                if (codigos.Length > 0)
                {
                    using (var codigosExtra = new CodigoBarrasExtraRI(codigos))
                    {
                        codigosExtra.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("No hay información extra disponible", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDeshabilitarProducto_Click(object sender, EventArgs e)
        {
            var idObtenido = idProducto;

            DialogResult confirmarDesicion = MessageBox.Show("¿Desea Deshabilitar este producto?", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmarDesicion == DialogResult.Yes)
            {
                //lbBackground.Text = string.Empty;
                //LimpiarCampos();
                //txtBoxBuscarCodigoBarras.Focus();
                btnSiguiente.PerformClick();
                cn.EjecutarConsulta($"UPDATE Productos SET Status = 0 WHERE IDUsuario = {FormPrincipal.userID} AND ID = {idObtenido}");
                cn.EjecutarConsulta($"DELETE FROM RevisarInventario WHERE IDAlmacen = {idObtenido} AND IDUsuario = {FormPrincipal.userID} AND NoRevision = {NoRevision}");
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            //if (validarAnterior == true)
            //{
            //    validarAnterior = false;
            //    using (var datoAnterior = cn.CargarDatos($"SELECT ID FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' ORDER BY ID DESC LIMIT 1"))
            //    {
            //        if (!datoAnterior.Rows.Count.Equals(0))
            //        {
            //            var result = datoAnterior.Rows[0]["ID"].ToString();
            //            convertirId = Convert.ToInt32(result);

            //            var restaId = cn.CargarDatos($"SELECT COUNT(*) AS Count FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}'");

            //            numFilasExistentes = Convert.ToInt32(restaId.Rows[0]["Count"].ToString());
            //        }
            //        else
            //        {
            //            MessageBox.Show("No hay productos revisados anteriormente", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //}
            //var idStatico = (convertirId - numFilasExistentes);

            //if ((convertirId - numFilasExistentes) <= idStatico && (convertirId - numFilasExistentes) != 0/*convertirId > 0*/)
            //{
            //    //MessageBox.Show($"Id:{convertirId}");
            //    var informacionProducto = cn.CargarDatos($"SELECT * FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{convertirId}'");

            //    foreach (DataRow datosObtenidos in informacionProducto.Rows)
            //    {
            //        if (!datosObtenidos["CodigoBarras"].ToString().Equals(0))
            //        {
            //            txtBoxBuscarCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
            //        }
            //        else if (!datosObtenidos["ClaveInterna"].ToString().Equals(0))
            //        {
            //            txtBoxBuscarCodigoBarras.Text = datosObtenidos["ClaveInterna"].ToString();
            //        }
            //        buscarCodigoBarras();
            //    }

            //    //convertirId--; == Esta dentro de un mbox con una variabble llamada respuesta dentro del metodo "buscarCodigoBarras()" en este mismo form
            //}
            //else
            //{
            //    MessageBox.Show("No hay mas productos anteriores", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}



            //var idMafufo = 0;
            //for (int x = 0; x < idDeProductos.Count; x++)
            //{
            //    idMafufo = idDeProductos[x];
            //}
            //idProductoAux = (idMafufo - 1);


            //var datosId = cn.CargarDatos($"SELECT * FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idMafufo}'");
            //if (!datosId.Rows.Count.Equals(0))
            //{
            //    foreach (DataRow datosObtenidos in datosId.Rows)
            //    {
            //        if (!datosObtenidos["CodigoBarras"].ToString().Equals(0))
            //        {
            //            txtBoxBuscarCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
            //        }
            //        else if (!datosObtenidos["ClaveInterna"].ToString().Equals(0))
            //        {
            //            txtBoxBuscarCodigoBarras.Text = datosObtenidos["ClaveInterna"].ToString();
            //        }
            //        buscarCodigoBarras();
            //    }
            //}

            var codeBarras = txtCodigoBarras.Text;

            var idActual = cn.CargarDatos($"SELECT IDAlmacen FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' AND ClaveInterna = '{codeBarras}'");
            if (!idActual.Rows.Count.Equals(0))
            {
                var getId = Convert.ToInt32(idActual.Rows[0]["IDAlmacen"].ToString());
                var datosId = cn.CargarDatos($"SELECT * FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{(getId-1)}'");

                foreach (DataRow datosObtenidos in datosId.Rows)
                {
                    if (!datosObtenidos["ClaveInterna"].ToString().Equals(0))
                    {
                        LimpiarCampos();
                        txtNombreProducto.Text = datosObtenidos["Nombre"].ToString();
                        txtCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
                        lblPrecioProducto.Text = datosObtenidos["Precio"].ToString();
                        lblStockMinimo.Text = datosObtenidos["StockMinimo"].ToString();
                        lblStockMaximo.Text = datosObtenidos["StockNecesario"].ToString();
                        txtCantidadStock.Text = datosObtenidos["Stock"].ToString();
                        txtCantidadStock.Focus();
                    }
                    //else if (!datosObtenidos["CodigoBarras"].ToString().Equals(0))
                    //{
                    //    txtBoxBuscarCodigoBarras.Text = datosObtenidos["ClaveInterna"].ToString();
                    //}
                    //buscarCodigoBarras();
                }
            }
            else 
            {
                var idActual2 = cn.CargarDatos($"SELECT IDAlmacen FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codeBarras}'");
                if (!idActual2.Rows.Count.Equals(0))
                {
                    var getId = Convert.ToInt32(idActual.Rows[0]["IDalmacen"].ToString());
                    var datosId = cn.CargarDatos($"SELECT * FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{(getId - 1)}'");

                    foreach (DataRow datosObtenidos in datosId.Rows)
                    {
                        if (!datosObtenidos["CodigoBarras"].ToString().Equals(0))
                        {
                            LimpiarCampos();
                            txtNombreProducto.Text = datosObtenidos["Nombre"].ToString();
                            txtCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
                            lblPrecioProducto.Text = datosObtenidos["Precio"].ToString();
                            lblStockMinimo.Text = datosObtenidos["StockMinimo"].ToString();
                            lblStockMaximo.Text = datosObtenidos["StockNecesario"].ToString();
                            txtCantidadStock.Text = datosObtenidos["Stock"].ToString();
                        }
                        //else if (!datosObtenidos["ClaveInterna"].ToString().Equals(0))
                        //{
                        //    txtBoxBuscarCodigoBarras.Text = datosObtenidos["ClaveInterna"].ToString();
                        //}
                        //buscarCodigoBarras();
                    }
                }
                MessageBox.Show("No hay mas productos anteriores", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            BusquedaRevisionInventario busquedaR = new BusquedaRevisionInventario();

            busquedaR.FormClosed += delegate
            {
                
                if (!string.IsNullOrEmpty(BusquedaRevisionInventario.codigoBarras))
                {
                    var codBar = BusquedaRevisionInventario.codigoBarras.ToString();
                    LimpiarCampos();
                    txtBoxBuscarCodigoBarras.Text = codBar;
                    llenarCampos(codBar);
                }
            };
            busquedaR.ShowDialog();
            txtCantidadStock.Focus();
        }

        private void llenarCampos(string codigo)
        {
            using (var traerDatos = cn.CargarDatos($"SELECT Nombre, CodigoBarras, Precio, StockMinimo, StockNecesario, Stock FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codigo}'"))
            {
                if (!traerDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow dato in traerDatos.Rows)
                    {
                        txtNombreProducto.Text = dato["Nombre"].ToString();
                        txtCodigoBarras.Text = dato["CodigoBarras"].ToString();
                        lblPrecioProducto.Text = dato["Precio"].ToString();
                        lblStockMinimo.Text = dato["StockMinimo"].ToString();
                        lblStockMaximo.Text = dato["StockNecesario"].ToString();
                        txtCantidadStock.Text = dato["Stock"].ToString();
                    }
                }
            }
        }
    }
}
