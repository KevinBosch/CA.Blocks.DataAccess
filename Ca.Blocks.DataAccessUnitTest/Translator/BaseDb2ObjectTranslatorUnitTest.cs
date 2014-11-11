using System;
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
    public class BaseDb2ObjectTranslatorUnitTest
    {
        private const string unitTestTableName = "CA_BLOCKS_UNITTEST_TEMP_TESTTABLE";

        private string DropTestTableSQL()
        {
            return
                string.Format(
                    "if exists (select * from sysobjects where xtype = 'U' and id = object_id(N'{0}')) begin drop table {0} end",
                    unitTestTableName);
        }

        private string CreateTestTable(string coltype)
        {
            return
                string.Format(
                    "Create table {0} (col {1} )",
                    unitTestTableName, coltype);

        }

        private string InsertTestDataSQL(string data)
        {
            return
                string.Format(
                    "Insert into {0}  values ({1})",
                    unitTestTableName, data);
        }

        private string SelectTestDataSQL()
        {
            return
                string.Format("Select col from {0}", unitTestTableName);
        }
        #region Char
        [Test]
        public void BaseDb2ObjectTranslatorTestCharMapping()
        {
            var da = new UnitTestDataAccess();
            da.ExecuteNonQuery(DropTestTableSQL());
            da.ExecuteNonQuery(CreateTestTable("char(1)"));
            da.ExecuteNonQuery(InsertTestDataSQL("'T'"));

            var target = new BaseDb2ObjectTranslator<TestClassChar>();

            var result = target.Translate(da.ExecuteDataRow(SelectTestDataSQL()));
            
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

            var result = target.Translate(da.ExecuteDataRow(SelectTestDataSQL()));

            Assert.AreEqual(new TimeSpan(1,2,3), result.Col);

            da.ExecuteNonQuery(DropTestTableSQL());
        }


        #endregion

        #region more to come when i get time

        #endregion 
    }
}
