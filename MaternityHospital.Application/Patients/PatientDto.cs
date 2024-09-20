using MaternityHospital.Application.Mappings;
using MaternityHospital.Domain.Entities;

namespace MaternityHospital.Application.Patients;

public class PatientDto : IMapFrom<Patient>
{
	/// <summary>
	/// Объект, содержащий информацию о имени ребёнка
	/// </summary>
	public NameDto Name { get; set; } = new NameDto();

	/// <summary>
	/// Пол
	/// </summary>
	public string? Gender { get; set; }

	/// <summary>
	/// Дата рождения
	/// </summary>
	public DateTimeOffset BirthDate { get; set; }

	/// <summary>
	/// Статус активности
	/// </summary>
	public bool? Active { get; set; }
}
