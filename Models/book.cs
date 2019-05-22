using System.ComponentModel.DataAnnotations;

namespace MVC_DotNet.Models
{
    public class book
    {
        [Required()]
        public int id{get;set;}
        public string name{get;set;}
        public int? booknum{get;set;}
    }
}