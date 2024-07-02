using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Application.Contracts.Persistence
{
    public interface IPostRepository:IGenericRepository<Post>
    {
        Task Update(Post post);

        Task <List<Post>> GetAllPost();

        Task<Post> GetPostById(Guid id);

        Task<List<Post>> GetAllPost(Guid? skipRecord,Guid? brandId);

        Task<List<Post>> GetAllPost(string? searchName,Guid? brandId, Guid? vechicletypeId);

    }
}
