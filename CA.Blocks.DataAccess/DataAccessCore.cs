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
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Configuration;
using CA.Blocks.DataAccess.Translator;

namespace CA.Blocks.DataAccess 
{
	/// <summary>
	/// <para>
	/// This class provides the abstract implementation for the Code Associate Data Access Block.  
	/// The Abstract implementation is build upon utilizing common System.Data methods and interfacing out the Specific 
	/// DBCommand using the IDbCommand interface.  In doing this all specializations built on top of this class will behave 
	/// in the same manor. This class is abstract and cannot be created. 
	/// </para>
	/// </summary>
	/// <remarks>
	/// <p>source code:</p>
	/// <code outlining="true" source="..\CANC\Blocks\DataAccess\DataAccessCore.cs" lang="cs"/>
	/// </remarks>
	public abstract class DataAccessCore
	{
		static private bool _debugTrace = true;
		private readonly string _connectionString;

		#region private utility methods & constructors

		/// <summary>
		/// This method can be overridden to provide additional processing on the connection string 
		/// property. This is useful if for example you database is specified by way of a relative 
		/// directory which is no know at configuration time. For example accessing a Server path 
		/// in an http context. 
		///</summary>
		/// <remarks>
		/// <example>
		/// <p>Example override method using web running in a web enviroment</p>
		/// <code outlining="true" lang="cs" >
		/// if (connectionString.IndexOf("%MAPPATH%") > 0)
		/// {
		///     string serverpath = System.Web.HttpContext.Current.Server.MapPath("");
		///     connectionString = connectionString.Replace("%MAPPATH%", serverpath);
		/// }
		/// </code>
		/// </example>
		/// </remarks>
		/// <param name="connectionString"> the connection string as configured in the connectionStrings element of the config file</param>
		/// <returns> the default method returns the connection string as stored in the application configuration file</returns>
		protected string ResolveConnectionStringValue(string connectionString)
		{
			return connectionString;
		}


		/// <summary>
		/// This is a protected constructor which must be called by the inheriting class, bu default it will get the configuration 
		/// value stored in connectionStrings element of the configuration. This value can be overriden using the ResolveConnectionStringValue method. 
		/// </summary>
		/// <param name="dataServiceName"></param>
		protected DataAccessCore(string dataServiceName)
		{
			_connectionString = ConfigurationManager.ConnectionStrings[dataServiceName].ConnectionString;
			_connectionString = ResolveConnectionStringValue(_connectionString);
		}
	
		/// <summary>
		/// The WrapUp procedure is called when completing a database  call. It will establish 
		/// whether or not to close the connection pending the variable closeConnection which 
		/// would have been passed back from the PrepCommand. The PrepCommand and WrapUp work 
		/// in tandem when executing commands though this common class. 
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="closeConnection"></param>
		protected void WrapUp(IDbConnection conn, bool closeConnection)
		{
			if (closeConnection)
			{
				if (conn != null
					&& (conn.State == ConnectionState.Open
					|| conn.State == ConnectionState.Executing
					|| conn.State == ConnectionState.Fetching)
					)
				{
					conn.Close();
				}
			}
		}

		//TODO make a virtual method and include a timespan on the execute time of the query
		private void TraceDBStatement(IDbCommand cmd)
		{
			//System.Diagnostics.Debug.WriteLine(cmd.CommandText);
		}

		#endregion private utility methods & constructors

		#region abstract methods that must me implemented


		protected abstract DbDataAdapter GetDataAdapter(IDbCommand cmd);

		protected abstract bool PrepCommand(IDbCommand cmd);

		#endregion

		protected string ConnectionString
		{
			get {return _connectionString;}
		}

		#region ExecuteNonQuery

		protected int ExecuteNonQuery(IDbCommand cmd)
		{
			bool closeconection = PrepCommand(cmd);
			int rowCount = cmd.ExecuteNonQuery();
			if (_debugTrace)
				TraceDBStatement(cmd);
			WrapUp(cmd.Connection, closeconection);
			return rowCount;
		}
		#endregion ExecuteNonQuery

		#region ExecuteDataSet
		protected DataSet ExecuteDataSet(IDbCommand cmd)
		{
			DataSet ds = new DataSet();
			return (ExecuteDataSet(cmd, ds, "Results"));
		}


