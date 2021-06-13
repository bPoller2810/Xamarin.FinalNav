using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.FinalNav
{
    public static class FinalNavigatorExtensions
    {

        public static FinalNavigator OnPreBuild(this FinalNavigator self, Action<FinalIoc> preBuildAction)
        {
            if (preBuildAction is null)
            {
                throw new ArgumentNullException(nameof(preBuildAction));
            }

            self._preBuildAction = preBuildAction;
            return self;
        }

    }
}
