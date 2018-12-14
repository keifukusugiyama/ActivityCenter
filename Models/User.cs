using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace ActivityCenter.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string FirstName {get;set;}

        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string LastName {get;set;}

        [EmailAddress]
        [Required]
        public string Email {get;set;}

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
        [RegularExpression("(?=.*?[0-9])(?=.*?[A-Za-z])(?=.*[^0-9A-Za-z]).+", ErrorMessage = "Must contain at least 1 letter, 1 number, and a special character.")]
        public string Password {get;set;}

        public List<Participant> Participants { get; set; }
        public List<Event> CreatedEvents { get; set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
    }    
}
