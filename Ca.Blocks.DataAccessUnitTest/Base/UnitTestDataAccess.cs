using System.ComponentModel;
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
    public class UnitTestDataAccess : SqlServerDataAccess
    {
        public UnitTestDataAccess()
            : base("localsqlserverhost")
        {
        }


        private const string unitTestTableName = "CA_BLOCKS_UNITTEST_TEMP_TESTTABLE";
        public const string UNIT_TEST_COL_NAME = "col";

        protected string DropTestTableSQL()
        {
            return
                string.Format(
                    "if exists (select * from sysobjects where xtype = 'U' and id = object_id(N'{0}')) begin drop table {0} end",
                    unitTestTableName);
        }

        protected string CreateTestTable(string coltype)
        {
            return
                string.Format(
                    "Create table {0} (id int identity(1,1), col {1} )",
                    unitTestTableName, coltype);

        }

        protected string InsertTestDataSQL(string data)
        {
            return
                string.Format(
                    "Insert into {0}  values ({1})",
                    unitTestTableName, data);
        }

        protected string SelectTestDataSQL()
        {
            return
                string.Format("Select col from {0} /*##FILTER##*/", unitTestTableName);
        }

        protected string SelectTestDataSQL( string where)
        {
            return
                string.Format("Select col from {0} {1}", unitTestTableName, where);
        }


        // This is a backdoor used for unit testing to setup and teardown test data in the local sql server
        //  this is a helper function and bypasses all the security features around the block.
        public void ExecuteNonQuery(string query)
        {
            SqlCommand cmd = CreateTextCommand(query);
            ExecuteNonQuery(cmd); 
        }
    }
}
