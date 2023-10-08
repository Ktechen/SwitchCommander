using SwitchCommander.Application.Common.Services;

namespace SwitchCommander.UnitTests.Common.Services;

public class SymmetricServiceTest
{
    [Fact]
    public async void EncryptAndDecrypt_Success()
    {
        // Arrange
        ISymmetricService symmetricService = new SymmetricService();
        var encryptionKey = symmetricService.GenerateRandomKey().Result;
        var nonce = symmetricService.GenerateRandomNonce(12).Result;
        var plaintext = "Hello, world!";
        var pepper = "YourSecretPepperHere";

        // Act
        var ciphertext =
            await symmetricService.Encrypt(plaintext, encryptionKey, Convert.ToBase64String(nonce), pepper);
        var decryptedText =
            await symmetricService.Decrypt(ciphertext, encryptionKey, Convert.ToBase64String(nonce), pepper);

        // Assert
        Assert.Equal(plaintext, decryptedText);
    }

    [Fact]
    public void GenerateRandomKey_Success()
    {
        // Arrange
        ISymmetricService symmetricService = new SymmetricService();

        // Act
        var key = symmetricService.GenerateRandomKey().Result;

        // Assert
        Assert.NotNull(key);
        var bytes = new Span<byte>(new byte[256]);
        Assert.True(Convert.TryFromBase64String(key, bytes, out var la));
    }

    [Fact]
    public async void DecryptWithIncorrectKey_Error()
    {
        // Arrange
        ISymmetricService symmetricService = new SymmetricService();
        var encryptionKey = symmetricService.GenerateRandomKey().Result;
        var nonce = symmetricService.GenerateRandomNonce(12).Result;
        var plaintext = "Hello, world!";
        var pepper = "YourSecretPepperHere";
        var wrongKey = symmetricService.GenerateRandomKey().Result; // Use a different key

        // Act
        var ciphertext =
            await symmetricService.Encrypt(plaintext, encryptionKey, Convert.ToBase64String(nonce), pepper);
        var decryptedText = await symmetricService.Decrypt(ciphertext, wrongKey, Convert.ToBase64String(nonce), pepper);

        // Assert
        Assert.NotEqual(plaintext, decryptedText);
    }

    [Fact]
    public async void DecryptWithIncorrectPepper_Error()
    {
        // Arrange
        ISymmetricService symmetricService = new SymmetricService();
        var encryptionKey = symmetricService.GenerateRandomKey().Result;
        var nonce = symmetricService.GenerateRandomNonce(12).Result;
        var plaintext = "Hello, world!";
        var pepper = "YourSecretPepperHere";
        var wrongPepper = "WrongPepper"; // Use a different pepper

        // Act
        var ciphertext =
            await symmetricService.Encrypt(plaintext, encryptionKey, Convert.ToBase64String(nonce), pepper);
        var decryptedText =
            await symmetricService.Decrypt(ciphertext, encryptionKey, Convert.ToBase64String(nonce), wrongPepper);

        // Assert
        Assert.NotEqual(plaintext, decryptedText);
    }

    [Fact]
    public async void DecryptWithIncorrectNonce_Error()
    {
        // Arrange
        ISymmetricService symmetricService = new SymmetricService();
        var encryptionKey = symmetricService.GenerateRandomKey().Result;
        var nonce = symmetricService.GenerateRandomNonce(12).Result;
        var plaintext = "Hello, world!";
        var pepper = "YourSecretPepperHere";
        var wrongNonce = symmetricService.GenerateRandomNonce(12).Result; // Use a different nonce

        // Act
        var ciphertext =
            await symmetricService.Encrypt(plaintext, encryptionKey, Convert.ToBase64String(nonce), pepper);
        var decryptedText =
            await symmetricService.Decrypt(ciphertext, encryptionKey, Convert.ToBase64String(wrongNonce), pepper);

        // Assert
        Assert.NotEqual(plaintext, decryptedText);
    }
}