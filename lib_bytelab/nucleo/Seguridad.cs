using System.Security.Cryptography;
using System.Text;

namespace lib_bytelab.nucleo
{
    public class Seguridad
    {
        public static string Cifrar(string clave)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(clave));
            return Convert.ToHexString(bytes);
        }
    }
}
