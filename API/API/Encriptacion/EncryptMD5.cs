namespace API.Encriptacion;

using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
/*
 * Se encarga de encriptar y desencriptar las passwords generadas y añadidas por el cliente en MD5
 * para asi guardarlas en las tuplas correspondientes en la base de datos.
 */
public class EncryptMD5
{
    public string Encrypt(string input)
    {
        //El hash es una clave secreta que se toma como base para encriptar y desencriptar los datos
        string hash = "bases de datos";
        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        MD5 md5 = MD5.Create();
        TripleDES tripledes = TripleDES.Create();

        tripledes.Key =  md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        tripledes.Mode = CipherMode.ECB;

        ICryptoTransform transform = tripledes.CreateEncryptor();
        Byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

        return Convert.ToBase64String(result);
    }
    public string Decrypt(string mensaje_en)
    {
        string hash = "bases de datos";
        byte[] data = Convert.FromBase64String(mensaje_en);
        MD5 md5 = MD5.Create();
        TripleDES tripledes = TripleDES.Create();

        tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        tripledes.Mode = CipherMode.ECB;

        ICryptoTransform transform = tripledes.CreateDecryptor();
        Byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

        return UTF8Encoding.UTF8.GetString(result);
    }
}
