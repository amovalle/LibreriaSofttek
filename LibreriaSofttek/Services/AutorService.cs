using LibreriaSofttek.DTOs;
using LibreriaSofttek.Exceptions;
using LibreriaSofttek.Interfaces;
using LibreriaSofttek.Models;
using LibreriaSofttek.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.Services
{
    public class AutorService : IAutorService
    {
        private readonly LibreriaSofttekContext _context;

        public AutorService(LibreriaSofttekContext context)
        {
            _context = context;
        }

        public List<AutorDTO> GetAll()
        {
            return _context.Autor
                .Where(a => !a.Eliminado)
                .OrderBy(a => a.NombreCompleto)
                .Select(a => new AutorDTO
                {
                    Id = a.Id,
                    NombreCompleto = a.NombreCompleto,
                    FechaNacimiento = a.FechaNacimiento,
                    CiudadNacimiento = a.CiudadNacimiento,
                    CorreoElectronico = a.CorreoElectronico
                }).ToList();
        }

        public AutorDTO GetById(long id)
        {
            var autor = _context.Autor.Find(id);

            if (autor == null || autor.Eliminado)
                return null;

            return new AutorDTO
            {
                Id = autor.Id,
                NombreCompleto = autor.NombreCompleto,
                FechaNacimiento = autor.FechaNacimiento,
                CiudadNacimiento = autor.CiudadNacimiento,
                CorreoElectronico = autor.CorreoElectronico
            };
        }

        public AutorDetailDTO GetByIdDetail(long id)
        {
            // Se consulta el detalle completo el autor con libros relacionados
            var autor = _context.Autor
                .Where(a => a.Id == id && !a.Eliminado)
                .Select(a => new AutorDetailDTO
                {
                    Id = a.Id,
                    NombreCompleto = a.NombreCompleto,
                    FechaNacimiento = a.FechaNacimiento,
                    CiudadNacimiento = a.CiudadNacimiento,
                    CorreoElectronico = a.CorreoElectronico,
                    Libros = _context.Libro
                        .Where(l => l.IdAutor == a.Id && !l.Eliminado)
                        .Select(l => new LibroDTO
                        {
                            Id = l.Id,
                            Titulo = l.Titulo,
                            Ano = l.Ano,
                            Genero = l.Genero
                        })
                        .ToList()
                })
                .FirstOrDefault();

            return autor;
        }

        public void Create(AutorDTO autorDTO)
        {
            // Se lleva a cabo la asignación de valores y creación del registro
            var autor = new Autor
            {
                NombreCompleto = autorDTO.NombreCompleto,
                FechaNacimiento = autorDTO.FechaNacimiento,
                CiudadNacimiento = autorDTO.CiudadNacimiento,
                CorreoElectronico = autorDTO.CorreoElectronico,
                FechaRegistro = DateTime.Now // Se asigna fecha actual para la fecha de registro
            };

            _context.Autor.Add(autor);
            _context.SaveChanges();
        }

        public void Update(AutorDTO autorDTO)
        {
            // Se lleva a cabo la asignación de valores y actualización del registro
            var autor = _context.Autor.Find(autorDTO.Id);
            autor.NombreCompleto = autorDTO.NombreCompleto;
            autor.FechaNacimiento = autorDTO.FechaNacimiento;
            autor.CiudadNacimiento = autorDTO.CiudadNacimiento;
            autor.CorreoElectronico = autorDTO.CorreoElectronico;

            _context.Entry(autor).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var autor = _context.Autor.Find(id);
            if (autor != null)
            {
                // Control para validación de existencia de libros activos relacionados (Eliminado = false)
                bool librosRelacionados = _context.Libro.Any(l => l.IdAutor == autor.Id && !l.Eliminado);
                if (librosRelacionados)
                    throw new LibroExistenteException();

                // Opción para eliminación definitiva del registro, comentada para dejar evidencia en prueba técnica
                //_context.Autor.Remove(autor);

                // Se lleva a cabo la eliminación lógica del registro
                autor.Eliminado = true;
                _context.SaveChanges();
            }
        }
    }
}