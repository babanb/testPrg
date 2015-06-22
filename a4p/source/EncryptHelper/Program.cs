using System;
using System.Linq;

namespace EncryptHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            EncryptUserTable();
            Console.WriteLine("Executed successfully!!");
            Console.ReadLine();
        }

        private static void EncryptUserTable()
        {
            using (var dc = new AdoPetsDataContext())
            {
                var users = dc.Users.ToList();
                foreach (var user in users)
                {
                    user.FirstName = Encryption.Encrypt(user.FirstName);
                    user.MiddleName = Encryption.Encrypt(user.MiddleName);
                    user.LastName = Encryption.Encrypt(user.LastName);
                    user.BirthDate = Encryption.Encrypt(user.BirthDate);
                    user.Email = Encryption.Encrypt(user.Email);
                    user.PrimaryPhone = Encryption.Encrypt(user.PrimaryPhone);
                    user.SecondaryPhone = Encryption.Encrypt(user.SecondaryPhone);
                    user.ReferralSource = Encryption.Encrypt(user.ReferralSource);
                    user.Address1 = Encryption.Encrypt(user.Address1);
                    user.Address2 = Encryption.Encrypt(user.Address2);
                    user.PostalCode = Encryption.Encrypt(user.PostalCode);
                    user.City = Encryption.Encrypt(user.City);
                }

                dc.SubmitChanges();
            }
        }
    }
}
