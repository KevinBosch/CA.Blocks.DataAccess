using System;
using System.Data;


namespace CA.Blocks.DataAccess.Translator.Basic
{
    public class ByteTranslator : SimpleDbRow2ObjectTranslator<Byte>
    {
        private readonly string _colname;
        public ByteTranslator(string colname)
        {
            _colname = colname;
        }

        protected override Byte CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsByte(dr, _colname);
        }
    }
}
