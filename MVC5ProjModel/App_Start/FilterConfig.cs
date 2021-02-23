using System.Web;
using System.Web.Mvc;

namespace MVC5ProjModel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //异常过滤器生效于整个系统，任何接口或者页面报错都会执行MVC默认的异常处理(是一个虚方法,没有具体实现)，并返回一个默认的报错页面：Views/Shared/Error
            filters.Add(new HandleErrorAttribute());
            filters.Add(new myClass.CustomerErrorAttribute());//默认的异常处理方法不满足需求,因此在此类中重写了异常处理的方法(例如可以记录错误日志,然后再进行页面跳转)
        }
    }
}
