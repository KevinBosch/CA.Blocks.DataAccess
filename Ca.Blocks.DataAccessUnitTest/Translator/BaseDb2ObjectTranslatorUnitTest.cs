using System;
using System.Data.SqlClient;
using CA.Blocks.DataAccess.Translator;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.Translator
{
    public class TestClassChar
    {
        public char Col { get; set; }
    }

    public class TestClassTimeSpan
    {
        public TimeSpan Col { get; set; }
    }

    [TestFixture]
    public class BaseDb2ObjectTranslatorUnitTest : UnitTestDataAccess
    {
        //private const string unitTestTableName = "CA_BLOCKS_UNITTEST_TEMP_TESTTABLE";

    
        #region Char
        [Test]
        public void BaseDb2ObjectTranslatorTestCharMapping()
        {
            var da = new UnitTestDataAccess();
            da.ExecuteNonQuery(DropTestTableSQL());
            da.ExecuteNonQuery(CreateTestTable("char(1)"));
            da.ExecuteNonQuery(InsertTestDataSQL("'T'"));

            var target = new BaseDb2ObjectTranslator<TestClassChar>();
            SqlCommand cmd = CreateTextCommand(SelectTestDataSQL());
            var result = target.Translate(ExecuteDataRow(cmd));
            
            Assert.AreEqual('T', result.Col);

            da.ExecuteNonQuery(DropTestTableSQL());
        }

        #endregion 

        #region timespan

        [Test]
        public void BaseDb2ObjectTranslatorTestTimeSpanMapping()
        {
            var da = new UnitTestDataAccess();
            da.ExecuteNonQuery(DropTestTableSQL());
            da.ExecuteNonQuery(CreateTestTable("time(0)"));
            da.ExecuteNonQuery(InsertTestDataSQL("'01:02:03'"));

            var target = new BaseDb2ObjectTranslator<TestClassTimeSpan>();
            var cmd = CreateTextCommand(SelectTestDataSQL());
            var result = target.Translate(ExecuteDataRow(cmd));

            Assert.AreEqual(new TimeSpan(1,2,3), result.Col);

            da.ExecuteNonQuery(DropTestTableSQL());
        }


        #endregion

        #region more to come when i get time

        #endregion 
    }
}
