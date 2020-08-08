using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class SeriesRepository : BaseRepository<Series>
    {
        public SeriesRepository(Context context) : base(context)
        {
        }

        public override Series Get(int id, bool includeRelatedEntities = true)
        {
            return Context.Series
                .Include(s => s.ComicBooks)
                .Where(s => s.Id == id)
                .SingleOrDefault();
        }

        public override IList<Series> GetList()
        {
            return Context.Series
                    .Include(s => s.ComicBooks)
                    .OrderBy(s => s.Title)
                    .ToList();
        }

        public bool IsTitleAvailable(string title)
        {
            return !Context.Series.Any(s => s.Title == title);
        }
    }
}
