namespace MaternityHospital.Domain.Extensions;

public static class DateTimeExtension
{
	public static DateTime ParseDate(this string dateString)
	{
		if (DateTime.TryParse(dateString, out var date))
			return date;

		throw new FormatException("Invalid date format.");
	}
}
