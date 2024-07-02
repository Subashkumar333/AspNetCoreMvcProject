using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoro.Application.Contracts.Persistence
{
    public interface IUnitOfWork:IDisposable
    {
        public IBrandRepository Brand { get; }

        public IVehicleTypesRepository vehicleTypes { get; }

        public IPostRepository Post { get; }

        public ICartProductsRepository cartProducts { get; }

        public IOrderProductsRepository OrderProducts { get; }

        public IContactUsInformationRepoitory ContactUsData { get; }

        public ICompletedOrderRepository CompletedOrders { get; }
        Task SaveAsync();
    }
}
