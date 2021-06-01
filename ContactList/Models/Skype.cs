using System.ComponentModel.DataAnnotations;
namespace ContactList.Models
{
    public class Skype
    {
        public int? SkypeId { get; set; }
        public int ContactId { get; set; }

        [StringLength(25)]
        public string SkypeNumber { get; set; }

        public Contact Contact { get; set; }
}
}
