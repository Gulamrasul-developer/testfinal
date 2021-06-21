using System;
using Salon.DAL.Repository;
namespace Salon.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        public ILoginRepository Login { get; }
        public IUserRepository User { get; }      
        public IUserGroupRepository UserGroup { get; }
    }
}