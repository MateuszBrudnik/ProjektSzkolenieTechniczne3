using System;
namespace CarServiceAPI.DTOs
{
    public class CreateRepairDTO
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CarId { get; set; }
    }
}

