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
using System.Dynamic;

namespace CA.Blocks.DataAccess.Translator
{
    public class DynamicDbRow2ObjectTranslator : SimpleDbRow2ObjectTranslator<dynamic>
    {
        protected override dynamic CustomTranslate(DataRow dr)
        {
            dynamic item = new ExpandoObject();
            var d = item as IDictionary<string, object>;
            for (int i = 0; i < dr.Table.Columns.Count; i++)
                d.Add(dr.Table.Columns[i].ColumnName,
                    DBNull.Value.Equals(dr[i]) ? null : dr[i]);
            return item;
        }
    }
}
