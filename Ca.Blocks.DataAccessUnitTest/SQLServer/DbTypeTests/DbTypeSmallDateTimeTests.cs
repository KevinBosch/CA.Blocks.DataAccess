using System;
using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Translator.Basic;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer.DbTypeTests
{
    [TestFixture]
    public class DbTypeSmallDateTimeTests : UnitTestDataAccess
    {
        private void InsertTestDataSQL(DateTime data)
        {
            ExecuteNonQuery(InsertTestDataSQL(string.Format("'{0}'",data.ToString("yyyy MMMM dd HH:mm:ss"))));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ExecuteNonQuery(DropTestTableSQL());
            ExecuteNonQuery(CreateTestTable("SmallDateTime not null"));
            InsertTestDataSQL(DateTime.Now.AddMinutes(1));
            InsertTestDataSQL(DateTime.Now.AddDays(1));
            InsertTestDataSQL(DateTime.Now.AddDays(-1));
            InsertTestDataSQL(DateTime.Now.AddDays(100));
            InsertTestDataSQL(DateTime.Now.AddDays(-100));
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ExecuteNonQuery(DropTestTableSQL());
        }

        [Test]
        public void SelectAllDataDateTime()
        {
            //Setup 
            var t = new DateTimeTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL());
            //Act
            var data = t.Translate(ExecuteDataTable(cmd));
            //Assert
            Assert.AreEqual(5, data.Count);
        }

        [Test]
        public void SelectAllDataDateTimeWithFilter()
        {
            //setup
            DateTime testvalue = DateTime.Now;
            var t = new DateTimeTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col >= @testValue");
            cmd.Parameters.Add(testvalue.ToSqlParameter("@testValue"));

            //Act
            var data = t.Translate(ExecuteDataTable(cmd));

            //Asert
            Assert.AreEqual(3, data.Count);
        }


    }
}
