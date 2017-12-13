using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;



namespace wedding.Models
{
    public class WeddingPlanner
    {
        [Key]
        public int wedding_id { get; set; }

        [Required]
        [MinLength(3)]
        public string bride { get; set; }

        [Required]
        [MinLength(3)]
        public string groom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [ValidateDate]
        public DateTime Date { get; set; }


        [Required]
        [MinLength(3)]
        public string Address { get; set; }

        public User Planner { get; set; }

        public List<RSVP> Guests { get; set; }

        public int user_id {get; set;}

    }

    public class ValidateDate:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime Today = DateTime.Now; 
            if(value is DateTime)
            {
                DateTime InputDate = (DateTime)value;
                if (InputDate > Today)
                {
                    return ValidationResult.Success;
                } else {
                    return new ValidationResult("Cannot have your wedding in the past");
                }
            }
        return new ValidationResult("Please enter valid date");
    }
}

  
}

