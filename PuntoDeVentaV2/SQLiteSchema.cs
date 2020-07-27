using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    public class SQLiteSchema
    {
        public SQLiteSchema(SQLiteConnection _conn)
        {
            this.DbConnection = _conn;
            this.RefreshSchema();
        }

        /// <summary>
        /// Clears and refreshes the database schema.
        /// </summary>
        public void RefreshSchema()
        {
            this.Tables = new List<string>();
            this.Tables.Clear();
            this.Views = new List<string>();
            this.Views.Clear();
            this.DbConnection.Open();
            using (SQLiteCommand cmd = this.DbConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY name";
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.Tables.Add(dr["name"].ToString());
                }
                dr.Close();
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'view' ORDER BY name";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.Views.Add(dr["name"].ToString());
                }
                dr.Close();
            }
        }

        /// <summary>
        /// A list of columns and their metadata for a specified table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns lst></returns>
        public List<SQLiteColum> Columns(string tableName)
        {
            List<SQLiteColum> lst = new List<SQLiteColum>();
            using (SQLiteCommand cmd = this.DbConnection.CreateCommand())
            {
                cmd.CommandText = string.Format("pragma table_info({0})", tableName);
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lst.Add(new SQLiteColum(tableName,
                                            Convert.ToInt32(dr["cid"].ToString()),
                                            dr["name"].ToString(),
                                            dr["type"].ToString(),
                                            Convert.ToInt32(dr["notnull"].ToString()),
                                            dr["dflt_value"].ToString(),
                                            Convert.ToInt32(dr["pk"].ToString())));
                }
                dr.Close();
            }
            return lst;
        }

        /// <summary>
        /// Returns all SQLiteColumn records in the database.
        /// </summary>
        /// <returns lst></returns>
        public List<SQLiteColum> AllDatabaseColumns()
        {
            List<SQLiteColum> lst = new List<SQLiteColum>();
            using (SQLiteCommand cmd = this.DbConnection.CreateCommand())
            {
                foreach (var tableName_loopVariable in this.Tables)
                {
                    var tableName = tableName_loopVariable;
                    cmd.CommandText = string.Format("pragma table_info({0})", tableName);
                    SQLiteDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        lst.Add(new SQLiteColum(tableName,
                                                Convert.ToInt32(dr["cid"].ToString()),
                                                dr["name"].ToString(),
                                                dr["type"].ToString(),
                                                Convert.ToInt32(dr["notnull"].ToString()),
                                                dr["dflt_value"].ToString(),
                                                Convert.ToInt32(dr["pk"].ToString())));
                    }
                    dr.Close();
                }
            }
            return lst;
        }

        /// <summary>
        /// A connection the the SQLite database that has already been opened.
        /// </summary>
        public SQLiteConnection DbConnection { get; set; }

        /// <summary>
        /// The name of all of the tables in database.
        /// </summary>
        public List<string> Tables { get; set; }

        /// <summary>
        /// The name of all of the views in the database.
        /// </summary>
        public List<string> Views { get; set; }

        /// <summary>
        /// Represents the metadata for a SQLite Column
        /// </summary>
        public class SQLiteColum
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="table"></param>
            /// <param name="cId"></param>
            /// <param name="name"></param>
            /// <param name="type"></param>
            /// <param name="notNull"></param>
            /// <param name="dfltValue"></param>
            /// <param name="primaryKey"></param>
            public SQLiteColum(string table, int cId, string name, string type, int notNull, string dfltValue, int primaryKey)
            {
                this.Table = table;
                this.CId = cId;
                this.Name = name;
                this.Type = type;
                this.NotNull = notNull;
                this.DfltValue = dfltValue;
                this.PrimaryKey = primaryKey;
            }

            /// <summary>
            /// Constructor
            /// </summary>
            public SQLiteColum() { }

            /// <summary>
            /// The table that owns this column.
            /// </summary>
            public string Table { get; set; }

            /// <summary>
            /// The data type of the comlumn.
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// The column ID which represents the ordinal the column is in order in the table.
            /// </summary>
            public int CId { get; set; }

            /// <summary>
            /// The name of the column
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Whether a column can contain null data or not.
            /// </summary>
            public int NotNull { get; set; }

            /// <summary>
            /// Whether the column is a primary key or not.
            /// </summary>
            public int PrimaryKey { get; set; }

            /// <summary>
            /// Whether the column is a Default data.
            /// </summary>
            public string DfltValue { get; set; }
        }
    }
}
