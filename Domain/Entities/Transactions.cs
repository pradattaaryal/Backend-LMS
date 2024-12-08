using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string TransactionType { get; set; } // Borrow or Return
        public DateTime Date { get; set; }
    }
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string TransactionType { get; set; } // Borrow or Return
        public DateTime Date { get; set; }
    }
}
