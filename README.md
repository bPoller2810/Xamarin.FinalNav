# Xamarin.FinalCtrl

Get your Xamarin.Forms Pages and ViewModels into the real DI world.

![Nuget](https://img.shields.io/nuget/v/Xamarin.FinalNav?style=plastic)

## How to:

1. Clean the State in case you want to reuse your Activity/AppDelegate
2. Register your platform Services
3. Register your shared Services
4. Register your Pages
5. Initialize the Navigation
6. Navigate like a Pro 😎
---
### 1 CleanSystem [MainActivity.cs](sample/FinalNav.Sample.Android/MainActivity.cs) - OnCreate - Line 25 
```c#
FinalNavigator.Instance.CleanSystem();
```
---
### 2 RegisterService [MainActivity.cs](sample/FinalNav.Sample.Android/MainActivity.cs) - OnCreate - Line 35 
- [IPlatformDependentService](sample/FinalNav.Sample/Services/IPlatformDependentService.cs)
- [AndroidPlatformService](sample/FinalNav.Sample.Android/Service/AndroidPlatformService.cs)
```c#
FinalNavigator.Instance.RegisterService<IPlatformDependentService, AndroidPlatformService>();
```
---
### 3 RegisterService [App.xaml.cs](sample/FinalNav.Sample/App.xaml.cs) - Ctor - Line 17 
- [IDemoService](sample/FinalNav.Sample/Services/IDemoService.cs)
- [DefaultDemoService](sample/FinalNav.Sample/Services/DefaultDemoService.cs)
```c#
FinalNavigator.Instance.RegisterService<IDemoService, DefaultDemoService>();;
```
---
### 4 RegisterPage [App.xaml.cs](sample/FinalNav.Sample/App.xaml.cs) - Ctor - Line 19 
- [LoginPage](sample/FinalNav.Sample/Pages/UserPage.xaml)
- [LoginPageViewModel](sample/FinalNav.Sample/ViewModels/LoginPageViewModel.cs)
```c#
FinalNavigator.Instance.RegisterPage<LoginPage, LoginPageViewModel>();
```
---
### 5 InitializeRoot [App.xaml.cs](sample/FinalNav.Sample/App.xaml.cs) - Ctor - Line 22 
- [LoginPage](sample/FinalNav.Sample/Pages/UserPage.xaml)
```c#
FinalNavigator.Instance.InitializeRoot<LoginPage>(this);
```
---
### 6 PushAsync [LoginPageViewModel](sample/FinalNav.Sample/ViewModels/LoginPageViewModel.cs) - ExecuteAsyncCommand - Line 33-37 
- You can inject your runtime parameters into your Page and also your ViewModel (Note: Navigator matches by Order and Type) 
- [LoginPageViewModel](sample/FinalNav.Sample/ViewModels/LoginPageViewModel.cs) (See the comments on the multible Constructors, where the Username can be injected)
```c#
await FinalNavigator.Instance.PushAsync<UserPage>(new NavigationParameter
{
    Type = EParameterType.ViewModel,
    Parameter = Username,
});
```
---
## NavigationMethods:
All methods get invoked on App.MainPage.Navigation

- PushAsync<TPage>(params NavigationParameter[] userParameters)
- PopAsync()
- PopToRootAsync()
- PushModalAsync<TPage>()
- PopModalAsync()

