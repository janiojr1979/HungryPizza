using FluentValidation;
using HungryPizza.API.VO;
using System.Linq;

namespace HungryPizza.API.Validators
{
    public class RequestOrderValidator : AbstractValidator<RequestOrder>
    {
        public RequestOrderValidator()
        {
            RuleFor(x => x.Items).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("O pedido deve conter ao menos uma pizza.").Must(x => x.Count() <= 10).WithMessage("O pedido deve conter no máximo de 10 pizzas.");
        }
    }
}
