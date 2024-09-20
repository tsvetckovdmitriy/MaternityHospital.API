using MaternityHospital.Application.Mappings;
using MaternityHospital.Domain.Entities;

namespace MaternityHospital.Application.Patients;

public class NameDto : IMapFrom<Name>
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid? Id { get; set; }

	/// <summary>
	/// Дополнительная информация
	/// </summary>
	public string? Use { get; set; }

	/// <summary>
	/// Фамилия
	/// </summary>
	public string Family { get; set; } = "";

	/// <summary>
	/// Список имен
	/// </summary>
	public List<string>? Given { get; set; }
}
