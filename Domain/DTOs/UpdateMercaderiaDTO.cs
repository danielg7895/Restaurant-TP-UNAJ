﻿using Domain.Entities;
using FluentValidation;
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

    public class UpdateMercaderiaDTOValidator : AbstractValidator<UpdateMercaderiaDTO>
    {
        public UpdateMercaderiaDTOValidator()
        {
            RuleFor(m => m.Name).MinimumLength(1).MaximumLength(50).NotEmpty().WithMessage("Nombre no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Ingredients).MinimumLength(1).MaximumLength(50).NotEmpty().WithMessage("Ingredientes no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Preparation).MinimumLength(1).MaximumLength(50).NotEmpty().WithMessage("Preparation no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Image).MinimumLength(1).MaximumLength(50).NotEmpty().WithMessage("Image no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Price).GreaterThan(-1).LessThan(int.MaxValue).NotNull().WithMessage($"Precio debe estar entre 0 y {int.MaxValue}");
        }
    }
}
