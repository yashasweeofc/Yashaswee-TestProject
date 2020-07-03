using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yashaswee.ProcessOrders.Web.Models;

namespace Yashaswee.ProcessOrders.Web.Controllers
{
    public class PaymentActionsController : Controller
    {
        private OrderProcessingBREEntities db = new OrderProcessingBREEntities();

        // GET: PaymentActions
        public ActionResult Index()
        {
            var paymentActions = db.PaymentActions.Include(p => p.PaymentType);
            return View(paymentActions.ToList());
        }

        // GET: PaymentActions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAction paymentAction = db.PaymentActions.Find(id);
            if (paymentAction == null)
            {
                return HttpNotFound();
            }
            return View(paymentAction);
        }

        // GET: PaymentActions/Create
        public ActionResult Create()
        {
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "PaymentCode");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentActionID,PaymentActionCode,PaymentActionCodeDescription,PaymentTypeID,CodeStatus")] PaymentAction paymentAction)
        {
            if (ModelState.IsValid)
            {
                db.PaymentActions.Add(paymentAction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "PaymentCode", paymentAction.PaymentTypeID);
            return View(paymentAction);
        }

        // GET: PaymentActions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentAction paymentAction = db.PaymentActions.Find(id);
            if (paymentAction == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "PaymentCode", paymentAction.PaymentTypeID);
            return View(paymentAction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentActionID,PaymentActionCode,PaymentActionCodeDescription,PaymentTypeID,CodeStatus")] PaymentAction paymentAction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentAction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentTypeID = new SelectList(db.PaymentTypes, "PaymentTypeID", "PaymentCode", paymentAction.PaymentTypeID);
            return View(paymentAction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
