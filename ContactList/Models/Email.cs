using System.ComponentModel.DataAnnotations;
namespace ContactList.Models
{
    public class Email
    {
        public int? EmailId { get; set; }
        public int ContactId { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(20)]
        public string EmailAdress { get; set; }

        public Contact Contact { get; set; }
}
}
