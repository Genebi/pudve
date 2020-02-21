﻿using System;
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
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
        }

        private void btnHistorialPrecios_Click(object sender, EventArgs e)
        {
            using (var fechas = new FechasReportes())
            {
                fechas.ShowDialog();
            }
        }
    }
}
