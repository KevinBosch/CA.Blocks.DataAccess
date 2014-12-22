using System;
using System.Data;

namespace CA.Blocks.DataAccess.Translator.Basic
{

    public class CharTranslator : SimpleDbRow2ObjectTranslator<char>
    {
        private readonly string _colname;
        public CharTranslator(string colname)
        {
            _colname = colname;
        }

        protected override Char CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsChar(dr, _colname);
        }
    }
}
