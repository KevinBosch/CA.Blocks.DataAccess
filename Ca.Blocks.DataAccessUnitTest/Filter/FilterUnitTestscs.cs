using CA.Blocks.DataAccess;
using CA.Blocks.DataAccess.Filter;
using NUnit.Framework;


namespace CA.Blocks.DataAccessUnitTest.Filter
{
    public class TestFilter : BaseFilterSegment
    {
        public void HasIntColFilter(int value)
        {
            AddFilter("intCol = @value", value.ToSqlParameter("@value"));
        }

        public void HasShortColFilterBadColName(short value)
        {
            AddFilter("ShortCol = @value", value.ToSqlParameter("@value"));
        }
    }


    [TestFixture]
    public class FilterUnitTestscs
    {
        [Test]
        public void BasicTest()
        {
            var target = new TestFilter();
            target.HasIntColFilter(123);
            Assert.IsTrue(target.Parameters.Count == 1);
            Assert.AreEqual("intCol = @value", target.ToSQLFilter());
        }

        [Test]
        public void BasicTestWithWhere()
        {
            var target = new TestFilter();
            target.HasIntColFilter(123);
            Assert.IsTrue(target.Parameters.Count == 1);
            Assert.AreEqual("WHERE intCol = @value", target.ToSQLFilter(true));
        }


        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void BasicBadFilterDiffrentTypes()
        {
            var target = new TestFilter();
            target.HasIntColFilter(123);
            target.HasShortColFilterBadColName(123);
            Assert.Fail();
        }

        [Test]
        [ExpectedException(typeof(System.ApplicationException))]
        public void HasIntColFilter()
        {
            var target = new TestFilter();
            target.HasIntColFilter(123);
            target.HasIntColFilter(456);
            Assert.Fail();
        }

        [Test]
        public void BasicTestSillyAnd()
        {
            var target = new TestFilter();
            target.HasIntColFilter(123);
            target.HasIntColFilter(123);
            Assert.IsTrue(target.Parameters.Count == 1);
            Assert.AreEqual("intCol = @value And intCol = @value", target.ToSQLFilter());
        }

    }
}
