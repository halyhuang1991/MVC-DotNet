using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVC_DotNet.Models;

namespace MVC_DotNet.Controllers
{
    public static class TestUserStorage
{
    public static List<UserModel> UserList { get; set; } = new List<UserModel>() {
        new UserModel { username = "halyhuang",password = "112233"}
    };
}
    public class AccountController:Controller
    {
         public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel userFromFore)
        {
            var userFromStorage = TestUserStorage.UserList
                .FirstOrDefault(m => m.username == userFromFore.username && m.password == userFromFore.password);

            if (userFromStorage != null)
            {
                //you can add all of ClaimTypes in this collection 
                var claims = new List<Claim>(){
                    new Claim(ClaimTypes.Name,userFromStorage.username) 
                };

                //init the identity instances 
                var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "SuperSecureLogin"));

                //signin 
                try{
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
                    return Ok("/Myhome/index");
                    //return RedirectToAction("index", "MyHome");
                    //return Redirect("/Myhome/index");
                   

                }catch(Exception ex){
                    Console.WriteLine(ex.Message);
                   return Ok("/Myhome/Login");
                  //return RedirectToAction("Login", "MyHome");
                }
               
                

                
            }
            else
            {
                // ViewBag.ErrMsg = "UserName or Password is invalid";
                
                // return RedirectToAction("Login", "MyHome");
                return Ok("/Myhome/Login");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookie");

            return RedirectToAction("Login", "Home");
        }
    }
}