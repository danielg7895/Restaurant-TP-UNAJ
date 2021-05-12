using System;

namespace Application
{
    public class InvalidIdentifier : Exception
    {
        public InvalidIdentifier() : base("El id pasado por parametro no existe en la base de datos."){}
    }
}
