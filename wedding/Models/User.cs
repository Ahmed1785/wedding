using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;



namespace wedding.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        [MinLength(3)]
        public string first_name { get; set; }

        [Required]
        [MinLength(3)]
        public string last_name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]

        public string password { get; set; }

        public List<RSVP> RSVPS {get; set;}

    }

    public class LoginUser
    {


        [Key]
        public long loguser_id { get; set; }

        [Required]
        [EmailAddress]
        public string LogEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LogPassword { get; set; }

    }

      


    }
