using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public class UserRepository : IBaseUserRepository
    {
        public Context Context { get; set; }
        public UserRepository(Context context)
        {
            Context = context;
        }

        public IList<ComicBook> GetFavoriteComicBooks(string userGuid)
        {
            return Context.Users
                .Where(u => u.Id == userGuid)
                .SelectMany(u => u.FavoriteComicBooks)
                .Include(c => c.Series)
                .ToList();
        }
    }
}
