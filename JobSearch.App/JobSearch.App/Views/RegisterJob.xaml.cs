using JobSearch.App.Models;
using JobSearch.App.Services;
using JobSearch.App.Utility.Converters;
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
    public partial class RegisterJob : ContentPage
    {
        private JobService _service; 
        public RegisterJob()
        {
            InitializeComponent();
            _service = new JobService();
        }

        private void ImGoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Save(object sender, EventArgs e)
        {
            // TODO - Adicionar Validação
            txtMessage.Text = string.Empty;

            User user = JsonConvert.DeserializeObject<User>(App.Current.Properties["User"].ToString());

            Job job = new Job()
            {
                Company = txtCompany.Text,
                JobTitle = txtJobTitle.Text,
                CityState = txtCityState.Text,
                InitialSalary = TextToDoubleConverter.ToDouble(txtInitialSalary.Text),
                FinalSalary = TextToDoubleConverter.ToDouble(txtFinalSalary.Text),
                ContractType = (RBCLT.IsChecked) ? "CLT" : "PJ",
                TecnologyTools = txtTecnologyTools.Text,
                CompanyDescription = txtCompanyDescription.Text,
                JobDescription = txtJobDescription.Text,
                Benefits = txtBenefits.Text,
                InterestedSendEmailTo = txtInterestedSendEmailTo.Text,
                PublicationDate = DateTime.Now,
                UserId = user.Id
            };

            await Navigation.PushPopupAsync(new Loading());
            ResponseService<Job> responseService = await _service.AddJob(job);

            if (responseService.IsSuccess)
            {
                await Navigation.PopAllPopupAsync();
                await DisplayAlert("Vaga Cadastrada!", "Vaga Cadastrada com Sucesso.","OK");
                await Navigation.PopAsync();
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
                    await DisplayAlert("Erro!", "Oops! Ocorreu um erro inesperado!Tente novamente mais tarde.", "OK");
                }
                await Navigation.PopAllPopupAsync();
            }
        }
    }
}