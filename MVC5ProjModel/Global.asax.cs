using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC5ProjModel
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<DbContext>(null);
//            策略一：数据库不存在时重新创建数据库
//Database.SetInitializer<testContext>(new CreateDatabaseIfNotExists<testContext>());
//            策略二：每次启动应用程序时创建数据库
//Database.SetInitializer<testContext>(new DropCreateDatabaseAlways<testContext>());
//            策略三：模型更改时重新创建数据库
//Database.SetInitializer<testContext>(new DropCreateDatabaseIfModelChanges<testContext>());
//            策略四：从不创建数据库
//Database.SetInitializer<testContext>(null);
        }
    }
}
