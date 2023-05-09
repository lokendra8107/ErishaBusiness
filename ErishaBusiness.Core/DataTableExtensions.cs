using DataTables.AspNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ErishaBusiness.Core
{
   public static class DataTableExtensions
    {
        public static IQueryable<T> SortAndPage<T>(this IQueryable<T> source, IDataTablesRequest request)
        {
            return source.OrderBy(request.Columns).Page(request);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> source, IDataTablesRequest request)
        {
            return source.Skip(request.Start).Take(request.Length);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<IColumn> sortModels)
        {
            var expression = source.Expression;
            var count = 0;
            foreach (var item in sortModels.Where(x => x.Sort != null).OrderBy(x => x.Sort.Order))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.Field);
                var method = item.Sort.Direction == SortDirection.Descending ?
                    (count == 0 ? nameof(Queryable.OrderByDescending) : nameof(Queryable.ThenByDescending)) :
                    (count == 0 ? nameof(Queryable.OrderBy) : nameof(Queryable.ThenBy));
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }


        //https://github.com/ALMMa/datatables.aspnet/issues/58
        public static IEnumerable<T> Compute<T>(this IEnumerable<T> data, IDataTablesRequest request, out int filteredDataCount)
        {
            filteredDataCount = 0;
            if (!data.Any() || request == null)
                return data;
            IEnumerable<T> filteredData = Enumerable.Empty<T>();
            if (!String.IsNullOrEmpty(request.Search.Value))
            {
                var filteredColumn = request.Columns.Where(c => c.IsSearchable == true);
                foreach (IColumn sColumn in filteredColumn)
                {
                    //var propertyInfo = data.First().GetType().GetProperty(sColumn.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    data = data.Where(d => d.GetType().GetProperty(sColumn.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null).ToList();
                    data = data.Where(d => d.GetType().GetProperty(sColumn.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(d, null) != null).ToList();

                    IEnumerable <T> columnResult = data.Where(d => d.GetType().GetProperty(sColumn.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(d, null).ToString().ToLower().Contains(request.Search.Value.ToLower()));
                    // IEnumerable<T> columnResult = data.PropertyContains(propertyInfo, request.Search.Value);
                    filteredData = filteredData.Concat(columnResult);
                }

                filteredData = filteredData.Distinct();
            }
            else filteredData = data;

            // Ordering filtred data
            var orderedColumn = request.Columns.Where(c => c.IsSortable == true && c.Sort != null);
            foreach (IColumn sColumn in orderedColumn)
            {
                filteredData = filteredData.OrderBy(sColumn);
            }

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            // var dataPage = filteredData.OrderBy(d => d.ID).Skip(request.Start);
            var dataPage = filteredData.Skip(request.Start);
            if (request.Length != -1) dataPage = dataPage.Take(request.Length);

            filteredDataCount = filteredData.Count();
            return dataPage;
        }


        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> entities, IColumn column)
        {
            if (!entities.Any() || column == null)
                return entities;

            var propertyInfo = entities.First().GetType().GetProperty(column.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (column.Sort.Direction == SortDirection.Ascending)
            {
                return entities.OrderBy(e => propertyInfo.GetValue(e, null));
            }
            return entities.OrderByDescending(e => propertyInfo.GetValue(e, null));
        }

        public static IEnumerable<T> PropertyContains<T>(this IEnumerable<T> data, PropertyInfo propertyInfo, string value)
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            MemberExpression m = Expression.MakeMemberAccess(param, propertyInfo);
            ConstantExpression c = Expression.Constant(value, typeof(string));
            MethodInfo mi_contains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            MethodInfo mi_tostring = typeof(object).GetMethod("ToString");
            Expression call = Expression.Call(Expression.Call(m, mi_tostring), mi_contains, c);

            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(call, param);

            return data.AsQueryable().Where(lambda);
        }
        public static IEnumerable<IColumn> SortedColumns(this IDataTablesRequest request)
        {
            return request.Columns.Where(r => r.IsSortable && r.Sort != null);
        }
    }
}
