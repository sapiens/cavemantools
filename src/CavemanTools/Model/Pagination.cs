using System;

namespace CavemanTools.Model
{
    public class Pagination
    {
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public long Skip { get; private set; }
       
        public Pagination(int page=1,int pageSize=15)
        {
            page.Must(p=>p>=0,"Can't have negative page numbers");
            pageSize.Must(p=>p>0,"Page size must be > 1");
            Page = page;
            PageSize = pageSize;
            Skip = (page - 1)*pageSize;
        }

        private Pagination()
        {
            
        }
        
        public static Pagination Create(long skip, int pageSize)
        {
            pageSize.Must(p => p > 0, "Page size must be a positive number");
            int page = 1;
            unchecked
            {
                page = (int)(skip / pageSize + 1);
            }
            var r= new Pagination();
            r.Skip = skip;
            r.PageSize = pageSize;
            r.Page = page;
            return r;
        }
    }
}