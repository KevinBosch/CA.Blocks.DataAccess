using System.Data;


namespace CA.Blocks.DataAccess.Translator.Basic
{
    public class BinaryTranslator : SimpleDbRow2ObjectTranslator<byte[]>
    {
        private readonly string _colname;
        public BinaryTranslator(string colname)
        {
            _colname = colname;
        }

        protected override byte[] CustomTranslate(DataRow dr)
        {
            return DataHelper.GetValueFromRowAsBinary(dr, _colname);
        }
    }
}
