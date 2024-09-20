namespace MaternityHospital.Test;

public class PatientDto
{
	public NameDto Name { get; set; } = new NameDto();
	public string? Gender { get; set; }
	public DateTime BirthDate { get; set; }
	public bool? Active { get; set; }
}
