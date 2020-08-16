using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;

namespace ComicBookShared.Data
{
    public interface IBaseSeriesRepository : IBaseRepository<Series>
    {
        bool IsTitleAvailable(int id, string title);
    }
}
