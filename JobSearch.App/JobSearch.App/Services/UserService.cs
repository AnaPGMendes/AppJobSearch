using JobSearch.App.Models;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.App.Services
{
    public class UserService : Service
    {
        public async Task<ResponseService<User>> GetUser(string email, string password)
        {
            HttpResponseMessage response = await _client.GetAsync
                ($"{BaseApiUrl}/api/users?email={email}&password={password}");

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode; // .ToString() tambem é uma opção válida

            if (response.IsSuccessStatusCode)
            {
                // Ler String (conteudo
                // Deserializar para User
                responseService.Data = await response.Content.ReadAsAsync<User>();
            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            return responseService;
        }

        public async Task<ResponseService<User>> AddUser(User user)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync($"{BaseApiUrl}/api/Users/", user);

            ResponseService<User> responseService = new ResponseService<User>();
            responseService.IsSuccess = response.IsSuccessStatusCode;
            responseService.StatusCode = (int)response.StatusCode; // .ToString() tambem é uma opção válida

            if (response.IsSuccessStatusCode)
            {
                responseService.Data = await response.Content.ReadAsAsync<User>();
            }
            else
            {
                String problemResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ResponseService<User>>(problemResponse);

                responseService.Errors = errors.Errors;
            }
            return responseService;

            // Antes de ser adaptado ao "ResponseService" 
            //  if (response.IsSuccessStatusCode)
            //  { user = await response.Content.ReadAsAsync<User>(); }
            //  else { user = null; } 
            //  return user;
        }
    }
}
