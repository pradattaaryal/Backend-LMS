using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IDashboardRepository
    {
 
        Task<DashBoard> GetDashboardDataAsync();
 
        Task<IEnumerable<OverdueBorrower>> GetOverdueBorrowersAsync();
    }
}
