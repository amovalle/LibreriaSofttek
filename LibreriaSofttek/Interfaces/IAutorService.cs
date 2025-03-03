using LibreriaSofttek.DTOs;
using LibreriaSofttek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.Interfaces
{
    public interface IAutorService
    {
        List<AutorDTO> GetAll();
        AutorDTO GetById(long id);
        AutorDetailDTO GetByIdDetail(long id);
        void Create(AutorDTO autorDTO);
        void Update(AutorDTO autorDTO);
        void Delete(long id);
    }
}