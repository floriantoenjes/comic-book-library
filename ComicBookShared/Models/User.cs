using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ComicBookShared.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            FavoriteComicBooks = new List<ComicBook>();
        }

        public ICollection<ComicBook> FavoriteComicBooks { get; set; }
    }
}
