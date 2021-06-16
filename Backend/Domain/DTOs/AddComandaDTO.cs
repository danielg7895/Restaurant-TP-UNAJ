using FluentValidation;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class AddComandaDTO
    {
        public int FormaEntrega { get; set; }
        public List<int> Mercaderia { get; set; } = new();
    }

    public class AddComandaDTOValidator : AbstractValidator<AddComandaDTO>
    {
        public AddComandaDTOValidator()
        {
            RuleFor(c => c.Mercaderia).NotEmpty().WithMessage("MercaderiasIds es requerido");
            RuleFor(c => c.FormaEntrega).NotEmpty().WithMessage("FormaEntrega es requerido")
                .LessThan(4).WithMessage("FormaEntrega debe ser un numero entre 1 y 3 inclusive.")
                .GreaterThan(0).WithMessage("FormaEntrega debe ser un numero entre 1 y 3 inclusive.");
        }
    }

}
