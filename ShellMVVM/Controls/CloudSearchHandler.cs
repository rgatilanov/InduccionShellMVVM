using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ShellMVVM.Models;
using System.Linq;
using System.Threading.Tasks;
using ShellMVVM.Services;

namespace ShellMVVM.Controls
{
    public class CloudSearchHandler: SearchHandler
    {
        public IList<Cloud> Clouds { set; get; }
        public Type SelectedItemNavigationTarget { get; set; }
        public IDataCloud<Cloud> DataCloud => DependencyService.Get<IDataCloud<Cloud>>();
        public CloudSearchHandler()
        {
            Clouds = DataCloud.GetClouds();
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);
            if (string.IsNullOrWhiteSpace(newValue))
                ItemsSource = null;
            else
                ItemsSource = Clouds
                    .Where(cloud => cloud.Name.ToLower().Contains(newValue.ToLower())).ToList<Cloud>();
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"{GetNavigationTarget()}?name={((Cloud)item).Name}");
        }

        string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
            throw new Exception();
        }
    }
}
