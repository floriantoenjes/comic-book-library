using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public interface IBaseUserRepository
    {
        IList<ComicBook> GetFavoriteComicBooks(string userGuid);
    }
}
