using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Paging;

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

        public DataTable ExecuteDataTable(string query, PagingRequest pq)
        {
            SqlCommand cmd = CreateTextCommand(query);
            return base.ExecuteDataTable(cmd, pq);
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

        //  This is done for unit testing to expose all the protected methods as public
        public new SqlCommand CreateTextCommand(string query)
        {
            return base.CreateTextCommand(query);
        }

        public DataTable ExecuteDataTable(SqlCommand cmd )
        {
            return base.ExecuteDataTable(cmd);
        }

    }
}
