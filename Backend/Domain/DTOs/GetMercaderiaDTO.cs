﻿using FluentValidation;

namespace Domain.DTOs
{
    public class GetMercaderiaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }
    }

}
