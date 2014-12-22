using System.Diagnostics;
using System.Text;
using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Translator.Basic;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer.DbTypeTests
{
    [TestFixture]
    public class DbTypeVarBinaryTests : UnitTestDataAccess
    {
        private const string  TEST_DATA = "Binary Data 1";

        private void InsertTestDataAsBinarySQL(string data)
        {
            ExecuteNonQuery(InsertTestDataSQL(string.Format("CAST('{0}' as varbinary(50))", data)));
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            ExecuteNonQuery(DropTestTableSQL());
            ExecuteNonQuery(CreateTestTable("varbinary(50) not null"));
            InsertTestDataAsBinarySQL(TEST_DATA);
            InsertTestDataAsBinarySQL("Binary Data 2");
            InsertTestDataAsBinarySQL("Binary Data 3");
            InsertTestDataAsBinarySQL("Binary Data 4");
            InsertTestDataAsBinarySQL("Binary Data 5");
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            ExecuteNonQuery(DropTestTableSQL());
        }

        [Test]
        public void SelectAllDataBinary()
        {
            //Setup 
            var t = new BinaryTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL());
            //Act
            var data = t.Translate(ExecuteDataTable(cmd));
            //Assert
            Assert.AreEqual(5, data.Count);
            Assert.AreEqual(TEST_DATA, Encoding.ASCII.GetString(data[0]));
        }

        
        [Test]
        public void SelectDataBinaryWithFilter()
        {
            //setup
            byte[] testvalue = Encoding.ASCII.GetBytes(TEST_DATA);
            var t = new BinaryTranslator(UNIT_TEST_COL_NAME);
            var cmd = CreateTextCommand(SelectTestDataSQL(), "Where col = @testValue");
            cmd.Parameters.Add(testvalue.ToSqlParameter("@testValue"));

            //Act
            var data = t.Translate(ExecuteDataTable(cmd));

            //Asert
            Assert.AreEqual(1, data.Count);
        }


    }
}
