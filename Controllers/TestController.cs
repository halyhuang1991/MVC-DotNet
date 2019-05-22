using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVC_DotNet.Models;
using Microsoft.EntityFrameworkCore;
using MVC_DotNet.core;

namespace MVC_DotNet.Controllers
{
    public class TestController:BaseController
    {
        public IActionResult Index()
        {
            //Console.Write(value);
            return View(db.book.ToList());
        }
        public ActionResult Detail(int id = 0)
        {
            book order = db.book.Find(id);
            if (order == null)
            {
                return View(null);
            }
            return View(order);
        }
        public ActionResult Detailq(book book1)
        {
            if (ModelState.IsValid)
            {
                book order = db.book.Find(book1.id);
                if (order == null)
                {
                    return View(null);
                }
                return View(order);
            }
            else
            {//localhost:5000/Test/Detailq?id=null&name=12&booknum=12
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult DetailAdd(book order)
        {
            if(order.id==0)return View(order);
            if (ModelState.IsValid)
            {
                book order2 = db.book.Find(order.id);
               if(order2==null){
                    db.book.Add(order);
               }else{
                   //update all columns
                //    db.NewInstance();
                //    db.Entry(order).State = EntityState.Modified;
                //update name
                //  db.NewInstance();
                // db.book.Attach(order);
                // db.Entry(order).Property("name").IsModified = true;
                 order2.name=order.name;//update name
                    
               }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }
         protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}