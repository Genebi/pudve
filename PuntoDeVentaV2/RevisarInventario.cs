﻿using System;
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
        bool busquedaNormal = false;

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
        int numFilasExistentes = -1;

        string codBarras = string.Empty;

        //Validar la busqueda de el boton siguiente con el boton Anterior
        int idActualAnterior = 0;

        //Valifacion para el boton de Omitir
        bool botonOmitir = false;

        Dictionary<int, string> listaProductos;

        List<int> idDeProductos = new List<int>();

        List<string> id = new List<string>();
        int contador = 1;

        bool validarSiguienteTerminar = false;
        string mensajeNoHay = string.Empty;
        int terminarRev = 0;


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

            if (FiltroRevisarInventario.datoCbo == "Normal")
            {
                //btnAnterior.Visible = false;
                btnOmitir.Location = new Point(23, 19);
                btnTerminar.Location = new Point(23, 83);
                btnTerminar.Size = new Size(373, 48);
            }
            else
            {
                //btnAnterior.Visible = true;
                btnAnterior.Location = new Point(23, 19);
                btnSiguiente.Location = new Point(225, 19);
                btnOmitir.Location = new Point(23, 83);
                btnTerminar.Location = new Point(225, 83);
                btnTerminar.Size = new Size(171, 48);
            }
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            var mostrarClave = FormPrincipal.clave;

            if (mostrarClave == 0)
            {
                lCodigoClave.Text = "Código de Barras:";
            }
            else if (mostrarClave == 1)
            {
                lCodigoClave.Text = "Código de Barras o Clave Interna:";
            }

            //var datosInventario = mb.DatosRevisionInventario();

            //listaProductos = new Dictionary<int, string>();

            //// Si existe un registro en la tabla obtiene los datos de lo contrario hace un insert para
            //// que exista la configuracion necesaria
            //if (datosInventario.Length > 0)
            //{
            //    cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET FechaInventario = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE IDUsuario = {FormPrincipal.userID}", true);

            //    datosInventario = mb.DatosRevisionInventario();
            //    fechaInventario = datosInventario[0];
            //    numeroRevision = datosInventario[1];
            //}
            //else
            //{
            //    cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, FechaInventario, NoRevision) VALUES ('{FormPrincipal.userID}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '1')", true);

            //    datosInventario = mb.DatosRevisionInventario();
            //    fechaInventario = datosInventario[0];
            //    numeroRevision = datosInventario[1];
            //}

            //lblNoRevision.Text = numeroRevision;

            //// Asignamos el numero de revision para que cargue los productos en el reporte al cerrar el form
            //Inventario.NumRevActivo = Convert.ToInt32(numeroRevision);
            //NoRevision = Convert.ToInt32(numeroRevision);
            //// Obtener el nombre de la computadora
            //nombrePC = Environment.MachineName;

            //// Ejecutar busqueda de productos cuando hay filtro
            //if (tipoFiltro != "Normal")
            //{
            //    if (tipoFiltro != "Filtros")
            //    {
            //        var consulta = $"SELECT COUNT(ID) AS Total FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND {tipoFiltro} {operadorFiltro} {cantidadFiltro}";
            //        cantidadRegistros = mb.CantidadFiltroInventario(consulta);
            //    }
            //    else
            //    {
            //        if (operadorFiltro.Equals("chkProveedor"))
            //        {
            //            if (strFiltroDinamico.Equals("SIN PROVEEDOR"))
            //            {
            //                var consulta = cs.CantidadListaProductosSinProveedor(FormPrincipal.userID, 1);
            //                cantidadRegistros = mb.CantidadFiltroInventario(consulta);
            //            }
            //            else
            //            {
            //                var consulta = cs.CantidadListaProductosProveedor(FormPrincipal.userID, strFiltroDinamico, 1);
            //                cantidadRegistros = mb.CantidadFiltroInventario(consulta);
            //            }
            //        }
            //        else
            //        {
            //            string Seleccionado = "SIN " + operadorFiltro.ToUpper().Remove(0, 3);
            //            if (strFiltroDinamico.Equals(Seleccionado))
            //            {
            //                var consulta = cs.CantidadListarProductosSinConceptoDinamico(FormPrincipal.userID, operadorFiltro.Remove(0, 3), 1);
            //                cantidadRegistros = mb.CantidadFiltroInventario(consulta);
            //            }
            //            else
            //            {
            //                var consulta = cs.CantidadListarProductosConceptoDinamico(FormPrincipal.userID, strFiltroDinamico, 1);
            //                cantidadRegistros = mb.CantidadFiltroInventario(consulta);
            //            }
            //        }
            //    }

            //    //lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";
            //    buscarCodigoBarras();
            //}
        }

        private string AplicarFiltro(int idProducto)
        {
            var consulta = string.Empty;

            if (tipoFiltro != "Normal")
            {
                if (tipoFiltro != "Filtros")
                {
                    consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND {tipoFiltro} {operadorFiltro} {cantidadFiltro} AND ID > {idProducto} AND (CodigoBarras != '' OR ClaveInterna != '') ORDER BY ID ASC LIMIT 1";
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
            if (cantidadRegistrosAux != cantidadRegistros || busquedaNormal == true)
            {
                // variableSin = SIN
                // 
                //id.Clear();
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
                                    { // En este las busquedas solo son con idUsuario y status = 1
                                        if (!dtListaProductosSinProveedor.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow drListaProductosSinProveedor in dtListaProductosSinProveedor.Rows)
                                            {
                                                if (!string.IsNullOrWhiteSpace(drListaProductosSinProveedor["CodigoBarras"].ToString()))
                                                {
                                                    listaCodigosBarras.Add(drListaProductosSinProveedor["CodigoBarras"].ToString());
                                                    id.Add(drListaProductosSinProveedor["ID"].ToString());
                                                }
                                                else
                                                {
                                                    listaCodigosBarras.Add(drListaProductosSinProveedor["ClaveInterna"].ToString());
                                                    id.Add(drListaProductosSinProveedor["ID"].ToString());
                                                }
                                            }
                                        }
                                        //id.Clear();
                                    }
                                    txtCantidadStock.SelectAll();
                                }
                                else
                                {
                                    using (DataTable dtListaProductosProveedor = cn.CargarDatos(cs.ListarProductosProveedor(FormPrincipal.userID, strFiltroDinamico, 1)))
                                    {// Esta es con idUsuario, nombreProvedor, status =1
                                        if (!dtListaProductosProveedor.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow drListaProductosProveedor in dtListaProductosProveedor.Rows)
                                            {
                                                if (!string.IsNullOrWhiteSpace(drListaProductosProveedor["CodigoBarras"].ToString()))
                                                {
                                                    listaCodigosBarras.Add(drListaProductosProveedor["CodigoBarras"].ToString());
                                                    id.Add(drListaProductosProveedor["ID"].ToString());
                                                }
                                                else
                                                {
                                                    listaCodigosBarras.Add(drListaProductosProveedor["ClaveInterna"].ToString());
                                                    id.Add(drListaProductosProveedor["ID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    //id.Clear();
                                    txtCantidadStock.SelectAll();
                                }
                            }
                            else
                            {
                                listaCodigosBarras.Clear();
                                if (strFiltroDinamico.Equals("SIN " + operadorFiltro.ToUpper().Remove(0, 3)))
                                {
                                    using (DataTable dtListaProductosSinConceptoDinamico = cn.CargarDatos(cs.ListarProductosSinConceptoDinamico(FormPrincipal.userID, operadorFiltro.Remove(0, 3), 1)))
                                    {// Este es sin conceptoDinamico y con idUsuario y status = 1
                                        if (!dtListaProductosSinConceptoDinamico.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow drListaProductosSinConceptoDinamico in dtListaProductosSinConceptoDinamico.Rows)
                                            {
                                                if (!string.IsNullOrWhiteSpace(drListaProductosSinConceptoDinamico["CodigoBarras"].ToString()))
                                                {
                                                    listaCodigosBarras.Add(drListaProductosSinConceptoDinamico["CodigoBarras"].ToString());
                                                    id.Add(drListaProductosSinConceptoDinamico["ID"].ToString());
                                                }
                                                else
                                                {
                                                    listaCodigosBarras.Add(drListaProductosSinConceptoDinamico["ClaveInterna"].ToString());
                                                    id.Add(drListaProductosSinConceptoDinamico["ID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    //id.Clear();
                                    txtCantidadStock.SelectAll();
                                }
                                else
                                {
                                    using (DataTable dtListaProductosConceptoDinamico = cn.CargarDatos(cs.ListarProductosConceptoDinamico(FormPrincipal.userID, strFiltroDinamico, 1)))
                                    {// Este es con conceptoDinamico, idusuario y status 1
                                        if (!dtListaProductosConceptoDinamico.Rows.Count.Equals(0))
                                        {
                                            foreach (DataRow drListaProductosConceptoDinamico in dtListaProductosConceptoDinamico.Rows)
                                            {
                                                if (!string.IsNullOrWhiteSpace(drListaProductosConceptoDinamico["CodigoBarras"].ToString()))
                                                {
                                                    listaCodigosBarras.Add(drListaProductosConceptoDinamico["CodigoBarras"].ToString());
                                                    id.Add(drListaProductosConceptoDinamico["ID"].ToString());
                                                }
                                                else
                                                {
                                                    listaCodigosBarras.Add(drListaProductosConceptoDinamico["ClaveInterna"].ToString());
                                                    id.Add(drListaProductosConceptoDinamico["ID"].ToString());
                                                }
                                            }
                                        }
                                    }
                                    //id.Clear();
                                    txtCantidadStock.SelectAll();
                                }
                            }
                        }
                        else
                        {
                            //var idNormal = idProductoAux;
                            if (idActualAnterior != 0)
                            {
                                var mostrarIDActual = 0;
                                var idParaSiguiente = cn.CargarDatos($"SELECT ID FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codBarras}' AND Status = 1 AND (CodigoBarras != '' OR ClaveInterna != '')");

                                if (!idParaSiguiente.Rows.Count.Equals(0))
                                {
                                    mostrarIDActual = Convert.ToInt32(idParaSiguiente.Rows[0]["ID"]);

                                    codigo = AplicarFiltro(mostrarIDActual);
                                    aplicar = true;
                                }
                            }
                            else
                            {
                                codigo = AplicarFiltro(idProductoAux);
                                aplicar = true;
                            }
                            txtCantidadStock.SelectAll();
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
                        txtCantidadStock.SelectAll();
                    }
                    else
                    {
                        // Verifica si el codigo existe en algun producto y si pertenece al usuario
                        // Si existe se trae la informacion del producto
                        var infoProducto = mb.BuscarCodigoInventario(codigo, aplicar);
                        var idFiltrado = MetodosBusquedas.idFiltrado.ToString();
                        if (!string.IsNullOrEmpty(idFiltrado))
                        {
                            id.Add(idFiltrado);
                        }

                        if (infoProducto.Length > 0)
                        {
                            // Para mostrar el numero de registro en el que va el proceso de revision
                            if (tipoFiltro != "Normal")
                            {
                                cantidadRegistrosAux += 1;

                                lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";
                            }

                            txtBoxBuscarCodigoBarras.Text = infoProducto[4];
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
                                var inventariado = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}' AND (CodigoBarras != '' OR ClaveInterna != '')");

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
                                            txtCantidadStock.Text = Utilidades.RemoverCeroStock(infoInventariado[0]);
                                        }

                                        if (respuesta == DialogResult.No)
                                        {
                                            //LimpiarCampos();
                                            //txtBoxBuscarCodigoBarras.Focus();
                                            txtCantidadStock.Text = Utilidades.RemoverCeroStock(infoInventariado[0]);
                                            btnOmitir.PerformClick();
                                            txtCantidadStock.Focus();
                                        }
                                    }
                                    txtCantidadStock.Focus();
                                }
                                else
                                {
                                    // Se asigna el stock registrado en la tabla Productos
                                    txtCantidadStock.Text = Utilidades.RemoverCeroStock(infoProducto[1]);
                                }

                                txtCantidadStock.Focus();
                                //txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
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
                        txtCantidadStock.SelectAll();
                    }
                }
                //txtCantidadStock.SelectAll();
                //txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
            }
            else
            {
                //mensajeNoHay = "No existen mas productos con los filtros aplpicados \n¿Desea terminar la revisión?";
                //var mensajeTerminar = MessageBox.Show("No existen mas productos con los filtros aplpicados \n¿Desea terminar la revisión?", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //if (mensajeTerminar == DialogResult.Yes)
                //{
                //}

                if (terminarRev == 0)
                {
                    btnTerminar.PerformClick();
                }
            }
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

                //txtBoxBuscarCodigoBarras = 
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
                    var inventariado = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}' AND (CodigoBarras != '' OR ClaveInterna != '')");

                    if (inventariado)
                    {
                        var infoInventariado = mb.DatosProductoInventariado(idProducto);

                        if (infoInventariado.Length > 0)
                        {
                            var respuesta = MessageBox.Show("Este producto ya fue inventariado\nFecha: " + infoInventariado[2] + " \n\n¿Desea modificarlo?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (respuesta == DialogResult.Yes)
                            {
                                // Se asigna el stock registrado en la tabla RevisarInventario
                                txtCantidadStock.Text = Utilidades.RemoverCeroStock(infoInventariado[0]);
                            }
                            txtCantidadStock.Focus();

                            if (respuesta == DialogResult.No)
                            {
                                //LimpiarCampos();
                                //txtBoxBuscarCodigoBarras.Focus();
                                btnOmitir.PerformClick();
                                txtCantidadStock.Focus();
                            }
                        }
                    }
                    else
                    {
                        // Se asigna el stock registrado en la tabla Productos
                        txtCantidadStock.Text = Utilidades.RemoverCeroStock(infoProducto[1]);
                    }

                    txtCantidadStock.Focus();
                    //txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
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
                busquedaNormal = true;
                buscarCodigoBarras();
                busquedaNormal = false;
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            codBarras = txtBoxBuscarCodigoBarras.Text;

            contador++;
            var busqueda = txtBoxBuscarCodigoBarras.Text;

            if (tipoFiltro != "Normal")
            {
                busqueda = "auxiliar";
            }

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                {
                    if (botonOmitir == false)
                    {
                        var cantidadStock = double.Parse(txtCantidadStock.Text);

                        if (cantidadStock >= 1000)
                        {
                            var respuesta = MessageBox.Show("¿Estás seguro de agregar esta cantidad al stock?\n\nCantidad: " + cantidadStock, "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (respuesta == DialogResult.No)
                            {
                                txtCantidadStock.Focus();
                                txtCantidadStock.SelectAll();
                                return;
                            }
                        }

                        // Comprobamos si el producto ya fue revisado
                        var existe = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}' AND (CodigoBarras != '' OR ClaveInterna != '')");
                        idDeProductos.Add(idProducto);

                        if (id.Count.Equals(0))
                        {
                            id.Add(idProducto.ToString());
                        }


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
                                txtCantidadStock.SelectAll();
                            }

                        }
                        else
                        {
                            if (botonOmitir == false)
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
                                    txtCantidadStock.SelectAll();
                                }
                            }
                        }
                    }
                    else
                    {
                        LimpiarCampos();
                        buscarCodigoBarras();
                        txtCantidadStock.Focus();
                        txtCantidadStock.SelectAll();
                        botonOmitir = true;
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
            //txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
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
            //txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            string terminar = "¿Desea terminar la revisión?";
            string mensajeMostrar = string.Empty;

            if (string.IsNullOrEmpty(mensajeNoHay))
            {
                mensajeMostrar = terminar;
            }
            else
            {
                mensajeMostrar = mensajeNoHay;
            }

                DialogResult deseaTernimar = MessageBox.Show($"{mensajeMostrar}", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (deseaTernimar == DialogResult.Yes)
            {
                terminarRev = 1;
                validarSiguienteTerminar = true;

                btnSiguiente.PerformClick();

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
                //txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
            }

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
                    var consulta = $"SELECT COUNT(ID) AS Total FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' AND {tipoFiltro} {operadorFiltro} {cantidadFiltro} AND (CodigoBarras != '' OR ClaveInterna != '')";
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
            if (!string.IsNullOrEmpty(txtBoxBuscarCodigoBarras.Text))
            {
                var idObtenido = idProducto;

                DialogResult confirmarDesicion = MessageBox.Show("¿Desea Deshabilitar este producto?", "Mensaje de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmarDesicion == DialogResult.Yes)
                {
                    //Se cambia el status de el producto a 0
                    cn.EjecutarConsulta($"UPDATE Productos SET Status = 0 WHERE IDUsuario = {FormPrincipal.userID} AND ID = {idObtenido}");
                    //cn.EjecutarConsulta($"DELETE FROM RevisarInventario WHERE IDAlmacen = {idObtenido} AND IDUsuario = {FormPrincipal.userID} AND NoRevision = {NoRevision}");

                    //Se elimina el ultimo dato en guardarse en esta lista
                    id.RemoveAt(id.Count - 1);

                    //Se elimina el ultimo dato en guardarse en esta lista (esta linea debe estar abajo de "btnOmitir.PerformClick();")
                    idDeProductos.RemoveAt(idDeProductos.Count - 1);

                    btnOmitir.PerformClick();



                    cantidadRegistros--;
                    cantidadRegistrosAux--;
                    lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";

                }
            }
            else
            {
                MessageBox.Show("No existe producto seleccionado para deshabilitar","Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            var idBueno = string.Empty;
            var obteniendoId = string.Empty;
            var validarAnterior = false;
            var codigoB = txtBoxBuscarCodigoBarras.Text;
            var claveB = txtCodigoBarras.Text;
            var buscarCode = string.Empty;

            var mensajeNoHayAnterior = false;

            if (claveB == string.Empty) { buscarCode = codigoB; } else { buscarCode = claveB; }

            using (var idActual = cn.CargarDatos($"SELECT ID FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND (CodigoBarras = '{buscarCode}' OR ClaveInterna = '{buscarCode}') AND Status = 1 AND (CodigoBarras != '' OR ClaveInterna != '')"))
            {
                if (!idActual.Rows.Count.Equals(0))
                {
                    foreach (DataRow dato in idActual.Rows)
                    {
                        idBueno = dato["ID"].ToString();
                    }

                    idActualAnterior = Convert.ToInt32(idBueno);

                    //Aqui tenemos la posicion de la lista en que esta el ID
                    int getIndice = id.FindIndex(y => y == idBueno);

                    for (int x = 0; x <= id.Count; x++)
                    {
                        if (x == getIndice)
                        {
                            if (x != 0)
                            {
                                obteniendoId = id[x - 1].ToString();
                                validarAnterior = true;

                                //Se modifica el id(Esto es para el boton Siguiente)
                                idProducto = Convert.ToInt32(obteniendoId);
                            }
                            else
                            {
                                validarAnterior = false;
                                //mensajeNoHayAnterior = true;
                            }
                        }
                    }
                }
            }

            if (validarAnterior == true)
            {
                var mostrarDatos = cn.CargarDatos($"SELECT * FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{obteniendoId}' AND  Status = 1 AND (CodigoBarras != '' OR ClaveInterna != '')");
                if (!mostrarDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow datosObtenidos in mostrarDatos.Rows)
                    {
                        var contenidoCodigoClave = mostrarDatos.Rows[0]["ClaveInterna"].ToString();

                        if (/*!datosObtenidos["ClaveInterna"].ToString().Equals(0)*/!string.IsNullOrWhiteSpace(contenidoCodigoClave))
                        {
                            LimpiarCampos();
                            txtBoxBuscarCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
                            txtNombreProducto.Text = datosObtenidos["Nombre"].ToString();
                            txtCodigoBarras.Text = datosObtenidos["ClaveInterna"].ToString();//
                            lblPrecioProducto.Text = datosObtenidos["Precio"].ToString();
                            lblStockMinimo.Text = datosObtenidos["StockMinimo"].ToString();
                            lblStockMaximo.Text = datosObtenidos["StockNecesario"].ToString();
                            txtCantidadStock.Text = Utilidades.RemoverCeroStock(datosObtenidos["Stock"].ToString());
                            txtCantidadStock.Focus();
                        }
                        else
                        {
                            LimpiarCampos();
                            txtBoxBuscarCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
                            txtNombreProducto.Text = datosObtenidos["Nombre"].ToString();
                            txtCodigoBarras.Text = datosObtenidos["CodigoBarras"].ToString();
                            lblPrecioProducto.Text = datosObtenidos["Precio"].ToString();
                            lblStockMinimo.Text = datosObtenidos["StockMinimo"].ToString();
                            lblStockMaximo.Text = datosObtenidos["StockNecesario"].ToString();
                            txtCantidadStock.Text = Utilidades.RemoverCeroStock(datosObtenidos["Stock"].ToString());
                            txtCantidadStock.Focus();
                        }
                    }
                }
            }

            cantidadRegistrosAux -= 1;
            if (cantidadRegistrosAux > 1)
            {
                lbCantidadFiltro.Text = $"{cantidadRegistrosAux} de {cantidadRegistros}";
            }
            else
            {
                lbCantidadFiltro.Text = $"1 de {cantidadRegistros}";
            }

            //Se elimina el ultimo item ingresado a la lista ya que es uno que se repire
            if (!id.Count.Equals(0) && cantidadRegistrosAux >= 0)
            {
                id.RemoveAt(id.Count - 1);
            }

            //Se Elimina el ultimo item de la lista idDeProductos para validar el boton anterior
            if (!idDeProductos.Count.Equals(0) && cantidadRegistrosAux >= 0)
            {
                idDeProductos.RemoveAt(idDeProductos.Count - 1);
            }

            //if (mensajeNoHayAnterior == true)
            //{
            //    MessageBox.Show("No hay mas productos anteriores", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            if (cantidadRegistrosAux < 1)
            {
                cantidadRegistrosAux = 1;
                //lbCantidadFiltro.Text = $"{cantidadRegistros} de {cantidadRegistros}";
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
                    idProducto = Convert.ToInt32(BusquedaRevisionInventario.id);
                }
            };
            busquedaR.ShowDialog();
            txtCantidadStock.Focus();
        }

        private void llenarCampos(string codigo)
        {
            using (var traerDatos = cn.CargarDatos($"SELECT Nombre, CodigoBarras, Precio, StockMinimo, StockNecesario, Stock FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codigo}' AND (CodigoBarras != '' OR ClaveInterna != '')"))
            {
                if (!traerDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow dato in traerDatos.Rows)
                    {
                        txtBoxBuscarCodigoBarras.Text = dato["CodigoBarras"].ToString();
                        txtNombreProducto.Text = dato["Nombre"].ToString();
                        txtCodigoBarras.Text = dato["CodigoBarras"].ToString();
                        lblPrecioProducto.Text = dato["Precio"].ToString();
                        lblStockMinimo.Text = dato["StockMinimo"].ToString();
                        lblStockMaximo.Text = dato["StockNecesario"].ToString();
                        txtCantidadStock.Text = Utilidades.RemoverCeroStock(dato["Stock"].ToString());
                    }
                }
            }
        }

        private void btnOmitir_Click(object sender, EventArgs e)
        {
            //Lo ponemos en true para que no haga toda la funcionalidad del boton siguiente
            botonOmitir = true;

            //Agregamos el id del producto a una lista temporal para cuando sse haca el decremento en el boton anterior
            idDeProductos.Add(idProducto);

            //Ejecutamos el evento click de el bonton siguiente
            btnSiguiente.PerformClick();

            //ponemos la variable en false para que despues no de problemas con dar click en el boton siguiente
            botonOmitir = false;


            //txtBoxBuscarCodigoBarras.Text = string.Empty;
            //txtNombreProducto.Text = string.Empty;
            //txtCodigoBarras.Text = string.Empty;
            //lblPrecioProducto.Text = string.Empty;
            //lblStockMinimo.Text = string.Empty;
            //lblStockMaximo.Text = string.Empty;
            //txtCantidadStock.Text = string.Empty;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
