using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public interface IUserRepository
    {
        User GetUser(int Id);
        User GetUser(string Email, string Password);
        IEnumerable<User> GetAllUsers();
        User Add(User User);
        User Update(User UserChanges);
        User Delete(int Id);
    }
}
