using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.FinalNav.Containers;

namespace Xamarin.FinalNav.DI
{

    internal class ConstructorMatchingModel
    {

        #region private member
        private readonly ConstructorInfo _constructorInfo;
        private readonly ParameterInfo[] _constructorParameters;

        private readonly IReadOnlyList<ServiceRegistrationContainer> _services;
        private readonly NavigationParameter[] _userParameters;

        private readonly ConstructorParameterMatching[] _constructorParameterMatchings;
        #endregion

        #region properties
        public bool SatisfiesUserParameters { get; private set; }
        public bool IsFullyQualified { get; private set; }
        public int Satisfactions => _constructorParameterMatchings.Where(c => c is not null).Count();
        #endregion

        #region ctor
        public ConstructorMatchingModel(ConstructorInfo constructorInfo, IReadOnlyList<ServiceRegistrationContainer> services, params NavigationParameter[] userParameters)
        {
            _constructorInfo = constructorInfo;
            _constructorParameters = _constructorInfo.GetParameters();
            _services = services;
            _userParameters = userParameters;

            _constructorParameterMatchings = new ConstructorParameterMatching[_constructorInfo.GetParameters().Length];
            Initialize();
        }
        #endregion

        #region init
        private void Initialize()
        {
            //check for user parameters
            SatisfiesUserParameters = InitializeUserParameters();

            //check for services
            InitializeServiceParameters();

            //check for null matchings => sa
            IsFullyQualified = _constructorParameterMatchings.All(m => m is not null);
        }
        private bool InitializeUserParameters()
        {
            if (_userParameters.Length == 0) { return true; }//no user parameters
            var found = 0;

            for (int i = 0; i < _constructorParameters.Length && found < _userParameters.Length; i++)
            {
                if (_userParameters[found].Parameter.GetType().Equals(_constructorParameters[i].ParameterType))
                {
                    MatchParameter(i, _userParameters[found], EParameterSource.UserParameter);
                    found++;
                }
            }
            return found == _userParameters.Length;
        }
        private void InitializeServiceParameters()
        {
            for (int i = 0; i < _constructorParameterMatchings.Length; i++)
            {
                if (_constructorParameterMatchings[i] is not null) { continue; }//already filled, skip

                var constructorParameter = _constructorParameters[i];
                if (_services.Any(s => s.ServiceType.Equals(constructorParameter.ParameterType)))
                {
                    MatchParameter(i, _services.FirstOrDefault(s => s.ServiceType.Equals(constructorParameter.ParameterType)), EParameterSource.Service);
                }

            }
        }


        private void MatchParameter(int parameterIndex, object parameterContainer, EParameterSource source)
        {
            var matching = new ConstructorParameterMatching
            {
                ParameterContainer = parameterContainer,
                Source = source,
            };
            _constructorParameterMatchings[parameterIndex] = matching;
        }
        #endregion

        #region public methods
        public object CreateObject()
        {

            var parameterInstances = new List<object>();
            foreach (var matching in _constructorParameterMatchings)
            {
                var parameter = matching.Source switch
                {
                    EParameterSource.Service => ((ServiceRegistrationContainer)matching.ParameterContainer).GetInstance(_services),
                    EParameterSource.UserParameter => ((NavigationParameter)matching.ParameterContainer).Parameter,

                    _ => throw new NotImplementedException(),
                };
                parameterInstances.Add(parameter);
            }

            var instance = _constructorInfo.Invoke(parameterInstances.ToArray());
            return instance;
        }
        #endregion

    }

}
