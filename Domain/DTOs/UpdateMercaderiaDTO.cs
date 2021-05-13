using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class UpdateMercaderiaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Price { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string Image { get; set; }
    }
}
