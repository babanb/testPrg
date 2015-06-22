using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Model;
using Model.Tools;
using Repository.Implementations;

namespace MassCreation
{
    class Program
    {
        private static UnitOfWork uow;

        private static Dictionary<string, PetTypeEnum> petTypes =
            new Dictionary<string, PetTypeEnum>(StringComparer.InvariantCultureIgnoreCase)
            {
                {Constants.Dog, PetTypeEnum.Dog},
                {Constants.Cat, PetTypeEnum.Cat},
                {Constants.Duck, PetTypeEnum.Duck},
                {Constants.Horse, PetTypeEnum.Horse},
                {Constants.Parrot, PetTypeEnum.Parrot},
                {Constants.Rabbit, PetTypeEnum.Rabbit},
                {Constants.GuineaPig, PetTypeEnum.GuineaPig},
                {Constants.Hamster, PetTypeEnum.Hamster},
                {Constants.Frog, PetTypeEnum.Frog},
                {Constants.Turtle, PetTypeEnum.Turtle},
                {Constants.Fish, PetTypeEnum.Fish},
                {Constants.Other, PetTypeEnum.Other}
            };

        private static Dictionary<string, PetGenderEnum> petGenders =
            new Dictionary<string, PetGenderEnum>(StringComparer.InvariantCultureIgnoreCase)
            {
                {Constants.Female, PetGenderEnum.Female},
                {Constants.Male, PetGenderEnum.Male},
            };

        private static Dictionary<string, int> columnIndex = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
        {
            {Constants.FirstName, -1}, //mandatory
            {Constants.Lastname, -1}, //mandatory
            {Constants.Phone1, -1},
            {Constants.Phone2, -1},
            {Constants.Email, -1}, //mandatory
            {Constants.AdoptionDate, -1}, //mandatory
            {Constants.AnimalName, -1}, //mandatory
            {Constants.AnimalType, -1}, //mandatory
            {Constants.Birthdate, -1}, //mandatory
            {Constants.PrimaryBreed, -1},
            {Constants.SecondaryBreed, -1},
            {Constants.Sex, -1},
            {Constants.ChipNumber, -1},
            {Constants.PromoCode, -1}, //mandatory
            {Constants.Immunization, -1} ,
            {Constants.ImmunizationDate, -1}, 
            {Constants.IsSterile, -1} 
        };


        private static DomainTypeEnum userDomainType = DomainTypeEnum.US;

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
                //var columns1 = lines.Skip(1).First().Split(new[] { "|" }, StringSplitOptions.None).ToList();
                var columns = lines.First().Split(new[] { "," }, StringSplitOptions.None).ToList();

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

        static List<string> GetLines()
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

