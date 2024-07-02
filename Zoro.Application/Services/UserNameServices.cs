using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoro.Application.Services
{
    public class UserNameServices : IUserNameServices
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserNameServices(UserManager<IdentityUser> userManager)
        {

            _userManager = userManager;

        }

        public async Task<string> GetUserName(string UserId)
        {
           if(String.IsNullOrEmpty(UserId))
            {
                return String.Empty;


            }

           var user = await _userManager.FindByIdAsync(UserId);
            
            if(user != null)
            {


                return user.UserName;

            }


            return "NA";


        }
    }
}
