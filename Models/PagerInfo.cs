namespace MVC_DotNet.Models
{
    public class PagerInfo
    {
        public int RecordCount { get; set; }

        public int CurrentPageIndex { get; set; }

        public int PageSize { get; set; }
    }
    public class PagerQuery<TPager, TEntityList>
    {
        public PagerQuery(TPager pager, TEntityList entityList)
        {
            this.Pager = pager;
            this.EntityList = entityList;
        }
        public TPager Pager { get; set; }
        public TEntityList EntityList { get; set; }
    }
}