using System;
using CA.Blocks.DataAccess;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    [TestFixture]
    public class SqlServerDataAccessExecuteScalarTests : SqlServerDataAccess
    {
        public SqlServerDataAccessExecuteScalarTests()
            : base("localsqlserverhost")
        {
        }


        [Test]
        public void ExecuteExecuteScalarByte()
        {
            // Setup
            var cmd = CreateTextCommand("Select Cast(1 as tinyint) as col");
            // act
            var result = ExecuteScalarAsByte(cmd);
            //Assert
            Assert.AreEqual((byte)1, result);
        }

        [Test]
        public void ExecuteExecuteScalarShort()
        {
            // Setup
            var cmd = CreateTextCommand("Select Cast(1 as smallint) as col");
            // act
            var result = ExecuteScalarAsShort(cmd);
            //Assert
            Assert.AreEqual((short)1, result);
        }

        [Test]
        public void ExecuteExecuteScalarInt()
        {
            // Setup
            var cmd = CreateTextCommand("Select Cast(1 as int) as col");
            // act
            var result = ExecuteScalarAsInt(cmd);
            //Assert
            Assert.AreEqual((int)1, result);
        }


        [Test]
        public void ExecuteExecuteScalarLong()
        {
            // Setup
            var cmd = CreateTextCommand("Select Cast(1 as bigint) as col");
            // act
            var result = ExecuteScalarAsLong(cmd);
            //Assert
            Assert.AreEqual((long)1, result);
        }


        [Test]
        public void ExecuteExecuteScalarGuid()
        {
            // Setup
            var cmd = CreateTextCommand("Select Cast('D79DB3C0-E5BE-4045-A37B-6DB923D37123' as uniqueidentifier) as col");
            // act
            var result = ExecuteScalarAsGuid(cmd);
            //Assert
            Assert.AreEqual(new Guid("D79DB3C0-E5BE-4045-A37B-6DB923D37123"), result);
        }


        [Test]
        public void ExecuteExecuteScalarString()
        {
            // Setup
            var cmd = CreateTextCommand("Select 'String Value' as col ");
            // act
            var result = ExecuteScalarAsString(cmd);
            //Assert
            Assert.AreEqual("String Value", result);
        }



    }
}
