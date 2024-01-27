using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DesafioTrilhaNetMvc.Context;
using DesafioTrilhaNetMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DesafioTrilhaNetMvc.Controllers
{
    public class TarefaController : Controller
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }
        //Obter Todas As Tarefas
        public IActionResult Index()
        {
            return View();
            
        }

        public IActionResult Criar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if(tarefa.Titulo == null)
            {
                ModelState.AddModelError("Titulo","Campo Obrigatório!");
            }
            if(tarefa.Data < DateTime.Today)
            {
                ModelState.AddModelError("Data","Data Inválida.");
            }
            if(ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        public IActionResult Editar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if(tarefa == null)
                return RedirectToAction(nameof(Index));

            return View(tarefa);
        }
        [HttpPost]
        public IActionResult Editar(Tarefa tarefa)
        {
            
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            if(tarefaBanco == null)
                return NotFound();

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult BuscarPorTitulo()
        {
            return View();
        }
        

       [HttpGet]
        public IActionResult BuscarPorTitulo(string titulo)
        {
            var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();

            return View(tarefa);
        }

        public IActionResult BuscarPorData()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BuscarPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();

            return View(tarefa);
        }

        public IActionResult BuscarPorStatus()
        {
            return View();
        }

       [HttpGet]
       public IActionResult BuscarPorStatus(EnumStatusTarefa status)
       {
            
         var tarefaStatus = _context.Tarefas.Where(x => x.Status == status);
            return View(tarefaStatus);
            
       }

       public IActionResult BuscarTodas()
       {
        var tarefa = _context.Tarefas.ToList();
        return View(tarefa);

       }
       
        public IActionResult Detalhes(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if(tarefa == null)
                return RedirectToAction(nameof(Index));

            return View(tarefa);
        }

        public IActionResult Deletar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            
                if(tarefa == null)
                    return RedirectToAction(nameof(Index));

            return View(tarefa);
        }
        
        [HttpPost]
        public IActionResult Deletar(Tarefa tarefa)
        {
            
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            if(tarefaBanco ==null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        
    }
}