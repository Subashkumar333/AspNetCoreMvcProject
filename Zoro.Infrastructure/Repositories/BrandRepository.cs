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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {

        public BrandRepository(ApplicationDbContext dbContext): base(dbContext)
        { 
        
        
        
        }     

            
        public async Task Update(Brand brand)
        {
            var obfromDb = await _dbcontext.Brand.FirstOrDefaultAsync(x => x.Id == brand.Id);

            if(obfromDb != null)
            {

                obfromDb.Name = brand.Name;

                obfromDb.PublishYear = brand.PublishYear;

                if(brand.BrandLogo!=null)
                {

                    obfromDb.BrandLogo = brand.BrandLogo;

                }

                _dbcontext.Update(obfromDb);

            }



        }
    }
}
