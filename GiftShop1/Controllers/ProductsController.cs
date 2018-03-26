using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GiftShop1.Models;

namespace GiftShop1.Controllers
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Products
        //public IEnumerable<Product.VMProduct> GetProducts()
        public IEnumerable<Product.VMProduct> GetProducts()
        {
            var products = from prod in db.Products.ToList()
                            select new Product.VMProduct(prod);
            return products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(new Product.VMProduct(product));
        }

        // PUT: api/Products/5
        /*[ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.productID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult PutProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Entry(product).State = EntityState.Modified;

            try
            {
                int count = db.SaveChanges();
                return Ok(new { count, product });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.productID))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(HttpStatusCode.NoContent);
                    throw;
                }
            }

        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        //[Authorize(Roles = ApplicationUser.RoleNames.ADMIN)]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            db.Entry(product).Reference(prod => prod.category).Load();
            return CreatedAtRoute("DefaultApi", new { id = product.productID }, new Product.VMProduct(product));
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        [Authorize(Roles = ApplicationUser.RoleNames.ADMIN)]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            int count = db.SaveChanges();

            return Ok(new { count , product });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.productID == id) > 0;
        }
    }
}