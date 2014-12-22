using System;
using System.Data;

namespace CA.Blocks.DataAccess.Translator.Basic
{

    public class DateTimeTranslator : SimpleDbRow2ObjectTranslator<DateTime>
    {
        private readonly string _colname;
        public DateTimeTranslator(string colname)
        {
            _colname = colname;
        }

        protected override DateTime CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsDateTime(dr, _colname);
        }
    }
}
