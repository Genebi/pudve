﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class AsignarMultipleProductos : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();

        public AsignarMultipleProductos()
        {
            InitializeComponent();
        }

        private void AsignarMultipleProductos_Load(object sender, EventArgs e)
        {
            CargarPropiedades();

            panelContenedor.HorizontalScroll.Visible = false;
        }

        private void botonAsignar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            var propiedad = btn.Tag;

            using (var ap = new AsignarPropiedad(propiedad))
            {
                ap.ShowDialog();
            }
        }

        private void AgregarOpcion(string nombre, string texto, int altura)
        {
            // PANEL MENSAJE VENTAS
            FlowLayoutPanel panelCustom = new FlowLayoutPanel();
            panelCustom.Name = "panel" + nombre;
            panelCustom.Width = 350;
            panelCustom.Height = 30;
            panelCustom.FlowDirection = FlowDirection.LeftToRight;
            panelCustom.Location = new Point(3, altura);
            panelCustom.BorderStyle = BorderStyle.FixedSingle;

            Label lbCustom = new Label();
            lbCustom.Text = texto;
            lbCustom.Name = "lb" + nombre;
            lbCustom.Width = 100;
            lbCustom.Height = 20;
            lbCustom.TextAlign = ContentAlignment.MiddleCenter;

            Button btnCustom = new Button();
            btnCustom.Name = "btn" + nombre;
            btnCustom.Text = "Asignar";
            btnCustom.Cursor = Cursors.Hand;
            btnCustom.Tag = nombre;
            btnCustom.Click += new EventHandler(botonAsignar_Click);

            panelCustom.Controls.Add(lbCustom);
            panelCustom.Controls.Add(btnCustom);
            panelContenedor.Controls.Add(panelCustom);
        }


        private void CargarPropiedades()
        {
            // PANEL MENSAJE VENTAS
            AgregarOpcion("Mensaje", "Mensaje Ventas", 10);
            // PANEL MENSAJE INVENTARIO
            AgregarOpcion("MensajeInventario", "Mensaje Inventario", 45);
            // PANEL STOCK
            AgregarOpcion("Stock", "Stock", 80);
            // PANEL STOCK MINIMO
            AgregarOpcion("StockMinimo", "Stock Minimo", 115);
            // PANEL STOCK MAXIMO
            AgregarOpcion("StockMaximo", "Stock Maximo", 150);
            // PANEL PRECIO
            AgregarOpcion("Precio", "Precio", 185);
            // PANEL NUMERO DE REVISION
            AgregarOpcion("NumeroRevision", "Número Revisión", 220);
            // PANEL TIPO DE IVA
            AgregarOpcion("TipoIVA", "Tipo de IVA", 255);
            // PANEL CLAVE DE PRODUCTO (FACTURACION)
            AgregarOpcion("ClaveProducto", "Clave de Producto", 290);
            // PANEL CLAVE UNIDAD MEDIDA (FACTURACION)
            AgregarOpcion("ClaveUnidad", "Clave de Unidad", 325);
            // PANEL CORREOS PRODUCTO
            AgregarOpcion("CorreosProducto", "Correos", 360);

            int alturaEjeY = 395;

            //======================================================================

            var conceptos = mb.ConceptosAppSettings();

            foreach (var concepto in conceptos)
            {
                // Este valor de proveedor esta agregado por defecto
                if (concepto == "Proveedor")
                {
                    AgregarOpcion(concepto, concepto, alturaEjeY);

                    alturaEjeY += 35;
                }
                else
                {
                    // Aqui consultamos y agregamos todos los restantes
                    AgregarOpcion(concepto, concepto, alturaEjeY);

                    alturaEjeY += 35;
                }   
            }
        }
    }
}
