using CarServiceAPI.Models;
using CarServiceAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CustomerService
{
    private readonly CarServiceDbContext _context;

    public CustomerService(CarServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerDTO>> GetCustomersAsync()
    {
        return await _context.Customers
            .Include(c => c.Cars)
            .ThenInclude(car => car.Repairs)
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Cars = c.Cars.Select(car => new CarDTO
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    CustomerId = car.CustomerId,
                    Repairs = car.Repairs.Select(r => new RepairDTO
                    {
                        Id = r.Id,
                        Description = r.Description,
                        Date = r.Date,
                        CarId = r.CarId
                    }).ToList()
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Cars)
            .ThenInclude(car => car.Repairs)
            .Where(c => c.Id == id)
            .Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Cars = c.Cars.Select(car => new CarDTO
                {
                    Id = car.Id,
                    Make = car.Make,
                    Model = car.Model,
                    Year = car.Year,
                    CustomerId = car.CustomerId,
                    Repairs = car.Repairs.Select(r => new RepairDTO
                    {
                        Id = r.Id,
                        Description = r.Description,
                        Date = r.Date,
                        CarId = r.CarId
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Customer> AddCustomerAsync(CreateCustomerDTO customerDTO)
    {
        var customer = new Customer
        {
            Name = customerDTO.Name,
            Email = customerDTO.Email
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(int id, UpdateCustomerDTO customerDTO)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        customer.Name = customerDTO.Name;
        customer.Email = customerDTO.Email;

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
