using Model.Tools;

namespace Model
{
    public partial class EncryptedText
    {
        public EncryptedText()
        {
            
        }

        public EncryptedText(string text)
        {
            Value = text;
        }

        public string Value
        {
            get
            {
                return EncryptedValue != null ? Encryption.Decrypt(EncryptedValue) : null;
            }
            set
            {
                EncryptedValue = Encryption.Encrypt(value);
            }
        }

        public static implicit operator string(EncryptedText et)
        {
            return et != null ? et.Value : string.Empty;
        }
    }
}
