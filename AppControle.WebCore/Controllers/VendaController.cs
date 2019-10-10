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
    public class VendaController : Controller
    {
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IEstoqueRepositorio _estoqueRepositorio;
        public VendaController(IVendaRepositorio vendaRepositorio, 
            IProdutoRepositorio produtoRepositorio,
            IEstoqueRepositorio estoqueRepositorio)
        {
            _vendaRepositorio = vendaRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _estoqueRepositorio = estoqueRepositorio;
            if (HttpContext != null)
            {
                ViewBag.Logado = HttpContext.Session.GetString("Logado");
            }

        }

        public IActionResult Index()
        {
            SetLogado();
            ViewBag.Vendas = _vendaRepositorio.ObterTodos2();
            return View();
        }
        [HttpGet]
        public IActionResult Novo()
        {
            SetLogado();
            ViewBag.Produtos = _produtoRepositorio.ObterTodos().Select(x =>
                      new SelectListItem()
                      {
                          Text = x.Descricao,
                          Value = x.Id.ToString()
                      });

            return View();
        }
        [HttpPost]
        public IActionResult Novo(Venda venda)
        {
            SetLogado();
            ViewBag.Produtos = _produtoRepositorio.ObterTodos().Select(x =>
              new SelectListItem()
              {
                  Text = x.Descricao,
                  Value = x.Id.ToString()
              });

            try
            {
                ViewBag.Errors = new List<string>();

                venda.Validate();

                var estoque = _estoqueRepositorio.ObterTodos().Where(x => x.ProdutoId == venda.ProdutoId).FirstOrDefault();
                if(estoque !=null)
                {
                    if(estoque.Quantidade < venda.Quantidade)
                    {
                        ViewBag.Errors.Add("A quantidade informada para o produto é superior a existente no estoque");
                        return View();
                    }
                }
                else
                {
                    ViewBag.Errors.Add("Não foi possível localizar o estoque do produto informado.");
                    return View();

                }
                if (!venda.MensagemValidacao.Any())
                {
                    venda.DataVenda = DateTime.Now;
                    _vendaRepositorio.Adicionar(venda);

                    estoque.Quantidade = estoque.Quantidade - venda.Quantidade;
                    _estoqueRepositorio.Atualizar(estoque);


                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Errors = venda.MensagemValidacao;
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