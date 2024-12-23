using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LanguageSchool.Models;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace LanguageSchool.Services
{
    public class ClassService
    {
        private readonly string _baseUrl = "https://localhost:44351/api";

        public async Task<List<Class>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/class";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Class>>(jsonResponse);
                }
                return new List<Class>();
            }
        }

        public async Task<List<Class>> GetAllAvailable()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/class/Available";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Class>>(jsonResponse);
                }
                return new List<Class>();
            }
        }

        public async Task<bool> CreateClass(Class model)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/class";
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> DeleteClass(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/class/{id}";
                HttpResponseMessage response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<Class> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/class/{id}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Class>(jsonResponse);
                }
                return null;
            }
        }
        public async Task<bool> EditClass(Class model)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/class/{model.Id}";
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
