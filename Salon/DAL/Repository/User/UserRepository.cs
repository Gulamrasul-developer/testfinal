using Dapper;
using System.Data;
using System.Threading.Tasks;
using Salon.DAL.Models;
using Salon.DAL.GenericRepository;
using Salon.Common.Configuration;
using System.Collections.Generic;

namespace Salon.DAL.Repository
{
    public class UserRepository : GenericBaseRepository<UserModel>, IUserRepository
    {
        private readonly IDbConnection _con;
        private readonly IDbTransaction _transaction;
        private readonly DynamicParameters _dParam;
        public UserRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
            _con = _transaction.Connection;
            _dParam = new DynamicParameters();
        }
        public Task<IEnumerable<UserModel>> Search(SearchUserModel search)
        {
            _dParam.Add("@FullName", search.FullName);
            _dParam.Add("@MobileNo", search.MobileNo);
            _dParam.Add("@Active", search.Active);
            return base.Search(_transaction, "USP_SearchUser", _dParam);
        }
        public Task<UserModel> GetById(long Id)
        {
            _dParam.Add("@Id", Id);
            return base.GetById(_transaction, "Usp_GetUserById", _dParam);
        }
        public Task<int> Add(UserModel model)
        {
            return base.Add(_transaction, "USP_AddUpdateUser", SetParam(model, 'A'));
        }
        public Task<int> Update(UserModel model)
        {
            return base.Update(_transaction, "USP_AddUpdateUser", SetParam(model, 'E'));
        }
        public Task<int> Active(long Id)
        {
            _dParam.Add("@Id", Id);
            _dParam.Add("@TableId", 2);
            _dParam.Add("@UserId", UserConfig.UserId);
            return base.Active(_transaction, "USP_Active", _dParam);
        }
        public Task<int> Delete(long Id)
        {
            _dParam.Add("@Id", Id);
            _dParam.Add("@TableId", 2);
            _dParam.Add("@UserId", UserConfig.UserId);
            return base.Delete(_transaction, "USP_Delete", _dParam);
        }
        private DynamicParameters SetParam(UserModel model, char mode)
        {
            _dParam.Add("@Mode", mode);
            _dParam.Add("@Id", model.Id);
            _dParam.Add("@FullName", model.FullName);
            _dParam.Add("@MobileNo", model.MobileNo);
            _dParam.Add("@Gender", model.Gender);
            _dParam.Add("@Email", model.Email);
            _dParam.Add("@UserId", UserConfig.UserId);
            return _dParam;
        }
    }
}