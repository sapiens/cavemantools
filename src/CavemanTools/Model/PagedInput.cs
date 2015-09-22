namespace CavemanTools.Model
{
    public class PagedInput : IPagedInput
    {
        public static int DefaultPageSize = 15;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public PagedInput()
        {
            Page = 1;
            PageSize = DefaultPageSize;
        }
    }
}