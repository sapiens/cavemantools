namespace CavemanTools.Model
{
    public interface IPagedInput
    {
        /// <summary>
        /// Current page
        /// </summary>
        int Page { get; }
        int PageSize { get; }
    }
}