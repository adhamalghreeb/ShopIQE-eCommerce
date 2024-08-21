using System.ComponentModel.DataAnnotations;
using eCommerce.Core.entities.identity;

namespace eCommerce.Core.entities
{
    public class Address
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? Lastname { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;

        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}