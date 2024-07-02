using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Application.Contracts.Persistence
{
    public interface IOrderProductsRepository: IGenericRepository<OrderProducts>
    {
        Task<OrderProducts> GeTByIDAsync(string userId, string Id);
       
        Task<List<OrderProducts>> GetByUserIdAsync(string userId);

        Task Update(OrderProducts orderProducts);

        Task<int> GetTotalOrdersAsync();
        Task<int> GetTotalPendingOrdersAsync();

        Task<int> GetTotalReadyToPickOrdersAsync();
    }
}
