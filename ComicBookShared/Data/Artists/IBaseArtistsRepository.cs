using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public interface IBaseArtistsRepository : IBaseRepository<Artist>
    {
        bool IsNameAvailable(int id, string name);
    }
}
