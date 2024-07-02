using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoro.Application.Services
{
    public interface IUserNameServices
    {

        public Task<string> GetUserName(string UserId);
    }
}
