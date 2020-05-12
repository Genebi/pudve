using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    class Paginar
    {
        //private String ps_cadena = "Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;";
        private string ps_cadena = string.Empty;
        private int _inicio = 0;
        private int _tope = 0;

        private int _numeroPagina = 1;
        private int _cantidadRegistros = 0;
        private int _ultimaPagina = 0;

        private String _datamember;
        private SQLiteDataAdapter _adapter;
        private DataSet _datos;

        private String _query;

        // s_query              =   El query de conexion
        // s_datamember         =   Se asigna al datagridview despues del datasource
        // i_cantidadxpagina    =   Cantidad de registros por pagina
        public Paginar(String s_query, String s_datamember, int i_cantidadxpagina)
        {
            this._query = s_query;
            this._inicio = 0;
            this._tope = i_cantidadxpagina;
            this._datamember = s_datamember;

            DataTable auxiliar;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                ps_cadena = "Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;";
            }
            else
            {
                ps_cadena = "Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;";
            }

            SQLiteConnection connection = new SQLiteConnection(ps_cadena);
            this._adapter = new SQLiteDataAdapter(_query, connection);
            this._datos = new DataSet();
            auxiliar = new DataTable();

            connection.Open();
            this._adapter.Fill(_datos, _inicio, _tope, _datamember);
            _adapter.Fill(auxiliar);
            connection.Close();
            this._cantidadRegistros = auxiliar.Rows.Count;

            asignarTope();
        }

        private void asignarTope()
        {
            _ultimaPagina = _cantidadRegistros / _tope;

            int aux = _cantidadRegistros % _tope;
            if (_ultimaPagina == 0)
            {
                this._ultimaPagina = 1;
            }
            else if (_ultimaPagina >= 1 && (aux > 0))
            {
                this._ultimaPagina = _ultimaPagina + 1;
            }
            this._numeroPagina = 1;
        }

        public DataSet cargar()
        {
            return _datos;
        }

        public DataSet primerPagina()
        {
            this._numeroPagina = 1;
            this._inicio = 0;
            this._datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, this._tope, this._datamember);
            return _datos;
        }

        public DataSet ultimaPagina()
        {
            this._numeroPagina = _ultimaPagina;
            this._inicio = (_ultimaPagina - 1) * _tope;
            this._datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, this._tope, this._datamember);
            return _datos;
        }

        public DataSet atras()
        {
            if (this._numeroPagina == 1)
            {
                return _datos;
            }
            this._numeroPagina--;
            this._inicio = _inicio - _tope;
            this._datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, this._tope, this._datamember);
            return _datos;
        }

        public DataSet adelante()
        {
            if (this._ultimaPagina == this._numeroPagina)
            {
                return _datos;
            }
            this._numeroPagina++;
            this._inicio = _inicio + _tope;
            this._datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);
            return _datos;
        }

        public DataSet irAPagina(int num_pagina)
        {
            if ((num_pagina <= 0) || (num_pagina > this._ultimaPagina))
            {
                MessageBox.Show("Número de página\nno valido.", "Error de Rango", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (num_pagina <= 0)
                {
                    this._numeroPagina = 1;
                    this._inicio = 0;
                }
                else if (num_pagina > this._ultimaPagina)
                {
                    this._numeroPagina = _ultimaPagina;
                    this._inicio = (_ultimaPagina - 1) * _tope;
                }
            }
            else if ((num_pagina > 0) || (num_pagina < this._ultimaPagina))
            {
                this._numeroPagina = num_pagina;
                this._inicio = _inicio + _tope;
            }
            
            this._datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);
            return _datos;
        }

        public DataSet actualizarTope(int i_tope)
        {
            this._tope = i_tope;
            this._inicio = 0;
            asignarTope();
            _datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);
            return _datos;
        }
        
        public DataSet actualizarPagina()
        {
            DataTable auxiliar;

            SQLiteConnection connection = new SQLiteConnection(ps_cadena);
            this._adapter = new SQLiteDataAdapter(_query, connection);
            this._datos = new DataSet();
            auxiliar = new DataTable();

            connection.Open();
            this._adapter.Fill(_datos, _inicio, _tope, _datamember);
            _adapter.Fill(auxiliar);
            connection.Close();

            if (this._ultimaPagina == this._numeroPagina)
            {
                return _datos;
            }
            this._datos.Clear();
            this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);
            return _datos;
        }

        public int countRow()
        {
            return _cantidadRegistros;
        }

        public int countPag()
        {
            return _ultimaPagina;
        }

        public int numPag()
        {
            return _numeroPagina;
        }

        public int limitRow()
        {
            return _tope;
        }
    }
}
