using Events.Application.UseCases.Hash;
using Events.Application.UseCases.HashUseCase.Verify.Interfaces;
using System.Security.Cryptography;

namespace Events.Application.UseCases.HashUseCase.Verify
{
    public class VerifyPasswordUseCase : IVerifyPasswordUseCase
    {
        public bool Execute(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, HashOption.Iterations, HashOption.HashAlgorithm, HashOption.KeySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
