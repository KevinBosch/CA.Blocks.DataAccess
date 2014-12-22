using System.Data;


namespace CA.Blocks.DataAccess.Translator.Basic
{
    public class ShortTranslator : SimpleDbRow2ObjectTranslator<short>
    {
        private readonly string _colname;
        public ShortTranslator(string colname)
        {
            _colname = colname;
        }

        protected override short CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsShort(dr, _colname);
        }
    }
}
