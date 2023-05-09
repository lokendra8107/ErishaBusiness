using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ErishaBusiness.Repo.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGenericRepository : IDisposable
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll<T>(string spName);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListById<T>(int? id, string spName, string paramName);

        /// <summary>
        /// Returns details of entity record on basis of ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>
        /// entity record on basis of id
        /// </returns>
        Task<T> Get<T>(int id, string spName, string paramName);

        /// <summary>
        /// Inserts entity details into database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// affected records
        /// </returns>
        Task<int> Insert<T>(T entity, string spName);

        /// <summary>
        /// Inserts entity details into database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="TableType">Type of the table.</param>
        /// <returns>
        /// affected records
        /// </returns>
        Task<int> Insert<T>(T entity, string spName, string TableType);
        /// <summary>
        /// Insert entity through JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">pass entity and it convert into JSON and then pass to database </param>
        /// <param name="spName"></param>
        /// <returns></returns>
        Task<int> InsertViaJson<T>(T entity, string spName);

        /// <summary>
        /// updates entity records on basis of ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// affected records
        /// </returns>
        Task<int> Update<T>(T entity, string spName);

        /// <summary>
        /// updates entity records on basis of ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="TableType">Type of the table.</param>
        /// <returns>
        /// affected records
        /// </returns>
        Task<int> Update<T>(T entity, string spName, string TableType);

        /// <summary>
        /// Update records
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> Update(string spName, object param);

        /// <summary>
        /// Hard Delete entity records on basis of ID
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// affected records
        /// </returns>
        Task<int> Delete(int id, string spName);

        /// <summary>
        /// Delete entity records on basis of ID
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// affected records
        /// </returns>
        Task<int> Delete(int id, string spName, int userId);

        /// <summary>
        /// Gets records on basis of page index and size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// all retrived records on basis of page index and size
        /// </returns>
        Task<IEnumerable<T>> GetAllByPageIndex<T>(int pageIndex, int pageSize, string spName);

        /// <summary>
        /// Searchs the records on basis of assigned searched item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchItem">The search item.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>
        /// all retrived records on basis of searched item
        /// </returns>
        Task<IEnumerable<T>> Search<T>(string searchItem, string spName, string paramName);

        /// <summary>
        /// Searchs the records on basis of assigned list parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="search">The search.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// all retrived records on basis of list of parameters
        /// </returns>
        Task<IEnumerable<T>> Search<T, B>(List<B> search, string spName);
    }
}
