using Dapper;
using System.Data;
using System.Threading.Tasks;
using Salon.DAL.Models;
using Salon.DAL.GenericRepository;
using Salon.Common.Configuration;
using System.Collections.Generic;

namespace Salon.DAL.Repository
{
    public class UserGroupRepository : GenericBaseRepository<UserGroupModel>, IUserGroupRepository
    {
        private readonly IDbConnection _con;
        private readonly IDbTransaction _transaction;
        private readonly DynamicParameters _dParam;
        public UserGroupRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
            _con = _transaction.Connection;
            _dParam = new DynamicParameters();
        }
        public Task<IEnumerable<UserGroupModel>> Search()
        {
         return Search(_transaction, "USP_SearchUserGroup", _dParam);
        }
        public Task<UserGroupModel> GetById(long Id)
        {
             _dParam.Add("@Id", Id);
            return base.GetById(_transaction, "Usp_GetUserGroupById", _dParam);
        }
        public Task<int> Add(UserGroupModel model)
        {
            return base.Add(_transaction, "USP_AddUpdateUserGroup", SetParam(model, 'A'));
        }
        public Task<int> Update(UserGroupModel model)
        {
            return base.Update(_transaction, "USP_AddUpdateUserGroup", SetParam(model, 'E'));
        }
        public Task<int> Active(long Id)
        {
            _dParam.Add("@Id", Id);
            _dParam.Add("@TableId", 1);
            _dParam.Add("@UserId", UserConfig.UserId);
            return base.Active(_transaction, "USP_Active", _dParam);
        }
        public Task<int> Delete(long Id)
        {
            _dParam.Add("@Id", Id);
            _dParam.Add("@TableId", 1);
            _dParam.Add("@UserId", UserConfig.UserId);
            return base.Delete(_transaction, "USP_Delete", _dParam);
        }
        private DynamicParameters SetParam(UserGroupModel model, char mode)
        {
            _dParam.Add("@Mode", mode);
            _dParam.Add("@Id", model.Id);
            _dParam.Add("@Name", model.Name);
            _dParam.Add("@Active", model.Active);
            _dParam.Add("@UserId", UserConfig.UserId);
            return _dParam;
        }
    }
}