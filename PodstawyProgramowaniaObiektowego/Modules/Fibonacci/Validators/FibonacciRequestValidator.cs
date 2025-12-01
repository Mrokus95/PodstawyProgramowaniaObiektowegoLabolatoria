using FluentValidation;
using PodstawyProgramowaniaObiektowego.Modules.Fibonacci.DTOs;

namespace PodstawyProgramowaniaObiektowego.Modules.Fibonacci.Validators;

public class FibonacciRequestValidator : AbstractValidator<FibonacciRequest>
{
    public FibonacciRequestValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThan(0).WithMessage("Liczba musi być większa od 0.")
            .LessThanOrEqualTo(30).WithMessage("Maksymalna wartość to 30.");
    }
}