		protected DataSet ExecuteDataSet(IDbCommand cmd, DataSet ds, string sTableNames)
		{
			// full ownership of the connection
			bool closeconection = PrepCommand(cmd);
			string[] sTableNNameArray = sTableNames.Split(',');

			using (DbDataAdapter theDataAdapter = GetDataAdapter(cmd))
			{
				for(int i = 1; i < sTableNNameArray.Length; i++)
				{
					theDataAdapter.TableMappings.Add(sTableNNameArray[0].Trim() + Convert.ToString(i), sTableNNameArray[i].Trim());
				}
				theDataAdapter.Fill(ds, sTableNNameArray[0].Trim());
			}
			WrapUp(cmd.Connection, closeconection);
			if (_debugTrace)
				TraceDBStatement(cmd);
			return (ds);
		}

		#endregion ExecuteDataSet

		#region ExecuteTable

		protected DataTable ExecuteDataTable(IDbCommand cmd)
		{
			DataSet ds = ExecuteDataSet(cmd);
			return(ds.Tables[0]);
		}

		#endregion ExecuteTable

		#region ExecuteDataRow

		protected DataRow ExecuteDataRow(IDbCommand cmd)
		{
			DataSet ds = ExecuteDataSet(cmd);
			DataRow dr = null;
			if (ds.Tables[0].Rows.Count > 0)
			{
                if (ds.Tables[0].Rows.Count == 1)
				    dr = ds.Tables[0].Rows[0];
                else
                    throw new DataException("Command was asked to execute a ExecuteDataRow however more than a single data row was found, ExecuteDataRow expects one or zero rows returned");
			}
			return(dr);
		}

		#endregion ExecuteDataRow

		#region ExecuteDictionary 

		protected IDictionary ExecuteDictionary(IDbCommand cmd)
		{
			Hashtable dictionary = new Hashtable();
			DataTable dt  = ExecuteDataTable(cmd);
			if (dt.Rows.Count > 0) 
			{
				for (int counter = 0; counter < dictionary.Count; counter++)
				{
					dictionary[dt.Columns[counter].ColumnName] = dt.Rows[0].ItemArray[counter];
				}
			}
			return (dictionary);
		}

		#endregion ExecuteDictionary 

		#region ExecuteScalar

		protected object ExecuteScalar(IDbCommand cmd)
		{
			PrepCommand(cmd);
			object rv = (cmd.ExecuteScalar());
			if (_debugTrace)
				TraceDBStatement(cmd);
			cmd.Connection.Close();
			return rv;
		}

        protected string ExecuteScalarAsString(IDbCommand cmd, string nullDefault = null)
        {
            Object result = ExecuteScalar(cmd);
            return result != null ? result.ToString() : nullDefault;
        }

        protected int ExecuteScalarAsInt(IDbCommand cmd, int nullDefault = 0)
        {
            Object result = ExecuteScalar(cmd);
            return result != null ? int.Parse(result.ToString()) : nullDefault;
        }

        protected short ExecuteScalarAsShort(IDbCommand cmd, short nullDefault = 0)
        {
            Object result = ExecuteScalar(cmd);
            return result != null ? short.Parse(result.ToString()) : nullDefault;
        }

		protected byte ExecuteScalarAsByte(IDbCommand cmd, byte nullDefault = 0)
		{
			Object result = ExecuteScalar(cmd);
			return result != null ? byte.Parse(result.ToString()) : nullDefault;
		}

        protected long ExecuteScalarAsLong(IDbCommand cmd, long nullDefault = 0)
        {
            Object result = ExecuteScalar(cmd);
            return result != null ? long.Parse(result.ToString()) : nullDefault;
        }

        protected Guid ExecuteScalarAsGuid(IDbCommand cmd, Guid nullDefault)
        {
            Object result = ExecuteScalar(cmd);
            return result != null ? Guid.Parse(result.ToString()) : nullDefault;
        }

	    protected Guid ExecuteScalarAsGuid(IDbCommand cmd)
	    {
	        return ExecuteScalarAsGuid(cmd, Guid.Empty);
	    }


	    #endregion ExecuteScalar

		#region ExecuteReader
		protected IDataReader ExecuteReader(IDbCommand cmd)
		{
			PrepCommand(cmd);
			return(cmd.ExecuteReader(CommandBehavior.CloseConnection));
		}

		#endregion ExecuteReader

        
        protected dynamic ExecuteObject(IDbCommand cmd)
        {
            var translator = new DynamicDbRow2ObjectTranslator();
            return translator.Translate(ExecuteDataRow(cmd));
        }

        protected IList<dynamic> ExecuteObjectList(IDbCommand cmd)
        {
            var translator = new DynamicDbRow2ObjectTranslator();
            return translator.Translate(ExecuteDataTable(cmd));
        } 
	}
}
