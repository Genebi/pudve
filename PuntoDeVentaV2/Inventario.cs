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
    public partial class Inventario : Form
    {
        RevisarInventario checkInventory = new RevisarInventario();
        ReporteFinalRevisarInventario FinalReportReviewInventory = new ReporteFinalRevisarInventario();

        public static int NumRevActivo;

        public int GetNumRevActive { get; set; }

        private void CargarNumRevActivo()
        {
            NumRevActivo = GetNumRevActive;
        }

        public Inventario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormCollection fOpen = Application.OpenForms;
            List<string> tempFormOpen = new List<string>();

            checkInventory.FormClosing += delegate
            {
                GetNumRevActive = Convert.ToInt32(checkInventory.NoActualCheckStock);
                CargarNumRevActivo();

                FinalReportReviewInventory.FormClosed += delegate
                {
                    foreach (Form formToClose in fOpen)
                    {
                        if (formToClose.Name != "FormPrincipal" && formToClose.Name != "Login")
                        {
                            tempFormOpen.Add(formToClose.Name);
                        }
                    }

                    foreach (var toClose in tempFormOpen)
                    {
                        Form ventanaAbierta = Application.OpenForms[toClose];
                        ventanaAbierta.Close();
                    }
                };
                if (!FinalReportReviewInventory.Visible)
                {
                    try
                    {
                        FinalReportReviewInventory.GetFilterNumActiveRecord = NumRevActivo;
                        FinalReportReviewInventory.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message.ToString(), "Error al abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            if (!checkInventory.Visible)
            {
                checkInventory.ShowDialog();
            }
            else
            {
                checkInventory.ShowDialog();
            }
        }
    }
}
