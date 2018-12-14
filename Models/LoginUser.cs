using System.ComponentModel.DataAnnotations;

namespace ActivityCenter.Models
{
    public class LoginUser
    {
        [Required]
        public string LoginEmail {get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}
