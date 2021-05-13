using System;

namespace Application
{
    public class InvalidIdentifier : Exception
    {
        public InvalidIdentifier() : base("El id enviado no existe en la base de datos."){}
        public InvalidIdentifier(string name) : base($"El id {name} enviado no existe en la base de datos."){}
    }

    public class InvalidDate : Exception
    {
        public InvalidDate() : base("El formato del Dia ingresado no es valido. El formato debe ser yyyy-mm-dd") { }
    }
}
