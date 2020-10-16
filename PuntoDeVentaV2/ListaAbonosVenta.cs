﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ListaAbonosVenta : Form
    {
        private int idVenta = 0;
        public ListaAbonosVenta(int idVenta)
        {
            InitializeComponent();

            this.idVenta = idVenta;
        }


        private void ListaAbonosVenta_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            sql_con.Open();

            var consulta = $"SELECT * FROM Abonos WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC";
            sql_cmd = new MySqlCommand(consulta, sql_con);
            dr = sql_cmd.ExecuteReader();

            Image ticket = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");

            DGVAbonos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVAbonos.Rows.Add();

                DataGridViewRow row = DGVAbonos.Rows[rowId];

                row.Cells["ID"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                row.Cells["Efectivo"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Efectivo")).ToString());
                row.Cells["Tarjeta"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Tarjeta")).ToString());
                row.Cells["Vales"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Vales")).ToString());
                row.Cells["Cheque"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Cheque")).ToString());
                row.Cells["Trans"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Transferencia")).ToString());
                row.Cells["Total"].Value = Modificar(dr.GetValue(dr.GetOrdinal("Total")).ToString());
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                row.Cells["Ticket"].Value = ticket;
            }

            dr.Close();
            sql_con.Close();

            DGVAbonos.ClearSelection();
        }

        private string Modificar(string cadena)
        {
            var cantidad = Convert.ToDecimal(cadena).ToString("0.00");

            return cantidad;
        }

        private void DGVAbonos_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 8)
                {
                    DGVAbonos.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVAbonos_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 8)
                {
                    DGVAbonos.Cursor = Cursors.Default;
                }
            }
        }

        private void DGVAbonos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 8)
                {
                    var idAbono = DGVAbonos.Rows[DGVAbonos.CurrentCell.RowIndex].Cells["ID"].Value.ToString();

                    var nombreTicket = $"ticket_abono_{idVenta}_{idAbono}.pdf";
                    var rutaTicket = @"C:\Archivos PUDVE\Ventas\Tickets\" + nombreTicket;

                    if (File.Exists(rutaTicket))
                    {
                        VisualizadorTickets ticket = new VisualizadorTickets(nombreTicket, rutaTicket);

                        ticket.FormClosed += delegate
                        {
                            ticket.Dispose();
                        };

                        ticket.ShowDialog();
                    }
                }

                DGVAbonos.ClearSelection();
            }
        }  
    }
}
