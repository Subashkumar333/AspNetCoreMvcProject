using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoro.Application.ApplicationConstants;
using Zoro.Domain.Models;

namespace Zoro.Infrastructure.Comman
{
    public class DataBaseSeeding
    {

        public static async Task SeedRole(IServiceProvider serviceProvider)
        {

            var scope = serviceProvider.CreateScope();

            var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Name=CustomRole.MasterAdmin,NormalizedName=CustomRole.MasterAdmin},
                new IdentityRole {Name=CustomRole.Admin,NormalizedName=CustomRole.Admin},
                new IdentityRole {Name=CustomRole.Customer,NormalizedName=CustomRole.Customer},

            };

            foreach (var role in roles)
            {
              if(! await roleManger.RoleExistsAsync(role.Name))
                {

                    await roleManger.CreateAsync(role);

                }


            }


        }






        public static async Task SeedDataAsync(ApplicationDbContext _dbcontext)
        {

         if(!_dbcontext.VechicleTypes.Any()) 
          {
                await _dbcontext.VechicleTypes.AddRangeAsync
                 (
               
                new  VechicleTypes
                {
                    Name="Gear Cycle"

                },

                 new VechicleTypes
                 {
                     Name = "Non Gear Cycle"

                 },

                  new VechicleTypes
                  {
                      Name = "Ladies Cycle"

                  },

                   new VechicleTypes
                   {
                       Name = "Kids Cycle"

                   },

                   new VechicleTypes
                   {
                       Name = "Adult Cycle"

                   }
                    );

                await _dbcontext.SaveChangesAsync();
            
           }

        }

    }
}
