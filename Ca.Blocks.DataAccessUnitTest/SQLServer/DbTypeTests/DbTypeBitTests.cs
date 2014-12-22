using CA.Blocks.DataAccess;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer.DbTypeTests
{
    [TestFixture]
    public class DbTypeBitTests : UnitTestDataAccess
    {
        private void InsertTestDataSQL(bool data)
        {
            ExecuteNonQuery(InsertTestDataSQL(data? "1":"0"));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ExecuteNonQuery(DropTestTableSQL());
            ExecuteNonQuery(CreateTestTable("bit not null"));
            InsertTestDataSQL(true);
            InsertTestDataSQL(false);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ExecuteNonQuery(DropTestTableSQL());
        }

        [Test]
        public void SelectAllDataBitInt()
        {
            //Setup 
           
            var cmd = CreateTextCommand(SelectTestDataSQL());
            //Act
            var data = this.ExecuteObjectList(cmd);
            //Assert
            Assert.AreEqual(2, data.Count);
        }

        [Test]
        public void SelectAllDataBitIntWithFilter ()
        {
            //setup
            const bool testvalue = true;
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col = @testValue");
            cmd.Parameters.Add(testvalue.ToSqlParameter("@testValue"));

            //Act
            var data = this.ExecuteObjectList(cmd);

            //Asert
            Assert.AreEqual(1, data.Count);
        }


    }
}
