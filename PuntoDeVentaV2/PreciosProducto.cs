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
    public partial class PreciosProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        private int counter = 0;
        string saveDirectoryImg = string.Empty;
        int mas = 0;
        int numerosPrecios = 1;
        int ContadorDescuentos = 0;
        int ContadorDecuentos2 = 0;
        int ContadorDescuentos3 = 0;
        int ContadorDescuento4 = 0;
        int ContadorDescuento5 = 0;
        int Contadorescuetnos6 = 0;
        int ContadorDescuentos7 = 0;
        public PreciosProducto()
        {
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            lblTiempo.Text = counter.ToString();
        }

        private void PreciosProducto_Load(object sender, EventArgs e)
        {
            lblCodigoDeBarras.Text = ConsultaPrecio.CodigoDeBarras;
            TomarNombreCodigoDeBarrasYImagen();
            PreciosConDescuentoOSinDescuento();
        }
        

        private void PreciosConDescuentoOSinDescuento()
        {
            var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
            string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
            var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
            int tiempoTablas = ConsultaDescuantos.Rows.Count * 3;
            counter = tiempoTablas + 5;
            if (!ConsultaDescuantos.Rows.Count.Equals(0))
            {
                for (int i = 0; i < ConsultaDescuantos.Rows.Count && i <= 2; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel1.Controls.Add(lblprecio);
                    flowLayoutPanel1.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    ContadorDescuentos += 3;
                }
                if (ConsultaDescuantos.Rows.Count > 2)
                {
                    TimerProductos = new System.Windows.Forms.Timer();
                    TimerProductos.Tick += new EventHandler(TimerProductos_Tick);
                    TimerProductos.Interval = 1000; // 1 second
                    TimerProductos.Start();
                    lblTiempo.Text = counter.ToString();
                }
            }
            else
            {
                var consutaPrecio = cn.CargarDatos(cs.BuscarPrecioPorIDdelProducto(IDProducto));
                int precio =  Convert.ToInt32(consutaPrecio.Rows[0]["Precio"]);
                flowLayoutPanel1.Visible = false;
                lblPrecioSinDescuentos.Visible = true;
                lblPrecioSinDescuentos.Text = precio.ToString("C2");
            }
        }

        private void TomarNombreCodigoDeBarrasYImagen()
        {
            DataTable producto;
            using (producto = cn.CargarDatos(cs.BuscarProductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras)))
            {
                foreach (DataRow item in producto.Rows)
                {
                    lblNombre.Text = item["Nombre"].ToString();
                    decimal precio = Convert.ToDecimal(item["Precio"]);

                    var servidor = Properties.Settings.Default.Hosting;
                    string DirectorioImagen;
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        saveDirectoryImg = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\";
                        DirectorioImagen = $@"\\{servidor}\pudve\Productos\";
                    }
                    else
                    {
                        DirectorioImagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
                    }
                    string nombrIemagen = item["Nombre"].ToString();
                    var nombrefinal = nombrIemagen.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                    string directorfinal = DirectorioImagen + nombrefinal + ".jpg";

                    using (DataTable consultalogo = cn.CargarDatos(cs.buscarNombreLogoTipo(FormPrincipal.userID)))
                    {
                        string NombreLogo = consultalogo.Rows[0]["LogoTipo"].ToString();

                        if (File.Exists(directorfinal))
                        {
                            pictureBox1.Image = Image.FromFile(directorfinal);
                        }
                        else if (!NombreLogo.Equals(""))
                        {
                            pictureBox1.Image = Image.FromFile(saveDirectoryImg + NombreLogo);
                        }
                        else
                        {
                            pictureBox1.Visible = false;
                            lblImagen.Visible = true;
                        }
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                timer1.Stop();
                this.Close();
            }  
            lblTiempo.Text = counter.ToString();
        }

        private void TimerProductos_Tick(object sender, EventArgs e)
        {
            ContadorDescuentos--;
            if (ContadorDescuentos == 0)
            {
                TimerProductos.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 3; i < ConsultaDescuantos.Rows.Count && i <= 5; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel2.Controls.Add(lblprecio);
                    flowLayoutPanel1.Visible = false;
                    flowLayoutPanel2.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    ContadorDecuentos2 += 3;
                }
                if (ConsultaDescuantos.Rows.Count>5)
                {
                    timerProductos2 = new System.Windows.Forms.Timer();
                    timerProductos2.Tick += new EventHandler(timerProductos2_Tick);
                    timerProductos2.Interval = 1000; // 1 second
                    timerProductos2.Start();
                }
            }
        }

        private void timerProductos2_Tick(object sender, EventArgs e)
        {
            ContadorDecuentos2--;
            if (ContadorDecuentos2 == 0)
            {
                timerProductos2.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 6; i < ConsultaDescuantos.Rows.Count && i <= 8; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel3.Controls.Add(lblprecio);
                    flowLayoutPanel2.Visible = false;
                    flowLayoutPanel3.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    ContadorDescuentos3 += 3;
                }
                if (ConsultaDescuantos.Rows.Count > 8)
                {
                    timerProductos3 = new System.Windows.Forms.Timer();
                    timerProductos3.Tick += new EventHandler(timerProductos3_Tick);
                    timerProductos3.Interval = 1000; // 1 second
                    timerProductos3.Start();
                }
            }
        }

        private void timerProductos3_Tick(object sender, EventArgs e)
        {
            ContadorDescuentos3--;
            if (ContadorDescuentos3 == 0)
            {
                timerProductos3.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 9; i < ConsultaDescuantos.Rows.Count && i <= 11; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel4.Controls.Add(lblprecio);
                    flowLayoutPanel3.Visible = false;
                    flowLayoutPanel4.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    ContadorDescuento4 += 3;
                }
                if (ConsultaDescuantos.Rows.Count > 11)
                {
                    timerProdcutos4 = new System.Windows.Forms.Timer();
                    timerProdcutos4.Tick += new EventHandler(timerProdcutos4_Tick);
                    timerProdcutos4.Interval = 1000; // 1 second
                    timerProdcutos4.Start();
                }
            }
        }

        private void timerProdcutos4_Tick(object sender, EventArgs e)
        {
            ContadorDescuento4--;
            if (ContadorDescuento4 == 0)
            {
                timerProdcutos4.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 12; i < ConsultaDescuantos.Rows.Count && i <= 14; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel5.Controls.Add(lblprecio);
                    flowLayoutPanel4.Visible = false;
                    flowLayoutPanel5.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    ContadorDescuento5 += 3;
                }
                if (ConsultaDescuantos.Rows.Count > 14)
                {
                    timerproducto5 = new System.Windows.Forms.Timer();
                    timerproducto5.Tick += new EventHandler(timerproducto5_Tick);
                    timerproducto5.Interval = 1000; // 1 second
                    timerproducto5.Start();
                }
            }
        }

        private void timerproducto5_Tick(object sender, EventArgs e)
        {
            ContadorDescuento5--;
            if (ContadorDescuento5 == 0)
            {
                timerproducto5.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 15; i < ConsultaDescuantos.Rows.Count && i <= 17; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel6.Controls.Add(lblprecio);
                    flowLayoutPanel5.Visible = false;
                    flowLayoutPanel6.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    Contadorescuetnos6 += 3;
                }
                if (ConsultaDescuantos.Rows.Count > 17)
                {
                    timerProductos6 = new System.Windows.Forms.Timer();
                    timerProductos6.Tick += new EventHandler(timerProductos6_Tick);
                    timerProductos6.Interval = 1000; // 1 second
                    timerProductos6.Start();
                }
            }
        }

        private void timerProductos6_Tick(object sender, EventArgs e)
        {
            Contadorescuetnos6--;
            if (Contadorescuetnos6 == 0)
            {
                timerProductos6.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 18; i < ConsultaDescuantos.Rows.Count && i <= 21; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel7.Controls.Add(lblprecio);
                    flowLayoutPanel6.Visible = false;
                    flowLayoutPanel7.Visible = true;
                    mas++;
                    numerosPrecios++;
                    
                    ContadorDescuentos7 += 3;
                }
                if (ConsultaDescuantos.Rows.Count > 22)
                {
                    timerProductos7 = new System.Windows.Forms.Timer();
                    timerProductos7.Tick += new EventHandler(timerProductos7_Tick);
                    timerProductos7.Interval = 1000; // 1 second
                    timerProductos7.Start();
                }
            }
        }

        private void timerProductos7_Tick(object sender, EventArgs e)
        {
            Contadorescuetnos6--;
            if (Contadorescuetnos6 == 0)
            {
                timerProductos7.Stop();
                var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
                string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
                var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
                for (int i = 22; i < ConsultaDescuantos.Rows.Count && i <= 24; i++)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel8.Controls.Add(lblprecio);
                    flowLayoutPanel7.Visible = false;
                    flowLayoutPanel8.Visible = true;
                    mas++;
                    numerosPrecios++;
                }
            }
        }
    }
}
