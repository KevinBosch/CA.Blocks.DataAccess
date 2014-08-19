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


using System.Collections.Generic;
using System.Data;

namespace CA.Blocks.DataAccess.Translator
{
    public abstract class SimpleDbReader2ObjectTranslator<T> 
    {
        public IList<T> Translate(IDataReader dr)
        {
            IList<T> result = new List<T>();
            while (dr.Read())
            {
                result.Add(TranslateRow(dr));
            }
            dr.Close();
            return result;
        }

        public T TranslateRow(IDataReader dr)
        {
            return dr == null ? default(T) : CustomTranslate(dr);
        }

        protected abstract T CustomTranslate(IDataReader dr);
    }
}
