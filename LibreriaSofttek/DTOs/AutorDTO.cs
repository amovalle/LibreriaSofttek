using LibreriaSofttek.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.DTOs
{
    public class AutorDTO
    {
        public long Id { get; set; }

        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "El campo Nombre completo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El texto en el campo Nombre completo no puede superar los 100 caracteres.")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "El campo Fecha de nacimiento es obligatorio.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime FechaNacimiento { get; set; }

        [Display(Name = "Ciudad de procedencia")]
        [Required(ErrorMessage = "El campo Ciudad de procedencia es obligatorio.")]
        [StringLength(50, ErrorMessage = "El texto en el campo Ciudad de procedencia no puede superar los 50 caracteres.")]
        public string CiudadNacimiento { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El campo Correo electrónico es obligatorio.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El formato del campo Correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }

        public virtual ICollection<Libro> Libros { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}