using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOT_NET_Core_Email_System.Models
{
    public interface IUserRepo
    {
        DbUser GetUser(string userName);
        DbUser GetUserEmail(string userEmail);
        DbUser Add(DbUser user);
        DbUser Update(DbUser userChanges);
    }
}
