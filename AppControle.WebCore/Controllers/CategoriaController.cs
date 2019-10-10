using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppControle.WebCore.Controllers
{

    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
            if (HttpContext != null)
            {
                ViewBag.Logado = HttpContext.Session.GetString("Logado");
            }

        }

        
        public IActionResult Index()
        {
            try
            {
                SetLogado();
                ViewBag.Categorias = _categoriaRepositorio.ObterTodos();
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public IActionResult Novo()
        {
            SetLogado();
            return View();
        }
        [HttpPost]
        public IActionResult Novo(Categoria categoria)
        {
            try
            {
                SetLogado();
                categoria.Validate();
                if (!categoria.MensagemValidacao.Any())
                {
                    _categoriaRepositorio.Adicionar(categoria);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Errors = categoria.MensagemValidacao;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errors = new List<string>();
                ViewBag.Errors.Add(ex.Message);
                return View();
            }
        }
        public void SetLogado()
        {
            if (HttpContext != null)
            {
                ViewBag.Logado = HttpContext.Session.GetString("Logado");
                if (ViewBag.Logado != "1")
                {
                    RedirectToAction("Login", "Usuario");
                }
            }
        }

    }
}