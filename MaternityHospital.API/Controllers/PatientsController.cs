using MaternityHospital.Application.Patients;
using MaternityHospital.Application.Patients.Commands.CreatePatient;
using MaternityHospital.Application.Patients.Commands.DeletePatient;
using MaternityHospital.Application.Patients.Commands.UpdatePatient;
using MaternityHospital.Application.Patients.Queries.GetPatient;
using MaternityHospital.Application.Patients.Queries.GetPatients;

using Microsoft.AspNetCore.Mvc;

namespace MaternityHospital.API.Controllers;

/// <summary>
/// API ��� �������� Patient
/// </summary>
[Produces("application/json")]
public class PatientsController : ApiControllerBase
{
	/// <summary>
	/// ��������� ������ ������� � �����
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
	/// ��������� ������ � ������ �� ��������������
	/// </summary>
	/// <param name="id">������������� ������</param>
	/// <response code="404">������ �� �������</response>
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<PatientDto>> Get(Guid id)
	{
		return await Mediator.Send(new GetPatientQuery { Id = id });
	}

	/// <summary>
	/// ���������� ������ � ������
	/// </summary>
	/// <remarks>
	/// ������ �������:
	///
	///     POST /api/patients
	///     {
	///         "Name":
	///         {
	///              "Id":"9ca7c58a-2781-499d-b4b8-23369d4d32ec",
	///              "Use":"official",
	///              "Family":"������",
	///              "Given":["����","��������"]
	///         },
	///         "Gender":"Male",
	///         "BirthDate":"2024-04-05T00:35:50.2060701+03:00",
	///         "Active":true
	///     }
	///
	/// </remarks>
	/// <response code="201">������������� �������</response>
	/// <response code="400">������������ ������</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<Guid>> Create(CreatePatientCommand command)
	{
		return await Mediator.Send(command);
	}

	/// <summary>
	/// ���������� ������ � ������
	/// </summary>
	/// <remarks>
	/// ������ �������:
	///
	///     PUT /api/patients/9ca7c58a-2781-499d-b4b8-23369d4d32ec
	///     {
	///         "Name":
	///         {
	///              "Id":"9ca7c58a-2781-499d-b4b8-23369d4d32ec",
	///              "Use":"official",
	///              "Family":"������",
	///              "Given":["����","��������"]
	///         },
	///         "Gender":"Male",
	///         "BirthDate":"2024-04-05T00:35:50.2060701+03:00",
	///         "Active":true
	///     }
	///
	/// </remarks>
	/// <param name="id">������������� ������</param>
	/// <response code="204">������ ������� ���������</response>
	/// <response code="404">������ �� �������</response>
	/// <response code="400">������������ ������</response>
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
	/// �������� ������ � ������
	/// </summary>
	/// <param name="id">������������� ������</param>
	/// <response code="204">������ ������� ��������</response>
	/// <response code="404">������ �� �������</response>
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(Guid id)
	{
		await Mediator.Send(new DeletePatientCommand { Id = id });

		return NoContent();
	}
}
