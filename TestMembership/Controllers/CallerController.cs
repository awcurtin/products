using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestMembership.Models;

namespace TestMembership.Controllers
{
    public class CallerController : ApiController
    {
        private static string uri = "http://localhost:49937/";

        // GET: api/Caller
        public async Task<List<Products>> Get() //Task<HttpResponseMessage>
        {
            var client = new HttpClient();
            
            client.BaseAddress = new Uri(uri);
            var response = await client.GetAsync("api/values/get");

            var test = await response.Content.ReadAsAsync<List<Products>>();

           // var blah = JsonConvert.DeserializeObject<List <Products>>(test);
            test.Add(new Products(44, "Added", 636.66m));

            //JsonSerializer don't use this

            //return new string[] { "value1", "value2" };
            return test;
        }

        // GET: api/Caller/5
        public async Task<IHttpActionResult> GetTwo(int id1, int id2) //IHttpActionResult async Task<List<Products>>
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);

            HttpResponseMessage firstResponse;
            try { 
                firstResponse = await client.GetAsync("api/values/get/" + id1);
            } catch (HttpRequestException e) {
                return InternalServerError();
            }

            if (!firstResponse.IsSuccessStatusCode)
            {   
                return NotFound();
            } 

            HttpResponseMessage secondResponse;
            try{
                secondResponse = await client.GetAsync("api/values/get/" + id2);
            } catch (HttpRequestException e) {
                return InternalServerError();
            }

            if (!secondResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var firstResult = await firstResponse.Content.ReadAsAsync<Products>();
            var secondResult = await secondResponse.Content.ReadAsAsync<Products>();

            List<Products> results = new List<Products>() { firstResult, secondResult, new Products(99, "Got it", 0.00m) };

            return Ok(results);
        }

        // POST: api/Caller
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Caller/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Caller/5
        public void Delete(int id)
        {
        }

        /* Divider here, testing stuff*/

    }
}
