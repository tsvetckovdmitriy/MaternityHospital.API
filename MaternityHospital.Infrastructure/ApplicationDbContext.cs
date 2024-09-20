using System.Reflection;

using MaternityHospital.Application.Interfaces;
using MaternityHospital.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace MaternityHospital.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
		: base(options)
	{
		Database.EnsureCreated();
	}

	public DbSet<Patient> Patients => Set<Patient>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		base.OnModelCreating(builder);
	}
}
