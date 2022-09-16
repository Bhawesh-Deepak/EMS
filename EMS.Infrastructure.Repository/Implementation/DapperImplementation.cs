using Dapper;
using EMS.Core.Repository.GenericRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Repository.Implementation
{
    public class DapperImplementation<TParams> : IDapperRepository<TParams>
    {
      
        private readonly string _connectionString;

        public DapperImplementation()
        {
          
            _connectionString = "server=89.163.218.70\\MSSQLSERVER2017; Database= BIS_Store1; User Id=igl;Password = Manoj@12345";
        }
        public void Dispose()
        {

        }

        public TModel Execute<TModel>(string sp, TParams entity)
        {
            try
            {
                TModel result;

                using IDbConnection db = new SqlConnection(_connectionString);

                if (db.State == ConnectionState.Closed)
                    db.Open();

                var parms = ConvertObjectToDBParameter<TParams>(entity);
                result = db.Query<TModel>(sp, parms, commandType: CommandType.StoredProcedure, transaction: null).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public TModel Get<TModel>(string sp, TParams entity)
        {
            DynamicParameters parms = null;
            if (entity != null)
            {
                parms = ConvertObjectToDBParameter<TParams>(entity);
            }

            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<TModel>(sp, parms, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public IEnumerable<TModel> GetAll<TModel>(string sp, TParams entity)
        {
            try
            {
                DynamicParameters parms = null;
                if (entity != null)
                {
                    parms = ConvertObjectToDBParameter<TParams>(entity);
                }
                using IDbConnection db = new SqlConnection(_connectionString);
                return db.Query<TModel>(sp, parms, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DynamicParameters ConvertObjectToDBParameter<T>(T entity)
        {
            Type t = entity.GetType();
            var paramData = new DynamicParameters();
            foreach (var entityProp in t.GetProperties())
            {
                paramData.Add($"@{entityProp.Name}", entityProp.GetValue(entity, null), GetDbType(entityProp.PropertyType));
            }
            return paramData;
        }
        public static DbType GetDbType(Type runtimeType)
        {
            var nonNullableType = Nullable.GetUnderlyingType(runtimeType);

            if (nonNullableType != null)
            {
                runtimeType = nonNullableType;
            }

            var templateValue = (Object)null;
            if (runtimeType.IsClass == false)
            {
                templateValue = Activator.CreateInstance(runtimeType);
            }

            var sqlParamter = new SqlParameter(parameterName: String.Empty, value: templateValue);

            return sqlParamter.DbType;
        }

    }
}
