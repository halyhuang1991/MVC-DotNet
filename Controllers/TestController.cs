using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVC_DotNet.Models;
using Microsoft.EntityFrameworkCore;
using MVC_DotNet.core;
using System.Collections.Generic;
using Newtonsoft.Json;

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
         public ActionResult DetailPart(){
             List<book> ls=db.book.ToList();
             return View("~/Views/Shared/part.cshtml",ls);
         }
        public ActionResult Index1(int? pageSize, int? pageIndex)
        {
            int pageIndex1 = pageIndex ?? 1;
            int pageSize1 = pageSize ?? 5;
            int count = 0;
            //从数据库在取得数据，并返回总记录数
            BaseDal<book> newsSer=new BaseDal<book>(db);
            var temp = newsSer.LoadPageEntities<int>(pageSize1, pageIndex1, 
            out count, t=>true, tempbook=>tempbook.id,true);
            PagerInfo pager = new PagerInfo();
            pager.CurrentPageIndex = pageIndex1;
            pager.PageSize = pageSize1;
            pager.RecordCount = count;
            PagerQuery<PagerInfo, IQueryable<book>> query = new PagerQuery<PagerInfo, IQueryable<book>>(pager, temp);
            ViewData["PagerInfo"]=query.Pager;
            ViewData["books"]=query.EntityList.ToList();
            return PartialView(query);
        }
        public ActionResult usersList() //返回类型也可写 JsonResult
        {
            var List = db.book.Select(x => new { CustomerID = x.id, ContactName = x.name }).ToList();
            return Json(List);
        }
         protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}