//===============================================================================
// Code Associate Data Access Block for .NET
//
//===============================================================================
// Copyright (C) 2002-2014 Ravin Enterprises Ltd. 
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================


using System.Data;

namespace CA.Blocks.DataAccess.Translator
{
    public abstract class DatabaseToObjectMapping
    {
        public string DestinationName;
        public string SourceNameName;
        public abstract object GetData(DataRow dr);
    }

    #region Bool

    public class DatabaseToObjectMappingBool : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingBool(string commonName)
            : this(commonName, commonName)
        {
        }


        public DatabaseToObjectMappingBool(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsBool(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullBool : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullBool(string commonName)
            : this(commonName, commonName)
        {
        }


        public DatabaseToObjectMappingNullBool(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullBool(dr, SourceNameName);
        }
    }


    #endregion

    #region Byte
    public class DatabaseToObjectMappingByte : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingByte(string commonName)
            : this(commonName, commonName)
        {
        }


        public DatabaseToObjectMappingByte(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsByte(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullByte : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullByte(string commonName)
            : this(commonName, commonName)
        {
        }
        public DatabaseToObjectMappingNullByte(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullByte(dr, SourceNameName);
        }
    }
    #endregion
    
    #region Sbyte

    public class DatabaseToObjectMappingSbyte : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingSbyte(string commonName)
            : this(commonName, commonName)
        {
        }


        public DatabaseToObjectMappingSbyte(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsSbyte(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullSbyte : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullSbyte(string commonName)
            : this(commonName, commonName)
        {
        }
        public DatabaseToObjectMappingNullSbyte(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullSbyte(dr, SourceNameName);
        }
    }
    #endregion

    #region Short
    public class DatabaseToObjectMappingShort : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingShort(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingShort(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsShort(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullShort : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullShort(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullShort(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullShort(dr, SourceNameName);
        }
    }

    #endregion

    #region Int
    public class DatabaseToObjectMappingInt : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingInt(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingInt(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsInt(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullInt : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullInt(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullInt(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullInt(dr, SourceNameName);
        }
    }
    
    #endregion

    #region Long

    public class DatabaseToObjectMappingLong : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingLong(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingLong(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsLong(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullLong : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullLong(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullLong(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullLong(dr, SourceNameName);
        }
    }

    #endregion

    #region Guid
    public class DatabaseToObjectMappingGuid : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingGuid(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingGuid(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsGuid(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullGuid : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullGuid(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullGuid(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullGuid(dr, SourceNameName);
        }
    }

    #endregion

    #region Decimal
    public class DatabaseToObjectMappingDecimal: DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingDecimal(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingDecimal(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsDecimal(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullDecimal : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullDecimal(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullDecimal(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullDecimal(dr, SourceNameName);
        }
    }
    #endregion
    
    #region Double
    public class DatabaseToObjectMappingDouble : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingDouble(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingDouble(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsDouble(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullDouble : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullDouble(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullDouble(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullDouble(dr, SourceNameName);
        }
    }
    #endregion

    #region DateTime
    public class DatabaseToObjectMappingDateTime : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingDateTime(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingDateTime(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsDateTime(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullDateTime : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullDateTime(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullDateTime(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullDateTime(dr, SourceNameName);
        }
    }
    #endregion

    #region Timespan
    public class DatabaseToObjectMappingTimeSpan : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingTimeSpan(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingTimeSpan(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsTimeSpan(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullTimeSpan : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullTimeSpan(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullTimeSpan(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullTimeSpan(dr, SourceNameName);
        }
    }

    #endregion

    #region Char
    public class DatabaseToObjectMappingChar : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingChar(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingChar(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsChar(dr, SourceNameName);
        }
    }

    public class DatabaseToObjectMappingNullChar : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingNullChar(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingNullChar(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;
        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsNullChar(dr, SourceNameName);
        }
    }
    
    #endregion

    #region String
    public class DatabaseToObjectMappingString : DatabaseToObjectMapping
    {
        public DatabaseToObjectMappingString(string commonName)
            : this(commonName, commonName)
        {
        }

        public DatabaseToObjectMappingString(string destinationName, string sourceNameName)
        {
            DestinationName = destinationName;
            SourceNameName = sourceNameName;

        }

        public override object GetData(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsString(dr, this.SourceNameName);
        }
    }

    #endregion
    
}
