using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public abstract class BaseComicBookArtistsRepository : BaseRepository<ComicBookArtist>
    {
        public BaseComicBookArtistsRepository(Context context) : base(context)
        { }

        public override ComicBookArtist Get(int id, bool includeRelatedEntities = true)
        {
            var comicBookArtists = Context.ComicBookArtists.AsQueryable();

            if (includeRelatedEntities)
            {
                comicBookArtists = comicBookArtists
                    .Include(ca => ca.ComicBook.Series)
                    .Include(ca => ca.Artist)
                    .Include(ca => ca.Role);
            }

            return comicBookArtists
                .Where(ca => ca.Id == id)
                .SingleOrDefault();
        }

        public override IList<ComicBookArtist> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
