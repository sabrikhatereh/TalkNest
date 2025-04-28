using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using MediatR.Pipeline;
using ValidationException = TalkNest.Application.Exceptions.ValidationException;
using TalkNest.Core.Shared.Result;

namespace TalkNest.Application.Behaviors
{
    public class RequestValidationBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var _validator = new ValidationContext<TRequest>(request);

            if (_validator is null)
                return;

            var validationResult = await Task.WhenAll(_validators
                .Select(v => v.ValidateAsync(_validator, cancellationToken)));


            var failures = validationResult
             .SelectMany(result => result.Errors)
             .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(new ValidationResultModel(validationResult));

        }
    }

}