using System.Web;
using System.Web.Mvc;

namespace NguyenVuLong_181212110
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
