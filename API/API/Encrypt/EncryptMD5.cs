namespace API.Encriptacion;

using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

public class EncryptMD5
{
    public string Encrypt(string input)
    {
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
