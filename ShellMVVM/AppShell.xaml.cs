using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using ShellMVVM.Views;
using System.Threading.Tasks;
using System.Linq;
using ShellMVVM.Services;

namespace ShellMVVM
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public IDataCloud<Models.Cloud> DataCloud => DependencyService.Get<IDataCloud<Models.Cloud>>();

        Random rand = new Random();
        Dictionary<string, Type> routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes { get { return routes; } }

        public ICommand HelpCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));
        public ICommand RandomPageCommand => new Command(async () => await NavigateToRandomPageAsync());
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            routes.Add("cloudeuadetails", typeof(CloudEUADetailPage));
            routes.Add("cloudchinadetails", typeof(CloudChinaDetailPage));

            foreach (var item in routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        async Task NavigateToRandomPageAsync()
        {
            string destinationRoute = routes.ElementAt(rand.Next(0, routes.Count)).Key;
            string cloudName = null;

            switch (destinationRoute)
            {
                case "cloudeuadetails":
                    cloudName = await DataCloud.GetRandomCloudsAsync();
                    break;
                case "cloudchinadetails":
                    cloudName = await DataCloud.GetRandomCloudsAsync("1");
                    break;
            }

            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{destinationRoute}?name={cloudName}");
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
