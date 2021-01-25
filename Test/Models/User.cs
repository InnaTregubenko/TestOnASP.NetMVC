using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field must be set.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field must be set.")]
        [RegularExpression(@"[0-9+()-]{7,14}", ErrorMessage = "Invalid phone.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The field must be set.")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateofBirth { get; set; }

        [Display(Name = "Married")]
        public bool Married { get; set; }

        [Required]
        [Display(Name = "Salary")]
       //[Range(typeof(decimal), "0", "100000,0", ErrorMessage = "Must be positive")]
        public decimal Salary { get; set; }

    }
}