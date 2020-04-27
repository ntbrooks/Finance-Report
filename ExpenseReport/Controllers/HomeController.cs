using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseReport.Controllers
{
    public class HomeController : Controller
    {
        ExpensesEntities DB = new ExpensesEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Bills()
        {
            var billsList = DB.Expenses.ToList();

            return View();
        }

        public ActionResult Monthly()
        {
            var expenses = new List<Expens>();
            var expenseList = DB.Expenses.ToList();

            foreach (var item in expenseList)
            {
                Expens expense = new Expens();
                expense.Amount = item.Amount;
                expense.CreatedDate = item.CreatedDate;
                expense.Date = item.Date;
                expense.Id = item.Id;
                expense.Name = item.Name;
                expense.TransactionType = item.TransactionType;
                expense.WorkExpense = item.WorkExpense;

                expenses.Add(expense);
            }

            return View(expenseList);
        }

        public ActionResult Yearly()
        {
            var expenseList = DB.Expenses.ToList();

            var transType = expenseList.GroupBy(x => x.TransactionType)
                .Select(group => new { Expens = group.Key, expenseList = group.ToList() })
                .ToList();

            ViewBag.Titles = expenseList.Select(x => x.TransactionType).Distinct().ToList();



            return View(expenseList);
        }

        public ActionResult Income()
        {
            var incomeList = DB.Incomes.ToList();

            return View();
        }

        public ActionResult Submission()
        {
            return View();
        }

        public ActionResult AddTransaction(Expens expense)
        {
            try
            {
                var transaction = new Expens()
                {
                    Amount = expense.Amount,
                    CreatedDate = DateTime.Now,
                    Date = expense.Date,
                    Name = expense.Name,
                    TransactionMonth = expense.Date.Month.ToString(),
                    TransactionType = expense.TransactionType,
                    WorkExpense = expense.WorkExpense,
                };


                DB.Expenses.Add(transaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            DB.SaveChanges();

            return View("Submission");
        }

    }
}