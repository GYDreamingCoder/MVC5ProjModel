using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5ProjModel.Models;
using System.Web.Security;

//解析参考：https://www.cnblogs.com/stevenhqq/archive/2011/03/05/1971445.html
namespace MVC5ProjModel.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 迁移购物车
        /// 私有方法，提供登录的时候迁移账户的购物车
        /// </summary>
        /// <param name="UserName"></param>
        private void MigrateShoppingCart(string UserName)
        {
            // 给登录用户关联购物车项
            var cart = ShoppingCart.getCart(this.HttpContext);

            cart.MigrateCart(UserName);
            Session[ShoppingCart.CartSessionKey] = UserName;
        }

        // GET: Account
        //登录
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl">登录后返回之前的请求地址</param>
        /// <returns></returns>
        [HttpPost]//[HttpPost]，指的是需要POST请求才可以访问，这也是一种.NET自带的辨识思维，默认的浏览器都是GET请求，这里用到了POST那么浏览器进入AddProduct这个方法的时候，会识别出是哪一个方法。[ValidateAntiForgeryToken],防止不合理的请求，攻击数据库。可加可不加，为了安全性考虑建议添加。
        public ActionResult LogOn(logOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    MigrateShoppingCart(model.UserName);
                    //登录
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);//否则重定向到首页
                    }
                    else return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("", "用户名或密码错误");
            }
            // 如果代码运行到这里，说明填写的表单有问题，那么重新显示表单，以便用户充填
            return View(model);
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //注册
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            MembershipCreateStatus createStatus;
            Membership.CreateUser(model.UserName, model.Password,model.Email, "question", "answer", true, null, out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                // 迁移该用户的购物车
                MigrateShoppingCart(model.UserName);
                //登录用户
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("LogOn", "Account");
            }
            else {
                ModelState.AddModelError("", new Exception("注册错误！"));
            }
            // 如果代码运行到这里，则肯定发生了错误，重新显示注册表单。
            return View(model);
        }

        /// <summary>
        /// 修改密码
        /// 此处使用了特性标记Authorize用来判断用户是否登录
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            //如果模型通过验证
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true/* userIsOnline */);
                    //修改密码并返回结果
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }
                //如果成员管理服务修改密码成功
                if (changePasswordSucceeded)
                {
                    RedirectToAction("ChangePasswordSuccess");
                }
                else ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            return View();
        }


        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
