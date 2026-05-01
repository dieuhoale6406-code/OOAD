using System.Security.Cryptography;
using System.Text;

namespace OOAD.Utils
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            using var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToHexString(hashBytes);
        }

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash))
                return false;

            var inputHash = Hash(password);

            return string.Equals(
                inputHash,
                storedHash,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}