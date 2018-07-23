using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_DotNet.Models;
namespace MVC_DotNet.Controllers
{
    public class MyhomeController:Controller
    {
         public IActionResult Index()
        {//http://localhost:5000/Myhome
            return View();
        }

    }
}