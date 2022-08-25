using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface ISimpleChatRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Save();
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null,
                           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                           string includeProperties = "");
    }
}
