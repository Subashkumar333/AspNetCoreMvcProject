using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Infrastructure.Comman;

namespace Zoro.Infrastructure.Repositories
{
    public class CartProductsRepository : GenericRepository<CarTproducts>, ICartProductsRepository
    {

        
        public CartProductsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
         
        }

        public async Task<bool> CheckIfItemAddedToCart(string userId, string productId)
        {
            var cartItem = await _dbcontext.CarTproducts
                                           .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductId == productId);
            return cartItem != null;
        }



        public async Task<List<CarTproducts>> GetAllCartByIdAsync(string userId)
        {

            return await _dbcontext.CarTproducts.Where(x => x.UserID == userId).Select(cart => new CarTproducts { ProductId=cart.ProductId, Id=cart.Id,ProductName = cart.ProductName, Price = cart.Price, Quantity = cart.Quantity, CreateOn = cart.CreateOn, CreatedBy = cart.CreatedBy,Image=cart.Image }).ToListAsync();
                
                
         }



        public async Task<CarTproducts> GeTByIDAsync(string userId, string Id)
        {

            if (!Guid.TryParse(Id, out Guid guidId))
            {
               
                throw new ArgumentException("Invalid Id format", nameof(Id));
            }

            return await _dbcontext.CarTproducts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == guidId && x.UserID == userId);
        }

        public async Task UpdateAsync(CarTproducts carTproducts)
        {
            var existingProduct = await _dbcontext.CarTproducts.FindAsync(carTproducts.Id);

            if (existingProduct != null)
            {
                
                existingProduct.ProductName = carTproducts.ProductName;
                existingProduct.Price = carTproducts.Price;
                existingProduct.Quantity = carTproducts.Quantity;
                

               
                await _dbcontext.SaveChangesAsync();
            }
            else
            {
                
                throw new ArgumentException("Product not found", nameof(carTproducts));
            }
        }
    }
}
