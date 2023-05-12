using CarParts.Models.Main;

namespace CarParts.Dto.ClientDto
{
    public class GetClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsSeller { get; set; }

    }
}
