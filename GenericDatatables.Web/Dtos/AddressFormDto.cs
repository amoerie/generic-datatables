namespace GenericDatatables.Web.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using GenericDatatables.Web.Models;
    using GenericDatatables.Web.Infrastructure.Dtos;

    public class AddressFormDto: IDto<Address>
    {
        public int Id { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "You don't have a street?")]
        public string Street { get; set; }

        [Display(Name = "House Nr.")]
        [Required(ErrorMessage = "What's your house number?")]
        public string HouseNumber { get; set; }

        [Display(Name = "Postal code")]
        [Required(ErrorMessage = "Ahum")]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Hey don't forget to fill in your city")]
        public string City { get; set; }
    }
}