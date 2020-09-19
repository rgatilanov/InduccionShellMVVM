using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using ShellMVVM.Models;
using ShellMVVM.Views;
using System.Windows.Input;

namespace ShellMVVM.ViewModels
{
    public class CloudEUAViewModel : BaseViewModel
    {
        public ObservableCollection<Cloud> SearchResults { get; private set; }
        public ObservableCollection<Cloud> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        //public ICommand SearchCommand => new Command<string>(SearchItems);

        public CloudEUAViewModel()
        {
            Title = "Clouds EUA";
            Items = new ObservableCollection<Cloud>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        public CloudEUAViewModel(string query)
        {
            Title = "Details " + query;
            SearchResults = new ObservableCollection<Cloud>();
            SearchItems(query);
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataCloud.GetCloudAsync();
                foreach (var item in items)
                {
                    if (item.Type == "0")
                        Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        void SearchItems(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                SearchResults = null;
            }
            else
            {
                var filteredItems = DataCloud.GetOneCloudAsync(query);
                SearchResults = new ObservableCollection<Cloud>(filteredItems);
            }
        }
    }
}