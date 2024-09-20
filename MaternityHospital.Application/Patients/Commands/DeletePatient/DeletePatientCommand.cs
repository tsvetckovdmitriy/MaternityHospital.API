using MaternityHospital.Application.Exceptions;
using MaternityHospital.Application.Interfaces;
using MaternityHospital.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Application.Patients.Commands.DeletePatient;

public class DeletePatientCommand : IRequest
{
	public Guid Id { get; set; }
}

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
{
	private readonly IApplicationDbContext _context;

	public DeletePatientCommandHandler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Patients
			.FirstOrDefaultAsync(p => p.Name.Id == request.Id, cancellationToken)
			??	throw new NotFoundException(nameof(Patient), request.Id);

		_context.Patients.Remove(entity);

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
