# Xamarin.FinalNav

Get your Xamarin.Forms Pages and ViewModels into the real DI world.

![Nuget](https://img.shields.io/nuget/v/Xamarin.FinalNav?logo=NuGet)
![Tests](https://github.com/bpoller2810/Xamarin.FinalNav/workflows/Tests/badge.svg)

## How to:
1. Create your container at your Platform
2. Register your platform Services
3. Hand the IoC container to your App
4. Register your shared Services
5. Register your Pages
6. Initialize the Navigation
7. Navigate like a Pro 😎

 Note: All files and Lines here base on the Sample project
 
---
### 1 new Ioc container [MainActivity.cs](sample/FinalNav.Sample.Android/MainActivity.cs)
```c#
var ioc = new FinalIoc();
```

---
### 2 Register platrform services [MainActivity.cs](sample/FinalNav.Sample.Android/MainActivity.cs)
```c#
ioc.RegisterService<IPlatformDependentService, AndroidPlatformService>();
```

---
### 3 LoadApplication [MainActivity.cs](sample/FinalNav.Sample.Android/MainActivity.cs)
```c#
LoadApplication(new App(ioc));
```
Note: "App.xaml.cs" needs the FinalIoc as constructor parameter

---
### 4 Register common code services [App.xaml.cs](sample/FinalNav.Sample/App.xaml.cs) 
```c#
container.RegisterService<IDemoService, DefaultDemoService>();
```

---
### 5 Register pages [App.xaml.cs](sample/FinalNav.Sample/App.xaml.cs)
```c#
container.RegisterPage<LoginPage, LoginPageViewModel>();
container.RegisterPage<UserPage, UserPageViewModel>();
```

---
### 6 Build the Navigator [App.xaml.cs](sample/FinalNav.Sample/App.xaml.cs) 
```c#
new FinalNavigator(this, container).Build<LoginPage>();
```

---
### 7 Navigate [LoginPageViewModel.cs](sample/FinalNav.Sample/ViewModels/LoginPageViewModel.cs)

To be able to Navigate include the "INavigationService" interface in the Page/ViewModel constructor:
```c#
public LoginPageViewModel(INavigationService navigationService)
```

Use the NavigationService to navigate further with all your services always on hand
```c#
await FinalNavigator.Instance.PushAsync<UserPage>(new NavigationParameter
{
    Type = EParameterType.ViewModel,
    Parameter = Username,
});
```
Note you also be able to use the following class constructors:
```c#
public PageParameter(object parameter)
public ViewModelParameter(object parameter)
```

---
## NavigationMethods:
All methods get invoked on App.MainPage.Navigation

- PushAsync<TPage>(params NavigationParameter[] userParameters)
- PopAsync()
- PopToRootAsync()
- PushModalAsync<TPage>()
- PopModalAsync()

