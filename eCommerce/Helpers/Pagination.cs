namespace eCommerce.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int PageIndex , int PageSize, int Count, IReadOnlyList<T> Data)
        {
            this.Count = Count;
            this.Data = Data;
            this.PageSize = PageSize;
            this.PageIndex = PageIndex;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
