using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Comanda
    {
        public Guid Id { get; set; }
        public int FormaEntregaId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public List<ComandaMercaderia> ComandaMercaderias { get; set; } = new();
    }
}
