using EMS.Core.Repository.GenericRepository;
using EMS.Infrastructure.Repository.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Repository.Extensions
{
    public static class RepositoryExtensions
    {
        public static void RepositoryExtension(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IGenericRepository<,>), typeof(GenericImplementation<,>));
        }
    }
}
