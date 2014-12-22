using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace CA.Blocks.DataAccess
{
    public enum SpecificSQLDateTimeType
    {
        Date,
        DateTime, // The default
        DateTime2,
        SmallDateTime
    }

    public enum SpecificSQLStringType
    {
        [System.Obsolete("Backwards compatibility only they will be removed from SQL server http://msdn.microsoft.com/en-nz/library/ms187993.aspx")]
        NText,
        NVarChar,
        [System.Obsolete("Backwards compatibility only they will be removed from SQL server http://msdn.microsoft.com/en-nz/library/ms187993.aspx")]
        Text,
        VarChar, // The Default
    }

    public enum SpecificSQLCharType
    {
       Char, // default 
       NChar
    }

    public static class SqlServerParameterExtensions
    {
        public static SqlCommand WithParameters(this SqlCommand cmd, IList<SqlParameter> parameters)
        {
            cmd.Parameters.AddRange(parameters.ToArray());
            return cmd;
        }

        #region SqlDbType.BigInt;
        private static SqlParameter ToSqlParameterBigInt(long? input, string strParameterName)
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

        public static SqlParameter ToSqlParameter(this long input, string strParameterName)
        {
            return ToSqlParameterBigInt(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this long? input, string strParameterName)
        {
            return ToSqlParameterBigInt(input, strParameterName);
        }
        #endregion


        #region SqlDbType.Binary / SqlDbType.VarBinary  ( We dont know the size so VarBinary and Binary are the same

        public static SqlParameter ToSqlParameter(this byte[] input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.VarBinary)
            {
                Direction = ParameterDirection.Input,
            };
            if (input != null)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }
        #endregion


        #region SqlDbType.Bit;
        private static SqlParameter ToSqlParameterBool(bool? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.Bit)
            {
                Direction = ParameterDirection.Input,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this bool input, string strParameterName)
        {
            return ToSqlParameterBool(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this bool? input, string strParameterName)
        {
            return ToSqlParameterBool(input, strParameterName);
        }
        #endregion


        #region SqlDbType.Char;


        private static SqlDbType ToSqlDbType(SpecificSQLCharType dbType)
        {
            return dbType == SpecificSQLCharType.Char ? SqlDbType.Char : SqlDbType.NChar;
        }


        private static SqlParameter ToSqlParameterChar(Char? input, string strParameterName, SpecificSQLCharType dbType)
        {
            var sqlparam = new SqlParameter(strParameterName, ToSqlDbType(dbType))
            {
                Direction = ParameterDirection.Input,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this Char input, string strParameterName, SpecificSQLCharType dbType = SpecificSQLCharType.Char)
        {
            return ToSqlParameterChar(input, strParameterName, dbType);
        }

        public static SqlParameter ToSqlParameter(this Char? input, string strParameterName, SpecificSQLCharType dbType = SpecificSQLCharType.Char)
        {
            return ToSqlParameterChar(input, strParameterName, dbType);
        }
        #endregion


        #region SqlDbType.DateTime;
        private static SqlDbType ToSqlDbType(SpecificSQLDateTimeType dbType)
        {
            switch (dbType)
            {
                case SpecificSQLDateTimeType.DateTime:
                {
                    return SqlDbType.DateTime;
                }
                case SpecificSQLDateTimeType.Date:
                {
                    return SqlDbType.Date;
                }
                case SpecificSQLDateTimeType.DateTime2:
                {
                    return SqlDbType.DateTime2;
                }
                case SpecificSQLDateTimeType.SmallDateTime:
                {
                    return SqlDbType.SmallDateTime;
                }
                default:
                    return SqlDbType.DateTime;
            }
        }

        private static SqlParameter ToSqlParameterDateTime(DateTime? input, string strParameterName, SpecificSQLDateTimeType dbType)
        {
            var sqlparam = new SqlParameter(strParameterName, ToSqlDbType(dbType))
            {
                Direction = ParameterDirection.Input
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this DateTime input, string strParameterName)
        {
            return ToSqlParameterDateTime(input, strParameterName, SpecificSQLDateTimeType.DateTime);
        }

        public static SqlParameter ToSqlParameter(this DateTime input, string strParameterName, SpecificSQLDateTimeType dbType)
        {
            return ToSqlParameterDateTime(input, strParameterName, dbType);
        }

        // Default to DateTime
        public static SqlParameter ToSqlParameter(this DateTime? input, string strParameterName)
        {
            return ToSqlParameterDateTime(input, strParameterName, SpecificSQLDateTimeType.DateTime);
        }

        public static SqlParameter ToSqlParameter(this DateTime? input, string strParameterName, SpecificSQLDateTimeType dbType)
        {
            return ToSqlParameterDateTime(input, strParameterName, dbType);
        }
        #endregion

        
        /*TODO
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
        SqlDbType.Real;
        SqlDbType.SmallDateTime;*/


        #region SqlDbType.SmallInt;
        private static SqlParameter ToSqlParameterInt16(Int16? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.SmallInt)
            {
                Direction = ParameterDirection.Input,
                Size = 2,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this Int16 input, string strParameterName)
        {
            return ToSqlParameterInt16(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this Int16? input, string strParameterName)
        {
            return ToSqlParameterInt16(input, strParameterName);
        }
        #endregion 

        /*
        SqlDbType.SmallMoney;
        SqlDbType.Structured;
        SqlDbType.Time;
        SqlDbType.Timestamp;*/

        #region SqlDbType.TinyInt;
        private static SqlParameter ToSqlParameterByte(byte? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.TinyInt)
            {
                Direction = ParameterDirection.Input,
                Size = 1,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this byte input, string strParameterName)
        {
            return ToSqlParameterByte(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this byte? input, string strParameterName)
        {
            return ToSqlParameterByte(input, strParameterName);
        }
        #endregion 

        /*
        SqlDbType.Udt;
        SqlDbType.UniqueIdentifier;
        SqlDbType.VarBinary; // use Binary
        */
        
        #region SqlDbType.VarChar;
        private static SqlDbType ToSqlDbType(SpecificSQLStringType input)
        {
            switch (input)
            {
                case SpecificSQLStringType.VarChar:
                    {
                        return SqlDbType.VarChar;
                    }
                case SpecificSQLStringType.NVarChar:
                    {
                        return SqlDbType.NVarChar;
                    }
                case SpecificSQLStringType.Text:
                {
                    return SqlDbType.Text;
                }
                case SpecificSQLStringType.NText:
                {
                    return SqlDbType.NText;
                }
         
                default:
                    return SqlDbType.VarChar;
            }
        }

        private static SqlParameter ToSqlParameterString(string input, string strParameterName, SpecificSQLStringType dbType)
        {
            var sqlparam = new SqlParameter(strParameterName, ToSqlDbType(dbType))
            {
                Direction = ParameterDirection.Input
            };
            if (input != null)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this string input, string strParameterName, SpecificSQLStringType dbType = SpecificSQLStringType.VarChar)
        {
            return ToSqlParameterString(input, strParameterName, dbType);
        }

        #endregion

         /*
        SqlDbType.Variant; // ?lets find a usage ? 
        SqlDbType.Xml;
        */

    }
}
