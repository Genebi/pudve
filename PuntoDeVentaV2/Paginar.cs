﻿using System;
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
        #region Inicio Declaracion de Variables Globales
        private string ps_cadena = string.Empty;    // Almacena ruta de la DB SQLite
        private int _inicio = 0;                    // Valor desde que registro de la DB inicia a mostrar
        private int _tope = 0;                      // Valor hasta que registro de la DB finaliza a mostrar

        private int _numeroPagina = 1;              // Número de página la que se esta mostrando
        private int _cantidadRegistros = 0;         // Cantidad de Filas segun la Consulta de la DB
        private int _ultimaPagina = 0;              // Almacena cantidad paginas a mostrar

        private String _datamember;                 // Sera a que tabla nos vamos a dirigir
        private SQLiteDataAdapter _adapter;         // Adaptador para manejo de SQLite
        private DataSet _datos;                     // DataSet donde se va almacenar la informacion

        private String _query;                      // Almacena la Consulta a SQLite
        #endregion Final Declaracion de Variables Globales

        // s_query              =   El query que proviene del sistema
        // s_datamember         =   Se asigna al datagridview despues del datasource
        // i_cantidadxpagina    =   Cantidad de registros por pagina
        public Paginar(String s_query, String s_datamember, int i_cantidadxpagina)
        {
            this._query = s_query;              // le asignamos la consulta que se le paso del sistema
            this._datamember = s_datamember;    // le asignamos a que tabla se va dirigir la consulta
            this._tope = i_cantidadxpagina;     // le asignamos la cantidad de productos que se va mostrar por pagina

            this._inicio = 0;                   // le asignamos el valor inicial desde que registro iniciara a mostrar
            
            DataTable auxiliar;                 // Tabla auxiliar para el manejo de la informcion

            #region Inicio Path de la DB de SQLite
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                ps_cadena = "Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;";
            }
            else
            {
                ps_cadena = "Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;";
            }
            #endregion Final Path de la DB de SQLite

            SQLiteConnection connection = new SQLiteConnection(ps_cadena);  // Iniciamos la conexion SQLite
            this._adapter = new SQLiteDataAdapter(_query, connection);      // Iniciamos el adaptador SQLite
            this._datos = new DataSet();                                    // Iniciamos el DataSet SQLite
            auxiliar = new DataTable();

            connection.Open();                                              // Abrimos conexion connection hacia SQLite
            this._adapter.Fill(_datos, _inicio, _tope, _datamember);        // Almacenamos en el Adapter el resultdo de
                                                                            // la consulta pero solo mostranndo los rangos
                                                                            // Mostrar desde Inicio hasta el Tope asignado
                                                                            // configurado desde el sistema
            _adapter.Fill(auxiliar);                                        // Llenamos la tabla auxiliar con el Adapter
            connection.Close();                                             // Cerramos la conexion SQLite
            this._cantidadRegistros = auxiliar.Rows.Count;                  // Le asignamos la cantidad de registros que tiene la consulta

            asignarTope();                                                  // Mandamos llamar al metodo asignarTope()
        }

        private void asignarTope()
        {
            _ultimaPagina = _cantidadRegistros / _tope;     // Asignamos la cantidad de paginas que tiene la
                                                            // DB pero hay que ver si la division de _cantidadRegistro
                                                            // entre _tope es exacta para saber usaremos el
                                                            // modulo que es la siguiente linea de código

            int aux = _cantidadRegistros % _tope;           // Asignamos si al usar el modulo da un residuo

            if (_ultimaPagina == 0)                         // si la division de _ultimaPagina da 0
            {
                this._ultimaPagina = 1;                     // Asignamos valor de _ultimaPagina = 1
            }
            else if (_ultimaPagina >= 1 && (aux > 0))       // Si la division de _ultimaPagina es mayor igual a 1
                                                            // y la variable aux es mayor que 0 entonces
            {
                this._ultimaPagina = _ultimaPagina + 1;     // agregamos un 1 o amentamos en 1 el valor de _ultimaPagina
            }

            this._numeroPagina = 1;                         // Asignamos el numero 1 a _ultimaPagina
        }

        public DataSet cargar()
        {
            return _datos;          // Retorna el DataSet
        }

        public DataSet primerPagina()
        {
            this._numeroPagina = 1;     // Asignamos a _numeroPagina el numero 1
            this._inicio = 0;           // Asignamos a _inicio el numero 0
            this._datos.Clear();        // Borramos el DataSet
            this._adapter.Fill(this._datos, this._inicio, this._tope, this._datamember);    // Almacenamos en el Adapter el resultdo de
                                                                                            // la consulta pero solo mostranndo los rangos
                                                                                            // Mostrar desde Inicio hasta el Tope asignado
                                                                                            // configurado desde el sistema
            return _datos;              // Retorna el DataSet
        }

        public DataSet ultimaPagina()
        {
            this._numeroPagina = _ultimaPagina;             // Asignamos _numeroPagina lo que tiene _ultimaPagina
            this._inicio = (_ultimaPagina - 1) * _tope;     // Asignamos a _inicio lo que resulte (_ultimaPagina-1)*_tope
            this._datos.Clear();                            // Borramos el DataSet 
            this._adapter.Fill(this._datos, this._inicio, this._tope, this._datamember);   // Almacenamos en el Adapter el resultdo de
                                                                                           // la consulta pero solo mostranndo los rangos
                                                                                           // Mostrar desde Inicio hasta el Tope asignado
                                                                                           // configurado desde el sistema 
            return _datos;      // Retorna el DataSet
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
