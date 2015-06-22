using System;
using System.Globalization;
using System.Threading;
using Model;
using UnitsNet;

namespace ADOPets.Web.Common.Helpers
{
    public static class HeightConverterHelper
    {
        /// <summary>
        /// Returns a string in the format: 10 m 5 cm, 10 ft 5 in
        /// </summary>
        /// <param name="unit">Target unit to convert(feet or meters)</param>
        /// <param name="measureValue">Measure value in meters</param>
        /// <returns></returns>
        public static string GetFullHeigtAsString(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            //database value in meter
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Length.FromMeters(dbValue);

            if (unit == HealthMeasureUnitEnum.Feet)
            {
                //rounding to only two decimal digits in feets
                total = Length.FromFeet(Math.Round(total.Feet, 2));

                var integerPart = Length.FromFeet(Math.Truncate(total.Feet));
                var decimalPart = Length.FromFeet(total.Feet - integerPart.Feet);

                var integerPartResult = (int)Math.Round(integerPart.Feet);
                var decimalPartResult = (int)Math.Round(decimalPart.Inches);

                if (integerPartResult == 0)
                {
                    return string.Format("{0} {1}", decimalPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Inches));
                }
                if (decimalPartResult == 0)
                {
                    return string.Format("{0} {1}", integerPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Feet));
                }
                return string.Format("{0} {1} {2} {3}",
                        integerPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Feet),
                        decimalPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Inches));
            }
            else
            {
                var integerPart = Length.FromMeters(Math.Truncate(total.Meters));
                var decimalPart = Length.FromMeters(total.Meters - integerPart.Meters);

                var integerPartResult = (int)Math.Round(integerPart.Meters);
                var decimalPartResult = (int)Math.Round(decimalPart.Centimeters);

                if (integerPartResult == 0)
                {
                    return string.Format("{0} {1}", decimalPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Centimeter));
                }
                if (decimalPartResult == 0)
                {
                    return string.Format("{0} {1}", integerPartResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Meter));
                }
                return string.Format("{0} {1} {2} {3}",
                        integerPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Meter),
                        decimalPartResult,
                        EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Centimeter));
            }
        }

        /// <summary>
        /// Return the value as a double in the format: ft.inches or meter.centimeter
        /// </summary>
        /// <param name="unit">Target unit to convert(feet or meters)</param>
        /// <param name="measureValue">Measure value in meters</param>
        /// <returns></returns>
        public static double GetFullHeightAsDouble(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var leftValue = GetLefttValue(unit, measureValue);

            var rightValue = GetRightValue(unit == HealthMeasureUnitEnum.Feet ? HealthMeasureUnitEnum.Inches : HealthMeasureUnitEnum.Centimeter , measureValue);
            var rightValueAsString = rightValue < 10 ? "0" + rightValue : rightValue.ToString();

            var result = (leftValue + Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + rightValueAsString);

            return Convert.ToDouble(result);
        }

        /// <summary>
        /// Returns the left part in the specified measure unit
        /// </summary>
        /// <param name="unit">Target unit to convert(feet or meters)</param>
        /// <param name="measureValue">Measure value in meters</param>
        /// <returns></returns>
        public static int GetLefttValue(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            if (unit == HealthMeasureUnitEnum.Feet)
            {
                var total = Length.FromMeters(dbValue);

                total = Length.FromFeet(Math.Round(total.Feet, 2));

                var integerPart = Length.FromFeet(Math.Truncate(total.Feet));

                var integerPartResult = (int) Math.Round(integerPart.Feet);

                return integerPartResult;
            }
            else
            {
                var integerPartResult = (int) Math.Round(Math.Truncate(dbValue));

                return integerPartResult;
            }
        }

        /// <summary>
        /// Returns the right part in the specified measure unit
        /// </summary>
        /// <param name="unit">Target unit to convert(inches or centimeters)</param>
        /// <param name="measureValue">Measure value in meters</param>
        /// <returns></returns>
        public static int GetRightValue(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Length.FromMeters(dbValue);

            if (unit == HealthMeasureUnitEnum.Inches)
            {
                total = Length.FromFeet(Math.Round(total.Feet, 2));

                var integerPart = Length.FromFeet(Math.Truncate(total.Feet));
                var decimalPart = Length.FromFeet(total.Feet - integerPart.Feet);

                var decimalPartResult = (int) Math.Round(decimalPart.Inches);

                return decimalPartResult;
            }
            else
            {
                var integerPart = Length.FromMeters(Math.Truncate(total.Meters));
                var decimalPart = Length.FromMeters(total.Meters - integerPart.Meters);

                var decimalPartResult = (int)Math.Round(decimalPart.Centimeters);

                return decimalPartResult;
            }
        }

        /// <summary>
        /// Returns the total value in meters
        /// </summary>
        /// <param name="fromUnit">Measure unit source</param>
        /// <param name="left">left value</param>
        /// <param name="right">right value</param>
        /// <returns></returns>
        public static double GetTotalInMeters(HealthMeasureUnitEnum fromUnit, int left, int right)
        {
            Length total;

            if (fromUnit == HealthMeasureUnitEnum.Feet)
            {
                total = Length.FromFeet(left) + Length.FromInches(right);
            }
            else
            {
                total = Length.FromMeters(left) + Length.FromCentimeters(right);
            }

            return total.Meters;
        }

    }
}