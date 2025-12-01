using FluentValidation;
using PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.DTOs;

namespace PodstawyProgramowaniaObiektowego.Modules.MonteCarloPiCalculator.Validators
{
    public class SimulationRequestValidator : AbstractValidator<SimulationRequest>
    {
        public SimulationRequestValidator()
        {
            RuleFor(x => x.Points)
                .GreaterThan(0)
                .WithMessage("Liczba punktów musi być większa od 0.")
                .LessThanOrEqualTo(1_000_000_000)
                .WithMessage("Przesadziłeś! Maksymalna liczba punktów to 1 miliard.");
        }
    }
}