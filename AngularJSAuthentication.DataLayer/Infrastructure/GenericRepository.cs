using Dapper.Contrib.Extensions;
using System.Data;
using System.Threading.Tasks;

namespace AngularJSAuthentication.DataLayer.Infrastructure
{
    public class GenericRepository<T> where T : class
    {
        protected IDbTransaction _transaction;
        protected IDbConnection _connection
        {
            get
            {
                return _transaction.Connection;
            }
        }

        public async Task<dynamic> InsertAsync(T entity)
        {
            var id = await _connection.InsertAsync(entity, _transaction);
            return id;
        }

        public async Task<T> GetByIdAsync(int Id)
        {            
            var entity = await _connection.GetAsync<T>(Id, _transaction);
            return entity;
        }

        public T GetById(int Id)
        {
            T entity =  _connection.Get<T>(Id, _transaction);
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            return await _connection.UpdateAsync(entity, _transaction);
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            return await _connection.DeleteAsync(entity, _transaction);
        }

    }
}
