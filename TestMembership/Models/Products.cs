using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        [Range(0, 10000, ErrorMessage = "Price must be in range [0,10000]"), Required(ErrorMessage = "Price must be supplied")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Name must be supplied")]
        public string Name { get; set; }
        
    }
}