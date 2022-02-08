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
    public partial class Cancelar_XML_mensajes : Form
    {
        string[][] arr_resultado;
        int t = 0;

        public Cancelar_XML_mensajes(string[][] arr_resul)
        {
            InitializeComponent();

            t = arr_resul.Length;
            arr_resultado = new string [t][];
            arr_resultado = arr_resul;

            cargar_datos();
        }

        private void cargar_datos()
        {
            int y = 7; //\r\n
            //int l = 0;

            for(var z=0; z< t; z++)
            {
                string tipo_img = "close";

                if(arr_resultado[z][6] == "201" | arr_resultado[z][6] == "202")
                {
                    tipo_img = "check";
                }

                PictureBox picbox = new PictureBox();
                picbox.Size = new Size(27, 27);
                picbox.Location = new Point(5, (y-5));
                picbox.Image = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black\" + tipo_img + ".png");
                
                Label lbl_f = new Label();
                lbl_f.Text = arr_resultado[z][2];
                lbl_f.Location = new Point(43, y);
                lbl_f.Size = new Size(50, 17);

                Label lbl_m = new Label();
                lbl_m.Text = arr_resultado[z][5];
                lbl_m.Location = new Point(106, y);
                //lbl_m.Size = new Size(589, 34);
                lbl_m.AutoSize = true;


                pnl_mensajes.Controls.Add(picbox);
                pnl_mensajes.Controls.Add(lbl_f);
                pnl_mensajes.Controls.Add(lbl_m);

                //l = 17 * 2;
                y = y + 11 + 17;
            }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
