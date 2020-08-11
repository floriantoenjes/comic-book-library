using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public class UserRepository : BaseUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public override IList<ComicBook> GetFavoriteComicBooks(string userGuid)
        {
            return Context.Users
                .Where(u => u.Id == userGuid)
                .SelectMany(u => u.FavoriteComicBooks)
                .Include(c => c.Series)
                .ToList();
        }
    }
}
