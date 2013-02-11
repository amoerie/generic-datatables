using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenericDatatables.Web.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public TimeSpan Time { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}