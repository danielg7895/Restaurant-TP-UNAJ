using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class AddComandaDTO
    {
        public int FormaEntregaId { get; set; }
        public int TotalPrice { get; set; }
        public List<int> MercaderiasIds { get; set; } = new();
    }
}
