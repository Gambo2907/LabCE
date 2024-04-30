using System.Text;

namespace API.RandPassword
{
    public class PasswordGen
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%^&*_+-=;:,.?";

        public string GeneratePassword(int length)
        {
            Random random = new Random();
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                password.Append(Characters[random.Next(Characters.Length)]);
            }

            return password.ToString();
        }
    }
}
