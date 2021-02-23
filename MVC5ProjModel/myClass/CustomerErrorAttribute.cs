using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5ProjModel.myClass
{

    /// <summary>
    /// 自定义异常过滤器
    /// </summary>
    public class CustomerErrorAttribute : HandleErrorAttribute
    {
        //参考:https://blog.csdn.net/xishining/article/details/84901053
    }
}