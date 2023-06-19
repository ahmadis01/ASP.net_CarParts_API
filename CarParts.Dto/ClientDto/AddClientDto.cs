﻿using CarParts.Models.Main;

namespace CarParts.Dto.ClientDto
{
    public class AddClientDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsSeller { get; set; }
        public string PhoneNumber { get; set; }
    }
}