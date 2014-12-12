using System;
using System.CodeDom.Compiler;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    // Shows how to pass a Context to SQL on each execute  for unit testing this is simply a random guid
    // in most cases this will be the security context like user name. This is usefull incase you want to 
    // do auditing, however the using the 

    internal class ContextUnitTestDataAccess : UnitTestDataAccess
    {
        private readonly string _randomContext = Guid.NewGuid().ToString();

        public string Context
        {
            get { return _randomContext; }
        }

        // To pass the context simply override the GetConnectionContext and return the string value you what to pass in.
        // from within SQL you can then use Select Cast(CONTEXT_INFO() as varchar(100)) to get the value backout
        protected override string GetConnectionContext()
        {
            return _randomContext;
        }

        public string GetContextFromDataBase()
        {
            var cmd = CreateTextCommand("Select Cast(CONTEXT_INFO() as varchar(100)) as CONTEXTINFO");
            string result = ExecuteScalar(cmd).ToString();
            return result.Replace("\0", string.Empty);
        }
    }



    [TestFixture]
    public partial class SqlServerDataAccessContextTests
    {
        [Test]
        public void ContextDataTest()
        {
            var target = new ContextUnitTestDataAccess();

            var result = target.GetContextFromDataBase();

            Assert.AreEqual(result, target.Context);
        }

    }


}
