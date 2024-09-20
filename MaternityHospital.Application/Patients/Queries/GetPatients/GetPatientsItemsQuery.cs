using AutoMapper;

using MaternityHospital.Application.Interfaces;
using MaternityHospital.Application.Mappings;
using MaternityHospital.Domain.Enums;
using MaternityHospital.Domain.Extensions;

using MediatR;

namespace MaternityHospital.Application.Patients.Queries.GetPatients;

public class GetPatientsItemsQuery : IRequest<List<PatientDto>>
{
	public string? BirthDate { get; set; }
	public string? Gender { get; set; }
	public bool? Active { get; set; }
}

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsItemsQuery, List<PatientDto>>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetPatientsQueryHandler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<List<PatientDto>> Handle(GetPatientsItemsQuery request, CancellationToken cancellationToken)
	{
		var query = _context.Patients.AsQueryable();
		if (!string.IsNullOrEmpty(request.BirthDate))
		{
			// не равно
			if (request.BirthDate.StartsWith("ne"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate != date);
			}
			// меньше
			else if (request.BirthDate.StartsWith("lt"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate < date);
			}
			// меньше или равно
			else if (request.BirthDate.StartsWith("gt"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate > date);
			}
			// больше
			else if (request.BirthDate.StartsWith("ge"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate >= date);
			}
			// больше или равно
			else if (request.BirthDate.StartsWith("le"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate <= date);
			}
			// начинается после
			else if (request.BirthDate.StartsWith("sa"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate.Date == date.Date);
			}
			// заканчивается до
			else if (request.BirthDate.StartsWith("eb"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate.Date > date.Date);
			}
			else if (request.BirthDate.StartsWith("ap"))
			{
				var date = request.BirthDate.Substring(2).ParseDate();
				query = query.Where(r => r.BirthDate.Date < date.Date);
			}
			// равно (по умолчанию)
			else
			{
				query = query.Where(r => r.BirthDate == request.BirthDate.ParseDate());
			}
		}

		if (Enum.TryParse(request.Gender, true, out GenderType genderType))
		{
			query = query.Where(r => r.Gender == genderType);
		}

		if (request.Active.HasValue)
		{
			query = query.Where(r => r.Active == request.Active);
		}

		return await query
			.ProjectToListAsync<PatientDto>(_mapper.ConfigurationProvider);
	}
}
