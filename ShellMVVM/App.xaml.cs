using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShellMVVM.Services;
using ShellMVVM.Views;

namespace ShellMVVM
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<CloudData>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
