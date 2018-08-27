using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_DotNet.Models;
using StackExchange.Redis;

namespace MVC_DotNet.Controllers
{
    public class MyhomeController:Controller
    {
        public readonly IConnectionMultiplexer _connect ;
         public readonly IOperation _Iop ;
        public MyhomeController(IConnectionMultiplexer connect,IOperation iop){
            _connect = connect;    
             _Iop = iop;            
        }
        
       
        [AllowAnonymous]
         public IActionResult Index()
        {//http://localhost:5000/Myhome
           HashEntry[] hs=new HashEntry[]{};
           hs.Append(new HashEntry("as","sds"));
           hs.Append(new HashEntry("as1","sds"));
            _connect.GetDatabase().StringSet("session","ok");
            string value=_connect.GetDatabase().StringGet("session").ToString();
            _Iop.run();
            ClaimsIdentity claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            UserModel user=new UserModel();
             user.username ="halyhuang";
             user.id=1;
             user.password ="ok";
           
            return View(user);
        }
        public IActionResult Login()
        {//http://localhost:5000/Myhome/Login
             string value=_connect.GetDatabase().StringGet("session").ToString();
             if(value=="ok")return RedirectToAction("Index");
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
           ViewData["Message"] = "Your contact page.";
            string user = userModel.username;
            return Content("Save Complete!");
           
        }
         public IActionResult part()
        {
            ViewData["Message"] = "Your contact page.";
            List<UserModel> list=new List<UserModel>();
            for (int i = 0; i < 4; i++)
            {
                list.Add(new UserModel { id = i,username = "名称_"+i});
            }
              return PartialView(list);
        }
       

    }
}