using Salon.DAL.Models;
using Salon.DAL.GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Salon.DAL.Repository
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        Task<IEnumerable<UserModel>> Search(SearchUserModel search);
    }
}