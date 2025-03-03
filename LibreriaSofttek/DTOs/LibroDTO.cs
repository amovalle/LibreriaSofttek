using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.DTOs
{
    public class LibroDTO
    {
        public long Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El campo Título es obligatorio.")]
        [StringLength(50, ErrorMessage = "El texto en el campo Título no puede superar los 50 caracteres.")]
        public string Titulo { get; set; }

        [Display(Name = "Año de publicación")]
        [Required(ErrorMessage = "La selección en Año de publicación es obligatoria.")]
        public int Ano { get; set; }

        [Display(Name = "Género literario")]
        [Required(ErrorMessage = "La selección en Género literario es obligatoria.")]
        public int Genero { get; set; }

        [Display(Name = "Número de páginas")]
        [Required(ErrorMessage = "El campo Número de páginas es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El valor en el campo Número de páginas debe ser un número mayor que 0.")]
        public int NumeroPaginas { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "La selección en Autor es obligatoria.")]
        public long IdAutor { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}