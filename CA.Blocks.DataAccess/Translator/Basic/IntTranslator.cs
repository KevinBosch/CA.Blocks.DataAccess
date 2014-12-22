using System.Data;


namespace CA.Blocks.DataAccess.Translator.Basic
{
    public class IntTranslator : SimpleDbRow2ObjectTranslator<int>
    {
        private readonly string _colname;
        public IntTranslator(string colname)
        {
            _colname = colname;
        }

        protected override int CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsInt(dr, _colname);
        }
    }
}
