using LibreriaSofttek.DTOs;
using LibreriaSofttek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.Interfaces
{
    public interface ILibroService
    {
        List<LibroDetailDTO> GetAll();
        LibroDTO GetById(long id);
        LibroDetailDTO GetByIdDetail(long id);
        void Create(LibroDTO libroDTO);
        void Update(LibroDTO libroDTO);
        void Delete(long id);
    }
}