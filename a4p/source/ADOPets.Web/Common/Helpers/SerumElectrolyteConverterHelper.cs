using System.Globalization;
using Model;

namespace ADOPets.Web.Common.Helpers
{
    public static class SerumElectrolyteConverterHelper
    {
        public static string GetFormatedSerumElectrolyte(string leftValue)
        {
            var left = double.Parse(leftValue, CultureInfo.InvariantCulture);
            var leftUnit = EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.GramLiter);

            return string.Format("{0} {1}", left, leftUnit);
        }
    }
}
