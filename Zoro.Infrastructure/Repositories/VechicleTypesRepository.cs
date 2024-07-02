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
    public class VechicleTypesRepository : GenericRepository<VechicleTypes>, IVehicleTypesRepository
    {
      
        public VechicleTypesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {



        }

        public async Task Update(VechicleTypes vechicleTypes)
        {
            var obfromDb = await _dbcontext.VechicleTypes.FirstOrDefaultAsync(x=>x.Id==vechicleTypes.Id);

            if (obfromDb != null)
            {

                obfromDb.Name = vechicleTypes.Name;

               

               _dbcontext.Update(obfromDb);

            }


        }
    }
}
