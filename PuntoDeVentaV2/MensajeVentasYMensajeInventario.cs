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
    public partial class MensajeVentasYMensajeInventario : Form
    {
        public MensajeVentasYMensajeInventario()
        {
            InitializeComponent();
        }

        Button btn = new Button();  
        public static string enviarDato;

        private void MensajeVentasYMensajeInventario_Load(object sender, EventArgs e)
        {
                generarButton(15, 15, "Mensaje\nVentas", "MensajeVentas");
            generarButton(145, 15, "Mensaje\nInventario", "MensajeInventario");
        }

        public void generarButton(int x, int y, string nombreTexto,string nombreButton)
        {
            btn = new BotonRedondo();
            btn.Name = "btn"+nombreButton;
            btn.Width = 120;
            btn.Height = 70;
            btn.Text = nombreTexto;
            btn.Location = new Point(x, y);
            btn.Click += new EventHandler(btn_click);
            btn.Cursor = Cursors.Hand;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Century", 8F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(0, 104, 144);
            btn.UseVisualStyleBackColor = false;
            Image imagenBoton = Image.FromFile(Properties.Settings.Default.rutaDirectorio + $@"\PUDVE\icon\black\edit.png");
            btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btn.Image = imagenBoton;
            this.Controls.Add(btn);
        }

        private void btn_click(object sender, EventArgs e)
        {
            Button cliclButton = (Button)sender;  //se hace un if para ver en que boton se dio el click y asi abrir correctamente lo que el usuario necesita
            if (cliclButton.Text == "Mensaje\nVentas")
            {
                enviarDato = "mensajeVentas";
                EditarMensajesParaEnviar mensaje = new EditarMensajesParaEnviar();
                mensaje.ShowDialog();
            }
            else if(cliclButton.Text == "Mensaje\nInventario")
            {
                enviarDato = "mensajeInventario";
                EditarMensajesParaEnviar mensaje = new EditarMensajesParaEnviar();
                mensaje.ShowDialog();
            }
        }

        private void MensajeVentasYMensajeInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
