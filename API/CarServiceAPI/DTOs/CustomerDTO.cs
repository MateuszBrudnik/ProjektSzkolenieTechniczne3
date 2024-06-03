﻿using System;
namespace CarServiceAPI.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<CarDTO> Cars { get; set; }
    }
}

