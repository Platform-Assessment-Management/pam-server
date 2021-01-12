using System;
using System.Collections.Generic;
using System.Text;

namespace PAMDomain
{
    public static class UtilDomain
    {
        private static IServiceProvider _serviceProvider;

        public static void SetService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

    }
}
