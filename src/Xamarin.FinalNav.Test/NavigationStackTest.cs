using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.FinalNav.Test.Models.TestPages;
using Xamarin.FinalNav.Test.XFMock;
using Xamarin.Forms;

namespace Xamarin.FinalNav.Test
{
    public class NavigationStackTest
    {

        [SetUp]
        public void Init()
        {
            MockXF.Init();

            FinalNavigator.Instance.CleanSystem();
            FinalNavigator.Instance.RegisterPage<TestPage, TestViewModel>();
        }


        [Test]
        public void CleanNavigationWithRoot()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            var stack = FinalNavigator.Instance.Navigation.NavigationStack;
            Assert.AreEqual(stack.Count, 1);
            Assert.AreEqual(stack[0].GetType(), typeof(TestPage));
        }

        [Test]
        public void CleanNavigationStackWithRoot()
        {
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            var stack = FinalNavigator.Instance.NavigationStack;
            Assert.AreEqual(stack.Count, 1);
            Assert.AreEqual(stack[0].PageType, typeof(TestPage));
        }

        [Test]
        public async Task NavigationWith2Pages()
        {
            FinalNavigator.Instance.RegisterPage<Test2Page, TestViewModel>();
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            await FinalNavigator.Instance.PushAsync<Test2Page>();
            var stack = FinalNavigator.Instance.Navigation.NavigationStack;
            Assert.AreEqual(stack.Count, 2);
            Assert.AreEqual(stack[0].GetType(), typeof(TestPage));
            Assert.AreEqual(stack[1].GetType(), typeof(Test2Page));
        }

        [Test]
        public async Task NavigationStackWith2Pages()
        {
            FinalNavigator.Instance.RegisterPage<Test2Page, TestViewModel>();
            FinalNavigator.Instance.InitializeRoot<TestPage>(new Application());
            await FinalNavigator.Instance.PushAsync<Test2Page>();
            var stack = FinalNavigator.Instance.NavigationStack;
            Assert.AreEqual(stack.Count, 2);
            Assert.AreEqual(stack[0].PageType, typeof(TestPage));
            Assert.AreEqual(stack[1].PageType, typeof(Test2Page));
        }

    }
}
