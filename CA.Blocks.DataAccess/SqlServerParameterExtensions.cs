using System;
using System.Data;
using System.Data.SqlClient;

namespace CA.Blocks.DataAccess
{
    public static class SqlServerParameterExtensions
    {
        #region SqlDbType.BigInt;
        public static SqlParameter ToSqlParameter(this long input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Input,
                Size = 8,
                Value = input
            };
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this long? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Input,
                Size = 8,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }
        #endregion
 
        /*TODO
        SqlDbType.Binary;
        SqlDbType.Bit;
        SqlDbType.Char;
        SqlDbType.Date;
        SqlDbType.DateTime;
        SqlDbType.DateTime2;

        SqlDbType.DateTimeOffset;
        SqlDbType.Decimal;
        SqlDbType.Float;
        SqlDbType.Image;
        */
        #region SqlDbType.Int;

        private static SqlParameter ToSqlParameterInt(int? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Size = 4,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }
        
        public static SqlParameter ToSqlParameter(this int input, string strParameterName)
        {
            return ToSqlParameterInt(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this int? input, string strParameterName)
        {
            return ToSqlParameterInt(input, strParameterName);
        }
        #endregion 
        /*
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
