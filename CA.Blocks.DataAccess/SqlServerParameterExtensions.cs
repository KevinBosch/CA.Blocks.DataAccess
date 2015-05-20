using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

    public enum SpecificSQLDecimalType
    {
        Decimal,
        Money, // The default
        SmallMoney,
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

        #region SqlDbType.BigInt ( long, Int64 ) 
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


        #region SqlDbType.Bit ( boolean ) 
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


        #region SqlDbType.Char   (Char)


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


        #region SqlDbType.DateTime ( System.DateTime )
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

        public static SqlParameter ToSqlParameter(this DateTime input, string strParameterName, SpecificSQLDateTimeType dbType = SpecificSQLDateTimeType.DateTime)
        {
            return ToSqlParameterDateTime(input, strParameterName, dbType);
        }

        // Default to DateTime

        public static SqlParameter ToSqlParameter(this DateTime? input, string strParameterName, SpecificSQLDateTimeType dbType = SpecificSQLDateTimeType.DateTime)
        {
            return ToSqlParameterDateTime(input, strParameterName, dbType);
        }
        #endregion

        /*TODO
        SqlDbType.DateTimeOffset;
        */
        #region SqlDbType.Decimal  (Decimal, Money, SmallMoney)

        private static SqlDbType ToSqlDbType(SpecificSQLDecimalType dbType)
        {
            switch (dbType)
            {
                case SpecificSQLDecimalType.Decimal:
                    {
                        return SqlDbType.Decimal;
                    }
                case SpecificSQLDecimalType.Money:
                    {
                        return SqlDbType.Money;
                    }
                case SpecificSQLDecimalType.SmallMoney:
                    {
                        return SqlDbType.SmallMoney;
                    }
                default:
                    return  SqlDbType.Decimal;
            }
        }

        private static SqlParameter ToSqlParameterDecimal(Decimal? input, string strParameterName, SpecificSQLDecimalType dbType)
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


        public static SqlParameter ToSqlParameter(this Decimal input, string strParameterName, SpecificSQLDecimalType dbType = SpecificSQLDecimalType.Decimal)
        {
            return ToSqlParameterDecimal(input, strParameterName, dbType);
        }

        // Default is SpecificSQLDecimalType.Decimal
        public static SqlParameter ToSqlParameter(this Decimal? input, string strParameterName, SpecificSQLDecimalType dbType = SpecificSQLDecimalType.Decimal)
        {
            return ToSqlParameterDecimal(input, strParameterName, dbType);
        }

        #endregion 
         
        #region SqlDbType.Float  (System.Double)

        private static SqlParameter ToSqlParameterDouble(Double? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.Float)
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

        public static SqlParameter ToSqlParameter(this Double input, string strParameterName)
        {
            return ToSqlParameterDouble(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this Double? input, string strParameterName)
        {
            return ToSqlParameterDouble(input, strParameterName);
        }
        #endregion

        /*
        SqlDbType.Image;
        */
        #region SqlDbType.Int  ( int, Int32 )

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


        #region SqlDbType.SmallInt  -> ( short, Int16)
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
         */
        #region SqlDbType.Time ( System.TimeSpan )

        private static SqlParameter ToSqlParameterTimeSpan(TimeSpan? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.Time)
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

        public static SqlParameter ToSqlParameter(this TimeSpan input, string strParameterName)
        {
            return ToSqlParameterTimeSpan(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this TimeSpan? input, string strParameterName)
        {
            return ToSqlParameterTimeSpan(input, strParameterName);
        }

        #endregion 

        /*
        SqlDbType.Timestamp;*/

        #region SqlDbType.TinyInt ( Byte ) 
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
         */
        #region SqlDbType.UniqueIdentifier ( System.Guid)
        private static SqlParameter ToSqlParameterGuid(Guid? input, string strParameterName)
        {
            var sqlparam = new SqlParameter(strParameterName, SqlDbType.SmallInt)
            {
                Direction = ParameterDirection.Input,
                Size = 16,
            };
            if (input.HasValue)
                sqlparam.Value = input;
            else
            {
                sqlparam.Value = DBNull.Value;
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this Guid input, string strParameterName)
        {
            return ToSqlParameterGuid(input, strParameterName);
        }

        public static SqlParameter ToSqlParameter(this Guid? input, string strParameterName)
        {
            return ToSqlParameterGuid(input, strParameterName);
        }
        #endregion 

        /*
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

        private static SqlParameter ToSqlParameterString(string input, string strParameterName, SpecificSQLStringType dbType, bool useEmptyStringForNull)
        {
            var sqlparam = new SqlParameter(strParameterName, ToSqlDbType(dbType))
            {
                Direction = ParameterDirection.Input
            };
            if (input != null)
                sqlparam.Value = input;
            else
            {
                if (useEmptyStringForNull)
                    sqlparam.Value = string.Empty;
                else
                    sqlparam.Value = DBNull.Value;   
            }
            return (sqlparam);
        }

        public static SqlParameter ToSqlParameter(this string input, string strParameterName, SpecificSQLStringType dbType = SpecificSQLStringType.VarChar, bool useEmptyStringForNull = false)
        {
            return ToSqlParameterString(input, strParameterName, dbType, useEmptyStringForNull);
        }

        #endregion

         /*
        SqlDbType.Variant; // ?lets find a usage ? 
        SqlDbType.Xml;
        */

    }
}
