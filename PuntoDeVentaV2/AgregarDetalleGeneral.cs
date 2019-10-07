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
    public partial class AgregarDetalleGeneral : Form
    {
        Conexion cn = new Conexion();

        public string getIdUsr { get; set; }
        public string getChkName { get; set; }

        string IdUsr = string.Empty, ChkName = string.Empty;

        public AgregarDetalleGeneral()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var DetalleGral = txtNombre.Text;

            if (string.IsNullOrWhiteSpace(DetalleGral))
            {
                MessageBox.Show("Introduzca un nombre para la ubicación", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int resultado = cn.EjecutarConsulta($"INSERT INTO DetalleGeneral (IDUsuario, ChckName, Descripcion) VALUES ('{IdUsr}', '{ChkName}', '{DetalleGral}')");

            if (resultado > 0)
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgregarDetalleGeneral_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            IdUsr = getIdUsr;
            ChkName = getChkName;
            label1.Text = "Concepto de " + ChkName;
        }
    }
}
