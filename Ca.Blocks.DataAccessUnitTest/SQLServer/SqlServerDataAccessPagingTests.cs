using System.Data;
using System.Data.SqlClient;
using CA.Blocks.DataAccess.Paging;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    [TestFixture]
    public class SqlServerDataAccessPagingTests : UnitTestDataAccess
    {
        [Test]
        public void GetBasicPagingRequest()
        {
            var target = new UnitTestDataAccess();
            SqlCommand cmd = CreateTextCommand("Select * from sysobjects");
            DataTable dt = target.ExecuteDataTable(cmd, new PagingRequest(10, 0, "ID"));
            Assert.AreEqual(10, dt.Rows.Count);
        }
    }


}
