namespace CavemanTools.Model
{
    public class PagedModel<T> : IPagedInput where T : class
    {
        public PagedResult<T> Data { get; set; }
        public int Page { get; set; }
        /// <summary>
        /// How many items are on a page
        /// </summary>
        public int PageSize { get; set; }


        public PagedModel(IPagedInput input=null)
        {
            if (input != null) Page = input.Page;
            Data = new PagedResult<T>();
            Page = 1;
        }
    }
}