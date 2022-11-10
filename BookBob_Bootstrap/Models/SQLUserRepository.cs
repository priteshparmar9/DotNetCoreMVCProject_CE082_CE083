using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly AppDbContext context;
        public SQLUserRepository(AppDbContext context)
        {
            this.context = context;
        }
        User IUserRepository.Add(User User)
        {
            User.Cart = new Cart();
            context.Users.Add(User);
            context.SaveChanges();
            return User;
        }
        User IUserRepository.Delete(int Id)
        {
            User User = context.Users.Find(Id);
            if (User != null)
            {
                context.Users.Remove(User);
                context.SaveChanges();
            }
            return User;
        }

        IEnumerable<User> IUserRepository.GetAllUsers()
        {
            return context.Users;
        }

        User IUserRepository.GetUser(int id)
        {
            return context.Users.FirstOrDefault(m => m.UserId == id);
        }

        User IUserRepository.Update(User UserChanges)
        {
            var User = context.Users.Attach(UserChanges);
            User.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UserChanges;
        }

        User IUserRepository.GetUser(string Email, string Password)
        {
            return context.Users.FirstOrDefault(m => m.Email == Email && m.Password == Password);   
        }
    }
}
