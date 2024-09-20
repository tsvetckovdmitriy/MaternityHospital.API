using FluentValidation;

using MaternityHospital.Application.Interfaces;
using MaternityHospital.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
	private readonly IApplicationDbContext _context;

	public CreatePatientCommandValidator(IApplicationDbContext context)
	{
		_context = context;

		RuleFor(v => v.Name.Id)
			.MustAsync(BeUniqueId).WithMessage("Указанный Id уже существует.");

		RuleFor(v => v.Name.Family)
			.NotEmpty().WithMessage("Family обязательное поле.");

		RuleFor(v => v.Gender)
			.Must(GenderValid).WithMessage("Неизвестное значение Gender.");

		RuleFor(v => v.BirthDate)
			.NotEmpty().WithMessage("BirthDate обязательное поле.");
	}

	public bool GenderValid(string? gender)
	{
		return Enum.TryParse(gender, true, out GenderType genderType);
	}

	public async Task<bool> BeUniqueId(Guid? guid, CancellationToken cancellationToken)
	{
		return await _context.Patients
			.AllAsync(l => l.Name.Id != guid, cancellationToken);
	}
}