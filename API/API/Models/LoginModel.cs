using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.Models
{
    public class LoginModel
    {
        public string? Correo { get; set; }
        public string? Password { get; set; }
    }
}
