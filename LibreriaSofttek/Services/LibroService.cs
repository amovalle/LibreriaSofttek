using LibreriaSofttek.Exceptions;
using LibreriaSofttek.DTOs;
using LibreriaSofttek.Helpers;
using LibreriaSofttek.Interfaces;
using LibreriaSofttek.Models;
using LibreriaSofttek.Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.Services
{
    public class LibroService : ILibroService
    {
        private readonly LibreriaSofttekContext _context;

        public LibroService(LibreriaSofttekContext context)
        {
            _context = context;
        }

        public List<LibroDetailDTO> GetAll()
        {
            return _context.Libro
                .Where(l => !l.Eliminado)
                .OrderBy(l => l.Titulo)
                .Select(l => new LibroDetailDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Ano = l.Ano,
                    Genero = l.Genero,
                    NumeroPaginas = l.NumeroPaginas,
                    NombreAutor = l.Autor.NombreCompleto
                }).ToList();
        }

        public LibroDTO GetById(long id)
        {
            var libro = _context.Libro.Find(id);

            if (libro == null || libro.Eliminado)
                return null;

            return new LibroDTO
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Ano = libro.Ano,
                Genero = libro.Genero,
                NumeroPaginas = libro.NumeroPaginas,
                IdAutor = libro.IdAutor
            };
        }

        public LibroDetailDTO GetByIdDetail(long id)
        {
            // Se consulta un libro específico con el nombre del autor relacionado

            var autor = _context.Libro
                .Where(l => l.Id == id && !l.Eliminado)
                .Select(l => new LibroDetailDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Ano = l.Ano,
                    Genero = l.Genero,
                    NumeroPaginas = l.NumeroPaginas,
                    NombreAutor = l.Autor.NombreCompleto
                })
                .FirstOrDefault();

            return autor;
        }

        public void Create(LibroDTO libroDTO)
        {
            // Control para no permitir el registro de un libro cuando se ha alcanzado el másximo permitido 
            long maxLibrosPermitidos = GetMaxLibrosPermitidos();
            long totalLibros = _context.Libro.Count(l => !l.Eliminado);

            if (totalLibros >= maxLibrosPermitidos)
                throw new MaxLibrosPermitidosException();

            // Control para no permitir el registro de un libro con el Id de un autor que no existe 
            var autor = _context.Autor.Find(libroDTO.IdAutor);
            if (autor == null || autor.Eliminado)
                throw new AutorNoExistenteExecption();

            if (totalLibros >= maxLibrosPermitidos)
                throw new MaxLibrosPermitidosException();

            // Se lleva a cabo la asignación de valores y creación del registro
            var libro = new Libro
            {
                Titulo = libroDTO.Titulo,
                Ano = libroDTO.Ano,
                Genero = libroDTO.Genero,
                NumeroPaginas = libroDTO.NumeroPaginas,
                IdAutor = libroDTO.IdAutor,
                FechaRegistro = DateTime.Now // Se asigna fecha actual para la fecha de registro
            };

            _context.Libro.Add(libro);
            _context.SaveChanges();
        }

        public void Update(LibroDTO libroDTO)
        {
            // Control para no permitir el registro de un libro con el Id de un autor que no existe 
            var autor = _context.Autor.Find(libroDTO.IdAutor);
            if (autor == null || autor.Eliminado)
                throw new AutorNoExistenteExecption();

            // Se lleva a cabo la asignación de valores y actualización del registro
            var libro = _context.Libro.Find(libroDTO.Id);
            libro.Titulo = libroDTO.Titulo;
            libro.Ano = libroDTO.Ano;
            libro.Genero = libroDTO.Genero;
            libro.NumeroPaginas = libroDTO.NumeroPaginas;
            libro.IdAutor = libroDTO.IdAutor;

            _context.Entry(libro).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var libro = _context.Libro.Find(id);
            if (libro != null)
            {
                // Opción para eliminación definitiva del registro, comentada para dejar evidencia en prueba técnica
                //_context.Libro.Remove(libro);

                // Se lleva a cabo la eliminación lógica del registro
                libro.Eliminado = true;
                _context.SaveChanges();
            }
        }

        public long GetMaxLibrosPermitidos()
        {
            string configMaxLibrosPermitidos = ConfigurationManager.AppSettings["MaxLibrosPermitidos"];

            if (string.IsNullOrWhiteSpace(configMaxLibrosPermitidos) || !long.TryParse(configMaxLibrosPermitidos, out long maxLibrosPermitidos))
                throw new ConfigurationErrorsException("No hay un máximo de libros permitido definido en el sistema. Por favor consultar con el soporte de la aplicación.");

            return maxLibrosPermitidos;
        }
    }
}