using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class ContactInformation
    {
        public int Id { get; set; }
        [DataType(DataType.PhoneNumber)]
        public List<string>PhoneNumbers { get; set; }
    }
}
