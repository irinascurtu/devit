using System.Collections.Generic;

namespace SuperHeroes.Domain
{
    public class Filter
    {
        private int? page;
        private int pageSize;
        public List<string> Country { get; set; }

        public int PageSize
        {
            get => pageSize == 0 ? 2 : pageSize;
            set => pageSize = value;
        }

        public int? Page
        {
            get => page == 0 ? 1 : page;
            set => this.page = value;
        }
    }
}
