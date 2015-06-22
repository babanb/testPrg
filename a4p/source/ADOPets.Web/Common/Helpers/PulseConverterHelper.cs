using System;
using Model;

namespace ADOPets.Web.Common.Helpers
{
    public class PulseConverterHelper
    {
        public static string GetFormatedPulse(string leftValue)
        {
            var left = int.Parse(leftValue);
            var leftUnit = EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.BeatsPerMinute);

            return string.Format("{0} {1}", left, leftUnit);
        }
    }
}