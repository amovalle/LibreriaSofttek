using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.DTOs
{
    public class LibroDetailDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public int Genero { get; set; }
        public int NumeroPaginas { get; set; }
        public long IdAutor { get; set; }
        public string NombreAutor { get; set; }
    }
}