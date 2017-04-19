using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using TestMembership.Models;
using Microsoft.Data.OData;
using TestMembership.Components;

namespace TestMembership.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TestMembership.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Products>("Products");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/Products
        public IHttpActionResult GetProducts(ODataQueryOptions<Products> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            
            
            //return Ok<IEnumerable<Products>>(products);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/Products(5)
        public IHttpActionResult GetProducts([FromODataUri] int? key, ODataQueryOptions<Products> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<Products>(products);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Products(5)
        public IHttpActionResult Put([FromODataUri] int? key, Delta<Products> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(products);

            // TODO: Save the patched entity.

            // return Updated(products);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Products
        public IHttpActionResult Post(Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(products);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Products(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int? key, Delta<Products> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(products);

            // TODO: Save the patched entity.

            // return Updated(products);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Products(5)
        public IHttpActionResult Delete([FromODataUri] int? key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
