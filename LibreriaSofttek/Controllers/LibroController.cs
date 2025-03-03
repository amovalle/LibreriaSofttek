using LibreriaSofttek.DTOs;
using LibreriaSofttek.Exceptions;
using LibreriaSofttek.Helpers;
using LibreriaSofttek.Helpers.Messages;
using LibreriaSofttek.Interfaces;
using LibreriaSofttek.Models;
using LibreriaSofttek.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibreriaSofttek.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;
        private readonly IAutorService _autorService;

        public LibroController(ILibroService libroService, IAutorService autorService)
        {
            _libroService = libroService;
            _autorService = autorService;
        }

        public ActionResult Index()
        {
            var libros = _libroService.GetAll();
            return View(libros);
        }

        public ActionResult Detail(long id)
        {
            var libro = _libroService.GetByIdDetail(id);

            if (libro == null)
            {
                TempData["ErrorMessage"] = DefaultMessages.RegistroNoExiste;
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        private void GetSelectList()
        {
            ViewBag.Autores = new SelectList(_autorService.GetAll(), "Id", "NombreCompleto");
            ViewBag.Anos = Enumerable.Range(1900, DateTime.Now.Year - 1899).Reverse();
            ViewBag.Generos = Enum.GetValues(typeof(GeneroEnum))
                .Cast<GeneroEnum>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.GetDisplayName()
                })
                .OrderBy(item => item.Text)
                .ToList();
        }

        public ActionResult Create()
        {
            GetSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(LibroDTO libroDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _libroService.Create(libroDTO);
                    TempData["SuccessMessage"] = DefaultMessages.RegistroExitoso;
                    return RedirectToAction("Index");
                }
                catch (MaxLibrosPermitidosException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("Index");
                }
                // Opción de excepción que controla el ingreso manual del IdAutor 
                catch (AutorNoExistenteExecption ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = DefaultMessages.ErrorOperacion;
                }
            }

            GetSelectList();
            return View(libroDTO);
        }

        public ActionResult Update(long id)
        {
            var libro = _libroService.GetById(id);
            if (libro == null)
            {
                TempData["ErrorMessage"] = DefaultMessages.RegistroNoExiste;
                return RedirectToAction("Index");
            }

            GetSelectList();
            return View(libro);
        }

        [HttpPost]
        public ActionResult Update(LibroDTO libroDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _libroService.Update(libroDTO);
                    TempData["SuccessMessage"] = DefaultMessages.ActualizacionExitosa;
                    return RedirectToAction("Index");
                }
                // Opción de excepción que controla el ingreso manual del IdAutor 
                catch (AutorNoExistenteExecption ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = DefaultMessages.ErrorOperacion;
                }
            }

            GetSelectList();
            return View(libroDTO);
        }

        public ActionResult Delete(long id)
        {
            try
            {
                _libroService.Delete(id);
                TempData["SuccessMessage"] = DefaultMessages.EliminacionExitosa;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = DefaultMessages.ErrorOperacion;
            }

            return RedirectToAction("Index");
        }
    }
}