        static bool ValidateColumns(List<string> columns, ref string errors)
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
            if (!columns.Contains(Constants.AdoptionDate))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.AdoptionDate);
                hasError = true;
            }
            if (!columns.Contains(Constants.AnimalName))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.AnimalName);
                hasError = true;
            }
            if (!columns.Contains(Constants.AnimalType))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.AnimalType);
                hasError = true;
            }
            if (!columns.Contains(Constants.Birthdate))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.Birthdate);
                hasError = true;
            }
            if (!columns.Contains(Constants.PromoCode))
            {
                errors += string.Format("\nThe column {0} is mandatory.", Constants.PromoCode);
                hasError = true;
            }

            return hasError;
        }

        static List<UserInformation> GetUserInfoFromExcel(List<string> csvLines)
        {
            var linesWithErrors = csvLines.Take(2).ToList();
            var result = new List<UserInformation>();

            var totalColumns = columnIndex.Count(c => c.Value > -1);

            foreach (var line in csvLines.Skip(1))
            {
                var errors = string.Empty;
                var hasError = false;

                var arr = line.Split(new[] { "," }, StringSplitOptions.None);

                if (arr.Count() != totalColumns)
                {
                    errors = "This row is corrupted, some columns are missing.";
                    linesWithErrors.Add(string.Format("{0} | {1}", line, errors));
                    continue;
                }

                var firstName = !string.IsNullOrEmpty(arr[columnIndex[Constants.FirstName]]) ? arr[columnIndex[Constants.FirstName]].Trim() : null;
                var lastName = !string.IsNullOrEmpty(arr[columnIndex[Constants.Lastname]]) ? arr[columnIndex[Constants.Lastname]].Trim() : null;
                var phone1 = columnIndex[Constants.Phone1] > -1 && !string.IsNullOrEmpty(arr[columnIndex[Constants.Phone1]]) ? arr[columnIndex[Constants.Phone1]].Trim() : null;
                var phone2 = columnIndex[Constants.Phone2] > -1 && !string.IsNullOrEmpty(arr[columnIndex[Constants.Phone2]]) ? arr[columnIndex[Constants.Phone2]].Trim() : null;
                var email = !string.IsNullOrEmpty(arr[columnIndex[Constants.Email]]) ? arr[columnIndex[Constants.Email]].Trim() : null;
                var petAdoptionDate = !string.IsNullOrEmpty(arr[columnIndex[Constants.AdoptionDate]]) ? arr[columnIndex[Constants.AdoptionDate]].Trim() : null;
                var petName = !string.IsNullOrEmpty(arr[columnIndex[Constants.AnimalName]]) ? arr[columnIndex[Constants.AnimalName]].Trim() : null;
                var petType = !string.IsNullOrEmpty(arr[columnIndex[Constants.AnimalType]]) ? arr[columnIndex[Constants.AnimalType]].Trim() : null;
                var petBirthDate = !string.IsNullOrEmpty(arr[columnIndex[Constants.Birthdate]]) ? arr[columnIndex[Constants.Birthdate]].Trim() : null;
                var petPrimaryBreed = columnIndex[Constants.PrimaryBreed] > -1 && !string.IsNullOrEmpty(arr[columnIndex[Constants.PrimaryBreed]]) ? arr[columnIndex[Constants.PrimaryBreed]].Trim() : null;
                var petSecondaryBreed = columnIndex[Constants.SecondaryBreed] > -1 && !string.IsNullOrEmpty(arr[columnIndex[Constants.SecondaryBreed]]) ? arr[columnIndex[Constants.SecondaryBreed]].Trim() : null;
                var petGender = columnIndex[Constants.Sex] > -1 && !string.IsNullOrEmpty(arr[columnIndex[Constants.Sex]]) ? arr[columnIndex[Constants.Sex]].Trim() : null;
                var chipNumber = columnIndex[Constants.ChipNumber] > -1 && !string.IsNullOrEmpty(arr[columnIndex[Constants.ChipNumber]]) ? arr[columnIndex[Constants.ChipNumber]].Trim() : null;
                var promoCode = !string.IsNullOrEmpty(arr[columnIndex[Constants.PromoCode]]) ? arr[columnIndex[Constants.PromoCode]].Trim() : null;
                var immunization = !string.IsNullOrEmpty(arr[columnIndex[Constants.Immunization]]) ? arr[columnIndex[Constants.Immunization]].Trim() : null;
                var immunizationDate = !string.IsNullOrEmpty(arr[columnIndex[Constants.ImmunizationDate]]) ? arr[columnIndex[Constants.ImmunizationDate]].Trim() : null;
                var isSterile = !string.IsNullOrEmpty(arr[columnIndex[Constants.IsSterile]]) ? arr[columnIndex[Constants.IsSterile]].Trim() : null;

                //var EmailId = new EncryptedText(email);

                //var isEmailExists = uow.UserRepository.GetUserIdByEmail(EmailId);
                //var isUserName = uow.UserRepository.GetSingle(u => u.Id == isEmailExists.Value);



                #region validate
                if (firstName == null || lastName == null || email == null || petName == null || petType == null || petBirthDate == null || petAdoptionDate == null || promoCode == null || immunization == null)
                {
                    hasError = true;
                    errors += string.Format("- The columns: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} are mandatories",
                        Constants.FirstName, Constants.Lastname, Constants.Email, Constants.AnimalName,
                        Constants.AnimalType, Constants.Birthdate, Constants.AdoptionDate, Constants.PromoCode, Constants.Immunization);
                }

                if ((phone1 != null && !IsValidPhone(phone1)) || (phone2 != null && !IsValidPhone(phone2)))
                {
                    hasError = true;
                    errors += "- The phone number is invalid. It must contains 10";
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

                var birthDate = new DateTime();
                if (petBirthDate != null && !DateTime.TryParse(petBirthDate, out birthDate))
                {
                    hasError = true;
                    errors += "- The birthdate format is invalid";
                }

                var adoptionDate = new DateTime();
                if (petAdoptionDate != null && !DateTime.TryParse(petAdoptionDate, out adoptionDate))
                {
                    hasError = true;
                    errors += "- The adoption format is invalid";
                }

                if (petType != null && !petTypes.ContainsKey(petType))
                {
                    hasError = true;
                    errors += "- The pet type is invalid.";
                }

                if (petGender != null && !petGenders.ContainsKey(petGender))
                {
                    hasError = true;
                    errors += "- The pet gender is invalid.";
                }

                if (promoCode != null && !uow.SubscriptionRepository.GetAll(s => s.PromotionCode == promoCode).Any())
                {
                    hasError = true;
                    errors += "- Promo Code does not exist";
                }
                var immDate = new DateTime();
                if (immunizationDate != null && !DateTime.TryParse(immunizationDate, out immDate))
                {
                    hasError = true;
                    errors += "- The immunization date format is invalid";
                }
                #endregion

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
                        Phone1 = phone1,
                        Phone2 = phone2,
                        Email = email,
                        PetName = petName,
                        PetType = petTypes[petType],
                        PetBirthDate = birthDate,
                        AdoptionDate = adoptionDate,
                        PetPrimaryBreed = petPrimaryBreed,
                        PetSecondaryBreed = petSecondaryBreed,
                        PetGender = petGender != null ? petGenders[petGender] : (PetGenderEnum?)null,
                        ChipNumber = chipNumber,
                        PromoCode = promoCode,
                        Immunization = immunization,
                        ImmunizationDate = immDate,
                        Sterile = Convert.ToBoolean(isSterile)
                    });
                }
            }

            var pathErrors = Directory.GetParent(csvPath);
            File.WriteAllLines(string.Format("{0}/{1}", pathErrors, Constants.ErrorFileName), linesWithErrors);

            return result;
        }

        static void InsertData(List<UserInformation> userInfos)
        {
            foreach (var userInfo in userInfos)
            {
                var isEmailExists = uow.UserRepository.GetUserIdByEmail(userInfo.Email);
                var isUserName = uow.UserRepository.GetSingle(u => u.Id == isEmailExists.Value);

                if (userInfo.UserId == 0 && isUserName == null)
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
                        PrimaryPhone = new EncryptedText(userInfo.Phone1),
                        SecondaryPhone = new EncryptedText(userInfo.Phone2),
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

                    var subscription = uow.SubscriptionRepository.GetSingleTracking(s => s.PromotionCode == userInfo.PromoCode && s.IsTrial.HasValue && s.IsTrial.Value);
                    var startDate = userInfo.AdoptionDate < DateTime.Today ? DateTime.Today : userInfo.AdoptionDate;
                    user.UserSubscription = new UserSubscription
                    {
                        Subscription = subscription,
                        StartDate = startDate,
                        RenewalDate = startDate.AddDays(subscription.Duration).AddDays(-1),
                        SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.NotSent,
                    };

                    var pet = new Pet
                    {
                        PetTypeId = userInfo.PetType,
                        Name = new EncryptedText(userInfo.PetName),
                        CustomBreed = userInfo.PetPrimaryBreed,
                        SecondaryBreed = userInfo.PetSecondaryBreed,
                        BirthDate = userInfo.PetBirthDate.Value,
                        AdoptionDate = userInfo.AdoptionDate,
                        GenderId = userInfo.PetGender,
                        CreationDate = DateTime.Now,
                        ChipNumber = new EncryptedText(userInfo.ChipNumber),
                        PetStatusId = PetStatusEnum.Active,
                        IsSterile = userInfo.Sterile
                    };

                    var farmer = new Farmer
                    {
                        Name = new EncryptedText("Humane Society of Broward County"),
                        Address1 = new EncryptedText("2070 Griffin Rd"),
                        CountryId = CountryEnum.UnitedStates,
                        StateId = StateEnum.Florida,
                        City = new EncryptedText("Fort Lauderdale"),
                        Zip = new EncryptedText("33312"),
                        PhoneOffice = new EncryptedText("954-989-3977")
                    };

                    pet.Farmer = farmer;

                    user.Pets = new List<Pet> { pet };

                    uow.UserRepository.Insert(user);
                    uow.Save();

                    uow = new UnitOfWork();
                    var petVaccination = new PetVaccination
                    {
                        PetId = user.Pets.FirstOrDefault().Id,
                        CustomVaccination = userInfo.Immunization,
                        InjectionDate = userInfo.ImmunizationDate.Value
                    };

                    uow.PetVaccinationRepository.Insert(petVaccination);
                    uow.Save();
                }
                else
                {
                    uow = new UnitOfWork();

                    var petDetail = uow.PetRepository.GetSingle(p => p.PetTypeId == userInfo.PetType && p.Users.Any(u => u.Id == isUserName.Id));

                    var petVaccination = new PetVaccination
                    {
                        PetId = petDetail.Id,
                        CustomVaccination = userInfo.Immunization,
                        InjectionDate = userInfo.ImmunizationDate.Value
                    };

                    uow.PetVaccinationRepository.Insert(petVaccination);
                    uow.Save();
                }
            }
        }

        private static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]");
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
