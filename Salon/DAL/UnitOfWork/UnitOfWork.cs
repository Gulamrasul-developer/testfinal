using System;
using System.Data;
using System.Data.SqlClient;
using Salon.DAL.Repository;
using Salon.Common.Configuration;
namespace Salon.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private bool _disposed;
        public UnitOfWork()
        {
            _connection = new SqlConnection(DBConfig.Connection);
            // _connection = new SQLiteConnection(_connectionString); // To use SQLite as data source 
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw ex;
            }
            finally
            {
                _transaction.Dispose();
            }
        }
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }
        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
        
        ~UnitOfWork()
        {
            dispose(false);
        }
        
        private ILoginRepository _login;
        public ILoginRepository Login => _login ?? (_login = new LoginRepository(_transaction));

        private IUserRepository _user;
        public IUserRepository User => _user ?? (_user = new UserRepository(_transaction));

        private IUserGroupRepository _userGroup;
        public IUserGroupRepository UserGroup => _userGroup ?? (_userGroup = new UserGroupRepository(_transaction));
    }
}