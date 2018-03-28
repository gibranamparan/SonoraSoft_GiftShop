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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GiftShop1.Models;

namespace GiftShop1.Controllers
{
    public class PurchasesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Purchases
        [Authorize(Roles = ApplicationUser.RoleNames.ADMIN+","+ApplicationUser.RoleNames.BUYER)]
        //public IQueryable<PurchaseCart> GetPurchases()
        public IEnumerable<PurchaseCart.VMPurchaseCart> GetPurchases()
        {
            string userID = User.IsInRole(ApplicationUser.RoleNames.ADMIN) ? null : User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userID))
            {
                var res = db.Purchases.ToList().Select(pur => new PurchaseCart.VMPurchaseCart(pur));
                return res;
            }
            else
            {
                var res = db.Purchases.ToList().Where(pur => pur.buyerID == userID).Select(pur => new PurchaseCart.VMPurchaseCart(pur));
                return res;
            }
        }

        // GET: api/Purchases/5
        [Authorize]
        [ResponseType(typeof(PurchaseCart.VMPurchaseCart))]
        public IHttpActionResult GetPurchases(int id)
        {
            PurchaseCart purchases = db.Purchases.Find(id);
            if (purchases == null)
            {
                return NotFound();
            }

            PurchaseCart.VMPurchaseCart vmPurchase = new PurchaseCart.VMPurchaseCart(purchases);

            return Ok(vmPurchase);
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
        [Authorize]
        public IHttpActionResult PostPurchases(PurchaseCart purchase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            purchase.createdAt = DateTime.Now;
            purchase.boughtAt = DateTime.Now;
            purchase.buyerID = string.IsNullOrEmpty(purchase.buyerID) ? User.Identity.GetUserId() : purchase.buyerID;

            var orders = purchase.products.ToList();
            orders.ForEach(orderProduct => orderProduct.product = null);
            purchase.products = orders;
            db.Purchases.Add(purchase);
            int count = db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase.purchaseID, count }, purchase);
        }

        // DELETE: api/Purchases/5
        [ResponseType(typeof(PurchaseCart))]
        [Authorize(Roles = ApplicationUser.RoleNames.ADMIN)]
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