using System;

namespace LibreriaSofttek.Exceptions
{
    public class LibroExistenteException : BusinessException
    {
        // Excepción generada cuando se intenta realizar el eliminado lógico de un autor con libros activos (Eliminado = false)
        public LibroExistenteException()
            : base("No es posible eliminar el autor. Tiene al menos un libro relacionado.")
        {
        }
    }
}