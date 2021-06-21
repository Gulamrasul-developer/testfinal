using Salon.DAL.Models;
using Salon.DAL.GenericRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Salon.DAL.Repository
{
    public interface IUserGroupRepository : IGenericRepository<UserGroupModel>
    {
        Task<IEnumerable<UserGroupModel>> Search();
    }
}