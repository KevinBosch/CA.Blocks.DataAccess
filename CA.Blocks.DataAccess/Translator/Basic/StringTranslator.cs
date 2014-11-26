using System.Data;

namespace CA.Blocks.DataAccess.Translator.Basic
{
    public class StringTranslator : SimpleDbRow2ObjectTranslator<string>
    {
        private readonly string _colname;
        public StringTranslator(string colname)
        {
            _colname = colname;
        }

        protected override string CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsString(dr, _colname);
        }
    }
}
