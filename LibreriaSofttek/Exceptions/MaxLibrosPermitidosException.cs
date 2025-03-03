using System;

namespace LibreriaSofttek.Exceptions
{
    public class MaxLibrosPermitidosException : BusinessException
    {
        // Excepción generada cuando se alcanza o supera el número de libros registrados según MaxLibrosPermitidos en web.config
        public MaxLibrosPermitidosException()
            : base("No es posible registrar el libro. Se alcanzó el número máximo de libros permitido.")
        {
        }
    }
}