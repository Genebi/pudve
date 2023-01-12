using MySql.Data.MySqlClient;
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
        #region Inicio Declaracion de Variables Globales
        private string ps_cadena = string.Empty;    // Almacena ruta de la DB SQLite
        private int _inicio = 0;                    // Valor desde que registro de la DB inicia a mostrar
        private int _tope = 0;                      // Valor hasta que registro de la DB finaliza a mostrar

        private int _numeroPagina = 1;              // Número de página la que se esta mostrando
        private int _cantidadRegistros = 0;         // Cantidad de Filas segun la Consulta de la DB
        private int _ultimaPagina = 0;              // Almacena cantidad paginas a mostrar

        private String _datamember;                 // Sera a que tabla nos vamos a dirigir
        private MySqlDataAdapter _adapter;         // Adaptador para manejo de SQLite
        private DataSet _datos;                     // DataSet donde se va almacenar la informacion

        private String _query;                      // Almacena la Consulta a SQLite
        #endregion Final Declaracion de Variables Globales

        // s_query              =   El query que proviene del sistema
        // s_datamember         =   Se asigna al datagridview despues del datasource
        // i_cantidadxpagina    =   Cantidad de registros por pagina
        public Paginar(string s_query, string s_datamember, int i_cantidadxpagina)
        {
            this._query = s_query;              // le asignamos la consulta que se le paso del sistema
            this._datamember = s_datamember;    // le asignamos a que tabla se va dirigir la consulta
            // le asignamos la cantidad de productos que se va mostrar por pagina
            if (i_cantidadxpagina >= 1)
            {
                this._tope = i_cantidadxpagina;

                this._inicio = 0;                   // le asignamos el valor inicial desde que registro iniciara a mostrar

                DataTable auxiliar;                 // Tabla auxiliar para el manejo de la informcion

                #region Inicio Path de la DB de SQLite
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                {
                    ps_cadena = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
                }
                else
                {
                    ps_cadena = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
                }
                #endregion Final Path de la DB de SQLite

                MySqlConnection connection = new MySqlConnection(ps_cadena);  // Iniciamos la conexion SQLite
                this._adapter = new MySqlDataAdapter(_query, connection);      // Iniciamos el adaptador SQLite
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
            else if (i_cantidadxpagina <= 0)
            {
                MessageBox.Show("No se puede poner 0 en cantidad de filas a mostrar.", "Avertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            //MySqlConnection connection = new MySqlConnection(ps_cadena);  // Iniciamos la conexion SQLite
            //this._adapter = new MySqlDataAdapter(_query, connection);      // Iniciamos el adaptador SQLite
            //this._datos = new DataSet();                                    // Iniciamos el DataSet SQLite
            //auxiliar = new DataTable();

            //connection.Open();                                              // Abrimos conexion connection hacia SQLite
            //this._adapter.Fill(_datos, _inicio, _tope, _datamember);        // Almacenamos en el Adapter el resultdo de
            //                                                                // la consulta pero solo mostranndo los rangos
            //                                                                // Mostrar desde Inicio hasta el Tope asignado
            //                                                                // configurado desde el sistema
            //_adapter.Fill(auxiliar);                                        // Llenamos la tabla auxiliar con el Adapter
            //connection.Close();                                             // Cerramos la conexion SQLite
            //this._cantidadRegistros = auxiliar.Rows.Count;                  // Le asignamos la cantidad de registros que tiene la consulta

            //if (_tope != 0)
            //{
            //    asignarTope();                                              // Mandamos llamar al metodo asignarTope()
            //}
        }

        private void asignarTope()
        {
            if (_tope != 0)
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
            else
            {
                MessageBox.Show($"Cantidad de empleados \n no valida", "Aviso de sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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
            if (this._numeroPagina == 1)    // Si _numeroPagina es igual a 1
            {
                return _datos;      // Se retorna _datos
            }
            else    // Si _numeroPagina es diferente a 1
            {
                this._numeroPagina--;               // Se disminuye _numeroPagina
                this._inicio = _inicio - _tope;     // Se asigna a _inicio el valor en funcion a la siguiente resta _inicio - _tope
                this._datos.Clear();                // Borramos el DataSet
                this._adapter.Fill(this._datos, this._inicio, this._tope, this._datamember);    // Almacenamos en el Adapter el resultdo de
                                                                                                // la consulta pero solo mostranndo los rangos
                                                                                                // Mostrar desde Inicio hasta el Tope asignado
                                                                                                // configurado desde el sistema 
                return _datos;                      // Retorna el DataSet
            }
        }

        public DataSet adelante()
        {
            if (this._ultimaPagina == this._numeroPagina)       // Si _ultimaPagina es igual a _numeroPagina
            {
                return _datos;      // Se retorna _datos
            }
            else    // Si _ultimaPagina es diferente a _numeroPagina
            {
                this._numeroPagina++;               // Se aumenta _numeroPagina
                this._inicio = _inicio + _tope;     // Se asigna a _inicio el valor en funcion a la siguiente resta _inicio + _tope
                this._datos.Clear();                // Borramos el DataSet
                this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);     // Almacenamos en el Adapter el resultdo de
                                                                                            // la consulta pero solo mostranndo los rangos
                                                                                            // Mostrar desde Inicio hasta el Tope asignado
                                                                                            // configurado desde el sistema 
                return _datos;                      // Retorna el DataSet
            }
        }

        public DataSet irAPagina(int num_pagina)
        {
            if (true)
            {
                if ((num_pagina <= 0) || (num_pagina > this._ultimaPagina))     // Si num_pagina es menor igual que 0 ó num_pagina es mayor que _ultimaPagina
                {
                    MessageBox.Show("Número de página\nno valido.", "Error de Rango", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (num_pagina <= 0)    // Si num_Pagina es menor igual que 0 
                    {
                        this._numeroPagina = 1;     // Le asignamos _numeroPagina igual a 1
                        this._inicio = 0;           // Le asignamos _inicio igual a 0
                    }
                    else if (num_pagina > this._ultimaPagina)   // Si num_pagina es mayor que _ultimaPagina
                    {
                        this._numeroPagina = _ultimaPagina;             // Le asignamos _numeroPagina es igual que _ultimaPagina
                        this._inicio = (_ultimaPagina - 1) * _tope;     // Le asignamos _inicio es igual a la resta _ultimaPagina - 1
                                                                        // esto multiplicado por _tope
                    }
                }
                else if ((num_pagina > 0) || (num_pagina <= this._ultimaPagina))     // Si no y num_pagina es mayor que 0 ó num_pagina es menor que _ultimaPagina
                {
                    if (num_pagina.Equals(this._numeroPagina))
                    {
                        //return _datos;      // Se retorna _datos
                    }
                    else
                    {
                        if (num_pagina.Equals(1))
                        {
                            this._numeroPagina = 1;     // Asignamos a _numeroPagina el numero 1
                            this._inicio = 0;           // Asignamos a _inicio el numero 0
                        }
                        else
                        {
                            this._numeroPagina = num_pagina;    // Le asignamos _numeroPagina igual num_pagina
                            this._inicio = (num_pagina - 1) * _tope;     // Le asignamos _inicio igual a la resta
                        }
                    }
                    //if (num_pagina.Equals(1))
                    //{
                    //    this._numeroPagina = 1;     // Asignamos a _numeroPagina el numero 1
                    //    this._inicio = 0;           // Asignamos a _inicio el numero 0
                    //}
                    //else if (num_pagina.Equals(_ultimaPagina))
                    //{
                    //    this._numeroPagina = _ultimaPagina;             // Asignamos _numeroPagina lo que tiene _ultimaPagina
                    //    this._inicio = _inicio + _tope;     // Asignamos a _inicio lo que resulte (_ultimaPagina-1)*_tope
                    //}
                    //else
                    //{
                    //    this._numeroPagina = num_pagina;    // Le asignamos _numeroPagina igual num_pagina
                    //    this._inicio = (num_pagina - 1) * _tope;     // Le asignamos _inicio igual a la resta
                    //}
                }

                this._datos.Clear();    // Borramos el DataSet
                this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);     // Almacenamos en el Adapter el resultdo de
                                                                                            // la consulta pero solo mostranndo los rangos
                                                                                            // Mostrar desde Inicio hasta el Tope asignado
                                                                                            // configurado desde el sistema
                //return _datos;                      // Retorna el DataSet
            }
            else if (_datos.Tables[0].Rows.Count <= 1)
            {
                this._datos.Clear();    // Borramos el DataSet
                this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);     // Almacenamos en el Adapter el resultdo de
                                                                                            // la consulta pero solo mostranndo los rangos
                                                                                            // Mostrar desde Inicio hasta el Tope asignado
                                                                                            // configurado desde el sistema
                //return _datos;                      // Retorna el DataSet
            }

            return _datos;
        }

        public DataSet actualizarTope(int i_tope)
        {
            if (i_tope >= 1)
            {
                this._tope = i_tope;    // Le asignamos _tope igual a i_tope
                this._inicio = 0;       // Le asignamos _inicio igual a 0
                asignarTope();          // Mandamos llamar al metodo asignarTope()
                _datos.Clear();         // Borramos el DataSet
                this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember);     // Almacenamos en el Adapter el resultdo de
                                                                                            // la consulta pero solo mostranndo los rangos
                                                                                            // Mostrar desde Inicio hasta el Tope asignado
                                                                                            // configurado desde el sistema
            }
            else if (i_tope <= 0)
            {
                MessageBox.Show("No se puede poner 0 en cantidad de filas a mostrar.", "Avertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return _datos;          // Retorna el DataSet
        }
        
        public DataSet actualizarPagina()
        {
            DataTable auxiliar;     // Tabla auxiliar para el manejo de la informcion

            MySqlConnection connection = new MySqlConnection(ps_cadena);      // Iniciamos la conexion SQLite
            this._adapter = new MySqlDataAdapter(_query, connection);          // Iniciamos el adaptador SQLite
            this._datos = new DataSet();                                        // Iniciamos el DataSet SQLite
            auxiliar = new DataTable();                                         

            connection.Open();                                          // Abrimos conexop SQLite
            this._adapter.Fill(_datos, _inicio, _tope, _datamember);    // Almacenamos en el Adapter el resultdo de
                                                                        // la consulta pero solo mostranndo los rangos
                                                                        // Mostrar desde Inicio hasta el Tope asignado
                                                                        // configurado desde el sistema
            _adapter.Fill(auxiliar);    // Llenamos la tabla auxiliar con el Adapter 
            connection.Close();         // Cerramos la conexion SQLite

            if (this._ultimaPagina == this._numeroPagina)   // Si _ultimaPagina es igual a _numeroPagina
            {
                return _datos;      // Retorna el DataSet
            }
            else
            {
                this._datos.Clear();    // Borramos contenido DataSet
                this._adapter.Fill(this._datos, this._inicio, _tope, this._datamember); // Almacenamos en el Adapter el resultdo de
                                                                                        // la consulta pero solo mostranndo los rangos
                                                                                        // Mostrar desde Inicio hasta el Tope asignado
                                                                                        // configurado desde el sistema
                return _datos;  // Retorna el DataSet
            }
        }

        public int countRow()
        {
            return _cantidadRegistros;  // Retorna la cantidad de registros (Productos, Servicios y Combos)
        }

        public int countPag()
        {
            return _ultimaPagina;   // Retorna el numero de la ultima pagina
        }

        public int numPag()
        {
            return _numeroPagina;   // Retorna el numero de pagina en que se encuentra actualmente
        }

        public int limitRow()
        {
            return _tope;   // Retorna el numero de tope a mostrar por pagina
        }

        public int inicio()
        {
            return _inicio;     // Retorna el numero donde inicio de registro a mostrar
        }
    }
}
