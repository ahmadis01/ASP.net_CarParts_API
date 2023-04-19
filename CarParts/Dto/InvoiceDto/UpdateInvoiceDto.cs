﻿namespace CarParts.Dto.InvoiceDto
{
    public class UpdateInvoiceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Coast { get; set; }
        public string? Notes { get; set; }
        public int Services { get; set; }
        public bool Received { get; set; }
    }
}
