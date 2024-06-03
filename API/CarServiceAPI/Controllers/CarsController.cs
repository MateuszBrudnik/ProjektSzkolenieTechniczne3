using Microsoft.AspNetCore.Mvc;
using CarServiceAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Kontroler zarządzający samochodami
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly CarService _carService;

    public CarsController(CarService carService)
    {
        _carService = carService;
    }

    /// <summary>
    /// Pobiera listę wszystkich samochodów
    /// </summary>
    /// <returns>Lista samochodów</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDTO>>> GetCars()
    {
        return await _carService.GetCarsAsync();
    }

    /// <summary>
    /// Pobiera szczegóły samochodu po identyfikatorze
    /// </summary>
    /// <param name="id">Identyfikator samochodu</param>
    /// <returns>Szczegóły samochodu</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CarDTO>> GetCar(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);

        if (car == null)
        {
            return NotFound();
        }

        return car;
    }

    /// <summary>
    /// Dodaje nowy samochód
    /// </summary>
    /// <param name="carDTO">Dane samochodu</param>
    /// <returns>Dodany samochód</returns>
    [HttpPost]
    public async Task<ActionResult<CarDTO>> PostCar(CreateCarDTO carDTO)
    {
        var car = await _carService.AddCarAsync(carDTO);
        var carResponse = new CarDTO
        {
            Id = car.Id,
            Make = car.Make,
            Model = car.Model,
            Year = car.Year,
            CustomerId = car.CustomerId
        };

        return CreatedAtAction("GetCar", new { id = car.Id }, carResponse);
    }

    /// <summary>
    /// Aktualizuje dane samochodu
    /// </summary>
    /// <param name="id">Identyfikator samochodu</param>
    /// <param name="carDTO">Dane samochodu</param>
    /// <returns>Brak zawartości</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCar(int id, UpdateCarDTO carDTO)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _carService.UpdateCarAsync(id, carDTO);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Usuwa samochód
    /// </summary>
    /// <param name="id">Identyfikator samochodu</param>
    /// <returns>Brak zawartości</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCar(int id)
    {
        var result = await _carService.DeleteCarAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
