﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FinalNav
{
    public sealed class FinalNavigator : INavigationService
    {

        #region private/internal member
        internal readonly Application _app;
        internal INavigation _navigation;
        private readonly FinalIoc _iocContainer;
        internal NavigationPage _navigationPage;

        internal Action<FinalIoc> _preBuildAction;
        #endregion

        #region properties
        public bool Initialized { get; private set; }
        #endregion

        #region ctor
        public FinalNavigator(Application app, FinalIoc iocContainer)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (iocContainer is null)
            {
                throw new ArgumentNullException(nameof(iocContainer));
            }

            _app = app;
            _iocContainer = iocContainer;

            if (!_iocContainer.IsRegistered<INavigationService>())
            {
                _iocContainer.RegisterService<INavigationService>(this);
            }
        }
        #endregion

        #region init
        public void Build<TRootPage>(Action<NavigationPage> navigationPageinit = null)
            where TRootPage : Page
        {
            var page = _iocContainer.GetPage<TRootPage>();
            FinishInit(page, navigationPageinit);
        }
        public void Build<TRootPage, TRootViewModel>(Action<NavigationPage> navigationPageinit = null)
            where TRootPage : Page
            where TRootViewModel : INotifyPropertyChanged
        {
            var page = _iocContainer.GetPage<TRootPage, TRootViewModel>();
            FinishInit(page, navigationPageinit);
        }

        private void FinishInit(Page page, Action<NavigationPage> navigationPageinit = null)
        {
            _preBuildAction?.Invoke(_iocContainer);// pre build action

            _navigationPage = new NavigationPage(page);
            if (navigationPageinit is not null)
            {
                navigationPageinit(_navigationPage);
            }
            _app.MainPage = _navigationPage;
            _navigation = _app.MainPage.Navigation;
            Initialized = true;
        }
        #endregion

        #region stack
        public async Task PushAsync<TPage>(params NavigationParameter[] userParameters)
            where TPage : Page
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _iocContainer.GetPage<TPage>(userParameters);
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.AppearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Appearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.AppearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Appearing();
            }
            await _navigation.PushAsync(page);
        }
        public async Task PushAsync<TPage, TViewModel>(params NavigationParameter[] userParameters)
            where TPage : Page
            where TViewModel : INotifyPropertyChanged
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _iocContainer.GetPage<TPage, TViewModel>(userParameters);
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.AppearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Appearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.AppearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Appearing();
            }
            await _navigation.PushAsync(page);
        }
        public async Task PopAsync()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _navigation.NavigationStack.Last();
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.DisappearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Disappearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.DisappearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Disappearing();
            }
            await _navigation.PopAsync();
        }
        public async Task PopToRootAsync()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _navigation.NavigationStack.Last();
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.DisappearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Disappearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.DisappearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Disappearing();
            }
            await _navigation.PopToRootAsync();
        }
        #endregion

        #region modal stack
        public async Task PushModalAsync<TPage>(params NavigationParameter[] userParameters)
            where TPage : Page
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _iocContainer.GetPage<TPage>(userParameters);
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.AppearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Appearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.AppearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Appearing();
            }
            await _navigation.PushModalAsync(page);
        }
        public async Task PushModalAsync<TPage, TViewModel>(params NavigationParameter[] userParameters)
            where TPage : Page
            where TViewModel : INotifyPropertyChanged
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _iocContainer.GetPage<TPage, TViewModel>(userParameters);
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.AppearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Appearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.AppearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Appearing();
            }
            await _navigation.PushModalAsync(page);
        }
        public async Task PopModalAsync()
        {
            if (!Initialized)
            {
                throw new InvalidOperationException("FinalNavigator is not initialized");
            }
            var page = _navigation.ModalStack.Last();
            if (page is null)
            {
                throw new InvalidOperationException("Page not found");
            }
            else if (page.BindingContext is IAsyncLifecycle ial)
            {
                await ial.DisappearingAsync();
            }
            else if (page.BindingContext is ILifecycle il)
            {
                il.Disappearing();
            }

            if (page is IAsyncLifecycle ialp)
            {
                await ialp.DisappearingAsync();
            }
            else if (page is ILifecycle ilp)
            {
                ilp.Disappearing();
            }
            await _navigation.PopModalAsync();
        }
        #endregion

    }

}
