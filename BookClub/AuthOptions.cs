using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookClub
{
    public class AuthOptions
    {
        public const string ISSUER = "BookClub"; // издатель токена
        public const string AUDIENCE = "BookClubClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
