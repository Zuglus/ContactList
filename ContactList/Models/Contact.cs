using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string surname { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }

      [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        public string organization { get; set; }
        public string position { get; set; }
    }
}
