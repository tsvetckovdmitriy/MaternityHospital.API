using MaternityHospital.Application.Exceptions;
using MaternityHospital.Application.Interfaces;
using MaternityHospital.Domain.Entities;
using MaternityHospital.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Application.Patients.Commands.UpdatePatient;

public class UpdatePatientCommand : IRequest
{
	public NameDto Name { get; set; } = new NameDto();
	public string? Gender { get; set; }
	public DateTime BirthDate { get; set; }
	public bool? Active { get; set; }
}

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
{
	private readonly IApplicationDbContext _context;

	public UpdatePatientCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
	{
		Enum.TryParse(request.Gender, true, out GenderType genderType);

		var entity = await _context.Patients
			.Include(x => x.Name)
			.FirstOrDefaultAsync(p => p.Name.Id == request.Name.Id, cancellationToken)
				?? throw new NotFoundException(nameof(Patient), request.Name.Id);

		entity.Name.Use = request.Name.Use;
		entity.Name.Family = request.Name.Family;
		entity.Name.Given = request.Name.Given;
		entity.Gender = genderType;
		entity.BirthDate = request.BirthDate;
		entity.Active = request.Active;

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}