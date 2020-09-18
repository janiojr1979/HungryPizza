using FluentValidation;
using HungryPizza.API.VO;

namespace HungryPizza.API.Validators
{
    public class RequestPizzaValidator : AbstractValidator<RequestPizza>
    {
        public RequestPizzaValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Favor informar nome.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Favor informar preço.");
        }
    }
}
