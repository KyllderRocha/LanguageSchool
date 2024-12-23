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
    public class EnrollmentService
    {
        private readonly string _baseUrl = "https://localhost:44351/api";

        public async Task<List<Enrollment>> GetEnrollmentsByClassId(int classId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/Enrollment/GetByClassId/{classId}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Enrollment>>(jsonResponse);
                }
                return new List<Enrollment>();
            }
        }

        public async Task<List<Enrollment>> GetEnrollmentsByStudentId(int studentId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/Enrollment/GetByStudentId/{studentId}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Enrollment>>(jsonResponse);
                }
                return null;
            }
        }

        public async Task<List<Student>> GetStudentsNotInClass(int classId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/Enrollment/GetStudentsNotInClass/{classId}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Student>>(jsonResponse);
                }
                return new List<Student>();
            }
        }

        public async Task<List<Class>> GetClassesNotForStudent(int studentId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/Enrollment/GetClassesNotForStudent/{studentId}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Class>>(jsonResponse);
                }
                return new List<Class>();
            }
        }


        public async Task<bool> AddEnrollment(Enrollment model)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/Enrollment";
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteEnrollment(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/Enrollment/{id}";
                HttpResponseMessage response = await client.DeleteAsync(url);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
