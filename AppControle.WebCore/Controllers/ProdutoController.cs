using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppControle.WebCore.Controllers
{

    [Authorize]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        public ProdutoController(IProdutoRepositorio produtoRepositorio, 
            ICategoriaRepositorio categoriaRepositorio,
            IEstoqueRepositorio estoqueRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _estoqueRepositorio = estoqueRepositorio;

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
                ViewBag.Produtos = _produtoRepositorio.ObterTodos2();
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
            //ViewBag.CategoriaId = new SelectList(_categoriaRepositorio.ObterTodos());
            ViewBag.CategoriaId = _categoriaRepositorio.ObterTodos().Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x.Nome,
                                      Value = x.Id.ToString()
                                  });
            return View();
        }
        [HttpPost]
        public IActionResult Novo(Produto produto)
        {
            try
            {
                SetLogado();
                produto.Validate();
                if (!produto.MensagemValidacao.Any())
                {
                    _produtoRepositorio.Adicionar(produto);
                    _estoqueRepositorio.Adicionar(new Estoque
                    {
                        Quantidade = produto.Quantidade,
                        ProdutoId = produto.Id
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Errors = produto.MensagemValidacao;
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

        [HttpGet]
        public IActionResult Editar(int id)
        {
            SetLogado();
            ViewBag.Produto = _produtoRepositorio.ObterPorId(id);
            return View();
        }
        [HttpPost]
        public IActionResult Editar(Estoque estoque)
        {
            try
            {
                SetLogado();
                estoque.Validate();
                if (!estoque.MensagemValidacao.Any())
                {
                    if (_estoqueRepositorio.ObterTodos().Where(x=>x.Id == estoque.Id).FirstOrDefault() != null)
                    {
                        //_estoqueRepositorio.Remover(estoque);
                        _estoqueRepositorio.Atualizar(estoque);
                    }
                    else
                    {
                        _estoqueRepositorio.Adicionar(estoque);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Errors = estoque.MensagemValidacao;
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
            }
        }
    }
}