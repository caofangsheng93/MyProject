using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ioc.Core.Data;

namespace Ioc.Service
{
   public interface IUserService
    {
       IQueryable<User> GetUsers();

       User GetUser(long id);

       void UpdateUser(User user);

       void InsertUser(User user);

       void DeleteUser(User user);

    }
}
