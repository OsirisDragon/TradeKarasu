using System.ComponentModel.DataAnnotations;

namespace CrossModel.Authenticate
{
    public class AuthenticateResponse
    {
        public string UserID { get; set; }

        public string Token { get; set; }
    }
}