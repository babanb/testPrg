using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Model.Tools;
using Repository.Implementations;

namespace MassCreation.Groupon
{
    class Program
    {

        private static Dictionary<string, int> columnIndex = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
        {
            {Constants.FirstName, -1}, //mandatory
            {Constants.Lastname, -1}, //mandatory
            {Constants.Email, -1}, //mandatory
            {Constants.PromoCode, -1} //mandatory
        };

        private static DomainTypeEnum userDomainType = DomainTypeEnum.US;

        private static UnitOfWork uow;
        private static string csvPath;

        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            Console.WriteLine("The column names are:");
            columnIndex.Keys.ToList().ForEach(Console.WriteLine);

            var lines = GetLines();

            if (lines == null)
            {
                Console.WriteLine("Error reading csv file.");
                Console.ReadLine();
            }
            else
            {
                var errors = string.Empty;
                var columns = lines.Skip(1).First().Split(new[] { "|" }, StringSplitOptions.None).ToList();

                if (ValidateColumns(columns, ref errors))
                {
                    Console.WriteLine(errors);
                    Console.ReadLine();
                }
                else
                {
                    columnIndex.Keys.ToList().ForEach(key =>
                    {
                        columnIndex[key] = columns.FindIndex(c => key.Equals(c, StringComparison.OrdinalIgnoreCase));
                    });
                    using (uow = new UnitOfWork())
                    {

                        var data = GetUserInfoFromExcel(lines.ToList());
                        InsertData(data);
                        Console.WriteLine();
                        Console.WriteLine(data.Count + " records were inserted successfully.");
                        Console.WriteLine("You have in the same folder of your csv, a file named: {0} \nwith the possible lines with errors.", Constants.ErrorFileName);
                        Console.ReadLine();
                    }
                }
            }
        }

        private static List<string> GetLines()
        {
            var openFileDialog = new OpenFileDialog { Filter = "Excel Files|*.csv" };
            openFileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(openFileDialog.FileName))
            {
                csvPath = openFileDialog.FileName;
                return File.ReadLines(openFileDialog.FileName).ToList();
            }
            return null;
        }

        private static bool ValidateColumns(List<string> columns, ref string errors)
        {
            var hasError = false;
            if (!columns.Contains(Constants.FirstName))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.FirstName);
                hasError = true;
            }
            if (!columns.Contains(Constants.Lastname))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.Lastname);
                hasError = true;
            }
            if (!columns.Contains(Constants.Email))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.Email);
                hasError = true;
            }
            if (!columns.Contains(Constants.PromoCode))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.PromoCode);
                hasError = true;
            }

            return hasError;
        }

        private static List<UserInformation> GetUserInfoFromExcel(List<string> csvLines)
        {
            var linesWithErrors = csvLines.Take(2).ToList();
            var result = new List<UserInformation>();

            var totalColumns = columnIndex.Count(c => c.Value > -1);

            foreach (var line in csvLines.Skip(2))
            {
                var errors = string.Empty;
                var hasError = false;

                var arr = line.Split(new[] { "|" }, StringSplitOptions.None);

                if (arr.Count() != totalColumns)
                {
                    errors = "This row is corrupted, some columns are missing.";
                    linesWithErrors.Add(string.Format("{0} | {1}", line, errors));
                    continue;
                }

                var firstName = !string.IsNullOrEmpty(arr[columnIndex[Constants.FirstName]]) ? arr[columnIndex[Constants.FirstName]].Trim() : null;
                var lastName = !string.IsNullOrEmpty(arr[columnIndex[Constants.Lastname]]) ? arr[columnIndex[Constants.Lastname]].Trim() : null;
                var email = !string.IsNullOrEmpty(arr[columnIndex[Constants.Email]]) ? arr[columnIndex[Constants.Email]].Trim() : null;
                var promoCode = !string.IsNullOrEmpty(arr[columnIndex[Constants.PromoCode]]) ? arr[columnIndex[Constants.PromoCode]].Trim() : null;

                if (firstName == null || lastName == null || email == null || promoCode == null)
                {
                    hasError = true;
                    errors += string.Format("- The columns: {0}, {1}, {2}, {3}are mandatories", Constants.FirstName, Constants.Lastname, Constants.Email, Constants.PromoCode);
                }

                if (email != null && !IsValidEmail(email))
                {
                    hasError = true;
                    errors += "- The email format is invalid";
                }

                if (email != null && IsValidEmail(email))
                {
                    if (uow.UserRepository.MailAlreadyExist(email))
                    {
                        hasError = true;
                        errors += "- Already exist a user with this email.";
                    }
                }

                if (promoCode != null && !uow.SubscriptionRepository.GetAll(s => s.PromotionCode == promoCode).Any())
                {
                    hasError = true;
                    errors += "- Promo Code does not exist";
                }

                if (hasError)
                {
                    linesWithErrors.Add(string.Format("{0} | {1}", line, errors));
                }
                else
                {
                    result.Add(new UserInformation
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        PromoCode = promoCode
                    });
                }
            }

            var pathErrors = Directory.GetParent(csvPath);
            File.WriteAllLines(string.Format("{0}/{1}", pathErrors, Constants.ErrorFileName), linesWithErrors);

            return result;
        }

        private static void InsertData(List<UserInformation> userInfos)
        {
            foreach (var userInfo in userInfos)
            {
                var user = new User
                {
                    UserTypeId = UserTypeEnum.OwnerAdmin,
                    DomainTypeId = userDomainType,
                    TimeZoneId = TimeZoneEnum.UTC0500EasternTimeUSCanada,
                    FirstName = new EncryptedText(userInfo.FirstName),
                    LastName = new EncryptedText(userInfo.LastName),
                    Email = new EncryptedText(userInfo.Email),
                    GeneralConditions = false,
                    IsUsingDST = false,
                    CreationDate = DateTime.Now,
                    UserStatusId = UserStatusEnum.Active,
                    InfoPath = string.Format("{0}\\{1}", DateTime.Today.Year, DateTime.Today.DayOfYear),
                    CountryId = CountryEnum.UnitedStates,
                };

                var randomPart = GetRandomString(5);
                var userName = CreateUserName(userInfo.FirstName, userInfo.LastName);
                var credentials = new Login
                {
                    UserName = Encryption.Encrypt(userName),
                    Password = Encryption.EncryptAsymetric(userName + randomPart),
                    RandomPart = randomPart,
                    IsTemporalPassword = true
                };
                user.Logins = new List<Login> { credentials };

                var subscription = uow.SubscriptionRepository.GetSingleTracking(s => s.PromotionCode == userInfo.PromoCode && s.IsPromotionCode);
                user.UserSubscription = new UserSubscription
                {
                    Subscription = subscription,
                    StartDate = DateTime.Today,
                    RenewalDate = DateTime.Today.AddDays(subscription.Duration).AddDays(-1),
                    SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.NotSent,
                };

                uow.UserRepository.Insert(user);
                uow.Save();
            }
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        private static string GetRandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random(Environment.TickCount);
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        private static string CreateUserName(string firstName, string lastName)
        {
            int num = 1;
            var userName = string.Format("{0}_{1}", firstName, lastName);

            while (uow.LoginRepository.GetByUserName(userName) != null)
            {
                userName = string.Format("{0}_{1}{2}", firstName, lastName, num++);
            }

            return userName;
        }

    }
}
