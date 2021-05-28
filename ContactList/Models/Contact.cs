using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }

      [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Organization { get; set; }
        public string Position { get; set; }
    }
}
