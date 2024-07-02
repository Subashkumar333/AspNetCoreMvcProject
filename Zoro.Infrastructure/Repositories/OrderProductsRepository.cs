using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Infrastructure.Comman;
using Zoro.Infrastructure.Migrations;
using OrderProducts = Zoro.Domain.Models.OrderProducts;

namespace Zoro.Infrastructure.Repositories
{
    public class OrderProductsRepository : GenericRepository<OrderProducts>, IOrderProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderProductsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrderProducts> GeTByIDAsync(string userId, string Id)
        {
            if (!Guid.TryParse(Id, out Guid guidId))
            {

                throw new ArgumentException("Invalid Id format", nameof(Id));
            }


            return await _context.OrderProducts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == guidId && x.UserID == userId);
        }

        public async Task<List<OrderProducts>> GetByUserIdAsync(string userId)
        {
            return await _context.OrderProducts.Where(x => x.UserID == userId).Select(order => new OrderProducts { ProductId = order.ProductId, Id = order.Id, ProductName = order.ProductName,PhoneNumber=order.PhoneNumber, UserAddress = order.UserAddress ,Price = order.Price, Quantity = order.Quantity, CreateOn = order.CreateOn,  Image = order.Image,TotalPrice= order.TotalPrice,OrderStatus=order.OrderStatus }).ToListAsync();
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _context.OrderProducts.CountAsync();
        }

        public async Task<int> GetTotalPendingOrdersAsync()
        {
            return await _context.OrderProducts.CountAsync(op => op.OrderStatus == "Pending");
        }

        public async Task<int> GetTotalReadyToPickOrdersAsync()
        {
            return await _context.OrderProducts.CountAsync(op => op.OrderStatus == "Ready_To_Pickup");
        }

        public async Task Update(OrderProducts orderProducts)
        {
            var existingProduct = await _dbcontext.OrderProducts.FindAsync(orderProducts.Id);

            if (existingProduct != null)
            {
               
                existingProduct.OrderStatus = orderProducts.OrderStatus;                        
                await _dbcontext.SaveChangesAsync();
            }
            else
            {              
                throw new ArgumentException("Product not found", nameof(OrderProducts));
            }
        }
    }
}
