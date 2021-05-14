using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class ComandaResponseDTO
    {
        public Guid Id { get; set; }
        public int FormaEntregaId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public List<int> ComandaMercaderiasId { get; set; } = new();
    }
}
