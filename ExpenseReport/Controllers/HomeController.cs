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
            var expenseTable = DB.Expenses.ToList();
            var insertTable = new List<String>();

            foreach (var e in expenseTable)
            {
                var text = "<div>";
                var endText = "</div>";
                text += e.TransactionType + " " + e.Name + " $" + e.Amount + " " + e.Date + endText;
            }

            return View();
        }

        public ActionResult Bills()
        {
            var billsList = DB.Expenses.ToList();

            return View();
        }

        public ActionResult Expense()
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

        public ActionResult Income()
        {
            var incomeList = DB.Incomes.ToList();

            return View();
        }

    }
}