using System.Collections.Generic;

namespace Domain.Entities
{
    public class FormaEntrega
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Comanda> Comanda { get; set; }
    }
}
