using LibreriaSofttek.DTOs;
using LibreriaSofttek.Exceptions;
using LibreriaSofttek.Helpers.Messages;
using LibreriaSofttek.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibreriaSofttek.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        public ActionResult Index()
        {
            var autores = _autorService.GetAll();
            return View(autores);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AutorDTO autorDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _autorService.Create(autorDTO);
                    TempData["SuccessMessage"] = DefaultMessages.RegistroExitoso;
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = DefaultMessages.ErrorOperacion;
                }
            }

            return View(autorDTO);
        }

        public ActionResult Detail(long id)
        {
            var autor = _autorService.GetByIdDetail(id);

            if (autor == null)
                return HttpNotFound();

            return View(autor);
        }

        public ActionResult Update(long id)
        {
            var autor = _autorService.GetById(id);
            if (autor == null)
                return HttpNotFound();

            return View(autor);
        }

        [HttpPost]
        public ActionResult Update(AutorDTO autorDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _autorService.Update(autorDTO);
                    TempData["SuccessMessage"] = DefaultMessages.ActualizacionExitosa;
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = DefaultMessages.ErrorOperacion;
                }
            }

            return View(autorDTO);
        }

        public ActionResult Delete(long id)
        {
            try
            {
                _autorService.Delete(id);
                TempData["SuccessMessage"] = DefaultMessages.EliminacionExitosa;
            }
            catch (LibroExistenteException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = DefaultMessages.ErrorOperacion;
            }

            return RedirectToAction("Index");
        }
    }
}