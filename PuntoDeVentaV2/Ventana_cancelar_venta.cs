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
    public partial class Ventana_cancelar_venta : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public int venta_cancelada = 0;


        public Ventana_cancelar_venta()
        {
            InitializeComponent();
            txt_folio_nota.Focus();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string numero_folio = txt_folio_nota.Text;

            CancelarVenta(numero_folio);
        }

        private void CancelarVenta(string numFolio)
        {
            var folio = numFolio.Trim();

            if (!string.IsNullOrWhiteSpace(folio))
            {
                var consulta = $"SELECT ID FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Folio = {folio} AND Status = 1";
                // Verificar y obtener ID de la venta utilizando el folio
                var existe = (bool)cn.EjecutarSelect(consulta);

                if (existe)
                {
                    var respuesta = MessageBox.Show("¿Desea cancelar la venta?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        txt_folio_nota.Enabled = false;
                        btn_aceptar.Enabled = false;

                        var idVenta = Convert.ToInt32(cn.EjecutarSelect(consulta, 1));
                        Ventas.mostrarVenta = idVenta;

                        // Cancelar la venta
                        var resultado = cn.EjecutarConsulta(cs.ActualizarVenta(idVenta, 3, FormPrincipal.userID));

                        if (resultado > 0)
                        {
                            // Regresar la cantidad de producto vendido al stock
                            var productos = cn.ObtenerProductosVenta(idVenta);

                            if (productos.Length > 0)
                            {
                                foreach (var producto in productos)
                                {
                                    var info = producto.Split('|');
                                    var idProducto = info[0];
                                    var cantidad = float.Parse(info[2]);

                                    cn.EjecutarConsulta($"UPDATE Productos SET Stock =  Stock + {cantidad} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");
                                }
                            }

                            // Agregamos marca de agua al PDF del ticket de la venta cancelada
                            Utilidades.CrearMarcaDeAgua(idVenta, "CANCELADA");

                            var mensaje = MessageBox.Show("¿Desea devolver el dinero?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (mensaje == DialogResult.Yes)
                            {
                                var formasPago = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);

                                // Operacion para que la devolucion del dinero afecte al apartado Caja
                                if (formasPago.Length > 0)
                                {
                                    var total = formasPago.Sum().ToString();
                                    var efectivo = formasPago[0].ToString();
                                    var tarjeta = formasPago[1].ToString();
                                    var vales = formasPago[2].ToString();
                                    var cheque = formasPago[3].ToString();
                                    var transferencia = formasPago[4].ToString();
                                    var credito = formasPago[5].ToString();
                                    var anticipo = "0";

                                    var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    var concepto = $"DEVOLUCION DINERO VENTA CANCELADA ID {idVenta}";

                                    string[] datos = new string[] {
                                            "retiro", total, "0", concepto, fechaOperacion, FormPrincipal.userID.ToString(),
                                            efectivo, tarjeta, vales, cheque, transferencia, credito, anticipo
                                        };

                                    cn.EjecutarConsulta(cs.OperacionCaja(datos));
                                }
                            }

                            venta_cancelada = 1;
                        }
                        
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Folio no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void solo_digitos(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }
    }
}
