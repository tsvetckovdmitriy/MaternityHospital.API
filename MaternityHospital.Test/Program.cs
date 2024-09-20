using System.Net.Http.Json;
using System.Text.Json;

using MaternityHospital.Test;

var maleLastNames = new List<string>
{
	"Смирнов", "Иванов", "Кузнецов", "Попов", "Соколов", "Морозов", "Лебедев",
	"Ковалев", "Николаев", "Зайцев", "Федоров", "Соловьев", "Борисов", "Васильев",
	"Алексеев", "Киселев", "Дмитриев", "Степанов", "Медведев", "Грибанов"
};

var femaleLastNames = new List<string>
{
	"Смирнова", "Иванова", "Кузнецова", "Попова", "Соколова", "Морозова", "Лебедева",
	"Ковалева", "Николаева", "Зайцева", "Федорова", "Соловьева", "Борисова",
	"Васильева", "Алексеевна", "Киселева", "Дмитриева", "Степанова", "Медведева",
	"Грибанова"
};

var maleFirstNames = new List<string>
{
	"Иван", "Петр", "Сергей", "Александр", "Дмитрий", "Максим", "Андрей", "Николай",
	"Виктор", "Роман", "Денис", "Анатолий", "Станислав", "Валентин", "Артем", "Михаил",
	"Григорий", "Егор", "Алексей", "Владимир", "Константин", "Юрий", "Илья",
	"Станислав", "Вячеслав", "Денис", "Роман", "Павел", "Александр", "Никита",
	"Арсений", "Глеб"
};

var femaleFirstNames = new List<string>
{
	"Анна", "Екатерина", "Мария", "Ольга", "Татьяна", "Елена", "Светлана", "Наталья",
	"Анастасия", "Ирина", "Ксения", "Дарья", "Юлия", "Алёна", "Валерия", "Полина",
	"Кристина", "София", "Мила", "Ева", "Татьяна", "Вероника", "Маргарита",
	"Лилия", "Алина", "Света", "Виктория", "Диана", "Елизавета", "Сарра",
	"Кира", "Зоя"
};

var maleMiddleNames = new List<string>
{
	"Иванович", "Петрович", "Сидорович", "Алексеевич", "Дмитриевич", "Максимович",
	"Андреевич", "Викторович", "Романович", "Денисович", "Анатолиевич",
	"Станиславович", "Валентинович", "Артемович", "Михайлович", "Григорьевич",
	"Егорович", "Александрович", "Федорович", "Николаевич", "Сергеевич"
};

var femaleMiddleNames = new List<string>
{
	"Ивановна", "Петровна", "Сидоровна", "Алексеевна", "Дмитриевна", "Максимовна",
	"Андреевна", "Викторовна", "Романовна", "Денисовна", "Анатолиевна",
	"Станиславовна", "Валентиновна", "Артемовна", "Михайловна", "Григорьевна",
	"Егоровна", "Александровна", "Федоровна", "Николаевна", "Сергеевна"
};

Console.WriteLine("Добавления через API 100 сгенерированных сущностей Patient");

using HttpClient httpClient = new HttpClient();
var random = new Random();

for (int i = 0; i < 100; i++)
{
	var gender = GetRandomGender(random);

	string lastName = string.Empty;
	string firstName = string.Empty;
	string middleName = string.Empty;

	if (gender == GenderType.Female)
	{
		lastName = femaleLastNames[random.Next(femaleLastNames.Count)];
		firstName = femaleFirstNames[random.Next(femaleFirstNames.Count)];
		middleName = femaleMiddleNames[random.Next(femaleMiddleNames.Count)];
	}
	else
	{
		lastName = maleLastNames[random.Next(maleLastNames.Count)];
		firstName = maleFirstNames[random.Next(maleFirstNames.Count)];
		middleName = maleMiddleNames[random.Next(maleMiddleNames.Count)];
	}

	var patient = new PatientDto
	{
		Name = new NameDto
		{
			Id = Guid.NewGuid(),
			Use = "official",
			Family = lastName,
			Given = new List<string> { firstName, middleName }
		},
		Gender = gender.ToString(),
		BirthDate = DateTime.Now.AddDays(random.Next(-365, 0)),
		Active = random.Next(2) == 1,
	};

	using var response = await httpClient.PostAsync("https://localhost:8081/api/patients", JsonContent.Create(patient));
	response.EnsureSuccessStatusCode();

	Console.WriteLine($"Добавленна сущность: {JsonSerializer.Serialize(patient)}");
}

Console.WriteLine($"Все записи успешно добавлены");
Console.ReadLine();

GenderType GetRandomGender(Random random)
{
	int randomGender = random.Next(30);
	return randomGender switch
	{
		0 => GenderType.Unknown,
		< 5 => GenderType.Other,
		< 17 => GenderType.Female,
		_ => GenderType.Male,
	};
}