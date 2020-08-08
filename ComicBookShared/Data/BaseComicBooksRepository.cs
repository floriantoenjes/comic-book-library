using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public abstract class BaseComicBooksRepository : BaseRepository<ComicBook>
    {
        public BaseComicBooksRepository(Context context) : base(context)
        { }

        public abstract bool ComicBookSeriesHasIssueNumber(int comicBookId, int seriesId, int issueNumber);

        public abstract bool ComicBookHasArtistRoleCombination(int comicBookId, int artistId, int roleId);
    }
}
