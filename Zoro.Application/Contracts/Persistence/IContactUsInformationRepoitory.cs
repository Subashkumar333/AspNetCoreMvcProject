using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Application.Contracts.Persistence
{
    public interface IContactUsInformationRepoitory: IGenericRepository<ContactusData>
    {
        Task<IEnumerable<ContactusData>> GetRecentSubmissionsAsync(int count);


        Task<int> GetFeedBacksCountAsync();


        Task<List<ContactusData>> GetAllMessageAsync();


    }
}
