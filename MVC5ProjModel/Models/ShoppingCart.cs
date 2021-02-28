using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5ProjModel.Models;

//解析参考：https://www.cnblogs.com/stevenhqq/archive/2011/03/03/1969757.html
namespace MVC5ProjModel.Models
{
    public partial class ShoppingCart
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();

        public const string CartSessionKey = "CartId";

        string ShoppingCartId { get; set; }

        public static ShoppingCart getCart(HttpContextBase context) {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.getCartId(context);
            return cart;
        }

        public string getCartId(HttpContextBase context) {
            if (context.Session[CartSessionKey]==null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))//如果用户已经登录
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else {
                    Guid tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }


        public void MigrateCart(string userName) {
            var shoppingCart = storeDB.Carts.Where(m => m.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }

    }
}