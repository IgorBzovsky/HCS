﻿namespace HCS.Api.Controllers.Resources.Queries
{
    public class UserQueryResource
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
