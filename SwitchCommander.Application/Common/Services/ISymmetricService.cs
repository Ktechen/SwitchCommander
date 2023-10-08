namespace SwitchCommander.Application.Common.Services;

public interface ISymmetricService
{
    public Task<string> GenerateRandomKey();
    public Task<byte[]> GenerateRandomNonce(int length);
    public Task<byte[]> Encrypt(string plaintext, string key, string nonce, string pepper);
    public Task<string> Decrypt(byte[] ciphertext, string key, string nonce, string pepper);
}