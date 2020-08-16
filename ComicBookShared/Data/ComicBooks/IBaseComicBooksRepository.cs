using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public interface IBaseComicBooksRepository : IBaseRepository<ComicBook>
    {
        bool ComicBookSeriesHasIssueNumber(int comicBookId, int seriesId, int issueNumber);

        bool ComicBookHasArtistRoleCombination(int comicBookId, int artistId, int roleId);
    }
}
