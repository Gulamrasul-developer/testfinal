using System.Collections.Generic;
using System.Threading.Tasks;
namespace Salon.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(long Id);
        Task<int> Add(TEntity entity);
        Task<int> Update(TEntity entity);
        Task<int> Active(long Id);
        Task<int> Delete(long Id);
    }
}