using System;

namespace FytSoa.Common.Extensions
{
    public static class ServiceProviderManagerExtension
    {
        public static object GetService(this Type serviceType)
        {
            return FytSoaConfig.HttpCurrent.RequestServices.GetService(serviceType);
        }

    }
}
