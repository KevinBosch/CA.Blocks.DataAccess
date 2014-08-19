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


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CA.Blocks.DataAccess.Translator
{

    public class DatabaseToObjectMappings : List<DatabaseToObjectMapping>
    {
        public void RemoveByName(string propertyName)
        {
            RemoveAll(m => m.DestinationName == propertyName);
        }


    }


    public class BaseDb2ObjectTranslator<T> where T : new()
    {
        protected DatabaseToObjectMappings _mappings;

        #region Constructor
        public BaseDb2ObjectTranslator()
            : this(true)
        {

        }

        public BaseDb2ObjectTranslator(bool useDefault)
        {
            if (useDefault)
                GenerateDefaultMappings();
        }
        #endregion

        public IList<T> Translate(DataTable dt)
        {
            return (from DataRow dr in dt.Rows select Translate(dr)).ToList();
        }

        public T Translate(DataRow dr)
        {
            T item = default(T);
            if (dr != null)
            {
                item = new T();
                Translate(dr, item);
            }
            return item;
        }

        protected virtual void CustomTranslate(DataRow dr, T item)
        {
        }

        private void Translate(DataRow dr, T item)
        {
            foreach (var mapping in _mappings)
            {
                object data = mapping.GetData(dr);
                PropertyInfo pi = item.GetType().GetProperty(mapping.DestinationName);
                pi.SetValue(item, data, null);
            }
            CustomTranslate(dr, item);
        }

        #region  Default Mappings Domain is 1-1 with the query
        private DatabaseToObjectMapping GetDatabaseToObjectMapping(string typeName, string propertyName, bool isNullable)
        {
            DatabaseToObjectMapping result = null;
            switch (typeName)
            {
                case "System.String":
                    {
                        result = new DatabaseToObjectMappingString(propertyName);
                        break;
                    }
                case "System.DateTime":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullDateTime(propertyName);
                        else
                            result = new DatabaseToObjectMappingDateTime(propertyName);
                        break;
                    }
                case "System.Int64":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullLong(propertyName);
                        else
                            result = new DatabaseToObjectMappingLong(propertyName);
                        break;
                    }
                case "System.Int32":
                    {
                        if (isNullable)
                            result =new DatabaseToObjectMappingNullInt(propertyName);
                        else
                            result = new DatabaseToObjectMappingInt(propertyName);
                        break;
                    }
                case "System.Int16":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullShort(propertyName);
                        else
                            result = new DatabaseToObjectMappingShort(propertyName);
                        break;
                    }
                case "System.Byte":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullByte(propertyName);
                        else
                            result = new DatabaseToObjectMappingByte(propertyName);
                        break;
                    }
                case "System.Boolean":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullBool(propertyName);
                        else
                            result = new DatabaseToObjectMappingBool(propertyName);
                        break;
                    }
                case "System.Char":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullChar(propertyName);
                        else
                            result = new DatabaseToObjectMappingChar(propertyName);
                        break;
                    }

                case "System.TimeSpan":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullTimeSpan(propertyName);
                        else
                            result = new DatabaseToObjectMappingTimeSpan(propertyName);
                        break;
                    }
                case "System.Double":
                    {
                        
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullDouble(propertyName);
                        else
                            result = new DatabaseToObjectMappingDouble(propertyName);
                        break;
                    }

                case "System.Decimal":
                    {
                        if (isNullable)
                            result = new DatabaseToObjectMappingNullDecimal(propertyName);
                        else
                            result = new DatabaseToObjectMappingDecimal(propertyName);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException(string.Format("Unknown type '{0}' for DatabaseToObjectMapping", typeName));
                        // If you get here there is a missing  DatabaseToObjectMapping each to add in just follow the pattern above 
                    }
            }
            return result;
        }

        protected void GenerateDefaultMappings()
        {
            _mappings = new DatabaseToObjectMappings();
            var myObjectFields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var pi in myObjectFields)
            {
                if (pi.CanWrite)
                {
                    if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        _mappings.Add(GetDatabaseToObjectMapping(pi.PropertyType.GetGenericArguments()[0].FullName, pi.Name, true));
                    }
                    else
                    {
                        _mappings.Add(GetDatabaseToObjectMapping(pi.PropertyType.FullName, pi.Name, false));
                    }
                    
                }
            }
        }




        #endregion
    }
}
