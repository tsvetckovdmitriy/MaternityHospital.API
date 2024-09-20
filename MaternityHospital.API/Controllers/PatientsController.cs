using MaternityHospital.Application.Patients;
using MaternityHospital.Application.Patients.Commands.CreatePatient;
using MaternityHospital.Application.Patients.Commands.DeletePatient;
using MaternityHospital.Application.Patients.Commands.UpdatePatient;
using MaternityHospital.Application.Patients.Queries.GetPatient;
using MaternityHospital.Application.Patients.Queries.GetPatients;

using Microsoft.AspNetCore.Mvc;

namespace MaternityHospital.API.Controllers;

/// <summary>
/// API для сущности Patient
/// </summary>
[Produces("application/json")]
public class PatientsController : ApiControllerBase
{
	/// <summary>
	/// Получение списка записей о детях
	/// </summary>
	/// <param name="query"></param>
	/// <returns></returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<List<PatientDto>>> GetItems([FromQuery] GetPatientsItemsQuery query)
	{
		return await Mediator.Send(query);
	}

	/// <summary>
	/// Получение записи о ребёнке по идентификатору
	/// </summary>
	/// <param name="id">Идентификатор ребёнка</param>
	/// <response code="404">Запись не найдена</response>
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<PatientDto>> Get(Guid id)
	{
		return await Mediator.Send(new GetPatientQuery { Id = id });
	}

	/// <summary>
	/// Добавление записи о ребёнке
	/// </summary>
	/// <remarks>
	/// Пример запроса:
	///
	///     POST /api/patients
	///     {
	///         "Name":
	///         {
	///              "Id":"9ca7c58a-2781-499d-b4b8-23369d4d32ec",
	///              "Use":"official",
	///              "Family":"Иванов",
	///              "Given":["Иван","Иванович"]
	///         },
	///         "Gender":"Male",
	///         "BirthDate":"2024-04-05T00:35:50.2060701+03:00",
	///         "Active":true
	///     }
	///
	/// </remarks>
	/// <response code="201">Идентификатор ребёнкаа</response>
	/// <response code="400">Некорректные данные</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<Guid>> Create(CreatePatientCommand command)
	{
		return await Mediator.Send(command);
	}

	/// <summary>
	/// Обновление записи о ребёнке
	/// </summary>
	/// <remarks>
	/// Пример запроса:
	///
	///     PUT /api/patients/9ca7c58a-2781-499d-b4b8-23369d4d32ec
	///     {
	///         "Name":
	///         {
	///              "Id":"9ca7c58a-2781-499d-b4b8-23369d4d32ec",
	///              "Use":"official",
	///              "Family":"Иванов",
	///              "Given":["Иван","Иванович"]
	///         },
	///         "Gender":"Male",
	///         "BirthDate":"2024-04-05T00:35:50.2060701+03:00",
	///         "Active":true
	///     }
	///
	/// </remarks>
	/// <param name="id">Идентификатор ребёнка</param>
	/// <response code="204">Запись успешно обновлена</response>
	/// <response code="404">Запись не найдена</response>
	/// <response code="400">Некорректные данные</response>
	[HttpPut("{id}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult> Update(Guid id, UpdatePatientCommand command)
	{
		if (id != command.Name.Id)
		{
			return BadRequest();
		}

		await Mediator.Send(command);

		return NoContent();
	}

	/// <summary>
	/// Удаление записи о ребёнке
	/// </summary>
	/// <param name="id">Идентификатор ребёнка</param>
	/// <response code="204">Запись успешно удаленна</response>
	/// <response code="404">Запись не найдена</response>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await Mediator.Send(new DeletePatientCommand { Id = id });

		return NoContent();
	}
}
