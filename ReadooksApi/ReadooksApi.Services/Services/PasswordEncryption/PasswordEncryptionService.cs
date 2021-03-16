namespace Readooks.BusinessLogicLayer.Services.PasswordEncryption
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        public string Encrypt(string plainPassword, string salt)
        {
            return Hash.Create(plainPassword, salt);
        }

        public bool PasswordIsCorrect(string password, string salt, string encryptedPassword)
        {
            return (Encrypt(password, salt) == encryptedPassword);
        }
    }
}
