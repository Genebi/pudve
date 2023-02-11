using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class verificarBasesSQLite : Form
    {
        DBTables dbTables = new DBTables();
        Conexion cn = new Conexion();

        string fileName = "pudveDB.db",
               sourcePath = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\",
               targetPath = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\BackUp\",
               tabla = string.Empty,
               queryTabla = string.Empty,
               nametable = string.Empty,
               querySQLite = string.Empty,
               queryForeignKey = string.Empty,
               queryUpdateSQLite = string.Empty,
               machineName = string.Empty;

        int count = 0,
            contadorMetodoTablas = 0,
            numTable = 0;

        bool IsEmpty,
             exist;

        private void RestoreNameMachine()
        {
            Properties.Settings.Default.Hosting = machineName;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void ResetNameMachine()
        {
            machineName = Properties.Settings.Default.Hosting;
            Properties.Settings.Default.Hosting = string.Empty;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        public verificarBasesSQLite()
        {
            //InitializeComponent();
            //ResetNameMachine();
            //createDirToBackUpDB();  // Se crea una carpeta si no existe
            //doBackUpDB();           // Realizamos el copiado del archivo de un directorio a otro
            //Shown += new EventHandler(verificarBasesSQLite_Shown);  // Agregamos un manejador del evento Shown de la forma
            //// To report progress form the background worker we need to set this property
            //verificarDBSQLiteSegundoPlano.WorkerReportsProgress = true;
            //// This event will be raised on the worker thread when the worker starts
            //verificarDBSQLiteSegundoPlano.DoWork += new DoWorkEventHandler(verificarDBSQLiteSegundoPlano_DoWork);
            //// This event will be raised when we call ReportProgress
            //verificarDBSQLiteSegundoPlano.ProgressChanged += new ProgressChangedEventHandler(verificarDBSQLiteSegundoPlano_ProgressChanged);
        }

        private void verificarDBSQLiteSegundoPlano_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show("Error: " + e.Error.Message.ToString());
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                MessageBox.Show("Cancelación");
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                RestoreNameMachine();
                this.Close();
            }
        }

        private void verificarDBSQLiteSegundoPlano_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // The progress percentage is a property of e
            pBCheckDB.Value = e.ProgressPercentage;

            label1.ForeColor = Color.Red;
            label1.Text = "Verificado " + pBCheckDB.Value.ToString() + " %";

            Application.DoEvents();
        }

        private void verificarDBSQLiteSegundoPlano_DoWork(object sender, DoWorkEventArgs e)
        {
            // your background task goes here
            int contarProgreso = 0,
                cantidadTablas = dbTables.countTables(),
                aumentoProgressBar = 0;

            decimal resultado;

            RevisarTablas();

            resultado = 100 / cantidadTablas;

            aumentoProgressBar = (int)Math.Ceiling(resultado);

            for (int i = 0; i < cantidadTablas; i++)
            {
                if (contarProgreso >= 92)
                {
                    contarProgreso = 100;
                }
                else
                {
                    contarProgreso += aumentoProgressBar;
                }

                verificarDBSQLiteSegundoPlano.ReportProgress(contarProgreso);

                Thread.Sleep(180);
            }
        }

        private void RevisarTablas()
        {
            DataTable nameTables, ForeignKeyList;
            ConexionDataDictionary cnDataD = new ConexionDataDictionary();

            string nombresTablas = string.Empty,
                   listForeginKey = string.Empty,
                   nameOfTable = string.Empty;

            nombresTablas = "SELECT COUNT(*) AS Repeticiones, * FROM DiccionarioDeDatos dData GROUP BY NameTable HAVING COUNT(*)>1 ORDER BY NameTable ASC;";

            listForeginKey = "SELECT * FROM ForeignKeyList;";

            using (ForeignKeyList = cnDataD.CargarDatos(listForeginKey)) { }

            numTable = 1;

            using (nameTables = cnDataD.CargarDatos(nombresTablas))
            {
                if (!nameTables.Rows.Count.Equals(0))
                {
                    foreach (DataRow row in nameTables.Rows)
                    {
                        nameOfTable = string.Empty;
                        nameOfTable = row["NameTable"].ToString();

                        compararDataDictionary(nameOfTable, ForeignKeyList);
                    }
                }
            }
        }

        private void compararDataDictionary(string nombreTabla, DataTable foreignKeyList)
        {
            DataTable dataDictionary = new DataTable(), pudveDB = new DataTable();
            ConexionDataDictionary cnDataD = new ConexionDataDictionary();

            nametable = nombreTabla;

            string queryNameFilter = $"SELECT * FROM DiccionarioDeDatos WHERE NameTable = '{nametable}'";

            using (dataDictionary = cnDataD.CargarDatos(queryNameFilter))
            {

            }

            using (SQLiteConnection sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;"))
            {
                SQLiteSchema infoSchemaTables = new SQLiteSchema(sql_con);

                // Solo una tabla
                var InfoTablaOfDataBase = infoSchemaTables.Columns(nametable);

                if (!InfoTablaOfDataBase.Count.Equals(0))
                {
                    /********************************************************
                    *    Hacemos la estructura de la Tabla con sus campos   *
                    *    y tipos de dato de cada uno de ellos               *
                    ********************************************************/
                    #region Declaramos el nombre y tipo de columna  de la tabla pudveDB
                    DataColumn colNameTable = new DataColumn("NameTable");
                    colNameTable.DataType = System.Type.GetType("System.String");
                    pudveDB.Columns.Add(colNameTable);

                    DataColumn colDataType = new DataColumn("DataType");
                    colDataType.DataType = System.Type.GetType("System.String");
                    pudveDB.Columns.Add(colDataType);

                    DataColumn colCid = new DataColumn("Cid");
                    colCid.DataType = System.Type.GetType("System.Int64");
                    pudveDB.Columns.Add(colCid);

                    DataColumn colName = new DataColumn("Name");
                    colName.DataType = System.Type.GetType("System.String");
                    pudveDB.Columns.Add(colName);

                    DataColumn colNoVacio = new DataColumn("NoVacio");
                    colNoVacio.DataType = System.Type.GetType("System.Int64");
                    pudveDB.Columns.Add(colNoVacio);

                    DataColumn colPrimaryKey = new DataColumn("PrimaryKey");
                    colPrimaryKey.DataType = System.Type.GetType("System.Int64");
                    pudveDB.Columns.Add(colPrimaryKey);

                    DataColumn colDfltValue = new DataColumn("DfltValue");
                    colDfltValue.DataType = System.Type.GetType("System.String");
                    pudveDB.Columns.Add(colDfltValue);
                    #endregion

                    // llenamos cada una de las filas de la tabla
                    foreach (var item in InfoTablaOfDataBase)
                    {
                        DataRow newRow = pudveDB.NewRow();

                        newRow["NameTable"] = nametable.ToString();
                        newRow["DataType"] = item.Type.ToString();
                        newRow["Cid"] = item.CId.ToString();
                        newRow["Name"] = item.Name.ToString();
                        newRow["NoVacio"] = Convert.ToInt64(item.NotNull.ToString());
                        newRow["PrimaryKey"] = Convert.ToInt64(item.PrimaryKey.ToString());
                        newRow["DfltValue"] = item.DfltValue.ToString();

                        pudveDB.Rows.Add(newRow);
                    }
                }

                if (dataDictionary.Rows.Count.Equals(pudveDB.Rows.Count))
                {
                    // comparación y saber si las dos tablas tienen misma cantidad de filas

                    // obtenemos la diferencia de los registros por fila
                    DataTable dt = getDifferentRecords(dataDictionary, pudveDB);

                    // verificamos si no tiene diferencia de fila
                    if (dt.Rows.Count == 0)
                    {
                        // verificamos que sean los mismos nombres y tipos de columnas
                        fieldsCheckNameSQLite(dataDictionary, nametable);

                        // generamos las claves foraneas si las tiene
                        generateForeKeySQLiteCode(foreignKeyList, nametable);

                        // generamos el SQLite para hacer la actualizacion
                        createUpdateQuery(nametable, dataDictionary, pudveDB);

                        // hacemos el proceso de realizar las operaciones del SQLite
                        rebuildDataBaseSQLite(nametable);
                    }
                    else
                    {
                        #region sección para enviar mensaje de diferencia en columnas de la tabla
                        string numCol = string.Empty;

                        foreach (DataRow rowDt in dt.Rows)
                        {
                            numCol += "\nColumna No: " + rowDt["Cid"].ToString();
                        }

                        //MessageBox.Show("Revisar la siguiente información:\n" + numCol + "\nFavor de ponerse en contacto\nsu administrador de base de datos", "Revision de Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        #endregion

                        // generamos SQLite en base a la diferencia que tubo la compracion
                        getDifferentMetaData(dataDictionary, pudveDB);

                        // generamos las claves foraneas si las tiene
                        generateForeKeySQLiteCode(foreignKeyList, nametable);

                        // generamos el SQLite para hacer la actualizacion
                        createUpdateQuery(nametable, dataDictionary, pudveDB);

                        // hacemos el proceso de realizar las operaciones del SQLite
                        rebuildDataBaseSQLite(nametable);
                    }
                    numTable++;
                }
                else if (dataDictionary.Rows.Count > pudveDB.Rows.Count)
                {
                    // comparar si dataDictionary tiene menos filas que la tabla pudveDB
                    // y comprobar si dataDictionary tiene mas filas que la tabla pudveDB

                    // generamos el SQLite de la nueva tabla que agregaremos
                    fieldsCheckNameSQLite(dataDictionary, nametable);

                    // generamos el SQLite para hacer la actualización
                    createUpdateQueryChangeTable(nametable, dataDictionary, pudveDB);

                    // hacemos el proceso de realizar las operaciones del SQLite
                    rebuildDataBaseSQLite(nametable);
                }
                else if ((dataDictionary.Rows.Count > 0) && (pudveDB.Rows.Count.Equals(0)))
                {
                    // comparación si la tabla dataDictionary contiene filas
                    // y si tabla pudveDB no contiene ninguna fila
                    if (dataDictionary.Rows.Count > 0)
                    {
                        // generamos el SQLite de la nueva tabla que agregaremos
                        fieldsCheckNameSQLite(dataDictionary, nametable);

                        // generamos el SQLite para hacer la actualizacion
                        createUpdateQueryNvaTabla(nametable, dataDictionary, pudveDB);

                        // hacemos el proceso de realizar las operaciones del SQLite
                        rebuildDataBaseSQLite(nametable);
                    }
                }
            }
        }

        private void createUpdateQueryNvaTabla(string nametable, DataTable dataDictionary, DataTable pudveDB)
        {
            pudveDB = dataDictionary.Clone();

            queryUpdateSQLite = string.Empty;

            if (!nametable.Equals("") && !dataDictionary.Rows.Count.Equals(0) && pudveDB.Rows.Count.Equals(0))
            {
                queryUpdateSQLite += $"INSERT INTO '{nametable}_new' (";

                foreach (DataRow rowDataDictionary in dataDictionary.Rows)
                {
                    queryUpdateSQLite += $"{rowDataDictionary["Name"].ToString()}, ";
                }
                queryUpdateSQLite = queryUpdateSQLite.Remove(queryUpdateSQLite.Length - 2);
                queryUpdateSQLite += ") SELECT ";

                foreach (DataRow rowDataDictionary in dataDictionary.Rows)
                {
                    queryUpdateSQLite += $"{rowDataDictionary["Name"].ToString()}, ";
                }
                queryUpdateSQLite = queryUpdateSQLite.Remove(queryUpdateSQLite.Length - 2);
                queryUpdateSQLite += $" FROM '{nametable}'";
            }
        }

        private void createUpdateQueryChangeTable(string nametable, DataTable dataDictionary, DataTable pudveDB)
        {
            queryUpdateSQLite = string.Empty;

            if (dataDictionary.Rows.Count > pudveDB.Rows.Count)
            {
                int diferencia = dataDictionary.Rows.Count - pudveDB.Rows.Count;

                queryUpdateSQLite += $"INSERT INTO '{nametable}_new' (";

                for (int i = 0; i < dataDictionary.Rows.Count - diferencia; i++)
                {
                    queryUpdateSQLite += $"{dataDictionary.Rows[i]["Name"].ToString()}, ";
                }
                queryUpdateSQLite = queryUpdateSQLite.Remove(queryUpdateSQLite.Length - 2);
                queryUpdateSQLite += ") SELECT ";

                for (int i = 0; i < pudveDB.Rows.Count; i++)
                {
                    queryUpdateSQLite += $"{pudveDB.Rows[i]["Name"].ToString()}, ";
                }
                queryUpdateSQLite = queryUpdateSQLite.Remove(queryUpdateSQLite.Length - 2);
                queryUpdateSQLite += $" FROM '{nametable}'";
            }
        }

        private void getDifferentMetaData(DataTable dataDictionary, DataTable pudveDB)
        {
            if (dataDictionary.Rows.Count == pudveDB.Rows.Count)
            {
                for (int rowIndex = 0; rowIndex < dataDictionary.Rows.Count; rowIndex++)
                {
                    bool isEqual = arrayHaveSameContent(dataDictionary.Rows[rowIndex].ItemArray, pudveDB.Rows[rowIndex].ItemArray);

                    if (!isEqual)
                    {
                        generateSQLiteCode(dataDictionary);
                        break;
                    }
                }
            }
        }

        private void generateSQLiteCode(DataTable dataDictionary)
        {
            bool success = false, contains = false;

            string nvoFiltro = string.Empty;
            string strDefaultValue = string.Empty;

            int intDefaultValue = 0;

            querySQLite = string.Empty;

            querySQLite += $"CREATE TABLE {nametable}_new (";
            foreach (DataRow rowDataDirectory in dataDictionary.Rows)
            {
                querySQLite += $"{rowDataDirectory["Name"].ToString()} {rowDataDirectory["DataType"].ToString()} ";
                int noVacio = Convert.ToInt32(rowDataDirectory["NoVacio"].ToString());
                if (noVacio.Equals(1))
                {
                    querySQLite += $"NOT NULL ";
                }
                int primaryKey = Convert.ToInt32(rowDataDirectory["PrimaryKey"].ToString());
                if (primaryKey.Equals(1))
                {
                    querySQLite += $"PRIMARY KEY AUTOINCREMENT ";
                }
                if (!rowDataDirectory["DfltValue"].ToString().Equals(""))
                {
                    //querySQLite += $"DEFAULT ({rowDataDirectory["DfltValue"].ToString()}) ";
                    string defaultValue = rowDataDirectory["DfltValue"].ToString();
                    contains = defaultValue.Contains("-");

                    if (contains.Equals(true))
                    {
                        strDefaultValue = rowDataDirectory["DfltValue"].ToString();
                        querySQLite += $"DEFAULT {strDefaultValue} ";
                    }
                    else if (contains.Equals(false))
                    {
                        success = Int32.TryParse(rowDataDirectory["DfltValue"].ToString(), out intDefaultValue);

                        if (success)
                        {
                            querySQLite += $"DEFAULT ({rowDataDirectory["DfltValue"].ToString()}) ";
                        }
                        else
                        {
                            strDefaultValue = rowDataDirectory["DfltValue"].ToString();
                            querySQLite += $"DEFAULT {strDefaultValue} ";
                        }
                    }
                }
                querySQLite += $",";
            }
            querySQLite = querySQLite.Remove(querySQLite.Length - 1);
            querySQLite += $");";
        }

        private bool arrayHaveSameContent(object[] itemArray1, object[] itemArray2)
        {
            bool sameContent = true;

            for (int i = 0; i < itemArray1.Length; i++)
            {
                if (!itemArray1[i].Equals(itemArray2[i]))
                {
                    sameContent = false;
                    break;
                }
            }

            return sameContent;
        }

        private void rebuildDataBaseSQLite(string nametable)
        {
            string querySQLiteTranstacction = string.Empty;
            queryTabla = string.Empty;

            try
            {
                checkEmpty(nametable);
            }
            catch (Exception ex)
            {
                //cn.CrearTabla(querySQLite);
            }

            if (IsEmpty == true)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTabla(nametable));

                    if (dbTables.getValorVariable(nametable) > count)
                    {
                        if (count == 0)
                        {
                            int countFoundTable = comprobarSiExisteTablaEnLaDB(nametable);
                            string newStringQuery = string.Empty;

                            if (countFoundTable.Equals(0))
                            {
                                newStringQuery = querySQLite.Replace($"{nametable}_new", $"{nametable}");
                                cn.CrearTabla(newStringQuery);
                            }
                        }
                        if (count > 0 && count < dbTables.getValorVariable(nametable))
                        {
                            querySQLiteTranstacction += cn.ForeginKeysOff();
                            querySQLiteTranstacction += " " + cn.BeginTransaction();
                            querySQLiteTranstacction += " " + querySQLite;
                            querySQLiteTranstacction += " " + queryUpdateSQLite + ";";
                            queryTabla = dbTables.DropTabla(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            queryTabla = dbTables.QueryRename(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            querySQLiteTranstacction += " " + cn.Commit();
                            querySQLiteTranstacction += " " + cn.ForeginKeysOn();
                            cn.EjecutarConsulta(querySQLiteTranstacction);
                        }
                    }
                    else if (dbTables.getValorVariable(nametable).Equals(count))
                    {
                        if (count == 0)
                        {
                            int countFoundTable = comprobarSiExisteTablaEnLaDB(nametable);
                            string newStringQuery = string.Empty;

                            if (countFoundTable.Equals(0))
                            {
                                newStringQuery = querySQLite.Replace($"{nametable}_new", $"{nametable}");
                                cn.CrearTabla(newStringQuery);
                            }
                        }
                        if (count > 0 && count <= dbTables.getValorVariable(nametable))
                        {
                            querySQLiteTranstacction += cn.ForeginKeysOff();
                            querySQLiteTranstacction += " " + cn.BeginTransaction();
                            querySQLiteTranstacction += " " + querySQLite;
                            querySQLiteTranstacction += " " + queryUpdateSQLite + ";";
                            queryTabla = dbTables.DropTabla(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            queryTabla = dbTables.QueryRename(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            querySQLiteTranstacction += " " + cn.Commit();
                            querySQLiteTranstacction += " " + cn.ForeginKeysOn();
                            cn.EjecutarConsulta(querySQLiteTranstacction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + nametable + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (IsEmpty == false)
            {
                try
                {
                    count = cn.CountColumnasTabla(dbTables.PragmaTabla(nametable));

                    if (dbTables.getValorVariable(nametable) > count)
                    {
                        if (count == 0)
                        {
                            int countFoundTable = comprobarSiExisteTablaEnLaDB(nametable);
                            string newStringQuery = string.Empty;

                            if (countFoundTable.Equals(0))
                            {
                                newStringQuery = querySQLite.Replace($"{nametable}_new", $"{nametable}");
                                cn.CrearTabla(newStringQuery);
                            }
                        }
                        if (count > 0 && count < dbTables.getValorVariable(nametable))
                        {
                            querySQLiteTranstacction += cn.ForeginKeysOff();
                            querySQLiteTranstacction += " " + cn.BeginTransaction();
                            querySQLiteTranstacction += " " + querySQLite;
                            querySQLiteTranstacction += " " + queryUpdateSQLite + ";";
                            queryTabla = dbTables.DropTabla(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            queryTabla = dbTables.QueryRename(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            querySQLiteTranstacction += " " + cn.Commit();
                            querySQLiteTranstacction += " " + cn.ForeginKeysOn();
                            cn.EjecutarConsulta(querySQLiteTranstacction);
                        }
                    }
                    else if (dbTables.getValorVariable(nametable).Equals(count))
                    {
                        if (count == 0)
                        {
                            int countFoundTable = comprobarSiExisteTablaEnLaDB(nametable);
                            string newStringQuery = string.Empty;

                            if (countFoundTable.Equals(0))
                            {
                                newStringQuery = querySQLite.Replace($"{nametable}_new", $"{nametable}");
                                cn.CrearTabla(newStringQuery);
                            }
                        }
                        if (count > 0 && count <= dbTables.getValorVariable(nametable))
                        {
                            querySQLiteTranstacction += cn.ForeginKeysOff();
                            querySQLiteTranstacction += " " + cn.BeginTransaction();
                            querySQLiteTranstacction += " " + querySQLite;
                            querySQLiteTranstacction += " " + queryUpdateSQLite + ";";
                            queryTabla = dbTables.DropTabla(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            queryTabla = dbTables.QueryRename(nametable);
                            querySQLiteTranstacction += " " + queryTabla;
                            querySQLiteTranstacction += " " + cn.Commit();
                            querySQLiteTranstacction += " " + cn.ForeginKeysOn();
                            cn.EjecutarConsulta(querySQLiteTranstacction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al checar la tabla: " + nametable + " error No: " + ex.Message.ToString(), "Error de Checar Tablas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private int comprobarSiExisteTablaEnLaDB(string nametable)
        {
            int valor = cn.CountColumnasTabla(dbTables.encontrarTabla(nametable));

            return valor;
        }

        private bool checkEmpty(string nametable)
        {
            string queryTableCheck = $"SELECT * FROM '{tabla}'";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }

        private void createUpdateQuery(string nametable, DataTable dataDictionary, DataTable pudveDB)
        {
            queryUpdateSQLite = string.Empty;
            if (!nametable.Equals("") && !dataDictionary.Rows.Count.Equals(0) && !pudveDB.Rows.Count.Equals(0))
            {
                queryUpdateSQLite += $"INSERT INTO '{nametable}_new' (";

                foreach (DataRow rowDataDictionary in dataDictionary.Rows)
                {
                    queryUpdateSQLite += $"{rowDataDictionary["Name"].ToString()}, ";
                }
                queryUpdateSQLite = queryUpdateSQLite.Remove(queryUpdateSQLite.Length - 2);
                queryUpdateSQLite += ") SELECT ";

                foreach (DataRow rowpudve in pudveDB.Rows)
                {
                    queryUpdateSQLite += $"{rowpudve["Name"].ToString()}, ";
                }
                queryUpdateSQLite = queryUpdateSQLite.Remove(queryUpdateSQLite.Length - 2);
                queryUpdateSQLite += $" FROM '{nametable}'";
            }
        }

        private void generateForeKeySQLiteCode(DataTable foreignKeyList, string nombreTabla)
        {
            DataTable dtFiltrada = new DataTable();

            string nvoFiltro = string.Empty;

            nvoFiltro = $"name = '{nombreTabla}'";

            queryForeignKey = string.Empty;

            using (dtFiltrada = SelectDataTable(foreignKeyList, nvoFiltro))
            {
                if (!dtFiltrada.Rows.Count.Equals(0))
                {
                    querySQLite = querySQLite.Remove(querySQLite.Length - 2);

                    queryForeignKey += ", ";

                    foreach (DataRow rowForeingKey in dtFiltrada.Rows)
                    {
                        queryForeignKey += "FOREIGN KEY (";

                        if (!rowForeingKey["from"].ToString().Equals(""))
                        {
                            queryForeignKey += $"{rowForeingKey["from"].ToString()}) ";
                        }
                        if (!rowForeingKey["table"].ToString().Equals(""))
                        {
                            queryForeignKey += $"REFERENCES {rowForeingKey["table"].ToString()} ";
                        }
                        if (!rowForeingKey["to"].ToString().Equals(""))
                        {
                            queryForeignKey += $"({rowForeingKey["to"].ToString()}) ";
                        }
                        if (!rowForeingKey["on_update"].ToString().Equals("NO ACTION"))
                        {
                            queryForeignKey += $"ON UPDATE {rowForeingKey["on_update"].ToString()} ";
                        }
                        if (!rowForeingKey["on_delete"].ToString().Equals("NO ACTION"))
                        {
                            queryForeignKey += $"ON DELETE {rowForeingKey["on_delete"].ToString()} ";
                        }
                        queryForeignKey += ", ";
                    }
                    queryForeignKey = queryForeignKey.Remove(queryForeignKey.Length - 2);
                    queryForeignKey += ");";

                    querySQLite += queryForeignKey;
                }
            }
        }

        private void fieldsCheckNameSQLite(DataTable dataDictionary, string nametable)
        {
            DataTable dtFiltrada = new DataTable();

            bool success = false, contains = false;

            string nvoFiltro = string.Empty;
            string strDefaultValue = string.Empty;

            int intDefaultValue = 0;

            nvoFiltro = $"NameTable = '{nametable}'";

            querySQLite = string.Empty;

            using (dtFiltrada = SelectDataTable(dataDictionary, nvoFiltro))
            {
                if (!dtFiltrada.Rows.Count.Equals(0))
                {
                    querySQLite += $"CREATE TABLE {nametable}_new (";

                    foreach (DataRow rowDataFilter in dtFiltrada.Rows)
                    {
                        querySQLite += $"{rowDataFilter["Name"].ToString()} {rowDataFilter["DataType"].ToString()} ";

                        if (rowDataFilter["DataType"].ToString().Equals("INTEGER"))
                        {
                            int primaryKey = Convert.ToInt32(rowDataFilter["PrimaryKey"].ToString());
                            if (primaryKey.Equals(1))
                            {
                                querySQLite += $"PRIMARY KEY AUTOINCREMENT ";
                            }
                        }
                        else if (rowDataFilter["DataType"].ToString().Equals("TEXT"))
                        {
                            int primaryKey = Convert.ToInt32(rowDataFilter["PrimaryKey"].ToString());
                            if (primaryKey.Equals(1))
                            {
                                querySQLite += $"PRIMARY KEY ";
                            }
                        }

                        int noVacio = Convert.ToInt32(rowDataFilter["NoVacio"].ToString());
                        if (noVacio.Equals(1))
                        {
                            querySQLite += $"NOT NULL ";
                        }

                        if (!rowDataFilter["DfltValue"].ToString().Equals(""))
                        {
                            string defaultValue = rowDataFilter["DfltValue"].ToString();
                            contains = defaultValue.Contains("-");

                            if (contains.Equals(true))
                            {
                                strDefaultValue = rowDataFilter["DfltValue"].ToString();
                                querySQLite += $"DEFAULT {strDefaultValue} ";
                            }
                            else if (contains.Equals(false))
                            {
                                success = Int32.TryParse(rowDataFilter["DfltValue"].ToString(), out intDefaultValue);

                                if (success)
                                {
                                    querySQLite += $"DEFAULT ({rowDataFilter["DfltValue"].ToString()}) ";
                                }
                                else
                                {
                                    strDefaultValue = rowDataFilter["DfltValue"].ToString();
                                    querySQLite += $"DEFAULT {strDefaultValue} ";
                                }
                            }
                        }

                        querySQLite += $",";
                    }
                    querySQLite = querySQLite.Remove(querySQLite.Length - 1);
                    querySQLite += $");";
                }
            }
        }

        private DataTable SelectDataTable(DataTable dt, string filter)
        {
            DataRow[] rows;
            DataTable dtNew = new DataTable();

            // copiamos la estructura de la tabla
            dtNew = dt.Clone();

            // ordenamos (sort) y filtramos (filter) los datos
            rows = dt.Select(filter);

            // llenamos a dtNew con las filas filtradas
            foreach (DataRow dr in rows)
            {
                dtNew.ImportRow(dr);
            }

            // retornamos dt filtrada
            return dtNew;
        }

        #region Compare two DataTables and return a DataTable with DifferentRecords
        private DataTable getDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            // Create Empty Table   
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                    {
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                    }
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                    {
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                    }
                }
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }
        #endregion

        private void verificarBasesSQLite_Shown(object sender, EventArgs e)
        {
            if (contadorMetodoTablas == 0)
            {
                try
                {
                    // Start the background worker
                    verificarDBSQLiteSegundoPlano.RunWorkerAsync();
                    contadorMetodoTablas = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message.ToString());
                }
            }
        }

        private void doBackUpDB()
        {
            exist = System.IO.Directory.Exists(targetPath);

            if (exist)
            {
                #region Direcciones de carpetas
                /************************************************************************
                *                                                                       * 
                *   Use Path class to manipulate file and directory paths.              *
                *                                                                       *
                *************************************************************************
                *                                                                       *
                *   Usamos la clase Path para manipular el archivo y el directorio      *
                *                                                                       *
                ************************************************************************/
                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName.Remove(7) + " Fecha " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".db");
                #endregion

                #region Copiar contenido del directorio
                /************************************************************************************
                *                                                                                   * 
                *   To copy a folder's contents to a new location:                                  *
                *   Create a new target folder.                                                     *
                *   If the directory already exists, this method does not create a new directory.   *
                *                                                                                   *
                *************************************************************************************
                *                                                                                   *
                *   Para copiar el contenido del direcorio a una nueva locación:                    *
                *   Creamos un nuevo directorio.                                                    *
                *   Si el directorio ya existe, esté metodo no crea un nuevo directorio.            *
                *                                                                                   *
                ************************************************************************************/
                System.IO.Directory.CreateDirectory(targetPath);
                #endregion

                #region Copiamos archivo
                /********************************************************************************
                *                                                                               *
                *    To copy a file to another location and                                     *
                *    overwrite the destination file if it already exists.                       *
                *                                                                               *
                *********************************************************************************    
                *                                                                               *
                *    Para copiar un archivo a otra locación y                                   *
                *    sobre escribir los archivos del directorio destino si estos ya existen.    *
                *                                                                               *
                ********************************************************************************/
                System.IO.File.Copy(sourceFile, destFile, true);
                #endregion
            }
        }

        private void createDirToBackUpDB()
        {
            exist = System.IO.Directory.Exists(targetPath);

            if (!exist)
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
        }
    }
}
