using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_Core_Email_System.Models
{
    public class SQLUserRepo : IUserRepo
    {
        private readonly AppDbContext context;
        public SQLUserRepo(AppDbContext context)
        {
            this.context = context;
        }
        DbUser IUserRepo.Add(DbUser user)
        {
            context.User.Add(user);
            context.SaveChanges();
            return user;
        }

        DbUser IUserRepo.GetUser(string userName)
        {
            return context.User.FirstOrDefault(m => m.UserName == userName);
        }
        DbUser IUserRepo.GetUserEmail(string userEmail)
        {
            return context.User.FirstOrDefault(m => m.UserEmailId == userEmail);
        }
        DbUser IUserRepo.Update(DbUser userChanges)
        {
            var user = context.User.Attach(userChanges);
            user.State = EntityState.Modified;
            context.SaveChanges();
            return userChanges;
        }
    }
}
