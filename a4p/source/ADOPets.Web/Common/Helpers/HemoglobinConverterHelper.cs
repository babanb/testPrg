using System.Globalization;
using Model;

namespace ADOPets.Web.Common.Helpers
{
    public static class HemoglobinConverterHelper
    {
        public static string GetFormatedHemoglobin(string leftValue)
        {
            var left = double.Parse(leftValue, CultureInfo.InvariantCulture);
            var leftUnit = EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Percent);

            return string.Format("{0} {1}", left, leftUnit);
        }
    }
}
