using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5ProjModel.Models;

namespace MVC5ProjModel.Controllers
{
    public class HomeController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();

        [HandleError(ExceptionType = typeof(Exception), View = "MyError")]
        public ActionResult Index()
        {
            //throw new Exception("this is a Error Page");
            var albmums = GetTopSellingAlbums(5);
            return View(albmums);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 获取首页在售专辑
        /// 私有方法，不被控制器外部访问
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<Album> GetTopSellingAlbums(int count) {
            //根据订单数量来排序,返回订单数量最多的n个专辑
            return storeDB.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
        }
    }
}