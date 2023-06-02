using CarParts.Parameters.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarParts.Parameters
{
    public class PartParameters
    {
        public int CountryId { get; set; } = 0;
        public int CarId { get; set; } = 0;
        public int BrandId { get; set; } = 0;
        public int StoreId { get; set; } = 0;
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 
        public string OrderBy { get; set; } = "id";
        public string OrderStatus { get; set; } = "asc";
        public DateTime Date { get; set; }
        public string Search { get; set; } = string.Empty;

    }
}
