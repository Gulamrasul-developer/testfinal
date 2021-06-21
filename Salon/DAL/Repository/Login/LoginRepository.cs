using Dapper;
using System.Data;
using System.Threading.Tasks;
using Salon.DAL.Models;
using Salon.Common.Configuration;

namespace Salon.DAL.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnection _con;
        private readonly IDbTransaction _transaction;
        private readonly CommandType _commandType;
        private readonly DynamicParameters _dParam;
        public LoginRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
            _con = transaction.Connection;
            _commandType = CommandType.StoredProcedure;
            _dParam = new DynamicParameters();
        }
        public Task<LoginDetailsModel> LoginByMobilePassword(LoginModel login)
        {
            _dParam.Add("@Mode", "MP");
            _dParam.Add("@MobileNo", login.MobileNo);
            _dParam.Add("@Password", login.Password);
            _dParam.Add("@UserId", 0);
            _dParam.Add("@GroupId", 0);
            return _con.QueryFirstOrDefaultAsync<LoginDetailsModel>("USP_Login", _dParam, commandType: _commandType, transaction: _transaction);
        }
        public Task<LoginDetailsModel> LoginByMobile(MobileNoModel login)
        {
            _dParam.Add("@Mode", "M");
            _dParam.Add("@MobileNo", login.MobileNo);
            _dParam.Add("@Password", "");
            _dParam.Add("@UserId", 0);
            _dParam.Add("@GroupId", 0);
            return _con.QueryFirstOrDefaultAsync<LoginDetailsModel>("USP_Login", _dParam, commandType: _commandType, transaction: _transaction);
        }
        public Task<LoginDetailsModel> LoginByUserId(long UserId)
        {
            _dParam.Add("@Mode", "UId");
            _dParam.Add("@MobileNo", "");
            _dParam.Add("@Password", "");
            _dParam.Add("@UserId", UserId);
            _dParam.Add("@GroupId", UserConfig.GroupId);
            return _con.QueryFirstOrDefaultAsync<LoginDetailsModel>("USP_Login", _dParam, commandType: _commandType, transaction: _transaction);
        }
    }
}