using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_DotNet.Models;
using Microsoft.AspNetCore.Http;
namespace MVC_DotNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("home", "ok");
             var value = HttpContext.Session.GetString("home");
            Console.Write(value);
            return View();
        }

        public IActionResult About()
        {
            
            ViewData["Message"] = "Your application description page.";
           
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
