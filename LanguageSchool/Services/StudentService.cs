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
    public class StudentService
    {
        private readonly string _baseUrl = "https://localhost:44351/api";

        public async Task<List<Student>> GetAll()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/student";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Student>>(jsonResponse);
                }
                return null;
            }
        }

        public async Task<bool> CreateStudent(Student model)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/student";
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

        public async Task<bool> DeleteStudent(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/student/{id}";
                HttpResponseMessage response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<Student> GetById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/student/{id}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Student>(jsonResponse);
                }
                return null;
            }
        }
        public async Task<bool> EditStudent(Student model)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/student/{model.Id}";
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
