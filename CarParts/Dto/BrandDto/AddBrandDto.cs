﻿namespace CarParts.Dto.BrandDto
{
    public class AddBrandDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public int CountryId { get; set; }
    }
}
