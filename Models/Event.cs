using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace ActivityCenter.Models
{
    public class Event
    {
        [Key]
        public int EventId {get;set;}

        [Required]
        [MinLength(2)]
        public string Title {get;set;}

        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        [DataType(DataType.Time)]
        public DateTime Time {get;set;}

        public TimeSpan Duration {get;set;}

        [Required]
        [MinLength(10, ErrorMessage="Description must be 10 characters or longer")]
        public string Description {get;set;}

        public int UserId {get;set;}
        public User Coordinator {get;set;}

        public List<Participant> Participants { get; set; }

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}
