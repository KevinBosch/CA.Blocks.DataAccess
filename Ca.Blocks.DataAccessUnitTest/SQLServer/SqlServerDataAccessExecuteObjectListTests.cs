using System.Diagnostics;
using CA.Blocks.DataAccess.Translator;
using CA.Blocks.DataAccessUnitTest.Base;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    internal class Sysobjects
    {
        public string name { get; set; }
        public int id { get; set; }
        public string xtype { get; set; }
    }

    // Shows how to use the ExecuteObjectList to get a list of dyanmic objects from a SQL query.
    // This is handy for very quick development
    [TestFixture]
    public class SqlServerDataAccessExecuteObjectListTests
    {
        [Test]
        public void GetDataAsDyanmicList()
        {
            var target = new UnitTestDataAccess();

            var result = target.ExecuteObjectList("Select * from sysobjects");
            foreach (var o in result)
            {
                string s = o.name;
                Trace.WriteLine(s);
            }
        }

        // This will use the same dynamic function except it adds a little more work in converting the 
        // object into a known type rather than dyanmic
        [Test]
        public void GetDataAsFixedTypeFromDynamic()
        {
            var target = new UnitTestDataAccess();
            // Note we only support 1-1 mapping for now
            var t = new BaseDb2ObjectTranslator<Sysobjects>(); // <<-- need to have the ability to inject the map. 
      
            var result = t.Translate(target.ExecuteDataTable("Select * from sysobjects")); //<< inject a filter with parameters..

            foreach (var o in result)
            {
                string s = o.name;
                Trace.WriteLine(s);
            }
        }
    }


}
