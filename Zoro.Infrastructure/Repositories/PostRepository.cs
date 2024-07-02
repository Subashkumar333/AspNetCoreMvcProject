using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Application.Contracts.Persistence;
using Zoro.Domain.Models;
using Zoro.Infrastructure.Comman;

namespace Zoro.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {

        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {



        }

        public async Task<List<Post>> GetAllPost()
        {
            return await _dbcontext.Post.Include(x => x.Brand).Include(x => x.VechicleTypes).ToListAsync();
        }

        public async Task<List<Post>> GetAllPost(Guid? skipRecord, Guid? brandId)
        {
            var query = _dbcontext.Post.Include(x => x.Brand).Include(x => x.VechicleTypes).OrderByDescending(x => x.ModifiedOn);

            if (brandId != Guid.Empty)
            {
                query = (IOrderedQueryable<Post>)query.Where(x => x.BrandId == brandId);
            }

            var posts = await query.ToListAsync();


            if (skipRecord.HasValue)
            {
                var recordToRemove = posts.FirstOrDefault(x => x.Id == skipRecord.Value);
                if (recordToRemove != null)
                {
                    posts.Remove(recordToRemove);
                }
            }

            return posts;


        }

        public async Task<List<Post>> GetAllPost(string searchName, Guid? brandId, Guid? vechicletypeId)
        {
            var query = _dbcontext.Post.Include(x => x.Brand).Include(x => x.VechicleTypes).OrderByDescending(x => x.ModifiedOn);

            if (searchName == string.Empty && brandId == Guid.Empty && vechicletypeId == Guid.Empty)
            {
                return await query.ToListAsync();
            }

            if (brandId != Guid.Empty)
            {
                query = (IOrderedQueryable<Post>)query.Where(x => x.BrandId == brandId);
            }

            if (vechicletypeId != Guid.Empty)
            {
                query = (IOrderedQueryable<Post>)query.Where(x => x.VehicleTypeId == vechicletypeId);
            }

            if (!string.IsNullOrEmpty(searchName))
            {
                query = (IOrderedQueryable<Post>)query.Where(x => x.Name.Contains(searchName));
            }

            return await query.ToListAsync();


        }

        public async Task<Post> GetPostById(Guid id)
        {
            return await _dbcontext.Post.Include(x => x.Brand).Include(x => x.VechicleTypes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Post post)
        {
            var objFromDb = await _dbcontext.Post.FirstOrDefaultAsync(x => x.Id == post.Id);

            if (objFromDb != null)
            {

                objFromDb.Name = post.Name;

                objFromDb.BrandId = post.BrandId;
                objFromDb.VehicleTypeId = post.VehicleTypeId;              
                objFromDb.BycycleCategories = post.BycycleCategories;
                objFromDb.BycycleTypes = post.BycycleTypes;        
                objFromDb.Ratings = post.Ratings;
                objFromDb.PriceFrom = post.PriceFrom;
                objFromDb.PriceTo = post.PriceTo;
              


                if (post.VehicleImage != null)
                {

                    objFromDb.VehicleImage = post.VehicleImage;

                }

                _dbcontext.Update(objFromDb);

            }
        }
    }
}
