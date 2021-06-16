using FluentValidation;

namespace Domain.DTOs
{
    public class AddMercaderiaDTO
    {
        public string Nombre { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Precio { get; set; }
        public string Ingredientes { get; set; }
        public string Preparacion { get; set; }
        public string Imagen { get; set; }
    }

    public class AddMercaderiaDTOValidator : AbstractValidator<AddMercaderiaDTO>
    {
        public AddMercaderiaDTOValidator()
        {
            RuleFor(m => m.Nombre)        .MinimumLength(1).MaximumLength(50).NotEmpty().WithMessage("Nombre no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Ingredientes) .MinimumLength(1).MaximumLength(255).NotEmpty().WithMessage("Ingredientes no puede estar vacio o contener mas de 255 caracteres.");
            RuleFor(m => m.Preparacion) .MinimumLength(1).MaximumLength(255).NotEmpty().WithMessage("Preparation no puede estar vacio o contener mas de 255 caracteres.");
            RuleFor(m => m.Imagen)       .MinimumLength(1).MaximumLength(255).NotEmpty().WithMessage("Image no puede estar vacio o contener mas de 255 caracteres.");
            RuleFor(m => m.Precio)       .GreaterThan(-1).LessThan(int.MaxValue).NotNull().WithMessage($"Precio debe estar entre 0 y {int.MaxValue}");
        }
    }
}
