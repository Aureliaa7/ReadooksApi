namespace Readooks.BusinessLogicLayer.Services.PasswordEncryption
{
    public interface IPasswordEncryptionService
    {
        string Encrypt(string plainPassword, string salt);
        bool PasswordIsCorrect(string password, string salt, string encryptedPassword);
    }
}
