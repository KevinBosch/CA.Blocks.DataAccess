﻿using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Translator.Basic;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer.DbTypeTests
{
    [TestFixture]
    public class DbTypeIntTests : UnitTestDataAccess
    {
        private void InsertTestDataSQL(int data)
        {
            ExecuteNonQuery(InsertTestDataSQL(data.ToString()));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ExecuteNonQuery(DropTestTableSQL());
            ExecuteNonQuery(CreateTestTable("int not null"));
            InsertTestDataSQL(-1);
            InsertTestDataSQL(0);
            InsertTestDataSQL(123);
            InsertTestDataSQL(246);
            InsertTestDataSQL(int.MaxValue);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ExecuteNonQuery(DropTestTableSQL());
        }

        [Test]
        public void SelectAllDataInt()
        {
            //Setup 
            var t = new IntTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL());
            //Act
            var data = t.Translate(ExecuteDataTable(cmd));
            //Assert
            Assert.AreEqual(5, data.Count);
        }

        [Test]
        public void SelectAllDataIntWithFilter ()
        {
            //setup
            const int testvalue = 123; 
            var t = new IntTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col >= @testValue");
            cmd.Parameters.Add(testvalue.ToSqlParameter("@testValue"));

            //Act
            var data = t.Translate(ExecuteDataTable(cmd));

            //Asert
            Assert.AreEqual(3, data.Count);
        }


    }
}
