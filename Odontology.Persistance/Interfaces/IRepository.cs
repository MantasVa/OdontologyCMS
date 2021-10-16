using System.Linq;
using System.Threading.Tasks;

namespace Odontology.Persistance.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllQuery();

        Task<T> GetByIdAsync(int id);

        T Add(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(int id);
    }
}
