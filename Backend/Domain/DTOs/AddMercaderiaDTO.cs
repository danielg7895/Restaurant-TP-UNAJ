using FluentValidation;

namespace Domain.DTOs
{
    public class AddMercaderiaDTO
    {
        public string Name { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Price { get; set; }
        public string Ingredients { get; set; }
        public string Preparation { get; set; }
        public string Image { get; set; }
    }

    public class AddMercaderiaDTOValidator : AbstractValidator<AddMercaderiaDTO>
    {
        public AddMercaderiaDTOValidator()
        {
            RuleFor(m => m.Name)        .MinimumLength(1).MaximumLength(50).NotEmpty().WithMessage("Nombre no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Ingredients) .MinimumLength(1).MaximumLength(255).NotEmpty().WithMessage("Ingredientes no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Preparation) .MinimumLength(1).MaximumLength(255).NotEmpty().WithMessage("Preparation no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Image)       .MinimumLength(1).MaximumLength(255).NotEmpty().WithMessage("Image no puede estar vacio o contener mas de 50 caracteres.");
            RuleFor(m => m.Price)       .GreaterThan(-1).LessThan(int.MaxValue).NotNull().WithMessage($"Precio debe estar entre 0 y {int.MaxValue}");
        }
    }
}
