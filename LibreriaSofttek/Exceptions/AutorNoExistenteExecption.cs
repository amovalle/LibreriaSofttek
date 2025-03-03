using System;

namespace LibreriaSofttek.Exceptions
{
    public class AutorNoExistenteExecption : BusinessException
    {
        // Excepción generada cuando se intenta ingresar el Id de un autor no registrado o activo (Eliminado = false)
        public AutorNoExistenteExecption()
            : base("El autor no está registrado. Por favor, ingrese el Id de un autor válido.")
        {
        }
    }
}