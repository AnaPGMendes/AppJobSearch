using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Start : ContentPage
    {
        private JobService _service;
        public Start()
        {
            InitializeComponent();
            _service = new JobService();
        }

        private void GoVisualizer(object sender, EventArgs e)
        {
            var eventArgs = (TappedEventArgs)e;

            var page = new Visualizer();
            page.BindingContext = eventArgs.Parameter;

            Navigation.PushAsync(page);
            // Navigation.PushAsync(new Visualizer());
        }

        private void btGoRegisterJob(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterJob());
        }

        private void FocusPesquisa(object sender, EventArgs e)
        {
            plPesquisa.Focus();
        }

        private void FocusCidadeEstado(object sender, EventArgs e)
        {
            plCidadeEstado.Focus();
        }

        private void Logout(object sender, EventArgs e)
        {
            App.Current.Properties.Remove("User");
            App.Current.SavePropertiesAsync();
            App.Current.MainPage = new Login();
        }
    }
}