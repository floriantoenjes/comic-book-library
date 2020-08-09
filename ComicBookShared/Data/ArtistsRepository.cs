using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ArtistsRepository : BaseArtistsRepository
    {
        public ArtistsRepository(Context context) : base(context)
        {
        }

        public override Artist Get(int id, bool includeRelatedEntities = true)
        {
            var artists = Context.Artists.AsQueryable();

            if (includeRelatedEntities)
            {
                artists.Include(a => a.ComicBooks);
            }
            return artists.Where(a => a.Id == id)
                .SingleOrDefault();
        }

        public override IList<Artist> GetList()
        {
            return Context.Artists
                .Include(a => a.ComicBooks)
                .OrderBy(a => a.Name)
                .ToList();
        }

        public override bool IsNameAvailable(int id, string name)
        {
            return !Context.Artists.Any(a => a.Id != id && a.Name == name);
        }
    }
}
