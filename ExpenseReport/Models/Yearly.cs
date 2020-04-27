using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseReport.Models
{
    public class Yearly
    {
        public int NumOfTransactions { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal AverageSpent { get; set; }
        public string TransactionType { get; set; }
    }
}