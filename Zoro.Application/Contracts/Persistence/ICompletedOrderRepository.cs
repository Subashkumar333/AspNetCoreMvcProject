using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Application.Contracts.Persistence
{
    public interface ICompletedOrderRepository: IGenericRepository<CompletedOrders>
    {
      

        Task<List<CompletedOrders>> GetAllCartByIdAsync(string userId);

        Task<int> GetTotalCompletedOrdersAsync();

        Task<double> GetTotalAmountAsync();

    }
}
