using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace VLK.Trading.Application.Helpers
{
    public class Encryption
    {
        public static bool ValidatePassword(string password, string passwordsalt, string passwordHash, int? iterations)
        {
            var salt = Encoding.ASCII.GetBytes(passwordsalt);
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: iterations.Value, numBytesRequested: 256 / 8));
            return string.Equals(hashed, passwordHash);
        }
        private static string GetUniqID()
        {
            var ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            double t = ts.TotalMilliseconds / 1000;

            int a = (int)Math.Floor(t);
            int b = (int)((t - Math.Floor(t)) * 1000000);

            return a.ToString("x8") + b.ToString("x5");
        }
    }
}
