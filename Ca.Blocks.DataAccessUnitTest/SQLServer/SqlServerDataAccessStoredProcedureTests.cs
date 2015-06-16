using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using CA.Blocks.DataAccess.Paging;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    [TestFixture]
    public class SqlServerDataAccessStoredProcedureTests : UnitTestDataAccess
    {
        [Test]
        public void ExecuteSpwhoWithNoReturnValue()
        {
            SqlCommand cmd = CreateBlankStoredProcedureCommand("sp_who", false);
            var result = ExecuteDataTable(cmd);
            Assert.IsTrue(result.Rows.Count > 0);
            ////VIEWOUTPUT
            // Trace.Write(DataTableToText(result));
        }


        [Test]
        public void ExecuteSpwhoWithReturnValue()
        {
            SqlCommand cmd = CreateBlankStoredProcedureCommand("sp_who", true);
            var result = ExecuteDataTable(cmd);
            int spresult = this.GetStoredProcedureReturnValue(cmd);
            Assert.AreEqual(0, spresult);
            Assert.IsTrue(result.Rows.Count > 0);
        }
    }


}
