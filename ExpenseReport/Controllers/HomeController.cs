using ExpenseReport.Models;
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
            var expenseList = DB.Expenses.ToList();

            // Group transactions by month
            var groupMonths = expenseList.GroupBy(x => x.TransactionMonth)
                .Select(group => new { Expens = group.Key, expenseList = group.ToList() })
                .ToList();

            var itemList = new List<List<Expens>>();
            foreach (var transaction in groupMonths)
            {
                itemList.Add(transaction.expenseList);
            }
            
            var monthList = new List<Monthly>();
            
            foreach (var list in itemList)
            {
                var month = new Monthly();
                
                foreach (var item in list)
                {
                    month.TotalSpent += item.Amount;
                    month.TransactionMonth = item.TransactionMonth;
                }

                month.NumOfTransactions = list.Count;
                month.AverageSpent = Decimal.Round((month.TotalSpent / month.NumOfTransactions), 2);
                monthList.Add(month);
            }

            ViewBag.Months = monthList;

            return View(expenseList);
        }

        public ActionResult Yearly()
        {
            var expenseList = DB.Expenses.ToList();

            var yearTotal = new Yearly();
            yearTotal.NumOfTransactions = expenseList.Count;

            foreach (var item in expenseList)
            {
                yearTotal.TotalSpent += item.Amount;
            }

            var avg = yearTotal.TotalSpent / yearTotal.NumOfTransactions;
            yearTotal.AverageSpent = Decimal.Round(avg, 2);

            ViewBag.TotalSpent = yearTotal.TotalSpent;
            ViewBag.NumTransactions = yearTotal.NumOfTransactions;
            ViewBag.AverageSpent = yearTotal.AverageSpent;

            var transType = expenseList.GroupBy(x => x.TransactionType)
                .Select(group => new { Expens = group.Key, expenseList = group.ToList() })
                .ToList();

            ViewBag.Titles = expenseList.Select(x => x.TransactionType).Distinct().ToList();

            var yearlyList = new List<Yearly>();

            foreach (var lines in transType)
            {
                var year = new Yearly();
                year.TransactionType = lines.Expens;
                year.NumOfTransactions = lines.expenseList.Count;
                decimal total = 0;

                foreach (var item in lines.expenseList)
                {
                    total += item.Amount;
                }
                decimal average = total / year.NumOfTransactions;
                year.TotalSpent = total;
                year.AverageSpent = Decimal.Round(average, 2);

                yearlyList.Add(year);
            }

            return View(yearlyList);
        }

        public ActionResult Income()
        {
            var incomeList = DB.Incomes.ToList();

            return View();
        }

        public ActionResult Submit()
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

            return View("Submit");
        }

    }
}