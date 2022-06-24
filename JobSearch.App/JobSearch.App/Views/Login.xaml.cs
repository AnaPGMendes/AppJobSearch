using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using JobSearch.App.Utility.Load;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private UserService _service;
        public Login()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void BtGoRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }

        private async void BtGoStart(object sender, EventArgs e)
        {
            /* Navigation.PushAsync(new Start()); Não foi utilizado NavigationPage 
             * apartir do Login porque não faz sentido dar um 'back' e retornar do menu para o Login*/

            string email = textEmail.Text;
            string password;

            // entry adapatado ás plataformas
            if (Device.RuntimePlatform == Device.iOS ||
                Device.RuntimePlatform == Device.Android)
            { password = EntrySenha1.Text; }
            else { password = EntrySenha2.Text; }

            await Navigation.PushPopupAsync(new Loading());
            //wait Task.Delay(3000); - 3 segundos de atraso para simular atrasos gerados pela aplicação
            ResponseService<User> responseService = await _service.GetUser(email, password);

            if (responseService.IsSuccess)
            {
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));
                await App.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new Start()); // Current é a instancia atual (basicamente);
            }
            //if (responseService.IsSuccess)
            //{
            //    App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService.Data));
            //    await App.Current.SavePropertiesAsync();
            //    App.Current.MainPage = new NavigationPage(new Start());
            //}
            else
            {
                if (responseService.StatusCode == 404)
                {
                    await DisplayAlert("Erro!", "Nenhum usuário encontrado", "OK");
                }
                else
                {
                    await DisplayAlert("Erro!", "Oops! Ocorreu un erro inesperado!Tente novamente mais tarde.", "OK");
                }
            }
            await Navigation.PopAllPopupAsync();
        }

        // checkbox para tornar senha visível
        private void CheckBox_PasswordIsVisible(object sender, CheckedChangedEventArgs e)
        {
            // achando o checkbox
            var checkBox = (CheckBox)sender;

            // achando o "EntrySenha", procurando pela Grid que engloba ele
            var grid = checkBox.Parent as Grid;
            var entry1 = grid.FindByName<Entry>("EntrySenha1");
            var entry2 = grid.FindByName<Entry>("EntrySenha2");

            // tendo certeza de que o EntrySenha não é nulo
            if (entry1 != null)
            {
                entry1.IsPassword = !checkBox.IsChecked;

                //if (checkBox.IsChecked)
                //{
                //    entry.IsPassword = false;
                //}
                //else
                //{
                //    entry.IsPassword = true;
                //}
            }
            if (entry2 != null)
            {
                entry2.IsPassword = !checkBox.IsChecked;
            }
        }
    }
}