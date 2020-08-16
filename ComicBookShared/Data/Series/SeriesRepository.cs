using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class SeriesRepository : BaseRepository<Series>, IBaseSeriesRepository
    {
        public SeriesRepository(Context context) : base(context)
        {
        }

        public override Series Get(int id, bool includeRelatedEntities = true)
        {
            var series = Context.Series.AsQueryable();

            if (includeRelatedEntities)
            {
                series.Include(s => s.ComicBooks);
            }

            return series
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

        public bool IsTitleAvailable(int id, string title)
        {
            return !Context.Series.Any(s => s.Id != id && s.Title == title);
        }
    }
}
