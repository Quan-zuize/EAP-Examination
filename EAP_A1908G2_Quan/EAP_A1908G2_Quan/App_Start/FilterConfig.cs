using System.Web;
using System.Web.Mvc;

namespace EAP_A1908G2_Quan
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
