using System;
namespace CarServiceAPI.DTOs
{
    public class RepairDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CarId { get; set; }
        public CarDTO Car { get; set; }
    }
}

