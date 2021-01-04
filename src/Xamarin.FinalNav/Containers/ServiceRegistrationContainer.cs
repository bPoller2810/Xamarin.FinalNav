using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.FinalNav.DI;

namespace Xamarin.FinalNav.Containers
{
    internal class ServiceRegistrationContainer
    {

        #region properties
        public object Instance { get; private set; }

        public Type ServiceType { get; init; }
        public Type ServiceImplementation { get; init; }
        public EServiceLifetime Lifetime { get; init; }
        #endregion

        #region public methods
        public object GetInstance(IReadOnlyList<ServiceRegistrationContainer> services)
        {
            if (Instance is not null) { return Instance; }

            var constructor = DIHelper.GetBestMatchingConstructor(ServiceImplementation, services);
            var instance = constructor.CreateObject();
            if (Lifetime == EServiceLifetime.Singleton)
            {
                Instance = instance;
            }

            return instance;
        }
        #endregion

    }

}
