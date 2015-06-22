using System;
using System.Globalization;
using System.Threading;
using Model;
using UnitsNet;

namespace ADOPets.Web.Common.Helpers
{
    public class WeightConverterHelper
    {
        /// <summary>
        /// Returns a string in the format: 10 lbz 5 oz, 10 klg 5 g
        /// </summary>
        /// <param name="unit">Target unit to convert(pounds or kilogram)</param>
        /// <param name="measureValue">Measure value in grams</param>
        /// <returns></returns>
        public static string GetFullWeightAsString(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            //database value in grams
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Mass.FromGrams(dbValue);

            if (unit == HealthMeasureUnitEnum.Pounds)
            {
                var integerPart = Mass.FromPounds(Math.Truncate(total.Pounds));
                var decimalPart = Mass.FromPounds(total.Pounds - integerPart.Pounds);

                var integerPartResult = (int)Math.Round(integerPart.Pounds);
                var decimalPartResult = (int)Math.Round(decimalPart.Pounds * 16);

                if (integerPartResult == 0)
                {
                    return string.Format("{0} {1}", decimalPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Ounce));
                }
                if (decimalPartResult == 0)
                {
                    return string.Format("{0} {1}", integerPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Pounds));
                }
                return string.Format("{0} {1} {2} {3}",
                        integerPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Pounds),
                        decimalPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Ounce));
            }
            else
            {
                var integerPart = Mass.FromKilograms(Math.Truncate(total.Kilograms));
                var decimalPart = Mass.FromKilograms(total.Kilograms - integerPart.Kilograms);

                var integerPartResult = (int)Math.Round(integerPart.Kilograms);
                var decimalPartResult = (int)Math.Round(decimalPart.Grams);

                if (integerPartResult == 0)
                {
                    return string.Format("{0} {1}", decimalPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Gram));
                }
                if (decimalPartResult == 0)
                {
                    return string.Format("{0} {1}", integerPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Kilogram));
                }
                return string.Format("{0} {1} {2} {3}",
                        integerPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Kilogram),
                        decimalPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Gram));
            }
        }

        /// <summary>
        /// Return the value as a double in the format: pounds.ounces or kilograms.grams
        /// </summary>
        /// <param name="unit">Target unit to convert(pounds or kilogram)</param>
        /// <param name="measureValue">Measure value in grams</param>
        /// <returns></returns>
        public static double GetFullWeightAsDouble(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var leftValue = GetLefttValue(unit, measureValue);

            var rightValue = GetRightValue(unit == HealthMeasureUnitEnum.Pounds ? HealthMeasureUnitEnum.Ounce : HealthMeasureUnitEnum.Gram, measureValue);
            var rightValueAsString = rightValue < 10 ? "00" + rightValue : (rightValue < 100 ? "0" + rightValue : rightValue.ToString());

            var result = (leftValue + Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + rightValueAsString);

            return Convert.ToDouble(result);
        }

        /// <summary>
        /// Returns the left part in the specified measure unit
        /// </summary>
        /// <param name="unit">Target unit to convert(pounds or kilogram)</param>
        /// <param name="measureValue">Measure value in grams</param>
        /// <returns></returns>
        public static int GetLefttValue(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Mass.FromGrams(dbValue);

            if (unit == HealthMeasureUnitEnum.Pounds)
            {
                total = Mass.FromPounds(Math.Round(total.Pounds, 2));

                var integerPart = Mass.FromPounds(Math.Truncate(total.Pounds));

                var integerPartResult = (int)integerPart.Pounds;

                return integerPartResult;
            }
            else
            {
                var integerPartResult = (int)Math.Truncate(total.Kilograms);

                return integerPartResult;
            }
        }

        /// <summary>
        /// Returns the right part in the specified measure unit
        /// </summary>
        /// <param name="unit">Target unit to convert(ounces or grams)</param>
        /// <param name="measureValue">Measure value in grams</param>
        /// <returns></returns>
        public static int GetRightValue(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Mass.FromGrams(dbValue);

            if (unit == HealthMeasureUnitEnum.Ounce)
            {
                var integerPart = Mass.FromPounds(Math.Truncate(total.Pounds));
                var decimalPart = Mass.FromPounds(total.Pounds - integerPart.Pounds);

                var decimalPartResult = (int)Math.Round(decimalPart.Pounds * 16);

                return decimalPartResult;
            }
            else
            {
                var integerPart = Mass.FromKilograms(Math.Truncate(total.Kilograms));
                var decimalPart = Mass.FromKilograms(total.Kilograms - integerPart.Kilograms);

                var decimalPartResult = (int)Math.Round(decimalPart.Grams);

                return decimalPartResult;
            }
        }

        /// <summary>
        /// Returns the total value in grams
        /// </summary>
        /// <param name="fromUnit">Measure unit source</param>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <returns></returns>
        public static double GetTotalInGrams(HealthMeasureUnitEnum fromUnit, int left, int right)
        {
            Mass total;

            if (fromUnit == HealthMeasureUnitEnum.Pounds)
            {
                total = Mass.FromPounds(left) + Mass.FromPounds((double)right / 16);
            }
            else
            {
                total = Mass.FromKilograms(left) + Mass.FromGrams(right);
            }

            return total.Grams;
        }
    }
}