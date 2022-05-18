using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.CustomerOperations.Queries.GetCustomerDetail
{
    public class GetCustomerDetailQueryValidator  : AbstractValidator<GetCustomerDetailQuery>
    {
         public GetCustomerDetailQueryValidator()
         {
            RuleFor(query => query.CustomerId).GreaterThan(0);
         }
    }
}