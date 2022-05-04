using FluentValidation;

namespace WebApi.Application.OrderOperations.Queries.GetOrderByCustomerIdDetail
{
    public class GetOrderByCustomerIdQueryValidator : AbstractValidator<GetOrderByCustomerIdQuery>
    {
        public GetOrderByCustomerIdQueryValidator()
        {
            RuleFor(query => query.CustomerId).GreaterThan(0);
            
        }
    }
}