using Dapper;
using ErishaBusiness.Repo.Extensions;
using ErishaBusiness.Repo.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ErishaBusiness.Repo
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="SMARTS2.DomainRepository.Interfaces.IGenericRepository" />
    /// <seealso cref="IGenericRepository" />
    public abstract class BaseRepository : IGenericRepository, IDisposable
    {
        /// <summary>
        /// The database connection
        /// </summary>
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// The command timeout
        /// </summary>
        public int CommandTimeout { get; } = 60;

        /// <summary>
        /// The is disposed
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// Gets or sets the database transaction.
        /// </summary>
        /// <value>
        /// The database transaction.
        /// </value>
        public IDbTransaction _dbTransaction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository" /> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        protected BaseRepository(IDbConnection dbConnection) =>
            _dbConnection = dbConnection;

        #region Get
        /// <summary>
        /// Gets the specified item identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(int itemId, string spName, string paramName)
        {
            var parameters = GetParameter(paramName, itemId);
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(spName, parameters, transaction: _dbTransaction, commandTimeout: CommandTimeout, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets the query multiple asynchronous.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<GridReader> GetQueryMultipleAsync(string spName, object param = null) =>
         await _dbConnection.QueryMultipleAsync(spName, param, transaction: _dbTransaction, commandTimeout: CommandTimeout, commandType: CommandType.StoredProcedure);

        /// <summary>
        /// Gets the query multiple asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<GridReader> GetQueryMultipleAsyncJson<T>(string spName, T entity)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.AddJsonParam(entity);
            OpenConnection();
            return await _dbConnection.QueryMultipleAsync(spName, dynamicParameters, transaction: _dbTransaction, commandTimeout: CommandTimeout, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets the query first or default asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<T> GetQueryFirstOrDefaultAsync<T>(string spName, object param)
        {
            OpenConnection();
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(spName, param, commandType: CommandType.StoredProcedure, transaction: _dbTransaction, commandTimeout: CommandTimeout);
        }

        /// <summary>
        /// Gets the query first or default asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<T> GetQueryFirstOrDefaultAsyncJson<T>(string spName, object param)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.AddJsonParam(param);
            OpenConnection();
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(spName, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
        }

        /// <summary>
        /// Gets the query asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetQueryAsync<T>(string spName, object param = null)
        {
            OpenConnection();
            return await _dbConnection.QueryAsync<T>(spName, param, transaction: _dbTransaction, commandTimeout: CommandTimeout, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets the query asynchronous json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetQueryAsyncJson<T>(string spName, object param = null)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.AddJsonParam(param);
            OpenConnection();
            return await _dbConnection.QueryAsync<T>(spName, dynamicParameters, transaction: _dbTransaction, commandTimeout: CommandTimeout, commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region GetListById
        /// <summary>
        /// Gets the list by identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListById<T>(int? itemId, string spName, string paramName)
        {
            var parameters = GetParameter(paramName, itemId);
            var result = await _dbConnection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
            return result;
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Retrieves all entity records information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// all entity records
        /// </returns>
        public async Task<IEnumerable<T>> GetAll<T>(string spName) =>
            await _dbConnection.QueryAsync<T>(spName, commandTimeout: CommandTimeout);

        /// <summary>
        /// Retrieves all entity records information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="searchItem">The search item.</param>
        /// <param name="showAll">if set to <c>true</c> [show all].</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll<T>(string spName, int userId, string searchItem, bool showAll)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add(DbConstant.UserId, userId);
            dynamicParameters.Add(DbConstant.SearchedItem, searchItem);
            dynamicParameters.Add(DbConstant.Showall, showAll);
            return await _dbConnection.QueryAsync<T>(spName, dynamicParameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
        }
        #endregion

        #region Search
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
        public async Task<IEnumerable<T>> Search<T>(string searchItem, string spName, string paramName)
        {
            var parameters = GetParameter(paramName, searchItem);
            var result = await _dbConnection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
            return result;
        }

        /// <summary>
        /// Searches the specified search value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchValue">if set to <c>true</c> [search value].</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Search<T>(bool searchValue, string spName, string paramName)
        {
            var parameters = GetParameter(paramName, searchValue);
            var result = await _dbConnection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
            return result;
        }

        /// <summary>
        /// search the requested params
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="B"></typeparam>
        /// <param name="search">The search.</param>
        /// <param name="spname">The spname.</param>
        /// <returns>
        /// list of records
        /// </returns>
        public async Task<IEnumerable<T>> Search<T, B>(List<B> search, string spname) => await _dbConnection.QueryAsync<T>(spname, GetParametersGeneric(search), commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
        #endregion

        #region GetAllByPageIndex
        /// <summary>
        /// gets the records as per assigned page index and size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// list of records on basis of page size and index
        /// </returns>
        public async Task<IEnumerable<T>> GetAllByPageIndex<T>(int pageIndex, int pageSize, string spName) => await _dbConnection.QueryAsync<T>(spName, new { pageIndex, pageSize }, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeout);
        #endregion

        #region Insert
        /// <summary>
        /// Inserts records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// affected records
        /// </returns>
        public async Task<int> Insert<T>(T entity, string spName)
        {
            var parameters = GetParameters(entity).AddResultOutputParam();
            OpenConnection();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// Inserts records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="TableType">Type of the table.</param>
        /// <returns>
        /// affected records
        /// </returns>
        public async Task<int> Insert<T>(T entity, string spName, string TableType)
        {
            var parameters = GetParametersWithTable(entity, TableType).AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// it convert entity object into CamelCase JSON object then insert into the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="spName">stored procedure name</param>
        /// <returns></returns>
        public async Task<int> InsertViaJson<T>(T entity, string spName)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.AddJsonParam<T>(entity);
            dynamicParameters.AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, dynamicParameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            return dynamicParameters.GetResultParamValue();
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// affected records
        /// </returns>
        public async Task<int> Update<T>(T entity, string spName)
        {
            var parameters = GetParameters(entity).AddResultOutputParam();
            OpenConnection();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// Gets the query first asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public async Task<T> GetQueryFirstAsync<T>(string spName, object param = null)
        {
            return await _dbConnection.QueryFirstAsync<T>(spName, param, _dbTransaction, commandTimeout: CommandTimeout, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets the query first asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        public T GetQueryFirst<T>(string spName, object param = null)
        {
            return _dbConnection.QueryFirst<T>(spName, param, _dbTransaction, commandTimeout: CommandTimeout, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Updates records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="TableType">Type of the table.</param>
        /// <returns>
        /// affected records
        /// </returns>
        public async Task<int> Update<T>(T entity, string spName, string TableType)
        {
            var parameters = GetParametersWithTable(entity, TableType).AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// Update records
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> Update(string spName, object param)
        {
            var parameters = GetParameters(param).AddResultOutputParam();
            OpenConnection();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            return parameters.GetResultParamValue();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Hard Delete the record
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns>
        /// affted records
        /// </returns>
        public async Task<int> Delete(int itemId, string spName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", itemId);
            parameters = parameters.AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// Delete records
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// affted records
        /// </returns>
        public async Task<int> Delete(int itemId, string spName, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Id", itemId);
            parameters = parameters.AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// Deletes the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<int> Delete(string itemName, string spName, int userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);
            parameters.Add("@Name", itemName);
            parameters = parameters.AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.GetResultParamValue();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <returns></returns>
        public async Task<int> Delete<T>(T entity, string spName)
        {
            var parameters = GetParameters(entity).AddResultOutputParam();
            await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            return parameters.GetResultParamValue();

        }

        /// <summary>
        /// Delete Records
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="isInTranscation"></param>
        /// <returns></returns>
        public async Task<int> Delete(string spName, object param, bool isInTranscation = false)
        {
            var parameters = GetParameters(param).AddResultOutputParam();
            if (isInTranscation)
                await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure, transaction: _dbTransaction);
            else
            {
                OpenConnection();
                await _dbConnection.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            }
            return parameters.GetResultParamValue();
        }

        #endregion

        #region GetSingleParameter
        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="val">The value.</param>
        /// <returns></returns>
        private DynamicParameters GetParameter(string name, object val)
        {
            var parameters = new DynamicParameters();
            parameters.Add($"@{name}", val);
            return parameters;
        }
        #endregion

        #region GetParameters
        /// <summary>
        /// collects dynamic parameters from requested entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// parameters list
        /// </returns>
        public DynamicParameters GetParameters<T>(T entity)
        {
            Type myType = entity.GetType();
            var parameters = new DynamicParameters();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                string propName = $"@{prop.Name}";
                var propValue = prop.GetValue(entity, null);
                parameters.Add(propName, propValue);
            }
            return parameters;
        }

        /// <summary>
        /// collects dynamic parameters from requested object
        /// </summary>
        /// <param name="objectParam">The object parameter.</param>
        /// <returns></returns>
        public DynamicParameters GetParameters(object objectParam)
        {
            Type myType = objectParam.GetType();
            var parameters = new DynamicParameters();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                string propName = $"@{prop.Name}";
                var propValue = prop.GetValue(objectParam, null);
                parameters.Add(propName, propValue);
            }
            return parameters;
        }
        #endregion

        #region GetParametersGeneric
        /// <summary>
        /// get parameters from requested list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// parameter list
        /// </returns>
        public DynamicParameters GetParametersGeneric<T>(List<T> entity)
        {
            var parameters = new DynamicParameters();
            var properties = typeof(T).GetProperties();
            foreach (var item in entity)
            {
                dynamic val = "";
                foreach (var property in properties)
                {
                    if (val != "")
                    {
                        parameters.Add(val, property.GetValue(item, null));
                        val = "";
                    }
                    else if (val == "")
                        val = "@" + property.GetValue(item, null);
                }
            }
            return parameters;
        }
        #endregion

        #region GetParametersWithTable
        /// <summary>
        /// collects dynamic parameters from requested entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="TableType">Type of the table.</param>
        /// <returns>
        /// parameters list
        /// </returns>
        public DynamicParameters GetParametersWithTable<T>(T entity, string TableType)
        {
            Type myType = entity.GetType();
            var parameters = new DynamicParameters();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                string propName = $"@{prop.Name}";
                var propValue = prop.GetValue(entity, null);
                if (propValue is IList)
                {
                    propName = $"@{prop.Name}";
                    parameters = GetDataTable(parameters, propValue, propName, TableType);
                }
                else parameters.Add(propName, propValue);
            }
            return parameters;
        }
        #endregion

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="dynamicParameters">The dynamic parameters.</param>
        /// <param name="propValue">The property value.</param>
        /// <param name="propName">Name of the property.</param>
        /// <param name="tableType">Type of the table.</param>
        /// <returns></returns>
        public DynamicParameters GetDataTable(DynamicParameters dynamicParameters, object propValue, string propName, string tableType)
        {
            var dataTable = new DataTable();
            var type = propValue.GetType().GetGenericArguments()[0];
            var dtEntity = type.GetProperties();
            foreach (var propEntity in dtEntity)
                dataTable.Columns.Add(propEntity.Name);
            foreach (var properties in (IList)propValue)
            {
                var dataRow = dataTable.NewRow();
                foreach (var property in dtEntity)
                {
                    var propertyInfo = property.GetValue(properties, null);
                    if (propertyInfo is IList) dynamicParameters = GetDataTable(dynamicParameters, propertyInfo, property.Name, tableType);
                    else dataRow[property.Name] = property.GetValue(properties, null);
                }
                dataTable.Rows.Add(dataRow);
            }
            dynamicParameters.Add(propName, dataTable.AsTableValuedParameter(tableType));
            return dynamicParameters;
        }

        
        /// <summary>
        /// Opens the connection.
        /// </summary>
        public void OpenConnection()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void CloseConnection()
        {
            if (_dbConnection.State == ConnectionState.Open)
                _dbConnection.Close();
        }

        /// <summary>
        /// Disposes the connection.
        /// </summary>
        public void DisposeConnection()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            OpenConnection();
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction() => _dbTransaction?.Commit();

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction() => _dbTransaction?.Rollback();

        /// <summary>
        /// Ins the transaction.
        /// </summary>
        /// <returns></returns>
        public bool InTransaction() => _dbTransaction != null ? true : false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                // Dispose managed objects
            }
            // Dispose unmanaged objects
            DisposeConnection();
            _isDisposed = true;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseRepository" /> class.
        /// </summary>
        ~BaseRepository() => Dispose(false);
    }
}

