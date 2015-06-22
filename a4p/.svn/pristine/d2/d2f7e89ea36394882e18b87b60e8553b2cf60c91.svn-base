using System.Globalization;
using Model;

namespace ADOPets.Web.Common.Helpers
{
    public static class HemogramConverterHelper
    {
        public static string GetFormatedHemogram(string leftValue)
        {
            var left = double.Parse(leftValue, CultureInfo.InvariantCulture);
            var leftUnit = EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.GramLiter);

            return string.Format("{0} {1}", left, leftUnit);
        }
    }
}
