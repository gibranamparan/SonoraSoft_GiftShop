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
    public class PurchasesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Purchases
        public IQueryable<PurchaseCart> GetPurchases()
        {
            return db.Purchases;
        }

        // GET: api/Purchases/5
        [ResponseType(typeof(PurchaseCart))]
        public IHttpActionResult GetPurchases(int id)
        {
            PurchaseCart purchases = db.Purchases.Find(id);
            if (purchases == null)
            {
                return NotFound();
            }

            return Ok(purchases);
        }

        // PUT: api/Purchases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchases(int id, PurchaseCart purchases)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchases.purchaseID)
            {
                return BadRequest();
            }

            db.Entry(purchases).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Purchases
        [ResponseType(typeof(PurchaseCart))]
        public IHttpActionResult PostPurchases(PurchaseCart purchase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            purchase.createdAt = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.purchaseID }, purchase);
        }

        // DELETE: api/Purchases/5
        [ResponseType(typeof(PurchaseCart))]
        public IHttpActionResult DeletePurchases(int id)
        {
            PurchaseCart purchases = db.Purchases.Find(id);
            if (purchases == null)
            {
                return NotFound();
            }

            db.Purchases.Remove(purchases);
            db.SaveChanges();

            return Ok(purchases);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchasesExists(int id)
        {
            return db.Purchases.Count(e => e.purchaseID == id) > 0;
        }
    }
}