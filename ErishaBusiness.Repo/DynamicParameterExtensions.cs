using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;

namespace ErishaBusiness.Repo.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DynamicParameterExtensions
    {
        /// <summary>
        /// Adds the result output parameter.
        /// </summary>
        /// <param name="dynamicParameters">The dynamic parameters.</param>
        /// <returns></returns>
        public static DynamicParameters AddResultOutputParam(this DynamicParameters dynamicParameters)
        {
            dynamicParameters.Add(DbConstant.RetValue, 0, DbType.Int32, ParameterDirection.Output);
            return dynamicParameters;
        }

        /// <summary>
        /// Gets the result parameter value.
        /// </summary>
        /// <param name="dynamicParameters">The dynamic parameters.</param>
        /// <returns></returns>
        public static int GetResultParamValue(this DynamicParameters dynamicParameters)
        {
            return dynamicParameters.Get<int>(DbConstant.RetValue);
        }
       /// <summary>
       /// Add Json parameter to send in the stored procedure as parameter
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dynamicParameters"></param>
       /// <param name="entity">The entity</param>
       /// <returns></returns>
        public static DynamicParameters AddJsonParam<T>(this DynamicParameters dynamicParameters, T entity)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string json = JsonConvert.SerializeObject(entity);
            dynamicParameters.Add(DbConstant.Json, json);
            return dynamicParameters;
        }

        /// <summary>
        /// Adds the json parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dynamicParameters">The dynamic parameters.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        public static DynamicParameters AddJsonParam<T>(this DynamicParameters dynamicParameters, T entity, string paramName)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string json = JsonConvert.SerializeObject(entity);
            dynamicParameters.Add(paramName, json);
            return dynamicParameters;
        }
    }
}
