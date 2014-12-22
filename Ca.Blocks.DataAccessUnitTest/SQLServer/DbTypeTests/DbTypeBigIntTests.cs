using System.Collections.Generic;
using System.Data.SqlClient;
using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Translator.Basic;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer.DbTypeTests
{
    [TestFixture]
    public class DbTypeBigIntTests : UnitTestDataAccess
    {
        private void InsertTestDataSQL(long data)
        {
            ExecuteNonQuery(InsertTestDataSQL(data.ToString()));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ExecuteNonQuery(DropTestTableSQL());
            ExecuteNonQuery(CreateTestTable("bigint not null"));
            InsertTestDataSQL(-1);
            InsertTestDataSQL(0);
            InsertTestDataSQL(123);
            InsertTestDataSQL(246);
            InsertTestDataSQL((long)int.MaxValue + (long)int.MaxValue);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ExecuteNonQuery(DropTestTableSQL());
        }

        [Test]
        public void SelectAllDataBigInt()
        {
            //Setup 
            var t = new LongTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL());
            //Act
            var data = t.Translate(ExecuteDataTable(cmd));
            //Assert
            Assert.AreEqual(5, data.Count);
        }

        [Test]
        public void SelectAllDataBigIntWithFilter ()
        {
            //setup
            const long testvalue = 123; 
            var t = new LongTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col >= @testValue");
            cmd.Parameters.Add(testvalue.ToSqlParameter("@testValue"));

            //Act
            var data = t.Translate(ExecuteDataTable(cmd));

            //Asert
            Assert.AreEqual(3, data.Count);
        }

        
        [Test]
        public void SelectAllDataBigIntWithFilterAndSugger()
        {
            //setup
            const long testvalue = 123;
            var t = new LongTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col >= @testValue")
                .WithParameters(new List<SqlParameter>
                {
                    testvalue.ToSqlParameter("@testValue")
                });

            //Act
            var data = t.Translate(ExecuteDataTable(cmd));

            //Asert
            Assert.AreEqual(3, data.Count);
        }

    }
}
