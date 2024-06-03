using BuildingBlocks.CQRS;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBahavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : ICommand<TResponse>
        //This validation beh only applicable to CRUD operation ie command req only bcz,
        //this line -> where TRequest : ICommand<TResponse>
        //bcz TRquest should be ICommand type only...
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = 
                validationResult
                .Where( r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any()) throw new ValidationException(failures);

            return await next();
        }
    }
}
