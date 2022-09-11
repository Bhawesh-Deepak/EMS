using EMS.Core.Services.GenericService;
using EMS.Core.Services.Master;
using EMS.Core.Services.Survey;
using EMS.Infrastructure.Services.Implementation;
using EMS.Infrastructure.Services.MasterImplementation;
using EMS.Infrastructure.Services.SurveyImplementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Infrastructure.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void ServiceExtension(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IGenericService<,>),
                typeof(GenericServiceImplementation<,>));

            serviceCollection.AddTransient<ISurveyService, SurveyService>();
            serviceCollection.AddTransient<IMasterService, MasterService>();
        }
    }
}
