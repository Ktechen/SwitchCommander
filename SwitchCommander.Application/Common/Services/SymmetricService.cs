using System.Security.Cryptography;
using System.Text;

namespace SwitchCommander.Application.Common.Services
{
    public class SymmetricService : ISymmetricService
    {
        public async Task<string> GenerateRandomKey()
        {
            using var aesAlg = Aes.Create();
            aesAlg.GenerateKey();
            return await Task.FromResult(Convert.ToBase64String(aesAlg.Key));
        }

        public async Task<byte[]> GenerateRandomNonce(int length)
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            return await Task.FromResult(randomBytes);
        }

        public Task<byte[]> Encrypt(string plaintext, string key, string nonce, string pepper)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> Encrypt(string plaintext,  byte[] Key, byte[] IV, string pepper)
        {
            
            // Check arguments.
            if (plaintext == null || plaintext.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using var msEncrypt = new MemoryStream();
            await using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            await using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                //Write all data to the stream.
                await swEncrypt.WriteAsync(plaintext);
            }
            encrypted = msEncrypt.ToArray();

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        public async Task<string> Decrypt(byte[] ciphertext, string key, string nonce, string pepper)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(key);
            aesAlg.IV = Convert.FromBase64String(nonce);
            aesAlg.Mode = CipherMode.CBC;

            // Remove the pepper from the ciphertext.
            var pepperBytes = Encoding.UTF8.GetBytes(pepper);
            for (var i = 0; i < ciphertext.Length; i++)
            {
                ciphertext[i] ^= pepperBytes[i % pepperBytes.Length];
            }

            using var msDecrypt = new MemoryStream(ciphertext);
            using var decryptor = aesAlg.CreateDecryptor();
            await using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return await srDecrypt.ReadToEndAsync();
        }
    }
}