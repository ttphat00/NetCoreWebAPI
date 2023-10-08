namespace NetCore5WebAPI.Models
{
    public class PagedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public PagedList(List<T> items,int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPage = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
