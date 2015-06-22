using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Tools;
using Repository.Implementations;
using System.Linq.Expressions;
using Model;

namespace Hsbc.ExtractUser
{
    class Program
    {
        private static UnitOfWork uow;
        static void Main(string[] args)
        {
            using (uow = new UnitOfWork())
            {
                GetHsbcUsers();
            }
        }

        /// <summary>
        /// Gets the HSBC users.
        /// </summary>
        private static void GetHsbcUsers()
        {
            var filePath = ConfigurationManager.AppSettings[Constants.CSVFilePath];

            Expression<Func<User, object>> parames1 = v => v.Pets;
            Expression<Func<User, object>> parames2 = v => v.UserSubscription;
            Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2 };

            var userDetails = uow.UserRepository.GetAll(u => u.UserSubscription.Subscription.PromotionCode == "HSBC", navigationProperties: paramesArray);

            var csv = new StringBuilder();
            filePath = filePath + "Hsbc_Members.csv";

            if (!File.Exists(filePath))
            { csv.AppendLine("sep=,"); }

            var header = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}{9}",
                    Constants.FirstName, Constants.Lastname, Constants.Phone1, Constants.Phone2, Constants.Email,
                  Constants.CreationDate, Constants.PetName, Constants.PetType, Constants.AdoptionDate, Environment.NewLine);

            if (!File.Exists(filePath))
            { csv.Append(header); }

            foreach (var us in userDetails)
            {
                string creationDate = us.CreationDate.ToShortDateString();
                if (us.CreationDate.Date == DateTime.Now.Date)
                {
                    string firstName = us.FirstName;
                    string lastName = us.LastName;
                    string phone1 = us.PrimaryPhone;
                    string phone2 = us.SecondaryPhone;
                    string email = us.Email;

                    if (us.Pets != null && us.Pets.Count() > 0)
                    {
                        foreach (var pet in us.Pets)
                        {
                            string adoptionDate = (!string.IsNullOrEmpty(pet.AdoptionDate.ToString())) ? Convert.ToDateTime(pet.AdoptionDate).ToShortDateString() : "";

                            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}{9}", firstName, lastName, phone1, phone2, email,
                               creationDate, pet.Name.Value, pet.PetTypeId, adoptionDate, Environment.NewLine);
                            csv.Append(newLine);
                        }
                    }
                    else
                    {
                        string adoptionDate = (us.Pets != null && us.Pets.Count() > 0) ? Convert.ToDateTime(us.Pets.First().AdoptionDate).ToShortDateString() : "";
                        var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}{9}", firstName, lastName, phone1, phone2, email,
                           creationDate, "", "", adoptionDate, Environment.NewLine);
                        csv.Append(newLine);
                    }
                }
            }
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, csv.ToString());
            }
            else
            {
                File.AppendAllText(filePath, csv.ToString());
            }
        }

    }
}
