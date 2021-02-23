using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;//提供用于为 ASP.NET MVC 和 ASP.NET 数据控件定义元数据的特性类。

namespace MVC5ProjModel.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current password")]
        public string OldPassword { get; set; }
    }
}