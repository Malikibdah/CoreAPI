using System.ComponentModel.DataAnnotations;

namespace WepAPICoreTasks1.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "The Username is Required")]
        public string? Username { get; set; }
        
        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
