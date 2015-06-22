using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsbc.ImportUser
{
    public class Constants
    {
        public static string LogoForEmailPath = "LogoForEmailPath";

        public static string SignaturePath = "LogoSignatureForEmailPath";

        public static string ErrorFileName = "LinesWithError.csv";

        public static string FilePath = "FilePath";

        #region Columns Name

        public static string FirstName = "First Name";

        public static string Lastname = "Last Name";

        public static string Phone1 = "Phone 1";

        public static string Phone2 = "Phone 2";

        public static string Email = "Email";

        public static string AdoptionDate = "Adoption Date";

        public static string AnimalName = "Animal Name";

        public static string AnimalType = "Animal Type";

        public static string Birthdate = "Animal DOB";

        public static string PrimaryBreed = "Primary Breed";

        public static string SecondaryBreed = "Secondary Breed";

        public static string Sex = "Sex";

        public static string ChipNumber = "Chip Number";

        public static string Immunization = "Immunization";

        public static string ImmunizationDate = "Immunization Date";

        public static string IsSterile = "Sterile";

        public static string PromoCode = "PromoCode";

        public static string OptIn = "Opt In";

        #endregion

        #region Animal Types

        public static string Dog = "Dog";

        public static string Cat = "Cat";

        public static string Duck = "Duck";

        public static string Horse = "Horse";

        public static string Parrot = "Parrot";

        public static string Rabbit = "Rabbit";

        public static string GuineaPig = "GuineaPig";

        public static string Hamster = "Hamster";

        public static string Frog = "Frog";

        public static string Turtle = "Turtle";

        public static string Fish = "Fish";

        public static string Other = "Other";

        #endregion

        #region Animal Genders

        public static string Male = "N";

        public static string Female = "S";

        #endregion
    }
}
