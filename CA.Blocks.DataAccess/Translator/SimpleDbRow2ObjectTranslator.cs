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
using System.Linq;

namespace CA.Blocks.DataAccess.Translator
{
    
    // This object is a simple container in which the Object will do all the of Translate form a Data Row to the Object
    public abstract class SimpleDbRow2ObjectTranslator<T> 
    {
        public IList<T> Translate(DataRow[] drs)
        {
            return (from DataRow dr in drs select Translate(dr)).ToList();
        }

        public IList<T> Translate(DataTable dt)
        {
            return (from DataRow dr in dt.Rows select Translate(dr)).ToList();
        }

        public T Translate(DataRow dr)
        {
            return dr == null ? default(T) : CustomTranslate(dr);
        }

        protected abstract T CustomTranslate(DataRow dr);

    }
}
