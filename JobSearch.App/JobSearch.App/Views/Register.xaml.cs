using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Load;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private UserService _service;
        public Register()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // explicação em Login.xaml.cs
            var checkBox = (CheckBox)sender;

            var grid = checkBox.Parent as Grid;
            var entry1 = grid.FindByName<Entry>("EntrySenha1");
            var entry2 = grid.FindByName<Entry>("EntrySenha2");

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

        private void ImGoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveAction(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password;

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                password = txtEntrySenha1.Text;
            }
            else
            {
                password = txtEntrySenha2.Text;
            }

            User user = new User() { Name = name, Email = email, Password = password };

            await Navigation.PushPopupAsync(new Loading());
            ResponseService<User> responseService = await _service.AddUser(user);

            if (responseService.IsSuccess)
            {
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(responseService));
                await App.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new Start()); // Current é a instancia atual (basicamente);
            }
            else
            {
                if (responseService.StatusCode == 400)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var dictKey in responseService.Errors)
                    {
                        foreach (var message in dictKey.Value)
                        {
                            sb.AppendLine(message.ToString());
                        }
                    }
                    txtMessage.Text = sb.ToString();
                }
                else
                {
                    await DisplayAlert("Erro!", "Oops! Ocorreu un erro inesperado!Tente novamente mais tarde.", "OK");
                }
            }
            await Navigation.PopAllPopupAsync();
        }
    }
}