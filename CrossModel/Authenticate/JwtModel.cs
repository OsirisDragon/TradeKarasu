using System.ComponentModel.DataAnnotations;

namespace CrossModel.Authenticate
{
    public class JwtModel
    {
        public string UserID { get; set; }

        public string UserName { get; set; }
    }
}