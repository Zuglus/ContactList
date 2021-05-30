using System;
using System.ComponentModel.DataAnnotations;
namespace ContactList.Models
{
    public class Phone
    {
        public int? PhoneId { get; set; }
        public int ContactId { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(12)]
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        public Contact Contact { get; set; }
    }
}
