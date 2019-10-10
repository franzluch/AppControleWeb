using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AppControle.Domain.Contracts;
using AppControle.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppControle.WebCore.Controllers
{

    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            if (HttpContext != null)
            {
                ViewBag.Logado = HttpContext.Session.GetString("Logado");
            }
        }

        [Authorize]
        public IActionResult Index()
        {
            SetLogado();
            ViewBag.Usuarios = _usuarioRepositorio.ObterTodos();

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Novo()
        {
            SetLogado();
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Novo(Usuario usuario)
        {
            try
            {
                SetLogado();
                usuario.Validate();
                if (!usuario.MensagemValidacao.Any())
                {
                    _usuarioRepositorio.Adicionar(usuario);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Errors = usuario.MensagemValidacao;
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(Usuario usuario)
        {

            try
            {
                usuario.Validate();
                if (!usuario.MensagemValidacao.Any())
                {
                    if (_usuarioRepositorio.Autenticar(usuario))
                    {

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, usuario.Cpf),
                            new Claim(ClaimTypes.Email, usuario.Email),
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(principal);
                        HttpContext.Session.SetString("Logado", "1");

                        return RedirectToAction("Index", "Venda");
                    }
                    else
                    {
                        ViewBag.Errors = new List<string>();
                        ViewBag.Errors.Add("Email ou Cpf Inválidos!");
                        return View();
                    }
                }
                else
                {
                    ViewBag.Errors = usuario.MensagemValidacao;
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


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Usuario");
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