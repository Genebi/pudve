using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class EnvioDeNotaDeVentaCorreo : Form
    {
        string IDsDeVenta;
        int i = 1;
        public EnvioDeNotaDeVentaCorreo(string IDS)
        {
            InitializeComponent();
            IDsDeVenta = IDS;
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            var lista = IDsDeVenta.Split(',');
            bool sihayCorreo = false;
            foreach (var item in lista)
            {
                foreach (Control panel2 in pnl_principal.Controls)
                {
                    if (panel2 is Panel)
                    {
                        foreach (Control lbl in panel2.Controls)
                        {
                            if (lbl is Label)
                            {
                                sihayCorreo = true;
                            }
                        }
                    }
                }

            }
            if (sihayCorreo)
            {
                MessageBox.Show("Este proceso tomara unos segundos", "Aviso del Sitema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            foreach (var item in lista)
            {
                foreach (Control panel2 in pnl_principal.Controls)
                {
                    if (panel2 is Panel)
                    {
                        foreach (Control lbl in panel2.Controls)
                        {
                            if (lbl is Label)
                            {
                                ParaMandarRDLCCorreo paraMandar = new ParaMandarRDLCCorreo(Convert.ToInt32(item), lbl.Text);
                                paraMandar.ShowDialog();
                            }
                        }
                    }
                }
                
            }
            MessageBox.Show("Envio Exitoso","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
            this.Close();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (txt_correo.Text != "")
            {
                string ex_regular = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

                if (Regex.IsMatch(txt_correo.Text, ex_regular))
                {
                    // Agregar elementos 

                    agregar_elementos_correo(txt_correo.Text);

                    txt_correo.Text = string.Empty;
                    btn_enviar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("El formato del correo no es valido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay ningún correo por agregar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void agregar_elementos_correo(string txtcorreo)
        {
            FlowLayoutPanel p_correo = new FlowLayoutPanel();
            p_correo.Name = "pnl_correo" + i;
            p_correo.Size = new Size(365, 25);

            PictureBox img = new PictureBox();
            img.Name = "pctr_borrar" + i;
            img.Location = new Point(1, 1);
            img.Size = new Size(18, 18);
            img.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\close.png");
            img.Cursor = Cursors.Hand;
            img.Click += new EventHandler(img_eliminar_click);

            Label lbl = new Label();
            lbl.Name = "lb_correo" + i;
            lbl.Location = new Point(14, 1);
            lbl.Size = new Size(370, 24);
            lbl.Text = txtcorreo;

            p_correo.Controls.Add(img);
            p_correo.Controls.Add(lbl);
            p_correo.FlowDirection = FlowDirection.TopDown;

            pnl_principal.Controls.Add(p_correo);
            pnl_principal.FlowDirection = FlowDirection.TopDown;

            i++;
        }

        private void img_eliminar_click(object sender, EventArgs e)
        {
            PictureBox img_eliminar = sender as PictureBox;

            string id_img = img_eliminar.Name.Substring(11);
            string pnl_eliminar = "pnl_correo" + id_img;

            foreach (Control pnl_el in pnl_principal.Controls.OfType<FlowLayoutPanel>())
            {
                if (pnl_el.Name == pnl_eliminar)
                {
                    pnl_principal.Controls.Remove(pnl_el);
                }
            }
            if (pnl_principal.Controls.Count.Equals(0))
            {
                btn_enviar.Enabled = false;
            }
        }

        private void EnvioDeNotaDeVentaCorreo_Load(object sender, EventArgs e)
        {

        }
    }
}
