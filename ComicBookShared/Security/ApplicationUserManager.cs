using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;
using Microsoft.AspNet.Identity;

namespace ComicBookShared.Security
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> userStore) : base(userStore)
        { }
    }
}
