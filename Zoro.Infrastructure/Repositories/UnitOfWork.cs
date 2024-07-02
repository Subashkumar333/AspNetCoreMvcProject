using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Infrastructure.Comman;
using Zoro.Infrastructure.Migrations;

namespace Zoro.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _HttpcontextAccessor;

        public UnitOfWork(ApplicationDbContext dbcontext, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
            _HttpcontextAccessor = contextAccessor;

            Brand = new BrandRepository(_dbcontext);

            vehicleTypes = new VechicleTypesRepository(_dbcontext);

            Post = new PostRepository(_dbcontext);

            cartProducts = new CartProductsRepository(_dbcontext);

            OrderProducts = new OrderProductsRepository(_dbcontext);

            ContactUsData = new ContactUsInformationRepoitory(_dbcontext);

            CompletedOrders= new CompletedOrderRepository(_dbcontext);

        }

        public IBrandRepository Brand { get; private set; }

        public IVehicleTypesRepository vehicleTypes { get; private set; }

       

        public IPostRepository Post { get; private set; }

        public ICartProductsRepository cartProducts { get; private set; }

        public IOrderProductsRepository OrderProducts { get; private set; }

        public IContactUsInformationRepoitory ContactUsData { get; private set; }

        public ICompletedOrderRepository CompletedOrders { get; private set; }

        public void Dispose()
        {
          _dbcontext.Dispose(); 
        }

        public async Task SaveAsync()
        {
            _dbcontext.SaveCommonFields(_userManager, _HttpcontextAccessor);

            await _dbcontext.SaveChangesAsync();
        }
    }
}
