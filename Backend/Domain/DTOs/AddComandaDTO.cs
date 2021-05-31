using FluentValidation;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class AddComandaDTO
    {
        public int FormaEntregaId { get; set; }
        public List<int> MercaderiasIds { get; set; } = new();
    }

    public class AddComandaDTOValidator : AbstractValidator<AddComandaDTO>
    {
        public AddComandaDTOValidator()
        {
            RuleFor(c => c.MercaderiasIds).NotEmpty().WithMessage("MercaderiasIds es requerido");
        }
    }

}
