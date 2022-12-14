using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.Services.GenericService
{
    public interface IDapperService<TParams> : IDisposable
    {
        DbConnection GetDbconnection();
        TModel Get<TModel>(string sp, TParams entity);
        IEnumerable<TModel> GetAll<TModel>(string sp, TParams entity);
        TModel Execute<TModel>(string sp, TParams entity);
    }
}
