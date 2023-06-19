using CarParts.Models.Main;

namespace CarParts.Dto.ClientDto
{
    public class UpdateClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSeller { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}
