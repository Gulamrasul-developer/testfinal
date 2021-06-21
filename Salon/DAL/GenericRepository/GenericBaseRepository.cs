using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Salon.Common.Exceptions;

namespace Salon.DAL.GenericRepository
{
    public class GenericBaseRepository<TEntity> : IGenericDBRepository<TEntity> where TEntity : class
    {
        private IDbConnection _con;
        public async Task<IEnumerable<TEntity>> Search(IDbTransaction transaction, string query, DynamicParameters param)
        {
            _con = transaction.Connection;
            var result = await _con.QueryAsync<TEntity>(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
            return result.AsList<TEntity>().Count > 0 ? result : throw new DataNotFoundException();
        }
        public async Task<TEntity> GetById(IDbTransaction transaction, string query, DynamicParameters param)
        {
            _con = transaction.Connection;
            var result = await _con.QueryFirstOrDefaultAsync<TEntity>(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
            return result != null ? result : throw new DataNotFoundException();
        }
        public async Task<int> Add(IDbTransaction transaction, string query, DynamicParameters param)
        {
            _con = transaction.Connection;
            return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        }
        public async Task<int> Update(IDbTransaction transaction, string query, DynamicParameters param)
        {
            _con = transaction.Connection;
            return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        }
        public async Task<int> Active(IDbTransaction transaction, string query, DynamicParameters param)
        {
            _con = transaction.Connection;
            return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        }
        public async Task<int> Delete(IDbTransaction transaction, string query, DynamicParameters param)
        {
            _con = transaction.Connection;
            return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        }
        //public async Task<int> AddRange(IDbTransaction transaction, string query, DynamicParameters param)
        //{
        //    _con = transaction.Connection;
        //    return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        //}
        //public async Task<int> UpdateRange(IDbTransaction transaction, string query, DynamicParameters param)
        //{
        //    _con = transaction.Connection;
        //    return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        //}
        //public async Task<int> DeleteRange(IDbTransaction transaction, string query, DynamicParameters param)
        //{
        //    _con = transaction.Connection;
        //    return await _con.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure, transaction: transaction);
        //}
    }
}