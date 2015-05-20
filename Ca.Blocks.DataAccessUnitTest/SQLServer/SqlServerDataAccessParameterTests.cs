using CA.Blocks.DataAccess;
using NUnit.Framework;

namespace CA.Blocks.DataAccessUnitTest.SQLServer
{
    [TestFixture]
    public class SqlServerDataAccessParameterTests : SqlServerDataAccess
    {
        public SqlServerDataAccessParameterTests()
            : base("localsqlserverhost")
        {
        }

        /* TODO
        SqlDbType.BitInt;
        SqlDbType.Binary;
        SqlDbType.Bit;
        SqlDbType.Char;
        SqlDbType.Date;
        SqlDbType.DateTime;
        SqlDbType.DateTime2;

        SqlDbType.DateTimeOffset;
        SqlDbType.Decimal;
         */

        #region SqlDbType.Float 

        [Test]
        public void QueryWithParameterDouble()
        {
            // Setup
            double testNumber = 1.23;
            var cmd = CreateTextCommand("Select * from [sys].[dm_db_missing_index_group_stats] where avg_total_user_cost > @testNumber");
            // In the Query Above we have specified a @testid Now pass in the parameter
            cmd.Parameters.Add(testNumber.ToSqlParameter("@testNumber"));

            // act
            var list = ExecuteObjectList(cmd);

            //Assert
            foreach (var item in list)
            {
                Assert.IsTrue(item.avg_total_user_cost > testNumber);
            }
        }



        #endregion 

        /*
        SqlDbType.Float;
         */
        /*
        SqlDbType.Image;
        */

        [Test]
        public void QueryWithParameterInt32()
        {
            // Setup
            const int testid = 123;
            var cmd = CreateTextCommand("Select id, Name from sysobjects where id > @testid");
            // In the Query Above we have specified a @testid Now pass in the parameter
            cmd.Parameters.Add(testid.ToSqlParameter("@testid"));
            
            // act
            var list = ExecuteObjectList(cmd);

            //Assert
            foreach (var item in list)
            {
                Assert.IsTrue(item.id > testid);
            }
        }


        [Test]
        public void QueryWithParameterNullInt32()
        {
            // Setup
            int? testid = 123;
            var cmd = CreateTextCommand("Select id, Name from sysobjects where id > @testid");
            // In the Query Above we have specified a @testid Now pass in the parameter
            cmd.Parameters.Add(testid.ToSqlParameter("@testid"));

            // act
            var list = ExecuteObjectList(cmd);

            //Assert
            foreach (var item in list)
            {
                Assert.IsTrue(item.id > testid.Value);
            }
        }


        /* TODO 
            SqlDbType.Money;
            SqlDbType.NChar;
            SqlDbType.NText;
            SqlDbType.NVarChar;
            SqlDbType.Real;
            SqlDbType.SmallDateTime;
            SqlDbType.SmallInt;
            SqlDbType.SmallMoney;
            SqlDbType.Structured;
            SqlDbType.Text;
            SqlDbType.Time;
            SqlDbType.Timestamp;
            SqlDbType.TinyInt;
            SqlDbType.Udt;
            SqlDbType.UniqueIdentifier;
            SqlDbType.VarBinary;
            SqlDbType.VarChar;
            SqlDbType.Variant;
            SqlDbType.Xml;
            */


    }
}
