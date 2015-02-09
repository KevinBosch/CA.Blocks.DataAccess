//===============================================================================
// Code Associate Data Access Block for .NET
// DataHelper.cs
//
//===============================================================================
// Copyright (C) 2002-2014 Ravin Enterprises Ltd. 
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Data;
using System.Diagnostics;

namespace CA.Blocks.DataAccess
{
    /// <summary>
    /// This class is a helper class for dealing data values.  It is intended to be a static helper class only.
    /// </summary>
    public static class DataHelper
    {
        private static void ThrowExceptionIfIsNull(object obj, string sColumnName, string typeDescription)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException(
                           string.Format("Tried to get {0} from row as non-nullable {1}, however value is NULL.", sColumnName,
                                         typeDescription));
            }
        }

        private static void ThrowExceptionIfIsNull(object obj, int columnIndex, string typeDescription)
        {
            if (obj == null || obj == DBNull.Value)
            {
                throw new ArgumentNullException(
                           string.Format("Tried to get col in position {0} from row as non-nullable {1}, however value is NULL.", columnIndex,
                                         typeDescription));
            }
        }

        /// <summary>
        /// Will get the data value from the row as an object retuning the .NET null value  in the event the data value is 
        /// null in the DataRow.
        /// </summary>
        /// <param name="dr"> A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static object GetValueFromRow(DataRow dr, string sColumnName)
        {
            return dr.IsNull(sColumnName) ? null : dr[sColumnName];
        }

        /// <summary>
        /// Will get the data value from the row as an object retuning the .NET null value  in the event the data value is 
        /// null in the DataRow.
        /// </summary>
        /// <param name="dr"> A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="column">The Column that belongs to the DataTable</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static object GetValueFromRow(DataRow dr, DataColumn column)
        {
            return dr.IsNull(column) ? null : dr[column];
        }

        /// <summary>
        /// Will get the data value from the row as an object retuning the .NET null value  in the event the data value is 
        /// null in the DataRow.
        /// </summary>
        /// <param name="dr"> A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="columnIndex">The index of the Column that belongs to the DataTable</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static object GetValueFromRow(DataRow dr, int columnIndex)
        {
            return !dr.IsNull(columnIndex) ? dr[columnIndex] : null;
        }

        /// <summary>
        /// Will get the data value from the row as a string. The return value will be set to either null or and empty string depending 
        /// on the value of ReturnNullAsEmptyString  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName"> The Name of the Column in the DataRow </param>
        /// <param name="returnNullAsEmptyString">Sets the attribute on how an empty string will be treated, it true it will return string.empty else it will return null. </param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetValueFromRowAsString(DataRow dr, string sColumnName, bool returnNullAsEmptyString = false)
        {
            object result = GetValueFromRow(dr, sColumnName);
            if (result == null && returnNullAsEmptyString)
                result = string.Empty;
            return (string)result;
        }


        /// <summary>
        /// Will get the data value from the row as a string. The return value will be set to either null or and empty string depending 
        /// on the value of ReturnNullAsEmptyString  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="columnOrder"> The order of the Column in the DataRow </param>
        /// <param name="returnNullAsEmptyString">Sets the attribute on how an empty string will be treated, it true it will return string.empty else it will return null. </param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string GetValueFromRowAsString(DataRow dr, int columnOrder, bool returnNullAsEmptyString = false)
        {
            object result = GetValueFromRow(dr, columnOrder);
            if (result == null && returnNullAsEmptyString)
                result = string.Empty;
            return (string)result;
        }


        [DebuggerStepThrough]
        public static string GetValueFromRowAsString(DataRow dr, DataColumn column, bool returnNullAsEmptyString = false)
        {
            object result = GetValueFromRow(dr, column);
            if (result == null && returnNullAsEmptyString)
                result = string.Empty;
            return (string)result;
        }


        /// <summary>
        /// Will get the data value from the row as a nullable int. The return value will be set to either null or the int value
        /// This procedure assumes that the data is an integer, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static int? GetValueFromRowAsNullInt(DataRow dr, string sColumnName)
        {
            return ((int?)GetValueFromRow(dr, sColumnName));
        }


        /// <summary>
        /// Will get the data value from the row as a nullable int. The return value will be set to either null or the int value
        /// This procedure assumes that the data is an integer, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="column">The Column inside the datarow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static int? GetValueFromRowAsNullInt(DataRow dr, DataColumn column)
        {
            return ((int?)GetValueFromRow(dr, column));
        }

        [DebuggerStepThrough]
        public static int? GetValueFromRowAsNullInt(DataRow dr, int columnOrder)
        {
            return ((int?)GetValueFromRow(dr, columnOrder));
        }

        /// <summary>
        /// Will get the data value from the row as an int. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an integer, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static int GetValueFromRowAsInt(DataRow dr, string sColumnName)
        {
            int? val = GetValueFromRowAsNullInt(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "int");
            return val.Value;
        }


        /// <summary>
        /// Will get the data value from the row as an int. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an integer, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="column">The Column which is part of the data row</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static int GetValueFromRowAsInt(DataRow dr, DataColumn column)
        {
            int? val = GetValueFromRowAsNullInt(dr, column);
            ThrowExceptionIfIsNull(val, column.ColumnName, "int");
            return val.Value;
        }

        [DebuggerStepThrough]
        public static int GetValueFromRowAsInt(DataRow dr, int columnIndex)
        {
            int? val = GetValueFromRowAsNullInt(dr, columnIndex);
            ThrowExceptionIfIsNull(val, columnIndex, "int");
            return val.Value;
        }


        /// <summary>
        /// Will get the data value from the row as a nullable Decimal. The return value will be set to either null or the Decimal value
        /// This procedure assumes that the data is an Decimaleger, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Decimal? GetValueFromRowAsNullDecimal(DataRow dr, string sColumnName)
        {
            return ((Decimal?)GetValueFromRow(dr, sColumnName));
        }

        /// <summary>
        /// Will get the data value from the row as an Decimal. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an Decimaleger, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Decimal GetValueFromRowAsDecimal(DataRow dr, string sColumnName)
        {
            Decimal? val = GetValueFromRowAsNullDecimal(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "Decimal");
            return val.Value;
        }

        /// <summary>
        /// Will get the data value from the row as a nullable Double. The return value will be set to either null or the Double value
        /// This procedure assumes that the data is an Double, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Double? GetValueFromRowAsNullDouble(DataRow dr, string sColumnName)
        {
            return ((Double?)GetValueFromRow(dr, sColumnName));
        }

        /// <summary>
        /// Will get the data value from the row as an Double. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an Double, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Double GetValueFromRowAsDouble(DataRow dr, string sColumnName)
        {
            Double? val = GetValueFromRowAsNullDouble(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "Double");
            return val.Value;
        }


        /// <summary>
        /// Will get the data value from the row as a nullable long. The return value will be set to either null or the long value
        /// This procedure assumes that the data is an long, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static long? GetValueFromRowAsNullLong(DataRow dr, string sColumnName)
        {
            return ((long?)GetValueFromRow(dr, sColumnName));
        }

        /// <summary>
        /// Will get the data value from the row as a nullable long. The return value will be set to either null or the long value
        /// This procedure assumes that the data is an long, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="column">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static long? GetValueFromRowAsNullLong(DataRow dr, DataColumn column)
        {
            return ((long?)GetValueFromRow(dr, column));
        }

        /// <summary>
        /// Will get the data value from the row as an long. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an long, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static long GetValueFromRowAsLong(DataRow dr, string sColumnName)
        {
            long? val = GetValueFromRowAsNullLong(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "long");
            return val.Value;
        }

        /// <summary>
        /// Will get the data value from the row as an long. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an long, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="column">The Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static long GetValueFromRowAsLong(DataRow dr, DataColumn column)
        {
            long? val = GetValueFromRowAsNullLong(dr, column);
            ThrowExceptionIfIsNull(val, column.ColumnName, "long");
            return val.Value;
        }


        /// <summary>
        /// Will get a the data value from the data row as a bool. If a DB Null is found then throw a NullException
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        public static bool GetValueFromRowAsBool(DataRow dr, string sColumnName)
        {
            bool? val = GetValueFromRowAsNullBool(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "bool");
            return val.Value;
        }

        public static bool GetValueFromRowAsBool(DataRow dr, int columnIndex)
        {
            bool? val = GetValueFromRowAsNullBool(dr, columnIndex);
            ThrowExceptionIfIsNull(val, columnIndex, "bool");
            return val.Value;
        }

        public static bool GetValueFromRowAsBool(DataRow dr, DataColumn dc)
        {
            bool? val = GetValueFromRowAsNullBool(dr, dc);
            ThrowExceptionIfIsNull(val, dc.ColumnName, "bool");
            return val.Value;
        }


        /// <summary>
        /// Will get a the data value from the data row as a bool. If a DB Null is found then it will return the default bool value defined in DefaultReturnValueIfNull
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <param name="defaultReturnValueIfNull"> The default value in the event the DB col is null</param>
        /// <returns></returns>
        public static bool GetValueFromRowAsBool(DataRow dr, string sColumnName, bool defaultReturnValueIfNull)
        {
            bool? val = GetValueFromRowAsNullBool(dr, sColumnName);
            return val == null ? defaultReturnValueIfNull : (bool)val;
        }

        public static bool? GetValueFromRowAsNullBool(DataRow dr, string sColumnName)
        {
            return (bool?)GetValueFromRow(dr, sColumnName);
        }

        public static bool? GetValueFromRowAsNullBool(DataRow dr, int columnIndex)
        {
            return (bool?)GetValueFromRow(dr, columnIndex);
        }

        public static bool? GetValueFromRowAsNullBool(DataRow dr, DataColumn dc)
        {
            return (bool?)GetValueFromRow(dr, dc);
        }


        /// <summary>
        /// Will get the data value from the row as a nullable short. The return value will be set to either null or the short value
        /// This procedure assumes that the data is a short, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static short? GetValueFromRowAsNullShort(DataRow dr, string sColumnName)
        {
            return (short?)GetValueFromRow(dr, sColumnName);
        }

        /// <summary>
        /// Will get the data value from the row as an short. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an short, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static short GetValueFromRowAsShort(DataRow dr, string sColumnName)
        {
            short? val = GetValueFromRowAsNullShort(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "short");
            return val.Value;
        }


        /// <summary>
        /// Will get the data value from the row as a nullable sbyte. The return value will be set to either null or the sbyte value
        /// This procedure assumes that the data is a sbyte, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static sbyte? GetValueFromRowAsNullSbyte(DataRow dr, string sColumnName)
        {
            return (sbyte?)(GetValueFromRow(dr, sColumnName));
        }

        /// <summary>
        /// Will get the data value from the row as an sbyte. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an sbyte, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static sbyte GetValueFromRowAsSbyte(DataRow dr, string sColumnName)
        {
            sbyte? val = GetValueFromRowAsNullSbyte(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "short");
            return val.Value;
        }

        /// <summary>
        /// Will get the data value from the row as a nullable short. The return value will be set to either null or the short value
        /// This procedure assumes that the data is a short, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static byte? GetValueFromRowAsNullByte(DataRow dr, string sColumnName)
        {
            return (byte?)(GetValueFromRow(dr, sColumnName));
        }

        /// <summary>
        /// Will get the data value from the row as an byte. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an byte, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static byte GetValueFromRowAsByte(DataRow dr, string sColumnName)
        {
            byte? val = GetValueFromRowAsNullByte(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "byte");
            return val.Value;
        }

        /// <summary>
        /// Will get the data value from the row as a nullable short. The return value will be set to either null or the short value
        /// This procedure assumes that the data is a short, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime? GetValueFromRowAsNullDateTime(DataRow dr, string sColumnName)
        {
            return (DateTime?)(GetValueFromRow(dr, sColumnName));
        }

        /// <summary>
        /// Will get the data value from the row as an DateTime. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is an DateTime, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static DateTime GetValueFromRowAsDateTime(DataRow dr, string sColumnName)
        {
            DateTime? val = GetValueFromRowAsNullDateTime(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "DateTime");
            return val.Value;
        }



        public static TimeSpan? GetValueFromRowAsNullTimeSpan(DataRow dr, string sColumnName)
        {
            return (TimeSpan?)(GetValueFromRow(dr, sColumnName));
        }

        public static TimeSpan GetValueFromRowAsTimeSpan(DataRow dr, string sColumnName)
        {
            TimeSpan? val = GetValueFromRowAsNullTimeSpan(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "TimeSpan");
            return val.Value;
        }

        


        /// <summary>
        /// Will get the data value from the row as a Guid. If the value is null an <see cref="ArgumentNullException"/> will be thrown
        /// This procedure assumes that the data is a Guid, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Guid GetValueFromRowAsGuid(DataRow dr, string sColumnName)
        {
            Guid? val = GetValueFromRowAsNullGuid(dr, sColumnName);
            ThrowExceptionIfIsNull(val, sColumnName, "Guid");
            return val.Value;
        }

        /// <summary>
        /// Will get the data value from the row as a nullable Guid. The return value will be set to either null or the Guid value
        /// This procedure assumes that the data is a Guid, if not a cast exception will be thrown.  
        /// </summary>
        /// <param name="dr">A Valid <see cref="System.Data.DataRow"/> DataRow</param>
        /// <param name="sColumnName">The Name of the Column in the DataRow</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static Guid? GetValueFromRowAsNullGuid(DataRow dr, string sColumnName)
        {
            return ((Guid?)GetValueFromRow(dr, sColumnName));
        }

        [DebuggerStepThrough]
        public static char GetValueFromRowAsChar(DataRow dr, string sColumnName)
        {
            object result = GetValueFromRow(dr, sColumnName);
            if (result.ToString().Length > 0)
                return ((string)result)[0];
            else
                return '\0';

        }

        [DebuggerStepThrough]
        public static char? GetValueFromRowAsNullChar(DataRow dr, string sColumnName)
        {
            object result = GetValueFromRow(dr, sColumnName);
            if (result != null && result.ToString().Length > 0)
                return ((string)result)[0];
            else
                return null;

        }

        [DebuggerStepThrough]
        public static ulong GetValueFromRowAsRowVersion(DataRow dr, string sColumnName)
        {
            byte[] result = (byte[])DataHelper.GetValueFromRow(dr, sColumnName);
            return BitConverter.ToUInt64(result, 0);
        }


        [DebuggerStepThrough]
        public static byte[] GetValueFromRowAsBinary(DataRow dr, string sColumnName)
        {
            return (byte[])GetValueFromRow(dr, sColumnName);
        }

        [DebuggerStepThrough]
        public static byte[] GetValueFromRowAsBinary(DataRow dr, int columnIndex)
        {
            return (byte[])GetValueFromRow(dr, columnIndex);
        }

        [DebuggerStepThrough]
        public static byte[] GetValueFromRowAsBinary(DataRow dr, DataColumn dc)
        {
            return (byte[])GetValueFromRow(dr, dc);
        }



    }
}
