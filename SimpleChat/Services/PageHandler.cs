namespace SimpleChat.Services
{
    public class PageHandler
    {
        private readonly int _pageSize;

        public PageHandler(int pageSize)
        {
            _pageSize = pageSize;
        }

        public int CurrentPage { get; set; } = 1;

        public int ElementsCount {get; set;}

        public int PageCount
        {
            get
            {
                return (ElementsCount + _pageSize -1)/ _pageSize;
            }
        }

        public bool IsPrevPageExists
        {
            get
            {
                return CurrentPage < PageCount; 
            }
        }

        public int PrevPage
        {
            get
            {
                return (CurrentPage < PageCount) ? CurrentPage + 1 : CurrentPage;
            }
        }

        public bool IsNextPageExists
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public int NextPage
        {
            get
            {
                return (CurrentPage <= 1) ? CurrentPage : CurrentPage - 1; 
            }
        }

        public int SkipElements
        {
            get
            {
                return _pageSize * (CurrentPage - 1);
            }
        }
    }
}
