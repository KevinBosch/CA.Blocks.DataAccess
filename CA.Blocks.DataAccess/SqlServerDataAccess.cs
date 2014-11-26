//===============================================================================
// Code Associate Data Access Block for .NET
// DataAccessCore.cs
//
//===============================================================================
// Copyright (C) 2002-2014 Ravin Enterprises Ltd. 
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;
using CA.Blocks.DataAccess.Paging;

namespace CA.Blocks.DataAccess
{
	/// <summary>
    /// Provides a SQL server implementation for DataAccessCore
	/// </summary>
	/// <remarks>
    /// <p>source code:</p>
    /// <code outlining="true" source="..\CANC\Blocks\DataAccess\SqlServerDataAccess.cs" lang="cs"/> 
	/// </remarks>
	public class SqlServerDataAccess : DataAccessCore
	{

        public const string FILTER_REPLACE_STRING = "/*##FILTER##*/";
        public const string JOIN_REPLACE_STRING = "/*##JOIN##*/";

		public SqlServerDataAccess(string connectionString) : base(connectionString) 
		{

		}

        protected virtual string GetConnectionContext()
        {
            return null;
        }

        public void SetCommandContext(SqlConnection sqlConnection)
        {
            string context = GetConnectionContext();
            if (!String.IsNullOrWhiteSpace(context))
            {
                var cmd = CreateTextCommand("SET CONTEXT_INFO @AppContext");
                AddInputParamCommandAsBinary(cmd, "@AppContext", Encoding.ASCII.GetBytes(context));
                cmd.Connection = sqlConnection;
                cmd.ExecuteNonQuery();
            }
        }

	    protected override bool PrepCommand(IDbCommand cmd)
		{
			SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            SetCommandContext(sqlConnection);
            cmd.Connection = sqlConnection;
			return true;
		}

		protected override DbDataAdapter GetDataAdapter(IDbCommand cmd)
		{
			return (new SqlDataAdapter((SqlCommand)cmd));
		}

        #region StoredProcedureHelpers

        protected SqlCommand CreateBlankStoredProcedureCommand(string strStoredProcedureName, bool bolIncludeReturnValue)
        {
            SqlCommand sqlcmd = new SqlCommand
                                                {
                                                    CommandText = strStoredProcedureName,
                                                    CommandType = CommandType.StoredProcedure
                                                };
            if (bolIncludeReturnValue)
            {
                SqlParameter sqlparam = sqlcmd.CreateParameter();
                sqlparam.ParameterName = "Return";
                sqlparam.SqlDbType = SqlDbType.Int;
                sqlparam.Direction = ParameterDirection.ReturnValue;
                sqlcmd.Parameters.Add(sqlparam);
            }
            return (sqlcmd);
        }


        protected int GetStoredProcedureReturnValue(SqlCommand sqlcmd)
        {
            int result = -1;
            SqlParameter sqlparam = sqlcmd.Parameters["Return"];
            if (sqlparam != null)
            {
                if (sqlparam.Value != null)
                    result = (int) sqlparam.Value;
            }
            return result;
        }


        #endregion StoredProcedureHelpers

        #region TextCommandType Helpers
        protected SqlCommand CreateTextCommand(string sql)
        {
        SqlCommand sqlcmd = new SqlCommand
                                {
                                    CommandText = sql,
                                    CommandType = CommandType.Text
                                };
            return (sqlcmd);
        }

        protected SqlCommand CreateTextCommand(string sqlTemplate, string mainFilter)
        {
            string sql = sqlTemplate.Replace(FILTER_REPLACE_STRING, mainFilter);
            return CreateTextCommand(sql);
        }

        protected SqlCommand CreateTextCommand(string sqlTemplate, string mainFilter, string mainJoin)
        {
            string sql = sqlTemplate.Replace(JOIN_REPLACE_STRING, mainJoin);
            return CreateTextCommand(sql,mainFilter);
        }

        protected SqlCommand CreateTableSelectCommand(string tableName, string filter)
        {
            return CreateTextCommand(string.Format("SELECT * FROM {0} {1}", tableName, filter));
        }

