using HCS.Core.Extensions;

namespace HCS.Core.Queries
{
    public class UserQuery : IQueryObject
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
