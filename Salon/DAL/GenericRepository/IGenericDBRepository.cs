using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Salon.DAL.GenericRepository
{
    public interface IGenericDBRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Search(IDbTransaction transaction, string query, DynamicParameters param);
        Task<TEntity> GetById(IDbTransaction transaction, string query, DynamicParameters param);
        Task<int> Add(IDbTransaction transaction, string query, DynamicParameters param);
        Task<int> Update(IDbTransaction transaction,  string query, DynamicParameters param);
        Task<int> Active(IDbTransaction transaction, string query, DynamicParameters param);
        Task<int> Delete(IDbTransaction transaction,  string query, DynamicParameters param);
    }
}