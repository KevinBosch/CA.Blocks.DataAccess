using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CA.Blocks.DataAccess.Filter
{
    public enum BaseFilterSegmentCondition
    {
        And,
        Or
    }

    public abstract class BaseFilterSegment
    {
        private BaseFilterSegmentCondition _condition;
        StringBuilder _filter = new StringBuilder();
        private IList<SqlParameter> _parameters = new List<SqlParameter>();

        protected BaseFilterSegment()
        {
            _condition = BaseFilterSegmentCondition.And;
        }

        protected BaseFilterSegment(BaseFilterSegmentCondition condition)
        {
            _condition = condition;
        }

        protected void AddFilter(string filter)
        {
            if (!String.IsNullOrWhiteSpace(filter))
            {
                _filter.Append(string.Format(" {0} ", _condition));
            }
            _filter.Append(filter);
        }

        public IList<SqlParameter> Parameters
        {
            get
            {
                return _parameters;
            }
        }

        public string ToSQLFilter()
        {
            return _filter.ToString();
        }
    }
}
