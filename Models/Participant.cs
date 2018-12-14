using System;
using System.ComponentModel.DataAnnotations;

namespace ActivityCenter.Models
{
    public class Participant
    {
        [Key] 
        public int ParticipantId { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
