using System.ComponentModel.DataAnnotations;
namespace ContactList.Models
{
    public class Other
    {
        public int? OtherId { get; set; }
        public int ContactId { get; set; }

        [StringLength(50)]
        [Display(Name = "Другое")]
        public string OtherField { get; set; }

        public Contact Contact { get; set; }
}
}
