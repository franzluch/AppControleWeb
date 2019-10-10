using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppControle.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AppControle.Web.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        public CategoriaController(ICategoriaRepositorio categoriaRepositorio)
        {
            _categoriaRepositorio = categoriaRepositorio;
        }

        public ViewResult Index()
        {
            try
            {
                ViewBag.Categorias = _categoriaRepositorio.ObterTodos();
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
    }
}