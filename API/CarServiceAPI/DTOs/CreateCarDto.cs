using System;
namespace CarServiceAPI.DTOs
{
    public class CreateCarDTO
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int CustomerId { get; set; }
    }
}

