using System;
namespace CarServiceAPI.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int CustomerId { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<RepairDTO> Repairs { get; set; }
    }
}

