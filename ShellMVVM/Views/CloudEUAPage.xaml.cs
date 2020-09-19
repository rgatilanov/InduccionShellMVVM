using ShellMVVM.Models;
using ShellMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloudEUAPage : ContentPage
    {
        CloudEUAViewModel viewModel;
        public CloudEUAPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CloudEUAViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Cloud;
            if (item == null)
                return;

            string CloudName = item.Name;
            await Shell.Current.GoToAsync($"cloudeuadetails?name={item.Name}");

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}