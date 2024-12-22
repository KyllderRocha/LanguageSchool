using LanguageSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchool.Controllers
{
    public class ClassController : Controller
    {
        public ActionResult Index()
        {
            var Classes = new List<Class>
            {
                new Class { Id = 1, Code = "1001", Name = "Math" },
                new Class { Id = 2, Code = "1002", Name = "English" },
                new Class { Id = 3, Code = "1003", Name = "Science" }
            };
            return View(Classes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Exibir página de criação
        public IActionResult Criar()
        {
            return View(new TurmaViewModel());
        }

        // POST: Salvar a nova turma
        [HttpPost]
        public IActionResult SalvarCriacao(TurmaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Salvar no banco de dados (substitua pela lógica real)
                // Exemplo: _context.Turmas.Add(new Turma { Name = model.Name, Code = model.Code });
                // _context.SaveChanges();

                return RedirectToAction("Index"); // Redireciona para a lista de turmas
            }

            return View("Criar", model); // Se houver erro, retorna à tela de criação
        }

        // GET: Exibir página de edição
        public async Task<ActionResult> Editar(int id)
        {
            var turma = await _turmaService.ObterTurmaParaEdicaoAsync(id);
            if (turma == null)
            {
                return HttpNotFound();
            }

            return View(turma);
        }

        // POST: Vincular Aluno
        [HttpPost]
        public async Task<ActionResult> VincularAluno(int TurmaId, int AlunoId)
        {
            var sucesso = await _turmaService.VincularAlunoAsync(TurmaId, AlunoId);
            if (sucesso)
            {
                return RedirectToAction("Editar", new { id = TurmaId });
            }

            return new HttpStatusCodeResult(400, "Erro ao vincular aluno.");
        }

        // POST: Remover Vinculo
        [HttpPost]
        public async Task<ActionResult> RemoverVinculo(int TurmaId, int AlunoId)
        {
            var sucesso = await _turmaService.RemoverVinculoAsync(TurmaId, AlunoId);
            if (sucesso)
            {
                return RedirectToAction("Editar", new { id = TurmaId });
            }

            return new HttpStatusCodeResult(400, "Erro ao remover vínculo.");
        }

        // POST: Salvar edição da turma
        [HttpPost]
        public async Task<ActionResult> SalvarEdicao(TurmaEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sucesso = await _turmaService.SalvarEdicaoTurmaAsync(model);
                if (sucesso)
                {
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(400, "Erro ao salvar as edições.");
            }

            return View("Editar", model);
        }
    }
}