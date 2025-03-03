using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.DTOs
{
    public class AutorDetailDTO
    {
        public long Id { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CiudadNacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public List<LibroDTO> Libros { get; set; } = new List<LibroDTO>();
    }
}