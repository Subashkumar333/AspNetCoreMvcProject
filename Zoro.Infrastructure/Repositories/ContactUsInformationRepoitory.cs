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
    public class ContactUsInformationRepoitory : GenericRepository<ContactusData>, IContactUsInformationRepoitory
    {

        public ContactUsInformationRepoitory(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<ContactusData>> GetAllMessageAsync()
        {
            return await _dbcontext.ContactusData.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetFeedBacksCountAsync()
        {
            return await _dbcontext.ContactusData.CountAsync();
        }

        public async Task<IEnumerable<ContactusData>> GetRecentSubmissionsAsync(int count)
        {
            return await _dbcontext.ContactusData
                                 .OrderByDescending(c => c.CreateOn)
                                 .Take(count)
                                 .ToListAsync();
        }
    }
}
