using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBookArtistsRepository : BaseRepository<ComicBookArtist>, IBaseComicBookArtistsRepository
    {

        public ComicBookArtistsRepository(Context context) : base(context)
        {
        }

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
            return Context.ComicBookArtists
                .Include(ca => ca.Artist)
                .Include(ca => ca.ComicBook)
                .ToList();
        }
    }
}
