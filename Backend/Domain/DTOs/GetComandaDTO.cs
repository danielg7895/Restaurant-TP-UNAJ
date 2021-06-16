using System;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class GetComandaDTO
    {
        public Guid Id { get; set; }
        public int FormaEntrega { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Dia { get; set; }
    }
}
