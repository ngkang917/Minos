using Minos.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minos.Domain.Aggregates
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        T Add(T t);
        void Update(T t);
        Task<List<T>> GetAsync(T t);
        Task<T> FindAsync(T t);
        Task<T> FindByIdxAsync(T t);
        IUnitOfWork UnitOfWork { get; }
    }
}
