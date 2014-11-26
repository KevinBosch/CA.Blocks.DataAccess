using System.Collections.Generic;
using System.Linq;

namespace CA.Blocks.DataAccess.Paging
{
    public class PagingRequest
    {

        public PagingRequest()
        {

        }
        public PagingRequest(int take, int skip, string defaultOrderByCol)
        {
            Take = take;
            Skip = skip;
            SortOrder = new List<Sort> { new Sort { Field = defaultOrderByCol, Dir = "ASC" } };
        }
        public PagingRequest(int take, int skip, string defaultOrderByCol, string dir)
        {
            Take = take;
            Skip = skip;
            SortOrder = new List<Sort> { new Sort { Field = defaultOrderByCol, Dir = dir } };
        }


        public int Take { get; set; }
        public int Skip { get; set; }
        public IList<Sort> SortOrder { get; set; }
        public string GetOrderBy()
        {
            return SortOrder.Count > 0 ? string.Join(",", SortOrder.Select(sort => string.Format("{0} {1}", sort.Field, sort.Dir)).ToArray()) : "1 asc";
        }
    }
}
