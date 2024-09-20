using FluentValidation;

using MaternityHospital.Application.Patients.Commands.UpdatePatient;

namespace MaternityHospital.Application.Patients.Commands.CreatePatient;

public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
{
	public UpdatePatientCommandValidator()
	{
		RuleFor(v => v.Name.Family)
			.NotEmpty().WithMessage("Family обязательное поле.");

		RuleFor(v => v.BirthDate)
			.NotEmpty().WithMessage("BirthDate обязательное поле.");
	}
}