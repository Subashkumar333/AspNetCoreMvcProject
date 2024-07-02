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
    public class CompletedOrderRepository : GenericRepository<CompletedOrders>, ICompletedOrderRepository
    {
        public CompletedOrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {



        }


        public async Task<List<CompletedOrders>> GetAllCartByIdAsync(string userId)
        {
            return await _dbcontext.CompletedOrders.Where(x => x.UserID == userId).Select(Corder => new CompletedOrders { ProductId = Corder.ProductId, Id = Corder.Id,OrderId= Corder.OrderId, ProductName = Corder.ProductName, PhoneNumber = Corder.PhoneNumber, UserAddress = Corder.UserAddress, Price = Corder.Price, Quantity = Corder.Quantity, CreateOn = Corder.CreateOn, Image = Corder.Image, TotalPrice = Corder.TotalPrice, OrderStatus = Corder.OrderStatus,ModifiedOn= Corder.ModifiedOn,UserName= Corder.UserName }).ToListAsync();
        }

        public async Task<double> GetTotalAmountAsync()
        {
            double  totalAmount = await _dbcontext.CompletedOrders.SumAsync(o => o.TotalPrice);
            return totalAmount;
        }

        public async Task<int> GetTotalCompletedOrdersAsync()
        {
            return await _dbcontext.CompletedOrders.CountAsync();
        }
    }
}
