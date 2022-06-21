using Application.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepositoryAsync productRepository;

        public CreateProductCommandValidator(IProductRepositoryAsync productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Price)
             .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 100 characters.").MustAsync(IsUniqueName).WithMessage("{PropertyName} already exists.");

        }

        private async Task<bool> IsUniqueName(string name, CancellationToken cancellationToken)
        {
            return await productRepository.IsUniqueNameAsync(name);
        }

        //private async Task<bool> IsUniqueBarcode(string barcode, CancellationToken cancellationToken)
        //{
        //   // return await productRepository.IsUniqueBarcodeAsync(barcode);
        //}
    }
}
