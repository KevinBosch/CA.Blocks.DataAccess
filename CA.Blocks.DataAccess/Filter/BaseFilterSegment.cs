using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
            if (_filter.Length > 0 )
            {
                _filter.Append(string.Format(" {0} ", _condition));
            }
            _filter.Append(filter);
        }

        protected void AddFilter(string filter, SqlParameter sqlparam)
        {
            AddFilter(filter);
            AssignParameterValue(sqlparam);
        }


        protected void AssignParameterValue(SqlParameter sqlparam)
        {
            if (Parameters.All(x => x.ParameterName != sqlparam.ParameterName))
            {
                Parameters.Add(sqlparam);
            }
            else
            {
                var element = Parameters.FirstOrDefault(x => x.ParameterName == sqlparam.ParameterName);
                if (element.DbType != sqlparam.DbType)
                {
                    throw new ApplicationException(
                        string.Format("The Parameter {0} has been given two diffrent types {1} and {2}, this is not allowed within a single query", sqlparam.ParameterName, sqlparam.DbType, element.DbType));
                }
                if (!element.Value.Equals(sqlparam.Value))
                {
                    throw new ApplicationException(
                        string.Format("The Parameter {0} has been given two diffrent values {1} and {2}, this is not allowed within a single query", sqlparam.ParameterName, sqlparam.Value, element.Value));
                }
            }
        }


        public IList<SqlParameter> Parameters
        {
            get
            {
                return _parameters;
            }
        }

        public string ToSQLFilter(bool includeWhere = false)
        {
            if (includeWhere && _filter.Length > 0)
                return string.Format("WHERE {0}", _filter.ToString());
            else
                return _filter.ToString();

        }
    }
}
