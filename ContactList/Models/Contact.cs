using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        [Display(Name = "Фамилия"), StringLength(50)]
        public string Surname { get; set; }

        [Display(Name = "Имя"), StringLength(50), MinLength(2)]
        [Required(ErrorMessage = "Обязательное поле." +
            " Писать с большой буквы!!! И только буквы.")]
        [RegularExpression(@"^[A-ZА-Я]+[A-Za-zА-Яа-я]*$")]
        public string Name { get; set; }

        [Display(Name = "Отчество"), StringLength(50)]
        public string Patronymic { get; set; }

        [DataType(DataType.Date), Display(Name = "День рождения")]
        
        public DateTime Birthday { get; set; }

        [StringLength(50), Display(Name = "Место работы")]
        public string Organization { get; set; }

        [StringLength(50), Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Имя")]
        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + Patronymic;
            }
        }

        [Display(Name = "Телефоны")]
        public IList<Phone> Phones { get; set; }
    }
}
