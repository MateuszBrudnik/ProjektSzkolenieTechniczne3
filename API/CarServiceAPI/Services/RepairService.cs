using CarServiceAPI.Models;
using CarServiceAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RepairService
{
    private readonly CarServiceDbContext _context;

    public RepairService(CarServiceDbContext context)
    {
        _context = context;
    }

    public async Task<List<RepairDTO>> GetRepairsAsync()
    {
        return await _context.Repairs
            .Include(r => r.Car)
            .ThenInclude(c => c.Customer)
            .Select(r => new RepairDTO
            {
                Id = r.Id,
                Description = r.Description,
                Date = r.Date,
                CarId = r.CarId,
                Car = new CarDTO
                {
                    Id = r.Car.Id,
                    Make = r.Car.Make,
                    Model = r.Car.Model,
                    Year = r.Car.Year,
                    CustomerId = r.Car.CustomerId,
                    Customer = new CustomerDTO
                    {
                        Id = r.Car.Customer.Id,
                        Name = r.Car.Customer.Name,
                        Email = r.Car.Customer.Email
                    }
                }
            })
            .ToListAsync();
    }

    public async Task<RepairDTO> GetRepairByIdAsync(int id)
    {
        return await _context.Repairs
            .Include(r => r.Car)
            .ThenInclude(c => c.Customer)
            .Where(r => r.Id == id)
            .Select(r => new RepairDTO
            {
                Id = r.Id,
                Description = r.Description,
                Date = r.Date,
                CarId = r.CarId,
                Car = new CarDTO
                {
                    Id = r.Car.Id,
                    Make = r.Car.Make,
                    Model = r.Car.Model,
                    Year = r.Car.Year,
                    CustomerId = r.Car.CustomerId,
                    Customer = new CustomerDTO
                    {
                        Id = r.Car.Customer.Id,
                        Name = r.Car.Customer.Name,
                        Email = r.Car.Customer.Email
                    }
                }
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Repair> AddRepairAsync(CreateRepairDTO repairDTO)
    {
        var repair = new Repair
        {
            Description = repairDTO.Description,
            Date = repairDTO.Date,
            CarId = repairDTO.CarId
        };

        _context.Repairs.Add(repair);
        await _context.SaveChangesAsync();
        return repair;
    }

    public async Task<bool> UpdateRepairAsync(int id, UpdateRepairDTO repairDTO)
    {
        var repair = await _context.Repairs.FindAsync(id);
        if (repair == null) return false;

        repair.Description = repairDTO.Description;
        repair.Date = repairDTO.Date;
        repair.CarId = repairDTO.CarId;

        _context.Entry(repair).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRepairAsync(int id)
    {
        var repair = await _context.Repairs.FindAsync(id);
        if (repair == null) return false;

        _context.Repairs.Remove(repair);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CarDTO> GetCarByIdAsync(int carId)
    {
        return await _context.Cars
            .Include(c => c.Customer)
            .Where(c => c.Id == carId)
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
                }
            })
            .FirstOrDefaultAsync();
    }

    private bool RepairExists(int id)
    {
        return _context.Repairs.Any(e => e.Id == id);
    }
}
