using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibreriaSofttek.Models.Enums
{
    public enum GeneroEnum
    {
        [Display(Name = "Biografía")]
        Biografia = 1,

        [Display(Name = "Ciencia ficción")]
        CienciaFiccion = 2,

        [Display(Name = "Drama")]
        Drama = 3,

        [Display(Name = "Fantasía")]
        Fantasia = 4,

        [Display(Name = "Manual")]
        Manual = 5,

        [Display(Name = "Histórico")]
        Historico = 6,

        [Display(Name = "Humor")]
        Humor = 7,

        [Display(Name = "Infantil")]
        Infantil = 8,

        [Display(Name = "No ficción")]
        NoFicccion = 9,

        [Display(Name = "Poema")]
        Poema = 10,

        [Display(Name = "Romántico")]
        Romántico = 11,

        [Display(Name = "Terror")]
        Terror = 12,
    }
}