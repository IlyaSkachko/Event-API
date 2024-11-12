using Events.Application.UseCases.HashUseCase.Hash.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Events.Application.UseCases.Hash.HashPassword
{
    public class HashPasswordUseCase : IHashPasswordUseCase
    {
        public string Execute(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(HashOption.KeySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, HashOption.Iterations, HashOption.HashAlgorithm, HashOption.KeySize);

            return Convert.ToHexString(hash);
        }
    }
}
