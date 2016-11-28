using System.Data;
using CA.Blocks.DataAccess;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest
{
    [TestFixture]
    public class SqlParameterExtensionsTests
    {

        [Test]
        public void ToSqlParameterInt32()
        {
            // Setup
            int target = 123;
            // Act
            var sqlparam = target.ToSqlParameter("@target");
            //Asert
            Assert.AreEqual(DbType.Int32, sqlparam.DbType);
            Assert.AreEqual(ParameterDirection.Input, sqlparam.Direction);
            Assert.AreEqual(false, sqlparam.IsNullable);
            Assert.AreEqual("@target", sqlparam.ParameterName);
            Assert.AreEqual(4, sqlparam.Size);
            Assert.AreEqual(target, sqlparam.Value);
        }

        [Test]
        public void ToSqlParameterSameNameInt32()
        {
            // Setup
            int? target = 123;
            // Act
            var sqlparam = target.ToSqlParameter("@target");
            //Asert
            Assert.AreEqual(DbType.Int32, sqlparam.DbType);
            Assert.AreEqual(ParameterDirection.Input, sqlparam.Direction);
            Assert.AreEqual(false, sqlparam.IsNullable);
            Assert.AreEqual("@target", sqlparam.ParameterName);
            Assert.AreEqual(4, sqlparam.Size);
            Assert.AreEqual(target, sqlparam.Value);
        }

        [Test]
        public void ToSqlParameterStringTest()
        {
            // Setup
            string testdata = "01234567890123456789";
            // Act
            var sqlparam = testdata.ToSqlParameter("@test");
            //Asert
            Assert.AreEqual(DbType.AnsiString, sqlparam.DbType);
            Assert.AreEqual(ParameterDirection.Input, sqlparam.Direction);
            Assert.AreEqual(false, sqlparam.IsNullable);
            Assert.AreEqual("@test", sqlparam.ParameterName);
            Assert.AreEqual(testdata, sqlparam.Value);
        }

        [Test]
        public void ToSqlParameterStringTestTrim()
        {
            // Setup
            string testdata = "01234567890123456789";
            // Act
            var sqlparam = testdata.ToSqlParameter("@test", trimInputTo:15);
            //Asert
            Assert.AreEqual(DbType.AnsiString, sqlparam.DbType);
            Assert.AreEqual(ParameterDirection.Input, sqlparam.Direction);
            Assert.AreEqual(false, sqlparam.IsNullable);
            Assert.AreEqual("@test", sqlparam.ParameterName);
            Assert.AreEqual("012345678901234", sqlparam.Value);
        }

    }
}