        protected SqlCommand CreateTableSelectCommand(string tableName, string filter, string orderBy)
        {
            return CreateTextCommand(string.Format("SELECT * FROM {0} {1} Order By {2}", tableName, filter, orderBy));
        }

        #endregion StoredProcedureHelpers

        #region ParemeterHelpers

        protected SqlParameter AddInputParamCommand(SqlCommand cmd, string strParameterName, object objParameterValue, DbType odbType, int maxParamSize)
        {
            SqlParameter sqlparam = new SqlParameter(strParameterName, odbType);
            sqlparam.Direction = ParameterDirection.Input;

            if (maxParamSize > 0) sqlparam.Size = maxParamSize;

            if ((objParameterValue == null || objParameterValue == DBNull.Value))
            {
                sqlparam.Value = DBNull.Value;
                //Added the following as sometimes the type is changed to int32
                sqlparam.DbType = odbType;
            }
            else
                sqlparam.Value = objParameterValue;

            cmd.Parameters.Add(sqlparam);

            return (sqlparam);
        }

        protected SqlParameter AddInputParamCommand(SqlCommand cmd, string strParameterName, object objParameterValue, SqlDbType odbType, int maxParamSize)
        {
            SqlParameter sqlparam = new SqlParameter(strParameterName, odbType);
            sqlparam.Direction = ParameterDirection.Input;

            if (maxParamSize > 0) sqlparam.Size = maxParamSize;

            if ((objParameterValue == null || objParameterValue == DBNull.Value))
            {
                sqlparam.Value = DBNull.Value;
                //Added the following as sometimes the type is changed to int32
                sqlparam.SqlDbType = odbType;
            }
            else
                sqlparam.Value = objParameterValue;

            cmd.Parameters.Add(sqlparam);

            return (sqlparam);
        }

        protected SqlParameter AddInputParamCommandAsInt(SqlCommand cmd, string strParameterName, int? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Int, 4);
        }

