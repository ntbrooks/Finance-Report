using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExpenseReport;

namespace ExpenseReport.Controllers
{
    public class SubmissionController : Controller
    {
        private ExpensesEntities db = new ExpensesEntities();

        // GET: Submission
        public ActionResult Index()
        {
            return View();
        }


        // GET: Submission/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Submission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransactionType,Name,Amount,WorkExpense,Date,CreatedDate,TransactionMonth")] Expens expens)
        {
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expens);
        }




        //// GET: Submission/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Expens expens = db.Expenses.Find(id);
        //    if (expens == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(expens);
        //}

        //// POST: Submission/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Expens expens = db.Expenses.Find(id);
        //    db.Expenses.Remove(expens);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
