using System.ComponentModel.DataAnnotations;

namespace CrossModel.Authenticate
{
    public class AuthenticateRequest
    {
        [Required]
        [MaxLength(5)]
        public string UserID { get; set; }

        [Required]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}