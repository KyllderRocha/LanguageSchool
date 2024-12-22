using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MeuProjeto.Models;
using System.Net.Http.Headers;

namespace MeuProjeto.Services
{
    public interface ITurmaService
    {
        Task<TurmaEditViewModel> ObterTurmaParaEdicaoAsync(int turmaId);
        Task<bool> VincularAlunoAsync(int turmaId, int alunoId);
        Task<bool> RemoverVinculoAsync(int turmaId, int alunoId);
        Task<bool> SalvarEdicaoTurmaAsync(TurmaEditViewModel model);
    }

    public class TurmaService : ITurmaService
    {
        private readonly string _baseUrl = "https://suaapi.com";  // Substitua pela URL base da sua API

        // Obter os dados para editar uma turma
        public async Task<TurmaEditViewModel> ObterTurmaParaEdicaoAsync(int turmaId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/turmas/{turmaId}/editar";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TurmaEditViewModel>(jsonResponse);
                }
                return null;
            }
        }

        // Vincular aluno à turma
        public async Task<bool> VincularAlunoAsync(int turmaId, int alunoId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/turmas/{turmaId}/vincular-aluno";
                var content = new StringContent(JsonConvert.SerializeObject(new { AlunoId = alunoId }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
        }

        // Remover vínculo de aluno com a turma
        public async Task<bool> RemoverVinculoAsync(int turmaId, int alunoId)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/turmas/{turmaId}/remover-vinculo";
                var content = new StringContent(JsonConvert.SerializeObject(new { AlunoId = alunoId }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                return response.IsSuccessStatusCode;
            }
        }

        // Salvar alterações na turma
        public async Task<bool> SalvarEdicaoTurmaAsync(TurmaEditViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{_baseUrl}/turmas/{model.Id}/editar";
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(url, content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
