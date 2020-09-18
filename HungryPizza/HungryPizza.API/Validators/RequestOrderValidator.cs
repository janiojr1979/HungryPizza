using FluentValidation;
using HungryPizza.API.VO;
using HungryPizza.Infra.CrossCutting.Tools;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace HungryPizza.API.Validators
{
    public class RequestOrderValidator : AbstractValidator<RequestOrder>
    {
        public RequestOrderValidator()
        {
            RuleFor(x => x).Must(x=> x.ClientId.HasValue).WithMessage("Preenche com o código do cliente").When(x=> !(x.Address.HasValue() && x.City.HasValue() && x.Email.HasValue() && x.State.HasValue() && x.ZipCode.HasValue())).WithName("ClientId");
            RuleFor(x => x.Items).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("O pedido deve conter ao menos uma pizza.").Must(x => x.Count() <= 10).WithMessage("O pedido deve conter no máximo de 10 pizzas.");
        }
    }
}
