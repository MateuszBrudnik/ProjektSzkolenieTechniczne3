using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CarServiceAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

/// <summary>
/// Kontroler zarządzający naprawami
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RepairsController : ControllerBase
{
    private readonly RepairService _repairService;
    private readonly AzureBlobService _blobService;

    public RepairsController(RepairService repairService, AzureBlobService blobService)
    {
        _repairService = repairService;

        _blobService = blobService;
    }

    /// <summary>
    /// Pobiera listę wszystkich napraw
    /// </summary>
    /// <returns>Lista napraw</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RepairDTO>>> GetRepairs()
    {
        return await _repairService.GetRepairsAsync();
    }

    /// <summary>
    /// Pobiera szczegóły naprawy po identyfikatorze
    /// </summary>
    /// <param name="id">Identyfikator naprawy</param>
    /// <returns>Szczegóły naprawy</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RepairDTO>> GetRepair(int id)
    {
        var repair = await _repairService.GetRepairByIdAsync(id);

        if (repair == null)
        {
            return NotFound();
        }

        return repair;
    }

    /// <summary>
    /// Dodaje nową naprawę
    /// </summary>
    /// <param name="repairDTO">Dane naprawy</param>
    /// <returns>Dodana naprawa</returns>
    [HttpPost]
    public async Task<ActionResult<RepairDTO>> PostRepair(CreateRepairDTO repairDTO)
    {
        var repair = await _repairService.AddRepairAsync(repairDTO);
        var car = await _repairService.GetCarByIdAsync(repair.CarId);

        return CreatedAtAction("GetRepair", new { id = repair.Id }, new RepairDTO
        {
            Id = repair.Id,
            Description = repair.Description,
            Date = repair.Date,
            CarId = repair.CarId
        });
    }

    /// <summary>
    /// Przesyła plik do naprawy
    /// </summary>
    /// <param name="id">Identyfikator naprawy</param>
    /// <param name="file">Plik do przesłania</param>
    /// <returns>URL przesłanego pliku</returns>
    [HttpPost("{id}/upload")]
    public async Task<IActionResult> UploadFile(int id, IFormFile file)
    {
        var repair = await _repairService.GetRepairByIdAsync(id);

        if (repair == null)
        {
            return NotFound();
        }

        var filePath = Path.GetTempFileName();

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        await using var fileStream = new FileStream(filePath, FileMode.Open);
        var fileUrl = await _blobService.UploadFileAsync(fileStream, file.FileName, "repairs");

        return Ok(new { Url = fileUrl });
    }

    /// <summary>
    /// Aktualizuje dane naprawy
    /// </summary>
    /// <param name="id">Identyfikator naprawy</param>
    /// <param name="repairDTO">Dane naprawy</param>
    /// <returns>Brak zawartości</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRepair(int id, UpdateRepairDTO repairDTO)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _repairService.UpdateRepairAsync(id, repairDTO);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Usuwa naprawę
    /// </summary>
    /// <param name="id">Identyfikator naprawy</param>
    /// <returns>Brak zawartości</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepair(int id)
    {
        var result = await _repairService.DeleteRepairAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
