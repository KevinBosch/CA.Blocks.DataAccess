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
using System.Data.OleDb;

namespace CA.Blocks.DataAccess
{
	/// <summary>
	/// Provides a Oledb implementation for DataAccessCore. This block can be used to connect to any Oledb exposed database. 
	/// </summary>
	/// <remarks>
	/// <p>source code:</p>
	/// <code outlining="true" source="..\CANC\Blocks\DataAccess\OledbDataAccess.cs" lang="cs"/> 
	/// </remarks>
	public class OledbDataAccess : DataAccessCore
	{

		public OledbDataAccess(string connectionString) : base(connectionString) 
		{
		}

		protected override bool PrepCommand(IDbCommand cmd)
		{
			OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
			oleDbConnection.Open();
			cmd.Connection = oleDbConnection;
			return true;
		}

		protected override DbDataAdapter GetDataAdapter(IDbCommand cmd)
		{
			return (new OleDbDataAdapter((OleDbCommand)cmd));
		}


		public static OleDbCommand CreateBlankStoredProcedureCommand(string strStoredProcedureName, bool bolIncludeReturnValue)
		{

			OleDbCommand olecmd = new OleDbCommand();
			olecmd.CommandText = strStoredProcedureName;
			olecmd.CommandType = CommandType.StoredProcedure;
			if (bolIncludeReturnValue)
			{
				OleDbParameter OleDbParameter;
				OleDbParameter = olecmd.CreateParameter();
				OleDbParameter.ParameterName = "Return";
				OleDbParameter.DbType = DbType.Int32;
				OleDbParameter.Direction = ParameterDirection.ReturnValue;
				olecmd.Parameters.Add(OleDbParameter);
			}
			return (olecmd);
		}

		public static OleDbCommand CreateBlankStoredProcedureCommand(string strStoredProcedureName)
		{
			return (CreateBlankStoredProcedureCommand(strStoredProcedureName, false));
		}

		public static OleDbCommand CreateDynamicSQLCommand(string strSQL)
		{
			OleDbCommand olecmd = new OleDbCommand();
			olecmd.CommandText = strSQL;
			olecmd.CommandType = CommandType.Text;
			return (olecmd);
		}

		#region ParemeterHelpers



		/// <summary>
		/// Adds a parameter to a pre existing stored procedure command.
		/// </summary>
		/// <param name="cmd"> The SQL command for which the parameter should be added</param>
		/// <param name="strParameterName">The Parameter Name. note the underlying oledb parameter
		/// must match the order of the parameter in the command this is constratint on oledb provider
		///  http://support.microsoft.com/default.aspx?scid=kb;en-us;316744 </param>
		/// <param name="objParameterValue"> The value of the Parameter</param>
		/// <param name="odbType"> the type of the parameter</param>
		/// <param name="maxParamSize"> The max size of the parameter </param>
		/// <returns>The sql parameter just added</returns>
		public OleDbParameter AddInputParamCommand(OleDbCommand cmd, string strParameterName, object objParameterValue, DbType odbType, int maxParamSize)
		{
			OleDbParameter oleparam = new OleDbParameter(strParameterName, odbType);
			oleparam.Direction = ParameterDirection.Input;

			if (maxParamSize > 0)
				oleparam.Size = maxParamSize;

			if (objParameterValue == null || objParameterValue == DBNull.Value)
			{
				oleparam.Value = DBNull.Value;
				//Added the following as sometimes the type is changed to int32
				oleparam.DbType = odbType;
			}
			else
				oleparam.Value = objParameterValue;
			cmd.Parameters.Add(oleparam);

			return (oleparam);
		}

		public static OleDbParameter AddOutputParamCommand(OleDbCommand cmd, string strParameterName, DbType odbType, Int32 maxParamSize)
		{
			OleDbParameter oleparam = new OleDbParameter(strParameterName, odbType);
			oleparam.Direction = ParameterDirection.Output;
			if (maxParamSize > 0)
				oleparam.Size = maxParamSize;
			cmd.Parameters.Add(oleparam);

			return (oleparam);
		}

		#endregion

	}
}
