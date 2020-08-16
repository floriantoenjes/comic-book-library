﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicBookShared.Models;
using System.Data.Entity;

namespace ComicBookShared.Data
{
    public class ComicBooksRepository : BaseRepository<ComicBook>, IBaseComicBooksRepository
    {

        public ComicBooksRepository(Context context) : base(context)
        {
        }

        public override IList<ComicBook> GetList()
        {
            return Context.ComicBooks
                    .Include(cb => cb.Series)
                    .OrderBy(cb => cb.Series.Title)
                    .ThenBy(cb => cb.IssueNumber)
                    .ToList();
        }

        public override ComicBook Get(int id, bool includeRelatedEntities = true)
        {
            var comicBooks = Context.ComicBooks.AsQueryable();

            if (includeRelatedEntities)
            {
                comicBooks = comicBooks
                    .Include(cb => cb.Series)
                    .Include(cb => cb.Artists.Select(a => a.Artist))
                    .Include(cb => cb.Artists.Select(a => a.Role))
                    .Include(cb => cb.UsersWhoChoseAsFavorite);
            }

            return comicBooks
                    .Where(cb => cb.Id == id)
                    .SingleOrDefault();
        }

        public bool ComicBookSeriesHasIssueNumber(
            int comicBookId, int seriesId, int issueNumber)
        {
            return Context.ComicBooks.Any(cb => cb.Id != comicBookId &&
                    cb.SeriesId == seriesId &&
                    cb.IssueNumber == issueNumber);
        }

        public bool ComicBookHasArtistRoleCombination(
            int comicBookId, int artistId, int roleId)
        {
            return Context.ComicBookArtists.Any(ca => ca.ComicBookId == comicBookId &&
                    ca.ArtistId == artistId &&
                    ca.RoleId == roleId);
        }
    }
}