using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookClub
{
    public class AuthOptions
    {
        public const string ISSUER = "http://localhost:5000"; // издатель токена
        public const string AUDIENCE = "http://localhost:5000"; // потребитель токена
        private const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 1 минута
        public static SymmetricSecurityKey SecretKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
            }
        }
        public static string TOKENCOOKIE = ".AspNetCore.Application.Id";
        public static string TESTCOOKIE = ".AspNetCore.Application.System";
    }
}
