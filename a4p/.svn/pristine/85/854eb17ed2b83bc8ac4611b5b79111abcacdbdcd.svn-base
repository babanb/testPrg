using System;
using System.Globalization;
using Model;
using UnitsNet;

namespace ADOPets.Web.Common.Helpers
{
    public static class TemperatureConverterHelper
    {
        /// <summary>
        /// Returns a string in the format: 35.22 °C, 35.22 °F
        /// </summary>
        /// <param name="unit">Target unit to convert(Fahrenheit or Celcius)</param>
        /// <param name="measureValue">Measure value in Celsius</param>
        /// <returns></returns>
        public static string GetFullTemperatureAsString(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            //database value in grams
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Temperature.FromDegreesCelsius(dbValue);

            if (unit == HealthMeasureUnitEnum.Fahrenheit)
            {
                var result = Math.Round(total.DegreesFahrenheit, 2);

                return string.Format("{0} {1}", result, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Fahrenheit));
            }
            else
            {
                var integerResult = Math.Round(total.DegreesCelsius, 2);

                return string.Format("{0} {1}", integerResult, EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Celsius));
            }
        }

        /// <summary>
        /// Return the value as a double
        /// </summary>
        /// <param name="unit">Target unit to convert(Fahrenheit or Celcius)</param>
        /// <param name="measureValue">Measure value in Celsius</param>
        /// <returns></returns>
        public static double GetFullTemperatureAsDouble(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            //database value in celsius
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Temperature.FromDegreesCelsius(dbValue);

            var result = unit == HealthMeasureUnitEnum.Fahrenheit ? total.DegreesFahrenheit : total.DegreesCelsius;

            return Math.Round(result, 2);
        }

        /// <summary>
        /// Returns the value in the specified measure unit
        /// </summary>
        /// <param name="unit">Target unit to convert(Fahrenheit or Celcius)</param>
        /// <param name="measureValue">Measure value in Celsius</param>
        /// <returns></returns>
        public static double GetValue(HealthMeasureUnitEnum unit, string measureValue = "0")
        {
            var dbValue = double.Parse(measureValue, CultureInfo.InvariantCulture);

            var total = Temperature.FromDegreesCelsius(dbValue);

            if (unit == HealthMeasureUnitEnum.Fahrenheit)
            {
                var result = Math.Round(total.DegreesFahrenheit, 2);

                return result;
            }
            else
            {
                var result = Math.Round(total.DegreesCelsius, 2);
                
                return result;
            }
        }

        /// <summary>
        /// Returns the total value in Celsius
        /// </summary>
        /// <param name="fromUnit">Measure unit source</param>
        /// <param name="left">left value</param>
        /// <returns></returns>
        public static string GetTotalInCelsius(HealthMeasureUnitEnum fromUnit, double left)
        {
            Temperature total;

            if (fromUnit == HealthMeasureUnitEnum.Fahrenheit)
            {
                total = Temperature.FromDegreesFahrenheit(left);
            }
            else
            {
                total = Temperature.FromDegreesCelsius(left);
            }

            return total.DegreesCelsius.ToString(CultureInfo.InvariantCulture);
        }
    }
}
