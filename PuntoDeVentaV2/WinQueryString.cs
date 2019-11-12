using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class WinQueryString : Form
    {
        bool    filtroStock, 
                filtroPrecio;

        string  strFiltroStock = string.Empty, 
                strFiltroPrecio = string.Empty,
                strOpcionCBStock = string.Empty,
                strOpcionCBPrecio = string.Empty,
                strTxtStock = string.Empty,
                strTxtPrecio = string.Empty;

        public WinQueryString()
        {
            InitializeComponent();
        }

        private void txtCantStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Soló son permitidos numeros\nen este campo de Stock",
                                "Error de captura del Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WinQueryString_Load(object sender, EventArgs e)
        {
            cbTipoFiltroStock.SelectedIndex = 0;
            cbTipoFiltroStock.SelectedIndex = 0;

            cbTipoFiltroStock_SelectedIndexChanged(sender, e);

            validarChkBox();
        }

        private void validarChkBox()
        {
            cbTipoFiltroStock.SelectedIndex = 0;
            if (chkBoxStock.Checked.Equals(true))
            {
                filtroStock = Convert.ToBoolean(chkBoxStock.Checked);

                Properties.Settings.Default.chkFiltroStock = filtroStock;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantStock.Enabled = true;
                cbTipoFiltroStock.Enabled = true;
                txtCantStock.Text = "";
                txtCantStock.Focus();
            }
            else if (chkBoxStock.Checked.Equals(false))
            {
                filtroStock = Convert.ToBoolean(chkBoxStock.Checked);

                Properties.Settings.Default.chkFiltroStock = filtroStock;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantStock.Enabled = false;
                cbTipoFiltroStock.Enabled = false;
                txtCantStock.Text = "";
            }

            cbTipoFiltroPrecio.SelectedIndex = 0;
            if (chkBoxPrecio.Checked.Equals(true))
            {
                filtroPrecio = Convert.ToBoolean(chkBoxPrecio.Checked);

                Properties.Settings.Default.chkFiltroStock = filtroStock;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantPrecio.Enabled = true;
                cbTipoFiltroPrecio.Enabled = true;
                txtCantPrecio.Text = "";
                txtCantPrecio.Focus();
            }
            else if (chkBoxPrecio.Checked.Equals(false))
            {
                filtroPrecio = Convert.ToBoolean(chkBoxPrecio.Checked);

                Properties.Settings.Default.chkFiltroStock = filtroStock;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                txtCantPrecio.Enabled = false;
                cbTipoFiltroPrecio.Enabled = false;
                txtCantPrecio.Text = "";
            }
        }

        private void chkBoxStock_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBox();
        }

        private void cbTipoFiltroStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtroStock = Properties.Settings.Default.chkFiltroStock;

            strOpcionCBStock = Convert.ToString(cbTipoFiltroStock.SelectedItem);

            if (filtroStock.Equals(true))
            {
                strTxtStock = txtCantStock.Text;

                strFiltroStock = "Stock ";

                if (!strTxtStock.Equals(""))
                {
                    if (strOpcionCBStock.Equals("No Aplica"))
                    {
                        strFiltroStock = "";
                    }
                    else if (strOpcionCBStock.Equals("Mayor Igual"))
                    {
                        strFiltroStock += $">= ";
                    }
                    else if (strOpcionCBStock.Equals("Menor Igual"))
                    {
                        strFiltroStock += $"<= ";
                    }
                    else if (strOpcionCBStock.Equals("Igual Que"))
                    {
                        strFiltroStock += $"= ";
                    }
                    else if (strOpcionCBStock.Equals("Mayor Que"))
                    {
                        strFiltroStock += $"> ";
                    }
                    else if (strOpcionCBStock.Equals("Menor Que"))
                    {
                        strFiltroStock += $"< ";
                    }
                }
                else if (strTxtStock.Equals(""))
                {
                    strFiltroStock = "";
                }
            }
        }

        private void cbTipoFiltroStock_Click(object sender, EventArgs e)
        {
            cbTipoFiltroStock.DroppedDown = true;
        }

        private void txtCantPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Soló son permitidos numeros\nen este campo de Precio",
                                "Error de captura del Precio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbTipoFiltroPrecio_Click(object sender, EventArgs e)
        {
            cbTipoFiltroPrecio.DroppedDown = true;
        }

        private void chkBoxPrecio_CheckedChanged(object sender, EventArgs e)
        {
            validarChkBox();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            cbTipoFiltroStock_SelectedIndexChanged(sender, e);

            filtroStock = Properties.Settings.Default.chkFiltroStock;
            
            if (filtroStock.Equals(true))
            {
                strTxtStock = txtCantStock.Text;

                if (!strTxtStock.Equals(""))
                {
                    if (!strOpcionCBStock.Equals("No Aplica"))
                    {
                        strFiltroStock += "\'" + strTxtStock + "\'";

                        Properties.Settings.Default.strFiltroStock = strFiltroStock;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();

                        MessageBox.Show("Query Construido es: " + Properties.Settings.Default.strFiltroStock,
                                    "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Properties.Settings.Default.strFiltroStock = string.Empty;

                        MessageBox.Show("Query Construido es: " + Properties.Settings.Default.strFiltroStock,
                                    "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (strTxtStock.Equals(""))
                {
                    strFiltroStock = "";
                    MessageBox.Show("Favor de Introducior una Cantidad de Stock\nLa Cadena de Filtro es: " + strFiltroStock, 
                                    "Filtro Construido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (filtroStock.Equals(false))
            {
                MessageBox.Show("Que Paso...");
            }
        }
    }
}
