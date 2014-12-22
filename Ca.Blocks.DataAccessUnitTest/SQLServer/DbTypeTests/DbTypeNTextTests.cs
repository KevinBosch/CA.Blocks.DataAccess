using System;
using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Translator.Basic;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer.DbTypeTests
{
    [TestFixture]
    public class DbTypeNTextTests : UnitTestDataAccess
    {
        private const string  TEST_DATA = "Ntext data";

        private void InsertTestDataAsBinarySQL(string data)
        {
            ExecuteNonQuery(InsertTestDataSQL(string.Format("'{0}'", data)));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ExecuteNonQuery(DropTestTableSQL());
            ExecuteNonQuery(CreateTestTable("NText not null"));
            InsertTestDataAsBinarySQL(TEST_DATA);
            InsertTestDataAsBinarySQL(Guid.NewGuid().ToString());
            InsertTestDataAsBinarySQL(Guid.NewGuid().ToString());
            InsertTestDataAsBinarySQL(Guid.NewGuid().ToString());
            InsertTestDataAsBinarySQL(Guid.NewGuid().ToString());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ExecuteNonQuery(DropTestTableSQL());
        }

        [Test]
        public void SelectAllDataText()
        {
            //Setup 
            var t = new StringTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL());
            //Act
            var data = t.Translate(ExecuteDataTable(cmd));
            //Assert
            Assert.AreEqual(5, data.Count);
            Assert.AreEqual(TEST_DATA, data[0]);
        }

        
        [Test]
        public void SelectDataTextWithFilter()
        {
            //setup
            const string testvalue = TEST_DATA;
            var t = new StringTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col like @testValue");
            cmd.Parameters.Add(testvalue.ToSqlParameter("@testValue", SpecificSQLStringType.NText));

            //Act
            var data = t.Translate(ExecuteDataTable(cmd));

            //Asert
            Assert.AreEqual(1, data.Count);
        }

    }
}
