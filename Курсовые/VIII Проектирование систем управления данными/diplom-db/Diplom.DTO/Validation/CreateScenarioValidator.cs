using FluentValidation;

namespace Diplom.DTO.Validation;

internal class CreateScenarioValidator : AbstractValidator<CreateScenarioRequest>
{
    public CreateScenarioValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Название сценария обязательно.")
            .MinimumLength(3).WithMessage("Название должно быть не короче 3 символов.")
            .MaximumLength(50).WithMessage("Название не должно превышать 50 символов.");

        RuleFor(x => x.ActionIds)
            .NotEmpty().WithMessage("Сценарий должен содержать хотя бы одно действие.")
            .Must(x => x != null && x.Count > 0).WithMessage("Список действий не может быть пустым.");
    }
}
