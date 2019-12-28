using Microsoft.EntityFrameworkCore;
using MVC_DotNet.core;

namespace MVC_DotNet.Common
{
    public class BooksContext:DbContext
    {
        private DbContextOptionsBuilder _optionsBuilder;
        public DbSet<Models.book> book { get; set; } //和表同名
        private string connstr{ get; set; }
        public BooksContext(){
           // _optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=test; User=root;Password=;"); 
           var hostname = "X7IBH6D8QPECHMU";
           var username="haly";
            var password = "admin";
            var connString = $"Data Source={hostname};Initial Catalog=test;User ID={username};Password={password};pooling=false;Packet Size=512";
           //_optionsBuilder.UseSqlServer(connString); 
           connstr=connString;
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          // optionsBuilder=_optionsBuilder;
          optionsBuilder.UseSqlServer(this.connstr); 
        }
        public void NewInstance(){
             CallContext.SetData("DB",null);
        }
    }
    
}