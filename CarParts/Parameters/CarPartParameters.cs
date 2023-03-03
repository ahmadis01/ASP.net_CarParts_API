using CarParts.Parameters.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarParts.Parameters
{
    public class CarPartParameters
    {
        public int CountryId { get; set; } = 0;
        public int CarId { get; set; } = 0;
        public int PartId { get; set; } = 0;
        public int BrandId { get; set; } = 0;
        public int StoreId { get; set; } = 0;
        public int PageSize { get; set; }
        public int PageNumber { get; set; } 
        public bool IsOrginal { get; set; }
        public string OrderBy{ get; set; }
        public string OrderStatus { get; set; }
        public DateOnly Date { get; set; }
        public string Search { get; set; } = string.Empty;

    }
}
