using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public abstract class BaseUserRepository
    {
        public Context Context { get; set; }
        public BaseUserRepository(Context context)
        {
            Context = context;
        }

        public abstract IList<ComicBook> GetFavoriteComicBooks(string userGuid);
    }
}
