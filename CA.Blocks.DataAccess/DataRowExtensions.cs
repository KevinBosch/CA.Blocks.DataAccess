using System.Data;

namespace CA.Blocks.DataAccess
{
    // some semantic sugar to the DataHelper class. to allow us to use extention methods
    // So
    // int varName = DataHelper.GetValueFromRowAsInt(dr, "ColName");
    // can become
    // int varName = dr.AsInt("ColName");
    public static class DataRowExtensions
    {
        #region Binary
        public static byte[] AsBinary(this DataRow dr, string colName)
        {
            return DataHelper.GetValueFromRowAsBinary(dr, colName);
        }

        public static byte[] AsBinary(this DataRow dr, int columnIndex)
        {
            return DataHelper.GetValueFromRowAsBinary(dr, columnIndex);
        }

        public static byte[] AsBinary(this DataRow dr, DataColumn column)
        {
            return DataHelper.GetValueFromRowAsBinary(dr, column);
        }

        #endregion

        #region bool
        public static bool AsBool(this DataRow dr, string colName)
        {
            return DataHelper.GetValueFromRowAsBool(dr, colName);
        }

        public static bool AsBool(this DataRow dr, int columnindex)
        {
            return DataHelper.GetValueFromRowAsBool(dr, columnindex);
        }

        public static bool AsBool(this DataRow dr, DataColumn column)
        {
            return DataHelper.GetValueFromRowAsBool(dr, column);
        }
        // Nulls
        public static bool? AsNullBool(this DataRow dr, string colName)
        {
            return DataHelper.GetValueFromRowAsNullBool(dr, colName);
        }

        public static bool? AsNullBool(this DataRow dr, int columnindex)
        {
            return DataHelper.GetValueFromRowAsNullBool(dr, columnindex);
        }

        public static bool? AsNullBool(this DataRow dr, DataColumn column)
        {
            return DataHelper.GetValueFromRowAsNullBool(dr, column);
        }
        #endregion

        #region Byte
        //TODO 
        #endregion

        #region Char
        //TODO 
        #endregion

        #region DateTime
        //TODO 
        #endregion

        #region Double
        //TODO 
        #endregion

        #region Decimal
        //TODO 
        #endregion

        #region Guid
        //TODO 
        #endregion

        #region Int
        public static int AsInt(this DataRow dr, string colName)
        {
            return DataHelper.GetValueFromRowAsInt(dr, colName);
        }

        public static int AsInt(this DataRow dr, int columnindex)
        {
            return DataHelper.GetValueFromRowAsInt(dr, columnindex);
        }

        public static int AsInt(this DataRow dr, DataColumn column)
        {
            return DataHelper.GetValueFromRowAsInt(dr, column);
        }
        // Nulls
        public static int? AsNullInt(this DataRow dr, string colName)
        {
            return DataHelper.GetValueFromRowAsNullInt(dr, colName);
        }

        public static int? AsNullInt(this DataRow dr, int columnindex)
        {
            return DataHelper.GetValueFromRowAsNullInt(dr, columnindex);
        }

        public static int? AsNullInt(this DataRow dr, DataColumn column)
        {
            return DataHelper.GetValueFromRowAsNullInt(dr, column);
        }
        #endregion 

        #region long
        //TODO 
        #endregion

        #region RowVersion
        //TODO 
        #endregion

        #region Sbyte
        //TODO 
        #endregion

        #region Short
        //TODO 
        #endregion

        #region string
        public static string AsString(this DataRow dr, string colName, bool returnNullAsEmptyString = false)
        {
            return DataHelper.GetValueFromRowAsString(dr, colName, returnNullAsEmptyString);
        }
        public static string AsString(this DataRow dr, int columnOrder, bool returnNullAsEmptyString = false)
        {
            return DataHelper.GetValueFromRowAsString(dr, columnOrder, returnNullAsEmptyString);
        }
        public static string AsString(this DataRow dr, DataColumn column, bool returnNullAsEmptyString = false)
        {
            return DataHelper.GetValueFromRowAsString(dr, column, returnNullAsEmptyString);
        }
        #endregion 

        #region TimeSpan
        //TODO 
        #endregion


    }
}
