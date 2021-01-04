using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.FinalNav.Containers;

namespace Xamarin.FinalNav.DI
{
    internal static class DIHelper
    {

        public static ConstructorMatchingModel GetBestMatchingConstructor(Type elementType, IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            var constructors = elementType.GetConstructors().Select(c => new ConstructorMatchingModel(c, services, userParameters));
            if (constructors is null || constructors.Count() == 0) { throw new ArgumentException($"{elementType.Name} has no available constructor"); }

            var best = constructors
                .OrderByDescending(c => c.Satisfactions)
                .FirstOrDefault(c => c.SatisfiesUserParameters && c.IsFullyQualified);
            return best;
        }

    }
}
