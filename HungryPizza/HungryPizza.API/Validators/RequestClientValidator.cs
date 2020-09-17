using FluentValidation;
using HungryPizza.API.VO;

namespace HungryPizza.API.Validators
{
    public class RequestClientValidator : AbstractValidator<RequestClient>
    {
        public RequestClientValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Favor informar endereço.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Favor informar a cidade.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Favor informar e-mail.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Favor informar nome do cliente.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Favor informar telefone.");
            RuleFor(x => x.State).NotEmpty().WithMessage("Favor informar estado.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Favor informar cep.");
        }
    }
}
