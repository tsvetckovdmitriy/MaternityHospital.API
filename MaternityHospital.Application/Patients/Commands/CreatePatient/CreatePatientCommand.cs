using MaternityHospital.Application.Interfaces;
using MaternityHospital.Domain.Entities;
using MaternityHospital.Domain.Enums;

using MediatR;

namespace MaternityHospital.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommand : IRequest<Guid>
{
	public NameDto Name { get; set; } = new NameDto();
	public string? Gender { get; set; }
	public DateTime BirthDate { get; set; }
	public bool? Active { get; set; }
}

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
{
	private readonly IApplicationDbContext _context;

	public CreatePatientCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
	{
		Enum.TryParse(request.Gender, true, out GenderType genderType);

		var entity = new Patient
		{
			Name = new Name
			{
				Id = request.Name.Id ?? Guid.NewGuid(),
				Use = request.Name.Use,
				Family = request.Name.Family,
				Given = request.Name.Given
			},
			Gender = genderType,
			BirthDate = request.BirthDate,
			Active = request.Active
		};

		_context.Patients.Add(entity);

		await _context.SaveChangesAsync(cancellationToken);

		return entity.Name.Id;
	}
}