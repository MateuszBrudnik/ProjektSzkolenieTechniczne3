using CarServiceAPI.Models;
using CarServiceAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CarService
{
    private readonly CarServiceDbContext _context;

    public CarService(CarServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<CarDTO>> GetCarsAsync()
    {
        return await _context.Cars
            .Include(c => c.Customer)
            .Include(c => c.Repairs)
            .Select(c => new CarDTO
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                CustomerId = c.CustomerId,
                Customer = new CustomerDTO
                {
                    Id = c.Customer.Id,
                    Name = c.Customer.Name,
                    Email = c.Customer.Email
                },
                Repairs = c.Repairs.Select(r => new RepairDTO
                {
                    Id = r.Id,
                    Description = r.Description,
                    Date = r.Date,
                    CarId = r.CarId
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<CarDTO> GetCarByIdAsync(int id)
    {
        return await _context.Cars
            .Include(c => c.Customer)
            .Include(c => c.Repairs)
            .Where(c => c.Id == id)
            .Select(c => new CarDTO
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                CustomerId = c.CustomerId,
                Customer = new CustomerDTO
                {
                    Id = c.Customer.Id,
                    Name = c.Customer.Name,
                    Email = c.Customer.Email
                },
                Repairs = c.Repairs.Select(r => new RepairDTO
                {
                    Id = r.Id,
                    Description = r.Description,
                    Date = r.Date,
                    CarId = r.CarId
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Car> AddCarAsync(CreateCarDTO carDTO)
    {
        var car = new Car
        {
            Make = carDTO.Make,
            Model = carDTO.Model,
            Year = carDTO.Year,
            CustomerId = carDTO.CustomerId
        };

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        return car;
    }

    public async Task<bool> UpdateCarAsync(int id, UpdateCarDTO carDTO)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return false;

        car.Make = carDTO.Make;
        car.Model = carDTO.Model;
        car.Year = carDTO.Year;
        car.CustomerId = carDTO.CustomerId;

        _context.Entry(car).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return false;

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        return true;
    }
}
