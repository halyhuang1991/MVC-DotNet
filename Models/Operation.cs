using System;

namespace MVC_DotNet.Models
{
    public interface IOperation
    {
      
        void run();
        string a{get;set;}
    }
    public interface IOperationScoped : IOperation
    {
    }
    public interface IOperationTransient : IOperation
    {
    }
    
    public interface IOperationSingleton : IOperation
    {
    }
    public interface IOperationSingletonInstance : IOperation
    {
    }
    public class bar:IOperation
    {
        public string a{get;set;}
        public void run(){
            Console.WriteLine("server ok!");
        }
        

    }
}