        protected SqlParameter AddInputParamCommandAsShort(SqlCommand cmd, string strParameterName, short? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.SmallInt, 2);
        }

        protected SqlParameter AddInputParamCommandAsLong(SqlCommand cmd, string strParameterName, long? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.BigInt, 8);
        }

        //protected SqlParameter AddInputParamCommandAsDeciaml(SqlCommand cmd, string strParameterName, decimal? objParameterValue)
        //{
        //    return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Decimal, 38);
        //}

        protected SqlParameter AddInputParamCommandAsGuid(SqlCommand cmd, string strParameterName, Guid? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.UniqueIdentifier, 16);
        }


        protected SqlParameter AddInputParamCommandAsByte(SqlCommand cmd, string strParameterName, byte? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.TinyInt, 1);
        }

        protected SqlParameter AddInputParamCommandAsBinary(SqlCommand cmd, string strParameterName, byte[] objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.VarBinary, -1);
        }


        protected SqlParameter AddInputParamCommandAsBit(SqlCommand cmd, string strParameterName, bool objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Bit, 1);
        }

        protected SqlParameter AddInputParamCommandAsString(SqlCommand cmd, string strParameterName, string objParameterValue, int maxSize)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.VarChar, maxSize);
        }

        // dont care about the size of the object this assumes the check as been done already else SQL will raise an error 
        protected SqlParameter AddInputParamCommandAsString(SqlCommand cmd, string strParameterName, string objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.VarChar, -1 );
        }


        protected SqlParameter AddInputParamCommandAsBool(SqlCommand cmd, string strParameterName, bool? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Bit, 1);
        }

        protected SqlParameter AddInputParamCommandAsCharBool(SqlCommand cmd, string strParameterName, bool objParameterValue)
        {
            char DBValue = objParameterValue ? 'Y' : 'N';
            return AddInputParamCommand(cmd, strParameterName, DBValue, SqlDbType.Char, 1);
        }

        protected SqlParameter AddInputParamCommandAsChar(SqlCommand cmd, string strParameterName, char? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Char, 1);
        }

        protected SqlParameter AddInputParamCommandAsDateTime(SqlCommand cmd, string strParameterName, DateTime? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.DateTime, 8);
        }

        protected SqlParameter AddInputParamCommandAsDateTime2(SqlCommand cmd, string strParameterName, DateTime? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.DateTime2, 8);
        }

        protected SqlParameter AddInputParamCommandAsSmallDateTime(SqlCommand cmd, string strParameterName, DateTime? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.SmallDateTime, 4);
        }

        protected SqlParameter AddInputParamCommandAsMoney(SqlCommand cmd, string strParameterName, Decimal? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Money, 0);
        }

        protected SqlParameter AddInputParamCommandAsDecimal(SqlCommand cmd, string strParameterName, Decimal? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Decimal, 0);
        }

        protected SqlParameter AddInputParamCommandAsFloat(SqlCommand cmd, string strParameterName, Double? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Float, 0);
        }

        protected SqlParameter AddInputParamCommandAsDouble(SqlCommand cmd, string strParameterName, Double? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Float, 0);
        }

        protected SqlParameter AddInputParamCommandAsTimeSpan(SqlCommand cmd, string strParameterName, TimeSpan? objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.Time, 0);
        }


        protected SqlParameter AddInputParamCommandAsStringMax(SqlCommand cmd, string strParameterName, string objParameterValue)
        {
            return AddInputParamCommand(cmd, strParameterName, objParameterValue, SqlDbType.VarChar, int.MaxValue);
        }

        protected SqlParameter AddOutputParamCommand(SqlCommand cmd, string strParameterName, DbType odbType, Int32 maxParamSize)
        {
            SqlParameter sqlparam = new SqlParameter(strParameterName, odbType);
            sqlparam.Direction = ParameterDirection.Output;
            if (maxParamSize > 0)
                sqlparam.Size = maxParamSize;
            cmd.Parameters.Add(sqlparam);

            return (sqlparam);
        }

        protected SqlParameter AddOutputParamCommand(SqlCommand cmd, string strParameterName, SqlDbType odbType, Int32 maxParamSize)
        {
            SqlParameter sqlparam = new SqlParameter(strParameterName, odbType);
            sqlparam.Direction = ParameterDirection.Output;
            if (maxParamSize > 0)
                sqlparam.Size = maxParamSize;
            cmd.Parameters.Add(sqlparam);

            return (sqlparam);
        }


        protected SqlParameter AddAdapterInputParamCommand(SqlCommand cmd, string strParameterName, string sourceColName, DataTable sourceDataTable)
        {
            SqlParameter sqlparam;

            if (sourceDataTable.Columns.Contains(sourceColName))
            {
                DataColumn dc = sourceDataTable.Columns[sourceColName];
                sqlparam = new SqlParameter(strParameterName, dc.DataType);
                sqlparam.Direction = ParameterDirection.Input;
                
                sqlparam.SourceColumn = sourceColName;

                sqlparam.SourceVersion = DataRowVersion.Current;

                cmd.Parameters.Add(sqlparam);
            }
            else
            {
                throw new Exception(string.Format("SourceColName {0} does not exist in the SourceDataTable as such cannot be added as a parameter!", sourceColName));
            }
            return (sqlparam);
        }

        protected SqlParameter AddAdapterInputParamCommand(SqlCommand cmd, string strParameterName, DataTable sourceDataTable)
        {
            return
                AddAdapterInputParamCommand(cmd, strParameterName, strParameterName.Replace("@", string.Empty),
                                            sourceDataTable);
        }

        #endregion ParemeterHelpers 

        #region SQLType Helpers

        /// <summary>
        /// This is usefull when you dont know the sql datatype but you do know the physical type example is datatable
        /// DataColumn dc = ??
        //  AddInputParamCommand(cmd, dc.ColumnName, dr[dc], GetDBType(dc.DataType), dc.MaxLength);
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        protected SqlDbType GetDBType(System.Type theType)
        {
            SqlParameter p1 = new SqlParameter();
            TypeConverter tc = TypeDescriptor.GetConverter(p1.DbType);
            if (tc.CanConvertFrom(theType))
            {
                tc.ConvertFrom(theType.Name);
                p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
            }
            else
            {
                //Try brute force
                try
                {
                    p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
                }
                catch (Exception ex)
                {
                    //Do Nothing
                }
            }
            return p1.SqlDbType;
        }

        #endregion

        #region SQL Bulk Update Methods

        protected SqlDataAdapter CreateBulkInsertAdapter(string storedProcedureName, int batchSize)
        {
            SqlDataAdapter result = new SqlDataAdapter();
            result.UpdateBatchSize = batchSize;
            SqlCommand cmd = CreateBlankStoredProcedureCommand(storedProcedureName, false);
            cmd.UpdatedRowSource = UpdateRowSource.None;
            result.InsertCommand = cmd;
            return result;
        }

        // gets the first col which has an expression on.  
        // This will need to be refactored if you have expressions based on expressions as you will need to be aware of dependency order
        // if no expressions are found it will return null. 
        private DataColumn GetColunmWithExpression(DataTable dt)
        {
            DataColumn result = null;
            foreach (DataColumn dcloop in dt.Columns)
            {
                if (!string.IsNullOrEmpty(dcloop.Expression))
                {
                    result = dcloop;
                    break;
                }
            }
            return result;
        }

        protected void CementExpressionsAsValues(DataTable dt)
        {
            DataColumn colWithExpression = GetColunmWithExpression(dt);
            int excapeCounter = 0;
            while (colWithExpression != null && excapeCounter < dt.Columns.Count)
            {
                
                string tempColName = colWithExpression.ColumnName + Guid.NewGuid().ToString();
                dt.Columns.Add(tempColName, colWithExpression.DataType);

                foreach(DataRow dr in dt.Rows)
                {
                    dr[tempColName] = dr[colWithExpression.ColumnName];
                }
                dt.Columns.Remove(colWithExpression);
                dt.Columns[tempColName].ColumnName = colWithExpression.ColumnName;

                colWithExpression = GetColunmWithExpression(dt);
                excapeCounter++;
            }
	    }

        protected void ExecuteBulkInsertAdapter(SqlDataAdapter bulkAdapter, DataTable dt)
        {
            try
            {
                PrepCommand(bulkAdapter.InsertCommand);
                // possibly move this function out as it nos not really belong here 
                CementExpressionsAsValues(dt);
                bulkAdapter.Update(dt);
            }
            finally
            {
                WrapUp(bulkAdapter.InsertCommand.Connection, true);
            }     
        }
        #endregion SQL Bulk Update Methods


        #region 

        public DataTable ExecuteDataTable(SqlCommand cmd, PagingRequest page)
        {
            // this is sql server specific and only for direct quries

            string sortOrder = page.GetOrderBy();
            string sqlSelect = string.Format(" ROW_NUMBER() Over (Order By {0}) As RowNumber, ", sortOrder);
            cmd.CommandText = WrapPagingQuery(cmd.CommandText, sqlSelect);
            AddInputParamCommandAsInt(cmd, "@PagingRowNumberFrom", page.Skip + 1);
            AddInputParamCommandAsInt(cmd, "@PagingRowNumberTo", page.Skip + page.Take);
            return ExecuteDataTable(cmd);
        }

        protected string WrapPagingQuery(string sourceQuery, string orderOver)
        {
            sourceQuery = sourceQuery.Trim();
            if (sourceQuery.StartsWith("Select", StringComparison.CurrentCultureIgnoreCase))
            {
                sourceQuery = "Select " + orderOver + sourceQuery.Substring(6);
                string pagingWrapperSQL = @"With PagingWrapper As 
                            (
                              {0}
                            ) 
                        Select PagingWrapper.*
                        from PagingWrapper
                        Where PagingWrapper.RowNumber Between @PagingRowNumberFrom AND @PagingRowNumberTo
                        Order By PagingWrapper.RowNumber Asc";

                return string.Format(pagingWrapperSQL, sourceQuery);
            }
            else
            {
                throw new ApplicationException("To Execute ExecuteDataTable using a PagingRequest the Command must be text query and start with 'Select'   ");
            }
        }

        #endregion 
    }
}
