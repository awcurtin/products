using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestMembership.Components;
using TestMembership.Models;

namespace TestMembership.Controllers
{
     

    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        public static List<Products> database = new List<Products>();
     
        // GET api/values
        //[Route(Name = "route1")]
        public List<Products> Get()
        {
            return PocoRepository.GetProducts().ToList();
            //return ProductRepository.GetProducts().ToList();
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            Products product = ProductRepository.GetProduct(id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            bool deleted = ProductRepository.RemoveProduct(id);

            if (deleted)
            {
                //modify this with SQL to affect actual db
                //database.Remove(product);
                return Ok();
                //still modifying
            }

            return NotFound();
           
        }


        //[Route(Name = "andrew")]
        [HttpPost]
        public Products Andrew(Products value)
        {
            //value.Name = "Bob";
            database.Add(value);
            return value;
        }

        [HttpPost]
        public async Task<Products> AddProduct(Products p)
        {   
            
            return await ProductRepository.AddProduct(p);
        }

        [HttpPost]
        public async Task<Products> UpdatePrice(Products p)
        {

            return await ProductRepository.ChangePrice((int) p.Id, p.Price);
        }

        [HttpPost]
        public Products ChangePrice(Products p)
        {
            //return PocoRepository.ChangePrice((int) p.Id, p.Price);
            return PocoRepository.ChangePrice(p);
        }
        [HttpPost]
        public List<Products> PostAll(List<Products> entries)
        {
            
            return PocoRepository.AddAll(entries).ToList();

            //database.AddRange(entries);
            
        }

        [HttpPatch]
        public IHttpActionResult SetPrice(int id, decimal price)
        {
            Products product = database.SingleOrDefault(x => x.Id == id);
            if (product != null)
            {
                product.Price = price;
                return Ok(product);
            }

            return NotFound();
        }

        [HttpGet]
        public List<Products> Range(decimal upper, decimal lower = 0)
        {
            List<Products> itemsInRange = database.FindAll(x => x.Price >= lower && x.Price <= upper);
            return itemsInRange;
        }
    }
}
