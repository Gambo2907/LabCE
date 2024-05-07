using System.Text;

/*
 * Clase PasswordGen: Esta clase con su metodo GeneratePassword se encarga de realizar passwords
 * aleatorios con una cantidad de caracteres especificada por el usuario.
 * En este caso a la hora de llamar a este metodo se le ha dado un rango de 8 caracteres a todas las 
 * passwords generadas por esto.
 * 
 * */

namespace API.RandPassword
{
    public class PasswordGen
    {
        //Variable de donde se obtienen todos los caracteres que puede utilizar el metodo
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
