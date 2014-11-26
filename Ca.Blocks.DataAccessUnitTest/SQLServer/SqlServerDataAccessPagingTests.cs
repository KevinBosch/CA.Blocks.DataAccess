using System.Data;
using CA.Blocks.DataAccess.Paging;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    [TestFixture]
    public class SqlServerDataAccessPagingTests
    {
        [Test]
        public void GetBasicPagingRequest()
        {
            var target = new UnitTestDataAccess();
            DataTable dt = target.ExecuteDataTable("Select * from sysobjects", new PagingRequest(10, 0, "ID"));
            Assert.AreEqual(10, dt.Rows.Count);
        }
    }


}
