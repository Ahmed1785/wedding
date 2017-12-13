using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;



namespace wedding.Models
{
    
    public class RSVP
    {
        [Key]
        public int rsvp_id { get; set; }

        public User Guest { get; set; }

        public int user_id { get; set; }

        public int wedding_id { get; set; }

    }
}

