using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestMembership.Models
{
    public class Products
    {

        public Products(int id, string name, decimal price) //id, name, price, fetch based on id, update price
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public Products()
        {
           
        }

        public int? Id { get; set; }
   
        public decimal Price { get; set; }

        public string Name { get; set; }
        
    }
}