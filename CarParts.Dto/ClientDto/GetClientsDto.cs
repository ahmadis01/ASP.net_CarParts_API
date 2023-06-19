namespace CarParts.Dto.ClientDto
{
    public class GetClientsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsSeller { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public double TotalAccount { get; set; } = 0;
    }
}
