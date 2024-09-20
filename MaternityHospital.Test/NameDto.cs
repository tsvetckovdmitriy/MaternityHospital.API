namespace MaternityHospital.Test;

public class NameDto
{
	public Guid Id { get; set; } = new Guid();
	public string? Use { get; set; }
	public string Family { get; set; } = "";
	public List<string>? Given { get; set; }
}
