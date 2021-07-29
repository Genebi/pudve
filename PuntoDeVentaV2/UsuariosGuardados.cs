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
    public partial class UsuariosGuardados : Form
    {
        public UsuariosGuardados()
        {
            InitializeComponent();
        }

        private void UsuariosGuardados_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            List<String> usuarios = new List<String>();
            string path = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    var usuarioGuardado = s.Split(',');
                    dgvUsuariosGuardados.Rows.Add(usuarioGuardado[0].ToString().Replace("[", ""));
                }
            }
        }

        private void dgvUsuariosGuardados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(1))
            {
                string path = @"C:\Archivos PUDVE\DatosDeUsuarios\UsuarioyContraseña.txt";
                var contraseña = string.Empty;
                using (StreamReader sr2 = File.OpenText(path))
                {
                    string s2 = "";
                    while ((s2 = sr2.ReadLine()) != null)
                    {
                        var user = s2.Split(',');

                        if (dgvUsuariosGuardados.Rows[e.RowIndex].Cells[0].Value.ToString()== user[0].ToString().Replace("[", ""))
                        {
                            contraseña = user[1].ToString();
                        }
                    }
                }
                string item = "[" + dgvUsuariosGuardados.Rows[e.RowIndex].Cells[0].Value.ToString() + "," + contraseña;

                var lines = File.ReadAllLines(path).Where(line => line.Trim() != item).ToArray();
                File.WriteAllLines(path, lines);
            }
            dgvUsuariosGuardados.Rows.Clear();
            cargarDatos();
        }

        private void dgvUsuariosGuardados_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(1))
            {
                dgvUsuariosGuardados.Cursor = Cursors.Hand;
            }
            else
            {
                dgvUsuariosGuardados.Cursor = Cursors.Default;
            }
        }
    }
}
