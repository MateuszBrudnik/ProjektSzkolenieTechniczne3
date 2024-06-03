using Microsoft.AspNetCore.Mvc;
using CarServiceAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Kontroler zarządzający klientami
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomersController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Pobiera listę wszystkich klientów
    /// </summary>
    /// <returns>Lista klientów</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
    {
        return await _customerService.GetCustomersAsync();
    }

    /// <summary>
    /// Pobiera szczegóły klienta po identyfikatorze
    /// </summary>
    /// <param name="id">Identyfikator klienta</param>
    /// <returns>Szczegóły klienta</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }

    /// <summary>
    /// Dodaje nowego klienta
    /// </summary>
    /// <param name="customerDTO">Dane klienta</param>
    /// <returns>Dodany klient</returns>
    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> PostCustomer(CreateCustomerDTO customerDTO)
    {
        var customer = await _customerService.AddCustomerAsync(customerDTO);
        var customerResponse = new CustomerDTO
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email
        };

        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customerResponse);
    }

    /// <summary>
    /// Aktualizuje dane klienta
    /// </summary>
    /// <param name="id">Identyfikator klienta</param>
    /// <param name="customerDTO">Dane klienta</param>
    /// <returns>Brak zawartości</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, UpdateCustomerDTO customerDTO)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var result = await _customerService.UpdateCustomerAsync(id, customerDTO);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Usuwa klienta
    /// </summary>
    /// <param name="id">Identyfikator klienta</param>
    /// <returns>Brak zawartości</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
