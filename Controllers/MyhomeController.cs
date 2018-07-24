using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_DotNet.Models;
namespace MVC_DotNet.Controllers
{
    public class MyhomeController:Controller
    {
        [AllowAnonymous]
         public IActionResult Index()
        {//http://localhost:5000/Myhome
            ClaimsIdentity claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            UserModel user=new UserModel();
             user.username ="halyhuang";
             user.id=1;
             user.password ="ok";
           
            return View(user);
        }
        public IActionResult Login()
        {//http://localhost:5000/Myhome/Login
            
            return View();
        }
        [Authorize]
        public ActionResult jsontest(int? id)
        {
            if (id == null) id = 23;
            var tempObj = new { Controller = "MyHomeController", Action = "JsonResultDemo", id = id };
            return Json(tempObj);
        }
         [HttpPost]
        public ContentResult SaveUser(Models.UserModel userModel)
        {
          
            string user = userModel.username;
            return Content("Save Complete!");
           
        }
         
    }
}