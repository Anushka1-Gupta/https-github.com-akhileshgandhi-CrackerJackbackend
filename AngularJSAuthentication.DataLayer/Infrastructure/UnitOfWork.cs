using AngularJSAuthentication.Common.Constants;

using AngularJSAuthentication.Model;
using Dapper.Contrib.Extensions;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AngularJSAuthentication.DataLayer.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private string connStr => DbConstants.AuthContextDbConnection;

        #region Private variables
       

        #region Configuration

        public UnitOfWork(string dbName = "", string connectionString = "")
        {
            //FluentMapper.Initialize(config =>
            //{
            //    config.AddMap(new PeopleConfiguration());
            //    config.ForDommel();
            //});
            _connection = new SqlConnection();
            _connection.ConnectionString = !string.IsNullOrEmpty(connectionString) ? connectionString : connStr;
            _connection.Open();
            _transaction = _connection.BeginTransaction();

           
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                //_transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
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

        private void ResetRepositories()
        {
            //_CompanyRepository = null;
        }
        #endregion
        #endregion
    }
}
