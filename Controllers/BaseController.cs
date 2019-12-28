using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_DotNet.Common;
using MVC_DotNet.core;

namespace MVC_DotNet.Controllers
{
    public class BaseController: Controller
    {
         
        public BooksContext db
        {
            get
            {  //从当前线程中获取 BooksContext
                BooksContext db = CallContext.GetData("DB") as BooksContext;
                if (db == null)
                {
                    db = new BooksContext();
                    //放入数据槽，来使线程内唯一
                    CallContext.SetData("DB", db);
                }
                return db;
            }
            
        }
        public BooksContext DB2
        {
            get
            {
                BooksContext db = null;
                if (HttpContext.Items["db1"] == null)
                {
                    db = new BooksContext();
                    HttpContext.Items["db1"] = db;
                }
                else
                {
                    db = HttpContext.Items["db1"] as BooksContext;
                }
                return db;
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}