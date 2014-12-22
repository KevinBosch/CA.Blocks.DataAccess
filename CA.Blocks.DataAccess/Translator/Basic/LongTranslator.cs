using System.Data;

namespace CA.Blocks.DataAccess.Translator.Basic
{

    public class LongTranslator : SimpleDbRow2ObjectTranslator<long>
    {
        private readonly string _colname;
        public LongTranslator(string colname)
        {
            _colname = colname;
        }

        protected override long CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsLong(dr, _colname);
        }
    }
}
