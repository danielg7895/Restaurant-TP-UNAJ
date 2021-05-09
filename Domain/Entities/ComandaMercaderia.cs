using System;

namespace Domain.Entities
{
    public class ComandaMercaderia
    {
        public int Id { get; set; }
        public int MercaderiaId { get; set; }
        public Guid ComandaId { get; set; }
        public Mercaderia Mercaderia { get; set; }
        public Comanda Comanda { get; set; }

    }
}
