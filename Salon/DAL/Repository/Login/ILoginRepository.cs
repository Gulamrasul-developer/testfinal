using System.Threading.Tasks;
using Salon.DAL.Models;
namespace Salon.DAL.Repository
{
    public interface ILoginRepository
    {
        Task<LoginDetailsModel> LoginByMobilePassword(LoginModel login);
        Task<LoginDetailsModel> LoginByMobile(MobileNoModel login);
        Task<LoginDetailsModel> LoginByUserId(long UserId);
    }
}