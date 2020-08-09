using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public abstract class BaseSeriesRepository : BaseRepository<Series>
    {
        public BaseSeriesRepository(Context context) : base(context)
        {
        }

        public abstract bool IsTitleAvailable(int id, string title);
    }
}
