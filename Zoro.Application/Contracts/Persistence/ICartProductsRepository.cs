using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Application.Contracts.Persistence
{
    public interface ICartProductsRepository : IGenericRepository<CarTproducts>
    {

        Task UpdateAsync(CarTproducts carTproducts);

        Task<List<CarTproducts>> GetAllCartByIdAsync(string userId);
        Task<bool> CheckIfItemAddedToCart(string userId, string productId);
        Task<CarTproducts> GeTByIDAsync(string userId, string Id);
    }
}
