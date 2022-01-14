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
    public partial class AgregarDescuentoProducto : Form
    {
        Conexion cn = new Conexion();
        double precioProducto = Convert.ToDouble(AgregarEditarProducto.precioProducto);
        //1 = por cliente
        //2 = por mayoreo
        int tipoDescuento = 1;
        //Para el ID de los controles generados dinamicamente
        int idGenerado = 2;
        //Guarda la cantidad del rango inicial del descuento por mayoreo
        string rangoInicial = null;

        int vecesMostradas = 0;

        private bool refrescarForm = true;

        public AgregarDescuentoProducto()
        {
            InitializeComponent();

            this.ControlBox = false;

            //if (tipoDescuento == 1)
            //{
            //    txtTituloDescuento.Text = "Descuento por Producto";
            //    rbCliente.Checked = true;
            //}

            obtenerTipoDescuento();
        }

        private void AgregarDescuentoProducto_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            obtenerTipoDescuento();

            if (tipoDescuento == 1)
            {
                txtTituloDescuento.Text = "Descuento por Producto";
                rbCliente.Checked = true;
            }

            if (AgregarEditarProducto.DatosSourceFinal == 2)
            {
                if (AgregarEditarProducto.SearchDesCliente.Rows.Count > 0)
                {
                    rbCliente.Checked = true;
                }

                if (AgregarEditarProducto.SearchDesMayoreo.Rows.Count > 0)
                {
                    rbMayoreo.Checked = true;
                }
            }

            CargarFormularios(tipoDescuento);

            //if (FormPrincipal.userNickName.Equals("HOUSEDEPOTAUTLAN") || FormPrincipal.userNickName.Equals("HOUSEDEPOTREPARTO") || FormPrincipal.userNickName.Equals("OXXITO") || FormPrincipal.userNickName.Equals("CLARAZV1"))
            //{
            //    rbMayoreo.Visible = true;
            //}
            //else
            //{
            //    rbMayoreo.Visible = false;
            //}
        }

        private void obtenerTipoDescuento()
        {
            if (AgregarEditarProducto.DatosSourceFinal.Equals(1) ||
                AgregarEditarProducto.DatosSourceFinal.Equals(3) ||
                AgregarEditarProducto.DatosSourceFinal.Equals(5))
            {
                tipoDescuento = 1;
                rbCliente.Checked = true;
            }
            else if (AgregarEditarProducto.DatosSourceFinal.Equals(2))
            {
                if (AgregarEditarProducto.SearchDesCliente.Rows.Count > 0)
                {
                    tipoDescuento = 1;
                    rbCliente.Checked = true;
                }
                else if (AgregarEditarProducto.SearchDesMayoreo.Rows.Count > 0)
                {
                    tipoDescuento = 2;
                    rbMayoreo.Checked = true;
                }
                else
                {
                    tipoDescuento = 1;
                    rbCliente.Checked = true;
                }
            }
        }

        private void btnCancelarDesc_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAceptarDesc_Click(object sender, EventArgs e)
        {
            //Producto
            if (tipoDescuento == 1)
            {
                AgregarEditarProducto.descuentos.Clear();

                TextBox precio = (TextBox)this.Controls.Find("txtPrecio", true).FirstOrDefault();
                TextBox porcentaje = (TextBox)this.Controls.Find("txtPorcentaje", true).FirstOrDefault();
                TextBox precioDesc = (TextBox)this.Controls.Find("txtPrecioDescuento", true).FirstOrDefault();
                TextBox descuento = (TextBox)this.Controls.Find("txtDescuento", true).FirstOrDefault();

                if (porcentaje.Text.Equals("") && precioDesc.Text.Equals("") && descuento.Text.Equals(""))
                {
                    btnCancelarDesc.PerformClick();
                }
                else
                {
                    AgregarEditarProducto.descuentos.Add(tipoDescuento.ToString());
                    AgregarEditarProducto.descuentos.Add(precio.Text);
                    AgregarEditarProducto.descuentos.Add(porcentaje.Text);
                    AgregarEditarProducto.descuentos.Add(precioDesc.Text);
                    AgregarEditarProducto.descuentos.Add(descuento.Text);
                }
            }
            //Mayoreo
            if (tipoDescuento == 2)
            {
                int numeroDescuentosMayoreo = panelContenedor.Controls.Count;

                // Si tiene 2 descuentos por mayoreo en adelante
                if (numeroDescuentosMayoreo > 2)
                {
                    AgregarEditarProducto.descuentos.Clear();
                    AgregarEditarProducto.descuentos.Add(tipoDescuento.ToString());

                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        string descuentoMayoreo = string.Empty;

                        if (panel.Name == "panelMayoreoTitulos") { continue; }

                        foreach (Control item in panel.Controls)
                        {
                            if (item is TextBox)
                            {
                                var tb = item.Text;

                                // Validar precios para el descuento por rangos
                                string[] datosAux = item.Name.Split('_');

                                if (datosAux[1] == "3")
                                {
                                    if (string.IsNullOrWhiteSpace(tb))
                                    {
                                        refrescarForm = false;
                                        item.Focus();
                                        MessageBox.Show("Es necesario agregar todos los precios.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }


                                tb = tb == "" ? "N" : tb;

                                descuentoMayoreo += tb + "-";
                            }

                            if (item is CheckBox)
                            {
                                CheckBox cb = (CheckBox)item;

                                if (cb.Checked)
                                {
                                    descuentoMayoreo += "0";
                                }
                                else
                                {
                                    descuentoMayoreo += "1";
                                }
                            }
                        }

                        string cadenaDescuentoMayoreo = descuentoMayoreo;
                        string cadenaParaBuscar = "N";
                        bool cadenaEncontrada = cadenaDescuentoMayoreo.Contains(cadenaParaBuscar);

                        if (cadenaEncontrada)
                        {
                            AgregarEditarProducto.descuentos.Add(descuentoMayoreo);
                            btnCancelarDesc.PerformClick();
                        }
                        else if (!cadenaEncontrada)
                        {
                            AgregarEditarProducto.descuentos.Add(descuentoMayoreo);
                        }

                        descuentoMayoreo = string.Empty;
                    }
                }
                else
                {
                    refrescarForm = false;
                    MessageBox.Show("Es necesario agregar minímo 2 descuentos a mayoreo.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            refrescarForm = true;
            this.Hide();
        }

        public void cargarNvoDescuentos()
        {
            if (rbCliente.Checked == true)
            {
                FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                panelHijo.Name = "panelGeneradoCliente";
                panelHijo.Width = 725;
                panelHijo.Height = 250;

                Label lb1 = new Label();
                lb1.Text = "Precio de producto";
                lb1.Margin = new Padding(270, 10, 0, 0);
                lb1.Font = new Font("Century Gothic", 11);
                lb1.AutoSize = false;
                lb1.Width = 220;
                lb1.Height = 20;
                lb1.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb1 = new TextBox();
                tb1.Name = "txtPrecio";
                tb1.Width = 220;
                tb1.Height = 20;
                tb1.Margin = new Padding(270, 5, 0, 0);
                tb1.TextAlign = HorizontalAlignment.Center;
                tb1.Enabled = false;
                tb1.BackColor = Color.White;
                tb1.Text = precioProducto.ToString("0.00");

                Label lb2 = new Label();
                lb2.Text = "% de Descuento";
                lb2.AutoSize = false;
                lb2.Width = 220;
                lb2.Height = 20;
                lb2.Margin = new Padding(270, 20, 0, 0);
                lb2.Font = new Font("Century Gothic", 11);
                lb2.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb2 = new TextBox();
                tb2.Name = "txtPorcentaje";
                tb2.Width = 220;
                tb2.Height = 20;
                tb2.Margin = new Padding(270, 5, 0, 0);
                tb2.TextAlign = HorizontalAlignment.Center;
                tb2.KeyPress += new KeyPressEventHandler(soloDecimales);
                tb2.KeyUp += new KeyEventHandler(calculoDescuento);

                Label lb3 = new Label();
                lb3.Text = "Precio con Descuento";
                lb3.AutoSize = false;
                lb3.Width = 220;
                lb3.Height = 20;
                lb3.Margin = new Padding(270, 20, 0, 0);
                lb3.Font = new Font("Century Gothic", 11);
                lb3.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb3 = new TextBox();
                tb3.Name = "txtPrecioDescuento";
                tb3.Width = 220;
                tb3.Height = 20;
                tb3.Margin = new Padding(270, 5, 0, 0);
                tb3.TextAlign = HorizontalAlignment.Center;
                tb3.Enabled = false;
                tb3.BackColor = Color.White;

                Label lb4 = new Label();
                lb4.Text = "Descuento";
                lb4.AutoSize = false;
                lb4.Width = 220;
                lb4.Height = 20;
                lb4.Margin = new Padding(270, 20, 0, 0);
                lb4.Font = new Font("Century Gothic", 11);
                lb4.TextAlign = ContentAlignment.MiddleCenter;

                TextBox tb4 = new TextBox();
                tb4.Name = "txtDescuento";
                tb4.Width = 220;
                tb4.Height = 20;
                tb4.Margin = new Padding(270, 5, 0, 0);
                tb4.TextAlign = HorizontalAlignment.Center;
                tb4.Enabled = false;
                tb4.BackColor = Color.White;

                panelHijo.Controls.Add(lb1);
                panelHijo.Controls.Add(tb1);
                panelHijo.Controls.Add(lb2);
                panelHijo.Controls.Add(tb2);
                panelHijo.Controls.Add(lb3);
                panelHijo.Controls.Add(tb3);
                panelHijo.Controls.Add(lb4);
                panelHijo.Controls.Add(tb4);

                panelHijo.FlowDirection = FlowDirection.TopDown;

                panelContenedor.Controls.Add(panelHijo);
                panelContenedor.FlowDirection = FlowDirection.TopDown;
            }
            if (rbMayoreo.Checked == true)
            {
                FlowLayoutPanel panelHijo1 = new FlowLayoutPanel();
                panelHijo1.Name = "panelMayoreoTitulos";
                panelHijo1.Width = 725;
                panelHijo1.Height = 50;

                Label lb1 = new Label();
                lb1.Text = "Rango de Productos";
                lb1.AutoSize = false;
                lb1.Width = 220;
                lb1.Height = 20;
                lb1.Margin = new Padding(150, 20, 0, 0);
                lb1.Font = new Font("Century Gothic", 11);
                lb1.TextAlign = ContentAlignment.MiddleCenter;

                Label lb2 = new Label();
                lb2.Text = "Precios";
                lb2.AutoSize = false;
                lb2.Width = 220;
                lb2.Height = 20;
                lb2.Margin = new Padding(30, 20, 0, 0);
                lb2.Font = new Font("Century Gothic", 11);
                lb2.TextAlign = ContentAlignment.MiddleCenter;

                FlowLayoutPanel panelHijo2 = new FlowLayoutPanel();
                panelHijo2.Name = "panelMayoreo1";
                panelHijo2.Width = 725;
                panelHijo2.Height = 50;

                TextBox tb1 = new TextBox();
                tb1.Name = "tbMayoreo1_1";
                tb1.Width = 100;
                tb1.Height = 20;
                tb1.Margin = new Padding(120, 5, 0, 0);
                tb1.TextAlign = HorizontalAlignment.Center;
                tb1.Text = "1";
                tb1.ReadOnly = true;
                tb1.BackColor = Color.White;

                TextBox tb2 = new TextBox();
                tb2.Name = "tbMayoreo1_2";
                tb2.Width = 100;
                tb2.Height = 20;
                tb2.Margin = new Padding(50, 5, 0, 0);
                tb2.TextAlign = HorizontalAlignment.Center;
                tb2.KeyUp += new KeyEventHandler(rangoProductosTB);

                TextBox tb3 = new TextBox();
                tb3.Name = "tbMayoreo1_3";
                tb3.Width = 100;
                tb3.Height = 20;
                tb3.Margin = new Padding(95, 5, 0, 0);
                tb3.TextAlign = HorizontalAlignment.Center;
                tb3.Text = precioProducto.ToString("0.00");
                tb3.ReadOnly = true;
                tb3.BackColor = Color.White;

                Button btAgregar = new Button();
                btAgregar.Cursor = Cursors.Hand;
                btAgregar.Text = "+";
                btAgregar.Name = $"btnAgregarD1";
                btAgregar.Width = 20;
                btAgregar.Height = 20;
                btAgregar.BackColor = ColorTranslator.FromHtml("#4CAC18");
                btAgregar.ForeColor = ColorTranslator.FromHtml("white");
                btAgregar.FlatStyle = FlatStyle.Flat;
                btAgregar.Click += new EventHandler(AgregarLineaDescuento);
                btAgregar.Margin = new Padding(5, 5, 0, 0);

                CheckBox cb1 = new CheckBox();
                cb1.Name = "checkMayoreo1";
                cb1.Text = $"Las primeras siempre costarán {precioProducto.ToString("0.00")}";
                cb1.Margin = new Padding(120, 5, 0, 0);
                cb1.TextAlign = ContentAlignment.MiddleLeft;
                cb1.CheckedChanged += seleccionCheckBoxes;
                cb1.Checked = true;
                cb1.Width = 400;
                cb1.Tag = 1;

                panelHijo1.Controls.Add(lb1);
                panelHijo1.Controls.Add(lb2);
                panelHijo2.Controls.Add(tb1);
                panelHijo2.Controls.Add(tb2);
                panelHijo2.Controls.Add(tb3);
                panelHijo2.Controls.Add(btAgregar);
                panelHijo2.SetFlowBreak(btAgregar, true);
                panelHijo2.Controls.Add(cb1);

                panelHijo1.FlowDirection = FlowDirection.LeftToRight;
                panelHijo2.FlowDirection = FlowDirection.LeftToRight;

                panelContenedor.Controls.Add(panelHijo1);
                panelContenedor.Controls.Add(panelHijo2);

                panelContenedor.FlowDirection = FlowDirection.TopDown;
            }
        }

        public void cargarDescuentos()
        {
            if (tipoDescuento.Equals(1))
            {
                if (AgregarEditarProducto.SearchDesCliente.Rows.Count > 0)
                {
                    foreach (DataRow renglon in AgregarEditarProducto.SearchDesCliente.Rows)
                    {
                        FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                        panelHijo.Name = "panelGeneradoCliente";
                        panelHijo.Width = 725;
                        panelHijo.Height = 250;

                        Label lb1 = new Label();
                        lb1.Text = "Precio de producto";
                        lb1.Margin = new Padding(270, 10, 0, 0);
                        lb1.Font = new Font("Century Gothic", 11);
                        lb1.AutoSize = false;
                        lb1.Width = 220;
                        lb1.Height = 20;
                        lb1.TextAlign = ContentAlignment.MiddleCenter;

                        TextBox tb1 = new TextBox();
                        tb1.Name = "txtPrecio";
                        tb1.Width = 220;
                        tb1.Height = 20;
                        tb1.Margin = new Padding(270, 5, 0, 0);
                        tb1.TextAlign = HorizontalAlignment.Center;
                        tb1.Enabled = false;
                        tb1.BackColor = Color.White;
                        tb1.Text = precioProducto.ToString("0.00");

                        Label lb2 = new Label();
                        lb2.Text = "% de Descuento";
                        lb2.AutoSize = false;
                        lb2.Width = 220;
                        lb2.Height = 20;
                        lb2.Margin = new Padding(270, 20, 0, 0);
                        lb2.Font = new Font("Century Gothic", 11);
                        lb2.TextAlign = ContentAlignment.MiddleCenter;

                        TextBox tb2 = new TextBox();
                        tb2.Name = "txtPorcentaje";
                        tb2.Width = 220;
                        tb2.Height = 20;
                        tb2.Margin = new Padding(270, 5, 0, 0);
                        tb2.TextAlign = HorizontalAlignment.Center;
                        tb2.Leave += new EventHandler(borrarTextoMensaje);
                        tb2.KeyPress += new KeyPressEventHandler(soloDecimales);
                        tb2.KeyUp += new KeyEventHandler(calculoDescuento);
                        tb2.Text = renglon[2].ToString();

                        Label lb3 = new Label();
                        lb3.Text = "Precio con Descuento";
                        lb3.AutoSize = false;
                        lb3.Width = 220;
                        lb3.Height = 20;
                        lb3.Margin = new Padding(270, 20, 0, 0);
                        lb3.Font = new Font("Century Gothic", 11);
                        lb3.TextAlign = ContentAlignment.MiddleCenter;

                        TextBox tb3 = new TextBox();
                        tb3.Name = "txtPrecioDescuento";
                        tb3.Width = 220;
                        tb3.Height = 20;
                        tb3.Margin = new Padding(270, 5, 0, 0);
                        tb3.TextAlign = HorizontalAlignment.Center;
                        tb3.Enabled = false;
                        tb3.BackColor = Color.White;
                        tb3.Text = renglon[3].ToString();

                        Label lb4 = new Label();
                        lb4.Text = "Descuento";
                        lb4.AutoSize = false;
                        lb4.Width = 220;
                        lb4.Height = 20;
                        lb4.Margin = new Padding(270, 20, 0, 0);
                        lb4.Font = new Font("Century Gothic", 11);
                        lb4.TextAlign = ContentAlignment.MiddleCenter;

                        TextBox tb4 = new TextBox();
                        tb4.Name = "txtDescuento";
                        tb4.Width = 220;
                        tb4.Height = 20;
                        tb4.Margin = new Padding(270, 5, 0, 0);
                        tb4.TextAlign = HorizontalAlignment.Center;
                        tb4.Enabled = false;
                        tb4.BackColor = Color.White;
                        tb4.Text = renglon[4].ToString();

                        panelHijo.Controls.Add(lb1);
                        panelHijo.Controls.Add(tb1);
                        panelHijo.Controls.Add(lb2);
                        panelHijo.Controls.Add(tb2);
                        panelHijo.Controls.Add(lb3);
                        panelHijo.Controls.Add(tb3);
                        panelHijo.Controls.Add(lb4);
                        panelHijo.Controls.Add(tb4);

                        panelHijo.FlowDirection = FlowDirection.TopDown;

                        panelContenedor.Controls.Add(panelHijo);
                        panelContenedor.FlowDirection = FlowDirection.TopDown;
                    }
                }
                if (AgregarEditarProducto.SearchDesCliente.Rows.Count == 0)
                {
                    FlowLayoutPanel panelHijo = new FlowLayoutPanel();
                    panelHijo.Name = "panelGeneradoCliente";
                    panelHijo.Width = 725;
                    panelHijo.Height = 250;

                    Label lb1 = new Label();
                    lb1.Text = "Precio de producto";
                    lb1.Margin = new Padding(270, 10, 0, 0);
                    lb1.Font = new Font("Century Gothic", 11);
                    lb1.AutoSize = false;
                    lb1.Width = 220;
                    lb1.Height = 20;
                    lb1.TextAlign = ContentAlignment.MiddleCenter;

                    TextBox tb1 = new TextBox();
                    tb1.Name = "txtPrecio";
                    tb1.Width = 220;
                    tb1.Height = 20;
                    tb1.Margin = new Padding(270, 5, 0, 0);
                    tb1.TextAlign = HorizontalAlignment.Center;
                    tb1.Enabled = false;
                    tb1.BackColor = Color.White;
                    tb1.Text = precioProducto.ToString("0.00");

                    Label lb2 = new Label();
                    lb2.Text = "% de Descuento";
                    lb2.AutoSize = false;
                    lb2.Width = 220;
                    lb2.Height = 20;
                    lb2.Margin = new Padding(270, 20, 0, 0);
                    lb2.Font = new Font("Century Gothic", 11);
                    lb2.TextAlign = ContentAlignment.MiddleCenter;

                    TextBox tb2 = new TextBox();
                    tb2.Name = "txtPorcentaje";
                    tb2.Width = 220;
                    tb2.Height = 20;
                    tb2.Margin = new Padding(270, 5, 0, 0);
                    tb2.TextAlign = HorizontalAlignment.Center;
                    tb2.Leave += new EventHandler(borrarTextoMensaje);
                    tb2.KeyPress += new KeyPressEventHandler(soloDecimales);
                    tb2.KeyUp += new KeyEventHandler(calculoDescuento);

                    Label lb3 = new Label();
                    lb3.Text = "Precio con Descuento";
                    lb3.AutoSize = false;
                    lb3.Width = 220;
                    lb3.Height = 20;
                    lb3.Margin = new Padding(270, 20, 0, 0);
                    lb3.Font = new Font("Century Gothic", 11);
                    lb3.TextAlign = ContentAlignment.MiddleCenter;

                    TextBox tb3 = new TextBox();
                    tb3.Name = "txtPrecioDescuento";
                    tb3.Width = 220;
                    tb3.Height = 20;
                    tb3.Margin = new Padding(270, 5, 0, 0);
                    tb3.TextAlign = HorizontalAlignment.Center;
                    tb3.Enabled = false;
                    tb3.BackColor = Color.White;

                    Label lb4 = new Label();
                    lb4.Text = "Descuento";
                    lb4.AutoSize = false;
                    lb4.Width = 220;
                    lb4.Height = 20;
                    lb4.Margin = new Padding(270, 20, 0, 0);
                    lb4.Font = new Font("Century Gothic", 11);
                    lb4.TextAlign = ContentAlignment.MiddleCenter;

                    TextBox tb4 = new TextBox();
                    tb4.Name = "txtDescuento";
                    tb4.Width = 220;
                    tb4.Height = 20;
                    tb4.Margin = new Padding(270, 5, 0, 0);
                    tb4.TextAlign = HorizontalAlignment.Center;
                    tb4.Enabled = false;
                    tb4.BackColor = Color.White;

                    panelHijo.Controls.Add(lb1);
                    panelHijo.Controls.Add(tb1);
                    panelHijo.Controls.Add(lb2);
                    panelHijo.Controls.Add(tb2);
                    panelHijo.Controls.Add(lb3);
                    panelHijo.Controls.Add(tb3);
                    panelHijo.Controls.Add(lb4);
                    panelHijo.Controls.Add(tb4);

                    panelHijo.FlowDirection = FlowDirection.TopDown;

                    panelContenedor.Controls.Add(panelHijo);
                    panelContenedor.FlowDirection = FlowDirection.TopDown;
                }
            }
            if (tipoDescuento.Equals(2))
            {
                if (AgregarEditarProducto.SearchDesMayoreo.Rows.Count > 0)
                {
                    idGenerado = 1;

                    FlowLayoutPanel panelHijo1 = new FlowLayoutPanel();
                    panelHijo1.Name = "panelMayoreoTitulos";
                    panelHijo1.Width = 725;
                    panelHijo1.Height = 50;

                    Label lb1 = new Label();
                    lb1.Text = "Rango de Productos";
                    lb1.AutoSize = false;
                    lb1.Width = 220;
                    lb1.Height = 20;
                    lb1.Margin = new Padding(150, 20, 0, 0);
                    lb1.Font = new Font("Century Gothic", 11);
                    lb1.TextAlign = ContentAlignment.MiddleCenter;

                    Label lb2 = new Label();
                    lb2.Text = "Precios";
                    lb2.AutoSize = false;
                    lb2.Width = 220;
                    lb2.Height = 20;
                    lb2.Margin = new Padding(30, 20, 0, 0);
                    lb2.Font = new Font("Century Gothic", 11);
                    lb2.TextAlign = ContentAlignment.MiddleCenter;

                    panelHijo1.Controls.Add(lb1);
                    panelHijo1.Controls.Add(lb2);

                    panelHijo1.FlowDirection = FlowDirection.LeftToRight;

                    panelContenedor.Controls.Add(panelHijo1);

                    int cantidadRangos = AgregarEditarProducto.SearchDesMayoreo.Rows.Count;

                    foreach (DataRow renglon in AgregarEditarProducto.SearchDesMayoreo.Rows)
                    {
                        bool habilitado = true;

                        if (cantidadRangos != idGenerado)
                        {
                            habilitado = false;
                        }

                        FlowLayoutPanel panelHijo2 = new FlowLayoutPanel();
                        panelHijo2.Name = $"panelMayoreo{idGenerado}";
                        panelHijo2.Width = 725;
                        panelHijo2.Height = 50;

                        TextBox tb1 = new TextBox();
                        tb1.Name = $"tbMayoreo{idGenerado}_1";
                        tb1.Width = 100;
                        tb1.Height = 20;
                        tb1.Margin = new Padding(120, 5, 0, 0);
                        tb1.TextAlign = HorizontalAlignment.Center;
                        tb1.Text = renglon[1].ToString();
                        tb1.ReadOnly = true;
                        tb1.BackColor = Color.White;

                        TextBox tb2 = new TextBox();
                        tb2.Name = $"tbMayoreo{idGenerado}_2";
                        tb2.Width = 100;
                        tb2.Height = 20;
                        tb2.Margin = new Padding(50, 5, 0, 0);
                        tb2.TextAlign = HorizontalAlignment.Center;
                        tb2.Leave += new EventHandler(borrarTextoMensaje);
                        tb2.KeyUp += new KeyEventHandler(rangoProductosTB);
                        tb2.Text = renglon[2].ToString() != "N" ? renglon[2].ToString() : "";
                        tb2.Enabled = habilitado;

                        Label lb = new Label();
                        lb.Text = "";
                        lb.Name = $"fraseMas{idGenerado}";
                        lb.Width = 40;
                        lb.TextAlign = ContentAlignment.MiddleLeft;

                        TextBox tb3 = new TextBox();
                        tb3.Name = $"tbMayoreo{idGenerado}_3";
                        tb3.Width = 100;
                        tb3.Height = 20;
                        tb3.Margin = new Padding(95, 5, 0, 0);
                        tb3.TextAlign = HorizontalAlignment.Center;
                        tb3.Text = precioProducto.ToString("0.00");

                        if (cantidadRangos != idGenerado)
                        {
                            tb3.ReadOnly = true;
                        }
                        else
                        {
                            lb.Text = "o más";
                        }

                        tb3.BackColor = Color.White;
                        tb3.Text = renglon[3].ToString();
                        tb3.Enabled = habilitado;

                        Button btAgregar = new Button();
                        btAgregar.Cursor = Cursors.Hand;
                        btAgregar.Text = "+";
                        btAgregar.Name = $"btnAgregarD{idGenerado}";
                        btAgregar.Width = 20;
                        btAgregar.Height = 20;
                        btAgregar.BackColor = ColorTranslator.FromHtml("#4CAC18");
                        btAgregar.ForeColor = ColorTranslator.FromHtml("white");
                        btAgregar.FlatStyle = FlatStyle.Flat;
                        btAgregar.Click += new EventHandler(AgregarLineaDescuento);
                        btAgregar.Margin = new Padding(5, 5, 0, 0);
                        btAgregar.Enabled = habilitado;

                        Button bt = new Button();
                        bt.Cursor = Cursors.Hand;
                        bt.Text = "X";
                        bt.Name = $"btnEliminarD{idGenerado}";
                        bt.Width = 20;
                        bt.Height = 20;
                        bt.BackColor = ColorTranslator.FromHtml("#C00000");
                        bt.ForeColor = ColorTranslator.FromHtml("white");
                        bt.FlatStyle = FlatStyle.Flat;
                        bt.Click += new EventHandler(eliminarDescuentos);
                        bt.Margin = new Padding(5, 5, 0, 0);
                        bt.Enabled = habilitado;

                        string textoCheckbox = string.Empty;

                        if (idGenerado == 1)
                        {
                            textoCheckbox = $"Las primeras {tb2.Text} siempre costarán {precioProducto.ToString("0.00")}";

                        } else if (idGenerado > 1)
                        {
                            if (string.IsNullOrWhiteSpace(tb2.Text))
                            {
                                textoCheckbox = $"De {tb1.Text} en adelante siempre costarán {tb3.Text}";
                            } 
                            else
                            {
                                textoCheckbox = $"De entre {tb1.Text} a {tb2.Text} siempre costarán {tb3.Text}";
                            }
                            
                        }

                        CheckBox cb1 = new CheckBox();
                        cb1.Name = $"checkMayoreo{idGenerado}";
                        cb1.Text = textoCheckbox;
                        cb1.Margin = new Padding(120, 5, 0, 0);
                        cb1.TextAlign = ContentAlignment.MiddleLeft;
                        cb1.CheckedChanged += seleccionCheckBoxes;
                        cb1.Checked = true;
                        cb1.Width = 400;
                        cb1.Tag = idGenerado;
                        cb1.Checked = renglon[4].ToString() == "0" ? true : false;

                        panelHijo2.Controls.Add(tb1);
                        panelHijo2.Controls.Add(tb2);
                        panelHijo2.Controls.Add(lb);
                        panelHijo2.Controls.Add(tb3);

                        // Condiciones para cuando carga el descuento por rango al editar
                        // al primer rango no debe de agregarle la opcion de eliminar (boton rojo)
                        if (idGenerado == 1)
                        {
                            panelHijo2.Controls.Add(btAgregar);
                            panelHijo2.SetFlowBreak(btAgregar, true);
                            panelHijo2.Controls.Add(cb1);
                        }

                        if (idGenerado > 1)
                        {
                            panelHijo2.Controls.Add(btAgregar);
                            panelHijo2.Controls.Add(bt);
                            panelHijo2.SetFlowBreak(bt, true);
                            panelHijo2.Controls.Add(cb1);
                        }
                        
                        panelHijo2.FlowDirection = FlowDirection.LeftToRight;
                        panelContenedor.Controls.Add(panelHijo2);
                        panelContenedor.FlowDirection = FlowDirection.TopDown;

                        idGenerado++;
                    }
                }
                if (AgregarEditarProducto.SearchDesMayoreo.Rows.Count == 0)
                {
                    FlowLayoutPanel panelHijo1 = new FlowLayoutPanel();
                    panelHijo1.Name = "panelMayoreoTitulos";
                    panelHijo1.Width = 725;
                    panelHijo1.Height = 50;

                    Label lb1 = new Label();
                    lb1.Text = "Rango de Productos";
                    lb1.AutoSize = false;
                    lb1.Width = 220;
                    lb1.Height = 20;
                    lb1.Margin = new Padding(150, 20, 0, 0);
                    lb1.Font = new Font("Century Gothic", 11);
                    lb1.TextAlign = ContentAlignment.MiddleCenter;

                    Label lb2 = new Label();
                    lb2.Text = "Precios";
                    lb2.AutoSize = false;
                    lb2.Width = 220;
                    lb2.Height = 20;
                    lb2.Margin = new Padding(30, 20, 0, 0);
                    lb2.Font = new Font("Century Gothic", 11);
                    lb2.TextAlign = ContentAlignment.MiddleCenter;

                    FlowLayoutPanel panelHijo2 = new FlowLayoutPanel();
                    panelHijo2.Name = "panelMayoreo1";
                    panelHijo2.Width = 725;
                    panelHijo2.Height = 50;

                    TextBox tb1 = new TextBox();
                    tb1.Name = "tbMayoreo1_1";
                    tb1.Width = 100;
                    tb1.Height = 20;
                    tb1.Margin = new Padding(120, 5, 0, 0);
                    tb1.TextAlign = HorizontalAlignment.Center;
                    tb1.Text = "1";
                    tb1.ReadOnly = true;
                    tb1.BackColor = Color.White;

                    TextBox tb2 = new TextBox();
                    tb2.Name = "tbMayoreo1_2";
                    tb2.Width = 100;
                    tb2.Height = 20;
                    tb2.Margin = new Padding(50, 5, 0, 0);
                    tb2.TextAlign = HorizontalAlignment.Center;
                    tb2.Leave += new EventHandler(borrarTextoMensaje);
                    tb2.KeyUp += new KeyEventHandler(rangoProductosTB);

                    TextBox tb3 = new TextBox();
                    tb3.Name = "tbMayoreo1_3";
                    tb3.Width = 100;
                    tb3.Height = 20;
                    tb3.Margin = new Padding(95, 5, 0, 0);
                    tb3.TextAlign = HorizontalAlignment.Center;
                    tb3.Text = precioProducto.ToString("0.00");
                    tb3.ReadOnly = true;
                    tb3.BackColor = Color.White;

                    Button btAgregar = new Button();
                    btAgregar.Cursor = Cursors.Hand;
                    btAgregar.Text = "+";
                    btAgregar.Name = $"btnAgregarD1";
                    btAgregar.Width = 20;
                    btAgregar.Height = 20;
                    btAgregar.BackColor = ColorTranslator.FromHtml("#4CAC18");
                    btAgregar.ForeColor = ColorTranslator.FromHtml("white");
                    btAgregar.FlatStyle = FlatStyle.Flat;
                    btAgregar.Click += new EventHandler(AgregarLineaDescuento);
                    btAgregar.Margin = new Padding(5, 5, 0, 0);

                    CheckBox cb1 = new CheckBox();
                    cb1.Name = "checkMayoreo1";
                    cb1.Text = $"Las primeras siempre costarán {precioProducto.ToString("0.00")}";
                    cb1.Margin = new Padding(120, 5, 0, 0);
                    cb1.TextAlign = ContentAlignment.MiddleLeft;
                    cb1.CheckedChanged += seleccionCheckBoxes;
                    cb1.Checked = true;
                    cb1.Width = 400;
                    cb1.Tag = 1;

                    panelHijo1.Controls.Add(lb1);
                    panelHijo1.Controls.Add(lb2);
                    panelHijo2.Controls.Add(tb1);
                    panelHijo2.Controls.Add(tb2);
                    panelHijo2.Controls.Add(tb3);
                    panelHijo2.Controls.Add(btAgregar);
                    panelHijo2.SetFlowBreak(btAgregar, true);
                    panelHijo2.Controls.Add(cb1);

                    panelHijo1.FlowDirection = FlowDirection.LeftToRight;
                    panelHijo2.FlowDirection = FlowDirection.LeftToRight;

                    panelContenedor.Controls.Add(panelHijo1);
                    panelContenedor.Controls.Add(panelHijo2);

                    panelContenedor.FlowDirection = FlowDirection.TopDown;
                }
            }
        }

        private void borrarTextoMensaje(object sender, EventArgs e)
        {
            TextBox tbPorcentaje = (TextBox)sender;

            if (!tbPorcentaje.Text.Equals(string.Empty))
            {
                lblMensaje.Text = string.Empty;
            }
        }

        private void CargarFormularios(int tipo)
        {
            panelContenedor.Controls.Clear();

            if (tipo.Equals(1))
            {
                if (AgregarEditarProducto.DatosSourceFinal == 1 ||
                    AgregarEditarProducto.DatosSourceFinal == 3 ||
                    AgregarEditarProducto.DatosSourceFinal == 4 ||
                    AgregarEditarProducto.DatosSourceFinal == 5)
                {
                    cargarNvoDescuentos();
                }
                else if (AgregarEditarProducto.DatosSourceFinal == 2)
                {
                    cargarDescuentos();
                }
            }
            else if (tipo.Equals(2))
            {
                if (AgregarEditarProducto.DatosSourceFinal == 1 ||
                    AgregarEditarProducto.DatosSourceFinal == 3 ||
                    AgregarEditarProducto.DatosSourceFinal == 4 ||
                    AgregarEditarProducto.DatosSourceFinal == 5)
                {
                    cargarNvoDescuentos();
                }
                else if (AgregarEditarProducto.DatosSourceFinal == 2)
                {
                    cargarDescuentos();
                }
            }
        }


        private void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (AgregarEditarProducto.DatosSourceFinal.Equals(2))
            {
                if (AgregarEditarProducto.SearchDesMayoreo.Rows.Count > 0)
                {
                    return;
                }
            }

            txtTituloDescuento.Text = "Descuento por Producto";
            tipoDescuento = 1;
            CargarFormularios(tipoDescuento);
        }

        private void rbMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            if (AgregarEditarProducto.DatosSourceFinal.Equals(2))
            {
                if (AgregarEditarProducto.SearchDesCliente.Rows.Count > 0)
                {
                    return;
                }
            }

            txtTituloDescuento.Text = "Descuento por Mayoreo";
            tipoDescuento = idGenerado = 2;
            CargarFormularios(tipoDescuento);
        }
        
        private void rangoProductosTB(object sender, KeyEventArgs e)
        {
            //Se hace para obtener el numero de linea al que pertenece el TextBox
            TextBox tb = sender as TextBox;
            string nombre = tb.Name.Replace("tbMayoreo", "");
            string[] tmp = nombre.Split('_');
            //Hace referencia al segundo TextBox
            TextBox tb1 = (TextBox)this.Controls.Find("tbMayoreo" + tmp[0] + "_2", true).FirstOrDefault();
            //Hace referencia al tercer TextBox
            TextBox tb2 = (TextBox)this.Controls.Find("tbMayoreo" + tmp[0] + "_3", true).FirstOrDefault();
            //Se cambia el mensaje del CheckBox
            CheckBox cb = (CheckBox)this.Controls.Find("checkMayoreo" + tmp[0], true).FirstOrDefault();

            if (tmp[0] == "1")
            {
                cb.Text = "Las primeras " + tb1.Text + " siempre costarán " + precioProducto.ToString("0.00");
            }
            else
            {
                cb.Text = $"De entre {(Convert.ToInt32(rangoInicial) + 1)} a {tb1.Text} siempre costarán {tb2.Text}";
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(tb1.Text)) return;

                if (tmp[0] != "1")
                {
                    int idTemp = Convert.ToInt32(tmp[0]);

                    TextBox tbPrecioAnterior = (TextBox)this.Controls.Find("tbMayoreo" + (idTemp - 1) + "_3", true).FirstOrDefault();

                    // Comparando precio nuevo con la linea anterior
                    if (float.Parse(tb2.Text.Trim()) >= float.Parse(tbPrecioAnterior.Text.Trim()))
                    {
                        refrescarForm = false;
                        MessageBox.Show("El precio nuevo no puede ser mayor o igual al precio anterior.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                

                tb1.Enabled = false;
                tb2.Enabled = false;
                tb1.BackColor = Color.White;
                tb2.BackColor = Color.White;

                if (idGenerado > 2)
                {
                    Button bt = (Button)this.Controls.Find("btnEliminarD" + tmp[0], true).FirstOrDefault();
                    bt.Enabled = false;

                    Label lbFrase = (Label)this.Controls.Find("fraseMas" + (idGenerado - 1), true).FirstOrDefault();
                    lbFrase.Text = "";
                }

                if (idGenerado > 1)
                {
                    Button bt = (Button)this.Controls.Find("btnAgregarD" + tmp[0], true).FirstOrDefault();
                    bt.Enabled = false;
                }

                rangoInicial = tb1.Text;

                generarLineaMayoreo();
            }
        }
        
        private void generarLineaMayoreo()
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = $"panelMayoreo{idGenerado}";
            panelHijo.Width = 725;
            panelHijo.Height = 50;

            TextBox tb1 = new TextBox();
            tb1.Name = $"tbMayoreo{idGenerado}_1";
            tb1.Width = 100;
            tb1.Height = 20;
            tb1.Margin = new Padding(120, 5, 0, 0);
            tb1.TextAlign = HorizontalAlignment.Center;
            tb1.Text = (Convert.ToInt32(rangoInicial) + 1).ToString();
            tb1.ReadOnly = true;
            tb1.BackColor = Color.White;

            TextBox tb2 = new TextBox();
            tb2.Name = $"tbMayoreo{idGenerado}_2";
            tb2.Width = 100;
            tb2.Height = 20;
            tb2.Margin = new Padding(50, 5, 0, 0);
            tb2.TextAlign = HorizontalAlignment.Center;
            tb2.KeyUp += new KeyEventHandler(rangoProductosTB);

            Label lb = new Label();
            lb.Text = "o más";
            lb.Name = $"fraseMas{idGenerado}";
            lb.Width = 40;
            lb.TextAlign = ContentAlignment.MiddleLeft;

            TextBox tb3 = new TextBox();
            tb3.Name = $"tbMayoreo{idGenerado}_3";
            tb3.Width = 100;
            tb3.Height = 20;
            tb3.Margin = new Padding(50, 5, 0, 0);
            tb3.TextAlign = HorizontalAlignment.Center;
            tb3.KeyUp += new KeyEventHandler(rangoProductosTB);

            Button btAgregar = new Button();
            btAgregar.Cursor = Cursors.Hand;
            btAgregar.Text = "+";
            btAgregar.Name = $"btnAgregarD{idGenerado}";
            btAgregar.Width = 20;
            btAgregar.Height = 20;
            btAgregar.BackColor = ColorTranslator.FromHtml("#4CAC18");
            btAgregar.ForeColor = ColorTranslator.FromHtml("white");
            btAgregar.FlatStyle = FlatStyle.Flat;
            btAgregar.Click += new EventHandler(AgregarLineaDescuento);
            btAgregar.Margin = new Padding(5, 5, 0, 0);

            Button bt = new Button();
            bt.Cursor = Cursors.Hand;
            bt.Text = "X";
            bt.Name = $"btnEliminarD{idGenerado}";
            bt.Width = 20;
            bt.Height = 20;
            bt.BackColor = ColorTranslator.FromHtml("#C00000");
            bt.ForeColor = ColorTranslator.FromHtml("white");
            bt.FlatStyle = FlatStyle.Flat;
            bt.Click += new EventHandler(eliminarDescuentos);
            bt.Margin = new Padding(5, 5, 0, 0);

            CheckBox cb1 = new CheckBox();
            cb1.Name = $"checkMayoreo{idGenerado}";
            cb1.Margin = new Padding(120, 5, 0, 0);
            cb1.TextAlign = ContentAlignment.MiddleLeft;
            cb1.CheckedChanged += seleccionCheckBoxes;
            cb1.Checked = true;
            cb1.Width = 400;
            cb1.Tag = idGenerado;

            panelHijo.Controls.Add(tb1);
            panelHijo.Controls.Add(tb2);
            panelHijo.Controls.Add(lb);
            panelHijo.Controls.Add(tb3);
            panelHijo.Controls.Add(btAgregar);
            panelHijo.Controls.Add(bt);
            panelHijo.SetFlowBreak(bt, true);
            panelHijo.Controls.Add(cb1);

            panelHijo.FlowDirection = FlowDirection.LeftToRight;
            panelContenedor.Controls.Add(panelHijo);
            panelContenedor.FlowDirection = FlowDirection.TopDown;

            tb2.Focus();

            idGenerado++;
        }

        private void soloDecimales(object sender, KeyPressEventArgs e)
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
                    e.Handled = true;
            }
        }

        //Este evento es principalmente para los descuentos por Cliente
        private void calculoDescuento(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Name == "txtPorcentaje")
            {
                TextBox tbDescuento = (TextBox)this.Controls.Find("txtDescuento", true).FirstOrDefault();
                TextBox tbPrecioDescuento = (TextBox)this.Controls.Find("txtPrecioDescuento", true).FirstOrDefault();
                var valorPorc = tb.Text;
                valorPorc = procesarPorcentaje(valorPorc);
                double porcentaje = Convert.ToDouble(valorPorc);
                if (porcentaje == 0) { tb.Text = ""; }
                double descuento = precioProducto * porcentaje;
                tbDescuento.Text = descuento.ToString("0.00");
                tbPrecioDescuento.Text = (precioProducto - descuento).ToString("0.00");
            }
        }

        private void eliminarDescuentos(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            var id = bt.Name.Replace("btnEliminarD", "");

            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                if (panel.Name == "panelMayoreo" + id)
                {
                    panelContenedor.Controls.Remove(panel);
                    idGenerado--;
                    var idTmp = Convert.ToInt16(id) - 1;

                    if (idGenerado > 1)
                    {
                        Button btAgregar = (Button)this.Controls.Find($"btnAgregarD{idTmp}", true).FirstOrDefault();
                        btAgregar.Enabled = true;
                    }

                    if (idGenerado > 2)
                    {
                        Button btTmp   = (Button)this.Controls.Find($"btnEliminarD{idTmp}", true).FirstOrDefault();
                        TextBox tbTmp1 = (TextBox)this.Controls.Find($"tbMayoreo{idTmp}_2", true).FirstOrDefault();
                        TextBox tbTmp2 = (TextBox)this.Controls.Find($"tbMayoreo{idTmp}_3", true).FirstOrDefault();
                        btTmp.Enabled  = true;
                        tbTmp1.Enabled = true;
                        tbTmp2.Enabled = true;
                        tbTmp2.ReadOnly = false;

                        Label lbFrase = (Label)this.Controls.Find("fraseMas" + idTmp, true).FirstOrDefault();
                        lbFrase.Text = "o más";
                    }
                    else
                    {
                        TextBox tbTmp1 = (TextBox)this.Controls.Find($"tbMayoreo{idTmp}_2", true).FirstOrDefault();
                        tbTmp1.Enabled = true;
                    }
                }
            }
        }

        private void AgregarLineaDescuento(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            var id = btn.Name.Replace("btnAgregarD", "");

            //Hace referencia al segundo TextBox
            TextBox tb1 = (TextBox)this.Controls.Find("tbMayoreo" + id + "_2", true).FirstOrDefault();
            //Hace referencia al tercer TextBox
            TextBox tb2 = (TextBox)this.Controls.Find("tbMayoreo" + id + "_3", true).FirstOrDefault();
            //Se cambia el mensaje del CheckBox
            CheckBox cb = (CheckBox)this.Controls.Find("checkMayoreo" + id, true).FirstOrDefault();


            if (string.IsNullOrWhiteSpace(tb1.Text))
            {
                refrescarForm = false;
                MessageBox.Show("Es necesario ingresar una cantidad para el rango.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (id == "1")
            {
                cb.Text = "Las primeras " + tb1.Text + " siempre costarán " + precioProducto.ToString("0.00");
            }
            else
            {
                int idTemp = Convert.ToInt32(id);

                TextBox tbPrecioAnterior = (TextBox)this.Controls.Find("tbMayoreo" + (idTemp - 1) + "_3", true).FirstOrDefault();

                // Comparando precio nuevo con la linea anterior
                if (float.Parse(tb2.Text.Trim()) >= float.Parse(tbPrecioAnterior.Text.Trim()))
                {
                    refrescarForm = false;
                    MessageBox.Show("El precio nuevo no puede ser mayor o igual al precio anterior.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                cb.Text = $"De entre {(Convert.ToInt32(rangoInicial) + 1)} a {tb1.Text} siempre costarán {tb2.Text}";
            }

            tb1.Enabled = false;
            tb2.Enabled = false;
            tb1.BackColor = Color.White;
            tb2.BackColor = Color.White;

            if (idGenerado > 2)
            {
                Button bt = (Button)this.Controls.Find("btnEliminarD" + id, true).FirstOrDefault();
                bt.Enabled = false;

                Label lbFrase = (Label)this.Controls.Find("fraseMas" + (idGenerado - 1), true).FirstOrDefault();
                lbFrase.Text = "";
            }

            if (idGenerado > 1)
            {
                Button bt = (Button)this.Controls.Find("btnAgregarD" + id, true).FirstOrDefault();
                bt.Enabled = false;
            }

            rangoInicial = tb1.Text;


            generarLineaMayoreo();
            
        }
        
        //Seleccion de CheckBoxes para el descuento por Mayoreo
        private void seleccionCheckBoxes(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            var id = Convert.ToInt32(cb.Name.Replace("checkMayoreo", ""));
            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (CheckBox cbm in panel.Controls.OfType<CheckBox>())
                {
                    if (cb.Checked)
                    {
                        if (Convert.ToInt16(cbm.Tag) <= id)
                        {
                            cbm.Checked = true;
                        }
                    }
                    else
                    {
                        cbm.Checked = false;
                    }
                }
            }
        }

        private string procesarPorcentaje(string porcentaje)
        {
            int longitud = porcentaje.Length;
            if (longitud > 1)
            {
                var porc = float.Parse(porcentaje);
                //Verifica si la cantidad ingresada es el 100 porciento o mas
                if (porc >= 100)
                {
                    return "0.00";
                }
                if (porcentaje.Contains('.'))
                {
                    string[] tmp = porcentaje.Split('.');
                    porcentaje = porcentaje.Replace(".", "");
                    if (tmp[0].Length > 1)
                    {
                        porcentaje = "0." + porcentaje;
                    }
                    else
                    {
                        porcentaje = "0.0" + porcentaje;
                    }
                }
                else
                {
                    porcentaje = "0." + porcentaje;
                }
            }
            else if (longitud == 1)
            {
                porcentaje = "0.0" + porcentaje;
            }
            else
            {
                porcentaje = "0.00";
            }
            return porcentaje;
        }

        private void btnEliminarDescuentos_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("¿Estás seguro de eliminar los descuentos?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                var idProducto = AgregarEditarProducto.idProductoFinal;

                if (!string.IsNullOrWhiteSpace(idProducto))
                {
                    cn.EjecutarConsulta($"DELETE FROM DescuentoCliente WHERE IDProducto = {idProducto}");
                    AgregarEditarProducto.SearchDesCliente.Clear();
                    cn.EjecutarConsulta($"DELETE FROM DescuentoMayoreo WHERE IDProducto = {idProducto}");
                    AgregarEditarProducto.SearchDesMayoreo.Clear();

                    panelContenedor.Controls.Clear();
                    cargarNvoDescuentos();

                    this.Hide();
                }
            }
        }

        private void AgregarDescuentoProducto_Activated(object sender, EventArgs e)
        {
            if (refrescarForm)
            {
                obtenerTipoDescuento();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (vecesMostradas < 5)
            {
                Random randomValue = new Random();
                int A = randomValue.Next(0, 255);
                int B = randomValue.Next(0, 255);
                int C = randomValue.Next(0, 255);

                lblMensaje.ForeColor = System.Drawing.Color.FromArgb(A, B, C);
                
                vecesMostradas++;
            }
        }

        private void rbMayoreo_Click(object sender, EventArgs e)
        {
            if (AgregarEditarProducto.DatosSourceFinal.Equals(2))
            {
                if (AgregarEditarProducto.SearchDesCliente.Rows.Count > 0)
                {
                    //lblMensaje.Text = "Este producto ya tiene asignado descuento por Producto desea cambiarlo";
                    MessageBox.Show("Este producto ya tiene asignado Descuento por Producto, si desea cambiarlo es necesario eliminar el descuento actual.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void rbCliente_Click(object sender, EventArgs e)
        {
            if (AgregarEditarProducto.DatosSourceFinal.Equals(2))
            {
                if (AgregarEditarProducto.SearchDesMayoreo.Rows.Count > 0)
                {
                    //lblMensaje.Text = "Este producto ya tiene asignado descuento por Mayoreo desea cambiarlo";
                    MessageBox.Show("Este producto ya tiene asignado Descuento por Mayoreo, si desea cambiarlo es necesario eliminar el descuento actual.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);   
                }
            }
        }
    }
}
