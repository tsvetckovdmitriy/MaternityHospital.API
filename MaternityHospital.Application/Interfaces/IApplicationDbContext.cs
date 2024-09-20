using MaternityHospital.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Application.Interfaces;

public interface IApplicationDbContext
{
	DbSet<Patient> Patients { get; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
