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
    [QueryProperty("Name", "name")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CloudEUADetailPage : ContentPage
    {

        public string Name
        {
            set
            {
                //BindingContext = BearData.Bears.FirstOrDefault(m => m.Name == Uri.UnescapeDataString(value));
                CloudEUAViewModel viewModel = new CloudEUAViewModel(Uri.UnescapeDataString(value));
                BindingContext = viewModel.SearchResults.First();
            }
        }

        public CloudEUADetailPage()
        {
            InitializeComponent();
        }
    }
}