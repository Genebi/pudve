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


        public RevisarInventario()
        {
            InitializeComponent();
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            var datosInventario = mb.DatosRevisionInventario();

            // Si existe un registro en la tabla obtiene los datos de lo contrario hace un insert para
            // que exista la configuracion necesaria
            if (datosInventario.Length > 0)
            {
                cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET FechaInventario = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE IDUsuario = {FormPrincipal.userID}");

                datosInventario = mb.DatosRevisionInventario();
                fechaInventario = datosInventario[0];
                numeroRevision = datosInventario[1];
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, FechaInventario, NoRevision) VALUES ('{FormPrincipal.userID}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '1')");

                datosInventario = mb.DatosRevisionInventario();
                fechaInventario = datosInventario[0];
                numeroRevision = datosInventario[1];
            }

            lblNoRevision.Text = numeroRevision;

            // Asignamos el numero de revision para que cargue los productos en el reporte al cerrar el form
            Inventario.NumRevActivo = Convert.ToInt32(numeroRevision);

            nombrePC = Environment.MachineName;
        }

        private void buscarCodigoBarras()
        {
            if (txtBoxBuscarCodigoBarras.Text != string.Empty)
            {
                var codigo = txtBoxBuscarCodigoBarras.Text;

                // Verifica si el codigo existe en algun producto y si pertenece al usuario
                // Si existe se trae la informacion del producto
                var infoProducto = mb.BuscarCodigoInventario(codigo);

                if (infoProducto.Length > 0)
                {
                    lblNombreProducto.Text = infoProducto[0];

                    if (string.IsNullOrEmpty(infoProducto[3]))
                    {
                        lblCodigoDeBarras.Text = codigo;
                    }
                    else
                    {
                        lblCodigoDeBarras.Text = infoProducto[3];
                    }

                    lblPrecioProducto.Text = infoProducto[2];

                    idProducto = Convert.ToInt32(infoProducto[5]);

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
                                var respuesta = MessageBox.Show("Este producto ya fue inventariado\nFecha: "+ infoInventariado[2] +" \n\n¿Desea modificarlo?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (respuesta == DialogResult.Yes)
                                {
                                    // Se asigna el stock registrado en la tabla RevisarInventario
                                    txtCantidadStock.Text = infoInventariado[0];
                                }

                                if (respuesta == DialogResult.No)
                                {
                                    LimpiarCampos();
                                    txtBoxBuscarCodigoBarras.Focus();
                                    return;
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

                        // Verificar si es un servicio o paquete y mostrar los productos relacionados
                        if (infoProducto[6] == "S")
                        {
                            MessageBox.Show($"El código de barras pertenece a un SERVICIO\n\n{nombreProductos}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        if (infoProducto[6] == "PQ")
                        {
                            MessageBox.Show($"El código de barras pertenece a un PAQUETE\n\n{nombreProductos}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        LimpiarCampos();
                        txtBoxBuscarCodigoBarras.Focus();
                    }
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
            if (!string.IsNullOrWhiteSpace(txtBoxBuscarCodigoBarras.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                {
                    var cantidadStock = long.Parse(txtCantidadStock.Text);

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

                    // Si ya fue inventariado el producto actualizamos informacion
                    if (existe)
                    {
                        var info = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                        var datosProducto = mb.DatosProductoInventariado(idProducto);

                        var stockFisico = txtCantidadStock.Text;
                        var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var diferencia = Convert.ToInt32(datosProducto[1]) - Convert.ToInt32(stockFisico);

                        // Actualizar datos en RevisarInventario
                        cn.EjecutarConsulta($"UPDATE RevisarInventario SET StockAlmacen = '{info[4]}', StockFisico = '{stockFisico}', Fecha = '{fecha}', Diferencia = '{diferencia}' WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}'");

                        // Actualizar stock del producto
                        cn.EjecutarConsulta($"UPDATE Productos SET Stock = '{stockFisico}' WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        LimpiarCampos();
                        txtBoxBuscarCodigoBarras.Focus();
                    }
                    else
                    {
                        var info = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                        var stockFisico = txtCantidadStock.Text;
                        var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        var diferencia = Convert.ToInt32(info[4]) - Convert.ToInt32(stockFisico);

                        var datos = new string[]
                        {
                            idProducto.ToString(), info[1], info[6], info[7], info[4], stockFisico, numeroRevision,
                            fecha, "0", diferencia.ToString(), FormPrincipal.userID.ToString(), info[5], "0", "1", info[2],
                            nombrePC
                        };

                        // Guardamos la informacion en la tabla de RevisarInventario
                        cn.EjecutarConsulta(cs.GuardarRevisarInventario(datos));

                        // Actualizar stock del producto
                        cn.EjecutarConsulta($"UPDATE Productos SET Stock = '{stockFisico}' WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        LimpiarCampos();
                        txtBoxBuscarCodigoBarras.Focus();
                    }
                }
            }
        }

        private void txtCantidadStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para obligar a que sólo se introduzcan números
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))  // permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;   // el resto de teclas pulsadas se desactivan
                }
            }
        }

        private void txtCantidadStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtBoxBuscarCodigoBarras.Text))
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
            var cantidad = 0;

            if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                cantidad = Convert.ToInt32(txtCantidadStock.Text);
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
            var cantidad = 0;

            if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                cantidad = Convert.ToInt32(txtCantidadStock.Text);
            }

            cantidad += 1;

            txtCantidadStock.Text = cantidad.ToString();
            txtCantidadStock.Focus();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            // Guardamos los dos Datos de las variables del sistema
            //Properties.Settings.Default.InicioFinInventario = 2;
            //Properties.Settings.Default.Save();

            // Actualizar el numero de revision despues de haber terminado el inventario
            var numeroRevisionTmp = Convert.ToInt32(numeroRevision) + 1;

            cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET NoRevision = {numeroRevisionTmp} WHERE IDUsuario = {FormPrincipal.userID}");

            // Cambiamos el valor de la variable para eliminar los registros de la tabla RevisarInventario con el numero de revision
            Inventario.limpiarTabla = true;

            this.Hide();
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtBoxBuscarCodigoBarras.Text = string.Empty;
            txtCantidadStock.Text = string.Empty;
            lblNombreProducto.Text = string.Empty;
            lblCodigoDeBarras.Text = string.Empty;
            lblPrecioProducto.Text = string.Empty;
        }
    }
}
