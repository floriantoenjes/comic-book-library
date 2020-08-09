using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public abstract class BaseArtistsRepository : BaseRepository<Artist>
    {
        public BaseArtistsRepository(Context context) : base(context)
        {
        }

        public abstract bool IsNameAvailable(int id, string name);
    }
}
