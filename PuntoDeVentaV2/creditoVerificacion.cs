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
    public partial class creditoVerificacion : Form
    {
        string codigo = string.Empty;
        private static Random random = new Random();
        public bool validado = false;
        string numero = "";
        public creditoVerificacion(string cel)
        {
            InitializeComponent();
            numero = cel;
        }

        private void creditoVerificacion_Load(object sender, EventArgs e)
        {
            enviarWats();
        }


        private void enviarWats()
        {
            string mensaje = "Tu código de verificación es: ";
            codigo = RandomString(5);
            

            DialogResult dialogResult = MessageBox.Show("Se enviará un mensaje al siguiente número:"+numero, "Confirmación de envío", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://api.whatsapp.com/send?phone=+" + numero + "&text=" + mensaje + codigo);
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }
        
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void txtVerificar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || txtVerificar.Text.Length>=5)
            {
                if (txtVerificar.Text.Equals(codigo))
                {
                    validado = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Clave incorrecta", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVerificar.Clear();
                }
            }
        }

        private void txtVerificar_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
