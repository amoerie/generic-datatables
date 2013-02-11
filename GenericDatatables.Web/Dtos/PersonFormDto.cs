namespace GenericDatatables.Web.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GenericDatatables.Web.Models;
    using GenericDatatables.Web.Infrastructure.Dtos;

    public class PersonFormDto: IDto<Person>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hey why don't you tell me who you are?!")]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Come on now.")]
        public DateTime Birthday { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "What time is it again?")]
        public TimeSpan Time { get; set; }

        public AddressFormDto Address { get; set; }
    }


}