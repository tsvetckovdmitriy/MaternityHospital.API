using MaternityHospital.Domain.Enums;

namespace MaternityHospital.Domain.Entities;

public class Patient
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public long Id { get; set; } = 0;

	/// <summary>
	/// Объект, содержащий информацию о имени ребёнка
	/// </summary>
	public Name Name { get; set; } = new Name();

	/// <summary>
	/// Пол
	/// </summary>
	public GenderType? Gender { get; set; }

	/// <summary>
	/// Дата рождения
	/// </summary>
	public DateTimeOffset BirthDate { get; set; }

	/// <summary>
	/// Статус активности
	/// </summary>
	public bool? Active { get; set; }
}
