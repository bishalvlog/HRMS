namespace DigitalGold.Data.Dtos.Pagination.Filter
{
    public class PaginationFilter
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }

        public string Search { get; set; }
        public string Sort { get; set; }
    }
}
