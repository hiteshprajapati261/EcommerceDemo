namespace EcommerceDemo.Models.Model
{
    public class PageList
    {
        public int RecordStart { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }

        private int pageSize;

        public int PageSize
        {
            get
            {
                if (pageSize == 0)
                {
                    return 10;
                }
                return this.pageSize;
            }
            set
            {
                if (value == 0)
                {
                    value = 10;
                }
                this.pageSize = value;
            }
        }

    }
}
