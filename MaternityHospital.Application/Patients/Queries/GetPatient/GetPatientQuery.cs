using AutoMapper;

using MaternityHospital.Application.Exceptions;
using MaternityHospital.Application.Interfaces;
using MaternityHospital.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Application.Patients.Queries.GetPatient;

public class GetPatientQuery : IRequest<PatientDto>
{
	public Guid Id { get; set; }
}

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, PatientDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetPatientQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<PatientDto> Handle(GetPatientQuery request, CancellationToken cancellationToken)
	{
		var entity = await _context.Patients
			.Include(x => x.Name)
			.FirstOrDefaultAsync(p => p.Name.Id == request.Id, cancellationToken)
				?? throw new NotFoundException(nameof(Patient), request.Id);

		return _mapper.Map<PatientDto>(entity);
	}
}
