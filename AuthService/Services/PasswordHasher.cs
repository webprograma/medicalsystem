using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AuthService.Services
{
    public class PasswordHasher
    {
        public string Hash(string password)
        {
            // 128-bit salt yaratamiz
            byte[] salt = RandomNumberGenerator.GetBytes(16); // 128-bit
            string saltBase64 = Convert.ToBase64String(salt);

            // password + salt dan hash hosil qilamiz
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32
            ));

            // Format: {salt}:{hash}
            return $"{saltBase64}:{hash}";
        }

        public bool Verify(string storedHash, string password)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            string hashToCompare = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32
            ));

            return hashToCompare == parts[1];
        }
    }
}
