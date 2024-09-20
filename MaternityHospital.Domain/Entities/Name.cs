namespace MaternityHospital.Domain.Entities;

public class Name
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid Id { get; set; } = new Guid();

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
