
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Domain.Models;

namespace Zoro.Infrastructure.Comman
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }

        public DbSet<Brand> Brand { get; set; }

        public DbSet<VechicleTypes> VechicleTypes { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbSet<CarTproducts> CarTproducts { get; set; }

       public DbSet<OrderProducts> OrderProducts { get; set; }

       public DbSet<ContactusData> ContactusData { get; set; }

        public DbSet<CompletedOrders> CompletedOrders { get; set; }
        

    }
}
