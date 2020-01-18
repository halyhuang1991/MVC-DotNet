using System.Threading;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MVC_DotNet.Models;
using Microsoft.EntityFrameworkCore;
using MVC_DotNet.core;
using System.Collections.Generic;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace MVC_DotNet.Controllers
{
    public class TestController:BaseController
    {
        public IActionResult Index()
        {
            //Console.Write(value);
            //CallContext.SetData("DB",null);
            BaseDal<book> bookbll=new BaseDal<book>(db);
            var a=bookbll.GetList((x)=>true).ToList();
            var b=db.book.ToList();
            return View(b);
        }
        public JsonResult RabbitProduct(string input){
            //$.get("Test/RabbitProduct?input=ok",function(data){console.log(data)})
                    ConnectionFactory factory = new ConnectionFactory
                    {
                        UserName = "haly",//用户名
                        Password = "haly",//密码
                        HostName = "192.168.10.135"//rabbitmq ip
                    };

                    //创建连接
                    var connection = factory.CreateConnection();
                    //创建通道
                    var channel = connection.CreateModel();
                    //声明一个队列
                    channel.QueueDeclare("hello", false, false, false, null);

                    Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");

                    var sendBytes = Encoding.UTF8.GetBytes(input);
                        //发布消息
                     
                     channel.BasicPublish("", "hello", null, sendBytes);

                    channel.Close();
                    connection.Close();
                    return Json(new {returnObj="",msg="ok"});
        }
        public JsonResult RabbitConsume(){
            //$.get("Test/RabbitConsume",function(data){console.log(data)})
                    ConnectionFactory factory = new ConnectionFactory
                    {
                        UserName = "haly",//用户名
                        Password = "haly",//密码
                        HostName = "192.168.10.135"//rabbitmq ip
                    };

                    //创建连接
                    var connection = factory.CreateConnection();
                    //创建通道
                    var channel = connection.CreateModel();
                    string queueName="hello";
                    //事件基本消费者
                        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                        //接收到消息事件
                        consumer.Received += (ch, ea) =>
                        {
                            var message = Encoding.UTF8.GetString(ea.Body);

                            Console.WriteLine($"Queue:{queueName}收到消息： {message}");
                            //确认该消息已被消费
                            channel.BasicAck(ea.DeliveryTag, false);
                        };
                        //启动消费者 设置为手动应答消息
                        channel.BasicConsume(queueName, false, consumer);
                        Console.WriteLine($"Queue:{queueName}，消费者已启动");
                    Thread.Sleep(10);
                    channel.Close();
                    connection.Close();
                    return Json(new {returnObj="",msg="ok"});
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
        {//http://localhost:5000/Test/Detailq?id=1&name=213
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
        {//$.get('/Test/DetailAdd?id=1&name=213',function(){})
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
        public JsonResult GetConf(){

            return Json(new {msg="",result=""});
        }
        public JsonResult SelectRequest(){
            var rows=Request.Query["rows"];var page=Request.Query["page"];
            List<dynamic> ls=new List<dynamic>();
            for (int i = 0; i < 15; i++)
            {
                ls.Add(new {id=i,name="ok",price="ok"});
            }
            return Json(new {rows=ls,total=100});
        }
        public ActionResult bq(){
            return View();
        }
         protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}