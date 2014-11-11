﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CA.Blocks.DataAccess;

namespace CA.Blocks.DataAccessUnitTest.Base
{
    /*
         <configuration>
            <connectionStrings>    
                <add name="localsqlserverhost" connectionString="Server=(local);Database=tempdb;Integrated Security=SSPI" providerName="System.Data.SqlClient"/>
            </connectionStrings>
         </configuration>
         */
    // this class exposes the internal workings so we can test
    internal class UnitTestDataAccess : SqlServerDataAccess
    {
        public UnitTestDataAccess()
            : base("localsqlserverhost")
        {
        }

        public dynamic ExecuteObject(string query)
        {
            SqlCommand cmd = CreateTextCommand(query);
            return base.ExecuteObject(cmd);
        }

        public IList<dynamic> ExecuteObjectList(string query)
        {
            SqlCommand cmd = CreateTextCommand(query);
            return base.ExecuteObjectList(cmd);
        }

        public DataTable ExecuteDataTable(string query)
        {
            SqlCommand cmd = CreateTextCommand(query);
            return base.ExecuteDataTable(cmd);
        }

        public DataRow ExecuteDataRow(string query)
        {
            SqlCommand cmd = CreateTextCommand(query);
            return base.ExecuteDataRow(cmd);
        }

        public void ExecuteNonQuery(string query)
        {
            SqlCommand cmd = CreateTextCommand(query);
            base.ExecuteNonQuery(cmd); 
        }

    }
}