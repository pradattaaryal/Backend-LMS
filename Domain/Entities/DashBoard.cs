using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DashBoard
    {
        public int TotalUserBase { get; set; }
        public int TotalBookCount { get; set; }
        public int BranchCount { get; set; }
        public int TotalBorrowedBooks { get; set; }
        public int TotalReturnedBooks { get; set; }
        public List<OverdueBorrower> OverdueBorrowers { get; set; }
    }
    public class OverdueBorrower
    {
        public string Name { get; set; }
        public string BorrowedId { get; set; }
    }
}
