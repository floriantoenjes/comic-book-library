using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id, bool includeRelatedEntities = true);

        IList<TEntity> GetList();

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
