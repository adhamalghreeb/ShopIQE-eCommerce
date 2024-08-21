using System.ComponentModel.DataAnnotations;

namespace eCommerce.DTO
{
    public class AddressDto
    {
        
        public string? FirstName { get; set; } = string.Empty;


        public string? LastName { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string State { get; set; } = string.Empty; 

        [Required]
        public string Zipcode { get; set; } = string.Empty;
    }
}
