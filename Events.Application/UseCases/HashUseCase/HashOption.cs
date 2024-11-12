using System.Security.Cryptography;

namespace Events.Application.UseCases.Hash
{
    public static class HashOption
    {
        public static readonly int KeySize = 64;
        public static readonly int Iterations = 10000;
        public static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;
    }
}
